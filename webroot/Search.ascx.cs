using System;

public partial class Search : System.Web.UI.UserControl
{   
  

   protected void Page_Load(object sender, EventArgs e)
   {
       if (!IsPostBack)
       {
           txtSearch.Attributes["title"] = (string)base.GetGlobalResourceObject("string", "keywords");
           lblSearch.Text = (string)base.GetGlobalResourceObject("string", "lblSearch") + " &raquo; ";
       }
   }
    
 
    protected void btnGo_Click(object sender,EventArgs e)
    {
        if (  txtSearch.Attributes["title"] == txtSearch.Text ||    txtSearch.Text!= string.Empty)
        {
            Response.Redirect("SearchResult.aspx?searchtext=" + txtSearch.Text);
        }    
       

    }
   
   
}
