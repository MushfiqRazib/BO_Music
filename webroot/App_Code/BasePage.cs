using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
	public BasePage()
	{
		//
		// TODO: Add constructor logic here
		//
        
        /*
         * code@provas on 01-03-2009
          Added Google Analytics javascript
         */
        Page.RegisterClientScriptBlock("googleAnalytics", @"         <script type=""text/javascript"">
            var gaJsHost = ((""https:"" == document.location.protocol) ? ""https://ssl."" : ""http://www."");
            document.write(unescape(""%3Cscript src='"" + gaJsHost + ""google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E""));
            </script>
            <script type=""text/javascript"">
            try {
            var pageTracker = _gat._getTracker(""UA-7658263-1"");
            pageTracker._trackPageview();
            } catch(err) {}
        </script>
");
	}



    protected void Page_Load(object sender, System.EventArgs e) 
    {
          
        
    }
}
