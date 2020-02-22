using System;

public partial class confirmation : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       Master.SetVisitPageList(Functions.GetPageName(Request.Url.ToString()));
        //lblRegisterMessage.Text = (string)base.GetGlobalResourceObject("string", "lblRegisterMessage");
        if (!IsPostBack)
        {
            if (Request.Params["message"] != null)
            {
                lblRegisterMessage.Text = (string)base.GetGlobalResourceObject("string", Request.Params["message"].ToString());
            }
        }
    }
}
