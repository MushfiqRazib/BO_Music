
var lessButton = $('<a href="javascript:void(0)" class="morenewslink pinklink">more</a>');


$(document).ready(function() {
$('.menu-ul li a[href="News.aspx"]').addClass('active');
    $(".grdNews a").addClass("pinklink");

    $(".footer-container a").removeClass('pinklink').css('paddingLeft', '15px');


    $('.morenewslink').each(function() {

        $(this).click(function() {
            ToggleMoreNewsLink($(this));

        });

    });

    $('.lessnewslink ').each(function() {

        $(this).click(function() {
            LessMoreNewsLink($(this));

        });

    });



    //$('.grdNews tr:first a.morenewslink').click();

    $('.pager_link').each(function() {


        $(this).click(function() {

            var pageIndex = this.rel;
            var elem = UtilityService.getUrlVars();

            if (typeof elem['page'] == 'undefined') {

                elem.push('page');
            }
            elem['page'] = pageIndex;

            var urlString = UtilityService.getUrlString(elem);
            window.location.href = window.location.pathname + "?" + urlString;


        });

    });

    $('.grdNews tr:last').find('.seperator').css("display", "none");


});

function ToggleMoreNewsLink(item) {

    $(item).parents('.news-description').hide();
    $('.full-news-description', $(item).parents('.gridtext').parent()).slideDown('fast', function() {

        AdjustSideBarHeight();
    });

}

function LessMoreNewsLink(item) {
    $('.full-news-description').each(function() {
        // $(".news-description", $(this).parent()).show();

        //$(this).hide();

     });
    
     $('.full-news-description', $(item).parents('.gridtext').parent()).hide();
    $('.news-description', $(item).parents('.gridtext').parent()).slideDown('fast', function() {

        AdjustSideBarHeight();
    });

}
