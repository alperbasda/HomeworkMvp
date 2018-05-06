var classicAnswerContent =
    '<div class="form-group"><textarea name="classicAnswer" class="form-control" id="classicAnswer" placeholder="Cevap yazın"></textarea></div>';
var trueFalseId = 0;
var testId = 0;
var testContent =
    '<div class="input-group mb-3"><div class="input-group-addon"><input class="my-btn-radio" onclick="testTrue()" name="radioGroup" type="radio"></div><input name="testAnswer-' +
        testId + '" id="testAnswer-' + testId +
        '" type="text" class="form-control" placeholder="Cevap Yazın"><div class="input-group-append"><button type="button" class="btn btn-success btn-outline-secondary" onclick="newTest()">Yeni</button></div></div>';
var trueFalseContent =
    '<div class="input-group mb-3"><div class="input-group-addon"><input onclick="trueFalseTrue(this)" type="checkbox"/></div><input name="trueFalseAnswer-' +
        trueFalseId + '" id="trueFalseAnswer-' + trueFalseId +
        '" type="text" class="form-control" placeholder="Cevap Yazın"><div class="input-group-append"><button type="button" class="btn btn-success btn-outline-secondary" onclick="newTrueFalse()">Yeni</button></div></div>';
var createModalHtml = $('#change-dialog').html();
var deleteModalHtml = '<div class="modal-content"><div class="modal-header"><h5 class="modal-title islem-title">Silme İşlemi</h5><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button></div><div class="modal-body"><div class="m-scrollable" data-scrollbar-shown="true" data-scrollable="true" data-height="200"><div class="form-group lesson-name"></div><asp:literal id="ltr" runat="server"></asp:literal><input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" /><input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" /></div></div><div class="modal-footer"><button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button><button type="button" class="btn btn-primary" id="confirm-delete-item" onclick="DeleteTime()" data-dismiss="modal">Sil</button></div></div>';
var selectedQuestionCount = 0;
var selectedQuestionsModal = "";
var totalPoint = 0;
var eksik = 0;
$(document).ready(function () {
    if ($.trim($('#ContentPlaceHolder1_otoexam').val()).length !== 0) {
        var array = $('#ContentPlaceHolder1_otoexam').val().split(',');
        $.each(array,
            function (index, item) {
                
                if (item.toString() !== "-1") {
                    if (item.toString().length > 0) {
                        console.log(item);
                        $('#' + item.trim()).children('td').eq(5).children('i').click();
                    }
                    
                } else {
                    eksik++;
                }

            });
        if (eksik !== 0) {
            alert(eksik + " tane soru veritabanı yetersizliğinden eklenemedi!!!");
        }

    }

});

function addExamItem(item) {
    if ($(item).parent('td').parent('tr').hasClass('colorSuccess')) {
        $(item).parent('td').parent('tr').removeClass('colorSuccess');
        selectedQuestionCount--;
        $("#" + $(item).parents('td').parents('tr').attr('id') + "-q").remove();
        selectedQuestionsModal = $('#change-dialog').children('.modal-content').children('.modal-body').html();
        totalPoint = totalPoint - parseFloat($(item).parents('tr').children('td').eq(3).html());
    } else {
        $(item).parent('td').parent('tr').addClass('colorSuccess');
        selectedQuestionsModal += "<div id='" + $(item).parents('td').parents('tr').attr('id') + "-q'>" + $(item).parents('td').parents('tr').next().html();
        selectedQuestionsModal += "<br/>Puanı: " + $(item).parents('tr').children('td').eq(3).html().replace(',', '.') + "<br/><hr><br/><hr></div><input type='hidden' name='" + $(item).parents('td').parents('tr').attr('id') + "' id='" + $(item).parents('td').parents('tr').attr('id') + "'>";
        selectedQuestionCount++;
        $('#change-dialog').children('div').remove();
        $('#change-dialog').append('<div class="modal-content"><div class="modal-header"><h5 class="modal-title islem-title">Sinav Oluştur</h5><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button></div><div class="modal-body">' +
            selectedQuestionsModal + '</div></div>');
        totalPoint += parseFloat($(item).parents('tr').children('td').eq(3).html());

    }
    if (totalPoint >= 101) {
        alert("Sınav Puanı 100 ü aştı");
    }
    if ($('#m_header_topbar').children('div').eq(0).children('ul').eq(0).children('li').length === 0) {
        $('#m_header_topbar').children('div').eq(0).children('ul').eq(0).append(
            '<li id="questionLabel" class="m-nav__item m-topbar__user-profile m-topbar__user-profile--img  m-dropdown m-dropdown--medium m-dropdown--arrow m-dropdown--header-bg-fill m-dropdown--align-right m-dropdown--mobile-full-width m-dropdown--skin-light" data-dropdown-toggle="click"><a class="m-nav__link m-dropdown__toggle cursor-point" onclick="GetSelectedQuestionsModal()"><span class="m-topbar__username">Seçimlerim <span class="badge badge-light" id="selected-question-count"> 1</span></span></a></li>');
    } else {
        $('#selected-question-count').text(selectedQuestionCount);
    }
    if (selectedQuestionCount === 0) {
        $('#questionLabel').remove();
    }

}

function GetSelectedQuestionsModal() {
    $('#change-dialog').children('div').remove();
    $('#change-dialog').append('<div class="modal-content"><div class="modal-header"><h5 class="modal-title islem-title">Sinav Oluştur</h5><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button></div><div class="modal-body">' +
        selectedQuestionsModal + '<asp:literal id="ltr" runat="server"></asp:literal><input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" /><input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" /></div><div class="modal-footer"><label>Toplam Puan : ' + totalPoint + '</label><button type="button" id="next-btn" onclick="ExamNext()" class="btn btn-info">Devam</button></div></div>');
    $('#new-item').modal('show');
}

function ExamNext() {
    $('#change-dialog').children('.modal-content').children('.modal-body').children('div').hide();
    $('#change-dialog').children('.modal-content').children('.modal-body').append('<div class="input-group mb-3"><input name="examName" id="examName" type="text" class="form-control" placeholder="Sınav Adı (Zorunlu)"></div>');
    $('#change-dialog').children('.modal-content').children('.modal-body').append('<div class="input-group mb-3"><input name="observer" id="observer" type="text" class="form-control" placeholder="Gözetmen (Zorunlu)"></div>');

    $('#next-btn').html('Kaydet');
    $('#next-btn').attr('onclick', 'SubmitExam()');
    $('#form1').append("<input id='btn-create' type='hidden' onclick='javascript:__doPostBack(\"btnCreateExam\",\"0\");'>");
    $('#form1').attr('action', '../Exam/ExamDisplay.aspx');
}

function SubmitExam() {
    $('#btn-create').click();
}

$('#oto-exam').click(function () {
    $('.islem-title').text('Otomatik Sınav');
    $('#change-dialog').children('div').remove();
    if ($('#ContentPlaceHolder1_TypeSelector option:selected').val() !== "-1") {

        if ($('#ContentPlaceHolder1_TypeSelector option:selected').val() === "0") {
            $('#change-dialog').append('<div class="modal-content"><div class="modal-header"><h5 class="modal-title islem-title">Otomatik Sınav Oluştur</h5><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button></div><div class="modal-body"><div class="form-group"><label>Dogru Yanlış Soru Sayısı </label><input disabled class="form-control" type="number" name="dsay" id="dsay" value="0"></div><div class="form-group"><label>Test Soru Sayısı </label><input disabled class="form-control" type="number" name="tsay" id="tsay" value="0"></div><div class="form-group"><label>Klasik Soru Sayısı </label><input class="form-control" type="number" name="ksay" id="ksay" value="0"></input></div><input type="text" hidden name="ders" id="ders" value="' + $('#ContentPlaceHolder1_LessonSelector option:selected').val() + '"></input><br/><input type="text" hidden name="zorluk" id="zorluk" value="' + $('#ContentPlaceHolder1_DifficultySelector option:selected').val() + '"></input><br/><input type="text" hidden name="tip" id="tip" value="' + $('#ContentPlaceHolder1_TypeSelector option:selected').val() + '"></input><label>' + $('#ContentPlaceHolder1_LessonSelector option:selected').html() + ' dersi için ' + $('#ContentPlaceHolder1_DifficultySelector option:selected').html() + ' sınav oluşturulsun mu?(Soru Tipi : ' + $('#ContentPlaceHolder1_TypeSelector option:selected').html() + ')</label><asp:literal id="ltr" runat="server"></asp:literal><input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" /><input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" /></div><div class="modal-footer"><button type="button" id="next-btn" onclick="OtoExamCreate()" class="btn btn-info" data-dismiss="modal">Oluştur</button></div></div>');
        }
        else if ($('#ContentPlaceHolder1_TypeSelector option:selected').val() === "1") {
            $('#change-dialog').append('<div class="modal-content"><div class="modal-header"><h5 class="modal-title islem-title">Otomatik Sınav Oluştur</h5><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button></div><div class="modal-body"><div class="form-group"><label>Dogru Yanlış Soru Sayısı </label><input disabled class="form-control" type="number" name="dsay" id="dsay" value="0"></div><div class="form-group"><label>Test Soru Sayısı </label><input class="form-control" type="number" name="tsay" id="tsay" value="0"></div><div class="form-group"><label>Klasik Soru Sayısı </label><input disabled class="form-control" type="number" name="ksay" id="ksay" value="0"></input></div><input type="text" hidden name="ders" id="ders" value="' + $('#ContentPlaceHolder1_LessonSelector option:selected').val() + '"></input><br/><input type="text" hidden name="zorluk" id="zorluk" value="' + $('#ContentPlaceHolder1_DifficultySelector option:selected').val() + '"></input><br/><input type="text" hidden name="tip" id="tip" value="' + $('#ContentPlaceHolder1_TypeSelector option:selected').val() + '"></input><label>' + $('#ContentPlaceHolder1_LessonSelector option:selected').html() + ' dersi için ' + $('#ContentPlaceHolder1_DifficultySelector option:selected').html() + ' sınav oluşturulsun mu?(Soru Tipi : ' + $('#ContentPlaceHolder1_TypeSelector option:selected').html() + ')</label><asp:literal id="ltr" runat="server"></asp:literal><input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" /><input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" /></div><div class="modal-footer"><button type="button" id="next-btn" onclick="OtoExamCreate()" class="btn btn-info" data-dismiss="modal">Oluştur</button></div></div>');
        }
        else if ($('#ContentPlaceHolder1_TypeSelector option:selected').val() === "2") {
            $('#change-dialog').append('<div class="modal-content"><div class="modal-header"><h5 class="modal-title islem-title">Otomatik Sınav Oluştur</h5><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button></div><div class="modal-body"><div class="form-group"><label>Dogru Yanlış Soru Sayısı </label><input class="form-control" type="number" name="dsay" id="dsay" value="0"></div><div class="form-group"><label>Test Soru Sayısı </label><input disabled class="form-control" type="number" name="tsay" id="tsay" value="0"></div><div class="form-group"><label>Klasik Soru Sayısı </label><input disabled class="form-control" type="number" name="ksay" id="ksay" value="0"></input></div><input type="text" hidden name="ders" id="ders" value="' + $('#ContentPlaceHolder1_LessonSelector option:selected').val() + '"></input><br/><input type="text" hidden name="zorluk" id="zorluk" value="' + $('#ContentPlaceHolder1_DifficultySelector option:selected').val() + '"></input><br/><input type="text" hidden name="tip" id="tip" value="' + $('#ContentPlaceHolder1_TypeSelector option:selected').val() + '"></input><label>' + $('#ContentPlaceHolder1_LessonSelector option:selected').html() + ' dersi için ' + $('#ContentPlaceHolder1_DifficultySelector option:selected').html() + ' sınav oluşturulsun mu?(Soru Tipi : ' + $('#ContentPlaceHolder1_TypeSelector option:selected').html() + ')</label><asp:literal id="ltr" runat="server"></asp:literal><input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" /><input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" /></div><div class="modal-footer"><button type="button" id="next-btn" onclick="OtoExamCreate()" class="btn btn-info" data-dismiss="modal">Oluştur</button></div></div>');
        }
    }
    else {
        $('#change-dialog').append('<div class="modal-content"><div class="modal-header"><h5 class="modal-title islem-title">Otomatik Sınav Oluştur</h5><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button></div><div class="modal-body"><div class="form-group"><label>Dogru Yanlış Soru Sayısı </label><input class="form-control" type="number" name="dsay" id="dsay" value="0"></div><div class="form-group"><label>Test Soru Sayısı </label><input class="form-control" type="number" name="tsay" id="tsay" value="0"></div><div class="form-group"><label>Klasik Soru Sayısı </label><input class="form-control" type="number" name="ksay" id="ksay" value="0"></input></div><input type="text" hidden name="ders" id="ders" value="' + $('#ContentPlaceHolder1_LessonSelector option:selected').val() + '"></input><br/><input type="text" hidden name="zorluk" id="zorluk" value="' + $('#ContentPlaceHolder1_DifficultySelector option:selected').val() + '"></input><br/><input type="text" hidden name="tip" id="tip" value="' + $('#ContentPlaceHolder1_TypeSelector option:selected').val() + '"></input><label>' + $('#ContentPlaceHolder1_LessonSelector option:selected').html() + ' dersi için ' + $('#ContentPlaceHolder1_DifficultySelector option:selected').html() + ' sınav oluşturulsun mu?(Soru Tipi : Karışık)</label><asp:literal id="ltr" runat="server"></asp:literal><input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" /><input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" /></div><div class="modal-footer"><button type="button" id="next-btn" onclick="OtoExamCreate()" class="btn btn-info" data-dismiss="modal">Oluştur</button></div></div>');
    }


    if ($('#ContentPlaceHolder1_LessonSelector').val() === "-1") {
        alert("Lütfen Ders Seçiniz");
    }
    else if ($('#ContentPlaceHolder1_DifficultySelector').val() === "-1") {
        alert("Lütfen Sınav Zorluğunu seçiniz");
    } else {
        $('#next-btn').html('Oluştur');
        $('#next-btn').attr('onclick', 'OtoExamCreate()');
        $('#form1').append("<input id='btn-createoto' type='hidden' onclick='javascript:__doPostBack(\"btnCreateExam\",\"0\");'>");
        $('#form1').attr('action', '../Question/ListQuestions.aspx');
        $('#new-item').modal('show');
    }
});

function OtoExamCreate() {
    $('#btn-createoto').click();
}



$('#ContentPlaceHolder1_TypeSelector').change(function () {
    $('#Search').val("");
    $.each($('.first-tr'), function (index1, itemtr) {
        if ($(itemtr).hasClass('item-hidden2')) {
            $(itemtr).removeClass('item-hidden2');
        }
        if ($(itemtr).children().eq(2).html().toLowerCase() !== 'tip') {
            if (KontrolDifficulty(itemtr)) {
                if (KontrolType(itemtr)) {
                    if (KontrolLesson(itemtr)) {
                        if ($(itemtr).hasClass('item-hidden')) {
                            $(itemtr).removeClass('item-hidden');
                        }
                    } else {
                        $(itemtr).addClass('item-hidden');
                    }
                } else {
                    $(itemtr).addClass('item-hidden');
                }
            } else {
                $(itemtr).addClass('item-hidden');
            }
        }

    });
});

function difficultyChange() {
    $('#Search').val("");
    $.each($('.first-tr'), function (index1, itemtr) {
        if ($(itemtr).hasClass('item-hidden2')) {
            $(itemtr).removeClass('item-hidden2');
        }
        if ($(itemtr).children().eq(2).html().toLowerCase() !== 'tip') {
            if (KontrolDifficulty(itemtr)) {
                if (KontrolType(itemtr)) {
                    if (KontrolLesson(itemtr)) {
                        if ($(itemtr).hasClass('item-hidden')) {
                            $(itemtr).removeClass('item-hidden');
                        }
                    } else {
                        $(itemtr).addClass('item-hidden');
                    }
                } else {
                    $(itemtr).addClass('item-hidden');
                }
            } else {
                $(itemtr).addClass('item-hidden');
            }

        }

    });

};

$('#ContentPlaceHolder1_DifficultySelector').on('change', function () {
    difficultyChange();
});

$('#ContentPlaceHolder1_LessonSelector').on('change', function () {
    $('#Search').val("");
    $.each($('.first-tr'), function (index1, itemtr) {
        if ($(itemtr).hasClass('item-hidden2')) {
            $(itemtr).removeClass('item-hidden2');
        }
        if ($(itemtr).children().eq(2).html().toLowerCase() !== 'tip') {
            if (KontrolDifficulty(itemtr)) {
                if (KontrolType(itemtr)) {
                    if (KontrolLesson(itemtr)) {
                        if ($(itemtr).hasClass('item-hidden')) {
                            $(itemtr).removeClass('item-hidden');
                        }
                    } else {
                        $(itemtr).addClass('item-hidden');
                    }
                } else {
                    $(itemtr).addClass('item-hidden');
                }
            } else {
                $(itemtr).addClass('item-hidden');
            }

        }

    });
});

function KontrolDifficulty(itemtr) {
    if ($('#ContentPlaceHolder1_DifficultySelector option:selected').html().toLowerCase() !== "zorluk seçin") {
        if ($(itemtr).children().eq(1).html().toLowerCase().indexOf($('#ContentPlaceHolder1_DifficultySelector option:selected').html().toLowerCase()) !== -1) {
            return true;
        }
        else {
            return false;
        }
    }
    return true;

};
function KontrolLesson(itemtr) {
    if ($('#ContentPlaceHolder1_LessonSelector option:selected').html().toLowerCase() !== "ders seçin") {
        if ($(itemtr).children().eq(0).html().toLowerCase()
            .indexOf($('#ContentPlaceHolder1_LessonSelector option:selected').html().toLowerCase()) !==
            -1) {
            return true;
        } else {
            return false;
        }
    }
    return true;


};

function KontrolType(itemtr) {
    if ($('#ContentPlaceHolder1_TypeSelector option:selected').html().toLowerCase() !== "soru tipi seçin") {

        if ($(itemtr).children().eq(2).html().toLowerCase()
            .indexOf($('#ContentPlaceHolder1_TypeSelector option:selected').html().toLowerCase()) !==
            -1) {
            return true;
        } else {
            return false;
        }
    }
    return true;
};

$('#new-question').click(function () {
    $('#form1').attr('action', '../Question/ListQuestions.aspx');
    $('#change-dialog').children('div').remove();
    $('#form1').append("<input type='hidden' type='button' class='btnDelete' onclick='javascript:__doPostBack(\"btnCreate\",\"0\");' >");
    $('#change-dialog').append(createModalHtml);
});

function removeItem(item) {
    $('#form1').attr('action', '../Question/ListQuestions.aspx');
    $('#change-dialog').children('div').remove();
    $('#change-dialog').append(deleteModalHtml);
    $('#form1').append("<input type='hidden' type='button' class='btnDelete' onclick='javascript:__doPostBack(\"btnDelete\",\"" +
        $(item).parents('td').parents('tr').attr('id') +
        "\");' >");
    $('#new-item').modal('show');
    $('.lesson-name').append('Soru Silinsin mi?');
};

function DeleteTime() {
    $('.btnDelete').click();
};

function CreateItem() {
    $('.btnDelete').click();
};


function ChangeAnswerItem() {
    testId = 0;
    trueFalseId = 0;
    if ($('#ContentPlaceHolder1_selectType option:selected').val().toString() === "0") {
        $('#answer-items').children().remove();
        $('#answer-items').append(classicAnswerContent);
    }
    if ($('#ContentPlaceHolder1_selectType option:selected').val().toString() === "1") {
        $('#answer-items').children().remove();
        $('#answer-items').append(testContent);
        testId++;
    }
    if ($('#ContentPlaceHolder1_selectType option:selected').val().toString() === "2") {
        $('#answer-items').children().remove();
        $('#answer-items').append(trueFalseContent);
        trueFalseId++;
    }
};



function newTrueFalse() {

    var trueFalseContentNext =
        '<div class="input-group mb-3"><div class="input-group-addon"><input onclick="trueFalseTrue(this)" type="checkbox"/></div><input name="trueFalseAnswer-' +
            trueFalseId + '" id="trueFalseAnswer-' + trueFalseId +
            '" type="text" class="form-control" placeholder="Cevap Yazın"><div class="input-group-append"><button type="button" class="btn btn-success btn-outline-secondary" onclick="newTrueFalse()">Yeni</button></div></div>';

    $('#answer-items')
        .append(trueFalseContentNext
        );
    trueFalseId++;
}

function newTest() {

    var testContentNext =
        '<div class="input-group mb-3"><div class="input-group-addon"><input class="my-btn-radio" onclick="testTrue(this)" name="radioGroup" type="radio"></div><input name="testAnswer-' +
            testId + '" id="testAnswer-' + testId +
            '" type="text" class="form-control" placeholder="Cevap Yazın"><div class="input-group-append"><button type="button" class="btn btn-success btn-outline-secondary" onclick="newTest()">Yeni</button></div></div>';

    $('#answer-items')
        .append(testContentNext
        );

    testId++;

}

function trueFalseTrue(item) {
    //is checked ie 8 de desteklenmediği için attr kullanıyorum if ($(item).is(':checked'))
    var input = $(item).parent('div').parent('div').children('input');
    if ($(item).prop('checked')) {
        $(input).attr('id', $(input).attr('id') + '-true');
        $(input).attr('name', $(input).attr('name') + '-true');
    }
    else {
        $(input).attr('id', $(input).attr('id').split('-')[0] + '-' + $(input).attr('id').split('-')[1]);
        $(input).attr('name', $(input).attr('name').split('-')[0] + '-' + $(input).attr('name').split('-')[1]);
    }

}

function testTrue() {
    $.each($('.my-btn-radio'), function (index, itemradio) {
        var input = $(itemradio).parent('div').parent('div').children('input');
        if ($(itemradio).prop('checked')) {
            $(input).attr('id', $(input).attr('id') + '-true');
            $(input).attr('name', $(input).attr('name') + '-true');
        }
        else {
            $(input).attr('id', $(input).attr('id').split('-')[0] + '-' + $(input).attr('id').split('-')[1]);
            $(input).attr('name', $(input).attr('id').split('-')[0] + '-' + $(input).attr('name').split('-')[1]);
        }
    });
}

function editItem(item) {
    $('#form1').attr('action', '../Question/ListQuestions.aspx');
    $('#change-dialog').children('div').remove();
    $('#change-dialog').append(createModalHtml);
    $("#ContentPlaceHolder1_selectLesson > option").each(function (index, item2) {

        if ($(item2).html() === $(item).parents('tr').children('td').eq(0).html()) {
            $('#ContentPlaceHolder1_selectLesson').val($(item2).val());
        }
    });
    $("#ContentPlaceHolder1_selectType > option").each(function (index, item2) {

        if ($(item2).html() === $(item).parents('tr').children('td').eq(2).html()) {
            $('#ContentPlaceHolder1_selectType').val($(item2).val());
        }
    });
    $("#ContentPlaceHolder1_selectDifficulty > option").each(function (index, item2) {

        if ($(item2).html() === $(item).parents('tr').children('td').eq(1).html()) {
            $('#ContentPlaceHolder1_selectDifficulty').val($(item2).val());
        }
    });
    $('#points').val($(item).parents('tr').children('td').eq(3).html().replace(',', '.'));
    $('#question').html($(item).parents('tr').next().children('td').children('label').eq(1).html());

    $('#form1').append("<input type='hidden' type='button' class='btnEdit' onclick='javascript:__doPostBack(\"btnEdit\",\"" + $(item).parents('tr').attr('id') + "\");' >");
    $('.islem-title').html('Soru Düzenle');
    $('#create-item').attr('onclick', 'editTime()');
    $('#new-item').modal('show');
}

function editTime() {
    $('.btnEdit').click();
}

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
