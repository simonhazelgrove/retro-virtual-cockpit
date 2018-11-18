import $ from 'jquery';

export default function log() {
    return {
        logFade: null,
        logElement: $("#messageCode"),
        logError: function (errorText) {
            console.log(errorText);
            document.getElementById("error-text").textContent = errorText;
        },
        logMessage: function (message) {
            if (this.logElement.hasClass("invisible")) {
                return;
            }
            if (this.logFade != null) {
                clearTimeout(this.logFade);
            }
            this.logElement.text(message);
            this.logElement.animate({ opacity: 1 }, 100);
    
            this.logFade = setTimeout(this.fadeOutLog(), 3000);
        },
        fadeOutLog: function () {
            this.logElement.animate({ opacity: 0 }, 1000);
            this.logFade = null;
        }
    }
}
