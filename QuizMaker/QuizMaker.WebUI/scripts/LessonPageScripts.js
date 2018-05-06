$('#create-item').on('click', function () {
    if ($('#lesson-name').val() === "") {
        alert("Lütfen Ders Adı Giriniz");
    } else {
        $.ajax({
            url: 'ListLessons.aspx/CreateLesson',
            type: 'POST',
            data: '{name : "' + $('#lesson-name').val() + '"}',
            dataType: 'json',
            contentType: 'application/json; charset:Utf-8',
            success: function (element) {
                $(element.d).prependTo("table > tbody");
            }
        });
    }

});

function removeItem(item) {
    $('.islem-title').html("Silme İşlemi");
    
    $('.islem-title').parent('.modal-header').parent('.modal-content').children('.modal-footer')
        .children('.btn-primary').attr('id', 'confirm-delete-item');

    $('.islem-title').parent('.modal-header').parent('.modal-content').children('.modal-footer')
        .children('.btn-primary').html('Sil');

    if ($("#form1").children('.btnDelete').length > 0) {
        $("#form1").children('.btnDelete').remove();
        $('.lesson-name').children('label').empty();
    }
    if ($("#form1").children('.btnEdit').length > 0) {
        $("#form1").children('.btnEdit').remove();
        $('.lesson-name').children('input').remove();
    }

    $("#form1").append(
        "<input type='hidden' type='button' class='btnDelete' onclick='javascript:__doPostBack(\"btnDelete\",\"" +
        $(item).parents('td').parents('tr').attr('id') +
        "\");' >");
    $('.lesson-name').append('<label class="form-control-label pull-left">' +
        $(item).parents('td').parents('tr').children('td').first().html() +
        " dersi silinsin mi? </label>");
    $('#delete-item').modal('show');
};
$('#confirm-delete-item').click(function () {
    $('.btnDelete').click();
});


function editItem(item) {
    $('.islem-title').text("Düzenleme İşlemi");
    $('.islem-title').parent('.modal-header').parent('.modal-content').children('.modal-footer')
        .children('.btn-primary').attr('id', 'confirm-edit-item');

    $('.islem-title').parent('.modal-header').parent('.modal-content').children('.modal-footer')
        .children('.btn-primary').html('Düzenle');

    if ($("#form1").children('.btnEdit').length > 0) {
        $("#form1").children('.btnEdit').remove();
        $('.lesson-name').children('input').remove();
    }
    if ($("#form1").children('.btnDelete').length > 0) {
        $("#form1").children('.btnDelete').remove();
        $('.lesson-name').children('label').empty();
    }

    $("#form1").append(
        "<input type='hidden' type='button' class='btnEdit' onclick='javascript:__doPostBack(\"btnEdit\",\"" +
        $(item).parents('td').parents('tr').attr('id') +
        "\");' >");
    $('.lesson-name').append('<input name="lessonName"  class="form-control pull-left" placeholder="Yeni ders adı giriniz" value='+$(item).parents('td').parents('tr').children('td').first().html()+'></input>');
    $('#delete-item').modal('show');

    $('#confirm-edit-item').click(function () {
        $('.btnEdit').click();
    });
};


//<![CDATA[
var theForm = document.forms['form1'];
if (!theForm) {
    theForm = document.form1;
}
function __doPostBack(eventTarget, eventArgument) {
    if (!theForm.onsubmit || (theForm.onsubmit() !== false)) {
        theForm.__EVENTTARGET.value = eventTarget;
        theForm.__EVENTARGUMENT.value = eventArgument;
        theForm.submit();
    }
}
//]]>
