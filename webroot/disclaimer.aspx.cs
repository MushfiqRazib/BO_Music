using System;

public partial class disclaimer : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);
        if (!IsPostBack)
        {
            SetCulture();
            SetCulturalValue();
        }
        
    }
    private void SetCulture()
    {
        Master.SetCulture();

    }
    private void SetCulturalValue()
    {
        lblDisclaimer.Text = (string)base.GetGlobalResourceObject("string", "lblDisclaimer");
       
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


    protected void btnsubscribe_Click(object sender, EventArgs e) //event handler for email subscription
    {

        string mailSuccesMessage = "", mailFailureMessage = "";

        mailSuccesMessage = (string)base.GetGlobalResourceObject("string", "subscribeSuccessMassage");
        mailFailureMessage = (string)base.GetGlobalResourceObject("string", "subscribeFailureMassage");
        labelMailMessage.Text = "";//print the success message
        //query to insert email address into mailinglist table
        if (Functions.IsValidMail(txtMail.Text))
        {
            string mailSubscribe =
                @"INSERT INTO mailinglist (email, date)
                                  VALUES('" + txtMail.Text +
                "', CURRENT_DATE)";

            if (new DbHandler().ExecuteQuery(mailSubscribe)) //if successfully executed then
            {
                //labelMailMessage.Text = mailSuccesMessage;//print the success message
                this.Master.ShowStatus(mailSuccesMessage, 5000);
            }
            else
            {
                // labelMailMessage.Text = mailFailureMessage;//print the error message
                this.Master.ShowStatus(mailFailureMessage, 5000);
            }
        }
    }

}
