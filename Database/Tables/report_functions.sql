CREATE TABLE report_functions
(
  report_code character varying(20) NOT NULL,
  function_name character varying(20) NOT NULL,
  order_position integer,
  parameters text NOT NULL,
  iscustom boolean,
  CONSTRAINT reportname_functions_param PRIMARY KEY (report_code, function_name, parameters)
)
WITH (
  OIDS=FALSE
);

INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('1', 'Detail', 0, 'orderid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('1', 'Edit', 1, 'orderid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('1', 'Print', 2, 'orderid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('1', 'Ready', 0, 'orderid', true);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('1', 'Fucturen', 1, 'orderid', true);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('1', 'Verwijderen', 2, 'orderid', true);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('2', 'Detail', 0, 'invoiceid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('2', 'Edit', 1, 'invoiceid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('2', 'Print', 2, 'invoiceid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('2', 'Afdrukken Factuur', 0, 'invoiceid', true);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('2', 'Afdrukken Invoice', 1, 'invoiceid', true);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('2', 'Booken', 2, 'invoiceid', true);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('2', 'Verstuurd', 3, 'invoiceid', true);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('3', 'Detail', 0, 'supplyorderid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('3', 'Edit', 1, 'supplyorderid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('3', 'Print', 2, 'supplyorderid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('3', 'Receive', 3, 'supplyorderid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('3', 'Fullpaid', 0, 'supplyorderid', true);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('3', 'Pertialpaid', 1, 'supplyorderid', true);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('3', 'Unpaid', 2, 'supplyorderid', true);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('4', 'Detail', 0, 'articlecode', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('4', 'Edit', 1, 'articlecode', false);

INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('5', 'Detail', 0, 'categoryid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('5', 'Edit', 1, 'categoryid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('6', 'Detail', 0, 'composerid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('6', 'Edit', 1, 'composerid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('7', 'Detail', 0, 'customerid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('7', 'Edit', 1, 'customerid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('8', 'Detail', 0, 'editorid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('8', 'Edit', 1, 'editorid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('9', 'Detail', 0, 'gradeid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('9', 'Edit', 1, 'gradeid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('10', 'Detail', 0, 'periodid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('10', 'Edit', 1, 'periodid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('11', 'Detail', 0, 'articlecode', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('11', 'Edit', 1, 'articlecode', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('12', 'Detail', 0, 'newsid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('12', 'Edit', 1, 'newsid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('13', 'Detail', 0, 'publisherid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('13', 'Edit', 1, 'publisherid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('14', 'Detail', 0, 'serieid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('14', 'Edit', 1, 'serieid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('15', 'Detail', 0, 'spotlightid', false);
INSERT INTO report_functions (report_code, function_name, order_position, parameters, iscustom) VALUES ('15', 'Edit', 1, 'spotlightid', false);
