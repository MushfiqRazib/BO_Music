CREATE TABLE dfn_repdetail (
    report_code character varying(20) NOT NULL,
    report_name character varying(40) NOT NULL,
    report_order smallint DEFAULT 1 NOT NULL,
    field_caps text,
    sql_from character varying(30) NOT NULL,
    sql_where text,
    sql_groupby text,
    sql_orderby character varying(50),
    gis_theme_layer boolean,
    sql_orderdir character varying(4),
    sql_keyfields text,
    detail_fieldsets text NOT NULL,
    connection_string text,
    multiselect boolean,
    report_settings character varying
);

INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from, sql_where, sql_groupby, sql_orderby, gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) VALUES ('1', 'OrderManagement', 1, 'orderid=Order No;orderdate=Order Date;daddress=Delivery Address;customer=Customer;status=Status;description=Description', 'view_order_management', NULL, NULL, 'orderid', NULL, 'DESC', 'orderid', 'orderid', NULL, true, NULL);
INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from, sql_where, sql_groupby, sql_orderby, gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) VALUES ('2', 'InvoiceManagement', 2, 'invoiceid=Invoice No;invoicedate=Invoice Date;address=Invoice Address;customer=Customer;status=Status;credit=Credit ', 'view_invoice_management', NULL, NULL, 'invoiceid', NULL, 'DESC', 'invoiceid', 'invoiceid', NULL, true, NULL);
INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from, sql_where, sql_groupby, sql_orderby, gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) VALUES ('3', 'StockManagement', 3, 'orderid=Order No;orderdate=Order Date;supplier=Supplier;ddate=Delivery Date;rstatus=Receive Status,pstatus=Payment Status', 'view_stock_management', NULL, NULL, 'supplyorderid', NULL, 'DESC', 'supplyorderid', 'supplyorderid', NULL, true, NULL);
INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from		, sql_where, sql_groupby, sql_orderby  , gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) VALUES 
						  ('4', 		'Article'  , 4			 , ''		 , 'view_article', NULL	   , NULL		, 'articlecode', NULL			, 'ASC'		  , 'articlecode', 'articlecode'   , NULL			  , true	   , NULL);

						  
INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from		, sql_where, sql_groupby, sql_orderby  , gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) VALUES 
						  ('5', 		'Category'  , 5			 , ''		 , 'category', NULL	   , NULL		, 'categoryid', NULL			, 'ASC'		  , 'categoryid', 'categoryid'   , NULL			  , true	   , NULL);
INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from		, sql_where, sql_groupby, sql_orderby  , gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) VALUES 
						  ('6', 		'Composer'  , 6			 , ''		 , 'composer', NULL	   , NULL		, 'composerid', NULL			, 'ASC'		  , 'composerid', 'composerid'   , NULL			  , true	   , NULL);
INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from		, sql_where, sql_groupby, sql_orderby  , gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) VALUES 
						  ('7', 		'Customer'  , 7			 , ''		 , 'customer', NULL	   , NULL		, 'customerid', NULL			, 'ASC'		  , 'customerid', 'customerid'   , NULL			  , true	   , NULL);
INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from		, sql_where, sql_groupby, sql_orderby  , gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) VALUES 
						  ('8', 		'Editor'  , 8			 , ''		 , 'editor', NULL	   , NULL		, 'editorid', NULL			, 'ASC'		  , 'editorid', 'editorid'   , NULL			  , true	   , NULL);
INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from		, sql_where, sql_groupby, sql_orderby  , gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) VALUES 
						  ('9', 		'Grade'  , 9			 , ''		 , 'grade', NULL	   , NULL		, 'gradeid', NULL			, 'ASC'		  , 'gradeid', 'gradeid'   , NULL			  , true	   , NULL);
INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from		, sql_where, sql_groupby, sql_orderby  , gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) VALUES 
						  ('10', 		'Period'  , 10			 , ''		 , 'period', NULL	   , NULL		, 'periodid', NULL			, 'ASC'		  , 'periodid', 'periodid'   , NULL			  , true	   , NULL);
INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from		, sql_where, sql_groupby, sql_orderby  , gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) VALUES 
						  ('11', 		'Playlist'  , 11			 , ''		 , 'playlist', NULL	   , NULL		, 'articlecode', NULL			, 'ASC'		  , 'articlecode', 'articlecode'   , NULL			  , true	   , NULL);
INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from		, sql_where, sql_groupby, sql_orderby  , gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) VALUES 
						  ('12', 		'News'  , 12	 		 , ''		 , 'news', NULL	   , NULL		, 'newsid', NULL			, 'ASC'		  , 'newsid', 'newsid'   , NULL			  , true	   , NULL);
INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from		, sql_where, sql_groupby, sql_orderby  , gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) VALUES 
						  ('13', 		'Publisher'  , 13			 , ''		 , 'publisher', NULL	   , NULL		, 'publisherid', NULL			, 'ASC'		  , 'publisherid', 'publisherid'   , NULL			  , true	   , NULL);
INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from		, sql_where, sql_groupby, sql_orderby  , gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) VALUES 
						  ('14', 		'Serie'  , 14			 , ''		 , 'serie', NULL	   , NULL		, 'serieid', NULL			, 'ASC'		  , 'serieid', 'serieid'   , NULL			  , true	   , NULL);
INSERT INTO dfn_repdetail (report_code, report_name, report_order, field_caps, sql_from		, sql_where, sql_groupby, sql_orderby  , gis_theme_layer, sql_orderdir, sql_keyfields, detail_fieldsets, connection_string, multiselect, report_settings) VALUES 
						  ('15', 		'Spotlight'  , 15			 , ''		 , 'spotlight', NULL	   , NULL		, 'spotlightid', NULL			, 'ASC'		  , 'spotlightid', 'spotlightid'   , NULL			  , true	   , NULL);
UPDATE dfn_repdetail SET sql_orderdir='DESC' WHERE report_code= '7';