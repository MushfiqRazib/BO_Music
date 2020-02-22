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

public partial class newsdetails : BasePage
{
	DbHandler dbHandler = new DbHandler();
	
	String searchType = "";
	String searchQuery = "";
	ArrayList visitPageList;
	string cultureName = "";    
    protected void Page_Load(object sender, EventArgs e)
    {
		Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);

        setVisitPageList(Functions.GetPageName(Request.Url.ToString()));
		//GetVisitedDepth();

		if(!IsPostBack)
		{
			SetCulture();
			SetCulturalValue();
            LoadNews();
			
		}
		

    }
	void Master_MenuButton(object sender, EventArgs e)
	{
		Session["cultureName"] = Master.CurrentButton.ToString();
		SetCulture();
		SetCulturalValue();
	}
	private void SetCulture()
	{
		if (Session["cultureName"] != null)
		{
			cultureName = Session["cultureName"].ToString();
		}
		else
		{
			cultureName = HttpContext.Current.Request.UserLanguages[0];
			Session["cultureName"] = cultureName;
		}
		Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
		Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);

	}

    //private void GetVisitedDepth()
    //{
    //    lblPageRoot.Text = func.getVisitedPage(visitPageList);
    //    lblActivePage.Text = func.getActivePage(visitPageList);
    //}
	
	protected void setVisitPageList(String pageName)// setting the visit page list
	{
		if (Session["visitPageList"] != null)
		{
			visitPageList = (ArrayList)Session["visitPageList"];

		}
		else
		{

			visitPageList = new ArrayList();
		}


        Session["visitPageList"] = Functions.initVisitPageList(visitPageList, pageName);
	}
	private void SetCulturalValue()
	{
		header.Style.Add("background-image", "url(graphics/" + (string)base.GetGlobalResourceObject("string", "headerNews") + ")");
		//lblCurrentPage.Text = (string)base.GetGlobalResourceObject("string", "currentpage");
		btnGoback.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnGoBack");
		btnNewsArchive.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnNewsArchive");
	}
	protected string ShowImage(string filename)
	{
		string imageFile = ConfigurationManager.AppSettings["resources"].ToString() + "newsimage//" + filename;
		string ImageUrl="";
        if (Functions.FileExist(imageFile) && Functions.IsImageFile(imageFile))
		{
			ImageUrl = "<img align='left' border='0' hspace=8 vspace=0 src='resources//newsimage/"+filename+"' />";
		}
		return ImageUrl;

	}
	private void LoadNews()
	{
        string sqlQueryMore = @"Select subject, to_char(newsdate,'DD-Mon-YYYY') as date,description,newsimagefile from news where shownews=true  order by newsdate desc";
        grdNews.DataSource = dbHandler.GetDataTable(sqlQueryMore);       
		grdNews.DataBind();

	}
	protected void btnGoback_Click(object sender, ImageClickEventArgs e)
	{
		if (visitPageList.Count > 1)
		{
			string test = Request.Url.ToString();
			Response.Redirect(visitPageList[visitPageList.Count - 2].ToString());
		}
		else
		{
			Response.Redirect("home.aspx");
		}
	}
    
    protected void btnNewsArchive_Click1(object sender, ImageClickEventArgs e)
    {       
        string sqlQuery = @"Select subject, to_char(newsdate,'DD-Mon-YYYY') as date,description,newsimagefile from news where shownews=false  order by newsdate desc";
        grdNews.DataSource = dbHandler.GetDataTable(sqlQuery);
        grdNews.DataBind();
    }
}
