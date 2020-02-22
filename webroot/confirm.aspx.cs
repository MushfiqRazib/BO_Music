using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using Boeijenga.Business;
using Boeijenga.Common.Objects;
using Boeijenga.Common.Utils;
using Boeijenga.DataAccess;
using Npgsql;
using System.Drawing;
//ing System.Web.Mail;

public partial class cofirm : BasePage
{
     DataTable table = new DataTable();
    
    ArrayList visitPageList;
    string cultureName = "";
    string disclaimer = "";
    string strTrans = "";
    string strTrans_2 = "";
    string confirmMsg = "";
    public double grandTotoal = 0.0;
    public double total = 0.00;
    public double vat = 0.00;
    public double sub_total = 0.00;
    public double sub_vat = 0.00;
	string strTotal="";
    string deliveryAddress = "";
    string invoiceAddress = "";
	string euroUnicode = "&#8364;";

    private Customer customer;
    public double totalPrice = 0.0;
    int qty = 0;
    public double totalVat = 0.0;

    public Country Delivery_Country
    {
        set
        {
            ViewState["Delivery_Country"] = value;


        }

        get
        {           
                return (Country)ViewState["Delivery_Country"];          
        }
    }

    ArrayList cartTable = new ArrayList();
    Order order;
    IEnumerator enu;

    public string printName, printHno, printAddr, printPcode, printRes, printCont,OrderID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null || Session["order"] == null)
        {
            Response.Redirect("home.aspx");
            return;
        }

        Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);        
        if (!IsPostBack)
        {

           
            
                customer = GetCustomer();
                
                InitCartList();

                SetCulture();
                // setDataGrid();
                LoadBtnAttr();
                SetCulturalValue();
                InitOrderGrid(cartTable, grdOrder);
                ShowDeliveryInfo();
                LoadPayamentType();
          
            setVisitPageList(Functions.GetPageName(Request.Url.ToString()));
        }
       
       

       
        //GetVisitedDepth();


        //for payment method
        HandlePaymentIssues();
    }

    private void LoadPayamentType()
    {
        paymentTypeList.DataSource = new Facade().GetPaymentsTypes();
        paymentTypeList.DataTextField = "name";
        paymentTypeList.DataValueField = "id";
        paymentTypeList.DataBind();
    }

    private void HandlePaymentIssues()
    {
        string txToken = Request.QueryString.Get("tx");
        string transactionid = Request.QueryString.Get("transactionid");
        if (transactionid != null)
        {
            if (Functions.GetPaymentStatus(transactionid) == "completed")
            {
                updateOrderTable();
            }
        }
        if (txToken != null)
        {
            if (Functions.GetPaypalPaymentStatus(txToken))
            {
                updateOrderTable();
            }
        }
    }

    
    private void LoadBtnAttr()
    {
        if (Session["cultureName"].ToString() == "en-US")
        {
            confirmMsg = "Are you sure you want to confirm this order?";
            btnConfirm.Attributes.Add("onclick", "return confirm('" + confirmMsg + "');");
        }
        else
        {
            confirmMsg = "Bestelling akkoord?";
            btnConfirm.Attributes.Add("onclick", "return confirm('" + confirmMsg + "');");
        }
    }
	private void SetCulturalValue()
	{
		//header.Style.Add("background-image", "url(graphics/" + (string)base.GetGlobalResourceObject("string", "headerConfirm") + ")");
        //btnBack.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnGoBack");
        //btnConfirm.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnConfirm");
        strTrans = (string)base.GetGlobalResourceObject("string", "strTrans");
        strTrans_2 = (string)base.GetGlobalResourceObject("string", "srtTrans_2");
        lblPaymentType.Text = (string)base.GetGlobalResourceObject("string", "lblPaymentType");

        disclaimer = (string)base.GetGlobalResourceObject("string", "lblDisclaimer");
        lblVatText.Text = (string)base.GetGlobalResourceObject("string", "vat_only_report");
        lblTotalText.Text = (string)base.GetGlobalResourceObject("string", "lblTotal_Price");
        lblSubTotalText.Text = (string)base.GetGlobalResourceObject("string", "lblTotal_vat");
        lblDelAdd.Text = (string)base.GetGlobalResourceObject("string", "lblHeader");
         lblShippingCostText.Text = (string)base.GetGlobalResourceObject("string", "lblShipping");
        lblInvoiceAddressHeader.Text = (string)base.GetGlobalResourceObject("string", "lblInvoiceAddressHeader");
        LoadBtnAttr();
        //if (Session["cultureName"].ToString() == "en-US")
        //{
        //    strTrans = "Your order";
        //    strTrans_2 = "has been succesfully placed. You will receive a confirmation by email within a moment.";
        //}
        //else
        //{
        //     strTrans = "Uw bestelling";
        //    strTrans_2 = "is verstuurd. Binnen enkele ogenblikken ontvangt u een bevestiging per email. Hartelijk dank voor uw bestelling.";
        //}

        //steps
        lblHeader.Text = "4) " + (string)base.GetGlobalResourceObject("string", "stepPayment");
        lblBasket.Text = "1) " + (string)base.GetGlobalResourceObject("string", "basket");
        lblLogReg.Text = "2) " + (string)base.GetGlobalResourceObject("string", "steplogin");
        lblDelAddress.Text = "3) " + (string)base.GetGlobalResourceObject("string", "stepDelivaery");
        lblPayment.Text = "4) " + (string)base.GetGlobalResourceObject("string", "stepPayment");
        lblOrderComplete.Text = "5) " + (string)base.GetGlobalResourceObject("string", "stepComplete");
	}
	private void ShowDeliveryInfo()
	{
        if (Session["user"] != null)
        {
            User user = (User)Session["user"];
            lblDName.Text =user.dInitialName+" "+ user.dFirstname + " " + user.dMiddlename + " " + user.dLastname;
            lblDCountry.Text = Delivery_Country.CountryName;
            lblDAddress.Text = user.dAddress + " ";
            lblDHouseNum.Text = user.dHousenr + " ";
            lblDPostCode.Text = user.dPostcode + " ";
            lblDResidence.Text = user.dResidence + " ";
            deliveryAddress += lblDName.Text + "<br>" + lblDAddress.Text + " " + lblDHouseNum.Text + "<br>" + lblDPostCode.Text + "  " + lblDResidence.Text + "<br>" + lblDCountry.Text;
            ViewState["deliveryAddress"] = deliveryAddress;
        }
	}

    void Master_MenuButton(object sender, EventArgs e)
    {
        SaveOrder(grdOrder);
        Session["cultureName"] = Master.CurrentButton.ToString();
        SetCulture();
        SetCulturalValue();
        InitCartList();
        InitOrderGrid(cartTable, grdOrder);
    }


    protected void changeAddress_Click(object sender, EventArgs e)
    {
        SaveOrder(grdOrder);
        Response.Redirect("delivery.aspx");
    }
	private Country GetCountry(string countryName)
	{
	    return new Facade().GetCountry(countryName);
	}
    protected void setVisitPageList(String pageName)// setting the visit page list
    {
        Master.SetVisitPageList(pageName);
    }
  

    private void InitCartList()
    {
        // artcle code list is stored in session as order[hashtable]

        if (Session["order"] != null)
        {
            cartTable = (ArrayList)Session["order"];
        }
    }


    public double CalculateShippingCosts(ArrayList cartTable)
    {
        double shippingCost = 0;
        int sheetMusicOrderCount = 0;

        int bookOrderCount = 0;

        int cdDVDOrderCount = 0;

        foreach (Order order in cartTable)
        {

            switch (order.productType)
            {

                case "s":
                    sheetMusicOrderCount += order.quantity;
                    break;

                case "b":
                    bookOrderCount += order.quantity;
                    break;
                case "c":
                    cdDVDOrderCount += order.quantity;
                    break;
            }


        }
        if (sheetMusicOrderCount > 0)
        {
            shippingCost += new Facade().GetShippingCost(Delivery_Country.CountryCode, "s", sheetMusicOrderCount);

        }
        if (bookOrderCount > 0)
        {
            shippingCost += new Facade().GetShippingCost(Delivery_Country.CountryCode, "b", bookOrderCount);

        }
        if (cdDVDOrderCount > 0)
        {
            shippingCost += new Facade().GetShippingCost(Delivery_Country.CountryCode, "c", cdDVDOrderCount);

        }
        return shippingCost;
    }



    public double CalculateShippingCostsWithVat(ArrayList cartTable)
    {
        double shippingCost = 0;
        int sheetMusicOrderCount = 0;

        int bookOrderCount = 0;

        int cdDVDOrderCount = 0;

        foreach(Order order in cartTable)
        {

            switch (order.productType)
            {

                case "s":
                    sheetMusicOrderCount += order.quantity;
                    break;

                case "b":
                    bookOrderCount += order.quantity;
                    break;
                case "c":
                    cdDVDOrderCount += order.quantity;
                    break;
            }


        }
        if (sheetMusicOrderCount > 0)
        {
            shippingCost += new Facade().GetShippingCostWithVat(Delivery_Country.CountryCode, "s", sheetMusicOrderCount);

        }
        if (bookOrderCount > 0)
        {
            shippingCost += new Facade().GetShippingCostWithVat(Delivery_Country.CountryCode, "b", bookOrderCount);

        }
        if (cdDVDOrderCount > 0)
        {
            shippingCost += new Facade().GetShippingCostWithVat(Delivery_Country.CountryCode, "c", cdDVDOrderCount);

        }
        return shippingCost;
    }



    public void InitOrderGrid(ArrayList cartTable, GridView grdOrder)
    {
        if (cartTable.Count > 0)
        {
            DataColumn colQuantity;
            DataColumn colTotalPrice;

            colQuantity = new DataColumn();
            colQuantity.DataType = System.Type.GetType("System.Int32");
            colQuantity.ColumnName = "quantity";
            colQuantity.DefaultValue = 1;

            colTotalPrice = new DataColumn();
            colTotalPrice.DataType = System.Type.GetType("System.Double");
            colTotalPrice.ColumnName = "total";

            //String sql = GetCartListSql(cartTable);
            //DataTable dtOrder = DataAccessHelper.GetInstance().GetDataTable(sql);
            grdOrder.Columns[0].HeaderText = "&#160;&#160;" + (string)base.GetGlobalResourceObject("string", "lblCategory");
            grdOrder.Columns[1].HeaderText = (string)base.GetGlobalResourceObject("string", "productdescription");
            //grdOrder.Columns[2].HeaderText = (string)base.GetGlobalResourceObject("string", "price");
            grdOrder.Columns[3].HeaderText = (string)base.GetGlobalResourceObject("string", "price");
            grdOrder.Columns[4].HeaderText = (string)base.GetGlobalResourceObject("string", "quantity");
            grdOrder.Columns[5].HeaderText = (string)base.GetGlobalResourceObject("string", "total");
            //dtOrder.Columns.Add(colTotalPrice);
            // dtOrder.Columns.Add(colQuantity);

            //int rowCount = dtOrder.Rows.Count;
            DataTable dtOrder = new DataTable();
            dtOrder.Columns.Add("articlecode");
            dtOrder.Columns.Add("productType");
            dtOrder.Columns.Add("title");
            dtOrder.Columns.Add("subtitle");
            dtOrder.Columns.Add("publisher");
            dtOrder.Columns.Add("price");
            dtOrder.Columns.Add("quantity");
            dtOrder.Columns.Add("total");
            dtOrder.Columns.Add("vatpc");
            dtOrder.Columns.Add("deliverytime");
            int i = 0;
            
            enu = cartTable.GetEnumerator();
           
           
            while (enu.MoveNext())
            {
                DataRow row = dtOrder.NewRow();
                order = (Order)enu.Current;




                row["articlecode"] = order.articlecode.ToString();
                row["productType"] = order.productType.ToString();
                row["publisher"] = order.publisherName.ToString();
                row["title"] = order.productdescription.ToString();
                row["subtitle"] = order.subtitle.ToString();
                row["deliverytime"] = order.deliveryTime;


                double discount =  Math.Round(( order.price * (order.discountpc / 100)), 2);

                double price = Math.Round( (order.price - discount) , 2);
                  
                row["price"] = string.Format("{0:F2}", price);
                row["quantity"] = order.quantity.ToString();
                row["vatpc"] = string.Format("{0:F2}", order.vatpc);
                qty += (int)order.quantity;
              


                double total = Math.Round((price * order.quantity), 2);


                double vatIncludePrice = VatCalculator.GetVatIncludedPrice(total, order.vatpc);

                totalVat += (vatIncludePrice - total);

                totalPrice += total;
                row["total"] = string.Format("{0:F2}", total);
                dtOrder.Rows.Add(row);
                i++;
            }

           

            
            totalVat = Math.Round(totalVat, 2);
            grdOrder.DataSource = dtOrder;
            grdOrder.DataBind();

            double totalShippingCosts = CalculateShippingCostsWithVat(cartTable);

            double grandTotalPrice=Math.Round( (totalPrice + totalVat + totalShippingCosts),2);
            
            ViewState["GrandTotalPrice"] = grandTotalPrice;

            ViewState["TotalVat"] = totalVat;
            ViewState["TotalNetPrice"] = totalPrice;

            ViewState["TotalShippingCosts"] = totalShippingCosts;

           // Session["gtotal"] = string.Format("{0:F2}", grandTotoal);
            lblSubTotal.Text = "€ " + string.Format("{0:F2}",totalPrice.ToString()) + " ";
            lblVat.Text = "€ " + string.Format("{0:F2}", totalVat.ToString())+ " ";
            lblTotal.Text = "€ " + string.Format("{0:F2}",grandTotalPrice.ToString()) + " ";
            lblShippingCost.Text = "€ " + string.Format("{0:F2}",totalShippingCosts.ToString()) + " "; 
          
          



        }
        else
        {
            grdOrder.DataSource = null;
            grdOrder.DataBind();
            grdOrder.Visible = false;
            lblSubTotal.Text = "€ 0 ";
            lblVat.Text = "€ 0 ";
            lblTotal.Text = "€ 0 ";
            lblShippingCost.Text = "€ 0 "; 
          
        }
    }


 
    private Customer GetCustomer()
    {
        User LoginUser = (User)Session["user"];

        if(LoginUser.dCountry == null)
        {

            LoginUser = new Facade().LoadUserDeliveryInfo(LoginUser);

        }

        Customer customer = new Facade().GetCustomerByCustomerId(LoginUser.ID);
  
        lblName.Text = customer.Initialname + " " +customer.Firstname + " " + customer.Middlename + " " + customer.lastname;
        lblAddress.Text = customer.Address;
        lblResidence.Text = customer.Residence;
        lblHouseNum.Text = customer.HouseNumber;
        lblPostCode.Text = customer.ZipCode;
        lblCountry.Text = new Facade().GetCountryByCountryCode(customer.Country).CountryName;
        Delivery_Country = new Facade().GetCountryByCountryCode(LoginUser.dCountry);
      
        Session["customer"] = customer;
        
        //added by provas
        Session["discountpc"] = customer.Discountpc;
        invoiceAddress += lblName.Text + "<br>" + lblAddress.Text + "  " + lblHouseNum.Text + "<br>" + lblPostCode.Text + " " + lblResidence.Text + "<br>" + lblCountry.Text;
        ViewState["invoiceAddress"] = invoiceAddress;
        return customer;
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

 
    protected string ShowArticleTypeImg(string articleType)
    {
        string articleTypePath = "graphics/";
        return Functions.GetArticleTypeImage(articleType, articleTypePath);
    }


 
    private void SetCulture()
    {
        Master.SetCulture();
    }
    protected void SaveOrder(GridView gridView)
    {
        
        ArrayList cartTable = Functions.GetOrderGridData(gridView);
        Session["order"] = cartTable;
    }

    protected void lnkDelete_Command(object sender, CommandEventArgs e)
    {
        string articleCode = e.CommandArgument.ToString();
        SaveOrder(grdOrder);
        if (Session["order"] != null)
        {
            ArrayList  cartTable = (ArrayList)Session["order"];
            IEnumerator enu = cartTable.GetEnumerator();
            while (enu.MoveNext())
            {
                Order order = (Order)enu.Current;
                if (order.articlecode.ToString().Equals(articleCode))
                {
                    cartTable.Remove(order);
                    cartTable.TrimToSize();
                  //  InitCartList();
                    break;
                }
            }
            Session["order"] = cartTable;
           InitCartList();
            InitOrderGrid(cartTable, grdOrder);
          //  LoadDatagrid();

        }
    }
    
    

    private void updateOrderTable()
    {
        invoiceAddress = ViewState["invoiceAddress"].ToString();
        deliveryAddress = ViewState["deliveryAddress"].ToString();
        processOrder();
        Session["order"] = null;

      
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        SaveOrder(grdOrder);
        InitCartList();
        if (cartTable.Count > 0)
        {


            switch (this.paymentTypeList.SelectedItem.Text.ToUpper())
            {
                case "INVOICE":
                    updateOrderTable();
                    break;
                case "IDEAL":

                    string[] urlIDEAL = Functions.ExtractMultisafepayPaymentUrl((Customer)Session["customer"], ViewState["GrandTotalPrice"].ToString(), ((User)Session["user"]).Email, "IDEAL");
                    Response.Redirect(urlIDEAL[0].ToString());
                    break;
                case "MASTERCARD":
                    string[] urlMASTERCARD = Functions.ExtractMultisafepayPaymentUrl((Customer)Session["customer"], ViewState["GrandTotalPrice"].ToString(), ((User)Session["user"]).Email, "MASTERCARD");
                    Response.Redirect(urlMASTERCARD[0].ToString());
                    break;
                case "BANKTRANS":
                    string[] urlBANKTRANS = Functions.ExtractMultisafepayPaymentUrl((Customer)Session["customer"], ViewState["GrandTotalPrice"].ToString(), ((User)Session["user"]).Email, "BANKTRANS");
                    Response.Redirect(urlBANKTRANS[0].ToString());
                    break;
                case "VISA":
                    string[] urlVISA = Functions.ExtractMultisafepayPaymentUrl((Customer)Session["customer"], ViewState["GrandTotalPrice"].ToString(), ((User)Session["user"]).Email, "IDEAL");
                    Response.Redirect(urlVISA[0].ToString());
                    break;
                case "PAYPAL":

                    Response.Redirect(Functions.ExtractPaypalPaymentUrl(ViewState["GrandTotalPrice"].ToString()));
                    break;

            }

        }
       
    }


    private void processOrder()
    {
        string msg = "";
        bool b = transectionOrder(ref msg);

        // Error Processing 
		if (b == false) { lblTransection.Text=msg; }
		else
		{
            strTrans = (string)base.GetGlobalResourceObject("string", "strTrans");
            strTrans_2 = (string)base.GetGlobalResourceObject("string", "srtTrans_2");
      
            lblTransection.Text = strTrans + " #" + OrderID + " " + strTrans_2;
			Session["order"]=null;
           paymentTypeList.Enabled=false ;
			btnConfirm.Enabled =false;
            lblTransection.Focus();
            //btnBack.Focus();
            
            ConstructMail();// construct and send email		
		}
    }

    

    private void ConstructMail()
    {
        //invoice and delivery address of the mail
        string imagePath = System.Configuration.ConfigurationManager.AppSettings["web-graphics"].ToString();
        string headerPath = imagePath + "mail_header.gif";
        string footerPath = imagePath + "mail_footer.gif"; 
        string addHeader = @"
<style type='text/css'>
<!--
body {
	background-color: #e2e2e2;
}
body,td,th {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 11px;
	color: #000000;
}
-->
</style></head>

<body leftmargin='0' topmargin='0' rightmargin='0' bottommargin='0' marginwidth='0' marginheight='0'>
<center>
  <br />
  <br />
  <table width='680' border='0' cellspacing='0' cellpadding='0'>
  <tr>
    <td><img src='"+ headerPath +@"' width='680' height='83' /></td>
  </tr>
  <tr>
    <td bgcolor='#FFFFFF'><table width='680' border='0' cellspacing='0' cellpadding='8'>
      <tr>
        <td align='left' valign='top'>";
        string addFooter = @"</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td><img src='" + footerPath + @"' width='680' height='29' /></td>
  </tr>
</table></center>
</body>";

		string productTypeImage = "";
        string productDescMail = "";
        string productPriceMail = "";
        string productDisMail = "";
        string productVatMail = "";
        string productQtyMail = "";
        string productTotalMail = "";
		string[] colorArray ={ "white", "#EFEFEF" };
        string mailAddress = "<table width=100% align = \"center\" border=\"0\">" +
                "<tr>" +
                "<td width=200px align=\"left\" style=\"align: left;\"><b>" + "Invoice Address" +
                "</b></td>" +
                "<td width=90px align=\"left\">" +
                "</td>" +
                "<td width=200px align=\"left\" style=\"align: left;\"><b>" + "Delivery Address" +
                "</b></td>" +
            "</tr>" +
            "<tr>" +
                "<td align=\"left\" style=\"align: left;\">" + invoiceAddress +
                "</td>" +
                "<td align=\"left\">&nbsp;&nbsp;&nbsp;" +
                "</td>" +
                "<td align=\"left\" style=\"align: left;\">" + deliveryAddress +
                "</td>" +
            "</tr></table>";

        //create the body of the mail        
        string product = "<table cellpadding=3 cellspacing=0 width=650px align=\"center\" border=\"0\">" +
			"<tr><td colspan=7 align=left style='font-size:15px;'><font color=red><b>Order No#" + OrderID.ToString() + "</b></font> </td><tr>" +
			"<tr><td colspan=7 width=650>" +
			mailAddress +
            "</td></tr>" +
			"<tr><td colspan=5>&nbsp;</td></tr>"+
			"<tr style='background-color:#DEDEDE;'>" +
				"<td  width=100px align=\"left\"><b>" + "Product Type" +
                "</b></td>" +
				"<td  width=200px align=\"left\"><b>" + "Product Description" +
                "</b></td>" +
				"<td  width=125px align=\"right\"><b>" + "Price (excl.VAT)" +
                "</b></td>" +
                "<td  width=100px align=\"right\"><b>" + "Discount (%)" +
                "</b></center></td>" +
				"<td  width=100px align=\"right\"><b>" + "Quantity" +
                "</b></center></td>" +
                "<td  width=125px align=\"right\"><b>" + "Net Price" +
                "</b></center></td>" +
                "<td  width=100px align=\"right\" style=\"align: right;\"><b>" + "VAT (%)" +
                "</b></td>" +
            "</tr>" +
            "<tr>" +
                "<td align=\"center\">" +
                "</td>" +
                "<td align=\"center\">" +
                "</td>" +
                "<td align=\"center\">" +
                "</td>" +
                "<td align=\"center\">" +
                "</td>" +
                "<td align=\"center\">" +
                "</td>" +
                "<td align=\"center\">" +
                "</td>" +
                "<td align=\"center\">" +
                "</td>" +
            "</tr>";
		int colorIndex=0;
        foreach (Order order in cartTable)
        {
            //string path = Request.Url.ToString();
            //string articleTypePath = path.Substring(0, path.LastIndexOf('/') + 1) + "graphics/";
            productTypeImage = Functions.GetArticleTypeImage(order.productType);          
            
            //productDescMail = dr["ProductDescription"].ToString();
            productDescMail = @"<table>
                                    <tr>
                                        <td>                                                                    
                                            <b>"+order.productdescription+@"</b>                                                                    
                                        </td>
                                    </tr>
                                        <td>
                                            "+order.subtitle+@"
                                        </td>
                                    <tr>
                                        <td>
                                            <i>"+order.publisherName+@"</i>
                                        </td>
                                    </tr> 
                                </table>";
            
            
            
            
            productPriceMail = order.price.ToString();
            productDisMail = order.discountpc.ToString();
            productQtyMail = order.quantity.ToString();
            productTotalMail = order.productdescription.ToString();
            productVatMail = order.vatpc.ToString();

            product += "<td width=100px style='background-color:" + colorArray[colorIndex] + "' align=\"left\" style=\"align: left;\">" + productTypeImage +
                        "</center></td>" +
                        "<td width=200px  style='background-color:" + colorArray[colorIndex] + "' align=\"left\" style=\"align: left;\">" + productDescMail +
                        "</center></td>" +
                        "<td  width=125px style='background-color:" + colorArray[colorIndex] + "' align=\"right\" style=\"align: right;\">" + productPriceMail +
                        "</center></td>" +
                        "<td  width=100px style='background-color:" + colorArray[colorIndex] + "' align=\"right\" style=\"align: right;\">" + productDisMail +
                        "</center></td>" +
                        "<td width=100px style='background-color:" + colorArray[colorIndex] + "' align=\"right\" style=\"align: right;\">" + productQtyMail +
                        "</center></td>" +
                        "<td width=125px style='background-color:" + colorArray[colorIndex] + "' align=\"right\" style=\"align: right;\">" + productTotalMail +
                        "</center></td>" +
                        "<td width=100px style='background-color:" + colorArray[colorIndex] + "' align=\"right\" style=\"align: right;\">" + productVatMail +
                        "</center></td>" +
                        "</tr>";
			if(colorIndex==0){colorIndex=1;}else{colorIndex=0;}
        }
        //grandTotoal = Convert.ToDouble(Session["gtotal"].ToString().Replace(',','.'));
        //total = Convert.ToDouble(Session["total"].ToString().Replace(',', '.'));
        //vat = Convert.ToDouble(Session["vat"].ToString().Replace(',', '.'));
        string grandTotalStr = ViewState["GrandTotalPrice"].ToString();
       
        string totalStr = ViewState["TotalNetPrice"].ToString();
        string vatStr = ViewState["TotalVat"].ToString();

        string totalShippingCost = ViewState["TotalShippingCosts"].ToString();

        product += "<tr style='width:650px;background-color:#DEDEDE;'><td colspan=\"6\" align=\"right\">Total (excl.VAT) :" +
                "</td>" +
                "<td colspan=\"1\" align=\"right\">" + euroUnicode + " " + string.Format("{0:F2}", totalStr) +
                "</td>" +
                "</tr>" +
                "<tr style='background-color:#DEDEDE;'><td colspan=\"6\" align=\"right\">VAT :" +
                "</td>" +
                "<td colspan=\"1\" align=\"right\">" + euroUnicode + " " + string.Format("{0:F2}", vatStr) +
                "</td>" +
                "</tr>" + 
                "<tr style='background-color:#DEDEDE;'><td colspan=\"6\" align=\"right\">Total Shippingcost : " +
                "</td>" +
                "<td colspan=\"1\" align=\"right\"><b>" + euroUnicode + " " + string.Format("{0:F2}", totalShippingCost) +
                "</b></td>" +
                "</tr>" +
                "<tr style='background-color:#DEDEDE;'><td colspan=\"6\" align=\"right\"><b>Total Price</b> <b>:</b>" +
                "</td>" +
                "<td colspan=\"1\" align=\"right\"><b>" + euroUnicode + " " + string.Format("{0:F2}", grandTotalStr) +
                "</b></td>" +
                "</tr>" +
                "</table>";
        // create a table and insert all the tables into it
        string maintainEmail = "<table  align = \"center\" border=\"0\">" +
                
                "<tr><td align=\"center\">" + product + "</td><tr>" +
                "<tr><td align=\"left\">" + disclaimer + "</td><tr>" +
                "</table>";

        addHeader += maintainEmail + addFooter;
        //Response.Write(addHeader);
        senMail(addHeader);//send the mail               
    }

    private void senMail(string maintainEmail)
    {
		if (Session["user"] != null)
		{
            User user = (User)Session["user"];
            string mailFrom = System.Configuration.ConfigurationManager.AppSettings["mail-from"].ToString();
            string mailTo = user.Email;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Host = System.Configuration.ConfigurationManager.AppSettings["mail-server"].ToString();

            System.Net.Mail.MailAddress fromAddr = new System.Net.Mail.MailAddress(mailFrom);
            System.Net.Mail.SmtpPermission sett = new System.Net.Mail.SmtpPermission(System.Security.Permissions.PermissionState.Unrestricted);
            //client.UseDefaultCredentials = true;
            System.Net.Mail.MailAddress toAddr = new System.Net.Mail.MailAddress(mailTo);
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(mailFrom, mailTo);
            message.CC.Add(mailFrom);
            message.Subject = "Ordered Successfully";
            message.Body = maintainEmail;
            message.IsBodyHtml = true;
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
            }
		}
    }

    private bool transectionOrder(ref string msg)
    {
		bool b = false;
		if(Session["order"]!=null)
		{

			ArrayList  orderInfo = (ArrayList )Session["order"];
			DataTable table = new DataTable();
			ArrayList arrayList = new ArrayList();
			
			if ((User)Session["user"] != null)
			{
				User user = (User)Session["user"];
				int uid = user.ID;

				string sql_order = "select max(orderid) as maxid from orders";
				DataTable dtOrders = DataAccessHelper.GetInstance().GetDataTable(sql_order);
				int maxid;
				if (dtOrders.Rows[0]["maxid"].ToString().Equals(""))
					maxid = 1;
				else
					maxid = (int)dtOrders.Rows[0]["maxid"] + 1;

				OrderID=maxid.ToString();

                int paymentMode =int.Parse(this.paymentTypeList.SelectedItem.Value);

			    string countrycode = Delivery_Country.CountryCode;
                double shippingcost = CalculateShippingCostsWithVat(cartTable);
                double netShippingCost = CalculateShippingCosts(cartTable);
                double shippingCostVat = shippingcost - netShippingCost;

				sql_order = @"insert into Orders(dhousenr,daddress,dpostcode,dresidence,dcountry,orderid,orderdate,
							customer,shippingcost,paymode,shippingvat) values(:dhousenr,:daddress,:dpostcode,:dresidence,:dcountry,:orderid,:orderdate,:customer,:shippingcost,:paymode,:shippingvat)";

				int num_orders = orderInfo.Count ;
                NpgsqlCommand[] commands = new NpgsqlCommand[num_orders + 1];
				commands[0] = new NpgsqlCommand(sql_order);
				commands[0].Parameters.Add("dhousenr",lblDHouseNum.Text );
				commands[0].Parameters.Add("daddress", lblDAddress.Text );
				commands[0].Parameters.Add("dpostcode", lblDPostCode.Text  );
				commands[0].Parameters.Add("dresidence",lblDResidence.Text  );
				commands[0].Parameters.Add("dcountry", countrycode);
				commands[0].Parameters.Add("orderid", maxid);
				commands[0].Parameters.Add("orderdate", System.DateTime.Now.Date.ToString("yyyy-MM-dd"));
				commands[0].Parameters.Add("customer", uid);
				commands[0].Parameters.Add("shippingcost", shippingcost);
                commands[0].Parameters.Add("paymode", paymentMode);
                commands[0].Parameters.Add("shippingvat", shippingCostVat);
              
                
                //commands[0].Parameters.Add("orderstatus", "1"); 
				//add statement  of order table

				string sqlSelect="", articleCode="";
				double vatpc=0.00;
				double dblPrice=0.00;
				int  i = 1;
				
				IEnumerator enu=orderInfo.GetEnumerator(); 
				while(enu.MoveNext())
				{
					Order order=(Order)enu.Current;
					
                    sqlSelect = "insert into ordersline(orderid,articlecode,unitprice,vatpc,quantity,discountpc) values(:orderid,:articlecode,:unitprice,:vatpc,:quantity,:discountpc)";
					commands[i] = new NpgsqlCommand(sqlSelect);
					commands[i].Parameters.Add("orderid", maxid);
					commands[i].Parameters.Add("articlecode", order.articlecode);
					commands[i].Parameters.Add("unitprice", order.price);
					commands[i].Parameters.Add("vatpc", order.vatpc);
					commands[i].Parameters.Add("quantity", order.quantity);
                  
                    commands[i].Parameters.Add("discountpc", order.discountpc);

					i++; // increase index
				}
             
               // commands[i] = new NpgsqlCommand(sqlSelect);
               // commands[i].Parameters.Add("orderid", maxid);
              
			
				b = DataAccessHelper.GetInstance().ExecuteTransaction(commands, ref msg);
			}
		}
		return b;
    }


    protected void btnBack_Click(object sender, EventArgs e)
	{
		Response.Redirect("delivery.aspx");
	}
    protected void btnCart_Click(object sender, EventArgs e)
    {
        Response.Redirect("shoppingcart.aspx");
    }


    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        
    }

    protected void quantity_changed(object sender, EventArgs e)
    {
        SaveOrder(grdOrder);
        InitCartList();
        InitOrderGrid(cartTable, grdOrder);
    }
}
