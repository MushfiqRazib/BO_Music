CREATE OR REPLACE VIEW view_stock_management AS 
 SELECT to_char(s.supplyorderdate::timestamp with time zone, 'dd-mm-yyyy'::text) AS sdate, s.supplyorderid, (((COALESCE(p.firstname, ''::character varying)::text || ' '::text) || COALESCE(p.middlename, ''::character varying)::text) || ' '::text) || COALESCE(p.lastname, ''::character varying)::text AS supplier, to_char(s.deliverydate::timestamp with time zone, 'dd-mm-yyyy'::text) AS ddate, 
        CASE
            WHEN s.receivingstatus::text = 'N'::text THEN 'Not Received'::text
            WHEN s.receivingstatus::text = 'P'::text THEN 'Partially Received'::text
            WHEN s.receivingstatus::text = 'F'::text THEN 'Fully Received'::text
            ELSE NULL::text
        END AS rstatus, 
        CASE
            WHEN s.paymentstatus::text = 'U'::text THEN 'Unpaid'::text
            WHEN s.paymentstatus::text = 'P'::text THEN 'Partial Paid'::text
            WHEN s.paymentstatus::text = 'F'::text THEN 'Full Paid'::text
            ELSE NULL::text
        END AS pstatus
   FROM supplyorders s
   LEFT JOIN publisher p ON s.supplierid = p.publisherid::numeric;
