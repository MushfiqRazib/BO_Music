Ext.namespace('hit.OB');
hit.OB.ActionControl = Ext.extend(Ext.Panel, {
    constructor: function(config) {
        Ext.apply(config, {
            id: 'actioncontrol',
            renderTo: 'divAction',
            width: 400,
            layout: 'column',
            bodyStyle: {
                background: 'transparent',
                border: '0px'
            },
            items: [{
                xtype: 'box',
                anchor: '',
                autoEl: {
                    tag: 'div',
                    cls: '',
                    html: 'Met geselecteerde records : &nbsp;'
                }
            }, {
                xtype: 'combo',
                id: 'actionCombo',
                boxMaxHeight: 15,
                store: new Ext.data.ArrayStore({
                    fields: ['action', 'order', 'custom', 'parameter'],
                    data: config.data,
                    listeners: {
                        load: function() {
                            this.data = this.queryBy(function(record,
													id) {
                                if (record.get('custom') == 'true') {
                                    return true;
                                } else {
                                    return false;
                                }
                            });
                        }
                    }
                }),
                displayField: 'action',
                valueField: 'parameter',
                typeAhead: true,
                mode: 'local',
                forceSelection: true,
                triggerAction: 'all',
                listeners: {
                'render': function(c) {
                this.el.setStyle('height', '15px'); //.setStyle('font-size', '11px');
                this.el.addClass('font');
                    }
                }
            }, {
                xtype: 'button',
                text: ' OK ',
                handler: function() {
                    this.ownerCt.setActionParameter(Ext
									.getCmp('actionCombo').lastSelectionText);
                }

}]
            });
            hit.OB.ActionControl.superclass.constructor.call(this, config);
        },
        afterRender: function() {
            hit.OB.ActionControl.superclass.afterRender.call(this);
            if (Ext.getCmp('actionCombo').store.data.length == 0) {
                this.destroy();
            }
            /*
            * this.store.filterBy(function(record,id){ return record.get('age') >=
            * 30; //older than 30 years-old });
            */
        },
        setActionParameter: function(actionName) {
            switch (actionName) {
                case 'READY': //Custom function for Ordermanagement
                    new OrderManagementFunctions().Ready(OBSettings.GetMultiSelectValues('READY'));
                    break;
                case 'VERWIJDEREN':     //Custom function for Ordermanagement
                    new OrderManagementFunctions().Remove(OBSettings.GetMultiSelectValues('VERWIJDEREN'));
                    break;
                case 'FUCTUREN':        //Custom function for Ordermanagement
                    new OrderManagementFunctions().Fucturen(OBSettings.GetMultiSelectValues('FUCTUREN'));
                    break;
                case 'AFDRUKKEN FACTUUR':       //Print Dutch Invoice
                    new InvoiceManagementFunctions().PrintDutchInvoice(OBSettings.GetMultiSelectValues('Afdrukken factuur'));
                    break;
                case 'AFDRUKKEN INVOICE':
                    new InvoiceManagementFunctions().PrintEnglishInvoice(OBSettings.GetMultiSelectValues('Afdrukken invoice'));
                    break;
                case 'VERSTUURD':       //update invoice status as sent
                    new InvoiceManagementFunctions().Verstuurd(OBSettings.GetMultiSelectValues('VERSTUURD'));
                    break;
                case 'FULLPAID':        //update supply order status
                    new StockManagementFunctions().UpdatePaymentStatus(OBSettings.GetMultiSelectValues('FULLPAID'), 0);
                    break;
                case 'PERTIALPAID':     //update supply order status
                    new StockManagementFunctions().UpdatePaymentStatus(OBSettings.GetMultiSelectValues('PERTIALPAID'), 1);
                    break;
                case 'UNPAID':          //update supply order status
                    new StockManagementFunctions().UpdatePaymentStatus(OBSettings.GetMultiSelectValues('UNPAID'), 2);
                    break;

                default:
                    break;
            }
        },
        executeAction: function(param, url) {
            alert(url);
            Ext.Ajax.request({
                url: url, // 'WebService/Gobelinmusic.asmx/AddMailingList',
                method: 'POST',
                jsonData: {
                    param: param
                },
                timeout: 120000,
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                success: function(response, request) {
                    var res = Ext.decode(response.responseText);
                    /*
                    * if (res.d == 'mailinglistadded') {
                    * 
                    * Ext.MessageBox.Show(MyLang.CATALOG + " / " +
                    * MyLang.MAILINGLIST, MyLang.MAILINGLIST_ADDED); } else
                    * if (res.d == 'emailexists') {
                    * 
                    * Ext.MessageBox.Show(MyLang.CATALOG + " / " +
                    * MyLang.MAILINGLIST, MyLang.MAILINGLIST_EXISTS); }
                    * else { Ext.MessageBox.Show(MyLang.CATALOG + " / " +
                    * MyLang.MAILINGLIST, MyLang.MAILINGLIST_FAILURE); }
                    */
                },
                failure: function(response, opts) {
                    /*
                    * Ext.MessageBox.Show(MyLang.CATALOG + " / " +
                    * MyLang.MAILINGLIST,
                    * MyLang.COMMUNICATION_FAILURE_MSG);
                    */
                },
                scope: this
            });
        }
    });
