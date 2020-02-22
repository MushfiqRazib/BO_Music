using System;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;

namespace HawarIT.WebControls
{
	public enum FieldConversionType 
	{
		SQL_HTML,
		HTML_SQL
	}
	public  class FieldControlUtils
	{
		public static object GetFieldValue(string fieldvalue, string fieldtype, FieldConversionType conversion)
		{			
			if(fieldtype.ToUpper() == "STRING" )
			{
				if(conversion == FieldConversionType.HTML_SQL) 
				{
					return "'" + fieldvalue.Replace("'", "''")  + "'";
				}
				else if(conversion == FieldConversionType.SQL_HTML)
				{
					return fieldvalue;
				}
				else throw new Exception("conversion type not handled!" + conversion);
			}
			else if(fieldtype.ToUpper() == "INT" ||fieldtype.ToUpper() == "INTEGER")
			{
				if(conversion == FieldConversionType.HTML_SQL) 
				{
					if(fieldvalue == null || fieldvalue.Trim().Length == 0) 
					{
						return "null";
					}
					// replace strange seperators!
					fieldvalue = fieldvalue.Replace(",","");
					int i = int.Parse(fieldvalue);
					return i.ToString();
				}
				else if( conversion == FieldConversionType.SQL_HTML) 
				{
					if(fieldvalue == null || fieldvalue.Trim().Length == 0) 
					{
						return null;
					}
					// replace strange seperators!
					fieldvalue = fieldvalue.Replace(",","");
					int i = int.Parse(fieldvalue);
					return i;
				}
				else throw new Exception("conversion type not handled!" + conversion);
			}
			//Oracle fieldtype FLOAT
			else if(fieldtype.ToUpper()== "DECIMAL" || fieldtype.ToUpper()== "FLOAT")
			{
				if(conversion == FieldConversionType.HTML_SQL) 
				{
					if(fieldvalue == null || fieldvalue.Trim().Length == 0) 
					{
						return "null";
					}
					// replace strange seperators!
					fieldvalue = fieldvalue.Replace(",",".");
					double d = double.Parse(fieldvalue);
					
					return d.ToString();
				}
				else if( conversion == FieldConversionType.SQL_HTML) 
				{
					if(fieldvalue == null || fieldvalue.Trim().Length == 0) 
					{
						return null;
					}
					// replace strange seperators!
					fieldvalue = fieldvalue.Replace(",",".");
					double d = double.Parse(fieldvalue);
					
					return d;
				}
				else throw new Exception("conversion type not handled!" + conversion);
			}
				//TO:DO Write appropritate code for finance
			else if(fieldtype.ToUpper()  == "FINANCE")
			{
				if(conversion == FieldConversionType.HTML_SQL) 
				{
					if(fieldvalue == null || fieldvalue.Trim().Length == 0) 
					{
						return "null";
					}
					// replace strange seperators!
					fieldvalue = fieldvalue.Replace(",",".");
					double d = double.Parse(fieldvalue);
					
					return d.ToString();
				}
				else if( conversion == FieldConversionType.SQL_HTML) 
				{
					if(fieldvalue == null || fieldvalue.Trim().Length == 0) 
					{
						return null;
					}
					// replace strange seperators!
					fieldvalue = fieldvalue.Replace(",",".");
					double d = double.Parse(fieldvalue);
					
					return d;
				}
				else
				{
					throw new Exception("conversion type not handled!" + conversion);
				}
			
			}
			else if(fieldtype.ToUpper() == "DATE" )
			{
//				CultureInfo curci = new CultureInfo(CultureInfo.CurrentUICulture.Name);
			//	CultureInfo curci = new CultureInfo();
				
				if(conversion == FieldConversionType.HTML_SQL) 
				{
					if(fieldvalue == null || fieldvalue.Trim().Length == 0) 
					{
						return "null";
					}
					// dd-mm-yyyy 
					// mm/dd/yy
					fieldvalue = fieldvalue.Replace("-","/").Trim();
					// strip optional time arguments
					if(fieldvalue.IndexOf(" ") != -1) 
					{
						fieldvalue = fieldvalue.Substring(0, fieldvalue.IndexOf(" "));
					}
					int pos = fieldvalue.IndexOf("/");
					string day = fieldvalue.Substring(0, pos);
					fieldvalue = fieldvalue.Substring(pos + 1);
					pos = fieldvalue.IndexOf("/");
					string month = FindMonth(int.Parse(fieldvalue.Substring(0, pos)));
					string year = fieldvalue.Substring(pos + 1);
					return "'"+day+"-"+month+"-"+year+"'"; 

				}
					
				else if(conversion == FieldConversionType.SQL_HTML)
				{
					if(fieldvalue == null || fieldvalue.Trim().Length == 0) 
					{
						return null;
					}
						
					try 
					{
						fieldvalue = fieldvalue.Replace("-","/").Trim();
						int pos = fieldvalue.IndexOf("/");
						int day = int.Parse(fieldvalue.Substring(0, pos));
						fieldvalue = fieldvalue.Substring(pos + 1);

						pos = fieldvalue.IndexOf("/");
						int month = int.Parse(fieldvalue.Substring(0, pos));
						fieldvalue = fieldvalue.Substring(pos + 1);

						pos = fieldvalue.IndexOf(" ");
						int year = int.Parse(fieldvalue.Substring(0, pos));
						string strDate = "" + day + "-" + month + "-" + year;
						DateTime dt = DateTime.Parse(strDate);
						return dt;	
					}
					catch(Exception e) 
					{
						throw new Exception("Error while Converting Date. System Error:-" +e.Message);
						
					}
						
				}
				else throw new Exception("conversion type not handled!" + conversion);
			
			}

			else throw new Exception("fieldtype :"+ fieldtype.ToString()+ " not handled!" );
		}
		
        //public static string getDescription(string key, string fieldname,
        //                                    string lookupsql,string descriptionfield, 
        //                                    string connectionString) 
        //{
        //    if(key == null || lookupsql == null || fieldname == null || descriptionfield == null) 
        //    {
        //        return null;
        //    } 
        //    DataTable  dtDescription = new DataTable();
        //    lookupsql += " WHERE UPPER(" +fieldname+") =  '" + key.ToUpper()+ "'";
        //    OracleConnection connection = new OracleConnection(connectionString);
        //    OracleDataAdapter adapt = new OracleDataAdapter(lookupsql , connection);
        //    try
        //    {
        //        adapt.Fill(dtDescription);
        //    }
        //    catch (OracleException ox)
        //    {
        //        throw new HttpException(ox.ToString());
        //    }
        //    string strDescription = "";
        //    try
        //    {
        //        strDescription = (string) dtDescription.Rows[0][descriptionfield];
        //    }
        //    catch (Exception ex)
        //    {
        //        ex = ex;
        //        return null;
        //    }
			
        //    return strDescription;
				
        //}
		/// <summary>
		/// This routine helps to make a month value into its abbreviated String month value
		/// Therefore it should be used in sql for data manipulation in serted in oracle  insert
		/// </summary>
		/// <param name="mon">Sending month value</param>
		/// <returns>Abbreviated Oracle like String month</returns>
		public static string FindMonth(int mon)
		{
			string month="";
			switch (mon)
			{
				case 1: month	= "JAN";break;
				case 2: month	= "FEB";break;
				case 3: month	= "MAR";break;
				case 4: month	= "APR";break;
				case 5: month	= "MAY";break;
				case 6: month	= "JUN";break;
				case 7: month	= "JUL";break;
				case 8: month	= "AUG";break;
				case 9: month	= "SEP";break;
				case 10: month	= "OCT";break;
				case 11: month	= "NOV";break;
				case 12: month	= "DEC";break;
				default: throw new Exception("Invalid month value:");
			}
			return month;
		}
		/// <summary>
		/// An overloaded version of the GetDataSet function which takes
		/// the sql statement from the property of the control
		/// </summary>
		/// <returns>DataSet</returns>
        //public static DataTable GetDataTable(string myQuery)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        OracleConnection oracleConnection = new OracleConnection(System.Configuration.ConfigurationSettings.AppSettings["oracle-connection-string"]);
        //        OracleDataAdapter adapter = new OracleDataAdapter();
        //        adapter.SelectCommand = new OracleCommand(myQuery, oracleConnection);
        //        try
        //        {
        //            adapter.Fill(dt);
        //        }
        //        catch (OracleException ex) 
        //        {
        //            throw new HttpException("Error : " + ex.ToString());
        //        }
        //        finally
        //        {
        //            oracleConnection.Close();
        //            adapter.Dispose();
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new HttpException("SQL statement or Connection not VALID ! "  + ex.ToString());
        //    }
        //    return dt;
        //}
		
	}
}
