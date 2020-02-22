
var tellAFriendPanel = new Tell_A_Friend_Widget({ handler: function(e) {

    $.blockUI();


    var data = { msg: tellAFriendPanel.message.val(), senderEmail: tellAFriendPanel.email.val(), receiverEmail: tellAFriendPanel.receiverEmail.val() };
    $.ajax({
        type: "POST",
        dataType: 'json',
        async: false,
        contentType: "application/json; charset=utf-8",
        url: "webservices/BoeijengaMusic.asmx/TellFriend",
        data: '{msg: "' + tellAFriendPanel.message.val() + '", senderEmail : "' + tellAFriendPanel.email.val() + '", receiverEmail : "' + tellAFriendPanel.receiverEmail.val() + '" }',
        success: function(result) {
            $.unblockUI();
            showStatus(result.d, 4000, true, false);
        }
    });



    return false;

}
});


$(document).ready(function() {


    $('.searchresult-content-column').each(function() {

        if ($.trim($(this).text()) == "") {

            $(this).prev().hide().end().hide();


        }
    }
                    );




    var pager = $('.grdSearchResult .grid-pager-container').css("margin-top", "-3px");

    if (pager.length < 1) {

        $('.grdSearchResult .searchresult-grid-row-wrapper:gt(0)').css("margin-top", "-3px");

    }


    $('.search-result-row  .grid-pager-container').width($('.search-result-row:eq(0)').width()).css('margin-left', '-1px');

    AdjustSideBarHeight();


    $('.search-detail').each(function() {

        $(this).click(function(e) {


            ToggleDetailLink($(this));
            // e.stopPropagation();
            return false;
        });
    });


    $('.tell-a-friend-btn').each(function() {

        $(this).click(function(e) {
            tellAFriendPanel.renderTo($(this).parents('.searchresult-right-row-container'))
            AdjustSearchResultGridColumnsHeight();
            AdjustSideBarHeight();


            // e.stopPropagation();
            return false;
        });
    });




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

    })




    $('.sort-link').each(function() {


        $(this).click(function() {

            var sortString = this.rel;

            var elem = UtilityService.getUrlVars();

            if (typeof elem['sorting'] == 'undefined') {

                elem.push('sorting');
            }
            elem['sorting'] = sortString;

            var sortDirection = "asc";


            if (typeof elem['sortdirection'] == 'undefined') {

                elem.push('sortdirection');
                elem['sortdirection'] = "asc";
            } else {

                if (elem['sortdirection'] == "asc") {

                    elem['sortdirection'] = "desc";

                } else {

                    elem['sortdirection'] = "asc";

                }

            }
            var urlString = UtilityService.getUrlString(elem);
            window.location.href = window.location.pathname + "?" + urlString;


        });

    });


    $('.category-link').each(function() {


        $(this).click(function() {

            var sortString = this.rel;

            var elem = UtilityService.getUrlVars();

            if (typeof elem['type'] == 'undefined') {

                elem.push('type');
            }
            elem['type'] = sortString;
            elem['page'] = 1;
            var urlString = UtilityService.getUrlString(elem);
            window.location.href = window.location.pathname + "?" + urlString;


        });

    });



    $('.sub-category-link').each(function() {


        $(this).click(function() {

            var subCatString = this.rel;

            var elem = UtilityService.getUrlVars();

            if (typeof elem['subcat'] == 'undefined') {

                elem.push('subcat');
            }
            elem['subcat'] = subCatString;
            elem['page'] = 1;
            var urlString = UtilityService.getUrlString(elem);
            window.location.href = window.location.pathname + "?" + urlString;


        });







    });




    var elem = UtilityService.getUrlVars();

    if (typeof elem['type'] != 'undefined') {

       
        switch (elem['type']) {

            case 's':
                $('.menu-ul li a[href="searchresult.aspx?search=false&type=s&shop=true"]').addClass('active');
                break;
            case 'b':
                $('.menu-ul li a[href="searchresult.aspx?search=false&type=b&shop=true"]').addClass('active');
                break;
            case 'c':
                $('.menu-ul li a[href="searchresult.aspx?search=false&type=c,d&shop=true"]').addClass('active');
                break;
            case 'd':
                $('.menu-ul li a[href="searchresult.aspx?search=false&type=c,d&shop=true"]').addClass('active');
                break;
            case 'c,d':
                $('.menu-ul li a[href="searchresult.aspx?search=false&type=c,d&shop=true"]').addClass('active');
                break;
            default:
                $('.menu-ul li a[href="searchresult.aspx?search=false&shop=false"]').addClass('active');
                break;

        }
    }
    else {

        $('.menu-ul li a[href="searchresult.aspx?search=false&shop=false"]').addClass('active');
    }
});



function ToggleDetailLink(item) {

    $('.search-result-description').hide();

    var descriptionPanel = $('.search-result-description', item.parents('.searchresult-right-row-container'));
    var midPanel = $('.middiv', item.parents('.searchresult-right-row-container'));
    var colorCode = descriptionPanel.css('color');
    descriptionPanel.css('color', 'white');

    $('.middiv').each(function() {

        $(this).css('height', '130px');
    });

    if (descriptionPanel.height() > 0)
        midPanel.css('height', '10px');


    descriptionPanel.slideDown('slow', function() {
        AdjustSearchResultGridColumnsHeight();
        AdjustSideBarHeight();
        descriptionPanel.css('color', colorCode);

    });

    $('.search-detail').removeClass('search-detail-down');

    item.addClass('search-detail-down');


}


function AdjustSearchResultGridColumnsHeight() {


    $('.searchresult-grid-row-wrapper').each(function() {

        $('.searchresult-left-row-container', this).height($('.searchresult-right-row-container', this).height());

    });

} 