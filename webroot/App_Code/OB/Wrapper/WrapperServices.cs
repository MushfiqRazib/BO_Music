using System;
using System.Web.Services;
using System.Web.Script.Services;
using HIT.OB.STD.Wrapper.BLL;

/// <summary>
/// Summary description for OBWebServices
/// </summary>
namespace HIT.OB.STD.Wrapper.Services
{
    [WebService(Namespace = "http://hawarit.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WrapperServices : System.Web.Services.WebService
    {

        public WrapperServices()
        {
            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
        public string GetReportArguments(string reportCode)
        {
            try
            {
                return WrappingManager.GetReportArguments(reportCode);
            }
            catch (Exception ex)
            {
                return ex.StackTrace;
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
        public string GetReportList(string roleName)
        {
            try
            {
                return WrappingManager.GetReportList(roleName);
            }
            catch (Exception ex)
            {
                return ex.StackTrace;
            }            
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
        public bool UpdateUserDefinedReportSettings(string REPORT_CODE, string SQL_WHERE, string GROUP_BY, string ORDER_BY, string ORDER_BY_DIR, string report_settings)
        {
            bool isUpdated = false;
            try
            {
                //*** decode ecoded single quite(') to char
                REPORT_CODE = Microsoft.JScript.GlobalObject.unescape(REPORT_CODE);
                SQL_WHERE = Microsoft.JScript.GlobalObject.unescape(SQL_WHERE);
                GROUP_BY = Microsoft.JScript.GlobalObject.unescape(GROUP_BY);
                ORDER_BY = Microsoft.JScript.GlobalObject.unescape(ORDER_BY);
                ORDER_BY_DIR = Microsoft.JScript.GlobalObject.unescape(ORDER_BY_DIR);
                report_settings = Microsoft.JScript.GlobalObject.unescape(report_settings);
                isUpdated = WrappingManager.UpdateUserDefinedReportSettings(REPORT_CODE, SQL_WHERE, GROUP_BY, ORDER_BY, ORDER_BY_DIR, report_settings);
            }
            catch (Exception ex)
            {
                return isUpdated;
            }
            return isUpdated;
        }

        [WebMethod(true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
        public string InsertGroupColor(string REPORT_CODE, string GROUP_BY, string GROUP_CODE, string COLOR_CODE)
        {

            try
            {
                //*** decode ecoded single quite(') to char
                //SQL_WHERE = Microsoft.JScript.GlobalObject.unescape(SQL_WHERE);
                bool result = WrappingManager.InsertGroupColor(REPORT_CODE, GROUP_BY, GROUP_CODE, COLOR_CODE);
                string value = string.Empty;
                if (result)
                {
                    value = "{\"result\":\"true\"}";
                }
                else
                {
                    value = "{\"result\":\"false\"}";
                }
                return value;
            }
            catch (Exception ex)
            {
                return ex.StackTrace;
                //return false;
            }
        }


    }
}
