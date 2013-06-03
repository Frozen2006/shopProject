function closeModal() {
    $("#myModal").modal('hide');
}
function askRemove(productId) {
    $("#myModal").find("#delete")[0].onclick = function () { remover(productId); };
    $("#myModal").modal('show');
}

function newAlert(type, message) {
    $("#alert-area").append($("<div id='alertBlock' class='alert " + type + " fade in' data-alert>" + '<button type="button" class="close" data-dismiss="alert">&times;</button> ' + message + " </div>"));
    $("#alertBlock").delay(2000).fadeOut("slow", function () { $(this).remove(); });
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
                $("div#" + productId).find(".counter")[0].innerHTML = data[i].count;
                $("div#" + productId).find(".estimated_price_value")[0].innerHTML = data[i].positionPrice;

                var rem = $("div#" + productId).find("#update");

                if (rem.length > 0) {
                    rem[0].remove();
                }
            }
            $("#totalPrice")[0].innerHTML = data[0].totalPrice;




            $("#UpdateAll").prop("disabled", true);

            newAlert("alert-success", "All product successful update!");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            newAlert("alert-error", "Waring! Connection error! Try update you page.");
        }
    });
}

function getDataFromId(productId) {
    var boxWithCount = $("div#" + productId).find(".counter")[0];
    var val = boxWithCount.value;

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
    var boxWithCount = $("div#" + productId).find(".counter")[0];
    var val = boxWithCount.value;

    sendAjaxToChange(productId, val);
}

function sendAjaxToChange(productId, newVal) {
    $.ajax({
        url: "/Cart/SetNewValue",
        data: { productId: productId, count: newVal },
        type: "POST",
        success: function (data) {
            $("div#" + productId).find(".counter")[0].value = newVal;
            $("div#" + productId).find(".estimated_price_value")[0].innerHTML = data.positionPrice;
            $("#totalPrice")[0].innerHTML = data.totalPrice;

            $("div#" + productId).find("#update")[0].remove();
            
            $("#UpdateAll").prop("disabled", true);
            
            newAlert("alert-success", "Product count successful updated!");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            newAlert("alert-error", "Waring! Connection error! Try update you page.");
        }
    });
}

function brutforceOnce() {
    var productBox = $("div.product_box");

    var outData = [];

    for (var i in productBox) {
        var prodId = productBox[i].id;
        if (!isNaN(prodId)) {
            incriment(prodId);
        }
    }

    updateAll();
}

function brut(times) {
    
    for (var i = 0; i < times; i++) {
        brutforceOnce();
    }
}

function brut2(times, id) {

    for (var i = 0; i < times; i++) {
        $.ajax({
            url: "/Cart/SetNewValue",
            data: { productId: id, count: 1 },
            type: "POST",
            success: function (data) {
                console.log("success sending");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                newAlert("alert-error", "Waring! Connection error! Try update you page.");
            }
        });
    }
}

function constructSlider(sliderClass, step, min) {
        $("." + sliderClass).slider({
        range: "min",
        min: min,
        max: 10,
        value: 0,
        step: step,
        slide: function (event, ui) {
            var counter = event.target.parentNode.getElementsByClassName("counter")[0];
            counter.value = ui.value;

            $(counter).change();

            enableUpdateBtn(counter.id);
        }
    });

}

function enableUpdateBtn(productId) {
    $("#UpdateAll").prop("disabled", false);

    if ($("div#" + productId).find("#update").length == 0) {
        var btnCode = "<input type=\"button\" id=\"update\" class=\"btn btn-warning\" style=\"margin-right: 5px\" onclick=\"update(" + productId + ")\" value=\"Update\">";
        var deleteBtn = $("div#" + productId).find("#updateArea")[0];
        $(btnCode).appendTo(deleteBtn);
    }
}

function updateSliders() {
    var sliders = $(".slider");
    
    for (var sNum in sliders) {
        if (!isNaN(sNum)) {
            var count = getCounter(sliders[sNum]).value;


            $(sliders[sNum]).slider("value", count);
        }
    }

}

$(function () {
    constructSlider("int_slider", 1, 1);
    constructSlider("float_slider", 0.1, 0.1);
    updateSliders();
});

function countInput(event, price, sliderClass) {
    slider = getSlider(event.target);

    var stringCount = event.target.value;

    //Parsing input value. 
    var count;
    if (sliderClass == "int_slider") {
        count = parseInt(stringCount) || 0;
    }
    else {
        count = parseFloat(stringCount.replace(",", ".")) || 0;
    }

    //If can't parse than set it to 0.
    //event.target.value = count;


    $(slider).slider("value", count);

    //Set estimated price.
    var pricespan = getPrice(event.target);
    pricespan.innerHTML = (count * price).toFixed(2);
}

function correctInput(event, sliderClass) {

    enableUpdateBtn(event.target.id);

    var stringCount = event.target.value;

    //Trying to parse input
    var count;
    if (sliderClass == "int_slider") {
        count = parseInt(stringCount) || 0;
    }
    else {
        count = parseFloat(stringCount.replace(",", ".")).toFixed(1) || 0;
    }

    //If can't parse than set it to 0.
    event.target.value = count;
}


//Returns slider by its sibling node (e.g. button event sender)
function getSlider(element) {
    return element.parentNode.getElementsByClassName("slider")[0];
}

//Returns counter element by its sibling node (e.g. button event sender)
function getCounter(element) {
    return element.parentNode.getElementsByClassName("counter")[0];
}

function getPrice(element) {
    return element.parentNode.getElementsByClassName("estimated_price_value")[0];
}