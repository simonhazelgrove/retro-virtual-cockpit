import React, { MouseEvent } from "react"
import { DPadButton as DPadButtonType } from '../../../configs'

interface DPadButtonProps {
  button: DPadButtonType,
  onSendMessage: (message: string) => void
}

export const DPadButton: React.FC<DPadButtonProps> = (props: DPadButtonProps) => {
  var slotClassNames = "control-slot"
  var buttonClassNames = "control button-dpad"

  const click = (e: MouseEvent<HTMLDivElement>) => {
    const currentTargetRect = e.currentTarget.getBoundingClientRect()
    const clickX = e.pageX - currentTargetRect.left
    const clickY = e.pageY - currentTargetRect.top
    const thirdX = e.currentTarget.clientWidth / 3
    const thirdY = e.currentTarget.clientHeight / 3
    const posX = Math.floor(clickX / thirdX)
    const posY = Math.floor(clickY / thirdY)

    if (posY === 0 && props.button.up) {
      props.onSendMessage(props.button.up)
    } else if (posY === 2 && props.button.down) {
      props.onSendMessage(props.button.down)
    }
    if (posX === 0 && props.button.left) {
      props.onSendMessage(props.button.left)
    } else if (posX === 2 && props.button.right) {
      props.onSendMessage(props.button.right);
    }
    new Audio("sounds/bump.wav").play()
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