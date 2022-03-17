/* Häufige App-Funktion */

var app = (function () {
    "use strict";

    var app = {};

    // Häufige Initialisierungsfunktion (von jeder Seite aufzurufen)
    app.initialize = function () {
        $('body').append(
            '<div id="notification-message">' +
                '<div class="padding">' +
                    '<div id="notification-message-close"></div>' +
                    '<div id="notification-message-header"></div>' +
                    '<div id="notification-message-body"></div>' +
                '</div>' +
            '</div>');

        $('#notification-message-close').click(function () {
            $('#notification-message').hide();
        });


        // Nach der Initialisierung eine häufige Benachrichtigungsfunktion anzeigen
        app.showNotification = function (header, text) {
            $('#notification-message-header').text(header);
            $('#notification-message-body').text(text);
            $('#notification-message').slideDown('fast');
        };


        app.displayError = function (t, i) {
            $("#errorModal .error-title").html(t);
            $("#errorModal .error-desc").html(i);
            $("#errorModal").modal("show");
            $(".error-dialog").css("margin-top", "7%");
        }, app.closeInfo = function () {
            $("#infoModal").modal("hide");
        }



    };






    return app;
})();


