using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Boeijenga.Business;
using Boeijenga.Common.Objects;

/// <summary>
/// Summary description for BoeijengaMusic
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class BoeijengaMusic : System.Web.Services.WebService
{

    public BoeijengaMusic()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }



    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = false)]
    
    public string TellFriend(string msg ,string senderEmail, string receiverEmail)
    {
        string succsessMsg = "Message sent.";

        System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
        mail.From = senderEmail;
        mail.To = receiverEmail;
        mail.Body = msg;
        mail.Subject = "Nice Article";

        try
        {

            Functions.SendMail(mail);
        }catch(Exception ex)
        {
            return "could not send message.";

        }

        return succsessMsg;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false)]

   public List<SearchKeyword> GetSearchKeywords(string searchText)
    {
        searchText = Functions.AddSlashes(searchText);
        SearchKeyWordMap keywords = new SearchKeyWordMap();

        if (Application["SEARHKEYWORDSLISTS"] != null)
        {
            keywords = Application["SEARHKEYWORDSLISTS"] as SearchKeyWordMap;

            if (keywords != null && keywords.IsKeyExists(searchText))
            {

                return keywords.GetKeyWords(searchText);

            }

        }

        List<SearchKeyword> searchKeywords = new Facade().GetSearchKeyWords(searchText);

        keywords.AddKeyWords(searchText, searchKeywords);
        Application["SEARHKEYWORDSLISTS"] = keywords;
        return searchKeywords;
    }

}

