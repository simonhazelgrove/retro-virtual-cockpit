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
                            { type: "button", label: "Dogfight", press: [{"key":"D"}] },
                            { type: "button-red", label: "Jett.<br>Fuel", press: [{"key":"F","modifier":"J"}], decoration: "hazard" },
                            { type: "button-red", label: "Jett.<br>All", press: [{"key":"A","modifier":"J"}], decoration: "hazard" }
                        ]
                    },
                    {
                        id: "ufcp",
                        title: "UFCP",
                        controls: [
                            { type: "knob", label: "Mode", turn: [{"key":"F5"}] },
                            { type: "knob", label: "Channel", turn: [{"key":"F6"}] },
                            { type: "button", label: "Auto<br>Pilot", press: [{"key":"F7"}] },
                            { type: "switch", label: "Recce<br>Pod", flip: [{"key":"F8"}] }
                        ]
                    },
                    {
                        id: "radio",
                        title: "Radio",
                        controls: [
                            { type: "button", label: "Callsign", press: [{"key":"T"}] },
                            { type: "button", label: "Req<br>GCA", press: [{"key":"G"}] }
                        ]
                    },
                    {
                        id: "eject",
                        decoration: "none",
                        controls: [
                            { type: "handle", label: "Pull to eject", pull: [{"key":"E","modifier":"Control"}], decoration: "hazard" }
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
