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
using System.Threading;

public partial class Admin_stockPrint : System.Web.UI.Page
{
	DbHandler dbHandler = new DbHandler();
	string cultureName = "nl-NL";
	string sql="";
    protected void Page_Load(object sender, EventArgs e)
    {
		if(!IsPostBack)
		{
			if (Request.Params["orderNo"] != null)
			{
				string orderId=Request.Params["orderNo"].ToString();
				GenerateReport(orderId);
			}
		}
    }
	private void GenerateReport(string orderId)
	{
		sql = @"select ro.supplyorderid,rol.articlecode,a.title,
				(case when lower(a.articletype)='b' then 'Book'
				when lower(a.articletype)='c' then 'CD/DVD'
				when lower(a.articletype)='s' then 'SheetMusic'
				end
				) as articletype,
				coalesce(c.firstName,'')||' '||coalesce(c.middleName,'') ||' '||coalesce(c.lastname,'') as author,
				a.quantity as stock,
				sol.orderqty as OrderQty,rol.receiveid as ID,
				to_char(ro.receivedate,'dd-mm-yyyy')as receivedate,
				rol.receiveqty as PreviousReceive,sol.orderqty-
				(
					select sum(receiveqty) from receiveordersline rol_in 
					where rol_in.articlecode=rol.articlecode
					and rol_in.receiveid in(select receiveid from receiveorders where supplyorderid=" + orderId+@")
					group by rol_in.articlecode
				)as balance
				from receiveorders ro,receiveordersline rol,supplyordersline sol,article a,composer c
				where ro.receiveid=rol.receiveid 
				and ro.supplyorderid=sol.supplyorderid
				and rol.articlecode=sol.articlecode
				and rol.articlecode=a.articlecode
				and c.composerid=a.composer
				and ro.supplyorderid="+orderId+
				@"group by ro.supplyorderid,rol.articlecode,a.title,a.articletype,c.firstName,c.middleName,c.lastName,a.quantity,sol.orderqty,rol.receiveqty,rol.receiveid,ro.receivedate";
		DataTable dt = dbHandler.GetDataTable(sql);

		sql = @"select so.supplyorderid ,to_char(so.supplyorderdate,'dd-mm-yyyy') as orderdate,to_char(so. deliverydate,'dd-mm-yyyy') as deldate,
			(
				case when lower(so.receivingstatus)='n'then 'Not Received'
				when lower(so.receivingstatus)='p'then 'Partial Received'
				when lower(so.receivingstatus)='f'then 'Full Received'
				end
			)as RcvStatus,
			(
				case when lower(so.paymentstatus)='u' then 'Unpaid'
				when lower(so.paymentstatus)='p' then 'Partial paid'
				when lower(so.paymentstatus)='f' then 'Full paid'
				end
			) as PayStatus,
			coalesce(p.firstname||' ','')||coalesce(p.middlename||' ','') || coalesce(p.lastname||' ','') as SupplierName,
			coalesce(p.housenr||',','')||coalesce(p.address||'','') as supplierAddress1,
			coalesce(p.postcode||',','')||coalesce(p.residence||'','')as supplierAddress2,
			(select countryname from country where lower(countrycode)=lower(coalesce(p.country,'NL'))) as supplierCountry,
			coalesce(so.dhousenr||',','')||coalesce(so.daddress||'','') as  deliveryAddress1,
			coalesce(so.dpostcode||',','')||coalesce(so.dresidence||'','') as deliveryAddress2,
			(select countryname from country where lower(countrycode)=lower(coalesce(so.dcountry,'NL'))) as deliveryCountry
			from supplyorders so,publisher p
			where so.supplierid=p.publisherid 
			and so.supplyorderid=" + orderId;

		DataTable dtInfo=dbHandler.GetDataTable(sql);

		string destination = ConfigurationManager.AppSettings["resources"].ToString() + @"pdf\template.pdf";
		System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);

		PdfSharpPages report = new PdfSharpPages();

		PdfPage page = new PdfPage();
		report.document.AddPage(page);
		report.gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);

		double xStartColumn = 10;//starting x axis
		double yStartColumn = 10;//starting y axis
		double unitColumnGap = 60;// column gap

		report.SetHorizontalPos(report._leftMargin);//initialize the horizontal position
		report.SetVerticalPos(report._lineGap * 5);//initialize the vertical position

		report.gfx.DrawString("Order # " + dtInfo.Rows[0]["supplyorderid"].ToString(), report._normalFont, XBrushes.Red, report.GetHorizontalPos(xStartColumn), report.GetVerticalPos(yStartColumn));


		report.DrawRightAlign("Order Date: " + dtInfo.Rows[0]["orderdate"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (7 * unitColumnGap)+12, report.GetVerticalPos(0));


		report.gfx.DrawString("Delivery Date: " + dtInfo.Rows[0]["deldate"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (5 * unitColumnGap)+38, report.GetVerticalPos(0) + 15);

		report.gfx.DrawString("Delivery Status: " + dtInfo.Rows[0]["RcvStatus"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0), report.GetVerticalPos(0)+15);

		report.gfx.DrawString("Payment Status :" + dtInfo.Rows[0]["PayStatus"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0), report.GetVerticalPos(0)+30);

		report.gfx.DrawString("Supplier: ", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0), report.GetVerticalPos(0) + (4*report._lineGap));
		report.gfx.DrawString(dtInfo.Rows[0]["SupplierName"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0)+50, report.GetVerticalPos(0) + (4 * report._lineGap));
		report.gfx.DrawString(dtInfo.Rows[0]["supplierAddress1"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap-10, report.GetVerticalPos(0) + (5 * report._lineGap));
		report.gfx.DrawString(dtInfo.Rows[0]["supplierAddress2"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap-10, report.GetVerticalPos(0) + (6 * report._lineGap));
		report.gfx.DrawString(dtInfo.Rows[0]["supplierCountry"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap-10, report.GetVerticalPos(0) + (7 * report._lineGap)-20);

		report.gfx.DrawString("Delivery Address: ", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (5 * unitColumnGap) + 25, report.GetVerticalPos(0) + (3 * report._lineGap));
		report.gfx.DrawString(dtInfo.Rows[0]["deliveryAddress1"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (6 * unitColumnGap)+35, report.GetVerticalPos(0) + (3 * report._lineGap));
		report.gfx.DrawString(dtInfo.Rows[0]["deliveryAddress2"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (7 * unitColumnGap)-25 , report.GetVerticalPos(0) + (4 * report._lineGap));
		report.gfx.DrawString(dtInfo.Rows[0]["deliveryCountry"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (7 * unitColumnGap)-25 , report.GetVerticalPos(0) + (5 * report._lineGap));
		



		unitColumnGap = (page.Width - (2 * (report._rightMargin + xStartColumn)));
		unitColumnGap /= 15;//dividing total width into 15 columns

		XPen pen = new XPen(XColors.Black, 1);
		pen.DashStyle = XDashStyle.Solid;

        //XPen pen1 = new XPen(XColors.Black, 1);
        //pen1.DashStyle = XDashStyle.Solid;
		double initXLineStart = report.GetHorizontalPos(0);
		double initYLineStart = report.GetVerticalPos(report._lineGap * 8);    

		// creating the table
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0)-20, report.GetVerticalPos(0), report.GetHorizontalPos(0) + (page.Width - (2 * (report._rightMargin + xStartColumn))), report.GetVerticalPos(0));

		//adding the column headers
		report.DrawRightAlign("Article Code", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap - 4, report.GetVerticalPos(report._lineGap));
		report.gfx.DrawString("Article", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + 40, report.GetVerticalPos(0));
		report.DrawRightAlign("Current Stock", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (8 * unitColumnGap), report.GetVerticalPos(0));
		report.DrawRightAlign("Order Qty", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (9 * unitColumnGap)+10 , report.GetVerticalPos(0));
		report.DrawRightAlign("Receiving Date", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (10 * unitColumnGap)+40, report.GetVerticalPos(0));
		report.DrawRightAlign("Receive Qty", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (14 * unitColumnGap)-40, report.GetVerticalPos(0));
		report.DrawRightAlign("Receiving Balance", report._smallFontBold, XBrushes.Black, report.GetHorizontalPos(0) + (15 * unitColumnGap), report.GetVerticalPos(0));

		report.gfx.DrawLine(pen, report.GetHorizontalPos(0)-20, report.GetVerticalPos(report._lineGap), report.GetHorizontalPos(0) + (page.Width - (2 * (report._rightMargin + xStartColumn))), report.GetVerticalPos(0));

		int lineCounter = 1;
		//Double balance=0.00;
		for(int i=0;i<dt.Rows.Count;i++)
		//foreach (DataRow row in dt.Rows)
		{
			if(i==0)
			{
				report.DrawRightAlign(dt.Rows[i]["articlecode"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap - 4, report.GetVerticalPos(report._lineGap));
				lineCounter = report.PrintMultipleLine(dt.Rows[i]["title"].ToString(), report._smallFontBold, XBrushes.Black, unitColumnGap * 5, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0));
				lineCounter += report.PrintMultipleLine(dt.Rows[i]["author"].ToString(), report._smallFontItalic, XBrushes.Black, unitColumnGap * 5, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0) + (report._lineGap * lineCounter));
				lineCounter += report.PrintMultipleLine(dt.Rows[i]["articletype"].ToString(), report._smallFont, XBrushes.Black, unitColumnGap * 5, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0) + (report._lineGap * lineCounter));
				report.DrawRightAlign(dt.Rows[i]["stock"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (9 * unitColumnGap) - 32, report.GetVerticalPos(0));
				report.DrawRightAlign(dt.Rows[i]["orderqty"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (10 * unitColumnGap) - 25, report.GetVerticalPos(0));
				report.DrawRightAlign(dt.Rows[i]["balance"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (15 * unitColumnGap) - 5, report.GetVerticalPos(0));
				

			}
			else
			{
				if(dt.Rows[i]["articlecode"].ToString().Equals(dt.Rows[i-1]["articlecode"].ToString()))
				{
					report.DrawRightAlign("", report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap - 4, report.GetVerticalPos(report._lineGap));
					lineCounter = report.PrintMultipleLine("", report._smallFontBold, XBrushes.Black, unitColumnGap * 5, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0));
					lineCounter += report.PrintMultipleLine("", report._smallFontItalic, XBrushes.Black, unitColumnGap * 5, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0) + (report._lineGap * lineCounter));
					lineCounter += report.PrintMultipleLine("", report._smallFont, XBrushes.Black, unitColumnGap * 5, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0) + (report._lineGap * lineCounter));
					report.DrawRightAlign("", report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (9 * unitColumnGap) - 32, report.GetVerticalPos(0));
					report.DrawRightAlign("", report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (10 * unitColumnGap) - 25, report.GetVerticalPos(0));
					

				}
				else
				{

					report.gfx.DrawLine(pen, report.GetHorizontalPos(0) - 20, report.GetVerticalPos(report._lineGap * lineCounter), report.GetHorizontalPos(0) + (page.Width - (2 * (report._rightMargin + xStartColumn))), report.GetVerticalPos(0));
					report.DrawRightAlign(dt.Rows[i]["articlecode"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + unitColumnGap - 4, report.GetVerticalPos(report._lineGap));
					lineCounter = report.PrintMultipleLine(dt.Rows[i]["title"].ToString(), report._smallFontBold, XBrushes.Black, unitColumnGap * 5.5, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0));
					lineCounter += report.PrintMultipleLine(dt.Rows[i]["author"].ToString(), report._smallFontItalic, XBrushes.Black, unitColumnGap * 5.5, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0) + (report._lineGap * lineCounter));
					lineCounter += report.PrintMultipleLine(dt.Rows[i]["articletype"].ToString(), report._smallFont, XBrushes.Black, unitColumnGap * 5.5, report.GetHorizontalPos(0) + unitColumnGap + 4, report.GetVerticalPos(0) + (report._lineGap * lineCounter));
					report.DrawRightAlign(dt.Rows[i]["stock"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (9 * unitColumnGap) - 32, report.GetVerticalPos(0));
					report.DrawRightAlign(dt.Rows[i]["orderqty"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (10 * unitColumnGap) - 25, report.GetVerticalPos(0));

					report.DrawRightAlign(dt.Rows[i]["balance"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (15 * unitColumnGap) - 5, report.GetVerticalPos(0));

					
					

				}
					
			}
			
			
			report.DrawRightAlign(dt.Rows[i]["receivedate"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (11 * unitColumnGap), report.GetVerticalPos(0));
			report.DrawRightAlign(dt.Rows[i]["previousreceive"].ToString(), report._smallFont, XBrushes.Black, report.GetHorizontalPos(0) + (13 * unitColumnGap) - 10, report.GetVerticalPos(0));
			

			if(i==dt.Rows.Count-1)
				report.gfx.DrawLine(pen, report.GetHorizontalPos(0)-20, report.GetVerticalPos((report._lineGap-2) * lineCounter), report.GetHorizontalPos(0) + (page.Width - (2 * (report._rightMargin + xStartColumn))), report.GetVerticalPos(0));
			
		}
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0)-20, report.GetVerticalPos(0), report.GetHorizontalPos(0)-20, initYLineStart);
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + unitColumnGap, report.GetVerticalPos(0), report.GetHorizontalPos(0) + unitColumnGap, initYLineStart);

		report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (6 * unitColumnGap)+12, report.GetVerticalPos(0), report.GetHorizontalPos(0) + (6 * unitColumnGap)+12, initYLineStart);
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (9 * unitColumnGap)-28 , report.GetVerticalPos(0), report.GetHorizontalPos(0) + (9 * unitColumnGap)-28 , initYLineStart);
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (10 * unitColumnGap) -18, report.GetVerticalPos(0), report.GetHorizontalPos(0) + (10 * unitColumnGap)-18, initYLineStart);
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (11 * unitColumnGap) + 8, report.GetVerticalPos(0), report.GetHorizontalPos(0) + (11 * unitColumnGap) + 8, initYLineStart);
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (12 * unitColumnGap) + 28, report.GetVerticalPos(0), report.GetHorizontalPos(0) + (12 * unitColumnGap) + 28, initYLineStart);
		report.gfx.DrawLine(pen, report.GetHorizontalPos(0) + (15 * unitColumnGap), report.GetVerticalPos(0), report.GetHorizontalPos(0) + (15 * unitColumnGap), initYLineStart);

		report.SaveFile(destination);
		DownLoadPDF(destination);
	}
	private void DownLoadPDF(string fileName)
	{
		try
		{
			FileInfo fx = new FileInfo(fileName);
			FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate,
				FileAccess.ReadWrite, FileShare.ReadWrite);
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
}
