export default function tornado_pc() {
    return {
        title: "Tornado [PC]",
        panelRows: [
            {
                panels: [
                    {
                        id: "autopilot",
                        title: "Autopilot",
                        controls: [
                            { type: "knob", label: "Mode", values: [
                                [ [{"key":"Escape"}], "Off" ],
                                [ [{"key":"F6"}], "Lnd" ],
                                [ [{"key":"F7"}], "Trk" ],
                                [ [{"key":"F8"}], "Alt" ],
                                [ [{"key":"F9"}], "Ter" ]
                            ] },
                            { type: "switch", label: "Throttle", flip: [{"key":"F10"}] },
                            { type: "button", label: "Nxt Way", press: [{"key":"N"}] },
                            { type: "button", label: "Trim", press: [{"key":"NumPad5"}] },
                        ]
                    },
                    {
                        id: "hud",
                        title: "HUD",
                        controls: [
                            { type: "knob", label: "Contrast", turn: [{"key":"H"}] },
                            { type: "switch", label: "On/Off", flip: [{"key":"H","modifier":"Control"}] },
                            { type: "switch", label: "Spd", flip: [{"key":"H","modifier":"Alt"}] },
                        ]
                    }
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
                        id: "control",
                        title: "Control",
                        align: "left",
                        controls: [
                            { type: "lever", label: "Sweep", multistage: true, up: [{"key":"W"}], down: [{"key":"S"}] },
                            { type: "lever", label: "Flaps", multistage: true, up: [{"key":"A"}], down: [{"key":"Q"}] },
                            { type: "lever", label: "Gear", up: [{"key":"U"}], down: [{"key":"U"}], altColor: true },
                        ]
                    },
                    {
                        id: "mfd",
                        title: "MFD",
                        controls: [
                            { type: "button", label: "L Mode", press: [{"key":"OpenSquareBracket"}] },
                            { type: "button", label: "C Mode", press: [{"key":"D"}] },
                            { type: "button", label: "R Mode", press: [{"key":"CloseSquareBracket"}] },
                            { type: "button", label: "Active", press: [{"key":"Tab"}] },
                        ]
                    },
                    {
                        id: "viewcontrol",
                        title: "CTRL",
                        align: "right",
                        orientation: "vertical",
                        controls: [
                            { type: "button-h", label: "Rotate", left: [{"key":"X"}], right: [{"key":"Z"}] },
                            { type: "button-h", label: "Rotate Fst", left: [{"key":"X","modifier":"LeftShift"}], right: [{"key":"Z","modifier":"LeftShift"}] },
                            { type: "button-h", label: "Zoom", left: [{"key":"Comma"}], right: [{"key":"Period"}] },
                            { type: "button-h", label: "Zoom Fst", left: [{"key":"Comma","modifier":"LeftShift"}], right: [{"key":"Period","modifier":"LeftShift"}] },
                        ]
                    },
                    {
                        id: "externalviews",
                        title: "EXT",
                        align: "right",
                        orientation: "vertical",
                        controls: [
                            { type: "button", label: "Track", press: [{"key":"F1"}] },
                            { type: "button", label: "Sat", press: [{"key":"F2"}] },
                            { type: "button", label: "Rmt", press: [{"key":"F3"}] },
                            { type: "button", label: "Spec", press: [{"key":"F4"}] },
                            { type: "button", label: "Drn", press: [{"key":"F5"}] },
                        ]
                    },                    {
                        id: "internalview",
                        title: "View",
                        align: "right",
                        orientation: "vertical",
                        controls: [
                            { type: "button-dpad", label: "Head", up: [{"key":"Home"}], down: [{"key":"PageUp"}], left: [{"key":"End","modifier":"LeftShift"}], right: [{"key":"PageDown","modifier":"LeftShift"}] },
                            { type: "button", label: "Map", press: [{"key":"M"}] },
                            { type: "button", label: "Map Orig", press: [{"key":"O"}] },
                        ]
                    },             
                ]
            },
            {
                panels: [
                    {
                        id: "brakes",
                        title: "Brakes",
                        align: "left",
                        controls: [
                            { type: "lever", label: "Air/<br>ThrstRev", up: [{"key":"Backspace"}], down: [{"key":"Backspace","action":"Up","autoKeyUpDelay":200}], altColor: true },
                            { type: "lever", label: "Wheel<br>&nbsp;", up: [{"key":"B"}], down: [{"key":"B"}], startInUpPos: true },
                        ]
                    },
                    {
                        id: "eject",
                        decoration: "none",
                        controls: [
                            { type: "handle", label: "Pull to eject", pull: [{"key":"Q","modifier":"Control"}], decoration: "hazard" },
                        ]
                    }
                ]
            }            
        ]
    }
}