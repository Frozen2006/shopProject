    $(function () {
        $("#searchArea").autocomplete({
            appendTo: "#ui-widget-autoc",
            source: function (request, response) {
                $.ajax
                ({
                    url: "/Search/AutocompleatRow",
                    data: "{ 'data': '" + request.term + "' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response(data);
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                    }
                });
            },
            minLength: 2,
            delay: 150,
            select: function (event, ui) {
                var item = ui.item;

                //document.getElementById("City").value = item.city;
            }
        });

    });

