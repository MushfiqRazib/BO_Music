using System;
using System.Data;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;
using System.Threading;
using Boeijenga.DataAccess.Constants;
using Boeijenga.Business;
using System.Collections.Generic;
using Boeijenga.Common.Objects;

public partial class Admin_DateRange : System.Web.UI.Page
{
    DbHandler dbHandler = new DbHandler();
    string sql = "";
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["analysis"] != null)
            {
                if (Request.Params["analysis"].ToString().Equals("vat"))
                {
                    lblAnalysis.Text = "VAT Analysis";
                }
                else if (Request.Params["analysis"].ToString().Equals("sales"))
                {
                    lblAnalysis.Text = "Sales Statement";
                }
            }            
            
        }
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nl-NL");
        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("nl-NL");
    }
    protected void btnPdf_Click(object sender, EventArgs e)
    {
        if (Request.Params["analysis"] != null)
        {
            if (Request.Params["analysis"].ToString().Equals("vat"))
            {
                PrintVatAnalysis();
            }
            else if (Request.Params["analysis"].ToString().Equals("sales"))
            {
                PrintSalesStatement();
            }
        }
        
    }

    private void PrintSalesStatement()
    {
        #region variables
        double bookPrice;
        double cdPrice;
        double sheetPrice;
        int bookQty;
        int cdQty;
        int sheetQty;
        int flag = 0;
        int pageCount = 1;
        string strfd = txtFromDate.Text;
        string[] strDate = strfd.Split('-');
        string date = strDate[0];
        string Month = strDate[1];
        string Year = strDate[2];
        strfd = Year + '-' + Month + '-' + date;
        string strto = txtToDate.Text;
        strDate = strto.Split('-');
        date = strDate[0];
        Month = strDate[1];
        Year = strDate[2];
        strto = Year + '-' + Month + '-' + date;
        #endregion

        #region query
      
        DataTable dt1 = new Facade().GetReportByFromAndTo(strfd, strto, true, false, false); //dbHandler.GetDataTable(sql1);


//        string sql2 = @"select os.articlecode as ArticleCode,a.title as Title, Sum(unitprice) as Price,sum(os.quantity) as Quantity
//                    from orders o, ordersline os , article a
//                    where o.orderid = os.orderid
//                    and os.articlecode=a.articlecode 
//                    and o.orderdate between '" + strfd + "' and '" + strto + @"'
//                    and (os.articlecode like 'c%' or os.articlecode like 'd%') 
//                    group by os.articlecode,a.title 
//                    order by Quantity desc, Price desc";
        
        DataTable dt2 = new Facade().GetReportByFromAndTo(strfd, strto, false,true,false); //dbHandler.GetDataTable(sql2);



        DataTable dt3 = new Facade().GetReportByFromAndTo(strfd, strto, false, false, true);

        #endregion
        // Create a new PDF document
        PdfDocument document = new PdfDocument();
        

        // Create an empty page
        PdfPage page = document.AddPage();

        // Get an XGraphics object for drawing
        XGraphics gfx = XGraphics.FromPdfPage(page);

        int x = 30, y = 80;

        


        // Create a font
        XFont font_hdr = new XFont("Arial", 20, XFontStyle.Regular);
        XFont font = new XFont("Arial", 15, XFontStyle.Regular);
        XFont font_small = new XFont("Arial", 10, XFontStyle.Regular);

        XPen pen = new XPen(XColor.FromArgb(50, 0, 0, 0), 2);
        XPen totalPen = new XPen(XColor.FromName("black"), 1);
        

        XTextFormatter tf = new XTextFormatter(gfx);
        XTextFormatter tf1 = new XTextFormatter(gfx);
        XTextFormatter tf2 = new XTextFormatter(gfx);
        tf.Alignment = XParagraphAlignment.Right;
        tf1.Alignment = XParagraphAlignment.Center;
        tf2.Alignment = XParagraphAlignment.Left;
       
       
        int i = 0;
        XRect rec_hdr = new XRect(x + 200, y - 65, 300, 100);
        tf2.DrawString("Sales Statement", font_hdr, XBrushes.Black, rec_hdr);
        XRect rec = new XRect(x - 22, y - 35, 200, 100);
        tf.DrawString("Date Range: " + txtFromDate.Text + " to: " + txtToDate.Text + "", font_small, XBrushes.Black, rec);
        XRect rec_b = new XRect(x, y - 20, 200, 100);
        tf2.DrawString("Books", font, XBrushes.Black, rec_b);
        gfx.DrawLine(pen, 30, y + 3, 600, y + 3);
        gfx.DrawLine(pen, 30, y + 15, 600, y + 15);
        XRect rec1 = new XRect(x, y + 3, 570, 11);
        gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(120, 204, 204, 204)), rec1);
        tf2.DrawString("Article Code", font_small, XBrushes.Black, new XRect(x + 15, y + 5, 110, 100));
        tf.DrawString("Title", font_small, XBrushes.Black, new XRect(x + 70, y + 5, 110, 100));
        tf.DrawString("Quantity", font_small, XBrushes.Black, new XRect(x + 300, y + 5, 110, 100));
        tf.DrawString("Price", font_small, XBrushes.Black, new XRect(x + 400, y + 5, 110, 100));
        
                
        //double sum = dt1.Columns[2].com
        DataColumn sumofBookPrice = new DataColumn("sum", Type.GetType("System.Double"), "sum(Price)");
        dt1.Columns.Add(sumofBookPrice);
        

        DataColumn sumofBookQty = new DataColumn("qty", Type.GetType("System.Int32"), "sum(Quantity)");
        dt1.Columns.Add(sumofBookQty);
        
        DataColumn sumofCD = new DataColumn("sum", Type.GetType("System.Double"), "sum(Price)");
        dt2.Columns.Add(sumofCD);

        DataColumn sumofCDQty = new DataColumn("qty", Type.GetType("System.Int32"), "sum(Quantity)");
        dt2.Columns.Add(sumofCDQty);

        DataColumn sumofSheet = new DataColumn("sum", Type.GetType("System.Double"), "sum(Price)");
        dt3.Columns.Add(sumofSheet);

        DataColumn sumofSheetQty = new DataColumn("qty", Type.GetType("System.Int32"), "sum(Quantity)");
        dt3.Columns.Add(sumofSheetQty);

        try
        {
            bookPrice =Convert.ToDouble(dt1.Rows[0]["sum"].ToString());
            bookQty = Convert.ToInt32(dt1.Rows[0]["qty"].ToString());
        }
        catch
        {
            bookPrice = 0.0;
            bookQty = 0;
        }
        try
        {
            cdPrice = Convert.ToDouble(dt2.Rows[0]["sum"].ToString());
            cdQty = Convert.ToInt32(dt1.Rows[0]["qty"].ToString());
        }
        catch
        {
            cdPrice = 0.0;
            cdQty = 0;
        }
        try
        {
            sheetPrice =Convert.ToDouble( dt3.Rows[0]["sum"].ToString());
            sheetQty = Convert.ToInt32(dt3.Rows[0]["qty"].ToString());
        }
        catch
        {
            sheetPrice = 0.0;
            sheetQty = 0;
        }
        
        
        foreach (DataRow row in dt1.Rows)
        {
            y += 25;            
            tf2.DrawString(dt1.Rows[i]["ArticleCode"].ToString(), font_small, XBrushes.Black, new XRect(x + 20, y, 110, 100));
            tf2.DrawString(dt1.Rows[i]["Title"].ToString(), font_small, XBrushes.Black, new XRect(x + 90, y, 300, 100));
            tf.DrawString(dt1.Rows[i]["Quantity"].ToString(), font_small, XBrushes.Black, new XRect(x + 300, y, 110, 100));
            tf.DrawString(dt1.Rows[i]["Price"].ToString(), font_small, XBrushes.Black, new XRect(x + 400, y, 110, 100));
            i++;
            if (gfx.PageSize.Height - y <= 100)
            {
                page = document.AddPage();
                gfx =  XGraphics.FromPdfPage(page);
                tf = new XTextFormatter(gfx);
                tf1 = new XTextFormatter(gfx);
                tf2 = new XTextFormatter(gfx);
                tf.Alignment = XParagraphAlignment.Right;
                tf1.Alignment = XParagraphAlignment.Center;
                tf2.Alignment = XParagraphAlignment.Left;
                y = 50;
                ++pageCount;               
            }
        }        
        gfx.DrawLine(totalPen, 380, y + 15, 580, y + 15);
        y += 25;
        tf.DrawString("Total", font_small, XBrushes.Black, new XRect(x + 220, y, 110, 125));
        tf.DrawString(bookQty.ToString(), font_small, XBrushes.Black, new XRect(x + 300, y, 110, 125));
        tf.DrawString(bookPrice.ToString(), font_small, XBrushes.Black, new XRect(x + 400, y, 110, 125));
        gfx.DrawLine(pen, 30, y + 15, 600, y + 15);
        i = 0;
        y += 40;
        XRect rec_cd = new XRect(x, y, 200, 50);
        tf2.DrawString("CD/DVD", font, XBrushes.Black, rec_cd);
        gfx.DrawLine(pen, 30, y + 22, 600, y + 22);
        gfx.DrawLine(pen, 30, y + 36, 600, y + 36);
        XRect rec2 = new XRect(x, y + 22, 570, 15);
        gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(120, 204, 204, 204)), rec2);
        y += 20;

        tf2.DrawString("Article Code", font_small, XBrushes.Black, new XRect(x + 15, y + 5, 110, 100));
        tf.DrawString("Title", font_small, XBrushes.Black, new XRect(x + 70, y + 5, 110, 100));
        tf.DrawString("Quantity", font_small, XBrushes.Black, new XRect(x + 300, y + 5, 110, 100));
        tf.DrawString("Price", font_small, XBrushes.Black, new XRect(x + 400, y + 5, 110, 100));

        foreach (DataRow row in dt2.Rows)
        {
            y += 25;
            //x += 50;
            tf2.DrawString(dt2.Rows[i]["ArticleCode"].ToString(), font_small, XBrushes.Black, new XRect(x + 20, y, 110, 100));
            tf2.DrawString(dt2.Rows[i]["Title"].ToString(), font_small, XBrushes.Black, new XRect(x + 90, y, 300, 100));
            tf.DrawString(dt2.Rows[i]["Quantity"].ToString(), font_small, XBrushes.Black, new XRect(x + 300, y, 110, 100));
            tf.DrawString(dt2.Rows[i]["Price"].ToString(), font_small, XBrushes.Black, new XRect(x + 400, y, 110, 100));
            i++;
            if (gfx.PageSize.Height - y <= 100)
            {
                page = document.AddPage();
                gfx = XGraphics.FromPdfPage(page);
                tf = new XTextFormatter(gfx);
                tf1 = new XTextFormatter(gfx);
                tf2 = new XTextFormatter(gfx);
                tf.Alignment = XParagraphAlignment.Right;
                tf1.Alignment = XParagraphAlignment.Center;
                tf2.Alignment = XParagraphAlignment.Left;
                y = 50;
                ++pageCount;               
            }
        }

        gfx.DrawLine(totalPen, 380, y + 15, 580, y + 15);
        y += 25;
        tf.DrawString("Total", font_small, XBrushes.Black, new XRect(x + 220, y, 110, 125));
        tf.DrawString(cdQty.ToString(), font_small, XBrushes.Black, new XRect(x + 300, y, 110, 125));
        tf.DrawString(cdPrice.ToString(), font_small, XBrushes.Black, new XRect(x + 400, y, 110, 125));

        gfx.DrawLine(pen, 30, y + 15, 600, y + 15);
        i = 0;
        y += 40;
        
        
        XRect rec_sm = new XRect(x, y, 200, 50);
        tf2.DrawString("Sheet Music", font, XBrushes.Black, rec_sm);
        gfx.DrawLine(pen, 30, y + 22, 600, y + 22);
        gfx.DrawLine(pen, 30, y + 36, 600, y + 36);
        XRect rec3 = new XRect(x, y + 22, 570, 15);
        gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(120, 204, 204, 204)), rec3);
        y += 20;

        tf2.DrawString("Article Code", font_small, XBrushes.Black, new XRect(x + 15, y + 5, 110, 100));
        tf.DrawString("Title", font_small, XBrushes.Black, new XRect(x + 70, y + 5, 110, 100));
        tf.DrawString("Quantity", font_small, XBrushes.Black, new XRect(x + 300, y + 5, 110, 100));
        tf.DrawString("Price", font_small, XBrushes.Black, new XRect(x + 400, y + 5, 110, 100));

        foreach (DataRow row in dt3.Rows)
        {
            y += 25;
            tf2.DrawString(dt3.Rows[i]["ArticleCode"].ToString(), font_small, XBrushes.Black, new XRect(x + 20, y, 110, 100));
            tf2.DrawString(dt3.Rows[i]["Title"].ToString(), font_small, XBrushes.Black, new XRect(x + 90, y, 300, 100));
            tf.DrawString(dt3.Rows[i]["Quantity"].ToString(), font_small, XBrushes.Black, new XRect(x + 300, y, 110, 100));
            tf.DrawString(dt3.Rows[i]["Price"].ToString(), font_small, XBrushes.Black, new XRect(x + 400, y, 110, 100));
            i++;
            if (gfx.PageSize.Height - y <= 100)
            {
                page = document.AddPage();
                gfx = XGraphics.FromPdfPage(page);
                tf = new XTextFormatter(gfx);
                tf1 = new XTextFormatter(gfx);
                tf2 = new XTextFormatter(gfx);
                tf.Alignment = XParagraphAlignment.Right;
                tf1.Alignment = XParagraphAlignment.Center;
                tf2.Alignment = XParagraphAlignment.Left;
                y = 50;
                ++pageCount;              
            }
        }

        gfx.DrawLine(totalPen, 380, y + 15, 580, y + 15);
        y += 25;
        tf.DrawString("Total", font_small, XBrushes.Black, new XRect(x + 220, y, 110, 125));
        tf.DrawString(sheetQty.ToString(), font_small, XBrushes.Black, new XRect(x + 300, y, 110, 125));
        tf.DrawString(sheetPrice.ToString(), font_small, XBrushes.Black, new XRect(x + 400, y, 110, 125));

        gfx.DrawLine(pen, 30, y + 15, 600, y + 15);
        MemoryStream sm = new MemoryStream();
        document.Save(sm,false);

        PdfDocument finalDoc = PdfReader.Open(sm,PdfDocumentOpenMode.Import);
        PdfDocument outputDoc = new PdfDocument();
        for (int counter = 0; counter < document.Pages.Count; counter++)
        {
            PdfPage p = finalDoc.Pages[counter];
            outputDoc.AddPage(p);
            XGraphics xGraphics = XGraphics.FromPdfPage(outputDoc.Pages[counter]);

            tf = new XTextFormatter(xGraphics);
            tf1 = new XTextFormatter(xGraphics);
            tf2 = new XTextFormatter(xGraphics);
            tf.Alignment = XParagraphAlignment.Right;
            tf1.Alignment = XParagraphAlignment.Center;
            tf2.Alignment = XParagraphAlignment.Left;

            xGraphics.DrawString("Page " + (counter + 1) + " of " + pageCount + ".", font_small, XBrushes.Black, 30, xGraphics.PageSize.Height-25);
            xGraphics.Dispose();
        }
        

            

        MemoryStream stream = new MemoryStream();
        //document.Save(stream, false);
        outputDoc.Save(stream, false);
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-length", stream.Length.ToString());
        Response.BinaryWrite(stream.ToArray());
        Response.Flush();
        stream.Close();
        Response.End();
    }

    private void PrintVatAnalysis()
    {
        string strfd = txtFromDate.Text;
        DateTime dat = new DateTime();
        string[] strDate = strfd.Split('-');
        string date = strDate[0];
        string Month = strDate[1];
        string Year = strDate[2];
        strfd = Year + '-' + Month + '-' + date;
        string strto = txtToDate.Text;
        strDate = strto.Split('-');
        date = strDate[0];
        Month = strDate[1];
        Year = strDate[2];
        strto = Year + '-' + Month + '-' + date;


        dt = new Facade().GetVatAnalysis(strfd, strto); //dbHandler.GetDataTable(sql);
        //if (dt.Rows[0]["vat0"].ToString() == "")
        //    Label6.Text = "0.00";
        //else
        //    Label6.Text = dt.Rows[0]["vat0"].ToString();
        //if (dt.Rows[0]["vat6"].ToString() == "")
        //    Label7.Text = "0.00";
        //else
        //    Label7.Text = dt.Rows[0]["vat6"].ToString();
        //if (dt.Rows[0]["vat19"].ToString() == "")
        //    Label8.Text = "0.00";
        //else
        //    Label8.Text = dt.Rows[0]["vat19"].ToString();
        //if (dt.Rows[0]["price0"].ToString() == "")
        //    Label11.Text = "0.00";
        //else
        //    Label11.Text = dt.Rows[0]["price0"].ToString();
        //if (dt.Rows[0]["price6"].ToString() == "")
        //    Label12.Text = "0.00";
        //else
        //    Label12.Text = dt.Rows[0]["price6"].ToString();
        //if (dt.Rows[0]["price19"].ToString() == "")
        //    Label13.Text = "0.00";
        //else
        //    Label13.Text = dt.Rows[0]["price19"].ToString();


        // Create a new PDF document
        PdfDocument document = new PdfDocument();

        // Create an empty page
        PdfPage page = document.AddPage();

        // Get an XGraphics object for drawing
        XGraphics gfx = XGraphics.FromPdfPage(page);
        int x = 80, y = 110;
        // Create a font
        XFont font_hdr = new XFont("Arial", 20, XFontStyle.Regular);
        XFont font = new XFont("Arial", 13, XFontStyle.Regular);
        XFont font_small = new XFont("Arial", 12, XFontStyle.Regular);
        XFont font_smaller = new XFont("Arial", 10, XFontStyle.Regular);
        XRect rec_hdr = new XRect(180, 50, 200, 100);
        XRect rec = new XRect(-40, 85, 400, 50);
        XRect rec_vb = new XRect(x + 120, y, 110, 100);
        XRect rec_va = new XRect(x + 300, y, 150, 100);
        XRect rec_vf = new XRect(x, y + 25, 110, 100);
        XRect rec_v0 = new XRect(x + 120, y + 25, 100, 100);
        XRect rec_p0 = new XRect(x + 320, y + 25, 100, 100);
        XRect rec_vs = new XRect(x, y + 50, 110, 100);
        XRect rec_v6 = new XRect(x + 120, y + 50, 100, 100);
        XRect rec_p6 = new XRect(x + 320, y + 50, 100, 100);
        XRect rec_vn = new XRect(x, y + 75, 110, 100);
        XRect rec_v19 = new XRect(x + 120, y + 75, 100, 100);
        XRect rec_p19 = new XRect(x + 320, y + 75, 100, 100);
        XTextFormatter tf = new XTextFormatter(gfx);
        XTextFormatter tf1 = new XTextFormatter(gfx);
        XTextFormatter tf2 = new XTextFormatter(gfx);
        tf.Alignment = XParagraphAlignment.Right;
        tf1.Alignment = XParagraphAlignment.Center;
        tf2.Alignment = XParagraphAlignment.Left;
        // Draw the text
        //gfx.DrawString("Hello, World!", font, XBrushes.Black,new XRect(0, 0, page.Width, page.Height),XStringFormat.Center);
        gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(140, 204, 204, 204)), x - 30, y - 5, 500, 25);
        gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(80, 204, 204, 204)), x - 30, y + 20, 500, 75);
        tf1.DrawString("VAT Analysis", font_hdr, XBrushes.Black, rec_hdr);
        tf1.DrawString("Date Range: " + txtFromDate.Text + " to: " + txtToDate.Text + "", font, XBrushes.Black, rec);
        tf1.DrawString("VAT Basis", font, XBrushes.Black, rec_vb);
        tf1.DrawString("VAT Amount", font, XBrushes.Black, rec_va);
        tf2.DrawString("VAT free", font_small, XBrushes.Black, rec_vf);
        tf2.DrawString("VAT 6%", font_small, XBrushes.Black, rec_vs);
        tf2.DrawString("VAT 19%", font_small, XBrushes.Black, rec_vn);
        gfx.DrawString("€".PadRight(6), font_small, XBrushes.Black, rec_v0, XStringFormat.TopLeft);
        tf.DrawString(dt.Rows[0]["vat0"].ToString(), font_small, XBrushes.Black, rec_v0);
        gfx.DrawString("€".PadRight(6), font_small, XBrushes.Black, rec_v6, XStringFormat.TopLeft);
        tf.DrawString(dt.Rows[0]["vat6"].ToString(), font_small, XBrushes.Black, rec_v6);
        gfx.DrawString("€".PadRight(6), font_small, XBrushes.Black, rec_v19, XStringFormat.TopLeft);
        tf.DrawString(dt.Rows[0]["vat19"].ToString(), font_small, XBrushes.Black, rec_v19);
        //gfx.DrawString(Label11.Text, font, XBrushes.Black, x, y + 25, XStringFormat.TopLeft);
        gfx.DrawString("€".PadRight(6), font_small, XBrushes.Black, rec_p0, XStringFormat.TopLeft);
        tf.DrawString(dt.Rows[0]["price0"].ToString(), font_small, XBrushes.Black, rec_p0);
        gfx.DrawString("€".PadRight(6), font_small, XBrushes.Black, rec_p6, XStringFormat.TopLeft);
        tf.DrawString(dt.Rows[0]["price6"].ToString(), font_small, XBrushes.Black, rec_p6);
        gfx.DrawString("€".PadRight(6), font_small, XBrushes.Black, rec_p19, XStringFormat.TopLeft);
        tf.DrawString(dt.Rows[0]["price19"].ToString(), font_small, XBrushes.Black, rec_p19);
        gfx.DrawString("Page 1 of 1.", font_smaller, XBrushes.Black, 30, 780);
        // Save the document...
        //string filename = "HelloWorld.pdf";
        //document.Save(filename);
        // ...and start a viewer.
        MemoryStream stream = new MemoryStream();
        document.Save(stream, false);
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-length", stream.Length.ToString());
        Response.BinaryWrite(stream.ToArray());
        Response.Flush();
        stream.Close();
        Response.End();
    }
}
