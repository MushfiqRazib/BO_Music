<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    //void Application_Error(object sender, EventArgs e) 
    //{ 
    //    // Code that runs when an unhandled error occurs

    //}
    void Application_Error(Object sender, EventArgs e)
    {

        HttpContext ctx = HttpContext.Current;

        Exception exception = ctx.Server.GetLastError();
        Application["UnhandledException"] = exception;
        string errorInfo = "<br>Offending URL: " + ctx.Request.Url.ToString() +
               "<br>Source: " + exception.Source +
               "<br>Message: " + exception.Message +
               "<br>Stack trace: " + exception.StackTrace;
        Boeijenga.Common.Utils.LogWriter.Log(errorInfo);

    }


    void Session_Start(object sender, EventArgs e) 
    {
        Session["cultureName"] = "nl-NL";

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
