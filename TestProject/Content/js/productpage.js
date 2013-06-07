//Slider scripts
    function constructSlider(sliderClass, step) {
        $("." + sliderClass).slider({
            range: "min",
            min: 0,
            max: 10,
            value: 0,
            step: step,
            slide: function (event, ui) {
                var counter = getCounter(event.target);
                counter.value = ui.value;

                $(counter).change();
                $(ui).change();
            }
        });
    }

    $(function () {
        constructSlider("int_slider", 1);
        constructSlider("float_slider", 0.1);
    });

function countInput(event, price, sliderClass)
{
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

//Returns counter element by its sibling node (e.g. button event sender)
function getCounter(element) {
    return element.parentNode.getElementsByClassName("counter")[0];
}

//Returns input hidden element with product id by its sibling node (e.g. button event sender)
function getIdInput(element) {
    return element.parentNode.getElementsByClassName("product_id")[0];
}

//Returns slider by its sibling node (e.g. button event sender)
function getSlider(element) {
    return element.parentNode.getElementsByClassName("slider")[0];
}

//Returns price span by its sibling node (e.g. button event sender)
function getPrice(element) {
    return element.parentNode.getElementsByClassName("estimated_price_value")[0];
}

//Adding to cart scripts

    //public ActionResult AddToCart(int productId, int count)
    function sendRequest(id, c) {
        $.ajax
        ({
            url: "/Product/AddToCart",
            data: { productId: id, count: c.toString().replace(".",",") },
            type: "POST",
            success: function (data) {
                newAlert('alert', data.Report);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                var response = $.parseJSON(XMLHttpRequest.responseText);
    
                newAlert('alert alert-error', response.Report);
            },
            dataType: "json"
        })
    }

function addToCart(event) {
    var input = getIdInput(event.target);
    var id = parseInt(input.value);

    var counter = getCounter(event.target);
    var count = parseFloat(counter.value);

    if (count && count > 0) {
        sendRequest(id, count);
    }
}

//creating an area for alerts

$(function () {
    $("body").append("<div id='alert_area'></div>");
});
    
function newAlert(type, message) {
    $("#alert_area").append($("<div class='alert-message " + type + " fade in' data-alert><p> " + message + " </p></div>"));
    $(".alert-message").delay(2000).fadeOut("slow", function () { $(this).remove(); });
}

function addAll() {

    var productArray = new Array();
    var countArray = new Array();

    var idInputs = document.getElementsByClassName("product_id");

    for (var i = 0; i < idInputs.length; i++) {
        var count = getCounter(idInputs[i]);
    
        var numCount = parseFloat(count.value) || 0;

        if (numCount > 0) {
            productArray.push(idInputs[i].value);
            countArray.push(numCount.toString().replace(".", ","));
        }
    }

    if (productArray.length != 0
        && countArray.length != 0
        && productArray.length == countArray.length) {

        var postData = {
            productIds: productArray,
            counts: countArray
        };

        $.ajax({
            type: "POST",
            url: "/Product/AddArrayToCart",
            data: postData,
            success: function (data) {
                newAlert('alert', data.Report);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                var response = $.parseJSON(XMLHttpRequest.responseText);

                newAlert('alert alert-error', response.Report);
            },
            dataType: "json",
            traditional: true
        });
    }

}