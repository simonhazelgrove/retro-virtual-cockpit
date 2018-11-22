
window.$ = window.jQuery = require('jquery')
window.Popper = require('popper.js')
require('bootstrap')

import cookies from './cookie';
import websockets_ from './websockets';
import device from './device';
import cockpit_ui from './cockpit-ui';
import cockpit_configs from './cockpit-configs'

$(document).on("mobileinit", function () {
    $.mobile.autoInitializePage = false;
});

/* Connect UI & Menu */
$(function () {
    $("#connect-form").submit(function (event) {
        event.preventDefault();
        cookies().setCookie("connect-key", $("#connect-key").val(), 365 * 10);
        websockets_.connect();
    });

    $("#connected-icon").click(function () {
        $("#connect-panel").slideToggle(1000);
    });

    $("#connect-key").val(cookies().getCookie("connect-key"));

    $("#menu-icon").hover(function () {
        $(this).addClass("text-info");
    }, function () {
        $(this).removeClass("text-info");
    });

    $("#debug").change(function () {
        $("#messageCode").toggleClass("invisible");
    });

    $("#nightmode").change(function () {
        $("html").toggleClass("night");
    });

    $("#fullscreen").change(function () {
        if (this.checked) {
            device().requestFullScreen();
        } else {
            device().exitFullScreen();
        }
        $("html").toggleClass("fullscreen");
    });

    $("#displaymode").change(function () {
        var body = $("body");
        if (body.hasClass("minimised") || body.hasClass("single-panel")) {
            body.removeClass("minimised");
            body.removeClass("single-panel");
            $(".control-panel").show();
        } else {
            body.addClass("minimised");
        }
    });

    $("#back-button").click(function() {
        var body = $("body");
        if (body.hasClass("single-panel")) {
            body.removeClass("single-panel");
            body.addClass("minimised");
            $(".control-panel").show();
        }
    });

    cockpit_ui().setupConfigMenu(cockpit_configs());
});