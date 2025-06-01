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
                                [ [{"Key":"Escape"}], "Off" ],
                                [ [{"Key":"F6"}], "Lnd" ],
                                [ [{"Key":"F7"}], "Trk" ],
                                [ [{"Key":"F8"}], "Alt" ],
                                [ [{"Key":"F9"}], "Ter" ]
                            ] },
                            { type: "switch", label: "Throttle", flip: [{"Key":"F10"}] },
                            { type: "button", label: "Nxt Way", press: [{"Key":"N"}] },
                            { type: "button", label: "Trim", press: [{"Key":"NumPad5"}] },
                        ]
                    },
                    {
                        id: "hud",
                        title: "HUD",
                        controls: [
                            { type: "knob", label: "Contrast", turn: [{"Key":"H"}] },
                            { type: "switch", label: "On/Off", flip: [{"Key":"H","Modifier":"Control"}] },
                            { type: "switch", label: "Spd", flip: [{"Key":"H","Modifier":"Alt"}] },
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
                            { type: "throttle", up: [{"Key":"Equals"}], down: [{"Key":"Minus"}], max: [{"Key":"Equals","Modifier":"LeftShift"}], min: [{"Key":"Minus","Modifier":"LeftShift"}] }
                        ]
                    },
                    {
                        id: "control",
                        title: "Control",
                        align: "left",
                        controls: [
                            { type: "lever", label: "Sweep", multistage: true, up: [{"Key":"W"}], down: [{"Key":"S"}] },
                            { type: "lever", label: "Flaps", multistage: true, up: [{"Key":"A"}], down: [{"Key":"Q"}] },
                            { type: "lever", label: "Gear", up: [{"Key":"U"}], down: [{"Key":"U"}], altColor: true },
                        ]
                    },
                    {
                        id: "mfd",
                        title: "MFD",
                        controls: [
                            { type: "button", label: "L Mode", press: [{"Key":"OpenSquareBracket"}] },
                            { type: "button", label: "C Mode", press: [{"Key":"D"}] },
                            { type: "button", label: "R Mode", press: [{"Key":"CloseSquareBracket"}] },
                            { type: "button", label: "Active", press: [{"Key":"Tab"}] },
                        ]
                    },
                    {
                        id: "viewcontrol",
                        title: "CTRL",
                        align: "right",
                        orientation: "vertical",
                        controls: [
                            { type: "button-h", label: "Rotate", left: [{"Key":"X"}], right: [{"Key":"Z"}] },
                            { type: "button-h", label: "Rotate Fst", left: [{"Key":"X","Modifier":"LeftShift"}], right: [{"Key":"Z","Modifier":"LeftShift"}] },
                            { type: "button-h", label: "Zoom", left: [{"Key":"Comma"}], right: [{"Key":"Period"}] },
                            { type: "button-h", label: "Zoom Fst", left: [{"Key":"Comma","Modifier":"LeftShift"}], right: [{"Key":"Period","Modifier":"LeftShift"}] },
                        ]
                    },
                    {
                        id: "externalviews",
                        title: "EXT",
                        align: "right",
                        orientation: "vertical",
                        controls: [
                            { type: "button", label: "Track", press: [{"Key":"F1"}] },
                            { type: "button", label: "Sat", press: [{"Key":"F2"}] },
                            { type: "button", label: "Rmt", press: [{"Key":"F3"}] },
                            { type: "button", label: "Spec", press: [{"Key":"F4"}] },
                            { type: "button", label: "Drn", press: [{"Key":"F5"}] },
                        ]
                    },                    {
                        id: "internalview",
                        title: "View",
                        align: "right",
                        orientation: "vertical",
                        controls: [
                            { type: "button-dpad", label: "Head", up: [{"Key":"Home"}], down: [{"Key":"PageUp"}], left: [{"Key":"End","Modifier":"LeftShift"}], right: [{"Key":"PageDown","Modifier":"LeftShift"}] },
                            { type: "button", label: "Map", press: [{"Key":"M"}] },
                            { type: "button", label: "Map Orig", press: [{"Key":"O"}] },
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
                            { type: "lever", label: "Air/<br>ThrstRev", up: [{"Key":"Backspace"}], down: [{"Key":"Backspace","Action":"Up","DelayUntilKeyUp":200}], altColor: true },
                            { type: "lever", label: "Wheel<br>&nbsp;", up: [{"Key":"B"}], down: [{"Key":"B"}], startInUpPos: true },
                        ]
                    },
                    {
                        id: "eject",
                        decoration: "none",
                        controls: [
                            { type: "handle", label: "Pull to eject", pull: [{"Key":"Q","Modifier":"Control"}], decoration: "hazard" },
                        ]
                    }
                ]
            }            
        ]
    }
}