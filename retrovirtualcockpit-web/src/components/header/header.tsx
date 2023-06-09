import React from "react";
import { Menu } from "./menu"
import { CockpitConfig } from '../../configs'

export interface HeaderProps {
  onConfigChange: (config: CockpitConfig) => void
  onToggleConnector: () => void
  onToggleDebug: () => void
  onToggleFullscreen: () => void
  onToggleNightMode: () => void
  isConnected: boolean
  isNightMode: boolean
}

export const Header: React.FC<HeaderProps> = (props: HeaderProps) => {
  return <div className="container-fluid body-content">
    <Menu 
      isConnected={props.isConnected} 
      isNightMode={props.isNightMode}
      onConfigChange={props.onConfigChange} 
      onToggleConnector={props.onToggleConnector}
      onToggleDebug={props.onToggleDebug}
      onToggleFullscreen={props.onToggleFullscreen}
      onToggleNightMode={props.onToggleNightMode} />
  </div>
}