
using System;
using System.Windows.Forms;

using System.Drawing;

using VWS.Legenden;


namespace DrawLegends
{


    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }


        private void btnDrawLegenden_Click(object sender, EventArgs e)
        {
            pbTestImage.Image = cLegends.GetLegend();
        }


        private void btnInitialTest_Click(object sender, EventArgs e)
        {
            //using(
            System.Drawing.Bitmap bmp = new Bitmap(600, 600);
            //){

            // http://stackoverflow.com/questions/15889637/how-do-i-draw-a-rectangle-onto-an-image-with-transparency-and-text
            

            // >= 27, <= 166

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Modify the image using g here... 
                //Create a brush with an alpha value and use the g.FillRectangle function

                g.Clear(System.Drawing.Color.White);


                //Color customColor = Color.FromArgb(50, Color.Gray);
                Color customColor = Color.FromArgb(100, Color.Gray);
                SolidBrush shadowBrush = new SolidBrush(customColor);
                Rectangle rect = new Rectangle(20, 20, 20, 20);
                g.FillRectangle(shadowBrush, rect);


                // http://stackoverflow.com/questions/11402862/how-to-draw-a-line-on-a-image
                Pen blackPen = new Pen(Color.Black, 1);
                g.DrawLine(blackPen, 0, 50, 600, 50);


                SolidBrush redBrush = new SolidBrush(System.Drawing.Color.Red);
                Rectangle rect2 = new Rectangle(20, 60, 20, 20);
                g.FillRectangle(redBrush, rect2);


                Pen grayPen = new Pen(Color.Gray, 1);
                g.DrawLine(grayPen, 0, 90, 600, 90);


                SolidBrush greenBrush = new SolidBrush(System.Drawing.Color.LightGreen);
                Rectangle rect3 = new Rectangle(20, 100, 20, 20);
                g.FillRectangle(greenBrush, rect3);

                //g.FillRectangles(shadowBrush, new RectangleF[] { rectFToFill });



                // http://stackoverflow.com/questions/17192431/drawing-text-on-image-c-sharp
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                //g.DrawString("My\nText", new Font("Tahoma", 20), Brushes.HotPink, new PointF(20, 20));
                g.DrawString("HNFS 1.1", new Font("Tahoma", 20), Brushes.HotPink, new PointF(60 - 7, 20 - 7));



                g.DrawString("HNFS 1.2", new Font("Tahoma", 20), Brushes.HotPink, new PointF(60 - 7, 60 - 7));


                StringFormat strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Far;
                strFormat.LineAlignment = StringAlignment.Center;


                RectangleF rectfTextBoundaries = new RectangleF(0, 20, 300, 20);
                g.DrawString("100.00", new Font("Tahoma", 20), Brushes.IndianRed, rectfTextBoundaries, strFormat);
                //g.DrawRectangle(Pens.Black, RectF2Rect(rectfTextBoundaries));
                g.DrawRectangle(Pens.Black, rectfTextBoundaries.X, rectfTextBoundaries.Y - 2, rectfTextBoundaries.Width, rectfTextBoundaries.Height + 3);



                RectangleF rectfTextBoundaries2 = new RectangleF(0, 60, 300, 20);
                g.DrawString("1100.00", new Font("Tahoma", 20), Brushes.IndianRed, rectfTextBoundaries2, strFormat);
                //g.DrawRectangle(Pens.Black, RectF2Rect(rectfTextBoundaries));
                g.DrawRectangle(Pens.Black, rectfTextBoundaries2.X, rectfTextBoundaries2.Y - 2, rectfTextBoundaries2.Width, rectfTextBoundaries2.Height + 3);


                /*
                using (Font font2 = new Font("Tahoma", 20, FontStyle.Regular, GraphicsUnit.Point))
                {
                    Rectangle rectText = new Rectangle(0, 13, 400, 30);

                    // Create a TextFormatFlags with word wrapping, horizontal center and 
                    // vertical center specified.
                    TextFormatFlags flags = TextFormatFlags.Right |
                        TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;



                    string text2 = "Some Text";

                    //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                    // Draw the text and the surrounding rectangle.
                    //TextRenderer.DrawText(g, text2, font2, rectText, Color.Blue, flags);

                    TextRenderer.DrawText(g, text2, font2, rectText, Color.HotPink, Color.White, TextFormatFlags.Right);
                    g.DrawRectangle(Pens.Black, rectText);
                }
                */


                Pen vioPen = new Pen(Color.Violet, 1);
                g.DrawLine(vioPen, 301, 0, 301, 300);

            } // End using (Graphics g = Graphics.FromImage(bmp))

            pbTestImage.Image = bmp;
            //}

        } // End Sub btnInitialTest_Click


        private void btnGetData_Click(object sender, EventArgs e)
        {

            using (System.Data.DataTable dt = cDrawingData.GetData())
            {
                dgvLegendData.DataSource = dt;
            }

        } // End Sub btnGetData_Click



        private void btnHatchStyleTest_Click(object sender, EventArgs e)
        {
            System.Drawing.Bitmap bmp = new Bitmap(1500, 1500);


            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Modify the image using g here... 
                //Create a brush with an alpha value and use the g.FillRectangle function

                g.Clear(System.Drawing.Color.White);

                //SolidBrush greenBrush = new SolidBrush(System.Drawing.Color.LightGreen);
                //Rectangle rect3 = new Rectangle(20, 100, 200, 200);
                //g.FillRectangle(greenBrush, rect3);


                //HatchBrush hBrush = new HatchBrush(HatchStyle.Horizontal, Color.Red, Color.FromArgb(255, 128, 255, 255));


                System.Drawing.Drawing2D.HatchStyle mystyle = System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal;


                // System = Aperture
                // 0 = 38
                // 9 = 36
                // 39 = 62
                // 38 = 64


                //g.FillEllipse(hBrush, 0, 0, 100, 60);


                System.Drawing.Drawing2D.HatchStyle[] hss = cDrawingTools.GetHatchStyles();

                for (int i = 0; i < 53; ++i) // hss.Length; ++i)
                {
                    int j = i % 7;
                    int k = i / 7;

                    mystyle = (System.Drawing.Drawing2D.HatchStyle)i;
                    // mystyle = (HatchStyle)21;
                    //mystyle = HatchStyle.DarkUpwardDiagonal;

                    string strHatchStyle = mystyle.ToString();
                    strHatchStyle = i.ToString();

                    //mystyle = HatchStyle.Cross | HatchStyle.DarkHorizontal;

                    System.Drawing.Drawing2D.HatchBrush hBrush;


                    if (i == 53 || i == 54 || i == 55)
                        continue;
                    //hBrush = new HatchBrush(mystyle, Color.Black);
                    //else
                    //SolidBrush alwaysBrush = new SolidBrush(System.Drawing.Color.HotPink);
                    hBrush = new System.Drawing.Drawing2D.HatchBrush(mystyle, Color.Black, System.Drawing.Color.White);

                    int iSize = 50;

                    Rectangle myrect = new Rectangle(25 + j * 5 + (iSize + 25) * j*2, 25 + k * 5 + (iSize + 25) * k, iSize, iSize);
                    g.FillRectangle(hBrush, myrect);


                    using (System.Drawing.Font fnt = new System.Drawing.Font("Verdana", 8))
                    {
                        using (System.Drawing.SolidBrush sb = new SolidBrush(System.Drawing.Color.HotPink))
                        {
                            g.DrawString(strHatchStyle, fnt, sb, new PointF(myrect.X, myrect.Y + iSize));
                            g.DrawString(strHatchStyle, fnt, sb, new PointF(myrect.X, myrect.Y + iSize));
                        }
                    }
                } // Next i


            } // End Using Graphics g 

            this.pbTestImage.Image = bmp;
        } // End Sub btnHatchStyleTest_Click


        private void btnTextureTest_Click(object sender, EventArgs e)
        {
            System.Drawing.Bitmap bmp = new Bitmap(600, 600);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Modify the image using g here... 
                //Create a brush with an alpha value and use the g.FillRectangle function
                g.Clear(System.Drawing.Color.White);


                string dn = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                dn = System.IO.Path.Combine(dn, "../../");
                dn = System.IO.Path.Combine(dn, "hatches");
                // dn = System.IO.Path.Combine(dn, "hatch35.png");
                dn = System.IO.Path.Combine(dn, "hatch9.png");
                
                dn = System.IO.Path.GetFullPath(dn);
                using (Image image = Image.FromFile(dn))
                {

                    //TextureBrush tbrush = new TextureBrush(image, new Rectangle(95, 0, 50, 55));
                    //TextureBrush tbrush = new TextureBrush(image, new Rectangle(0, 0, image.Width, image.Height));
                    using (TextureBrush tbrush = new TextureBrush(image, System.Drawing.Drawing2D.WrapMode.Tile))
                    {
                        int x = 100;
                        int y = 100;
                        tbrush.TranslateTransform(x, y);
                        //tbrush.TranslateTransform(x, -y);
                        g.FillRectangle(tbrush, new Rectangle(x, y, 200, 200));
                    } // End Using TextureBrush tbrush

                } // End Using Image image 

            } // End Using Graphics g 

            this.pbTestImage.Image = bmp;
        } // End Sub btnTextureTest_Click


        private void btnRecolorTexture_Click(object sender, EventArgs e)
        {
            //System.Drawing.Image bmp = GetHatchImage(35, "#006400");
            //System.Drawing.Image bmp = GetHatchImage(9, "#FC4644");
            //bmp.Save(@"D:\{0}\Documents\Visual Studio 2010\Projects\COR-Basic\Basic\Basic\stylizer\pattern\oops.png", System.Drawing.Imaging.ImageFormat.Png);

            //this.pbTestImage.Image = bmp;

            System.Drawing.Bitmap bmp = new Bitmap(600, 600);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Modify the image using g here... 
                //Create a brush with an alpha value and use the g.FillRectangle function

                g.Clear(System.Drawing.Color.White);

                using (System.Drawing.Image image = cDrawingTools.GetHatchImage(35, "#006400"))
                {
                    //TextureBrush tbrush = new TextureBrush(image, new Rectangle(95, 0, 50, 55));
                    //TextureBrush tbrush = new TextureBrush(image, new Rectangle(0, 0, image.Width, image.Height));
                    using (TextureBrush tbrush = new TextureBrush(image, System.Drawing.Drawing2D.WrapMode.Tile))
                    {
                        int x = 100;
                        int y = 100;
                        tbrush.TranslateTransform(x, y);
                        //tbrush.TranslateTransform(x, -y);
                        g.FillRectangle(tbrush, new Rectangle(x, y, 200, 200));
                    } // End Using TextureBrush tbrush

                } // End Using System.Drawing.Image image

            } // End Using Graphics g 

            this.pbTestImage.Image = bmp;
        } // End Sub btnRecolorTexture_Click


        private void btnColors_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("num", typeof(int));
            dt.Columns.Add("HTML", typeof(string));
            dt.Columns.Add("RGB", typeof(string));
            dt.Columns.Add("R", typeof(int));
            dt.Columns.Add("G", typeof(int));
            dt.Columns.Add("B", typeof(int));

            // System.Drawing.Color c = System.Drawing.ColorTranslator.FromHtml("#F5F7F8");
            // string strHtmlColor = System.Drawing.ColorTranslator.ToHtml(c);


            // Already in T_SYS_ApertureColorToHex
            // string str = string.Format(@"D:\{0}\Documents\Visual Studio 2010\Projects\COR-Basic\Basic\Basic\stylizer\farben", System.Environment.UserName);
            string str = string.Format(@"D:\{0}\Documents\Visual Studio 2013\TFS\COR-Basic\COR-Basic\Basic\Basic\stylizer\farben", System.Environment.UserName);

            string[] filez = System.IO.Directory.GetFiles(str, "*.gif");
            foreach (string strFile in filez)
            {
                string strNumber = System.IO.Path.GetFileNameWithoutExtension(strFile);
                
                using (System.Drawing.Image img = System.Drawing.Image.FromFile(strFile))
                {

                    using (System.Drawing.Bitmap bmp = new Bitmap(img))
                    {
                        System.Drawing.Color col = bmp.GetPixel(10, 10);
                        string strRGB = string.Format("rgb({0},{1},{2})", col.R, col.G, col.B);
                        string strHtmlColor = System.Drawing.ColorTranslator.ToHtml(col);

                        System.Data.DataRow dr = dt.NewRow();
                        dr["num"] = strNumber;
                        dr["HTML"] = strHtmlColor;
                        dr["RGB"] = strRGB;
                        dr["R"]=col.R;
                        dr["G"]=col.G;
                        dr["B"] = col.B;

                        dt.Rows.Add(dr);
                    } // End Using System.Drawing.Bitmap bmp

                } // End Using System.Drawing.Image img

                this.dgvLegendData.DataSource = dt;

                // http://stackoverflow.com/questions/12634457/how-can-i-set-the-sort-column-order-and-glyph-on-a-datagridview-column-for-th
                this.dgvLegendData.Sort(this.dgvLegendData.Columns[0], System.ComponentModel.ListSortDirection.Ascending);

            } // Next strFile

        } // End Sub btnColors_Click


        private void btnGetColorData_Click(object sender, EventArgs e)
        {
            using (System.Data.DataTable dt = cDrawingData.GetColorTranslations())
            {
                this.dgvLegendData.DataSource = dt;
            }
        } // End Sub btnGetColorData


        private void btnChooseBrush_Click(object sender, EventArgs e)
        {
            //System.Drawing.Image img2 = System.Drawing.Image.FromFile(@"d:\{0}\documents\visual studio 2010\Projects\DrawLegends\DrawLegends\ApDrawingImages.png");
            //this.pbTestImage.Image = img2;

            using (System.Data.DataTable dt = cDrawingData.GetData())
            {
                //System.Drawing.Image img = new Bitmap("path/resource");
                string dn = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                dn = System.IO.Path.Combine(dn, "../../");
                dn = System.IO.Path.Combine(dn, "ApDrawingImages.png");


                System.Drawing.Image img = System.Drawing.Image.FromFile(dn);
                Bitmap newImage = new Bitmap(img.Width + 400, img.Height + 0);
                using (Graphics g = Graphics.FromImage(newImage))
                {
                    g.Clear(System.Drawing.Color.White);

                    // Draw base image
                    Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
                    g.DrawImageUnscaledAndClipped(img, rect);

                    //g.FillRectangle(new SolidBrush(Color.HotPink), 0, img.Height - 45, newImage.Width, 40);
                    //Font font = new Font("Times New Roman", 22.0f);
                    //PointF point = new PointF(20, img.Height - 40); // Text xy-start-position
                    //g.DrawString("Copyright (C) 2014 COR Managementsysteme GmbH", font, Brushes.Red, point);
                } // End Using Graphics g 

                this.pbTestImage.Image = cLegends.GetLegend(dt, newImage, img.Width, img.Height);

                this.pbTestImage.Image.Save(@"d:\aalol.png");


            } // End Using System.Data.DataTable dt

        } // End Sub btnChooseBrush_Click


        private void btnRenderHTML_Click(object sender, EventArgs e)
        {

            // http://dean.edwards.name/my/base64-ie.html
            string dn = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            dn = System.IO.Path.Combine(dn, "../../");
            dn = System.IO.Path.Combine(dn, "ApDrawingImages.png");
            dn = System.IO.Path.GetFullPath(dn);

            System.Drawing.Image img = System.Drawing.Image.FromFile(dn);
            Bitmap m_Bitmap = new Bitmap(img.Width + 500, img.Height + 0);
            using (Graphics g = Graphics.FromImage(m_Bitmap))
            {
                g.Clear(System.Drawing.Color.White);

                // Draw base image
                Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
                g.DrawImageUnscaledAndClipped(img, rect);
            } // End Using Graphics g


            //Bitmap m_Bitmap = new Bitmap(300, 300);
            using(System.Drawing.Graphics renderer = Graphics.FromImage(m_Bitmap))
            {
                //renderer.Clear(System.Drawing.Color.White);

                string strHTML = cDrawingData.GetLegendFooterHtml();

                // SizeF sfMeasuredSize = HtmlRenderer.HtmlRender.Measure(renderer, strHTML, 800); // Measure size of rendering
                SizeF sfMeasuredSize = HtmlRenderer.HtmlRender.MeasureGdiPlus(renderer, strHTML, 800); // Measure size of rendering
                PointF point = new PointF(img.Width, img.Height - sfMeasuredSize.Height - 20); // Calculate positoin
                SizeF maxSize = new System.Drawing.SizeF(300, 300);

                HtmlRenderer.HtmlRender.RenderGdiPlus(renderer, strHTML, point, maxSize);
                // HtmlRenderer.HtmlRender.Render(renderer, strHTML, point, maxSize);

                //var img = HtmlRender.RenderToImageGdiPlus(html1, new Size(600, 200));
            } // End Using renderer

            this.pbTestImage.Image = m_Bitmap;
        } // End Sub btnRenderHTML_Click


    } // End Class Form1 : Form


} // End Namespace DrawLegends
