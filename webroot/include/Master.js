

CultureName = "en-US";
var CulterInfo = {
            CultureName :  "nl-NL",
            DecimalSeparator : ',',

            GroupSeparator : '.'
}



var backLink = $('<a class="back-link" href="javascript:history.back(1)">&laquo;back</a>');


$(document).ready(function() {
    if (history.length > 0) {
        var backLinkContainer = $('.content-header .preview-link:last');
        if (backLinkContainer.length < 1) {

            backLinkContainer = $('.content-header:first');
            backLink.css("padding", "7px");

        }
        backLinkContainer.append(backLink);
    }
/* This part integrated with the following section 
    if ($('.userlogintext').is(":visible") == true) {
        $('.login-link-container').height(0);
    } else {

    $('.login-link-container').height(20);

    }
*/

/* This part added for removing the side effect of fixing the alignemnt login-elements */
    if ($('.login-link-container').children().children()[0] == null) {
        $('.login-elements-hide').addClass('login-elements-show');
        $('.login-elements-show').removeClass('login-elements-hide');
        $('.login-link-container').height(0);
    }
    else {
        $('.login-elements-show').addClass('login-elements-hide');
        $('.login-elements-show').removeClass('login-elements-show');
        $('.login-link-container').height(20);
    }
/* -------- ------------- -------------- */

    AdjustSideBarHeight();

    $('.userlogintext').each(function() {

        this.value = $(this).attr('title');
        $(this).addClass('watermarked');

        $(this).focus(function() {

            if ($(this).hasClass('fakepassword')) {

                $(this).hide()

                $('.password').show().focus();
            } else {

                if (this.value == $(this).attr('title')) {
                    this.value = '';
                    $(this).removeClass('watermarked');
                }
            }
        });

        $(this).blur(function() {

            if ($(this).hasClass('password')) {
                if (this.value == '') {
                    $(this).hide()

                    $('.fakepassword').show()
                }
            } else {
                if (this.value == '') {
                    this.value = $(this).attr('title');
                    $(this).addClass('watermarked');
                }
            }
        });
    });

});


function AdjustSideBarHeight() {
    if ($(".content-sidebar").height() < $(".content-body").height()) {
        $(".content-sidebar").height($(".content-body").height());
    } 

}