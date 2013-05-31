function Bookin(hour, day, mounth, year) {
    $("#myModal").find("#bookThis")[0].onclick = function () { SendBook(hour, day, mounth, year); };
    $("#time-place")[0].innerHTML = hour + ":00-" + (hour + 1) + ":00 " + mounth + "/" + day + "/" + year;
    $("#myModal").modal('show');
}

function closeModal() {
    $("#myModal").modal('hide');
}


function SendBook(hour, day, mounth, year) {
    $.ajax({
        url: "/TimeSlots/Book",
        data: { hour: hour, day : day, mounth : mounth, year : year },
        type: "POST",
        success: function (data) {
            $("td#" + hour + "-" + day + "-" + mounth + "-" + year).style.background = "green";
            $("#myModal").modal('hide');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}