$(document).ready(function()
{
    $('.menu-ul li a[href="home.aspx"]').addClass('active');


    $(".grdNews a").addClass("pinklink");

    $(".footer-container a").removeClass('pinklink').css('paddingLeft', '15px');

    $('.spotlight-morelink').each(function()
    {

        $(this).click(function()
        {
            ToggleMoreSpotlightLink($(this));


        });

    });




    $('.morenewslink').each(function()
    {

        $(this).click(function()
        {
            ToggleMoreNewsLink($(this));

        });

    });

    $('.lessnewslink ').each(function() {

        $(this).click(function() {
            LessMoreNewsLink($(this));

        });

    });


    $('.grdNews tr:first a.morenewslink').click();

    //$('#homenewsepdiv_4').css("display", "none");
    $('.grdNews tr:last').find('.seperator').css("display", "none");


});

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


function ToggleMoreSpotlightLink(item) {
    $(".content-sidebar").css("height", "auto");
    $('.spotlight-description', item.parents('.sidebar-content-body')).hide();

    $('.spotlight-full-description', item.parents('.sidebar-content-body')).slideDown('slow', function() {

        if ($(".content-sidebar").height() < $(".content-body").height()) {

            $(".content-body").height($(".content-sidebar").height());

        }

    });

 }


function  ToggleMoreNewsLink(item) {

    $('.full-news-description').each(function() {
        $(".news-description", $(this).parent()).show();
       
        $(this).hide();

    });
    $(item).parents('.news-description').hide();
    AdjustSideBarHeight();
    $('.full-news-description', $(item).parents('.gridtext').parent()).slideDown('slow', function() {

    AdjustSideBarHeight();
    });
   
}