using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class errorpage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Exception ex = (Exception)Application["UnhandledException"];
       
        if (ex != null)
        {
            lblErrorMessage.Text = "' " + ex.Message + " !'";
            Application.Remove("UnhandledException");

        } 
    }
}
