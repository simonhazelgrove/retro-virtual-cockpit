import React from "react";
import { useState } from "react";
import { Form, Dropdown } from 'react-bootstrap'
import { GearFill, LightningFill, Fullscreen, InfoCircleFill, MoonFill } from 'react-bootstrap-icons';
import { CockpitConfig } from "../../../types";
import cockpit_configs from '../../../configs/cockpit-config.js'

export interface MenuProps {
  onConfigChange: (config: CockpitConfig) => void,
  onToggleConnector: () => void,
  onToggleDebug: () => void,
  onToggleFullscreen: () => void
  onToggleNightMode: () => void
  isConnected: boolean
  isNightMode: boolean
}

export const Menu: React.FC<MenuProps> = (props: MenuProps) => {
  const configs = cockpit_configs()
  const [config, setConfig] = useState<CockpitConfig>()
  const [halfScale, setHalfScale] = useState(false)

  const getConnectedIconClass = ():string => {
    if (props.isConnected) {
      return props.isNightMode ? "text-muted" : "text-warning"
    } else {
      return props.isNightMode ? "text-dark" : "text-muted"
    }
  }

  const connectedIconClass = getConnectedIconClass() + " large-icon"

  const configSelect = (event: any) => {
    const title = event.target.value
    const selectedConfig: CockpitConfig | undefined = configs.find((config: CockpitConfig): boolean => config.title === title)
    if (selectedConfig) {
      setConfig(selectedConfig)
      props.onConfigChange(selectedConfig)
    }
  }

  const configOptions = configs.map((config: CockpitConfig, id: number) => <option value={config.title} key={id}>{config.title}</option>)

  const connectionClick = () => props.isConnected && props.onToggleConnector()

  const clickHalfScale = () => {
    let viewport:HTMLElement|null = document.querySelector("meta[name=viewport]");
    if (!viewport) {
        // in case there is no view port meta tag creates one and add it to the head
        viewport = document.createElement('meta')
        viewport.setAttribute("name", "viewport")
        document.getElementsByTagName("head")[0].appendChild(viewport)
    }
    
    const content = halfScale 
      ? 'width=device-width, initial-scale=1'
      : 'width=device-width, initial-scale=0.75, maximum-scale=0.75, minimum-scale=0.75'
    // this is where the magic happens by changing the vewport meta tag
    viewport.setAttribute("content", content)
    setHalfScale(!halfScale)
  }

  return <div className="row dark-well">
    <div className="col-1">
      <LightningFill className={connectedIconClass} onClick={connectionClick} />
    </div>
    <div className="col-9 text-center">
      <Form.Select aria-label="Select configuration..." 
        className="btn btn-dark btn-lg dropdown-toggle text-muted"
        onChange={configSelect}>
        {!config && <option>Select configuration...</option>}
        {configOptions}
      </Form.Select>
      <span id="error-text" className="error"></span>
      <span id="messageCode" className="invisible"></span>
    </div>
    <div className="col-1">
      <Dropdown autoClose="outside">
        <Dropdown.Toggle variant="dark">
          <GearFill className="large-icon text-muted" />
        </Dropdown.Toggle>
        <Dropdown.Menu variant="dark">
          <Dropdown.Item href="#/fullscreen" className="text-muted">
            <Fullscreen />&nbsp; 
            <Form.Check inline type="switch" label="Fullscreen" id="fullscreenToggle" onClick={props.onToggleFullscreen} />
          </Dropdown.Item>
          <Dropdown.Item href="#/debug" className="text-muted">
            <InfoCircleFill />&nbsp; 
            <Form.Check inline type="switch" label="Show Commands" id="debugConsoleToggle" onClick={props.onToggleDebug} />
          </Dropdown.Item>
          <Dropdown.Item href="#/nightmode" className="text-muted">
            <MoonFill />&nbsp; 
            <Form.Check inline type="switch" label="Night Mode" id="nightModeToggle" onClick={props.onToggleNightMode} />
          </Dropdown.Item>
          <Dropdown.Item href="#/halfscale" className="text-muted">
            <MoonFill />&nbsp; 
            <Form.Check inline type="switch" label="Half Scale" id="halfScale" onClick={clickHalfScale} />
          </Dropdown.Item>
        </Dropdown.Menu>
      </Dropdown>
    </div>
  </div>
}