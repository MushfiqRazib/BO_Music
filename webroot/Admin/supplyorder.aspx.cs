
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
using System.Text.RegularExpressions;  
using Npgsql;
using HawarIT.WebControls;
using System.Net.Mail;
using Boeijenga.DataAccess.Constants;
using Boeijenga.Business;
using System.Collections.Generic;
using Boeijenga.Common.Objects;


public partial class Admin_supplyorder : System.Web.UI.Page
{
    int pageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["page-size"].ToString());
    /*
     * table to hold data for footer when article is selected
     */
    static DataTable footerTable = new DataTable();
    /*
     * mainTable is the selected article table that is populated when
     * the insert link is clicked 
     */
    static DataTable mainTable = new DataTable();
//    DataTable articleTable = new DataTable();
    string msg = "";
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txtFilter.Attributes.Add("onkeypress", "return clickButton(event,'" + lnkArticleFilter.ClientID + "')");
            LoadSUpplierDropDownList();//Load the dropdownlist with supplier name
            LoadCountryDropDownList();
            if (Request.Params["orderNo"] == null)
            {
                LoadOrdersLine("");//Load the mainTable first time
                InitializeOrderDate();//populate the order date field with current date
            }
            else
            {
                string orderNo = Request.Params["orderNo"].ToString();
                if (IsValidOrder(orderNo))
                {
                    EditOrdersLine(orderNo);
                    lnkSubmit.Visible = true;
                    lnkSendOrder.Visible = true;
                    ddlSupplier.Visible = false;
                }
                else
                {
                    Boeijenga.Common.Utils.LogWriter.Log(string.Format("Order# {0} not found", orderNo));
                    throw new Exception(string.Format("Order# {0} not found", orderNo));
                }
            }
            if (ViewState["sortExpr"] == null)
            {
                ViewState["sortExpr"] = "articlecode";
                ViewState["sortdir"] = "asc";
            }
            BindArticleGrid(ViewState["sortExpr"].ToString(), ViewState["sortdir"].ToString(), 0, pageSize);

            lnkSubmit.CausesValidation = false;
            lnkSendOrder.CausesValidation = false;
            lnkSubmit.Attributes.Add("onclick", "return CompareDates();");   
            //lnkSubmit.Attributes.Add("onclick", "return confirm('Are you sure you want to save this order?');");   
            lnkSendOrder.Attributes.Add("onclick", "return CompareDates();");             

        }
        
		
    }

    private bool IsValidOrder(string orderNo)
    {
        if (orderNo.Equals("") || orderNo == null)
        {
            return false;
        }
        
        //string query = @"select * from supplyorders where supplyorderid ='" + orderNo + "'";
       // DataTable dt = new DataTable();
        DataTable dt = new Facade().GetSupplyOrdersBySupplyOrderId(orderNo);
        if (dt.Rows.Count > 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// when open in edit mode
    /// </summary>
    /// <author>
    /// Abdullah Al Mohammad
    /// </author>
    /// <Last Updated>
    /// 20/07/2007
    /// </Last>
    /// <param name="orderNo">order number</param>
    private void EditOrdersLine(string orderNo)
    {
        //string query = @"select COALESCE(s.dhousenr,'') as dhousenr,COALESCE(s.daddress,'') as daddress,COALESCE(s.dpostcode,'') as dpostcode,COALESCE(s.dresidence,'') as dresidence,COALESCE(s.dcountry,'') as dcountry,s.supplyorderid as supplyorderid,s.supplyorderdate as supplyorderdate,s.supplierid as supplierid," +
        //"s.deliverydate as deliverydate,s.supplyorder_by as supplyorder_by,s.receivingstatus as receivingstatus,s.paymentstatus as paymentstatus, (COALESCE(p.housenr,'')||', '||COALESCE(p.address,'')||'<br>'||COALESCE(p.postcode,'')||', '||COALESCE(p.residence,'')||" +
        //"'<br>'||(select countryname from country where lower(countrycode)=lower(coalesce(p.country,'NL')))) as supplieraddress from supplyorders s, publisher p, country c  where s.supplyorderid ='" + orderNo + "' and " +
        //"p.publisherid =s.supplierid";             
        DataTable dt = new Facade().GetOrdersLineByOrderId(orderNo);
       lblPrintSupplyOrderNo.Text = dt.Rows[0]["supplyorderid"].ToString();


       txtOrderDate.Text = System.DateTime.Parse(dt.Rows[0]["supplyorderdate"].ToString()).ToString("dd-MM-yyyy");
       txtDeliveryDate.Text = System.DateTime.Parse(dt.Rows[0]["deliverydate"].ToString()).ToString("dd-MM-yyyy");
       txtDHouse.Text = dt.Rows[0]["dhousenr"].ToString();
       txtDAddress.Text = dt.Rows[0]["daddress"].ToString();
       txtDPostCode.Text = dt.Rows[0]["dpostcode"].ToString();
       txtDResidence.Text = dt.Rows[0]["dresidence"].ToString();
       ddlDCountry.SelectedValue = dt.Rows[0]["dcountry"].ToString();
       ddlSupplier.SelectedValue = dt.Rows[0]["supplierid"].ToString();
       lblPrintSupplierName.Text = ddlSupplier.SelectedItem.Text;
       lblPrintSupplierAddress.Text = dt.Rows[0]["supplieraddress"].ToString();
       txtOrderBy.Text = dt.Rows[0]["supplyorder_by"].ToString();

       /*--------------Load the grid---------------*/



//       query = @" select s.supplyorderid as supplyorderid,s.supplier_articlecode as supplyArticleID
//, s.articlecode as articlecode,COALESCE(s.vatpc,0) as vat, " +
//               "s.orderqty as qty,round((s.unitprice*s.orderqty)+(s.unitprice*s.orderqty*COALESCE(s.vatpc,0))/100,2) as netprice, " +
//               "('<b>'||COALESCE(a.title,'')||'</b>'||'<br>'|| " +
//               " '<i>'||coalesce(p.firstName,'')||' '||coalesce(p.middleName,'') ||' '||coalesce(p.lastname,'')||'</i><br>'|| " +
//               " (case when lower(a.articletype)='c' then 'CD/DVD' when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='s' then 'SheetMusic' end)) "+
//               " as title,a.quantity as stock, " +
//               "s.unitprice as price from supplyordersline s, article a, composer p where s.supplyorderid = '" + orderNo + "' and " +
//               "a.articlecode = s.articlecode and a.composer = p.composerid";
       mainTable = new Facade().GetOrdersForSupplyOrderByOrderId(orderNo);
       grdOrder.DataSource = mainTable;
       grdOrder.DataBind();
       
    }

    /// <summary>
    /// populate the order date field with current date
    /// </summary>
    /// <author>
    /// Abdullah Al Mohammad 
    /// </author>
    /// <last updated>
    /// Last updated - 19/07/2007
    /// </last>
    private void InitializeOrderDate()
    {        
        txtOrderDate.Text = System.DateTime.Now.Date.ToString("dd-MM-yyyy");
    }
    /// <summary>
    /// Load the dropdownlist with supplier name
    /// </summary>
    /// <author>
    /// Abdullah Al Mohammad 
    /// </author>
    /// <last updated>
    /// Last updated - 19/07/2007
    /// </last>
    private void LoadSUpplierDropDownList()
    {


        //string query = @"select COALESCE(firstname,'')||' '||COALESCE(middlename,'')||' '||COALESCE(lastname,'') as name, publisherid from "+
        //    "publisher order by firstname asc";
        DataTable dt = new Facade().GetSupplier();   
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ddlSupplier.Items.Add(dt.Rows[i]["name"].ToString());
            ddlSupplier.Items[i].Value = dt.Rows[i]["publisherid"].ToString();
        }
        LoadSupplierAddress();
    }

    private void LoadSupplierAddress()
    {
        //string query = @"select (COALESCE(housenr,'')||', '||COALESCE(address,'')||'<br>'||COALESCE(postcode,'')||', '||COALESCE(residence,'')||" +
        //                "'<br>'||(select countryname from country where lower(countrycode)=lower(coalesce(p.country,'')))) as supplieraddress from publisher p, country c where " +
        //                "publisherid = '" + ddlSupplier.SelectedValue.ToString() + "'";
        DataTable dt = new Facade().GetSupplierAddressByPublisherId(ddlSupplier.SelectedValue.ToString());
        lblPrintSupplierName.Text = ddlSupplier.SelectedItem.ToString();
        lblPrintSupplierAddress.Text = dt.Rows[0]["supplieraddress"].ToString();
    }

    /// <summary>
    /// Load Country drop down list
    /// </summary>
    /// <author>
    /// Abdullah Al Mohammad
    /// </author>
    /// <Last Updated>
    /// 20/07/2007
    /// </Last>
    private void LoadCountryDropDownList()
    {
       // string query = @"select distinct countryname, countrycode from country order by countryname";
      //  DataTable dt = new Facade().GetCountry() ;        
        //ddlDCountry.Items.Add("Select");
        //ddlDCountry.Items[0].Value = "select";
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    ddlDCountry.Items.Add(dt.Rows[i]["countryname"].ToString());
        //    ddlDCountry.Items[i].Value = dt.Rows[i]["countrycode"].ToString();
        //}
        List<Country> countryList = new Facade().GetCountry();
        if (countryList != null)
        {
            ddlDCountry.DataSource = countryList;
            ddlDCountry.DataValueField = "countrycode";
            ddlDCountry.DataTextField = "countryname";
            ddlDCountry.DataBind();
        }
        ddlDCountry.SelectedValue = "NL";
    }

    /// <summary>
    /// Load the mainTable that holds the ordered items
    /// </summary>
    /// <param name="query">the database query that to be executed</param>
    /// /// <author>
    /// Abdullah Al Mohammad 
    /// </author>
    /// <last updated>
    /// Last updated - 19/07/2007
    /// </last>
    private void LoadOrdersLine(string selectedcode)
    {
        if (selectedcode.Equals(string.Empty))//if the query is empty--generally when loading the table first time without orders
        {
           
            // query = @"select articlecode, title, quantity as stock,'1' as qty,price,'0' as vat,'' as netprice from article where articlecode = ''";
            mainTable = new Facade().GetOrdersLine();
            mainTable.Columns["price"].DataType = System.Type.GetType("System.String");
            mainTable.Columns.Add("supplyArticleID");
            DataSet ds = new DataSet();
            ds.Tables.Add(mainTable);
            BuildNoRecords(grdOrder, ds);//to set the grdOrder grid viewable without any data
        }
        else //if the query is not empty
        {


            footerTable = new Facade().GetOrdersLineByArticleCode(selectedcode);
            //now populate the footer row of grdOrder grid with footerTable row
            ((Label)grdOrder.FooterRow.FindControl("lblFooterArticleCode")).Text = footerTable.Rows[0]["articlecode"].ToString();
            ((Label)grdOrder.FooterRow.FindControl("lblTitle")).Text = footerTable.Rows[0]["title"].ToString();
            ((Label)grdOrder.FooterRow.FindControl("lblStock")).Text = footerTable.Rows[0]["stock"].ToString();
            ((TextBox)grdOrder.FooterRow.FindControl("IntegerControl1")).Text = footerTable.Rows[0]["qty"].ToString();
            ((TextBox)grdOrder.FooterRow.FindControl("txtPrice")).Text =
                string.Format("{0:F2}", double.Parse(footerTable.Rows[0]["price"].ToString())); 
            ((TextBox)grdOrder.FooterRow.FindControl("txtVat")).Text =
                string.Format("{0:F2}", double.Parse(footerTable.Rows[0]["vat"].ToString())); 


            /*
             * now check that if selected items already inserted into mainTable
             * if already in the table then set the insert button's enable property
             * to false.
             */
            for (int i = 0; i < mainTable.Rows.Count; i++)
            {
                string code = mainTable.Rows[i]["articlecode"].ToString();

                if (code.Equals(footerTable.Rows[0]["articlecode"].ToString()))
                {
                    ((LinkButton)grdOrder.FooterRow.FindControl("lnkInsert")).Visible = false;
                    return;
                }
            }
            //if the item is unique to already selected then enable the insert button
            ((LinkButton)grdOrder.FooterRow.FindControl("lnkInsert")).Visible = true;
        }

    }
    /// <summary>
    /// Load the article grid that to be selected
    /// </summary>
    /// /// <author>
    /// Abdullah Al Mohammad 
    /// </author>
    /// <last updated>
    /// Last updated - 19/07/2007
    /// </last>
    private void LoadgrdArticle()
    {
        //string qurery = @"select COALESCE(a.articlecode,'') as code, (case when lower(a.articletype)='s' then 'Sheet Music'  when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='c' then 'CD/DVD' end)as type,a.title, (case when char_length(a.descriptionen)>150 then substr(a.descriptionen,0,150) end) as description," +
        //                "a.quantity as qty,a.price as price,COALESCE(c.firstname,'')||' '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') as author "+
        //                "from article a, composer c where a.composer = c.composerid order by a.title asc";

//        string qurery = @"select COALESCE(a.articlecode,'') as code, (case when lower(a.articletype)='s' then 'Sheet Music'  when lower(a.articletype)='b' then 'Book' when lower(a.articletype)='c' then 'CD/DVD' end)as type,
//                         a.title,(COALESCE(a.editionno,'')) as editionno, a.quantity as qty,(case when lower(a.articletype)='s' then a.price * 0.65 when lower(a.articletype)='b' then a.price * 0.75 when lower(a.articletype)='c' then a.price * 0.70 end) as price,
//                         COALESCE(c.firstname,'')||' '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') as author,
//                         (COALESCE(p.firstname,'')||' '||COALESCE(p.middlename,'')||' '||COALESCE(p.lastname,'')) as publisher
//                         from article a left join composer c on a.composer=c.composerid left join publisher p  on a.publisher=p.publisherid
//                         order by a.title asc";


        DataTable articleTable = new Facade().GetArticleForSupplyOrder();
        Session["articleTable"] = articleTable;
        
        grdArticle.DataSource = articleTable;
        grdArticle.DataBind();
        //qurery = @"select coalesce(max(supplyorderid)+1,1) as orderid from supplyorders";
        DataTable tempTable = new Facade().GetMaxSupplyOrders();
        if (Request.Params["orderNo"] == null)
        {
            lblPrintSupplyOrderNo.Text = tempTable.Rows[0]["orderid"].ToString();
        }
        
    }
        

    /// <summary>
    /// the aim of this method is to make gird viewable without
    /// any data populated. because we need to view the grid
    /// with header and footer first time when there is no row in
    /// that grid. if this method finds the table without any row
    /// then it makes an empty row and binds to it
    /// </summary>
    /// <param name="gridView">the gird to be populated</param>
    /// <param name="ds">data set</param>
    /// /// <author>
    /// Abdullah Al Mohammad 
    /// </author>
    /// <last updated>
    /// Last updated - 19/07/2007
    /// </last>
    private void BuildNoRecords(GridView gridView, DataSet ds)
    {
        try
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gridView.DataSource = ds;
                gridView.DataBind();
            }
            else
            {
                gridView.DataSource = ds;
                gridView.DataBind();
            }
        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
        }
    }

    /// <summary>
    /// this method works when the article name link is selected
    /// this selects the article from article grid and calls the 
    /// LoadOrdersLine() method to populate the footer of mainTable
    /// </summary>
    /// <param name="sender"> object sender</param>
    /// <param name="e"> CommandEventArgs</param>
    /// /// <author>
    /// Abdullah Al Mohammad 
    /// </author>
    /// <last updated>
    /// Last updated - 19/07/2007
    /// </last>
    protected void lnkSelect_Command(object sender, CommandEventArgs e)
    {
        string selectedCode = e.CommandArgument.ToString();//get the article code
        LoadOrdersLine(selectedCode);
        //string button = ""; bool flag = false;
        //foreach (GridViewRow row in grdArticle.Rows)
        //{
        //    LinkButton lButton = (LinkButton)row.Cells[0].FindControl("lnkArticle");
        //    button = lButton.CommandArgument.ToString();
        //    flag = false;
        //    if (button.Equals(selectedCode))
        //    {
        //        flag = true;
        //        LoadOrdersLine(flag, selectedCode);
        //        break;
        //    }            
            
        //}
    }
    

    /// <summary>
    /// the event method for insert button
    /// it populates the mainTable with the footer row data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// /// <author>
    /// Abdullah Al Mohammad 
    /// </author>
    /// <last updated>
    /// Last updated - 19/07/2007
    /// </last>
    protected void lnkInsert_Click(object sender, EventArgs e)
    {
       string empty = mainTable.Rows[0]["articlecode"].ToString();
       if (empty.Equals("") || empty == null)
       {
           mainTable.Rows.Remove(mainTable.Rows[0]);
           grdOrder.DataSource = mainTable;
           grdOrder.DataBind();
       }
        //when the footer contains data and the mainTable is created successfully
        if (footerTable.Rows.Count > 0 && mainTable.Columns.Contains("articlecode"))
        {
            if (!AreadyExist())
            {
                string qty = "", price = "", vat = "", supplyArticleID="";
                qty = ((TextBox)grdOrder.FooterRow.FindControl("IntegerControl1")).Text;
                price = ((TextBox)grdOrder.FooterRow.FindControl("txtPrice")).Text;
                supplyArticleID = ((TextBox)grdOrder.FooterRow.FindControl("txtSupplyOrderArticleCode")).Text;
                vat = ((TextBox)grdOrder.FooterRow.FindControl("txtVat")).Text;
                
                

                //Regex expression = new Regex(@"^\[^0|-]+[0-9]*[,]?[0-9]+$");
                //if (expression.Match(price).Success == true)
                //{

                    if (qty == null || qty.Equals(""))
                    {
                        qty = "1";
                    }
                    if (price == null || price.Equals(""))
                    {
                        price = footerTable.Rows[0]["price"].ToString();
                    }
                    if (vat == null || vat.Equals(""))
                    {
                        vat = footerTable.Rows[0]["vat"].ToString();
                    }
                    bool found = false;


                    // System.Globalization.NumberStyles.Number
                    DataRow row = mainTable.NewRow();
                    row["articlecode"] = footerTable.Rows[0]["articlecode"].ToString();
                    row["title"] = footerTable.Rows[0]["title"].ToString();
                    row["stock"] = footerTable.Rows[0]["stock"].ToString();
                    row["SupplyArticleID"] = supplyArticleID;

                
                    row["qty"] = qty;

                    try
                    {
                        row["price"] = string.Format("{0:F2}", double.Parse(price));
                    }
                    catch
                    {
                        row["price"] = string.Format("{0:F2}", double.Parse(footerTable.Rows[0]["price"].ToString()));
                    }
                    try
                    {
                        row["vat"] = string.Format("{0:F2}", double.Parse(vat));
                    }
                    catch
                    {
                        row["vat"] = string.Format("{0:F2}", double.Parse(footerTable.Rows[0]["vat"].ToString()));

                    }

                    //calculate the netprice
                    double net = (double.Parse(row["qty"].ToString()) * double.Parse(row["price"].ToString())) + (double.Parse(row["qty"].ToString()) * double.Parse(row["price"].ToString()) * double.Parse(row["vat"].ToString())) / 100;
                    //row["netprice"] = "€ " + string.Format("{0:F2}", net);//add the euro sign
                    row["netprice"] = string.Format("{0:F2}", net);//add the euro sign

                    // row["price"] = row["price"].ToString();//add the euro sign
                    mainTable.Rows.Add(row);
                    grdOrder.DataSource = mainTable;
                    grdOrder.DataBind();
                    lnkSubmit.Visible = true;
                    lnkSendOrder.Visible = true;
                    ((LinkButton)grdOrder.FooterRow.FindControl("lnkInsert")).Visible = false;//set the insert button's enable property to false        

                //}
            }

            else
            {
                grdOrder.DataSource = mainTable;
                grdOrder.DataBind();
               
                    lnkSubmit.Visible = true;
                    lnkSendOrder.Visible = true;
                
                //((LinkButton)grdOrder.FooterRow.FindControl("lnkInsert")).Visible = false;

            }
        }
    }

    private bool AreadyExist()
    {
        bool state = false;
        for (int i = 0; i < mainTable.Rows.Count; i++)
        {
            if(footerTable.Rows[0]["articlecode"].ToString().Equals(mainTable.Rows[i]["articlecode"].ToString()))
            {
                //mainTable.Rows.Remove(mainTable.Rows[i]);
                
                state = true;
                break;
            }
        }
        return state;
    }

    /// <summary>
    /// event method for drop down list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// /// <author>
    /// Abdullah Al Mohammad 
    /// </author>
    /// <last updated>
    /// Last updated - 19/07/2007
    /// </last>
    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSupplierAddress();        
    }

    
    
    /// <summary>
    /// event method for submit button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// /// <author>
    /// Abdullah Al Mohammad 
    /// </author>
    /// <last updated>
    /// Last updated - 19/07/2007
    /// </last>
    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            if (CheckForEmptyInitialRow())
            {
                try
                {
                    SaveOrders();
                    Response.Write("<script> parent.OBSettings.RefreshPage();</script>");
                }
                catch (Exception ex)
                {
                    Boeijenga.Common.Utils.LogWriter.Log(ex);
                    lblMessage.Text = ex.Message;
                }
            }
        }
    }

    
    protected void lnkSendOrder_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            msg = "sent";
            lnkSubmit_Click(sender, e);
            //if (!EmptyOrderFields())
            //{
                if (CheckForEmptyInitialRow())
                {
                    ConstructMail();
                }
            //}
        }
    }


    /// <summary>
    /// if the empty initial row exist then remove it
    /// </summary>
    /// <author>
    /// Abdullah Al Mohammad
    /// </author>
    /// <Last Updated>
    /// 20/07/2007
    /// </Last>
    /// <returns> returns true on empty</returns>
    private bool CheckForEmptyInitialRow()
    {
        string empty = mainTable.Rows[0]["articlecode"].ToString();
        if (empty.Equals("") || empty == null)
        {
            mainTable.Rows.Remove(mainTable.Rows[0]);
            grdOrder.DataSource = mainTable;
            grdOrder.DataBind();
            if (grdOrder.Rows.Count == 0)
            {
                LoadOrdersLine("");
                lnkSubmit.Visible = false;
                lnkSendOrder.Visible = false;
                //((LinkButton)grdOrder.FooterRow.FindControl("lnkInsert")).Visible = false;
                return false;
            }
        }
        return true;
    }

  
   
   /// <summary>
   /// the delete event method
   /// </summary>
   /// <author>
   /// Abdullah Al Mohammad
   /// </author>
   /// <updated>
   /// First Updated - 20/07/2007
   /// Last Updated - 22/07/2007
   /// </updated>
   /// <param name="sender"></param>
   /// <param name="e"></param>
    protected void grdOrder_RowDelete(object sender, CommandEventArgs e)
    {
        
        string selectedCode = e.CommandArgument.ToString();//get the article code
        string button = "";       
        int counter = mainTable.Rows.Count;
        bool state = false;
        counter = 0;
        if (selectedCode == null||selectedCode=="") return;

        for (int i = 0; i < mainTable.Rows.Count; i++)
        {
            button = mainTable.Rows[i]["articlecode"].ToString();
            if (selectedCode.Equals(button))
            {
                mainTable.Rows.Remove(mainTable.Rows[i]);
                state = true;
                grdOrder.DataSource = mainTable;
                grdOrder.DataBind();
                if (mainTable.Rows.Count == 0)
                {
                    LoadOrdersLine("");
                    lnkSubmit.Visible = false;
                    lnkSendOrder.Visible = false;
                }
                //((LinkButton)grdOrder.FooterRow.FindControl("lnkInsert")).Visible = false;//disable the insert button                        
                break;
            }

        }
        if (state == false)
        {
            grdOrder.DataSource = mainTable;
            grdOrder.DataBind();
            string empty = mainTable.Rows[0]["articlecode"].ToString();
            if (empty.Equals("") || empty == null)
              {
                 lnkSubmit.Visible = false;
                 lnkSendOrder.Visible = false;
              }
            else
            {
                lnkSubmit.Visible = true;
                lnkSendOrder.Visible = true;
            }
        }
        
    }

    /// <summary>
    /// to save the order
    /// </summary>
    /// <author>
    /// Abdullah Al Mohammad
    /// </author>
    /// <updated>
    /// First Updated - 20-07-2007
    /// </updated>
    /// <param name="queryOrders"> query for supply orders</param>
    /// <param name="queryOrdersLine">query for supply ordersline</param>
    private void SaveOrders()
    {
        string exception = "";
        //int value = 0;
        int order_num = mainTable.Rows.Count;

        if (Request.Params["orderNo"] != null)
        {
            ++order_num;
        }


        System.Globalization.CultureInfo enUS = new System.Globalization.CultureInfo("en-US", true);
        System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.DateTimeFormatInfo();
        dtfi.ShortDatePattern = "dd-MM-yyyy";
        dtfi.DateSeparator = "-";
   
        SupplyOrder objSupplyOrder = new SupplyOrder();
        objSupplyOrder.Dhousenr = txtDHouse.Text.Replace("<", "").Trim();
        objSupplyOrder.Daddress = txtDAddress.Text.Replace("<", "").Trim();
        objSupplyOrder.Dpostcode = txtDPostCode.Text.Replace("<", "").Trim();
        objSupplyOrder.Dresidence = txtDResidence.Text.Replace("<", "").Trim();
        objSupplyOrder.Dcountry = ddlDCountry.SelectedValue.ToString();
        objSupplyOrder.Supplyorderid = Int32.Parse(lblPrintSupplyOrderNo.Text);
        objSupplyOrder.Supplyorderdate = DateTime.Parse(txtOrderDate.Text.Trim(), dtfi);
        objSupplyOrder.Supplierid = Int32.Parse(ddlSupplier.SelectedValue.ToString());
        objSupplyOrder.Deliverydate = DateTime.Parse(txtDeliveryDate.Text.Trim(),dtfi);
        objSupplyOrder.Supplyorder_by =  txtOrderBy.Text.Replace("<", "").Trim();
        objSupplyOrder.Receivingstatus = "N";
        objSupplyOrder.Paymentstatus = "U";
        

        if (new Facade().SaveSupplyOrder(objSupplyOrder, mainTable, (object)Request.Params["orderNo"], order_num, ref msg))
        {
            if (!msg.Equals("sent"))
            {
                msg = "saved";
            }

            lblMessage.Text = "You have successfully " + msg + " order # " + lblPrintSupplyOrderNo.Text + ".";
            msg = "";
            //lnkSubmit.Visible = false;
            //lnkSendOrder.Visible = false;
            //grdOrder.DeleteRow(grdOrder.Rows.Count);
            //grdOrder.DataSource = null;
            //grdOrder.DataBind();

        }
        else
        {
            lblMessage.Text = exception;
        }
    }

    private void ConstructMail()
    {        
        string euroUnicode = "&#8364;";
        string[] colorArray ={ "white", "#EFEFEF" };
        string imagePath = System.Configuration.ConfigurationManager.AppSettings["web-graphics"].ToString();
        string headerPath = imagePath + "mail_header.gif";
        string footerPath = imagePath + "mail_footer.gif";
        // the header of the mail
        string addHeader = @"<style type='text/css'>
        <!--
        body {
	            background-color: #e2e2e2;
             }
        body,td,th {
	                font-family: Tahoma, Arial, Helvetica, sans-serif, Verdana;
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
            <td><img src='" + headerPath + @"' width='680' height='83' /></td>
        </tr>
        <tr>
            <td bgcolor='#FFFFFF'><table width='680' border='0' cellspacing='0' cellpadding='8'>
        <tr>
            <td align='left' valign='top'>";
        //the footer of the mail
        string addFooter = @"</td>
            </tr>
          </table></td>
        </tr>
        <tr>
            <td><img src='" + footerPath + @"' width='680' height='29' /></td>
        </tr>
        </table></center>
        </body>";
        // a table that contains the supplier address and the delivery address
        string supplyOrders = "<table border='0' width=650px align = \"center\" border=\"0\">" +
           
            "<tr>" +
                "<td align=\"left\" style=\"width:25px;align: left;\"><b>Supplier: </b>"+              
                "</td>" +
                "<td align=\"left\" style=\"width:365px;align: left;\">" + lblPrintSupplierName.Text +
                "</td>" +
                "<td align=\"right\" style=\"width:110px;align: right;\"><b>Delivery Address: </b>" +
                "</td>" +
                "<td align=\"left\" style=\"width:150px;align: left;\">" + txtOrderBy.Text +
                "</td>" +
            "</tr>" +
            "<tr>" +
                "<td align=\"left\" style=\"width:25px;align: left;\">" +
                "</td>" +
                "<td align=\"left\" style=\"width:365px;align: left;\">" + lblPrintSupplierAddress.Text + "." +
                "</td>" +
                "<td align=\"right\" style=\"width:110px;align: right;\">" +
                "</td>" +
                "<td align=\"left\" style=\"width:150px;align: left;\">" + txtDHouse.Text + ", " + txtDAddress.Text + "<br>" +
                txtDPostCode.Text + ", " + txtDResidence.Text + "<br>" + ddlDCountry.SelectedItem.ToString() + "." +
                "</td>" +
                "</tr></table>";
        //creating a table that contains the products ordered to the supplier
        string product = "<table border='0' cellpadding=3 cellspacing=0 width=650px align=\"center\" border=\"0\">" +
            "<tr><td  align=left style='font-size:15px;'><font color=red><b>Order No# " + lblPrintSupplyOrderNo.Text + "</b></font> </td>" +
           "<tr> <td  align='right'>Order Date: &nbsp;&nbsp;&nbsp;&nbsp;" + txtOrderDate.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "</td>" +
            "<tr> <td  align='right'>Delivery Date: " + txtDeliveryDate.Text + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "</td>" +
             "</tr>" +
             "<tr><td  width=650px>" +
             supplyOrders +
             "</td></tr>" +
             "<tr><td >&nbsp;</td></tr>" +
             "<tr><td ><b>Order Details:</b></td></tr>" +
             "<tr>" +
             "<td width=650px style='background-color:#DEDEDE;'><table width=650px cellpadding=0 cellspacing=0>" +

                 
                 "<tr style='background-color:#DEDEDE;'>" +
                 "<td  width=80px align=\"left\"><b>" + "Article" +
                 "</b></td>" +
                 "<td  width=100px align=\"left\"><b>" + "Supplyer Article Code" +
                 "</b></td>" +
                 "<td  width=220px align=\"left\"><b>" + "Product Description" +
                 "</b></td>" +
                 "<td  width=80px align=\"right\"><b>" + "Qty" +
                 "</b></td>" +
                 "<td  width=80px align=\"right\"><b>" + "Price" +
                 "</b></center></td>" +
                 "<td  width=60px align=\"right\"><b>" + "VAT(%)" +
                 "</b></center></td>" +
                 "<td  width=130px align=\"right\" style=\"align: right;\"><b>" + "Net Price" +
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
             "</tr>";
        int colorIndex = 0;
        double grandTotal = 0.0;
        foreach (DataRow row in mainTable.Rows)
        {
            product += "<tr style='background-color:" + colorArray[colorIndex]+"'><td width=80px align=\"left\" style=\"align: left;\">" + row["articlecode"].ToString()+
                        "</center></td>" +                        

                        "<td width=100px   align=\"left\" style=\"align: left;\">" + row["supplyarticleid"].ToString() +
                        "</center></td>" +

                        "<td width=275px   align=\"left\" style=\"align: left;\">" + row["title"].ToString() +
                        "</center></td>" +

                        "<td  width=80px align=\"right\" style=\"align: right;\">" +  row["qty"].ToString() +
                        "</center></td>" +

                        "<td width=80px  align=\"right\" style=\"align: right;\">" +euroUnicode+" "+ row["price"].ToString() +
                        "</center></td>" +

                        "<td width=60px  align=\"right\" style=\"align: right;\">" + row["vat"].ToString() +
                        "</center></td>" +

                        "<td width=125px  align=\"right\" style=\"align: right;\">" + euroUnicode + " " + row["netprice"].ToString() +
                        "</center></td>" +
                        "</tr>";
            grandTotal += double.Parse(row["netprice"].ToString());
            if (colorIndex == 0) { colorIndex = 1; } else { colorIndex = 0; }

        }
        product += "<tr style='background-color:#DEDEDE;'><td colspan='6'  align=\"right\"><b>Total Price</b> (exclusive shipping costs)<b>:</b>" +
                "</td>" +
                "<td colspan='1' align=\"right\"><b>" + euroUnicode + " " + string.Format("{0:F2}", grandTotal) +
                "</b></td></tr></table></td>" +
                "</tr></table>";
        
        string maintainEmail = "<table  align = \"center\" border=\"0\">" +

                "<tr><td align=\"center\">" + product + "</td><tr>" +
                "</table>";
        // now adding all tables 
        addHeader += maintainEmail + addFooter;      
        //now send mail
        senMail(addHeader);
    }
    private void senMail(string maintainEmail)
    {
       // string query = @"select coalesce(email,'') as email from publisher  where publisherid ='"+ddlSupplier.SelectedValue.ToString()+"'";
       DataTable dt = new Facade().GetEmailAddressFromPublisherByPublisherId(ddlSupplier.SelectedValue.ToString());
       if (!dt.Rows[0]["email"].ToString().Equals(""))
        {
            try
            {
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(System.Configuration.ConfigurationManager.AppSettings["mail-server"]);
                System.Net.Mail.MailAddress fromAddr = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["mail-company"]);
                System.Net.Mail.SmtpPermission sett = new System.Net.Mail.SmtpPermission(System.Security.Permissions.PermissionState.Unrestricted);
                //client.UseDefaultCredentials = true;
                System.Net.Mail.MailAddress toAddr = new System.Net.Mail.MailAddress(dt.Rows[0]["email"].ToString());
                //System.Net.Mail.MailAddress toAddr = new System.Net.Mail.MailAddress("saiketpodder@yahoo.com");
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(fromAddr, toAddr);
                MailAddress copy = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["mail-company"]);
                message.CC.Add(copy);
                message.Subject = "Order From Boeijenga";
                message.Body = maintainEmail;
                message.IsBodyHtml = true;
                client.Send(message);
            }
            catch (Exception ex)
            {
                Boeijenga.Common.Utils.LogWriter.Log(ex);
                throw new Exception("SMTP Server Error: " + ex.Message);
            }
        }
       else
        {
           lblMessage.Text = "Order can not be send! email address is invalid!!";
        }
    }



    //protected void lnkArticleFilter_Click1(object sender, EventArgs e)
    //{
    //    if (!ddlArticleFilter.SelectedValue.ToString().Equals("all"))
    //    {

    //        string searchText = txtFilter.Text.Replace("'", "''");
    //        DataTable articleTable = (DataTable)Session["articleTable"];
    //        DataTable tempTable = articleTable.Copy();
    //        tempTable.Clear();
    //        try
    //        {
    //            DataRow[] row = articleTable.Select(ddlArticleFilter.SelectedValue.ToString() + " LIKE '*" + searchText.Trim() + "*'", "title");
    //            tempTable.Clear();

    //            for (int i = 0; i < row.Length; i++)
    //            {
    //                tempTable.ImportRow(row[i]);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Boeijenga.Common.Utils.LogWriter.Log(ex);
    //            throw new Exception(ex.Message);
    //        }

    //        grdArticle.DataSource = tempTable;
    //        grdArticle.DataBind();
    //    }
    //    else
    //    {
    //        LoadgrdArticle();
    //    }
    //    RegisterStartupScript("popup", "<script type='text/javascript'>PopupArticle();</script>");
    //    //Response.Write("<script type='text/javascript'>PopupArticle();</script>");
    //}
    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        Response.Write("<script> parent.OBSettings.RefreshPage();</script>");
    }


    protected void grdArticle_Sorting(object sender, GridViewSortEventArgs e)
    {
        string dir = "ASC";
        ViewState["sortExpr"] = e.SortExpression;
        if (ViewState["sortdir"] != null)
        {
            if (ViewState["sortdir"].ToString().Equals("ASC"))
            {
                ViewState["sortdir"] = dir = "DESC";
            }
            else
            {
                ViewState["sortdir"] = dir = "ASC";
            }
        }
        else
        {
            ViewState["sortdir"] = dir = "DESC";
        }
        BindArticleGrid(e.SortExpression, dir, 0, pageSize);
    }

    private void BindArticleGrid(string orderBy, string dir, long offset, int limit)
    {
        DataRecord recordSet = new Facade().GetArticleInfo(orderBy, dir, offset, pageSize);
        grdArticle.VirtualItemCount = int.Parse(recordSet.Count.ToString());
        grdArticle.DataSource = recordSet.Table;
        grdArticle.DataBind();
    }

    protected void grdArticle_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdArticle.PageIndex = e.NewPageIndex;
        BindArticleGrid(ViewState["sortExpr"].ToString(), ViewState["sortdir"].ToString(), pageSize * e.NewPageIndex, pageSize);
    }
    protected void grdArticle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //string rowType = e.Row.RowType.ToString();
        //if (rowType.ToLower().Equals("pager"))
        //{
        //    GridViewRow pager = e.Row;
        //    TextBox tbFilter = new TextBox();
        //    pager.Cells[0].Controls.Add(tbFilter);   
        //    int temp = 1;
        //}
    }
}

