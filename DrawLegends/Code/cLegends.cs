#define TARGET

using System;


namespace VWS.Legenden 
{


    class cLegends
    {


        public static System.Drawing.SizeF MeasureString(string s, System.Drawing.Font font)
        {
            System.Drawing.SizeF result;
            int iCharsFilled;
            int iLinesFilled;

            using (System.Drawing.Image image = new System.Drawing.Bitmap(1, 1))
            {
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image))
                {
                    // bmp.Width
                    // bmp.Height

                    //result = g.MeasureString(s, font);
                    result = g.MeasureString(s, font, new System.Drawing.SizeF(600, 600), System.Drawing.StringFormat.GenericTypographic, out iCharsFilled, out iLinesFilled);
                    //g.MeasureString(s, font, int.MaxValue, StringFormat.GenericTypographic);
                } // End Using System.Drawing.Graphics g 

            } // End Using image

            return result;
        } // End Function MeasureString


        public static System.Drawing.Image GetLegend()
        {
            System.Drawing.Image img = null;
            int maxsize = 1200;
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(maxsize, maxsize);

            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.White);
            }

            using (System.Data.DataTable dt = cDrawingData.GetData())
            {
                img = GetLegend(dt, bmp, 0, 0);
            }

            return img;
        } // End Function GetLegend


        public static System.Drawing.Image GetLegend(System.Data.DataTable dtLegendData, System.Drawing.Bitmap bmp, int origWidth, int origHeight)
        {
            //Random r = new Random();
            //Color[] cols = GetColors();
            //MsgBox(cols[123].ToKnownColor().ToString());
            System.Drawing.Color TextColor = System.Drawing.Color.Black;
            System.Drawing.Color ValueColor = System.Drawing.Color.Black;


            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
            {
                // Modify the image using g here... 
                //g.Clear(System.Drawing.Color.White);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;



                int iBulletSize = 10;
                float fWidth = iBulletSize * 35;

                int iOriginX = origWidth + iBulletSize;
                int iOriginY = 20;


                // 8 ==> 125
                // 10 ==> 150
                // 15 ==> 225
                // 20 ==> 300

                using (System.Drawing.StringFormat strFormat = new System.Drawing.StringFormat())
                {
                    strFormat.Alignment = System.Drawing.StringAlignment.Far;
                    strFormat.LineAlignment = System.Drawing.StringAlignment.Center;

                    string strFont = "Tahoma";
                    /*
                    strFont = "Algerian";
                    strFont = "Cambria";
                    strFont = "Vivaldi";
                    strFont = "Comic Sans MS";
                    strFont = "GDT";
                    strFont = "Wingdings";
                    */



                    System.Drawing.FontStyle fs = System.Drawing.FontStyle.Regular;

                    if (cDrawingTools.HasFont(strFont))
                    {
                        System.Drawing.FontFamily ff = new System.Drawing.FontFamily(strFont);
                        if (ff.IsStyleAvailable(System.Drawing.FontStyle.Regular))
                            fs = System.Drawing.FontStyle.Regular;
                        else if (ff.IsStyleAvailable(System.Drawing.FontStyle.Italic))
                            fs = System.Drawing.FontStyle.Italic;
                        else if (ff.IsStyleAvailable(System.Drawing.FontStyle.Bold))
                            fs = System.Drawing.FontStyle.Bold;
                        else if (ff.IsStyleAvailable(System.Drawing.FontStyle.Strikeout))
                            fs = System.Drawing.FontStyle.Strikeout;
                        else if (ff.IsStyleAvailable(System.Drawing.FontStyle.Underline))
                            fs = System.Drawing.FontStyle.Underline;
                        else
                        { 
                            throw new InvalidOperationException(string.Format("No font style available for font \"{0}\".",strFont));
                        }
                    }
                    else
                        throw new InvalidOperationException(string.Format("No font with name \"{0}\" installed.", strFont));
                    


                    using (System.Drawing.Font fnt = new System.Drawing.Font(strFont, iBulletSize, fs))
                    {

                        string strLogoPath = null;

#if TARGET //= "winexe" Or TARGET = "exe"
                        strLogoPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        strLogoPath = System.IO.Path.Combine(strLogoPath, "logos");
                        strLogoPath = System.IO.Path.Combine(strLogoPath, "cs_klein.png");
#else
                        strLogoPath = System.Web.HttpContext.Current.Request.MapPath("~/images/VWS/logos/cs_klein.png");
#endif

                        if (System.IO.File.Exists(strLogoPath))
                        {

                            using (System.Drawing.Image imgLogo = System.Drawing.Image.FromFile(strLogoPath))
                            {
                                // Draw base image
                                int iLogoX = (int)(iOriginX + 2 * iBulletSize - 7);
                                int iLogoY = (int)(-(0.34f * iBulletSize) + iOriginY + 0 * (iBulletSize * 2));
                                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(iLogoX, iLogoY, imgLogo.Width, imgLogo.Height);
                                g.DrawImageUnscaledAndClipped(imgLogo, rect);
                                iOriginY = iOriginY + imgLogo.Height + 3 * iBulletSize;
                            } // End Using System.Drawing.Image imgLogo 

                        } // End if (System.IO.File.Exists(strLogoPath))

                        using (System.Drawing.SolidBrush TextBrush = new System.Drawing.SolidBrush(TextColor))
                        {
                            // Legendentext
                            g.DrawString("Mietobjekt", fnt, TextBrush, new System.Drawing.PointF(iOriginX + 2 * iBulletSize - 7, -(0.34f * iBulletSize) + iOriginY + 0 * (iBulletSize * 2)));
                        } // End Using System.Drawing.SolidBrush TextBrush


                        System.Drawing.RectangleF rectfValueTitleBoundary = new System.Drawing.RectangleF(iOriginX, iOriginY + 0 * (iBulletSize * 2), fWidth, iBulletSize + 2);
                        using (System.Drawing.SolidBrush ValueBrush = new System.Drawing.SolidBrush(ValueColor))
                        {
                            // Legendenwert
                            g.DrawString("Fläche", fnt, ValueBrush, rectfValueTitleBoundary, strFormat);
                        } // End Using System.Drawing.SolidBrush ValueBrush
                                





                        //iOriginX = origWidth + iBulletSize;
                        //iOriginY = 20;

                        iOriginY = iOriginY + iBulletSize * 3;



                        //using (System.Data.DataTable dt = cDrawingData.GetData())
                        //{
                            //dgvLegendData.DataSource = dt;

                            for (int i = 0; i < dtLegendData.Rows.Count; ++i)
                            {
                                System.Data.DataRow dr = dtLegendData.Rows[i];

                                string strFC = System.Convert.ToString(dr["Html_ForeColor"]);
                                string strBC = System.Convert.ToString(dr["Html_BackColor"]);
                                string strLC = System.Convert.ToString(dr["Html_LineColor"]);

                                System.Drawing.Color colFC = System.Drawing.ColorTranslator.FromHtml(strFC);
                                System.Drawing.Color colHB = System.Drawing.ColorTranslator.FromHtml(strBC);
                                System.Drawing.Color colHL = System.Drawing.ColorTranslator.FromHtml(strLC);

                                int iLegendPattern = System.Convert.ToInt32(dr["AP_LEG_Pattern"]);
                                

                                // Add legend rectangle with color 
                                using (System.Drawing.Brush CurrentBrush = cDrawingTools.GetBrush(iLegendPattern, colFC, colHB, colHL))
                                {
                                    System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(iOriginX, iOriginY + i * (iBulletSize * 2), iBulletSize, iBulletSize);

                                    if (CurrentBrush != null)
                                    {
                                        //if (object.ReferenceEquals(CurrentBrush, typeof(TextureBrush)))
                                        if (CurrentBrush is System.Drawing.TextureBrush)
                                            ((System.Drawing.TextureBrush)CurrentBrush).TranslateTransform(rect2.X, rect2.Y);

                                        // Punkt - Quadrat
                                        g.FillRectangle(CurrentBrush, rect2);
                                        // cDrawingTools.FillTriangle(g, CurrentBrush, iOriginX, iOriginY, i, iBulletSize);
                                        // cDrawingTools.FillCircle(g, CurrentBrush, iOriginX + iBulletSize / 2.0f, iOriginY + i * (iBulletSize * 2) + iBulletSize / 2.0f, iBulletSize / 2.0f);

                                    } // End if (CurrentBrush != null)

                                } // End Using System.Drawing.Brush CurrentBrush 
                                // End of adding legend rectangle with color 



                                string strTitle = System.Convert.ToString(dr["Text"]);
                                using (System.Drawing.SolidBrush TextBrush = new System.Drawing.SolidBrush(TextColor))
                                {
                                    // Legendentext
                                    g.DrawString(strTitle, fnt, TextBrush, new System.Drawing.PointF(iOriginX + 2 * iBulletSize - 7, -(0.34f * iBulletSize) + iOriginY + i * (iBulletSize * 2)));
                                } // End Using System.Drawing.SolidBrush TextBrush


                                System.Drawing.SizeF fff = MeasureString(strTitle, fnt);
                                Console.WriteLine(fff);


                                double dblValue = System.Convert.ToDouble(dr["Value"]);
                                //string strValue = string.Format("{0:f2}", fValue);
                                //string strValue = string.Format("{0:##0.00}", fValue);


                                // System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
                                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("de-CH");
                                // ci = new System.Globalization.CultureInfo("en-US");
                                

                                // http://msdn.microsoft.com/en-us/library/0c899ak8.aspx
                                // http://stackoverflow.com/questions/8941219/string-formatting-for-decimal-places-and-thousands
                                string strValue = string.Format(ci, "{0:#,0.00} m²", dblValue);
                                ci = null;



                                System.Drawing.RectangleF rectfTextBoundaries2 = new System.Drawing.RectangleF(iOriginX, iOriginY + i * (iBulletSize * 2), fWidth, iBulletSize + 2);
                                using (System.Drawing.SolidBrush ValueBrush = new System.Drawing.SolidBrush(ValueColor))
                                {
                                    // Legendenwert
                                    g.DrawString(strValue, fnt, ValueBrush, rectfTextBoundaries2, strFormat);
                                } // End Using System.Drawing.SolidBrush ValueBrush
                                
                                // Debug: Check alignment
                                // g.DrawRectangle(System.Drawing.Pens.Black, rectfTextBoundaries2.X, rectfTextBoundaries2.Y - 2, rectfTextBoundaries2.Width, rectfTextBoundaries2.Height + 2);

                            } // Next i
                            
                            // Add Foooter
                            //Bitmap m_Bitmap = new Bitmap(300, 300);
                            //renderer.Clear(System.Drawing.Color.White);
                            string strHTML = cDrawingData.GetLegendFooterHtml();
                            
                            // SizeF sfMeasuredSize = HtmlRenderer.HtmlRender.Measure(renderer, strHTML, 800); // Measure size of rendering
                            System.Drawing.SizeF sfMeasuredSize = HtmlRenderer.HtmlRender.MeasureGdiPlus(g, strHTML, 800); // Measure size of rendering
                            System.Drawing.PointF point = new System.Drawing.PointF(iOriginX, bmp.Height - sfMeasuredSize.Height - 20); // Calculate positoin
                                
                            System.Drawing.SizeF maxSize = new System.Drawing.SizeF(300, 300);
                            HtmlRenderer.HtmlRender.RenderGdiPlus(g, strHTML, point, maxSize);
                            // HtmlRenderer.HtmlRender.Render(renderer, strHTML, point, maxSize);

                            //var img = HtmlRender.RenderToImageGdiPlus(html1, new Size(600, 200));
                            // End Add Foooter

                            using (System.Drawing.Pen BorderColorPen = new System.Drawing.Pen(System.Drawing.Color.Black))
                            {
                                BorderColorPen.Width = 1;
                                g.DrawRectangle(BorderColorPen, new System.Drawing.Rectangle(0, 0, bmp.Width - 1, bmp.Height - 1));

                                g.DrawLine(BorderColorPen, new System.Drawing.Point(origWidth, 0), new System.Drawing.Point(origWidth, bmp.Height));
                            } // End Using System.Drawing.Pen BorderColorPen


                        //} // End Using System.Data.DataTable dt

                    } // End Using System.Drawing.Font fnt

                } // End Using StringFormat strFormat

            } // End Using Graphics g

            return bmp;
        } // End Sub GetLegend


    } // End Class cLegends


} // End Namespace DrawLegends
