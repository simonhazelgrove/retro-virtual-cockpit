import React, { ChangeEvent } from "react";
import { useState } from "react";
import { useCookies } from 'react-cookie';
import connectKey from '../../utils/connectKey'
import { Form, Button, Row, Col } from 'react-bootstrap';

export interface ConnectorProps {
  onConnect: (connection: WebSocket) => void
}

export const Connector: React.FC<ConnectorProps> = (props: ConnectorProps) => {
  const [cookies, setCookie] = useCookies(["connect-key"]);
  const cookieKey = cookies["connect-key"]

  const [key, setKey] = useState(cookieKey)
  const [submitEnabled, setSubmitEnabled] = useState(isKeyValid(cookieKey))

  const connectClick = () => {
    setCookie("connect-key", key)
    const ip = connectKey.decode(key)
    const connection = new WebSocket("ws://" + ip + ":6437")

    connection.onopen = function () {
      props.onConnect(connection)
    };
  }

  function isKeyValid(newKey: string): boolean {
    return newKey !== undefined && newKey.length === 8 && /^[A-Z0-9]+$/.test(newKey);
  }

  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    var newKey = event.target.value.toUpperCase()
    setKey(newKey)
    setSubmitEnabled(isKeyValid(newKey))
  };

  return <div className="row dark-well">
    <div className="col-md-12 centered">
      <Row>
        <Col md="auto">
          <Form.Group>
            <Form.Label visuallyHidden>Connection Key:</Form.Label>
            <Form.Control required type="text" placeholder="XXXXXXXX" size="lg" defaultValue={cookieKey} onChange={handleChange} className="uppercase"/>
          </Form.Group>
        </Col>
        <Col md="auto">
          <Button variant="dark" size="lg" type="submit" disabled={!submitEnabled} onClick={connectClick}>Connect</Button>
        </Col>
      </Row>
    </div>
  </div>
}
