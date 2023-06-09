import { useState, useEffect } from 'react'
import { Toast, ToastContainer } from 'react-bootstrap'
import styles from "./debugConsole.module.css"

export interface DebugProps {
  messages: string[]
}

export const DebugConsole: React.FC<DebugProps> = (props: DebugProps) => {

  const [startFrom, setStartFrom] = useState(0)

  useEffect(() => {
    if (props.messages.length === 0) {
      setStartFrom(0)
    }
  }, [props.messages])

  return <div
      aria-live="polite"
      aria-atomic="true"
      className={`${styles.toastContainer} position-sticky`}
      style={{ bottom: "0px" }}
    >
    <ToastContainer>
      {props.messages.map((message, idx) => (
        idx >= startFrom && 
        <Toast bg="dark" className={styles.toast} autohide delay={3000} key={idx} onClose={() => setStartFrom(idx + 1)}>
          <Toast.Body className={styles.toastBody}>
            {message}
          </Toast.Body>
        </Toast>
      ))}
    </ToastContainer>
  </div>
}