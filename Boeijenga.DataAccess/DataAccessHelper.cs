using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
using System.Collections.Specialized;
using System.Collections;
using Boeijenga.DataAccess.Constants;
using Boeijenga.Common.Utils;

namespace Boeijenga.DataAccess
{
    public class DataAccessHelper
    {
        private static string CONNECTION_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        private static readonly DataAccessHelper _instance;
        private DataAccessHelper(){}

        public static DataAccessHelper GetInstance()
        {
            if (_instance == null)
            {
                return new DataAccessHelper();
            }
            return _instance;
        }

        public string ConnectionString
        {
            get { return CONNECTION_STRING; }
        }


        #region GetDataTable
        public DataTable GetDataTable(string query)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable resultset = new DataTable();
                        try
                        {
                            con.Open();
                            adapter.Fill(resultset);
                        }
                        catch (NpgsqlException ex)
                        {
                            Boeijenga.Common.Utils.LogWriter.Log(ex.Message + ". Query:" + ex.ErrorSql);
                            throw new Exception(ex.Message);
                        }
                        return resultset;
                    }
                }
            }
        }

        public DataTable GetDataTable(NpgsqlCommand command)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
            {
                command.Connection = con;
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                {
                    DataTable resultset = new DataTable();
                    try
                    {
                        con.Open();
                        adapter.Fill(resultset);
                    }
                    catch (NpgsqlException ex)
                    {
                        Boeijenga.Common.Utils.LogWriter.Log(ex.Message + ". Query:" + ex.ErrorSql);
                        throw new Exception(ex.Message);
                    }

                    return resultset;
                }
            }
        }

        /// <summary>
        /// This routine Executes a NpgsqlSelect query taking two parameter
        /// 1. Query String 2. Connection String and Returns a DataTable.
        /// </summary>
        /// <param name="myQuery">NpgsqlSelect query</param>
        /// <param name="myConnectionString">Connection string</param>
        /// <returns>Result DataTable</returns>
        public DataTable GetDataTable(string query, string connectionString)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable resultset = new DataTable();
                        try
                        {
                            con.Open();
                            adapter.Fill(resultset);
                        }
                        catch (NpgsqlException ex)
                        {
                            Boeijenga.Common.Utils.LogWriter.Log(ex.Message + ". Query:" + ex.ErrorSql);
                            throw new Exception(ex.Message);
                        }
                        return resultset;
                    }
                }
            }
        }
        #endregion

        #region GetDataset
        /// <summary>
        /// This routine Executes a NpgsqlSelect query taking QueryString
        /// and Returns a DataSet
        /// </summary>
        /// <param name="myQuery">Query String</param>
        /// <returns>Result DataSet</returns>
        public DataSet GetDataSet(string query)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        DataSet resultset = new DataSet();
                        try
                        {
                            con.Open();
                            adapter.Fill(resultset);
                        }
                        catch (NpgsqlException ex)
                        {
                            Boeijenga.Common.Utils.LogWriter.Log(ex.Message + ". Query:" + ex.ErrorSql);
                            throw new Exception(ex.Message);
                        }
                        return resultset;
                    }
                }
            }
        }

        public DataSet GetDataSet(string[] queries, string[] tableName)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
            {
                con.Open();
                DataSet resultset = new DataSet();
                for (int i = 0; i < queries.Length; i++)
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queries[i], con))
                    {
                        try
                        {
                            adapter.Fill(resultset, tableName[i]);
                        }
                        catch (NpgsqlException ex)
                        {
                            Boeijenga.Common.Utils.LogWriter.Log(ex.Message + ". Query:" + ex.ErrorSql);
                            throw new Exception(ex.Message);
                        }
                    }
                }
                return resultset;
            }
        }
        public DataSet GetDataSet(NpgsqlCommand[] commands, string[] tableName)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
            {
                con.Open();
                DataSet resultset = new DataSet();
                for (int i = 0; i < commands.Length; i++)
                {
                    commands[i].Connection = con;
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(commands[i]))
                    {
                        try
                        {
                            adapter.Fill(resultset, tableName[i]);
                        }
                        catch (NpgsqlException ex)
                        {
                            Boeijenga.Common.Utils.LogWriter.Log(ex.Message + ". Query:" + ex.ErrorSql);
                            throw new Exception(ex.Message);
                        }
                    }
                }
                return resultset;
            }
        }

        /// <summary>
        /// This routine Executes a NpgsqlSelect query taking two parameter
        /// 1. Query String 2. Connection String and Returns a DataSet.
        /// </summary>
        /// <param name="myQuery">NpgsqlSelect query</param>
        /// <param name="myConnectionString">Connection string</param>
        /// <returns>Result DataSet</returns>
        public DataSet GetDataSet(string query, string connectionString)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        DataSet resultset = new DataSet();
                        try
                        {
                            con.Open();
                            adapter.Fill(resultset);
                        }
                        catch (NpgsqlException ex)
                        {
                            Boeijenga.Common.Utils.LogWriter.Log(ex.Message + ". Query:" + ex.ErrorSql);
                            throw new Exception(ex.Message);
                        }
                        return resultset;
                    }
                }
            }
        }
        #endregion

        #region Execute Query
        /// <summary>
        /// This routine Executes UPDATE and INSERT query taking QueryString
        /// </summary>
        /// <param name="myQuery">Query String</param>
        public void ExecuteQuery(string query)
        {
           
            using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (NpgsqlException ex)
                    {
                       
                        Boeijenga.Common.Utils.LogWriter.Log(ex.Message + ". Query:" + ex.ErrorSql);
                        throw new Exception(ex.Message);
                    }
                    
                }
            }
        }

       
        /// <summary>
        /// This routine Executes UPDATE and INSERT query taking NpgsqlCommand
        /// </summary>
        /// <param name="myQuery">Query String</param>
        public void ExecuteQuery(NpgsqlCommand command)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
            {
                command.Connection = con;
                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (NpgsqlException ex)
                {
                    string values = "";
                    foreach (NpgsqlParameter param in command.Parameters)
                    {
                        values += param.Value + ",";
                    }
                    Boeijenga.Common.Utils.LogWriter.Log(ex.Message + ". Query:" + ex.ErrorSql);
                    throw new Exception(ex.Code);
                }
            }
        }


        public bool ExecuteQuery(NpgsqlCommand command, ref string msg)
        {
            bool flag = true;
            using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
            {
                command.Connection = con;
                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    flag = true;
                }
                catch (NpgsqlException ex)
                {
                    flag = false;
                    string values = "";
                    foreach (NpgsqlParameter param in command.Parameters)
                    {
                        values += param.Value + ",";
                    }
                    msg = ex.Message;
                    Boeijenga.Common.Utils.LogWriter.Log(ex.Message + ". Query:" + ex.ErrorSql);
                    throw new Exception(ex.Code);
                }
                return flag;
            }
        }

        /// <summary>
        /// This routine Executes UPDATE and INSERT query taking two parameter
        /// 1. Query String 2. Connection String and Returns a DataSet.
        /// </summary>
        /// <param name="myQuery">NpgsqlSelect query</param>
        /// <param name="myConnectionString">Connection string</param>
        public void ExecuteQuery(string query, string connectionString)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (NpgsqlException ex)
                    {
                        Boeijenga.Common.Utils.LogWriter.Log(ex.Message + ". Query:" + ex.ErrorSql);
                        throw new Exception(ex.Message);
                    }
                }
            }
        }


        //public bool ExecuteQuery(NpgsqlCommand command, ref string msg) 
        //{
        //    bool status = false;
        //    using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
        //    {
        //        command.Connection = con;
        //        try
        //        {
        //            con.Open();                // open the connection  
        //            command.ExecuteNonQuery(); //execute query
        //            status = true;
        //        }
        //        catch (NpgsqlException e)
        //        {
        //            msg = "ERROR: " + e.Code + "<br>" + "ERROR Message: " + e.Message;
        //            status = false;
        //        }

        //    }
           
        //    return status;

        //}


        public object ExecuteScaler(NpgsqlCommand command)
        {

            using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
            {
                object objReturnVal = null;
                command.Connection = con;
                try
                {
                    con.Open();
                    objReturnVal = command.ExecuteNonQuery();
                }
                catch (NpgsqlException ex)
                {
                    Boeijenga.Common.Utils.LogWriter.Log(ex.Message + ". Query:" + ex.ErrorSql);
                    throw new Exception(ex.Message);
                }
                return objReturnVal;
            }
        }
        #endregion

        #region Execute Transaction
        /// <summary>
        /// This routine executes a series of queries using Mysql Transaction.
        /// if the transaction executes successfully it will COMMIT
        /// otherwise Transaction will be RollBack.
        /// </summary>
        /// <param name="queries">Array of query</param>
        /// <param name="conn">Mysql Connection</param>
        public bool ExecuteTransaction(ArrayList commands, ref string msg)
        {
            bool flag = true;
            NpgsqlTransaction transaction = null;
            using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    transaction = con.BeginTransaction();
                    foreach (NpgsqlCommand command in commands)
                    {
                        try
                        {
                            command.Connection = con;
                            command.ExecuteNonQuery();
                        }
                        catch (NpgsqlException ex)
                        {
                            Boeijenga.Common.Utils.LogWriter.Log(ex.Message + ". Query:" + ex.ErrorSql);
                            throw new Exception(ex.Message);
                        }
                    }
                    transaction.Commit();
                    con.Close();
                }
                catch (NpgsqlException ex)
                {
                    msg = "ERROR: " + ex.Code + "<br>" + "ERROR Message: " + ex.Message;
                    Boeijenga.Common.Utils.LogWriter.Log(msg);
                    transaction.Rollback();
                    flag = false;
                    throw new Exception(ex.Message);
                }
                return flag;
            }


        }
        /// <summary>
        ///if the transaction executes successfully it will COMMIT
        /// otherwise Transaction will be RollBack.
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool ExecuteTransaction(NpgsqlCommand[] commands, ref string msg)
        {
            bool flag = true;
            NpgsqlTransaction transaction = null;
            using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    transaction = con.BeginTransaction();
                    foreach (NpgsqlCommand command in commands)
                    {
                        try
                        {
                            command.Connection = con;
                            command.ExecuteNonQuery();
                        }
                        catch (NpgsqlException ex)
                        {
                            Boeijenga.Common.Utils.LogWriter.Log(ex.Message + ". Query:" + ex.ErrorSql);
                            throw new Exception(ex.Message);
                        }
                    }
                    transaction.Commit();
                    con.Close();
                }
                catch (NpgsqlException ex)
                {
                    msg = "ERROR: " + ex.Code + "<br>" + "ERROR Message: " + ex.Message;
                    Boeijenga.Common.Utils.LogWriter.Log(msg);
                    transaction.Rollback();
                    flag = false;
                    throw new Exception(ex.Message);
                }
                return flag;
            }
        }

        public bool BatchUpdate(DataTable[] Tables, NpgsqlCommand[] commands, ref string msg)
        {
            bool flag = true;
            NpgsqlTransaction transaction = null;
            using (NpgsqlConnection con = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    transaction = con.BeginTransaction();
                    for (int i = 0; i < commands.Length; i++)
                    {
                        try
                        {
                            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                            {
                                commands[i].Connection = con;
                                adapter.UpdateCommand = commands[i];
                                adapter.Update(Tables[i]);
                            }
                        }
                        catch (NpgsqlException ex)
                        {
                            Boeijenga.Common.Utils.LogWriter.Log(ex.Message + ". Query:" + ex.ErrorSql);
                            throw new Exception(ex.Message);
                        }
                    }
                    transaction.Commit();
                    con.Close();
                }
                catch (NpgsqlException ex)
                {
                    msg = "ERROR: " + ex.Code + "<br>" + "ERROR Message: " + ex.Message;
                    Boeijenga.Common.Utils.LogWriter.Log(msg);
                    transaction.Rollback();
                    flag = false;
                    throw new Exception(ex.Message);
                }
                return flag;
            }
        }
        #endregion


        public StringDictionary GetLangDic(string lang)
        {

            StringDictionary langDic = new StringDictionary();

            using (NpgsqlConnection con = new NpgsqlConnection(CONNECTION_STRING))
            {
                string query = string.Format(SQLConstants.SELECT_LANG, lang);
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        NpgsqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string key = reader.GetString(0);
                            string value = reader.GetString(1);

                            langDic.Add(key, value);

                        }
                    }
                    catch (Exception exp)
                    {
                        LogWriter.Log(exp);
                        throw new Exception(exp.Message);
                    }
                }

            }

            return langDic;
        }
        private string ConvertArrayToCommaSeperatedString(string[] ids)
        {
            string result = string.Empty;

            for (int i = 0; i < ids.Length; i++)
            {
                if (i > 0)
                {
                    result += string.Format(", '{0}'", ids[i]);
                }
                else
                {
                    result += string.Format("'{0}'", ids[i]);
                }
            }

            return result;
        }
    }
}
