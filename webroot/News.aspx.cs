using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Globalization;
using System.Threading;
using Boeijenga.Common.Objects;
using Boeijenga.Business;
using Boeijenga.DataAccess;
using Npgsql;

public partial class news : BasePage
{

    int CurrentPage
    {
        get
        {

            if (ViewState["CURRENT_PAGE"] != null)
            {

                return Int32.Parse(ViewState["CURRENT_PAGE"].ToString());

            }
            else return 1;

        }

        set
        {

            ViewState["CURRENT_PAGE"] = value;

        }
    }

    int NEWS_TOTAL_DATA_COUNT
    {

        get
        {
            if (ViewState["NEWS_TOTAL_DATA_COUNT"] != null)
            {

                return Int32.Parse(ViewState["NEWS_TOTAL_DATA_COUNT"].ToString());
            }

            return 0;


        }

        set
        {

            ViewState["NEWS_TOTAL_DATA_COUNT"] = value;

        }

    }

    public string ApplicationNewsImagePath = System.Configuration.ConfigurationManager.AppSettings["news-imageuri"];
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.

        Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);
        Master.SetVisitPageList(Functions.GetPageName(Request.Url.ToString()));
        if (!IsPostBack)
        {
            Master.SetCulture();
            InitParameter();
            LoadNewsGrid();
        }
    }

    private void InitParameter()
    {
        if (Request.Params["page"] != null)
        {
            int pageNum = 1;
            Int32.TryParse(Request.Params["page"].ToString(), out pageNum);

            if (pageNum > 0)
            {
                CurrentPage = pageNum;
            }
        }
        else
        {

            CurrentPage = 1;
        }
    }

    protected string GetNewsImagePath(string imageName)
    {

        return string.Format(ApplicationNewsImagePath, imageName + "&");

    }


    private void LoadNewsGrid()
    {
       string moreStr = (string)base.GetGlobalResourceObject("string", "more");
       string lessStr = (string)base.GetGlobalResourceObject("string", "less");
        string sqlNews = @"
            Select upper(title) as title, newsimagefile,to_char(newsdate,'DD-Mon-YYYY') as date,
            (case when char_length(description)>220 then 
	            substr(description,0,220) 
            else
	            description
            end
            ) as description,description  as fulldescription
            from news 
            where shownews=true
            order by newsdate desc, subject asc  offset :offset limit :limit";

        string sqlNewsCounter = @"
            Select count(*) as totalcount
            from news 
            where shownews=true
            ";

        NpgsqlCommand newsCounterCommand = new NpgsqlCommand(sqlNewsCounter);

        NEWS_TOTAL_DATA_COUNT = Int32.Parse(DataAccessHelper.GetInstance().GetDataTable(newsCounterCommand).Rows[0]["totalcount"].ToString());

        NpgsqlCommand newsCommand = new NpgsqlCommand(sqlNews);
        newsCommand.Parameters.Add("offset", GetCurrentOffset(CurrentPage-1));
        newsCommand.Parameters.Add("limit",GetLimit());

        grdNews.DataSource = DataAccessHelper.GetInstance().GetDataTable(newsCommand);
        grdNews.DataBind();
        UpdatePager();
    }


    private int GetCurrentOffset(int pageNum)
    {
        return grdNews.PageSize * pageNum;
    }

    private int GetLimit()
    {
        return grdNews.PageSize;
    }

    protected void UpdatePager()
    {
        lblArticleCount.Text = NEWS_TOTAL_DATA_COUNT.ToString() + "  articles";
        if (NEWS_TOTAL_DATA_COUNT <= grdNews.PageSize)
        {

            return;
        }

        double d = (double.Parse(NEWS_TOTAL_DATA_COUNT.ToString()) / GetLimit());
        int totalPages = (int)Math.Ceiling(d);

        searchResultGridHeader.Controls.Clear();
        searchResultGridHeader.Controls.Add(new LiteralControl("<a rel='1'  class='pager_link' href='javascript:void(0)'>First</a>"));
        if ((CurrentPage) == 1)
        {

            searchResultGridHeader.Controls.Add(
        new LiteralControl("<a rel='" + (CurrentPage - 1).ToString() +
                           "'  class='pager_link_active' href='javascript:void(0)'>prev</a>"));

        }
        else if (CurrentPage > 1)
        {

            searchResultGridHeader.Controls.Add(
        new LiteralControl("<a rel='" + (CurrentPage - 1).ToString() +
                           "'  class='pager_link' href='javascript:void(0)'>prev</a>"));

        }





        int startIndex = 1;
        if ((CurrentPage - 2) > 0)
        {
            startIndex = CurrentPage - 2;

        }

        int loopCounter = 0;
        for (int i = startIndex; i <= totalPages; i++)
        {

            if (i == totalPages && (totalPages > 1))
            {

                searchResultGridHeader.Controls.Add(new LiteralControl("...."));

            }



            if (i == CurrentPage)
            {
                searchResultGridHeader.Controls.Add(new LiteralControl("<a rel='" + i.ToString() + "'  class='pager_link_active' href='javascript:void(0)'>" + i.ToString() + "</a>"));

            }
            else if (loopCounter < 4)
            {
                searchResultGridHeader.Controls.Add(new LiteralControl("<a rel='" + i.ToString() + "'  class='pager_link' href='javascript:void(0)'>" + i.ToString() + "</a>"));

            }
            else if (i == totalPages)
            {
                searchResultGridHeader.Controls.Add(new LiteralControl("<a  rel='" + i.ToString() + "' class='pager_link' href='javascript:void(0)'>" + i.ToString() + "</a>"));

            }

            loopCounter++;
        }

        if ((CurrentPage) == totalPages)
        {

            searchResultGridHeader.Controls.Add(
        new LiteralControl("<a rel='" + (CurrentPage + 1).ToString() +
                           "'  class='pager_link_active' href='javascript:void(0)'>next</a>"));

        }
        else if (CurrentPage < totalPages)
        {

            searchResultGridHeader.Controls.Add(
        new LiteralControl("<a rel='" + (CurrentPage + 1).ToString() +
                           "'  class='pager_link' href='javascript:void(0)'>next</a>"));

        }

        searchResultGridHeader.Controls.Add(new LiteralControl("<a rel='" + totalPages.ToString() + "'  class='pager_link' href='javascript:void(0)'>Last</a>"));


    }
    protected void btnContact_Click(object sender, EventArgs e)
    {
        Response.Redirect("contact.aspx");
    }


    void Master_MenuButton(object sender, EventArgs e)
    {
        Session["cultureName"] = Master.CurrentButton.ToString();
        InitParameter();
        LoadNewsGrid();
        
    }

    protected string GetDescriptionWithMoreIfRequired(string fullDescrip)
    {
        return DescriptionTextHandler.GetDescriptionWithMoreIfRequired(fullDescrip, (string)base.GetGlobalResourceObject("string", "more"));
    }
}
