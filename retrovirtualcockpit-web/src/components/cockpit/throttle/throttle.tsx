import React from "react"
import { motion, useAnimation } from "framer-motion"
import { Throttle as ThrottleType } from '../../../types'

interface ThrottleProps {
  throttle: ThrottleType,
  onSendMessage: (message: string) => void
}

export const Throttle: React.FC<ThrottleProps> = (props: ThrottleProps) => {
  var slotClassNames = "control-slot"
  var handleClassNames = "control throttle"

  const anim = useAnimation()

  const maxHandleMove = 50

  var doubleTapIndex = 0
  var doubleTapTimer = setTimeout(() => {}, 0)

  const minOrMax = async (doubleTapClientY: number, target: any) => {
    const currentTargetRect = target.getBoundingClientRect()
    const clickY = doubleTapClientY - currentTargetRect.top
    const thirdY = target.clientHeight / 3
    const posY = Math.floor(clickY / thirdY)

    if (posY === 0 && props.throttle.max) {
      props.onSendMessage(props.throttle.max)
      await anim.start({ y: -maxHandleMove })
      await anim.start({ y: 0 })
    } else if (posY === 2 && props.throttle.min) {
      props.onSendMessage(props.throttle.min)
      await anim.start({ y: maxHandleMove })
      await anim.start({ y: 0 })
    }
  }

  const tap = async (event: any, info: any) => {
    if (doubleTapIndex === 1) {
      // Double tap happened
      clearTimeout(doubleTapTimer)
      await minOrMax(info.point.y - window.scrollY, event.target)
    } else if (doubleTapIndex <= 1) {
        doubleTapIndex = doubleTapIndex + 1

        // Start a timer to cancel the double tap after 1s
        doubleTapTimer = setTimeout(() => {
            doubleTapIndex = 0
            doubleTapTimer = setTimeout(() => {}, 0)
        }, 500)
    }
  }

  const swipe = async (swipeY: number) => {
    if(swipeY >= maxHandleMove) {
      props.onSendMessage(props.throttle.down)
    } else if (swipeY <= -maxHandleMove) {
      props.onSendMessage(props.throttle.up)
    }
    await anim.start({ y: 0 })
  }

  if (props.throttle.decoration === "hazard") {
    slotClassNames += ` ${props.throttle.decoration}`
  }

  return <div className={slotClassNames}>
    <div className={handleClassNames}>
      <motion.div 
        className="throttle-base"
        onTap={tap}
      />
      <motion.div className="throttle-handle"
        drag="y"
        dragConstraints={{ top: 0, bottom: maxHandleMove }}
        dragElastic={0.2}
        onDragEnd={async (e, info) => { swipe(info.offset.y) }}
        onTap={tap}
        animate={anim}
        />
    </div>
  </div>
}