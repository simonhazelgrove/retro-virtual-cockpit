import React from "react";
import { Button as ButtonType } from '../../../configs'

interface ButtonProps {
  button: ButtonType,
  onSendMessage: (message: string) => void
}

export const Button: React.FC<ButtonProps> = (props: ButtonProps) => {
  var slotClassNames = "control-slot"
  var buttonClassNames = "control"
  const press = () => {
    props.onSendMessage(props.button.press)
    new Audio("sounds/bump.wav").play()
  }
  if (props.button.decoration === "hazard") {
    slotClassNames += ` ${props.button.decoration}`
  }
  if (props.button.type === "button") {
    buttonClassNames += " button"
  }
  else if (props.button.type === "button-red") {
    buttonClassNames += " button-red"
  }
  return <div className={slotClassNames}>
    <div className={buttonClassNames} onClick={press}></div>
    {props.button.label &&
      <p dangerouslySetInnerHTML={{__html: props.button.label}} />
    }
  </div>
}