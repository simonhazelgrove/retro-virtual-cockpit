import $ from 'jquery';
import cookies from './cookie';
import websockets_ from './websockets';
import device from './device';
import cockpit_ui from './cockpit-ui';
import cockpit_configs from './cockpit-configs'

$(function () {

    /* Connect UI & Menu */

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

    $("#debug").click(function () {
        $("#messageCode").toggleClass("not-hidden");
    });

    $("#nightmode").change(function () {
        $("html").toggleClass("night");
    });

    $("#fullscreen").click(function () {
        device().requestFullScreen();
    });

    cockpit_ui().setupConfigMenu(cockpit_configs());
});