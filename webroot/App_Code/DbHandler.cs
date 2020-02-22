using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;


using Npgsql;

/// <summary>
/// Summary description for DbHandler
/// </summary>
public class DbHandler
{
    NpgsqlConnection conn;
	public DbHandler()
	{
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        conn = new NpgsqlConnection(conStr);
	}
    public DbHandler(string connectionString)
    {
        conn = new NpgsqlConnection(connectionString);
    }

    /// <summary>
    /// This method help to fill the given dataset with a given tablename
    /// It can fill the same dataset with a different tablename
    /// </summary>
    /// <param name="sqlQuery">Supplied Npgsql query</param>
    /// <param name="ds">Supplied Dataset</param>
    /// <param name="tblName">Supplied Tablename</param>
    /// <returns></returns>
    public DataTable GetDataTable(string sqlQuery)
    {
        DataTable datatable = new DataTable();
//        NpgsqlConnection objCon = conn;
        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
        try
        {
            adapter.SelectCommand = new NpgsqlCommand(sqlQuery, conn);
            adapter.Fill(datatable);
        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
            throw new HttpException("SQL:" + sqlQuery + " Error found" + ex.ToString());
        }
        return datatable;
    }
	/// <summary>
	/// This function will return a DataTable for a given NpgsqlCommand
	/// </summary>
	/// <param name="command">Supplied NpgsqlCommand command</param>
	/// <returns> DataTable</returns>
	public DataTable GetDataTable(NpgsqlCommand command)
	{
		DataTable dataTable=new DataTable();
		NpgsqlDataAdapter adapter=new NpgsqlDataAdapter();
		try
		{
			command.Connection=conn;
			adapter.SelectCommand=command;
			adapter.Fill(dataTable);
		}
		catch(Exception ex)
		{
            Boeijenga.Common.Utils.LogWriter.Log(ex);
            throw new HttpException("Npgsql:" + command.ToString() + " Error found" + ex.ToString());
		}

		return dataTable;
		
		
	}

    public bool HasRecord(DataTable dt)
    {
        return dt.Rows.Count > 0;
    }

    #region Execute Query
    //test method execute query
    public bool ExecuteQuery(string query) //query for subscription email
    {
        bool status = false;
        try
        {
            conn.Open();// open the connection  
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            cmd.ExecuteNonQuery(); //execute query
            status= true;
        }
        catch (NpgsqlException e)
        {
            status= false;
        }
        finally
        {
            conn.Close(); //closing connection
        }
        return status;
        
    }
    /// <summary>
    /// This method should Execute insert, update, delete query
    /// if the query execute successfully, then it will retrun true otherwise false
    /// </summary>
    /// <param name="command">npgsql command with command text</param>
    /// <returns>true/false</returns>
    public bool ExecuteQuery(NpgsqlCommand command) //query for subscription email
    {
        bool status = false;
        try
        {
            conn.Open();                // open the connection  
            command.Connection = conn;
            command.ExecuteNonQuery(); //execute query
            status = true;
        }
        catch (NpgsqlException e)
        {
            status = false;
        }
        finally
        {
            conn.Close(); //closing connection
        }
        return status;

    }
    public bool ExecuteQuery(NpgsqlCommand command, NpgsqlConnection conn) //query for subscription email
    {
        bool status = false;
        try
        {
            command.Connection = conn;
            command.ExecuteNonQuery(); //execute query
            status = true;
        }
        catch (NpgsqlException e)
        {
            status = false;
        }
        return status;

    }
    /// <summary>
	///This method should Execute insert, update, delete query
    /// if the query execute successfully, then it will retrun true otherwise false with ref error msg
	/// </summary>
	/// <param name="command"></param>
	/// <param name="msg"></param>
	/// <returns></returns>
	public bool ExecuteQuery(NpgsqlCommand command, ref string msg) //query for subscription email
	{
		bool status = false;
		try
		{
			conn.Open();                // open the connection  
			command.Connection = conn;
			command.ExecuteNonQuery(); //execute query
			status = true;
		}
		catch (NpgsqlException e)
		{
			msg = "ERROR: " + e.Code + "<br>" + "ERROR Message: " + e.Message;
			status = false;
		}
		finally
		{
			conn.Close(); //closing connection
		}
		return status;

	}
    #endregion

    #region Execute Transaction
    /// <summary>
    /// This routine executes a series of queries using NpgSQL Transaction.
    /// if the transaction executes successfully it will COMMIT
    /// otherwise Transaction will be RollBack.
    /// </summary>
    /// <param name="queries">Array of query</param>
    /// <param name="conn">Mysql Connection</param>
    public bool ExecuteTransaction(ArrayList queries, ref string msg)
    {
        NpgsqlTransaction transaction = null;
        bool flag = true;
        try
        {
            conn.Open();
            transaction = conn.BeginTransaction();
            for (int i = 0; i < queries.Count; i++)
            {
                new NpgsqlCommand(queries[i].ToString(), conn, transaction).ExecuteNonQuery();
            }
            transaction.Commit();
        }
        catch (NpgsqlException ex)
        {
            msg = ex.Message;
            transaction.Rollback();
            flag = false;
        }
        finally
        {
            conn.Close();
        }
        return flag;
    }
    /// <summary>
    /// This routine executes a series of queries using NpgSQL Transaction.
    /// if the transaction executes successfully it will COMMIT
    /// otherwise Transaction will be RollBack.
    /// </summary>
    /// <param name="queries">Array of query</param>
    /// <param name="conn">Mysql Connection</param>
    public bool ExecuteTransaction(NpgsqlCommand []commands, ref string msg)
    {
        NpgsqlTransaction transaction = null;
        bool flag = true;
        try
        {
            conn.Open();
            transaction = conn.BeginTransaction();
            foreach (NpgsqlCommand command in commands)
            {
                command.Connection = conn;
                command.Transaction = transaction;
                command.ExecuteNonQuery();
            }
            transaction.Commit();
        }
        catch (NpgsqlException ex)
        {
            msg = "ERROR: " + ex.Code + "<br>" + "ERROR Message: " + ex.Message;
            transaction.Rollback();
            flag = false;
        }
        finally
        {
            conn.Close();
        }
        return flag;
    }

    public void ExecuteTransaction(NpgsqlCommand[] commands)
    {
        NpgsqlTransaction transaction = null;        
        try
        {
            conn.Open();
            transaction = conn.BeginTransaction();
            foreach (NpgsqlCommand command in commands)
            {
                command.Connection = conn;
                command.Transaction = transaction;
                command.ExecuteNonQuery();
            }
            transaction.Commit();
        }
        catch (NpgsqlException ex)
        {
            throw new Exception("ERROR: " + ex.Code + "<br>" + "ERROR Message: " + ex.Message);
            transaction.Rollback();            
        }
        finally
        {
            conn.Close();
        }        
    }

    public void ExecuteTransaction(ArrayList commands)
    {
        NpgsqlTransaction transaction = null;
        try
        {
            conn.Open();
            transaction = conn.BeginTransaction();
            foreach (NpgsqlCommand command in commands)
            {
                command.Connection = conn;
                command.Transaction = transaction;
                command.ExecuteNonQuery();
            }
            transaction.Commit();
        }
        catch (NpgsqlException ex)
        {
            throw new Exception("ERROR: " + ex.Code + "<br>" + "ERROR Message: " + ex.Message);
            transaction.Rollback();
        }
        finally
        {
            conn.Close();
        }
    }

    #endregion
}
