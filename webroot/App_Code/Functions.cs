using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;
using System.Web.Mail;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Drawing;
using Boeijenga.Business;
using Boeijenga.Common.Objects;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.Net;

/// <summary>
/// Summary description for Functions
/// </summary>
public static class Functions
{
   
    public static Hashtable pageTable = new Hashtable(); // will be stored the page name and their representing name. 
    public static string selectedColor = "#C0C0FF";
    public static string normalColor = "white";
    public static string alternateColor = "#EFEFEF";
     static Functions()
	{
		//
		// TODO: Add constructor logic here
		//
         SetPageTable();
    }

     public static string AsCommaSeparatedSqlString(string articleCode)
     {
         string[] articleArr = articleCode.Split(',');

         for (int i = 0; i < articleArr.Length; i++)
         {
             string code = articleArr[i];
             code = "'" + code + "'";
             articleArr[i] = code;

         }


         articleCode = string.Join(",", articleArr);
         return articleCode;
     }

     public static string GetCultureStr(string cultureName)
     {
         return cultureName.ToString().Substring(0, 2);
     }

    /// <summary>
    /// code@provas 27-Mar-09
    /// This method should be returns all files in the specified directory
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetFiles(string directory)
    {
        return (IEnumerable<string>)Directory.GetFiles(directory);
    }

    public static string AddSlashes(string InputTxt)
    {
        // List of characters handled:
        // \000 null
        // \010 backspace
        // \011 horizontal tab
        // \012 new line
        // \015 carriage return
        // \032 substitute
        // \042 double quote
        // \047 single quote
        // \134 backslash
        // \140 grave accent

        string Result = InputTxt;

        try
        {
            Result = System.Text.RegularExpressions.Regex.Replace(InputTxt, @"[\000\010\011\012\015\032\042\047\134\140]", "\\$0");
        }
        catch (Exception Ex)
        {
            // handle any exception here
            Console.WriteLine(Ex.Message);
        }

        return Result;
    }

    /// <summary>
    /// code@provas 27-Mar-09
    /// This method should be returns name of all child directory in the specified path
    /// </summary>
    /// <param name="dirSource"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetDirectories(string dirSource)
    {
        return (IEnumerable<string>)Directory.GetDirectories(dirSource, "*", System.IO.SearchOption.AllDirectories);
    }
    /// <summary>
    /// code@provas 27-Mar-09
    /// [sbcd]\d{6}[.](?:png|gif|jpg|ico|jpeg|bmp|mp3|pdf means
    /// any charecter in [sbcd]
    /// 6 digit
    /// followed by dot (.)
    /// followed by any charectset between (png,gif,jpg,ico,jpeg,bmp,mp3,pdf)
    /// for example, s986484.jpg is valid
    /// </summary>
    /// <param name="fullName"></param>
    /// <returns></returns>
    public static string GetFileName(string fullName)
    {
        string fileName = fullName.Substring(fullName.LastIndexOf(@"\") + 1);
        return Regex.Match(fileName, @"[sbcd]\d{6}[.](?:png|gif|jpg|ico|jpeg|bmp|mp3|pdf)", RegexOptions.IgnoreCase).Value;
    }
    /// <summary>
    /// code@provas 27-Mar-09
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetArticleCode(string fileName)
    {
        return Regex.Match(fileName, @"[sbcd]\d{6}", RegexOptions.IgnoreCase).Value;
    }

    #region GetArticleTypeImage
    /// <summary>
    /// This method will return the imagepath taking article type
    /// </summary>
    /// <param name="articleType">Article Type</param>
    /// <returns>Image path</returns>
    public static string GetArticleTypeImage(string articleType,string imagePath)
    {
        switch (articleType.ToUpper())
        {
            case "B": return "<img src='" + imagePath + "Book.png'/>";
			case "S": return "<img src='" + imagePath + "sheetmusic.png'/>";
			case "C": return "<img src='" + imagePath + "CD.png'/>";
            default: return "";
        }
    }
    public static string GetArticleTypeImage(string articleType)
    {
        string imagePath = System.Configuration.ConfigurationManager.AppSettings["web-graphics"].ToString();
        switch (articleType.ToUpper())
        {

            case "B": return "<img src='" + imagePath + "Book.png'/>";
            case "S": return "<img src='" + imagePath + "sheetmusic.png'/>";
            case "C": return "<img src='" + imagePath + "CD.png'/>";
            default: return "";
        }
    }
    #endregion

    #region File Exist
    /// <summary>
    /// This method will return true if a file is exist
    /// otherwise false
    /// </summary>
    /// <param name="filename">full file name</param>
    /// <returns>exist status</returns>
    public static bool FileExist(string filename)
    {
        FileInfo fi = new FileInfo(filename);
        return fi.Exists;
    }
    #endregion

    #region IsImageFile
    /// <summary>
    /// This method will return true if the type is image
    /// otherwise false
    /// </summary>
    /// <param name="filename">full file name</param>
    /// <returns>true/false</returns>
    public static bool IsImageFile(string filename)
    {
        string[] imageExtensions = { "png", "gif", "jpg", "ico", "jpeg","bmp" };
        string fileExtension = filename.Substring(filename.LastIndexOf('.')+1);

        foreach (string extension in imageExtensions)
        {
            if (extension.ToUpper().Equals(fileExtension.ToUpper()))
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsPdfFile(string filename)
    {

        string fileExtension = filename.Substring(filename.LastIndexOf('.') + 1);


        if ("pdf".ToUpper().Equals(fileExtension.ToUpper()))
        {
            return true;
        }
        else

            return false;
    }

    public static bool IsMusicFile(string filename)
    {
        string[] musicExtensions = { "mp3" };
        string fileExtension = filename.Substring(filename.LastIndexOf('.') + 1);

        foreach (string extension in musicExtensions)
        {
            if (extension.ToUpper().Equals(fileExtension.ToUpper()))
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    public static string getVisitedPage(ArrayList visitPageList)// for getting the vistied pages as string from visitpage list
    {
        int lastIndex = visitPageList.Count - 1;
        string visitedPage = "";
        for (int i = 0; i < visitPageList.Count; i++)
        {

            if (i != lastIndex)//this is for visited page 
            {

                visitedPage += pageTable[visitPageList[i].ToString().ToLower()] + " - ";
            }

        }

        return visitedPage;
    }


    public static string getActivePage(ArrayList visitPageList)// for getting the active page from visitpage list
    {
        
        return pageTable[visitPageList[visitPageList.Count - 1].ToString().ToLower()].ToString();
       
    }


    public static  ArrayList  initVisitPageList(ArrayList visitPageList, String pageName)// responsible for initialiazation of visitpagelist
    {
        pageName = pageName.ToLower();
        if (!visitPageList.Contains(pageName))
        {
            visitPageList.Add(pageName);
        }
        else
        {
            int pageIndex = visitPageList.IndexOf(pageName);
            if (pageIndex < visitPageList.Count - 1)
            {
                visitPageList.RemoveRange(pageIndex + 1, visitPageList.Count - (pageIndex+1));// if any page exist in the list after the current page then remove it.
            }
        }

        return visitPageList;
    }


    public static ArrayList GetOrderGridData(GridView gridView)
    {
        GridViewRowCollection rowCollection = gridView.Rows;
        ArrayList cartTable = new ArrayList(rowCollection.Count);
        for (int i = 0; i < rowCollection.Count; i++)
        {

            GridViewRow row = rowCollection[i];

            TextBox control = (TextBox)row.Cells[4].FindControl("intCtrQuanity");
            TextBox txtArticleCode = (TextBox)row.Cells[7].FindControl("txtArticleCode");
            String quantity = control.Text;

            quantity = quantity.Replace("-", "");
            if (quantity == null || quantity.Equals("") || quantity.Substring(0, 1).Equals("-") || int.Parse(quantity) <= 0 || quantity.Equals(""))
            {
                quantity = "1";
            }


            String articleCode = txtArticleCode.Text;
            Order order = new Order(articleCode, Convert.ToInt32(quantity));
            order = new Facade().LoadOrderInfo(articleCode, order);
            order.publisherName = new Facade().GetPublisherName(articleCode);
            cartTable.Add(order);


            // String temp= control.GetType().ToString();
        }
        return cartTable;
    }

    public static string GetPageName(string url)
    {
        int startIndex = url.LastIndexOf('/') + 1;
        int endIndex = url.LastIndexOf(".aspx") + 5;
        return url.Substring(startIndex, endIndex - startIndex);
    }

    public static string GetPageTitle(string url)
    {
        return pageTable[GetPageName(url)].ToString();

    }

    public static void SetPageTable()
    {
        pageTable.Add("home.aspx", "home");
        pageTable.Add("searchresult.aspx", "search result");
        pageTable.Add("details.aspx", "details");
        pageTable.Add("shoppingcart.aspx", "shoppingcart");
        pageTable.Add("delivery.aspx", "delivery");
        pageTable.Add("login.aspx", "login");
        pageTable.Add("signup.aspx", "signup");
        pageTable.Add("confirm.aspx", "confirm");
        pageTable.Add("confirmation.aspx", "confirmation");
        
        pageTable.Add("register.aspx", "register");
		pageTable.Add("newsdetails.aspx", "newsdetails");
		pageTable.Add("spotlightarchive.aspx", "spotlightarchive");
		pageTable.Add("advancesearch.aspx", "advancesearch");
        pageTable.Add("news.aspx", "news");
        pageTable.Add("about.aspx", "about us");
        pageTable.Add("contact.aspx", "contact");
        pageTable.Add("route.aspx", "route");


    }


    public static void BindDropDownList(DropDownList ddl, string[] text)
    {
        // Create new DataTable and DataSource objects.
        DataTable table = new DataTable();
        // Declare DataColumn and DataRow variables.
        DataRow row;

        // Create new DataColumn, set DataType, ColumnName and add to DataTable.  
        AddColumn(ref table, System.Type.GetType("System.Int32"), "key");
        AddColumn(ref table, System.Type.GetType("System.String"), "item");

        // Create new DataRow objects and add to DataTable.    
        for (int i = 0; i < text.Length; i++)
        {
            row = table.NewRow();
            row["key"] = i;
            row["item"] = text[i];
            table.Rows.Add(row);
        }

        ddl.DataSource = table;
        ddl.DataValueField = "key";
        ddl.DataTextField = "item";
        ddl.DataBind();
    }
    #region Add Column
    public static void AddColumn(ref DataTable table, System.Type t, string caption)
    {
        // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
        DataColumn column;
        column = new DataColumn();
        column.DataType = t;
        column.ColumnName = caption;
        table.Columns.Add(column);

    }
    #endregion

    #region Add column Header
    public  static void AddColumnHeader(ref DataTable table, string[] captions, System.Type[] types)
    {
        // Create new DataColumn, set DataType, ColumnName and add to DataTable. 
        for (int index = 0; index < captions.Length; index++)
        {
            AddColumn(ref table, types[index], captions[index]);
        }
    }
    #endregion

    #region SendMail
    /// <summary>
    /// This method will Send a mail taking a MailMessage object
    /// </summary>
    /// <param name="mail">MailMessage</param>
    public static void SendMail(MailMessage mail)
    {
        try
        {
            SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["mail-server"].ToString();
            SmtpMail.Send(mail);
           
//          SmtpMail.SmtpServer = "192.168.1.50";
        }
        catch (Exception ex)
        {
            throw new Exception("SMTP Server Error: " + ex.Message);
        }

    }
    #endregion

    public static bool IsValidMail(string mailAddress)
    {
        string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        if (System.Text.RegularExpressions.Regex.IsMatch(mailAddress, pattern))
        {
            return true;
        }
        return false;
    }

    public static string GetArticleProperty(DataRow dr)
    {
        switch (dr["articletype"].ToString())
        {
            case "b":
            case "B":
                if (dr["pages"].ToString().Equals(""))
                    return dr["isbn13"].ToString();
                else
                    return dr["isbn13"].ToString() + " - " + dr["pages"].ToString() + " pagina's";
            case "c":
            case "C":
                if (dr["period"].ToString().Equals(""))
                    return dr["publicationno"].ToString();
                else
                    return dr["publicationno"].ToString() + " - " + dr["period"].ToString() + " sec";
            case "s":
            case "S":
                if (dr["duration"].ToString().Equals(""))
                    return dr["editionno"].ToString();
                else
                    return dr["editionno"].ToString() + " - " + dr["duration"].ToString() + " sec";
            default:
                return "";
        }
    }

    public static void ExportCSV(string filename, string query, string delemeter)
    {
        //CreateDirectory(GetDirectory(filename));
        StreamWriter sw = new StreamWriter(filename, false);

        // First we will write the headers.
        DbHandler handler = new DbHandler();
        DataTable dt =  handler.GetDataTable(query);
        int iColCount = dt.Columns.Count;
        for (int i = 0; i < iColCount; i++)
        {
            sw.Write(dt.Columns[i]);
            if (i < iColCount - 1)
            {
                sw.Write(delemeter);
            }
        }
        sw.Write(sw.NewLine);
        // Now write all the rows.            
        foreach (DataRow dr in dt.Rows)
        {
            for (int i = 0; i < iColCount; i++)
            {
                if (!Convert.IsDBNull(dr[i]))
                {
                    sw.Write(dr[i].ToString().Replace(delemeter, ".").Replace("\"", "\"\""));
                }
                if (i < iColCount - 1)
                {
                    sw.Write(delemeter);
                }
            }
            sw.Write(sw.NewLine);
        }
        sw.Close();
    }

    public static string ConvertAsciiToUtf8(string asciiString)
    {
        System.Text.Encoding ascii = System.Text.Encoding.ASCII;
        System.Text.Encoding utf8 = System.Text.Encoding.UTF8;

        byte[] asciiBytes = ascii.GetBytes(asciiString);
        byte[] utf8Bytes = System.Text.Encoding.Convert(ascii, utf8,
        asciiBytes);

        char[] utf8Chars = new char[utf8.GetCharCount(utf8Bytes, 0,
        utf8Bytes.Length)];
        utf8.GetChars(utf8Bytes, 0, utf8Bytes.Length, utf8Chars, 0);
        string utf8String = new string(utf8Chars);

        return utf8String;
    }

    public static Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
    {
        System.Drawing.Bitmap bmpOut = null;
        Bitmap loBMP = new Bitmap(lcFilename);
        try
        {
            int lnNewWidth = 0;
            int lnNewHeight = 0;

            ResizeImage(loBMP.Width, loBMP.Height, lnWidth, lnHeight, ref lnNewWidth, ref lnNewHeight);

            bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
            Graphics g = Graphics.FromImage(bmpOut);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
            g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);
        }
        catch
        {
            return null;
        }
        finally
        {
            loBMP.Dispose();
        }
        return bmpOut;
    }

    private static  void ResizeImage(int bmpWidth, int bmpHeight, int lnWidth, int lnHeight, ref int lnNewWidth, ref int lnNewHeight)
    {
        decimal lnRatio;
        // if image width & height is less than specified width & height
        if (bmpWidth <= lnWidth && bmpHeight <= lnHeight)
        {
            lnNewWidth = bmpWidth;
            lnNewHeight = bmpHeight;
        }
        //if image width is less and height is greater
        else if (bmpWidth <= lnWidth && bmpHeight >= lnHeight)
        {
            lnNewHeight = lnHeight;
            lnRatio = (decimal)bmpWidth / bmpHeight;
            decimal lnTemp = lnRatio * lnNewHeight;
            lnNewWidth = (int)lnTemp;
        }
        else // else do
        {
            lnNewWidth = lnWidth;
            lnRatio = (decimal)bmpHeight / bmpWidth;
            decimal lnTemp = lnRatio * lnNewWidth;
            lnNewHeight = (int)lnTemp;
        }

        // check for width and height resize yet??
        if (lnNewWidth > lnWidth || lnNewHeight > lnHeight)
        {
            ResizeImage(lnNewWidth, lnNewHeight, lnWidth, lnHeight, ref lnNewWidth, ref lnNewHeight);
        }
    }




    public static int getProductCount(ArrayList orderList)
    {
        return orderList.Count;
    }

    public static double GetProductToatlPrice(ArrayList orderList)
    {
        double retVal = 0;
        foreach(Order order in orderList)
        {
            retVal += order.vatIncludedPrice * order.quantity;


        }

        return retVal;
    }



    public static string[] ExtractMultisafepayPaymentUrl(Customer invoiceAddress,string total, string email, string gateway)
        {
            total = total.Replace(",", ".");
            MD5CryptoServiceProvider md5Hasher;
            Byte[] hashedBytes;
            UTF8Encoding encoder;
            string signature;
            string transId = Guid.NewGuid().ToString();
            string requestedUrl = string.Empty;

            string  gtotal = int.Parse(Math.Round(Convert.ToDouble(total) * 100).ToString()).ToString();
            //List<Address> userAddresses = DataAccessManager.GetAddress(userID.ToString());
            //Address invoiceAddress = (from address in userAddresses
            //                          where address.ID == addressID
            //                          select address).First();


            md5Hasher = new MD5CryptoServiceProvider();

            encoder = new UTF8Encoding();
            StringBuilder sBuilder = new StringBuilder();

            sBuilder.Append(gtotal.ToString());
            sBuilder.Append(ConfigurationManager.AppSettings["multipay-currency"].ToString());
            sBuilder.Append(ConfigurationManager.AppSettings["multipay-account"].ToString());
            sBuilder.Append(ConfigurationManager.AppSettings["multipay-site_id"].ToString());
            sBuilder.Append(transId);

            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(sBuilder.ToString()));

            signature = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            PaymentRequest paymentRequest = new PaymentRequest();

            paymentRequest.merchant.account = ConfigurationManager.AppSettings["multipay-account"].ToString();
            paymentRequest.merchant.cancel_url = "";//ConfigurationManager.AppSettings("max-allowable-download");
            paymentRequest.merchant.close_window = "";//ConfigurationManager.AppSettings("max-allowable-download");
            paymentRequest.merchant.notification_url = ConfigurationManager.AppSettings["multipay-notification_url"].ToString();
            paymentRequest.merchant.redirect_url = ConfigurationManager.AppSettings["redirect_url"].ToString();
            paymentRequest.merchant.site_id = ConfigurationManager.AppSettings["multipay-site_id"];
            paymentRequest.merchant.site_secure_code = ConfigurationManager.AppSettings["multipay-site_secure_code"];

            paymentRequest.customer.address1 = invoiceAddress.address1;
            paymentRequest.customer.address2 = "";
            paymentRequest.customer.city = invoiceAddress.city;
            paymentRequest.customer.Country = invoiceAddress.Country;
            paymentRequest.customer.email = email;
            paymentRequest.customer.firstname = invoiceAddress.firstname;
            paymentRequest.customer.forwardedip = "";
            paymentRequest.customer.HouseNumber = invoiceAddress.HouseNumber;
            paymentRequest.customer.ipaddress = "";
            paymentRequest.customer.lastname = invoiceAddress.lastname;
            paymentRequest.customer.locale = "en";
            paymentRequest.customer.phone = invoiceAddress.Telephone;
            paymentRequest.customer.state = "";
            paymentRequest.customer.ZipCode = invoiceAddress.ZipCode;

            paymentRequest.transaction.amount = gtotal.ToString();
            paymentRequest.transaction.currency = ConfigurationManager.AppSettings["multipay-currency"].ToString();
            paymentRequest.transaction.gateway = gateway;
            paymentRequest.transaction.daysactive = "";
            paymentRequest.transaction.description = "My shop";
            paymentRequest.transaction.id = transId;
            paymentRequest.transaction.items = "items";
            paymentRequest.transaction.manual = false;
            paymentRequest.transaction.var1 = "var1";
            paymentRequest.transaction.var2 = "var2";
            paymentRequest.transaction.var3 = "var3";

            paymentRequest.signature = signature;

            requestedUrl = RequestPaymentUrl(Functions.GenerateTransactionXML(paymentRequest), ConfigurationManager.AppSettings["multipay-api_url"].ToString());
            return new String[] { requestedUrl, transId };
        }

    public static string GenerateTransactionXML(PaymentRequest request)
    {
        StringBuilder requestXML = new StringBuilder();

        requestXML.Append(@"<?xml version=""1.0"" encoding=""utf-8""?>");
        requestXML.Append("<redirecttransaction>");
        requestXML.Append("<merchant>");
        requestXML.Append("<account>" + request.merchant.account + "</account>");
        requestXML.Append("<site_id>" + request.merchant.site_id + "</site_id>");
        requestXML.Append("<site_secure_code>" + request.merchant.site_secure_code + "</site_secure_code>");
        requestXML.Append("<notification_url>" + request.merchant.notification_url + "</notification_url>");
        requestXML.Append("<redirect_url>" + request.merchant.redirect_url + "</redirect_url>");
        requestXML.Append("<cancel_url>" + request.merchant.cancel_url + "</cancel_url>");
        requestXML.Append("<close_window>" + request.merchant.close_window + "</close_window>");
        requestXML.Append("</merchant>");
        requestXML.Append("<customer>");
        requestXML.Append("<locale>" + request.customer.locale + "</locale>");
        requestXML.Append("<ipaddress>" + request.customer.ipaddress + "</ipaddress>");
        requestXML.Append("<forwardedip>" + request.customer.forwardedip + "</forwardedip>");
        requestXML.Append("<firstname>" + request.customer.firstname + "</firstname>");
        requestXML.Append("<lastname>" + request.customer.lastname + "</lastname>");
        requestXML.Append("<address1>" + request.customer.address1 + "</address1>");
        requestXML.Append("<address2>" + request.customer.address2 + "</address2>");
        requestXML.Append("<housenumber>" + request.customer.HouseNumber + "</housenumber>");
        requestXML.Append("<zipcode>" + request.customer.ZipCode + "</zipcode>");
        requestXML.Append("<city>" + request.customer.city + "</city>");
        requestXML.Append("<state>" + request.customer.state + "</state>");
        requestXML.Append("<country>" + request.customer.Country + "</country>");
        requestXML.Append("<phone>" + request.customer.phone + "</phone>");
        requestXML.Append("<email>" + request.customer.email + "</email>");
        requestXML.Append("</customer>");
        requestXML.Append("<transaction>");
        requestXML.Append("<id>" + request.transaction.id + "</id>");
        requestXML.Append("<currency>" + request.transaction.currency + "</currency>");
        requestXML.Append("<amount>" + request.transaction.amount + "</amount>");
        requestXML.Append("<description>" + request.transaction.description + "</description>");
        requestXML.Append("<var1>" + request.transaction.var1 + "</var1>");
        requestXML.Append("<var2>" + request.transaction.var2 + "</var2>");
        requestXML.Append("<var3>" + request.transaction.var3 + "</var3>");
        requestXML.Append("<items>" + request.transaction.items + "</items>");
        requestXML.Append("<manual>" + request.transaction.manual + "</manual>");
        requestXML.Append("<gateway><id>" + request.transaction.gateway + "</id></gateway>");
        requestXML.Append("<daysactive>" + request.transaction.daysactive + "</daysactive>");
        requestXML.Append("</transaction>");
        requestXML.Append("<signature>" + request.signature + "</signature>");
        requestXML.Append("</redirecttransaction>");

        return requestXML.ToString();
    }

        private  static string RequestPaymentUrl(string requestXml, string apiUrl)
        {
            HttpWebRequest httpWebRequest;
            HttpWebResponse httpWebResponse;
            StreamWriter streamWriter;
            StreamReader streamReader;
            string stringResult;

            httpWebRequest = HttpWebRequest.Create(apiUrl) as HttpWebRequest;
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = requestXml.Length;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            

            streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            streamWriter.Write(requestXml);
            streamWriter.Close();

            httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
            streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            stringResult = streamReader.ReadToEnd();


            string xmlstring = stringResult;
            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.LoadXml(xmlstring);
            System.Xml.XmlNode DescriptionNode = xd.SelectSingleNode("/redirecttransaction/transaction/payment_url");
            streamReader.Close();
            return DescriptionNode.InnerText.ToString();
        }

       

        public static string GetPaymentStatus(string transID)
        {
            Merchant merchant = new Merchant();
            merchant.account = ConfigurationManager.AppSettings["multipay-account"];
            merchant.cancel_url = "";//ConfigurationManager.AppSettings("max-allowable-download");
            merchant.close_window = "";//ConfigurationManager.AppSettings("max-allowable-download");
            merchant.notification_url = ConfigurationManager.AppSettings["multipay-notification_url"];
            merchant.redirect_url = "";//ConfigurationManager.AppSettings("max-allowable-download");
            merchant.site_id = ConfigurationManager.AppSettings["multipay-site_id"];
            merchant.site_secure_code = ConfigurationManager.AppSettings["multipay-site_secure_code"];

            return RequestPaymentStatus(Functions.GenerateTransactionStatusXML(merchant, transID), ConfigurationManager.AppSettings["multipay-api_url"].ToString());
        }

        public static string GenerateTransactionStatusXML(Merchant merchant, string transID)
        {
            StringBuilder requestXML = new StringBuilder();

            requestXML.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            requestXML.Append("<status>");
            requestXML.Append("<merchant>");
            requestXML.Append("<account>" + merchant.account + "</account>");
            requestXML.Append("<site_id>" + merchant.site_id + "</site_id>");
            requestXML.Append("<site_secure_code>" + merchant.site_secure_code + "</site_secure_code>");
            requestXML.Append("</merchant>");
            requestXML.Append("<transaction>");
            requestXML.Append("<id>" + transID + "</id>");
            requestXML.Append("</transaction>");
            requestXML.Append("</status>");

            return requestXML.ToString();

        }


        public static string RequestPaymentStatus(string requestXml, string apiUrl)
        {
            HttpWebRequest httpWebRequest;
            HttpWebResponse httpWebResponse;
            StreamWriter streamWriter;
            StreamReader streamReader;
            string status = string.Empty;
            string stringResult;

            httpWebRequest = HttpWebRequest.Create(apiUrl) as HttpWebRequest;
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = requestXml.Length;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            streamWriter.Write(requestXml);
            streamWriter.Close();

            httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
            streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            stringResult = streamReader.ReadToEnd();


            string xmlstring = stringResult;
            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.LoadXml(xmlstring);
            if (xd.SelectSingleNode("status/error/code") != null)
            {
                status = GetTransactionErrorDescription(xd.SelectSingleNode("status/error/code").InnerText.ToString());
            }
            else
            {
                System.Xml.XmlNode DescriptionNode = xd.SelectSingleNode("/status/ewallet/status");
                status = DescriptionNode.InnerText.ToString();
            }
            streamReader.Close();
            return status;
        }


        public static string GetTransactionErrorDescription(string errorCode)
        {
            return GetTransactionErrorMessage(errorCode);
        }


        public static string GetTransactionErrorMessage(string errorCode)
        {
            string msg = string.Empty;

            switch (errorCode)
            {
                case "1000":
                    msg = "Invalid Berichttype onbekend";
                    break;
                case "1001":
                    msg = "Invalid amount";
                    break;
                case "1002":
                    msg = "Invalid currency";
                    break;
                case "1003":
                    msg = "Invalid merchant Account ID";
                    break;
                case "1004":
                    msg = "Invalid merchant Site ID";
                    break;
                case "1005":
                    msg = "Invalid merchant Site Security Code";
                    break;
                case "1006":
                    msg = "Invalid transaction ID";
                    break;
                case "1007":
                    msg = "Invalid IP-address";
                    break;
                case "1008":
                    msg = "Invalid description";
                    break;
                case "1010":
                    msg = "Invalid variable";
                    break;
                case "1011":
                    msg = "Invalid customer Account ID";
                    break;
                case "1012":
                    msg = "Invalid customer Security Code";
                    break;
                case "1013":
                    msg = "Invalid signature";
                    break;
                case "1014":
                    msg = "Unspecified error";
                    break;
                case "1015":
                    msg = "Unknown account";
                    break;
                case "1016":
                    msg = "Missing information";
                    break;
                case "1017":
                    msg = "Insufficient balance";
                    break;
                case "9999":
                    msg = "Unknown error";
                    break;
                default:
                    break;
            }
            return msg;
        }


        public static string ExtractPaypalPaymentUrl(string grandTotal)
        {
            string transId = Guid.NewGuid().ToString();
            string PayPalBaseUrl = ConfigurationManager.AppSettings["paypalurl"].ToString();
            string AccountEmail = ConfigurationManager.AppSettings["AccountEmail"].ToString();
            string BuyerEmail = ConfigurationManager.AppSettings["BuyerEmail"].ToString();
            string SuccessUrl = ConfigurationManager.AppSettings["SuccessUrl"].ToString();
            string CancelUrl = "";

            string LogoUrl = "";
            string ItemName = "items";
            string InvoiceNo = "";

            string Currencycode = ConfigurationManager.AppSettings["multipay-currency"].ToString();

            StringBuilder url = new StringBuilder();

            url.Append(PayPalBaseUrl + "?cmd=_xclick&business=" +
                System.Web.HttpUtility.UrlEncode(AccountEmail));

            if (BuyerEmail != null && BuyerEmail != "")
                url.AppendFormat("&email={0}", System.Web.HttpUtility.UrlEncode(BuyerEmail));

            if (Convert.ToDouble( grandTotal) != 0.0)
                url.AppendFormat("&amount={0:f2}", grandTotal.Replace(",","."));


            if (Currencycode != null && Currencycode != "")
                url.AppendFormat("&currency_code={0}", Currencycode);



            if (LogoUrl != null && LogoUrl != "")
                url.AppendFormat("&image_url={0}", System.Web.HttpUtility.UrlEncode(LogoUrl));

            if (ItemName != null && ItemName != "")
                url.AppendFormat("&item_name={0}", System.Web.HttpUtility.UrlEncode(ItemName));


            if (InvoiceNo != null && InvoiceNo != "")
                url.AppendFormat("&invoice={0}", System.Web.HttpUtility.UrlEncode(InvoiceNo));

            if (SuccessUrl != null && SuccessUrl != "")
                url.AppendFormat("&return={0}", System.Web.HttpUtility.UrlEncode(SuccessUrl));

            if (CancelUrl != null && CancelUrl != "")
                url.AppendFormat("&cancel_return={0}", System.Web.HttpUtility.UrlEncode(CancelUrl));

            return url.ToString(); ;

        }

        public static bool GetPaypalPaymentStatus(string txToken)
        {

           string  authToken = ConfigurationManager.AppSettings["PDTtoken"].ToString();


           string  query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, authToken);

            // Create the request back
           string url = ConfigurationManager.AppSettings["paypalurl"].ToString();

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            // Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;

            // Write the request back IPN strings
            StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            stOut.Write(query);
            stOut.Close();

            // Do the request to PayPal and get the response
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = stIn.ReadToEnd();
            stIn.Close();

            // If response was SUCCESS, parse response string and output details
            if (strResponse.StartsWith("SUCCESS"))
            {

                return true;

            }
            else
                return false;
        }

}



      