export interface CockpitConfig {
    title: string
    panelRows: PanelRow[]
}

export interface PanelRow {
    panels: Panel[]
}

export interface Panel {
    id: string
    title?: string
    decoration?: string
    align?: string
    orientation?: string
    controls: Control[]
}

export interface Control {
    type: string
    label?: string
    decoration?: string
}

export interface SubPanel extends Control {
    controls: Control[]
}

export interface Button extends Control {
    press: string
}

export interface ButtonRed extends Button {
}

export interface DoubleButton extends Control {
    left: string
    right: string
    // or
    up: string
    down: string
}

export interface DPadButton extends Control {
    up: string
    down: string
    left: string
    right: string
}

export interface Knob extends Control {
    turn: string
    values: [string, string][]
}

export interface Switch extends Control {
    flip: string
    on: string
    off: string
}

export interface Handle extends Control {
    pull: string
}

export interface Throttle extends Control {
    up: string
    down: string
    max: string
    min: string
}

export interface Lever extends Control {
    up: string
    down: string
    multistage: boolean
    altColor: boolean
    startInUpPos: boolean
}
