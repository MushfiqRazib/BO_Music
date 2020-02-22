using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Web.UI.WebControls;
using Boeijenga.Business;
using Boeijenga.Common.Objects;
using Boeijenga.Common.Utils;



public partial class shoppingcart : BasePage
{
   
    ArrayList cartTable = new ArrayList();
    Order order;
    IEnumerator enu;
   
    
    ArrayList visitPageList;
    string cultureName = "";
    public double totalPrice = 0.0;
    int qty = 0;
    public double totalVat = 0.0;


    protected void Page_Load(object sender, EventArgs e)
    {
        
        Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);

        Master.SetVisitPageList(Functions.GetPageName(Request.Url.ToString()));
        if (!IsPostBack)
        {
            SetCulture();
            InitCartList();
            LoadDatagrid();
        }
       
    }
    
    private void SetCulture()
    {
        Master.SetCulture();
    }

 

   
    void Master_MenuButton(object sender, EventArgs e)
    {
        SaveOrder(grdOrder);
        string cultureName = Master.CurrentButton.ToString();
        Session["cultureName"] = cultureName;
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
        InitCartList();
        LoadDatagrid();
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Session["cultureName"] != null)
        {
            string cultureName = Session["cultureName"].ToString();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
        }
        SetObjectValue();
    }
    private void SetObjectValue()
    {
        lblHeader.Text = "1) " + (string)base.GetGlobalResourceObject("string", "basket");
        lblBasket.Text = "1) " + (string)base.GetGlobalResourceObject("string", "basket");
        lblLogReg.Text = "2) " + (string)base.GetGlobalResourceObject("string", "steplogin");
        lblDelAddress.Text = "3) " + (string)base.GetGlobalResourceObject("string", "stepDelivaery");
        lblPayment.Text = "4) " + (string)base.GetGlobalResourceObject("string", "stepPayment");
        lblOrderComplete.Text = "5) " + (string)base.GetGlobalResourceObject("string", "stepComplete");
     }

    private void InitCartList()
    {
        if (Session["order"] != null)
        {
            cartTable = (ArrayList)Session["order"];
        }
    }

    



    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void btnContinue_click(object sender, EventArgs e)
    {
        SaveOrder(grdOrder);
        //Session["CartItems"] = qty;
        Response.Redirect("signup.aspx");
    
    }

  
    #endregion

    private void LoadDatagrid()
    {
        
        InitOrderGrid(cartTable, grdOrder);
    }

    public void InitOrderGrid(ArrayList cartTable , GridView  grdOrder)
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
            //DataTable dtOrder = dbHandler.GetDataTable(sql);
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
            ArrayList shopItems = (ArrayList)Session["order"];
            enu = shopItems.GetEnumerator();
            while (enu.MoveNext())
            {
                DataRow row = dtOrder.NewRow();
                order = (Order)enu.Current;
                row["articlecode"] = order.articlecode.ToString();
                row["productType"] = order.productType.ToString();
                row["publisher"] = order.publisherName.ToString();
                row["title"] = order.productdescription.ToString();
                row["subtitle"] = order.subtitle.ToString();
                row["price"] = string.Format("{0:F2}", order.vatIncludedPrice);
                row["quantity"] = order.quantity.ToString();
                row["vatpc"] = string.Format("{0:F2}", order.vatpc);
                row["deliverytime"] = order.deliveryTime;
                qty += (int)order.quantity;
                Session["CartItems"] = qty;
                double total = Math.Round( (order.vatIncludedPrice * order.quantity),2 );


                double vatIncludePrice = VatCalculator.GetVatIncludedPrice(total, order.vatpc);

                totalPrice += total;
                row["total"] = string.Format("{0:F2}", total);
                dtOrder.Rows.Add(row);
                i++;
            }

            
            grdOrder.DataSource = dtOrder;
            grdOrder.DataBind();
            lblHeaderTotPrice.Text = (string)base.GetGlobalResourceObject("string", "total") + "  " + (string)base.GetGlobalResourceObject("string", "price");
            lblTotalPrice.Text = string.Format("{0:F2}", totalPrice);    
         }
        else
        {
            grdOrder.DataSource = null;
            grdOrder.DataBind();
            lblTotalPrice.Text = "0";
            grdOrder.Visible = false;
            lblEmptyCart.Visible = true;
            btnContinue.Enabled = false;
        }
    }


    protected string ShowArticleTypeImg(string articleType)
    {
        string articleTypePath = "graphics/";
        return Functions.GetArticleTypeImage(articleType, articleTypePath);
    }


    protected void SaveOrder(GridView gridView)
    {
        //Hashtable cartTable = new Hashtable();

        ArrayList cartTable = Functions.GetOrderGridData(gridView);

        Session["order"] = cartTable;
    }

    


    protected void btnaddmore_Click(object sender, EventArgs e)
    {
        SaveOrder(grdOrder);

        if (Session["lastvisitedsearchpage"] != null)
        {
            Response.Redirect(Session["lastvisitedsearchpage"].ToString(), true);
        }
        else
        {
            Response.Redirect("searchresult.aspx");
        }
    }
    protected void lnkDelete_Command(object sender, CommandEventArgs e)
    {
        string articleCode = e.CommandArgument.ToString();
        SaveOrder(grdOrder);
        if (Session["order"] != null)
        {
            cartTable = (ArrayList)Session["order"];
            IEnumerator enu = cartTable.GetEnumerator();
            while (enu.MoveNext())
            {
                Order order = (Order)enu.Current;
                if (order.articlecode.ToString().Equals(articleCode))
                {
                    cartTable.Remove(order);
                    cartTable.TrimToSize();
                    
                    break;
                }
            }
            Session["order"] = cartTable;
            InitCartList();
            LoadDatagrid();
            
        }
    }
    
    
    private string GetPublisherName(string articleCode)
    {
        return new Facade().GetPublisherName(articleCode);
    }
    private Order LoadOrderInfo(string articleCode, Order order)
    {
        return new Facade().LoadOrderInfo(articleCode, order);
    }

    protected void quantity_changed(object sender, EventArgs e)
    {
        SaveOrder(grdOrder);
        InitCartList();
        InitOrderGrid(cartTable, grdOrder);
    }
}