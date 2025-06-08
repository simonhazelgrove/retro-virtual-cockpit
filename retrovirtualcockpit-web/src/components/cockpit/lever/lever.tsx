import React from "react";
import { Lever as LeverType } from '../../../types'
import { motion, useAnimation } from "framer-motion"
import lever from '../../../sounds/lever.wav'

interface LeverProps {
  lever: LeverType,
  onSendMessage: (message: string) => void
}

export const Lever: React.FC<LeverProps> = (props: LeverProps) => {
    var slotClassNames = "control-slot"
    var controlClassNames = "control lever"
    var handleClassNames = "lever-handle"

    const throwAmount = 54; // Pixels the lever moves

    var upYPos = -throwAmount;
    var downYPos = 0;

    if (props.lever.multistage) {
        controlClassNames += " multistage"
    }

    if (props.lever.altColor) {
        controlClassNames += " alt-color"
    }

    if (props.lever.startInUpPos) {
        handleClassNames += " start-in-up-pos"
        upYPos = 0;
        downYPos = throwAmount;
    }

    const anim = useAnimation()

    const maxHandleMove = 50
    
    const swipe = async (swipeY: number) => {
        if(swipeY >= maxHandleMove) {
            props.onSendMessage(props.lever.down)
            new Audio(lever).play()
        } else if (swipeY <= -maxHandleMove) {
            props.onSendMessage(props.lever.up)
            new Audio(lever).play()
        }

        if (props.lever.multistage) {
            // Return lever to middle pos
            await anim.start({ y: 0 })
        } else {
            // Return lever to either top or bottom
            const newYPos = swipeY < 0 ? upYPos : downYPos
            await anim.start({ y: newYPos })
        }
    }
  
    return <div className={slotClassNames}>
        <div className={controlClassNames}>
        <div className="lever-base" />
        <motion.div className={handleClassNames}
            drag="y"
            dragConstraints={{ top: 0, bottom: maxHandleMove }}
            dragElastic={0.2}
            onDragEnd={async (e, info) => { swipe(info.offset.y) }}
            animate={anim}
            />
        </div>
        {props.lever.label &&
            <p dangerouslySetInnerHTML={{__html: props.lever.label}} />
        }
    </div>
}
  
