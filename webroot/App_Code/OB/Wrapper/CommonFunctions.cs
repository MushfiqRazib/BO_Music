using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CommonFunctions
/// </summary>
namespace HIT.OB.STD.Wrapper
{
    public class CommonFunctions
    {
        public CommonFunctions()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static string GetCaption(string field, string[] capList)
        {
            foreach(string fieldCapDef in capList)
            {
                string[] capdefParts = fieldCapDef.Split('=');
                if (field.ToLower().Equals(capdefParts[0].ToLower()))
                {
                    return capdefParts[1];   
                }   
            }
            return field;
        }

        

        public static string GetDocBasePath(string constantName)
        {
            return HIT.OB.STD.Wrapper.DAL.ConfigManager.GetDocBasePath();

            //*** Following section were for .Net
            //string line, basePath = string.Empty;
            //string configPath = AppDomain.CurrentDomain.BaseDirectory + "\\Components\\config.asp";
            //constantName = constantName.ToLower();
            ////string configPath = @"E:\Projecten\ID01\ID01_Remote\trunk\www\ID01\" + "\\config.php";

            //// Read the file and retrieve the connection string
            //using (System.IO.StreamReader file = new System.IO.StreamReader(configPath))
            //{
            //    while ((line = file.ReadLine().ToLower()) != null)
            //    {
            //        if (line.StartsWith("const") && line.IndexOf(constantName)>0)
            //        {
            //            LogWriter.WriteLog("line:"+line);
            //            System.Text.RegularExpressions.Regex rsRegEx = new System.Text.RegularExpressions.Regex(@"\s+");
            //            basePath = line.Replace("const", "").Replace("=", "").Replace(constantName, "").Replace("\"","").Trim();                                          
            //            break;
            //        }
            //    }
            //}
            
        }

    }


     
}
