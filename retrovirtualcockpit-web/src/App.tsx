import { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import { CockpitConfig } from './configs'
import { Cockpit } from './components/cockpit'
import { Header } from './components/header'
import { Connector } from './components/connector'
import { DebugConsole } from './components/debugConsole'

function App() {
  const [config, setConfig] = useState<CockpitConfig>()
  const [connection, setConnection] = useState<WebSocket>()
  const [showConnector, setShowConnector] = useState(true)
  const [showDebug, setShowDebug] = useState(false)
  const [nightMode, setNightMode] = useState(false)
  const [debugMessages, setDebugMessages] = useState<string[]>([])

  const onConfigChanged = (selectedConfig: CockpitConfig) => {
    setConfig(selectedConfig)
    onSendMessage("SetConfig:" + selectedConfig.title)
  }

  const onConnect = (connection: WebSocket) => {
    setConnection(connection)
    setShowConnector(false)
  }

  const onToggleConnector = () => {
    setShowConnector(!showConnector)
  }

  const toggleDebug = () => {
    setShowDebug(!showDebug)
  }

  const toggleFullscreen = () => {

    var doc = window.document;
    var docEl = doc.documentElement;

    var requestFullScreen = docEl.requestFullscreen
    var cancelFullScreen = doc.exitFullscreen
 
    if(!doc.fullscreenElement) {
        requestFullScreen.call(docEl)
    } else {
        cancelFullScreen.call(doc)
    }
  }

  const toggleNightMode = () => {
    setNightMode(!nightMode)
  }

  useEffect(() => {
    if (!showDebug) {
      setDebugMessages([])
    }
  }, [showDebug])

  useEffect(() => {
    if (nightMode) {
      document.body.classList.add('night');
    } else {
      document.body.classList.remove('night');
    }
  })

  const onSendMessage = (message: string) => {
    if (connection) {
      connection.send(message)
    }
    if (showDebug) {
      setDebugMessages([...debugMessages, message])
    }
  }

  return (
    <div className="App">
      {showConnector && <Connector onConnect={onConnect} />}
      <Header 
        isConnected={connection !== undefined} 
        isNightMode={nightMode}
        onConfigChange={onConfigChanged} 
        onToggleConnector={onToggleConnector} 
        onToggleDebug={toggleDebug}
        onToggleFullscreen={toggleFullscreen}
        onToggleNightMode={toggleNightMode} />
      {config && <Cockpit config={config} onSendMessage={onSendMessage } />}
      <DebugConsole messages={debugMessages} />
    </div>
  );
}

export default App;
