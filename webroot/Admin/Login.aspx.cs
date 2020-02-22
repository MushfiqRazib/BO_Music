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
using Npgsql;

public partial class Admin_Login : System.Web.UI.Page
{
	DbHandler handler = new DbHandler();
	
	string incorrectID = "";
	int customerid = 0; 
    protected void Page_Load(object sender, EventArgs e)
    {
		SetMessageValue();
		txtPassword.Attributes.Add("onkeypress", "return clickButton(event,'" + btnLogin.ClientID + "')");
		txtUserName.Attributes.Add("onkeypress", "return clickButton(event,'" + btnLogin.ClientID + "')");
		ClientScript.RegisterStartupScript(typeof(Page),"SetFocus", "<script>document.getElementById('" + txtUserName.ClientID + "').focus();</script>");
    }
	private void SetMessageValue()
	{
			
		RequiredFieldValidator1.ErrorMessage = "Required";
		RequiredFieldValidator2.ErrorMessage = "Required";
		RegularExpressionValidator1.ErrorMessage = "This is not a valid email address";
		incorrectID = "Sorry!! You Should have admin privilege to access this site.";
	}
	protected void btnLogin_Click(object sender, EventArgs e)
	{
		labelMessage.Text = "";//print the success message            
        if (Functions.IsValidMail(txtUserName.Text))
		{
			//string loginQuery = "SELECT customerid,firstname,middlename,lastname FROM customer WHERE email = '" + txtUserName.Text.Trim() + "' AND password =  '" + txtPassword.Text.Trim() + "'";

			NpgsqlCommand com = new NpgsqlCommand("select email,roles.role as role from customer,roles where email=@email and password=@password and roles.roleid=customer.role and customer.role=1");
			com.Parameters.Add("email",txtUserName.Text.Trim());
			com.Parameters.Add("password",txtPassword.Text.Trim());
			DataTable dt=handler.GetDataTable(com);
			if (dt.Rows.Count == 1)
			{
				string role = dt.Rows[0]["role"].ToString();//get the customer id
				FormsAuthentication.RedirectFromLoginPage(role, false);
			}
			else
			{
				labelMessage.Text = incorrectID;//print the error message
			}
		}
	}
	protected void btnClear_Click(object sender, ImageClickEventArgs e)
	{
		txtUserName.Text = "";
		txtPassword.Text = "";
	}
}
