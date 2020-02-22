using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class MP3Player : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           // string to = HttpContext.Current.Server.MapPath("~/mp3");
            PseudoMP31.Src =  "Resources/audio/" + Request.QueryString.ToString() + "";
            PseudoMP31.Stream = false;
            PseudoMP31.AutoStart = false;
            PseudoMP31.Loop = false;
            PseudoMP31.Console = true;


           // dvPlayer.InnerHtml = "<pseudoengine:pseudomp3 id='PseudoMP31' runat='server' Src = 'Admin/" + Request.QueryString.ToString() + "' Stream = 'false' AutoStart = 'false' Loop = 'false' Console = 'true'> </pseudoengine:pseudomp3>";
        }
    }
}
