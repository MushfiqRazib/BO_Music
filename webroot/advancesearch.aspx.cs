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

public partial class advancesearch : BasePage
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
		GetVisitedDepth();

		if (!IsPostBack)
		{
			SetCulture();
			LoadValues();
            LoadDivsAccordingCategory();
		}
		SetCulturalValue();
		
		txtSearch.Attributes.Add("onkeypress", "return clickButton(event,'" + imbGo.ClientID + "')");
		txtISMNSearchMusic.Attributes.Add("onkeypress", "return clickButton(event,'" + imbGo.ClientID + "')");
		txtISBNSearchBook.Attributes.Add("onkeypress", "return clickButton(event,'" + imbGo.ClientID + "')");
		txtISBNAll.Attributes.Add("onkeypress", "return clickButton(event,'" + imbGo.ClientID + "')");
		txtISMNAll.Attributes.Add("onkeypress", "return clickButton(event,'" + imbGo.ClientID + "')");
		
    }
	//private void ValidateInput()
	//{
	//    Regex reg = new Regex("^[a-z A-z 0-9]");
	//    if(!reg.IsMatch(txtSearch.Text.Trim()))
	//            txtSearch.Text=txtSearch.Text.Substring(1,txtSearch.Text.Length);
	//    if(!reg.IsMatch(txtISMNAll.Text.Trim()))
	//        txtISMNAll.Text = txtISMNAll.Text.Substring(1, txtISMNAll.Text.Length);
	//    if(!reg.IsMatch(txtISBNAll.Text.Trim()))
	//        txtISBNAll.Text = txtISBNAll.Text.Substring(1, txtISBNAll.Text.Length);
	//    if(!reg.IsMatch(txtISBNSearchBook.Text.Trim()))
	//        txtISBNSearchBook.Text = txtISBNSearchBook.Text.Substring(1, txtISBNSearchBook.Text.Length);
	//    if(!reg.IsMatch(txtISMNSearchMusic.Text.Trim()))
	//        txtISMNSearchMusic.Text = txtISMNSearchMusic.Text.Substring(1, txtISMNSearchMusic.Text.Length);
	//}
	public void LoadPeriodChoice()
	{
		string sql = "select distinct periodid, periodsen,periodsnl  from period p,article a where p.periodid=a.period order by p.periodid";
		DataTable dt = dbHandler.GetDataTable(sql);
		string catPrefix = Session["cultureName"].ToString().Substring(0, 2);
		cblPeriod.Items.Clear();
        if (dt.Rows.Count == 0)
        {
            trPeriod.Style.Add("display","none");
        }
        else
		foreach(DataRow dr in dt.Rows)
		{
			ListItem list = new ListItem();
			string catName = "periods" + catPrefix;
			list.Text = dr[catName.Trim()].ToString();
			list.Value = dr["periodid"].ToString();
			cblPeriod.Items.Add(list);

		}

	}
	public void LoadGradeChoice()
	{
		string sql = "select distinct g.gradenameen , g.gradenamenl ,g.gradeid  from grade g,article a where g.gradeid=a.grade order by g.gradeid";
		DataTable dt = dbHandler.GetDataTable(sql);
		string catPrefix = Session["cultureName"].ToString().Substring(0, 2);
		cblGradeMusic.Items.Clear();

        if (dt.Rows.Count == 0)
        {
            trGrade.Style.Add("display","none");
        }
        else
		foreach (DataRow dr in dt.Rows)
		{
			ListItem list = new ListItem();
			string catName = "gradename" + catPrefix;
			list.Text = dr[catName.Trim()].ToString();
			list.Value = dr["gradeid"].ToString();
			cblGradeMusic.Items.Add(list);

		}
	}
	public void LoadCountryChoice()
	{
		//string sql = "select countrycode,countryname  from  country where  priority!=0 order by  priority ";
		string sql = "select distinct countrycode,countryname  from  country c,article a where c.countrycode=a.country order by  c.countrycode";
		DataTable dt = dbHandler.GetDataTable(sql);
		cblCountryMusic.Items.Clear();
        
		foreach (DataRow dr in dt.Rows)
		{
			ListItem list = new ListItem();
			list.Text = dr["countryname"].ToString();
			list.Value = dr["countrycode"].ToString();
			cblCountryMusic.Items.Add(list);

		}
	}
	public void LoadEventChoice()
	{
		string sql = "select distinct e.eventid, e.eventnameen ,e.eventnamenl from events e,article a where e.eventid=a.events";
		DataTable dt = dbHandler.GetDataTable(sql);
		string catPrefix = Session["cultureName"].ToString().Substring(0, 2);
		cblEventMusic.Items.Clear();

        if (dt.Rows.Count == 0)
        {
            trEvent.Style.Add("display","none");
        }
        else
		foreach (DataRow dr in dt.Rows)
		{
			ListItem list = new ListItem();
			string catName = "eventname" + catPrefix;
			list.Text = dr[catName.Trim()].ToString();
			list.Value = dr["eventid"].ToString();
			cblEventMusic.Items.Add(list);

		}
	}
	/*
	 * Load category depending on Culture
	 * Author:Shahriar
	 * Date:21-7-07
	 *	
	 */
	private void LoadCategoryChoice(string culture)
	{
		string sql = @"select distinct c.categorynameen, c.categorynamenl ,c.vatpc ,c.categoryid from category c,article a where 
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
        order by c.categoryid";
		DataTable dt=dbHandler.GetDataTable(sql);
		string catPrefix = Session["cultureName"].ToString().Substring(0, 2);
		cblCategoryBook.Items.Clear();
		cblCategoryCD.Items.Clear();
		cblCategoryMusic.Items.Clear();

        if (dt.Rows.Count == 0)
        {
           trCategory.Style.Add("display", "none");
           trbookCategory.Style.Add("display", "none");
           trcddvdCategory.Style.Add("display", "none");
        }
        else
		foreach(DataRow dr in dt.Rows)
		{
			string toCompare = dr["categoryid"].ToString(); //Compare to find out category
			if (toCompare.Substring(0, 1).ToLower().Equals("b"))
			{
				ListItem bookList = new ListItem();
				string catName = "categoryname" + catPrefix;
				bookList.Text = dr[catName.Trim()].ToString();
				bookList.Value = dr["categoryid"].ToString();
				cblCategoryBook.Items.Add(bookList);
			}
			else if (toCompare.Substring(0, 1).ToLower().Equals("c"))
			{
				ListItem cdDVDList = new ListItem();
				string catName = "categoryname" + catPrefix;
				cdDVDList.Text = dr[catName.Trim()].ToString();
				cdDVDList.Value = dr["categoryid"].ToString();
				cblCategoryCD.Items.Add(cdDVDList);
			}
			else if (toCompare.Substring(0, 1).ToLower().Equals("s"))
			{
				ListItem sheetMusicList = new ListItem();
				string catName = "categoryname" + catPrefix;
				sheetMusicList.Text = dr[catName.Trim()].ToString();
				sheetMusicList.Value = dr["categoryid"].ToString();
				cblCategoryMusic.Items.Add(sheetMusicList);
			}
		}
		//for (int i = 0; i < dt.Rows.Count; i++)
		//{
		//    string toCompare = dt.Rows[i]["categoryid"].ToString(); //Compare to find out category
		//    if (toCompare.Substring(0, 1).ToLower().Equals("b"))
		//    {
		//        ListItem bookList = new ListItem();
		//        string catName = "categoryname" + catPrefix;
		//        bookList.Text = dt.Rows[i][catName.Trim()].ToString();
		//        bookList.Value = dt.Rows[i]["categoryid"].ToString();
		//        cblCategoryBook.Items.Add(bookList);
		//    }
		//    else if (toCompare.Substring(0, 1).ToLower().Equals("c"))
		//    {
		//        ListItem cdDVDList = new ListItem();
		//        string catName = "categoryname" + catPrefix;
		//        cdDVDList.Text = dt.Rows[i][catName.Trim()].ToString();
		//        cdDVDList.Value = dt.Rows[i]["categoryid"].ToString();
		//        cblCategoryCD.Items.Add(cdDVDList);
		//    }
		//    else if (toCompare.Substring(0, 1).ToLower().Equals("s"))
		//    {
		//        ListItem sheetMusicList = new ListItem();
		//        string catName = "categoryname" + catPrefix;
		//        sheetMusicList.Text = dt.Rows[i][catName.Trim()].ToString();
		//        sheetMusicList.Value = dt.Rows[i]["categoryid"].ToString();
		//        cblCategoryMusic.Items.Add(sheetMusicList);
		//    }
		//}
		
		
	}
	private void LoadProductChoice()
	{
		//int num=2;
		string[] textArray={"CD","DVD"};
		string[] valueArray={"c","d"};
		cblTypeCD.Items.Clear();
		for (int i = 0; i < textArray.Length; i++)
		{
			ListItem list = new ListItem();
			list.Text = textArray[i].ToString();
			list.Value = valueArray[i].ToString();
			cblTypeCD.Items.Add(list);
		}
	}
	private void LoadlanguageChoice()
	{
		string sql = "select distinct languagename,languagecode from language l,article a where a.language=l.languagecode";
		DataTable dt = dbHandler.GetDataTable(sql);
		cblLanguageAll.Items.Clear();
		cblLanguageBook.Items.Clear();
		foreach (DataRow dr in dt.Rows)
		{
			ListItem bookList = new ListItem();
			bookList.Text = dr["languagename"].ToString();
			bookList.Value =dr["languagecode"].ToString();
			cblLanguageBook.Items.Add(bookList);
			cblLanguageAll.Items.Add(bookList);
			
		}


        ListItem lit = new ListItem();
        lit.Text = "Other";
        lit.Value = "1000";
        cblLanguageBook.Items.Add(lit);
        cblLanguageAll.Items.Add(lit);

        if (!ddlCategory.SelectedValue.Equals("b"))
        {
            ListItem lit1 = new ListItem();
            lit1.Text = "All";
            lit1.Value = "0";
            cblLanguageAll.Items.Add(lit1);
            cblLanguageBook.Items.Add(lit1);
        }
	}
	private void LoadChoice()
	{
		LoadCategoryChoice(cultureName);
		LoadlanguageChoice();
		LoadEventChoice();
		LoadCountryChoice();
		LoadGradeChoice();
		LoadPeriodChoice();
		LoadProductChoice();
		
	}
	private void LoadValues()
	{
        Session["shop"] = null;
		ddlCategory.DataBind();
		if(Session["articletype"]!=null)
		{
			ddlCategory.SelectedValue=Session["articletype"].ToString();
		}
		else
		{
			ddlCategory.SelectedItem.Text="All";
		}
		
		LoadChoice();
	}
	void Master_MenuButton(object sender, EventArgs e)
	{
		Session["cultureName"] = Master.CurrentButton.ToString();
		SetCulture();
		SetCulturalValue();
		LoadChoice();
		
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
		header.Style.Add("background-image", "url(graphics/" + (string)base.GetGlobalResourceObject("string", "headerAdvanceSearch") + ")");
		lblCurrentPage.Text = (string)base.GetGlobalResourceObject("string", "currentpage");
        lblCategory.Text = (string)base.GetGlobalResourceObject("string", "lblCategory");
        lblSearchText.Text = (string)base.GetGlobalResourceObject("string", "searchText");
        lblLanguageAll.Text= lblLanguageBook.Text = (string)base.GetGlobalResourceObject("string", "lblLanguageBook");
        //lblLanguageAll.Text = (string)base.GetGlobalResourceObject("string", "lblLanguageBook");
        lblCategoryBook.Text= lblCategoryCD.Text= lblCategoryListMusic.Text = (string)base.GetGlobalResourceObject("string", "lblCategoryListMusic");

        lblCountryListMusic.Text = (string)base.GetGlobalResourceObject("string", "lblCountry");
        lblGradeList.Text = (string)base.GetGlobalResourceObject("string", "lblGradeList");
        lblISMNAll.Text = lblISMNMusic.Text = (string)base.GetGlobalResourceObject("string", "lblISMNMusic");
        lblISBNAll.Text= lblISBNSearchBook.Text = (string)base.GetGlobalResourceObject("string", "lblISBNSearchBook");
        lblSearch.Text = (string)base.GetGlobalResourceObject("string", "lblSearch");
        lblType.Text = (string)base.GetGlobalResourceObject("string", "lblType");
	}
	public ArrayList GetSelectedValuesFromCheckListBox(CheckBoxList chkObj)
	{

		ArrayList itemList = new ArrayList();
		foreach (ListItem item in chkObj.Items)
		{
			if (item.Selected)
				itemList.Add(item.Value);
		}
		return itemList;

	}
	public string GetInClauseString(ArrayList itemList)
	{
		string clauseString = "";

		if (itemList.Count > 0)
		{
			for (int i = 0; i < itemList.Count; i++)
			{
				clauseString += "'" + itemList[i].ToString() + "'";
				if (i != itemList.Count - 1)
				{
					clauseString += ",";
				}

			}
			clauseString = " ( " + clauseString + ")";

		}
		return clauseString;
	}
	private void ClearFields()
	{
		
		txtISMNAll.Text=" ";
		txtISBNAll.Text=" ";
		txtISBNSearchBook.Text=" ";
		txtISMNSearchMusic.Text=" ";
	}
	protected void ddlCategory_SelectedTextChanged(object sender, EventArgs e)
	{
		//Session["articletype"] = ddlCategory.SelectedValue;
        LoadDivsAccordingCategory();
		
	    
	}

    private void LoadDivsAccordingCategory()
    {
        ClearFields();
        if (ddlCategory.SelectedValue.Equals("b"))
        {
            lblSearchText.Text = "Advanced search / Books";
            divBooks.Style.Clear();
            LoadCategoryChoice(cultureName);
            LoadlanguageChoice();

            divSheetMusic.Style.Add("display", "none");
            divCDDVD.Style.Add("display", "none");
            divAll.Style.Add("display", "none");

        }
        else if (ddlCategory.SelectedValue.Equals("c"))
        {
            lblSearchText.Text = "Advanced search / CD/DVD";
            divCDDVD.Style.Clear();
            LoadCategoryChoice(cultureName);
            LoadProductChoice();

            divBooks.Style.Add("display", "none");
            divSheetMusic.Style.Add("display", "none");
            divAll.Style.Add("display", "none");

        }
        else if (ddlCategory.SelectedValue.Equals("s"))
        {
            lblSearchText.Text = "Advanced search / SheetMusic";
            divSheetMusic.Style.Remove("display");
            LoadCategoryChoice(cultureName);
            LoadEventChoice();
            LoadPeriodChoice();
            LoadCountryChoice();
            LoadGradeChoice();


            divBooks.Style.Add("display", "none");
            divCDDVD.Style.Add("display", "none");
            divAll.Style.Add("display", "none");
        }
        else
        {
            lblSearchText.Text = "Advanced search / All";
            divAll.Style.Remove("display");

            LoadlanguageChoice();

            divBooks.Style.Add("display", "none");
            divSheetMusic.Style.Add("display", "none");
            divCDDVD.Style.Add("display", "none");
        }
    }
	protected void imbGo_Click(object sender, ImageClickEventArgs e)
	{
		searchType = ddlCategory.SelectedValue.ToString();
		searchQuery = txtSearch.Text;
		NpgsqlCommand query=null;
		
		switch (searchType)
		{
			case "a":	query=SQLBuilder_For_All();
						break;
			case "b":	query=SQLBuilder_For_Book();
						break;
			case "c":	query=SQLBuilder_for_CD();
						break;
			case "s":	query=BuildSheetMusicQuery();
						break;
			default: break;
		}
		Session["advanceSearch"]=query;
		Session["articletype"]=searchType;
        //Session["articlename"] = searchQuery;
		Response.Redirect("SearchResult.aspx");

	}
	private NpgsqlCommand SQLBuilder_For_All()
	{
	
		string sqlStatement="";
		string extraParameter = "";
		string searchText=txtSearch.Text.Trim().ToLower ().Replace("'", "\'").Replace("<","");
		string searchISMN = txtISMNAll.Text.Trim().ToLower().Replace("'", "\'").Replace("<", "");
		string searchISBN = txtISBNAll.Text.Trim().ToLower().Replace("'", "\'").Replace("<", "");
		

		NpgsqlCommand command = new NpgsqlCommand();

		sqlStatement = "select a.articlecode,a.articletype," +
						"a.title,COALESCE(c.firstname,'')||'  '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') as Author," +
						"a.price,a.imagefile," +
						"(case when char_length(description" + Session["cultureName"].ToString().Substring(0, 2) + @")>220 then" +
						" substr(description" + Session["cultureName"].ToString().Substring(0, 2) + @",0,250) || '...&nbsp;<a href=""details.aspx?articlecode=' || a.articlecode || '"">more</a>'" +
						" else " +
						" description" + Session["cultureName"].ToString().Substring(0, 2) +
						" end " +
						" ) as description" +
						" from article a,composer c " +
						"where ((lower(a.descriptionen) like lower(:searchText) or lower(a.title) like lower(:searchText) or (lower((COALESCE(c.firstname||' ','') || COALESCE(c.middlename||' ','')|| COALESCE(c.lastname))) like lower(:searchText ))))";


		command.Parameters.Add("searchText","%"+searchText+ "%");	
			
		if(searchISMN.Length>0)
		{
			extraParameter ="and lower(a.ISMN)  like lower(:searchISMN )";
		}
		sqlStatement=sqlStatement+extraParameter;
		command.Parameters.Add("searchISMN", "%" + searchISMN + "%");	

		if (searchISBN.Length >0)
		{
			extraParameter = "and (lower(a.ISBN10) like lower(:searchISBN) or lower(a.ISBN13) like lower(:searchISBN) ) ";
		}
		sqlStatement = sqlStatement + extraParameter;
		command.Parameters.Add("searchISBN", "%" + searchISBN+ "%");	

		/* Check which language selected */
		if (!cblLanguageAll.SelectedValue.Equals("0"))
		{
			int num_item=cblLanguageAll.Items.Count;
			extraParameter="";
            foreach (ListItem item in cblLanguageAll.Items)
			{
				if (item.Selected)
					extraParameter = extraParameter + "'" + item.Value.ToString() + "',".Trim();
			}
			if (!extraParameter.Equals("") && extraParameter.Length > 0)
			{
				extraParameter=extraParameter.Substring(0,extraParameter.Length-1);
				sqlStatement = sqlStatement + "and lower(a.language) IN("+extraParameter+") ";
			}
		}
		
		/* Adding relation at the end of query */
		sqlStatement = sqlStatement + "and a.composer=c.composerid";

		command.CommandText=sqlStatement;
		return command;
		
	}
	private NpgsqlCommand SQLBuilder_For_Book()
	{

		string sqlStatement = "";
		string extraParameter = "";
		string searchText = txtSearch.Text.Trim().ToLower().Replace("'", "\'").Replace("<", "");
		string searchISBN = txtISBNSearchBook.Text.Trim().ToLower().Replace("'", "\'").Replace("<", "");

		NpgsqlCommand command = new NpgsqlCommand();

		sqlStatement = "select a.articlecode,a.articletype," +
						"a.title,COALESCE(c.firstname,'')||'  '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') as Author," +
						"a.price,a.imagefile," +
						"(case when char_length(description" + Session["cultureName"].ToString().Substring(0, 2) + @")>220 then" +
						" substr(description" + Session["cultureName"].ToString().Substring(0, 2) + @",0,250) || '...&nbsp;<a href=""details.aspx?articlecode=' || a.articlecode || '"">more</a>'" +
						" else " +
						" description" + Session["cultureName"].ToString().Substring(0, 2) +
						" end " +
						" ) as description" +
						" from article a,composer c " +
						"where ((lower(a.descriptionen) like lower(:searchText) or lower(a.title) like :searchText or (lower((COALESCE(c.firstname||' ','') || COALESCE(c.middlename||' ','')|| COALESCE(c.lastname,''))) like lower(:searchText))))";

		command.Parameters.Add("searchText", "%" + searchText + "%");	

		if (searchISBN.Length > 0)
		{
			extraParameter = "and (lower(a.ISBN10) like lower(:searchISBN) or lower(a.ISBN13) like lower(:searchISBN) ) ";
		}
		sqlStatement = sqlStatement + extraParameter;
		command.Parameters.Add("searchISBN", "%" + searchISBN + "%");	

		/* Check which category selected */

		int num_item = cblCategoryBook.Items.Count;
		extraParameter = "";
		foreach (ListItem item in cblCategoryBook.Items)
		{
			if (item.Selected)
				extraParameter = extraParameter + "'" + item.Value.ToString().ToLower() + "',";
		}

		if (!extraParameter.Equals("") && extraParameter.Length > 0)
		{
		
			extraParameter = extraParameter.Substring(0, extraParameter.Length - 1);
			sqlStatement = sqlStatement + "and lower(a.category) IN(" + extraParameter + ")";
		}


		/* Check which language selected */

		num_item = cblLanguageBook.Items.Count;
		extraParameter = "";
		foreach(ListItem item in cblLanguageBook.Items)
		{
			if (item.Selected)
				extraParameter = extraParameter + "'" + item.Value.ToString().ToLower() + "',";
		}
		if (!extraParameter.Equals("") && extraParameter.Length>0)
			extraParameter = extraParameter.Substring(0, extraParameter.Length - 1);


		if (!extraParameter.Equals("") && !cblLanguageBook.Items[num_item-1].Selected)  //if other not selected but remaining language selected 
			sqlStatement = sqlStatement + "and lower(a.Language) IN(" + extraParameter + ") ";
		else if (extraParameter.Equals("") && cblLanguageBook.Items[num_item-1].Selected) //if other selected bot remaining not selected
			sqlStatement = sqlStatement + "and lower(a.Language) NOT IN('1','2','3','4') ";
		else if(!extraParameter.Equals("") && cblLanguageBook.Items[num_item-1].Selected) // if other and remaining selected
		{
			extraParameter="";
			foreach (ListItem item in cblLanguageBook.Items)
			{
				if (!item.Selected)
					extraParameter = extraParameter + "'" + item.Value.ToString() + "',";
			}
			if (!extraParameter.Equals("") && extraParameter.Length > 0)
			{
				extraParameter = extraParameter.Substring(0, extraParameter.Length - 1);
				sqlStatement = sqlStatement + "and lower(a.Language) NOT IN(" + extraParameter + ")";
			}

		}



		/* Adding relation at the end of query */
		sqlStatement = sqlStatement + " and a.composer=c.composerid and lower(a.articletype)='b'";
		command.CommandText =sqlStatement;
		return command;
		
	}
	private NpgsqlCommand SQLBuilder_for_CD()
	{

		string sqlStatement = "";
		string extraParameter = "";
		string searchText = txtSearch.Text.Trim().ToLower().Replace("'", "\'").Replace("<", "");

		NpgsqlCommand command = new NpgsqlCommand();

		sqlStatement = "select a.articlecode,a.articletype," +
						"a.title,COALESCE(c.firstname,'')||'  '||COALESCE(c.middlename,'')||' '||COALESCE(c.lastname,'') as Author," +
						"a.price,a.imagefile,"+
						"(case when char_length(description" + Session["cultureName"].ToString().Substring(0, 2) + @")>220 then"+ 
						" substr(description" + Session["cultureName"].ToString().Substring(0, 2) + @",0,250) || '...&nbsp;<a href=""details.aspx?articlecode=' || a.articlecode || '"">more</a>'"+
						" else "+
						" description" + Session["cultureName"].ToString().Substring(0, 2) + 
						" end "+
						" ) as description" +
						" from article a,composer c " +
						"where ((lower(a.descriptionen) like lower(:searchText)  or lower(a.title) like lower(:searchText) or (lower((COALESCE(c.firstname||' ','') || COALESCE(c.middlename||' ','')|| COALESCE(c.lastname,''))) like lower(:searchText))))";


		command.Parameters.Add("searchText", "%" + searchText + "%");	

		extraParameter = "";
		foreach (ListItem item in cblCategoryCD.Items)
		{
			if (item.Selected)
				extraParameter = extraParameter + "'" + item.Value.ToString().ToLower() + "',";
		}

		if (!extraParameter.Equals("") && extraParameter.Length > 0)
		{
			extraParameter = extraParameter.Substring(0, extraParameter.Length - 1);
			sqlStatement = sqlStatement + " and lower(a.category) IN(" + extraParameter + ")";
		}
		

		/* Adding Product type */
		
		extraParameter = "";
		foreach (ListItem item in cblTypeCD.Items)
		{
			if (item.Selected)
				extraParameter = extraParameter + "'" + item.Value.ToString().ToLower() + "',";
		}
		if (!extraParameter.Equals("") && extraParameter.Length > 0)
		{
			extraParameter = extraParameter.Substring(0, extraParameter.Length - 1);
			sqlStatement = sqlStatement + " and lower(substring(a.articlecode,1,1))IN (" + extraParameter + ")";
		}
		
		
		/* Adding relation at the end of query */

		sqlStatement = sqlStatement + " and a.composer=c.composerid and lower(a.articletype)='c'";
		command.CommandText=sqlStatement;
		return command;
	}
	protected NpgsqlCommand BuildSheetMusicQuery()
	{

		string searchQuery = txtSearch.Text.Trim().ToLower().Replace("'", "\'").Replace("<", "");
		string ismn = txtISMNSearchMusic.Text.Trim().Replace("'", "\'").Replace("<", "");
		string extraParameter="";
		NpgsqlCommand command = new NpgsqlCommand();

		string sqlSearch = @"
          select a.articlecode,a.title,a.articletype,c.firstname||'  '||c.middlename||' '||c.lastname as author,a.price,a.imagefile,
            (case when char_length(description" + Session["cultureName"].ToString().Substring(0, 2) + @")>220 then 
	            substr(description" + Session["cultureName"].ToString().Substring(0, 2) + @",0,250) || '...&nbsp;<a href=""details.aspx?articlecode=' || a.articlecode || '"">more</a>'
            else
	            description" + Session["cultureName"].ToString().Substring(0, 2) + @"
            end
            ) as description
            from article a,composer c where  articletype like lower('s')  and a.composer=c.composerid and (a.articletype)='s' ";

			

		if (!searchQuery.Equals(""))
		{
			sqlSearch += " and ( lower(title) like lower(:searchQuery) or lower(description" + Session["cultureName"].ToString().Substring(0, 2) + @") like lower(:searchQuery) or lower(c.firstname||c.lastname||c.lastname) like lower(:searchQuery)) ";
			command.Parameters.Add("searchQuery", "%" + searchQuery + "%");
		}
		

		
		foreach (ListItem item in  cblCategoryMusic.Items)
		{
			if (item.Selected)
				extraParameter = extraParameter + "'" + item.Value.ToString().ToLower() + "',";
		}

		if (!extraParameter.Equals("") && extraParameter.Length > 0)
		{
			extraParameter = extraParameter.Substring(0, extraParameter.Length - 1);
			sqlSearch = sqlSearch + " and  lower(a.category) in(" + extraParameter + ")";
		}

		extraParameter="";
		foreach (ListItem item in cblEventMusic.Items)
		{
			if (item.Selected)
				extraParameter = extraParameter + "'" + item.Value.ToString() + "',";
		}

		if (!extraParameter.Equals("") && extraParameter.Length > 0)
		{
			extraParameter = extraParameter.Substring(0, extraParameter.Length - 1);
			sqlSearch = sqlSearch + " and  a.events in (" + extraParameter + ")";
		}
		extraParameter = "";

		foreach (ListItem item in cblPeriod.Items)
		{
			if (item.Selected)
				extraParameter = extraParameter + "'" + item.Value.ToString() + "',";
		}

		if (!extraParameter.Equals("") && extraParameter.Length > 0)
		{
			extraParameter = extraParameter.Substring(0, extraParameter.Length - 1);
			sqlSearch = sqlSearch + " and a.period in (" + extraParameter + ")";
		}


		extraParameter = "";

		foreach (ListItem item in cblCountryMusic.Items)
		{
			if (item.Selected)
				extraParameter = extraParameter + "'" + item.Value.ToString().ToLower() + "',";
		}

		if (!extraParameter.Equals("") && extraParameter.Length > 0)
		{
			extraParameter = extraParameter.Substring(0, extraParameter.Length - 1);
			sqlSearch = sqlSearch + " and lower(a.country) in(" + extraParameter + ")";
		}

		extraParameter = "";

		foreach (ListItem item in cblGradeMusic.Items)
		{
			if (item.Selected)
				extraParameter = extraParameter + "'" + item.Value.ToString().ToLower() + "',";
		}

		if (!extraParameter.Equals("") && extraParameter.Length > 0)
		{
			extraParameter = extraParameter.Substring(0, extraParameter.Length - 1);
			sqlSearch = sqlSearch + " and lower(a.grade) in(" + extraParameter + ")";
		}

			
		if (!ismn.Equals(""))
		{
			sqlSearch += " and lower(ismn) like lower(:ismn)";
		}
		command.Parameters.Add("ismn","%"+ismn + "%");
		//sqlSearch += " order by a.publishdate desc; ";
		command.CommandText=sqlSearch;
		return command;

	}

}
