var cont = false;
$('#Search').on('input', function () {
    $.each($('tr'), function (index1, itemtr) {
        if (!$(itemtr).hasClass('item-hidden')) {
            cont = false;
            $.each($(itemtr).children('td'), function (index, item) {





                if ($(item).contents().get(0).nodeValue != null) {
                    if ($(item).contents().get(0).nodeValue.toString().toLowerCase().indexOf($('#Search').val().toLowerCase()) === -1) {
                        if (cont === false) {
                            $(itemtr).addClass('item-hidden2');
                        }
                    }
                    else {
                        if ($(itemtr).hasClass('item-hidden2')) {
                            $(itemtr).removeClass('item-hidden2');
                        }
                        cont = true;

                    }
                }
                if (!$(item).parents('tr').hasClass('first-tr')) {
                    
                    var a = $(item).children('label').eq(1).html().substring(6).toLowerCase()
                        .indexOf($('#Search').val().toLowerCase());
                    
                    if (a !== -1) {
                        
                        if ($(itemtr).prev().hasClass('item-hidden2')) {
                            $(itemtr).prev().removeClass('item-hidden2');
                        }
                    } 
                }

            });
        }
    });


});

