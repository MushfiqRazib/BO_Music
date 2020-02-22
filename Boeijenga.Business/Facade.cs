using System;
using System.Data;
using System.Collections.Generic;
using Boeijenga.Common.Objects;
using Boeijenga.DataAccess;
using System.Collections;
using System.Web;
using Npgsql;

namespace Boeijenga.Business
{
    public class Facade
    {

        public User CheckLogIn(string userName, string password)
        {
            return DataAccessManager.CheckLogIn(userName,password);
        }


        #region Country
        public List<Country> GetCountry()
        {
            return DataAccessManager.GetCountry();
        }

        public Country GetCountry(string countryName)
        {
            return DataAccessManager.GetCountry(countryName);
        }

        public Country GetCountryByCountryCode(string countryCode)
        {
            return DataAccessManager.GetCountryByCountryCode(countryCode);
        }

        #endregion
        #region Invoice
        public DataTable GetInvoiceInfoByOrderId(int OrderId)
        {
            return DataAccessManager.GetInvoiceInfoByOrderId(OrderId);
        }

        public DataTable GetInvoiceInfowithCustomerNameByOrderId(string OrderId)
        {
            return DataAccessManager.GetInvoiceInfowithCustomerNameByOrderId(OrderId);
        }
        #endregion

        #region Customer
        public DataTable GetCustomerInfoByOrderId(int OrderId)
        {
            return DataAccessManager.GetCustomerInfoByOrderId(OrderId);
        }

        public Customer GetCustomerByCustomerId(int CustomerId)
        {
            return DataAccessManager.GetCustomerByCustomerId(CustomerId);
        }
        #endregion

        #region Article
        public DataTable GetArticleByArticleCode(string ArticleCode)
        {
            return DataAccessManager.GetArticleByArticleCode(ArticleCode);
        }

        public DataTable GetArticleInfo()
        {
            return DataAccessManager.GetArticleInfo();
        }
        public DataRecord GetArticleInfo(string orderBy, string dir, long offset, int limit)
        {
            return DataAccessManager.GetArticleInfo(orderBy, dir, offset, limit);
        }

        public DataTable GetArticleCodeInfoByType(char articleType)
        {
            return DataAccessManager.GetArticleCodeInfoByType(articleType);
        }


        #endregion

        #region Report

        public DataTable LoadForeignKeyDisplayColumn(string TableName)
        {
            return DataAccessManager.LoadForeignKeyDisplayColumn(TableName);
        }
        public DataTable GetPrimaryKey(string TableName)
        {
            return DataAccessManager.GetPrimaryKey(TableName);
        }

        public DataTable GetColumn(string tableName, string ColumnName, string Tab, string Prefix)
        {
            return DataAccessManager.GetColumn(tableName, ColumnName, Tab, Prefix);
        }

        public DataTable GetPrimaryKeyByTable(string Tab)
        {
            return DataAccessManager.GetPrimaryKeyByTable(Tab);
        }


        public DataTable GetMaxIdFromColumn(string ColumnName, string Tab)
        {
            return DataAccessManager.GetMaxIdFromCoslumn(ColumnName, Tab);
        }

        public DataTable GetColumnNames(string Tab)
        {
            return DataAccessManager.GetColumnNames(Tab);
        }
        public DataTable GetColumnCaptionByTableName(string TableName)
        {
            return DataAccessManager.GetColumnCaptionByTableName(TableName);
        }
        public DataTable GetColumnBySQL(string sql)
        {
            return DataAccessManager.GetColumnBySQL(sql);
        }

        public DataTable GetForeignKeys(string TableName)
        {
            return DataAccessManager.GetForeignKeys(TableName);
        }
        public DataTable UpdateTableByTableName(string TableName)
        {
            return DataAccessManager.UpdateTableByTableName(TableName);
        }
        public DataTable GetDataTypeByTableName(string TableName)
        {
            return DataAccessManager.GetDataTypeByTableName(TableName);
        }
        public DataTable GetArticleReferenceTableBySql(string sql)
        {
            return DataAccessManager.GetArticleReferenceTableBySql(sql);
        }
        public DataRecord GetReferenceTableBySql(string sql, long offset, int limit)
        {
            return DataAccessManager.GetReferenceTableBySql(sql, offset, limit);
        }
        public DataTable GetLookupDropDownColumn(string TableName)
        {
            return DataAccessManager.GetLookupDropDownColumn(TableName);
        }
        public bool ExecuteInsertCommand(NpgsqlCommand npg,  ref string msg)
        {
            return DataAccessManager.ExecuteInsertCommand(npg, ref msg);
        }
        public bool ExecuteUpdateCommand(NpgsqlCommand npg, ref string msg)
        {
            return DataAccessManager.ExecuteUpdateCommand(npg, ref msg);
        }

        public DataTable GetDataByTableAndColumnName(string TableName, string ColumnName)
        {
            return DataAccessManager.GetDataByTableAndColumnName(TableName, ColumnName);
        }
        public DataTable GetAvailableFieldsbyTableName(string TableName)
        {
            return DataAccessManager.GetAvailableFieldsbyTableName(TableName);
        }
        

        public DataTable GetDataTableBySQL(string sql)
        {
            return DataAccessManager.GetDataTableBySQL(sql);
        }
        public DataTable GetReportByFromAndTo(string from, string to, bool Isdt1, bool Isdt2, bool Isdt3)
        {
            return DataAccessManager.GetReportByFromAndTo(from, to, Isdt1, Isdt2, Isdt3);
        }
        public DataTable GetVatAnalysis(string from, string to)
        {
            return DataAccessManager.GetVatAnalysis(from, to);
        }
        public DataTable GetReportByFromAndToForSalesAnalysis(string from, string to,string limit, bool Isdt1, bool Isdt2, bool Isdt3)
        {
            return DataAccessManager.GetReportByFromAndToForSalesAnalysis(from, to, limit, Isdt1, Isdt2, Isdt3);
        }
        
        #endregion


        #region Order

        public string MakeOrderReadyAndGetInvalidIds(Int32[] orderNrs)
        {
            string invalidOrderIds = string.Empty;
            try
            {
                string orderIds = Boeijenga.Common.Utils.Function.GetCommaSepartedList(orderNrs);
                invalidOrderIds = Boeijenga.DataAccess.DataAccessManager.MakeOrderReadyAndGetInvalidIds(orderIds);
                return invalidOrderIds;
            }
            catch (Exception ex)
            {
                Boeijenga.Common.Utils.LogWriter.Log(ex);
                throw ex;
            }
        }

        public string MakeInvoiceAndGetInvalidIds(Int32[] orderNrs)
        {
            string invalidOrderIds = string.Empty;
            try
            {
                string orderIds = Boeijenga.Common.Utils.Function.GetCommaSepartedList(orderNrs);
                invalidOrderIds = Boeijenga.DataAccess.DataAccessManager.MakeInvoiceAndGetInvalidIds(orderIds);
                return invalidOrderIds;
            }
            catch (Exception ex)
            {
                Boeijenga.Common.Utils.LogWriter.Log(ex);
                throw ex;
            }
        }

        public string DeleteOrdersAndGetInvalidIds(Int32[] orderNrs)
        {
            string invalidOrderIds = string.Empty;
            try
            {
                string orderIds = Boeijenga.Common.Utils.Function.GetCommaSepartedList(orderNrs);
                invalidOrderIds = Boeijenga.DataAccess.DataAccessManager.DeleteOrdersAndGetInvalidIds(orderIds);
                return invalidOrderIds;
            }
            catch (Exception ex)
            {
                Boeijenga.Common.Utils.LogWriter.Log(ex);
                throw ex;
            }
        }

        public DataTable GetOrderInfoWithCalculationByOrderId(int OrderId)
        {
            return DataAccessManager.GetOrderInfoWithCalculationByOrderId(OrderId);
        }

        public OrderDTO GetOrderByOrderId(int OrderId)
        {
            return DataAccessManager.GetOrderByOrderId(OrderId);
        }

        public bool UpdateOrder(OrderDTO order, List<OrderLine> orderedItems, Customer customer, int order_no, ref string msg)
        {
            return DataAccessManager.UpdateOrder(order, orderedItems, customer, order_no, ref msg);
        }
        public DataTable GetDeliveryInfowithCustomerNameByOrderId(string OrderId)
        {
            return DataAccessManager.GetDeliveryInfowithCustomerNameByOrderId(OrderId);
        }

        #endregion

        #region DeleteFromTable
        public void DeleteFromTable(string TableName, string FilterString)
        {
            DataAccessManager.DeleteFromTable(TableName, FilterString);
        }
        #endregion

        #region Pdf Report
        public DataTable GetDiscountAmountandVATAmountByOrderId(string OrderId)
        {
            return DataAccessManager.GetDiscountAmountandVATAmountByOrderId(OrderId);
        }
        #endregion

        #region Invoice

        public DataTable GetInvoiceLineInfoByInvoiceId(string InvoiceId)
        {
            return DataAccessManager.GetInvoiceLineInfoByInvoiceId(InvoiceId);
        }

        public DataTable GetInvoiceLineByInvoiceId(string InvoiceId)
        {
            return DataAccessManager.GetInvoiceLineByInvoiceId(InvoiceId);
        }

        public Invoice GetInvoiceByInvoiceId(string InvoiceId)
        {
            return DataAccessManager.GetInvoiceByInvoiceId(InvoiceId);
        }


        public bool UpdateInvoice(Invoice invoice, List<OrderLine> orderedItems, bool isCredited, int num, ref string msg)
        {
            return DataAccessManager.UpdateInvoice(invoice, orderedItems, isCredited, num, ref msg);
        }

        public void DeleteOrderlineByArticleCode(string articlecode)
        {
            DataAccessManager.DeleteOrderlineByArticleCode(articlecode);
        }

        public void CallSPDeletedCreditInvoice(string invoice, string arguments)
        {
            DataAccessManager.CallSPDeletedCreditInvoice(invoice, arguments);
        }

        public void UpdateOrderByOrderIdandOrderStatus(string[] orderfordelete, int looporder)
        {
            DataAccessManager.UpdateOrderByOrderIdandOrderStatus(orderfordelete, looporder);
        }

        public void DeleteFromInvoicebyInvoiceId(string invoiceid)
        {
            DataAccessManager.DeleteFromInvoicebyInvoiceId(invoiceid);
        }

        public void DeleteFromInvoiceLinebyInvoiceId(string invoiceid)
        {
            DataAccessManager.DeleteFromInvoiceLinebyInvoiceId(invoiceid);
        }

        public bool CallSPInsertCreditInvoice(string invoice, string arguments, ref string msg)
        {
           return DataAccessManager.CallSPInsertCreditInvoice(invoice, arguments, ref msg);
        }
        public DataTable GetInvoiceReportByInvoiceStatus(string status, object sortexpression, object sortdirection)
        {
            return DataAccessManager.GetInvoiceReportByInvoiceStatus(status, sortexpression, sortdirection);
        }

        public void UpdateInvoiceByDateandInvoiceNo(string date, string invoiceno)
        {
            DataAccessManager.UpdateInvoiceByDateandInvoiceNo(date, invoiceno);
        }

        public DataTable GetInvoiceLineReportByInvoiceId(string invoiceid)
        {
            return DataAccessManager.GetInvoiceLineReportByInvoiceId(invoiceid);
        }

        public void UpdateInvoiceByInvoiceId(string invoiceId)
        {
            DataAccessManager.UpdateInvoiceByInvoiceId(invoiceId);
        }

         public string UpdateStatusAsSentAndGetInvalidIDs(Int32[] invoiceNrs)
        {
            string invalidInvoiceIds = string.Empty;
            try
            {
                string invoiceIds = Boeijenga.Common.Utils.Function.GetCommaSepartedList(invoiceNrs);
                invalidInvoiceIds = Boeijenga.DataAccess.DataAccessManager.UpdateStatusAsSentAndGetInvalidIDs(invoiceIds);
                return invalidInvoiceIds;
            }
            catch (Exception ex)
            {
                Boeijenga.Common.Utils.LogWriter.Log(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceNrs"></param>
        /// <param name="culture">en_US/nl-NL</param>
        /// <returns></returns>
         public string PrintInvoice(Int32[] invoiceNrs, string culture)
         {
             
             throw new NotImplementedException();
         }
        #endregion

        #region Supply Order


        
        public DataTable GetOrderIdFromSupplyOrder()
        {
            return DataAccessManager.GetOrderIdFromSupplyOrder();
        }

        public DataTable GetSupplier()
        {
            return DataAccessManager.GetSupplier();
        }
        public DataTable GetSupplierAddressByPublisherId(string publisherId)
        {
            return DataAccessManager.GetSupplierAddressByPublisherId(publisherId);
        }
        public DataTable GetOrdersLine()
        {
            return DataAccessManager.GetOrdersLine();
        }
        public DataTable GetSupplyOrdersBySupplyOrderId(string SupplyOrderId)
        {
            return DataAccessManager.GetSupplyOrdersBySupplyOrderId(SupplyOrderId);
        }
        public DataTable GetOrdersLineByOrderId(string OrderId)
        {
            return DataAccessManager.GetOrdersLineByOrderId(OrderId);
        }
        public DataTable GetOrdersForSupplyOrderByOrderId(string OrderId)
        {
            return DataAccessManager.GetOrdersForSupplyOrderByOrderId(OrderId);
        }
        public DataTable GetArticleForSupplyOrder()
        {
            return DataAccessManager.GetArticleForSupplyOrder();
        }

        public DataTable GetMaxSupplyOrders()
        {
            return DataAccessManager.GetMaxSupplyOrders();
        }

        public DataTable GetOrdersLineByArticleCode(string articlecode)
        {
            return DataAccessManager.GetOrdersLineByArticleCode(articlecode);
        }
        public DataTable GetEmailAddressFromPublisherByPublisherId(string publisherId)
        {
            return DataAccessManager.GetEmailAddressFromPublisherByPublisherId(publisherId);
        }
        

        public bool SaveSupplyOrder(SupplyOrder supplyorder, DataTable dt, object orderno ,int num, ref string msg)
        {
            return DataAccessManager.SaveSupplyOrder(supplyorder, dt, orderno, num, ref msg);
        }
        #endregion

        #region Receive Order
        public DataTable GetRceiveDeatilsFromSupplyordersbySupplyOrderId(string SupplyOrderId)
        {
            return DataAccessManager.GetRceiveDeatilsFromSupplyordersbySupplyOrderId(SupplyOrderId);
        }

        public bool SaveReceiveOrder(ReceiveOrder objReceiveOrder, List<ReceiveOrderLine> receiveorderlines, ArrayList PrevRecvQty, ArrayList OrderQty, string supplyorder, int num, ref string msg)
        {
            return DataAccessManager.SaveReceiveOrder(objReceiveOrder, receiveorderlines, PrevRecvQty, OrderQty, supplyorder, num, ref msg);
        }

        public DataTable GetReceiveDetailsBySupplyOrder(string SupplyOrder)
        {
            return DataAccessManager.GetReceiveDetailsBySupplyOrder(SupplyOrder);
        }

        public DataTable GetMaxReceiveOrders()
        {
            return DataAccessManager.GetMaxReceiveOrders();
        }

        public DataTable GetSupplyOrderbySupplyOrderId(string SupplyOrder)
        {
            return DataAccessManager.GetSupplyOrderbySupplyOrderId(SupplyOrder);
        }
        #endregion


        #region Stock Management
         public void UpdatePaymentStatus(Int32[] orderNrs, int status)
         {
             string orderIds = Boeijenga.Common.Utils.Function.GetCommaSepartedList(orderNrs);
             Boeijenga.DataAccess.DataAccessManager.UpdatePaymentStatus(orderIds, GetPaymentStatusCode(status));
         }

         private static string GetPaymentStatusCode(int status)
         {
             switch (status)
             {
                 case 0: return "F";
                 case 1: return "P";
                 case 2: return "U";
                 default: return "X";
             }
         }

         #endregion


         #region Article
         public DataTable GetCategoryInfo()
         {
             return DataAccessManager.GetCategoryInfo();
         }

         public DataTable GetInstrumentationInfo()
         {
             return DataAccessManager.GetInstrumentationInfo();
         }


         public List<Composer> GetComposer()
         {
             return DataAccessManager.GetComposer();
         }

         public List<Publisher> GetPublisher()
         {
             return DataAccessManager.GetPublisher();
         }

            public  string GetPublisherName(string articleCode)
            {

                return DataAccessManager.GetPublisherName(articleCode);
            }


        public List<Grade> GetGradebyCulture(string name)
         {
             return DataAccessManager.GetGradebyCulture(name);
         }


         public DataTable GetArticleType()
         {
             return DataAccessManager.GetArticleType();
         }
         public DataTable GetAritcleByArticleCode(string articlecode)
         {
             return DataAccessManager.GetAritcleByArticleCode(articlecode);
         }

        

         public List<Period> GetPeriodyCulture(string name)
         {
             return DataAccessManager.GetPeriodyCulture(name);
         }
         public List<SubCategory> GetSubCategory(string name)
         {
             return DataAccessManager.GetSubCategory(name);
         }

         public List<Category> GetCategories(string name)
         {
             return DataAccessManager.GetCategories(name);
         }
        

         public bool AddArticle(Article article, ref string msg)
         {
             return DataAccessManager.AddArticle(article, ref msg);
         }

         public bool UpdateArticle(Article article, ref string msg)
         {
             return DataAccessManager.UpdateArticle(article, ref msg);
         }
         #endregion


        #region Publisher

         public bool AddPublisher(Publisher publisher, ref string msg)
         {
             return DataAccessManager.AddPublisher(publisher, ref msg);
         }
        #endregion


         #region Composer

         public bool AddComposer(Composer composer, ref string msg)
         {
             return DataAccessManager.AddComposer(composer, ref msg);
         }
         #endregion

         public bool ExecuteTransactionArraylistCommand(ArrayList commands, ref string msg)
         {
             return DataAccessManager.ExecuteTransactionArraylistCommand(commands, ref msg);
         }


       


         public  MainProduct GetSheetMusicOrgaanMainProducts()
         {
             return DataAccessManager.GetSheetMusicOrgaanMainProducts();
         }

         public  MainProduct GetSheetMusicOtherMainProducts()
         {
             return DataAccessManager.GetSheetMusicOtherMainProducts();
         }


         public  MainProduct GetBookMainProducts()
         {
             return DataAccessManager.GetBookMainProducts();
         }


         public  MainProduct GetCDDVDMainProducts()
         {
             return DataAccessManager.GetCDDVDMainProducts();
         }


         public List<SearchKeyword> GetSearchKeyWords(string searchText)
         {
             return DataAccessManager.GetSearchKeyWords(searchText);
         }



          public Order LoadOrderInfo(string articleCode, Order order)
          {
              
                       

              return DataAccessManager.LoadOrderInfo(articleCode, order);
              


          }


        public double GetShippingCost(string countryCode,string articleType,int quantity)
        {
            return DataAccessManager.GetShippingCost(countryCode, articleType, quantity);

        }
        public double GetShippingCostWithVat(string countryCode, string articleType, int quantity)
        {
            return DataAccessManager.GetShippingCostWithVat(countryCode, articleType, quantity);

        }


        public DataTable GetPaymentsTypes()
        {
            return DataAccessManager.GetPaymentsTypes();
        }

        public User  LoadUserDeliveryInfo(User user)
        {
                Customer customer = GetCustomerByCustomerId(user.ID);
                user.CompanyName = customer.Companyname;
                user.dAddress = customer.DeliveryAddress.Daddress;
                user.dCountry = customer.DeliveryAddress.Dcountry;
                user.dFirstname = customer.Dfirstname;
                user.dHousenr = customer.DeliveryAddress.Dhousenr;
                user.dInitialName = customer.Dinitialname;
                user.dLastname = customer.Dlastname;
                user.dMiddlename = customer.Dmiddlename;
                user.dPostcode = customer.DeliveryAddress.Dpostcode;
                user.dResidence = customer.DeliveryAddress.Dresidence;
                return user;
          
        }
    }
}
