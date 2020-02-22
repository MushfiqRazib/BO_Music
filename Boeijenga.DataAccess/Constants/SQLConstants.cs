using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.DataAccess.Constants
{
    public class SQLConstants
    {
        public const string GET_PAYMENT_TYPES =@" select id,name from paymode order by id" ;
        public const string GET_SHIPPINGCOST = "get_shippingcost";

        public const string GET_SHIPPINGCOST_WITH_VAT = "get_shippingcost_with_vat";


        public const string GET_COUNTRY_BY_COUNTRYNAME = @"select countrycode, countryname ,iseu
                                                            from country
                                                             where countryname= :countryname";
        public const string GET_COUNTRY_BY_COUNTRYCODE = @"select countrycode, countryname ,iseu
                                                            from country
                                                             where countrycode= :countrycode";

        #region OrderManagement
        public const string MAKE_ORDER_READY = @"SELECT proc_make_ready_and_get_invalid_orderids(array[{0}]);";
        public const string MAKE_INVOICE = @"SELECT proc_make_invoice_and_get_invalid_orderids(array[{0}]);";
        public const string DELETE_ORDERS = @"SELECT proc_delete_orders_and_get_invalid_orderids(array[{0}]);";


        public const string GET_COUNTRIES = "select countrycode, countryname from country order by countryname";
        public const string GET_COMPOSER = "select composerid, ltrim(firstname || ' ' || middlename || ' ' || lastname, ' ') as composername from composer order by composername asc";

        public const string GET_PUBLISHER = "select * from publisher order by firstname asc";

        public const string Get_Grade_by_Culture = "select * from grade";
        public const string Get_Period_by_Culture = "select * from period";
        public const string Get_SubCategory_by_Culture = "select * from subcategory";

        public const string Get_Category_by_Culture = "select * from category";

        public const string SELECT_LANG = "";
        public const string Get_Invoice_Info_By_Order_Id = @"select o.orderid,to_char(o.orderdate,'dd-mm-yyyy') as orderdate,
				COALESCE(c.firstname,'')||' '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') || ' (' || c.email || ')' as Customer,
				COALESCE(c.address,'')||' '||COALESCE(c.housenr,'')||'<br>'||COALESCE(c.postcode,'')||' '||COALESCE(c.residence,'')||'<br>'||(select countryname from country con where con.countrycode=c.country) AS Address
				 from customer c,orders o 
				 where c.customerid =o.customer and o.orderid=:OrderId";

        public const string Get_Customer_Info_By_Order_Id = "select o.orderid,c.customerid,COALESCE(c.dfirstname,'') as fname,COALESCE(c.dmiddlename,'') as mname,COALESCE(c.dlastname,'') as lname,o.dhousenr,o.daddress ,o.dpostcode,o.dresidence,o.dcountry,o.remarks,COALESCE(o.shippingcost,0.00)as shippingcost " +
                "from orders o,customer c" +
                " where o.customer=c.customerid" +
                " and o.orderid=:OrderId";


        public const string Get_Order_Info_With_Calculation_By_Order_Id = @"select o.orderid,o.articlecode,
                    '<b>'||a.title||'</b>'|| '<br>'||
		                coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'')
		                ||'<br>'||
		                coalesce((select coalesce(p.firstName,'')||' '||coalesce(p.middleName,'') ||' '||coalesce(p.lastname,'') from publisher p where p.publisherid=a.publisher),'')
		                ||'- '|| o.articlecode
		             as Title,
			     o.quantity,a.quantity as stock,
			     o.unitprice as unitprice,	
			     round((o.unitprice-round(o.unitprice*o.discountpc/100,2))*o.quantity,2) as totalprice,
			     
			     round((((o.unitprice)-((o.unitprice)*(coalesce( (case when o.articlecode='z001' then 0.00 else (case when o.discountpc not in(0.00) then o.discountpc else 0.00 end) end),0))/100))*o.vatpc)/100,2)*o.quantity
				as vatAmount,
			     coalesce(case when a.articlecode='z001' then 0.00 else round(round(round(o.unitprice*o.quantity,2)*o.discountpc,2)/100,2)end) as discountAmount,
			     vatpc,o.discountpc as discount from ordersline o,article a,composer c,orders os
			     where c.composerid=a.composer and o.articlecode=a.articlecode and os.orderid=o.orderid  and o.orderid=:OrderId " + @"
					order by o.articlecode";

        public const string Get_Order_By_Order_Id = @"select * from orders where orderid=:OrderId";
        public const string Get_Customer_By_Customer_Id = @"SELECT firstname, middlename, lastname, initialname, housenr, address, 
                                           postcode, residence, country, email, website, telephone, fax, 
                                           companyname, dhousenr, daddress, dpostcode, dresidence, dcountry, 
                                           customerid, password, CASE WHEN discountpc IS NULL THEN 0 ELSE discountpc END discountpc, vatnr, dfirstname, dmiddlename, 
                                           dlastname, dinitialname, role
                                      FROM customer
                                      WHERE customerid=:CustomerId";
        public const string DELETE_FROM_TABLE = @"Delete From :tableName where :filterString";
        public const string delete_orderline_byorder_id = @"delete from ordersline where orderid=:orderid";
        public const string update_order_by_order_id = @"update orders set daddress=:daddress,dhousenr=:dhousenr ,dpostcode=:dpostcode,dresidence=:dresidence,dcountry=:dcountry,remarks=:remarks where orderid=:orderid";
        public const string update_customer_by_customer_id = @"update customer set dfirstname=:dfirstname,dmiddlename=:dmiddlename,dlastname=:dlastname,dinitialname=:dinitialname where customerid=:customerid";
        public const string insert_into_orederline = @"insert into ordersline(orderid,articlecode,unitprice,vatpc,quantity,discountpc) values(:orderid,:articlecode,:unitprice,:vatpc,:quantity,:discountpc);";

        public const string get_article_by_article_code = @"
                            select a.articlecode, 
                            ('<b>'||
	                            (case when char_length(a.title)>80 then substr(a.title,0,80)||'...' else a.title end)
	                            ||'</b>'||'<br>'||  '<i>'||coalesce(p.firstName,'')||' '||coalesce(p.middleName,'') ||' '||coalesce(p.lastname,'')||'</i><br>'||  
	                            (case when lower(a.articletype)='c' then 'CD/DVD' 
	                            when lower(a.articletype)='b' then 'Book' 
	                            when lower(a.articletype)='z' then 'S&H' 
	                            when lower(a.articletype)='s' then 'SheetMusic' 
	                            else '' end))  as title,
                            a.quantity as stock, '1' as qty,a.price,'0.00' as vat,'0.00' as discount,(a.price) as netprice 
                            from article a, composer p 
                            where a.articlecode=:articlecode 
                            and a.composer = p.composerid";

        public const string get_article_info = @"select COALESCE(a.articlecode,'') as code, (case when lower(a.articletype)='s' then 'Sheet Music'  when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='z' then 'S&H' when lower(a.articletype)='c' then 'CD/DVD' else '' end)as type,a.title, (case when char_length(a.descriptionen)>150 then substr(a.descriptionen,0,150) end) as description,
                       a.quantity as qty,a.price as price,COALESCE(c.firstname,'')||' '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') as author 
                       from article a, composer c where a.composer = c.composerid order by a.title";

        public const string GET_ARTICLE_INFO_WITH_LIMIT = @"select COALESCE(a.articlecode,'') as code, (case when lower(a.articletype)='s' then 'Sheet Music'  when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='z' then 'S&H' when lower(a.articletype)='c' then 'CD/DVD' else '' end)as type,a.title, (case when char_length(a.descriptionen)>150 then substr(a.descriptionen,0,150) end) as description,
                       a.quantity as qty,a.price as price,COALESCE(c.firstname,'')||' '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') as author 
                       from article a
                       LEFT JOIN composer c ON a.composer = c.composerid order by {0} {1}
                       offset {2} limit {3}";
        public const string GET_ARTICLE_INFO_COUNT = @"select count(*) FROM article";


        public const string get_order_id_from_supply_order = @"select coalesce(max(supplyorderid)+1,1) as orderid from supplyorders";


        #endregion

        #region Pdf Reprot
        public const string Get_Delivery_Info_with_Customer_Name_By_Order_Id = "select o.orderid,COALESCE(c.dfirstname,'')||' '||COALESCE(c.dmiddlename,'')||' '||COALESCE(c.dlastname,'') as Customer," +
                   "COALESCE(o.daddress,'')||'  '||COALESCE(o.dhousenr,'') as house, COALESCE(o.dpostcode,'')|| '  ' ||COALESCE(o.dresidence,'') as postcode," +
                   "(select countryname from country con where con.countrycode=o.dcountry) || '.' AS Address," +
                   " COALESCE(o.shippingcost,0.00) as shippingcost,COALESCE(o.remarks,'')as remarks," +
                   " to_char(o.orderdate,'dd-mm-yyyy') as orderdate" +
                   " from orders o,customer c where c.customerid =o.customer and o.orderid=:OrderId";


        public const string Get_Invoice_Info_with_Customer_Name_By_Order_Id = "select o.orderid," +
            "COALESCE(c.firstname,'')||' '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') as Customer," +
            "COALESCE(c.address,'')||'  '||COALESCE(c.housenr,'') as house, COALESCE(c.postcode,'')||'  '||COALESCE(c.residence,'')  as postcode, (select countryname from country con where con.countrycode=c.country) || '.' AS Address" +
            " from customer c,orders o " +
            " where c.customerid =o.customer and o.orderid=:OrderId";


        public const string Get_Discount_Amount_and_VAT_Amount_By_Order_Id = @"select o.orderid,o.articlecode,
            a.title as title, coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'') as author, (case when lower(a.articletype)='c' then 'CD/DVD' when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='s' then 'SheetMusic' else '' end) as type,
            o.quantity as qty,a.quantity as stock,o.unitprice,
	    trunc((o.quantity*o.unitprice)-
				trunc(((o.quantity*o.unitprice)*
				(coalesce( (case when o.articlecode='z001' then 0.00 else (case when o.discountpc not in(0.00) then o.discountpc else 0.00 end) end),0))/100),2),2) as totalprice,
            trunc((((o.quantity*o.unitprice)-((o.quantity*o.unitprice)*(coalesce( (case when o.articlecode='z001' then 0.00 else (case when o.discountpc not in(0.00) then o.discountpc else 0.00 end) end),0))/100))*o.vatpc)/100,2) as vatAmount,
            coalesce(case when a.articlecode='z001' then 0.00 else trunc((o.unitprice*o.quantity*o.discountpc )/100,2)end) as discountAmount,
            trunc((
		(trunc((o.quantity*o.unitprice)-
				trunc(((o.quantity*o.unitprice)*
				(coalesce( (case when o.articlecode='z001' then 0.00 else (case when o.discountpc not in(0.00) then o.discountpc else 0.00 end) end),0))/100),2),2)) +
		(trunc((((o.quantity*o.unitprice)-((o.quantity*o.unitprice)*(coalesce( (case when o.articlecode='z001' then 0.00 else (case when o.discountpc not in(0.00) then o.discountpc else 0.00 end) end),0))/100))*o.vatpc)/100,2))
            ),2)as NETPrice from ordersline o,article a,composer c
             where c.composerid=a.composer and o.articlecode=a.articlecode and o.orderid=:OrderId order by a.articlecode;";




        #endregion


        #region Invoice Management

        public const string UPDATE_STATUS_AS_SENT = @"SELECT proc_update_status_as_sent_and_get_invalid_ids(array[{0}]);";
        public const string Get_Invoice_Line_Info_By_Invoice_Id = "select i.invoiceid,to_char(i.invoicedate,'dd-mm-yyyy') as invoicedate," +
                    "(" +
                    "case when i.invoicestatus='1' then 'Boeken'" +
                    "when invoicestatus='2' then 'Geboekt'" +
                    "end" +
                    ")as status,invoicestatus,i.customerbtwnr," +
                    " i.housenr,i.address,i.postcode,i.residence,i.country,i.credit," +
                    "(select COALESCE(firstname||' ','')||COALESCE(middlename||' ','')||COALESCE(lastname,'') from customer where customerid=i.customer) as customer" +
                    " from invoice i where i.invoiceid=:InvoiceId";



        public const string Get_Invoice_Line_By_Invoice_Id = @"select ol.articlecode,il.invoiceid,ol.orderid,coalesce( (case when ol.articlecode='z001' then 0.00 else ol.discountpc end),0)as discountpc,ol.vatpc,
					(
					 select '<b>'||title ||'</b><br>'||
						(select coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'') from composer c,article a where c.composerid=a.composer and a.articlecode=ol.articlecode)
						||'<br>'||
						coalesce((select coalesce(p.firstName,'')||' '||coalesce(p.middleName,'') ||' '||coalesce(p.lastname,'') from publisher p,article a where p.publisherid=a.publisher and a.articlecode=ol.articlecode),'')
						||'- '|| ol.articlecode
						from article
						where articlecode=ol.articlecode
					 ) as Article,
					 ol.unitprice as unitprice,
					 (case when i.credit is null then ol.quantity else ol.creditedquantity end) as quantity, article.quantity as stock,
					round((ol.unitprice-round(ol.unitprice*ol.discountpc/100,2))*(case when i.credit is null then ol.quantity else ol.creditedquantity*-1 end),2) as totalprice,
					 round((((ol.unitprice)-((ol.unitprice)*(coalesce( (case when ol.articlecode='z001' then 0.00 else (case when ol.discountpc not in(0.00) then ol.discountpc else 0.00 end) end),0))/100))*ol.vatpc)/100,2)*(case when i.credit is null then ol.quantity else ol.creditedquantity*-1 end)
					 as vatAmount,
					 coalesce(case when ol.articlecode='z001' then 0.00 else round(round(ol.unitprice*ol.discountpc,2)/100,2)*ol.quantity end) as discountAmount
					 from orders o,ordersline ol,invoiceline il,invoice i,article 
					 where il.invoiceid=i.invoiceid 
					 and o.orderid=ol.orderid 
					 and il.orderid=ol.orderid 
					 and (case when i.credit is null then ol.quantity>0 else ol.creditedquantity>0 end)
                                             and ol.articlecode = article.articlecode
                                             and i.invoiceid in (:InvoiceId) 
                                            order by ol.articlecode";



        public const string Get_Invoice_By_Invoice_Id = @"select * from invoice where invoiceid=:InvoiceId";

        public const string Update_Invoice_By_Invoice_Id = @"update invoice set  customerbtwnr=:customerbtwnr,housenr=:housenr,address=:address,postcode=:postcode," +
                            "residence=:residence,country=:country,invoicedate=:invoicedate,invoicestatus=:invoicestatus where invoiceid=:InvoiceId";


        public const string delete_orderline_by_articleCode = "delete from ordersline where articlecode=:ArticleCode";


        public const string Get_Invoice_Report_By_InvoiceStatus_Equal_1 = "select to_char(i.invoicedate,'dd-MM-yyyy') as invoicedate," +
                   "i.invoiceid,(select coalesce(firstname||' ','')||coalesce(middlename||' ','')||coalesce(lastname,'') from customer where customerid=i.customer) as customer," +
                   "(coalesce(i.housenr||', ','')||coalesce(i.address||', ','')||coalesce(i.residence,' ') )as address," +
                   " (case when i.invoicestatus='1' then 'Nieuw'" +
                   " when i.invoicestatus='2'  then 'Verstuurd'" +
                   " when i.invoicestatus='3'  then 'Geboekt'" +
                   " end) as status, credit " +
                   " from invoice i where i.invoicestatus='1'";

        public const string Get_Invoice_Report_By_InvoiceStatus_Equal_2 =
             "select to_char(i.invoicedate,'dd-MM-yyyy') as invoicedate," +
                   "i.invoiceid,(select coalesce(firstname||' ','')||coalesce(middlename||' ','')||coalesce(lastname,'') from customer where customerid=i.customer) as customer," +
                   "(coalesce(i.housenr||', ','')||coalesce(i.address||', ','')||coalesce(i.residence,' ') )as address," +
                   " (case when i.invoicestatus='1' then 'Nieuw'" +
                   " when i.invoicestatus='2'  then 'Verstuurd'" +
                   " when i.invoicestatus='3'  then 'Geboekt'" +
                   " end) as status, credit " +
                   " from invoice i where i.invoicestatus='2' ";


        public const string Get_Invoice_Report_By_InvoiceStatus_Equal_3 =
            "select to_char(i.invoicedate,'dd-MM-yyyy') as invoicedate," +
                  "i.invoiceid,(select coalesce(firstname||' ','')||coalesce(middlename||' ','')||coalesce(lastname,'') from customer where customerid=i.customer) as customer," +
                  "(coalesce(i.housenr||', ','')||coalesce(i.address||', ','')||coalesce(i.residence,' ') )as address," +
                  " (case when i.invoicestatus='1' then 'Nieuw'" +
                  " when i.invoicestatus='2'  then 'Verstuurd'" +
                  " when i.invoicestatus='3'  then 'Geboekt'" +
                  " end) as status, credit " +
                  " from invoice i where i.invoicestatus='3'";

        public const string Get_Invoice_Report_By_InvoiceStatus_Equal_4 =
            "select to_char(i.invoicedate,'dd-MM-yyyy') as invoicedate," +
                   "i.invoiceid,(select coalesce(firstname||' ','')||coalesce(middlename||' ','')||coalesce(lastname,'') from customer where customerid=i.customer) as customer," +
                   "(coalesce(i.housenr||', ','')||coalesce(i.address||', ','')||coalesce(i.residence,' ') )as address," +
                   " (case when i.invoicestatus='1' then 'Nieuw'" +
                   " when i.invoicestatus='2'  then 'Verstuurd'" +
                   " when i.invoicestatus='3'  then 'Geboekt'" +
                   " end) as status, credit " +
                   " from invoice i ";


        public const string Get_Invoice_Line_Report_By_InvoiceId = @"
                    select il.invoiceid,ol.orderid,ol.articlecode,
                        (select '<b>'||title ||'</b><br>'||
                        (select coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'') from composer c,article a where c.composerid=a.composer and a.articlecode=ol.articlecode)
                        ||'<br>'||
                        coalesce((select coalesce(p.firstName,'')||' '||coalesce(p.middleName,'') ||' '||coalesce(p.lastname,'') from publisher p,article a where p.publisherid=a.publisher and a.articlecode=ol.articlecode),'')
                        ||'- '|| ol.articlecode
                        from article
                        where articlecode=ol.articlecode) as Article,
                                            ol.unitprice as unitprice,
					    case when i.credit is null then ol.quantity else ol.creditedquantity end as quantity, 
					    article.quantity as stock, ol.vatpc as Vat,
		                            coalesce( (case when ol.articlecode='z001' then 0.00 else (case when ol.discountpc not in(0.00) then ol.discountpc else 0.00 end) end),0) as Discount,
					    round((ol.unitprice-round(ol.unitprice*ol.discountpc/100,2))*ol.quantity,2) as totalprice
                                             from ordersline ol,invoiceline il,invoice i,article, category ca
                                             where il.invoiceid=i.invoiceid
                                             and il.orderid=ol.orderid
		                                    and ca.categoryid =( 
                                                                select case 
                                                                  when (position(',' in category)-1)<0 then 
                                                                   category 
                                                                  else 
                                                                   substr (category, 1,position(',' in category)-1)
                                                                  end
                                                                 from article 
                                                                 where articlecode=ol.articlecode
                                                                )
and (case when i.credit is null then ol.quantity>0 else ol.creditedquantity>0 end)
                                             and ol.articlecode = article.articlecode
                                             and i.invoiceid in (:InvoiceId) 
                                            order by ol.articlecode
                    ";
        #endregion


        #region Spply Order
        public const string Get_SUpplier = @"select COALESCE(firstname,'')||' '||COALESCE(middlename,'')||' '||COALESCE(lastname,'') as name, publisherid from " +
            "publisher order by firstname asc";


        public const string Get_Supplier_Address_By_PublisherId = @"select (COALESCE(housenr,'')||', '||COALESCE(address,'')||'<br>'||COALESCE(postcode,'')||', '||COALESCE(residence,'')||" +
                       "'<br>'||(select countryname from country where lower(countrycode)=lower(coalesce(p.country,'')))) as supplieraddress from publisher p, country c where " +
                       "publisherid =:PublisherId";

        public const string Get_Orders_Line = @"select articlecode, title, quantity as stock,'1' as qty,price,'0' as vat,'' as netprice from article where articlecode = ''";

        public const string Get_SupplyOrders_By_SupplyOrderId = @"select * from supplyorders where supplyorderid =:SupplyOrderId";

        public const string Get_OrdersLine_By_OrderId = @"select COALESCE(s.dhousenr,'') as dhousenr,COALESCE(s.daddress,'') as daddress,COALESCE(s.dpostcode,'') as dpostcode,COALESCE(s.dresidence,'') as dresidence,COALESCE(s.dcountry,'') as dcountry,s.supplyorderid as supplyorderid,s.supplyorderdate as supplyorderdate,s.supplierid as supplierid," +
      "s.deliverydate as deliverydate,s.supplyorder_by as supplyorder_by,s.receivingstatus as receivingstatus,s.paymentstatus as paymentstatus, (COALESCE(p.housenr,'')||', '||COALESCE(p.address,'')||'<br>'||COALESCE(p.postcode,'')||', '||COALESCE(p.residence,'')||" +
      "'<br>'||(select countryname from country where lower(countrycode)=lower(coalesce(p.country,'NL')))) as supplieraddress from supplyorders s, publisher p, country c  where s.supplyorderid =:OrderNo and " +
      "p.publisherid =s.supplierid";


        public const string Get_Orders_For_SupplyOrder_By_OrderId = @" select s.supplyorderid as supplyorderid,s.supplier_articlecode as supplyArticleID
, s.articlecode as articlecode,COALESCE(s.vatpc,0) as vat, " +
              "s.orderqty as qty,round((s.unitprice*s.orderqty)+(s.unitprice*s.orderqty*COALESCE(s.vatpc,0))/100,2) as netprice, " +
              "('<b>'||COALESCE(a.title,'')||'</b>'||'<br>'|| " +
              " '<i>'||coalesce(p.firstName,'')||' '||coalesce(p.middleName,'') ||' '||coalesce(p.lastname,'')||'</i><br>'|| " +
              " (case when lower(a.articletype)='c' then 'CD/DVD' when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='s' then 'SheetMusic' else '' end)) " +
              " as title,a.quantity as stock, " +
              "s.unitprice as price from supplyordersline s, article a, composer p where s.supplyorderid =:OrderNo and " +
              "a.articlecode = s.articlecode and a.composer = p.composerid";




        public const string Get_Article_For_SupplyOrder = @"select COALESCE(a.articlecode,'') as code, (case when lower(a.articletype)='s' then 'Sheet Music'  when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='c' then 'CD/DVD' else '' end)as type,
                         a.title,(COALESCE(a.editionno,'')) as editionno, a.quantity as qty,(case when lower(a.articletype)='s' then a.price * 0.65 when lower(a.articletype)='b' then a.price * 0.75 when lower(a.articletype)='c' then a.price * 0.70 end) as price,
                         COALESCE(c.firstname,'')||' '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') as author,
                         (COALESCE(p.firstname,'')||' '||COALESCE(p.middlename,'')||' '||COALESCE(p.lastname,'')) as publisher
                         from article a left join composer c on a.composer=c.composerid left join publisher p  on a.publisher=p.publisherid
                         order by a.title asc";


        public const string Get_Max_Supply_Orders = @"select coalesce(max(supplyorderid)+1,1) as orderid from supplyorders";

        public const string Get_OrdersLine_By_ArticleCode = @"select a.articlecode, " +
                               "('<b>'||(case when char_length(a.title)>80 then substr(a.title,0,80)||'...' else a.title end)||'</b>'||'<br>'|| " +
               " '<i>'||coalesce(p.firstName,'')||' '||coalesce(p.middleName,'') ||' '||coalesce(p.lastname,'')||'</i><br>'|| " +
               " (case when lower(a.articletype)='c' then 'CD/DVD' when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='s' then 'SheetMusic' else '' end)) " +
               " as title," +
                               "a.quantity as stock, '1' as qty,(case when lower(a.articletype)='s' then a.price * 0.65 when lower(a.articletype)='b' then a.price * 0.75 when lower(a.articletype)='c' then a.price * 0.70 end) as price,'0' as vat,(a.price) as netprice from article a, composer p where " +
                    "a.articlecode=:ArticleCode and a.composer = p.composerid";


        public const string insert_into_supplyorders = @"insert into supplyorders (dhousenr,daddress,dpostcode,dresidence,dcountry,supplyorderid,supplyorderdate,supplierid," +
                        "deliverydate,supplyorder_by,receivingstatus,paymentstatus) " +
                        "values(:dhousenr,:daddress,:dpostcode,:dresidence,:dcountry,:supplyorderid,:supplyorderdate,:supplierid,:deliverydate,:supplyorder_by,:receivingstatus,:paymentstatus)";
        public const string insert_into_supplyordersline = @"insert into supplyordersline (supplyorderid,articlecode,unitprice,vatpc,orderqty,supplier_articlecode) " +
                       "values(:supplyorderid,:articlecode,:unitprice,:vatpc,:orderqty,:supplierArticlecodeID) ";



        public const string update_into_supplyorders_by_orderno = @"update supplyorders set dhousenr=:dhousenr,daddress=:daddress,dpostcode=:dpostcode," +
                            "dresidence=:dresidence,dcountry=:dcountry,supplyorderid=:supplyorderid,supplyorderdate=:supplyorderdate," +
                            "supplierid=:supplierid,deliverydate=:deliverydate,supplyorder_by=:supplyorder_by,receivingstatus=:receivingstatus," +
                            "paymentstatus=:paymentstatus where supplyorderid =:OrderNo";


        public const string Get_EmailAddress_From_Publisher_By_PublisherId = @"select coalesce(email,'') as email from publisher  where publisherid =:PublisherId";
        #endregion


        #region Receive Order

        public const string Get_RceiveDeatils_From_Supplyorders_by_SupplyOrderId = "select so.dcountry,COALESCE(c.shippingcost,0.00) as shipcost from supplyorders so,country c  " +
             "where  so.dcountry=c.countrycode and supplyorderid=:SupplyOrderId";

        public const string insert_into_receiveorders = "insert into receiveorders values(:receiveid,:supplyorderid,:receivedate,:shippingcost,:remarks,:received_by)";

        public const string insert_into_receiveordersline = "insert into receiveordersline values(:receiveid,:articlecode,:purchaseprice,:receiveqty)";


        public const string Get_ReceiveDetails_By_SupplyOrder = "select a.articlecode,so.supplyorderid," +
             "'<b>'||a.title||'</b>'|| '<br>'||coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'')||'<br>'||(case when lower(a.articletype)='c' then 'CD/DVD' when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='s' then 'SheetMusic' else '' end) as Title," +
             "sol.unitprice," +
             "(case when lower(so.receivingstatus)='n' then 'Not Received' " +
             "when lower(so.receivingstatus)='p' then 'Partially Received' " +
             "when lower(so.receivingstatus)='f'  then 'Full Received' end) as receivingstatus," +
             "sol.orderqty,0 as receiveqty, " +
             "(" +
             "(select sum(receiveqty) from supplyordersline where articlecode=a.articlecode and supplyorderid=so.supplyorderid) " +
             ") as previous from article a,supplyordersline sol,supplyorders so,composer c " +
             "where sol.articlecode=a.articlecode " +
             "and so.supplyorderid=sol.supplyorderid " +
             "and c.composerid=a.composer " +
             "and a.articlecode IN(select articlecode from supplyordersline where supplyorderid=:SupplyOrder) " +
             "and so.supplyorderid=:SupplyOrder";

        public const string Get_Max_ReceiveOrders = "select coalesce(max(receiveid),0) as maximum from receiveorders";
        public const string Get_SupplyOrder_by_SupplyOrderId = "select to_char(supplyorderdate,'dd-mm-yyyy') as supdate ,to_char(deliverydate,'dd-mm-yyyy') as deldate from supplyorders where supplyorderid=:SupplyOrder";
        #endregion


        #region Stock Management
        public const string UPDATE_PAYMENT_STATUS = @"Update supplyorders set paymentstatus=:paymentstatus
                                                    where supplyorderid in ({0})";

        #endregion


        #region Article Management
        public const string Get_Category = @"Select * from category order by categoryid asc";
        public const string Get_Instrumentation = @"Select * from instrumentation order by instrumentname asc";
        public const string Get_ArticleType = @"Select distinct articletype from article";
        public const string Get_Aritcle_By_ArticleCode = @"SELECT * FROM article where articlecode=:ArticleCode";

        public const string Get_ArticleCode_Info_By_Type = @"SELECT articlecode 
                                                                FROM article 
                                                                where articletype=:articletype 
                                                                order by articlecode desc
                                                                limit 1";


        public const string insert_into_article = @"  INSERT INTO article(
            articlecode, descriptionen, title, subtitle, composer, serie, 
            grade, editor, subcategory, events, publisher, country, price, 
            editionno, publicationno, pages, publishdate, duration, ismn, 
            isbn10, isbn13, articletype, quantity, imagefile, pdffile, purchaseprice, 
            descriptionnl, category, period, isactive, price_bak, 
            containsmusic, keywords, instrumentation)
    VALUES ( :articlecode, :descriptionen, :title, :subtitle, :composer, :serie, 
            :grade, :editor, :subcategory, :events, :publisher, :country, :price, 
            :editionno, :publicationno, :pages, :publishdate, :duration, :ismn, 
            :isbn10, :isbn13, :articletype, :quantity, :imagefile, :pdffile, :purchaseprice, 
            :descriptionnl, :category, :period, :isactive, :price_bak, 
            :containsmusic, :keywords, :instrumentation);";




        public const string update_into_article = @" UPDATE article
   SET descriptionen=:descriptionen, title=:title, subtitle=:subtitle, composer=:composer, 
       serie=:serie, grade=:grade, editor=:editor, subcategory=:subcategory, events=:events, publisher=:publisher, 
       country=:country, price=:price, editionno=:editionno, publicationno=:publicationno, pages=:pages, publishdate=:publishdate, 
       duration=:duration, ismn=:ismn, isbn10=:isbn10, isbn13=:isbn13, articletype=:articletype, quantity=:quantity, 
       imagefile=:imagefile, pdffile=:pdffile, purchaseprice=:purchaseprice, descriptionnl=:descriptionnl, 
       category=:category, period=:period, isactive=:isactive, price_bak=:price_bak, containsmusic=:containsmusic, 
       keywords=:keywords, instrumentation=:instrumentation
 WHERE articlecode=:ArticleCode";
        #endregion



        #region Publisher
        public const string insert_into_publisher = @"INSERT INTO publisher(firstname, middlename, lastname, initialname, housenr, address, postcode, residence, country, email, website, telephone, fax, companyname, ispublisher) VALUES (:firstname,:middlename,:lastname,:initialname,:housenr,:address,:postcode,:residence,:country,:email,:website,:telephone,:fax,:companyname,:ispublisher);";
        #endregion

        #region Composer
        public const string insert_into_composer = @"INSERT INTO composer(
            firstname, middlename, lastname, country, dob, dod)
    VALUES (:firstname, :middlename, :lastname, :country, :dob, :dod);";
        #endregion

        #region Record
        public const string Load_FKDisplay_Column = @"select columnnames from lookup where lower(tablename)=:TableName";

        public const string Get_Primary_Key = @"select column_name,constraint_name,substring(constraint_name from '_.key') as cons
			from information_schema.constraint_column_usage where lower(table_name)=:TableName and substring(constraint_name from '_.key')='_pkey'";

        public const string Get_Column_CategoryTable = @"select  cast(substring(:ColumnName ,2,length(:ColumnName)) as int4) as maxid " +
                    "from :Tab where lower(substring(:ColumnName ,1,1))=lower(':Prefix') " +
                    " order by cast(substring(:ColumnName ,2,length(:ColumnName)) as int4) desc " +
                    "Limit 1";

        public const string Get_Column_Table =@"select  cast(substring(:ColumnName ,3,length(:ColumnName)) as int4) as maxid " +
                    "from :Tab where lower(substring(:ColumnName,1,1))=lower(':Prefix') " +
                    " order by cast(substring(:ColumnName ,3,length(:ColumnName)) as int4) desc " +
                    "Limit 1";

        public const string Get_Primary_Key_ByTable = @"select column_name,constraint_name,substring(constraint_name from '_.key') as cons
			from information_schema.constraint_column_usage where lower(table_name)=:Tab and substring(constraint_name from '_.key')='_pkey'";

        public const string Get_MaxId_FromCoslumn = @"select coalesce(max(cast(:ColumnName as int4)),0) as maxid from :tab";

        public const string Get_Column_Names = @"select column_name from information_schema.columns where lower(table_name)=:Tab order by ordinal_position";
        public const string Get_ColumnCaption_ByTableName = "select initcap(column_name) as col,coalesce(character_maximum_length,0) as len,data_type from information_schema.columns where lower(table_name)=:TableName order by ordinal_position";
        public const string Get_Foreign_Keys = "select  split_part(constraint_name,'_',2) as col,constraint_type from information_schema.table_constraints where lower(table_name)=:TableName and lower(constraint_type)='foreign key'";

        public const string Update_Table_By_TableName = @"select column_name,data_type from information_schema.columns where lower(table_name)=:tableName order by ordinal_position";

        public const string Get_DataType_By_TableName = @"select column_name,data_type from information_schema.columns where lower(table_name)=:TableName order by ordinal_position";

        public const string Get_Lookup_DropDownColumn = "select initcap(column_name)as col from information_schema.columns where lower(table_name)=:TableName order by column_name";

        public const string Get_Data_ByTableAndColumnName = @"select split_part(a.constraint_name, '_', 1) as BaseTable,split_part(a.constraint_name, '_', 2) as FKey,s.table_name as RefTab
				from information_schema.referential_constraints a,information_schema.table_constraints s
				where lower(split_part(a.constraint_name, '_', 1))=:TableName and lower(split_part(a.constraint_name,'_',2)) =:ColumnName and a.unique_constraint_name=s.constraint_name and s.constraint_type='PRIMARY KEY'
				order by a.constraint_name
				";


        public const string Get_Report_By_FromAndTo_dt1 = @"select os.articlecode as ArticleCode,a.title as Title, round(Sum(unitprice*os.quantity*(1-discountpc/100)),2) as Price, sum(os.quantity) as Quantity
                    from invoiceline il
		            left join orders o on il.orderid = o.orderid
		            left join ordersline os on o.orderid = os.orderid
		            left join article a on os.articlecode=a.articlecode 
                    where o.orderdate between :From and :To  
                    and os.articlecode like 'b%' 
                    group by os.articlecode,a.title 
                    order by Quantity desc, Price desc";


        public const string Get_Report_By_FromAndTo_dt2 = @"select os.articlecode as ArticleCode,a.title as Title, round(Sum(unitprice*os.quantity*(1-discountpc/100)),2) as Price, sum(os.quantity) as Quantity
                    from invoiceline il
		            left join orders o on il.orderid = o.orderid
		            left join ordersline os on o.orderid = os.orderid
		            left join article a on os.articlecode=a.articlecode 
                    where o.orderdate between :From and :To 
                    and (os.articlecode like 'c%' or os.articlecode like 'd%') 
                    group by os.articlecode,a.title 
                    order by Quantity desc, Price desc";


        public const string Get_Report_By_FromAndTo_dt3 = @"select os.articlecode as ArticleCode,a.title as Title, round(Sum(unitprice*os.quantity*(1-discountpc/100)),2) as Price, sum(os.quantity) as Quantity
                    from invoiceline il
		            left join orders o on il.orderid = o.orderid
		            left join ordersline os on o.orderid = os.orderid
		            left join article a on os.articlecode=a.articlecode 
                    where o.orderdate between :From and :To 
                    and os.articlecode like 's%' 
                    group by os.articlecode,a.title 
                    order by Quantity desc, Price desc";


             public const string Get_Vat_Analysis = @"select  sum(case when ol.vatpc=0.00 then round((((ol.unitprice)-((ol.unitprice)*(coalesce( (case when ol.articlecode='z001' then 0.00 else (case when ol.discountpc not in(0.00) then ol.discountpc else 0.00 end) end),0))/100))*ol.vatpc)/100,2)*ol.quantity else 0.00 end) as vat0, 
        sum(case when ol.vatpc=6.00 then round((((ol.unitprice)-((ol.unitprice)*(coalesce( (case when ol.articlecode='z001' then 0.00 else (case when ol.discountpc not in(0.00) then ol.discountpc else 0.00 end) end),0))/100))*ol.vatpc)/100,2)*ol.quantity else 0.00 end) as vat6, 
        sum(case when ol.vatpc=19.00 then round((((ol.unitprice)-((ol.unitprice)*(coalesce( (case when ol.articlecode='z001' then 0.00 else (case when ol.discountpc not in(0.00) then ol.discountpc else 0.00 end) end),0))/100))*ol.vatpc)/100,2)*ol.quantity else 0.00 end) as vat19,
	sum(case when ol.vatpc=0.00 then round((ol.unitprice-round(ol.unitprice*ol.discountpc/100,2))*ol.quantity,2) else 0.00 end) as price0, 
        sum(case when ol.vatpc=6.00 then round((ol.unitprice-round(ol.unitprice*ol.discountpc/100,2))*ol.quantity,2)else 0.00 end) as price6, 
        sum(case when ol.vatpc=19.00 then round((ol.unitprice-round(ol.unitprice*ol.discountpc/100,2))*ol.quantity,2)else 0.00 end) as price19
                              from invoiceline as il
                              left join orders as o on il.orderid = o.orderid 
                              left join ordersline as ol on il.orderid=ol.orderid
			      join invoice as i on il.invoiceid=i.invoiceid
			      where i.invoicedate between :From and :To";





             public const string Get_ReportByFromAndTo_For_SalesAnalysis_dt1 = @"select os.articlecode as ArticleCode,a.title as Title, Sum(unitprice) as Price,sum(os.quantity) as Quantity
                    from orders o, ordersline os , article a
                    where o.orderid = os.orderid
                    and os.articlecode=a.articlecode 
                    and o.orderdate between :From and :To
                    and os.articlecode like 'b%' 
                    group by os.articlecode,a.title 
                    order by Quantity desc, Price desc limit :Limit";

             public const string Get_ReportByFromAndTo_For_SalesAnalysis_dt2 = @"select os.articlecode as ArticleCode,a.title as Title, Sum(unitprice) as Price,sum(os.quantity) as Quantity
                    from orders o, ordersline os , article a
                    where o.orderid = os.orderid
                    and os.articlecode=a.articlecode 
                    and o.orderdate between :From and :To
                    and (os.articlecode like 'c%' or os.articlecode like 'd%') 
                    group by os.articlecode,a.title 
                    order by Quantity desc, Price desc limit :Limit";


             public const string Get_ReportByFromAndTo_For_SalesAnalysis_dt3 = @"select os.articlecode as ArticleCode,a.title as Title, Sum(unitprice) as Price,sum(os.quantity) as Quantity
                    from orders o, ordersline os , article a
                    where o.orderid = os.orderid
                    and os.articlecode=a.articlecode 
                    and o.orderdate between :From and :To
                    and os.articlecode like 's%' 
                    group by os.articlecode,a.title 
                    order by Quantity desc, Price desc limit :Limit";


            public const string Get_Available_Fields_by_TableName = @"
            select c.column_name as value, case when v.cons='_pkey' then 'PRIMARY KEY' else c.column_name end as text
            from information_schema.columns c
            left join 
            (select column_name , substring(constraint_name from '_.key') as cons
            from information_schema.constraint_column_usage 
            where lower(table_name)=:TableName 
            and substring(constraint_name from '_.key')='_pkey'
            ) v
            on c.column_name=v.column_name
            where lower(table_name)=:TableName";

        #endregion


            public const string SHEETMUSIC_ORGAAN_MAINPRODUCTS = @"SELECT a.articlecode,a.title,case when a.imagefile !='' then a.imagefile when  a.articletype='b' then 'book.png' when a.articletype='s' then  'musicsheet.png' when a.articletype='c' then  'CD.png' end as imagefile
                                                              FROM article a
                                                              LEFT JOIN mainproducts m ON (m.articlecode= a.articlecode)
                                                              WHERE m.maincategory LIKE 'sheetmusic_orgaan_category'";


        public const string SHEETMUSIC_OTHER_MAINPRODUCTS = @"SELECT a.articlecode,a.title,case when a.imagefile !='' then a.imagefile when  a.articletype='b' then 'book.png' when a.articletype='s' then  'musicsheet.png' when a.articletype='c' then  'CD.png' end as imagefile
                                                              FROM article a
                                                              LEFT JOIN mainproducts m ON (m.articlecode= a.articlecode)
                                                              WHERE m.maincategory LIKE 'sheetmusic_other_category'";



        public const string BOOK_MAINPRODUCTS = @"SELECT a.articlecode,a.title,case when a.imagefile !='' then a.imagefile when  a.articletype='b' then 'book.png' when a.articletype='s' then  'musicsheet.png' when a.articletype='c' then  'CD.png' end as imagefile
                                                  FROM article a
                                                  LEFT JOIN mainproducts m ON (m.articlecode= a.articlecode)
                                                  WHERE m.maincategory LIKE 'book_category'";



        public const string CDDVD_MAINPRODUCTS = @"SELECT a.articlecode,a.title,case when a.imagefile !='' then a.imagefile when  a.articletype='b' then 'book.png' when a.articletype='s' then  'musicsheet.png' when a.articletype='c' then  'CD.png' end as imagefile
                                                   FROM article a
                                                   LEFT JOIN mainproducts m ON (m.articlecode= a.articlecode)
                                                   WHERE m.maincategory LIKE 'cddvd_category'";


        public const string Login_SQL =
                                        @"SELECT customerid,firstname,middlename,lastname FROM customer WHERE email =@email AND password =@password";



        public const string GET_SEARCH_KEY_WORDS = @"SELECT keyname FROM searchkeywords
                                                     WHERE {0}
                                                      ORDER BY {1}";

        public const string ORDER_INFO = @"select title,deliverytime,COALESCE(subtitle,'')as subtitle,articletype,round(a.price+round(a.price*c.vatpc/100,2),2) as vatincludedprice, a.price as price,vatpc from article a, category c where 
                           c.categoryid =( 
                            select case 
                              when (position(',' in category)-1)<0 then 
                               category 
                              else 
                               substr (category, 1,position(',' in category)-1)
                              end
                             from article 
                             where articlecode=a.articlecode
                            )
                            and articlecode='{0}'";


        public const string PUBLISHER_NAME = @"select case when a.articletype='b' then (COALESCE(p.firstname) || ' '|| COALESCE(p.middlename,'') ||' '|| COALESCE(p.lastname) ) " +
                               "else (COALESCE(c.firstname) ||' '|| COALESCE(c.middlename,'')  ||' '|| COALESCE(c.lastname) )end  as publisher " +
                               "from article a, publisher p, composer c " +
                               "where p.publisherid=a.publisher " +
                               "and a.composer = c.composerid  and  articlecode='{0}'";

    
    }
}
