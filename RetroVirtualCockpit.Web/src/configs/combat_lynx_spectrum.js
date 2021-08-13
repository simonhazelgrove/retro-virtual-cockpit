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
                            { type: "switch", label: "View", flip: "Map.View" },
                            { type: "button-dpad", label: "Move", up: "Map.Up", down: "Map.Down", left: "Map.Left", right: "Map.Right" },
                            { type: "button-dpad", label: "Move Fast", up: "Map.Up.Fast", down: "Map.Down.Fast", left: "Map.Left.Fast", right: "Map.Right.Fast" },
                        ]
                    },
                    {
                        id: "loadout",
                        title: "Loadout",
                        controls: [
                            { type: "button-h", label: "-    +", left: "Load.Less", right: "Load.More" },
                            { type: "button", label: "Next", press: "Load.Next" },
                            { type: "button", label: "Exit", press: "Load.Exit" },
                        ]
                    },
                    {
                        id: "weapons",
                        title: "Weapons",
                        controls: [
                            { type: "button-h", label: "Select", left: "Weapon.Prev", right: "Weapon.Next" },
                            { type: "switch", label: "Sights", flip: "Weapon.Sight" },
                            { type: "button-red", label: "Fire", press: "Weapon.Fire", decoration: "hazard" },
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
                            { type: "button", label: "Read", press: "Msg.Read" },
                            {
                                type: "subpanel",
                                controls: [
                                    { type: "label", label: "Base Pos" },
                                    { type: "button", label: "0", press: "Msg.Base.Pos.0" },
                                    { type: "button", label: "1", press: "Msg.Base.Pos.1" },
                                    { type: "button", label: "2", press: "Msg.Base.Pos.2" },
                                    { type: "button", label: "3", press: "Msg.Base.Pos.3" },
                                    { type: "button", label: "4", press: "Msg.Base.Pos.4" },
                                    { type: "button", label: "5", press: "Msg.Base.Pos.5" },
                                ]
                            },
                            {
                                type: "subpanel",
                                controls: [
                                    { type: "label", label: "Base Stats" },
                                    { type: "button", label: "0", press: "Msg.Base.Status.0" },
                                    { type: "button", label: "1", press: "Msg.Base.Status.1" },
                                    { type: "button", label: "2", press: "Msg.Base.Status.2" },
                                    { type: "button", label: "3", press: "Msg.Base.Status.3" },
                                    { type: "button", label: "4", press: "Msg.Base.Status.4" },
                                    { type: "button", label: "5", press: "Msg.Base.Status.5" },
                                ]
                            },
                        ]
                    },                    
                    {
                        id: "game",
                        title: "Game",
                        controls: [
                            { type: "button", label: "Pause", press: "Game.Pause" },
                        ]
                    },
                ]
            }
        ]
    }
}
