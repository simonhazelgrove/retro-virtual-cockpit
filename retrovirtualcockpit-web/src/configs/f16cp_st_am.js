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
                            { type: "button", label: "Dogfight", press: [{"Key":"D"}] },
                            { type: "button-red", label: "Jett.<br>Fuel", press: [{"Key":"F","Modifier":"J"}], decoration: "hazard" },
                            { type: "button-red", label: "Jett.<br>All", press: [{"Key":"A","Modifier":"J"}], decoration: "hazard" }
                        ]
                    },
                    {
                        id: "ufcp",
                        title: "UFCP",
                        controls: [
                            { type: "knob", label: "Mode", turn: [{"Key":"F5"}] },
                            { type: "knob", label: "Channel", turn: [{"Key":"F6"}] },
                            { type: "button", label: "Auto<br>Pilot", press: [{"Key":"F7"}] },
                            { type: "switch", label: "Recce<br>Pod", flip: [{"Key":"F8"}] }
                        ]
                    },
                    {
                        id: "radio",
                        title: "Radio",
                        controls: [
                            { type: "button", label: "Callsign", press: [{"Key":"T"}] },
                            { type: "button", label: "Req<br>GCA", press: [{"Key":"G"}] }
                        ]
                    },
                    {
                        id: "eject",
                        decoration: "none",
                        controls: [
                            { type: "handle", label: "Pull to eject", pull: [{"Key":"E","Modifier":"Control"}], decoration: "hazard" }
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
