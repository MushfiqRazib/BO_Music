using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using Npgsql;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Collections;


    public class PageProperties
    {
        public double _leftMargin = 35;
        public double _rightMargin = 20;
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

        public XFont _largeFont = new XFont("Times New Roman", 20, XFontStyle.Regular);
        public XFont _largeFontBold = new XFont("Times New Roman", 20, XFontStyle.Bold);
        public XFont _mediumFont = new XFont("Arial", 11, XFontStyle.Bold);
        public XFont _normalFont = new XFont("Arial", 10, XFontStyle.Regular);
        public XFont _smallFont = new XFont("Arial", 8, XFontStyle.Regular);
        public XFont _smallFont1 = new XFont("Arial", 6, XFontStyle.Regular);
        public XFont _verySmallFont = new XFont("Arial", 6, XFontStyle.Regular);
        public XFont _smallFontBold = new XFont("Arial", 8, XFontStyle.Bold);
        public XFont _smallFontItalic = new XFont("Arial", 8, XFontStyle.Italic);
        public XFont _normalFontBold = new XFont("Arial", 10, XFontStyle.Bold);



        public XRect _rect;// = new XRect(230,0, 100, 60);
        public XPen _borderPen=new XPen(XColors.Black, 1);
        

        public int AdminId;


        public void NewPage()
        {            
            PdfPage page = document.AddPage();
            SetVerticalPos(_topMargin);
            SetHorizontalPos(_leftMargin);
            _pageNumber++;
            gfx = XGraphics.FromPdfPage(page);
        }

        public void NewPage(PageSize size, PageOrientation orientation)
        {
            PdfPage page = document.AddPage();
            page.Size = size;
            page.Orientation = orientation;
            SetVerticalPos(_topMargin);
            SetHorizontalPos(_leftMargin);
            _pageNumber++;

            gfx = XGraphics.FromPdfPage(page);
        }

        public void NewPage(PageSize size, PageOrientation orientation, double topMarzin, double leftMarzin)
        {
            PdfPage page = document.AddPage();
            page.Size = size;
            page.Orientation = orientation;
            SetVerticalPos(topMarzin);
            SetHorizontalPos(leftMarzin);
            _pageNumber++;

            gfx = XGraphics.FromPdfPage(page);
        }

        public void SaveFile(string path)
        {
            document.Save(path);
        }
        public PageProperties()
        {
            document = new PdfDocument();
        }

        public double GetBoxX(double value)
        {
            return _boxX += value;
        }
        public double GetBoxY(double value)
        {
            return _boxY += value;
        }

        public double SetBoxX(double value)
        {
            return _boxX = value;
        }
        public double SetBoxY(double value)
        {
            return _boxY = value;
        }

        //public double GetVerticalPos(double value)
        //{
        //    if(value > _lineGap)
        //    {

        //    }
        //    if ((_verticalPos + value + _lineGap) > (gfx.PageSize.Height - _bottomMargin))
        //    {
        //        double temp = _verticalPos;
        //        NewPage();
        //        return temp;
        //    }
        //    return _verticalPos += value;
        //}

        /// <summary>
        /// This function print data with right align...
        /// </summary>
        /// <param name="yourString"></param>
        /// <param name="font"></param>
        /// <param name="brash"></param>
        /// <param name="HigheshHorizontalPos"> Last position to align</param>
        /// <param name="verticalPos"></param>
        public void DrawNumericRightAlign(string yourString, XFont font, XBrush brush, double HigheshHorizontalPos, double verticalPos)
        {
            XSize textSize = gfx.MeasureString(yourString, font);
            long pixLength = Convert.ToInt64(textSize.Width);
            gfx.DrawString(yourString, font, brush, HigheshHorizontalPos - pixLength, verticalPos);

        }



        public double GetVerticalPos(double value)
        {
            return _verticalPos += value;
        }

        public double GetHorizontalPos(double value)
        {
            return _horizontalPos += value;
        }
        //public double GetVerticalPos(double value)
        //{
        //    return _verticalPos + value;
        //}

        //public double GetHorizontalPos(double value)
        //{
        //    return _horizontalPos + value;
        //}
        public double SetVerticalPos(double value)
        {
            return _verticalPos = value;
        }

        public double SetHorizontalPos(double value)
        {
            return _horizontalPos = value;
        }


        public void PrintALine(string yourString, XFont font, XBrush brush, double columnWidth, double horizontalPos, double verticalPos)
        {
            XSize strSize = this.gfx.MeasureString(yourString, font);
            int intWidth = (int)strSize.Width;

            if (columnWidth >= intWidth) //allowed pixel sets in one line
            {
                gfx.DrawString(yourString, font, brush, horizontalPos, verticalPos);
            }
            else
            {
                string myString = "";
                for (int i = 0; i < yourString.Length; i++)
                {
                    strSize = gfx.MeasureString(myString, font);
                    intWidth = (int)strSize.Width;

                    if (columnWidth > intWidth)
                    {
                        myString += yourString.Substring(i, 1);
                    }
                    else if (columnWidth < intWidth)
                    {
                        myString.Remove(myString.Length - 1);
                    }

                }
                gfx.DrawString(myString, font, brush, horizontalPos, verticalPos);
            }
        }
        
        /// <summary>
        /// Create a box and transform x and y co-ordinate
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="rect"></param>
        /// <param name="borderPen"></param>
        public void CreateBox(XGraphics gfx, XRect rect, XPen borderPen)
        {
            //int dEllipse = 15;
            //XRect rect = new XRect(75, 130, 400, 300);
            //if (number % 2 == 0)
            //    rect.X = 300 - 5;
            //rect.Y = 40 + ((number - 1) / 2) * (200 - 5);
            //rect.Inflate(-10, -10);
            //XRect rect2 = rect;
            //rect2.Offset(this.borderWidth, this.borderWidth);
            //gfx.DrawRectangle(borderPen, rect);
            //gfx.DrawRoundedRectangle(this.borderPen, new XSolidBrush(this.backColor), rect, new XSize(dEllipse, dEllipse));
            //rect.Inflate(-5, -5);

            //XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
            //gfx.DrawString(title, font, XBrushes.Black, rect, XStringFormat.CenterTop);

            //rect.Inflate(-10, -5);
            //rect.Y += 20;
            //rect.Height -= 20;
            //gfx.DrawRectangle(XPens.Red, rect);
            this._state = gfx.Save();
            gfx.TranslateTransform(rect.X, rect.Y);
            SetBoxX(10);
            SetBoxY(10);
        }

        public void DrawBox(XGraphics gfx, XRect rect, XPen borderPen)
        {
            //int dEllipse = 15;
            //XRect rect = new XRect(75, 130, 400, 300);
            //if (number % 2 == 0)
            //    rect.X = 300 - 5;
            //rect.Y = 40 + ((number - 1) / 2) * (200 - 5);
            //rect.Inflate(-10, -10);
            //XRect rect2 = rect;
            //rect2.Offset(this.borderWidth, this.borderWidth);
            gfx.DrawRectangle(borderPen, rect);
            //gfx.DrawRoundedRectangle(this.borderPen, new XSolidBrush(this.backColor), rect, new XSize(dEllipse, dEllipse));
            //rect.Inflate(-5, -5);

            //XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
            //gfx.DrawString(title, font, XBrushes.Black, rect, XStringFormat.CenterTop);

            //rect.Inflate(-10, -5);
            //rect.Y += 20;
            //rect.Height -= 20;
            //gfx.DrawRectangle(XPens.Red, rect);

            this._state = gfx.Save();
            gfx.TranslateTransform(rect.X, rect.Y);
            SetBoxX(0);
            SetBoxY(0);
        }
        public void DrawBox(XGraphics gfx, XRect rect, XBrush brush)
        {
            gfx.DrawRectangle(brush, rect);
            //gfx.DrawRoundedRectangle(this.borderPen, new XSolidBrush(this.backColor), rect, new XSize(dEllipse, dEllipse));
            //rect.Inflate(-5, -5);

            //XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
            //gfx.DrawString(title, font, XBrushes.Black, rect, XStringFormat.CenterTop);

            //rect.Inflate(-10, -5);
            //rect.Y += 20;
            //rect.Height -= 20;
            //gfx.DrawRectangle(XPens.Red, rect);

            this._state = gfx.Save();
            gfx.TranslateTransform(rect.X, rect.Y);
            SetBoxX(0);
            SetBoxY(0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gfx"></param>
        public void EndBox(XGraphics gfx)
        {
            if (gfx != null)
                gfx.Restore(this._state);
        }

    }


