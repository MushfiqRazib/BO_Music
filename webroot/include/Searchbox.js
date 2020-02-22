$(document).ready(function() {



    $('.searchkeywords-text').each(function() {

        //alert($(this).attr('title'));
        // this.value = $(this).attr('title');
        $(this).addClass('watermarked');
        $(this).keydown(function(e) {
            if (e.keyCode == 13) {


                if (this.value != $(this).attr('title') && this.value != '' && $('.as-results').is(':visible') == false) {
                    window.location = "SearchResult.aspx?searchtext=" + this.value;
                }

                // $('input.searchkeywords-button').trigger('click');
                // alert("Hello" + this.value);
                e.stopPropagation();
                return false;

            }

        });





        $(this).focus(function() {



            if (this.value == $(this).attr('title')) {
                this.value = '';
                $(this).removeClass('watermarked');
            }

        });

        $(this).blur(function() {


            if (this.value == '') {
                this.value = $(this).attr('title');
                $(this).addClass('watermarked');
            }

        });


    });



    InitSearchBox();


});

function InitSearchBox() {
    var selectedText = "";
    var elem = UtilityService.getUrlVars();
    if (typeof elem['searchtext'] != 'undefined') {

        selectedText = unescape(elem['searchtext']);
       
    }

    $(".searchkeywords-text").autoSuggest('webservices/BoeijengaMusic.asmx/GetSearchKeywords', { selectedItemProp: "KeyName", searchObjProps: "KeyName" ,selectedText: selectedText});

    

}