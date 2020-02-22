using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Boeijenga.Common.Objects;
using Npgsql;

public partial class Login : BasePage
{
    ArrayList cartTable = new ArrayList();
    DbHandler handler = new DbHandler();
    
    ArrayList visitPageList;
    string cultureName = "";
    string incorrectID = "";
    int customerid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);

        setVisitPageList(Functions.GetPageName(Request.Url.ToString()));
        //GetVisitedDepth();
        SetCulture();
        SetCulturalValue();
		if (!IsPostBack)
		{
			SetFocus(txtUserName);
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
        txtPassword.Attributes.Add("onkeypress", "return clickButton(event,'" + btnLogin.ClientID + "')");
        txtUserName.Attributes.Add("onkeypress", "return clickButton(event,'" + btnLogin.ClientID + "')");
    }
      

    //private void GetVisitedDepth()
    //{
    //    lblPageRoot.Text = func.getVisitedPage(visitPageList);
    //    lblActivePage.Text = func.getActivePage(visitPageList);
    //}
    private void SetCulturalValue()
    {
        header.Style.Add("background-image", "url(graphics/" + (string)base.GetGlobalResourceObject("string", "headerLogin") + ")");
        btnLogin.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnLogin");
		btnClear.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnClear");
        RequiredFieldValidator1.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        RequiredFieldValidator2.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        RegularExpressionValidator1.ErrorMessage = (string)base.GetGlobalResourceObject("string", "emailValidMessage");
        lblUserName.Text = (string)base.GetGlobalResourceObject("string", "lblUserName");
        lblPassword.Text = (string)base.GetGlobalResourceObject("string", "lblPassword");
        incorrectID = (string)base.GetGlobalResourceObject("string", "invalidLogin");
        //lblCurrentPage.Text = (string)base.GetGlobalResourceObject("string", "currentpage");
    }

    void Master_MenuButton(object sender, EventArgs e)
    {
        Session["cultureName"] = Master.CurrentButton.ToString();
        SetCulture();
        SetCulturalValue();
        //LoadRecords();
       // SetObjectValue();
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
    protected void setVisitPageList(String pageName)
    {
        if (Session["visitPageList"] != null)
        {
            visitPageList = (ArrayList)Session["visitPageList"];
        }
        else
        {
            visitPageList = new ArrayList();
        }

        Session["visitPageList"] = Functions.initVisitPageList(visitPageList, pageName);

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        labelMessage.Text = "";//print the success message            
        if (Functions.IsValidMail(txtUserName.Text))
        {
            //string loginQuery = "SELECT customerid,firstname,middlename,lastname FROM customer WHERE email = '" + txtUserName.Text.Trim() + "' AND password =  '" + txtPassword.Text.Trim() + "'";
			string loginQuery = "SELECT customerid,firstname,middlename,lastname FROM customer WHERE email =@email AND password =@password";
			NpgsqlCommand comm =new NpgsqlCommand(loginQuery);
			comm.Parameters.Add("email", txtUserName.Text.Trim());
			comm.Parameters.Add("password", txtPassword.Text.Trim());

            DataTable dtArticle = handler.GetDataTable(comm);//if successfully executed then
            if (dtArticle.Rows.Count == 1)
            {
                customerid = int.Parse(dtArticle.Rows[0]["customerid"].ToString());//get the customer id
                string firstName = dtArticle.Rows[0]["firstName"].ToString();
                string middleName = dtArticle.Rows[0]["middlename"].ToString();
                string lastName = dtArticle.Rows[0]["lastname"].ToString();
                string fullName = firstName + " " + middleName + " " + lastName;

                // codes goes here for session register
                User user = new User(customerid, txtUserName.Text);//calling User constructor
                user.Name = fullName;
                Session["user"] = user;//setting the session of user
                Response.Redirect("home.aspx");

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
