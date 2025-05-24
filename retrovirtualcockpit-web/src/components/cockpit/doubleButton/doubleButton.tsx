import React, { MouseEvent } from "react"
import { DoubleButton as DoubleButtonType } from '../../../configs'
import bump from '../../../sounds/bump.wav'

interface DoubleButtonProps {
  button: DoubleButtonType,
  orientation: string,
  onSendMessage: (message: string) => void
}

export const DoubleButton: React.FC<DoubleButtonProps> = (props: DoubleButtonProps) => {
  var slotClassNames = "control-slot"
  var buttonClassNames = props.orientation === "horizontal" ? "control button-h" : "control button-v"

  const click = (e: MouseEvent<HTMLDivElement>) => {
    const currentTargetRect = e.currentTarget.getBoundingClientRect()

    if (props.orientation === "horizontal") {
      const clickX = e.pageX - currentTargetRect.left
      const middleX = e.currentTarget.clientWidth / 2
      
      if (clickX >= middleX && props.button.right) {
        props.onSendMessage(props.button.right)
      } else if (clickX < middleX && props.button.left) {
        props.onSendMessage(props.button.left)
      }   
    } else if (props.orientation === "vertical") {
      const clickY = e.pageY - currentTargetRect.top
      const middleY = e.currentTarget.clientHeight / 2
      
      if (clickY >= middleY && props.button.down) {
        props.onSendMessage(props.button.down)
      } else if (clickY < middleY && props.button.up) {
        props.onSendMessage(props.button.up)
      }   
    }
    new Audio(bump).play()
  }

  if (props.button.decoration === "hazard") {
    slotClassNames += ` ${props.button.decoration}`
  }

  return <div className={slotClassNames}>
    <div className={buttonClassNames} onClick={click}></div>
    {props.button.label &&
      <p dangerouslySetInnerHTML={{__html: props.button.label}} />
    }
  </div>
}