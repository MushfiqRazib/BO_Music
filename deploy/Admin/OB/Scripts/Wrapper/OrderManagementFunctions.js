function OrderManagementFunctions() {
    this.Remove = function(orderItemsData) {
        Ext.Ajax.request({
            url: '../../WebServices/OrderManagementWebService.asmx/DeleteOrdersAndGetInvalidIds',
            jsonData: {
                orderIds: orderItemsData
            },
            method: 'POST',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            success: function(response, request) {
                var d = Ext.decode(response.responseText);
                if (d.d != '') {
                    alert('Order#:' + d.d + ' are not in Assigned state');
                } else {
                    alert('Orders deletion is successful');
                }
                OBSettings.RefreshPage();

            },
            failure: function(response, opts) {
                alert('Something went wrong');
            },
            scope: this
        });
    }

    this.Ready = function(orderItemsData) {
        Ext.Ajax.request({
            url: '../../WebServices/OrderManagementWebService.asmx/MakeOrderReadyAndGetInvalidIds',
            jsonData: {
                orderIds: orderItemsData
            },
            method: 'POST',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            success: function(response, request) {
                var d = Ext.decode(response.responseText);
                if (d.d != '') {
                    alert('Status of the Order#' + d.d + ' should be Assigned.');
                } else {
                    alert('Operation is successful');
                    //OBSettings.RefreshPage()
                }
                OBSettings.RefreshPage();

            },
            failure: function(response, opts) {
                alert('Something went wrong');
            },
            scope: this
        });
    }

    this.Fucturen = function(orderItemsData) {
        Ext.Ajax.request({
            url: '../../WebServices/OrderManagementWebService.asmx/MakeInvoiceAndGetInvalidIds',
            jsonData: {
                orderIds: orderItemsData
            },
            method: 'POST',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            success: function(response, request) {
                var d = Ext.decode(response.responseText);
                if (d.d != '') {
                    alert('Status of the Order#' + d.d + ' should be in Ready state.');
                } else {
                    alert('Operation is successful');
                    //OBSettings.RefreshPage()
                }
                OBSettings.RefreshPage();

            },
            failure: function(response, opts) {
                alert('Something went wrong');
            },
            scope: this
        });
    }

}



