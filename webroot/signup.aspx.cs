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
using Boeijenga.Common.Objects;
using Npgsql;

public partial class signup : BasePage
{
    DbHandler dbHandler = new DbHandler();
    DbHandler handler = new DbHandler();
    DataTable dtcusomer = new DataTable();
    ArrayList visitPageList;
    ArrayList cartTable;
    string cultureName = "";
    string message = "";
    string clickHere = "";
    string agree = "";
    string errorMessage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtPassword.Attributes.Add("onkeypress", "return clickButton(event,'" + btnLogin.ClientID + "')");
            txtUserName.Attributes.Add("onkeypress", "return clickButton(event,'" + btnLogin.ClientID + "')");
           
            LoadCountryName();
        }
        if (Session["user"] != null)
        {
            Response.Redirect("delivery.aspx");
        }
       
        Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);

        setVisitPageList(Functions.GetPageName(Request.Url.ToString()));
        //GetVisitedDepth();
        SetCulture();            
        SetCulturalValue();
       
        //txtUserName.Attributes.Add("onkeypress", "return clickButton(event,'" + btnLogin.ClientID + "')");
        //btnLogin.Attributes.Add("onkeypress", "return clickButton(event,'" + btnLogin.ClientID + "')");
      
    }


    public void LoadCountryName()
    {
        string sqlQuery = "select countrycode, countryname from country order by countryname";
        DataTable dtCountry = handler.GetDataTable(sqlQuery);
        ddlCountry.DataSource = dtCountry;
        ddlDCountry.DataSource = dtCountry;
        ddlCountry.DataValueField = dtCountry.Columns["countrycode"].ToString();
        ddlDCountry.DataValueField = dtCountry.Columns["countrycode"].ToString();
        ddlCountry.DataTextField = dtCountry.Columns["countryname"].ToString();
        ddlDCountry.DataTextField = dtCountry.Columns["countryname"].ToString();
        ddlCountry.DataBind();
        ddlDCountry.DataBind();
        ddlCountry.SelectedValue = "NL";
        ddlDCountry.SelectedValue = "NL";

    }

    private void SetCulturalValue()
    {
		//header.Style.Add("background-image", "url(graphics/" + (string)base.GetGlobalResourceObject("string", "headerSignup") + ")");  
		//btnLogin.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnLogin");
        //btnClear.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnClear");

        RequiredFieldValidator1.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        RequiredFieldValidator2.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        RegularExpressionValidator1.ErrorMessage = (string)base.GetGlobalResourceObject("string", "emailValidMessage");
        lblCustomerAlready.Text = (string)base.GetGlobalResourceObject("string", "header_login");
        //lblCustomerNotYet.Text = (string)base.GetGlobalResourceObject("string", "lblCustomerNotYet");
        //lblFillUserName.Text = (string)base.GetGlobalResourceObject("string", "lblFillUserName");
        lblUserName.Text = (string)base.GetGlobalResourceObject("string", "lblUserName");
        lblPassword.Text = (string)base.GetGlobalResourceObject("string", "lblPassword");
        lblPassword1.Text = (string)base.GetGlobalResourceObject("string", "lblPassword");
        clickHere = (string)base.GetGlobalResourceObject("string", "notACustomermessage");
        message = (string)base.GetGlobalResourceObject("string", "lblErrorMessage");
        string [] arrClickHere = clickHere.Split('#');
        //lblClickHere.Text = arrClickHere[0];
        string[] str = arrClickHere[1].Split(' ');
        //lblClickHere.Text += " " + "<a href = 'register.aspx'>" + str[1] + "</a> ";
        //lblClickHere.Text += arrClickHere[2];


        //register 

        //header.Style.Add("background-image", "url(graphics/" + (string)base.GetGlobalResourceObject("string", headerText) + ")");
        btnLogin.Text = (string)base.GetGlobalResourceObject("string", "header_login");
       // btnSubmit.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnSubmit");
        valInvalidEmail.ErrorMessage = (string)base.GetGlobalResourceObject("string", "emailValidMessage");
        valAddress.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        valCountry.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        valEmail.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        valFName.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        valHousenr.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        valPostCode.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        valLName.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        valPassword.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        valResidence.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");

        lblInitialName.Text = (string)base.GetGlobalResourceObject("string", "lblInitialName");
        lblFirstName.Text = (string)base.GetGlobalResourceObject("string", "lblFirstName");
        lblMName.Text = (string)base.GetGlobalResourceObject("string", "lblMName");
        lblLastName.Text = (string)base.GetGlobalResourceObject("string", "lblLastName");
        lblCompany.Text = (string)base.GetGlobalResourceObject("string", "lblCompany");
        lblHouse.Text = (string)base.GetGlobalResourceObject("string", "lblHouse");
        lblAddress.Text = (string)base.GetGlobalResourceObject("string", "lblAddress");
        lblPostCode.Text = (string)base.GetGlobalResourceObject("string", "lblPostCode");
        lblResidence.Text = (string)base.GetGlobalResourceObject("string", "lblResidence");
        lblCountry.Text = (string)base.GetGlobalResourceObject("string", "lblCountry");
        lblTelephone.Text = (string)base.GetGlobalResourceObject("string", "lblTelephone");
        lblFax.Text = (string)base.GetGlobalResourceObject("string", "lblFax");
        lblEMail.Text = (string)base.GetGlobalResourceObject("string", "lblEMail");
        lblPassword.Text = (string)base.GetGlobalResourceObject("string", "lblPassword");
        lblRePassword.Text = (string)base.GetGlobalResourceObject("string", "lblRePassword");
        lblWebsite.Text = (string)base.GetGlobalResourceObject("string", "lblWebsite");
        errorMessage = (string)base.GetGlobalResourceObject("string", "subscribeFailureMassage");
        agree = (string)base.GetGlobalResourceObject("string", "agree");
        //misMatchPassword = (string)base.GetGlobalResourceObject("string", "misMatchPassword");
        //lblCurrentPage.Text = (string)base.GetGlobalResourceObject("string", "currentpage");
        //lblDifferentDelivery.Text = (string)base.GetGlobalResourceObject("string", "changeDelivery");
        //lblDifferentDelivery.Text = (string)base.GetGlobalResourceObject("string", "changeDelivery");
        chkDelivery.Text = (string)base.GetGlobalResourceObject("string", "changeDelivery");
        //Label1.Text = (string)base.GetGlobalResourceObject("string", "lblInitialName");
        //Label2.Text = (string)base.GetGlobalResourceObject("string", "lblFirstName");
        Label3.Text = (string)base.GetGlobalResourceObject("string", "lblMName");
        Label4.Text = (string)base.GetGlobalResourceObject("string", "lblLastName");
        Label5.Text = (string)base.GetGlobalResourceObject("string", "lblHouse");
        Label6.Text = (string)base.GetGlobalResourceObject("string", "lblAddress");
        Label7.Text = (string)base.GetGlobalResourceObject("string", "lblPostCode");
        Label8.Text = (string)base.GetGlobalResourceObject("string", "lblResidence");
        Label9.Text = (string)base.GetGlobalResourceObject("string", "lblCountry");
        chkAgree.Text = (string)base.GetGlobalResourceObject("string", "chkAgree");

        //steps

        lblHeader.Text = "2) " + (string)base.GetGlobalResourceObject("string", "steplogin");
        lblBasket.Text = "1) " + (string)base.GetGlobalResourceObject("string", "basket");
        lblLogReg.Text = "2) " + (string)base.GetGlobalResourceObject("string", "steplogin");
        lblDelAddress.Text = "3) " + (string)base.GetGlobalResourceObject("string", "stepDelivaery");
        lblPayment.Text = "4) " + (string)base.GetGlobalResourceObject("string", "stepPayment");
        lblOrderComplete.Text = "5) " + (string)base.GetGlobalResourceObject("string", "stepComplete");

    }

    //private void GetVisitedDepth()
    //{
    //    lblPageRoot.Text = func.getVisitedPage(visitPageList);
    //    lblActivePage.Text = func.getActivePage(visitPageList);
    //}
    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {        
        InitializeComponent();
        base.OnInit(e);
    }
   
    private void InitializeComponent()
    {
       // this.btnupdate.Click += new System.Web.UI.ImageClickEventHandler(this.btnupdate_click);
       // this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_click);
    }
    #endregion	
 
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
        lblErrorMessage.Text = "";//print the success message            
        if (Functions.IsValidMail(txtUserName.Text))
        {
            string loginQuery = "SELECT customerid,firstname,middlename,lastname,vatnr FROM customer WHERE email = '" + txtUserName.Text + "' AND password =  '" + txtPassword.Text + "'";
            DbHandler handler = new DbHandler();
            DataTable dtArticle = handler.GetDataTable(loginQuery);//if successfully executed then
            if (handler.HasRecord(dtArticle))
            {
                int customerid = int.Parse(dtArticle.Rows[0]["customerid"].ToString());//get the customer id
                string firstName = dtArticle.Rows[0]["firstName"].ToString();
                string middleName = dtArticle.Rows[0]["middlename"].ToString();
                string lastName = dtArticle.Rows[0]["lastname"].ToString();
                string vatNr = dtArticle.Rows[0]["vatnr"].ToString();
                string fullName = firstName + " " + middleName + " " + lastName;

                // codes goes here for session register
                User user = new User(customerid, txtUserName.Text);//calling User constructor
                user.Name = fullName;
                user.Vatnr = vatNr;
                Session["user"] = user;//setting the session of user
                Response.Redirect("delivery.aspx");

            }
            else
            {
                lblErrorMessage.Text = message;//print the error message
            }
        }
    }
    protected void btnClear_Click(object sender, ImageClickEventArgs e)
    {
        txtUserName.Text = "";
        txtPassword.Text = "";
    }

    private bool CheckEmptyFields()
    {
        if (txtAddress.Text == "" || txtAddress.Text == null) return false;
        else if (txtEmail.Text == "" || txtEmail.Text == null) return false;
        else if (txtFName.Text == "" || txtFName.Text == null) return false;
        else if (txtHousenr.Text == "" || txtHousenr.Text == null) return false;
        else if (txtLName.Text == "" || txtLName.Text == null) return false;
        //else if (txtMName.Text == "" || txtMName.Text == null) return false;
        else if (txtRegPassword.Text == "" || txtPassword.Text == null) return false;
        else if (txtPostCode.Text == "" || txtPostCode.Text == null) return false;
        else if (txtResidence.Text == "" || txtResidence.Text == null) return false;
        else if (chkAgree.Checked == false)
        {
            lblErrorMessage.Text = agree;
            return false;
        }
        else if (!Functions.IsValidMail(txtEmail.Text)) return false;
        else if (txtRePassword.Text != txtRePassword.Text)
        {
            //lblError.Text = misMatchPassword;
            return false;
        }
        else
            return true;
    }
    public bool getQuery(string query)
    {

        string sqlQuery = "";
        if (query.Equals("insert"))
        {
            sqlQuery = @"insert into customer(firstname,middlename,lastname,companyname,housenr,address,postcode,residence,country,email ,dfirstname, dmiddlename, dlastname, dinitialname,dhousenr,daddress ,dpostcode, dresidence, dcountry,password,initialname,website,telephone,fax) 
                         values(:firstname,:middlename,:lastname,:companyname,:housenr,:address,:postcode,:residence,:country,:email ,:dfirstname, :dmiddlename, :dlastname, :dinitialname,:dhousenr,:daddress ,:dpostcode,:dresidence, :dcountry,:password,:initialname,:website,:telephone,:fax)";
        }
        else
        {
            if (Session["user"] != null)
            {
                User user = (User)Session["user"];
                string uid = user.ID.ToString();
                sqlQuery = @"update customer set firstname=:firstname,middlename=:middlename,
                            lastname=:lastname,companyname=:companyname,housenr=:housenr,address=:address,postcode=:postcode,
                            residence=:residence,country=:country,email=:email ,dfirstname=:dfirstname, 
                            dmiddlename=:dmiddlename, dlastname=:dlastname, dinitialname=:dinitialname,
                            dhousenr=:dhousenr,daddress=:daddress ,dpostcode=:dpostcode,dresidence=:dresidence,
                            dcountry=:dcountry,password=:password,initialname=:initialname,website=:website,
                            telephone=:telephone,fax=:fax
                            where customerid='" + uid + "'";
            }
        }

        NpgsqlCommand command = new NpgsqlCommand(sqlQuery);

        if (chkDelivery.Checked == false)
        {
            txtDAddress.Text = txtAddress.Text;
            txtDFName.Text = txtFName.Text;
            txtDMName.Text = txtMName.Text;
            txtDLName.Text = txtLName.Text;
            txtDHousenr.Text = txtHousenr.Text;
            txtDPostCode.Text = txtPostCode.Text;
            txtDResidence.Text = txtResidence.Text;
            ddlDCountry.SelectedValue = ddlCountry.SelectedValue.ToString();
           // ddlDInitialName.Value = ddlInitialName.SelectedValue.ToString();
        }
        if (chkDelivery.Checked == false && Session["user"] != null)
        {
            User user = (User)Session["user"];
            string sql = @"select * from customer where customerid='" + user.ID.ToString() + "'";
            dtcusomer = handler.GetDataTable(sql);
            if (dtcusomer.Rows.Count >= 1)
            {
                txtDFName.Text = dtcusomer.Rows[0]["dfirstname"].ToString();
                txtDMName.Text = dtcusomer.Rows[0]["dmiddlename"].ToString();
                txtDLName.Text = dtcusomer.Rows[0]["dlastname"].ToString();
                txtDHousenr.Text = dtcusomer.Rows[0]["dhousenr"].ToString();
                txtDAddress.Text = dtcusomer.Rows[0]["daddress"].ToString();
                txtDPostCode.Text = dtcusomer.Rows[0]["dpostcode"].ToString();
                txtDResidence.Text = dtcusomer.Rows[0]["dresidence"].ToString();
                ddlDCountry.SelectedValue = dtcusomer.Rows[0]["dcountry"].ToString();
                //ddlDInitialName.Value = dtcusomer.Rows[0]["dinitialname"].ToString();
            }
        }
        else
        {

        }
        command.Parameters.Add("firstname", txtFName.Text);
        command.Parameters.Add("middlename", txtMName.Text);
        command.Parameters.Add("lastname", txtLName.Text);
        command.Parameters.Add("dfirstname", txtDFName.Text);
        command.Parameters.Add("dmiddlename", txtDMName.Text);
        command.Parameters.Add("dlastname", txtDLName.Text);
        command.Parameters.Add("companyname", txtCompany.Text);
        command.Parameters.Add("housenr", txtHousenr.Text);
        command.Parameters.Add("address", txtAddress.Text);
        command.Parameters.Add("postcode", txtPostCode.Text);
        command.Parameters.Add("residence", txtResidence.Text);
        command.Parameters.Add("country", ddlCountry.SelectedValue.ToString());
        command.Parameters.Add("telephone", txtTelephone.Text);
        command.Parameters.Add("fax", txtFax.Text);
        command.Parameters.Add("email", txtEmail.Text);
        command.Parameters.Add("dhousenr", txtDHousenr.Text);
        command.Parameters.Add("daddress", txtDAddress.Text);
        command.Parameters.Add("dpostcode", txtDPostCode.Text);
        command.Parameters.Add("dresidence", txtDResidence.Text);
        command.Parameters.Add("dcountry", ddlDCountry.SelectedValue.ToString());
        command.Parameters.Add("password", txtRePassword.Text);
        command.Parameters.Add("initialname", rdoInitialName.SelectedValue.ToString());
        command.Parameters.Add("website", txtWebsite.Text);
        command.Parameters.Add("dinitialname", rdoInitialName.SelectedValue.ToString());

        bool b = handler.ExecuteQuery(command);
        return b;

        


    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (chkDelivery.Checked == true)
        {
            Page.Validate("accValidation");
            if (!Page.IsValid)
            {
                return;
            }
            else
            {
                Page.Validate("delivaryValidation");
                if (!Page.IsValid)
                {
                    return;
                }

            }
        }
        else
        {
            Page.Validate("accValidation");
            if (!Page.IsValid)
            {
                return;
            }
        }

        if (CheckEmptyFields())
        {
            if (Session["user"] == null)
            {
                //insert
                bool b = getQuery("insert");
                if (b == true)
                {
                    User user = new User();
                    string sqlQuery = @"select max(customerid) as id from customer";
                    DataTable dtcusomer1 = handler.GetDataTable(sqlQuery);
                    user.ID = (int)dtcusomer1.Rows[0]["id"];
                    user.Name = txtFName.Text + " " + txtMName.Text + " " + txtLName.Text;
                    user.Email = txtEmail.Text;
                    //				user.Password = txtPassword.Text;
                    Session["user"] = user;
                    string msg = "lblRegisterMessage";
                    Response.Redirect("delivery.aspx");
                }
                else
                {
                    lblErrorMessage.Text = errorMessage;

                }


            }
            else
            {
                bool b = getQuery("update");
                if (b == true) { Response.Redirect("home.aspx"); }
                else {
                    // lblError.Text = errorMessage;
                }

            }

        }
    }
}
