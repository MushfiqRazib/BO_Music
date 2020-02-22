CREATE OR REPLACE FUNCTION proc_make_invoice_and_get_invalid_orderids(p_orderid bigint[])
  RETURNS character varying AS
$BODY$
DECLARE
	
	_invalid_orderids character varying;    -- it should store the invalid orderid if any
	_rec record;				-- temporary loop variable to store articlecode and ordered quantity
	_cur_invoiceid bigint;
	_cur_customer bigint;
BEGIN
	_invalid_orderids = '';
	select max(invoiceid) into _cur_invoiceid from invoice ;
	_cur_customer = -1;


	--store the customer info with order status into the loop variable
	FOR _rec IN (SELECT 
			  o.customer, 
			  o.orderid, 
			  c.housenr, 
			  c.address, 
			  c.postcode, 
			  c.residence, 
			  c.country, 
			  c.vatnr, 
			  CAST(o.orderstatus as integer) status
			FROM 
			  public.orders o
			left join public.customer c on c.customerid = o.customer
			where o.orderid = any ( p_orderid)
			order by o.customer, o.orderid) LOOP

		--the order will be processed only if the orderstatus is Ready 
		if _rec.status =2 then
			if(_cur_customer!=_rec.customer) then
				_cur_customer = _rec.customer;						--set current customer
				_cur_invoiceid = _cur_invoiceid + 1;					--set current invoiceid
				
				--insert invoice with customer data
				insert into invoice(housenr,address,postcode,residence,country,invoiceid,customer,customerbtwnr)
					values(_rec.housenr,_rec.address,_rec.postcode,_rec.residence,_rec.country,_cur_invoiceid,_cur_customer,_rec.vatnr);
					
			end if;
		
			insert into invoiceline (invoiceid, orderid) 					--Insert invoiceline
				values (_cur_invoiceid, _rec.orderid);
			update orders set orderstatus = '3' where orderid=_rec.orderid;		--Update orderstatus as 'Invoiced'
		else
			--treat as invalid order and concatenate with invalid orderIDs
			_invalid_orderids = _invalid_orderids || cast (_rec.orderid as character varying) || ',';	
		end if;
			

	END LOOP;
	
	RETURN Trim(trailing ',' from _invalid_orderids);
EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'NUM:%, DETAILS:% inside Stored function <proc_make_invoice_and_get_invalid_orderids(%)>', SQLSTATE, SQLERRM, p_orderid;
END;
$BODY$
  LANGUAGE 'plpgsql' VOLATILE;


