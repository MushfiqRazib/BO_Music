/*
 * upload2.aspx.cs
 * Last Updated -- 05/07/2007
 * Author - Abdullah Al Mohammad
 * 
 */

using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web;
using Npgsql;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Threading;
using System.Web.Configuration;  

public partial class Admin_upload : System.Web.UI.Page
{
  
    DbHandler handler = new DbHandler();
    private static DataTable articleTable;
    DataTable newsTable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessageArticle.Text = "";
        lblMessageNews.Text = "";
        lblArtImage.Text = "";
        lblArtFile.Text = "";
        lblNwsImage.Text = "";
        
        if (!IsPostBack)
        {
            LoadSession();//initialize the session required at startup
            LoadArticleGrid(Session["ArtSortOrder"].ToString());//Load Article Grid
            LoadNewsGrid(Session["NewsSortOrder"].ToString(),Session["type"].ToString());//Load News Grid                        
            grdArticle.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Functions.selectedColor);
            grdNews.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(Functions.selectedColor);
            
        }
        //if (Session["ArticleCode"] == null)
        //{
        //    LoadArticleGrid(Session["ArtSortOrder"].ToString());//Load Article Grid

        //}
        //if (Session["NewsCode"] == null)
        //{
        //    LoadNewsGrid(Session["NewsSortOrder"].ToString(), Session["type"].ToString());//Load News Grid                        

        //}

       
    }

   

    /// <summary>
    /// LoadSession is used to initialize the sorting order sessions
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    private void LoadSession()
    {
        string initialOrder = "Date desc";//initialize to date column in descending order

        if (Session["ArtSortOrder"] == null)
        {
            Session["ArtSortOrder"] = initialOrder;
        }
        if (Session["NewsSortOrder"] == null)
        {
            Session["NewsSortOrder"] = initialOrder;
        }
        if (Session["type"] == null)
        {
            Session["type"] = "true";
        }
    }
    

    /// <summary>
    /// LoadArticleGrid mehtod loads data to article grid
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    /// <param name="sortOrder">
    /// contains the column name and order to sort
    /// </param>
    private void LoadArticleGrid(string sortOrder)
    {
        string searchText = txtFilter.Text.Trim().ToLower();
        //string query = @"select articlecode as Code, title as Title, to_char(publishdate,'yyyy-mm-dd') as Date, (case when lower(articletype)='c' then 'CD/DVD' when lower(articletype)='b' then 'Book' when lower(articletype)='s' then 'SheetMusic' end) as Type, imagefile as Image," +
        //                "pdffile as File from article";

        string query = @"select a.articlecode as Code, a.title as Title, to_char(a.publishdate,'yyyy-mm-dd') as Date, 
                        (case when lower(a.articletype)='c' then 'CD/DVD' when lower(a.articletype)='b' then 'Book' 
                         when lower(a.articletype)='s' then 'SheetMusic' end) as Type, a.imagefile as Image, a.pdffile as File, 
                        (case when a.containsmusic='true' then (a.articlecode||'.mp3')end) as musicfile
                         from article a ";

        if (txtFilter.Text.Length != 0 && ddlFilter.SelectedValue.ToString().Equals("article"))
        {
            query += @" where lower(a.articlecode) like :searchText OR lower(a.title) like :searchText" +
                      " OR lower(a.articletype) like :searchText OR to_char(a.publishdate,'yyyy-mm-dd') like :searchText " +
                      " OR lower(a.imagefile) like :searchText OR lower(a.pdffile) like :searchText ";
        }
        if (!sortOrder.Trim().Equals(""))
        {
            query += " order by " + sortOrder;
        }
        NpgsqlCommand command = new NpgsqlCommand(query);
        command.Parameters.Add("searchText", "%" + searchText + "%");

        articleTable = handler.GetDataTable(command);
        if (articleTable.Rows.Count == 0)
        {
            //articleTable.NewRow();
            articleTable.Rows.Add(articleTable.NewRow());
            grdArticle.DataSource = articleTable;
            grdArticle.DataBind();

            //grdArticle.Rows[0].Cells.Clear;
            grdArticle.Rows[0].Cells.Clear();
        }
        else
        {
            grdArticle.DataSource = articleTable;
            grdArticle.DataBind();
        }
        txtArticlePDF.Text = "";
        txtArtImg.Text = "";
       
        
    }

    /// <summary>
    /// LoadNewsGrid mehtod loads data to article grid
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    /// <param name="sortOrder">
    ///  contains the column name and order to sort
    /// </param>
    private void LoadNewsGrid(string sortOrder, string type)
    {
        string searchText = txtFilter.Text.Trim().ToLower();
        string query = @"select newsid as Code, title as Title, to_char(newsdate,'yyyy-mm-dd') as Date, newsimagefile as Image " +
                               " from news where (shownews = " + type +") ";
        if (txtFilter.Text.Length != 0 && ddlFilter.SelectedValue.ToString().Equals("news"))
        {
            query += @" and (lower(newsid) like :searchText OR lower(title) like :searchText" +
                      " OR to_char(newsdate,'yyyy-mm-dd') like :searchText " +
                      " OR lower(newsimagefile) like :searchText) ";
        }
        if (!sortOrder.Trim().Equals(""))
        {
            query += " order by " + sortOrder;
        }

        NpgsqlCommand command = new NpgsqlCommand(query);
        command.Parameters.Add("searchText", "%" + searchText + "%");
        newsTable = handler.GetDataTable(command);
        if (newsTable.Rows.Count.Equals(0))
        {
            newsTable.Rows.Add(newsTable.NewRow());
            grdNews.DataSource = newsTable;
            grdNews.DataBind();
            grdNews.Rows[0].Cells.Clear();
        }
        else
        {
            grdNews.DataSource = newsTable;
            grdNews.DataBind();
        }
        txtNwsImg.Text = "";
        
    }

 

    /// <summary>
    /// LoadNewsImageFiles() is used to load news image files name
    /// in txtNwsImg text box.
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    /// <param name="code">
    ///  contains the newsid from news table
    /// </param>
    protected void LoadNewsImageFiles(string code)
    {
        string query = @"select newsimagefile from news where
                             newsid = '" + code + "'";
        DataTable dtable = handler.GetDataTable(query);
        if (handler.HasRecord(dtable))
        {
            txtNwsImg.Text = dtable.Rows[0]["newsimagefile"].ToString();
        }
    }

    /// <summary>
    /// LoadArticleImageAndFiles() is used to load Article imagefile names
    /// and file names in txtArtImg and txtArticlePDF text box.
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    /// <param name="code">
    /// contains the articlecode from article table table
    /// </param>
    protected void LoadArticleImageAndFiles(string code)
    {
        string query = @"select imagefile, pdffile from article where
                             articlecode = '" + code + "'";
        DataTable dtable = handler.GetDataTable(query);
        if (handler.HasRecord(dtable))
        {
            txtArtImg.Text = dtable.Rows[0]["imagefile"].ToString();
            txtArticlePDF.Text = dtable.Rows[0]["pdffile"].ToString();
        }
        
    }   


    /// <summary>
    /// page index changing event for article grid
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdArticle_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdArticle.SelectedIndex = -1;
        grdArticle.PageIndex = e.NewPageIndex;
        LoadArticleGrid(Session["ArtSortOrder"].ToString());
        Session["ArticleCode"] = null;
        txtArticlePDF.Text = "";
        txtArtImg.Text = "";
        txtArticleMusic.Text = "";
    }

    /// <summary>
    /// select link click event
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void lnkSelect_Click(object sender, CommandEventArgs e)
    //{
    //    string code = e.CommandArgument.ToString();        
    //    Session["ArticleCode"] = code;
    //    colorSelection(grdArticle, "lnkSelect",code, "#8B9BBA");//highlight the selected row
    //    string query = @"select imagefile, pdffile from article where articlecode ='" + code + "'";
    //    DataTable dTable = handler.GetDataTable(query);
    //    txtArtImg.Text = dTable.Rows[0]["imagefile"].ToString();//populate text box with data
    //    txtArticlePDF.Text = dTable.Rows[0]["pdffile"].ToString();//populate text box with data
    //}

    /// <summary>
    /// colorSelection method to highlight the selected row
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    /// <param name="GridViewRow"> contains the the grid to be processed </param>
    /// <param name="type"> contains the id of the select link </param>
    /// <param name="code"> code contains article code for article or news id for news </param>
    /// <param name="colorCode"> colour value </param>
    //private void colorSelection(GridView grd, string type, string code, string color)
    //{

    //    foreach (GridViewRow row in grd.Rows)
    //    {                        
    //        LinkButton id = (LinkButton)row.Cells[0].FindControl(type);
    //        if (id.CommandArgument.ToString().Equals(code))
    //        {
    //            row.BackColor = System.Drawing.ColorTranslator.FromHtml(Functions.selectedColor);
    //        }
    //        else if (row.RowState.ToString().ToLower().Equals("normal"))
    //        {
    //            row.BackColor = System.Drawing.ColorTranslator.FromHtml(Functions.normalColor);

    //        }
    //        else if (row.RowState.ToString().ToLower().Equals("alternate"))
    //        {
    //            row.BackColor = System.Drawing.ColorTranslator.FromHtml(Functions.alternateColor);

    //        }

    //    }


    //}

    /// <summary>
    /// event method for sorting article grid
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdArticle_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingOrder = "";
        grdArticle.SelectedIndex = -1;
        if (Session["ArtSortOrder"] == null)
        {
            sortingOrder = e.SortExpression.ToString() + " " + "desc";//initially set to descending
            Session["ArtSortOrder"] = sortingOrder;

        }
        else
        {
            sortingOrder = Session["ArtSortOrder"].ToString();
            string[] splitter = sortingOrder.Split(' ');
            sortingOrder = splitter[splitter.Length - 1];//get only 'desc' or 'asc'
            if (sortingOrder.Equals("desc"))
            {                
                sortingOrder = e.SortExpression.ToString() + " " + "asc";//switch to ascending
                Session["ArtSortOrder"] = sortingOrder;
            }
            if (sortingOrder.Equals("asc"))
            {
                sortingOrder = e.SortExpression.ToString() + " " + "desc";//switch to descending
                Session["ArtSortOrder"] = sortingOrder;
            }
        }
        LoadArticleGrid(sortingOrder);//load the grid
        Session["ArticleCode"] = null;
        txtArticlePDF.Text = "";
        txtArtImg.Text = "";
        txtArticleMusic.Text = "";
    }




    /// <summary>
    /// event method for sorting news grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdNews_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdNews.SelectedIndex = -1;
        grdNews.PageIndex = e.NewPageIndex;
        LoadNewsGrid(Session["NewsSortOrder"].ToString(), Session["type"].ToString());
        Session["NewsCode"] = null;
        txtNwsImg.Text = "";
        //txtNwsImg.Text = "";        
    }
    protected void grdNews_Sorting(object sender, GridViewSortEventArgs e)
    {
        grdNews.SelectedIndex = -1;
        string sortingOrder = "";
        if (Session["NewsSortOrder"] == null)
        {
            sortingOrder = e.SortExpression.ToString() + " " + "desc";//initially set to descending
            Session["NewsSortOrder"] = sortingOrder;

        }
        else
        {
            sortingOrder = Session["NewsSortOrder"].ToString();
            string[] splitter = sortingOrder.Split(' ');
            sortingOrder = splitter[splitter.Length - 1];//get only descending or ascending
            if (sortingOrder.Equals("desc"))
            {
                sortingOrder = e.SortExpression.ToString() + " " + "asc";//switch to ascending
                Session["NewsSortOrder"] = sortingOrder;
            }
            if (sortingOrder.Equals("asc"))
            {
                sortingOrder = e.SortExpression.ToString() + " " + "desc";//switch to descending
                Session["NewsSortOrder"] = sortingOrder;
            }
        }
        LoadNewsGrid(sortingOrder, Session["type"].ToString());//load the grid    
        Session["NewsCode"] = null;
        txtNwsImg.Text = "";
    }

    /// <summary>
    /// event method for clicking select link in news grid
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void lnkSelectNews_Click(object sender, CommandEventArgs e)
    //{
    //    string code = e.CommandArgument.ToString();
    //    Session["NewsCode"] = code;
    //    colorSelection(grdNews, "lnkSelectNews", code, "#8B9BBA");
    //    string query = @"select newsimagefile from news where newsid ='" + code + "'";
    //    DataTable dTable = handler.GetDataTable(query);
    //    txtNwsImg.Text = dTable.Rows[0]["newsimagefile"].ToString();//populate the text box with data        
    //}

    /// <summary>
    /// event method for upload button in article grid
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUploadArticle_Click(object sender, EventArgs e)
    {
        if ((uplTheFileArticleImg.Value != "" && Session["ArticleCode"] != null) ||
             (uplTheFileArticlePDF.Value != "" && Session["ArticleCode"] != null) ||
             (uplMusic.Value != "" && Session["ArticleCode"] != null)
            )
        {
            if (uplTheFileArticleImg.Value != "" && Session["ArticleCode"] != null)
            {
                if (UploadImage("images", uplTheFileArticleImg, Session["ArticleCode"].ToString()))
                {
                    //Session["ArticleCode"] = null;
                }
            }
            if (uplTheFileArticlePDF.Value != "" && Session["ArticleCode"] != null)
            {
                if (UploadPDF(Session["ArticleCode"].ToString()))
                {
                    // Session["ArticleCode"] = null;
                }
            }
            if (uplMusic.Value != "" && Session["ArticleCode"] != null)
            {
                UploadMusic(uplMusic, Session["ArticleCode"].ToString());
            }
            grdArticle.SelectedIndex = -1;
            LoadArticleGrid(Session["ArtSortOrder"].ToString());//Load Article Grid

        }
        else
        {

            if (Session["ArticleCode"] == null)
            {
                lblMessageArticle.Text = "No Article Selected!";
            }
            else
            {
                lblMessageArticle.Text = "No Images\\Document Files\\Music Files Selected!";

            }
        }
    }

    private double GetMaxFileSize()
    {
        System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
        HttpRuntimeSection section = config.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
        return Math.Round(section.MaxRequestLength / 1024.0, 1);
    }


    private bool UploadMusic(HtmlInputFile musicFile, string code)
    {
        double maxAllowedFileSize = GetMaxFileSize();
        if (!Functions.IsMusicFile(musicFile.Value))
        {
            lblArtMusic.Text = "Not a compatible music file!";
            return false;
        }
        else if (musicFile.Size > maxAllowedFileSize)
        {
            lblArtFile.Text = string.Format("Make sure your file is under {0:0.#} MB.", maxAllowedFileSize);
            return false;
        }
        else
        {
            string filePath = ConfigurationManager.AppSettings["resources"].ToString() + "audio\\";
            string fileName = uplMusic.Value;
            string[] splitName = fileName.Split('\\');
            fileName = splitName[splitName.Length - 1];

            try
            {
                if (txtArticleMusic.Text != "" || txtArticleMusic.Text != null)
                {
                    DeleteExistingFile("audio", txtArticleMusic.Text);
                }
                uplMusic.PostedFile.SaveAs(filePath + code + fileName.Substring(fileName.LastIndexOf('.')));
                string query = @"update article set containsmusic = true
                                 where articlecode = '" + code + "'";

                if (handler.ExecuteQuery(query))
                {
                    lblArtMusic.Text = "Music upload completed.";
                }
            }
            catch (Exception ex)
            {
                Boeijenga.Common.Utils.LogWriter.Log(ex);
                lblArtMusic.Text = " Error in uploading ";
                return false;

            }

            return true;

        }
    }
    /// <summary>
    /// UploadImage is used to upload images
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    /// <param name="imageType"> identifies for article type or news type images</param>
    /// <param name="uplTheFile"> htmlinput file used to browse the selected file</param>
    /// <param name="code"> articlecode or newsid</param>
    private bool UploadImage(string imageType, HtmlInputFile uplTheFile, string code)
    {

        if (!Functions.IsImageFile(uplTheFile.Value))//check for valid image
        {
            if (imageType.Equals("images"))
            {
                //lblMessageArticle.Text += " Not an image! ";
                lblArtImage.Text = " Not an image! ";
            }
            else
            {
                //lblMessageNews.Text += " Not an image! ";
                lblNwsImage.Text = " Not an image! ";
            }
            return false;
        }

        if (imageType.Equals("images"))
        {
            if (!txtArtImg.Text.Trim().Equals(""))
            {
                DeleteExistingFile(imageType, txtArtImg.Text);//delete previous file
            }
        }
        if (imageType.Equals("newsimage"))
        {
            if (!txtNwsImg.Text.Trim().Equals(""))
            {
                DeleteExistingFile(imageType, txtNwsImg.Text);//delete previous file
            }
        }
        string filePath = ConfigurationManager.AppSettings["resources"].ToString() + imageType + "/";
        string fileName = uplTheFile.Value;
        string[] splitName = fileName.Split('\\');
        fileName = splitName[splitName.Length - 1];

        //if the image for article then rename the image file to article code
        if (imageType.Equals("images"))
        {
            string[] splitter = fileName.Split('.');
            fileName = code + "." + splitter[splitter.Length - 1];
        }
        try
        {
            uplTheFile.PostedFile.SaveAs(filePath + fileName);
        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
            if (imageType.Equals("images"))
            {
                //lblMessageArticle.Text += " Error uploading images for article. ";
                lblArtImage.Text = " Error in uploading ";
            }
            else
            {
                //lblMessageNews.Text += "Error uploading! images for news. ";
                lblNwsImage.Text = "Error in uploading ";
            }
        }

        //when the image is article type or news type with Allow Process is checked
        if (imageType.Equals("images") || (imageType.Equals("newsimage") && chkProcessImg.Checked == true))
        {
            int width = 94;//width for thumbnail image
            int height = 132;//height for thumbnail image
            string Path = filePath + fileName;
            Bitmap bmp = Functions.CreateThumbnail(Path, width, height);
            int widthLarge = 180;//width for large image
            int heightLarge = 252;//height for large image
            Bitmap bmpLarge = Functions.CreateThumbnail(Path, widthLarge, heightLarge);
            if (bmp == null || bmpLarge == null)
            {
                this.ErrorResult();
                return false;
            }

            //rename the thumbnail image with prefix 'Thumb_'
            string outputThumb = filePath + "Thumb_" + fileName;
            string LargeFilename = filePath + fileName;

            try
            {

                bmp.Save(outputThumb);//Save the thumb bitmap
                bmpLarge.Save(LargeFilename);//Save the Large bitmap 
                bmp.Dispose();
                bmpLarge.Dispose();

                string query = @"update article set imagefile = '" + fileName + "'" +
                         "where articlecode = '" + code + "'";
                if (imageType.Equals("newsimage"))
                {
                    query = @"update news set newsimagefile = '" + fileName + "'" +
                         "where newsid = '" + code + "'";
                }
                //update the text fields for article and news that contains the
                //file name from database
                if (handler.ExecuteQuery(query))
                {
                    if (imageType.Equals("images"))
                    {
                        //LoadArticleGrid(Session["ArtSortOrder"].ToString());
                        //LoadArticleImageAndFiles(code);
                        //lblMessageArticle.Text += " Image upload complete for article. ";
                        lblArtImage.Text = " Image upload completed ";
                       // Session["ArticleCode"] = null;
                        
                    }
                    else
                    {
                        LoadNewsGrid(Session["NewsSortOrder"].ToString(), Session["type"].ToString());
                       // LoadNewsImageFiles(code);
                        //lblMessageNews.Text += " Image upload complete for news. " ;
                        lblNwsImage.Text = " Image upload completed ";
                        //Session["NewsCode"] = null;
                    }

                }

            }
            catch (Exception ex)
            {
                Boeijenga.Common.Utils.LogWriter.Log(ex);
                bmp.Dispose();
                bmpLarge.Dispose();
                this.ErrorResult();
                return false;
            }
        }

       //when the Allow Precess is not checked for News
        else if (imageType.Equals("newsimage") && chkProcessImg.Checked == false)
        {
            int width = 180;//width for news image
            int height = 252;//height for news image
            string Path = filePath + fileName;
            Bitmap bmp = Functions.CreateThumbnail(Path, width, height);
            if (bmp == null)
            {
                this.ErrorResult();
                return false;
            }
            string LargeFilename = filePath + fileName;

            try
            {
                bmp.Save(LargeFilename);
                bmp.Dispose();
                string query = @"update news set newsimagefile = '" + fileName + "'" +
                         "where newsid = '" + code + "'";
                if (handler.ExecuteQuery(query))
                {
                    LoadNewsGrid(Session["NewsSortOrder"].ToString(), Session["type"].ToString());
                    //LoadNewsImageFiles(code);
                    //lblMessageNews.Text += " Image upload complete for news. ";
                    lblNwsImage.Text = " Image upload completed ";
                }
            }
            catch (Exception ex)
            {
                Boeijenga.Common.Utils.LogWriter.Log(ex);
                bmp.Dispose();
                this.ErrorResult();
                return false;
            }
        }
        return true;

    }

/// <summary>
/// DeleteExistingFile method is used to delete files
/// Updated - 05/07/2007
/// Author - Abdullah Al Mohammad
/// </summary>
/// <param name="type"> identifies for article type or news type images</param>
/// <param name="fileName"> the name of the file to be deleted</param>
    protected void DeleteExistingFile(string type, string fileName)
    {
        string baseLocation = ConfigurationManager.AppSettings["resources"].ToString() + type + "/";
        if (fileName != "" || fileName != null)
        {
            string file = baseLocation + fileName;
            string thFile = baseLocation + "Thumb_" + fileName;
            string[] fileEntries = Directory.GetFiles(baseLocation);
            foreach (string sFile in fileEntries)
            {
                if (sFile == file)
                {
                    File.Delete(file);
                }
                if (sFile == thFile)//if thumbnail file exist with prefix 'Thumb_'
                {
                    File.Delete(thFile);
                }
            }
        }

    }

    /// <summary>
    /// CreateThumbnail method for create new resized image
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    /// <param name="lcFilename">file name of the image to be resized</param>
    /// <param name="lnWidth">desired max width</param>
    /// <param name="lnHeight">desired max height</param>
    /// <returns> returns bitmap image</returns>

    //public static Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
    //{
    //    System.Drawing.Bitmap bmpOut = null;
    //    Bitmap loBMP = new Bitmap(lcFilename);
    //    try
    //    {
    //        int lnNewWidth = 0;
    //        int lnNewHeight = 0;

    //        ResizeImage(loBMP.Width, loBMP.Height, lnWidth, lnHeight, ref lnNewWidth, ref lnNewHeight);

    //        bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
    //        Graphics g = Graphics.FromImage(bmpOut);
    //        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
    //        g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
    //        g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);
    //    }
    //    catch
    //    {
    //        return null;
    //    }
    //    finally
    //    {
    //        loBMP.Dispose();
    //    }
    //    return bmpOut;
    //}


   
    /// <summary>
    /// ResizeImage method to resize the images
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    /// <param name="bmpWidth">selected image's width</param>
    /// <param name="bmpHeight">selected image's height</param>
    /// <param name="lnWidth">disired max width</param>
    /// <param name="lnHeight">desired max height</param>
    /// <param name="lnNewWidth">calculated new width</param>
    /// <param name="lnNewHeight">calculated new height</param>
    //private static void ResizeImage(int bmpWidth, int bmpHeight, int lnWidth, int lnHeight, ref int lnNewWidth, ref int lnNewHeight)
    //{
    //    decimal lnRatio;
    //    // if image width & height is less than specified width & height
    //    if (bmpWidth <= lnWidth && bmpHeight <= lnHeight)
    //    {
    //        lnNewWidth = bmpWidth;
    //        lnNewHeight = bmpHeight;
    //    }
    //    //if image width is less and height is greater
    //    else if (bmpWidth <= lnWidth && bmpHeight >= lnHeight)
    //    {
    //        lnNewHeight = lnHeight;
    //        lnRatio = (decimal)bmpWidth / bmpHeight;
    //        decimal lnTemp = lnRatio * lnNewHeight;
    //        lnNewWidth = (int)lnTemp;
    //    }
    //    else // else do
    //    {
    //        lnNewWidth = lnWidth;
    //        lnRatio = (decimal)bmpHeight / bmpWidth;
    //        decimal lnTemp = lnRatio * lnNewWidth;
    //        lnNewHeight = (int)lnTemp;
    //    }

    //    // check for width and height resize yet??
    //    if (lnNewWidth > lnWidth || lnNewHeight > lnHeight)
    //    {
    //        ResizeImage(lnNewWidth, lnNewHeight, lnWidth, lnHeight, ref lnNewWidth, ref lnNewHeight);
    //    }
    //}

    /// <summary>
    /// UploadPDF method for uploading files
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    /// <param name="code">contains articlecode</param>
    private bool UploadPDF(string code)
    {

        if (txtArticlePDF.Text != "" || txtArticlePDF.Text != null)
        {
            DeleteExistingFile("pdf", txtArticlePDF.Text);//delete if exist
        }
        string filePath = ConfigurationManager.AppSettings["resources"].ToString() + "pdf/";
        string fileName = uplTheFileArticlePDF.Value;
        string[] splitName = fileName.Split('\\');
        fileName = splitName[splitName.Length - 1];

        try
        {
            uplTheFileArticlePDF.PostedFile.SaveAs(filePath + fileName);
            string query = @"update article set pdffile = '" + fileName + "'" +
                             "where articlecode = '" + code + "'";
            if (handler.ExecuteQuery(query))
            {
                //LoadArticleGrid(Session["ArtSortOrder"].ToString());
                //LoadArticleImageAndFiles(code);
                //lblMessageArticle.Text += "File upload complete for image. ";
                lblArtFile.Text = "File upload completed ";
               // Session["ArticleCode"] = null;

            }

        }
        catch (Exception ex)
        {
            Boeijenga.Common.Utils.LogWriter.Log(ex);
            lblArtFile.Text = " Error in uploading ";
            return false;

        }

        return true;
    }

    /// <summary>
    /// ErrorResult mehtod to generate error
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    private void ErrorResult()
    {
        Response.Clear();
        Response.StatusCode = 404;
        Response.End();
    }

    /// <summary>
    /// click event method for upload button in news
    /// Updated - 05/07/2007
    /// Author - Abdullah Al Mohammad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUploadNews_Click(object sender, EventArgs e)
    {
        if (uplTheFileNewsImg.Value != "" && Session["NewsCode"] != null)
        {
            if (UploadImage("newsimage", uplTheFileNewsImg, Session["NewsCode"].ToString()))
            {
               // Session["NewsCode"] = null;
            }
        }

        else
        {
            if (Session["NewsCode"] == null)
            {
                lblMessageNews.Text = " No News Selected! ";
            }
            else
            {
                lblMessageNews.Text = " No Images Selected! ";
                //lblNwsImage.Text = " No Images Selected! ";
            }
        }
    }
    protected void ddlNews_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["NewsCode"] = null;
        if (ddlNews.SelectedValue.ToString().Equals("true"))
        {
            Session["type"] = "true";
        }
        else if (ddlNews.SelectedValue.ToString().Equals("false"))
        {
            Session["type"] = "false";
        }
        else
        {
            Session["type"] = "true OR shownews = false";
        }
        LoadNewsGrid(Session["NewsSortOrder"].ToString(), Session["type"].ToString());
        Session["NewsCode"] = null;
        txtNwsImg.Text = "";
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        grdArticle.SelectedIndex = -1;
        grdNews.SelectedIndex = -1;
        Session["NewsCode"] = null;
        Session["ArticleCode"] = null;
        if (ddlFilter.SelectedValue.ToString().Equals("article"))
        {
            LoadArticleGrid(Session["ArtSortOrder"].ToString());//Load Article Grid
        }
        if (ddlFilter.SelectedValue.ToString().Equals("news"))
        {
            LoadNewsGrid(Session["NewsSortOrder"].ToString(), Session["type"].ToString());//Load News Grid 
        }
    }
    protected void grdArticle_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = articleTable;
        int index = (grdArticle.PageIndex * grdArticle.PageSize) + grdArticle.SelectedIndex;
        Session["ArticleCode"] = dt.Rows[index]["code"].ToString();
        txtArticleMusic.Text = dt.Rows[index]["musicfile"].ToString();
        txtArticlePDF.Text = dt.Rows[index]["file"].ToString();
        txtArtImg.Text = dt.Rows[index]["image"].ToString();


        //GridViewRow row = grdArticle.SelectedRow;
        //Label temp = (Label)row.FindControl("lblArticleCode"); //row.Cells[3].Text;        
        //if (temp.Text.Length > 0)
        //{
        //    Session["ArticleCode"] = temp.Text;
        //    temp = (Label)row.FindControl("lblImage");
        //    txtArtImg.Text = temp.Text;
        //    temp = (Label)row.FindControl("lblFile");
        //    txtArticlePDF.Text = temp.Text;
        //}       
    }
    protected void grdNews_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = grdNews.SelectedRow;
        Label temp = (Label)row.FindControl("lblCode");        
        if (temp.Text.Length > 0)
        {
            Session["NewsCode"] = temp.Text;
            temp = (Label)row.FindControl("lblNwsImg");
            txtNwsImg.Text = temp.Text;
        }
    }

    /// <summary>
    /// code@Provas
    /// date: 27-mar-09
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSynchronize_Click(object sender, EventArgs e)
    {
        Synchronizer sync = new Synchronizer();
        string resourcePath = System.Configuration.ConfigurationSettings.AppSettings["resources"].ToString();
        if (sync.Synchronize(resourcePath))
        {
            lblMessageArticle.Text = "Synchronization completed !!!";
        }
        else
        {
            lblMessageArticle.Text = "Synchronization failed.";
        }

    }
}
