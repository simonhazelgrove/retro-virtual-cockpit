import Accordion from 'react-bootstrap/Accordion'
import download1 from '../../images/download1.png'
import download2 from '../../images/download2.png'
import download3 from '../../images/download3.png'
import download4 from '../../images/download4.png'
import download5 from '../../images/download5.png'
import download6 from '../../images/download6.png'
import whatIsThis from '../../images/WhatIsThis.png'

export default function Download() {
  const downloadURL = process.env.REACT_APP_SERVER_DOWNLOAD_URL
  const gitHubUrl = process.env.REACT_APP_GITHUB_REPO_URL

  return (
    <div className="instructions">
      <h1>What is this?</h1>
      <p>The Retro Virtual Cockpit lets you control any game with too many key controls to remember (e.g. old flight sims) from a nice cockpit UI
        running on a touch-screen device connected to the same local network as your game computer.
      </p>
      <img src={whatIsThis} id="whatisthis" className="img-fluid" alt="Diagram showing an iPad controlling a game on Windows" />
      <h1>Instructions</h1>
      <ol>
        <li>Run the server app on the same Windows <span className="emoji">🪟🖥️</span> computer you are running your game on. You can get it by either:<br />
            - Build it yourself from the <a href={gitHubUrl}>GitHub repository</a><br />
            &nbsp;&nbsp;or<br />
            - Download a <a href={downloadURL}>pre-built version</a><br />
            <Accordion flush className="warning">
              <Accordion.Item eventKey="0">
                <Accordion.Header>
                    <div><span className="emoji">⚠️</span> This is a hobby project, no expense has been spent on security.  You will need to ignore 
                    some browser & OS security warnings to download and run the app.  You do this at your own risk.</div>
                    <button className="button">Download instructions</button>
                </Accordion.Header>
                <Accordion.Body>
                  <p>First three steps are specific to Chrome browser - other browsers will be similar:</p>
                  <ol>
                    <li>Click 'Keep' on the warning message<br />
                      <img src={download1} className="img-fluid" alt="Screenshot of Chrome browser's insecure download message" /></li>
                    <li>Click the arrow next to the file name in the downloads bar<br />
                      <img src={download2} className="img-fluid" alt="Screenshot of Chrome browser's suspicious download message" /></li>
                    <li>Click the 'Download suspicious file' button<br />
                      <img src={download3} className="img-fluid" alt="Screenshot of Chrome browser's final warning about the download" /></li>
                    <li>The .zip file will download, extract to your chosen location</li>
                    <li>Run RetroVirtualCockpit.Server.exe</li>
                    <li>Click 'More info'<br />
                      <img src={download4} className="img-fluid" alt="Screenshot of Windows dialog about unsecure application" /></li>
                    <li>Click 'Run anyway'<br />
                      <img src={download5} className="img-fluid" alt="Screenshot of Windows dialog and its 'Run Anyway' button" /></li>
                    <li>Click 'Allow' to open Windows Firewall to allow the app to receive calls from this website<br />
                      <img src={download6} className="img-fluid" alt="Screenshot of Windows prompt to allow the app access to the network" /></li>
                  </ol>
                </Accordion.Body>
              </Accordion.Item>
            </Accordion>
        </li>
        <li><span className="emoji">👀</span> Note the code you see generated in the server app e.g:<br/><br/>
          <div className="console"><code>Server has started on 192.168.1.22:6437.<br/>
          Enter this connection key at Retro Virtual Cockpit website: '<mark>MQQEMDTO</mark>'.<br/>
          Waiting for a connection...</code></div>
        </li>
        <li>Open this website on a touch-screen device on the same local network as your windows computer</li>
        <li>Type the code into the box in the top left of the page on your device, click Connect button</li>
        <li>Select a game configuration to show the cockpit controls</li>
        <li><span className="emoji">🕹️</span> Control your game via the virtual cockpit on your device<span className="emoji">✈️</span></li>
      </ol>
    </div>
  );
}