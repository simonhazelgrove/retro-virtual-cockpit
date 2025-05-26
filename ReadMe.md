# Introductions

Retro Virtual Cockpit is a tool designed to make playing retro video games with lots of key bindings (e.g. flight sims) easier.

The client program takes inputs from several different sources and translates them into keypresses that games running in an emulator will understand.

The different sources of input include:

- Cockpit UI - the original Retro Virtual Cockpit was designed as a web ui that looks like buttons and switches in a cockpit.  This is a website that you can access from any device on your network e.g. tablets or mobile phones. 
- Joystick device - any connected DirectInput device can have any of its controls mapped to either keypresses or mouse movements.
- Mouse 

# Building / Running

You can build & run either / both the react app or server app, or use the pre-built & hosted site at: http://xbattlestation.com/retrovirtualcockpit/

# Repo Items

## RetroVirtualCockpit.Server

Console app that reads messages from receivers (e.g. client web app, mouse, joystick) translates them into GameActions, and replays commands into the host machine via either keyboard or mouse actions.

## RetroVirtualCockpit.InputTester

This program will output the state of any Direct Input controller changes.  Use it to identify buttons, sliders, axis etc on a controller.

## retrovirtualcockpit-web

React website client that provides buttons and other controls that send commands to the server.

## RetroVirtualCockpit.Web

Original static HTML version of the UI.  Now replaced by the react app, this has been kept for reference.