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

public partial class about : BasePage
{
    ArrayList cartTable = new ArrayList();
    string cultureName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);
        if (!IsPostBack)
        {
            SetCulture();
            Master.SetVisitPageList(Functions.GetPageName(Request.Url.ToString()));
        }
      
        SetCulturalValue();
    }

    private void SetCulturalValue()
    {

    }
    private void SetCulture()
    {
        Master.SetCulture();
    }
    void Master_MenuButton(object sender, EventArgs e)
    {
        Session["cultureName"] = Master.CurrentButton.ToString();
        SetCulture();
        SetCulturalValue();
    }
    protected void btnContact_Click(object sender, EventArgs e)
        {
            Response.Redirect("contact.aspx");
        }


      
}
