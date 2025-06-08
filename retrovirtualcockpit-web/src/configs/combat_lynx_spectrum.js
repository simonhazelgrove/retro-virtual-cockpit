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
                            { type: "switch", label: "View", flip: [{"key":"M"}] },
                            { type: "button-dpad", label: "Move", up: [{"key":"_7"}], down: [{"key":"_6"}], left: [{"key":"_5"}], right: [{"key":"_8"}] },
                            { type: "button-dpad", label: "Move Fast", up: [{"key":"_7","modifier":"_0"}], down: [{"key":"_6","modifier":"_0"}], left: [{"key":"_5","modifier":"_0"}], right: [{"key":"_8","modifier":"_0"}] },
                        ]
                    },
                    {
                        id: "loadout",
                        title: "Loadout",
                        controls: [
                            { type: "button-h", label: "-    +", left: [{"key":"J"}], right: [{"key":"K"}] },
                            { type: "button", label: "Next", press: [{"key":"Enter"}] },
                            { type: "button", label: "Exit", press: [{"key":"Space"}] },
                        ]
                    },
                    {
                        id: "weapons",
                        title: "Weapons",
                        controls: [
                            { type: "button-h", label: "Select", left: [{"key":"_2"}], right: [{"key":"_9"}] },
                            { type: "switch", label: "Sights", flip: [{"key":"Enter"}] },
                            { type: "button-red", label: "Fire", press: [{"key":"_0"}], decoration: "hazard" },
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
                            { type: "button", label: "Read", press: [{"key":"_1"}] },
                            {
                                type: "subpanel",
                                controls: [
                                    { type: "label", label: "Base Pos" },
                                    { type: "button", label: "0", press: [{"key":"P","modifier":"LeftShift"}] },
                                    { type: "button", label: "1", press: [{"key":"Q","modifier":"LeftShift"}] },
                                    { type: "button", label: "2", press: [{"key":"W","modifier":"LeftShift"}] },
                                    { type: "button", label: "3", press: [{"key":"E","modifier":"LeftShift"}] },
                                    { type: "button", label: "4", press: [{"key":"R","modifier":"LeftShift"}] },
                                    { type: "button", label: "5", press: [{"key":"T","modifier":"LeftShift"}] },
                                ]
                            },
                            {
                                type: "subpanel",
                                controls: [
                                    { type: "label", label: "Base Stats" },
                                    { type: "button", label: "0", press: [{"key":"P","modifier":"RightShift"}] },
                                    { type: "button", label: "1", press: [{"key":"Q","modifier":"RightShift"}] },
                                    { type: "button", label: "2", press: [{"key":"W","modifier":"RightShift"}] },
                                    { type: "button", label: "3", press: [{"key":"E","modifier":"RightShift"}] },
                                    { type: "button", label: "4", press: [{"key":"R","modifier":"RightShift"}] },
                                    { type: "button", label: "5", press: [{"key":"T","modifier":"RightShift"}] },
                                ]
                            },
                        ]
                    },                    
                    {
                        id: "game",
                        title: "Game",
                        controls: [
                            { type: "button", label: "Pause", press: [{"key":"H"}] },
                        ]
                    },
                ]
            }
        ]
    }
}
