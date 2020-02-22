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
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;
using Boeijenga.DataAccess.Constants;
using Boeijenga.Business;
using Boeijenga.Common.Objects;

public partial class Admin_printOrder : System.Web.UI.Page
{
	DbHandler dbHandler = new DbHandler();
    protected void Page_Load(object sender, EventArgs e)
    {
		if (Request.Params["order"] != null)
		{
			string orderNo = Request.Params["order"];			
			CreateReport(orderNo);

		}
		//btnPrint.Attributes.Add("OnClick","javascript:window.print()");

    }
    
    /// <summary>
    /// method that creates the pdf with pdfsharp
    /// </summary>
    /// <param name="order">the order number</param>
    protected void CreateReport(string order)
    {
      
        string shippingCost = "";
 
        DataTable dtDelivery = new Facade().GetDeliveryInfowithCustomerNameByOrderId(order);

        string dCustomer = dtDelivery.Rows[0]["customer"].ToString();
        string dHouse = dtDelivery.Rows[0]["house"].ToString();
        string dPostcode = dtDelivery.Rows[0]["postcode"].ToString();
        string dAddress = dtDelivery.Rows[0]["Address"].ToString();

        shippingCost = dtDelivery.Rows[0]["shippingcost"].ToString();
        string ShippingCostValue = "€" + dtDelivery.Rows[0]["shippingcost"].ToString();
        string RemarksValue = dtDelivery.Rows[0]["remarks"].ToString().Trim();
       // string DiscountpcValue = dtDelivery.Rows[0]["discountpc"].ToString();
        string OrderDateValue = dtDelivery.Rows[0]["orderdate"].ToString();

        DataTable dtInvoice = new Facade().GetInvoiceInfowithCustomerNameByOrderId(order);
        string customer = dtInvoice.Rows[0]["customer"].ToString();
        string house = dtInvoice.Rows[0]["house"].ToString();
        string postcode = dtInvoice.Rows[0]["postcode"].ToString();
        string address = dtInvoice.Rows[0]["address"].ToString();

        string today = "";

        double xStartColumn = 10;//starting x axis
        double yStartColumn = 10;//starting y axis
        double unitColumnGap = 60;// column gap
        string destination = ConfigurationManager.AppSettings["resources"].ToString() + "pdf\\template.pdf";

        PdfSharpPages report = new PdfSharpPages();      
        
        PdfPage page = report.document.AddPage();
        report.gfx = XGraphics.FromPdfPage(page);

        report.SetHorizontalPos(report._leftMargin);//initialize the horizontal position
        report.GetVerticalPos(report._lineGap * 5);//initialize the vertical position
        report.gfx.DrawString("Order No# " + order, report._normalFontBold, XBrushes.Red, report.GetHorizontalPos(xStartColumn), report.GetVerticalPos(yStartColumn));
        report.gfx.DrawString("Order Date: " + OrderDateValue, report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (6.0 * unitColumnGap), report.GetVerticalPos(0));
        report.gfx.DrawString("Customer :", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0), report.GetVerticalPos(4 * report._lineGap));
        report.gfx.DrawString("Delivery Address :", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (4.5 * unitColumnGap), report.GetVerticalPos(0));
        report.gfx.DrawString(customer, report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap, report.GetVerticalPos(0));
        report.gfx.DrawString(dCustomer, report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (6 * unitColumnGap), report.GetVerticalPos(0));

        report.gfx.DrawString(house, report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap, report.GetVerticalPos(report._lineGap));
        report.gfx.DrawString(dHouse, report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (6 * unitColumnGap), report.GetVerticalPos(0));

        report.gfx.DrawString(postcode, report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap, report.GetVerticalPos(report._lineGap));
        report.gfx.DrawString(dPostcode, report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (6 * unitColumnGap), report.GetVerticalPos(0));
        report.gfx.DrawString(address, report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap, report.GetVerticalPos(report._lineGap));
        report.gfx.DrawString(dAddress, report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (6 * unitColumnGap), report.GetVerticalPos(0));


        //report.gfx.DrawString("Discount(%) :", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0), report.GetVerticalPos(2 * report._lineGap));
        //report.gfx.DrawString(DiscountpcValue, report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap, report.GetVerticalPos(0));
        report.gfx.DrawString("Remarks :", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0), report.GetVerticalPos(report._lineGap));
        report.gfx.DrawString(RemarksValue, report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap, report.GetVerticalPos(0));
 
        //contents from database
 
        //Adding Discount Amount and VAT Amount

        DataTable dt = new Facade().GetDiscountAmountandVATAmountByOrderId(order);

        double grandTotal = 0.00;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            grandTotal += Double.Parse(dt.Rows[i]["NETPrice"].ToString());
        }
        string TotalValue = "€ " + string.Format("{0:F2}",grandTotal);
       // Double shipCost = System.Math.Round((double.Parse(shippingCost) * grandTotal) / 100, 2);
        //ShippingCostValue = "€ " + string.Format("{0:F2}", shipCost);
        string GrandTotalValue = "€ " + string.Format("{0:F2}",(grandTotal ));
        DataColumn index;


        index = new DataColumn();
        index.DataType = System.Type.GetType("System.Int32");
        index.ColumnName = "index";

        dt.Columns.Add(index);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            row["index"] = i + 1;
        }
        //getting the workspace width (gap between left margin and right margin)
        unitColumnGap = (page.Width - (2 * (report._rightMargin + xStartColumn)));
        unitColumnGap /= 15;//dividing total width into 15 columns



        XPen pen = new XPen(XColors.Black, 1);
        pen.DashStyle = XDashStyle.Solid;

        //draw first horizontal line for table
        double initXLineStart = report.GetHorizontalPos(0);
        double initYLineStart = report.GetVerticalPos(report._lineGap * 2);       

        // creating the table
        report.gfx.DrawLine(pen, report.GetHorizontalPos(0), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (page.Width - (2 * (report._rightMargin + xStartColumn))), report.GetVerticalPos(0));

        //adding the column headers
        report.DrawRightAlign("Serial#", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap - 4, report.GetVerticalPos(report._lineGap));
        report.gfx.DrawString("Article", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0));
        report.DrawRightAlign("QTY", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (7.5 * unitColumnGap) - 4, report.GetVerticalPos(0));
        report.DrawRightAlign("Stock", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (8.5 * unitColumnGap) - 4, report.GetVerticalPos(0));
        report.DrawRightAlign("Price(Excl.VAT)", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (10.5 * unitColumnGap) - 4, report.GetVerticalPos(0));
        report.DrawRightAlign("Discount", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (12 * unitColumnGap) - 4, report.GetVerticalPos(0));
        report.DrawRightAlign("VAT", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (13 * unitColumnGap) - 4, report.GetVerticalPos(0));
        report.DrawRightAlign("Net Price", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (15 * unitColumnGap) - 4, report.GetVerticalPos(0));

        //report.gfx.DrawLine(pen, report.GetHorizontalPos(0), report.GetVerticalPos(report._lineGap), report.GetHorizontalPos(0) + (page.Width - (2 * (report._rightMargin + xStartColumn))), report.GetVerticalPos(0));

        int lineCounter = 1;//counts the number of lines that will dynamically assigns        
        int pageCount=0;
        foreach (DataRow row in dt.Rows)
        {
            //report.gfx.DrawLine(pen, report.GetHorizontalPos(0), report.GetVerticalPos(report._lineGap), report.GetHorizontalPos(0) + (page.Width - (2 * (report._rightMargin + xStartColumn))), report.GetVerticalPos(0));
            pageCount++;
            report.DrawRightAlign(row["index"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap - 4, report.GetVerticalPos(report._lineGap * 1));
            lineCounter = report.PrintMultipleLine(row["title"].ToString(), report._smallFontBold, XBrushes.Black, unitColumnGap * 5.5, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0));
            lineCounter += report.PrintMultipleLine(row["author"].ToString(), report._smallFontItalic, XBrushes.Black, unitColumnGap * 5.5, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0) + (report._lineGap * lineCounter));
            lineCounter += report.PrintMultipleLine(row["type"].ToString(), report._smallFont, XBrushes.Black, unitColumnGap * 5.5, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0) + (report._lineGap * lineCounter));
            report.DrawRightAlign(row["qty"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (7.5 * unitColumnGap) - 4, report.GetVerticalPos(0));
            report.DrawRightAlign(row["stock"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (8.5 * unitColumnGap) - 4, report.GetVerticalPos(0));
            report.DrawRightAlign("€ " + row["unitprice"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (10.5 * unitColumnGap) - 4, report.GetVerticalPos(0));
            report.DrawRightAlign("€ " + row["discountAmount"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (12 * unitColumnGap) - 4, report.GetVerticalPos(0));
            report.DrawRightAlign("€ " + row["vatAmount"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (13 * unitColumnGap) - 4, report.GetVerticalPos(0));
            report.DrawRightAlign("€ " + row["NETprice"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (15 * unitColumnGap) - 4, report.GetVerticalPos(0));
            report.gfx.DrawLine(pen, report.GetHorizontalPos(0), report.GetVerticalPos((report._lineGap-2) * lineCounter), report.GetHorizontalPos(0) + (page.Width - (2 * (report._rightMargin + xStartColumn))), report.GetVerticalPos(0));

            report.gfx.DrawLine(pen, report.GetHorizontalPos(0), report.GetVerticalPos(0), initXLineStart, initYLineStart);
            report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + unitColumnGap, report.GetVerticalPos(0), report.GetHorizontalPos(0) + unitColumnGap, initYLineStart);
            report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (6.5 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (6.5 * unitColumnGap), initYLineStart);
            report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (7.5 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (7.5 * unitColumnGap), initYLineStart);
            report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (8.5 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (8.5 * unitColumnGap), initYLineStart);
            report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (10.5 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (10.5 * unitColumnGap), initYLineStart);
            report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (12 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (12 * unitColumnGap), initYLineStart);
            report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (13 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (13 * unitColumnGap), initYLineStart);
            report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (15 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (15 * unitColumnGap), initYLineStart);

            if (pageCount%12 == 0)
            {
                page = report.document.Pages.Add();
                report.gfx = XGraphics.FromPdfPage(page);
                lineCounter = 1;
                report.SetVerticalPos(110);
                report.gfx.DrawLine(pen, report.GetHorizontalPos(0), report.GetVerticalPos(report._lineGap), report.GetHorizontalPos(0) + (page.Width - (2 * (report._rightMargin + xStartColumn))), report.GetVerticalPos(0));
                initYLineStart = 123;
            }

        }
        //report.gfx.DrawLine(pen, report.GetHorizontalPos(0), report.GetVerticalPos(0), initXLineStart, initYLineStart);
        //report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + unitColumnGap, report.GetVerticalPos(0), report.GetHorizontalPos(0) + unitColumnGap, initYLineStart);
        //report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (6.5 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (6.5 * unitColumnGap), initYLineStart);
        //report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (7.5 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (7.5 * unitColumnGap), initYLineStart);
        //report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (8.5 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (8.5 * unitColumnGap), initYLineStart);
        //report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (10.5 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (10.5 * unitColumnGap), initYLineStart);
        //report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (12 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (12 * unitColumnGap), initYLineStart);
        //report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (13 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (13 * unitColumnGap), initYLineStart);
        //report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (15 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (15 * unitColumnGap), initYLineStart);

        //report.gfx.DrawLine(pen, report.GetHorizontalPos(0), report.GetVerticalPos(0) - initLineStart, report.GetHorizontalPos(0), report.GetVerticalPos(0));

        report.DrawRightAlign("Sub Total: ", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (13 * unitColumnGap), report.GetVerticalPos(report._lineGap));
        report.DrawRightAlign(TotalValue, report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (15 * unitColumnGap) - 4, report.GetVerticalPos(0));
        //report.DrawRightAlign("Shipping Cost: ", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (13 * unitColumnGap), report.GetVerticalPos(report._lineGap));
        //report.DrawRightAlign(ShippingCostValue, report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (15 * unitColumnGap) - 4, report.GetVerticalPos(0));
        report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (13 * unitColumnGap), report.GetVerticalPos(report._lineGap), report.GetHorizontalPos(0) + (15 * unitColumnGap), report.GetVerticalPos(0));
        report.DrawRightAlign("Total Amount: ", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (13 * unitColumnGap), report.GetVerticalPos(report._lineGap));
        report.DrawRightAlign(GrandTotalValue, report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (15 * unitColumnGap) - 4, report.GetVerticalPos(0));


       // Response.Write("<script language = javascript> window.open(\"" + destination + "\",\"\",\"resizable= yes,width=660, height=434,status=no,toolbar=yes,menubar=yes,location=no\" )</script> ");
             
        //report.DeleteExistFile(destination);       
        report.SaveFile(destination);
        //destination = ConfigurationManager.AppSettings["web-resources"].ToString() + "pdf/template.pdf";

       // Response.Write("<script language = javascript> window.open(\"" + destination + "\",\"PrintOrder\",\"resizable= yes,width=660, height=434,status=1,toolbar=yes,menubar=yes,location=no\" )</script> ");
       // string pdfName = ConfigurationManager.AppSettings["web-resources"].ToString() + "pdf/" + btnPreviewPdf.CommandArgument.ToString();
        //Response.Write("<script language = javascript> window.open(\"" + pdfName + "\",\"\",\"resizable= yes,width=660, height=434,status=no,toolbar=yes,menubar=yes,location=no\" )</script> ");
      
        /*
         *end of code for pdfsharp 
         */
        DownLoadPDF(destination);
           
    }
    private void DownLoadPDF(string fileName)
    {
        //Download to client
        try
        {
            FileInfo fx = new FileInfo(fileName);
            FileStream fs = new FileStream(fileName, FileMode.Open,
            FileAccess.Read, FileShare.ReadWrite);
            byte[] data = new byte[(int)fs.Length];
            fs.Read(data, 0, (int)fs.Length);
            fs.Close();
            fs = null;
            //fx.Delete();
            Response.Clear();
            Response.ContentType = "application/pdf";
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + tablename + ".csv");
            Response.BinaryWrite(data);
            Response.Flush();
            Response.End();
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
}
