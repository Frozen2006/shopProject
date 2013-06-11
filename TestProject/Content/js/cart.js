//close pop-up window
function closeModal() {
    $("#myModal").modal('hide');
}
//show pop-up window
function askRemove(productId) {
    $("#myModal").find("#delete")[0].onclick = function () { remover(productId); };
    $("#myModal").modal('show');
}

//send remove ajax
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

//pudate count of all product's
function updateAll() {
    var productBox = $("div.product_box");

    var outData = [];

    for(var key in productBox)
    {
        var prodId = productBox[key].id;
        if (!isNaN(prodId)) {
            outData.push(getDataFromId(prodId));
        }
    }

    var ids = [];
    var counts = [];
    
    for (var key in outData) {
        ids.push(outData[key].Id);
        counts.push(outData[key].Count);
    }


    $.ajax({
        url: "/Cart/SetNewValueToAll",
        data: { Id: ids, Count: counts },
        traditional: true,
        type: "POST",
        success: function (data) {
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

            newAlert("alert alert-success", "All product successful update!");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            newAlert("alert alert-error", "Waring! Connection error! Try update you page.");
        }
    });
}

//get product data
function getDataFromId(productId) {
    var boxWithCount = $("div#" + productId).find(".counter")[0];
    var val = boxWithCount.value;
    return { Id: productId, Count: val };
}

//update one product (this method call, when clicked on updae button)
function update(productId) {
    var boxWithCount = $("div#" + productId).find(".counter")[0];
    var val = boxWithCount.value;

    sendAjaxToChange(productId, val);
}

//ajax request to change count of one product
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
            
            newAlert("alert alert-success", "Product count successful updated!");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            newAlert("alert alert-error", "Waring! Connection error! Try update you page.");
        }
    });
}

//create value sliders
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

//show update btn
function enableUpdateBtn(productId) {
    $("#UpdateAll").prop("disabled", false);

    if ($("div#" + productId).find("#update").length == 0) {
        var btnCode = "<input type=\"button\" id=\"update\" class=\"btn btn-warning\" style=\"margin-right: 5px\" onclick=\"update(" + productId + ")\" value=\"Update\">";
        var deleteBtn = $("div#" + productId).find("#updateArea")[0];
        $(btnCode).appendTo(deleteBtn);
    }
}

//setup start value of slidebars
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
    var slider = getSlider(event.target);

    var stringCount = event.target.value;

    //Parsing input value. 
    //Parsing input value. 
    if (sliderIsFloat(slider)) {
        count = parseFloat(stringCount.replace(",", ".")) || 0;
    } else {
        count = parseInt(stringCount) || 0;
    }

    if (count <= 0) {
        if (sliderIsFloat(slider)) {
            count = 0.1;
        } else {
            count = 1;
        }
        
    }

    //If can't parse than set it to 0.
    //event.target.value = count;


    $(slider).slider("value", count);

    //Set estimated Price.
    var pricespan = getPrice(event.target);
    pricespan.innerHTML = (count * price).toFixed(2);
}

//Checks whether slider is float or not by it's css class.
function sliderIsFloat(slider) {
    return $(slider).hasClass("float_slider");
}

function correctInput(event, sliderClass) {
    var slider = getSlider(event.target);

    enableUpdateBtn(event.target.id);

    var stringCount = event.target.value;

    //Trying to parse input
    //If can't parse than set it to 0.
    var count;
    if (sliderIsFloat(slider)) {
        count = parseFloat(stringCount.replace(",", ".")).toFixed(1) || 0;
    }
    else {
        count = parseInt(stringCount) || 0;
    }

    if (count <= 0) {
        if (sliderIsFloat(slider)) {
            count = 0.1;
        } else {
            count = 1;
        }

    }

    //Setting a correct value.
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