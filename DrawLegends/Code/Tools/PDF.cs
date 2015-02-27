
using System;
using System.Collections.Generic;
using System.Web;


namespace Tools.FileFormat
{


    public class PDF
    {


        protected static PdfSharp.Pdf.PdfDocument CreateDocument()
        {
            PdfSharp.Pdf.PdfDocument doc = new PdfSharp.Pdf.PdfDocument();
            doc.Info.Title = "AmChart image created with PDFsharp";
            doc.Info.Author = "Stefan Steiger";
            doc.Info.Subject = "AmCharts"; // "Created with code snippets that show the use of graphical functions";
            doc.Info.Keywords = "PDFsharp, XGraphics, AmCharts";

            return doc;
        } // End Function CreateDocument


        public static void ImageDataToPdfFile(byte[] imgdata, string strPdfFileName)
        {
            if (System.IO.File.Exists(strPdfFileName))
                System.IO.File.Delete(strPdfFileName);

            using (PdfSharp.Pdf.PdfDocument pdfdoc = CreateDocument())
            {
                PdfSharp.Pdf.PdfPage pg = pdfdoc.AddPage();
                AddImageToPage(pg, imgdata);

                pdfdoc.Save(strPdfFileName);
            } // End Using pdfdoc

        } // End Sub ImageDataToPdfFile


        public static byte[] ImageToPdfData(System.Drawing.Image img)
        {
            byte[] baReturnValue = null;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {

                using (PdfSharp.Pdf.PdfDocument pdfdoc = CreateDocument())
                {
                    PdfSharp.Pdf.PdfPage pg = pdfdoc.AddPage();
                    AddImageToPage(pg, img);

                    pdfdoc.Save(ms);
                } // End Using pdfdoc

                baReturnValue = ms.ToArray();
            } // End Using ms

            return baReturnValue;
        } // End Sub ImageToPdfData


        public static byte[] ImageDataToPdfData(byte[] imgdata)
        {
            byte[] baReturnValue = null;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {

                using (PdfSharp.Pdf.PdfDocument pdfdoc = CreateDocument())
                {
                    PdfSharp.Pdf.PdfPage pg = pdfdoc.AddPage();
                    AddImageToPage(pg, imgdata);

                    pdfdoc.Save(ms);
                } // End Using pdfdoc

                baReturnValue = ms.ToArray();
            } // End Using ms

            return baReturnValue;
        } // End Sub ImageDataToPdfData


        public static byte[] Base64ImageToPdfData(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException("data");

            System.Text.RegularExpressions.Match ma = System.Text.RegularExpressions.Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)");
            if (ma.Success)
            {
                string base64Data = ma.Groups["data"].Value;
                byte[] binData = Convert.FromBase64String(base64Data);

                return ImageDataToPdfData(binData);
            }

            throw new ArgumentException("data improper");
        } // End Function Base64ImageToPdfData


        public static byte[] ImageFileToPdfData(string strImageFileName)
        {
            byte[] baReturnValue = null;

            if (!System.IO.File.Exists(strImageFileName))
                throw new System.IO.FileNotFoundException(strImageFileName);

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (PdfSharp.Pdf.PdfDocument pdfdoc = CreateDocument())
                {
                    PdfSharp.Pdf.PdfPage pg = pdfdoc.AddPage();
                    AddImageToPage(pg, strImageFileName);

                    pdfdoc.Save(ms);
                } // End Using pdfdoc

                baReturnValue = ms.ToArray();
            } // End using ms

            return baReturnValue;
        } // End Function ImageFileToPdfData


        public static void ImageDataToImageFile(byte[] binData, string strImageFileName)
        {

            using (System.IO.Stream stream = new System.IO.MemoryStream(binData))
            {
                using (System.Drawing.Image img = System.Drawing.Image.FromStream(stream))
                {
                    if (System.IO.File.Exists(strImageFileName))
                        System.IO.File.Delete(strImageFileName);

                    img.Save(strImageFileName, System.Drawing.Imaging.ImageFormat.Png);
                } // End Using img

            } // End Using stream

        } // End Sub ImageDataToImageFile


        // http://www.pdfsharp.net/wiki/Images-sample.ashx
        public static void ImageFileToPdfFile(string strImageFileName, string strPdfFileName)
        {
            if (!System.IO.File.Exists(strImageFileName))
                throw new System.IO.FileNotFoundException(strImageFileName);

            if (System.IO.File.Exists(strPdfFileName))
                System.IO.File.Delete(strPdfFileName);

            using (PdfSharp.Pdf.PdfDocument pdfdoc = CreateDocument())
            {
                PdfSharp.Pdf.PdfPage pg = pdfdoc.AddPage();
                AddImageToPage(pg, strImageFileName);

                pdfdoc.Save(strPdfFileName);
            } // End Using pdfdoc

        } // End Sub ImageFileToPdfFile


        public static void ImageFileToPdfFileStream(string strImageFileName, string strPdfFileName)
        {
            if (!System.IO.File.Exists(strImageFileName))
                throw new System.IO.FileNotFoundException(strImageFileName);

            if (System.IO.File.Exists(strPdfFileName))
                System.IO.File.Delete(strPdfFileName);

            using (PdfSharp.Pdf.PdfDocument pdfdoc = CreateDocument())
            {
                PdfSharp.Pdf.PdfPage pg = pdfdoc.AddPage();
                AddImageToPage(pg, strImageFileName);

                using (System.IO.FileStream fs = System.IO.File.Create(strPdfFileName))
                {
                    pdfdoc.Save(fs);
                } // End Using fs

            } // End Using pdfdoc

        } // End Sub ImageFileToPdfFileStream


        protected static void AddImageToPage(PdfSharp.Pdf.PdfPage page, byte[] binData)
        {
            using (System.IO.Stream stream = new System.IO.MemoryStream(binData))
            {
                using (System.Drawing.Image img = System.Drawing.Image.FromStream(stream))
                {
                    //if (System.IO.File.Exists(strImageFileName))
                    //System.IO.File.Delete(strImageFileName);

                    //img.Save(strImageFileName, System.Drawing.Imaging.ImageFormat.Png);

                    using (PdfSharp.Drawing.XImage image = PdfSharp.Drawing.XImage.FromGdiPlusImage(img))
                    {
                        AddImageToPage(page, image);
                    } // End Using image

                } // End Using img

                /*
                var pictureBox = new PictureBox
                {
                    Image = new System.Drawing.Bitmap(stream),
                };
                */
                //var form = new Form { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
                //form.Controls.Add(pictureBox);

            } // End Using stream

        } // End Sub AddImageToPage


        protected static void AddImageToPage(PdfSharp.Pdf.PdfPage page, string strImageFileName)
        {
            using (PdfSharp.Drawing.XImage image = PdfSharp.Drawing.XImage.FromFile(strImageFileName))
            {
                AddImageToPage(page, image);
            } // End Using image

        } // End Sub AddImageToPage


        protected static void AddImageToPage(PdfSharp.Pdf.PdfPage page, System.Drawing.Image img)
        {
            
            using (PdfSharp.Drawing.XImage image = PdfSharp.Drawing.XImage.FromGdiPlusImage(img))
            {
                AddImageToPage(page, image);
            } // End Using image

        } // End Sub AddImageToPage


        protected static void old_AddImageToPage(PdfSharp.Pdf.PdfPage page, PdfSharp.Drawing.XImage image)
        {
            using (PdfSharp.Drawing.XGraphics gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(page))
            {
                //1 centimeter = 28.3464567 PostScript points
                int iMarginTop = (int)(1.5 * 28.3464567);

                // Left position in point
                int iMarginLeft = (int)(page.Width.Point - image.PixelWidth * 72 / image.HorizontalResolution) / 2;

                gfx.DrawImage(image, iMarginLeft, iMarginTop);
            } // End Using PdfSharp.Drawing.XGraphics gfx

        } // End Sub AddImageToPage


        protected static void AddImageToPage(PdfSharp.Pdf.PdfPage page, PdfSharp.Drawing.XImage image)
        {
            using (PdfSharp.Drawing.XGraphics gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(page))
            {
                if (page.Width.Point < image.PointWidth)
                    page.Orientation = PdfSharp.PageOrientation.Landscape;
                if (page.Width.Point < image.PointWidth)
                    page.Width = image.PointWidth;

                int iMarginTop = Convert.ToInt32((page.Height.Point - image.PointHeight) / 2);
                int iMarginLeft = Convert.ToInt32((page.Width.Point - image.PointWidth) / 2);

                gfx.DrawImage(image, iMarginLeft, iMarginTop);
            } // End Using PdfSharp.Drawing.XGraphics gfx

        } // End Sub AddImageToPage


    } // End Class PDF


} // End Namespace Tools.FileFormat
