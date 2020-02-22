function OrderManagementFunctions() {
    this.Remove = function(orderItemsData) {
        var answer = confirm("Are you sure you want to delete?")
        if (answer) {
            Ext.Ajax.request({
                url: '../../WebService/OrderManagementWebService.asmx/Remove',
                jsonData: {
                    orderItems: orderItemsData
                },
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                success: function(response, request) {
                    var d = Ext.decode(response.responseText);
                    if (d.d != '') {
                        alert(d.d);
                    } else {
                        alert('Operation is successful');
                        OBSettings.RefreshPage()
                    }

                },
                failure: function(response, opts) {
                    alert('Something went wrong');
                },
                scope: this 
            });
        }
    }

    this.Check = function(orderItemsData) {
        Ext.Ajax.request({
        url: '../../WebService/OrderManagementWebService.asmx/Check',
            jsonData: {
                orderItems: orderItemsData
            },
            method: 'POST',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            success: function(response, request) {
                var d = Ext.decode(response.responseText);
                if (d.d != '') {
                    window.open('../../orders/ErrCheckPdf.aspx');
                } else {
                    alert('Operation is successful');
                    OBSettings.RefreshPage()
                }

            },
            failure: function(response, opts) {
                alert('Something went wrong...');
            },
            scope: this,
            timeout: 10000000
        });
    }

    this.Produce = function(orderItemsData) {
        Ext.Ajax.request({
            url: '../../WebService/OrderManagementWebService.asmx/Produce',
            jsonData: {
                orderItems: orderItemsData
            },
            method: 'POST',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            success: function(response, request) {
                var d = Ext.decode(response.responseText);
                if (d.d != '') {
                    alert(d.d);
                } else {
                    alert('Operation is successful');
                    OBSettings.RefreshPage()
                }

            },
            failure: function(response, opts) {
                alert('Something went wrong');
            },
            scope: this
        });
    }

    this.Billing = function(orderItemsData) {
        Ext.Ajax.request({
            url: '../../WebService/OrderManagementWebService.asmx/Billing',
            jsonData: {
                orderItems: orderItemsData
            },
            method: 'POST',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            success: function(response, request) {
                var d = Ext.decode(response.responseText);
                if (d.d != '') {
                    alert(d.d);
                } else {
                    alert('Operation is successful');
                    OBSettings.RefreshPage()
                }

            },
            failure: function(response, opts) {
                alert('Something went wrong');
            },
            scope: this
        });
    }

}

function InvoiceManagementFunctions() {
    String.prototype.contains = function(it) { return this.indexOf(it) != -1; };
    this.PrintInvoice = function(orderItemsData) {
    var answer = confirm("Are you sure you want to print these invoices?")
        if (answer) {
            Ext.Ajax.request({
                url: '../../WebService/InvoiceManagementWebService.asmx/PrintInvoice',
                jsonData: {
                    orderItems: orderItemsData
                },
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                success: function(response, request) {
                    var d = Ext.decode(response.responseText);
                    if (d.d != '') {
                        if (d.d.contains('../')) {
                            window.open(d.d);
                        }
                        else {
                            alert(d.d);
                        }
                    } else {
                        alert('Something went wrong');
                    }

                },
                failure: function(response, opts) {
                    alert('Something went wrong');
                },
                scope: this
            });
        }
    }

    this.Download = function(orderItemsData) {

        if (orderItemsData.length == 0) {
            alert('You have to select one record.');
        }
        else if (orderItemsData.length == 1) {
            Ext.Ajax.request({
                url: '../../WebService/InvoiceManagementWebService.asmx/Download',
                jsonData: {
                    orderItems: orderItemsData
                },
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                success: function(response, request) {
                    var d = Ext.decode(response.responseText);
                    if (d.d != '') {
                        if (d.d.contains('../')) {
                            window.open(d.d);
                        }
                        else {
                            alert(d.d);
                        }
                    } else {
                        alert('Something went wrong');
                    }
                },
                failure: function(response, opts) {
                    alert('Something went wrong');
                },
                scope: this
            });
        }
        else {
            alert('You have selected more than one record.');
        }
    }

    this.Verstuurd = function(invoiceItemsData) {
        Ext.Ajax.request({
            url: '../../WebService/InvoiceManagementWebService.asmx/UpdateStatusAsSentAndGetInvalidIDs',
            jsonData: {
                invoiceIds: invoiceItemsData
            },
            method: 'POST',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            success: function(response, request) {
                var d = Ext.decode(response.responseText);
                if (d.d != '') {
                    alert(d.d);
                } else {
                    alert('Operation is successful');
                    OBSettings.RefreshPage()
                }

            },
            failure: function(response, opts) {
                alert('Something went wrong');
            },
            scope: this
        });
}





