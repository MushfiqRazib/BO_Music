
$(document).ready(function() {

    if ($('.confirm-button').attr('disabled') == true) {


        ShowOrderComplete();

    }

   



});


function ShowOrderComplete() {


    var lastStep = $('.order-step-header-container div:last');


    lastStep.prev().addClass('order-step-right-panel').removeClass('order-step-left-panel').find('span').css('color', 'white');


    lastStep.addClass('order-step-left-panel').removeClass('order-step-right-panel').find('span').css('color', 'black');

    $('.content-header-container span').text(lastStep.text());




 }