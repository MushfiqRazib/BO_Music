using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;
using PdfSharp;

   public class TamplatePage : PageProperties
   {
        
        public string  filename;       
        public XFont font = new XFont("Times", 15, XFontStyle.BoldItalic);

        public TamplatePage(string destFile)
        {
            // Open an existing document for editing and loop through its pages
            document = PdfReader.Open(destFile);

            PdfPage page = document.Pages[0];
            gfx = XGraphics.FromPdfPage(page);            
            filename = destFile;
            _pageNumber++;
        }

        public TamplatePage(string sourceFile, string destFile,PageOrientation orientation)
        {

            // Open an existing document for editing and loop through its pages
            document = PdfReader.Open(destFile);
            
            PdfPage page = document.Pages[0];
                       
            page.Size=PageSize.A4;
            page.Orientation = orientation;
            gfx = XGraphics.FromPdfPage(page);
            SetVerticalPos(_topMargin);
            SetHorizontalPos(_leftMargin);
            filename = sourceFile;
            _pageNumber++;
        }


        public TamplatePage(string sourceFile,string destFile)
        {
           
            
            // Open an existing document for editing and loop through its pages
            document = PdfReader.Open(destFile);

            PdfPage page = document.Pages[0];
            page.Size = PageSize.A4;
            gfx = XGraphics.FromPdfPage(page);
            SetVerticalPos(_topMargin);
            SetHorizontalPos(_leftMargin);
            filename = sourceFile;
            _pageNumber++; 
        }

        /// <summary>
        /// when same tamplate page is used in this document
        /// </summary>
        public void NewTamplatePage()
        {
            PdfDocument doc = PdfReader.Open(filename, PdfDocumentOpenMode.Import);
            PdfPage pp = doc.Pages[0];
            PdfPage page = document.AddPage(pp);
            gfx = XGraphics.FromPdfPage(page);
            SetVerticalPos(_topMargin);
            SetHorizontalPos(_leftMargin);
            _pageNumber++;

        }

        public void NewTamplatePage(PageOrientation orientation)
        {
            PdfDocument doc = PdfReader.Open(filename, PdfDocumentOpenMode.Import);
            PdfPage pp = doc.Pages[0];
            PdfPage page = document.AddPage(pp);
            page.Orientation = orientation;
            page.Size = PageSize.A4;
            gfx = XGraphics.FromPdfPage(page);
            SetVerticalPos(_topMargin);
            SetHorizontalPos(_leftMargin);
            _pageNumber++;

        }

       /// <summary>
       /// when different tamplate is used in this document
       /// </summary>
       /// <param name="sourcefilename"> The full path of the sourcefile tamplate</param>
        public void NewTamplatePage(string sourcefilename)
        {
            PdfDocument doc = PdfReader.Open(sourcefilename, PdfDocumentOpenMode.Import);
            PdfPage pp = doc.Pages[0];
            PdfPage page = document.AddPage(pp);
            gfx = XGraphics.FromPdfPage(page);
            SetVerticalPos(_topMargin);
            SetHorizontalPos(_leftMargin);
            filename = sourcefilename;
            _pageNumber++;

        }
        
        
    }

