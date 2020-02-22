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
using HawarIT.WebControls;
using Npgsql;
using Boeijenga.DataAccess.Constants;
using Boeijenga.Business;
using System.Collections.Generic;
using Boeijenga.Common.Objects;

public partial class Admin_receiveorders : System.Web.UI.Page
{
	DbHandler dbHandler=new DbHandler ();
	string sql="";
	
    protected void Page_Load(object sender, EventArgs e)
    {
		if(!IsPostBack)
		{
			if (Request.Params["soid"] != null)
			{
				string supplyOrder = Request.Params["soid"];
				LoadRecevingOrders(supplyOrder);
			}
            ///lnkSave.Visible = false;
		}
		else
			lblErrorMsg.Text="";

    }
	/*--------------Event Handler Area-------------*/
	/*
	 * Event Handler for save Button
	 * Author:Shahriar
	 * Date:12-7-07
	 */
	protected void lnkSave_Click(object sender, EventArgs e)
	{
		string supplyOrder = Request.Params["soid"];
		bool b=SaveReceiveDetails(supplyOrder);
        if (b == true)
        {
            Response.Write("<script> parent.OBSettings.RefreshPage();</script>");
        }
		//else
		//{
		//    b=UpdateReceiveDetails(supplyOrder);
		//}

        //if(b==true)
        //    Response.Redirect("stockmanagement.aspx");
	}
	/*
	 * Event Handler for cancel Button
	 * Author:Shahriar
	 * Date:19-7-07
	 */
	protected void lnkCancel_Click(object sender, EventArgs e)
	{
        Response.Write("<script> parent.OBSettings.RefreshPage();</script>");
    }
	/*--------------Function Area------------------*/
	/*
	 * Check wheather the receiving record of the respective sypply order
	 * Number existed or not.
	 * Author:Shahriar
	 * Date:19-7-07
	 */
	//private bool IsEmptyRecord(string supplyOrder)
	//{
	//    sql = "select count(*) as count from receiveorders where supplyorderid='" + supplyOrder + "'";
	//    DataTable dtSupplyOrders = dbHandler.GetDataTable(sql);
	//    string count=dtSupplyOrders.Rows[0]["count"].ToString();
	//    if(count.Equals("0"))
	//        return true;
	//    else
	//        return false;
	//}
	/*
	 * Function for update Receive Details
	 * Author:Shahriar
	 * Date:19-7-07
	 */
	//private bool UpdateReceiveDetails(string supplyOrder)
	//{
	//    int index=1;
	//    Double orderedQty=0.00;
	//    Double receivedQty = 0.00;
	//    int num_orders = (grdReceive.Rows.Count*3) + 2;
	//    NpgsqlCommand[] commands = new NpgsqlCommand[num_orders];
	//    sql = "update receiveorders set receivedate=:receivedate,received_by=:received_by,remarks=:remarks where receiveid=:receiveid and supplyorderid=:supplyorderid";
	//    commands[0] = new NpgsqlCommand(sql);
	//    commands[0].Parameters.Add("receiveid", lblReceiveNoValue.Text);
	//    commands[0].Parameters.Add("supplyorderid", lblSupplyOrderValue.Text);
	//    commands[0].Parameters.Add("receivedate", System.DateTime.Parse(txtReceiveDt.Text).ToString("yyyy-MM-dd"));
	//    commands[0].Parameters.Add("received_by", txtReceiveBy.Text);
	//    commands[0].Parameters.Add("remarks", txtRemarks.Text);

	//    foreach (GridViewRow row in grdReceive.Rows)
	//    {
	//        Label lblArticleCode = (Label)row.Cells[0].FindControl("lblArticleCode");
	//        Label lblPrevRecvQty = (Label)row.Cells[0].FindControl("lblPrevRecvQty");
	//        Label lblOrderQty = (Label)row.Cells[0].FindControl("lblOrderQty");
	//        DecimalControl intCtrPurchasePrice = (DecimalControl)row.Cells[0].FindControl("intCtrPurchasePrice");
	//        IntegerControl intCtrRecvQty = (IntegerControl)row.Cells[0].FindControl("intCtrRecvQty");
	//        sql = "update receiveordersline set purchaseprice=:purchaseprice,receiveqty=:receiveqty where receiveid=:receiveid and articlecode=:articlecode";
	//        commands[index] = new NpgsqlCommand(sql);
	//        commands[index].Parameters.Add("receiveid", lblReceiveNoValue.Text);
	//        commands[index].Parameters.Add("articlecode", lblArticleCode.Text);
	//        commands[index].Parameters.Add("purchaseprice", intCtrPurchasePrice.Text.Replace(',', '.'));
	//        commands[index].Parameters.Add("receiveqty", intCtrRecvQty.Text);
	//        index++;

	//        //calculate total received quantity
	//        Double recvQty = Double.Parse(lblPrevRecvQty.Text) + Double.Parse(intCtrRecvQty.Text);
	//        receivedQty+=recvQty;
	//        //calculate wheather received qty fulfil ordered qty
	//        orderedQty+= Double.Parse(lblOrderQty.Text);
			

	//        sql = "update supplyordersline set receiveqty=:receiveqty where supplyorderid=:supplyorderid and articlecode=:articlecode";
	//        commands[index] = new NpgsqlCommand(sql);
	//        commands[index].Parameters.Add("receiveqty", recvQty);
	//        commands[index].Parameters.Add("supplyorderid", lblSupplyOrderValue.Text);
	//        commands[index].Parameters.Add("articlecode", lblArticleCode.Text);
	//        index++;

	//        sql = "update article set quantity=(quantity+:quantity) where articlecode=:articlecode";
	//        commands[index] = new NpgsqlCommand(sql);
	//        commands[index].Parameters.Add("quantity", intCtrRecvQty.Text);
	//        commands[index].Parameters.Add("articlecode",lblArticleCode.Text);
			
	//        index++;

			
	//    }
	//    string rcvStatus = "";
	//    if (receivedQty == 0)
	//        rcvStatus = "N";
	//    else if (receivedQty > 0 && receivedQty < orderedQty)
	//        rcvStatus = "P";
	//    else if (receivedQty >= orderedQty)
	//        rcvStatus = "F";

	//    sql = "update supplyorders set receivingstatus=:receivingstatus where supplyorderid=:supplyorderid";
	//    commands[index] = new NpgsqlCommand(sql);
	//    commands[index].Parameters.Add("receivingstatus", rcvStatus);
	//    commands[index].Parameters.Add("supplyorderid", supplyOrder);

	//    string errorMsg = "";
	//    bool b = dbHandler.ExecuteTransaction(commands, ref errorMsg);
	//    if (b == false)
	//        lblErrorMsg.Text = errorMsg;

	//    return b;
	//}
	/*
	 * Function for saving Receive Details
	 * Author:Shahriar
	 * Date:19-7-07
	 */ /// It doesn't work properly, by-Arif.
	private bool SaveReceiveDetails(string supplyOrder)
	{		

        DataTable dtReceive = new Facade().GetRceiveDeatilsFromSupplyordersbySupplyOrderId(supplyOrder);
		string shipCost = dtReceive.Rows[0]["shipcost"].ToString();
		int counter=0;
		foreach (GridViewRow row in grdReceive.Rows)
		{
			TextBox intCtrRecvQty = (TextBox)row.Cells[0].FindControl("intCtrRecvQty");
			double qty=Double.Parse(intCtrRecvQty.Text.ToString());
			if(qty!=0) //if no quantity received then don't insert into DB
				counter++;
		}
		int num_orders=(counter)*3+2;
	

        ReceiveOrder objReceiveOrder = new ReceiveOrder();
        objReceiveOrder.Receiveid =Int32.Parse(lblReceiveNoValue.Text);
        objReceiveOrder.Supplyorderid = Int32.Parse(lblSupplyOrderValue.Text);
       

		System.Globalization.CultureInfo enUS = new System.Globalization.CultureInfo("en-US", true);
		System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.DateTimeFormatInfo();
		dtfi.ShortDatePattern = "dd-MM-yyyy";
		dtfi.DateSeparator = "-";
		DateTime dtIn = Convert.ToDateTime(txtReceiveDt.Text.Trim(), dtfi);


        objReceiveOrder.Receivedate = Convert.ToDateTime(txtReceiveDt.Text.Trim(), dtfi);
        objReceiveOrder.Shippingcost = double.Parse(shipCost.Replace(',', '.'));
        objReceiveOrder.Remarks = txtRemarks.Text;
        objReceiveOrder.Received_by = txtReceiveBy.Text;

    

        List<ReceiveOrderLine> objReceiveOrderLines = new List<ReceiveOrderLine>();
        ArrayList PrevRecvQty = new ArrayList();
        ArrayList OrderQty = new ArrayList();

		foreach (GridViewRow row in grdReceive.Rows)
		{
            ReceiveOrderLine objReceiveOrderLine = new ReceiveOrderLine();

			Label lblArticleCode = (Label)row.Cells[0].FindControl("lblArticleCode");
			Label lblPrevRecvQty = (Label)row.Cells[0].FindControl("lblPrevRecvQty");
            PrevRecvQty.Add(lblPrevRecvQty.Text);
			Label lblOrderQty = (Label)row.Cells[0].FindControl("lblOrderQty");
            OrderQty.Add(lblOrderQty.Text);
			TextBox intCtrPurchasePrice=(TextBox)row.Cells[0].FindControl("intCtrPurchasePrice");
			TextBox intCtrRecvQty = (TextBox)row.Cells[0].FindControl("intCtrRecvQty");
			double qty=Double.Parse(intCtrRecvQty.Text.ToString());
			Double recvQty=0.0;
			if(qty!=0) //if no quantity received then don't insert into DB
			{
                

                objReceiveOrderLine.Receiveid = Int32.Parse(lblReceiveNoValue.Text);
                objReceiveOrderLine.Articlecode = lblArticleCode.Text;
                objReceiveOrderLine.Purchaseprice = double.Parse(intCtrPurchasePrice.Text.Replace(',', '.'));
                objReceiveOrderLine.Receiveqty = Int32.Parse(intCtrRecvQty.Text);
                objReceiveOrderLines.Add(objReceiveOrderLine);
               
			}
          
		}
     

		string errorMsg = "";
        bool b = new Facade().SaveReceiveOrder(objReceiveOrder, objReceiveOrderLines, PrevRecvQty, OrderQty, supplyOrder, num_orders, ref errorMsg);
		if (b == false)
			lblErrorMsg.Text = errorMsg;
		
		return b;
	}
	/*
	 * Function for displaying Receiving Details
	 * Author:Shahriar
	 * Date:19-7-07
	 */
	private void LoadReceivigDetails(string supplyOrder)
	{
        //sql = "select a.articlecode,so.supplyorderid,"+
        //    "'<b>'||a.title||'</b>'|| '<br>'||coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'')||'<br>'||(case when lower(a.articletype)='c' then 'CD/DVD' when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='s' then 'SheetMusic' end) as Title," +
        //    "sol.unitprice," +
        //    "(case when lower(so.receivingstatus)='n' then 'Not Received' "+
        //    "when lower(so.receivingstatus)='p' then 'Partially Received' "+
        //    "when lower(so.receivingstatus)='f'  then 'Full Received' end) as receivingstatus,"+
        //    "sol.orderqty,0 as receiveqty, "+
        //    "("+
        //    "(select sum(receiveqty) from supplyordersline where articlecode=a.articlecode and supplyorderid=so.supplyorderid) "+
        //    ") as previous from article a,supplyordersline sol,supplyorders so,composer c " +
        //    "where sol.articlecode=a.articlecode "+
        //    "and so.supplyorderid=sol.supplyorderid "+
        //    "and c.composerid=a.composer " +
        //    "and a.articlecode IN(select articlecode from supplyordersline where supplyorderid="+supplyOrder+") "+
        //    "and so.supplyorderid="+supplyOrder;

        DataTable dtReceive = new Facade().GetReceiveDetailsBySupplyOrder(supplyOrder);
        if (dtReceive.Rows[0]["receivingstatus"].ToString().Equals("Not Received"))
        {
            lnkSave.Visible = true;
        }
        else
        {
            lnkSave.Visible = false;
        
        }
		grdReceive.DataSource =dtReceive;
		grdReceive.DataBind();
	}
	
	/*
	 * Function for display New Receiveing orders Details
	 * Author:Shahriar
	 * Date:18-7-07
	 */
	private void LoadRecevingOrders(string supplyOrder)
	{
		//if (IsEmptyRecord(supplyOrder))
			DisplayNew(supplyOrder);
		//else
		//    DisplayExisted(supplyOrder);
	}
	/*
	 * Function for display Existed Receiveing orders Details
	 * Author:Shahriar
	 * Date:19-7-07
	 */
	//private void DisplayExisted(string supplyID)
	//{
	//    sql="select ro.receiveid,to_char(ro.receivedate,'dd-mm-yyyy')as receivedate,to_char(so.supplyorderdate,'dd-mm-yyyy') as supplyorderdate,to_char(so.deliverydate,'dd-mm-yyyy') as deliverydate,ro.remarks "+
	//        "from  receiveorders ro,supplyorders so "+
	//        "where ro.supplyorderid=so.supplyorderid "+
	//        "and ro.supplyorderid="+supplyID;
	//    DataTable dt = dbHandler.GetDataTable(sql);
	//    lblReceiveNoValue.Text = dt.Rows[0]["receiveid"].ToString();
	//    txtReceiveDt.Text = dt.Rows[0]["receivedate"].ToString();
	//    lblSupDelDateValue.Text = dt.Rows[0]["deliverydate"].ToString();
	//    lblSupOrdDateValue.Text = dt.Rows[0]["supplyorderdate"].ToString();
	//    lblSupplyOrderValue.Text = supplyID;
	//    txtRemarks.Text = dt.Rows[0]["remarks"].ToString();
	//    LoadReceivigDetails(supplyID);
	//}
	/*
	 * Author:Shahriar
	 * Date:19-7-07
	 */
	private void DisplayNew(string supplyID)
	{
		//sql = "select coalesce(max(receiveid),0) as maximum from receiveorders";
        DataTable dt = new Facade().GetMaxReceiveOrders();
		int receiveId=int.Parse(dt.Rows[0]["maximum"].ToString());
		receiveId = receiveId+1;
		lblReceiveNoValue.Text=receiveId.ToString();
		txtReceiveDt.Text=System.DateTime.Now.Date.ToString("dd-MM-yyyy");
		lblSupplyOrderValue.Text = supplyID;

		//sql = "select to_char(supplyorderdate,'dd-mm-yyyy') as supdate ,to_char(deliverydate,'dd-mm-yyyy') as deldate from supplyorders where supplyorderid=" + supplyID;
        DataTable dtSupply = new Facade().GetSupplyOrderbySupplyOrderId(supplyID);
		lblSupOrdDateValue.Text = dtSupply.Rows[0]["supdate"].ToString();
		lblSupDelDateValue.Text = dtSupply.Rows[0]["deldate"].ToString();

		LoadReceivigDetails(supplyID);
			
		
	}


}
