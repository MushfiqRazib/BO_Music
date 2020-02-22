using System;
using System.IO;

public class LogWriter
{
    static bool DEBUG = false;
    public static void WriteLog(string msg)
    {

        System.IO.StreamWriter sw = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"log.txt", true);
        sw.WriteLine("At {0} message :{1}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), msg);
        sw.Close();

    }
    public static void WriteLog(Exception exception)
    {
        System.IO.StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"log.txt", true);
        sw.WriteLine("At {0} \n\tError Message :{1}\n\tStackTrace: {2}",
            DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), exception.Message, exception.StackTrace);
        sw.Close();
    }

}