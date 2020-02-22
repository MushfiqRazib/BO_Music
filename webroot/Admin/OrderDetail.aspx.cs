using System;
using System.Data;
using System.Web.UI.WebControls;
using Boeijenga.Business;
using System.Collections.Generic;
using Boeijenga.Common.Objects;


public partial class Admin_OrderDetail : System.Web.UI.Page
{

    DbHandler dbHandler = new DbHandler();
	string orderID="";
    double discount = 0.0;
	DataTable dtOrderStore;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            divArticle.Visible = false;
            if (Request.Params["order"] != null) 
            {
				Session["footerTable"]=null;
				Session["mainTable"] = null;
				orderID = Request.Params["order"];
				LoadInitialName();
				LoadCountryName();
				LoadInfo();
				LoadOrder(orderID);
				//LoadgrdArticle();

            }
			else{lblError.Text="";}
			//LoadValidatorText();
        }
		

    }
    protected void LoadgrdArticle(object sender, EventArgs e)
	{       
        DataTable articleTable = new Facade().GetArticleInfo();
		grdArticle.DataSource = articleTable;
		grdArticle.DataBind();

        articleTable = new Facade().GetOrderIdFromSupplyOrder();
		if (Request.Params["orderNo"] == null)
		{
			//lblPrintSupplyOrderNo.Text = articleTable.Rows[0]["orderid"].ToString();
		}
        //Page.FindControl("divArticle").Visible = true;
        if (divArticle.Visible)
            divArticle.Visible = false;
        else
            divArticle.Visible = true;
	}
	private void LoadInitialName()
	{
		ddlInitialName.Items.Add("Mr. ");
		ddlInitialName.Items.Add("Mrs. ");
		ddlInitialName.Items.Add("Dhr. ");
		ddlInitialName.Items.Add("Mevr. ");

	}
	public void LoadCountryName()
	{
        List<Country> countryList =  new Facade().GetCountry();
        if (countryList != null)
        {
            ddlCountry.DataSource = countryList;
            ddlCountry.DataValueField = "countrycode";
            ddlCountry.DataTextField = "countryname";
            ddlCountry.DataBind();
        }

	}
    // not used
	//private void LoadValidatorText()
	//{
	//    valDate.ErrorMessage = "This is not a valid date format";
	//    valDateRequired.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
	//    valCustomer.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
	//    valAddress.ErrorMessage=(string)base.GetGlobalResourceObject("string", "requiredValidMessage");
	//    valDiscount.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
	//    valHousenr.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
	//    valPostCode.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
	//    valResidence.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
	//    valShippingCost.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");

	//}
	private void LoadInfo()
	{

		string order = Request.Params["order"];

		lblOrderNo.Text += " " + order;
    
        DataTable dtInvoice = new Facade().GetInvoiceInfoByOrderId(Int32.Parse(order));
		lblCustomerInvValue.Text = dtInvoice.Rows[0]["customer"].ToString();
		lblAddressInvValue.Text = dtInvoice.Rows[0]["address"].ToString();
		//txtOrderDateValue.Text =ADate.Value;
		ADate.Text = dtInvoice.Rows[0]["orderdate"].ToString();


        DataTable dtOrder = new Facade().GetCustomerInfoByOrderId(Int32.Parse(order));
		txtHideID.Value = dtOrder.Rows[0]["customerid"].ToString();
		txtFirstName.Text = dtOrder.Rows[0]["fname"].ToString();
		txtMiddleName.Text = dtOrder.Rows[0]["mname"].ToString();
		txtLastName.Text = dtOrder.Rows[0]["lname"].ToString();
		txtHousenr.Text = dtOrder.Rows[0]["dhousenr"].ToString();
		txtAddress.Text = dtOrder.Rows[0]["daddress"].ToString();
		txtPostCode.Text = dtOrder.Rows[0]["dpostcode"].ToString();
		txtResidence.Text = dtOrder.Rows[0]["dresidence"].ToString();
		ddlCountry.SelectedValue = dtOrder.Rows[0]["dcountry"].ToString();
        //intCtrDiscount.Text = dtOrder.Rows[0]["discountpc"].ToString();
		txtRemarks.Text = dtOrder.Rows[0]["remarks"].ToString();
		//intCtrShippingCost.Text = dtOrder.Rows[0]["shippingcost"].ToString();				
	}
	protected void LoadOrder(string order)
	{
        DataTable dt = new Facade().GetOrderInfoWithCalculationByOrderId(Int32.Parse(order));
		if (Session["mainTable"]==null)
		{
			dtOrderStore=dt;
			DataColumn index;
			index = new DataColumn();
			index.DataType = System.Type.GetType("System.Int32");
			index.ColumnName = "index";
			dtOrderStore.Columns.Add(index);
		}
		else
			dtOrderStore=(DataTable)Session["mainTable"];

		double subTotal=0.0;
        double vat = 0.00;
		double grandTotal=0.0;
		for (int i = 0; i < dtOrderStore.Rows.Count; i++)
		{
            subTotal += Double.Parse(dtOrderStore.Rows[i]["TotalPrice"].ToString());
            discount += Double.Parse(dtOrderStore.Rows[i]["discountAmount"].ToString());
            vat += Double.Parse(dtOrderStore.Rows[i]["vatAmount"].ToString());
		}

		//discount = subTotal * (double.Parse(intCtrDiscount.Text) / 100);                
		lblSubTotalValue.Text = "€ " + string.Format("{0:F2}", subTotal);
        string lbltoatlVat = string.Format("{0:F2}", vat);
        vat = Convert.ToDouble(lbltoatlVat);
        grandTotal = subTotal + vat;
        lblVatValue.Text = "€ " + string.Format("{0:F2}", vat);
		//lblShippinCostAmtValue.Text = "€ " + string.Format("{0:F2}", double.Parse(intCtrShippingCost.Text));
		lblGrandTotalValue.Text = "€ " + string.Format("{0:F2}", grandTotal);

		for (int i = 0; i < dtOrderStore.Rows.Count; i++) 
		{
			DataRow row = dtOrderStore.Rows[i];
			row["index"] = i+1;
		}
		Session["mainTable"]=dtOrderStore;
		grdOrderLine.DataSource = dtOrderStore;
		grdOrderLine.DataBind();
	}

    protected void lnkSave_Click(object sender, EventArgs e)
    {
		
		bool b=SaveUpdate();
        if(b==true)
			Response.Redirect("ordermanagement.aspx");
		
    }
	private bool SaveUpdate()
	{		
		orderID = Request.Params["order"];
		int num_order = grdOrderLine.Rows.Count + 3;       

        OrderDTO objOrder = new Facade().GetOrderByOrderId(Int32.Parse(orderID));
        if (objOrder != null)
        {
            objOrder.Dhousenr = txtHousenr.Text.Trim();
            objOrder.Daddress = txtAddress.Text.Trim();
            objOrder.Dpostcode = txtPostCode.Text.Trim();
            objOrder.Dresidence = txtResidence.Text.Trim();
            objOrder.Dcountry = ddlCountry.SelectedValue.Trim();
            objOrder.Orderdate = DateTime.Parse(ADate.Text.ToString());
            objOrder.Remarks = txtRemarks.Text.Trim();
            objOrder.Orderid = Int32.Parse(orderID);
        }

        Customer objCustomer = new Facade().GetCustomerByCustomerId(Int32.Parse(txtHideID.Value.ToString().Trim()));


        if (objCustomer != null)
        {
            objCustomer.Dfirstname = txtFirstName.Text.Trim();
            objCustomer.Dmiddlename = txtMiddleName.Text.Trim();
            objCustomer.Dlastname = txtLastName.Text.Trim();
            objCustomer.Dinitialname = ddlInitialName.Text.Trim();
            objCustomer.Firstname = txtFirstName.Text.Trim();           
        }
       
        List<OrderLine> orderlines = new List<OrderLine>();
		
		foreach (GridViewRow row in grdOrderLine.Rows)
		{

            OrderLine orderline = new OrderLine();

			Label lblorderid = (Label)row.Cells[0].FindControl("lblOrderId");
            orderline.Orderid = Int32.Parse(lblorderid.Text);

			Label lblArticleCode = (Label)row.Cells[0].FindControl("lblArticleCode");
            orderline.Articlecode = lblArticleCode.Text;

			TextBox intCtrQuanity = (TextBox)row.Cells[2].FindControl("intCtrQuanity");
            orderline.Quantity = Convert.ToInt32(intCtrQuanity.Text);

			TextBox intCtrUnitPrice = (TextBox)row.Cells[4].FindControl("intCtrUnitPrice");
            orderline.Unitprice = Convert.ToDouble(intCtrUnitPrice.Text);
            
            TextBox intCtrDiscount = (TextBox)row.Cells[5].FindControl("intCtrDiscount");
            orderline.Discountpc = Convert.ToDouble(intCtrDiscount.Text);
			
            TextBox intCtrVat = (TextBox)row.Cells[6].FindControl("intCtrVat");
            orderline.Vatpc = Convert.ToDouble(intCtrVat.Text);

            orderlines.Add(orderline);           
		}
		string msg = "";

        bool b = new Facade().UpdateOrder(objOrder, orderlines, objCustomer, num_order, ref msg);
		
        if (b == false)
        {
            lblError.Text = msg;
        }
		return b;
       
	}
	protected void lnkPrint_Click(object sender, EventArgs e)
	{
		SaveUpdate();
		string order = orderID = Request.Params["order"];

		/*string script = " <script type='text/javascript'>";
		script += "window.open('printOrder.aspx?order=" + order + "','PrintOrder','toolbar=yes,menubar=yes,resizable=yes,scrollbars=yes,height=500,width=800')";
		script += "</script>";*/
		string script = " <script type='text/javascript'>";
		script += "var popUpTest=window.open('printOrder.aspx?order=" + order + "','PrintOrder','toolbar=yes,menubar=yes,resizable=yes,scrollbars=yes,height=500,width=800,status=1,location=1');";
		script += @" if(!popUpTest) {alert('Please turn off popup blocker.');}";
		script += "</script>";
		Response.Write(script);
		Response.Write(script);
		

	}
	protected void lnkDelete_Click(object sender, CommandEventArgs e)
	{
		string articleCode = e.CommandArgument.ToString();
		orderID = Request.Params["order"];
		//string sql = "delete from ordersline where articlecode='" + articleCode + "'";
		//bool b=dbHandler.ExecuteQuery(sql);
		//orderID = Request.Params["order"];
		dtOrderStore = (DataTable)Session["mainTable"];
		int index=0;
		for(int i=0;i<dtOrderStore.Rows.Count;i++)
		{
			if (dtOrderStore.Rows[i]["articlecode"].ToString().Equals(articleCode))
			{
				index=i;
			}
		}
		dtOrderStore.Rows.RemoveAt(index);
		Session["mainTable"]=dtOrderStore;
		LoadOrder(orderID);
	}
	protected void lnkCancel_Click(object sender, EventArgs e)
	{
		Session["footerTable"] = null;
		Session["mainTable"] = null;
        Response.Write("<script> parent.OBSettings.RefreshPage();</script>");
    }
	protected void lnkSelect_Command(object sender, CommandEventArgs e)
	{
		string selectedCode = e.CommandArgument.ToString();
		string button = "";
		foreach (GridViewRow row in grdArticle.Rows)
		{
			LinkButton lButton = (LinkButton)row.Cells[0].FindControl("lnkSelect");
			button = lButton.CommandArgument.ToString();
            if (button.Equals(selectedCode))
            {              
                LoadOrdersLine(selectedCode);
                break;
            }
		}
        divArticle.Visible = false;
	}
	private void LoadOrdersLine(string articlecode)
	{
        DataTable footerTable = new Facade().GetArticleByArticleCode(articlecode);

			((Label)grdOrderLine.FooterRow.FindControl("lblFooterArticleCode")).Text = footerTable.Rows[0]["articlecode"].ToString();
			((Label)grdOrderLine.FooterRow.FindControl("lblTitle")).Text = footerTable.Rows[0]["title"].ToString();
			((Label)grdOrderLine.FooterRow.FindControl("lblStock")).Text = footerTable.Rows[0]["stock"].ToString();
			((Label)grdOrderLine.FooterRow.FindControl("lblQuanityNew")).Text = footerTable.Rows[0]["qty"].ToString();
			((Label)grdOrderLine.FooterRow.FindControl("lblPriceNew")).Text = footerTable.Rows[0]["price"].ToString();
			((Label)grdOrderLine.FooterRow.FindControl("lblVatNew")).Text = footerTable.Rows[0]["vat"].ToString();

			Session["footerTable"] = footerTable;
			///*
			// * now check that if selected items already inserted into mainTable
			// * if already in the table then set the insert button's enable property
			// * to false.
			// */
			dtOrderStore = (DataTable)Session["mainTable"];
			for (int i = 0; i < dtOrderStore.Rows.Count; i++)
			{
				string code = dtOrderStore.Rows[i]["articlecode"].ToString();

				if (code.Equals(footerTable.Rows[0]["articlecode"].ToString()))
				{
					((LinkButton)grdOrderLine.FooterRow.FindControl("lnkInsert")).Visible = false;
					return;
				}
			}
			////if the item is unique to already selected then enable the insert button
			((LinkButton)grdOrderLine.FooterRow.FindControl("lnkInsert")).Visible = true;
	}
	protected void lnkInsert_Click(object sender, EventArgs e)
	{
		orderID = Request.Params["order"];
		if(Session["footerTable"]!=null)
		{
			dtOrderStore=(DataTable)Session["mainTable"];
			DataTable dt=(DataTable)Session["footerTable"];
			
			string qty = dt.Rows[0]["qty"].ToString();
			string price = dt.Rows[0]["price"].ToString();
			string vatpc = dt.Rows[0]["vat"].ToString();
            string discount = dt.Rows[0]["discount"].ToString();
			string artCode = dt.Rows[0]["articlecode"].ToString();
			if (!OrderExist(artCode))
			{
				DataRow dr = dtOrderStore.NewRow();
				dr["orderid"] = orderID;
				dr["articlecode"] = artCode;
				dr["title"] = dt.Rows[0]["title"].ToString();
				dr["quantity"] =qty;
				dr["stock"] = dt.Rows[0]["stock"].ToString();
				dr["unitprice"] =price ;
				dr["vatpc"] = vatpc;
                dr["discountAmount"] = discount;
                dr["vatAmount"] = "0.00";
                dr["TotalPrice"] = (Double.Parse(qty) * Double.Parse(price)) - Double.Parse(vatpc);
				dtOrderStore.Rows.Add(dr);
				Session["mainTable"] = dtOrderStore;
				LoadOrder(orderID);
			}
		}
	}
	private bool OrderExist(string artCode)
	{
		bool state = false;
		dtOrderStore = (DataTable)Session["mainTable"];
		foreach(DataRow dr in dtOrderStore.Rows)
		{
			if(dr["articlecode"].ToString().Equals(artCode))
			{
				state=true;
				break;
			}
		}
		return state;
	}
}
