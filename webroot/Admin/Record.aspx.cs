using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using Boeijenga.Business;
using Boeijenga.Common.Objects;

public partial class Admin_TableEditor_Record : System.Web.UI.Page
{
    #region Variables
    int numberOfColumn = 2;   //the information will be displayed in t
    string rtbsave = "";
    TextBox tBox = null;
    CheckBox cBox = null;
    Label lblName = null;
    Label lblPName = null;
    string tableName = "";
    string mode = "";
    string primaryField = "";
    string sql = "";
    DbHandler dbHandler = new DbHandler();
    NpgSqlMeta npMeta = new NpgSqlMeta();
    ArrayList customPrimaryKey = new ArrayList();
    ArrayList customFormatPk = new ArrayList();
    string pk = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["tablename"] != null && Request.Params["mode"] != null)
        {
            tableName = Request.Params["tablename"].ToString().ToLower();
            lblTableName.Text = tableName;
            pk = GetPrimaryKey(tableName);

            mode = Request.Params["mode"].ToString();
            if (!mode.Equals("delete"))
            {
                if (Request.Params[pk] != null)
                    primaryField = Request.Params[pk].ToString();


                //when user wants to add a new record
                //as the OBrowser tries to view the page in the edit tab
                //it automatically call this page with edit mode without primaryField value
                if (mode.Equals("edit") && primaryField.Equals(string.Empty)) 
                {
                    return;
                }

                LoadControls();
            }
        }
        if (!IsPostBack)
        {
            if (ViewState["pageSize"] == null)
            {
                ViewState["pageSize"] = int.Parse(System.Configuration.ConfigurationManager.AppSettings["page-size"].ToString());
            }
            lblErrorMesg.Text = "";

            Session["sortDirection"] = null;
            Session["sortExpression"] = null;
        }
    }

    /*---------------------Function Area------------------*/
    private string LoadFKDisplayColumn(string tableName)
    {
        string selCols = "";
        DataTable dt = new Facade().LoadForeignKeyDisplayColumn(tableName); //dbHandler.GetDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            selCols = dt.Rows[0]["columnnames"].ToString();
            string[] selInvCols = selCols.Split(',');
            for (int i = 0; i < selInvCols.Length; i++)
            {
                ListItem litem = new ListItem();
                litem.Text = selInvCols[i].ToString();
                litem.Value = selInvCols[i].ToLower();
            }
        }
        else
        {
            selCols = GetColumnNames(tableName);
        }
        return selCols;
    }

    private string GetPrimaryKey(string tab)
    {
        DataTable dt = new Facade().GetPrimaryKeyByTable(tab); //dbHandler.GetDataTable(sql);
        return dt.Rows[0]["column_name"].ToString().ToLower();
    }

    private string GetColumnNames(string tab)
    {
        string col = "";
        DataTable dt = new Facade().GetColumnNames(tab.ToLower()); //dbHandler.GetDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            col += dr["column_name"].ToString().ToLower() + ",";
        }
        if (col.Length > 0)
            return col.Substring(0, col.Length - 1);
        else
            return " * ";
    }

    #region Build the Contents
    private void BuildControls(string tab, DataTable dtValue, string pk)
    {
        DataTable dt = new Facade().GetColumnCaptionByTableName(tableName.ToLower()); //dbHandler.GetDataTable(sql);
        mode = Request.Params["mode"].ToString();
        ArrayList arrayFK = GetForeignKeys(tab);
        ArrayList arrayMandetory = npMeta.GetMendetoryFields(tab);
        int count = 0;
        TableRow tr = null;
        TableRow tr1 = null;
        foreach (DataColumn dc in dtValue.Columns)
        {
            DataRow[] dr = dt.Select("col='" + dc.ColumnName + "'");
            string columnName = dr[0]["col"].ToString();
            int maxLen = int.Parse(dr[0]["len"].ToString());
            string dataType = dr[0]["data_type"].ToString().ToLower();
            if (columnName.ToLower().Equals(pk.ToLower()) && dataType.ToLower().Equals("integer") && mode.Equals("new")) //avoid Showing Primary Key information
                continue;
            else if (columnName.ToLower().Equals(pk.ToLower()) && !mode.Equals("new"))
            {
                continue;
            }
            //code@provas date 15/12/2008. 
            //help to display in two column
            if (count % numberOfColumn == 0)
            {
                tr = new TableRow();
                tr1 = new TableRow();
            }
            /*-------------First cell contains Column Name----------------*/
            TableCell tc1 = new TableCell();

            Label label = new Label();
            label.Text = columnName;
            label.ID = "lbl" + columnName;
            tc1.HorizontalAlign = HorizontalAlign.Right;
            tc1.Controls.Add(label);

            /*--------------Second cell Contains Data------------------*/
            TableCell tc2 = new TableCell();

            if (dataType.Equals("boolean"))
            {
                cBox = new CheckBox();
                cBox.ID = "chk" + columnName;
                if (!mode.Equals("new"))
                    if (dtValue.Rows[0][columnName].ToString().ToLower().Equals("true"))
                        cBox.Checked = true;
                    else
                        cBox.Checked = false;

            }
            else
            {
                tBox = new TextBox();
                lblName = new Label();
                lblPName = new Label();
                if (arrayFK.Contains(columnName.ToLower()))
                {
                    //tBox.ReadOnly = true;
                    tBox.Attributes.Add("onkeypress", "return false");
                }
                if (!mode.Equals("new"))
                {
                    if (dataType.Equals("numeric") || dataType.Equals("integer"))
                    {
                        tBox.Text = dtValue.Rows[0][columnName].ToString();
                        tBox.MaxLength = 8;
                    }
                    else if ((maxLen > 50 && dataType.Equals("character varying")) || dataType.Equals("text"))
                    {
                        tBox.Text = dtValue.Rows[0][columnName].ToString();
                        tBox.MaxLength = maxLen;
                        tBox.Attributes.Add("ondblClick", "DisplayDiv('visible',this.id);");
                    }
                    else if (dataType.Equals("date") && !dtValue.Rows[0][columnName].ToString().Equals(""))
                    {
                        tBox.Text = System.DateTime.Parse(dtValue.Rows[0][columnName].ToString()).ToString("dd-MM-yyyy");
                        tBox.MaxLength = 10;
                    }
                    else
                    {
                        tBox.Text = dtValue.Rows[0][columnName].ToString();
                        tBox.MaxLength = maxLen;
                    }
                }
                else
                {
                    if (dataType.Equals("date"))
                    {
                        tBox.Text = System.DateTime.Now.Date.ToString("dd-MM-yyyy").ToString();
                        tBox.MaxLength = 10;
                    }
                    else if ((dataType.Equals("numeric") || dataType.Equals("integer")) && !arrayFK.Contains(columnName.ToLower()) && !columnName.ToLower().Equals(pk.ToLower()))
                    {
                        tBox.Text = "0";
                        tBox.MaxLength = 8;
                    }
                    else if ((maxLen > 50 && dataType.Equals("character varying")) || dataType.Equals("text"))
                    {
                        if (dataType.Equals("character varying"))
                            tBox.MaxLength = maxLen;
                        tBox.Attributes.Add("ondblClick", "javascript:return DisplayDiv('visible',this.id);");
                    }
                    else
                        tBox.MaxLength = maxLen;
                }

                tBox.ID = "txt" + columnName;

            }

            tc2.HorizontalAlign = HorizontalAlign.Left;

            if (dataType.Equals("boolean"))
                tc2.Controls.Add(cBox);
            else
                tc2.Controls.Add(tBox);

            /*-----------------Third Cell contains Image or Foreign Key References-------------*/
            TableCell tc3 = new TableCell();
            if (arrayFK.Contains(columnName.ToLower()))
            {
                ImageButton imglnk = new ImageButton();
                imglnk.ID = "lnk" + columnName.ToLower();
                imglnk.ImageUrl = "media/calendar.jpg";
                imglnk.AlternateText = "Select "+columnName;
                imglnk.Attributes.Add("Title", "Select " + columnName);
                imglnk.Height = Unit.Pixel(18);
                imglnk.Width = Unit.Pixel(20);
                imglnk.CausesValidation = false;
                imglnk.CommandArgument = columnName;
                imglnk.Command += new CommandEventHandler(this.lnkref_Command);

                AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                trigger.ControlID = imglnk.ID;
                trigger.EventName = "Click";
                upnlForeignKey.Triggers.Add(trigger);

                tc3.Controls.Add(imglnk);
            }
            else if (columnName.ToLower().Equals(pk.ToLower()))
            {
                Image img = new Image();
                img.ImageUrl = "media/key.gif";
                img.ImageAlign = ImageAlign.Left;
                //img.Attributes.Add("onClick", "javascript:ShowPkDiv('" + tableName + "','" + mode + "',this.id);");
                tc3.Controls.Add(img);
            }
            else
            {
                if (dataType.Equals("date"))
                {
                    Image img = new Image();
                    img.ImageUrl = "media/fTable.jpg";
                    img.Attributes.Add("Title", "Select Date");
                    img.Style.Add(HtmlTextWriterStyle.Height, "20px");
                    img.Style.Add(HtmlTextWriterStyle.Width, "22px");
                    if (!mode.Equals("view"))
                        img.Attributes.Add("Onclick", "displayDatePicker('" + "txt" + columnName + "');");
                    tc3.Controls.Add(img);
                }
            }

            tc3.HorizontalAlign = HorizontalAlign.Left;
            /*---------------------------Add validator if required-----------*/

            TableCell tc4 = new TableCell();

            if (arrayMandetory.Contains(columnName.ToLower()) && !mode.Equals("view") && !dataType.Equals("boolean"))
            {
                RequiredFieldValidator rfv = new RequiredFieldValidator();
                rfv.Display = ValidatorDisplay.Dynamic;
                rfv.ControlToValidate = "txt" + columnName;
                rfv.ID = "rfv" + columnName;
                rfv.Text = "Required";
                tc4.Controls.Add(rfv);
            }

            if (dataType.Equals("numeric") || dataType.Equals("integer"))
            {
                RegularExpressionValidator rexval = new RegularExpressionValidator();
                rexval.Display = ValidatorDisplay.Dynamic;
                rexval.ValidationExpression = @"^\d*$|^\d*[.,]\d{2}$";
                rexval.ControlToValidate = "txt" + columnName;
                rexval.ID = "rexval" + columnName;
                rexval.Text = "Numeric Required";
                tc4.Controls.Add(rexval);
            }
            tc4.HorizontalAlign = HorizontalAlign.Left;


            tr.Cells.Add(tc1);
            tr.Cells.Add(tc2);
            tr.Cells.Add(tc3);
            tr.Cells.Add(tc4);
            /*--------------optionally Generate a key Generator----------*/

            AddRow(tr, count);

            count++;
        }
    }

    private void LoadControls()
    {
        mode = Request.Params["mode"].ToString();
        tableName = Request.Params["tablename"].ToString();
        //string pk = GetPrimaryKey(tableName);

        if (!mode.Equals("new") && (pk != null || pk != ""))
            primaryField = Request.Params[pk].ToString();

        switch (mode)
        {
            case "view": tblControl.Visible = true;
                lnkSave.Visible = false;
                break;
            case "edit": tblControl.Visible = true;
                lnkSave.Visible = true;
                lnkSave.Text = "Update";
                break;
            case "new": tblControl.Visible = true;
                lnkSave.Visible = true;
                break;

        }
        sql = "select " + GetColumnNames(tableName) + " from " + tableName;
        if (!mode.Equals("new") && (pk != null || pk != ""))
            sql += " where " + pk + "='" + primaryField + "'";

        DataTable dt = new Facade().GetColumnBySQL(sql); //dbHandler.GetDataTable(sql);

        BuildControls(tableName, dt, pk);
    }

    private ArrayList GetForeignKeys(string tab)
    {
        DataTable dt = new Facade().GetForeignKeys(tab.ToLower()); //dbHandler.GetDataTable(sql);
        ArrayList array = new ArrayList();
        foreach (DataRow dr in dt.Rows)
        {
            array.Add(dr["col"].ToString());
        }

        if (tab.Equals("article"))
        {
            array.Add("category");
            array.Add("subcategory");
        }
        return array;
    }

    #endregion

    /// <summary>
    ///code@provas date 15/12/2008. 
    ///help to display the information according to the value of numberofcolumn
    /// </summary>
    /// <param name="tr"></param>
    /// <param name="count"></param>
    private void AddRow(TableRow tr, int count)
    {
        if (count % numberOfColumn == 0)
        {
            tblControl.Rows.Add(tr);
        }
    }

    #region Build Dynamic Query
    private string BuildUpdateQuery()
    {
        tableName = Request.Params["tablename"].ToString();

        primaryField = Request.Params[pk].ToString();

        string column = "update " + tableName + " set ";
        DataTable dt = new Facade().UpdateTableByTableName(tableName); //dbHandler.GetDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["column_name"].ToString().Equals(pk))
            {
                continue;
            }
            TextBox tb = null;
            CheckBox cb = null;
            if (dr["data_type"].ToString().ToLower().Equals("boolean"))
            {
                cb = (CheckBox)tblControl.FindControl("chk" + dr["column_name"].ToString());
                column += dr["column_name"].ToString() + "=@" + dr["column_name"].ToString() + ",";
            }
            else
            {
                tb = (TextBox)tblControl.FindControl("txt" + dr["column_name"].ToString());
                column += dr["column_name"].ToString() + "=@" + dr["column_name"].ToString() + ",";
            }


        }
        column = column.Substring(0, column.Length - 1);
        column += " where " + pk + "='" + primaryField + "'";
        return column;
    }
    private string BuildInsertQuery()
    {
        tableName = Request.Params["tablename"].ToString();
        string column = "";
        string parm = "";
        DataTable dt = new Facade().GetDataTypeByTableName(tableName.ToLower());// dbHandler.GetDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["column_name"].ToString().Equals(pk) && 
                dr["data_type"].ToString().ToLower().Equals("integer"))
            {
                continue;
            }
            TextBox tb = null;
            CheckBox cb = null;
            if (dr["data_type"].ToString().ToLower().Equals("boolean"))
            {
                cb = (CheckBox)tblControl.FindControl("chk" + dr["column_name"].ToString());
                column += dr["column_name"].ToString() + ",";
                parm += "@" + dr["column_name"].ToString() + ",";
            }
            else
            {
                tb = (TextBox)tblControl.FindControl("txt" + dr["column_name"].ToString());
                if (!tb.Text.ToString().Trim().Equals(""))
                {
                    column += dr["column_name"].ToString() + ",";
                    parm += "@" + dr["column_name"].ToString() + ",";
                }
            }


        }
        column = column.Substring(0, column.Length - 1);
        parm = parm.Substring(0, parm.Length - 1);
        sql = "insert into " + tableName + "(" + column + ") values(" + parm + ") ";
        return sql;
    }
    #endregion

    //public string GetRefTablePrimaryKey()
    //{
    //    string refTable = Session["refTable"].ToString();
    //    return npMeta.GetPrimaryKey(refTable);
    //}


    private void LoadReferenceTable(string orderBy, string dir, long offset, int limit)
    {
        string refTable = Session["refTable"].ToString();
        //string val = txtSearchValue.Text.Trim();
        //string col = ddlSearch.SelectedValue.ToString();
        string pk = GetPrimaryKey(refTable);

        sql = "select " + LoadFKDisplayColumn(refTable) + " from " + refTable;

        //if (!val.Equals(""))
        //    sql += " where lower(" + col.ToLower() + ") like '%" + val.ToLower() + "%' ";

        if (orderBy != "" && dir != "")
        {
            sql += "  order by " + orderBy + " " + dir;
        }

        DataRecord recordSet = new Facade().GetReferenceTableBySql(sql, offset, limit);//dbHandler.GetDataTable(sql);
        if (recordSet.Table.Rows.Count < 1)
        {
            DataRow dr = recordSet.Table.NewRow();
            recordSet.Table.Rows.Add(dr);
        }
        try
        {
            grdForeignKey.DataSource = recordSet.Table;
            grdForeignKey.VirtualItemCount = int.Parse(recordSet.Count.ToString());
            grdForeignKey.DataBind();
            HideFirstColumn();
            upnlForeignKey.Update();
        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
        }

        modalPopupExtender2.Show();

    }

    private void HideFirstColumn()
    {
        if (grdForeignKey.Columns.Count > 0)
            grdForeignKey.Columns[0].Visible = false;
        else
        {
            grdForeignKey.HeaderRow.Cells[0].Visible = false;
            foreach (GridViewRow gvr in grdForeignKey.Rows)
            {
                gvr.Cells[0].Visible = false;
            }
        }
    }

    #region Execute SQL
    private void ExecuteInsert(string sql)
    {
        string errorMsg = "";
        NpgsqlCommand npg = new NpgsqlCommand(sql);
        DataTable dt = new Facade().GetDataTypeByTableName(tableName.ToLower());//dbHandler.GetDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["column_name"].ToString().Equals(pk) && 
                dr["data_type"].ToString().ToLower().Equals("integer"))
            {
                continue;
            }
            TextBox tb = null;
            CheckBox cb = null;
            if (dr["data_type"].ToString().ToLower().Equals("boolean"))
            {
                cb = (CheckBox)tblControl.FindControl("chk" + dr["column_name"].ToString());
                npg.Parameters.Add(dr["column_name"].ToString(), cb.Checked);
            }
            else if (dr["data_type"].ToString().ToLower().Equals("date"))
            {
                tb = (TextBox)tblControl.FindControl("txt" + dr["column_name"].ToString());
                //string[] dtIn=tb.Text.Split(new char[]{'-'});
                System.Globalization.CultureInfo enUS = new System.Globalization.CultureInfo("en-US", true);
                System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.DateTimeFormatInfo();
                dtfi.ShortDatePattern = "dd-MM-yyyy";
                dtfi.DateSeparator = "-";
                if (!tb.Text.ToString().Trim().Equals(""))
                {
                    DateTime dtIn = Convert.ToDateTime(tb.Text.Trim(), dtfi);
                    npg.Parameters.Add(dr["column_name"].ToString(), dtIn.ToString("yyyy-MM-dd"));
                }
            }
            else if (dr["data_type"].ToString().ToLower().Equals("numeric") || dr["data_type"].ToString().ToLower().Equals("integer"))
            {
                tb = (TextBox)tblControl.FindControl("txt" + dr["column_name"].ToString());
                if (!tb.Text.ToString().Trim().Equals(""))
                    npg.Parameters.Add(dr["column_name"].ToString(), Double.Parse(tb.Text.Trim().Replace(',', '.')));
            }
            else
            {
                tb = (TextBox)tblControl.FindControl("txt" + dr["column_name"].ToString());
                if (!tb.Text.ToString().Trim().Equals(""))
                    npg.Parameters.Add(dr["column_name"].ToString(), tb.Text);
            }
        }
        bool b = new Facade().ExecuteInsertCommand(npg, ref errorMsg); //dbHandler.ExecuteQuery(npg, ref errorMsg);
        if (b)
        {
            Session["refTable"] = null;
            Session["refCol"] = null;
            Session["sortDirection"] = null;
            Session["sortExpression"] = null;
            lblErrorMesg.Text = "";
        }
        else
        {
            lblErrorMesg.Text = errorMsg;
        }

    }
    private void ExecuteUpdate(string sql)
    {
        string errorMsg = "";
        NpgsqlCommand npg = new NpgsqlCommand(sql);
        DataTable dt = new Facade().GetDataTypeByTableName(tableName.ToLower());//dbHandler.GetDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["column_name"].ToString().Equals(pk))
            {
                continue;
            }
            TextBox tb = null;
            CheckBox cb = null;
            if (dr["data_type"].ToString().ToLower().Equals("boolean"))
            {
                cb = (CheckBox)tblControl.FindControl("chk" + dr["column_name"].ToString());
                npg.Parameters.Add(dr["column_name"].ToString(), cb.Checked);
            }
            else if (dr["data_type"].ToString().ToLower().Equals("date"))
            {
                tb = (TextBox)tblControl.FindControl("txt" + dr["column_name"].ToString());
                //string[] dtIn=tb.Text.Split(new char[]{'-'});
                System.Globalization.CultureInfo enUS = new System.Globalization.CultureInfo("en-US", true);
                System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.DateTimeFormatInfo();
                dtfi.ShortDatePattern = "dd-MM-yyyy";
                dtfi.DateSeparator = "-";
                if (!tb.Text.ToString().Trim().Equals(""))
                {
                    DateTime dtIn = Convert.ToDateTime(tb.Text.Trim(), dtfi);
                    npg.Parameters.Add(dr["column_name"].ToString(), dtIn.ToString("yyyy-MM-dd"));
                }
                else
                    npg.Parameters.Add(dr["column_name"].ToString(), null);

            }
            else if (dr["data_type"].ToString().ToLower().Equals("numeric") || dr["data_type"].ToString().ToLower().Equals("integer"))
            {
                tb = (TextBox)tblControl.FindControl("txt" + dr["column_name"].ToString());
                if (!tb.Text.ToString().Trim().Equals(""))
                    npg.Parameters.Add(dr["column_name"].ToString(), Double.Parse(tb.Text.Trim().Replace(',', '.')));
                else
                    npg.Parameters.Add(dr["column_name"].ToString(), null);
            }
            else
            {
                tb = (TextBox)tblControl.FindControl("txt" + dr["column_name"].ToString());
                if (!tb.Text.ToString().Trim().Equals(""))
                    npg.Parameters.Add(dr["column_name"].ToString(), tb.Text);
                else
                {
                    npg.Parameters.Add(dr["column_name"].ToString(), null);
                }
            }
        }
        bool b = new Facade().ExecuteUpdateCommand(npg, ref errorMsg);//dbHandler.ExecuteQuery(npg, ref errorMsg);
        if (b)
        {
            Session["refTable"] = null;
            Session["refCol"] = null;
            Session["sortDirection"] = null;
            Session["sortExpression"] = null;
        }
        else
        {
            lblErrorMesg.Text = errorMsg;
        }

    }
    #endregion

    /*-------------------Event Handler Area ---------------*/

    protected void lnkSave_Click(object sender, System.EventArgs e)
    {
        mode = Request.Params["mode"].ToString();
        tableName = Request.Params["tablename"].ToString();

        switch (mode)
        {
            case "edit": sql = BuildUpdateQuery();
                ExecuteUpdate(sql);
                break;
            case "new": sql = BuildInsertQuery();
                ExecuteInsert(sql);
                break;
            default: break;
        }
        Response.Write("<script> parent.OBSettings.RefreshPage();</script>");
    }

    protected void lnkref_Command(object sender, CommandEventArgs e)
    {
        //txtSearchValue.Text = "";
        Session["sortDirection"] = null;
        Session["sortExpression"] = null;
        tableName = Request.Params["tablename"].ToString();
        string colName = e.CommandArgument.ToString();

        DataTable dt = new Facade().GetDataByTableAndColumnName(tableName.ToLower(), colName.ToLower()); //dbHandler.GetDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            Session["refTable"] = dt.Rows[0]["reftab"].ToString();
            Session["refCol"] = colName;
            LoadReferenceTable("", "", 0, int.Parse(ViewState["pageSize"].ToString()));
        }
    }

    protected void lnkSelect_Command(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        string refTable = Session["refCol"].ToString();
        string refCol = Session["refCol"].ToString();
        tableName = Request.Params["tablename"].ToString();

        string columnName = "txt" + refCol;
        TextBox tb = (TextBox)tblControl.FindControl(columnName);
        tb.Text = id.ToString();

        Session["sortDirection"] = null;
        Session["sortExpression"] = null;
        Session["refCol"] = null;
        Session["refTable"] = null;
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        Session["refTable"] = null;
        Session["sortDirection"] = null;
        Session["sortExpression"] = null;
        Response.Write("<script> parent.OBSettings.RefreshPage();</script>");

    }
    #region Foreign Key Grid Event
    protected void grdForeignKey_Sorting(object sender, GridViewSortEventArgs e)
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
        LoadReferenceTable(e.SortExpression, dir, 0, int.Parse(ViewState["pageSize"].ToString()));
    }
    protected void grdFKeyTab_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sort = "";
        string sortDirection = "";
        sort = e.SortExpression.ToString().ToLower();

        if (Session["sortDirection"] == null && Session["sortExpression"] == null)
        {
            Session["sortDirection"] = "Desc";
            Session["sortExpression"] = sort;
        }
        else
        {
            sort = Session["sortExpression"].ToString().ToLower();
            sortDirection = Session["sortDirection"].ToString();
            if (e.SortExpression.ToString().ToLower().Equals(sort))
            {
                if (sortDirection.Equals("Desc"))
                {
                    Session["sortDirection"] = "Asc";
                    Session["sortExpression"] = sort;
                }
                else
                {
                    Session["sortExpression"] = sort;
                    Session["sortDirection"] = "Desc";
                }
            }
            else
            {
                Session["sortDirection"] = "Desc";
                Session["sortExpression"] = e.SortExpression.ToString();
            }
        }
        string temp = @"<script>javascript:DisplayDivFk('visible','" + Session["refCol"].ToString() + "')</script>";
        Page.RegisterStartupScript("ShowDiv", temp);
    }

    protected void grdForeignKey_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdForeignKey.PageIndex = e.NewPageIndex;
        if (ViewState["sortExpr"] != null)
        {
            LoadReferenceTable(ViewState["sortExpr"].ToString(), ViewState["sortdir"].ToString(),
                int.Parse(ViewState["pageSize"].ToString()) * e.NewPageIndex, int.Parse(ViewState["pageSize"].ToString()));
        }
        else
        {
            LoadReferenceTable("", "", int.Parse(ViewState["pageSize"].ToString()) * e.NewPageIndex, int.Parse(ViewState["pageSize"].ToString()));
        }
    }
    protected void grdForeignKey_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Literal lit = new Literal();
            lit.Text = "<a href='#' onClick=\"SetValue('txt" + Session["refCol"].ToString() + "','" +
                e.Row.Cells[0].Text + "')\"" + ">" + e.Row.Cells[1].Text + "</a>";
            e.Row.Cells[1].Controls.Add(lit);
       }
    }

    #endregion

}