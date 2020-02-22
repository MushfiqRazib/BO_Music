CREATE OR REPLACE VIEW view_invoice_management AS 
 SELECT to_char(i.invoicedate::timestamp with time zone, 'dd-MM-yyyy'::text) AS invoicedate, i.invoiceid, (COALESCE(c.firstname::text || ' '::text, ''::text) || COALESCE(c.middlename::text || ' '::text, ''::text)) || COALESCE(c.lastname, ''::character varying)::text AS customer, (COALESCE(i.housenr::text || ', '::text, ''::text) || COALESCE(i.address::text || ', '::text, ''::text)) || COALESCE(i.residence, ' '::character varying)::text AS address, 
        CASE
            WHEN i.invoicestatus::text = '1'::text THEN 'Nieuw'::text
            WHEN i.invoicestatus::text = '2'::text THEN 'Verstuurd'::text
            WHEN i.invoicestatus::text = '3'::text THEN 'Geboekt'::text
            ELSE NULL::text
        END AS status, i.credit
   FROM invoice i
   LEFT JOIN customer c ON c.customerid::numeric = i.customer;