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

using System.IO;
using Boeijenga.Common.Objects;

public partial class Details : BasePage
{
    DbHandler handler = new DbHandler();
  
    public string imageUrlGoBack ;
    ArrayList visitPageList;
    ArrayList cartTable = new ArrayList();
	string cultureName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);

        setVisitPageList(Functions.GetPageName(Request.Url.ToString()));
        //GetVisitedDepth();

        if (!IsPostBack)
        {
            SetCulture();
           
            SetCulturalValue();
			LoadData();
            ShowPDF();
        }
        else
        {
            if (!CheckParameter())
            {
                throw new Exception("No such articles found");
            }
        }
       
    }

    private void ShowPDF()
    {
        if (Request.Params["showpdf"] != null)
        {
            string editionNr = Request.Params["showpdf"].ToString();
            string query = "select pdffile from article where articlecode = '" + editionNr + "'";
            DataTable dtPdf = handler.GetDataTable(query);
            if (dtPdf.Rows.Count > 0)
            {
                string fileName = dtPdf.Rows[0]["pdffile"].ToString();
                if (!fileName.Equals(string.Empty))
                {
                    string pdfName = ConfigurationManager.AppSettings["resources"].ToString() + "pdf\\" + fileName;
                    DownLoadPDF(pdfName);
                }
                else
                {
                    throw new HttpException("PDF for the article '" + editionNr + "' not found.");
                }
            }
            else
            {
                throw new HttpException("Article #" + editionNr + " not found in the database!");
            }
        }
    }
    /// <summary>
    /// Reacts to Master Page menu click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void Master_MenuButton(object sender, EventArgs e)
    {
        string cultureName = Master.CurrentButton.ToString();
        Session["cultureName"] = cultureName;
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
        LoadData();
        SetCulturalValue();
    }


    protected string ShowButonImage(string btnName)
    {
        string ImageSrc = ConfigurationManager.AppSettings["web-graphics"].ToString() + (string)base.GetGlobalResourceObject("string", btnName);

        return ImageSrc;
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
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        if (visitPageList.Count > 1)
        {
            if (Request.Params["event"] != null)
            {
                Response.Redirect("searchresult.aspx?event=" + Request.Params["event"].ToString());
            }
            else
            {
                string test = Request.Url.ToString();
                Response.Redirect(visitPageList[visitPageList.Count - 2].ToString());
            }
        }
        else
        {
            Response.Redirect("home.aspx");
        }
        //Response.Redirect();
    }
    private void LoadData()
    {
        if (CheckParameter())
        {
            // string articleType = GetArticleType();

            string articleCode = Request.Params["articlecode"].ToString();

            /*string sqlSelectArticle = @"
                select a.articletype,a.grade, a.price,a.pdffile, 
                (case when a.title is null then '' else a.title end) as title,
                (case when a.description" + Session["cultureName"].ToString().Substring(0, 2) + @" is null then '' else a.description" + Session["cultureName"].ToString().Substring(0, 2) + @" end) as description,
                (case when a.imagefile is null then '' else a.imagefile end) as imagefile,
                (
                 (case when p.firstname is null then '' else p.firstname || ' ' end) || 
                 (case when p.middlename is null then '' else p.middlename || ' ' end) ||
                 (case when p.lastname is null then '' else p.lastname end)
                ) as publisher,
                (
                 (case when c.firstname is null then '' else c.firstname || ' ' end) || 
                 (case when c.middlename is null then '' else c.middlename || ' ' end) ||
                 (case when c.lastname is null then '' else c.lastname end)
                ) as author,
               (d.gradename" + Session["cultureName"].ToString().Substring(0, 2) + @") as gradename
               from article a, publisher p, composer c, grade d
               where p.publisherid=a.publisher
               and a.composer = c.composerid
               and a.articlecode='" + articleCode + @"'
               and d.gradeid = a.grade";*/

            // code@provas on 04-12-08
            // inserted the bussiness logic for editionnr into the query instead of c# to increase its performance
			string sqlSelectArticle= @"
                select a.articletype,a.grade,
                (case when a.articletype='c' then '' else (COALESCE(editionno, COALESCE(isbn13,isbn10))) end ) as editionno, a.containsmusic , round(a.price+round(a.price*cat.vatpc/100,2),2) as Price,a.pdffile, 
                (case when a.title is null then '' else a.title end) as title,a.subtitle,
                (case when a.descriptionen is null then '' else a.descriptionen end) as description,
                coalesce(a.imagefile, case when a.articletype='b' then 'book.png' when a.articletype='s' then  'musicsheet.png' when a.articletype='c' then  'CD.png' end)   as imagefile,
                (
                 (case when p.firstname is null then '' else p.firstname || ' ' end) || 
                 (case when p.middlename is null then '' else p.middlename || ' ' end) ||
                 (case when p.lastname is null then '' else p.lastname end)
                ) as publisher,
                (
                 (case when c.firstname is null then '' else c.firstname || ' ' end) || 
                 (case when c.middlename is null then '' else c.middlename || ' ' end) ||
                 (case when c.lastname is null then '' else c.lastname end)
                ) as author,
               (d.gradename" + Session["cultureName"].ToString().Substring(0, 2) + @") as gradename,
               a.instrumentation as instrumentation
               from article a left join 
	            publisher p on p.publisherid=a.publisher left join 
	            composer c on a.composer = c.composerid left join grade d on d.gradeid = a.grade
                left join category cat on 
                cat.categoryid =( 
                            select case 
                              when (position(',' in category)-1)<0 then 
                               category 
                              else 
                               substr (category, 1,position(',' in category)-1)
                              end
                             from article 
                             where articlecode=a.articlecode
                            )
               where 
               a.articlecode='" + articleCode + @"'";
               
            DataTable dtArticle = handler.GetDataTable(sqlSelectArticle);
            if (dtArticle.Rows.Count > 0)
            {
                btnPlay.Attributes.Add("onclick", "javascript:return OpenPlayer('" + articleCode + "')");
                btnPlay.Visible = Convert.ToBoolean(dtArticle.Rows[0]["containsmusic"].ToString());
                btnAddtoPlay.Attributes.Add("onclick", "javascript:return AddToPlayList('" + articleCode + "','" + dtArticle.Rows[0]["title"].ToString() + "')");//
                btnAddtoPlay.Visible = Convert.ToBoolean(dtArticle.Rows[0]["containsmusic"].ToString());

                //code@provas on 04-12-08
                lblisbn13.Text = "Edition No";
                if (dtArticle.Rows[0]["editionno"].ToString() != "")
                {
                    lblisbn13Value.Text = dtArticle.Rows[0]["editionno"].ToString();
                }
                else
                {
                    lblisbn13Value.Visible = false;
                    lblisbn13.Visible = false;
                }
                if (dtArticle.Rows[0]["instrumentation"].ToString().Length == 0)
                {
                    lblInstrumentation.Visible = false;
                }
                lblAuthorValue.Text = dtArticle.Rows[0]["author"].ToString();
                //lblDegreeValue.Text = dtArticle.Rows[0]["gradename"].ToString();
                lblDescriptionValue.Text = dtArticle.Rows[0]["description"].ToString();
                //lblPriceValue.Text = dtArticle.Rows[0]["description"].ToString();
                lblInstrumentationValue.Text = dtArticle.Rows[0]["instrumentation"].ToString();
                lblPublisherValue.Text = dtArticle.Rows[0]["publisher"].ToString();
                lblTitleValue.Text = dtArticle.Rows[0]["title"].ToString();
                lblSubTitle.Text = dtArticle.Rows[0]["subtitle"].ToString();
                lblPriceValue.Text = "<b>€ " + string.Format("{0:F2}", double.Parse(dtArticle.Rows[0]["price"].ToString())) + "</b>";
                LoadResources(dtArticle);
            }
        }
        }

    //private bool GetArticleType()
    //{      

    //    string articleCode = Request.Params["articlecode"].ToString();
    //    string query = @"SELECT articletype FROM article WHERE articlecode = '" + articleCode + "'";
    //    DataTable dtArticle = handler.GetDataTable(query);

    //    string type = dtArticle.Rows[0]["articletype"].ToString();
    //    if (type == "b") return true;
    //    else
    //        return false;
    //}
    
    private void LoadResources(DataTable table)
    {
        string filename = table.Rows[0]["imagefile"].ToString();
        string fileFullname = ConfigurationManager.AppSettings["resources"].ToString()
            + "images\\" + filename;
        if (filename != null && Functions.FileExist(fileFullname) && Functions.IsImageFile(fileFullname))
        {
            imgArticle.ImageUrl = ConfigurationManager.AppSettings["web-resources"].ToString()+"images/" + filename;
            imgArticle.Visible = true;
        }
        else
        {
            imgArticle.Visible = false;
        }
        filename = table.Rows[0]["pdffile"].ToString();
        fileFullname = ConfigurationManager.AppSettings["resources"].ToString()+ "pdf\\" + filename;
        if (filename != "" && Functions.FileExist(fileFullname))
        {
            btnPreviewPdf.CommandArgument = table.Rows[0]["pdffile"].ToString();
            btnPreviewPdf.Visible = true;
        }
        else
        {
            btnPreviewPdf.Visible = false;
        }
    }
    private void SetCulturalValue()
    {
		header.Style.Add("background-image", "url(graphics/" + (string)base.GetGlobalResourceObject("string", "headerDetails") + ")");
        btnPreviewPdf.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnPreviewPdf");
        btnQuickBuy.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnQuickBuy");
        btnAddToCart.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnAddToCart");
        btnGoBack.ImageUrl = "graphics/"+(string)base.GetGlobalResourceObject("string", "btnGoBack");
        if (CheckParameter())
        {
            string articleCode = Request.Params["articlecode"].ToString();
            string query = @"SELECT articletype FROM article WHERE articlecode = '" + articleCode + "'";
            DataTable dtArticle = handler.GetDataTable(query);

            if (dtArticle.Rows.Count > 0)
            {
                string type = dtArticle.Rows[0]["articletype"].ToString();
                if (type == "b")
                {
                    lblAuthor.Text = (string)base.GetGlobalResourceObject("string", "author");
                    //lblDegree.Text = (string)base.GetGlobalResourceObject("string", "degree");
                }
                else
                {
                    lblAuthor.Text = (string)base.GetGlobalResourceObject("string", "lblComposer");
                    //lblDegree.Text = (string)base.GetGlobalResourceObject("string", "lblLabel");
                }

                lblPrice.Text = (string)base.GetGlobalResourceObject("string", "price");
                lblPublisher.Text = (string)base.GetGlobalResourceObject("string", "publisher");
                lblTitle.Text = (string)base.GetGlobalResourceObject("string", "title");
                lblDescription.Text = (string)base.GetGlobalResourceObject("string", "description");
                lblInstrumentation.Text = (string)base.GetGlobalResourceObject("string", "instrumentation");
                //lblCurrentPage.Text = (string)base.GetGlobalResourceObject("string", "currentpage");
            }
			//else
			//{                
			//    throw new Exception("No such articles found");
			//}
        }
    }
    private bool CheckParameter()
    {
        return (Request.Params["articlecode"] != null && !Request.Params["articlecode"].ToString().Equals(""));
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
    //private void GetVisitedDepth()
    //{
    //    lblPageRoot.Text = func.getVisitedPage(visitPageList);
    //    lblActivePage.Text = func.getActivePage(visitPageList);
    //}
    protected void btnQuickBuy_Click(object sender, ImageClickEventArgs e)
    {
        if (CheckParameter())
        {
            //Label cartitem = (Label)Master.FindControl("lblCartItem");
            //Session["CartItems"] = "1";
            //cartitem.Text = Session["CartItems"].ToString();
            ArrayList cartTable = new ArrayList();
            string articleCode = Request.Params["articlecode"].ToString();
            Order order = new Order(articleCode, 1);
            order = LoadOrderInfo(articleCode, order);
            order.publisherName = GetPublisherName(articleCode);
            cartTable.Add(order);
            Session["order"] = cartTable;
            Response.Redirect("signup.aspx");
        }
    }
    protected void btnAddToCart_Click(object sender, ImageClickEventArgs e)
    {
        if (CheckParameter())
        {
            ArrayList cartTable = new ArrayList();
            if (Session["order"] != null)
            {
                cartTable = (ArrayList)Session["order"];
            }
            string articleCode = Request.Params["articlecode"].ToString();
            Order order = new Order(articleCode, 1);
            order = LoadOrderInfo(articleCode, order);
            order.publisherName = GetPublisherName(articleCode);
            cartTable.Add(order);
            Session["order"] = cartTable;
            
            //Event
            if (Request.Params["event"] != null)
            {
                Response.Redirect("shoppingcart.aspx?event=" + Request.Params["event"].ToString());
            }

            Response.Redirect("shoppingcart.aspx");
        }
    }
    protected void btnPreviewPdf_Click(object sender, ImageClickEventArgs e)
    {
        //string pdfName = "";
        string pdfName = ConfigurationManager.AppSettings["resources"].ToString() + "pdf\\" + btnPreviewPdf.CommandArgument.ToString();
        DownLoadPDF(pdfName);
        //Response.Write("<script language = javascript> window.open(\"" + pdfName + "\",\"\",\"resizable=yes,status=no,toolbar=yes,menubar=yes,location=no\" )</script> ");

    }
    private string GetPublisherName(string articleCode)
    {
        string sqlQuey = "select case when a.articletype='b' then (COALESCE(p.firstname) || ' '|| COALESCE(p.middlename,'') ||' '|| COALESCE(p.lastname) ) " +
                            "else (COALESCE(c.firstname) ||' '|| COALESCE(c.middlename,'')  ||' '|| COALESCE(c.lastname) )end  as publisher " +
                            "from article a, publisher p, composer c " +
                            "where p.publisherid=a.publisher " +
                            "and a.composer = c.composerid  and  articlecode='" + articleCode + "'";
        DataTable dtArticle = handler.GetDataTable(sqlQuey);
        //return dtArticle.Rows[0]["publisher"].ToString();
        if (dtArticle.Rows.Count > 0)
            return dtArticle.Rows[0]["publisher"].ToString();
        else
            return "";
    }
    private Order LoadOrderInfo(string articleCode, Order order)
    {
        string sqlQuey = @"select title,COALESCE(subtitle,'') as subtitle, articletype,round(a.price+round(a.price*c.vatpc/100,2),2) as Price,a.price as pricevat,vatpc from article a, category c 
        where c.categoryid =( 
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
        DataTable dtArticle = handler.GetDataTable(sqlQuey);
        order.productType = dtArticle.Rows[0]["articletype"].ToString();
        order.productdescription = dtArticle.Rows[0]["title"].ToString();
        order.subtitle = dtArticle.Rows[0]["subtitle"].ToString();
        order.vatIncludedPrice = Double.Parse(dtArticle.Rows[0]["price"].ToString());
        order.price = Double.Parse(dtArticle.Rows[0]["pricevat"].ToString());
        order.vatpc = Double.Parse(dtArticle.Rows[0]["vatpc"].ToString());
        return order;
    }

    /// <summary>
    /// This function will be responsible to download a PDF file to client
    /// </summary>
    /// <param name="fileName"></param>
    private void DownLoadPDF(string fileName)
    {
        try
        {
            FileInfo fx = new FileInfo(fileName);
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            byte[] data = new byte[(int)fs.Length];
            fs.Read(data, 0, (int)fs.Length);
            fs.Close();
            fs = null;
            //fx.Delete();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(data);
            Response.Flush();
            //Response.End();
        }
        catch (Exception ex)
        {
            Response.ContentType = "application/pdf";
            Response.Expires = -1;
            Response.Buffer = true;
            Response.Write("<html><pre>" + ex.ToString() +
                "</pre></html>");
        }
    }

}
