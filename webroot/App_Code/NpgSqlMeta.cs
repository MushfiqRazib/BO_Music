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
using Npgsql;
using Boeijenga.DataAccess.Constants;
using Boeijenga.Business;
using System.Collections.Generic;
using Boeijenga.Common.Objects;
/// <summary>
/// Summary description for NpgSqlMeta
/// </summary>
public class NpgSqlMeta
{
	NpgsqlConnection conn;
	DbHandler dbHandler=new DbHandler();
	string sql="";
	public NpgSqlMeta()
	{

        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        // string conStr = "user id=postgres; password=postgres; database=Bo02N3; server=localhost; encoding=unicode; Timeout=60;";
		conn = new NpgsqlConnection(conStr);
	}
	public string GetPrimaryKey(string tableName)
	{
		//sql = @"select column_name,constraint_name,substring(constraint_name from '_.key') as cons
		//	from information_schema.constraint_column_usage where lower(table_name)='" + tableName.ToLower() + "' and substring(constraint_name from '_.key')='_pkey'";
        DataTable dt = new Facade().GetPrimaryKey(tableName); //dbHandler.GetDataTable(sql);
		return dt.Rows[0]["column_name"].ToString().ToLower();
	}
	public string GetColumnNames(string tableName)
	{
		string col = "";
        sql = "select column_name from information_schema.columns where lower(table_name)='" + tableName.ToLower() + "'";
		DataTable dt = dbHandler.GetDataTable(sql);
		foreach (DataRow dr in dt.Rows)
		{
			col += dr["column_name"].ToString().ToLower() + ",";
		}

		return col.Substring(0, col.Length - 1);
	}
	public ArrayList GetForeignKeys(string tab)
	{
        sql = "select split_part(constraint_name,'_',2) as col,constraint_type from information_schema.table_constraints where lower(table_name)='" + tab.ToLower() + "' and lower(constraint_type)='foreign key'";
		DataTable dt = dbHandler.GetDataTable(sql);
		ArrayList array = new ArrayList();
		foreach (DataRow dr in dt.Rows)
		{
			array.Add(dr["col"].ToString());
		}
		return array;
	}
	public ArrayList GetMendetoryFields(string tab)
	{
        sql = "select column_name as col from information_schema.columns where lower(table_name)='" + tab.ToLower() + "' and lower(is_Nullable)='no'";
		DataTable dt = dbHandler.GetDataTable(sql);
		ArrayList array = new ArrayList();
		foreach (DataRow dr in dt.Rows)
		{
			array.Add(dr["col"].ToString());
		}
		return array;
	}
}
