using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using Boeijenga.Common.Objects;
using Boeijenga.Business;
using System.Collections.Generic;

public partial class Admin_InvoiceDetail : System.Web.UI.Page
{
    static public string[] orderfordelete = new string[30];
    
    int i = 0;
	
	DbHandler dbHandler = new DbHandler();
    protected void Page_Load(object sender, EventArgs e)
    {
		if(!IsPostBack)
		{
			if (Request.Params["inv"] != null)
			{
				string invoiceId = Request.Params["inv"];
				LoadCountryName();
				LoadInvoiceLineInfo(invoiceId);
				LoadInvoiceLine(invoiceId);
                SetObjectEnability();
			}
            string confirmMsg = "Are you sure you want to delete this order?";
            lnkDelete.Attributes.Add("onclick", "return confirm('" + confirmMsg + "');");
		}
		else
		{
            lblErrorMsg.Text = "";
		}
    }

    private void SetObjectEnability()
    {
        bool enable = bool.Parse(ViewState["isCredited"].ToString());
        //lnkCreditInvoice.Visible = !enable;
    }


	/*---------------Evant Handler------------------*/
	/*
	 * Event Handler for delete link
	 * Author:Shahriar
	 * Date:15-7-07
	 */
	protected void lnkDelete_Click(object sender, CommandEventArgs e)
	{
		//Each Invoice should have at least one order item
        if(grdInvoiceLine.Rows.Count>1)
		{           
			string articleCode = e.CommandArgument.ToString();
            new Facade().DeleteOrderlineByArticleCode(articleCode);
			string invoiceId = Request.Params["inv"];
			LoadInvoiceLineInfo(invoiceId);
			LoadInvoiceLine(invoiceId);		
		}
		//If only one order item exists and try to delete that raise Error
		else
		{
			lblErrorMsg.Text ="Sorry!! Each Invoice shold have at least one order item.";
		}
	}
	/*
	 * Event Handler for Print Button
	 * Author:Shahriar
	 * Date:15-7-07
	 */
	protected void lnkPrint_Click(object sender, EventArgs e)
	{
		string invoiceId = "";
		if (Request.Params["inv"] != null)
		{
			invoiceId = Request.Params["inv"];
			SaveInvoiceDetails(invoiceId);
			ArrayList array = new ArrayList();
			array.Add(invoiceId);
			Session["selectedinvoice"] = array;
			string script = " <script type='text/javascript'>";
			script += "var popUpTest=window.open('InvoiceEnglish.aspx','PrintInvoice','toolbar=yes,menubar=yes,resizable=yes,scrollbars=yes,height=500,width=800,status=1,location=1');";
            script += @" if(!popUpTest) {alert('Please turn off popup blocker.');}";
			script += "</script>";
			Response.Write(script);
		}
	}
	/*
	 * Event Handler for save Button
	 * Author:Shahriar
	 * Date:12-7-07
	 */
	protected void lnkSave_Click(object sender, EventArgs e)
	{
		string invoiceId="";
		if (Request.Params["inv"] != null)
		{
			invoiceId = Request.Params["inv"];
			if(SaveInvoiceDetails(invoiceId))
				Response.Redirect("invoicemanagement.aspx");
		}
	}
	/*
	 * Event Handler for cancel Button
	 * Author:Shahriar
	 * Date:12-7-07
	 */
	protected void lnkCancel_Click(object sender, EventArgs e)
	{
        Response.Write("<script> parent.OBSettings.RefreshPage();</script>");
    }
	/*--------------- Function Area-----------------*/
	/*
	 * Function for saving changed values
	 * Author:Shahriar
	 * Date:12-7-07
	 */
	private bool SaveInvoiceDetails(string invoiceId)
	{
        int num = grdInvoiceLine.Rows.Count + 1;       
        string errorMsg = "";

        Invoice objInvoice = new Facade().GetInvoiceByInvoiceId(invoiceId);

        if (objInvoice != null)
        {
            objInvoice.Customerbtwnr = txtCustBTWValue.Text.Trim();
            objInvoice.Housenr = txtHouseNr.Text.Trim();
            objInvoice.address = txtAddress.Text.Trim();
            objInvoice.Postcode = txtPostCode.Text.Trim();
            objInvoice.Residence = txtResidence.Text.Trim();
            objInvoice.Country = ddlCountry.SelectedValue.ToString();
            
        }
		
		System.Globalization.CultureInfo enUS = new System.Globalization.CultureInfo("en-US", true);
		System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.DateTimeFormatInfo();
		dtfi.ShortDatePattern = "dd-MM-yyyy";
		dtfi.DateSeparator = "-";

		DateTime dtIn = Convert.ToDateTime(txtInvoiceDate.Text.Trim(), dtfi);

        objInvoice.Invoicedate = DateTime.Parse(dtIn.ToString("yyyy-MM-dd"));
        objInvoice.Invoicestatus = drpStatus.SelectedValue.ToString();
      
        List<OrderLine> orderLines = new List<OrderLine>();
		foreach (GridViewRow row in grdInvoiceLine.Rows)
		{
            OrderLine orderLine = new OrderLine();
			Label lblOrder = (Label)row.Cells[0].FindControl("lblOrder");
			string order = lblOrder.Text;
            orderLine.Orderid = Int32.Parse(order.ToString());
			Label lblArticleID = (Label)row.Cells[0].FindControl("lblArticleID");
			string article = lblArticleID.Text;
            orderLine.Articlecode = article;
			TextBox intCtrQuanity = (TextBox)row.Cells[2].FindControl("intCtrQuanity");
            if (bool.Parse(ViewState["isCredited"].ToString()).Equals(false))
            {
                orderLine.Quantity = Convert.ToInt32(intCtrQuanity.Text.Replace(',', '.'));
            }
            else
            {
                orderLine.Creditedquantity = Convert.ToInt32(intCtrQuanity.Text.Replace(',', '.'));
            }
			TextBox intCtrUnitPrice = (TextBox)row.Cells[4].FindControl("intCtrUnitPrice");
            orderLine.Unitprice = Convert.ToDouble(intCtrUnitPrice.Text.Replace(',', '.'));
			TextBox intCtrVat= (TextBox)row.Cells[4].FindControl("intCtrVat");
            orderLine.Vatpc = Convert.ToDouble(intCtrVat.Text.Replace(',', '.'));
			//DecimalControl intCtrDiscount= (DecimalControl)row.Cells[4].FindControl("intCtrDiscount");
           
            orderLine.Unitprice = double.Parse(intCtrUnitPrice.Text.Replace(',', '.'));
            orderLine.Vatpc = double.Parse(intCtrVat.Text.Replace(',', '.'));

            orderLines.Add(orderLine);
		}
        bool b = new Facade().UpdateInvoice(objInvoice, orderLines, bool.Parse(ViewState["isCredited"].ToString()), num, ref errorMsg);
        if (b == false)
        {
            if (errorMsg.Contains("23514"))
            {
                errorMsg = "Credited quantity cannot be greater than Ordered quantity";
            }
            lblErrorMsg.Text = errorMsg;
        }
		return b;
	}
	/*
	 * Function For Populating calculated Values
	 * Author:Shahriar
	 * Date:12-7-07
	 */
	private void LoadCalculated(DataTable dt,string invoiceId)
	{
        Double totalShippingCost = 0.00;
        Double totalDiscount = 0.00;
        Double totalVat = 0.00;
        Double subTotal = 0.00;
        Double netPrice = 0.00;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            subTotal += Double.Parse(dt.Rows[i]["totalprice"].ToString());
            totalDiscount += Double.Parse(dt.Rows[i]["discountAmount"].ToString());
            totalVat += double.Parse(dt.Rows[i]["vatAmount"].ToString());
        }
        netPrice = subTotal + totalVat;        
        lblNetPriceValue.Text = "€ " + string.Format("{0:F2}", netPrice);

		lblTotalDiscountValue.Text = "€ " + string.Format("{0:F2}", totalDiscount);
		lblVATValue.Text = "€ " + string.Format("{0:F2}", totalVat);

        // code@provas on 04-12-2008
        // the following commented lines are not needed as we are not displaying shippingcost
        //string sql="select sum(o.shippingcost) as total from orders o"+
        //            " where o.orderid in(select orderid from invoiceline where invoiceid='" + invoiceId + "')";

        //DataTable dtShipCost=dbHandler.GetDataTable(sql);
        //totalShippingCost = Double.Parse(dtShipCost.Rows[0]["total"].ToString() != "" ? dtShipCost.Rows[0]["total"].ToString() : "0");
		//lblTotalShippingCostValue.Text = "€ " + string.Format("{0:F2}", totalShippingCost);        
		
	}
	/*
	 * Function For Bound to Grid
	 * Author:Shahriar
	 * Date:12-7-07
	 */
	private void BindGrid(DataTable dt)
	{
        string defaultValues = "";		
        foreach (DataRow dr in dt.Rows)
        {
            defaultValues += dr["quantity"].ToString() + ",";
        }
        RegisterClientScriptBlock("defaultValue","<script type='text/javascript'>var defaultValues='"+defaultValues.TrimEnd(',')+"';</script>");
        
        grdInvoiceLine.DataSource = dt;
        grdInvoiceLine.DataBind();
	}
	/*
	 * Function for populating Country Name in Drop Down List
	 * Author:Shahriar
	 * DAte:12-7-07
	 */
	public void LoadCountryName()
	{
        List<Country> countryList = new Facade().GetCountry();
        if (countryList != null)
        {
            ddlCountry.DataSource = countryList;
            ddlCountry.DataValueField = "countrycode";
            ddlCountry.DataTextField = "countryname";
            ddlCountry.DataBind();
        }
	}
	
	/*
	 * Load Invoiceline info According to Invoice ID
	 * Author:Shahriar
	 * Date:13-7-07
	 */
	
	private void LoadInvoiceLineInfo(string invoiceId)
	{      
		DataTable dtInvoiceLineInfo = new Facade().GetInvoiceLineInfoByInvoiceId(invoiceId);
		lblInvoiceNoValue.Text = dtInvoiceLineInfo.Rows[0]["invoiceid"].ToString();
		txtInvoiceDate.Text = dtInvoiceLineInfo.Rows[0]["invoicedate"].ToString();
		lblCustomerValue.Text = dtInvoiceLineInfo.Rows[0]["customer"].ToString();
		txtCustBTWValue.Text = dtInvoiceLineInfo.Rows[0]["customerbtwnr"].ToString();
		txtHouseNr.Text = dtInvoiceLineInfo.Rows[0]["housenr"].ToString();
		txtAddress.Text = dtInvoiceLineInfo.Rows[0]["address"].ToString();
		txtPostCode.Text = dtInvoiceLineInfo.Rows[0]["postcode"].ToString();
		txtResidence.Text = dtInvoiceLineInfo.Rows[0]["residence"].ToString();
		ddlCountry.SelectedValue = dtInvoiceLineInfo.Rows[0]["country"].ToString();
		drpStatus.SelectedValue = dtInvoiceLineInfo.Rows[0]["invoicestatus"].ToString();
		DataTable dt=LoadInvoiceLine(invoiceId);
		LoadCalculated(dt, invoiceId);
		BindGrid(dt);
        ViewState["isCredited"] = dtInvoiceLineInfo.Rows[0]["credit"].ToString().Equals(string.Empty)?false:true;
		
	}
    /// <summary>
    /// Sql modified by Provas
    /// reason: made one query to handle both invoice AND creditinvoice
    /// date: 12-12-08
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <returns></returns>
	private DataTable LoadInvoiceLine(string invoiceId)
	{
        DataTable dtInvoiceLine = new Facade().GetInvoiceLineByInvoiceId(invoiceId);
        //orderfordelete.
        foreach (DataRow r in dtInvoiceLine.Rows)
        {
            orderfordelete[i] = r["orderid"].ToString();
            i += 1;
        }
        Session["looporder"] = i;
        //Session["ordernumber"] = orderfordelete;
		return dtInvoiceLine;
		// maping and bind the datasource  to display data in Grid		
	}
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        string sql;
        bool b;
        string invoice = lblInvoiceNoValue.Text.ToString();

        if (bool.Parse(ViewState["isCredited"].ToString()))
        {
            string arguments = "";
            foreach (GridViewRow row in grdInvoiceLine.Rows)
            {
                arguments += GetItemInfo(row);
            }
            if (!arguments.Equals(string.Empty))
            {
               new Facade().CallSPDeletedCreditInvoice(invoice, arguments);
               // sql = "SELECT proc_deletecreditinvoice(" + invoice + ",array[" + arguments.TrimEnd(',') + "])";
               //dbHandler.ExecuteQuery(new NpgsqlCommand(sql));
            }
        }
        else
        {
            i = Convert.ToInt32(Session["looporder"]);
            //orderfordelete = Session["ordernumber"].ToString();
            //for (int z = 0; z < (i / 2); z++)
            //{
               // sql = "update orders set orderstatus='2' where orderid='" + orderfordelete[z] + "'";
                new Facade().UpdateOrderByOrderIdandOrderStatus(orderfordelete , i);

                //b = dbHandler.ExecuteQuery(sql);
           // }

                new Facade().DeleteFromInvoicebyInvoiceId(invoice);
          //  sql = "delete from invoice where invoiceid='" + invoice + "'";
           // b = dbHandler.ExecuteQuery(sql);

               new Facade().DeleteFromInvoiceLinebyInvoiceId(invoice);
          //  sql = "delete from invoiceline where invoiceid='" + invoice + "'";
          //  b = dbHandler.ExecuteQuery(sql);

        }

        Response.Redirect("invoicemanagement.aspx");
    }

    protected void lnkCreditInvoice_Click(object sender, EventArgs e)
    {
        string arguments = "";
        string errorMsg = "";
        foreach (GridViewRow row in grdInvoiceLine.Rows)
        {
            arguments += GetItemInfo(row);
        }
        if (!arguments.Equals(string.Empty))
        {
           // string query = "SELECT proc_insertcreditinvoice(" + lblInvoiceNoValue.Text + ",array[" + arguments.TrimEnd(',') + "])";
            //dbHandler.ExecuteQuery();

            bool b = new Facade().CallSPInsertCreditInvoice(lblInvoiceNoValue.Text, arguments, ref errorMsg);
            if (b == false)
            {
                if (errorMsg.Contains("23514"))
                {
                    errorMsg = "Credited quantity cannot be greater than Ordered quantity";
                }
                lblErrorMsg.Text = errorMsg;
            }
            else
            {
                Response.Redirect("invoicemanagement.aspx");
            }
        }
        else
        {
            lblErrorMsg.Text = "Nothing to credit here! ";
        }
    }

    private string GetItemInfo(GridViewRow row)
    {
        TextBox intCtrQuanity = (TextBox)row.FindControl("intCtrQuanity");
        if (int.Parse(intCtrQuanity.Text) > 0)
        {
            Label lblOrder = (Label)row.FindControl("lblOrder");
            Label lblArticleID = (Label)row.FindControl("lblArticleID");
            return "['" + lblOrder.Text + "','" + lblArticleID.Text + "','" + intCtrQuanity.Text + "'],";
        }
        else
            return string.Empty;
    }
}
