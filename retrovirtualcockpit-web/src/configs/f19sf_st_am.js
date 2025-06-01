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
                            { type: "switch", label: "Gear", flip: [{"Key":"_6"}] },
                            { type: "switch", label: "Auto Pilot", flip: [{"Key":"_7"}] },
                            { type: "switch", label: "Flaps", flip: [{"Key":"_9"}] },
                            { type: "switch", label: "Brakes", flip: [{"Key":"_0"}] },
                            { type: "button", label: "Stick", press: [{"Key":"Insert"}] }
                        ]
                    },
                    {
                        id: "left-mfd",
                        title: "Left MFD",
                        controls: [
                            { type: "button", label: "Mode", press: [{"Key":"F3"}] },
                            { type: "button-h", label: "Zoom", left: [{"Key":"Z"}], right: [{"Key":"X"}] }
                        ]
                    },
                    {
                        id: "hud",
                        title: "HUD",
                        controls: [
                            { type: "knob", label: "Mode", turn: [{"Key":"F2"}] },
                            { type: "switch", label: "ILS", flip: [{"Key":"F9"}] }
                        ]
                    },                    
                    {
                        id: "right-mfd",
                        title: "Right MFD",
                        controls: [
                            { type: "button", label: "TGT INF", press: [{"Key":"F4"}] },
                            { type: "button", label: "Ord", press: [{"Key":"F5"}] },
                            { type: "button", label: "Dmg", press: [{"Key":"F6"}] },
                            { type: "button", label: "Nav", press: [{"Key":"F7"}] },
                            { type: "button", label: "Msn", press: [{"Key":"F10"}] }
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
                            { type: "throttle", up: [{"Key":"Equals"}], down: [{"Key":"Minus"}], max: [{"Key":"Equals","Modifier":"LeftShift"}], min: [{"Key":"Minus","Modifier":"LeftShift"}] }
                        ]
                    },
                    {
                        id: "countermeasures",
                        title: "Countermeasures",
                        controls: [
                            { type: "button", label: "Flare", press: [{"Key":"_1"}] },
                            { type: "button", label: "Chaff", press: [{"Key":"_2"}] },
                            { type: "switch", label: "IR Jam", flip: [{"Key":"_3"}] },
                            { type: "switch", label: "ECM", flip: [{"Key":"_4"}] },
                            { type: "button", label: "Decoy", press: [{"Key":"_5"}] }
                        ]
                    },
                    {
                        id: "target",
                        title: "Target",
                        controls: [
                            { type: "button", label: "Select", press: [{"Key":"B"}] },
                            { type: "button", label: "Des", press: [{"Key":"N"}] }
                        ]
                    },
                    {
                        id: "weapons",
                        title: "Weapons",
                        controls: [
                            { type: "knob", label: "Select", turn: [{"Key":"Space"}] },
                            { type: "button-red", label: "Pickle", press: [{"Key":"Enter"}], decoration: "hazard" },
                            { type: "button-red", label: "Gun", press: [{"Key":"Backspace"}], decoration: "hazard" },
                            { type: "switch", label: "Bay", flip: [{"Key":"_8"}] }
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
                                    { type: "button", label: "Edit", press: [{"Key":"F8"}] },
                                    { type: "button", label: "Reset", press: [{"Key":"F8","Modifier":"LeftShift"}] }
                                ]
                            },
                            { type: "button-h", label: "Select", left: [{"Key":"NumPad9"}], right: [{"Key":"NumPad3"}] },
                            { type: "button-dpad", label: "Move", up: [{"Key":"NumPad8"}], down: [{"Key":"NumPad2"}], left: [{"Key":"NumPad4"}], right: [{"Key":"NumPad6"}] }
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
                            { type: "button", label: "Quit", press: [{"Key":"Q","Modifier":"Alt"}], decoration: "hazard" },
                            { type: "button", label: "Resupply", press: [{"Key":"R","Modifier":"Alt"}] },
                            { type: "switch", label: "Pause", on: [{"Key":"P","Modifier":"Alt"}], off: [{"Key":"P"}] },
                            { type: "switch", label: "Accel", on: [{"Key":"Z","Modifier":"LeftShift"}], off: [{"Key":"X","Modifier":"LeftShift"}] }
                        ]
                    },
                    {
                        id: "eject",
                        decoration: "none",
                        controls: [
                            { type: "handle", label: "Pull to eject", pull: [{"Key":"F10","Modifier":"LeftShift"}], decoration: "hazard" },
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
                            { type: "button", label: "Cockpit", press: [{"Key":"F1"}] },
                            { type: "button", label: "Slot", press: [{"Key":"F1","Modifier":"LeftShift"}] },
                            { type: "button", label: "Chase", press: [{"Key":"F2","Modifier":"LeftShift"}] },
                            { type: "button", label: "Side", press: [{"Key":"F3","Modifier":"LeftShift"}] },
                            { type: "button", label: "Missile", press: [{"Key":"F4","Modifier":"LeftShift"}] },
                            { type: "button", label: "Tac", press: [{"Key":"F5","Modifier":"LeftShift"}] },
                            { type: "button", label: "Rev Tac", press: [{"Key":"F6","Modifier":"LeftShift"}] },
                            { type: "switch", label: "Angle", flip: [{"Key":"C"}] }
                        ]
                    },
                    {
                        id: "head",
                        title: "Head",
                        controls: [
                            { type: "button-dpad", up: [{"Key":"Slash","Modifier":"LeftShift"}], down: [{"Key":"Period","Modifier":"LeftShift"}], left: [{"Key":"M","Modifier":"LeftShift"}], right: [{"Key":"Comma","Modifier":"LeftShift"}] }
                        ]
                    },
                    {
                        id: "camera",
                        title: "Camera",
                        controls: [
                            { type: "button-dpad", up: [{"Key":"Slash"}], down: [{"Key":"Period"}], left: [{"Key":"M"}], right: [{"Key":"Comma"}] },
                        ]
                    },
                ]
            }
        ]
    }
}
