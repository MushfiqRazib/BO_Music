using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Data.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Boeijenga.DataAccess.Constants;
using Boeijenga.Business;
using System.Collections.Generic;
using Boeijenga.Common.Objects;

public partial class Admin_ImportExcelToDb : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ChangeForTableName();
            ShowAvailableFields(DropDownList1.SelectedValue);

        }
    }

   
    protected void Button1_Click(object sender, EventArgs e)
    {
        string excelFile="";
        try
        {
            DataTable dt = GetXLSDataTable(ref excelFile, "Blad1");

            if (DataTableToDatabase(dt, DropDownList1.SelectedValue))
            {
                Label1.Text = "Successfully inserted";
            }
            
            if (System.IO.File.Exists(excelFile))
            {
                System.IO.File.Delete(excelFile);
            }
        }
        catch (Exception exp)
        {
            Boeijenga.Common.Utils.LogWriter.Log(exp);
            Label1.Text = exp.Message.ToString();
            
        }
        finally
        {
            
        
        }
    }

    public string FilterCellData(string sourceData)
    {

        string filterstring = sourceData;
        if (sourceData.IndexOf("'") != -1)
        {
            filterstring = sourceData.Replace("'", "''");
      
        }
        
        return filterstring;

    }

    public bool DataTableToDatabase(DataTable srcdt, string physicalTableName)
    {
        bool isInsert = true;

        try
        {
          //  DbHandler dh = new DbHandler();
            StringBuilder MyStringBuilder = new StringBuilder(String.Empty);


            for (int i = 1; i < srcdt.Columns.Count; i++)
            {
                // loop counter started from 1 as we like exclude the first columns as that
                // coulumn is auto generated primary key.
                MyStringBuilder.Append(srcdt.Columns[i].ColumnName + ",");
            }

            ArrayList queries = new ArrayList();
            StringBuilder QueryBuilder = new StringBuilder();
            string columns = MyStringBuilder.ToString().Substring(0, MyStringBuilder.ToString().Length - 1);

            for (int i = 0; i < srcdt.Rows.Count; i++)
            {
                string qr;
                if (physicalTableName.Equals("article"))
                {
                    qr = "insert into " + physicalTableName  + " (articlecode," + columns + ") values ('" + GetArticlePrimaryKey(srcdt.Rows[i]["articletype"].ToString(), (i + 1)) + "',";
                }
                else
                {
                    qr = "insert into " + physicalTableName  + " (" + columns + ") values (";        
                }
                

                for (int j = 1; j < srcdt.Columns.Count; j++)
                {
                    string CellData = srcdt.Rows[i][j].ToString();
                    if (CellData == "" || CellData == null || CellData == "")
                    {
                        CellData = "NULL";
                    }
                    else
                    {
                        CellData = FilterCellData(CellData);
                        CellData = "'" + CellData + "'";
                    }
                    qr += CellData + ",";
                }
                qr = qr.Substring(0, qr.Length - 1);
                qr += ");";
                queries.Add(qr);
                QueryBuilder.Append(qr);
            
            }
            string msg = string.Empty;
            new Facade().ExecuteTransactionArraylistCommand(queries, ref msg); 
            //dh.ExecuteTransaction(queries,ref msg);
            if (!msg.Equals(string.Empty))
            {
                isInsert = false;
                Label1.Text = msg;
            }
        }
        catch
        {
            isInsert = false;
        }
        return isInsert;
    }

    private string GetArticlePrimaryKey(string articletype,int index)
    {
       // DbHandler dh = new DbHandler();
        DataTable dt = new Facade().GetDataTableBySQL("select max(substring(articlecode,2,length(articlecode))) from article where substring(articlecode,1,1)='" + articletype + "'"); //dh.GetDataTable("select max(substring(articlecode,2,length(articlecode))) from article where substring(articlecode,1,1)='" + articletype + "'");
        int maxval=Int32.Parse(dt.Rows[0][0].ToString());

        maxval = maxval + index;
        
        string newPrimaryKey = string.Format("{0:000000}", maxval);
        newPrimaryKey = articletype + newPrimaryKey;

        //if (articletype == "c" || articletype == "d")
        //{
        //    newPrimaryKey = "c" + newPrimaryKey;
        //}
        //else
        //{
        //    newPrimaryKey = articletype + newPrimaryKey;
        //}

        return newPrimaryKey;
        
    }
    private DataTable getDataFromXLS(string strFilePath, string sheetName)
    {
        
        string strConnectionString = string.Empty;
        strConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + @";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";
        OleDbConnection cnCSV = new OleDbConnection(strConnectionString);
        DataTable dtCSV = new DataTable();
        try
        {

           
            //strConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilePath + @";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";
            //AccessDatabaseEngine.exe should be installed for following connection string which given you xlsx supports //

            cnCSV.Open();
            OleDbCommand cmdSelect = new OleDbCommand(@"SELECT * FROM [" + sheetName + "$]", cnCSV);
            OleDbDataAdapter daCSV = new OleDbDataAdapter();
            daCSV.SelectCommand = cmdSelect;
          
            daCSV.Fill(dtCSV);
            cnCSV.Close();
            daCSV = null;
           
        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
            Label1.Text = ex.Message;
        }
        finally
        {
            cnCSV.Close();
        }
        return dtCSV;
    }

    protected void btnUpdateArticle_Click(object sender, EventArgs e)
    {
        string excelFile="";
        try
        {
            DataTable dt = GetXLSDataTable(ref excelFile, "article");

            if (UpdateDatabase(dt, drpdlEvents.SelectedValue, DropDownList1.SelectedValue))
            {
                Label1.Text = "Successfully inserted";
            }

            if (System.IO.File.Exists(excelFile))
            {
                System.IO.File.Delete(excelFile);
            }
        }
        catch (Exception exp)
        {
            Boeijenga.Common.Utils.LogWriter.Log(exp);
            Label1.Text = exp.Message.ToString();

        }
        finally
        {

        }
    }

    private DataTable GetXLSDataTable(ref string excelFile, string sheetName)
    {
        string ext = "";

        int statr = FileUpload1.PostedFile.FileName.ToString().LastIndexOf(".");
        int len = FileUpload1.PostedFile.FileName.ToString().Length - statr;
        ext = FileUpload1.PostedFile.FileName.ToString().Substring(statr, len);

        string filepath = System.Guid.NewGuid() + "_TempExcelFile" + ext;
        string excelFile1 = "~\\" + filepath;
        excelFile = Server.MapPath(excelFile1);
        FileUpload1.PostedFile.SaveAs(excelFile);
        
        return getDataFromXLS(excelFile, sheetName);
    }

    public bool UpdateDatabase(DataTable srcdt, string eventName, string physicalTableName)
    { 
        bool isUpdate = true;

        try
        {
          //  DbHandler dh = new DbHandler();
            StringBuilder MyStringBuilder = new StringBuilder(String.Empty);

            foreach (DataRow dr in srcdt.Rows)
            {
                MyStringBuilder.AppendFormat(string.Format(@"'{0}' ,", dr["articlecode"].ToString()));
            }

            string updateQuery = @"UPDATE article SET events = " + eventName + @" WHERE articlecode IN (" + MyStringBuilder.ToString().TrimEnd(new char[]{','}) + @")";

            string msg = string.Empty;
            new Facade().ExecuteUpdateCommand(new Npgsql.NpgsqlCommand(updateQuery),ref msg);

            if (!msg.Equals(string.Empty))
            {
                isUpdate = false;
                Label1.Text = msg;
            }
        }
        catch
        {
            isUpdate = false;
        }
        return isUpdate;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChangeForTableName();
        ShowAvailableFields(DropDownList1.SelectedValue);
        Label1.Text = "";
    }

    private void ShowAvailableFields(string table)
    {
      //  DbHandler handler = new DbHandler();
       
        DataTable dt = new Facade().GetAvailableFieldsbyTableName(table.ToLower()); //handler.GetDataTable(query);
        StorePrimaryKeys(dt);
        lstFields.DataSource = dt;
        lstFields.DataTextField = "text";
        lstFields.DataValueField = "value";
        lstFields.DataBind();
    }

    private void StorePrimaryKeys(DataTable dt)
    {
        DataRow[] rows = dt.Select("text='PRIMARY KEY'");
        string keys = "";
        foreach (DataRow row in rows)
        {
            keys += row["value"] + ",";
        }
        ViewState["pKeys"] = keys.TrimEnd(',');
    }

    private void ChangeForTableName()
    {
        if (DropDownList1.SelectedItem.Text.Equals("article"))
        {
            tblEvents.Visible = true;
            LoadEventDropdown();
        }
        else
        {
            tblEvents.Visible = false;
        }
    }

    public void LoadEventDropdown()
    {
       // DbHandler dh = new DbHandler();
        DataTable dt = new Facade().GetDataTableBySQL(@"SELECT eventid, eventnameen FROM events");

        if (dt.Rows.Count > 0)
        {
            drpdlEvents.DataSource = dt;
            drpdlEvents.DataTextField= dt.Columns["eventnameen"].ToString();
            drpdlEvents.DataValueField = dt.Columns["eventid"].ToString();
            drpdlEvents.DataBind();
        }
    }
    protected void btnUpdateRecords_Click(object sender, EventArgs e)
    {
        string excelFile = "";
        DataTable dt = new DataTable();
        try
        {
            dt = GetXLSDataTable(ref excelFile, "Blad1");
            if (dt.Rows.Count < 1)
            {
                Label1.Text = "No record found in the sheet";
                return;
            }
            string key = "";
            if (!PrimaryKeysExists(dt, ref key))
            {
                Label1.Text = "Column '" + key + "' not found in the Excel Sheet";
                return;
            }
        }
        catch (Exception exp)
        {
            Boeijenga.Common.Utils.LogWriter.Log(exp);
            Label1.Text = exp.Message.ToString();
        }
        finally
        {
            if (System.IO.File.Exists(excelFile))
            {
                System.IO.File.Delete(excelFile);
            }
        }
        string pKeys = Convert.ToString(ViewState["pKeys"]).ToLower();
        foreach (ListItem item in lstFields.Items)
        {
            if (item.Selected)
            {
                if (!ColumnExists(item.Value,dt))
                {
                    Label1.Text = "Column " + item.Value + " is not in the Excel Sheet.";
                    return;
                }
                item.Selected = pKeys.IndexOf(item.Value.ToLower()) != -1 ? item.Selected = false : item.Selected = true;
            }
        }
        ArrayList commands = BuildUpdateQueries(dt);
        //DbHandler handler = new DbHandler();
        string messege = "";
        if (new Facade().ExecuteTransactionArraylistCommand(commands, ref messege))
        {
            Label1.Text = "All records are updated successfully !!";
        }
        else
        {
            Label1.Text = "Update failed.<br/> "+ messege;
        }
        
    }

    private ArrayList BuildUpdateQueries(DataTable dt)
    {
        ArrayList queries = new ArrayList();
        string query = "";
        string[] pKeys = Convert.ToString(ViewState["pKeys"]).Split(',');

        foreach (DataRow row in dt.Rows)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            query = "Update " + DropDownList1.SelectedValue + " Set ";
            foreach (ListItem item in lstFields.Items)
            {
                if (item.Selected)
                {
                    query += item.Value + " = '" + row[item.Value].ToString() + "',";
                    //command.Parameters.Add(item.Value, row[item.Value].ToString());
                }
                //query += item.Selected ? item.Value + " = '" + row[item.Value] + "'," : "";
            }
            query = query.TrimEnd(',');
            query += " where ";
            
            foreach (string pKey in pKeys)
            {
                query += pKey + " = '" + row[pKey].ToString() + "' and ";  //build where clause
                //command.Parameters.Add(pKey, row[pKey].ToString());
                //                query += pKey + " = '" + row[pKey] + "' and ";  //build where clause
            }
            //command.CommandText=query.Substring(0, query.Length - 4);
            queries.Add(query.Substring(0, query.Length - 4));
        }
        return queries;
    }

    /// <summary>
    /// This function will check whether Primary key 
    /// column is exists in the databatable or not.
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    private bool PrimaryKeysExists(DataTable dt, ref string key)
    {
        string []pKeys = Convert.ToString(ViewState["pKeys"]).Split(','); //get primary key from the datatable
        foreach (string pKey in pKeys)
        {
            if (!ColumnExists(pKey, dt))
            {
                key = pKey;
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// This function will check whether a supplied 
    /// column is exists in the databatable or not.
    /// </summary>
    /// <param name="column"></param>
    /// <param name="dt"></param>
    /// <returns></returns>
    private bool ColumnExists(string column, DataTable dt)
    {
        foreach (DataColumn dc in dt.Columns)
        {
            if (dc.ColumnName.ToLower().Equals(column))
            {
                return true;
            }
        }
        return false;
    }

////    protected void Button2_Click(object sender, EventArgs e)
////    {
////        Functions func = new Functions();
////        Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
////        command.CommandText = @"insert into articles (articlecode,publisher,editionno,isbn10,isbn13,title,Subtitle,composer,price,isactive,articletype,Quantity,Category) 
////values (:articlecode,:publisher,:editionno,:isbn10,:isbn13,:title,:Subtitle,:composer,:price,:isactive,:articletype,:Quantity,:Category)";
////        command.Parameters.Add("articlecode","b000762");
////        command.Parameters.Add("publisher","1860");
////        command.Parameters.Add("editionno","NULL");
////        command.Parameters.Add("isbn10","NULL");
////        command.Parameters.Add("isbn13","NULL");
////        command.Parameters.Add("title", Functions.ConvertAsciiToUtf8("Dein tief betrübter Papa …"));
////        command.Parameters.Add("Subtitle","NULL");
////        command.Parameters.Add("composer","1214");
////        command.Parameters.Add("price","7.2");
////        command.Parameters.Add("isactive","True");
////        command.Parameters.Add("articletype","b");
////        command.Parameters.Add("Quantity","2");
////        command.Parameters.Add("Category","B101");
////        DbHandler handler = new DbHandler();
////        handler.ExecuteQuery(command);
//////('b000762','1860',NULL,NULL,NULL,'Dein tief betrübter Papa …',NULL,'1214','7.2','True','b','2','B101')";
////    }
}
