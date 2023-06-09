import React from "react";
import { 
  Control, 
  Panel as PanelType, 
  Button as ButtonType, 
  DoubleButton as DoubleButtonType, 
  DPadButton as DPadButtonType, 
  Switch as SwitchType,
  Knob as KnobType,
  Handle as HandleType,
  Throttle as ThrottleType,
  SubPanel as SubPanelType,
  Lever as LeverType
} from '../../../configs'
import { Label } from "../label"
import { Button } from "../button"
import { DoubleButton } from "../doubleButton"
import { DPadButton } from "../dPadButton"
import { Switch } from "../switch"
import { Knob } from "../knob"
import { Handle } from "../handle"
import { Throttle } from "../throttle"
import { Lever } from "../lever"

interface PanelProps {
  panel: PanelType,
  onSendMessage: (message: string) => void
}

export const Panel: React.FC<PanelProps> = (props: PanelProps) => {
  const generateControls = (controls: Control[], panelId: string) => controls.map((control: Control, id: number) => {
    const controlId = `${panelId}Control${id}`
    switch (control.type) {
      case "label": 
        return <Label label={control} key={controlId} />
      case "button": 
      case "button-red": 
        return <Button button={control as ButtonType} key={controlId} onSendMessage={props.onSendMessage} />
      case "button-h": 
        return <DoubleButton button={control as DoubleButtonType} key={controlId} onSendMessage={props.onSendMessage} orientation="horizontal" /> 
      case "button-v": 
        return <DoubleButton button={control as DoubleButtonType} key={controlId} onSendMessage={props.onSendMessage} orientation="vertical" /> 
      case "button-dpad": 
        return <DPadButton button={control as DPadButtonType} key={controlId} onSendMessage={props.onSendMessage} /> 
      case "switch": 
        return <Switch switch={control as SwitchType} key={controlId} onSendMessage={props.onSendMessage} /> 
      case "lever": 
        return <Lever lever={control as LeverType} key={controlId} onSendMessage={props.onSendMessage} /> 
      case "knob": 
        return <Knob knob={control as KnobType} key={controlId} onSendMessage={props.onSendMessage} /> 
      case "handle": 
        return <Handle handle={control as HandleType} key={controlId} onSendMessage={props.onSendMessage} />
      case "throttle": 
        return <Throttle throttle={control as ThrottleType} key={controlId} onSendMessage={props.onSendMessage} />
      case "subpanel":
        return <div key={controlId} className="control-panel">{generateControls((control as SubPanelType).controls, "controlId")}</div>
    }
    console.error(`Control ${controlId} is of unknown type: ${control.type}`)
    return <div key={controlId}></div>;
  })  

  const controls = generateControls(props.panel.controls, props.panel.id)

  var classNames = "control-panel"
  if (props.panel.orientation) {
    classNames += ` ${props.panel.orientation}`
  } 
  if (props.panel.align) { 
    classNames += ` pull-${props.panel.align}`
  } 

  return <div className={classNames}>
    {(!props.panel.decoration || props.panel.decoration !== "none") && 
      <>
        <span className="screw top-left"></span>
        <span className="screw bottom-left"></span>
      </>
    }
    {props.panel.title && <h1>{props.panel.title}</h1>
    }    
    {controls}
    {(!props.panel.decoration || props.panel.decoration !== "none") && 
      <>
        <span className="screw top-right"></span>
        <span className="screw bottom-right"></span>
      </>
    }
  </div>
}