using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for OleDbDBHandler
/// </summary>
namespace HIT.PEditor.Wrapper
{
    public class OracleDBHandler : PEditorFunctionsManager
    {
        public OracleDBHandler()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void SaveData()
        {

            string fileLocation = ConfigurationManager.AppSettings["XmlFilePath"].ToString();
            string filePath = fileLocation + WrapperAppParams.FileName;
            Dictionary<string, string> fieldValDictionary = CommonFunctions.GetFieldValueList(filePath);

            StringBuilder sqlBuilder = new StringBuilder();
            string mapsetCode = string.Empty;
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection_Wrapper"].ToString();
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    //sql = @"select mapset_type from maps,mapset where maps.mapset_code = mapset.mapset_code and map_code like @mapCode";
                    sqlBuilder.AppendFormat(@"update {0} set ", WrapperAppParams.TableName);

                    foreach (KeyValuePair<string, string> kvp in fieldValDictionary)
                    {
                        sqlBuilder.AppendFormat(" \"{0}\" = '{1}', ", kvp.Key, kvp.Value);
                    }

                    sqlBuilder = sqlBuilder.Remove(sqlBuilder.ToString().LastIndexOf(','), 1);
                    sqlBuilder.AppendFormat(" where");
                    string[] pkFieldList = WrapperAppParams.FieldNames.Split(';');
                    string[] pkFieldValues = WrapperAppParams.FieldValues.Split(';');
                    for (int k = 0; k < pkFieldList.Length; k++)
                    {
                        sqlBuilder.AppendFormat(" \"{0}\"= '{1}' ", pkFieldList[k], pkFieldValues[k]);
                        if (k < pkFieldList.Length - 1)
                        {
                            sqlBuilder.Append(" AND ");
                        }
                    }

                    using (OleDbCommand cmd = new OleDbCommand(sqlBuilder.ToString(), connection))
                    {
                        cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void WritePropertyXML()
        {
            try
            {
                string logMsg = "";
                string query = "select * from " + WrapperAppParams.TableName + " where ";
                string[] pkFieldList = WrapperAppParams.FieldNames.Split(';');
                string[] pkFieldValues = WrapperAppParams.FieldValues.Split(';');
                for (int k = 0; k < pkFieldList.Length; k++)
                {
                    query += " \"" + pkFieldList[k] + "\"='" + pkFieldValues[k] + "'";
                    if (k < pkFieldList.Length - 1)
                    {
                        query += " AND ";
                    }
                }

                OleDbConnection dbConnection = new OleDbConnection();

                LogWriter.WriteLog(query);

                dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection_Wrapper"].ToString();
                dbConnection.Open();
                DataTable dtValues = GetDataTable(query, dbConnection);

                XmlDocument xml = new XmlDocument();

                XmlElement root = xml.CreateElement("CAD");
                xml.AppendChild(root);

                XmlComment comment = xml.CreateComment("field-value pair below");
                root.AppendChild(comment);
                XmlElement group = xml.CreateElement("group");
                group.InnerText = WrapperAppParams.GroupName;
                root.AppendChild(group);

                //ArrayList skipList = CommonFunctions.GetSkipFieldList();

                DataRow dtRow = dtValues.Rows[0];
                foreach (DataColumn col in dtValues.Columns)
                {
                    XmlElement field = xml.CreateElement("field");
                    XmlElement name = xml.CreateElement("name");
                    //if (!skipList.Contains(col.ColumnName.ToLower()))
                    //{
                    name.InnerText = col.ColumnName;
                    XmlElement value = xml.CreateElement("value");
                    if (col.DataType.Name == "DateTime")
                    {
                        string date = "";
                        try
                        {
                            date = Convert.ToDateTime(dtRow[col.ColumnName]).ToString("dd-MM-yyyy");
                        }
                        catch
                        {

                        }
                        finally
                        {
                            value.InnerText = date;
                        }
                    }
                    else
                    {
                        value.InnerText = dtRow[col.ColumnName].ToString();
                    }

                    field.AppendChild(name);
                    field.AppendChild(value);
                    root.AppendChild(field);
                    //}
                }

                LogWriter.WriteLog(xml.InnerXml);

                string fileLocation = HttpContext.Current.Server.MapPath("Output") + "\\";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileLocation + WrapperAppParams.FileName, false);

                sw.WriteLine(xml.InnerXml);
                sw.Close();

                dbConnection.Close();
                dtValues.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to Writexml: " + ex.StackTrace);
            }

        }

        public DataTable GetDataTable(string query, OleDbConnection dbConnection)
        {
            DataTable dataTable = new DataTable();
            try
            {
                LogWriter.WriteLog("query: " + query);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = new OleDbCommand(query, dbConnection);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw new Exception("From GetDataTable method:" + ex.Message);
            }
            return dataTable;
        }

        public ArrayList GetFieldList(string tableName, OleDbConnection dbConnection)
        {
            string query = "SELECT * FROM " + tableName + " WHERE 1 = 0";
            DataTable dt = GetDataTable(query, dbConnection);
            ArrayList fieldList = new ArrayList();
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                fieldList.Add(dt.Columns[k]);
            }
            return fieldList;
        }

    }
}
