using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Script.Services;

/// <summary>
/// Summary description for InvoiceManagementWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class InvoiceManagementWebService : System.Web.Services.WebService
{

    public InvoiceManagementWebService()
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
    public string UpdateStatusAsSentAndGetInvalidIDs(string[][] invoiceIds)
    {
        string result = string.Empty;
        Stack<Int32> invoiceNrs = new Stack<int>(invoiceIds.Length);

        try
        {
            foreach (string[] item in invoiceIds)
            {
                invoiceNrs.Push(Int32.Parse(item[0]));
            }
            result = new Boeijenga.Business.Facade().UpdateStatusAsSentAndGetInvalidIDs(invoiceNrs.ToArray());
        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
            result = string.Format("invoice status cannot update as sent:{0}", ex.Message);
        }
        return result;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = false)]
    public string PrintDutchInvoice(string[][] invoiceIds)
    {
        string result = string.Empty;
        Stack<Int32> invoiceNrs = new Stack<int>(invoiceIds.Length);

        try
        {
            foreach (string[] item in invoiceIds)
            {
                invoiceNrs.Push(Int32.Parse(item[0]));
            }
            result = new Boeijenga.Business.Facade().PrintInvoice(invoiceNrs.ToArray(), "nl-NL");
        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
            result = string.Format("invoice status cannot update as sent:{0}", ex.Message);
        }
        return result;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = false)]
    public string PrintEnglishInvoice(string[][] invoiceIds)
    {
        string result = string.Empty;
        Stack<Int32> invoiceNrs = new Stack<int>(invoiceIds.Length);

        try
        {
            foreach (string[] item in invoiceIds)
            {
                invoiceNrs.Push(Int32.Parse(item[0]));
            }
            
            result = new Boeijenga.Business.Facade().PrintInvoice(invoiceNrs.ToArray(), "en-US");
        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
            result = string.Format("invoice status cannot update as sent:{0}", ex.Message);
        }
        return result;
    }
}

