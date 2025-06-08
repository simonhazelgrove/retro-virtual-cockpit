import React from "react";
import { CockpitConfig, Panel as PanelType, PanelRow } from '../../types'
import { Panel } from "./panel"

interface CockpitProps {
  config: CockpitConfig,
  onSendMessage: (message: string) => void
}

export const Cockpit: React.FC<CockpitProps> = (props: CockpitProps) => {
  const panelRows = props.config.panelRows.map((panelRow: PanelRow, rowid: number) => 
    <div className="control-panel-row" key={rowid}>
      {panelRow.panels.map((panel: PanelType) => <Panel key={panel.id} panel={panel} onSendMessage={props.onSendMessage} />)}
    </div>
  )

  return <div id="virtual-cockpit">{panelRows}</div>
}