using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Boeijenga.Common.Objects;
using Boeijenga.DataAccess;
using Npgsql;
using System.Globalization;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using Boeijenga.Business;

public partial class SearchResult : BasePage
{
     DataTable searchData = new DataTable();
    DataTable currentSearchDataTable;
    private List<Category> categoryList = new List<Category>();

    private List<Category> bookCategoryList = new List<Category>();
    private List<Category> sheetmusicCategoryList = new List<Category>();
    private List<Category> cddvdCategoryList = new List<Category>();


    public string ApplicationImagePath = System.Configuration.ConfigurationManager.AppSettings["searchresult-article-imageuri"];
   

    int TotalNumberOfSheetmusic
    {

        get
        {
            
            if (ViewState["SHEETMUSIC_COUNT"] != null)
            {

                return Int32.Parse(ViewState["SHEETMUSIC_COUNT"].ToString());

            }
            else
            {
                return 0;
            }


        }

        set
        {

            ViewState["SHEETMUSIC_COUNT"] = value;

        }

    }


    int TotalNumberOfCDDVD
    {

        get
        {
            if (ViewState["CDDVD_COUNT"] != null)
            {

                return Int32.Parse(ViewState["CDDVD_COUNT"].ToString());

            }
            else
            {
                return 0;
            }


        }

        set
        {

            ViewState["CDDVD_COUNT"] = value;

        }

    }




    int TotalNumberOfBooks
    {

        get
        {
            if (ViewState["BOOK_COUNT"] != null)
            {

                return Int32.Parse(ViewState["BOOK_COUNT"].ToString());

            }
            else
            {
                return 0;
            }


        }

        set
        {

            ViewState["BOOK_COUNT"] = value;

        }

    }


    public String SearchType
    {
        get
        {
            if (ViewState["SEARCH_TYPE"] != null)
            {

                return ViewState["SEARCH_TYPE"].ToString();

            }
            else
            {
                return "%%";
            }
        }
        set
        {
            ViewState["SEARCH_TYPE"] = value;
        }

    }



    public String SubCategory
    {
        get
        {
            if (ViewState["SUBCATEGORY"] != null)
            {

                return ViewState["SUBCATEGORY"].ToString();

            }
            else
            {
                return "";
            }
        }
        set
        {
            ViewState["SUBCATEGORY"] = value;
        }

    }



    String SearchQuery
    {
        get
        {

            if (ViewState["SEARCH_QUERY"] != null)
            {

                return ViewState["SEARCH_QUERY"].ToString();

            }
            else return "";

        }

        set
        {

            ViewState["SEARCH_QUERY"] = value;

        }
    }


    String SortDirection
    {
        get
        {

            if (ViewState["SORT_DIRECTION"] != null)
            {

                return ViewState["SORT_DIRECTION"].ToString();

            }
            else return "asc";

        }

        set
        {

            ViewState["SORT_DIRECTION"] = value;

        }
    }



    String SortString
    {
        get
        {

            if (ViewState["SORT_STRING"] != null)
            {

                return ViewState["SORT_STRING"].ToString();

            }
            else return "title";

        }

        set
        {

            ViewState["SORT_STRING"] = value;

        }
    }


    bool IsShop
    {
        get
        {

            if (ViewState["SHOP"] != null)
            {

                return Boolean.Parse(ViewState["SHOP"].ToString());

            }
            else return false;

        }

        set
        {

            ViewState["SHOP"] = value;

        }
    }

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

    ArrayList visitPageList;
    ArrayList cartTable;
    string cultureName = "";
    int count = -1;

    string order = "asc";
    string[] sorten = { "Title", "Composer", "Publisher", "Price" };
    string[] sortnl = { "Titel", "Auteur", "Uitgever", "Prijs" };
    string[] sortvalue = { "title", "author", "p.firstname", "price" };

    protected void Page_Load(object sender, EventArgs e)
    {

        Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);
        // grdSearchResult.AlternatingRowStyle.BackColor="":

        setVisitPageList("searchresult.aspx");
        // GetVisitedDepth();
        if (!IsPostBack)
        {
            Session["lastvisitedsearchpage"] = Request.Url.ToString();
            LoadPageContent();

            UpdateTopTenLabel();
        }

        UpdatePager();


    }
    public void LoadSubCategory()
    {
        categoryList = new Facade().GetCategories(CultureInfo.CurrentCulture.Name);


        foreach (Category category in categoryList)
        {

            if (category.Categoryid.ToLower().StartsWith("s"))
            {

                sheetmusicCategoryList.Add(category);

            }
            else if (category.Categoryid.ToLower().StartsWith("b"))
            {
                bookCategoryList.Add(category);
            }
            else if (category.Categoryid.ToLower().StartsWith("c") || category.Categoryid.ToLower().StartsWith("d"))
            {

                cddvdCategoryList.Add(category);
            }
        }


    }

    private void LoadPageContent()
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



        LoadParameters();
        SetCulture();
        SetCulturalValue();
        LoadSubCategory();



        if (Request.Params["articlecode"] != null)
        {
            ShowArticles(Functions.AsCommaSeparatedSqlString(Request.Params["articlecode"].ToString()), GetCurrentOffset(CurrentPage - 1), GetLimit());

        }
        else if (Request.Params["composer"] != null)
        {
            ShowSearchResult(Request.Params["composer"].ToString(), GetCurrentOffset(CurrentPage - 1), GetLimit());
        }
        else if (Request.Params["event"] != null)
        {
            LoadPageSpecificInfo(SearchType);
            ShowSearchResult(SearchType, SearchQuery, SortString, SortDirection, GetCurrentOffset(CurrentPage - 1), GetLimit());
        }
        else
        {
            if (SearchType.Equals("a") || SearchType.Equals("k"))
            {
                SearchType = "%%";
            }



            if (CheckParameter(Request.Params["shop"]))
            {


                if (IsShop == true)
                {
                    LoadPageSpecificInfo(SearchType);
                    ShowSearchResultFromWebShop(SearchType, SearchQuery, SortString, SortDirection, GetCurrentOffset(CurrentPage - 1), GetLimit());
                }
                else
                {
                    LoadPageSpecificInfo("p");
                    ShowPublish(SortString, SortDirection, GetCurrentOffset(CurrentPage - 1), GetLimit());
                }
            }
            else
            {
                LoadPageSpecificInfo(SearchType);
                ShowSearchResult(SearchType, SearchQuery, SortString, SortDirection, GetCurrentOffset(CurrentPage - 1), GetLimit());
            }
        }
    }

    void Master_MenuButton(object sender, EventArgs e)
    {
        Session["cultureName"] = Master.CurrentButton.ToString();
        SetCulture();
        SetCulturalValue();
        LoadPageContent();

        UpdateTopTenLabel();
    }


    private int GetLimit()
    {
        return grdSearchResult.PageSize;
    }

    private int GetCurrentOffset(int pageNum)
    {
        return grdSearchResult.PageSize * pageNum;
    }

    private void ShowSearchResult(string composerid, int offset, int limit)
    {

        if (SearchType.Equals("k"))
        {
            SearchType = "%%";
        }

        string subcatQuery = GetSubCatQuery();
        string query = @"select a.articlecode,a.deliverytime,a.isbn10 as isbn,a.title,a.subtitle,a.articletype,a.pdffile, a.instrumentation,a.containsmusic,COALESCE(c.firstname,'')||' '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') as author,round(a.price+round(a.price*cat.vatpc/100,2),2) as Price,
             case when a.imagefile !='' then a.imagefile when  a.articletype='b' then 'book.png' when a.articletype='s' then  'musicsheet.png' when a.articletype='c' then  'CD.png' end as imagefile,
            a.description" + GetCultureStr() + @" as description,(COALESCE(p.firstname,'')||' '||COALESCE(p.middlename,'')||' '||COALESCE(p.lastname,'')) as publisher
            from category cat, article a left join composer c on a.composer=c.composerid  
            left join publisher p on a.publisher=p.publisherid  
            where a.composer in (" + composerid + @")
            and a.isactive=true
              " + subcatQuery + @"
 and a.articletype like :searchType
            order by " + SortString + " " + order + " offset :offset limit :limit  ;";


        NpgsqlCommand command = new NpgsqlCommand(query);
        //command.Parameters.Add("composer", composerid);
        command.Parameters.Add("searchType", SearchType);
        command.Parameters.Add("searchQuery", "%" + SearchQuery.Replace("'", "\'") + "%");
        command.Parameters.Add("offset", offset);
        command.Parameters.Add("limit", limit);

        string categoryCountSql = @"select a.articletype, case  when (position(',' in category)-1)<0 then 
                               category 
                              else 
                               substr (category, 1,position(',' in category)-1)
                              end	as category	  from category cat, article a left join composer c on a.composer=c.composerid  
            left join publisher p on a.publisher=p.publisherid  
            where a.composer in (" + composerid + @")
 " + subcatQuery + @"
 and a.articletype like :searchType
            and a.isactive=true";

        NpgsqlCommand commandCategoryCount = new NpgsqlCommand(categoryCountSql);
        commandCategoryCount.Parameters.Add("searchType", SearchType);
        DataTable totalCategoryCountTable = DataAccessHelper.GetInstance().GetDataTable(commandCategoryCount);
        NEWS_TOTAL_DATA_COUNT = totalCategoryCountTable.Rows.Count;

        CalculateCategoryCount(totalCategoryCountTable);
        CalculateSubcategory(totalCategoryCountTable);

        searchData = DataAccessHelper.GetInstance().GetDataTable(command);
        grdSearchResult.DataSource = searchData;
        grdSearchResult.DataBind();


        UpdateTotalDatatCountInfo(NEWS_TOTAL_DATA_COUNT);
        UpdatePager();
        UpdateCategoryFilter();
    }



    private void ShowArticles(string articleCode, int offset, int limit)
    {
          string articleTypeQuery = GetArticleTypeQuery();
        string subcatQuery = GetSubCatQuery();
        string query = @"select a.articlecode,a.deliverytime,a.isbn10 as isbn,a.title,a.subtitle,a.articletype,a.pdffile, a.instrumentation,a.containsmusic,COALESCE(c.firstname,'')||' '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') as author,round(a.price+round(a.price*cat.vatpc/100,2),2) as Price,
             case when a.imagefile !='' then a.imagefile when  a.articletype='b' then 'book.png' when a.articletype='s' then  'musicsheet.png' when a.articletype='c' then  'CD.png' end  as imagefile,
            a.description" + GetCultureStr() + @" as description,(COALESCE(p.firstname,'')||' '||COALESCE(p.middlename,'')||' '||COALESCE(p.lastname,'')) as publisher
            from category cat, article a left join composer c on a.composer=c.composerid  
            left join publisher p on a.publisher=p.publisherid  
            where a.articlecode in (" + articleCode + @") and
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

             " + subcatQuery + @"
             " + articleTypeQuery + @"
            order by " + SortString + " " + order + " offset :offset limit :limit  ;";


        NpgsqlCommand command = new NpgsqlCommand(query);
      
        command.Parameters.Add("searchQuery", "%" + SearchQuery.Replace("'", "\'") + "%");
        command.Parameters.Add("offset", offset);
        command.Parameters.Add("limit", limit);

        string categoryCountSql = @"select a.articletype , case  when (position(',' in category)-1)<0 then 
                               category 
                              else 
                               substr (category, 1,position(',' in category)-1)
                              end	as category	 from category cat, article a left join composer c on a.composer=c.composerid  
            left join publisher p on a.publisher=p.publisherid  
            where a.articlecode in (" + articleCode + @") and
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
 " + articleTypeQuery + @"
 " + subcatQuery + @"

           ";

        NpgsqlCommand commandCategoryCount = new NpgsqlCommand(categoryCountSql);
   
        DataTable totalCategoryCountTable = DataAccessHelper.GetInstance().GetDataTable(commandCategoryCount);
        NEWS_TOTAL_DATA_COUNT = totalCategoryCountTable.Rows.Count;

        CalculateCategoryCount(totalCategoryCountTable);

        CalculateSubcategory(totalCategoryCountTable);


        searchData = DataAccessHelper.GetInstance().GetDataTable(command);
        grdSearchResult.DataSource = searchData;
        grdSearchResult.DataBind();


        UpdateTotalDatatCountInfo(NEWS_TOTAL_DATA_COUNT);
        UpdatePager();
        UpdateCategoryFilter();
    }


    private void LoadPageSpecificInfo(string type)
    {
        switch (type.ToLower())
        {
            case "b": Page.Title = (string)base.GetGlobalResourceObject("string", "Book");
                sorten[1] = "Author";
                sortnl[1] = "Auteur";
                break;
            case "p": Page.Title = (string)base.GetGlobalResourceObject("string", "publisher");
                sorten[1] = "Composer";
                sortnl[1] = "Componist";
                break;
            case "c": Page.Title = (string)base.GetGlobalResourceObject("string", "CDDVD");
                sorten[1] = "Performer";
                sortnl[1] = "Uitvoerende";
                break;
            case "s": Page.Title = (string)base.GetGlobalResourceObject("string", "sheetmusic");
                sorten[1] = "Composer";
                sortnl[1] = "Componist";
                break;
            default: break;

        }
    }

    private void LoadParameters()
    {
        if (Request.Params["sorting"] != null)
        {

            string tempStr = Request.Params["sorting"].ToString();

            List<string> shortlist = new List<string> { "Title", "Composer", "Publisher", "Price" };

            if (shortlist.Find(n => n.ToLower() == tempStr.ToLower()) != null)
            {
                SortString = tempStr;
            }



        }

        if (Request.Params["sortdirection"] != null)
        {

            string tempStr = Request.Params["sortdirection"].ToString();

            List<string> shortlist = new List<string> { "asc", "desc" };

            if (shortlist.Find(n => n.ToLower() == tempStr.ToLower()) != null)
            {
                SortDirection = tempStr;
            }



        }


        if (Request.Params["searchtext"] != null)
        {

            SearchQuery = Request.Params["searchtext"].ToString();


        }


        if (Request.Params["type"] != null)
        {
            SearchType = Request.Params["type"];

        }

        if (Request.Params["subcat"] != null)
        {
            SubCategory = Request.Params["subcat"];

        }


        if (Request.Params["shop"] != null)
        {
            try
            {
                IsShop = Boolean.Parse(Request.Params["shop"]);
            }
            catch (Exception ex)
            {

                IsShop = false;
            }
        }
    }
    private void SetCulture()
    {
        Master.SetCulture();
    }


    private string GetSubCatQuery()
    {

        if (SubCategory != "")
        {

            return " and cat.categoryid = '" + SubCategory + "'";
        }

        return "";

    }

    private string GetArticleTypeQuery()
    {

        if (!SearchType.Equals("%%"))
        {

            return " and a.articletype in (" + Functions.AsCommaSeparatedSqlString(SearchType) + ")";
        }

        return "";

    }

    private void ShowSearchResultFromWebShop(String searchType, String searchQuery, String sort, String order, int offset, int limit)
    {

        string cultureName = GetCultureStr();

        string subcatQuery = GetSubCatQuery();

        string articleTypeQuery = GetArticleTypeQuery();

        string sqlSearch = @"
          select a.articlecode,a.isbn10 as isbn,a.deliverytime,a.title,a.subtitle,a.articletype, a.pdffile, a.instrumentation, a.containsmusic,COALESCE(c.firstname,'')||' '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') ||'&nbsp '
			||(case when (c.dob is not null and c.dob<>'') then '('||c.dob||(case when (c.dod is not null and c.dod <>'') then '-'||c.dod else '*' end)||')' else '*' end) as author,round(a.price+round(a.price*cat.vatpc/100,2),2) as Price,
           case when a.imagefile !='' then a.imagefile when  a.articletype='b' then 'book.png' when a.articletype='s' then  'musicsheet.png' when a.articletype='c' then  'CD.png' end as imagefile,
            a.description" + cultureName + @" as description,(COALESCE(p.firstname,'')||' '||COALESCE(p.middlename,'')||' '||COALESCE(p.lastname,'')) as publisher
              	
            from category cat,article a left join composer c on a.composer=c.composerid left join publisher p  on a.publisher=p.publisherid right join defaultwebshop d on d.article=a.articlecode
            where cat.categoryid =( 
                            select case 
                              when (position(',' in category)-1)<0 then 
                               category 
                              else 
                               substr (category, 1,position(',' in category)-1)
                              end
                             from article 
                             where articlecode=a.articlecode
                            )

            " + articleTypeQuery + @"
            " + subcatQuery + @"
            and a.isactive=true 
            
            and ( lower(title) like lower(:searchQuery) 
            or lower(coalesce(c.firstname||' ','')||trim(both '' from coalesce((case when c.middlename='' then null else c.middlename end)||' ',''))||coalesce(c.lastname,'')) like lower(:searchQuery)
            or lower(coalesce(p.firstname||' ','')||trim(both '' from coalesce((case when p.middlename='' then null else p.middlename end)||' ',''))||coalesce(p.lastname,'')) like lower(:searchQuery))
            order by " + sort + " " + order + " offset :offset limit :limit ";



        string sqlCategoryCount = @"
          select a.articletype, case  when (position(',' in category)-1)<0 then 
                               category 
                              else 
                               substr (category, 1,position(',' in category)-1)
                              end	as category	 
			from category cat,article a left join composer c on a.composer=c.composerid left join publisher p  on a.publisher=p.publisherid right join defaultwebshop d on d.article=a.articlecode
            where cat.categoryid =( 
                            select case 
                              when (position(',' in category)-1)<0 then 
                               category 
                              else 
                               substr (category, 1,position(',' in category)-1)
                              end
                             from article 
                             where articlecode=a.articlecode
                          
              )
 " + articleTypeQuery + @"
               " + subcatQuery + @"
            and a.isactive=true 

           
            and ( lower(title) like lower(:searchQuery) 
            
            or lower(coalesce(c.firstname||' ','')||trim(both '' from coalesce((case when c.middlename='' then null else c.middlename end)||' ',''))||coalesce(c.lastname,'')) like lower(:searchQuery)
            or lower(coalesce(p.firstname||' ','')||trim(both '' from coalesce((case when p.middlename='' then null else p.middlename end)||' ',''))||coalesce(p.lastname,'')) like lower(:searchQuery))
            ";


        //searchData = DataAccessHelper.GetInstance().GetDataTable(sqlSearch);
        NpgsqlCommand command = new NpgsqlCommand(sqlSearch);
        command.Parameters.Add("offset", offset);
        command.Parameters.Add("limit", limit);
      
        command.Parameters.Add("searchQuery", "%" + searchQuery.Replace("'", "\'") + "%");

        NpgsqlCommand commandCategoryCount = new NpgsqlCommand(sqlCategoryCount);
       
        commandCategoryCount.Parameters.Add("searchQuery", "%" + searchQuery.Replace("'", "\'") + "%");

        DataTable resultTable = DataAccessHelper.GetInstance().GetDataTable(commandCategoryCount);

        CalculateCategoryCount(resultTable);
        CalculateSubcategory(resultTable);
        NEWS_TOTAL_DATA_COUNT = resultTable.Rows.Count;

        grdSearchResult.DataSource = DataAccessHelper.GetInstance().GetDataTable(command);
        grdSearchResult.DataBind();

        UpdateTotalDatatCountInfo(NEWS_TOTAL_DATA_COUNT);
        UpdatePager();
        UpdateCategoryFilter();

    }


    private void PopulateSheetMusicSubcategoryConatiner()
    {
        foreach (Category category in sheetmusicCategoryList)
        {
            if (category.CategoryCount > 0)
            {

                string categoryName = category.Categorynamenl ?? category.Categorynameen;

                sheetMusicSubcategoryConatiner.Controls.Add(
                    new LiteralControl(
                        " <li><a href='javascript:void(0)' rel='" + category.Categoryid + "'class='sub-category-link'>» " + categoryName + " <span class='category-count'>(" + category.CategoryCount.ToString() + ")</span> </a></li>"));
            }
        }
    }


    private void PopulateBookcSubcategoryConatiner()
    {
        foreach (Category category in bookCategoryList)
        {
            if (category.CategoryCount > 0)
            {

                string categoryName = category.Categorynamenl ?? category.Categorynameen;

                bookSubcategoryConatiner.Controls.Add(
                    new LiteralControl(
                        " <li><a href='javascript:void(0)' rel='" + category.Categoryid + "'class='sub-category-link'>» " + categoryName + " <span class='category-count'>(" + category.CategoryCount.ToString() + ")</span> </a></li>"));
            }
        }
    }
    private void PopulateCDdvdSubcategoryConatiner()
    {
        foreach (Category category in cddvdCategoryList)
        {
            if (category.CategoryCount > 0)
            {

                string categoryName = category.Categorynamenl ?? category.Categorynameen;

                cddvdSubcategoryConatiner.Controls.Add(
                    new LiteralControl(
                        " <li><a href='javascript:void(0)' rel='" + category.Categoryid + "'class='sub-category-link'>» " + categoryName + " <span class='category-count'>(" + category.CategoryCount.ToString() + ")</span> </a></li>"));
            }
        }
    }



    private void UpdateCategoryFilter()
    {


        if (TotalNumberOfSheetmusic > 0)
        {
            sheetmusicCountAnchor.Visible = true;
            this.sheetmusicCountAnchor.InnerHtml = "» Sheetmusic  <span class='category-count'>(" + TotalNumberOfSheetmusic + ")</span>";



            PopulateSheetMusicSubcategoryConatiner();

        }
        else
        {
            sheetmusicCountAnchor.Visible = false;
        }


        if (TotalNumberOfBooks > 0)
        {
            booksCountAnchor.Visible = true;
            this.booksCountAnchor.InnerHtml = "» Books  <span class='category-count'> (" + TotalNumberOfBooks + ")</span>";


            PopulateBookcSubcategoryConatiner();
        }
        else
        {
            booksCountAnchor.Visible = false;
        }


        if (TotalNumberOfCDDVD > 0)
        {
            cdDVDCountAnchor.Visible = true;
            this.cdDVDCountAnchor.InnerHtml = "» CD/DVD  <span class='category-count'> (" + TotalNumberOfCDDVD + ")";
            PopulateCDdvdSubcategoryConatiner();
        }
        else
        {
            cdDVDCountAnchor.Visible = false;
        }


    }

    protected void UpdatePager()
    {
        if (NEWS_TOTAL_DATA_COUNT <= grdSearchResult.PageSize)
        {

            return;
        }

        double d = (double.Parse(NEWS_TOTAL_DATA_COUNT.ToString()) / grdSearchResult.PageSize);
        int totalPages = (int)Math.Ceiling(d);

        searchResultGridHeader.Controls.Clear();
        searchResultGridHeader.Controls.Add(new LiteralControl("<span color ='white'>Pages&nbsp;&nbsp;&laquo;</span>"));
        if ((CurrentPage) == 1)
        {

            searchResultGridHeader.Controls.Add(
        new LiteralControl("<a rel='" + (CurrentPage - 1).ToString() +
                           "'  class='pager_link_active' href='javascript:void(0)'>previous</a>"));

        }
        else if (CurrentPage > 1)
        {

            searchResultGridHeader.Controls.Add(
        new LiteralControl("<a rel='" + (CurrentPage - 1).ToString() +
                           "'  class='pager_link' href='javascript:void(0)'>previous</a>"));

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

        //searchResultGridHeader.Controls.Add(new LiteralControl("<a rel='" + totalPages.ToString() + "'  class='pager_link' href='javascript:void(0)'>Last</a>"));

        searchResultGridHeader.Controls.Add(new LiteralControl("<span color ='white'> &raquo;</span>"));
    }



    protected void UpdateTotalDatatCountInfo(int totalDataCount)
    {
        lblArticleCount.Text = totalDataCount.ToString() + "   articles";
    }



    private void ShowSearchResult(String searchType, String searchQuery, String sort, String order, int offset, int limit)
    {


        string sqlevent1 = string.Empty;
        string sqlevent2 = string.Empty;
        string searchCondition = string.Empty;
         
          string articleTypeQuery = GetArticleTypeQuery();
        string subcatQuery = GetSubCatQuery();

//        if (SearchType != "" && SearchType.Equals("k"))
//        {
//            searchCondition = @" and lower(keywords) like lower( :searchQuery ) ";
//        }
//        else
//        {
//            searchCondition = @" and ( lower(a.title) like lower( :searchQuery ) or  lower(a.subtitle) like lower( :searchQuery ) 
//            or lower((COALESCE(c.firstname,'')||' '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,''))) like lower(:searchQuery)
//            or lower((COALESCE(p.firstname,'')||' '||COALESCE(p.middlename,'')||' '||COALESCE(p.lastname,''))) like lower(:searchQuery)) ";
//        }


        if (SearchType != "" && SearchType.Equals("k"))
        {
            searchCondition = @" and lower(keywords) like lower( :searchQuery ) ";
        }
        else
        {
            searchCondition = @" and ( hitsoundsas (a.title, :searchQuery ) >0 or  hitsoundsas(a.subtitle ,  :searchQuery )>0 
            or hitsoundsas( (COALESCE(c.firstname,'')||' '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'')) , :searchQuery) >0
            or hitsoundsas((COALESCE(p.firstname,'')||' '||COALESCE(p.middlename,'')||' '||COALESCE(p.lastname,'')) , :searchQuery )> 0) ";
        }



        if (Request.Params["event"] != null)
        {
            sqlevent1 = @" inner join events e on a.events = e.eventid ";
            sqlevent2 = string.Format(" and a.events = {0} ", Request.Params["event"].ToLower());
        }

        string sqlSearch = @"select a.articlecode,a.deliverytime,a.isbn10 as isbn,a.articletype,a.pdffile,a.title,a.subtitle, a.instrumentation, a.containsmusic,COALESCE(c.firstname,'')||' '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') ||'&nbsp '
			||(case when (c.dob is not null and c.dob<>'') then '('||c.dob||(case when (c.dod is not null and c.dod <>'') then '-'||c.dod else '*' end)||')' else '' end) as author,round(a.price+round(a.price*cat.vatpc/100,2),2) as Price,
            case when a.imagefile !='' then a.imagefile when  a.articletype='b' then 'book.png' when a.articletype='s' then  'musicsheet.png' when a.articletype='c' then  'CD.png' end   as imagefile,
            a.description" + GetCultureStr() + @" as description,(COALESCE(p.firstname,'')||' '||COALESCE(p.middlename,'')||' '||COALESCE(p.lastname,'')) as publisher
           from category cat,article a left join composer c on a.composer=c.composerid left join publisher p  on a.publisher=p.publisherid 
            " + sqlevent1 + @" 
			where 
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
 " + articleTypeQuery + @"
" + subcatQuery + @"
            and a.isactive=true
            and articlecode not like('z001')"
           
            + sqlevent2 + searchCondition + @" order by " + sort + " " + order + " offset :offset limit :limit;";




        NpgsqlCommand command = new NpgsqlCommand(sqlSearch);
        command.Parameters.Add("searchQuery", "%" + searchQuery.Replace("'", "\'") + "%");
        command.Parameters.Add("offset", offset);
        command.Parameters.Add("limit", limit);

        searchData = DataAccessHelper.GetInstance().GetDataTable(command);
        grdSearchResult.DataSource = searchData;
        grdSearchResult.DataBind();




        //for category

        string sqlCategoryCount = @"
          select a.articletype , case  when (position(',' in category)-1)<0 then 
                               category 
                              else 
                               substr (category, 1,position(',' in category)-1)
                              end	as category
			from category cat,article a left join composer c on a.composer=c.composerid left join publisher p  on a.publisher=p.publisherid 
            " +
            sqlevent1 +
            @" 
			where 
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
 " + articleTypeQuery + @"
" + subcatQuery + @"
            and a.isactive=true
            and articlecode not like('z001')"
          
            + sqlevent2 + searchCondition;

        NpgsqlCommand commandCategoryCount = new NpgsqlCommand(sqlCategoryCount);
       
        commandCategoryCount.Parameters.Add("searchQuery", "%" + searchQuery.Replace("'", "\'") + "%");
        DataTable resultTable = DataAccessHelper.GetInstance().GetDataTable(commandCategoryCount);

        CalculateCategoryCount(resultTable);
        CalculateSubcategory(resultTable);

        NEWS_TOTAL_DATA_COUNT = resultTable.Rows.Count;

        UpdateTotalDatatCountInfo(NEWS_TOTAL_DATA_COUNT);
        UpdatePager();
        UpdateCategoryFilter();

    }


    private void CalculateSubcategory(DataTable resultTable)
    {
        InitCategoryLists(resultTable, sheetmusicCategoryList);

        InitCategoryLists(resultTable, bookCategoryList);
        InitCategoryLists(resultTable, cddvdCategoryList);


    }

    private void InitCategoryLists(DataTable resultTable, List<Category> categoryList)
    {
        foreach (Category category in categoryList)
        {

            category.CategoryCount = resultTable.AsEnumerable().Count(n => n.Field<string>("category") == category.Categoryid);

        }



    }

    private void CalculateCategoryCount(DataTable resultTable)
    {
        TotalNumberOfSheetmusic = resultTable.AsEnumerable().Count(n => n.Field<string>("articletype") == "s");
        TotalNumberOfBooks = resultTable.AsEnumerable().Count(n => n.Field<string>("articletype") == "b");

        TotalNumberOfCDDVD = resultTable.AsEnumerable().Count(n => n.Field<string>("articletype") == "c");
    }

    protected string ShowImage(string filename)
    {

        string ImageUrl = string.Empty;
        string fileFullname = ConfigurationManager.AppSettings["resources"].ToString()
            + "images\\Thumb_" + filename;

        if (filename != null && Functions.FileExist(fileFullname) && Functions.IsImageFile(fileFullname))
        {
            ImageUrl = "<img src='resources/images/Thumb_" + filename + "'/>";
        }
        return ImageUrl;

        //string ImageUrl="<img src='resources/images/Thumb_" + filename+"'/>";
        // return ImageUrl;
    }
   protected string  GetArticleImagePath(string imageName)
   {

       return string.Format(ApplicationImagePath, imageName+"&");

   }


    protected string ShowButonImage(string btnName)
    {
        string ImageSrc = @"~/graphics/" + (string)base.GetGlobalResourceObject("string", btnName);

        return ImageSrc;
    }

    protected void setVisitPageList(String pageName)// setting the visit page list
    {
        Master.SetVisitPageList(pageName);

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Session["cultureName"] != null)
        {
            string cultureName = Session["cultureName"].ToString();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
        }
        SetCulturalValue();
    }

    private void SetObjectValue()
    {


    }

    private bool CheckParameter(object sessionObj)
    {
        return sessionObj != null;
    }

    protected void btnDetail_Command(object sender, CommandEventArgs e)
    {
        string eventId = string.Empty;

        if (Request.Params["event"] != null)
        {
            eventId = "&event=" + Request.Params["event"].ToString();
        }

        string articleCode = e.CommandArgument.ToString();
        Response.Redirect("Details.aspx?" + "articlecode=" + articleCode + eventId);
    }

    protected void btnAddToCart_Command(object sender, CommandEventArgs e)
    {
        ArrayList cartTable = new ArrayList();
        if (Session["order"] != null)
        {
            cartTable = (ArrayList)Session["order"];
        }
        string articleCode = e.CommandArgument.ToString();
        if (!isOrderExists(articleCode))
        {
            Order order = new Order(articleCode, 1);
            order = new Facade().LoadOrderInfo(articleCode, order);
            order.publisherName = GetPublisherName(articleCode);
            cartTable.Add(order);
        }
        Session["order"] = cartTable;

        //Event
        if (Request.Params["event"] != null)
        {
            Response.Redirect("shoppingcart.aspx?event=" + Request.Params["event"].ToString());
        }

        Response.Redirect("shoppingcart.aspx");
    }
    private bool isOrderExists(string articleCode)
    {
        bool exists = false;
        if (Session["order"] != null)
        {
            ArrayList cartTable = (ArrayList)Session["order"];
            IEnumerator enu = cartTable.GetEnumerator();
            while (enu.MoveNext())
            {
                Order order = (Order)enu.Current;
                if (order.articlecode.ToString().Equals(articleCode))
                {
                    exists = true;
                    break;
                }
            }
        }
        return exists;
    }
    private void SetCulturalValue()
    {

        lblSort.Text = (string)base.GetGlobalResourceObject("string", "sort");
        lblCategoryHeader.Text = (string)base.GetGlobalResourceObject("string", "lblCategory");
        if (SearchType == "%%")
        {
            divHeader.Visible = true;
            lblSheader.Text = (string)base.GetGlobalResourceObject("string", "lblSheader");
        }
        else
            divHeader.Visible = false;

    }
    private string GetPublisherName(string articleCode)
    {
        string sqlQuey = "select case when a.articletype='b' then (COALESCE(p.firstname,'') || ' '|| COALESCE(p.middlename,'') ||' '|| COALESCE(p.lastname,'') ) " +
                            "else (COALESCE(c.firstname) ||' '|| COALESCE(c.middlename,'')  ||' '|| COALESCE(c.lastname) )end  as publisher " +
                            "from article a, publisher p, composer c " +
                            "where p.publisherid=a.publisher " +
                            "and a.composer = c.composerid  and  articlecode='" + articleCode + "'";
        DataTable dtArticle = DataAccessHelper.GetInstance().GetDataTable(sqlQuey);
        if (dtArticle.Rows.Count > 0)
            return dtArticle.Rows[0]["publisher"].ToString();
        else
            return "";
    }



    private void ShowPublish(string sort, string order, int offset, int limit)
    {
        string cultureStr = GetCultureStr();
        string articleTypeQuery = GetArticleTypeQuery();
        string subcatQuery = GetSubCatQuery();
        string query = @"select a.articlecode,a.deliverytime,a.isbn10 as isbn,a.title,a.subtitle,a.articletype,a.pdffile, a.instrumentation, a.containsmusic,COALESCE(c.firstname,'')||' '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') as author,round(a.price+round(a.price*cat.vatpc/100,2),2) as Price,
             case when a.imagefile !='' then a.imagefile when  a.articletype='b' then 'book.png' when a.articletype='s' then  'musicsheet.png' when a.articletype='c' then  'CD.png' end  as imagefile,
             a.description" + cultureStr + @" as description,(COALESCE(p.firstname,'')||' '||COALESCE(p.middlename,'')||' '||COALESCE(p.lastname,'')) as publisher
            from category cat, article a left join composer c on a.composer=c.composerid  left join publisher p on a.publisher=p.publisherid  where a.publisher=:publisher and
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
 " + articleTypeQuery + @"
 " + subcatQuery + @"
           and ( lower(title) like lower(:searchQuery) 
            or lower(c.firstname||c.lastname||c.lastname) like lower(:searchQuery) 
            or lower(coalesce(p.firstname||' ','')||coalesce(p.lastname ||' ','')|| coalesce(p.lastname,''))
            like lower(:searchQuery))order by " + sort + " " + order + " offset :offset limit :limit;";





        string sqlCategoryCount = @"
          select a.articletype , case  when (position(',' in category)-1)<0 then 
                               category 
                              else 
                               substr (category, 1,position(',' in category)-1)
                              end	as category	 
			from category cat, article a left join composer c on a.composer=c.composerid  left join publisher p 
                on a.publisher=p.publisherid  where a.publisher=:publisher and
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
 " + articleTypeQuery + @"
 " + subcatQuery + @"
           
            and ( lower(title) like lower(:searchQuery)
            or lower(c.firstname||c.lastname||c.lastname) like lower(:searchQuery) 
            or lower(coalesce(p.firstname||' ','')||coalesce(p.lastname ||' ','')|| coalesce(p.lastname,'')) like lower(:searchQuery))  ";


        NpgsqlCommand command = new NpgsqlCommand(query);
        command.Parameters.Add("publisher", ConfigurationManager.AppSettings["boeijenga-id"].ToString());
        command.Parameters.Add("searchQuery", "%" + SearchQuery.Replace("'", "\'") + "%");
        command.Parameters.Add("offset", offset);
        command.Parameters.Add("limit", limit);






        NpgsqlCommand commandCategoryCount = new NpgsqlCommand(sqlCategoryCount);
        
        commandCategoryCount.Parameters.Add("searchQuery", "%" + SearchQuery.Replace("'", "\'") + "%");
        commandCategoryCount.Parameters.Add("publisher", ConfigurationManager.AppSettings["boeijenga-id"].ToString());

        searchData = DataAccessHelper.GetInstance().GetDataTable(command);
        // lblResultCount.Text = searchData.Rows.Count.ToString();
        grdSearchResult.DataSource = searchData;
        grdSearchResult.DataBind();

        DataTable resultTable = DataAccessHelper.GetInstance().GetDataTable(commandCategoryCount);
        CalculateCategoryCount(resultTable);
        CalculateSubcategory(resultTable);

        NEWS_TOTAL_DATA_COUNT = resultTable.Rows.Count;

        UpdateTotalDatatCountInfo(NEWS_TOTAL_DATA_COUNT);
        UpdatePager();
        UpdateCategoryFilter();
    }

    private string GetCultureStr()
    {
        return Functions.GetCultureStr(Session["cultureName"].ToString());
    }


    protected string GetAuthorComposer()
    {
        ++count;

        if (searchData.Rows.Count >= 1)
        {
            if (count >= int.Parse(searchData.Rows.Count.ToString()))
            {
                count = int.Parse(searchData.Rows.Count.ToString()) - 1;
            }
            string str = searchData.Rows[count]["articletype"].ToString();
            if (str.Equals("b"))
            {
                return "author";
            }
            else if (str.Equals("c"))
            {
                return "lblPerformer";
            }
            else return "lblComposer";
        }
        return "author";
    }



    public string GetType(string type)
    {

        switch (type.ToLower())
        {
            case "s":
                type = "Sheetmusic";
                break;
            case "b":
                type = "Books";
                break;
            case "c":
                type = "CD/DVD";
                break;
            case "c,d":
                type = "CD/DVD";
                break;
            case "d":
                type = "CD/DVD";
                break;

        }

        return type;
    }


    public bool IsPdfExists(string filename)
    {
        string ImageUrl = string.Empty;
        string fileFullname = ConfigurationManager.AppSettings["resources"].ToString()
            + "pdf\\" + filename;

        if (filename != null && Functions.FileExist(fileFullname) && Functions.IsPdfFile(fileFullname))
        {
            return true;
        }
        else
            return false;

    }

    public string getViewPdfPath(string filename)
    {

        if (filename != "")
        {
            string url = Request.Url.AbsoluteUri;
            url = url.Remove(url.LastIndexOf('/')) + "/resources/pdf/" + filename;

            string returnUrl = "http://docs.google.com/gview?url=" + url + "&embedded=true style=width:600px; height:500px; frameborder=0','PDF Viewer','menubar=1,resizable=1,width=750,height=750";
            //javascript:window.open('http://docs.google.com/gview?url=http://localhost:2650/webroot/resources/pdf/23490.pdf&embedded=true style=width:600px; height:500px; frameborder=0','PDF Viewer','menubar=1,resizable=1,width=750,height=750');" />
            return returnUrl;
        }
        else
            return "";

    }

    public bool IsMusicFileExists(bool containMusic, string articlecode)
    {

        string fileFullname = ConfigurationManager.AppSettings["resources"].ToString()
            + "audio\\" + articlecode + ".mp3";

        if (articlecode != null && Functions.FileExist(fileFullname) && containMusic)
        {
            return true;
        }
        else
            return false;


    }
    public string GetPDFpath()
    {
        UriBuilder uri = new UriBuilder();
        string s = uri.Path;
        return s;
    }



    #region Top Ten

    public void BindTopTenGridForWebShop(String searchType, String searchQuery, String sort, String order, int offset, int limit)
    {

        string sqlSearch = @"
          select a.articlecode, a.title
			from category cat,article a left join composer c on a.composer=c.composerid left join publisher p  on a.publisher=p.publisherid right join defaultwebshop d on d.article=a.articlecode
            where cat.categoryid =( 
                            select case 
                              when (position(',' in category)-1)<0 then 
                               category 
                              else 
                               substr (category, 1,position(',' in category)-1)
                              end
                             from article 
                             where articlecode=a.articlecode
                            )
            and a.isactive=true 
            and a.articletype like :searchType
            and ( lower(title) like lower(:searchQuery) 
            or lower(coalesce(c.firstname||' ','')||trim(both '' from coalesce((case when c.middlename='' then null else c.middlename end)||' ',''))||coalesce(c.lastname,'')) like lower(:searchQuery)
            or lower(coalesce(p.firstname||' ','')||trim(both '' from coalesce((case when p.middlename='' then null else p.middlename end)||' ',''))||coalesce(p.lastname,'')) like lower(:searchQuery))
            order by " + sort + " " + order + " offset :offset limit :limit ";

        //searchData = DataAccessHelper.GetInstance().GetDataTable(sqlSearch);
        NpgsqlCommand command = new NpgsqlCommand(sqlSearch);
        command.Parameters.Add("offset", offset);
        command.Parameters.Add("limit", limit);
        command.Parameters.Add("searchType", searchType);
        command.Parameters.Add("searchQuery", "%" + searchQuery.Replace("'", "\'") + "%");

        this.gridTopTen.DataSource = DataAccessHelper.GetInstance().GetDataTable(command);
        this.gridTopTen.DataBind();



    }

    private void UpdateTopTenLabel()
    {

        string searchType = SearchType;
        string toptenString = "toptenProducts";
        switch (searchType.ToLower())
        {
            case "s":
                toptenString = "toptensheetmusic";
                BindTopTenSheetMusic();
                break;
            case "c":
                toptenString = "toptenCDDVD";
                BindTopTenCDDVD();
                break;
            case "b":
                toptenString = "toptenBook";
                BindTopTenBooks();
                break;
            default:

                if (Request.Params["articlecode"] != null)
                {
                    string articleCode = Request.Params["articlecode"].ToString();

                    if (IsSheetMusic(articleCode))
                    {

                        toptenString = "toptensheetmusic";
                        BindTopTenSheetMusic();
                    }
                    else if (IsBook(articleCode))
                    {

                        toptenString = "toptenBook";
                        BindTopTenBooks();
                    }
                    else if (IsCdDVD(articleCode))
                    {

                        toptenString = "toptenCDDVD";
                        BindTopTenCDDVD();
                    }



                }
                else
                {
                    toptenString = "toptenProducts";
                    BindTopTenProducts();
                }
                break;

        }




        lblTopTenProducts.Text = (string)base.GetGlobalResourceObject("string", toptenString);
    }

    private bool IsSheetMusic(string articleCode)
    {
        return articleCode.ToLower().StartsWith("s");
    }

    private bool IsBook(string articleCode)
    {
        return articleCode.ToLower().StartsWith("b");
    }

    private bool IsCdDVD(string articleCode)
    {
        if (articleCode.ToLower().StartsWith("c") || articleCode.ToLower().StartsWith("d"))
        {

            return true;
        }
        return false;
    }


    private void BindTopTenBooks()
    {

        string query = @"select count(o.articlecode) as totalcount ,o.articlecode,a.title
                        FROM ordersline o
                        left join article a on (a.articlecode = o.articlecode)
                        where a.articletype='b'
                        group by o.articlecode,a.title
                        order by totalcount desc
                        offset 0 limit 10";



        NpgsqlCommand command = new NpgsqlCommand(query);
        searchData = DataAccessHelper.GetInstance().GetDataTable(command);
        gridTopTen.DataSource = searchData;
        gridTopTen.DataBind();

    }

    private void BindTopTenSheetMusic()
    {

        string query = @"select count(o.articlecode) as totalcount ,o.articlecode,a.title
                        FROM ordersline o
                        left join article a on (a.articlecode = o.articlecode)
                        where a.articletype='s'
                        group by o.articlecode,a.title
                        order by totalcount desc
                        offset 0 limit 10";



        NpgsqlCommand command = new NpgsqlCommand(query);
        searchData = DataAccessHelper.GetInstance().GetDataTable(command);
        gridTopTen.DataSource = searchData;
        gridTopTen.DataBind();

    }


    private void BindTopTenProducts()
    {

        string query = @"select count(o.articlecode) as totalcount ,o.articlecode,a.title
                        FROM ordersline o
                        left join article a on (a.articlecode = o.articlecode)
                        
                        group by o.articlecode,a.title
                        order by totalcount desc
                        offset 0 limit 10";



        NpgsqlCommand command = new NpgsqlCommand(query);
        searchData = DataAccessHelper.GetInstance().GetDataTable(command);
        gridTopTen.DataSource = searchData;
        gridTopTen.DataBind();

    }

    private void BindTopTenCDDVD()
    {

        string query = @"select count(o.articlecode) as totalcount ,o.articlecode,a.title
                        FROM ordersline o
                        left join article a on (a.articlecode = o.articlecode)
                        where a.articletype in ('d' ,'c')
                        group by o.articlecode,a.title
                        order by totalcount desc
                        offset 0 limit 10";



        NpgsqlCommand command = new NpgsqlCommand(query);
        searchData = DataAccessHelper.GetInstance().GetDataTable(command);
        gridTopTen.DataSource = searchData;
        gridTopTen.DataBind();

    }






    #endregion





}
