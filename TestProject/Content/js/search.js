    //custon jQuery UI autocompleat (only settings)
    $.widget( "custom.searchautocomplete", $.ui.autocomplete, {
        _renderMenu: function( ul, items ) {
            var that = this,
                currentCategory = "";
            $.each( items, function( index, item ) {
                var aaa = ul[0];
                var newItem = document.createElement("B");
                newItem.innerHTML = "Price: " + item.price + "$";
                newItem.style.display = "inline";
                newItem.style.float = "right";
                aaa.appendChild(newItem);
                that._renderItemData(ul, item);

            });
        }
    });

      $(function() {
   
          $("#searchArea").searchautocomplete({
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
                          //alert(textStatus);
                      }
                  });
              },
              minLength: 2,
              delay: 150,
              select: function (event, ui) {
                  var item = ui.item;
                  //var regExp = /^(?:http:\/\/)?([^\/]+)/g;
                  //var regRez = regExp.exec(document.URL);

                 // var test = regRez[0] + "/Product/Details/" + item.ProductId;
                  //document.location.href = regRez[0] + "/Product/Details/" + item.ProductId;
                  document.location.href = "/Product/Details/" + item.ProductId;
              }
          });

      });