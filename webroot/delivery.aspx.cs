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
using Boeijenga.Business;
using Boeijenga.Common.Objects;
using Boeijenga.DataAccess;

public partial class delivery : BasePage
{
  
    ArrayList visitPageList;
    ArrayList cartTable;
    string cultureName = "";
    public double grandTotoal = 0.0;
	
	
   
    public string printName, printHno, printAddr, printPcode, printRes, printCont;
	protected void Page_Load(object sender, EventArgs e)
	{
		Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);
		//User us = new User(21, "ch@yahoo.com");
		//Session["user"] = us;
		if (!IsPostBack)
		{
			SetCulture();
			//setDataGrid();
			SetCulturalValue();
         Master.SetVisitPageList(Functions.GetPageName(Request.Url.ToString()));
      
			//LoadInitialName();
			LoadCountryName();
			LoadDeliveryInfo();
			//LoadGrid();
		
		}
       
	}
	private void LoadDeliveryInfo()
	{
		if (Session["user"] != null)
		{
			User user = (User)Session["user"];

		    user = new Facade().LoadUserDeliveryInfo(user);


          


            txtCompany.Text = user.CompanyName;
            txtDFName.Text = user.dFirstname;
            txtDMName.Text = user.dMiddlename;
            txtDLName.Text = user.dInitialName;
            txtDHouseNum.Text = user.dHousenr;
            txtDAddress.Text = user.dAddress;
            txtDResidence.Text = user.dResidence;
            txtDPostCode.Text = user.dPostcode;
            ddlCountry.SelectedValue = user.dCountry;
            rdoInitialName.SelectedValue = user.dInitialName;
		}
	}
	
	private void LoadCountryName()
	{
		string sqlQuery = "select countrycode, countryname from country order by countryname";
		DataTable dtCountry = DataAccessHelper.GetInstance().GetDataTable(sqlQuery);
		ddlCountry.DataSource = dtCountry;
		ddlCountry.DataValueField = dtCountry.Columns["countrycode"].ToString();
		ddlCountry.DataTextField = dtCountry.Columns["countryname"].ToString();
		ddlCountry.DataBind();

	}

    void Master_MenuButton(object sender, EventArgs e)
    {
        Session["cultureName"] = Master.CurrentButton.ToString();
        SetCulture();
		SetCulturalValue();
		//setDataGrid();
      
    }

 

    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {        
        InitializeComponent();
        base.OnInit(e);
    }
   
    private void InitializeComponent()
    {
        //this.btnupdate.Click += new System.Web.UI.ImageClickEventHandler(this.btnupdate_click);
        //this.btnnext.Click += new System.Web.UI.ImageClickEventHandler(this.btnnext_click);
    }
    #endregion	
   


    
    protected string ShowArticleImage(string articleType) 
    {
        if (articleType.ToUpper().Equals("B"))
        {
            return "<img src='graphics/book.png' />";
        }
        else if (articleType.ToUpper().Equals("C"))
        {
            return "<img src='graphics/cd.png' />";
        }
        else
        {
            return "<img src='graphics/musicsheet.png' />";
        }

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

    
   
    private void SetCulturalValue()
	{
		valAddress.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
		valFName.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
		valHouseNum.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
		valPostCode.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
		valLName.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");		
		valResidence.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
		lblInitialName.Text = (string)base.GetGlobalResourceObject("string", "lblInitialName");
		lblDFName.Text = (string)base.GetGlobalResourceObject("string", "lblFirstName");
		lblDMName.Text = (string)base.GetGlobalResourceObject("string", "lblMName");
		lblDLName.Text = (string)base.GetGlobalResourceObject("string", "lblLastName");
		lblDHouseNum.Text = (string)base.GetGlobalResourceObject("string", "lblHouse");
		lblDAddress.Text = (string)base.GetGlobalResourceObject("string", "lblAddress");
		lblDResidence.Text = (string)base.GetGlobalResourceObject("string", "lblResidence");
		lblDPostCode.Text = (string)base.GetGlobalResourceObject("string", "lblPostCode");
		lblDCountry.Text = (string)base.GetGlobalResourceObject("string", "lblCountry");
        //lblHeader.Text = (string)base.GetGlobalResourceObject("string", "lblHeader");
        lblCompany.Text = (string)base.GetGlobalResourceObject("string", "lblCompany");
        //steps
        lblHeader.Text = "3) " + (string)base.GetGlobalResourceObject("string", "stepDelivaery");
        lblBasket.Text = "1) " + (string)base.GetGlobalResourceObject("string", "basket");
        lblLogReg.Text = "2) " + (string)base.GetGlobalResourceObject("string", "steplogin");
        lblDelAddress.Text = "3) " + (string)base.GetGlobalResourceObject("string", "stepDelivaery");
        lblPayment.Text = "4) " + (string)base.GetGlobalResourceObject("string", "stepPayment");
        lblOrderComplete.Text = "5) " + (string)base.GetGlobalResourceObject("string", "stepComplete");
	}
	
	
	protected void btnNext_Click(object sender,EventArgs e)
	{
       
        if (EmptyValidateFields())
        {
            User user=(User)Session["user"];
            user.CompanyName = txtCompany.Text;
		    user.dAddress=txtDAddress.Text;
		    user.dCountry=ddlCountry.SelectedValue.ToString();
		    user.dFirstname=txtDFName.Text;
		    user.dHousenr=txtDHouseNum.Text;
		    user.dInitialName=rdoInitialName.Text.Trim();
		    user.dLastname=txtDLName.Text;
		    user.dMiddlename=txtDMName.Text ;
		    user.dPostcode=txtDPostCode.Text;
		    user.dResidence=txtDResidence.Text;
            Session["user"] = user; 
		    Response.Redirect("confirm.aspx");
        }
	}

    /// <summary>
    /// this method search for empty fields from server side
    /// author -- Abdullah Al Mohammad
    /// Last Updated - 06-07-2007
    /// </summary>
    /// <returns>
    /// returns true if not empty
    /// if empty then return false
    ///</returns>
    private bool EmptyValidateFields()
    {
        if (txtDFName.Text.Trim().Equals("") || txtDLName.Text.Trim().Equals("") || txtDPostCode.Text.Trim().Equals("")
            || txtDHouseNum.Text.Trim().Equals("")||txtDAddress.Text.Trim().Equals("")||txtDResidence.Text.Trim().Equals(""))
        {
            return false;
        }
        return true;

    }
}
