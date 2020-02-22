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

public partial class order : BasePage
{
    DbHandler dbHandler = new DbHandler();
    protected void Page_Load(object sender, EventArgs e)
    {
        setDatagrid();

    }

    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {        
        InitializeComponent();
        base.OnInit(e);
    }
   
    private void InitializeComponent()
    {
        this.btnupdate.Click += new System.Web.UI.ImageClickEventHandler(this.btnupdate_click);
        this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_click);
    }
    #endregion	
    private void setDatagrid()
    {
        String sql = "select * from article";
        DataTable dt = dbHandler.GetDataTable(sql);
        grdTest.DataSource = dt;
        grdTest.DataBind();


    }

    protected void btnupdate_click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("http://localhost:1830/bo01/home.aspx");
    }
    protected void btnNext_click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("http://localhost:1830/bo01/webshop.aspx");
    }
    
}
