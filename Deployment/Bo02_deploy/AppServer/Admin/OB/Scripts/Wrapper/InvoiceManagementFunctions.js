function InvoiceManagementFunctions() {
	String.prototype.contains = function(it) {
		return this.indexOf(it) != -1;
	};
	this.PrintEnglishInvoice = function(invoiceItemsData) {
	    if (invoiceItemsData.length > 0) {
	        var answer = confirm("Are you sure you want to print these invoices?")
	        if (answer) {
	            var invoiceIDs = '';
	            for (i = 0; i < invoiceItemsData.length; i++)
	                invoiceIDs += invoiceItemsData[i][0] + ",";
	            invoiceIDs = invoiceIDs.substring(0, invoiceIDs.length - 1);
	            window.open('../InvoiceEnglish.aspx?Factuurnr=' + invoiceIDs + '&lang=en-US');
	        }
	    }
	    else {
	        alert('No invoice record is selected!');
	    }
	}
	this.PrintDutchInvoice = function(invoiceItemsData) {
	    if (invoiceItemsData.length > 0) {
	        var answer = confirm("Are you sure you want to print these invoices?")
	        if (answer) {
	            var invoiceIDs = '';
	            for (i = 0; i < invoiceItemsData.length; i++)
	                invoiceIDs += invoiceItemsData[i][0] + ",";
	            invoiceIDs = invoiceIDs.substring(0, invoiceIDs.length - 1);
	            window.open('../InvoiceEnglish.aspx?Factuurnr=' + invoiceIDs + '&lang=nl-NL');
	        }
	    }
	    else {
	        alert('No invoice record is selected!');
	    }
	}

	this.Verstuurd = function(invoiceItemsData) {
		Ext.Ajax.request({
			url : '../../WebServices/InvoiceManagementWebService.asmx/UpdateStatusAsSentAndGetInvalidIDs',
			jsonData : {
				invoiceIds : invoiceItemsData
			},
			method : 'POST',
			headers : {
				'Content-Type' : 'application/json; charset=utf-8'
			},
			success : function(response, request) {
				var d = Ext.decode(response.responseText);
				if (d.d != '') {
					alert('Status of Invoice#:' + d.d + ' should be Nieuw');
				} else {
					alert('Operation is successful');
				}
				OBSettings.RefreshPage()

			},
			failure : function(response, opts) {
				alert('Something went wrong');
			},
			scope : this
		});
	}
}
