# Retro Virtual Cockpit

Retro Virtual Cockpit is a tool designed to make playing retro video games with lots of key bindings (e.g. flight sims) easier.

The client program takes inputs from several different sources and translates them into keypresses that games running in an emulator will understand.

The different sources of input include:

- Cockpit UI - the original Retro Virtual Cockpit was designed as a web ui that looks like buttons and switches in a cockpit.  This is a website that you can access from any device on your network e.g. tablets or mobile phones. 
- Joystick device - any connected DirectInput device can have any of its controls mapped to either keypresses or mouse movements.

## Repo Items

### RetroVirtualCockpit.Client

### RetroVirtualCockpit.Client.Config

This program will output the state of any Direct Input controller changes.  Use it to identify buttons, sliders, axis etc on a controller.

### RetroVirtualCockpit.Web

