//jQurey UI Alert functionality

//Makes the alert area on the center of the screen.
$(function () {
    $("body").append("<div id='alert_area'></div>");
});

//Shows user the alert pupup message.
//type is a css-class of the alert and it should be
//"alert" or"alert alert-error"
function newAlert(type, message) {
    $("#alert_area").append($("<div class='alert-message " + type + " fade in' data-alert><p> " + message + " </p></div>"));
    $(".alert-message").delay(2000).fadeOut("slow", function () { $(this).remove(); });
}