//custom jQuery UI autocompleat (only settings)
    $.widget( "custom.searchautocomplete", $.ui.autocomplete, {
        _renderMenu: function( ul, items ) {
            var that = this,
                currentCategory = "";
            $.each( items, function( index, item ) {
                var perentElement = ul[0];
                var newItem = document.createElement("B");
                newItem.innerHTML = "Price: " + item.price + "$";
                newItem.style.display = "inline";
                newItem.style.float = "right";
                perentElement.appendChild(newItem);
                that._renderItemData(ul, item);

            });
        }
    });

//setup serach autocompleat ajax
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
                      }
                  });
              },
              minLength: 2,
              delay: 150,
              select: function (event, ui) {
                  var item = ui.item;
                  document.location.href = "/Product/Details/" + item.ProductId;
              }
          });

      });