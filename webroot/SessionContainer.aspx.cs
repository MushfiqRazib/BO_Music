using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Globalization;

namespace bo01
{
    public partial class SessionContainer : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetCulture()
        {
            string cultureName = "";
            if (Session["cultureName"] != null)
            {
                cultureName = Session["cultureName"].ToString();
            }
            else
            {
                cultureName = HttpContext.Current.Request.UserLanguages[0];
                Session["cultureName"] = cultureName;
            }
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
        }
        public void SetCulture(string cultureName)
        {
            Session["cultureName"] = cultureName;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
        }
        public string GetCulture()
        {
            if (Session["cultureName"] != null)
            {
                return Session["cultureName"].ToString();
            }
            else
            {
                string cultureName = HttpContext.Current.Request.UserLanguages[0];
                Session["cultureName"] = cultureName;
                return cultureName;
            }
        }

    }
}