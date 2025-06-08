import React from "react"
import { motion, useAnimation } from "framer-motion"
import { Handle as HandleType } from '../../../types'

interface HandleProps {
  handle: HandleType,
  onSendMessage: (message: string) => void
}

export const Handle: React.FC<HandleProps> = (props: HandleProps) => {
  var slotClassNames = "control-slot centered large control"
  var handleClassNames = "control"

  const anim = useAnimation()

  const maxHandleMove = 50

  if (props.handle.decoration === "hazard") {
    slotClassNames += ` ${props.handle.decoration}`
  }

  return <motion.div className={slotClassNames} 
    drag="y"
    dragConstraints={{ top: 0, bottom: maxHandleMove }}
    dragElastic={0.2}
    animate={anim}
    onDragEnd={async (e, info) => {
      if(info.offset.y >= maxHandleMove) {
        props.onSendMessage(props.handle.pull)
      }
      await anim.start({ y: 0 })
    }}
    >
    <div className={handleClassNames}>
      {props.handle.label &&
        <p dangerouslySetInnerHTML={{__html: props.handle.label}} />
      }
    </div>
  </motion.div>
}