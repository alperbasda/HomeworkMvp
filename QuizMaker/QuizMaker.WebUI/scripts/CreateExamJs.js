
$(document).ready(function() {
    $('#ContentPlaceHolder1_tableItems').children('table').children('thead').children('tr').children('th').eq(4).html('Gör');
    $('#ContentPlaceHolder1_tableItems').children('table').children('thead').children('tr').children('th').eq(5).html('Cevaplar');
    $.each($('#ContentPlaceHolder1_tableItems').children('table').children('tbody').children('tr'),
        function (index, item) {
            $(item).children('td').eq(5)
                .children('i').removeClass('fa-trash');
            $(item).children('td').eq(5)
                .children('i').addClass('fa-eye');
        });
});

function removeItem(item) {
    window.location = "../Exam/ExamAnswers.aspx?ExamId=" + $(item).parents('td').parents('tr').attr('id');
}

function editItem(item) {
    window.location = "../Exam/ExamDisplay.aspx?ExamId=" + $(item).parents('td').parents('tr').attr('id');
}