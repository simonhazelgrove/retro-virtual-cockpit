import React from "react";
import { useState } from "react";
import { useCookies } from 'react-cookie';
import connectKey from '../../utils/connectKey'
import { Form, Button, Row, Col } from 'react-bootstrap';

export interface ConnectorProps {
  onConnect: (connection: WebSocket) => void
}

export const Connector: React.FC<ConnectorProps> = (props: ConnectorProps) => {
  const [key, setKey] = useState("")
  const [cookies, setCookie] = useCookies(["connect-key"]);

  const connectClick = () => {
    setCookie("connect-key", key)
    const ip = connectKey.decode(key)
    const connection = new WebSocket("ws://" + ip + ":6437")

    connection.onopen = function () {
      props.onConnect(connection)
      // $("#connect-panel").slideToggle(1000);
      // $("#connected-icon").toggleClass("text-muted");
      // $("#connected-icon").toggleClass("text-warning");
      // $("#error-text").text("");
    };

    // // Log errors
    // this.connection.onerror = function (error) {
    //     log().logError("Connection error - is the client running?");
    // };

    // // Log messages from the server
    // this.connection.onmessage = function (e) {
    //     log().logError("Server: " + e.data);
    // };
  }

  const cookieKey = cookies["connect-key"]
  if (cookieKey && !key) {
    setKey(cookieKey)
  }

  return <div className="row dark-well">
    <div className="col-md-12 centered">
      <Row>
        <Col md="auto">
          <Form.Group>
            <Form.Label visuallyHidden>Connection Key:</Form.Label>
            <Form.Control type="text" placeholder="XXXXXXXX" className="uppercase" size="lg" value={key} onChange={e => setKey(e.target.value)}/>
          </Form.Group>
        </Col>
        <Col md="auto">
          <Button variant="dark" size="lg" type="submit" onClick={connectClick}>Connect</Button>
        </Col>
      </Row>
    </div>
  </div>
}