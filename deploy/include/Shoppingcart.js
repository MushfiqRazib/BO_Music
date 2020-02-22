

$(document).ready(function() {

    if ($('.order-grid').height() < 230) {
        $('.order-grid').height(230);

    }

    AdjustSideBarHeight();


    $('.order-grid .delete-link').click(function() {

        return confirm("Are you sure you want to delete this product?");


    });

    $('.order-grid .order-quantity-tag').keypress(function(event) {
        if (event.charCode && (event.charCode < 48 || event.charCode > 57)) {
            event.preventDefault();
        }
    });





});
