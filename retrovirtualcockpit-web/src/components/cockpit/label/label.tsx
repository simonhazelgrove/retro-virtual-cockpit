import React from "react";
import { Control as ControlType } from '../../../configs'

interface LabelProps {
  label: ControlType
}

export const Label: React.FC<LabelProps> = (props: LabelProps) => {
  var slotClassNames = "control-slot"
  if (props.label.decoration === "hazard") {
    slotClassNames += ` ${props.label.decoration}`
  }
  return <div className={slotClassNames}>
    <div className="label"></div>
    {props.label.label &&
      <p dangerouslySetInnerHTML={{__html: props.label.label}} />
    }
  </div>
}