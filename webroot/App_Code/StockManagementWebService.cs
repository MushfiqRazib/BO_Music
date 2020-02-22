using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Script.Services;

/// <summary>
/// Summary description for StockManagementWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class StockManagementWebService : System.Web.Services.WebService
{

    public StockManagementWebService()
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
    public string UpdatePaymentStatus(string[][] invoiceIds, int status)
    {
        string result = string.Empty;
        Stack<Int32> invoiceNrs = new Stack<int>(invoiceIds.Length);

        try
        {
            foreach (string[] item in invoiceIds)
            {
                invoiceNrs.Push(Int32.Parse(item[0]));
            }
            new Boeijenga.Business.Facade().UpdatePaymentStatus(invoiceNrs.ToArray(),status);
        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
            result = string.Format("status cannot update:{0}", ex.Message);
        }
        return result;
    }

}

