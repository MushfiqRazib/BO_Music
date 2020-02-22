function StockManagementFunctions() {
    String.prototype.contains = function(it) { return this.indexOf(it) != -1; };
    this.UpdatePaymentStatus = function(orderItemsData, statusCode) {
    var answer = confirm("Are you sure you want to update payment status for these invoices?")
        if (answer) {
            Ext.Ajax.request({
            url: '../../WebServices/StockManagementWebService.asmx/UpdatePaymentStatus',
                jsonData: {
                invoiceIds: orderItemsData,
                status: statusCode
                },
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                success: function(response, request) {
                    //var d = Ext.decode(response.responseText);
                	OBSettings.RefreshPage();

                },
                failure: function(response, opts) {
                    alert('Something went wrong');
                },
                scope: this
            });
        }
    }
}





