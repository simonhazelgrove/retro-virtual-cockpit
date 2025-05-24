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
                            //{ type: "switch", label: "Autopilot", on: "Controls.AutoPilot.On", off: "Controls.AutoPilot.Off" },
                            { type: "knob", label: "Mode", values: [
                                [ "Controls.AutoPilot.Off", "Off" ],
                                [ "Controls.AutoPilot.Mode.AutoLand", "Lnd" ],
                                [ "Controls.AutoPilot.Mode.Track", "Trk" ],
                                [ "Controls.AutoPilot.Mode.AltitudeHeading", "Alt" ],
                                [ "Controls.AutoPilot.Mode.Terrain", "Ter" ]
                            ] },
                            //{ type: "button", label: "Land", press: "Controls.AutoPilot.Mode.AutoLand" },
                            //{ type: "button", label: "Track", press: "Controls.AutoPilot.Mode.Track" },
                            //{ type: "button", label: "Alt/Hdg", press: "Controls.AutoPilot.Mode.AltitudeHeading" },
                            //{ type: "button", label: "Terr", press: "Controls.AutoPilot.Mode.Terrain" },
                            { type: "switch", label: "Throttle", flip: "Controls.AutoPilot.AutoThrottle" },
                            { type: "button", label: "Nxt Way", press: "Controls.AutoPilot.NextWaypoint" },
                            { type: "button", label: "Trim", press: "Controls.AutoPilot.AutoTrim" },
                        ]
                    },
                    {
                        id: "hud",
                        title: "HUD",
                        controls: [
                            { type: "knob", label: "Contrast", turn: "HUD.Contrast" },
                            { type: "switch", label: "On/Off", flip: "HUD.OnOff" },
                            { type: "switch", label: "Spd", flip: "HUD.AirspeedDisplay" },
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
                            { type: "throttle", up: "Controls.Throttle.Up", down: "Controls.Throttle.Down", max: "Controls.Throttle.Max", min: "Controls.Throttle.Min" }
                        ]
                    },
                    {
                        id: "control",
                        title: "Control",
                        align: "left",
                        controls: [
                            { type: "lever", label: "Sweep", multistage: true, up: "Controls.WingSweep.Forward", down: "Controls.WingSweep.Back" },
                            { type: "lever", label: "Flaps", multistage: true, up: "Controls.FlapsSlats.Up", down: "Controls.FlapsSlats.Down" },
                            { type: "lever", label: "Gear", up: "Controls.Gear", down: "Controls.Gear", altColor: true },
                        ]
                    },
                    {
                        id: "mfd",
                        title: "MFD",
                        controls: [
                            { type: "button", label: "L Mode", press: "MFD.Left.Mode" },
                            { type: "button", label: "C Mode", press: "MFD.Center.Mode" },
                            { type: "button", label: "R Mode", press: "MFD.Right.Mode" },
                            { type: "button", label: "Active", press: "MFD.Active.Switch" },
                        ]
                    },
                    {
                        id: "viewcontrol",
                        title: "CTRL",
                        align: "right",
                        orientation: "vertical",
                        controls: [
                            { type: "button-h", label: "Rotate", left: "View.External.Track.Left", right: "View.External.Track.Right" },
                            { type: "button-h", label: "Rotate Fst", left: "View.External.Track.LeftFast", right: "View.External.Track.RightFast" },
                            { type: "button-h", label: "Zoom", left: "View.External.Zoom.In", right: "View.External.Zoom.Out" },
                            { type: "button-h", label: "Zoom Fst", left: "View.External.Zoom.InFast", right: "View.External.Zoom.OutFast" },
                        ]
                    },
                    {
                        id: "externalviews",
                        title: "EXT",
                        align: "right",
                        orientation: "vertical",
                        controls: [
                            { type: "button", label: "Track", press: "View.External.Tracking" },
                            { type: "button", label: "Sat", press: "View.External.Satellite" },
                            { type: "button", label: "Rmt", press: "View.External.Remote" },
                            { type: "button", label: "Spec", press: "View.External.Spectator" },
                            { type: "button", label: "Drn", press: "View.External.Drone" },
                        ]
                    },                    {
                        id: "internalview",
                        title: "View",
                        align: "right",
                        orientation: "vertical",
                        controls: [
                            { type: "button-dpad", label: "Head", up: "View.Head.FrontUp", down: "View.Head.Rear", left: "View.Head.Left", right: "View.Head.Right" },
                            { type: "button", label: "Map", press: "View.Head.Map" },
                            { type: "button", label: "Map Orig", press: "View.Head.Map.Origin" },
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
                            { type: "lever", label: "Air/<br>ThrstRev", up: "Controls.AirBrake.On", down: "Controls.AirBrake.Off", altColor: true },
                            { type: "lever", label: "Wheel<br>&nbsp;", up: "Controls.Brake.Toggle", down: "Controls.Brake.Toggle", startInUpPos: true },
                        ]
                    },
                    {
                        id: "eject",
                        decoration: "none",
                        controls: [
                            { type: "handle", label: "Pull to eject", pull: "Controls.Eject", decoration: "hazard" },
                        ]
                    }
                ]
            }            
        ]
    }
}