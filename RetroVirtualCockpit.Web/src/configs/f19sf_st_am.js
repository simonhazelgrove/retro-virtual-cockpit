export default function f19sf_st_am() {
    return {
        title: "[ST/Amiga] F-19 Stealth Fighter",
        panelRows: [
            {
                panels: [
                    {
                        title: "HUD",
                        controls: [
                            { type: "knob", label: "Mode", turn: "HUD.Mode" },
                            { type: "switch", label: "ILS", flip: "HUD.ILS" }
                        ]
                    },
                    {
                        title: "Left MFD",
                        controls: [
                            { type: "button", label: "Mode", press: "MFD.L.Change" },
                            { type: "button-h", label: "Zoom", left: "MFD.L.Zoom.In", right: "MFD.L.Zoom.Out" }
                        ]
                    },
                    {
                        title: "Right MFD",
                        controls: [
                            { type: "button", label: "Data", press: "MFD.R.Data" },
                            { type: "button", label: "Ord", press: "MFD.R.Ordnance" },
                            { type: "button", label: "Dmg", press: "MFD.R.Damage" },
                            { type: "button", label: "Way p", press: "MFD.R.Waypoints" },
                            { type: "button", label: "Miss", press: "MFD.R.Mission" }
                        ]
                    },
                    {
                        title: "Countermeasures",
                        controls: [
                            { type: "button", label: "Flare", press: "Defence.Flare" },
                            { type: "button", label: "Chaff", press: "Defence.Chaff" },
                            { type: "switch", label: "IR Jam", flip: "Defence.IRJam" },
                            { type: "switch", label: "ECM", flip: "Defence.ECM" },
                            { type: "button", label: "Decoy", press: "Defence.Decoy" }
                        ]
                    }
                ]
            },
            {
                panels: [
                    {
                        align: "left",
                        controls: [
                            { type: "throttle", up: "Controls.Throttle.Up", down: "Controls.Throttle.Down", max: "Controls.Throttle.Max", min: "Controls.Throttle.Min" }
                        ]
                    },
                    {
                        title: "Control",
                        controls: [
                            { type: "switch", label: "Gear", flip: "Controls.Gear" },
                            { type: "switch", label: "Auto Pilot", flip: "Controls.AutoPilot" },
                            { type: "switch", label: "Flaps", flip: "Controls.Flaps" },
                            { type: "switch", label: "Brakes", flip: "Controls.Brakes" },
                            { type: "button", label: "Stick", press: "Controls.StickSensitivity" }
                        ]
                    },
                    {
                        title: "Target",
                        controls: [
                            { type: "button", label: "Select", press: "Target.Select" },
                            { type: "button", label: "Des", press: "Target.Designate" }
                        ]
                    },
                    {
                        title: "Weapons",
                        controls: [
                            { type: "knob", label: "Select", turn: "Weapon.Select" },
                            { type: "button-red", label: "Pickle", press: "Weapon.Drop", decoration: "hazard" },
                            { type: "button-red", label: "Gun", press: "Weapon.FireGun", decoration: "hazard" },
                            { type: "switch", label: "Bay", flip: "Weapon.Bay" }
                        ]
                    },
                    {
                        title: "Camera",
                        controls: [
                            { type: "button-dpad", up: "Camera.Front", down: "Camera.Rear", left: "Camera.Left", right: "Camera.Right" },
                        ]
                    },
                    {
                        title: "Waypoint Edit",
                        align: "right",
                        orientation: "vertical",
                        controls: [
                            {
                                type: "subpanel",
                                controls: [
                                    { type: "button", label: "Edit", press: "Waypoint.Edit" },
                                    { type: "button", label: "Reset", press: "Waypoint.Reset" }
                                ]
                            },
                            { type: "button-h", label: "Select", left: "Waypoint.Select.Previous", right: "Waypoint.Select.Next" },
                            { type: "button-dpad", label: "Move", up: "Waypoint.Move.Up", down: "Waypoint.Move.Down", left: "Waypoint.Move.Left", right: "Waypoint.Move.Right" }
                        ]
                    }
                ]
            },
            {
                panels: [
                    {
                        decoration: "none",
                        controls: [
                            { type: "handle", label: "Pull to eject", pull: "Controls.Eject", decoration: "hazard" },
                        ]
                    }
                ]
            },
            {
                panels: [
                    {
                        title: "Game",
                        controls: [
                            { type: "button", label: "Quit", press: "Game.Quit" },
                            { type: "button", label: "Resupply", press: "Game.Resupply" },
                            { type: "button", label: "Pause", press: "Game.Pause" },
                            { type: "switch", label: "Accel", on: "Game.Time.Accelerate", off: "Game.Time.Normal" }
                        ]
                    },
                    {
                        title: "View",
                        controls: [
                            { type: "button", label: "Cockpit", press: "View.Cockpit" },
                            { type: "button", label: "Slot", press: "View.External.Slot" },
                            { type: "button", label: "Chase", press: "View.External.ChasePlane" },
                            { type: "button", label: "Side", press: "View.External.Side" },
                            { type: "button", label: "Missile", press: "View.External.Missile" },
                            { type: "button", label: "Tac", press: "View.External.Tactical" },
                            { type: "button", label: "Rev Tac", press: "View.External.ReverseTactical" },
                            { type: "switch", label: "Angle", flip: "View.Angle" }
                        ]
                    },
                    {
                        title: "Head",
                        controls: [
                            { type: "button-dpad", label: "Head", up: "View.Head.Front", down: "View.Head.Rear", left: "View.Head.Left", right: "View.Head.Right" }
                        ]
                    }
                ]
            }
        ]
    }
}
