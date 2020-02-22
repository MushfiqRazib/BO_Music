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
using bo01;

public partial class Authors : BasePage
{
    ArrayList cartTable = new ArrayList();
    string cultureName = "";
    static string defIndex = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);
        SetCulture();
        if (!IsPostBack)
        {
            DisplayAuthorsIndex();
            DisplayAuthorsInfo(defIndex);
        }
        SetCulturalValue();
    }

    private void DisplayAuthorsInfo(string range)
    {
        string[] token = range.ToUpper().Split('-');
        string startIndex = token[0].Trim();
        
        string endIndex="";
        try
        {
            endIndex = token[1].Trim();
        }
        catch
        {
            endIndex = startIndex;
        }

        endIndex = (endIndex.EndsWith("Z")) ? endIndex + "Z" : endIndex.Substring(0, endIndex.Length - 1) +
            char.ConvertFromUtf32(Convert.ToChar(endIndex.Substring(endIndex.Length - 1, 1)) + 1);
        
        lstAuthors.DataSource = GetAuthorsInfo(startIndex, endIndex);
        lstAuthors.DataBind();
    }

    #region Display Author's Index
    /// <summary>
    /// This method should display the alphabatic index range in the datagrid
    /// </summary>
    private void DisplayAuthorsIndex()
    {
        DataTable indexAuthors = GetIndexedTable();
        gvIndex.DataSource = indexAuthors;
        gvIndex.DataBind();
    }

    private DataTable GetIndexedTable()
    {
        string[] authorIndex = { "A", "Ba - Bk", "Bl - Bz", "C", "D", "E - F", "G", "Ha - Hk", 
            "Hl - J", "K", "L", "M", "N - P", "Q - R", "Sa - Sk", "Sl - T", "U - V", "W - Z" };
        defIndex = authorIndex[0];                        // store first index as default selected index
        DataTable dtAuthorIndex = new DataTable();
        DataColumn dcIndex = new DataColumn("index");     //store index as per composer's name
        dtAuthorIndex.Columns.Add(dcIndex);
        DataRow dr;
        foreach (string range in authorIndex)
        {
            dr = dtAuthorIndex.NewRow();
            dr["index"] = range;
            dtAuthorIndex.Rows.Add(dr);
        }
        dr = dtAuthorIndex.NewRow();
        dr["index"] = (string)base.GetGlobalResourceObject("string", "CollectionBooks");
        dtAuthorIndex.Rows.Add(dr);
        dr = dtAuthorIndex.NewRow();
        dr["index"] = (string)base.GetGlobalResourceObject("string", "CollectionSheetmusic");
        dtAuthorIndex.Rows.Add(dr);
        dr = dtAuthorIndex.NewRow();
        dr["index"] = (string)base.GetGlobalResourceObject("string", "CollectionCD");
        dtAuthorIndex.Rows.Add(dr);
        
       

        return dtAuthorIndex;
    }
    #endregion

    #region Get Authors Info
    /// <summary>
    /// This function should store a datatable containing 
    /// complete set of Composer's record except 'Collection' and 'Diverse'
    /// and store in the viewstate.
    /// </summary>
    /// <returns></returns>
    private DataTable GetAuthorsInfo(string startIndex, string endIndex)
    {

        DbHandler handler = new DbHandler();
        string query = @"
            select lower(COALESCE(lastname, '') || COALESCE(firstname, '') || COALESCE(middlename, '')) as name,
		    trim(both ' ,' from (COALESCE(lastname || ', ' , '') || COALESCE(firstname || ' ', '') || COALESCE(middlename || ' ', '')) || 
		    case when (dob is null or dob='') and (dod is null or dod='') then '' 
			when (dob is null or dob='') then '(*' || dod || ')'
			when (dod is null or dod='') then '(' || dob || '*)'
			else '(' || dob || '-' || dod || ')'  end) as info, composerid
            from composer
            where upper(COALESCE(lastname, '') || COALESCE(firstname, '') || COALESCE(middlename, '')) between '" + startIndex + @"%' and '" + endIndex + @"%'
            and lower(COALESCE(lastname, '') || COALESCE(firstname, '') || COALESCE(middlename, '')) not in ('collection','diverse','')
            order by lower(COALESCE(lastname, '') || COALESCE(firstname, '') || COALESCE(middlename, ''))
";
        return handler.GetDataTable(new Npgsql.NpgsqlCommand(query));

    }
    #endregion

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

    void Master_MenuButton(object sender, EventArgs e)
    {
        Session["cultureName"] = Master.CurrentButton.ToString();
        SetCulture();
        SetCulturalValue();
        DisplayAuthorsIndex();
        DisplayAuthorsInfo(defIndex);
    }

    protected void lnkAuthor_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandArgument.ToString().ToLower().IndexOf("collect") !=-1 ||
            e.CommandArgument.ToString().ToLower().IndexOf("verzam") != -1)
        {
            string type = string.Empty;
            switch (e.CommandArgument.ToString())
            {
                case "Collection – Books":
                case "Verzamel – Boeken":
                    type = "b";
                    break;
                case "Collection – Sheetmusic":
                case "Verzamel – Bladmuziek":
                    type = "s";
                    break;
                case "Collection – CD":
                case "Verzamel – CD":
                    type = "c";            
                    break;
                default:
                    break;

            }
            Response.Redirect("SearchResult.aspx?composer=132,1208&type="+type);
            
        }
        else
        {
            defIndex = e.CommandArgument.ToString();
            DisplayAuthorsInfo(defIndex);
        }
        SetCulturalValue();
    }

    private void SetCulturalValue()
    {
        lblIndexHeader.Text = (string)base.GetGlobalResourceObject("string", "composer") + " A-Z";
        lblAuthorInfo.Text = (string)base.GetGlobalResourceObject("string", "composer") + " " + defIndex;

    }

    //private void SetCulturalValue(string index)
    //{
    //    lblAuthorInfo.Text = (string)base.GetGlobalResourceObject("string", "composer") + " " + index;
    //}

    protected void gvIndex_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }
    protected void gvIndex_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.fontWeight='bold';";
            e.Row.Attributes["onmouseout"] = "this.style.fontWeight='normal';";

          //  e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvIndex, "Select$" + e.Row.RowIndex);
        }

    }
}
