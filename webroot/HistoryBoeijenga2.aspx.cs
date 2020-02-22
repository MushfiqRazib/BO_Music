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
using System.Globalization;
using System.Threading;
using bo01;

public partial class HistoryBoeijenga2 : BasePage
{
    ArrayList cartTable = new ArrayList();
    string cultureName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);
        if (!IsPostBack)
        {
            SetCulture();
        }
        if (Session["order"] != null)
        {
            cartTable = (ArrayList)Session["order"];
            if (cartTable.Count != 0)
            {
                Label cartitem = (Label)Master.FindControl("lblCartItem");
                cartitem.Text = "(" + Session["CartItems"].ToString() + ")";
            }
            else
            {
                Label cartitem = (Label)Master.FindControl("lblCartItem");
                cartitem.Text = "";
            }

        }
        SetCulturalValue();
    }

    private void SetCulturalValue()
    {

    }
    private void SetCulture()
    {
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
    void Master_MenuButton(object sender, EventArgs e)
    {
        Session["cultureName"] = Master.CurrentButton.ToString();
        SetCulture();
        SetCulturalValue();
    }
}

