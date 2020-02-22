using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using Boeijenga.Common.Objects;
using Npgsql;

public partial class Register : BasePage
{
	DbHandler handler = new DbHandler();

    ArrayList visitPageList;
    ArrayList cartTable;
    DataTable dtcusomer = new DataTable();
	string cultureName = "";
	string errorMessage="";
    string agree = "";
    string misMatchPassword = "";
	string headerText="";
    string[] initialName = {"Mr.", "Mrs.", "Dhr.", "Mevr."," "};
    protected void Page_Load(object sender, EventArgs e)
    {
		Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);

        setVisitPageList(Functions.GetPageName(Request.Url.ToString()));
        //GetVisitedDepth();
		if (!IsPostBack) 
		{
			SetCulture();
			clearFields();
			LoadCountryName();
			LoadInitialName();
			checkUser();
		}
        
        SetCulturalValue();
        chkDelivery.Attributes.Add("onclick", "javascript:return GetInvoiceAddress(this)");
    }

    protected void setVisitPageList(String pageName)// setting the visit page list
    {
        Master.SetVisitPageList(pageName);
    }
    //private void GetVisitedDepth()
    //{
    //    lblPageRoot.Text = func.getVisitedPage(visitPageList);
    //    lblActivePage.Text = func.getActivePage(visitPageList);
    //}


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
	public void LoadInitialName()
	{
           // ddlInitialName.DataSource = initialName;
            //ddlDInitialName.DataSource = initialName;
           // ddlInitialName.DataBind();
           // ddlDInitialName.DataBind();
        
        //ddlInitialName.Items.Add("Mr. ");
        //ddlInitialName.Items.Add("Mrs. ");
        //ddlInitialName.Items.Add("Dhr. ");
        //ddlInitialName.Items.Add("Mevr. ");
        //ddlDInitialName.Items.Add("Mr. ");
        //ddlDInitialName.Items.Add("Mrs. ");
        //ddlDInitialName.Items.Add("Dhr. ");
        //ddlDInitialName.Items.Add("Mevr. ");
		
	}
	public void checkUser()
	{
		if(Session["user"]!=null)
		{
			User user = (User)Session["user"];
			headerText = "headerChangeProfile";
			showFields(user.ID.ToString());

            reqPasswordValidator.Enabled = false;
            valPassword.Enabled = false;
            chkAgree.Visible = false;
		}
		else
		{
			headerText = "headerRegister";
            valPassword.Enabled = true;
            reqPasswordValidator.Enabled = true;
            chkAgree.Visible = true;
		}
	}
	public void clearFields()
	{
		txtFName.Text =null;
		txtMName.Text = null;
		txtLName.Text = null;
        txtCompany.Text = null;
		txtHousenr.Text = null;
		txtAddress.Text = null;
		txtPostCode.Text = null;
		txtResidence.Text = null;
		txtEmail.Text = null;
		txtHousenr.Text = null;
		txtPassword.Text = null;
		txtWebsite.Text = null;
		lblError.Text="";

	}
	protected void btnSubmit_Click(object sender, EventArgs e)
	{
        if (chkDelivery.Checked)
        {
            Page.Validate("registration_delivery_validate");
            if(! Page.IsValid)
            {

                return;
            }
        }




	    if (CheckEmptyFields())
        {
            if (Session["user"] == null)
            {
                 if (chkAgree.Checked == false)
                {
                    lblError.Text = agree;
                    return ;
                }
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
					Response.Redirect("confirmation.aspx?message=" + msg);
                }
                else
                {
                    lblError.Text = errorMessage;

                }


            }
            else
            {
                bool b = getQuery("update");
                if (b == true) { Response.Redirect("home.aspx"); }
                else { lblError.Text = errorMessage; }

            }

        }
	}
    private bool CheckEmptyFields()
    {
        if (txtAddress.Text == "" || txtAddress.Text == null) return false;
        else if (txtEmail.Text == "" || txtEmail.Text == null) return false;
        else if (txtFName.Text == "" || txtFName.Text == null) return false;
        else if (txtHousenr.Text == "" || txtHousenr.Text == null) return false;
        else if (txtLName.Text == "" || txtLName.Text == null) return false;
        //else if (txtMName.Text == "" || txtMName.Text == null) return false;
        
        else if (txtPostCode.Text == "" || txtPostCode.Text == null) return false;
        else if (txtResidence.Text == "" || txtResidence.Text == null) return false;
       
        else if (!Functions.IsValidMail(txtEmail.Text)) return false;

        if (Session["user"] == null)
        {
            if (txtPassword.Text == "" || txtPassword.Text == null) return false;
            else if (txtPassword.Text != txtRePassword.Text)
            {
                lblError.Text = misMatchPassword;
                return false;
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
            
                if (txtPassword.Text != txtRePassword.Text)
                {
                    lblError.Text = misMatchPassword;
                    return false;
                }
            }

        }

        return true;
    }
	public void showFields(string uid)
	{
		Page.Title="Change Profile";
		string sqlQuery = @"select * from customer where customerid='"+uid +"'";
	    dtcusomer=handler.GetDataTable(sqlQuery); 
		txtFName.Text =dtcusomer.Rows[0]["firstname"].ToString ();
		txtMName.Text =dtcusomer.Rows[0]["middlename"].ToString ();
		txtLName.Text =dtcusomer.Rows[0]["lastname"].ToString ();
        txtCompany.Text = dtcusomer.Rows[0]["companyname"].ToString();
		txtHousenr.Text =dtcusomer.Rows[0]["housenr"].ToString ();
		txtAddress.Text =dtcusomer.Rows[0]["address"].ToString ();
		txtPostCode.Text  =dtcusomer.Rows[0]["postcode"].ToString ();
		txtResidence.Text  =dtcusomer.Rows[0]["residence"].ToString ();
        txtTelephone.Text = dtcusomer.Rows[0]["telephone"].ToString();
        txtFax.Text = dtcusomer.Rows[0]["fax"].ToString();
		txtEmail.Text =dtcusomer.Rows[0]["email"].ToString ();
		txtPassword.Text = dtcusomer.Rows[0]["password"].ToString();
		txtWebsite.Text = dtcusomer.Rows[0]["website"].ToString ();
		ddlCountry.SelectedValue = dtcusomer.Rows[0]["country"].ToString();
		rdoInitialName.SelectedValue = dtcusomer.Rows[0]["initialname"].ToString();
        txtDFName.Text = dtcusomer.Rows[0]["dfirstname"].ToString();
        txtDMName.Text = dtcusomer.Rows[0]["dmiddlename"].ToString();
        txtDLName.Text = dtcusomer.Rows[0]["dlastname"].ToString();
        txtDHousenr.Text = dtcusomer.Rows[0]["dhousenr"].ToString();
        txtDAddress.Text = dtcusomer.Rows[0]["daddress"].ToString();
        txtDPostCode.Text = dtcusomer.Rows[0]["dpostcode"].ToString();
        txtDResidence.Text = dtcusomer.Rows[0]["dresidence"].ToString();                        
        ddlDCountry.SelectedValue = dtcusomer.Rows[0]["dcountry"].ToString();
        rdoDInitialName.SelectedValue = dtcusomer.Rows[0]["dinitialname"].ToString();
        if (chkDelivery.Checked == false)
        {
            chkDelivery.Checked = true;
        }
			
	}
	public bool getQuery(string query)
	{

		string sqlQuery="";
		if(query.Equals("insert"))
		{
            sqlQuery = @"insert into customer(firstname,middlename,lastname,companyname,housenr,address,postcode,residence,country,email ,dfirstname, dmiddlename, dlastname, dinitialname,dhousenr,daddress ,dpostcode, dresidence, dcountry,password,initialname,website,telephone,fax) 
                         values(:firstname,:middlename,:lastname,:companyname,:housenr,:address,:postcode,:residence,:country,:email ,:dfirstname, 
                       :dmiddlename, :dlastname, :dinitialname,:dhousenr,:daddress ,:dpostcode,:dresidence, :dcountry,:password,:initialname,:website,:telephone,:fax)";
		}
		else
		{
			if(Session["user"]!=null)
			{
				User user=(User)Session["user"];
				string uid=user.ID.ToString ();
                sqlQuery = @"update customer set firstname=:firstname,middlename=:middlename,
                            lastname=:lastname,companyname=:companyname,housenr=:housenr,address=:address,postcode=:postcode,
                            residence=:residence,country=:country,email=:email ,dfirstname=:dfirstname, 
                            dmiddlename=:dmiddlename, dlastname=:dlastname, dinitialname=:dinitialname,
                            dhousenr=:dhousenr,daddress=:daddress ,dpostcode=:dpostcode,dresidence=:dresidence,
                            dcountry=:dcountry,{0} initialname=:initialname,website=:website,
                            telephone=:telephone,fax=:fax
                            where customerid='" + uid + "'";

                sqlQuery = String.Format(sqlQuery, (String.IsNullOrEmpty(txtPassword.Text)) ? "" : "password='" + txtPassword.Text.Trim() + "',");
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
                rdoDInitialName.SelectedValue = rdoInitialName.SelectedValue;
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
                    rdoDInitialName.SelectedValue = dtcusomer.Rows[0]["dinitialname"].ToString();
                }
            }
			command.Parameters.Add("firstname",txtFName.Text );
			command.Parameters.Add("middlename", txtMName.Text);
			command.Parameters.Add("lastname",txtLName.Text);
            command.Parameters.Add("dfirstname", txtDFName.Text);
            command.Parameters.Add("dmiddlename", txtDMName.Text);
            command.Parameters.Add("dlastname", txtDLName.Text);
            command.Parameters.Add("companyname", txtCompany.Text);
			command.Parameters.Add("housenr",txtHousenr.Text);
			command.Parameters.Add("address",txtAddress.Text);
			command.Parameters.Add("postcode",txtPostCode.Text);
			command.Parameters.Add("residence",txtResidence.Text);
			command.Parameters.Add("country",ddlCountry.SelectedValue.ToString ());
            command.Parameters.Add("telephone", txtTelephone.Text);
            command.Parameters.Add("fax", txtFax.Text);
			command.Parameters.Add("email",txtEmail.Text);
			command.Parameters.Add("dhousenr",txtDHousenr.Text);
			command.Parameters.Add("daddress",txtDAddress.Text);
			command.Parameters.Add("dpostcode",txtDPostCode.Text);
			command.Parameters.Add("dresidence",txtDResidence.Text );
			command.Parameters.Add("dcountry",ddlDCountry.SelectedValue.ToString ());
            
			command.Parameters.Add("initialname",rdoInitialName.SelectedValue);
			command.Parameters.Add("website",txtWebsite.Text );
            command.Parameters.Add("dinitialname", rdoDInitialName.SelectedValue.ToString());

            if(query.Equals("insert")){
                command.Parameters.Add("password", txtPassword.Text.ToString());
            }

			bool b=handler.ExecuteQuery(command);
			return b;
            
			
		
		
	}
	private void SetCulturalValue()
	{
		//header.Style.Add("background-image", "url(graphics/" + (string)base.GetGlobalResourceObject("string", headerText) + ")");
		//btnSubmit.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnSubmit");
		valInvalidEmail.ErrorMessage = (string)base.GetGlobalResourceObject("string", "emailValidMessage");
		//valAddress.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
		valCountry.ErrorMessage  =(string)base.GetGlobalResourceObject("string", "requiredValidMessage");
		valEmail.ErrorMessage  =(string)base.GetGlobalResourceObject("string", "requiredValidMessage");
		valFName.ErrorMessage  =(string)base.GetGlobalResourceObject("string", "requiredValidMessage");
		//valHousenr.ErrorMessage =(string)base.GetGlobalResourceObject("string", "requiredValidMessage");
		//valPostCode.ErrorMessage  = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
		//valLName.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
		valPassword.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
		valResidence.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");


        rfvTxtDFName.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        rfvTxtDLName.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        rfvTxtDHousenr.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        rfvTxtDAddress.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        rfvTxtDPostCode.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        rfvTxtDResidence.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        rfvTxtLName.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        rfvTxtPostCode.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        rfvTxtAddress.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        rfvTxtHousenr.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");




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
        lblFax.Text=(string)base.GetGlobalResourceObject("string","lblFax");
		lblEMail.Text = (string)base.GetGlobalResourceObject("string", "lblEMail");
		lblPassword.Text = (string)base.GetGlobalResourceObject("string", "lblPassword");
        lblRePassword.Text = (string)base.GetGlobalResourceObject("string", "lblRePassword");
		lblWebsite.Text = (string)base.GetGlobalResourceObject("string", "lblWebsite");
        errorMessage = (string)base.GetGlobalResourceObject("string", "subscribeFailureMassage");
        agree = (string)base.GetGlobalResourceObject("string", "agree");
        misMatchPassword = (string)base.GetGlobalResourceObject("string", "misMatchPassword");
		//lblCurrentPage.Text = (string)base.GetGlobalResourceObject("string", "currentpage");
        //lblDifferentDelivery.Text = (string)base.GetGlobalResourceObject("string", "changeDelivery");
        //lblDifferentDelivery.Text = (string)base.GetGlobalResourceObject("string", "changeDelivery");
        chkDelivery.Text = (string)base.GetGlobalResourceObject("string", "changeDelivery");
        Label1.Text = (string)base.GetGlobalResourceObject("string", "lblInitialName");
        Label2.Text = (string)base.GetGlobalResourceObject("string", "lblFirstName");
        Label3.Text = (string)base.GetGlobalResourceObject("string", "lblMName");
        Label4.Text = (string)base.GetGlobalResourceObject("string", "lblLastName");
        Label5.Text = (string)base.GetGlobalResourceObject("string", "lblHouse");
        Label6.Text = (string)base.GetGlobalResourceObject("string", "lblAddress");
        Label7.Text = (string)base.GetGlobalResourceObject("string", "lblPostCode");
        Label8.Text = (string)base.GetGlobalResourceObject("string", "lblResidence");
        Label9.Text = (string)base.GetGlobalResourceObject("string", "lblCountry");
        chkAgree.Text = (string)base.GetGlobalResourceObject("string", "chkAgree");


	}

    protected void chkDelivery_CheckedChanged(object sender, EventArgs e)
    {
    
    }
    

}
