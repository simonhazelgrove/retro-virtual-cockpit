import React, { useState } from "react";
import { Knob as KnobType } from '../../../configs'

interface KnobProps {
  knob: KnobType,
  onSendMessage: (message: string) => void
}

interface KnobValue {
  ctrl: string,
  label: string,
  angle: number
}

const getValueAngle = (index: number, count: number): number => {
  if (count === 4) {
    // Four values, 90 degrees apart, offset -45 degrees
    return (90 * index) - 45
  } else {
    // Otherwise 45 degrees apart, starting at 45, and ignoring 180 (only values to left or right of center)
    const onLeftSideOffset = index >= 3 ? 45 : 0
    return 45 + (45 * index) + onLeftSideOffset
  }
}

export const Knob: React.FC<KnobProps> = (props: KnobProps) => {
  var values:KnobValue[] = props.knob.values && props.knob.values.map(([ctrl, label], index: number) => {
    const angle = getValueAngle(index, props.knob.values.length)
    return {ctrl, label, angle}
  })

  const [rotation, setRotation] = useState(values ? values[0].angle : 0)
  const [valueIndex, setValueIndex] = useState(0)

  var slotClassNames = "control-slot"
  var knobClassNames = "control knob"

  var leftValueLabels = null;
  var rightValueLabels = null;

  if (props.knob.values) {
    knobClassNames += " inner-control-column"

    var splitIndex = 0
    if (props.knob.values.length === 4) {
      splitIndex = 2
    } else if (props.knob.values.length >= 5) {
      splitIndex = 3
    }

    rightValueLabels = props.knob.values.map(([ctrl, label], index: number) => {
      return (index < splitIndex) &&
        <li key={index}>
          {label}<br/>
        </li>
    })

    leftValueLabels = props.knob.values.map(([ctrl, label], index: number) => {
      return (index >= splitIndex) &&
        <li key={index}>
          {label}<br/>
        </li>
    })
    leftValueLabels.reverse()
  }

  const turn = () => {
    if (values) {
      // Cycle through values
      var i = valueIndex + 1;
      if (i === values.length) {
        i = 0
      }
      props.onSendMessage(values[i].ctrl)
      setRotation(values[i].angle)
      setValueIndex(i)
    } else {
      // No set values, just keep rotating
      props.onSendMessage(props.knob.turn)
      setRotation(rotation + 20)
    }

    new Audio("sounds/knob.wav").play()
  }

  if (props.knob.decoration === "hazard") {
    slotClassNames += ` ${props.knob.decoration}`
  }

  return <div className={slotClassNames}>
    <div>
      {leftValueLabels &&
        <div className="inner-control-column">
          <ul>{leftValueLabels}</ul>
        </div>
      }

      <div className={knobClassNames} style={{transform:`rotate(${rotation}deg)`}} onClick={turn}></div>

      {rightValueLabels &&
        <div className="inner-control-column">
          <ul>{rightValueLabels}</ul>
        </div>
      }
    </div>
    {props.knob.label &&
      <p dangerouslySetInnerHTML={{__html: props.knob.label}} />
    }
  </div>
}