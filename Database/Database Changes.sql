delete from report_functions;
INSERT INTO report_functions(
            report_code, function_name, order_position, parameters, iscustom)
    VALUES (1, 'Detail', 0, 'orderid', false);
INSERT INTO report_functions(
            report_code, function_name, order_position, parameters, iscustom)
    VALUES (1, 'Edit', 1, 'orderid', false);    

INSERT INTO report_functions(
            report_code, function_name, order_position, parameters, iscustom)
    VALUES (1, 'Ready', 2, 'orderid', true);    

	
delete from dfn_repdetail;
INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from, sql_where, sql_groupby, sql_orderby, gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) 
VALUES ('1', 'OrderManagement', 1,'orderid=Order No;orderdate=Order Date;daddress=Delivery Address;customer=Customer;status=Status ', 'view_order_management', NULL, NULL, 'orderid', NULL, 'DESC', 'orderid', 'orderid', NULL, true, NULL);	