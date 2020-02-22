using System;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;

/// <summary>
/// Summary description for OrderManagementWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class OrderManagementWebService : System.Web.Services.WebService
{

    public OrderManagementWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false)]
    public string MakeOrderReadyAndGetInvalidIds(string[][] orderIds)
    {
        string result = string.Empty;
        Stack<Int32> orderNrs =new Stack<int>(orderIds.Length);
        
        try
        {
            foreach (string[] item in orderIds)
            {
                orderNrs.Push(Int32.Parse(item[0]));
            }
            result = new Boeijenga.Business.Facade().MakeOrderReadyAndGetInvalidIds(orderNrs.ToArray());
        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
            result = string.Format("Order cannot be ready:{0}",ex.Message);
        }
        return result;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false)]
    public string MakeInvoiceAndGetInvalidIds(string[][] orderIds)
    {
        string result = string.Empty;
        Stack<Int32> orderNrs = new Stack<int>(orderIds.Length);

        try
        {
            foreach (string[] item in orderIds)
            {
                orderNrs.Push(Int32.Parse(item[0]));
            }
            result = new Boeijenga.Business.Facade().MakeInvoiceAndGetInvalidIds(orderNrs.ToArray());
        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
            result = string.Format("Order cannot be invoiced:{0}", ex.Message);
        }
        return result;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = false)]
    public string DeleteOrdersAndGetInvalidIds(string[][] orderIds)
    {
        string result = string.Empty;
        Stack<Int32> orderNrs = new Stack<int>(orderIds.Length);

        try
        {
            foreach (string[] item in orderIds)
            {
                orderNrs.Push(Int32.Parse(item[0]));
            }
            result = new Boeijenga.Business.Facade().DeleteOrdersAndGetInvalidIds(orderNrs.ToArray());
        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
            result = string.Format("Order cannot be ready:{0}", ex.Message);
        }
        return result;
    }

}

