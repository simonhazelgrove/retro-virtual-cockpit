export default function f16cp_st_am() {
    return {
        title: "F-16 Combat Pilot [ST/Amiga]",
        panelRows: [
            {
                panels: [
                    {
                        id: "control",
                        title: "Control",
                        controls: [
                            { type: "button", label: "Dogfight", press: "MFD.DogfightMode" },
                            { type: "button-red", label: "Jett.<br>Fuel", press: "Stores.Jettison.Fuel", decoration: "hazard" },
                            { type: "button-red", label: "Jett.<br>All", press: "Stores.Jettison.All", decoration: "hazard" }
                        ]
                    },
                    {
                        id: "ufcp",
                        title: "UFCP",
                        controls: [
                            { type: "knob", label: "Mode", turn: "UFCP.Mode" },
                            { type: "knob", label: "Channel", turn: "UFCP.Channel" },
                            { type: "button", label: "Auto<br>Pilot", press: "UFCP.AutoPilot" },
                            { type: "switch", label: "Recce<br>Pod", flip: "UFCP.ReccePod" }
                        ]
                    },
                    {
                        id: "radio",
                        title: "Radio",
                        controls: [
                            { type: "button", label: "Callsign", press: "Radio.Callsign" },
                            { type: "button", label: "Req<br>GCA", press: "Radio.RequestGCA" }
                        ]
                    },
                    {
                        id: "eject",
                        decoration: "none",
                        controls: [
                            { type: "handle", label: "Pull to eject", pull: "Controls.Eject", decoration: "hazard" }
                        ]
                    }
                ]
            },
            {
                panels: [
                ]
            }
        ]
    }
}
