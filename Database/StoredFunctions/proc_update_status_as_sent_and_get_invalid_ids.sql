-- Function: proc_update_status_as_sent_and_get_invalid_ids(bigint[])

-- DROP FUNCTION proc_update_status_as_sent_and_get_invalid_ids(bigint[]);

CREATE OR REPLACE FUNCTION proc_update_status_as_sent_and_get_invalid_ids(p_invoiceid bigint[])
  RETURNS character varying AS
$BODY$
DECLARE
	
	_invalid_invoiceids character varying;    -- it should store the invalid invoiceid if any
BEGIN
	--Store the list of invoiceid whose status is not nieuw. 
	--These IDs will be treated as invalid and will be return to the user.
	SELECT array_to_string(
		ARRAY(select invoiceid
		from invoice 
			where cast(invoicestatus as integer ) <> 1
			and invoiceid = any(p_invoiceid)
			order by invoiceid
		), ',') into _invalid_invoiceids;

	--update the invoices having invoicestatus as nieuw (status code = 1)
	update invoice set invoicestatus='2' 
	where cast(invoicestatus as integer ) = 1 
	and invoiceid = any (p_invoiceid);

    	RETURN _invalid_invoiceids;

EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'NUM:%, DETAILS:% inside Stored function <proc_update_as_sent_and_get_invalid_invoiceids(%)>', SQLSTATE, SQLERRM, p_invoiceid;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE
  COST 100;
ALTER FUNCTION proc_update_status_as_sent_and_get_invalid_ids(bigint[]) OWNER TO postgres;
