using System;
using System.Data;
using System.Configuration;
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

/// <summary>
/// Summary description for PdfSharpPages
/// </summary>
public class PdfSharpPages
{
    public double _leftMargin = 35;
    public double _rightMargin = 35;
    public double _topMargin = 50;
    public double _bottomMargin = 70;
    public double _lineGap = 13;
    public double _lineGapLarge = 20;
    public double _smallLineGap = 6;
    public int _pageNumber = 0;

    private double _verticalPos = 0;
    private double _horizontalPos = 0;
    public double _normalcharlength = 6;
    public double _smallcharlength = 5;
    public double _largecharlength = 10;
    public int _pricelength = 12;

    private double _boxX = 0;
    private double _boxY = 0;

    public PdfDocument document = new PdfDocument();
    public XGraphics gfx = null;
    public XGraphicsState _state;

    public XFont _largeFont = new XFont("Arial", 22, XFontStyle.Regular);
    public XFont _largeFontBold = new XFont("Arial", 22, XFontStyle.Bold);
    public XFont _mediumFont = new XFont("Arial", 11, XFontStyle.Regular);
    public XFont _mediumFontBold = new XFont("Arial", 11, XFontStyle.Bold);
    public XFont _normalFont = new XFont("Arial", 10, XFontStyle.Regular);    
    public XFont _normalFontBold = new XFont("Arial", 10, XFontStyle.Bold);
    public XFont _smallFont = new XFont("Arial", 8, XFontStyle.Regular);
    public XFont _smallFontBold = new XFont("Arial", 8, XFontStyle.Bold);
    public XFont _smallFontItalic = new XFont("Arial", 8, XFontStyle.Italic);


    public XRect _rect;
    public XPen _borderPen;

    public XBrushes _blackBrushes;
    public XBrushes _redBrushes;
	public PdfSharpPages()
	{
        document = new PdfDocument();
	}
    public void SaveFile(string path)
    {
        document.Save(path);

    }
    public double GetVerticalPos(double value)
    {
        return _verticalPos += value;
    }


    public double GetHorizontalPos(double value)
    {
        return _horizontalPos += value;
    }
    public double SetVerticalPos(double value)
    {
        return _verticalPos = value;
    }

    public double SetHorizontalPos(double value)
    {
        return _horizontalPos = value;
    }
    public void DrawRightAlign(string yourString, XFont font, XBrush brush, double HigheshHorizontalPos, double verticalPos)
    {
        XSize textSize = gfx.MeasureString(yourString, font);
        long pixLength = Convert.ToInt64(textSize.Width);
        gfx.DrawString(yourString, font, brush, HigheshHorizontalPos - pixLength, verticalPos);

    }

    public int PrintMultipleLine(string yourString, XFont font, XBrush brush, double columnWidth, double horizontalPos, double verticalPos)
    {
        XSize strSize = this.gfx.MeasureString(yourString, font);
        int yourStringWidth = (int)strSize.Width;
        string[] splitter = yourString.Split(' ');
        string myString = "";
        int MyLine = 0;
        int tLength = 0;
        int temp = 0;
        int lineCounter = 0;
        for (int i = 0; i < splitter.Length; i++)
        {
            strSize = this.gfx.MeasureString(splitter[i], font);
            tLength += (int)strSize.Width;
            strSize = this.gfx.MeasureString(myString, font);
            tLength += (int)strSize.Width;
            strSize = this.gfx.MeasureString("a", font);
            tLength += (int)strSize.Width;

            if (tLength == columnWidth)
            {
                for (int j = temp; j <= i; j++)
                {
                    myString += splitter[j]+" ";
                }
                temp = i + 1;
                //draw(mystr);
                ++lineCounter;
                gfx.DrawString(myString, font, brush, horizontalPos, verticalPos + (lineCounter - 1) * 13);
                myString = "";
                tLength = 0;
            }
            else if (tLength > columnWidth)
            {
                for (int j = temp; j < i; j++)
                {
                    myString += splitter[j] + " ";
                }
                temp = i+1;
                //draw(mystr);
                ++lineCounter;
                gfx.DrawString(myString, font, brush, horizontalPos, verticalPos + (lineCounter - 1) * 13);
                myString = "";
                myString += splitter[i] + " ";
                tLength = 0;
            }
            else if (splitter.Length == i + 1)
            {
                for (int j = temp; j <= i; j++)
                {
                    myString += splitter[j] + " ";                    
                }
                ++lineCounter;
                gfx.DrawString(myString, font, brush, horizontalPos, verticalPos + (lineCounter - 1) * 13);
            }
        }


        return lineCounter;
 }


    public void DeleteExistFile(string d)
    {
        System.IO.FileInfo fi = new System.IO.FileInfo(d);
        if (fi.Exists)
        {
            fi.Delete();
        }
    }
}
