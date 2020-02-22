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

public partial class Admin_ordermanagement : System.Web.UI.Page
{

    DbHandler dbHandler = new DbHandler();
	string sortDirection="ASC";
	string successMessage_pre="Successfully posted to ";
	string successMessage_post = " State.";
	string unSuccessMessage="Sorry!! Operation Unsuccessful.";
    string order = "";
    protected void Page_Load(object sender, EventArgs e)
    {
		SetCultureValue();
        if (!IsPostBack) 
        {
            //lblHitCount.Text = "Total Hits: " + SetHitValue();
            LoadOrder(drpOrderFilter.SelectedValue,"");
			
        }
		else
		{
			lblMessage.Text="";
		}
		
    }

    //protected string SetHitValue()
    //{
    //    string sqlUpdateHitcount = "select totalhits as MaxHit from hitcounter;";
    //    DataTable dt = dbHandler.GetDataTable(sqlUpdateHitcount);
    //    return dt.Rows[0]["MaxHit"].ToString();
    //}
	private bool CheckOrderStatus(string order,string status)
	{
		string sql = "select count(*) as Count from orders o where o.orderstatus='"+status+"' and o.orderid=" + order;
		DataTable dtStatus=dbHandler.GetDataTable(sql);
		if(dtStatus.Rows[0]["count"].ToString().Equals("0"))
			return false;
		else
			return true;
	}
	private bool CheckOrderStatus(ArrayList orderList, string status)
	{

		string orderIdIn = "";
		for (int i = 0; i < orderList.Count; i++)
		{
			orderIdIn += orderList[i].ToString() + ",";
		}
		orderIdIn = orderIdIn.Substring(0, orderIdIn.Length - 1);


		string sql = "select distinct orderstatus from orders o where orderid in (" + orderIdIn+")";
		DataTable dtStatus = dbHandler.GetDataTable(sql);
		if (dtStatus.Rows.Count == 1 && dtStatus.Rows[0]["orderstatus"].Equals("2"))
			return true;
		else 
			return false;
	}
	/*
	 * Function for checking whether selected orders from same customer
	 * Author: Shahriar
	 * Date:6-7-07
	 */	
	private bool CheckValidInvoice(ArrayList orderList)
	{
		string orderIdIn="";
		for(int i=0;i<orderList.Count;i++)
		{
			orderIdIn+=orderList[i].ToString()+",";
		}
		orderIdIn=orderIdIn.Substring(0,orderIdIn.Length-1);
		string sql = "select distinct customer from orders where orderid in(" + orderIdIn + ")";
		DataTable dtCustomer=dbHandler.GetDataTable(sql);
		return !(dtCustomer.Rows.Count>1);
	}
	private void SetCultureValue()
	{
		btnOrderFilter.ImageUrl = "../graphics/" + (string)base.GetGlobalResourceObject("string", "btnSubmit");
		btnAction.ImageUrl = "../graphics/" + (string)base.GetGlobalResourceObject("string", "btnSubmit");
	}
	private void SetAvilableActions(string status)
	{
		drpAction.Items.Clear();
		if(status.Equals("1"))
		{
			drpAction.Items.Insert(0,new ListItem("Ready","2"));
			drpAction.Items.Insert(1,new ListItem("Verwijderen","1"));
			
		}
		else if(status.Equals("2"))
		{
			drpAction.Items.Insert(0, new ListItem("Fucturen", "3"));
			
		}
		else if(status.Equals("4"))
		{
			drpAction.Items.Insert(0, new ListItem("Ready", "2"));
			drpAction.Items.Insert(1, new ListItem("Fucturen", "3"));
			drpAction.Items.Insert(2, new ListItem("Verwijderen", "1"));
		}
		
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
    private void LoadOrder(string status,string orderby)
    {

		// order Management Query
		//Author:Sazib
	
		/*string orderString = @"select o.orderid,COALESCE(c.firstname,'')|| ' '|| COALESCE(c.middlename,'')|| ' '||COALESCE( c.lastname,'') as customer,to_char(o.orderdate,'dd-mm-yyyy') as orderdate,( case when o.orderstatus='1' then 'Assigned' when o.orderstatus='2' then 'Ready' when o.orderstatus='3' then 'invoiced' end) as status
							 from orders o,customer c where o.customer=c.customerid and o.orderstatus like '" + status + "' order by o.orderdate desc; ";*/

		// query updated on 29-6-07 for modification and enhancement 
		// Author: Shahriar
		/*string orderString = @"" +
		"select o.orderid,"+
		"COALESCE(c.firstname,'')|| ' '|| COALESCE(c.middlename,'')|| ' '||COALESCE( c.lastname,'') as customer,"+
		"(COALESCE(c.dhousenr,'') ||', '|| COALESCE(c.daddress,'')||', '||COALESCE(c.dresidence,'')) as DAddress,"+
		"to_char(o.orderdate,'yyyy-mm-dd') as orderdate,"+
		"( case when o.orderstatus='1' then 'Assigned'"+
		" when o.orderstatus='2' then 'Ready'"+
		" when o.orderstatus='3' then 'invoiced'"+
		"end) as status "+
		"from orders o,customer c where o.customer=c.customerid ";*/

        string orderString = @"select o.orderid, 
						COALESCE(c.firstname,'')|| ' '|| COALESCE(c.middlename,'')|| ' '||COALESCE( c.lastname,'') as customer,
						(COALESCE(c.dhousenr,'') ||(case when length(c.dhousenr)>0 then ', ' else '' end)||
							COALESCE(c.daddress,'')||	(case when length(c.daddress)>0 then ', ' else '' end)||
							COALESCE(c.dresidence,'')) as DAddress,to_char(o.orderdate,'dd-MM-yyyy') as orderdate,
						( case when o.orderstatus='1' then 'Assigned'
						 when o.orderstatus='2' then 'Ready'
						 when o.orderstatus='3' then 'Invoiced'
						 end) as status from orders o,customer c where o.customer=c.customerid";
		//check whether  show all or not

		if (!status.Equals("4"))
		{
			orderString+=" and o.orderstatus like '" + status + "'";
		}
				
		//Checking whether order by requested
		//if(!orderby.Trim().Equals(""))
		//    orderString+=" order by "+orderby;

		if (Session["sortExpression"] != null)
		{
			orderString+=" order by "+Session["sortExpression"].ToString()+" "+Session["sortDirection"].ToString();
		}
		else
			orderString += " order by o.orderid desc";

		//orderSource.SelectCommand=orderString;
		//orderSource.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["connection-string"].ToString(); 
		DataTable dt = dbHandler.GetDataTable(orderString);
		grdOrder.DataSource = dt;
		grdOrder.DataBind();

		foreach (GridViewRow row in grdOrder.Rows)
		{
			LinkButton lnkEdit = (LinkButton)row.Cells[7].FindControl("lnkEdit");
			CheckBox chkOrder=(CheckBox)row.Cells[0].FindControl("chkOrder");											
			Label lblOrderStatus = (Label)row.Cells[5].FindControl("lblOrderStatus");
			if(!lblOrderStatus.Text.ToLower().Equals("assigned"))
				lnkEdit.Visible=false;
		
			if(lblOrderStatus.Text.ToLower().Equals("invoiced"))
				chkOrder.Visible=false;
				
				
			
		} 
		if(status.Equals("3"))
		{
            HtmlInputCheckBox chkAll = (HtmlInputCheckBox)grdOrder.HeaderRow.FindControl("chkAll");
			chkAll.Disabled=true;
			drpAction.Visible = false;
			btnAction.Visible=false;
		}
		else
		{
			drpAction.Visible = true;
			btnAction.Visible=true;
			SetAvilableActions(status);
		}

		grdOrderLine.DataSource = null;
		grdOrderLine.DataBind();
    }


    protected void btnOrderFilter_Click(object sender, EventArgs e)
    {

		Session["FilterType"]=drpOrderFilter.SelectedValue.ToString();
		LoadOrder(drpOrderFilter.SelectedValue, "");
				
     

    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        ArrayList orderList = new ArrayList();
        foreach( GridViewRow row in grdOrder.Rows)
        {
            CheckBox checkBox = (CheckBox)row.Cells[0].FindControl("chkOrder");
           if (checkBox.Checked) 
           {

               Label tempLabel = (Label)row.Cells[4].FindControl("lblOrderId");
               string orderId = tempLabel.Text;
               orderList.Add(orderId);
           }
        }

        if (orderList.Count > 0)
        {
            if (drpAction.SelectedValue.Equals("2")) //Ready State
            {
                DoReady(orderList);

            }

			else if (drpAction.SelectedValue.Equals("1")) //Verwijderen state
            {
				//if (!this.IsClientScriptBlockRegistered("confirm"))
				//this.Page.RegisterStartupScript("confirm", "<script>checkDelete()</script>");
				DoDelete();
            }
			else if (drpAction.SelectedValue.Equals("3")) //Fucturen state
            {
				DoFacturen(orderList);
            }
        }
        else 
        {

            //ShowALert("Selection Required");
			//if (!this.IsClientScriptBlockRegistered("required"))
				Page.RegisterStartupScript("required", ShowALert("Check Box Selection Required"));
				//Page.ClientScript.RegisterClientScriptBlock(GetType(string),"required", ShowALert("Selection Required"));
             
        }
    }

	private bool CheckValidInvoice(string order)
	{
		bool b=true;
        string sql = "select o.orderid,o.articlecode,a.quantity as stock,o.quantity from article a ,ordersline o where o.articlecode=a.articlecode and o.orderid=" + order;
		DataTable dtOrders = dbHandler.GetDataTable(sql);
		
		for(int i=0;i<dtOrders.Rows.Count;i++)
		{
			int stock = int.Parse(dtOrders.Rows[i]["stock"].ToString().Trim());
			int qty = int.Parse(dtOrders.Rows[i]["quantity"].ToString().Trim());
			if(qty>stock)
			{
				b=false;break;
			}
		}
		return b;
		
	}
    private bool ProcessInvoice(ArrayList orderList,string invoiceId,ref string msg)
    {

		string sql="";
		ArrayList arrayOrder=new ArrayList();
		
			string order=orderList[0].ToString();
			sql = @"select customer from orders where orderid='" + order + "'";
			DataTable dtCustID = dbHandler.GetDataTable(sql);
			int custid = int.Parse(dtCustID.Rows[0]["customer"].ToString());

			/*
			 * new query fixing bug, not delivery info only invoice info
			 * Author:shahriar
			 * Date:8-7-07
			 */
			sql = "select c.housenr,c.address,c.postcode,c.residence,c.country,o.remarks,c.vatnr  from customer c ,orders o where c.customerid=o.customer and c.customerid=" + custid;
			DataTable dt=dbHandler.GetDataTable(sql);

			int num_orders=(orderList.Count*2)+1;
			NpgsqlCommand[] commands = new NpgsqlCommand[num_orders];

        string sqlInvoice = @"insert into invoice(housenr,address,postcode,residence,country,invoiceid,customer,customerbtwnr)
								values(:housenr,:address,:postcode,:residence,:country,:invoiceid,:customer,:customerbtwnr)";

			commands[0] = new NpgsqlCommand(sqlInvoice);
			commands[0].Parameters.Add("housenr", dt.Rows[0]["housenr"].ToString());
			commands[0].Parameters.Add("address", dt.Rows[0]["address"].ToString());
			commands[0].Parameters.Add("postcode", dt.Rows[0]["postcode"].ToString());
			commands[0].Parameters.Add("residence", dt.Rows[0]["residence"].ToString());
			commands[0].Parameters.Add("country", dt.Rows[0]["country"].ToString());
			commands[0].Parameters.Add("invoiceid", invoiceId);
			commands[0].Parameters.Add("customer", custid);
            commands[0].Parameters.Add("customerbtwnr", dt.Rows[0]["vatnr"].ToString());
		

			int comIndex=1;
			for (int i = 0; i < orderList.Count; i++)
			{
				string sqlinvoiceline = "insert into invoiceline(orderid,invoiceid) values(:orderid,:invoiceid)";

				commands[comIndex] = new NpgsqlCommand(sqlinvoiceline);

				commands[comIndex].Parameters.Add("orderid", orderList[i].ToString());
				commands[comIndex].Parameters.Add("invoiceid", invoiceId);

				string updateorder = "update orders set orderstatus=:orderstatus where orderid=:orderid";

				commands[comIndex+1] = new NpgsqlCommand(updateorder);
				commands[comIndex+1].Parameters.Add("orderstatus", "3");
				commands[comIndex+1].Parameters.Add("orderid", orderList[i].ToString());

				
				
				//if (b == false)
				//{
				//    arrayOrder.Add(order);
				//}

				comIndex=comIndex+2;
			     
			}
			bool b = dbHandler.ExecuteTransaction(commands, ref msg);
			return b;
		
      
    }
    private void DoFacturen(ArrayList orderList)
    {

		if (CheckValidInvoice(orderList)) // check whether of same customer
		{
			if(CheckOrderStatus(orderList,"2"))
			{
				string sql_invoice = "select max(invoiceid) as maxid from invoice";
				DataTable dtInvoice = dbHandler.GetDataTable(sql_invoice);
				int maxid;
				if (dtInvoice.Rows[0]["maxid"].ToString().Equals(""))
					maxid = 1;
				else
					maxid = (int)dtInvoice.Rows[0]["maxid"] + 1;

				string invoiceid = maxid.ToString();

				//ArrayList arryOrder=ProcessInvoice(orderList, invoiceid);	
				string msg="";
				bool b = ProcessInvoice(orderList, invoiceid,ref msg);	
				if(b==true)
				{
					lblMessage.Text = successMessage_pre + " Fucturen" + successMessage_post;
					LoadOrder(drpOrderFilter.SelectedValue, "");
				}
				else
				{
					lblMessage.Text =msg;
				}
				//if(arryOrder.Count>0)
				//{
				//    lblMessage.Text="Process Completed with some exceptions!!";
				//    colorSelection(arryOrder, "red");
				//}
				//else
				//{
				//    lblMessage.Text = successMessage_pre + " Fucturen" + successMessage_post;
				//    LoadOrder(drpOrderFilter.SelectedValue, "");
				//}	
			}
			else
			{
				lblMessage.Text = "Sorry!! only orders of ready state can posted to Facturen.";
			}
		}
		else
		{
			lblMessage.Text = "Sorry!! You should select orders of same customer.";

		}
		
    }


	/* Check staus of delete
	 * If status is assigned only then delete is possible
	 * Author:Shahriar 3/7/07
	 */
	private ArrayList CheckValidStatus(string status)
	{
		ArrayList statusArray=new ArrayList ();
		foreach (GridViewRow row in grdOrder.Rows)
		{
			CheckBox checkBox = (CheckBox)row.Cells[0].FindControl("chkOrder");
			if (checkBox.Checked)
			{

				Label tempLabel = (Label)row.Cells[2].FindControl("lblOrderId");
				Label tempLabelStatus = (Label)row.Cells[5].FindControl("lblOrderStatus");
				string orderId = tempLabel.Text.Trim();
				string orderStatus=tempLabelStatus.Text.Trim();
				if (orderStatus.Equals(status)) // Sttus is Assigned or not.If yes then user can Delete order
				{
					statusArray.Add(orderId);
				}
			}
		}
		return statusArray;
	}
    private void DoDelete()
    {
		//if (!txtHideValue.Value.Trim().Equals("yes"))
		//    return;


		ArrayList orderList = CheckValidStatus("Assigned");
		if(orderList.Count==0)
		{
			lblMessage.Text = "Sorry!! can delete orders of only assigned state.";
			return;
		}
			
		for (int i = 0; i < orderList.Count; i++)
		{
			string order = (string)orderList[i];
			string sql = "delete from orders  where orderid='" + order + "'";

			dbHandler.ExecuteQuery(sql);

		}
		LoadOrder(drpOrderFilter.SelectedValue, "");
    }

    private Boolean CheckValidQuantity(DataTable dt) 
    {
        foreach(DataRow row in dt.Rows)
        {

            int qty = Convert.ToInt32(row["qty"]);
            int stock = Convert.ToInt32(row["stock"]);
           
            if(qty>stock)
            {
				
                return false;
            }

        
        }


        return true;

    }

	/*
	 *Function for
	 *total quantity of identical items from toatl orders exceds stock
	 * Author: Shahriar
	 * Date:8-7-07
	 */
	private bool IsValidStock(ArrayList orderList)
	{
		string strIN = "";
		for (int i = 0; i < orderList.Count; i++)
		{
			strIN += orderList[i].ToString() + ",";
		}
		strIN = strIN.Substring(0, strIN.Length - 1);

		string sql = "select ol.articlecode,sum(ol.quantity) as quantity,a.quantity as stock " +
					"from ordersline ol,article a " +
					" where a.articlecode=ol.articlecode " +
					" and ol.orderid in(" + strIN + ") " +
					" group by ol.articlecode,a.quantity";
		DataTable dtStock = dbHandler.GetDataTable(sql);
		for (int i = 0; i < dtStock.Rows.Count; i++)
		{
			if((int.Parse(dtStock.Rows[i]["quantity"].ToString()))>(int.Parse(dtStock.Rows[i]["stock"].ToString())))
			{
				return false;	
			}
		}

		return true;

	}
	private Boolean IsValidOrder(string order) 
	{
		if(CheckOrderStatus(order,"1")) //Check Whether in Assigned State
		{
			DataTable dt = dbHandler.GetDataTable("select o.quantity as qty,a.quantity as stock from ordersline o,article a where o.articlecode=a.articlecode and orderid=" + order);
			if(!CheckValidQuantity(dt))
				return false;
				
			else
				return true;
				
		}
		else
			return false;
	}
    private Boolean IsValidOrder(ArrayList orderList) 
    {
		
		//if(IsValidStock(orderList))
		//{
			for (int i = 0; i < orderList.Count; i++)
			{
				if(CheckOrderStatus(orderList[i].ToString(),"1")) //Check Whether in Assigned State
				{
					DataTable dt = dbHandler.GetDataTable("select o.quantity as qty,a.quantity as stock from ordersline o,article a where o.articlecode=a.articlecode and orderid=" + orderList[i].ToString());
					if(!CheckValidQuantity(dt))
					{
						//lblMessage.Text = "Sorry!! ordered quantity is larger then stock quantity";
						return false;
					}
				}
				else
				{
					//lblMessage.Text = "Sorry!! only assigned state orders can be posted";
					return false;
				}

		   }
		//}
		//else
		//{
		//    lblMessage.Text = "Sorry!! total ordered quantity is larger then stock quantity ";
		//    return false;
		//}


        return true;
    }

	/*
	 * Function for processing Ready satate
	 * 
	 */
    private void DoReady(ArrayList orderList) 
    {
		ArrayList invalidOrders=new ArrayList ();
		string msg="";
		for (int i = 0; i < orderList.Count; i++)
		{
			string order = (string)orderList[i];
			if (IsValidOrder(order))
			{
				if(UpdateStock(order))
				{
					string sql = "update orders set orderstatus='2' where orderid='" + order + "'";
					bool b = dbHandler.ExecuteQuery(sql);
				}
			}
			else
			{
				invalidOrders.Add(order);
			}
		}		
		if(invalidOrders.Count==0)
			lblMessage.Text = successMessage_pre + "Ready" + successMessage_post;
		else
		{
			for(int i=0;i<invalidOrders.Count;i++)
			{
				msg+=invalidOrders[i].ToString()+",";
			}
            msg = msg.Substring(0, msg.Length - 1);
			lblMessage.Text ="Order# "+msg+" failed to make into ready state.";
		}
		LoadOrder(drpOrderFilter.SelectedValue, "");
    }
	private bool UpdateStock(string order)
	{
		string sql = "select orderid,articlecode,quantity from ordersline where orderid=" + order;
		DataTable dtStock=dbHandler.GetDataTable(sql);
		bool b=true;
		NpgsqlCommand[] commands = new NpgsqlCommand[dtStock.Rows.Count];
		for(int i=0;i<dtStock.Rows.Count;i++)
		{
			string qty = dtStock.Rows[i]["quantity"].ToString();
			string articleCode = dtStock.Rows[i]["articlecode"].ToString();
			sql = "update article set quantity=(quantity-"+qty+") where articlecode='"+articleCode+"'";
			commands[i]=new NpgsqlCommand (sql);
		}
		string msg="";
		b=dbHandler.ExecuteTransaction(commands,ref msg);
		return b;

	}
    private string ShowAlert(string msg) 
    {
        string script = " <script language=JavaScript>";
        script += "alert('" + msg + "');";
        script += "</script>";
		return script;
		//Response.Write(script);
    
    }
	private string ShowALert(string msg)
	{
		string script = " <script type='text/javascript'>";
		script += "alert('" + msg + "')";
		script += "</script>";
		return script;

	}
    private string ShowConfirm(string msg)
    {
        string script = "<script type='text/javascript'>";
		script += " var myTextField = document.getElementById('txtHideValue'); ";
        script += " var answer = confirm('" + msg + "'); ";
		script += " if (answer){ myTextField.Text='yes';} else{myTextField.Text='no';} ";
        script += "</script>";
		return script;
        //Response.Write(script);

    }	

	// event for print a order
	//Author:Shahriar: 1-7-07
	protected void lnkPrint_Click(object sender,CommandEventArgs e)
	{
		string order = e.CommandArgument.ToString();

		//Response.Redirect("printOrder.aspx?order=" + order);
		string script = " <script type='text/javascript'>";
		script += "var popUpTest=window.open('printOrder.aspx?order=" + order + "','PrintOrder','toolbar=yes,menubar=yes,resizable=yes,scrollbars=yes,height=500,width=800,status=1,location=1');";
		script += @" if(!popUpTest) {alert('Please turn off popup blocker.');}";
		script += "</script>";
		//string script = " <script type='text/javascript'>";
		//script += "window.open('printOrder.aspx?order=" + order + "','PrintOrder','toolbar=yes,menubar=yes,resizable=yes,scrollbars=yes,height=500,width=800,status=1,location=1')";
		//script += "</script>";
		Response.Write(script);


		// Updated by Abdullah Al Mohammad
		// Date modified 10-07-2007
		//Response.Redirect("printOrder.aspx?order=" + order);       


	}
	//Color selected row 
	//Author:shahriar 3/7/07
	private void colorSelection(string orderID,string colorCode)
	{
		
		foreach (GridViewRow row in grdOrder.Rows)
		{
			
			Label id= (Label)row.Cells[0].FindControl("lblOrderId");
			if(id.Text.Equals(orderID))
			{
				
				row.BackColor = System.Drawing.ColorTranslator.FromHtml(colorCode);
				
				break;
			}
			
		}
	}
	
    protected void lnkSelect_Command(object sender, CommandEventArgs e)
    {
        order = e.CommandArgument.ToString();
        Session["grdorder"] = order;
       
		//Function for coloring selected row
		if(Session["sort"]!=null)
			sortDirection= Session["sort"].ToString();
		else
			sortDirection = "";
		
		LoadOrder(drpOrderFilter.SelectedValue, sortDirection);
        colorSelection(grdOrder, "lblOrderId",order, "#8B9BBA");
        //colorSelection(order,"#8B9BBA");		
	   
       // // modified no 1st july2007 
       // // author:shahriar
       // string sql = "select o.orderid," +
       //     "'<b>'||a.title||'</b>'|| '<br>'||coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'')||'<br>'||(case when lower(a.articletype)='c' then 'CD/DVD' when lower(a.articletype)='h' then 'S&H' when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='s' then 'SheetMusic' end) as Title," +
       //     "o.quantity,a.quantity as stock,o.unitprice," +
       //     "round( ((o.unitprice*o.quantity)+ ((o.unitprice*o.quantity*o.vatpc)/100)),2) as totalprice" +
       //     ",vatpc from ordersline o,article a,composer c " +
       //     " where c.composerid=a.composer and o.articlecode=a.articlecode and o.orderid=" + order;


       // string sql="select orderid,"+
       //     "'<b>'||a.title||'</b>'|| '<br>'||coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'')||'<br>'||"+
       //     "(case when lower(a.articletype)='c' then 'CD/DVD' when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='s' then 'SheetMusic' end"+
       //     ") as Title,o.quantity,a.quantity as stock,o.unitprice,o.unitprice*o.quantity as totalprice,"+
       //     "vatpc from ordersline o,article a,composer c where c.composerid=a.composer and o.articlecode=a.articlecode and orderid="+ order;

       //DataTable dt = dbHandler.GetDataTable(sql);

       //DataColumn index;
      

       //index = new DataColumn();
       //index.DataType = System.Type.GetType("System.Int32");
       //index.ColumnName = "index";
     
      

       // dt.Columns.Add(index);
      

       //for (int i = 0; i < dt.Rows.Count; i++) 
       //{
       //    DataRow row = dt.Rows[i];
       //    row["index"] = i+1;

       
       //}

       //grdOrderLine.DataSource = dt;
       //grdOrderLine.DataBind();
        loadgrdOrderLine();
    }

    private void loadgrdOrderLine()
    {
        order = Session["grdorder"].ToString();
        string sql = @"
                        select o.orderid,o.articlecode,
                        '<b>'||a.title||'</b>'|| '<br>'||
                        coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'')
                        ||'<br>'||
                        coalesce((select coalesce(p.firstName,'')||' '||coalesce(p.middleName,'') ||' '||coalesce(p.lastname,'') from publisher p where p.publisherid=a.publisher),'')
                        ||'- '|| o.articlecode
                        as Title,
                        o.quantity,a.quantity as stock,
                        o.unitprice as unitprice,
                        
                        round((o.unitprice-round(o.unitprice*o.discountpc/100,2))*o.quantity,2) as totalprice,
                        vatpc,
                        o.discountpc as discount
                        from ordersline o,article a,composer c, orders os
                        where c.composerid=a.composer and o.orderid = os.orderid
                        and o.articlecode=a.articlecode and o.orderid=" + order + @"
					    order by o.articlecode";


        //string sql="select orderid,"+
        //    "'<b>'||a.title||'</b>'|| '<br>'||coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'')||'<br>'||"+
        //    "(case when lower(a.articletype)='c' then 'CD/DVD' when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='s' then 'SheetMusic' end"+
        //    ") as Title,o.quantity,a.quantity as stock,o.unitprice,o.unitprice*o.quantity as totalprice,"+
        //    "vatpc from ordersline o,article a,composer c where c.composerid=a.composer and o.articlecode=a.articlecode and orderid="+ order;

        DataTable dt = dbHandler.GetDataTable(sql);

        DataColumn index;


        index = new DataColumn();
        index.DataType = System.Type.GetType("System.Int32");
        index.ColumnName = "index";



        dt.Columns.Add(index);


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            row["index"] = i + 1;
        }

        grdOrderLine.DataSource = dt;
        grdOrderLine.DataBind();
    }
    protected void grdOrderLine_DataBound(object sender, EventArgs e)
    {
		
		if(Session["FilterType"]==null || Session["FilterType"].ToString().Equals("1"))
		{
			foreach (GridViewRow row in grdOrderLine.Rows)
			{
				Label lblQty = (Label)row.Cells[0].FindControl("lblQty");
				int qty = Convert.ToInt32(lblQty.Text);

				Label lblStock = (Label)row.Cells[0].FindControl("lblStock");
				int stock = Convert.ToInt32(lblStock.Text.ToString());

				if (qty > stock)
				{
					row.BackColor = System.Drawing.ColorTranslator.FromHtml("Red");
					row.ForeColor = System.Drawing.ColorTranslator.FromHtml("White");
				}
            }
        }
  

    }
    protected void lnkEdit_Command(object sender, CommandEventArgs e)
    {

        string order = e.CommandArgument.ToString();
        Response.Redirect("editorder.aspx?order="+order);
    }

	protected void  grdOrder_PageChanging(object sender, GridViewPageEventArgs e)
	{
		grdOrder.PageIndex=e.NewPageIndex;
		LoadOrder(drpOrderFilter.SelectedValue,"");
	}

	

	//Event Handling for Sorting
	//Author: Shahriar 2/7/07
	protected void grdOrder_Sorting(object sender, GridViewSortEventArgs e)
	{
		string sort="";
		
		if(Session["sortDirection"]==null)
		{
			sort=e.SortExpression.ToString();
			Session["sortDirection"]="Desc";
			//Session["sort"]=sort;
			Session["sortExpression"]=sort;
			
		}
		else
		{
			sortDirection=Session["sortDirection"].ToString();
			sort = Session["sortExpression"].ToString().ToLower();
			/*
			 * 
			 */
			if(e.SortExpression.ToString().ToLower().Equals(sort))
			{
				if(sortDirection.Equals("Desc"))
				{
					Session["sortDirection"] = "Asc";
					Session["sortExpression"] = sort;
				}
				else
				{
					Session["sortExpression"] = sort;
					Session["sortDirection"] = "Desc";
				}
			}
			else
			{
				Session["sortDirection"] = "Desc";
				Session["sortExpression"] = e.SortExpression.ToString();
			}			
		}
		LoadOrder(drpOrderFilter.SelectedValue,sort);
	}
    protected void grdOrderLine_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdOrderLine.PageIndex = e.NewPageIndex;
        //string order = e.ToString();
        loadgrdOrderLine();
    }
}

