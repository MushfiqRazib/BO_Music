/*
 *Page:             contact.aspx (Bo01)
 *Author :          Abdullah Al Mohammad (Titu)
 *                  Software Engineer
 *                  ERP Systems Limited.
 *Createion Date:   11/09/2007
 * 
 */
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using System.Net.Mail;
using bo01;
using Boeijenga.Common.Objects;

public partial class contact : BasePage
{
    DbHandler handler = new DbHandler();
    ArrayList cartTable = new ArrayList();
    string cultureName = "";
    string headerText = "";
    string subject = "";
    string[] initialName = { "Mr.", "Mrs.", "Dhr.", "Mevr." };
    string[] subjectEN = { "Help", "Informationrequest", "Comments/Suggestions", "Other" };
    string[] subjectNL = { "Help", "Informatieaanvraag", "Opmerkingen/Suggesties", "Overig" };
    protected void Page_Load(object sender, EventArgs e)
    {
       // txtQuestion.Attributes.Add("onClick", "<script>StopPropogation(event, this);</script>");
        Master.MenuButton += new MasterPageMenuClickHandler(Master_MenuButton);
        Master.SetVisitPageList(Functions.GetPageName(Request.Url.ToString()));
        if (!IsPostBack)
        {
            LoadSubjectEN();
            SetCulture();
            checkUser();
            LoadCountryName();
            LoadInitialName();
        }
        //if (Session["order"] != null)
        //{
        //    cartTable = (ArrayList)Session["order"];
        //    if (cartTable.Count != 0)
        //    {
        //        Label cartitem = (Label)Master.FindControl("lblCartItem");
        //        cartitem.Text = "(" + Session["CartItems"].ToString() + ")";
        //    }
        //    else
        //    {
        //        Label cartitem = (Label)Master.FindControl("lblCartItem");
        //        cartitem.Text = "";
        //    }
        //}
        //subject = ddlSubject.SelectedItem.Text.ToString();
        SetCulturalValue();
        //the following code in this Page_Load method handles the Enter key event for text fields
        //txtAddress.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSend.ClientID + "')");
        //txtCity.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSend.ClientID + "')");
        //txtEmail.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSend.ClientID + "')");
        //txtName.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSend.ClientID + "')");
        //txtPhone.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSend.ClientID + "')");
        //txtPostCode.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSend.ClientID + "')");
        //txtQuestion.Attributes.Add("onkeypress", "return clickButton(event,'" + btnSend.ClientID + "')");
    }
    public void LoadInitialName()
    {
        //ddlInitialName.DataSource = initialName;
        //ddlInitialName.DataBind();
    }
    public void LoadSubjectEN()
    {
        //ddlSubject.DataSource = subjectEN;
        //ddlSubject.DataBind();
    }
    public void LoadSubjectNL()
    {
        //ddlSubject.DataSource = subjectNL;
        //ddlSubject.DataBind();
    }
    /// <summary>
    /// setting the cultural value
    /// </summary>
    private void SetCulturalValue()
    {
        valFName.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");

        valPostCode.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
        valLName.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");


        lblDFName.Text = (string)base.GetGlobalResourceObject("string", "lblFirstName");
        lblDMName.Text = (string)base.GetGlobalResourceObject("string", "lblMName");
        lblDLName.Text = (string)base.GetGlobalResourceObject("string", "lblLastName");


        lblMsg.Text = (string)base.GetGlobalResourceObject("string", "lblMsg");
        lblDCountry.Text = (string)base.GetGlobalResourceObject("string", "lblCountry");

        if (Session["cultureName"].ToString() == "en-US")
            LoadSubjectEN();
        else
            LoadSubjectNL();
    }
    /// <summary>
    /// setting the culture
    /// </summary>
    private void SetCulture()
    {
        if (Session["cultureName"] != null)
        {
            cultureName = Session["cultureName"].ToString();
        }
        else
        {
            cultureName = HttpContext.Current.Request.UserLanguages[0];
            Session["cultureName"] = cultureName;
        }
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);

    }

    void Master_MenuButton(object sender, EventArgs e)
    {
        Session["cultureName"] = Master.CurrentButton.ToString();
        SetCulture();
        SetCulturalValue();
    }

    public void checkUser()
    {
        if (Session["user"] != null)
        {
            User user = (User)Session["user"];
            headerText = "headerChangeProfile";
            //showFields(user.ID.ToString());
        }
        else
        {
            headerText = "headerRegister";
        }
    }

    /// <summary>
    /// method to load the country name to the dropdownlist
    /// <author>
    /// Abdullah Al Mohammad
    /// </author>
    /// <date>
    /// 11-12-2007
    /// </date>
    /// </summary>
    public void LoadCountryName()
    {
        string sqlQuery = "select countrycode, countryname from country order by countryname";
        DataTable dtCountry = handler.GetDataTable(sqlQuery);
        ddlCountry.DataSource = dtCountry;       
        ddlCountry.DataValueField = dtCountry.Columns["countrycode"].ToString();        
        ddlCountry.DataTextField = dtCountry.Columns["countryname"].ToString();        
        ddlCountry.DataBind();        
        ddlCountry.SelectedValue = "NL";
    }

    /// <summary>
    /// event method for send button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSend_Click(object sender, EventArgs e)
    {
        //subject = ddlSubject.SelectedItem.Text.ToString();
        if (Page.IsValid == true)
        {        
            ConstructMail();//construct and send mail
            Response.Redirect("confirmation.aspx?message=mailsend");//go to the confirmation page
        }
    }
    
    /// <summary>
    /// checks the required text fields are empty or not
    /// </summary>
    /// <returns></returns>
    //public bool EmptyFields()
    //{
    //    if (txtName.Text == "" || txtName.Text == null || txtEmail.Text == "" || txtEmail.Text == null ||
    //        txtPhone.Text == "" || txtPhone.Text == null || txtQuestion.Text == "" || txtQuestion.Text == null)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
    /// <summary>
    /// construct the mail
    /// </summary>
    private void ConstructMail()
    {
        string imagePath = System.Configuration.ConfigurationManager.AppSettings["web-graphics"].ToString();
        string headerPath = imagePath + "mail_header.gif";
        string footerPath = imagePath + "mail_footer.gif";
        string from = GetAddress();//getting the address of the client
        string addHeader = @"
            <style type='text/css'>
            <!--
            body {
            background-color: #e2e2e2;
            }
            body,td,th {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 11px;
            color: #000000;
            }
            -->
            </style></head>

            <body leftmargin='0' topmargin='0' rightmargin='0' bottommargin='0' marginwidth='0' marginheight='0'>
            <center>
            <br />
            <br />
            <table width='680' border='0'  cellspacing='0' cellpadding='0'>
            <tr>
            <td><img src='" + headerPath + @"' width='680' height='83' /></td>
            </tr>
            <tr>
            <td bgcolor='#FFFFFF' align='center' width='680'><table  width='680px' align='center' border='0' cellspacing='0' cellpadding='0'>
              <tr>
               <td align='center' valign='top' style='word-wrap: break-word;padding-left: 10px;padding-right: 10px;' >";

          string addFooter = @"</td>
            </tr>
            </table></td>
            </tr>
            <tr>
            <td><img src='" + footerPath + @"' width='680' height='29' /></td>
            </tr>
            </table></center>
            </body>";

          
          string body = "<table cellpadding=0 cellspacing=0 width='640px' align=\"center\" border='0'>" +
            "<tr><td></td></tr>"+
            "<tr>" +
            "<td width='100%' style='padding-left: 10px;padding-right: 10px;background-color:#DEDEDE;'  align=\"left\"><b>" + "From:" +
            "</b></td>" +
            "</tr>" +
            "<tr>" +
            "<td width='100%' align=\"center\">" +
            "</td>" +
            "</tr>" +
            "<tr>" +
            "<td width='100%' style='word-wrap: break-word;padding-left: 10px;padding-right: 10px;padding-top: 10px;' align=\"left\">" + from +
            "</td>" +
            "</tr>"+
             "<tr><td width='100%'></td></tr>" +
             "<tr>" +
            "<td width='100%' align=\"center\">" +
            "</td>" +
            "</tr>" +
            "<tr>" +
            "<td width='100%' align=\"center\" style='padding-top: 10px;'>&nbsp;" +
            "</td>" +
            "</tr>" +
            "<tr>" +
            "<td width='100%' style='word-wrap: break-word;padding-left: 10px;padding-right: 10px;background-color:#DEDEDE;'  align=\"left\"><b>" + "Message:" +
            "</b></td>" +
            "</tr>" +
            "<tr><td width='100%'>" +
            "</td>" +
            "</tr>" +
            "<tr>" +
            "<td width='630px' style='word-wrap: break-word;padding-left: 10px;padding-top: 10px;' align='left'>" + txtMessage.Text +
            "</td>" +
            "</tr>" +
            "</table>";

          string maintainEmail = "<table width='680px'  cellpadding=0 cellspacing=0  align = \"center\" border='0' bordercolor='red'>" +

                 "<tr><td width='100%' align='center' style='word-wrap: break-word'>" + body + "</td><tr>" +
                 "</table>";
          addHeader += body + addFooter;
          //Response.Write(addHeader);
          SendMail(addHeader);//send the mail       
    }
        
    /// <summary>
    /// getting address from the text fields
    /// </summary>
    /// <returns></returns>
    private string GetAddress()
    {
        string address = "";
        address += @"Name:           " +  txtDFName.Text +" " + txtDMName.Text + " " + txtDLName.Text + "<br>" +
                    "Company:        " + txtCompany.Text + "<br>" +
        //            "Address:        " + txtAddress.Text + "<br>" +
        //            "Postal Code:    " + txtPostCode.Text + "<br>" +
        //            "City:           " + txtCity.Text + "<br>" +
                    "Country:        " + ddlCountry.SelectedItem.Text + "<br>" +
                    "E-mail Address: " + txtEmail.Text + "<br>" +
                    "Phone number:   " + txtPhone.Text + "<br>";                    
        return address;
    }

    /// <summary>
    /// send the mail
    /// </summary>
    /// <param name="maintainEmail"></param>
    private void SendMail(string maintainEmail)
    {
        try
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(System.Configuration.ConfigurationManager.AppSettings["mail-server"]);
            System.Net.Mail.MailAddress fromAddr = new System.Net.Mail.MailAddress(txtEmail.Text);//new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["mail-company"]);
            System.Net.Mail.SmtpPermission sett = new System.Net.Mail.SmtpPermission(System.Security.Permissions.PermissionState.Unrestricted);
            System.Net.Mail.MailAddress toAddr = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["mail-from"]);//new System.Net.Mail.MailAddress(user.Email.ToString());
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(fromAddr, toAddr);
            ////MailAddress copy = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["mail-company"]);
            ////message.CC.Add(copy);
            message.Subject = subject;
            message.Body = maintainEmail;
            message.IsBodyHtml = true;
            client.Send(message);
        }
        catch (Exception ex) { throw new Exception("SMTP Server Error: " + ex.Message); }
    }
    protected void btnPreviewPdf_Click(object sender, ImageClickEventArgs e)
    {
        string pdfName = ConfigurationManager.AppSettings["web-resources"].ToString() + "pdf/route_NL_Oosterwolde.pdf";
        Response.Write("<script language = javascript> window.open(\"" + pdfName + "\",\"\",\"resizable=yes,status=no,toolbar=yes,menubar=yes,location=no\" )</script> ");
    }
    protected void btnAssen_Click(object sender, ImageClickEventArgs e)
    {
        string pdfName = ConfigurationManager.AppSettings["web-resources"].ToString() + "pdf/route_NL_Assen.pdf";
        Response.Write("<script language = javascript> window.open(\"" + pdfName + "\",\"\",\"resizable=yes,status=no,toolbar=yes,menubar=yes,location=no\" )</script> ");
    }
    protected void btnLinkOW_Click(object sender, ImageClickEventArgs e)
    {
        Response.Write("<script language = javascript> window.open('http://maps.google.com/maps?f=d&hl=nl&geocode=&time=&date=&ttype=&saddr=Oosterwolde,+N381&daddr=Veenhuizen,+Hoofdweg+158&sll=37.0625,-95.677068&sspn=49.891082,82.265625&ie=UTF8&z=13&om=1')</script>");
        //Response.Redirect("http://maps.google.com/maps?f=d&hl=nl&geocode=&time=&date=&ttype=&saddr=Oosterwolde,+N381&daddr=Veenhuizen,+Hoofdweg+158&sll=37.0625,-95.677068&sspn=49.891082,82.265625&ie=UTF8&z=13&om=1");
    }
    protected void btnLinkAss_Click(object sender, ImageClickEventArgs e)
    {
        Response.Write("<script language = javascript> window.open('http://maps.google.com/maps?f=d&hl=nl&geocode=&time=&date=&ttype=&saddr=Assen,+a28&daddr=Veenhuizen,+Hoofdweg+158&sll=53006945,6.335835&sspn=0.07468,0.160675&ie=UTF8&ll=53.012201,6.458588&spn=0.074671,0.160675&z=13&om=1')</script>");
        //Response.Redirect("http://maps.google.com/maps?f=d&hl=nl&geocode=&time=&date=&ttype=&saddr=Assen,+a28&daddr=Veenhuizen,+Hoofdweg+158&sll=53006945,6.335835&sspn=0.07468,0.160675&ie=UTF8&ll=53.012201,6.458588&spn=0.074671,0.160675&z=13&om=1");
    }
    protected void btnContact_Click(object sender, EventArgs e)
    {
        Response.Redirect("contact.aspx");
    }
}
