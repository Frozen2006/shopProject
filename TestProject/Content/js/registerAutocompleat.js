//Send request, when user write zip code, and display suggest.
//Use only on registration page
$(function () {
        $("#Zip").autocomplete( {
            source: function (request, response) {
                $.ajax
                ({
                    url: "/Membership/RegisterRow",
                    data: "{ 'term': '" + request.term + "' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response(data);
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                    }
                });
            },
            minLength: 2,
            delay: 150,
            select: function(event, ui) {
                var item = ui.item;

                document.getElementById("City").value = item.city;
            }
        });

    });
