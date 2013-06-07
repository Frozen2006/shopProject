//Scripts for sliders, estimated prices and ajax adding to cart.

//REQUIRES: "alerts.js" to communicate with user

//A counter has the following structure
/*
<div class="counter_box">
    <div class="controls_wrapper">
        <span class="price">Price: <span class="price_value">5.00</span>$</span>
        <div class="slider int_slider"></div>
        <input type="text" class="counter" />
        <span class="estimated_price"> packs for <span class="estimated_price_value">1</span>$</span>
        <input type="hidden" class="product_id" value="123" />
    </div>

    <input type="button" class="add_button btn btn-danger" value="Add" onclick="addToCart(event)" />
</div>
*/

//The following methods help to get counter elements (product id, estimated price etc.)
//to get an easy acces to them from event handlers

//Returns counter input(<input type="text" class="counter" />) by the slider or it's sibling  (e.g. button event sender)
function getCounterInput(element) {
    return element.parentNode.getElementsByClassName("counter")[0];
}

//Returns product id by the slider or it's sibling  (e.g. button event sender)
function getId(element) {
    var input = element.parentNode.getElementsByClassName("product_id")[0];
    return parseInt(input.value);
}

//Returns slider by its sibling node (e.g. button event sender)
function getSlider(element) {
    return element.parentNode.getElementsByClassName("slider")[0];
}

//Returns estimatied price span by the slider or it's sibling  (e.g. button event sender)
function getEstimatedPriceSpan(element) {
    return element.parentNode.getElementsByClassName("estimated_price_value")[0];
}

//Gets prodct price (Number) by the slider or it's sibling  (e.g. button or input event sender)
function getPrice(element) {
    var priceSpan = element.parentNode.getElementsByClassName("price_value")[0];
    var string = priceSpan.innerHTML.toString().replace(",", ".");
    return parseFloat(string);
}

//Makes slider from all elements with css-class sliderClass.
//For correct work sliderClass must be "int_slider" or "float_slider"
//the step parameter defines slider's step. It should be 1, 0.1.
function constructSlider(sliderClass, step) {
    $("." + sliderClass).slider({
        range: "min",
        min: 0,
        max: 10,
        value: 0,
        step: step,
        slide: function (event, ui) {
            var counter = getCounterInput(event.target);
            counter.value = ui.value;

            $(counter).change();
            $(ui).change();
        }
    });
}

//Initiates all counters on the page. To create slider you should have the following markup
//<div class="int_slider"></div>
//or
//<div class="float_slider"></div>
$(function () {
    constructSlider("int_slider", 1);
    constructSlider("float_slider", 0.1);
});

//Checks whether slider is float or not by it's css class.
function sliderIsFloat(slider) {
    return $(slider).hasClass("float_slider");
}

//Event handler. Changes the slider value when the price text box (<input type="text">) is changed by user.
//Attatch it to the input "onchange" and "onkeyup" events
function countInput(event) {
    var slider = getSlider(event.target);
    var price = getPrice(event.target);

    //New count value string
    var stringCount = event.target.value;
    var count;
    
    //Parsing input value. 
    if (sliderIsFloat(slider)) {
        count = parseFloat(stringCount.replace(",", ".")) || 0;
    } else {
        count = parseInt(stringCount) || 0;
    }
    
    if (count < 0) {
        count = 0;
    }

    //Setting slider value
    $(slider).slider("value", count);

    //Set estimated price.
    var pricespan = getEstimatedPriceSpan(event.target);
    pricespan.innerHTML = (count * price).toFixed(2);
}

//Event handler. Attatch it to the onBlur event. Then when input loose focus.
//inputted data will automatically converted to a digit.
//(e.g. "0.1asd" => "0.1")
function correctInput(event, sliderClass) {
    var slider = getSlider(event.target);

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
    
    if (count < 0) {
        count = 0;
    }
    
    //Setting a correct value.
    event.target.value = count;
}

//Adding to cart functionality

//Sends an AJAX request to the server to c units of product with Id id
//to the cart
//MVC action: 
//public ActionResult AddToCart(int productId, int count)
function sendRequest(id, c) {
    $.ajax
    ({
        url: "/Product/AddToCart",
        data: { productId: id, count: c.toString().replace(".", ",") },
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

//Event handler for the button that sends add request the server.
function addToCart(event) {
    var id = getId(event.target);

    var counter = getCounterInput(event.target);
    var count = parseFloat(counter.value);

    if (count && count > 0) {
        sendRequest(id, count);
    }
}

//Event handler for the "Add all" button
//Gets all products user want to buy, makes the arrays and sends
//it to the server to add to a cart.
//MVC action: 
//public ActionResult AddArrayToCart(int[] productIds, double[] counts)
function addAll() {

    var productArray = new Array();
    var countArray = new Array();

    var idInputs = document.getElementsByClassName("product_id");

    for (var i = 0; i < idInputs.length; i++) {
        var count = getCounterInput(idInputs[i]);

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