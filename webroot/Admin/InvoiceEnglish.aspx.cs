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
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;
using System.Threading;


public partial class Admin_InvoiceEnglish : System.Web.UI.Page
{
    //PrintenInvoice invoiceWriter = new PrintenInvoice(arrayInvoice, sourcePath, destPath);
	ArrayList selectedInvoice;
    string[] culturalValue = new string[30];
	DbHandler dbHandler = new DbHandler();
	string cultureName = "nl-NL";
    Hashtable ht = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {
		if(!IsPostBack)
		{
            selectedInvoice = GetInvoiceIDs();
            SetCulture(GetCulture());
            try
            {
                DisplaySelectedInvoice(selectedInvoice);
            }
            catch (Exception ex)
            {
                Boeijenga.Common.Utils.LogWriter.Log(ex);
                throw ex;
            }
			SetCulturalValue();	

		}
    }

    private string GetCulture()
    {
        return Request.Params["lang"] != null ? Request.Params["lang"].ToString() : cultureName;
    }

    private ArrayList GetInvoiceIDs()
    {
        ArrayList invoiceIDs = new ArrayList();
        if (Request.Params["Factuurnr"] != null)
        {
            string []idList = Request.Params["Factuurnr"].Split(',');
            foreach (string id in idList)
            {
                invoiceIDs.Add(id);
            }
        }
        return invoiceIDs;
    }
	/*------------- Function Area-------------------------*/
	/*
	 * This function will set the culture value
	 * Author:Shahriar
	 * Date:17-7-07
	 */
	private string[] SetCulturalValue()
	{        
		string Invoice= "graphics/" + (string)base.GetGlobalResourceObject("string", "btnPreviewPdf");
        //string invoice_Report = (string)base.GetGlobalResourceObject("string", "invoice_Report");
        string invoice_Report = string.Empty;

        if (Request.Params["credit"] != null)
        {
            string[] credit = Request.Params["credit"].Split(new char[] { ',' });
            for (int i = 0; i < credit.Length; i++)
            {
                if (credit[i].Length > 0)
                {
                    ht.Add(selectedInvoice[i], (string)base.GetGlobalResourceObject("string", "credit_invoice"));
                }
                else
                {
                    ht.Add(selectedInvoice[i], (string)base.GetGlobalResourceObject("string", "invoice_Report"));
                }
            }
        }
        else
        {
            for (int i = 0; i < selectedInvoice.Count; i++)
            {
                ht.Add(selectedInvoice[i], (string)base.GetGlobalResourceObject("string", "invoice_Report"));
            }
        }




        //if (Request.Params["credit"]!=null && !Request.Params["credit"].ToString().Equals(string.Empty))
        //{
        //    invoice_Report = (string)base.GetGlobalResourceObject("string", "credit_invoice");
        //}
        //else
        //{
        //    invoice_Report = (string)base.GetGlobalResourceObject("string", "invoice_Report");
        //}
        culturalValue[0] = invoice_Report;
        string our_vat_report = (string)base.GetGlobalResourceObject("string", "our_vat_report");
        culturalValue[1] = our_vat_report;
        string bank_acc_report = (string)base.GetGlobalResourceObject("string", "bank_acc_report");
        culturalValue[2] = bank_acc_report;
        string for_payment_report = (string)base.GetGlobalResourceObject("string", "for_payment_report");
        culturalValue[3] = for_payment_report;
        string customer_id_report = (string)base.GetGlobalResourceObject("string", "customer_id_report");
        culturalValue[4] = customer_id_report;
        string invoice_no_report = (string)base.GetGlobalResourceObject("string", "invoice_no_report");
        culturalValue[5] = invoice_no_report;
        string invoice_date_report = (string)base.GetGlobalResourceObject("string", "invoice_date_report");
        culturalValue[6] = invoice_date_report;
        string vat_basis_report = (string)base.GetGlobalResourceObject("string", "vat_basis_report");
        culturalValue[7] = vat_basis_report;
        string vat_amount_report = (string)base.GetGlobalResourceObject("string", "vat_amount_report");
        culturalValue[8] = vat_amount_report;
        string vat_free_report = (string)base.GetGlobalResourceObject("string", "vat_free_report");
        culturalValue[9] = vat_free_report;
        string vat_6_report = (string)base.GetGlobalResourceObject("string", "vat_6_report");
        culturalValue[10] = vat_6_report;
        string vat_19_report = (string)base.GetGlobalResourceObject("string", "vat_19_report");
        culturalValue[11] = vat_19_report;
        string your_vat_no_report = (string)base.GetGlobalResourceObject("string", "your_vat_no_report");
        culturalValue[12] = your_vat_no_report;
        string summery_report = (string)base.GetGlobalResourceObject("string", "summery_report");
        culturalValue[13] = summery_report;
        string sub_total_report = (string)base.GetGlobalResourceObject("string", "sub_total_report");
        culturalValue[14] = sub_total_report;
        string shipping_cost_report = (string)base.GetGlobalResourceObject("string", "shipping_cost_report");
        culturalValue[15] = shipping_cost_report;
        string invoice_total_report = (string)base.GetGlobalResourceObject("string", "invoice_total_report");
        culturalValue[16] = invoice_total_report;
        string no_deduction_accepted_report = (string)base.GetGlobalResourceObject("string", "no_deduction_accepted_report");
        culturalValue[17] = no_deduction_accepted_report;
        string due_date_report = (string)base.GetGlobalResourceObject("string", "due_date_report");
        culturalValue[18] = due_date_report;
        string artical_report = (string)base.GetGlobalResourceObject("string", "artical_report");
        culturalValue[19] = artical_report;
        string des_report = (string)base.GetGlobalResourceObject("string", "des_report");
        culturalValue[20] = des_report;
        string qnty_report = (string)base.GetGlobalResourceObject("string", "qnty_report");
        culturalValue[21] = qnty_report;
        string retail_price_report = (string)base.GetGlobalResourceObject("string", "retail_price_report");
        culturalValue[22] = retail_price_report;
        string discount_report = (string)base.GetGlobalResourceObject("string", "discount_report");
        culturalValue[23] = discount_report;
        string net_price_report = (string)base.GetGlobalResourceObject("string", "net_price_report");
        culturalValue[24] = net_price_report;
        string vat_report = (string)base.GetGlobalResourceObject("string", "vat_report");
        culturalValue[25] = vat_report;
        string vat_only_report = (string)base.GetGlobalResourceObject("string", "vat_only_report");
        culturalValue[26] = vat_only_report;
        culturalValue[27] = cultureName;

        return culturalValue;
	}
	/*
	 * Thsi function will set culture according to customer 
	 * preference.if no preference found culture is default.
	 * 
	 */
	private void SetCulture(string cultureName)
	{
        //if (Session["cultureName"] != null)
        //{
        //    cultureName = Session["cultureName"].ToString();
        //}
        //else
        //{
        //    cultureName = HttpContext.Current.Request.UserLanguages[0];
        //    Session["cultureName"] = cultureName;
        //}
        //cultureName = HttpContext.Current.Request.UserLanguages[0];
        Session["cultureName"] = cultureName;
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
		Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
    }
    #region Report Generator
    /*
	 * This function will generate report
	 * Author:Shahriar
	 * Date:15-7-07
	 */
	private void ReportGenerator(PdfSharpPages report,string destination,DataTable dtBody,DataTable dtHead,string invoiceId)
	{
		Double totalShippingCost = 0.00;
		Double totalDiscount = 0.00;
		Double totalVat = 0.00;
		Double subTotal = 0.00;
		Double netPrice = 0.00;

		

		for (int i = 0; i < dtBody.Rows.Count; i++)
		{
			subTotal += Double.Parse(dtBody.Rows[i]["unitprice"].ToString()) * Double.Parse(dtBody.Rows[i]["quantity"].ToString());
			totalDiscount += ((Double.Parse(dtBody.Rows[i]["unitprice"].ToString()) * Double.Parse(dtBody.Rows[i]["quantity"].ToString())) * Double.Parse(dtBody.Rows[i]["discountpc"].ToString())) / 100;
			totalVat += ((Double.Parse(dtBody.Rows[i]["unitprice"].ToString()) * Double.Parse(dtBody.Rows[i]["quantity"].ToString())) * Double.Parse(dtBody.Rows[i]["vatpc"].ToString())) / 100;
		}
		
		string sql = "select sum(o.shippingcost) as total from orders o" +
					" where o.orderid in(select orderid from invoiceline where invoiceid='" + invoiceId + "')";

		DataTable dtShipCost = dbHandler.GetDataTable(sql);
		totalShippingCost = Double.Parse(dtShipCost.Rows[0]["total"].ToString());
		netPrice = subTotal - totalDiscount + totalVat + totalShippingCost;
		
		
		PdfPage page=new PdfPage();
		report.document.AddPage(page);
		report.gfx = XGraphics.FromPdfPage(page,XGraphicsPdfPageOptions.Append);
		
		double xStartColumn = 5;//starting x axis
		double yStartColumn =5;//starting y axis
		double unitColumnGap = 50;// column gap

		report.SetHorizontalPos(report._leftMargin);//initialize the horizontal position
		report.SetVerticalPos(report._lineGap * 2);//initialize the vertical position

		report.gfx.DrawString("Invoice # " + invoiceId, report._normalFontBold, XBrushes.Red, report.GetHorizontalPos(xStartColumn), report.GetVerticalPos(yStartColumn));
		report.DrawRightAlign("Invoice Date: " + dtHead.Rows[0]["invoicedate"].ToString(), report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + ((9 * unitColumnGap)), report.GetVerticalPos(0));

		report.gfx.DrawString("Customer :", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0), report.GetVerticalPos(4 * report._lineGap));
		report.gfx.DrawString(dtHead.Rows[0]["customer"].ToString().Trim(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap, report.GetVerticalPos(0));

		report.gfx.DrawString("Status:", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + ((7 * unitColumnGap)+10), report.GetVerticalPos(0));
		report.gfx.DrawString(dtHead.Rows[0]["status"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + ((7 * unitColumnGap) + 50), report.GetVerticalPos(0));

		report.gfx.DrawString("Customer BTW#:", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0), report.GetVerticalPos(2* report._lineGap));
		report.gfx.DrawString(dtHead.Rows[0]["customerbtwnr"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap, report.GetVerticalPos(0));

		report.gfx.DrawString("Total Shipping Cost:", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + ((7 * unitColumnGap) + 10), report.GetVerticalPos(0));
		report.gfx.DrawString("€ " + string.Format("{0:F2}", totalShippingCost), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + ((7 * unitColumnGap) +100), report.GetVerticalPos(0));

		report.gfx.DrawString("Invoice Address:", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0), report.GetVerticalPos(2 * report._lineGap));
		report.gfx.DrawString(dtHead.Rows[0]["housenr"].ToString() + "," + dtHead.Rows[0]["address"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (20 + unitColumnGap), report.GetVerticalPos(0));

		report.gfx.DrawString("Total Discount:", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + ((7 * unitColumnGap) + 10), report.GetVerticalPos(0));
		report.gfx.DrawString("€ " + string.Format("{0:F2}", totalDiscount), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + ((7 * unitColumnGap) + 100), report.GetVerticalPos(0));

		report.gfx.DrawString(dtHead.Rows[0]["postcode"].ToString() + "," + dtHead.Rows[0]["residence"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (20 + unitColumnGap), report.GetVerticalPos(1*report._lineGap));
		report.gfx.DrawString(dtHead.Rows[0]["country"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (20 + unitColumnGap), report.GetVerticalPos(1 * report._lineGap));
		//report.gfx.DrawString("", report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (20 + unitColumnGap), report.GetVerticalPos(1 * report._lineGap));

		report.gfx.DrawString("Total VAT:", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + ((7 * unitColumnGap) + 10), report.GetVerticalPos(1 * report._lineGap)-20);
		report.gfx.DrawString("€ " + string.Format("{0:F2}", totalVat), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + ((7 * unitColumnGap) + 100), report.GetVerticalPos(0)-20);
	
		//report.gfx.DrawString(dtHead.Rows[0]["residence"].ToString() + ",", report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (20 + unitColumnGap), report.GetVerticalPos(1 * report._lineGap));

		report.gfx.DrawString("Net Price:", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + ((7 * unitColumnGap) + 10), report.GetVerticalPos(1 * report._lineGap)-20);
		report.gfx.DrawString("€ " + string.Format("{0:F2}", netPrice), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + ((7 * unitColumnGap) + 100), report.GetVerticalPos(0)-20);

		unitColumnGap = (page.Width - (2 * (report._rightMargin + xStartColumn)));
		unitColumnGap /= 15;//dividing total width into 15 columns


		XPen pen = new XPen(XColors.Black, 1);
		pen.DashStyle = XDashStyle.Solid;

		double initXLineStart = report.GetHorizontalPos(0);
		double initYLineStart = report.GetVerticalPos(report._lineGap);

		// creating the table
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (page.Width - (2 * (report._rightMargin + xStartColumn))), report.GetVerticalPos(0));

		//adding the column headers
		report.DrawRightAlign("Order#", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap - 4, report.GetVerticalPos(report._lineGap));
		report.gfx.DrawString("Article", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap+4, report.GetVerticalPos(0));
		report.DrawRightAlign("Unit Price", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (9 * unitColumnGap), report.GetVerticalPos(0));
		report.DrawRightAlign("Quantity", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (10*unitColumnGap)+10, report.GetVerticalPos(0));
		report.DrawRightAlign("VAT(%)", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (11*unitColumnGap)+10, report.GetVerticalPos(0));
		report.DrawRightAlign("Discount(%)", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (12*unitColumnGap)+30, report.GetVerticalPos(0));
		report.DrawRightAlign("Shipping Cost", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (15 * unitColumnGap)-5, report.GetVerticalPos(0));

		report.gfx.DrawLine(pen, report.GetHorizontalPos(0), report.GetVerticalPos(report._lineGap), report.GetHorizontalPos(0) + (page.Width - (2 * (report._rightMargin + xStartColumn))), report.GetVerticalPos(0));

		int lineCounter = 1;

		foreach (DataRow row in dtBody.Rows)
		{

			report.DrawRightAlign(row["orderid"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap - 4, report.GetVerticalPos(report._lineGap));
			lineCounter = report.PrintMultipleLine(row["title"].ToString(), report._smallFontBold, XBrushes.Black, unitColumnGap *5.5, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0));
			lineCounter += report.PrintMultipleLine(row["Composer"].ToString(), report._smallFontItalic, XBrushes.Black, unitColumnGap *5.5, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0) + (report._lineGap * lineCounter));
			lineCounter += report.PrintMultipleLine(row["ArticleType"].ToString(), report._smallFont, XBrushes.Black, unitColumnGap * 5.5, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0) + (report._lineGap * lineCounter));

			report.DrawRightAlign("€ " +row["unitprice"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (9 * unitColumnGap), report.GetVerticalPos(0));
			report.DrawRightAlign(row["quantity"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (10* unitColumnGap), report.GetVerticalPos(0));
			report.DrawRightAlign(row["vatpc"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + ( 11* unitColumnGap), report.GetVerticalPos(0));
			report.DrawRightAlign(row["discountpc"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (12* unitColumnGap)+30, report.GetVerticalPos(0));
			report.DrawRightAlign("€ " + row["shippingcost"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (15 * unitColumnGap) - 5, report.GetVerticalPos(0));
			report.gfx.DrawLine(pen, report.GetHorizontalPos(0), report.GetVerticalPos(report._lineGap * lineCounter), report.GetHorizontalPos(0) + (page.Width - (2 * (report._rightMargin + xStartColumn))), report.GetVerticalPos(0));
			
		}
		//report.gfx.DrawLine(pen, report.GetHorizontalPos(0), report.GetVerticalPos(0)+unitColumnGap, initXLineStart, initYLineStart);
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0), report.GetVerticalPos(0), report.GetHorizontalPos(0), initYLineStart);
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + unitColumnGap, report.GetVerticalPos(0), report.GetHorizontalPos(0) + unitColumnGap, initYLineStart);

		report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (7.5*unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (7.5*unitColumnGap), initYLineStart);
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (9 * unitColumnGap)+4, report.GetVerticalPos(0), report.GetHorizontalPos(0) + (9*unitColumnGap)+4, initYLineStart);
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (10 * unitColumnGap) + 14, report.GetVerticalPos(0), report.GetHorizontalPos(0) + (10 * unitColumnGap) + 14, initYLineStart);
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (11 * unitColumnGap) + 15, report.GetVerticalPos(0), report.GetHorizontalPos(0) + (11 * unitColumnGap) + 15, initYLineStart);
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (12 * unitColumnGap) + 35, report.GetVerticalPos(0), report.GetHorizontalPos(0) + (12 * unitColumnGap) + 35, initYLineStart);
		//report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (14 * unitColumnGap) + 10, report.GetVerticalPos(0), report.GetHorizontalPos(0) + (14 * unitColumnGap) + 10, initYLineStart);
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (15 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (15 * unitColumnGap), initYLineStart);

    }
    #endregion

    /*
	 * This function will download report to client
	 * Date:15-7-07
	 */
	private void DownLoadPDF(string fileName)
	{
		try
		{
			FileInfo fx = new FileInfo(fileName);
			FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate,FileAccess.ReadWrite, FileShare.ReadWrite);
			byte[] data = new byte[(int)fs.Length];
			fs.Read(data, 0, (int)fs.Length);
			fs.Close();
			fs = null;
			fx.Delete();
			Response.Clear();
			Response.ContentType = "application/pdf";
			Response.BinaryWrite(data);
			Response.Flush();
			//Response.End();
		}
		catch (Exception ex)
		{
            Boeijenga.Common.Utils.LogWriter.Log(ex);
            Response.ContentType = "application/pdf";
			Response.Expires = -1;
			Response.Buffer = true;
			Response.Write("<html><pre>" + ex.ToString() +
				"</pre></html>");
		}
	}
	/*
	 * This function will parse the requested order number build a query and print PDF
	 * Author:Shahriar
	 * Date:15-7-07
	 */
	private void DisplaySelectedInvoice(ArrayList arrayInvoice)
	{
        string destination =  @"template.pdf";
        string destPath = MapPath(destination);
        string blank = "false";
        if (Session["blank"] != null)
        {
            blank = Session["blank"].ToString();
        }
        string sourceFile;
        if (blank.Equals("true"))
        {
            sourceFile = "../Templates/padpaper.pdf";
        }
        else
        {
            sourceFile = "../Templates/briefpapier.pdf";
        }
        Session["blank"] = "";
        string sourcePath = MapPath(sourceFile);
        System.IO.File.Copy(sourcePath, destPath, true);

        PrintenInvoice invoiceWriter = new PrintenInvoice(ht, sourcePath, destPath);
        //System.IO.File.Create(destPath);
        //SetCulture();
        invoiceWriter.CulturalValues = SetCulturalValue();
        //invoiceWriter
        invoiceWriter.path = destPath;
        invoiceWriter.Print();
        Response.Redirect(destination);
//      DownLoadPDF(destination);
	}


	/*
	 * Load Invoiceline Details According to Invoice ID
	 * Author:Shahriar
	 * Date:13-7-07
	 */

	private DataTable LoadInvoiceLine(string invoiceId)
	{
		string sql =" select ol.articlecode,il.invoiceid,"+
					" ol.orderid,o.discountpc,ol.vatpc,o.shippingcost,a.title ,"+
					" (case when lower(articletype)='c' then 'CD/DVD' when lower(articletype)='b' then 'Book' when lower(articletype)='s' then 'SheetMusic' end) as ArticleType,"+
					" (select coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'') from composer c,article a where c.composerid=a.composer and a.articlecode=ol.articlecode) as Composer,"+
					" ol.unitprice, ol.quantity "+
					" from orders o,ordersline ol,invoiceline il,invoice i ,Article a"+
					" where il.invoiceid=i.invoiceid "+
					" and a.articlecode=ol.articlecode"+
					" and o.orderid=ol.orderid "+
					" and il.orderid=ol.orderid "+
					" and ol.orderid in (select orderid from invoiceline where invoiceid='"+invoiceId+"')"+
					" order by ol.orderid";

		DataTable dtInvoiceLine = dbHandler.GetDataTable(sql);
		return dtInvoiceLine;
	}
	/*
	 * Load Invoiceline Customer info According to Invoice ID
	 * Author:Shahriar
	 * Date:15-7-07
	 */
	private DataTable LoadInvoiceLineInfo(string invoiceId)
	{
		string sql = "select i.invoiceid,to_char(i.invoicedate,'dd-mm-yyyy') as invoicedate," +
					"(" +
					"case when i.invoicestatus='1' then 'Boeken'" +
					"when invoicestatus='2' then 'Geboekt'" +
					"end" +
					")as status,invoicestatus,i.customerbtwnr," +
					" i.housenr,i.address,i.postcode,i.residence,(select countryname from country where countrycode=i.country) as country," +
					"(select COALESCE(firstname||' ','')||COALESCE(middlename||' ','')||COALESCE(lastname,'') from customer where customerid=i.customer) as customer" +
					" from invoice i where i.invoiceid='" + invoiceId + "'";

		DataTable dtInvoiceLineInfo = dbHandler.GetDataTable(sql);
		return dtInvoiceLineInfo;
		
	}
    //private void SetCulturalValue()
    //{
    //    btnQuickBuy.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnQuickBuy");
    //    btnSpotlight.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnSpotlight");
    //    btnDetail.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnDetail");
    //    btnsubscribe.ImageUrl = "graphics/" + (string)base.GetGlobalResourceObject("string", "btnSubscribe");
    //    RegularExpressionValidator1.ErrorMessage = (string)base.GetGlobalResourceObject("string", "emailValidMessage");
    //    RequiredFieldValidator1.ErrorMessage = (string)base.GetGlobalResourceObject("string", "requiredValidMessage");
    //    mailSuccesMessage = (string)base.GetGlobalResourceObject("string", "subscribeSuccessMassage");
    //    mailFailureMessage = (string)base.GetGlobalResourceObject("string", "subscribeFailureMassage");
    //    lblEmail.Text = (string)base.GetGlobalResourceObject("string", "lblEmail");
    //    headerContactInfo.Style.Add("background-image", "url(graphics/" + (string)base.GetGlobalResourceObject("string", "headerContactInformation") + ")");
    //}
}
