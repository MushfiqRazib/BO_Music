using System;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (User.IsInRole("Admin")) 
        //{
        //    hdnRoleName.Value = "Admin";
        //}
        //else if (User.IsInRole("Sales"))
        //{
        //    hdnRoleName.Value = "Sales";
        //}
        //else if (User.IsInRole("POD partner"))
        //{
        //    hdnRoleName.Value = "POD partner";
        //}
        //else if (User.IsInRole("POD master"))
        //{
        //    hdnRoleName.Value = "POD master";
        //}
        //else if (User.IsInRole("Royalty owner"))
        //{
        //    hdnRoleName.Value = "Royalty owner";
        //}
        hdnRoleName.Value = "Admin";
       
    }
}
