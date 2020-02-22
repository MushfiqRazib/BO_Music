using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bo01;
using Boeijenga.DataAccess;
using Npgsql;

public partial class subscribe_widget : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblEmail.Text = (string)base.GetGlobalResourceObject("string", "lblEmail");

    }

    protected void btnsubscribe_Click(object sender, EventArgs e) //event handler for email subscription
    {
        string mailSuccesMessage = "", mailFailureMessage = "";

        mailSuccesMessage = (string)base.GetGlobalResourceObject("string", "subscribeSuccessMassage");
        mailFailureMessage = (string)base.GetGlobalResourceObject("string", "subscribeFailureMassage");
        //query to insert email address into mailinglist table

        MainLayOut mainLayout = (MainLayOut)(this.Page.Master as MasterPage);

        string temp = "";

        if (Functions.IsValidMail(txtMail.Text))
        {
            string mailSubscribe = @"INSERT INTO mailinglist (email, date)
                                  VALUES('" + txtMail.Text + "', CURRENT_DATE)";





            try
            {
                if (DataAccessHelper.GetInstance().ExecuteQuery(new NpgsqlCommand(mailSubscribe), ref temp))
                //if successfully executed then
                {
                    mainLayout.ShowStatus(mailSuccesMessage, 5000);
                }
            }
            catch (Exception ex)
            {
                // labelMailMessage.Text = mailFailureMessage;//print the error message
                //this.Page.Master.ShowStatus(mailFailureMessage, 5000);

                mainLayout.ShowStatus(mailFailureMessage, 5000);

            }
        }
        else
        {


            mainLayout.ShowStatus((string)base.GetGlobalResourceObject("string", "subscribe_mail_invalidemail"), 5000);


        }

    }


}
