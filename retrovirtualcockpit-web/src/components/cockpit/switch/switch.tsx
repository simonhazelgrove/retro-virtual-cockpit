import React, { useState } from "react";
import { Switch as SwitchType } from '../../../configs'
import switchSound from '../../../sounds/switch.wav'

interface SwitchProps {
  switch: SwitchType
  onSendMessage: (message: string) => void
}

export const Switch: React.FC<SwitchProps> = (props: SwitchProps) => {
  const [state, setState] = useState("off")

  var slotClassNames = "control-slot"
  const switchClassNames = `control switch ${state}`

  const flip = () => {
    setState(state === "on" ? "off" : "on")

    if (props.switch.flip) {
      props.onSendMessage(props.switch.flip)
    }
    else if (state === "on" && props.switch.on) {
      props.onSendMessage(props.switch.on)
    } else if (state === "off" && props.switch.off)
    {
      props.onSendMessage(props.switch.off)
    }

    new Audio(switchSound).play()
  }

  if (props.switch.decoration === "hazard") {
    slotClassNames += ` ${props.switch.decoration}`
  }

  return <div className={slotClassNames}>
    <div className={switchClassNames} onClick={flip}></div>
    {props.switch.label &&
      <p dangerouslySetInnerHTML={{__html: props.switch.label}} />
    }
  </div>
}