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
        <li>Run the server app on the same Windows computer you are running your game on. You can get it by either:<br />
            - Build it yourself from the <a href={gitHubUrl} target="_blank">GitHub repository</a><span className="emoji">🔗↗️</span><br />
            &nbsp;&nbsp;or<br />
            - Download a <a href={downloadURL} target="_blank">pre-built version</a><span className="emoji">🔗↗️</span><br />
        </li>
        <li>Note the code you see generated in the server app e.g:<br/><br/>
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