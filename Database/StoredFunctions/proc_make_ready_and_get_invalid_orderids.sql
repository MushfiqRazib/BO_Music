

CREATE OR REPLACE FUNCTION proc_make_ready_and_get_invalid_orderids(p_orderid bigint[])
  RETURNS character varying AS
$BODY$
DECLARE
	
	_invalid_orderids character varying;    -- it should store the invalid orderid if any
	_rec record;				-- temporary loop variable to store articlecode and ordered quantity
BEGIN
	_invalid_orderids = '';
	FOR i IN array_lower(p_orderid,1)..array_upper(p_orderid,1) LOOP			--Loop through all orders
		if(select CAST(orderstatus AS integer) from orders where orderid = p_orderid[i])=1 then  --if the status is assigned

			--check that the ordered quantities does not exceeds from stock quantities (count of status false = 0 means the order is valid)
			if(select count(status) from (select orderid, status from (
			select distinct ol.orderid, case when ol.quantity > a.quantity then false else true end status
			from ordersline ol, article a 
			where ol.articlecode = a.articlecode
			and ol.orderid = p_orderid[i]) v
			where status=false) v2) = 0 then			
				FOR _rec IN select articlecode, quantity from ordersline where orderid = p_orderid[i]  LOOP
					--update stock
					Update article set quantity = quantity - _rec.quantity where articlecode = _rec.articlecode;
				END LOOP;
				--update order status
				update orders set orderstatus = 2 where orderid = p_orderid[i]; 
			else
				--concatenate orderid into _invalid_orderids
				_invalid_orderids = _invalid_orderids || cast (p_orderid[i] as character varying) || ',';
			end if; 
		else
			_invalid_orderids = _invalid_orderids || cast (p_orderid[i] as character varying) || ',';
		end if;
			

	END LOOP;
	
	RETURN Trim(trailing ',' from _invalid_orderids);
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'NUM:%, DETAILS:% inside Stored function <proc_make_ready_and_get_invalid_orderids(%)>', SQLSTATE, SQLERRM, p_orderid;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE
  COST 100;
