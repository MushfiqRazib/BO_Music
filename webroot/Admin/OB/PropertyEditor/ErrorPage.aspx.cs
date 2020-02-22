using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Exception ex = (Exception)Application["Error"];
        if (ex != null)
        {
            string message = "";
            if (ex.InnerException != null)
            {
                message = "' " + ex.InnerException.Message + " !'";
            }
            else
            {
                message = "' " + ex.Message + " !'";
            }
            lblError.Text = message;
            Application.Remove("Error");

        }
    }
}
