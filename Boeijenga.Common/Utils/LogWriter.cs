using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Boeijenga.Common.Utils
{
    public class LogWriter
    {
        public static void Log(Exception exception)
        {
            try
            {
                StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"log.txt", true);
                sw.WriteLine("At {0} \n\tError Message :{1}\n\tStackTrace: {2}",
                    DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), exception.Message, exception.StackTrace);
                sw.Close();
            }
            catch
            {
                throw new Exception("File not found: " + System.AppDomain.CurrentDomain.BaseDirectory + @"log.txt");
            }
        }

        public static void Log(string message)
        {
            try
            {
                StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"log.txt", true);
                sw.WriteLine("At {0} message :{1}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), message);
                sw.Close();
            }
            catch
            {
                //throw new HttpException("File not found: " + System.AppDomain.CurrentDomain.BaseDirectory + @"Eventlog.txt");
            }
        }
    }
}
