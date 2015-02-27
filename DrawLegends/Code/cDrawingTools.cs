#define TARGET

using System;
using System.Collections.Generic;
using System.Text;


namespace VWS.Legenden 
{


    class cDrawingTools
    {


        public static System.Drawing.Drawing2D.HatchStyle[] GetHatchStyles()
        {
            System.Drawing.Drawing2D.HatchStyle[] styles = (System.Drawing.Drawing2D.HatchStyle[])Enum.GetValues(typeof(System.Drawing.Drawing2D.HatchStyle));
            return styles;
        } // End Function GetHatchStyles


        public static void FillTriangle(System.Drawing.Graphics g, System.Drawing.Brush CurrentBrush, int iOriginX, int iOriginY, int i, int iBulletSize)
        {
            // Create points that define polygon.
            System.Drawing.PointF point1 = new System.Drawing.PointF(iOriginX, iOriginY + i * (iBulletSize * 2) + iBulletSize);
            System.Drawing.PointF point2 = new System.Drawing.PointF(iOriginX + iBulletSize, iOriginY + i * (iBulletSize * 2) + iBulletSize);
            System.Drawing.PointF point3 = new System.Drawing.PointF(iOriginX + iBulletSize / 2.0f, iOriginY + i * (iBulletSize * 2));
            System.Drawing.PointF[] curvePoints = { point1, point2, point3 };

            // Define fill mode.
            System.Drawing.Drawing2D.FillMode newFillMode = System.Drawing.Drawing2D.FillMode.Winding;

            // Fill polygon to screen.
            g.FillPolygon(CurrentBrush, curvePoints, newFillMode);
        } // End Sub FillTriangle


        public static void DrawCircle(System.Drawing.Graphics g, System.Drawing.Pen pen,
                                  float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        } // End Sub DrawCircle


        public static void FillCircle(System.Drawing.Graphics g, System.Drawing.Brush brush,
                                      float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        } // End Sub FillCircle


        public static System.Drawing.Image GetHatchImage(int iHatchNumber, string strHtmlColor)
        {
            System.Drawing.Color targetcol = System.Drawing.ColorTranslator.FromHtml(strHtmlColor);
            return GetHatchImage(iHatchNumber, targetcol);
        } // End Function GetHatchImage


        public static System.Drawing.Image GetHatchImage(int iHatchNumber, System.Drawing.Color targetcol)
        {
            System.Drawing.Bitmap bmp = null;

#if TARGET //= "winexe" Or TARGET = "exe"
            string strBasePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            strBasePath = System.IO.Path.Combine(strBasePath, "hatches");
#else
            string strBasePath = System.Web.HttpContext.Current.Request.MapPath("~/images/VWS/hatches/");
#endif


            string strInput = System.IO.Path.Combine(strBasePath, "hatch" + iHatchNumber.ToString() + ".png");

            System.Drawing.Color sourcecol = System.Drawing.Color.Black;

            switch (iHatchNumber)
            {
                case 9:
                    sourcecol = System.Drawing.Color.FromArgb(255, 255, 65, 65); // hatch9.png
                    break;
                case 35:
                    sourcecol = System.Drawing.Color.FromArgb(255, 32, 119, 32); // hatch 35.png
                    sourcecol = System.Drawing.Color.HotPink;
                    break;
                case 69:
                    sourcecol = System.Drawing.Color.FromArgb(255, 0, 0, 22); // hatch 39.png
                    break;
                default:
                    sourcecol = System.Drawing.Color.HotPink;
                    throw new NotImplementedException("No sourcecolor for hatch pattern no. " + iHatchNumber.ToString() + " implemented.");
            } // End switch (iHatchNumber)


            //targetcol = System.Drawing.Color.HotPink;

            System.Drawing.Color coltrans = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            System.Drawing.Color coltrans2 = System.Drawing.Color.FromArgb(0, 0, 0, 0);


            Tools.ColorSpace.HSLColor sourcehsl = new Tools.ColorSpace.HSLColor(sourcecol);

            using (System.Drawing.Image img = System.Drawing.Image.FromFile(strInput))
            {
                bmp = new System.Drawing.Bitmap(img);

                for (int x = 0; x < bmp.Width; ++x)
                {
                    for (int y = 0; y < bmp.Height; ++y)
                    {
                        System.Drawing.Color col = bmp.GetPixel(x, y);

                        if (col == coltrans || col == coltrans2) continue;

                        //col = System.Drawing.Color.FromArgb(255, 255 - col.R, 255 - col.G, 255 - col.B);
                        Tools.ColorSpace.HSLColor hsl = new Tools.ColorSpace.HSLColor(col);


                        double deltalight = sourcehsl.Lightness - hsl.Lightness;
                        double deltasat = sourcehsl.Saturation - hsl.Saturation;


                        Tools.ColorSpace.HSLColor targethsl = new Tools.ColorSpace.HSLColor(targetcol);
                        targethsl.Lightness = targethsl.Lightness - deltalight;
                        if (targethsl.Hue != 0 || targethsl.Saturation != 0)
                            targethsl.Saturation = targethsl.Saturation - deltasat;


                        targethsl.Lightness = Math.Min(targethsl.Lightness, 1.0);
                        targethsl.Lightness = Math.Max(targethsl.Lightness, 0.0);

                        targethsl.Saturation = Math.Min(targethsl.Saturation, 1.0);
                        targethsl.Saturation = Math.Max(targethsl.Saturation, 0.0);


                        // targethsl.Lightness = hsl.Lightness;
                        // targethsl.Saturation = hsl.Saturation;
                        //if (targethsl.Hue != 0 || targethsl.Saturation != 0) targethsl.Saturation = hsl.Saturation;
                        //col = targetcol;
                        //bmp.MakeTransparent(System.Drawing.Color.White);
                        col = targethsl.Color;
                        bmp.SetPixel(x, y, col);
                    } // Next y

                } // Next x

            } // End Using (System.Drawing.Image img = System.Drawing.Image.FromFile(strInput)) 

            return bmp;
        } // End Function GetHatchImage


        public static System.Drawing.Brush GetBrush(int iLegendPattern, System.Drawing.Color colFC, System.Drawing.Color colHB, System.Drawing.Color colHL)
        {
            if (iLegendPattern == 12) // Normal, SolidBrush
            {
                return new System.Drawing.SolidBrush(colFC);
            }
            else if (iLegendPattern == 43) // MB: Leerstand, HatchBrush - HatchStyle.DarkUpwardDiagonal
            {
                return new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.DarkUpwardDiagonal, System.Drawing.Color.Black, System.Drawing.Color.White);
            }
            else if (iLegendPattern == 0) // 0 -- >40 Zeilen / >40 lignes, keine
            {
                return null; // Console.WriteLine("40 Zeilen / >40 lignes");
            }

            //else // [9: kein Mietobjekt,35: MB Mieter ], TextureBrush
            //Image image = Image.FromFile(@"D:\{0}\documents\visual studio 2010\Projects\DrawLegends\DrawLegends\hatches\hatch" + iLegendPattern + ".png");

            System.Drawing.TextureBrush tb = null;

            using (System.Drawing.Image image = GetHatchImage(iLegendPattern, colFC))
            {
                tb = new System.Drawing.TextureBrush(image, System.Drawing.Drawing2D.WrapMode.Tile);
            } // End Using Image image

            // tb.TranslateTransform(x, y);
            return tb;
        } // End Function GetBrush


        public static bool HasFont(string fontName)
        {
            System.Drawing.Text.FontCollection fontsCollection = new System.Drawing.Text.InstalledFontCollection();
            foreach (System.Drawing.FontFamily ffThisFontFamiliy in fontsCollection.Families)
            {
                // if (ffThisFontFamiliy.Name == fontName)
                if (StringComparer.OrdinalIgnoreCase.Equals(ffThisFontFamiliy.Name, fontName))
                    return true;
            } // Next ffThisFontFamiliy

            return false;
        } // End Function HasFont


    } // End Class cDrawingTools


} // End Namespace DrawLegends
