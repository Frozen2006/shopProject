﻿function Bookin(hour, day, mounth, year, slotType) {
    $("#myModal").find("#bookThis")[0].onclick = function () { SendBook(hour, day, mounth, year, slotType); };
    $("#time-place")[0].innerHTML = hour + ":00-" + (hour + 1) + ":00 " + mounth + "/" + day + "/" + year;
    $("#myModal").modal('show');
}

function closeModal() {
    $("#myModal").modal('hide');
}


function SendBook(hour, day, mounth, year, slotType) {
    $.ajax({
        url: "/TimeSlots/Book",
        data: { hour: hour, day: day, mounth: mounth, year: year, slotType: slotType },
        type: "POST",
        success: function (data) {
            $("td#" + hour + "-" + day + "-" + mounth + "-" + year)[0].style.background = "green";
            $("#myModal").modal('hide');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}