import $ from 'jquery';
import log from './log';

// This module is exported as a singleton so the same connection is available to all users of it
export default {
    connection: null,
    decodeConnectKey: function (clientCode) {
        var decoded = "";
        var codex = "";

        for (var i = 0; i < 5; i++) {
            codex += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }

        for (var i = 0; i < 4; i++) {
            var num = 16 * codex.indexOf(clientCode[0]);

            codex = codex.slice(16);
            clientCode = clientCode.slice(1);

            num += codex.indexOf(clientCode[0]);

            codex = codex.slice(16);
            clientCode = clientCode.slice(1);

            decoded += num;

            if (i < 3) {
                decoded += ".";
            }
        }

        return decoded;
    },
    connect: function () {
        var connectKey = document.getElementById("connect-key").value;
        var ip = this.decodeConnectKey(connectKey.toUpperCase());

        this.connection = new WebSocket("ws://" + ip + ":6437");

        this.connection.onopen = function () {
            $("#connect-panel").slideToggle(1000);
            $("#connected-icon").toggleClass("text-muted");
            $("#connected-icon").toggleClass("text-warning");
            $("#error-text").text("");
        };

        // Log errors
        this.connection.onerror = function (error) {
            log().logError("Connection error - is the client running?");
        };

        // Log messages from the server
        this.connection.onmessage = function (e) {
            log().logError("Server: " + e.data);
        };
    },
    sendMessage: function (value) {
        log().logMessage(value);
        if (this.connection != null) {
            this.connection.send(value);
        }
    }
}