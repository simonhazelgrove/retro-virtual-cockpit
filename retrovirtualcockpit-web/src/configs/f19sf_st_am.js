export default function f19sf_st_am() {
    return {
        title: "F-19 Stealth Fighter [PC/ST/Amiga]",
        panelRows: [
            {
                panels: [
                    {
                        id: "control",
                        title: "Control",
                        controls: [
                            { type: "switch", label: "Gear", flip: [{"key":"_6"}] },
                            { type: "switch", label: "Auto Pilot", flip: [{"key":"_7"}] },
                            { type: "switch", label: "Flaps", flip: [{"key":"_9"}] },
                            { type: "switch", label: "Brakes", flip: [{"key":"_0"}] },
                            { type: "button", label: "Stick", press: [{"key":"Insert"}] }
                        ]
                    },
                    {
                        id: "left-mfd",
                        title: "Left MFD",
                        controls: [
                            { type: "button", label: "Mode", press: [{"key":"F3"}] },
                            { type: "button-h", label: "Zoom", left: [{"key":"Z"}], right: [{"key":"X"}] }
                        ]
                    },
                    {
                        id: "hud",
                        title: "HUD",
                        controls: [
                            { type: "knob", label: "Mode", turn: [{"key":"F2"}] },
                            { type: "switch", label: "ILS", flip: [{"key":"F9"}] }
                        ]
                    },                    
                    {
                        id: "right-mfd",
                        title: "Right MFD",
                        controls: [
                            { type: "button", label: "TGT INF", press: [{"key":"F4"}] },
                            { type: "button", label: "Ord", press: [{"key":"F5"}] },
                            { type: "button", label: "Dmg", press: [{"key":"F6"}] },
                            { type: "button", label: "Nav", press: [{"key":"F7"}] },
                            { type: "button", label: "Msn", press: [{"key":"F10"}] }
                        ]
                    },
                ]
            },
            {
                panels: [
                    {
                        id: "throttle",
                        align: "left",
                        controls: [
                            { type: "throttle", up: [{"key":"Equals"}], down: [{"key":"Minus"}], max: [{"key":"Equals","modifier":"LeftShift"}], min: [{"key":"Minus","modifier":"LeftShift"}] }
                        ]
                    },
                    {
                        id: "countermeasures",
                        title: "Countermeasures",
                        controls: [
                            { type: "button", label: "Flare", press: [{"key":"_1"}] },
                            { type: "button", label: "Chaff", press: [{"key":"_2"}] },
                            { type: "switch", label: "IR Jam", flip: [{"key":"_3"}] },
                            { type: "switch", label: "ECM", flip: [{"key":"_4"}] },
                            { type: "button", label: "Decoy", press: [{"key":"_5"}] }
                        ]
                    },
                    {
                        id: "target",
                        title: "Target",
                        controls: [
                            { type: "button", label: "Select", press: [{"key":"B"}] },
                            { type: "button", label: "Des", press: [{"key":"N"}] }
                        ]
                    },
                    {
                        id: "weapons",
                        title: "Weapons",
                        controls: [
                            { type: "knob", label: "Select", turn: [{"key":"Space"}] },
                            { type: "button-red", label: "Pickle", press: [{"key":"Enter"}], decoration: "hazard" },
                            { type: "button-red", label: "Gun", press: [{"key":"Backspace"}], decoration: "hazard" },
                            { type: "switch", label: "Bay", flip: [{"key":"_8"}] }
                        ]
                    },
                    {
                        id: "waypoints",
                        title: "Waypoint Edit",
                        align: "right",
                        orientation: "vertical",
                        controls: [
                            {
                                type: "subpanel",
                                controls: [
                                    { type: "button", label: "Edit", press: [{"key":"F8"}] },
                                    { type: "button", label: "Reset", press: [{"key":"F8","modifier":"LeftShift"}] }
                                ]
                            },
                            { type: "button-h", label: "Select", left: [{"key":"NumPad9"}], right: [{"key":"NumPad3"}] },
                            { type: "button-dpad", label: "Move", up: [{"key":"NumPad8"}], down: [{"key":"NumPad2"}], left: [{"key":"NumPad4"}], right: [{"key":"NumPad6"}] }
                        ]
                    }
                ]
            },
            {
                panels: [
                    {
                        id: "game",
                        title: "Game",
                        controls: [
                            { type: "button", label: "Quit", press: [{"key":"Q","modifier":"Alt"}], decoration: "hazard" },
                            { type: "button", label: "Resupply", press: [{"key":"R","modifier":"Alt"}] },
                            { type: "switch", label: "Pause", on: [{"key":"P","modifier":"Alt"}], off: [{"key":"P"}] },
                            { type: "switch", label: "Accel", on: [{"key":"Z","modifier":"LeftShift"}], off: [{"key":"X","modifier":"LeftShift"}] }
                        ]
                    },
                    {
                        id: "eject",
                        decoration: "none",
                        controls: [
                            { type: "handle", label: "Pull to eject", pull: [{"key":"F10","modifier":"LeftShift"}], decoration: "hazard" },
                        ]
                    }
                ]
            },
            {
                panels: [
                    {
                        id: "view",
                        title: "View",
                        controls: [
                            { type: "button", label: "Cockpit", press: [{"key":"F1"}] },
                            { type: "button", label: "Slot", press: [{"key":"F1","modifier":"LeftShift"}] },
                            { type: "button", label: "Chase", press: [{"key":"F2","modifier":"LeftShift"}] },
                            { type: "button", label: "Side", press: [{"key":"F3","modifier":"LeftShift"}] },
                            { type: "button", label: "Missile", press: [{"key":"F4","modifier":"LeftShift"}] },
                            { type: "button", label: "Tac", press: [{"key":"F5","modifier":"LeftShift"}] },
                            { type: "button", label: "Rev Tac", press: [{"key":"F6","modifier":"LeftShift"}] },
                            { type: "switch", label: "Angle", flip: [{"key":"C"}] }
                        ]
                    },
                    {
                        id: "head",
                        title: "Head",
                        controls: [
                            { type: "button-dpad", up: [{"key":"Slash","modifier":"LeftShift"}], down: [{"key":"Period","modifier":"LeftShift"}], left: [{"key":"M","modifier":"LeftShift"}], right: [{"key":"Comma","modifier":"LeftShift"}] }
                        ]
                    },
                    {
                        id: "camera",
                        title: "Camera",
                        controls: [
                            { type: "button-dpad", up: [{"key":"Slash"}], down: [{"key":"Period"}], left: [{"key":"M"}], right: [{"key":"Comma"}] },
                        ]
                    },
                ]
            }
        ]
    }
}
