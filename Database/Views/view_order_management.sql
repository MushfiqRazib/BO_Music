CREATE OR REPLACE VIEW view_order_management AS 
 SELECT to_char(o.orderdate::timestamp with time zone, 'dd-MM-yyyy'::text) AS orderdate, o.orderid, (((COALESCE(c.firstname, ''::character varying)::text || ' '::text) || COALESCE(c.middlename, ''::character varying)::text) || ' '::text) || COALESCE(c.lastname, ''::character varying)::text AS customer, (((COALESCE(c.dhousenr, ''::character varying)::text || 
        CASE
            WHEN length(c.dhousenr::text) > 0 THEN ', '::text
            ELSE ''::text
        END) || COALESCE(c.daddress, ''::character varying)::text) || 
        CASE
            WHEN length(c.daddress::text) > 0 THEN ', '::text
            ELSE ''::text
        END) || COALESCE(c.dresidence, ''::character varying)::text AS daddress, 
        CASE
            WHEN o.orderstatus::text = '1'::text THEN 'Assigned'::text
            WHEN o.orderstatus::text = '2'::text THEN 'Ready'::text
            WHEN o.orderstatus::text = '3'::text THEN 'Invoiced'::text
            ELSE NULL::text
        END AS status
   FROM orders o, customer c
  WHERE o.customer = c.customerid::numeric;
