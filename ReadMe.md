# Retro Virtual Cockpit

Retro Virtual Cockpit is a tool designed to make playing retro video games with lots of key bindings (e.g. flight sims) easier.

The client program takes inputs from several different sources and translates them into keypresses that games running in an emulator will understand.

The different sources of input include:

- Cockpit UI - the original Retro Virtual Cockpit was designed as a web ui that looks like buttons and switches in a cockpit.  This is a website that you can access from any device on your network e.g. tablets or mobile phones. 
- Joystick device - any connected DirectInput device can have any of its controls mapped to either keypresses or mouse movements.

## Repo Items

### RetroVirtualCockpit.Server

Console app that reads messages from receivers (e.g. client web app, mouse, joystick) translates them into GameActions, and replays commands into the host machine via either keyboard or mouse actions.

#### Architecture

Diagram created in https://asciiflow.com/#/local/RetroVirtualCockpit

                 ┌───────────────────┐
            ┌────┤ WebClientReceiver │
            │    └──┬────────────────┴─┐
Receives    ├───────┤ JoystickReceiver │
 event      │       └──┬───────────────┤
messages    ├──────────┤ MouseReceiver │
            │          └───────────────┘
            │    ┌───────────────────────────┐
            └───►│ RetroVirtualCockpitServer | Translates received messages into GameActions:
                 └─────┬─────────────────────┘ sequences of messages for keyboard & mouse
                       ▼        
             ┌───────────────────┐
             │ MessageDispatcher ├────────┐
             └───────────────────┘        │  Sends
                                          │ messages
             ┌────────────────────┐       │
             │ KeyboardDispatcher │◄──────┤
             └──┬─────────────────┤       │
                │ MouseDispatcher │◄──────┘
                └─────────────────┘


### RetroVirtualCockpit.InputTester

This program will output the state of any Direct Input controller changes.  Use it to identify buttons, sliders, axis etc on a controller.

### retrovirtualcockpit-web

Web SPA client that provides buttons and other controls that send commands to the server


## Attributions

- lever.wav - modified from https://freesound.org/people/BMacZero/sounds/94120/
