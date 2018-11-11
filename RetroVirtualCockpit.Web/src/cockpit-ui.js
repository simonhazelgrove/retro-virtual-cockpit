import $ from 'jquery';
import audio from './audio';
import websockets_ from './websockets';

export default function cockpit_ui() {
    return {
        setupButtonControl: function (control, config) {
            control.on("click", function (event) {
                audio().bumpSound.play();
                if (config.press) {
                    websockets_.sendMessage(config.press);
                }
            });
        },
        setupHorizontalButtonControl: function (control, config) {
            control.on("click", function (event) {
                audio().bumpSound.play();
                var middleX = control.width() / 2;
                var offsetX = control.offset().left;
                var posX = event.pageX - offsetX;
                if (posX >= middleX && config.right) {
                    websockets_.sendMessage(config.right);
                } else if (posX < middleX && config.left) {
                    websockets_.sendMessage(config.left);
                }   
            });
        },
        setupSwitchControl: function (control, config) {
            control = control.find("div");
            control.on("click", function (event) {

                audio().switchSound.play();

                if (config.flip) {
                    websockets_.sendMessage(config.flip);
                }

                if (control.hasClass("on")) {
                    if (config.off) {
                        websockets_.sendMessage(config.off);
                    }
                } else {
                    if (config.on) {
                        websockets_.sendMessage(config.on);
                    }
                }
                control.toggleClass("on");
            });
        },
        setupKnobControl: function (control, config) {
            control = control.find("div");
            control.on("click", function (event) {
                audio().bumpSound.play();

                if (config.turn) {
                    websockets_.sendMessage(config.turn);
                }

                // Rotate control
                var rotation = control.data("rotate");
                if (!rotation) rotation = 0;
                rotation += 20;
                control.css({ 'transform': 'rotate(' + rotation + 'deg)' });
                control.data("rotate", rotation);
            });
        },
        setupThrottleControl: function (control, config) {
            var handle = control.find(".throttle-handle");
            // Double click top or bottom of throttle to set to min / max
            if (config.min || config.max) {
                var middleY = control.height() / 2;
                var offsetY = control.offset().top;
                control.on("dblclick", function (event) {
                    var posY = event.pageY - offsetY;
                    if (posY >= middleY && config.min) {
                        websockets_.sendMessage(config.min);
                        handle.animate({ top: 40 }, 200);
                    } else if (posY < middleY && config.max) {
                        websockets_.sendMessage(config.max);
                        handle.animate({ top: -40 }, 200);
                    }
                });
            }
            // Nudge throttle up & down by swiping 
            if (config.up || config.down) {
                handle.on("swipeup", function (event) {
                    handle.animate({ top: -40 }, 200)
                        .animate({ top: 0 }, 200);
                        websockets_.sendMessage(config.up);
                });
                handle.on("swipedown", function (event) {
                    handle.animate({ top: 40 }, 200)
                        .animate({ top: 0 }, 200);
                        websockets_.sendMessage(config.down);
                });
                
                var dragStartY = 0;
                var dragY = 0;

                handle.on("mousedown", function (event) {
                    dragStartY = event.pageY;
                    $(document).on("mousemove", function (event) {
                        // During drag
                        dragY = event.pageY - dragStartY;
                        dragY = Math.max(Math.min(dragY, 40), -40);
                        handle.css("top", dragY + "px");
                    });
                    $(document).on("mouseup", function (event) {
                        // End drag
                        $(document).off("mousemove");
                        $(document).off("mouseup");
                        if (dragY < 0 && config.up) {
                            websockets_.sendMessage(config.up);
                        } else if (dragY > 0 && config.down) {
                            websockets_.sendMessage(config.down);
                        }
                        handle.animate({ top: 0 }, 200);
                    });
                });
            }
        },
        setupHandleControl: function (control, config) {
            if (config.pull) {
                // Pull handle by swiping down
                control.on("swipedown", function (event) {
                    control.animate({ top: 20 }, 200)
                        .animate({ top: 0 }, 200);
                        websockets_.sendMessage(config.pull);
                });
                // Pull handle by dragging down
                control.on("mousedown", function (event) {
                    var dragStartY = event.pageY;
                    var dragY = 0;
                    $(document).on("mousemove", function (event) {
                        // During drag
                        dragY = event.pageY - dragStartY;
                        dragY = Math.max(Math.min(dragY, 20), 0);
                        control.css("top", dragY + "px");
                    });
                    $(document).on("mouseup", function (event) {
                        // End drag
                        $(document).off("mousemove");
                        $(document).off("mouseup");
                        if (dragY == 20) {
                            websockets_.sendMessage(config.pull);
                        }
                        control.animate({ top: 0 }, 200);
                    });
                });
            }
        },
        setupDpadControl: function (control, config) {
            control.on("click", function (event) {
                audio().bumpSound.play();
                var thirdX = control.width() / 3;
                var thirdY = control.height() / 3;
                var offsetX = control.offset().left;
                var offsetY = control.offset().top;
                var posX = Math.floor((event.pageX - offsetX) / thirdX);
                var posY = Math.floor((event.pageY - offsetY) / thirdY);
                if (posY == 0 && config.up) {
                    websockets_.sendMessage(config.up);
                } else if (posY == 2 && config.down) {
                    websockets_.sendMessage(config.down);
                }
                if (posX == 0 && config.left) {
                    websockets_.sendMessage(config.left);
                } else if (posX == 2 && config.right) {
                    websockets_.sendMessage(config.right);
                }
            });
        },
        configureControl: function (config) {
            var control = $("#" + config.id);

            switch (config.type) {
                case "button": this.setupButtonControl(control, config); break;
                case "button-h": this.setupHorizontalButtonControl(control, config); break;
                case "switch": this.setupSwitchControl(control, config); break;
                case "knob": this.setupKnobControl(control, config); break;
                case "throttle": this.setupThrottleControl(control, config); break;
                case "handle": this.setupHandleControl(control, config); break;
                case "button-dpad": this.setupDpadControl(control, config); break;
            }
        },
        addControl: function (config, container) {
            var slot = $("<div>", { class: "control-slot" });
            if (config.decoration) {
                slot.addClass(config.decoration);
            }
            var control = $("<div>", { class: "control " + config.type });
            slot.append(control);
            switch (config.type) {
                case "button":
                case "button-red":
                case "button-h":
                case "button-dpad":
                case "switch":
                case "knob":
                    if (config.label) {
                        slot.append($("<p>" + config.label + "</p>"));
                    }
                    break;
                case "throttle":
                    control.append($("<div>", { class: "throttle-base" }));
                    control.append($("<div>", { class: "throttle-handle" }));
                    break;
                case "handle":
                    slot.id = "controls-eject";
                    slot.addClass("centered");
                    slot.addClass("large");
                    slot.addClass("control");
                    if (config.label) {
                        control.append($("<p>" + config.label + "</p>"));
                    }
                    break;
            }
            return slot;
        },
        setupControl: function (config, container) {
            if (config.controls) {
                this.setupPanel({
                    decoration: "none",
                    align: "horizontal",
                    controls: config.controls
                }, container);
            } else {
                var control = this.addControl(config, container);
                config.id = control.id;
                switch (config.type) {
                    case "button": 
                    case "button-red":
                        this.setupButtonControl(control, config);
                        break;
                    case "button-h": this.setupHorizontalButtonControl(control, config); break;
                    case "switch": this.setupSwitchControl(control, config); break;
                    case "knob": this.setupKnobControl(control, config); break;
                    case "throttle": this.setupThrottleControl(control, config); break;
                    case "handle": this.setupHandleControl(control, config); break;
                    case "button-dpad": this.setupDpadControl(control, config); break;
                }
                container.append(control);
            }
        },
        setupPanel: function (config, container) {
            var panel = $("<div>", { class: "control-panel" });
            if (config.orientation) {
                panel.addClass(config.orientation);
            }
            if (config.align) {
                panel.addClass(config.align);
            }
            if (!config.decoration || config.decoration != "none") {
                panel.append("<span class='screw top-left'></span>");
                panel.append("<span class='screw bottom-left'></span>");
            }
            if (config.title) {
                panel.append("<h1>" + config.title + "</h1>");
            }
            var me = this;
            config.controls.forEach(function (controlConfig) { me.setupControl(controlConfig, panel) });
            if (!config.decoration || config.decoration != "none") {
                panel.append("<span class='screw top-right'></span>");
                panel.append("<span class='screw bottom-right'></span>");
            }
            container.append(panel);
        },
        setupPanelRow: function (config, container) {
            var panelRow = $("<div>", { class: "control-panel-row" });
            var me = this;
            config.panels.forEach(function (panelConfig) { me.setupPanel(panelConfig, panelRow) });
            container.append(panelRow);
        },
        setupCockpit: function (config) {
            var container = $("#virtual-cockpit");
            container.empty();
            var me = this;
            config.panelRows.forEach(function (panelRowConfig) { me.setupPanelRow(panelRowConfig, container) });
        },
        setupConfigMenu: function (configs) {
            var configMenu = $("#configMenu");
            var me = this;
            configs.forEach(function(config) {
                var link = $("<li class='menu-item'><a href='#'>" + config.title + "</a></li>");
                link.click(function () {
                    me.setupCockpit(config);
                    $("#configName").text(config.title);
                });
                configMenu.append(link);
            });
        }
    }
}
