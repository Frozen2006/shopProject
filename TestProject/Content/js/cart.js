function closeModal() {
    $("#myModal").modal('hide');
}
function askRemove(productId) {
    $("#myModal").find("#delete")[0].onclick = function () { remover(productId); };
    $("#myModal").modal('show');
}

function remover(productId) {
    $.ajax({
        url: "/Cart/Remove",
        data: { productId: productId },
        type: "POST",
        success: function (data) {
            $("div#" + productId).remove();
            $("#totalPrice")[0].innerHTML = data;
            $("#myModal").modal('hide');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}

function updateAll() {
    var productBox = $("div.product_box");

    var outData = [];

    for(var i in productBox)
    {
        var prodId = productBox[i].id;
        if (!isNaN(prodId)) {
            outData.push(getDataFromId(prodId));
        }
    }

    var ids = [];
    var counts = [];
    
    for (var i in outData) {
        ids.push(outData[i].Id);
        counts.push(outData[i].Count);
    }


    $.ajax({
        url: "/Cart/SetNewValueToAll",
        data: { Id: ids, Count: counts },
        traditional: true,
        type: "POST",
        success: function (data) {
            var productBox = $("div.product_box");
            for (var i in data) {
                var productId = data[i].Id;
                var sss = $("div#" + productId).find("#count");
                $("div#" + productId).find("#count")[0].innerHTML = data[i].count;
                $("div#" + productId).find("#price")[0].innerHTML = data[i].positionPrice;

                var rem = $("div#" + productId).find("#update");

                if (rem.length > 0) {
                    rem[0].remove();
                }
            $("div#" + productId).find("#count")[0].className = "badge";
            }
            $("#totalPrice")[0].innerHTML = data[0].totalPrice;




            $("#UpdateAll").prop("disabled", true);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            location.reload();
        }
    });
}


function getDataFromId(productId) {
    var boxWithCount = $("div#" + productId).find("#count")[0];
    var val = boxWithCount.innerHTML;

    return { Id: productId, Count: val };
}

function decrement(productId) {
    var boxWithCount = $("div#" + productId).find("#count")[0];
    var boxWithPrice = $("div#" + productId).find("#price")[0];
    var oldVal = boxWithCount.innerHTML;
    var newVal = oldVal;
    if (oldVal > 1) {
        newVal--;
    }
    var priceOfOneItem = parseFloat(boxWithPrice.innerHTML.replace(",", ".")) / parseFloat(oldVal);
    var newPrice = priceOfOneItem * newVal;
    boxWithPrice.innerHTML = newPrice.toFixed(2);


    boxWithCount.className = "badge badge-warning";
    boxWithCount.innerHTML = newVal;

    $("#UpdateAll").prop("disabled", false);

    if ($("div#" + productId).find("#update").length == 0) {
        var btnCode = "<input type=\"button\" id=\"update\" class=\"btn btn-warning\" style=\"margin-right: 5px\" onclick=\"update(" + productId + ")\" value=\"Update\">";
        var deleteBtn = $("div#" + productId).find("#updateArea")[0];
        $(btnCode).appendTo(deleteBtn);
    }
    // sendAjaxToChange(productId, newVal);
}

function incriment(productId) {
    var boxWithCount = $("div#" + productId).find("#count")[0];
    var boxWithPrice = $("div#" + productId).find("#price")[0];
    var oldVal = boxWithCount.innerHTML;
    var newVal = oldVal;
    newVal++;
    
    var priceOfOneItem = parseFloat(boxWithPrice.innerHTML.replace(",", ".")) / parseFloat(oldVal);
    var newPrice = priceOfOneItem * newVal;
    boxWithPrice.innerHTML = newPrice.toFixed(2);


    boxWithCount.className = "badge badge-warning";
    boxWithCount.innerHTML = newVal;
    
    $("#UpdateAll").prop("disabled", false);

    if ($("div#" + productId).find("#update").length == 0) {
        var btnCode = "<input type=\"button\" id=\"update\" class=\"btn btn-warning\" style=\"margin-right: 5px\" onclick=\"update(" + productId + ")\" value=\"Update\">";
        var deleteBtn = $("div#" + productId).find("#updateArea")[0];
        $(btnCode).appendTo(deleteBtn);
    }
    //sendAjaxToChange(productId, newVal);
}


function update(productId) {
    var boxWithCount = $("div#" + productId).find("#count")[0];
    var val = boxWithCount.innerHTML;

    sendAjaxToChange(productId, val);
}

function sendAjaxToChange(productId, newVal) {
    $.ajax({
        url: "/Cart/SetNewValue",
        data: { productId: productId, count: newVal },
        type: "POST",
        success: function (data) {
            $("div#" + productId).find("#count")[0].innerHTML = newVal;
            $("div#" + productId).find("#price")[0].innerHTML = data.positionPrice;
            $("#totalPrice")[0].innerHTML = data.totalPrice;

            $("div#" + productId).find("#update")[0].remove();
            $("div#" + productId).find("#count")[0].className = "badge";
            
            $("#UpdateAll").prop("disabled", true);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            location.reload();
        }
    });
}

