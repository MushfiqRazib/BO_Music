using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Boeijenga.Business;
using System.Collections.Generic;
using Boeijenga.Common.Objects;
using System.Globalization;
using System.IO;
using System.Diagnostics;



public partial class Admin_Article : System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        //Mp3Upload.FileName = "text.mp3";
        string action = string.Empty;
        if (Request.QueryString["mode"] != null)
        {
            action = Request.QueryString["mode"].ToString();
        }

        if (action == "add")
        {
            Session["_action"] = "add";
        }
        else if (action == "edit")
        {
            Session["_action"] = "edit";
            if (Request.QueryString["articlecode"] != null)
            {
                Session["articlecode"] = Request.QueryString["articlecode"].ToString();
            }
        }

        else if (action == "detail")
        {
            Session["_action"] = "detail";
            if (Request.QueryString["articlecode"] != null)
            {
                Session["articlecode"] = Request.QueryString["articlecode"].ToString();
            }
            btnSaveArticle.Style.Add("display", "none");
        }

        if (!Page.IsPostBack)
        {           
            LoadComposer();
            LoadPublisher();
            LoadGrade();
            LoadArticleType();
            LoadPeriod();
            LoadSubCategory();

            //Session["_action"] = "edit";
           // Session["articlecode"] = "s000001";
            if (Session["_action"] != null && (String.Compare(Session["_action"].ToString(), "edit") == 0) || (String.Compare(Session["_action"].ToString(), "detail") == 0))
            {
                LoadDataForEditMode();
            }

        }
        
    }
    #region event
    public void LoadComposer()
    {
        List<Composer> composerList = new Facade().GetComposer();
       
        if (composerList != null)
        {           
            ddlComposer.DataSource = composerList;
            ddlComposer.DataValueField = "composerid";
            ddlComposer.DataTextField = "composername";
            ddlComposer.DataBind();
        }

    }

    public void LoadGrade()
    {
        List<Grade> gradeList = new Facade().GetGradebyCulture(CultureInfo.CurrentCulture.Name);

        if (gradeList != null && CultureInfo.CurrentCulture.Name == "en-US")
        {
            ddlGrade.DataSource = gradeList;
            ddlGrade.DataValueField = "gradeid";
            ddlGrade.DataTextField = "gradenameen";
            ddlGrade.DataBind();
        }
        else if (gradeList != null && CultureInfo.CurrentCulture.Name == "nl-NL")
        {
            ddlGrade.DataSource = gradeList;
            ddlGrade.DataValueField = "gradeid";
            ddlGrade.DataTextField = "gradenamenl";
            ddlGrade.DataBind();
        }

    }


    public void LoadPublisher()
    {
        List<Publisher> publisherList = new Facade().GetPublisher();
        if (publisherList != null)
        {
            ddlPublisher.DataSource = publisherList;
            ddlPublisher.DataValueField = "publisherid";
            ddlPublisher.DataTextField = "Publishername";
            ddlPublisher.DataBind();
        }

    }

    public void LoadPeriod()
    {
        List<Period> periodList = new Facade().GetPeriodyCulture(CultureInfo.CurrentCulture.Name);

        if (periodList != null && CultureInfo.CurrentCulture.Name == "en-US")
        {
          //  periodList.Sort();
            periodList.Sort(delegate(Period p1, Period p2) { return p1.Periodsen.CompareTo(p2.Periodsen); });

            ddlPeriod.DataSource = periodList;
            ddlPeriod.DataValueField = "Periodid";
            ddlPeriod.DataTextField = "Periodsen";
            ddlPeriod.DataBind();
        }

        else if (periodList != null && CultureInfo.CurrentCulture.Name == "nl-NL")
        {
            periodList.Sort(delegate(Period p1, Period p2) { return p1.Periodsnl.CompareTo(p2.Periodsnl); });
            ddlPeriod.DataSource = periodList;
            ddlPeriod.DataValueField = "Periodid";
            ddlPeriod.DataTextField = "Periodsnl";
            ddlPeriod.DataBind();
        }

    }

    public void LoadSubCategory()
    {
        List<SubCategory> subCategoryList = new Facade().GetSubCategory(CultureInfo.CurrentCulture.Name);
        if (subCategoryList != null && CultureInfo.CurrentCulture.Name == "en-US")
        {
            subCategoryList.Sort(delegate(SubCategory p1, SubCategory p2) { return p1.Subcategorynameen.CompareTo(p2.Subcategorynameen); });
            ddlDetailSubCategory.DataSource = subCategoryList;
            ddlDetailSubCategory.DataValueField = "subcategoryid";
            ddlDetailSubCategory.DataTextField = "subcategorynameen";
            ddlDetailSubCategory.DataBind();
        }

        else if (subCategoryList != null && CultureInfo.CurrentCulture.Name == "nl-NL")
        {
            subCategoryList.Sort(delegate(SubCategory p1, SubCategory p2) { return p1.Subcategorynamenl.CompareTo(p2.Subcategorynamenl); });
            ddlDetailSubCategory.DataSource = subCategoryList;
            ddlDetailSubCategory.DataValueField = "subcategoryid";
            ddlDetailSubCategory.DataTextField = "subcategorynamenl";
            ddlDetailSubCategory.DataBind();
        }
    }
    public void LoadArticleType()
    {
        DataTable dtArticleType = new Facade().GetArticleType();
        if (dtArticleType != null)
        {
            for (int i = 0; i < dtArticleType.Rows.Count; i++)
            {
                if (dtArticleType.Rows[i]["articletype"] != null)
                {
                    switch (dtArticleType.Rows[i]["articletype"].ToString())
                    {
                        case "b":
                            ddlArticleType.Items.Add(new ListItem("Book", "b"));
                            break;
                        case "s":
                            ddlArticleType.Items.Add(new ListItem("Sheet Music", "s"));
                            break;

                        case "c":
                            ddlArticleType.Items.Add(new ListItem("CD", "c"));
                            break;

                        case "d":
                            ddlArticleType.Items.Add(new ListItem("DVD", "d"));
                            break;
                        default:
                            break;
                    }
                }
            }

        }

    }


    #endregion
    public string insName;
    #region "Tab Event"

    #endregion

    #region event

    public void LoadPdfFlashforArticle()
    {
        dvflash.InnerHtml = @"<object height='249px' width='270px'>
                                        <param name='movie' value='../Resources/swf/flashfile.swf'>
                                        <embed src='../Resources/swf/flashfile.swf' height='249px' width='270px'></embed>
                                    </object>";
        
    }

    public void LoadDataForEditMode()
    {
        string fileName = string.Empty; string to = string.Empty;
        if (Session["articlecode"] != null)
        {
            //  s004753
            DataTable dtarticle = new Facade().GetAritcleByArticleCode(Session["articlecode"].ToString());
            //DataTable dtarticle = new Facade().GetAritcleByArticleCode("s004753");
            if (dtarticle != null && dtarticle.Rows.Count > 0)
            {
                txtTile.Text = dtarticle.Rows[0]["title"].ToString();
                ddlArticleType.SelectedValue = dtarticle.Rows[0]["articletype"].ToString();
                ddlComposer.Text = dtarticle.Rows[0]["composer"].ToString();
                ddlGrade.Text = dtarticle.Rows[0]["grade"].ToString();
                txtPrice.Text = dtarticle.Rows[0]["price"].ToString();
                ddlPublisher.Text = dtarticle.Rows[0]["publisher"].ToString();

                txtEditionNumber.Text = dtarticle.Rows[0]["editionno"].ToString();
                txtInstrumentation.Text = dtarticle.Rows[0]["instrumentation"].ToString();
                txtCategories.Text = dtarticle.Rows[0]["category"].ToString();
                chkIsactive.Checked = bool.Parse(dtarticle.Rows[0]["isactive"].ToString());

                //CultureInfo provider = CultureInfo.InvariantCulture;

                ADate.Text = dtarticle.Rows[0]["publishdate"].ToString().Equals(string.Empty)?"":
                    DateTime.Parse(dtarticle.Rows[0]["publishdate"].ToString()).ToString("dd-MM-yyyy");

                txtQuantity.Text = dtarticle.Rows[0]["quantity"].ToString();
                txtPurchasePrice.Text = dtarticle.Rows[0]["purchaseprice"].ToString();
                txtSubtitle.Text = dtarticle.Rows[0]["subtitle"].ToString();
                txtSerie.Text = dtarticle.Rows[0]["serie"].ToString();
                ddlPeriod.Text = dtarticle.Rows[0]["period"].ToString();
                ddlDetailSubCategory.Text = dtarticle.Rows[0]["subcategory"].ToString();
                txtDuration.Text = dtarticle.Rows[0]["duration"].ToString();
                txtISBN.Text = dtarticle.Rows[0]["isbn10"].ToString();
                txtKeywords.Text = dtarticle.Rows[0]["keywords"].ToString();
                
                FCKeditor1.Value = dtarticle.Rows[0]["descriptionen"].ToString();
               
                
                FCKeditor2.Value = dtarticle.Rows[0]["descriptionnl"].ToString();
               
                if (string.Compare(ddlArticleType.SelectedValue, "s") == 0)
                {
                    fileName = dtarticle.Rows[0]["articlecode"].ToString() + ".mp3";
                    Session["_mp3File"] = fileName;
                    to = HttpContext.Current.Server.MapPath("~/Resources/audio");
                    if (File.Exists(to + "\\" + fileName))
                    {
                        LoadMp3ForArticle(fileName);
                    }
                }

                to = HttpContext.Current.Server.MapPath("~/img");
                fileName = dtarticle.Rows[0]["imagefile"].ToString();
                Session["_imgFile"] = fileName;
                if (File.Exists(to + "\\" + fileName))
                {
                    LoadImageForArticle(fileName);
                }
                if (dtarticle.Rows[0]["pdffile"].ToString() != string.Empty)
                {
                    LoadPdfForArticle(dtarticle.Rows[0]["pdffile"].ToString());
                    LoadPdfFlashforArticle();
                }
            }
        }
    }

    private void LoadMp3ForArticle(string fileName)
    {
        dvmp3.InnerHtml = "<iframe src='../MP3Player.aspx?" + fileName + "' width='220px' style='padding-left: 30px; padding-top: 20px;border:none'></iframe>";
    }
    protected void LoadgrdCategory(object sender, EventArgs e)
    {
        DataTable categoryTable = new Facade().GetCategoryInfo();
        grdCategory.DataSource = categoryTable;
        grdCategory.DataBind();
        upnlCategory.Update();
        mpeCategory.Show();
        //pnlCategory.Style.Add("display", "block");
    }

    protected void LoadgrdInstrumentation(object sender, EventArgs e)
    {
        DataTable InstrumentTable = new Facade().GetInstrumentationInfo();
        grdInstrumentation.DataSource = InstrumentTable;
        grdInstrumentation.DataBind();
        upnlInstrumentation.Update();
        mpeInstrumentation.Show();
        pnlInstrumentation.Style.Add("display", "block");
    }

    protected void LoadPnlComposer(object sender, EventArgs e)
    {
        mpeComposer.Show();
        //pnlAddComposer.Style.Add("display", "block");
    }
    protected void LoadPnlPublisher(object sender, EventArgs e)
    {
        mpeAddPublisher.Show();
        //pnlAddPublisher.Style.Add("display", "block");
    }
    protected void lnkSelect_Command(object sender, CommandEventArgs e)
    {
        string selectedCode = e.CommandArgument.ToString();
        string button = "";
        foreach (GridViewRow row in grdCategory.Rows)
        {
            LinkButton lButton = (LinkButton)row.Cells[0].FindControl("lnkSelect");
            button = lButton.CommandArgument.ToString();
            if (button.Equals(selectedCode))
            {
                txtCategories.Text = selectedCode;
                break;
            }
        }
        //pnlCategory.Style.Add("display", "none");
    }

    public void LoadPdfForArticle(string fileName)
    {
        string filePath = HttpContext.Current.Server.MapPath("~/Resources/pdf/");
        Session["_pdfFile"] = fileName;
        string swfPath = @"C:\Program Files\SWFTools\";
        try
        {
            string val = "script.bat";

            if (File.Exists(swfPath + val))
            {
                File.Delete(swfPath + val);
            }

            FileInfo fi = new FileInfo(swfPath + val);
            StreamWriter sw = fi.CreateText();          
            string[] splitextension = fileName.Split('.');
            sw.WriteLine("cd\\");
            sw.WriteLine("C:");
            sw.WriteLine("cd Program Files\\SWFTools");
           // sw.WriteLine("pdf2swf /w");
            sw.WriteLine("pdf2swf -o " + splitextension[0] + ".swf " + splitextension[0] + ".pdf");
            sw.WriteLine("swfcombine -o flashfile.swf SimpleViewer.swf viewport=" + splitextension[0] + ".swf");
            sw.WriteLine("pause");
            sw.Close();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = swfPath + val;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            try
            {
                using (Process exeProcess = System.Diagnostics.Process.Start(startInfo))
                {
                }
            }
            catch
            {
            }
            string to = HttpContext.Current.Server.MapPath("~/Resources/swf");
            string from = swfPath + "flashfile.swf";
            if (File.Exists(to + "\\flashfile.swf"))
            {
                File.Delete(to + "\\flashfile.swf");
            }
            File.Copy(from, to + "\\flashfile.swf");

        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
        }

    }
    protected void btnShowPdf_Load(object sender, EventArgs e)
    {
        //string filePath = ConfigurationManager.AppSettings["resources"].ToString() + "pdf\\";
        string filePath = HttpContext.Current.Server.MapPath("~/Resources/pdf/");
        string fileName = pdfUpload.PostedFile.FileName;
        Session["_pdfFile"] = fileName;
        string[] splitName = fileName.Split('\\');
        fileName = splitName[splitName.Length - 1];
        string swfPath = @"C:\Program Files\SWFTools\";
        try
        {
            if (File.Exists(filePath + fileName))
            {
                File.Delete(filePath + fileName);
            }
            if (File.Exists(swfPath + fileName))
            {
                File.Delete(swfPath + fileName);
            }

            pdfUpload.PostedFile.SaveAs(filePath + fileName);
            pdfUpload.PostedFile.SaveAs(swfPath + fileName);

            string val = "script.bat";

            if (File.Exists(swfPath + val))
            {
                File.Delete(swfPath + val);
            }

            FileInfo fi = new FileInfo(swfPath + val);
            StreamWriter sw = fi.CreateText();
            string[] splitextension = fileName.Split('.');
            sw.WriteLine("cd\\");
            sw.WriteLine("C:");
            sw.WriteLine("cd Program Files\\SWFTools");
           // sw.WriteLine("pdf2swf /w");
            sw.WriteLine("pdf2swf -o " + splitextension[0] + ".swf " + splitextension[0] + ".pdf");
            sw.WriteLine("swfcombine -o flashfile.swf SimpleViewer.swf viewport=" + splitextension[0] + ".swf");
            sw.WriteLine("pause");
            sw.Close();


            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = swfPath + val;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            try
            {
                using (Process exeProcess = System.Diagnostics.Process.Start(startInfo))
                {
                    //exeProcess.WaitForExit();
                }
            }
            catch
            {
            }
            string to = HttpContext.Current.Server.MapPath("~/Resources/swf");

            string from = swfPath + "flashfile.swf";
            if (File.Exists(to + "\\flashfile.swf"))
            {
                File.Delete(to + "\\flashfile.swf");
            }
            File.Copy(from, to + "\\flashfile.swf");
            LoadPdfFlashforArticle();

            //string script = "document.getElementById('myTab').tabber.tabShow(1);";
            //ScriptManager.RegisterClientScriptBlock(ImageUpload, ImageUpload.GetType(), "viewMediaTab", script, true);
        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
        }
    }


    protected void btnMp3Upload_Load(object sender, EventArgs e)
    {
        try
        {
            string to = HttpContext.Current.Server.MapPath("~/Resources/audio");
            string fileName = Mp3Upload.PostedFile.FileName;
            if (!File.Exists(to + "\\" + fileName))
            {
                Mp3Upload.PostedFile.SaveAs(to + "\\" + fileName);
            }

            LoadMp3ForArticle(fileName);
            Session["_mp3File"] = fileName;

        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
        }
    }


    protected void btnSaveArticle_Click(object sender, EventArgs e)
    {
        try
        {
            Article article = new Article();
            string msg = string.Empty;
            if (Session["_action"] != null && String.Compare(Session["_action"].ToString(), "add") == 0)
            {
                article.Articlecode = AutoGenerationOfPrimaryKey(Convert.ToChar(ddlArticleType.SelectedValue));
            }
            else if (Session["_action"] != null && String.Compare(Session["_action"].ToString(), "edit") == 0)
            {
                article.Articlecode = Session["articlecode"].ToString(); //"s004753";
            }


            article.Articletype = Convert.ToChar(ddlArticleType.SelectedValue);
            if (Session["_pdfFile"] != null)
                article.Pdffile = Session["_pdfFile"].ToString();

            if (Session["_imgFile"] != null)
                article.Imagefile = Session["_imgFile"].ToString();
            if (Convert.ToChar(ddlArticleType.SelectedValue) == 's')
            {
                string fileName = string.Empty;
                article.Containsmusic = true;
                string to = HttpContext.Current.Server.MapPath("~/Resources/audio");
                if (Session["_mp3File"] != null)
                    fileName = Session["_mp3File"].ToString();


                if (File.Exists(to + "\\" + fileName))
                {
                    File.Move(to + "\\" + fileName, to + "\\" + article.Articlecode + ".mp3");
                }
            }


            article.Category = txtCategories.Text.Trim();
            article.Composer = Int32.Parse(ddlComposer.SelectedValue);
            article.Descriptionen = FCKeditor1.Value;
            article.Descriptionnl = FCKeditor2.Value;
            article.Duration = txtDuration.Text.Trim();
            article.Editionno = txtEditionNumber.Text.Trim();
            article.Title = txtTile.Text.Trim();
            article.Publisher = Int32.Parse(ddlPublisher.SelectedValue);
            article.Instrumentation = txtInstrumentation.Text.Trim();
            article.Grade = ddlGrade.SelectedValue;
            if (txtPrice.Text.Trim() != string.Empty)
            {
                article.Price = double.Parse(txtPrice.Text.Trim());
            }
            else
            {
                article.Price = 0.0;
            }
            article.Isactive = chkIsactive.Checked;
            if (txtQuantity.Text.Trim() != string.Empty)
            {
                article.Quantity = int.Parse(txtQuantity.Text.Trim());
            }
            else
            {
                article.Quantity = 0;
            }
            //CultureInfo provider = CultureInfo.InvariantCulture;
            if (string.Compare(ADate.Text, "") == 0)
                article.Publishdate = DateTime.Now.Date;
            else
                article.Publishdate = DateTime.Parse(ADate.Text.ToString());

            if (txtPurchasePrice.Text.Trim() != string.Empty)
            {
                article.Purchaseprice = double.Parse(txtPurchasePrice.Text.Trim());
            }
            else
            {
                article.Purchaseprice = 0.0;
            }

            article.Subtitle = txtSubtitle.Text.Trim();
            article.Serie = txtSerie.Text.Trim();
            article.Isbn10 = txtISBN.Text.Trim();
            article.Period = int.Parse(ddlPeriod.SelectedValue);
            article.Subcategory = ddlDetailSubCategory.SelectedItem.Text.Trim();
            article.Keywords = txtKeywords.Text.Trim();
            bool b = true;
            if (Session["_action"] != null && String.Compare(Session["_action"].ToString(), "add") == 0)
            {
                b = new Facade().AddArticle(article, ref msg);
            }
            if (Session["_action"] != null && String.Compare(Session["_action"].ToString(), "edit") == 0)
            {
                b = new Facade().UpdateArticle(article, ref msg);
            }
            if (b == false)
            {
                lblError.Text = msg;
            }
            Session["_imgFile"] = null;
            Session["_pdfFile"] = null;
            Session["_mp3File"] = null;

            Response.Write("<script> parent.OBSettings.RefreshPage();</script>");
        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
        }

    }


    protected void btnShowImage_Load(object sender, EventArgs e)
    {
        try
        {
            string to = HttpContext.Current.Server.MapPath("~/img");
            string fileName = ImageUpload.PostedFile.FileName;
            Session["_imgFile"] = fileName;
            if (!File.Exists(to + "\\" + fileName))
            {
                ImageUpload.PostedFile.SaveAs(to + "\\" + fileName);
            }
            LoadImageForArticle(fileName);

            //imgThumb.ImageUrl = "../img/" + fileName;
            //upnlImageUpload.Update();

            //string script = "document.getElementById('myTab').tabber.tabShow(1);";
            //ScriptManager.RegisterClientScriptBlock(ImageUpload, ImageUpload.GetType(), "viewMediaTab", script, true);
        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
        }
    }

    private void LoadImageForArticle(string fileName)
    {
        dvImage.InnerHtml = "<img alt='' height='250px' width='275px' src='../img/"
        + fileName.Substring(fileName.LastIndexOf("\\") + 1) + "' />";
    }


    protected void btnInstrumentationSave_Load(object sender, EventArgs e)
    {
        insName = string.Empty;
        foreach (GridViewRow row in grdInstrumentation.Rows)
        {
            CheckBox chk = (CheckBox)row.Cells[0].FindControl("lnkSelect");
            if (chk != null && chk.Checked)
            {
                HiddenField hdfInstrumentname = (HiddenField)row.Cells[1].FindControl("hdfInstrumentname");
                insName += hdfInstrumentname.Value.ToString();
                insName += ",";
            }
        }
        if (insName != string.Empty)
        {
            insName = insName.Substring(0, insName.Length - 1).ToString();
        }
        pnlInstrumentation.Style.Add("display", "none");
        txtInstrumentation.Text = insName;
    }


    protected void btnCancelInstrumentation_Load(object sender, EventArgs e)
    {
        pnlInstrumentation.Style.Add("display", "none");
    }
    protected void btnPanelCategoryCancel_Load(object sender, EventArgs e)
    {
        pnlCategory.Style.Add("display", "none");
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        Publisher publisher = new Publisher();
        publisher.Firstname = txtFirstName.Text.Trim();
        publisher.Middlename = txtMiddleName.Text.Trim();
        publisher.Lastname = txtLatName.Text.Trim();
        publisher.Initialname = txtInitialName.Text.Trim();
        publisher.address = new Address();
        publisher.address.Housenr = txtHouseNr.Text.Trim();
        publisher.address.address = txtAddress.Text.Trim();
        publisher.address.Postcode = txtPostCode.Text.Trim();
        publisher.address.Residence = txtResidence.Text.Trim();
        publisher.address.Country = txtCountry.Text.Trim();

        publisher.Email = txtEmail.Text.Trim();
        publisher.Website = txtWebsite.Text.Trim();
        publisher.Telephone = txtTelephone.Text.Trim();
        publisher.Fax = txtFax.Text.Trim();

        publisher.Companyname = txtCompanyName.Text.Trim();
        publisher.Ispublisher = chkIsPublisher.Checked;

        bool b = new Facade().AddPublisher(publisher, ref msg);

        if (b == false)
        {
            lblError.Text = msg;
        }
        else
        {
            pnlAddPublisher.Style.Add("display", "none");
            ddlPublisher.Items.Clear();
            LoadPublisher();
           
            txtFirstName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtLatName.Text = string.Empty;
            txtInitialName.Text = string.Empty;

            txtHouseNr.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPostCode.Text = string.Empty;
            txtResidence.Text = string.Empty;
            txtCountry.Text = string.Empty;

            txtEmail.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            txtFax.Text = string.Empty;

            txtCompanyName.Text = string.Empty;
            chkIsPublisher.Checked = false;
        }
    }



    protected void btnSaveComposer_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        Composer composer = new Composer();
        composer.Country = txtCountryComposer.Text.Trim();
        composer.Dob = txtDob.Text.Trim();
        composer.Dod = txtDod.Text.Trim();
        composer.Firstname = txtFirstNameComposer.Text.Trim();
        composer.Lastname = txtLastNameComposer.Text.Trim();
        composer.Middlename = txtMiddleNameComposer.Text.Trim();

        bool b = new Facade().AddComposer(composer, ref msg);

        if (b == false)
        {
            lblError1.Text = msg;
        }
        else
        {
            pnlAddComposer.Style.Add("display", "none");
            ddlComposer.Items.Clear();
            LoadComposer();

            txtFirstNameComposer.Text = string.Empty;
            txtMiddleNameComposer.Text = string.Empty;
            txtLastNameComposer.Text = string.Empty;
            txtCountryComposer.Text = string.Empty;
            txtDob.Text = string.Empty;
            txtDod.Text = string.Empty;
        }
    }


    protected void btnCancelComposer_Click(object sender, EventArgs e)
    {
        pnlAddComposer.Style.Add("display", "none");
    }
    protected void btnArticleCancel_Click(object sender, EventArgs e)
    {       
        Response.Write("<script> parent.OBSettings.RefreshPage();</script>");
    }
    #endregion




    public string AutoGenerationOfPrimaryKey(char articleType)
    {
        DataTable dt = new Facade().GetArticleCodeInfoByType(articleType);
        string fvalue = string.Empty;
        if (dt != null)
        {
            fvalue = dt.Rows[dt.Rows.Count - 1][0].ToString();
            //char prefix = fvalue[0];
            //if (fvalue[1] >= '0' && fvalue[1] <= '9')
            //{
            //    fvalue = fvalue.Substring(1, fvalue.Length - 1).ToString();
            //    int suffix = int.Parse(fvalue);
            //    suffix++;
            //    string s = suffix.ToString();
            //    if (s.Length != fvalue.Length)
            //    {
            //        int count0 = fvalue.Length - s.Length;
            //        fvalue = s.PadLeft(count0 + s.Length, '0').ToString();
            //    }
            //    else
            //    {
            //        fvalue = s;
            //    }
            //    fvalue = fvalue.PadLeft(fvalue.Length + 1, prefix);


            //}
            fvalue = articleType.ToString() + (int.Parse(fvalue.Substring(1)) + 1).ToString().PadLeft(6, '0');
        }
        else
        {
            fvalue = articleType.ToString();
            fvalue = fvalue.PadRight(6, '0') + "1";
        }
        return fvalue;
    }





}
