-- Function: proc_delete_orders_and_get_invalid_orderids(bigint[])

-- DROP FUNCTION proc_delete_orders_and_get_invalid_orderids(bigint[]);

CREATE OR REPLACE FUNCTION proc_delete_orders_and_get_invalid_orderids(p_orderid bigint[])
  RETURNS character varying AS
$BODY$
DECLARE
	
	_invalid_orderids character varying;    -- it should store the invalid orderid if any
BEGIN
	--Store the list of orderid whose status is not assigned. 
	--These IDs will be treated as invalid and will be return to the front end.
	SELECT array_to_string(
		ARRAY(select orderid
		from orders 
			where cast(orderstatus as integer ) <> 1
			and orderid = any(p_orderid)
			order by orderid
		), ',') into _invalid_orderids;

	--delete the orders having orderstatus as assigned (status code = 1)
	delete from orders where cast(orderstatus as integer ) = 1 and orderid = any (p_orderid);

    RETURN _invalid_orderids;

EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'NUM:%, DETAILS:% inside Stored function <proc_delete_orders_and_get_invalid_orderids(%)>', SQLSTATE, SQLERRM, p_orderid;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE
  COST 100;
ALTER FUNCTION proc_delete_orders_and_get_invalid_orderids(bigint[]) OWNER TO postgres;
