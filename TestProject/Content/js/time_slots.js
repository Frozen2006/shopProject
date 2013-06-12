//Shows a modal window with question
function Bookin(hour, day, mounth, year, slotType) {
    $("#myModal").find("#bookThis")[0].onclick = function () { SendBook(hour, day, mounth, year, slotType); };
    $("#time-place")[0].innerHTML = hour + ":00-" + (hour + slotType) + ":00 " + mounth + "/" + day + "/" + year;
    $("#myModal").modal('show');
}

//Send ajax to book time
function SendBook(hour, day, mounth, year, slotType) {
    $.ajax({
        url: "/TimeSlots/Book",
        data: { hour: hour, day: day, mounth: mounth, year: year, slotType: slotType },
        type: "POST",
        success: function (data) {
            var currentSlot = $("td#" + hour + "-" + day + "-" + mounth + "-" + year + "-" + slotType)[0];
            currentSlot.style.background = "#5BB75B";
            currentSlot.onclick = function () { };
            currentSlot.innerHTML = "Your slot";
            $("#myModal").modal('hide');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}