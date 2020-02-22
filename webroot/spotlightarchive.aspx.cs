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
using Boeijenga.Common.Objects;

public partial class spotlightarchive : BasePage
{
	DbHandler dbHandler = new DbHandler();
	
	ArrayList visitPageList;
	string cultureName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
		Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);

        setVisitPageList(Functions.GetPageName(Request.Url.ToString()));
		//GetVisitedDepth();
		
		if (!IsPostBack)
		{
			SetCulture();
			
		}
		SetCulturalValue();
		LoadSpotlightArchive();
		
	}
	void Master_MenuButton(object sender, EventArgs e)
	{
		Session["cultureName"] = Master.CurrentButton.ToString();
		SetCulture();
		SetCulturalValue();
		LoadSpotlightArchive();
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

	private void GetVisitedDepth()
	{
        lblPageRoot.Text = Functions.getVisitedPage(visitPageList);
        lblActivePage.Text = Functions.getActivePage(visitPageList);
	}

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
		header.Style.Add("background-image", "url(graphics/" + (string)base.GetGlobalResourceObject("string", "headerSpotlight") + ")");
		//lblCurrentPage.Text = (string)base.GetGlobalResourceObject("string", "currentpage");
		btnGoback.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnGoBack");
	}
	protected string ShowImage(string filename)
	{
		string imageFile = ConfigurationManager.AppSettings["resources"].ToString() + "images//" + filename;
		string ImageUrl = "";
        if (Functions.FileExist(imageFile) && Functions.IsImageFile(imageFile))
		{
			ImageUrl = "<img align='left' border='0' hspace=8 vspace=0 src='resources//images/" + filename + "' />";
		}
		return ImageUrl;

	}
	protected string ShowButonImage(string btnName)
	{
		string ImageSrc = "graphics//" + (string)base.GetGlobalResourceObject("string", btnName);
		return ImageSrc;
	}
	private void LoadSpotlightArchive()
	{
		cultureName=Session["cultureName"].ToString ();
		string queryField="";
		if(cultureName.Equals("en-US"))
			queryField = "a.descriptionen";
		else
			queryField = "a.descriptionnl";

		string sqlQuery = @"
							select a.articlecode," + queryField + @" as description,a.title,round(a.price+round(a.price*cat.vatpc/100,2),2) as Price,a.imagefile,
							(case 	when lower(a.articletype)='b' then 
							a.isbn13 || ' - ' || a.pages when lower(a.articletype)='c' then 
							 a.publicationno || ' - '||a.period when lower(a.articletype)='s' then 
							 a.editionno || ' - '||a.duration else	' '	end	) as articleProperty,a.articletype 
                            from spotlight s,article a, category cat where (a.articlecode=s.article AND s.spotlight=FALSE and 
                            cat.categoryid =( 
                            select case 
                              when (position(',' in category)-1)<0 then 
                               category 
                              else 
                               substr (category, 1,position(',' in category)-1)
                              end
                             from article 
                             where articlecode=a.articlecode
                            ))";
		grdSpotlightArchive.DataSource = dbHandler.GetDataTable(sqlQuery);
		grdSpotlightArchive.DataBind();
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
	protected void btnQuickBuy_Command(object sender, CommandEventArgs e)
	{
        Label cartitem = (Label)Master.FindControl("lblCartItem");
        Session["CartItems"] = "1";
        cartitem.Text = Session["CartItems"].ToString();
		ArrayList cartTable = new ArrayList();
		string articleCode = e.CommandArgument.ToString();
		Order order = new Order(articleCode, 1);
		order = LoadOrderInfo(articleCode, order);
		order.publisherName = GetPublisherName(articleCode);  
		cartTable.Add(order);
		Session["order"] = cartTable;
		Response.Redirect("signup.aspx");

	}
	protected void btnDetail_Command(object sender, CommandEventArgs e)
	{
		string articleCode = e.CommandArgument.ToString();
		Response.Redirect("Details.aspx?" + "articlecode=" + articleCode);

	}
	private string GetPublisherName(string articleCode)
	{
		string sqlQuey = "select case when a.articletype='b' then (COALESCE(p.firstname) || ' '|| COALESCE(p.middlename,'') ||' '|| COALESCE(p.lastname) ) " +
							"else (COALESCE(c.firstname) ||' '|| COALESCE(c.middlename,'')  ||' '|| COALESCE(c.lastname) )end  as publisher " +
							"from article a, publisher p, composer c " +
							"where p.publisherid=a.publisher " +
							"and a.composer = c.composerid  and  articlecode='" + articleCode + "'";
		DataTable dtArticle = dbHandler.GetDataTable(sqlQuey);
        if (dtArticle.Rows.Count > 0)
            return dtArticle.Rows[0]["publisher"].ToString();
        else
            return "";
	}

	private Order LoadOrderInfo(string articleCode, Order order)
	{
        string sqlQuey = @"select title,COALESCE(subtitle,'') as subtitle, articletype,a.price as pricevat, round(a.price+round(a.price*c.vatpc/100,2),2) as Price ,vatpc from article a, category c where 
                           c.categoryid =( 
                            select case 
                              when (position(',' in category)-1)<0 then 
                               category 
                              else 
                               substr (category, 1,position(',' in category)-1)
                              end
                             from article 
                             where articlecode=a.articlecode
                            )
                           and articlecode='" + articleCode.Trim() + "'";
		DataTable dtArticle = dbHandler.GetDataTable(sqlQuey);
		order.productType = dtArticle.Rows[0]["articletype"].ToString();
		order.productdescription = dtArticle.Rows[0]["title"].ToString();
        order.subtitle = dtArticle.Rows[0]["subtitle"].ToString();
		order.vatIncludedPrice = Double.Parse(dtArticle.Rows[0]["price"].ToString());
        order.price = Double.Parse(dtArticle.Rows[0]["pricevat"].ToString());
        order.vatpc = Double.Parse(dtArticle.Rows[0]["vatpc"].ToString());
		return order;

	}
	protected string GetProperty(string articletype,string articleproperty)
	{
		string propertyPosfix="";
		string cultureProperty="";
		switch(articletype.ToLower())
		{
            case "b": cultureProperty = "PostfixBookProperty"; break;
            case "c": cultureProperty = "PostfixCdProperty"; break;
            case "s": cultureProperty = "PostfixMusicsheetProperty"; break;
			default: break;
		}
		propertyPosfix = (string)base.GetGlobalResourceObject("string", cultureProperty);
		return articleproperty+" "+propertyPosfix;
		
	}
	protected void grdSpotlightArchive_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		//Label lblProperty = (Label)grdSpotlightArchive.FindControl("Label1");
		//lblProperty.Text=;
	}
}
