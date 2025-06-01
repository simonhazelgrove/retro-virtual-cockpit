export default function f16cp_st_am() {
    return {
        title: "Combat Lynx [Spectrum]",
        panelRows: [
            {
                panels: [
                    {
                        id: "map",
                        title: "Map",
                        controls: [
                            { type: "switch", label: "View", flip: [{"Key":"M"}] },
                            { type: "button-dpad", label: "Move", up: [{"Key":"_7"}], down: [{"Key":"_6"}], left: [{"Key":"_5"}], right: [{"Key":"_8"}] },
                            { type: "button-dpad", label: "Move Fast", up: [{"Key":"_7","Modifier":"_0"}], down: [{"Key":"_6","Modifier":"_0"}], left: [{"Key":"_5","Modifier":"_0"}], right: [{"Key":"_8","Modifier":"_0"}] },
                        ]
                    },
                    {
                        id: "loadout",
                        title: "Loadout",
                        controls: [
                            { type: "button-h", label: "-    +", left: [{"Key":"J"}], right: [{"Key":"K"}] },
                            { type: "button", label: "Next", press: [{"Key":"Enter"}] },
                            { type: "button", label: "Exit", press: [{"Key":"Space"}] },
                        ]
                    },
                    {
                        id: "weapons",
                        title: "Weapons",
                        controls: [
                            { type: "button-h", label: "Select", left: [{"Key":"_2"}], right: [{"Key":"_9"}] },
                            { type: "switch", label: "Sights", flip: [{"Key":"Enter"}] },
                            { type: "button-red", label: "Fire", press: [{"Key":"_0"}], decoration: "hazard" },
                        ]
                    }
                ]
            },
            {
                panels: [
                    {
                        id: "messages",
                        title: "Messages",
                        orientation: "vertical",
                        controls: [
                            { type: "button", label: "Read", press: [{"Key":"_1"}] },
                            {
                                type: "subpanel",
                                controls: [
                                    { type: "label", label: "Base Pos" },
                                    { type: "button", label: "0", press: [{"Key":"P","Modifier":"LeftShift"}] },
                                    { type: "button", label: "1", press: [{"Key":"Q","Modifier":"LeftShift"}] },
                                    { type: "button", label: "2", press: [{"Key":"W","Modifier":"LeftShift"}] },
                                    { type: "button", label: "3", press: [{"Key":"E","Modifier":"LeftShift"}] },
                                    { type: "button", label: "4", press: [{"Key":"R","Modifier":"LeftShift"}] },
                                    { type: "button", label: "5", press: [{"Key":"T","Modifier":"LeftShift"}] },
                                ]
                            },
                            {
                                type: "subpanel",
                                controls: [
                                    { type: "label", label: "Base Stats" },
                                    { type: "button", label: "0", press: [{"Key":"P","Modifier":"RightShift"}] },
                                    { type: "button", label: "1", press: [{"Key":"Q","Modifier":"RightShift"}] },
                                    { type: "button", label: "2", press: [{"Key":"W","Modifier":"RightShift"}] },
                                    { type: "button", label: "3", press: [{"Key":"E","Modifier":"RightShift"}] },
                                    { type: "button", label: "4", press: [{"Key":"R","Modifier":"RightShift"}] },
                                    { type: "button", label: "5", press: [{"Key":"T","Modifier":"RightShift"}] },
                                ]
                            },
                        ]
                    },                    
                    {
                        id: "game",
                        title: "Game",
                        controls: [
                            { type: "button", label: "Pause", press: [{"Key":"H"}] },
                        ]
                    },
                ]
            }
        ]
    }
}
