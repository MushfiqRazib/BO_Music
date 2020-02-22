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
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;
using Npgsql;
using System.Threading;
using System.Globalization;

/// <summary>
/// Summary description for transportlist
/// </summary>

public class PrintenInvoice
{
    DataTable dthead1 = new DataTable();
    DataTable dtPage = new DataTable();
    int pagenumber = 1;
    string totalpage = "";
    private ArrayList invoiceIdList;
    Hashtable ht;
    public string path = "";
    private TamplatePage report;
    public string[] CulturalValues = new string[30];
    public XFont totalFont = new XFont("Arial", 12, XFontStyle.Regular);
    NpgsqlConnection con = new NpgsqlConnection();
    string Today = DateTime.Today.ToString("dd-MM-yyyy");
    DbHandler dbHandler = new DbHandler();
    string iid = "";

    public PrintenInvoice(ArrayList ritId, string sourcePath, string destPath)
    {
        invoiceIdList = ritId;
        report = new TamplatePage(sourcePath, destPath);
    }
    public PrintenInvoice(Hashtable ht , string sourcePath, string destPath)
    {
        this.ht = ht;
        report = new TamplatePage(sourcePath, destPath);
    }
    public PrintenInvoice(ArrayList ritId)
    {
        PageProperties property = new PageProperties();
        invoiceIdList = ritId;
        //report = property.NewPage(PageSize.A4, PageOrientation.Portrait);
    }
    public void Print()
    {
        string invoiceId;
        int pageCounter = 0;

        foreach (DictionaryEntry de in ht)
        {
            if (pageCounter > 0)
            {
                report.NewTamplatePage();
                PdfPage page = new PdfPage();               
            }
            ++pageCounter;
            invoiceId = de.Key.ToString();
            this.CulturalValues[0] = de.Value.ToString();
            DataTable dtInvoiceDetails = GetInvoiceLineInfo(invoiceId);
            DataTable dtInvoiceCustomer = GetInvoiceInfo(invoiceId);
            DataTable dtVat = GetVatInfo(invoiceId);
            DataTable dtPrice = GetPriceInfo(invoiceId);
            dtPage = GetPageNumber(invoiceId);
            ReportGenerator(dtInvoiceDetails, dtInvoiceCustomer, dtVat, invoiceId, dtPrice);
            report.gfx.Dispose();
        }


        //for (int k = 0; k < invoiceIdList.Count; k++)
        //{
        //    if (k > 0)
        //    {
        //        report.NewTamplatePage();
        //        PdfPage page = new PdfPage();                
        //    }
        //    invoiceId = invoiceIdList[k].ToString();
        //    DataTable dtInvoiceDetails = GetInvoiceLineInfo(invoiceId);
        //    DataTable dtInvoiceCustomer = GetInvoiceInfo(invoiceId);
        //    DataTable dtVat = GetVatInfo(invoiceId);
        //    DataTable dtPrice = GetPriceInfo(invoiceId);
        //    dtPage = GetPageNumber(invoiceId);
            
        //    //if (this.credit != null && credit.Length>0)
        //    //{
        //    //    for (int i = 0; i < credit.Length; i++)
        //    //    {
        //    //        if (invoiceId.Equals(credit[i].ToString()))
        //    //        {
        //    //            this.CulturalValues[0] = System.Web.UI (string)base.GetGlobalResourceObject("string", "credit_invoice");
        //    //            break;
        //    //        }
        //    //        else
        //    //        {
        //    //            this.CulturalValues[0] = (string)base.GetGlobalResourceObject("string", "invoice_Report");
        //    //        }
        //    //    }
        //    //}
        //    ReportGenerator(dtInvoiceDetails, dtInvoiceCustomer, dtVat, invoiceId, dtPrice);
        //    report.gfx.Dispose();
        //}
        report.SaveFile(path);            
    }

    #region Report Generator
    /// <summary>
    /// This method will Generate the invoice as a pdf file.
    /// </summary>
    /// <param name="dtBody">this datatable will contain all article information in the invoice</param>
    /// <param name="dtHead">this datatable will contain all information related to this invoice</param>
    /// <param name="dtVat">this datatable will contain all types of VAT summary exist in the invoice</param>
    /// <param name="invoiceId">Supplied invoiceid</param>
    private void ReportGenerator(DataTable dtBody, DataTable dtHead, DataTable dtVat, string invoiceId, DataTable dtPrice)
    {
        Double totalShippingCost = 0.00;
        Double totalVat = 0.00;
        Double subTotal = 0.00;
        Double invoiceTotal = 0.00;

        for (int i = 0; i < dtBody.Rows.Count; i++)
        {
            subTotal += Double.Parse(dtBody.Rows[i][5].ToString());// +Double.Parse(dtBody.Rows[i][8].ToString());            
            totalVat += Double.Parse(dtBody.Rows[i][8].ToString());            
        }

        string sql = "select sum(o.shippingcost) as total from orders o" +
                    " where o.orderid in(select orderid from invoiceline where invoiceid='" + invoiceId + "')";

        DataTable dtShipCost = dbHandler.GetDataTable(sql);
        totalShippingCost = Double.Parse(dtShipCost.Rows[0]["total"].ToString());
        if (!dtHead.Rows[0]["customerbtwnr"].ToString().Equals(""))
        {
            invoiceTotal = subTotal;// +totalShippingCost;
            //invoiceTotal = subTotal + totalShippingCost;
        }
        else
            invoiceTotal = subTotal + totalVat;
        //Print Invoice
        XForm form = new XForm(report.document, XUnit.FromMillimeter(1000), XUnit.FromMillimeter(400));
        XGraphics graphics = XGraphics.FromForm(form);
        double xInit = 0, yInit = 0;
        graphics.DrawString(CulturalValues[0], report._largeFontBold, XBrushes.Black, xInit, yInit, XStringFormat.TopLeft);
        report.gfx.DrawImage(form, 390, 40);

        // Print Customer Address
        dthead1 = dtHead;
        form = new XForm(report.document, XUnit.FromMillimeter(1000), XUnit.FromMillimeter(400));
        graphics = XGraphics.FromForm(form);
        xInit = 95; yInit = 20;
        //graphics.DrawRectangle(report._borderPen, 0, 0, 300, 100);
        graphics.DrawString(dtHead.Rows[0]["companyname"].ToString().Trim(), report._normalFont, XBrushes.Black, xInit, yInit-report._lineGap, XStringFormat.TopLeft);
        graphics.DrawString(dtHead.Rows[0]["customer"].ToString().Trim(), report._normalFont, XBrushes.Black, xInit, yInit, XStringFormat.TopLeft);
        graphics.DrawString(dtHead.Rows[0]["address"].ToString() + "  " + dtHead.Rows[0]["housenr"].ToString(), report._normalFont, XBrushes.Black, xInit, yInit + report._lineGap, XStringFormat.TopLeft);
        graphics.DrawString(dtHead.Rows[0]["postcode"].ToString() + "  " + dtHead.Rows[0]["residence"].ToString(), report._normalFont, XBrushes.Black, xInit, yInit + report._lineGap * 2, XStringFormat.TopLeft);
        graphics.DrawString(dtHead.Rows[0]["country"].ToString(), report._normalFont, XBrushes.Black, xInit, yInit + report._lineGap * 3, XStringFormat.TopLeft);
        report.gfx.DrawImage(form, 245, 130);

        // Print Our VAT No + Bankaccount details
        form = new XForm(report.document, XUnit.FromMillimeter(1000), XUnit.FromMillimeter(400));
        graphics = XGraphics.FromForm(form);
        xInit = 5; yInit = 5;
        graphics.DrawRectangle(XBrushes.Gray, 0, 0, 300, 20);
        graphics.DrawString(CulturalValues[1], report._smallFont, XBrushes.White, xInit, yInit, XStringFormat.TopLeft);
        graphics.DrawString(CulturalValues[2], report._smallFont, XBrushes.White, xInit + 120, yInit, XStringFormat.TopLeft);
        graphics.DrawRectangle(new XSolidBrush(XColor.FromArgb(120, 204, 204, 204)), 0, 20, xInit + 115, 60);
        graphics.DrawString("NL006012152B01", report._smallFontBold, XBrushes.Black, 5, yInit + 25, XStringFormat.TopLeft);
        graphics.DrawRectangle(new XSolidBrush(XColor.FromArgb(80, 204, 204, 204)), xInit + 115, 20, 180, 60);
        graphics.DrawString("ABNAMRO:", report._smallFont, XBrushes.Black, xInit + 120, yInit + 25, XStringFormat.TopLeft);
        graphics.DrawString("61 26 26 032", report._smallFontBold, XBrushes.Black, xInit + 200, yInit + 25, XStringFormat.TopLeft);
        graphics.DrawString("IBAN:", report._smallFont, XBrushes.Black, xInit + 120, yInit + 25 + report._lineGap, XStringFormat.TopLeft);
        graphics.DrawString("NL08ABNA0612626032", report._smallFontBold, XBrushes.Black, xInit + 200, yInit + 25 + report._lineGap, XStringFormat.TopLeft);
        graphics.DrawString("BIC:", report._smallFont, XBrushes.Black, xInit + 120, yInit + 25 + report._lineGap * 2, XStringFormat.TopLeft);
        graphics.DrawString("ABNANL2A", report._smallFontBold, XBrushes.Black, xInit + 200, yInit + 25 + report._lineGap * 2, XStringFormat.TopLeft);
        report.gfx.DrawImage(form, 28, 260);

        //print For payment please quote
        iid = invoiceId;
        form = new XForm(report.document, XUnit.FromMillimeter(1000), XUnit.FromMillimeter(400));
        graphics = XGraphics.FromForm(form);
        xInit = 5; yInit = 5;
        graphics.DrawRectangle(XBrushes.Gray, 0, 0, 230, 20);
        graphics.DrawString(CulturalValues[3], report._smallFont, XBrushes.White, xInit, yInit, XStringFormat.TopLeft);
        graphics.DrawRectangle(new XSolidBrush(XColor.FromArgb(120, 204, 204, 204)), 0, 20, 230, 60);
        graphics.DrawString(CulturalValues[4], report._smallFont, XBrushes.Black, 5, yInit + 25, XStringFormat.TopLeft);
        graphics.DrawString(dtHead.Rows[0]["customerid"].ToString().Trim(), report._smallFontBold, XBrushes.Black, xInit + 100, yInit + 25, XStringFormat.TopLeft);
        graphics.DrawString(CulturalValues[5], report._smallFont, XBrushes.Black, 5, yInit + 25 + report._lineGap, XStringFormat.TopLeft);
        graphics.DrawString(invoiceId, report._smallFontBold, XBrushes.Black, xInit + 100, yInit + 25 + report._lineGap, XStringFormat.TopLeft);
        graphics.DrawString(CulturalValues[6], report._smallFont, XBrushes.Black, 5, yInit + 25 + report._lineGap * 2, XStringFormat.TopLeft);
        string s = dtHead.Rows[0]["invoicedate"].ToString();
        graphics.DrawString(s, report._smallFontBold, XBrushes.Black, xInit + 100, yInit + 25 + report._lineGap * 2, XStringFormat.TopLeft);
        graphics.DrawRectangle(report._borderPen, 0, 0, 230, 80);
        report.gfx.DrawImage(form, 333, 260);


        //VAT basis + VAT amount
        form = new XForm(report.document, XUnit.FromMillimeter(1000), XUnit.FromMillimeter(400));
        graphics = XGraphics.FromForm(form);
        xInit = 5; yInit = 5;
        double seg_width = 70;
        XTextFormatter tf = new XTextFormatter(graphics);

        graphics.DrawRectangle(XBrushes.Gray, 0, 0, 230, 20);
        graphics.DrawString(CulturalValues[7], report._smallFont, XBrushes.White, 98, yInit, XStringFormat.TopLeft);
        graphics.DrawString(CulturalValues[8], report._smallFont, XBrushes.White, 180, yInit, XStringFormat.TopLeft);
        graphics.DrawRectangle(new XSolidBrush(XColor.FromArgb(120, 204, 204, 204)), 0, 20, seg_width, 60);
        graphics.DrawRectangle(new XSolidBrush(XColor.FromArgb(80, 204, 204, 204)), seg_width, 20, seg_width, 60);
        graphics.DrawRectangle(new XSolidBrush(XColor.FromArgb(120, 204, 204, 204)), seg_width * 2, 20, 90, 60);
        graphics.DrawString(CulturalValues[9], report._smallFont, XBrushes.Black, 5, yInit + 25, XStringFormat.TopLeft);
        XRect rec = new XRect(seg_width, yInit + 25, 65, 20);
        tf.Alignment = XParagraphAlignment.Right;
        if (!dtHead.Rows[0]["customerbtwnr"].ToString().Equals(""))
        {
            Double vatBasis = double.Parse(dtPrice.Rows[0]["price0"].ToString()) + double.Parse(dtPrice.Rows[0]["price6"].ToString()) + double.Parse(dtPrice.Rows[0]["price19"].ToString());
            tf.DrawString("€".PadRight(25), report._smallFont, XBrushes.Black, rec);
            tf.DrawString(vatBasis.ToString("N2"), report._smallFont, XBrushes.Black, rec);
            rec = new XRect(seg_width * 2, yInit + 25, 85, 20);
            tf.DrawString("€".PadRight(25), report._smallFont, XBrushes.Black, rec);
            tf.DrawString("0.00", report._smallFont, XBrushes.Black, rec);
            graphics.DrawString(CulturalValues[10], report._smallFont, XBrushes.Black, 5, yInit + 25 + report._lineGap, XStringFormat.TopLeft);
            rec = new XRect(seg_width, yInit + 25 + report._lineGap, 65, 20);
            tf.DrawString("€".PadRight(25), report._smallFont, XBrushes.Black, rec);
            tf.DrawString("0.00", report._smallFont, XBrushes.Black, rec);
            rec = new XRect(seg_width * 2, yInit + 25 + report._lineGap, 85, 20);
            tf.DrawString("€".PadRight(25), report._smallFont, XBrushes.Black, rec);
            tf.DrawString("0.00", report._smallFont, XBrushes.Black, rec);
            graphics.DrawString(CulturalValues[11], report._smallFont, XBrushes.Black, 5, yInit + 25 + report._lineGap * 2, XStringFormat.TopLeft);

            rec = new XRect(seg_width, yInit + 25 + report._lineGap * 2, 65, 20);
            tf.DrawString("€".PadRight(25), report._smallFont, XBrushes.Black, rec);
            tf.DrawString("0.00", report._smallFont, XBrushes.Black, rec);
            rec = new XRect(seg_width * 2, yInit + 25 + report._lineGap * 2, 85, 20);
            tf.DrawString("€".PadRight(25), report._smallFont, XBrushes.Black, rec);
            tf.DrawString("0.00", report._smallFont, XBrushes.Black, rec);

            graphics.DrawString(CulturalValues[12], report._smallFont, XBrushes.Black, 5, yInit + 25 + report._lineGap * 4, XStringFormat.TopLeft);
            graphics.DrawString(dtHead.Rows[0]["customerbtwnr"].ToString(), report._smallFont, XBrushes.Black, 100, yInit + 25 + report._lineGap * 4, XStringFormat.TopLeft);
        }
        else
        {
            tf.DrawString("€".PadRight(25), report._smallFont, XBrushes.Black, rec);
            tf.DrawString(double.Parse(dtPrice.Rows[0]["price0"].ToString()).ToString("N2"), report._smallFont, XBrushes.Black, rec);
            rec = new XRect(seg_width * 2, yInit + 25, 85, 20);
            tf.DrawString("€".PadRight(25), report._smallFont, XBrushes.Black, rec);
            tf.DrawString(double.Parse(dtVat.Rows[0]["vat0"].ToString()).ToString("N2"), report._smallFont, XBrushes.Black, rec);
            graphics.DrawString(CulturalValues[10], report._smallFont, XBrushes.Black, 5, yInit + 25 + report._lineGap, XStringFormat.TopLeft);
            rec = new XRect(seg_width, yInit + 25 + report._lineGap, 65, 20);
            tf.DrawString("€".PadRight(25), report._smallFont, XBrushes.Black, rec);
            tf.DrawString(double.Parse(dtPrice.Rows[0]["price6"].ToString()).ToString("N2"), report._smallFont, XBrushes.Black, rec);
            rec = new XRect(seg_width * 2, yInit + 25 + report._lineGap, 85, 20);
            tf.DrawString("€".PadRight(25), report._smallFont, XBrushes.Black, rec);
            tf.DrawString(double.Parse(dtVat.Rows[0]["vat6"].ToString()).ToString("N2"), report._smallFont, XBrushes.Black, rec);
            graphics.DrawString(CulturalValues[11], report._smallFont, XBrushes.Black, 5, yInit + 25 + report._lineGap * 2, XStringFormat.TopLeft);

            rec = new XRect(seg_width, yInit + 25 + report._lineGap * 2, 65, 20);
            tf.DrawString("€".PadRight(25), report._smallFont, XBrushes.Black, rec);
            tf.DrawString(double.Parse(dtPrice.Rows[0]["price19"].ToString()).ToString("N2"), report._smallFont, XBrushes.Black, rec);
            rec = new XRect(seg_width * 2, yInit + 25 + report._lineGap * 2, 85, 20);
            tf.DrawString("€".PadRight(25), report._smallFont, XBrushes.Black, rec);
            tf.DrawString(double.Parse(dtVat.Rows[0]["vat19"].ToString()).ToString("N2"), report._smallFont, XBrushes.Black, rec);

            //graphics.DrawString(CulturalValues[12], report._smallFont, XBrushes.Black, 5, yInit + 25 + report._lineGap * 4, XStringFormat.TopLeft);
            //graphics.DrawString(dtHead.Rows[0]["customerbtwnr"].ToString(), report._smallFont, XBrushes.Black, 100, yInit + 25 + report._lineGap * 4, XStringFormat.TopLeft);
        }
        //report.gfx.DrawImage(form, 28, 720);

        //Page Number
        //string Show_page = "Page "+ s +" of "+seg_width +".";
        totalpage = dtPage.Rows[0]["page"].ToString();
        if (Convert.ToInt32(totalpage)>=1)
            pagenumber = 1;
        string Show_page = "Page " + pagenumber + " of " + totalpage +" (" + invoiceId + ").";
        pagenumber += 1;
        //graphics.DrawString(Show_page, report._smallFont, XBrushes.Black, 5, yInit + 25 + report._lineGap * 4, XStringFormat.TopLeft);
        graphics.DrawString(Show_page, report._smallFont, XBrushes.Black, 5, yInit + 90, XStringFormat.TopLeft);
        report.gfx.DrawImage(form, 28, 710);
        //if (dtHead.Rows[0]["customerbtwnr"].ToString().Equals(""))

        //Sub Total
        form = new XForm(report.document, XUnit.FromMillimeter(1000), XUnit.FromMillimeter(400));
        graphics = XGraphics.FromForm(form);
        tf = new XTextFormatter(graphics);
        tf.Alignment = XParagraphAlignment.Right;
        xInit = 5; yInit = 5;
        graphics.DrawRectangle(XBrushes.Gray, 0, 0, 230, 20);
        graphics.DrawString(CulturalValues[13], report._smallFont, XBrushes.White, xInit, yInit, XStringFormat.TopLeft);
        graphics.DrawRectangle(new XSolidBrush(XColor.FromArgb(80, 204, 204, 204)), 100, 20, 130, 60);
        graphics.DrawString(CulturalValues[14], report._smallFont, XBrushes.Black, 5, yInit + 25, XStringFormat.TopLeft);
        graphics.DrawString(CulturalValues[26], report._smallFont, XBrushes.Black, 5, yInit + 25 + report._lineGap, XStringFormat.TopLeft);
        //graphics.DrawString("VAT: ", report._smallFontBold, XBrushes.Black, 5, yInit + 25 + report._lineGap, XStringFormat.TopLeft); 
        graphics.DrawString(CulturalValues[16], report._smallFontBold, XBrushes.Black, 5, yInit + 25 + report._lineGap * 2, XStringFormat.TopLeft);
        rec = new XRect(100, yInit + 25, 100, 20);
        tf.DrawString("€".PadRight(25), report._smallFont, XBrushes.Black, rec);
        tf.DrawString(subTotal.ToString("N2"), report._smallFont, XBrushes.Black, rec);
        rec = new XRect(100, yInit + 25 + report._lineGap, 100, 20);
        tf.DrawString("€".PadRight(25), report._smallFont, XBrushes.Black, rec);
        if (!dtHead.Rows[0]["customerbtwnr"].ToString().Equals(""))
        {
            tf.DrawString("0.00", report._smallFont, XBrushes.Black, rec);
        }
        else
            tf.DrawString(totalVat.ToString("N2"), report._smallFont, XBrushes.Black, rec);
        rec = new XRect(100, yInit + 25 + report._lineGap * 2, 100, 20);
        tf.DrawString("€".PadRight(25), report._smallFontBold, XBrushes.Black, rec);
        tf.DrawString(invoiceTotal.ToString("N2"), report._smallFontBold, XBrushes.Black, rec);
        graphics.DrawString(CulturalValues[17], report._verySmallFont, XBrushes.Black, 115, yInit + 25 + report._lineGap * 3, XStringFormat.TopLeft);
        graphics.DrawString(CulturalValues[18], report._smallFont, XBrushes.Black, 5, yInit + 25 + report._lineGap * 4, XStringFormat.TopLeft);

        System.Globalization.CultureInfo enUS = new System.Globalization.CultureInfo("en-US", true);
        System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.DateTimeFormatInfo();
        dtfi.ShortDatePattern = "dd-MM-yyyy";
        dtfi.DateSeparator = "-";

        DateTime dtIn = Convert.ToDateTime(s, dtfi);

        dtIn = dtIn.AddDays(14);
        graphics.DrawString(dtIn.ToString("dd-MM-yyyy"), report._smallFont, XBrushes.Black, 115, yInit + 25 + report._lineGap * 4, XStringFormat.TopLeft);
        graphics.DrawRectangle(report._borderPen, 0, 0, 230, 80);
        report.gfx.DrawImage(form, 333, 710);

        //Populate Article Details

        double[] colWidth = { 50, 220, 40, 60, 55, 60, 50 };
        //string[] colName = {"Article ID", "Description", "Quantity", "Retail Price", "Discount", "Net Price", "VAT" };
        form = new XForm(report.document, XUnit.FromMillimeter(1000), XUnit.FromMillimeter(400));
        graphics = XGraphics.FromForm(form);
        PrintArticleDetail(colWidth, ref form, dtBody);
        report.gfx.DrawImage(form, 28, 350);
    }
    #endregion

    #region Print Article details
    /// <summary>
    /// This method will print the Article detail table with appropriate 
    /// column header according to the template
    /// </summary>
    /// <param name="colwidth">different column width</param>
    /// <param name="form">Supplied XForm</param>
    /// <param name="dtArticle">supplied datatable containing article information</param>
    private void PrintArticleDetail(double[] colwidth, ref XForm form, DataTable dtArticle)
    {
        XGraphics graphics = XGraphics.FromForm(form);
        XTextFormatter tf = new XTextFormatter(graphics);
        XTextFormatter tf1 = new XTextFormatter(graphics);
        XRect rec;
        int pagecounter = 0;
        double left=0, top=0;
        // Print header
        for (int index = 0; index < colwidth.Length; index++)
        {
            rec = new XRect(left, top, colwidth[index], 19);
            /*if (index % 2 == 0)
            {
                graphics.DrawRectangle(new XSolidBrush(XColor.FromArgb(120, 204, 204, 204)), rec);
            }
            else
            {*/
            graphics.DrawRectangle(XBrushes.Gray, rec);
            //}
            string header = dtArticle.Columns[index].ColumnName;
            int count = header.Length;
            if (dtArticle.Columns[index].DataType.FullName.Equals("System.String"))
            {
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString(header.PadLeft(count + 3), report._smallFontBold, XBrushes.White, rec);
            }
            else
            {
                tf.Alignment = XParagraphAlignment.Center;
                tf.DrawString(header.PadRight(count + 3), report._smallFontBold, XBrushes.White, rec);
            }
            left += colwidth[index];

        }
        //print Articles 
        foreach (DataRow row in dtArticle.Rows)
        {
            pagecounter++;
            top += 20;
            left = 0;
            if (pagecounter % 15 == 0)
            {
                //top = -500;
                //report.document.AddPage();
                report.gfx.DrawImage(form, 28, 350);
                top = 0;
                PdfPage page = new PdfPage();
                page = report.document.Pages.Add();
                report.gfx = XGraphics.FromPdfPage(page);
                form = new XForm(report.document, XUnit.FromMillimeter(1000), XUnit.FromMillimeter(400));
                graphics = XGraphics.FromForm(form);
                tf = new XTextFormatter(graphics);
                tf1 = new XTextFormatter(graphics);
                //Print Invoice
                XForm form1 = new XForm(report.document, XUnit.FromMillimeter(1000), XUnit.FromMillimeter(400));
                XGraphics graphics1 = XGraphics.FromForm(form1);
                double xInit = 0, yInit = 0;
                graphics1.DrawString(CulturalValues[0], report._largeFontBold, XBrushes.Black, xInit, yInit, XStringFormat.TopLeft);
                report.gfx.DrawImage(form1, 450, 40);
                // Print Customer Address
                form1 = new XForm(report.document, XUnit.FromMillimeter(1000), XUnit.FromMillimeter(400));
                graphics1 = XGraphics.FromForm(form1);
                xInit = 95; yInit = 20;
                //graphics.DrawRectangle(report._borderPen, 0, 0, 300, 100);
                graphics1.DrawString(dthead1.Rows[0]["companyname"].ToString().Trim(), report._normalFont, XBrushes.Black, xInit, yInit - report._lineGap, XStringFormat.TopLeft);
                graphics1.DrawString(dthead1.Rows[0]["customer"].ToString().Trim(), report._normalFont, XBrushes.Black, xInit, yInit, XStringFormat.TopLeft);
                graphics1.DrawString(dthead1.Rows[0]["address"].ToString() + "  " + dthead1.Rows[0]["housenr"].ToString(), report._normalFont, XBrushes.Black, xInit, yInit + report._lineGap, XStringFormat.TopLeft);
                graphics1.DrawString(dthead1.Rows[0]["postcode"].ToString() + "  " + dthead1.Rows[0]["residence"].ToString(), report._normalFont, XBrushes.Black, xInit, yInit + report._lineGap * 2, XStringFormat.TopLeft);
                graphics1.DrawString(dthead1.Rows[0]["country"].ToString(), report._normalFont, XBrushes.Black, xInit, yInit + report._lineGap * 3, XStringFormat.TopLeft);
                report.gfx.DrawImage(form1, 245, 130);

                // Print Our VAT No + Bankaccount details
                form1 = new XForm(report.document, XUnit.FromMillimeter(1000), XUnit.FromMillimeter(400));
                graphics1 = XGraphics.FromForm(form1);
                xInit = 5; yInit = 5;
                graphics1.DrawRectangle(XBrushes.Gray, 0, 0, 300, 20);
                graphics1.DrawString(CulturalValues[1], report._smallFont, XBrushes.White, xInit, yInit, XStringFormat.TopLeft);
                graphics1.DrawString(CulturalValues[2], report._smallFont, XBrushes.White, xInit + 120, yInit, XStringFormat.TopLeft);
                graphics1.DrawRectangle(new XSolidBrush(XColor.FromArgb(120, 204, 204, 204)), 0, 20, xInit + 115, 60);
                graphics1.DrawString("NL006012152B01", report._smallFontBold, XBrushes.Black, 5, yInit + 25, XStringFormat.TopLeft);
                graphics1.DrawRectangle(new XSolidBrush(XColor.FromArgb(80, 204, 204, 204)), xInit + 115, 20, 180, 60);
                graphics1.DrawString("ABNAMRO:", report._smallFont, XBrushes.Black, xInit + 120, yInit + 25, XStringFormat.TopLeft);
                graphics1.DrawString("61 26 26 032", report._smallFontBold, XBrushes.Black, xInit + 200, yInit + 25, XStringFormat.TopLeft);
                graphics1.DrawString("IBAN:", report._smallFont, XBrushes.Black, xInit + 120, yInit + 25 + report._lineGap, XStringFormat.TopLeft);
                graphics1.DrawString("NL08ABNA0612626032", report._smallFontBold, XBrushes.Black, xInit + 200, yInit + 25 + report._lineGap, XStringFormat.TopLeft);
                graphics1.DrawString("BIC:", report._smallFont, XBrushes.Black, xInit + 120, yInit + 25 + report._lineGap * 2, XStringFormat.TopLeft);
                graphics1.DrawString("ABNANL2A", report._smallFontBold, XBrushes.Black, xInit + 200, yInit + 25 + report._lineGap * 2, XStringFormat.TopLeft);
                report.gfx.DrawImage(form1, 28, 260);


                //print For payment please quote
                form1 = new XForm(report.document, XUnit.FromMillimeter(1000), XUnit.FromMillimeter(400));
                graphics1 = XGraphics.FromForm(form1);
                xInit = 5; yInit = 5;
                graphics1.DrawRectangle(XBrushes.Gray, 0, 0, 230, 20);
                graphics1.DrawString(CulturalValues[3], report._smallFont, XBrushes.White, xInit, yInit, XStringFormat.TopLeft);
                graphics1.DrawRectangle(new XSolidBrush(XColor.FromArgb(120, 204, 204, 204)), 0, 20, 230, 60);
                graphics1.DrawString(CulturalValues[4], report._smallFont, XBrushes.Black, 5, yInit + 25, XStringFormat.TopLeft);
                graphics1.DrawString(dthead1.Rows[0]["customerid"].ToString().Trim(), report._smallFontBold, XBrushes.Black, xInit + 100, yInit + 25, XStringFormat.TopLeft);
                graphics1.DrawString(CulturalValues[5], report._smallFont, XBrushes.Black, 5, yInit + 25 + report._lineGap, XStringFormat.TopLeft);
                graphics1.DrawString(iid, report._smallFontBold, XBrushes.Black, xInit + 100, yInit + 25 + report._lineGap, XStringFormat.TopLeft);
                graphics1.DrawString(CulturalValues[6], report._smallFont, XBrushes.Black, 5, yInit + 25 + report._lineGap * 2, XStringFormat.TopLeft);
                string s = dthead1.Rows[0]["invoicedate"].ToString();
                graphics1.DrawString(s, report._smallFontBold, XBrushes.Black, xInit + 100, yInit + 25 + report._lineGap * 2, XStringFormat.TopLeft);
                graphics1.DrawRectangle(report._borderPen, 0, 0, 230, 80);
                report.gfx.DrawImage(form1, 333, 260);

                form1 = new XForm(report.document, XUnit.FromMillimeter(1000), XUnit.FromMillimeter(400));
                graphics1 = XGraphics.FromForm(form1);
                string Show_page = "Page " + pagenumber + " of " + totalpage + " (" + iid + ").";
                pagenumber += 1;
                //graphics.DrawString(Show_page, report._smallFont, XBrushes.Black, 5, yInit + 25 + report._lineGap * 4, XStringFormat.TopLeft);
                graphics1.DrawString(Show_page, report._smallFont, XBrushes.Black, 5, yInit + 90, XStringFormat.TopLeft);
                report.gfx.DrawImage(form1, 28, 710);


                for (int index = 0; index < colwidth.Length; index++)
                {
                    rec = new XRect(left, top, colwidth[index], 19);
                    /*if (index % 2 == 0)
                    {
                        graphics.DrawRectangle(new XSolidBrush(XColor.FromArgb(120, 204, 204, 204)), rec);
                    }
                    else
                    {*/
                    graphics.DrawRectangle(XBrushes.Gray, rec);
                    //}
                    string header = dtArticle.Columns[index].ColumnName;
                    int count = header.Length;
                    if (dtArticle.Columns[index].DataType.FullName.Equals("System.String"))
                    {
                        tf.Alignment = XParagraphAlignment.Left;
                        tf.DrawString(header.PadLeft(count + 3), report._smallFontBold, XBrushes.White, rec);
                    }
                    else
                    {
                        tf.Alignment = XParagraphAlignment.Center;
                        tf.DrawString(header.PadRight(count + 3), report._smallFontBold, XBrushes.White, rec);
                    }
                    left += colwidth[index];

                }
                left = 0;
                top += 20;
                //rec;
                //report.document.AddPage(page);
                //report.gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);
            }
            for (int index = 0; index < colwidth.Length; index++)
            {
                //tf.Alignment = (dtArticle.Columns[index].DataType.FullName.Equals("System.String")) ? XParagraphAlignment.Left : XParagraphAlignment.Right;
                rec = new XRect(left, top, colwidth[index], 20);
                XRect rec1 = new XRect(left + 5, top, colwidth[index], 20);
                if (index % 2 == 0)
                {
                    graphics.DrawRectangle(new XSolidBrush(XColor.FromArgb(120, 204, 204, 204)), rec);
                }
                else
                {
                    graphics.DrawRectangle(new XSolidBrush(XColor.FromArgb(60, 204, 204, 204)), rec);
                }
                int count = row[index].ToString().Length;
                if (dtArticle.Columns[index].DataType.FullName.Equals("System.String"))
                {
                    tf.Alignment = XParagraphAlignment.Left;
                    //tf1.Alignment = XParagraphAlignment.Right;
                    if(dtArticle.Columns[index].ToString().Equals(CulturalValues[19]))
                        tf.DrawString(row[index].ToString().PadLeft(count + 3), report._smallFont, XBrushes.Black, rec);
                    if (dtArticle.Columns[index].ToString().Equals(CulturalValues[20]))
                    {
                        if (row[index].ToString().Trim().Equals("Shipping and handling"))
                        {
                            if (CulturalValues[27] == "nl-NL")
                                tf.DrawString("Verzendkosten".ToString(), report._smallFont, XBrushes.Black, rec1);
                            else
                                tf.DrawString("Shipping and handling".ToString(), report._smallFont, XBrushes.Black, rec1);
                        }
                        else
                            tf.DrawString(row[index].ToString() + " - " + row["Composer"].ToString(), report._smallFont, XBrushes.Black, rec1);
                    }
                }
                else
                {
                    tf.Alignment = XParagraphAlignment.Right;
                    tf.DrawString(row[index].ToString().PadRight(count + 3), report._smallFont, XBrushes.Black, rec);
                    if (dtArticle.Columns[index].ToString().Equals(CulturalValues[22]) || dtArticle.Columns[index].ToString().Equals(CulturalValues[24]))
                        tf.DrawString("€".PadRight(22), report._smallFont, XBrushes.Black, rec);
                }
                //tf.DrawString(row[index].ToString(), report._smallFont, XBrushes.Black, rec);
                left += colwidth[index];
            }
        }
    }
    #endregion

    #region Get InvoiceLine information
    /// <summary>
    /// this method will load the information about invoiceline related to the supplied invoiceid
    /// </summary>
    /// <param name="invoiceId">Supplied invoiceid</param>
    /// <returns>DataTable with invoiceline information</returns>
    private DataTable GetInvoiceLineInfo(string invoiceId)
    {
        //string article_id = CulturalValues[19];
        string sql = @"select ol.articlecode as """ + CulturalValues[19] + @""", a.title as """ + CulturalValues[20] + @""",
                    (case when i.credit is null then ol.quantity else ol.creditedquantity*-1 end) as """ + CulturalValues[21] + @""",
                    ol.unitprice as """ + CulturalValues[22] + @""",
                    coalesce( (case when a.articlecode='z001' then 0.00 else ol.discountpc end),0) as """ + CulturalValues[23] + @""", 
		            round((ol.unitprice-round(ol.unitprice*ol.discountpc/100,2))*(case when i.credit is null then ol.quantity else ol.creditedquantity*-1 end),2)
		            as """ + CulturalValues[24] + @""",
                    ol.vatpc as """ + CulturalValues[25] + @""",
                    coalesce(case when ol.articlecode='z001' then 0.00 else round((ol.unitprice*ol.quantity*ol.discountpc )/100,2)end) as discount,
                    round((((ol.unitprice)-((ol.unitprice)*(coalesce( (case when ol.articlecode='z001' then 0.00 else 
                    (case when ol.discountpc not in(0.00) then ol.discountpc else 0.00 end) end),0))/100))*ol.vatpc)/100,2)*(case when i.credit is null then ol.quantity else ol.creditedquantity*-1 end) as vat,
                    il.invoiceid, ol.orderid,ol.vatpc,o.shippingcost, 
                    (case 
                        when lower(articletype)='c' then 'CD/DVD' 
                        when lower(articletype)='b' then 'Book' 
                        when lower(articletype)='s' then 'SheetMusic' 
                     end) as ArticleType, 
                     (select coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'') 
                     from composer c,article a 
                     where c.composerid=a.composer and a.articlecode=ol.articlecode) as Composer
                     from orders o,ordersline ol,invoiceline il,invoice i ,Article a 
                     where il.invoiceid=i.invoiceid
                     and a.articlecode=ol.articlecode 
                     and o.orderid=ol.orderid  
                     and il.orderid=ol.orderid 
                     and (case when i.credit is null then ol.quantity>0 else ol.creditedquantity>0 end)
                     and i.invoiceid in (" + invoiceId + @") order by a.articlecode;";

        /*
        @"select ol.articlecode as ""Article ID"", a.title as ""Description"",ol.quantity as ""Quantity"",ol.unitprice as ""Retail Price"",
            round((ol.quantity* ol.unitprice)*o.discountpc/100,2) as ""Discount"", round(ol.quantity* ol.unitprice*(1-o.discountpc/100),2) as  ""Net Price"", round(ol.quantity* ol.unitprice*(1-o.discountpc/100)*ol.vatpc/100,2) as ""VAT"",il.invoiceid, ol.orderid,ol.vatpc,o.shippingcost, 
                (case 
                when lower(articletype)='c' then 'CD/DVD' 
                when lower(articletype)='b' then 'Book' 
                when lower(articletype)='s' then 'SheetMusic' 
                 end) as ArticleType, 
                (select coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'') 
                 from composer c,article a 
                 where c.composerid=a.composer 
                   and a.articlecode=ol.articlecode) as Composer
    
            from orders o,ordersline ol,invoiceline il,invoice i ,Article a 
            where il.invoiceid=i.invoiceid  
              and a.articlecode=ol.articlecode 
              and o.orderid=ol.orderid  
              and il.orderid=ol.orderid  
              and ol.orderid in 
               (select orderid 
                from invoiceline 
                where invoiceid=" + invoiceId+@") 
            order by ol.orderid;";
         */

        DataTable dtInvoiceLine = dbHandler.GetDataTable(sql);
        return dtInvoiceLine;
    }
    #endregion

    #region Get Invoice Information
    /// <summary>
    /// this method will Load the information related to the supplied invoiceid
    /// </summary>
    /// <param name="invoiceId">Supplied invoiceid</param>
    /// <returns>datatable invoice with information</returns>
    private DataTable GetInvoiceInfo(string invoiceId)
    {
        string sql = @"select i.invoiceid,i.customer as customerid, to_char(i.invoicedate,'dd-mm-yyyy') as invoicedate,
                    (case when i.invoicestatus='1' then 'Boeken' 
                    when invoicestatus='2' then 'Geboekt'
                    end)as status,invoicestatus,i.customerbtwnr,
                    i.housenr,i.address,i.postcode,i.residence,(select countryname from country where countrycode=i.country) as country,(select companyname from customer where customerid=i.customer) as companyname,
                    (select COALESCE(initialname||' ','')||COALESCE(firstname||' ','')||COALESCE(middlename||' ','')||COALESCE(lastname,'') from customer where customerid=i.customer) as customer
                     from invoice i where i.invoiceid=" + invoiceId;

        /*string sql = "select i.invoiceid,i.customer as customerid, to_char(i.invoicedate,'dd-mm-yyyy') as invoicedate," +
                    "(" +
                    "case when i.invoicestatus='1' then 'Boeken'" +
                    "when invoicestatus='2' then 'Geboekt'" +
                    "end" +
                    ")as status,invoicestatus,i.customerbtwnr," +
                    " i.housenr,i.address,i.postcode,i.residence,(select countryname from country where countrycode=i.country) as country," +
                    "(select COALESCE(firstname||' ','')||COALESCE(middlename||' ','')||COALESCE(lastname,'') from customer where customerid=i.customer) as customer" +
                    " from invoice i where i.invoiceid='" + invoiceId + "'";
        */
        DataTable dtInvoiceLineInfo = dbHandler.GetDataTable(sql);
        return dtInvoiceLineInfo;

    }
    #endregion

    #region Get VAT Information
    /// <summary>
    /// This method will Load different types of VAT informaiton related to the supplied invoiceid
    /// </summary>
    /// <param name="invoiceid">Supplied invoiceid</param>
    /// <returns>DataTable with Different VAT information</returns>
    private DataTable GetVatInfo(string invoiceid)
    {
//        string sqlVat = @"select  sum(case when ol.vatpc=0.00 then round((((ol.unitprice)-((ol.unitprice)*(coalesce( (case when ol.articlecode='z001' then 0.00 else (case when ol.discountpc not in(0.00) then ol.discountpc else 0.00 end) end),0))/100))*ol.vatpc)/100,2)*ol.quantity else 0.00 end) as vat0, 
//                                  sum(case when ol.vatpc=6.00 then round((((ol.unitprice)-((ol.unitprice)*(coalesce( (case when ol.articlecode='z001' then 0.00 else (case when ol.discountpc not in(0.00) then ol.discountpc else 0.00 end) end),0))/100))*ol.vatpc)/100,2)*ol.quantity else 0.00 end) as vat6, 
//                                  sum(case when ol.vatpc=19.00 then round((((ol.unitprice)-((ol.unitprice)*(coalesce( (case when ol.articlecode='z001' then 0.00 else (case when ol.discountpc not in(0.00) then ol.discountpc else 0.00 end) end),0))/100))*ol.vatpc)/100,2)*ol.quantity else 0.00 end) as vat19
//                              from invoiceline as il
//                              left join orders as o on il.orderid = o.orderid 
//                              left join ordersline as ol on il.orderid=ol.orderid
//                              where il.invoiceid=" + invoiceid;
        string sqlVat = @"select  sum(case when ol.vatpc=0.00 then round((((ol.unitprice)-((ol.unitprice)*(coalesce( (case when ol.articlecode='z001' then 0.00 else (case when ol.discountpc not in(0.00) then ol.discountpc else 0.00 end) end),0))/100))*ol.vatpc)/100,2)*(case when i.credit is null then ol.quantity else ol.creditedquantity*-1 end) else 0.00 end) as vat0, 
                                  sum(case when ol.vatpc=6.00 then round((((ol.unitprice)-((ol.unitprice)*(coalesce( (case when ol.articlecode='z001' then 0.00 else (case when ol.discountpc not in(0.00) then ol.discountpc else 0.00 end) end),0))/100))*ol.vatpc)/100,2)*(case when i.credit is null then ol.quantity else ol.creditedquantity*-1 end) else 0.00 end) as vat6, 
                                  sum(case when ol.vatpc=19.00 then round((((ol.unitprice)-((ol.unitprice)*(coalesce( (case when ol.articlecode='z001' then 0.00 else (case when ol.discountpc not in(0.00) then ol.discountpc else 0.00 end) end),0))/100))*ol.vatpc)/100,2)*(case when i.credit is null then ol.quantity else ol.creditedquantity*-1 end) else 0.00 end) as vat19
                              from invoice as i left join  invoiceline as il on i.invoiceid = il.invoiceid
                              left join orders as o on il.orderid = o.orderid 
                              left join ordersline as ol on il.orderid=ol.orderid
                              where il.invoiceid="+invoiceid;

        return dbHandler.GetDataTable(sqlVat);
    }
    #endregion

    #region Get Price Information
    /// <summary>
    /// This method will Load different types of Price informaiton related to the supplied invoiceid
    /// </summary>
    /// <param name="invoiceid">Supplied invoiceid</param>
    /// <returns>DataTable with Different Price information</returns>
    private DataTable GetPriceInfo(string invoiceid)
    {
        string sqlPrice = @"
        select  sum(case when ol.vatpc=0.00 then round((ol.unitprice-round(ol.unitprice*ol.discountpc/100,2))*(case when i.credit is null then ol.quantity else ol.creditedquantity*-1 end),2) else 0.00 end) as price0, 
        sum(case when ol.vatpc=6.00 then round((ol.unitprice-round(ol.unitprice*ol.discountpc/100,2))*(case when i.credit is null then ol.quantity else ol.creditedquantity*-1 end),2)else 0.00 end) as price6, 
        sum(case when ol.vatpc=19.00 then round((ol.unitprice-round(ol.unitprice*ol.discountpc/100,2))*(case when i.credit is null then ol.quantity else ol.creditedquantity*-1 end),2)else 0.00 end) as price19
                                   from invoice as i left join  invoiceline as il on i.invoiceid = il.invoiceid
	                            left join orders as o on il.orderid = o.orderid
	                            left join ordersline as ol on il.orderid=ol.orderid
                            where il.invoiceid=" + invoiceid;
        return dbHandler.GetDataTable(sqlPrice);
    }
    #endregion


    #region Get Page Number
    /// <summary>
    /// This method will Load the number of pages required related to the supplied invoiceid
    /// </summary>
    /// <param name="invoiceid">Supplied invoiceid</param>
    /// <returns>DataTable with total page required</returns>
    private DataTable GetPageNumber(string invoiceid)
    {
        string sqlPrice = @"
                            select ceiling((count(ol.orderid) :: numeric)/15) as page from ordersline ol
                               where ol.orderid in (select orderid from invoiceline
                                    where invoiceid = " + invoiceid + ")";
        return dbHandler.GetDataTable(sqlPrice);
    }
    #endregion

    public void PrintPageNumber()
    {
        report.SetHorizontalPos(0);
        report.SetVerticalPos(0);            
        PdfPages pages = report.document.Pages;
        report.gfx.Dispose();
       
        int j = 1;

        for (int i = 0; i < pages.Count; i++)
        {

            if ((i + 1) == j * 10)
            {
                j *= 10;
                report.GetHorizontalPos(5);
            }

            XGraphics gfx = XGraphics.FromPdfPage(pages[i]);
            gfx.DrawString(report._pageNumber.ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + 540, report.GetVerticalPos(0) + 81.30);
            gfx.Dispose();
        }
    }

}