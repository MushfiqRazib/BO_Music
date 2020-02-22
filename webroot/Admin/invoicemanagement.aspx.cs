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
using Boeijenga.DataAccess.Constants;
using Boeijenga.Business;
using System.Collections.Generic;
using Boeijenga.Common.Objects;

public partial class Admin_invoicemanagement : System.Web.UI.Page
{
   
	DbHandler dbHandler=new DbHandler ();
    protected void Page_Load(object sender, EventArgs e)
    {	
		Session["selectedinvoice"]=null;
        if (!IsPostBack)
        {

            LoadInvoice(drpInvoiceFilter.SelectedValue.ToString());
        }
    }
	/*
	 * Event Handler for Print an Invoice
	 * Author:Shahriar
	 * Date:15-7-07
	 */
	protected void lnkPrint_Command(object sender, CommandEventArgs e)
	{
        string credit = string.Empty;
        string invoiceId = e.CommandArgument.ToString();
        foreach (GridViewRow row in grdInvoice.Rows)
        {
            LinkButton lBtn = (LinkButton)row.Cells[2].FindControl("lnkSelect");
            if (lBtn!=null && lBtn.CommandArgument.Equals(invoiceId))
            {
                credit = row.Cells[6].Text.Equals("&nbsp;") ? string.Empty : row.Cells[6].Text;
                break;
            }
        }
		ArrayList array=new ArrayList();
		array.Add(invoiceId);
		Session["selectedinvoice"]=array;
		string script = " <script type='text/javascript'>";
		script += "var popUpTest=window.open('InvoiceEnglish.aspx?credit="+credit+"','PrintInvoice','toolbar=yes,menubar=yes,resizable=yes,scrollbars=yes,height=500,width=800,status=1,location=1');";
		script += @" if(!popUpTest) {alert('Please turn off popup blocker.');}";
		script += "</script>";


        if (drpAction.SelectedValue.Equals("1")) //Afdrukken selected	
        {
            Session["blank"] = "false";
            Session["cultureName"] = "nl-NL";
            Session["selectedinvoice"] = array;
            Response.Write(script);
        }
        else if (drpAction.SelectedValue.Equals("3"))	//Factuur selected
        {
            Session["blank"] = "false";
            Session["cultureName"] = "en-US";
            Session["selectedinvoice"] = array;
            Response.Write(script);
        }
        else if (drpAction.SelectedValue.Equals("2")) //Afdrukken Faktuur Blank	
        {
            Session["blank"] = "true";
            Session["cultureName"] = "nl-NL";
            Session["selectedinvoice"] = array;
            Response.Write(script);
        }
        else if (drpAction.SelectedValue.Equals("4"))	//Factuur selected Blank
        {
            Session["blank"] = "true";
            Session["cultureName"] = "en-US";
            Session["selectedinvoice"] = array;
            Response.Write(script);
        }


		//Response.Write(script);
		//Response.Redirect("InvoiceEnglish.aspx");
	}
	/*
	 * Event Handler for Edit an Invoice
	 * Author:Shahriar
	 * Date:13-7-07
	 */
	protected void lnkEdit_Command(object sender, CommandEventArgs e)
	{
		string invoiceId = e.CommandArgument.ToString();
		Response.Redirect("editInvoice.aspx?inv=" + invoiceId);
	}
	/*
	 * Event Handler for click Yes[MessageBox]
	 * Author:Shahriar
	 * Date:12-7-07
	 */
	protected void btnYes_Click(object sender, System.EventArgs e)
	{
		string Factuurnr = "";
		
		foreach (GridViewRow row in grdInvoice.Rows)
		{
			CheckBox cb = (CheckBox)row.FindControl("chkInvoice");
			if ((cb != null) && cb.Checked)
			{
				Label lbl=(Label)row.FindControl("lblInvoiceNr");
				Factuurnr +=lbl.Text+ ",";
			}
		}
		
		if (Factuurnr != "")
		{
			Factuurnr = Factuurnr.Substring(0, Factuurnr.Length - 1);
			if (drpInvoiceFilter.SelectedIndex == 1 || drpAction.SelectedIndex == 1) //Afdrukken Invoice selected	
			{
				Response.Write("<script>window.open('InvoiceEnglish.aspx?Factuurnr=" + Factuurnr + "');</script>");
				//Response.Redirect("InvoiceEnglish.aspx?Factuurnr=" + Factuurnr);
			}
			else if (drpInvoiceFilter.SelectedIndex == 0 || drpAction.SelectedIndex == 0)	//Afdrukken Factuur selected
			{
				Response.Write("<script>window.open('InvoiceDutch.aspx?Factuurnr=" + Factuurnr + "');</script>");
				//Response.Redirect("InvoiceDutch.aspx?Factuurnr=" + Factuurnr);
			}

		}
		HideMsgBox("msgbox");
		//ShowMsgBox("msgbox2");
	}
	/*
	 * Event Handler for Click No [Message]
	 * Author:Provas
	 */
	protected void btnNo_Click(object sender, System.EventArgs e)
	{
		HideMsgBox("msgbox");
	}
	/*
	 * Event Handler for click Yes[MessageBox2]
	 * Author:Shahriar
	 * Date:12-7-07
	 */
	protected void btnYes2_Click(object sender, System.EventArgs e)
	{
		string qryUpdate = "";
		DateTime date = DateTime.Now;
		string curDate = date.Year + "-" + date.Month + "-" + date.Day;
		foreach (GridViewRow row in grdInvoice.Rows)
		{
			CheckBox cb = (CheckBox)row.FindControl("chkInvoice");
			if ((cb != null) && cb.Checked)
			{
				Label lbl = (Label)row.FindControl("lblInvoiceNr");
                new Facade().UpdateInvoiceByDateandInvoiceNo(curDate, lbl.Text);
				//string sql= "UPDATE invoice SET printed='" + curDate + "' WHERE invoicenr=" +lbl.Text;
				//dbHandler.ExecuteQuery(sql);
			}
		}
		
	}
	/*
	 * Event Handler for Click No [Message2]
	 * Author:Provas
	 */
	protected void btnNo2_Click(object sender, System.EventArgs e)
	{
		HideMsgBox("msgbox2");
	}
	/*
	 * Event Handler for Button OK [Action]
	 * Author:Shahriar
	 * Date:12-7-07
	 */
	protected void  btnAction_Click(object sender, ImageClickEventArgs e)
	{
		
		ArrayList array = new ArrayList();
        System.Text.StringBuilder credit = new System.Text.StringBuilder();
        string symbol = "";
		//Check # of selected items
		foreach (GridViewRow row in grdInvoice.Rows)
		{            
			CheckBox cb = (CheckBox)row.FindControl("chkInvoice");
			Label lblInvoiceNr = (Label)row.FindControl("lblInvoiceNr");
			if (cb.Checked)
			{
                LinkButton lBtn = (LinkButton)row.Cells[2].FindControl("lnkSelect");
                if (lBtn != null)
                {
                    credit.Append(symbol);
                    credit.Append(row.Cells[6].Text.Equals("&nbsp;") ? string.Empty : row.Cells[6].Text);
                    symbol = ",";
                }
				array.Add(lblInvoiceNr.Text);

			}
		}
        
		if(array.Count>0)
		{
			string script = " <script type='text/javascript'>";
            script += "window.open('InvoiceEnglish.aspx?credit=" + credit.ToString() + "','PrintInvoice','toolbar=yes,menubar=yes,resizable=yes,scrollbars=yes,height=500,width=800,status=1,location=1')";
            
			script += "</script>";

			if (drpAction.SelectedValue.Equals("1")) //Afdrukken selected	
			{
                Session["blank"] = "false";
                Session["cultureName"] = "nl-NL";
				Session["selectedinvoice"] = array;
				Response.Write(script);
			}
			else if (drpAction.SelectedValue.Equals("3"))	//Factuur selected
			{
                Session["blank"] = "false";
                Session["cultureName"] = "en-US";
				Session["selectedinvoice"] = array;
				Response.Write(script);
			}
            else if (drpAction.SelectedValue.Equals("2")) //Afdrukken Faktuur Blank	
            {
                Session["blank"] = "true";
                Session["cultureName"] = "nl-NL";
                Session["selectedinvoice"] = array;
                Response.Write(script);
            }
            else if (drpAction.SelectedValue.Equals("4"))	//Factuur selected Blank
            {
                Session["blank"] = "true";
                Session["cultureName"] = "en-US";
                Session["selectedinvoice"] = array;
                Response.Write(script);
            }
            else if (drpAction.SelectedValue.Equals("6"))	//Factuur selected Blank
            {
                DoNew(array);
            }
			//Response.Redirect("InvoiceEnglish.aspx");
		}
		//No item selected
		else 
		{
			Page.RegisterStartupScript("required", "<script language='javascript'> alert('Check Box Selection Required');</script>");
		}
	}
	/*
	 * Event Handler for Button OK [Filter]
	 * Author:Shahriar
	 * Date:11-7-07
	 */
	protected void btnOrderFilter_Click(object sender, ImageClickEventArgs e)
	{
		LoadInvoice(drpInvoiceFilter.SelectedValue.ToString());
	}
	/*
	 * Event handler for invoice # selection
	 * Author:Shahriar
	 * Date:11-7-07
	 */
	
	protected void lnkSelect_Command(object sender, CommandEventArgs e)
    {
		string invoiceId = e.CommandArgument.ToString();
		SetRowColor(grdInvoice, "lblInvoiceNr", invoiceId, "#8B9BBA");
		LoadInvoiceLine(invoiceId);
	}
	/*
	 * Event handler for Paging
	 * Author:Shahriar
	 * Date:11-7-07
	 */
	protected void grdInvoice_PageChanging(object sender, GridViewPageEventArgs e)
	{
		grdInvoice.PageIndex = e.NewPageIndex;
		LoadInvoice(drpInvoiceFilter.SelectedValue.ToString());
	}
	/*
	 * Event handler for Sorting
	 * Author:Shahriar
	 * Date:11-7-07
	 */
	protected void grdInvoicer_Sorting(object sender, GridViewSortEventArgs e)
	{
		string sort = "";
		string sortDirection="";
		if (Session["sortDirectionInv"] == null)
		{
			sort = e.SortExpression.ToString();
			Session["sortDirectionInv"] = "Desc";
			//Session["sort"]=sort;
			Session["sortExpressionInv"] = sort;

		}
		else
		{
			sortDirection = Session["sortDirectionInv"].ToString();
			sort = Session["sortExpressionInv"].ToString().ToLower();
			/*
			 * 
			 */
			if (e.SortExpression.ToString().ToLower().Equals(sort))
			{
				if (sortDirection.Equals("Desc"))
				{
					Session["sortDirectionInv"] = "Asc";
					Session["sortExpressionInv"] = sort;
				}
				else
				{
					Session["sortExpressionInv"] = sort;
					Session["sortDirectionInv"] = "Desc";
				}
			}
			else
			{
				Session["sortDirectionInv"] = "Desc";
				Session["sortExpressionInv"] = e.SortExpression.ToString();
			}


		}
		LoadInvoice(drpInvoiceFilter.SelectedValue.ToString());

	}
	/*------------------Function Area--------------*/

	/*
	 * Function for Hideing Confirmation Message Box
	 * Author:Provas
	 */
	private void HideMsgBox(string id)
	{
		string expScript = @"<script language='javascript'>";
		expScript += id + @".style.visibility = 'hidden';";
		expScript += @"</script>";
		RegisterStartupScript("Hide", expScript);
	}

	/*
	 * Function for display Confirmation Message Box
	 * Author:Provas
	 */
	private void ShowMsgBox(string id)
	{
		string expScript = @"<script language='javascript'>";
		expScript += id + @".style.visibility = 'visible';";
		expScript += @"</script>";
		this.RegisterStartupScript("Show", expScript);
	}
	
	private void LoadInvoice(string status)
	{

        DataTable dt = new Facade().GetInvoiceReportByInvoiceStatus(status, Session["sortExpressionInv"], Session["sortDirectionInv"]);
       
        grdInvoice.DataSource = dt;
		grdInvoice.DataBind();
	}
    /// <summary>
    /// Sql modified by Provas
    /// reason: make one query to handle both invoice AND creditinvoice
    /// date: 12-12-08
    /// </summary>
    /// <param name="invoiceId"></param>
    private void LoadInvoiceLine(string invoiceId)
	{

       
       
        /*
        string sql = "select il.invoiceid,ol.orderid,ol.articlecode," +
                    "(" +
                    "select '<b>'||title ||'</b><br>'||" +
                    "(case when lower(articletype)='c' then 'CD/DVD' when lower(articletype)='b' then 'Book' when lower(articletype)='s' then 'SheetMusic' when lower(articletype)='h' then 'S&H'  end)||'<br>'||" +
                    "(select coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'') from composer c,article a where c.composerid=a.composer and a.articlecode=ol.articlecode)" +
                    "from article" +
                    " where articlecode=ol.articlecode" +
                    ") as Article," +
                    "ol.unitprice," +
                    " ol.quantity, article.quantity as stock," +
                    "(ol.unitprice * ol.quantity) as totalprice" +
                    " from ordersline ol,invoiceline il,invoice i" +
                    " where il.invoiceid=i.invoiceid" +
                    " and il.orderid=ol.orderid" +
                    " and ol.articlecode = article.articlecode" +
                    " and ol.orderid in (select orderid from invoiceline where invoiceid='" + invoiceId + "') and i.invoiceid='" + invoiceId + "'" +
                    "order by il.orderid";
        */

        DataTable dtInvoiceLine = new Facade().GetInvoiceLineReportByInvoiceId(invoiceId);

		/*
		 * Removed 12-7-07
		 * Chenge Request Made By Mr.Asad
		 * 
		//Add a new Column SL#
		DataColumn dtColInvoiceLine=new DataColumn("SL");
		dtColInvoiceLine.DataType = System.Type.GetType("System.String");
		dtInvoiceLine.Columns.Add(dtColInvoiceLine);
		int index=1;
		for (int i = 0; i < dtInvoiceLine.Rows.Count; i++)
		{
			DataRow row = dtInvoiceLine.Rows[i];
			if(i==0) //Check if first row then no check start indexing
				row["SL"] = index;
			else
			{
				if(dtInvoiceLine.Rows[i]["orderid"].ToString().Equals(dtInvoiceLine.Rows[i-1]["orderid"].ToString()))
					row["SL"] ="";
				else
					row["SL"] = ++index;
			}
		}
		*/

		// maping and bind the datasource  to display data in Grid
		grdInvoiceLine.DataSource = dtInvoiceLine;
		grdInvoiceLine.DataBind();
	}
	/*
	 * Function for changing color on invoiceid selection
	 * Author:Provas
	 */
	private void SetRowColor(GridView grd, string type, string code, string color)
	{
		foreach (GridViewRow row in grd.Rows)
		{
			Label id = (Label)row.Cells[0].FindControl(type);
			if (id.Text.ToString().Equals(code))
			{
				row.BackColor = System.Drawing.ColorTranslator.FromHtml(Functions.selectedColor);
			}
			else if (row.RowState.ToString().ToLower().Equals("normal"))
			{
				row.BackColor = System.Drawing.ColorTranslator.FromHtml(Functions.normalColor);

			}
			else if (row.RowState.ToString().ToLower().Equals("alternate"))
			{
				row.BackColor = System.Drawing.ColorTranslator.FromHtml(Functions.alternateColor);

			}
		}
	}
    private void DoNew(ArrayList orderList)
    {
        ArrayList invalidOrders = new ArrayList();
        string msg = "";
        for (int i = 0; i < orderList.Count; i++)
        {
            string order = (string)orderList[i];

            new Facade().UpdateInvoiceByInvoiceId(order);
           // string sql = "update invoice set invoicestatus='2' where invoiceid='" + order + "'";
          //  bool b = dbHandler.ExecuteQuery(sql);
        }
        if (invalidOrders.Count == 0)
            lblMessage.Text = "";
        else
        {
            for (int i = 0; i < invalidOrders.Count; i++)
            {
                msg += invalidOrders[i].ToString() + ",";
            }
            msg = msg.Substring(0, msg.Length - 1);
            lblMessage.Text = "Order# " + msg + " failed to make into ready state.";
        }
        LoadInvoice(drpInvoiceFilter.SelectedValue.ToString());
    }
}
