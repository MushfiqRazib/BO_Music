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
using Npgsql;

public partial class Admin_stockmanagement : System.Web.UI.Page
{

    DbHandler handler = new DbHandler();
	string initialOrder = "sdate desc";     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            LoadSession();
            if (Session["receivingType"] != null)
            {
                ddlReceivingType.SelectedValue = Session["receivingType"].ToString();
            }
            else
            {
                Session["receivingType"] = ddlReceivingType.SelectedValue.ToString();
            }
            LoadGrdStockOrder(ddlReceivingType.SelectedValue.ToString(),initialOrder);
        }		
		
    }
	/*
	 * Event Handler for Print the details of an supply order
	 * Author:Shahriar
	 * Date:24-7-07
	 */
	protected void lnkPrint_Command(object sender, CommandEventArgs e)
	{

		//string orderId = e.CommandArgument.ToString();
		string script = " <script type='text/javascript'>";
		script += "window.open('stockPrint.aspx?orderNo=" + e.CommandArgument.ToString() + "','PrintInvoice','toolbar=yes,menubar=yes,resizable=yes,scrollbars=yes,height=500,width=800,status=1,location=1')";
		script += "</script>";
		Response.Write(script);
		
	}
	protected void lnkEdit_Command(object sender, CommandEventArgs e)
	{

		//string orderId = e.CommandArgument.ToString();
		Response.Redirect("supplyorder.aspx?orderNo=" + e.CommandArgument.ToString());
	}
	protected void lnkReceiving_Command(object sender, CommandEventArgs e)
	{

		//string orderId = e.CommandArgument.ToString();
		Response.Redirect("receiveorders.aspx?soid=" + e.CommandArgument.ToString());
	}
    private void LoadSession()
    {
        if (Session["stockOrder"] == null)
        {
            Session["stockOrder"] = initialOrder; 
        }
        //if (Session["stockOrderLine"] == null)
        //{
        //    Session["stockOrderLine"] = "a.title asc"; ;
        //}       
    }

    private void LoadGrdStockOrder(string type, string order)
    {
		string query="";
		query = @"select to_char(s.supplyorderdate,'dd-mm-yyyy') as sdate, s.supplyorderid as orderno, " +
                       " COALESCE(p.firstname,'')||' '|| COALESCE(p.middlename,'')||' '||COALESCE(p.lastname,'') as supplier," +
                       " to_char(s.deliverydate,'dd-mm-yyyy') as ddate, (case when s.receivingstatus = 'N' then 'Not Received' "+
                       " when s.receivingstatus = 'P' then 'Partially Received' when s.receivingstatus = 'F' then 'Fully Received' end) as rstatus, "+
                       "(case when s.paymentstatus = 'U' then 'Unpaid'when s.paymentstatus = 'P' then 'Partial Paid' when s.paymentstatus = 'F' then 'Full Paid' end) as pstatus from supplyorders s, publisher p ";
		if(!type.Equals("A"))
			query += " where s.supplierid = p.publisherid and s.receivingstatus = '" + type + "' order by " + order;
		else
			query += " where s.supplierid = p.publisherid  order by " + order;	

        DataTable dt = handler.GetDataTable(query);
        grdStockOrder.DataSource = dt;
        grdStockOrder.DataBind();
        grdSupplyOrderLine.DataSource = null;
        grdSupplyOrderLine.DataBind();
        lblOrderDetails.Visible = false;
    }



    protected void btnOrderFilter_Click(object sender, ImageClickEventArgs e)
    {
        Session["receivingType"] = ddlReceivingType.SelectedValue.ToString();
        LoadGrdStockOrder(Session["receivingType"].ToString(), initialOrder);
    }
    protected void grdStockOrder_Sorting(object sender, GridViewSortEventArgs e)
    {
        string order = "";
        if (Session["stockOrder"] == null)
        {
            order = e.SortExpression.ToString() + " " + "desc";
        }

        else
        {
            string[] splitter = Session["stockOrder"].ToString().Split(' ');
            order = splitter[splitter.Length - 1];
            if (order.Equals("desc"))
            {
                order = e.SortExpression.ToString() + " " + "asc";
            }
            else
            {
                order = e.SortExpression.ToString() + " " + "desc";
            }
        }
        Session["stockOrder"] = order;

        LoadGrdStockOrder(Session["receivingType"].ToString(), order);
        
    }
    protected void lnkSelect_Command(object sender, CommandEventArgs e)
    {
        string orderNo = e.CommandArgument.ToString();
		colorSelection(grdStockOrder, "lblOrderId", orderNo, "#8B9BBA");
        Session["orderNo"] = orderNo;
        LoadGrdStockOrderLines(orderNo);
        ChangeDdlPaymentStatus(orderNo);        
       
    }

    private void ChangeDdlPaymentStatus(string orderNo)
    {
        string query = @"select paymentstatus from supplyorders" +
                        " where supplyorderid = '" + orderNo + "'";
        DataTable dt = handler.GetDataTable(query);
        ddlPaymentStatus.SelectedValue = dt.Rows[0]["paymentstatus"].ToString();
    }
	protected void lnkNew_Click(object sender, EventArgs e)
	{
		Response.Redirect("supplyorder.aspx");
	}
	private void colorSelection(GridView grd, string type, string code, string color)
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
    private void LoadGrdStockOrderLines(string orderNo)
    {
        //"'<b>'||a.title||'</b>'|| '<br>'||coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'')||'<br>'||"+
        //    "(case when lower(a.articletype)='c' then 'CD/DVD' when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='s' then 'SheetMusic' end"+
        //    ") as Title
        string query = @"select '<b>'||a.title||'</b>' || '<br><i>'||coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'')||'<br></i>'||"+
			"(case when lower(a.articletype)='c' then 'CD/DVD' when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='s' then 'SheetMusic' end"+
			") as articlename, a.quantity as stock, s.orderqty as qty," +
                       " s.unitprice as price, round(((s.orderqty * s.unitprice*s.vatpc)/100 ),2) as svat, round(((s.orderqty * s.unitprice)+(s.orderqty * s.unitprice*s.vatpc)/100 ),2) as netprice from article a, supplyordersline s, composer c " +
                       " where a.articlecode = s.articlecode and c.composerid=a.composer and s.supplyorderid = '" + orderNo + "' order by a.title asc";           
       
        DataTable dt = handler.GetDataTable(query);

		//DataColumn netPrice;
		//netPrice = new DataColumn();
		//DataColumn vat = new DataColumn();
		//netPrice.DataType = System.Type.GetType("System.String");
		//vat.DataType = System.Type.GetType("System.String");
		//netPrice.ColumnName = "Net Price";
		//vat.ColumnName = "Vat";
		//dt.Columns.Add(netPrice);
		//dt.Columns.Add(vat);
		//for (int i = 0; i < dt.Rows.Count; i++)
		//{
		//    DataRow row = dt.Rows[i];
		//    row["Net Price"] = string.Format("{0:F2}", double.Parse(dt.Rows[i]["netprice"].ToString()));
		//    row["Vat"] = string.Format("{0:F2}", double.Parse(dt.Rows[i]["svat"].ToString()));
		//}

        grdSupplyOrderLine.DataSource = dt;
        grdSupplyOrderLine.DataBind();
        lblOrderDetails.Visible = true;
    }
    
    
    protected void grdStockOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {        
        grdStockOrder.PageIndex = e.NewPageIndex;
        LoadGrdStockOrder(Session["receivingType"].ToString(), Session["stockOrder"].ToString());
    }
    //protected void grdSupplyOrderLine_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grdSupplyOrderLine.PageIndex = e.NewPageIndex;
    //    LoadGrdStockOrderLines(Session["orderNo"].ToString());
    //}
    //protected void grdSupplyOrderLine_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    string order = "";
    //    if (Session["stockOrderLine"] == null)
    //    {
    //        order = e.SortExpression.ToString() + " " + "desc";
    //    }

    //    else
    //    {
    //        string[] splitter = Session["stockOrderLine"].ToString().Split(' ');
    //        order = splitter[splitter.Length - 1];
    //        if (order.Equals("desc"))
    //        {
    //            order = e.SortExpression.ToString() + " " + "asc";
    //        }
    //        else
    //        {
    //            order = e.SortExpression.ToString() + " " + "desc";
    //        }
    //    }
    //    Session["stockOrderLine"] = order;

    //    LoadGrdStockOrderLines(Session["orderNo"].ToString(), order);
    //}


    protected void btnUpdatePaymentStatus_Click(object sender, ImageClickEventArgs e)
    {
		int count=0;
        foreach(GridViewRow row in grdStockOrder.Rows)
        {
            CheckBox chkBox = (CheckBox)row.Cells[0].FindControl("chkOrder");
            if (chkBox.Checked)
            {
				count++;
                LinkButton lBtn = (LinkButton)row.Cells[2].FindControl("lnkSelect");
				try
				{
					string query = @"update supplyorders set paymentstatus = '"+ddlPaymentStatus.SelectedValue.ToString()+"' "+
                               " where supplyorderid = '"+lBtn.CommandArgument.ToString()+"'";
					handler.ExecuteQuery(query);
				}
				catch(Exception ex)
				{
                    Boeijenga.Common.Utils.LogWriter.Log(ex);
                    Page.RegisterStartupScript("required", "<script language='javascript'> alert(' " + ex.ToString() + " ');</script>");
				}
               
            }

        }
		if (count == 0) Page.RegisterStartupScript("required", "<script language='javascript'> alert('Check Box Selection Required');</script>");

        LoadGrdStockOrder(Session["receivingType"].ToString(), Session["stockOrder"].ToString());

    }

    
}

