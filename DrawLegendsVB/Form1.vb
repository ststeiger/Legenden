
Imports DrawLegendsVB.Portal.VWS.Legenden


Public Class Form1


    Private Sub btnInitialTest_Click(sender As System.Object, e As System.EventArgs) Handles btnInitialTest.Click
        'using(
        Dim bmp As System.Drawing.Bitmap = New Bitmap(600, 600)
        '){

        ' http://stackoverflow.com/questions/15889637/how-do-i-draw-a-rectangle-onto-an-image-with-transparency-and-text


        ' >= 27, <= 166

        Using g As Graphics = Graphics.FromImage(bmp)
            ' Modify the image using g here... 
            'Create a brush with an alpha value and use the g.FillRectangle function

            g.Clear(System.Drawing.Color.White)


            'Color customColor = Color.FromArgb(50, Color.Gray);
            Dim customColor As Color = Color.FromArgb(100, Color.Gray)
            Dim shadowBrush As New SolidBrush(customColor)
            Dim rect As New Rectangle(20, 20, 20, 20)
            g.FillRectangle(shadowBrush, rect)


            ' http://stackoverflow.com/questions/11402862/how-to-draw-a-line-on-a-image
            Dim blackPen As New Pen(Color.Black, 1)
            g.DrawLine(blackPen, 0, 50, 600, 50)


            Dim redBrush As New SolidBrush(System.Drawing.Color.Red)
            Dim rect2 As New Rectangle(20, 60, 20, 20)
            g.FillRectangle(redBrush, rect2)


            Dim grayPen As New Pen(Color.Gray, 1)
            g.DrawLine(grayPen, 0, 90, 600, 90)


            Dim greenBrush As New SolidBrush(System.Drawing.Color.LightGreen)
            Dim rect3 As New Rectangle(20, 100, 20, 20)
            g.FillRectangle(greenBrush, rect3)

            'g.FillRectangles(shadowBrush, new RectangleF[] { rectFToFill });



            ' http://stackoverflow.com/questions/17192431/drawing-text-on-image-c-sharp
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
            'g.DrawString("My\nText", new Font("Tahoma", 20), Brushes.HotPink, new PointF(20, 20));
            g.DrawString("HNFS 1.1", New Font("Tahoma", 20), Brushes.HotPink, New PointF(60 - 7, 20 - 7))



            g.DrawString("HNFS 1.2", New Font("Tahoma", 20), Brushes.HotPink, New PointF(60 - 7, 60 - 7))


            Dim strFormat As New StringFormat()
            strFormat.Alignment = StringAlignment.Far
            strFormat.LineAlignment = StringAlignment.Center


            Dim rectfTextBoundaries As New RectangleF(0, 20, 300, 20)
            g.DrawString("100.00", New Font("Tahoma", 20), Brushes.IndianRed, rectfTextBoundaries, strFormat)
            'g.DrawRectangle(Pens.Black, RectF2Rect(rectfTextBoundaries));
            g.DrawRectangle(Pens.Black, rectfTextBoundaries.X, rectfTextBoundaries.Y - 2, rectfTextBoundaries.Width, rectfTextBoundaries.Height + 3)



            Dim rectfTextBoundaries2 As New RectangleF(0, 60, 300, 20)
            g.DrawString("1100.00", New Font("Tahoma", 20), Brushes.IndianRed, rectfTextBoundaries2, strFormat)
            'g.DrawRectangle(Pens.Black, RectF2Rect(rectfTextBoundaries));
            g.DrawRectangle(Pens.Black, rectfTextBoundaries2.X, rectfTextBoundaries2.Y - 2, rectfTextBoundaries2.Width, rectfTextBoundaries2.Height + 3)


            '
            '                using (Font font2 = new Font("Tahoma", 20, FontStyle.Regular, GraphicsUnit.Point))
            '                {
            '                    Rectangle rectText = new Rectangle(0, 13, 400, 30);
            '
            '                    // Create a TextFormatFlags with word wrapping, horizontal center and 
            '                    // vertical center specified.
            '                    TextFormatFlags flags = TextFormatFlags.Right |
            '                        TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
            '
            '
            '
            '                    string text2 = "Some Text";
            '
            '                    //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            '
            '                    // Draw the text and the surrounding rectangle.
            '                    //TextRenderer.DrawText(g, text2, font2, rectText, Color.Blue, flags);
            '
            '                    TextRenderer.DrawText(g, text2, font2, rectText, Color.HotPink, Color.White, TextFormatFlags.Right);
            '                    g.DrawRectangle(Pens.Black, rectText);
            '                }
            '                



            Dim vioPen As New Pen(Color.Violet, 1)

            g.DrawLine(vioPen, 301, 0, 301, 300)
        End Using ' (Graphics g = Graphics.FromImage(bmp))
        pbTestImage.Image = bmp
        '}

    End Sub ' btnInitialTest_Click


    Private Sub btnHatchStyleTest_Click(sender As System.Object, e As System.EventArgs) Handles btnHatchStyleTest.Click
        Dim bmp As System.Drawing.Bitmap = New Bitmap(600, 600)


        Using g As Graphics = Graphics.FromImage(bmp)
            ' Modify the image using g here... 
            'Create a brush with an alpha value and use the g.FillRectangle function

            g.Clear(System.Drawing.Color.White)

            'SolidBrush greenBrush = new SolidBrush(System.Drawing.Color.LightGreen);
            'Rectangle rect3 = new Rectangle(20, 100, 200, 200);
            'g.FillRectangle(greenBrush, rect3);



            'HatchStyle.


            'HatchBrush hBrush = new HatchBrush(HatchStyle.Horizontal, Color.Red, Color.FromArgb(255, 128, 255, 255));


            Dim mystyle As System.Drawing.Drawing2D.HatchStyle = System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal


            ' System = Aperture
            ' 0 = 38
            ' 9 = 36
            ' 39 = 62
            ' 38 = 64


            'g.FillEllipse(hBrush, 0, 0, 100, 60);


            Dim hss As System.Drawing.Drawing2D.HatchStyle() = cDrawingTools.GetHatchStyles()

            For i As Integer = 0 To 52
                ' hss.Length; ++i)
                Dim j As Integer = i Mod 7
                Dim k As Integer = i / 7

                mystyle = DirectCast(i, System.Drawing.Drawing2D.HatchStyle)
                ' mystyle = (HatchStyle)21;
                'mystyle = HatchStyle.DarkUpwardDiagonal;


                'mystyle = HatchStyle.Cross | HatchStyle.DarkHorizontal;

                Dim hBrush As System.Drawing.Drawing2D.HatchBrush


                If i = 53 OrElse i = 54 OrElse i = 55 Then
                    Continue For
                End If
                'hBrush = new HatchBrush(mystyle, Color.Black);
                'else
                'SolidBrush alwaysBrush = new SolidBrush(System.Drawing.Color.HotPink);
                hBrush = New System.Drawing.Drawing2D.HatchBrush(mystyle, Color.Black, System.Drawing.Color.White)

                Dim iSize As Integer = 40

                Dim myrect As New Rectangle(25 + j * 5 + (iSize + 25) * j, 25 + k * 5 + (iSize + 25) * k, iSize, iSize)
                g.FillRectangle(hBrush, myrect)
            Next i
        End Using ' Graphics g 

        Me.pbTestImage.Image = bmp
    End Sub ' btnHatchStyleTest_Click


    Private Sub btnGetColorData_Click(sender As System.Object, e As System.EventArgs) Handles btnGetColorData.Click

        Using dt As System.Data.DataTable = cDrawingData.GetColorTranslations()
            Me.dgvLegendData.DataSource = dt
        End Using

    End Sub ' btnGetColorData_Click


    Private Sub btnDrawLegenden_Click(sender As System.Object, e As System.EventArgs) Handles btnDrawLegenden.Click
        pbTestImage.Image = cLegends.GetLegend()
    End Sub ' btnDrawLegenden_Click


    Private Sub btnTextureTest_Click(sender As System.Object, e As System.EventArgs) Handles btnTextureTest.Click

        Dim bmp As System.Drawing.Bitmap = New Bitmap(600, 600)

        Using g As Graphics = Graphics.FromImage(bmp)
            ' Modify the image using g here... 
            'Create a brush with an alpha value and use the g.FillRectangle function
            g.Clear(System.Drawing.Color.White)

            ' Image image = Image.FromFile(@"D:\{0}\documents\visual studio 2010\Projects\DrawLegends\DrawLegends\hatches\hatch35.png");
            ' Get from assembly. fixed only in C# version

            Using image__1 As Image = Image.FromFile(String.Format("D:\{0}\documents\visual studio 2010\Projects\DrawLegends\DrawLegends\hatches\hatch9.png", Environment.UserName))
                'TextureBrush tbrush = new TextureBrush(image, new Rectangle(95, 0, 50, 55));
                'TextureBrush tbrush = new TextureBrush(image, new Rectangle(0, 0, image.Width, image.Height));
                Using tbrush As New TextureBrush(image__1, System.Drawing.Drawing2D.WrapMode.Tile)
                    Dim x As Integer = 100
                    Dim y As Integer = 100
                    tbrush.TranslateTransform(x, y)
                    'tbrush.TranslateTransform(x, -y);
                    g.FillRectangle(tbrush, New Rectangle(x, y, 200, 200))
                End Using
            End Using
        End Using
        ' End Using Graphics g 
        Me.pbTestImage.Image = bmp

    End Sub ' btnTextureTest_Click


    Private Sub btnGetData_Click(sender As System.Object, e As System.EventArgs) Handles btnGetData.Click
        Using dt As System.Data.DataTable = cDrawingData.GetData()
            dgvLegendData.DataSource = dt
        End Using
    End Sub ' btnGetData_Click


    Private Sub btnChooseBrush_Click(sender As System.Object, e As System.EventArgs) Handles btnChooseBrush.Click

        'System.Drawing.Image img2 = System.Drawing.Image.FromFile(string.Format(@"d:\{0}\documents\visual studio 2010\Projects\DrawLegends\DrawLegends\ApDrawingImages.png", Environment.UserName));
        'this.pbTestImage.Image = img2;

        Using dt As System.Data.DataTable = cDrawingData.GetData()
            'System.Drawing.Image img = new Bitmap("path/resource");
            Dim img As System.Drawing.Image = System.Drawing.Image.FromFile(String.Format("d:\{0}\documents\visual studio 2010\Projects\DrawLegends\DrawLegends\ApDrawingImages.png", Environment.UserName))
            Dim newImage As New Bitmap(img.Width + 400, img.Height + 0)
            Using g As Graphics = Graphics.FromImage(newImage)
                g.Clear(System.Drawing.Color.White)

                ' Draw base image
                Dim rect As New Rectangle(0, 0, img.Width, img.Height)

                'g.FillRectangle(new SolidBrush(Color.HotPink), 0, img.Height - 45, newImage.Width, 40);
                'Font font = new Font("Times New Roman", 22.0f);
                'PointF point = new PointF(20, img.Height - 40); // Text xy-start-position
                'g.DrawString("Copyright (C) 2014 COR Managementsysteme GmbH", font, Brushes.Red, point);
                g.DrawImageUnscaledAndClipped(img, rect)
            End Using ' g
            ' End Using Graphics g 
            Me.pbTestImage.Image = cLegends.GetLegend("dwg", "de", dt, newImage, img.Width, img.Height)



            Me.pbTestImage.Image.Save("d:\aalol.png")
        End Using ' System.Data.DataTable dt

    End Sub ' btnChooseBrush_Click


    Private Sub btnRecolorTexture_Click(sender As System.Object, e As System.EventArgs) Handles btnRecolorTexture.Click

        'System.Drawing.Image bmp = GetHatchImage(35, "#006400");
        'System.Drawing.Image bmp = GetHatchImage(9, "#FC4644");
        'bmp.Save(@"D:\{0}\Documents\Visual Studio 2010\Projects\COR-Basic\Basic\Basic\stylizer\pattern\oops.png", System.Drawing.Imaging.ImageFormat.Png);

        'this.pbTestImage.Image = bmp;

        Dim bmp As System.Drawing.Bitmap = New Bitmap(600, 600)

        Using g As Graphics = Graphics.FromImage(bmp)
            ' Modify the image using g here... 
            'Create a brush with an alpha value and use the g.FillRectangle function

            g.Clear(System.Drawing.Color.White)

            ' Image image = Image.FromFile(@"D:\{0}\documents\visual studio 2010\Projects\DrawLegends\DrawLegends\hatches\hatch35.png");
            'Image image = Image.FromFile(@"D:\{0}\documents\visual studio 2010\Projects\DrawLegends\DrawLegends\hatches\hatch9.png");
            Using image As System.Drawing.Image = cDrawingTools.GetHatchImage(35, "#006400")
                'TextureBrush tbrush = new TextureBrush(image, new Rectangle(95, 0, 50, 55));
                'TextureBrush tbrush = new TextureBrush(image, new Rectangle(0, 0, image.Width, image.Height));
                Using tbrush As New TextureBrush(image, System.Drawing.Drawing2D.WrapMode.Tile)
                    Dim x As Integer = 100
                    Dim y As Integer = 100
                    tbrush.TranslateTransform(x, y)
                    'tbrush.TranslateTransform(x, -y);
                    g.FillRectangle(tbrush, New Rectangle(x, y, 200, 200))
                End Using ' tbrush

            End Using ' image

        End Using ' Graphics g 

        Me.pbTestImage.Image = bmp
    End Sub ' btnRecolorTexture_Click


    Private Sub btnColors_Click(sender As System.Object, e As System.EventArgs) Handles btnColors.Click

        Dim dt As New System.Data.DataTable()
        dt.Columns.Add("num", GetType(Integer))
        dt.Columns.Add("HTML", GetType(String))
        dt.Columns.Add("RGB", GetType(String))
        dt.Columns.Add("R", GetType(Integer))
        dt.Columns.Add("G", GetType(Integer))
        dt.Columns.Add("B", GetType(Integer))

        ' System.Drawing.Color c = System.Drawing.ColorTranslator.FromHtml("#F5F7F8");
        ' string strHtmlColor = System.Drawing.ColorTranslator.ToHtml(c);


        ' Already in T_SYS_ApertureColorToHex
        Dim filez As String() = System.IO.Directory.GetFiles(String.Format("D:\{0}\Documents\Visual Studio 2010\Projects\COR-Basic\Basic\Basic\stylizer\farben", Environment.UserName), "*.gif")
        For Each strFile As String In filez
            Dim strNumber As String = System.IO.Path.GetFileNameWithoutExtension(strFile)

            Using img As System.Drawing.Image = System.Drawing.Image.FromFile(strFile)
                Using bmp As System.Drawing.Bitmap = New Bitmap(img)
                    Dim col As System.Drawing.Color = bmp.GetPixel(10, 10)
                    Dim strRGB As String = String.Format("rgb({0},{1},{2})", col.R, col.G, col.B)
                    Dim strHtmlColor As String = System.Drawing.ColorTranslator.ToHtml(col)

                    Dim dr As System.Data.DataRow = dt.NewRow()
                    dr("num") = strNumber
                    dr("HTML") = strHtmlColor
                    dr("RGB") = strRGB
                    dr("R") = col.R
                    dr("G") = col.G
                    dr("B") = col.B

                    dt.Rows.Add(dr)

                End Using ' System.Drawing.Bitmap bmp

            End Using ' System.Drawing.Image img

            Me.dgvLegendData.DataSource = dt

            ' http://stackoverflow.com/questions/12634457/how-can-i-set-the-sort-column-order-and-glyph-on-a-datagridview-column-for-th

            Me.dgvLegendData.Sort(Me.dgvLegendData.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
        Next strFile

    End Sub ' btnColors_Click


    Private Sub btnRenderHTML_Click(sender As System.Object, e As System.EventArgs) Handles btnRenderHTML.Click
        ' http://dean.edwards.name/my/base64-ie.html

        Dim img As System.Drawing.Image = System.Drawing.Image.FromFile(String.Format("d:\{0}\documents\visual studio 2010\Projects\DrawLegends\DrawLegends\ApDrawingImages.png", Environment.UserName))
        Dim m_Bitmap As New Bitmap(img.Width + 500, img.Height + 0)
        Using g As Graphics = Graphics.FromImage(m_Bitmap)
            g.Clear(System.Drawing.Color.White)

            ' Draw base image
            Dim rect As New Rectangle(0, 0, img.Width, img.Height)
            g.DrawImageUnscaledAndClipped(img, rect)
        End Using


        'Bitmap m_Bitmap = new Bitmap(300, 300);
        Using renderer As System.Drawing.Graphics = Graphics.FromImage(m_Bitmap)
            'renderer.Clear(System.Drawing.Color.White);


            Dim strHTML As String = cDrawingData.GetLegendFooterHtml("dwg", "de")

            ' SizeF sfMeasuredSize = HtmlRenderer.HtmlRender.Measure(renderer, strHTML, 800); // Measure size of rendering
            Dim sfMeasuredSize As SizeF = HtmlRenderer.HtmlRender.MeasureGdiPlus(renderer, strHTML, 800)
            ' Measure size of rendering
            Dim point As New PointF(img.Width, img.Height - sfMeasuredSize.Height - 20)
            ' Calculate positoin
            Dim maxSize As SizeF = New System.Drawing.SizeF(300, 300)

            ' HtmlRenderer.HtmlRender.Render(renderer, strHTML, point, maxSize);

            'var img = HtmlRender.RenderToImageGdiPlus(html1, new Size(600, 200));
            HtmlRenderer.HtmlRender.RenderGdiPlus(renderer, strHTML, point, maxSize)
        End Using

        Me.pbTestImage.Image = m_Bitmap
    End Sub ' btnRenderHTML_Click


    Private Sub btnToPdf_Click(sender As System.Object, e As System.EventArgs) Handles btnToPdf.Click
        Dim strURL As String = "http://www.cor-management.ch/corwebsite/pictures/Titelbild_01.jpg"
        ' Tools.Web.DownloadHelper.GetFileAsByteArray(strURL, @"d:\test1.jpg");
        ' Tools.Web.DownloadHelper.GetHeaders(strURL);
        ' Tools.Web.DownloadHelper.SaveFileToPath(strURL);

        Dim ba As Byte() = Portal.Tools.Web.DownloadHelper.GetFileAsByteArray(strURL)
        'Dim baPDF As Byte() = Tools.FileFormat.PDF.ImageDataToPdfData(ba)
        Dim baPDF As Byte() = Tools.FileFormat.PDF.ImageToPdfData(System.Drawing.Image.FromFile("d:\aalol.png"))

        System.IO.File.WriteAllBytes("d:\myimage.pdf", baPDF)
    End Sub


End Class
