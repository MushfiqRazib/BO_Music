using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Boeijenga.Common.Objects;
using Boeijenga.DataAccess.Constants;
using System.Data;
using Npgsql;
using System.Collections;

namespace Boeijenga.DataAccess
{
    public class DataAccessManager
    {              
        #region Country
        public static List<Country> GetCountry()
        {
            List<Country> countries = new List<Country>();
            string sql = SQLConstants.GET_COUNTRIES;
            DataTable dtCountries = DataAccessHelper.GetInstance().GetDataTable(new NpgsqlCommand(sql));
            if (dtCountries.Rows.Count > 0)
            {
                foreach (DataRow row in dtCountries.Rows)
                {
                    Country country = new Country();
                    country.CountryCode = row["countrycode"].ToString();
                    country.CountryName = row["countryname"].ToString();
                    countries.Add(country);
                }
            }
            return countries;
        }
        #endregion

        #region Order
        public static string MakeOrderReadyAndGetInvalidIds(string orderNrs)
        {
            NpgsqlCommand command = new NpgsqlCommand(string.Format(SQLConstants.MAKE_ORDER_READY, orderNrs));
            return DataAccessHelper.GetInstance().GetDataTable(command).Rows[0][0].ToString();
        }
        public static string MakeInvoiceAndGetInvalidIds(string orderNrs)
        {
            NpgsqlCommand command = new NpgsqlCommand(string.Format(SQLConstants.MAKE_INVOICE, orderNrs));
            return DataAccessHelper.GetInstance().GetDataTable(command).Rows[0][0].ToString();
        }
        public static string DeleteOrdersAndGetInvalidIds(string orderNrs)
        {
            NpgsqlCommand command = new NpgsqlCommand(string.Format(SQLConstants.DELETE_ORDERS, orderNrs));
            return DataAccessHelper.GetInstance().GetDataTable(command).Rows[0][0].ToString();
        }

        public static DataTable GetInvoiceInfoByOrderId(int OrderId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Invoice_Info_By_Order_Id);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("OrderId", OrderId);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static OrderDTO GetOrderByOrderId(int OrderId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Order_By_Order_Id);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("OrderId", OrderId);
                DataTable dtOrder = DataAccessHelper.GetInstance().GetDataTable(command);
                OrderDTO order = new OrderDTO();
                if (dtOrder != null && dtOrder.Rows.Count > 0)
                {

                    order.Customer = Int32.Parse(dtOrder.Rows[0]["customer"].ToString());
                    order.Invoicedate = DateTime.Parse(Boeijenga.Common.Utils.Function.HandleNull(dtOrder.Rows[0]["invoicedate"].ToString(), 0));
                    order.Orderdate = DateTime.Parse(dtOrder.Rows[0]["orderdate"].ToString());
                    order.Orderid = Int32.Parse(dtOrder.Rows[0]["orderid"].ToString());
                    order.Orderstatus = dtOrder.Rows[0]["orderstatus"].ToString();
                    order.Remarks = dtOrder.Rows[0]["remarks"].ToString();
                    order.Shippingcost = double.Parse(dtOrder.Rows[0]["shippingcost"].ToString());

                }
                return order;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

      

        public static DataTable GetCustomerInfoByOrderId(int OrderId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Customer_Info_By_Order_Id);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("OrderId", OrderId);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetArticleByArticleCode(string ArticleCode)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.get_article_by_article_code);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("articlecode", ArticleCode);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static DataTable GetArticleCodeInfoByType(char articleType)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_ArticleCode_Info_By_Type);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("articletype", articleType.ToString());

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public static DataTable GetArticleInfo()
        {

            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.get_article_info);
                command.CommandType = CommandType.Text;
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static DataRecord GetArticleInfo(string orderBy, string dir, long offset, int limit)
        {
            try
            {
                DataRecord recordSet = new DataRecord();
                NpgsqlCommand command = new NpgsqlCommand(string.Format(SQLConstants.GET_ARTICLE_INFO_WITH_LIMIT, orderBy, dir, offset, limit));
                command.CommandType = CommandType.Text;
                recordSet.Table = DataAccessHelper.GetInstance().GetDataTable(command);

                NpgsqlCommand command2 = new NpgsqlCommand(SQLConstants.GET_ARTICLE_INFO_COUNT);
                recordSet.Count = long.Parse(DataAccessHelper.GetInstance().GetDataTable(command2).Rows[0][0].ToString());
                
                return recordSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetOrderIdFromSupplyOrder()
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.get_order_id_from_supply_order);
                command.CommandType = CommandType.Text;
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetOrderInfoWithCalculationByOrderId(int OrderId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Order_Info_With_Calculation_By_Order_Id);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("OrderId", OrderId);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetDeliveryInfowithCustomerNameByOrderId(string OrderId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Delivery_Info_with_Customer_Name_By_Order_Id);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("OrderId", OrderId);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetInvoiceInfowithCustomerNameByOrderId(string OrderId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Invoice_Info_with_Customer_Name_By_Order_Id);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("OrderId", OrderId);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetDiscountAmountandVATAmountByOrderId(string OrderId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Discount_Amount_and_VAT_Amount_By_Order_Id);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("OrderId", OrderId);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetInvoiceLineInfoByInvoiceId(string InvoiceId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Invoice_Line_Info_By_Invoice_Id);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("InvoiceId", InvoiceId);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetInvoiceLineByInvoiceId(string InvoiceId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Invoice_Line_By_Invoice_Id);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("InvoiceId", InvoiceId);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static DataTable GetInvoiceLineReportByInvoiceId(string InvoiceId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Invoice_Line_Report_By_InvoiceId);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("InvoiceId", InvoiceId);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static bool UpdateOrder(OrderDTO order, List<OrderLine> orderedItems, Customer customer, int order_no, ref string msg)
        {
            msg = string.Empty;

            NpgsqlCommand[] commands = new NpgsqlCommand[order_no];

            commands[0] = new NpgsqlCommand(SQLConstants.delete_orderline_byorder_id);
            commands[0].Parameters.Add("orderid", order.Orderid);

            commands[1] = new NpgsqlCommand(SQLConstants.update_order_by_order_id);
            commands[1].Parameters.Add("dhousenr", order.Dhousenr);
            commands[1].Parameters.Add("daddress", order.Daddress);
            commands[1].Parameters.Add("dpostcode", order.Dpostcode);
            commands[1].Parameters.Add("dresidence", order.Dresidence);
            commands[1].Parameters.Add("dcountry", order.Dcountry);
            commands[1].Parameters.Add("orderdate", order.Orderdate);
            commands[1].Parameters.Add("remarks", order.Remarks);
            commands[1].Parameters.Add("orderid", order.Orderid);

            commands[2] = new NpgsqlCommand(SQLConstants.update_customer_by_customer_id);
            commands[2].Parameters.Add("dfirstname", customer.Dfirstname);
            commands[2].Parameters.Add("dmiddlename", customer.Dmiddlename);
            commands[2].Parameters.Add("dlastname", customer.Dlastname);
            commands[2].Parameters.Add("dinitialname", customer.Dinitialname);
            commands[2].Parameters.Add("customerid", customer.Customerid);
            int i = 3;
            string str_value = "";
            foreach (OrderLine orderline in orderedItems)
            {
                commands[i] = new NpgsqlCommand(SQLConstants.insert_into_orederline);

                str_value = orderline.Unitprice.ToString().Replace(',', '.');// unitprice.ToString().Replace(',', '.');
                commands[i].Parameters.Add("unitprice", str_value);
                str_value = orderline.Discountpc.ToString().Replace(',', '.');
                commands[i].Parameters.Add("discountpc", str_value);
                str_value = orderline.Quantity.ToString().Replace(',', '.');
                commands[i].Parameters.Add("quantity", str_value);
                str_value = orderline.Vatpc.ToString().Replace(',', '.');
                commands[i].Parameters.Add("vatpc", str_value);
                commands[i].Parameters.Add("orderid", orderline.Orderid);
                commands[i].Parameters.Add("articlecode", orderline.Articlecode);
                i++;
            }

            return DataAccessHelper.GetInstance().ExecuteTransaction(commands, ref msg);

        }
        #endregion

        #region Invoice

        public static string UpdateStatusAsSentAndGetInvalidIDs(string invoiceNrs)
        {
            NpgsqlCommand command = new NpgsqlCommand(string.Format(SQLConstants.UPDATE_STATUS_AS_SENT, invoiceNrs));
            return DataAccessHelper.GetInstance().GetDataTable(command).Rows[0][0].ToString();
        }
        
        public static Invoice GetInvoiceByInvoiceId(string InvoiceId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Invoice_By_Invoice_Id);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("InvoiceId", InvoiceId);
                DataTable dtInvoice = DataAccessHelper.GetInstance().GetDataTable(command);
                Invoice invoice = new Invoice();
                if (dtInvoice != null && dtInvoice.Rows.Count > 0)
                {
                    invoice.address = dtInvoice.Rows[0]["address"].ToString();
                    invoice.Invoicedate = DateTime.Parse(Boeijenga.Common.Utils.Function.HandleNull(dtInvoice.Rows[0]["invoicedate"].ToString(), 0));
                    invoice.Country = dtInvoice.Rows[0]["country"].ToString();
                    invoice.Credit = Int32.Parse(Boeijenga.Common.Utils.Function.HandleNull( dtInvoice.Rows[0]["credit"].ToString(),1));
                    invoice.Customer = Int32.Parse(dtInvoice.Rows[0]["customer"].ToString());
                    invoice.Customerbtwnr = dtInvoice.Rows[0]["customerbtwnr"].ToString();
                    invoice.Housenr = dtInvoice.Rows[0]["housenr"].ToString();
                    invoice.Invoiceid = Int32.Parse(dtInvoice.Rows[0]["invoiceid"].ToString());
                    invoice.Invoicestatus = dtInvoice.Rows[0]["invoicestatus"].ToString();
                    invoice.Postcode = dtInvoice.Rows[0]["postcode"].ToString();
                    invoice.Remarks = dtInvoice.Rows[0]["remarks"].ToString();
                    invoice.Residence = dtInvoice.Rows[0]["residence"].ToString();
                    invoice.Transferedon = DateTime.Parse(Boeijenga.Common.Utils.Function.HandleNull(dtInvoice.Rows[0]["transferedon"].ToString(),0));
                }
                return invoice;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        public static bool UpdateInvoice(Invoice invoice, List<OrderLine> orderedItems,bool isCredited, int num, ref string msg)
        {
            msg = string.Empty;

            NpgsqlCommand[] commands = new NpgsqlCommand[num];

            commands[0] = new NpgsqlCommand(SQLConstants.Update_Invoice_By_Invoice_Id);
            commands[0].Parameters.Add("InvoiceId", invoice.Invoiceid.ToString());

            commands[0].Parameters.Add("customerbtwnr", invoice.Customerbtwnr);
            commands[0].Parameters.Add("housenr", invoice.Housenr);
            commands[0].Parameters.Add("address", invoice.address);
            commands[0].Parameters.Add("postcode", invoice.Postcode);
            commands[0].Parameters.Add("residence", invoice.Residence);
            commands[0].Parameters.Add("country", invoice.Country);
            commands[0].Parameters.Add("invoicedate", invoice.Invoicedate.ToString("yyyy-MM-dd"));
            commands[0].Parameters.Add("invoicestatus", invoice.Invoicestatus);

            int i = 1; string updateSql = string.Empty;       
            foreach (OrderLine orderline in orderedItems)
            {
                updateSql = "update ordersline set unitprice=:unitprice,vatpc=:vatpc,";
                commands[i] = new NpgsqlCommand();
                if (isCredited.Equals(false))
                {
                    updateSql += "quantity=:quantity";
                    commands[i].Parameters.Add("quantity", orderline.Quantity);
                }
                else
                {
                    updateSql += "creditedquantity=:creditedquantity";
                    commands[i].Parameters.Add("creditedquantity", orderline.Creditedquantity);
                }
                updateSql += " where orderid='" + orderline.Orderid + "' and articlecode='" + orderline.Articlecode + "'";
                commands[i].CommandText = updateSql;

                commands[i].Parameters.Add("unitprice", orderline.Unitprice);
               
                commands[i].Parameters.Add("vatpc", orderline.Vatpc);
                i++;
            }

            return DataAccessHelper.GetInstance().ExecuteTransaction(commands, ref msg);

        }

        public static DataTable GetInvoiceReportByInvoiceStatus(string status, object sortexpression, object sortdirection)
        {
            string sql = "";
            switch (status)
            { 

                case "2":
                    sql = SQLConstants.Get_Invoice_Report_By_InvoiceStatus_Equal_2;
                    break;
                case "1":
                    sql = SQLConstants.Get_Invoice_Report_By_InvoiceStatus_Equal_1;
                    break;
                case "3":
                    sql = SQLConstants.Get_Invoice_Report_By_InvoiceStatus_Equal_3;
                    break;
                case "4":	//Select Alles
                    sql = SQLConstants.Get_Invoice_Report_By_InvoiceStatus_Equal_4;
                    break;
                default: break;
            }

            if (sortexpression != null)
            {
                sql += " order by " + sortexpression.ToString() + " " + sortdirection.ToString();
            }
            else
                sql += " order by i.invoiceid desc";


            NpgsqlCommand command = new NpgsqlCommand(sql);
            command.CommandType = CommandType.Text;
            return DataAccessHelper.GetInstance().GetDataTable(command);

        }

        #endregion

        #region Customer

        public static Customer GetCustomerByCustomerId(int CustomerId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Customer_By_Customer_Id);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("CustomerId", CustomerId);
                DataTable dtCustomer = DataAccessHelper.GetInstance().GetDataTable(command);
                Customer customer = new Customer();
                if (dtCustomer != null && dtCustomer.Rows.Count > 0)
                {
                    customer.Companyname = dtCustomer.Rows[0]["companyname"].ToString();
                    customer.Customerid = Int32.Parse(dtCustomer.Rows[0]["customerid"].ToString());
                    customer.Address = dtCustomer.Rows[0]["address"].ToString();
                    customer.DeliveryAddress = new DeliveryAddress();
                    customer.DeliveryAddress.Daddress = dtCustomer.Rows[0]["daddress"].ToString();
                    customer.DeliveryAddress.Dcountry = dtCustomer.Rows[0]["dcountry"].ToString();
                    customer.DeliveryAddress.Dhousenr = dtCustomer.Rows[0]["dhousenr"].ToString();
                    customer.DeliveryAddress.Dpostcode = dtCustomer.Rows[0]["dpostcode"].ToString();
                    customer.DeliveryAddress.Dresidence = dtCustomer.Rows[0]["dresidence"].ToString();
                    customer.Dfirstname = dtCustomer.Rows[0]["dfirstname"].ToString();
                    customer.Dinitialname = dtCustomer.Rows[0]["dinitialname"].ToString();
                    customer.Discountpc = double.Parse(dtCustomer.Rows[0]["discountpc"].ToString());
                    customer.Dlastname = dtCustomer.Rows[0]["dlastname"].ToString();
                    customer.Dmiddlename = dtCustomer.Rows[0]["dmiddlename"].ToString();
                    customer.Email = dtCustomer.Rows[0]["email"].ToString();
                    customer.Fax = dtCustomer.Rows[0]["fax"].ToString();
                    customer.Firstname = dtCustomer.Rows[0]["firstname"].ToString();
                    customer.Initialname = dtCustomer.Rows[0]["initialname"].ToString();
                    customer.Lastname = dtCustomer.Rows[0]["lastname"].ToString();
                    customer.Middlename = dtCustomer.Rows[0]["middlename"].ToString();
                    customer.Password = dtCustomer.Rows[0]["password"].ToString();
                    customer.Role = Int32.Parse(dtCustomer.Rows[0]["role"].ToString());
                    customer.Telephone = dtCustomer.Rows[0]["telephone"].ToString();
                    customer.Vatnr = dtCustomer.Rows[0]["vatnr"].ToString();
                    customer.Website = dtCustomer.Rows[0]["website"].ToString();
                    customer.Residence = dtCustomer.Rows[0]["residence"].ToString();
                    customer.HouseNumber = dtCustomer.Rows[0]["housenr"].ToString();
                    customer.ZipCode = dtCustomer.Rows[0]["postcode"].ToString();
                    customer.Country = dtCustomer.Rows[0]["country"].ToString();
                    

                }
                return customer;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void DeleteOrderlineByArticleCode(string articlecode)
        {
            try
            {              
              DataAccessHelper.GetInstance().ExecuteQuery(SQLConstants.delete_orderline_by_articleCode.Replace(":ArticleCode","'" + articlecode + "'"));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void CallSPDeletedCreditInvoice(string invoice, string arguments)
        {
            try
            {
                DataAccessHelper.GetInstance().ExecuteQuery(new NpgsqlCommand("SELECT proc_deletecreditinvoice(" + invoice + ",array[" + arguments.TrimEnd(',') + "])"));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void UpdateOrderByOrderIdandOrderStatus(string[] orderfordelete, int looporder)
        {
            try
            {
                string sql = string.Empty;
                for (int z = 0; z < (looporder / 2); z++)
                {
                    sql = "update orders set orderstatus='2' where orderid='" + orderfordelete[z] + "'";                 

                    DataAccessHelper.GetInstance().ExecuteQuery(sql);
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void UpdateInvoiceByDateandInvoiceNo(string date, string invoiceno)
        {
            try
            {
                DataAccessHelper.GetInstance().ExecuteQuery("UPDATE invoice SET printed='" + date + "' WHERE invoicenr=" + invoiceno);              
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void UpdateInvoiceByInvoiceId(string invoiceId)
        {
            try
            {
                DataAccessHelper.GetInstance().ExecuteQuery("update invoice set invoicestatus='2' where invoiceid='" + invoiceId + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void DeleteFromInvoicebyInvoiceId(string invoiceid)
        {
            try
            {
                DataAccessHelper.GetInstance().ExecuteQuery("delete from invoice where invoiceid='" + invoiceid + "'");               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void DeleteFromInvoiceLinebyInvoiceId(string invoiceid)
        {
            try
            {
                DataAccessHelper.GetInstance().ExecuteQuery("delete from invoiceline where invoiceid='" + invoiceid + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool CallSPInsertCreditInvoice(string invoiceno, string arguments, ref string msg)
        {
            try
            {
                return DataAccessHelper.GetInstance().ExecuteQuery(new NpgsqlCommand("SELECT proc_insertcreditinvoice(" + invoiceno + ",array[" + arguments.TrimEnd(',') + "])"),ref msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Supply Order

        public static DataTable GetSupplier()
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_SUpplier);
                command.CommandType = CommandType.Text;
               // command.Parameters.Add("OrderId", OrderId);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static DataTable GetSupplierAddressByPublisherId(string PublisherId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Supplier_Address_By_PublisherId);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("PublisherId", PublisherId);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static DataTable GetSupplyOrdersBySupplyOrderId(string SupplyOrderId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_SupplyOrders_By_SupplyOrderId);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("SupplyOrderId", SupplyOrderId);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static DataTable GetOrdersLineByOrderId(string OrderNo)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_OrdersLine_By_OrderId);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("OrderNo", OrderNo);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetOrdersForSupplyOrderByOrderId(string OrderNo)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Orders_For_SupplyOrder_By_OrderId);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("OrderNo", OrderNo);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static DataTable GetOrdersLine()
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Orders_Line);
                command.CommandType = CommandType.Text;
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static DataTable GetArticleForSupplyOrder()
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Article_For_SupplyOrder);
                command.CommandType = CommandType.Text;
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetMaxSupplyOrders()
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Max_Supply_Orders);
                command.CommandType = CommandType.Text;
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetOrdersLineByArticleCode(string ArticleCode)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_OrdersLine_By_ArticleCode);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("ArticleCode", ArticleCode);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static DataTable GetEmailAddressFromPublisherByPublisherId(string PublisherId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_OrdersLine_By_ArticleCode);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("PublisherId", PublisherId);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
       


        public static bool SaveSupplyOrder(SupplyOrder supplyorder, DataTable dt, object orderno, int num, ref string msg)
        {
            msg = string.Empty; int value = 0;

            NpgsqlCommand[] command = new NpgsqlCommand[num + 1];
            if (orderno == null)
            {
                command[0] = new NpgsqlCommand(SQLConstants.insert_into_supplyorders);
            }
            else
            {
                command[0] = new NpgsqlCommand(SQLConstants.update_into_supplyorders_by_orderno);
                command[0].Parameters.Add("OrderNo", orderno.ToString());
            }
            command[0].Parameters.Add("dhousenr", supplyorder.Dhousenr);
            command[0].Parameters.Add("daddress", supplyorder.Daddress);
            command[0].Parameters.Add("dpostcode", supplyorder.Dpostcode);
            command[0].Parameters.Add("dresidence", supplyorder.Dresidence);
            command[0].Parameters.Add("dcountry", supplyorder.Dcountry);
            command[0].Parameters.Add("supplyorderid", supplyorder.Supplyorderid);
            command[0].Parameters.Add("supplyorderdate", supplyorder.Supplyorderdate);
            command[0].Parameters.Add("supplierid", supplyorder.Supplierid);
            command[0].Parameters.Add("deliverydate", supplyorder.Deliverydate);
            command[0].Parameters.Add("supplyorder_by", supplyorder.Supplyorder_by);
            command[0].Parameters.Add("receivingstatus", supplyorder.Receivingstatus);
            command[0].Parameters.Add("paymentstatus", supplyorder.Paymentstatus);


            if (orderno != null)
            {
                value = 1;
                string deleteQuery = @"delete from supplyordersline where supplyorderid = '" + orderno.ToString() + "'";
                command[value] = new NpgsqlCommand(deleteQuery);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                command[value + i + 1] = new NpgsqlCommand(SQLConstants.insert_into_supplyordersline);
                //if (Request.Params["orderNo"] == null)
                //{
                command[value + i + 1].Parameters.Add("supplyorderid", supplyorder.Supplyorderid);
                //}
                command[value + i + 1].Parameters.Add("articlecode", dt.Rows[i]["articlecode"].ToString());
                command[value + i + 1].Parameters.Add("unitprice", dt.Rows[i]["price"].ToString().Replace(',', '.'));
                command[value + i + 1].Parameters.Add("vatpc", dt.Rows[i]["vat"].ToString().Replace(',', '.'));
                command[value + i + 1].Parameters.Add("orderqty", dt.Rows[i]["qty"].ToString());
                command[value + i + 1].Parameters.Add("supplierArticlecodeID", dt.Rows[i]["SupplyArticleID"].ToString());


                //command[0].Parameters.Add("receiveqty", );
            }

            return DataAccessHelper.GetInstance().ExecuteTransaction(command, ref msg);

        }



        #endregion
        #region Receive Order

        public static DataTable GetMaxReceiveOrders()
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Max_ReceiveOrders);
                command.CommandType = CommandType.Text;
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static DataTable GetReceiveDetailsBySupplyOrder(string SupplyOrder)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_ReceiveDetails_By_SupplyOrder);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("SupplyOrder", SupplyOrder);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetSupplyOrderbySupplyOrderId(string SupplyOrder)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_SupplyOrder_by_SupplyOrderId);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("SupplyOrder", SupplyOrder);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool SaveReceiveOrder(ReceiveOrder objReceiveOrder, List<ReceiveOrderLine> receiveorderlines, ArrayList PrevRecvQty, ArrayList OrderQty, string supplyorder, int num, ref string msg)
        {
            int index = 1,i=0;
            Double orderedQty = 0.00;
            Double receivedQty = 0.00;
            string sql = string.Empty;
            NpgsqlCommand[] commands = new NpgsqlCommand[num];
            commands[0] = new NpgsqlCommand(SQLConstants.insert_into_receiveorders);
            commands[0].Parameters.Add("receiveid", objReceiveOrder.Receiveid.ToString());
            commands[0].Parameters.Add("supplyorderid", objReceiveOrder.Supplyorderid.ToString());            
            commands[0].Parameters.Add("receivedate", objReceiveOrder.Receivedate);
            commands[0].Parameters.Add("shippingcost", objReceiveOrder.Shippingcost);
            commands[0].Parameters.Add("remarks", objReceiveOrder.Remarks);
            commands[0].Parameters.Add("received_by", objReceiveOrder.Received_by);


            foreach (ReceiveOrderLine objReceiveOrderLine in receiveorderlines)
            {
                Double recvQty = 0.0;
                if (objReceiveOrderLine.Receiveqty != 0) //if no quantity received then don't insert into DB
                {
                    commands[index] = new NpgsqlCommand(SQLConstants.insert_into_receiveordersline);
                
                    commands[index].Parameters.Add("receiveid", objReceiveOrderLine.Receiveid);
                    commands[index].Parameters.Add("articlecode", objReceiveOrderLine.Articlecode);
                    commands[index].Parameters.Add("purchaseprice", objReceiveOrderLine.Purchaseprice);
                    commands[index].Parameters.Add("receiveqty", objReceiveOrderLine.Receiveqty);
                    index++;
                    //calculate total received quantity

                    recvQty = 0;
                    recvQty = Double.Parse(PrevRecvQty[i].ToString()) + Double.Parse(objReceiveOrderLine.Receiveqty.ToString());
                   
                    //calculate wheather received qty fulfil ordered qty


                    sql = "update supplyordersline set receiveqty=:receiveqty where supplyorderid=:supplyorderid and articlecode=:articlecode";
                    commands[index] = new NpgsqlCommand(sql);
                    commands[index].Parameters.Add("receiveqty", recvQty);
                    commands[index].Parameters.Add("supplyorderid", objReceiveOrder.Supplyorderid);
                    commands[index].Parameters.Add("articlecode", objReceiveOrderLine.Articlecode);
                    index++;

                    sql = "update article set quantity=(quantity+:quantity) where articlecode=:articlecode";
                    commands[index] = new NpgsqlCommand(sql);
                    commands[index].Parameters.Add("quantity", objReceiveOrderLine.Receiveqty);
                    commands[index].Parameters.Add("articlecode", objReceiveOrderLine.Articlecode);
                    index++;
                }
                receivedQty += Double.Parse(PrevRecvQty[i].ToString()) + Double.Parse(objReceiveOrderLine.Receiveqty.ToString());
                orderedQty += Double.Parse(OrderQty[i].ToString());
                i++;
            }

            string rcvStatus = "";
            if (receivedQty == 0)
                rcvStatus = "N";
            else if (receivedQty > 0 && receivedQty < orderedQty)
                rcvStatus = "P";
            else if (receivedQty >= orderedQty)
                rcvStatus = "F";

            sql = "update supplyorders set receivingstatus=:receivingstatus where supplyorderid=:supplyorderid";
            commands[index] = new NpgsqlCommand(sql);
            commands[index].Parameters.Add("receivingstatus", rcvStatus);
            commands[index].Parameters.Add("supplyorderid", supplyorder);

            return DataAccessHelper.GetInstance().ExecuteTransaction(commands, ref msg);
        }








        public static DataTable GetRceiveDeatilsFromSupplyordersbySupplyOrderId(string SupplyOrderId)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_RceiveDeatils_From_Supplyorders_by_SupplyOrderId);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("SupplyOrderId", SupplyOrderId);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Delete From Table
        public static void DeleteFromTable(string TableName, string FilterString)
        {
            try
            {
                string query = SQLConstants.DELETE_FROM_TABLE.Replace(":tableName", TableName);
                query = query.Replace(":filterString", FilterString);

                NpgsqlCommand command = new NpgsqlCommand(query);

                DataAccessHelper.GetInstance().ExecuteQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Stock Management
        public static void UpdatePaymentStatus(string orderNrs, string status)
        {
            NpgsqlCommand command = new NpgsqlCommand(string.Format(SQLConstants.UPDATE_PAYMENT_STATUS, orderNrs));
            command.Parameters.Add("paymentstatus",status);
            DataAccessHelper.GetInstance().ExecuteQuery(command);
        }

        #endregion

        #region Article
        public static DataTable GetCategoryInfo()
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Category);
                command.CommandType = CommandType.Text;
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public static DataTable GetInstrumentationInfo()
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Instrumentation);
                command.CommandType = CommandType.Text;
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetArticleType()
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_ArticleType);
                command.CommandType = CommandType.Text;
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public static DataTable GetAritcleByArticleCode(string ArticleCode)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Aritcle_By_ArticleCode);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("ArticleCode", ArticleCode);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static List<Composer> GetComposer()
        {
            List<Composer> composers = new List<Composer>();
            string sql = SQLConstants.GET_COMPOSER;
            DataTable dtComposer = DataAccessHelper.GetInstance().GetDataTable(new NpgsqlCommand(sql));
            if (dtComposer.Rows.Count > 0)
            {
                foreach (DataRow row in dtComposer.Rows)
                {
                    if (string.Compare(row["composername"].ToString(), string.Empty) != 0)
                    {
                        Composer composer = new Composer();
                        composer.Composerid = Int32.Parse(row["composerid"].ToString());
                        composer.Firstname = string.Empty;
                        composer.composername = row["composername"].ToString();
                        composer.Middlename = string.Empty;
                        composer.Lastname = string.Empty;
                        composer.Country = string.Empty;
                        composer.Dob = string.Empty;
                        composer.Dod = string.Empty;
                        composers.Add(composer);
                    }
                }
            }
            return composers;
        }


        public static List<Publisher> GetPublisher()
        {
            List<Publisher> publishers = new List<Publisher>();
            string sql = SQLConstants.GET_PUBLISHER;
            DataTable dtPublisher = DataAccessHelper.GetInstance().GetDataTable(new NpgsqlCommand(sql));
            if (dtPublisher.Rows.Count > 0)
            {
                foreach (DataRow row in dtPublisher.Rows)
                {
                    Publisher publisher = new Publisher();
                    publisher.Publisherid = Int32.Parse(row["publisherid"].ToString());                   
                    publisher.Publishername = row["firstname"].ToString() + " " + row["middlename"].ToString()+ " " + row["lastname"].ToString();
                    publishers.Add(publisher);
                }
            }
            return publishers;
        }



        public static List<Grade> GetGradebyCulture(string culturename)
        {
            List<Grade> grades = new List<Grade>();
            string sql = SQLConstants.Get_Grade_by_Culture;
            DataTable dtGrade = DataAccessHelper.GetInstance().GetDataTable(new NpgsqlCommand(sql));
            if (dtGrade.Rows.Count > 0)
            {
                foreach (DataRow row in dtGrade.Rows)
                {
                    Grade grade = new Grade();
                    grade.Gradeid = row["gradeid"].ToString();
                    if (string.Compare(culturename, "en-US") == 0)
                    {
                        grade.Gradenameen = row["gradenameen"].ToString();
                    }
                    else if (string.Compare(culturename, "nl-NL") == 0)
                    {
                        grade.Gradenamenl = row["gradenamenl"].ToString();
                    }

                    grades.Add(grade);
                }
            }
            return grades;
        }



        public static List<Period> GetPeriodyCulture(string culturename)
        {
            List<Period> periodes = new List<Period>();
            string sql = SQLConstants.Get_Period_by_Culture;
            DataTable dtPeriod = DataAccessHelper.GetInstance().GetDataTable(new NpgsqlCommand(sql));
            if (dtPeriod.Rows.Count > 0)
            {
                foreach (DataRow row in dtPeriod.Rows)
                {
                    Period period = new Period();
                    period.Periodid = row["periodid"].ToString();
                    if (string.Compare(culturename, "en-US") == 0)
                    {
                        period.Periodsen = row["periodsen"].ToString();
                    }
                    else if (string.Compare(culturename, "nl-NL") == 0)
                    {
                        period.Periodsnl = row["periodsnl"].ToString();
                    }

                    periodes.Add(period);
                }
            }
            return periodes;
        }


        public static List<SubCategory> GetSubCategory(string culturename)
        {
            List<SubCategory> subCategories = new List<SubCategory>();
            string sql = SQLConstants.Get_SubCategory_by_Culture;
            DataTable dtsubCategorie = DataAccessHelper.GetInstance().GetDataTable(new NpgsqlCommand(sql));
            if (dtsubCategorie.Rows.Count > 0)
            {
                foreach (DataRow row in dtsubCategorie.Rows)
                {
                    SubCategory subCategorie = new SubCategory();
                    subCategorie.Subcategoryid = row["subcategoryid"].ToString();
                    if (string.Compare(culturename, "en-US") == 0)
                    {
                        subCategorie.Subcategorynameen = row["subcategorynameen"].ToString();
                    }
                    else if (string.Compare(culturename, "nl-NL") == 0)
                    {
                        subCategorie.Subcategorynamenl = row["subcategorynamenl"].ToString();
                    }

                    subCategories.Add(subCategorie);
                }
            }
            return subCategories;
        }


        public static List<Category> GetCategories(string culturename)
        {
            List<Category> categories = new List<Category>();
            string sql = SQLConstants.Get_Category_by_Culture;
            DataTable dtCategorie = DataAccessHelper.GetInstance().GetDataTable(new NpgsqlCommand(sql));
            if (dtCategorie.Rows.Count > 0)
            {
                foreach (DataRow row in dtCategorie.Rows)
                {
                    Category categorie = new Category();
                    categorie.Categoryid = row["categoryid"].ToString();
                    if (string.Compare(culturename, "en-US") == 0)
                    {
                        categorie.Categorynameen = row["categorynameen"].ToString();
                    }
                    else if (string.Compare(culturename, "nl-NL") == 0)
                    {
                        categorie.Categorynamenl = row["categorynamenl"].ToString();
                    }

                    categories.Add(categorie);
                }
            }
            return categories;
        }





        public static bool AddPublisher(Publisher publisher, ref string msg)
        {
            msg = string.Empty;

            NpgsqlCommand commands = new NpgsqlCommand(SQLConstants.insert_into_publisher);

           // commands[0] = new NpgsqlCommand(SQLConstants.insert_into_publisher);
            commands.Parameters.Add("firstname", publisher.Firstname);
            commands.Parameters.Add("middlename", publisher.Middlename);
            commands.Parameters.Add("lastname", publisher.Lastname);
            commands.Parameters.Add("initialname", publisher.Initialname);
            commands.Parameters.Add("housenr", publisher.address.Housenr);
            commands.Parameters.Add("address", publisher.address.address);
            commands.Parameters.Add("postcode", publisher.address.Postcode);
            commands.Parameters.Add("residence", publisher.address.Residence);
            commands.Parameters.Add("country", publisher.address.Country);
            commands.Parameters.Add("email", publisher.Email);

            commands.Parameters.Add("website", publisher.Website);
            commands.Parameters.Add("telephone", publisher.Telephone);
            commands.Parameters.Add("fax", publisher.Fax);
            commands.Parameters.Add("companyname", publisher.Companyname);
            commands.Parameters.Add("ispublisher", publisher.Ispublisher);

            return DataAccessHelper.GetInstance().ExecuteQuery(commands, ref msg);

        }




        public static bool AddArticle(Article article, ref string msg)
        {
            msg = string.Empty;

            NpgsqlCommand commands = new NpgsqlCommand(SQLConstants.insert_into_article);

            // commands[0] = new NpgsqlCommand(SQLConstants.insert_into_publisher);
            commands.Parameters.Add("articlecode", article.Articlecode);
            commands.Parameters.Add("descriptionen", article.Descriptionen);
            commands.Parameters.Add("title", article.Title);
            commands.Parameters.Add("subtitle", article.Subtitle);
            commands.Parameters.Add("composer", article.Composer);
            commands.Parameters.Add("serie", article.Serie);
            commands.Parameters.Add("grade", article.Grade);
            commands.Parameters.Add("editor", article.Editor);
            commands.Parameters.Add("subcategory", article.Subcategory);
            commands.Parameters.Add("events", article.Events);

            commands.Parameters.Add("publisher", article.Publisher);
            commands.Parameters.Add("country", article.Country);
            commands.Parameters.Add("price", article.Price);
            commands.Parameters.Add("editionno", article.Editionno);
            commands.Parameters.Add("publicationno", article.Publicationno);

            commands.Parameters.Add("pages", article.Pages);
            commands.Parameters.Add("publishdate", article.Publishdate);
            commands.Parameters.Add("duration", article.Duration);
            commands.Parameters.Add("ismn", article.Ismn);
            commands.Parameters.Add("isbn10", article.Isbn10);
            commands.Parameters.Add("isbn13", article.Isbn13);
            commands.Parameters.Add("articletype", article.Articletype.ToString());
            commands.Parameters.Add("quantity", article.Quantity);
            commands.Parameters.Add("imagefile", article.Imagefile);
            commands.Parameters.Add("pdffile", article.Pdffile);

            commands.Parameters.Add("purchaseprice", article.Purchaseprice);
            commands.Parameters.Add("descriptionnl", article.Descriptionnl);

            commands.Parameters.Add("category", article.Category);
            commands.Parameters.Add("period", article.Period);

            commands.Parameters.Add("isactive", article.Isactive);
            commands.Parameters.Add("price_bak", article.Price_bak);
            commands.Parameters.Add("containsmusic", article.Containsmusic);
            commands.Parameters.Add("keywords", article.Keywords);
            commands.Parameters.Add("instrumentation", article.Instrumentation);
           



            return DataAccessHelper.GetInstance().ExecuteQuery(commands, ref msg);

        }

        public static bool UpdateArticle(Article article, ref string msg)
        {
            msg = string.Empty;

            NpgsqlCommand commands = new NpgsqlCommand(SQLConstants.update_into_article);

            // commands[0] = new NpgsqlCommand(SQLConstants.insert_into_publisher);
            commands.Parameters.Add("ArticleCode", article.Articlecode);
            commands.Parameters.Add("descriptionen", article.Descriptionen);
            commands.Parameters.Add("title", article.Title);
            commands.Parameters.Add("subtitle", article.Subtitle);
            commands.Parameters.Add("composer", article.Composer);
            commands.Parameters.Add("serie", article.Serie);
            commands.Parameters.Add("grade", article.Grade);
            commands.Parameters.Add("editor", article.Editor);
            commands.Parameters.Add("subcategory", article.Subcategory);
            commands.Parameters.Add("events", article.Events);

            commands.Parameters.Add("publisher", article.Publisher);
            commands.Parameters.Add("country", article.Country);
            commands.Parameters.Add("price", article.Price);
            commands.Parameters.Add("editionno", article.Editionno);
            commands.Parameters.Add("publicationno", article.Publicationno);

            commands.Parameters.Add("pages", article.Pages);
            commands.Parameters.Add("publishdate", article.Publishdate);
            commands.Parameters.Add("duration", article.Duration);
            commands.Parameters.Add("ismn", article.Ismn);
            commands.Parameters.Add("isbn10", article.Isbn10);
            commands.Parameters.Add("isbn13", article.Isbn13);
            commands.Parameters.Add("articletype", article.Articletype.ToString());
            commands.Parameters.Add("quantity", article.Quantity);
            commands.Parameters.Add("imagefile", article.Imagefile);
            commands.Parameters.Add("pdffile", article.Pdffile);

            commands.Parameters.Add("purchaseprice", article.Purchaseprice);
            commands.Parameters.Add("descriptionnl", article.Descriptionnl);

            commands.Parameters.Add("category", article.Category);
            commands.Parameters.Add("period", article.Period);

            commands.Parameters.Add("isactive", article.Isactive);
            commands.Parameters.Add("price_bak", article.Price_bak);
            commands.Parameters.Add("containsmusic", article.Containsmusic);
            commands.Parameters.Add("keywords", article.Keywords);
            commands.Parameters.Add("instrumentation", article.Instrumentation);




            return DataAccessHelper.GetInstance().ExecuteQuery(commands, ref msg);

        }


        public static bool AddComposer(Composer composer, ref string msg)
        {
            msg = string.Empty;

            NpgsqlCommand commands = new NpgsqlCommand(SQLConstants.insert_into_composer);

            commands.Parameters.Add("firstname", composer.Firstname);
            commands.Parameters.Add("middlename", composer.Middlename);
            commands.Parameters.Add("lastname", composer.Lastname);           
            commands.Parameters.Add("country", composer.Country);
            commands.Parameters.Add("dob", composer.Dob);
            commands.Parameters.Add("dod", composer.Dod);

            return DataAccessHelper.GetInstance().ExecuteQuery(commands, ref msg);

        }

        public static DataTable LoadForeignKeyDisplayColumn(string TableName)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Load_FKDisplay_Column);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("TableName", TableName);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetPrimaryKey(string TableName)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Primary_Key);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("TableName", TableName);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetColumn(string TableName, string ColumnName, string Tab, string Prefix)
        {
            try
            {

                NpgsqlCommand command;
                if (TableName.ToLower().Equals("category"))
                {
                    command = new NpgsqlCommand(SQLConstants.Get_Column_CategoryTable);
                }
                else
                {
                    command = new NpgsqlCommand(SQLConstants.Get_Column_Table);
                }
                command.CommandType = CommandType.Text;
                command.Parameters.Add("ColumnName", ColumnName);
                command.Parameters.Add("Tab", Tab);
                command.Parameters.Add("Prefix", Prefix);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static DataTable GetPrimaryKeyByTable(string Tab)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Primary_Key_ByTable);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("Tab", Tab);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static DataTable GetMaxIdFromCoslumn(string ColumnName, string Tab)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_MaxId_FromCoslumn);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("ColumnName", ColumnName);
                command.Parameters.Add("Tab", Tab);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetColumnNames(string Tab)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Column_Names);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("Tab", Tab);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetColumnCaptionByTableName(string TableName)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_ColumnCaption_ByTableName);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("TableName", TableName);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static DataTable GetForeignKeys(string TableName)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Foreign_Keys);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("TableName", TableName);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public static DataTable GetColumnBySQL(string sql)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(sql);
                command.CommandType = CommandType.Text;               
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable UpdateTableByTableName(string TableName)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Update_Table_By_TableName);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("TableName", TableName);
                command.Parameters.Add("tableName", TableName.ToLower());
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static DataTable GetDataTypeByTableName(string TableName)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_DataType_By_TableName);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("TableName", TableName);                
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetArticleReferenceTableBySql(string sql)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(sql);
                command.CommandType = CommandType.Text;               
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataRecord GetReferenceTableBySql(string sql, long offset, int limit)
        {
            try
            {
                DataRecord recordSet = new DataRecord();
                NpgsqlCommand command = new NpgsqlCommand(sql + " offset " + offset + " limit " + limit);
                recordSet.Table = DataAccessHelper.GetInstance().GetDataTable(command);
                if (sql.ToLower().Contains("order by"))
                {
                    sql = "Select count(*) " + sql.Substring(sql.ToLower().IndexOf("from"), sql.ToLower().IndexOf("order by") - sql.ToLower().IndexOf("from"));
                }
                else
                {
                    sql = "Select count(*) " + sql.Substring(sql.ToLower().IndexOf("from"));
                }
                NpgsqlCommand command2 = new NpgsqlCommand(sql);
                recordSet.Count = long.Parse( DataAccessHelper.GetInstance().GetDataTable(command2).Rows[0][0].ToString());
                return recordSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetLookupDropDownColumn(string TableName)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Lookup_DropDownColumn);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("TableName", TableName);
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static DataTable GetDataByTableAndColumnName(string TableName, string ColumnName)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Data_ByTableAndColumnName);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("TableName", TableName);
                command.Parameters.Add("ColumnName", ColumnName);
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetAvailableFieldsbyTableName(string TableName)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(SQLConstants.Get_Available_Fields_by_TableName);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("TableName", TableName);               
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        

        public static bool ExecuteInsertCommand(NpgsqlCommand commands, ref string msg)
        {
            msg = string.Empty;
            return DataAccessHelper.GetInstance().ExecuteQuery(commands, ref msg);
        }
        public static bool ExecuteUpdateCommand(NpgsqlCommand commands, ref string msg)
        {
            msg = string.Empty;
            return DataAccessHelper.GetInstance().ExecuteQuery(commands, ref msg);
        }

        public static DataTable GetDataTableBySQL(string sql)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(sql);
                command.CommandType = CommandType.Text;
                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static DataTable GetReportByFromAndTo(string From, string To, bool Isdt1, bool Isdt2, bool Isdt3)
        {
            try
            {
                NpgsqlCommand command = null;

                if (Isdt1)
                {
                    command = new NpgsqlCommand(SQLConstants.Get_Report_By_FromAndTo_dt1);
                }
                else if (Isdt2)
                {
                    command = new NpgsqlCommand(SQLConstants.Get_Report_By_FromAndTo_dt2);
                }
                else if (Isdt3)
                {
                    command = new NpgsqlCommand(SQLConstants.Get_Report_By_FromAndTo_dt3);
                }

                command.CommandType = CommandType.Text;
                command.Parameters.Add("From", From);
                command.Parameters.Add("To", To);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetReportByFromAndToForSalesAnalysis(string From, string To,string Limit, bool Isdt1, bool Isdt2, bool Isdt3)
        {
            try
            {
                NpgsqlCommand command =null;

                if (Isdt1)
                {
                    command = new NpgsqlCommand(SQLConstants.Get_ReportByFromAndTo_For_SalesAnalysis_dt1);
                }
                else if (Isdt2)
                {
                    command = new NpgsqlCommand(SQLConstants.Get_ReportByFromAndTo_For_SalesAnalysis_dt2);
                }
                else if (Isdt3)
                {
                    command = new NpgsqlCommand(SQLConstants.Get_ReportByFromAndTo_For_SalesAnalysis_dt3);
                }

                command.CommandType = CommandType.Text;

                command.Parameters.Add("From", From);
                command.Parameters.Add("To", To);
                command.Parameters.Add("Limit", Limit);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public static DataTable GetVatAnalysis(string From, string To)
        {
            try
            {
                NpgsqlCommand command;
                command = new NpgsqlCommand(SQLConstants.Get_Vat_Analysis);
            
                command.CommandType = CommandType.Text;
                command.Parameters.Add("From", From);
                command.Parameters.Add("To", To);

                return DataAccessHelper.GetInstance().GetDataTable(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
      
        public static bool ExecuteTransactionArraylistCommand(ArrayList commands, ref string msg)
        {
            msg = string.Empty;
            return DataAccessHelper.GetInstance().ExecuteTransaction(commands, ref msg);
        }

        #endregion


        public static User CheckLogIn(string userName, string password)
        {
            int customerid=0;
            User user = null;
            string loginQuery = SQLConstants.Login_SQL;
            NpgsqlCommand command = new NpgsqlCommand(loginQuery);
            command.Parameters.Add("email", userName);
            command.Parameters.Add("password", password);

            DataTable dtArticle = DataAccessHelper.GetInstance().GetDataTable(command);//if successfully executed then
            if (dtArticle.Rows.Count == 1)
            {
                customerid = int.Parse(dtArticle.Rows[0]["customerid"].ToString());//get the customer id
                string firstName = dtArticle.Rows[0]["firstName"].ToString();
                string middleName = dtArticle.Rows[0]["middlename"].ToString();
                string lastName = dtArticle.Rows[0]["lastname"].ToString();
                string fullName = firstName + " " + middleName + " " + lastName;

                // codes goes here for session register
                 user = new User(customerid, userName);//calling User constructor
                user.Name = fullName;
                user.FirstName = firstName;


            }
           
            return user;
        }


        public static MainProduct GetSheetMusicOrgaanMainProducts ()
        {
            MainProduct mainProduct = new MainProduct();
            string mainProductQuery = SQLConstants.SHEETMUSIC_ORGAAN_MAINPRODUCTS;
            NpgsqlCommand command = new NpgsqlCommand(mainProductQuery);

            DataTable dtProduct = DataAccessHelper.GetInstance().GetDataTable(command);//if successfully executed then
            if (dtProduct.Rows.Count == 1)
            {
                mainProduct.ProductCategory = "Sheetmusic Orgaan";
                mainProduct.ArticleCode = dtProduct.Rows[0]["articlecode"].ToString();
                mainProduct.Title = dtProduct.Rows[0]["title"].ToString();
                mainProduct.ImageFile = dtProduct.Rows[0]["imagefile"].ToString();


            }
            return mainProduct;
        }

        public static MainProduct GetSheetMusicOtherMainProducts()
        {
            MainProduct mainProduct = new MainProduct();
            string mainProductQuery = SQLConstants.SHEETMUSIC_OTHER_MAINPRODUCTS;
            NpgsqlCommand command = new NpgsqlCommand(mainProductQuery);

            DataTable dtProduct = DataAccessHelper.GetInstance().GetDataTable(command);//if successfully executed then
            if (dtProduct.Rows.Count == 1)
            {
                mainProduct.ProductCategory = "Sheetmusic Other";
                mainProduct.ArticleCode = dtProduct.Rows[0]["articlecode"].ToString();
                mainProduct.Title = dtProduct.Rows[0]["title"].ToString();
                mainProduct.ImageFile = dtProduct.Rows[0]["imagefile"].ToString();


            }
            return mainProduct;
        }


        public static MainProduct GetBookMainProducts()
        {
            MainProduct mainProduct = new MainProduct();
            string mainProductQuery = SQLConstants.BOOK_MAINPRODUCTS;
            NpgsqlCommand command = new NpgsqlCommand(mainProductQuery);

            DataTable dtProduct = DataAccessHelper.GetInstance().GetDataTable(command);//if successfully executed then
            if (dtProduct.Rows.Count == 1)
            {
                mainProduct.ProductCategory = "Books";
                mainProduct.ArticleCode = dtProduct.Rows[0]["articlecode"].ToString();
                mainProduct.Title = dtProduct.Rows[0]["title"].ToString();
                mainProduct.ImageFile = dtProduct.Rows[0]["imagefile"].ToString();


            }
            return mainProduct;
        }


        public static MainProduct GetCDDVDMainProducts()
        {
            MainProduct mainProduct = new MainProduct();
            string mainProductQuery = SQLConstants.CDDVD_MAINPRODUCTS;
            NpgsqlCommand command = new NpgsqlCommand(mainProductQuery);

            DataTable dtProduct = DataAccessHelper.GetInstance().GetDataTable(command);//if successfully executed then
            if (dtProduct.Rows.Count == 1)
            {
                mainProduct.ProductCategory = "CD Dvd";
                mainProduct.ArticleCode = dtProduct.Rows[0]["articlecode"].ToString();
                mainProduct.Title = dtProduct.Rows[0]["title"].ToString();

                mainProduct.ImageFile = dtProduct.Rows[0]["imagefile"].ToString();
            }
            return mainProduct;
        }


        public static List<SearchKeyword> GetSearchKeyWords(string searchText)
        {
            List<SearchKeyword> keywords = new List<SearchKeyword>();



            string[] tempSearchKeyList = searchText.Split(' ');

            StringBuilder whereClauseBuilder = new StringBuilder();
            string whereCluase = "";
            string orderCluase = "";

            for (int i = 0; i < tempSearchKeyList.Length; i++)
            {
                string key = tempSearchKeyList[i];

                if (i > 0)
                {
                    whereCluase += String.Format(" and hitsoundsas( keyname,{0} ) >0", "'" + key + "'");
                    orderCluase += String.Format(" ,hitsoundsas( keyname,{0}) desc", "'" + key + "'");
                }
                else
                {

                    whereCluase += String.Format(" hitsoundsas( keyname,{0} ) >0", "'" + key + "'");
                    orderCluase += String.Format(" hitsoundsas( keyname,{0}) desc", "'" + key + "'");

                }



            }



            NpgsqlCommand command = new NpgsqlCommand(String.Format(SQLConstants.GET_SEARCH_KEY_WORDS, whereCluase, orderCluase));

            DataTable keyTable = DataAccessHelper.GetInstance().GetDataTable(command);
            foreach (DataRow dataRow in keyTable.Rows)
            {
                SearchKeyword keyWord = new SearchKeyword();
                keyWord.KeyName = dataRow["keyname"].ToString();
                keywords.Add(keyWord);

            }

            return keywords;
        }

        public static Order LoadOrderInfo(string articleCode, Order order)
        {
            string sqlQuey =string.Format(SQLConstants.ORDER_INFO, articleCode.Trim());

            NpgsqlCommand command = new NpgsqlCommand(sqlQuey);
            DataTable dtArticle = DataAccessHelper.GetInstance().GetDataTable(command);
            order.productType = dtArticle.Rows[0]["articletype"].ToString();
            order.productdescription = dtArticle.Rows[0]["title"].ToString();
            order.subtitle = dtArticle.Rows[0]["subtitle"].ToString();
            order.price = Double.Parse(dtArticle.Rows[0]["price"].ToString());
            order.vatIncludedPrice = Double.Parse(dtArticle.Rows[0]["vatincludedprice"].ToString());
            order.vatpc = Double.Parse(dtArticle.Rows[0]["vatpc"].ToString());
            order.deliveryTime = dtArticle.Rows[0]["deliverytime"].ToString();
            return order;
        }

        public static string GetPublisherName(string articleCode)
        {
            string sqlQuey =String.Format(  SQLConstants.PUBLISHER_NAME , articleCode);
            NpgsqlCommand command = new NpgsqlCommand(sqlQuey);

            DataTable dtArticle = DataAccessHelper.GetInstance().GetDataTable(command);
            if (dtArticle.Rows.Count > 0)
                return dtArticle.Rows[0]["publisher"].ToString();
            else
                return "";
        }

        public static Country GetCountry(string countryName)
        {
            Country country = new Country();
            string sqlQuey = String.Format(SQLConstants.GET_COUNTRY_BY_COUNTRYNAME, countryName);
            NpgsqlCommand command = new NpgsqlCommand(sqlQuey);
            command.Parameters.Add("countryname", countryName);

            DataTable dtCountry = DataAccessHelper.GetInstance().GetDataTable(command);

            if(dtCountry.Rows.Count > 0)
            {

                country.CountryCode = dtCountry.Rows[0]["countrycode"].ToString();
                country.CountryName = dtCountry.Rows[0]["countryname"].ToString();
                country.IsEU = dtCountry.Rows[0]["iseu"].ToString();
           
            }

            return country;
            
        }


        public static Country GetCountryByCountryCode(string countryCode)
        {
            Country country = new Country();
            string sqlQuey = String.Format(SQLConstants.GET_COUNTRY_BY_COUNTRYCODE, countryCode);
            NpgsqlCommand command = new NpgsqlCommand(sqlQuey);
            command.Parameters.Add("countrycode", countryCode);

            DataTable dtCountry = DataAccessHelper.GetInstance().GetDataTable(command);

            if (dtCountry.Rows.Count > 0)
            {

                country.CountryCode = dtCountry.Rows[0]["countrycode"].ToString();
                country.CountryName = dtCountry.Rows[0]["countryname"].ToString();
                country.IsEU = dtCountry.Rows[0]["iseu"].ToString();
              
            }

            return country;

        }
 
    
    public static double GetShippingCost(string countryCode,string articleType,int quantity)
    {
        double shippingCost = 0;
        NpgsqlCommand command = new NpgsqlCommand(SQLConstants.GET_SHIPPINGCOST);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("p_countrycode", countryCode);
        command.Parameters.Add("p_article_type", articleType);
        command.Parameters.Add("p_quantity", quantity);
        DataTable dtShippingCost = DataAccessHelper.GetInstance().GetDataTable(command);
        if (dtShippingCost.Rows.Count > 0)
        {

         shippingCost=  double.Parse( dtShippingCost.Rows[0][0].ToString());
        }

        return shippingCost;
    }
    public static double GetShippingCostWithVat(string countryCode, string articleType, int quantity)
    {
        double shippingCost = 0;
        NpgsqlCommand command = new NpgsqlCommand(SQLConstants.GET_SHIPPINGCOST_WITH_VAT);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("p_countrycode", countryCode);
        command.Parameters.Add("p_article_type", articleType);
        command.Parameters.Add("p_quantity", quantity);
        DataTable dtShippingCost = DataAccessHelper.GetInstance().GetDataTable(command);
        if (dtShippingCost.Rows.Count > 0)
        {

            shippingCost = double.Parse(dtShippingCost.Rows[0][0].ToString());
        }

        return shippingCost;
    }


        public static DataTable GetPaymentsTypes()
        {
            NpgsqlCommand command = new NpgsqlCommand(SQLConstants.GET_PAYMENT_TYPES);
            return DataAccessHelper.GetInstance().GetDataTable(command);
        }
    }

}
