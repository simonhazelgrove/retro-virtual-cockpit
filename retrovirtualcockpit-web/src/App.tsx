import { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import { CockpitConfig, Message } from './types'
import { Cockpit } from './components/cockpit'
import { Header } from './components/header'
import { Connector } from './components/connector'
import { DebugConsole } from './components/debugConsole'
import Download from './components/download/download';

function App() {
  const [config, setConfig] = useState<CockpitConfig>()
  const [connection, setConnection] = useState<WebSocket>()
  const [showConnector, setShowConnector] = useState(true)
  const [showDebug, setShowDebug] = useState(false)
  const [nightMode, setNightMode] = useState(false)
  const [debugMessages, setDebugMessages] = useState<(string | Message)[]>([])

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

  const onSendMessage = (message: string | Message[]) => {
    if (connection) {
      if (typeof message === 'string' || message instanceof String) {
        connection.send(message.toString())
      }
      else {
        connection.send(JSON.stringify(message))
      }
    }
    if (showDebug) {
      if (typeof message === 'string') {
        setDebugMessages([...debugMessages, message])
      } else {
        // If the message is an array, we assume it's a list of messages
        setDebugMessages([...debugMessages, ...message])
      }
    }
  }

  return (
    <div className="App">
      <Header 
        isConnected={connection !== undefined} 
        isNightMode={nightMode}
        onConfigChange={onConfigChanged} 
        onToggleConnector={onToggleConnector} 
        onToggleDebug={toggleDebug}
        onToggleFullscreen={toggleFullscreen}
        onToggleNightMode={toggleNightMode} />
      {showConnector && <Connector onConnect={onConnect} />}
      {config && <Cockpit config={config} onSendMessage={onSendMessage } />}
      {!config && <Download />}
      <DebugConsole messages={debugMessages} />
    </div>
  );
}

export default App;
