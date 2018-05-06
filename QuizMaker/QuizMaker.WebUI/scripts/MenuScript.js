//bu saçma yöntemden kurtulmalıyım :) 
$(document).ready(function () {
    $.each($('.m-menu__item'), function (index, item) {
        if ($(item).hasClass('m-menu__item--active')) {
            $(item).removeClass('m-menu__item--active');
        }
    });
    if (window.location.pathname.indexOf("Question") !== -1) {
        $('#mitem3').addClass('m-menu__item--active');
    }
    else if (window.location.pathname.indexOf("Lesson") !== -1) {
        $('#mitem2').addClass('m-menu__item--active');
    }
    else if (window.location.pathname.indexOf("CreateExam") !== -1) {
        $('#mitem4').addClass('m-menu__item--active');
    }

    else {
        $('#mitem1').addClass('m-menu__item--active');
    }

});

