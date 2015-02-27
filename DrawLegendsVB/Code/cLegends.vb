
Namespace Portal.VWS.Legenden


    Public Class cLegends


        Public Shared Function MeasureString(s As String, font As System.Drawing.Font) As System.Drawing.SizeF
            Dim result As System.Drawing.SizeF
            Dim iCharsFilled As Integer
            Dim iLinesFilled As Integer

            Using image As System.Drawing.Image = New System.Drawing.Bitmap(1, 1)
                Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(image)
                    ' bmp.Width
                    ' bmp.Height

                    'result = g.MeasureString(s, font);
                    'g.MeasureString(s, font, int.MaxValue, StringFormat.GenericTypographic);
                    result = g.MeasureString(s, font, New System.Drawing.SizeF(600, 600), System.Drawing.StringFormat.GenericTypographic, iCharsFilled, iLinesFilled)
                    ' End Using System.Drawing.Graphics g 
                End Using
            End Using
            ' End Using image
            Return result
        End Function ' MeasureString 


        Public Shared Function GetLegend() As System.Drawing.Image
            Dim img As System.Drawing.Image = Nothing
            Dim maxsize As Integer = 1200
            Dim bmp As New System.Drawing.Bitmap(maxsize, maxsize)

            Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)
                g.Clear(System.Drawing.Color.White)
            End Using

            Using dt As System.Data.DataTable = cDrawingData.GetData()
                img = GetLegend("1010_GB01_EG00_0000", "de", dt, bmp, 0, 0)
            End Using

            Return img
        End Function ' GetLegend


        Public Shared Function GetLegend(strDWG As String, strSprache As String, dtLegendData As System.Data.DataTable, bmp As System.Drawing.Bitmap, origWidth As Integer, origHeight As Integer) As System.Drawing.Image
            'Random r = new Random();
            'Color[] cols = GetColors();
            'MsgBox(cols[123].ToKnownColor().ToString());
            Dim TextColor As System.Drawing.Color = System.Drawing.Color.Black
            Dim ValueColor As System.Drawing.Color = System.Drawing.Color.Black


            Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)
                ' Modify the image using g here... 
                'g.Clear(System.Drawing.Color.White);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit


                Dim strLogoFile As String = Portal.Web.config.GetAppSetting(Of String)("Legend_Logo", "cs_klein.png")
                Dim iBulletSize As Integer = Portal.Web.config.GetAppSetting(Of Integer)("Legend_Size", 8)
                Dim fWidth As Single = iBulletSize * 35

                Dim iOriginX As Integer = origWidth + iBulletSize
                Dim iOriginY As Integer = 20


                ' 8 ==> 125
                ' 10 ==> 150
                ' 15 ==> 225
                ' 20 ==> 300

                Using strFormat As New System.Drawing.StringFormat()
                    strFormat.Alignment = System.Drawing.StringAlignment.Far
                    strFormat.LineAlignment = System.Drawing.StringAlignment.Center

                    Dim strFont As String = "Tahoma"
                    '
                    '                    strFont = "Algerian";
                    '                    strFont = "Cambria";
                    '                    strFont = "Vivaldi";
                    '                    strFont = "Comic Sans MS";
                    '                    strFont = "GDT";
                    '                    strFont = "Wingdings";
                    '                    




                    Dim fs As System.Drawing.FontStyle = cDrawingTools.GetBestFontStyle(strFont)


                    Using fnt As New System.Drawing.Font(strFont, iBulletSize, fs)

                        Dim strLogoPath As String = Nothing


#If TARGET = "winexe" Or TARGET = "exe" Then
                        strLogoPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                        strLogoPath = System.IO.Path.Combine(strLogoPath, "logos")
                        strLogoPath = System.IO.Path.Combine(strLogoPath, strLogoFile)
#Else
                        strLogoPath = System.Web.HttpContext.Current.Request.MapPath("~/images/VWS/logos/" + strLogoFile)
#End If

                        If System.IO.File.Exists(strLogoPath) Then

                            Using imgLogo As System.Drawing.Image = System.Drawing.Image.FromFile(strLogoPath)
                                ' Draw base image
                                Dim iLogoX As Integer = CInt(iOriginX + 2 * iBulletSize - 7)
                                Dim iLogoY As Integer = CInt(Math.Truncate(-(0.34F * iBulletSize) + iOriginY + 0 * (iBulletSize * 2)))
                                Dim rect As New System.Drawing.Rectangle(iLogoX, iLogoY, imgLogo.Width, imgLogo.Height)
                                g.DrawImageUnscaledAndClipped(imgLogo, rect)
                                iOriginY = iOriginY + imgLogo.Height + 3 * iBulletSize
                            End Using ' System.Drawing.Image imgLogo 

                        End If ' System.IO.File.Exists(strLogoPath)


                        Using TextBrush As New System.Drawing.SolidBrush(TextColor)
                            ' Legendentext
                            g.DrawString(dtLegendData.TableName, fnt, TextBrush, New System.Drawing.PointF(iOriginX + 2 * iBulletSize - 7, -(0.34F * iBulletSize) + iOriginY + 0 * (iBulletSize * 2)))
                        End Using ' System.Drawing.SolidBrush TextBrush


                        Dim rectfValueTitleBoundary As New System.Drawing.RectangleF(iOriginX, iOriginY + 0 * (iBulletSize * 2), fWidth, iBulletSize + 2)
                        Using ValueBrush As New System.Drawing.SolidBrush(ValueColor)
                            ' Legendenwert
                            g.DrawString("Fläche", fnt, ValueBrush, rectfValueTitleBoundary, strFormat)
                        End Using ' System.Drawing.SolidBrush ValueBrush



                        'iOriginX = origWidth + iBulletSize;
                        'iOriginY = 20;

                        iOriginY = iOriginY + iBulletSize * 3




                        ' Using dt As System.Data.DataTable = cDrawingData.GetData()
                        '    dgvLegendData.DataSource = dt

                        For i As Integer = 0 To dtLegendData.Rows.Count - 1
                            Dim dr As System.Data.DataRow = dtLegendData.Rows(i)

                            Dim strFC As String = System.Convert.ToString(dr("Html_ForeColor"))
                            Dim strBC As String = System.Convert.ToString(dr("Html_BackColor"))
                            Dim strLC As String = System.Convert.ToString(dr("Html_LineColor"))

                            Dim colFC As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml(strFC)
                            Dim colHB As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml(strBC)
                            Dim colHL As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml(strLC)

                            Dim iLegendPattern As Integer = System.Convert.ToInt32(dr("AP_LEG_Pattern"))


                            ' Add legend rectangle with color 
                            Using CurrentBrush As System.Drawing.Brush = VWS.Legenden.cDrawingTools.GetBrush(iLegendPattern, colFC, colHB, colHL)
                                Dim rect2 As New System.Drawing.Rectangle(iOriginX, iOriginY + i * (iBulletSize * 2), iBulletSize, iBulletSize)

                                If CurrentBrush IsNot Nothing Then
                                    'if (object.ReferenceEquals(CurrentBrush, typeof(TextureBrush)))
                                    If TypeOf CurrentBrush Is System.Drawing.TextureBrush Then
                                        DirectCast(CurrentBrush, System.Drawing.TextureBrush).TranslateTransform(rect2.X, rect2.Y)
                                    End If

                                    ' Punkt - Quadrat
                                    g.FillRectangle(CurrentBrush, rect2)
                                    ' cDrawingTools.FillTriangle(g, CurrentBrush, iOriginX, iOriginY, i, iBulletSize)

                                    ' cDrawingTools.FillCircle(g, CurrentBrush, iOriginX + iBulletSize / 2.0F, iOriginY + i * (iBulletSize * 2) + iBulletSize / 2.0F, iBulletSize / 2.0F)
                                    ' End if (CurrentBrush != null)
                                End If
                            End Using
                            ' End Using System.Drawing.Brush CurrentBrush 
                            ' End of adding legend rectangle with color 


                            Dim strTitle As String = System.Convert.ToString(dr("Text"))
                            Using TextBrush As New System.Drawing.SolidBrush(TextColor)
                                ' Legendentext
                                If i = 0 Then
                                    Using fnt2 As New System.Drawing.Font(strFont, iBulletSize, fs Or Drawing.FontStyle.Bold)
                                        g.DrawString(strTitle, fnt2, TextBrush, New System.Drawing.PointF(iOriginX + 2 * iBulletSize - 7, -(0.34F * iBulletSize) + iOriginY + i * (iBulletSize * 2)))
                                    End Using ' fnt2
                                Else
                                    g.DrawString(strTitle, fnt, TextBrush, New System.Drawing.PointF(iOriginX + 2 * iBulletSize - 7, -(0.34F * iBulletSize) + iOriginY + i * (iBulletSize * 2)))
                                End If

                            End Using


                            Dim fff As System.Drawing.SizeF = MeasureString(strTitle, fnt)
                            Console.WriteLine(fff)


                            Dim dblValue As Double = System.Convert.ToDouble(dr("Value"))
                            'string strValue = string.Format("{0:f2}", fValue);
                            'string strValue = string.Format("{0:##0.00}", fValue);


                            ' System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
                            Dim ci As New System.Globalization.CultureInfo("de-CH")
                            ' ci = new System.Globalization.CultureInfo("en-US");


                            ' http://msdn.microsoft.com/en-us/library/0c899ak8.aspx
                            ' http://stackoverflow.com/questions/8941219/string-formatting-for-decimal-places-and-thousands
                            Dim strValue As String = String.Format(ci, "{0:#,0.00} m²", dblValue)
                            ci = Nothing



                            Dim rectfTextBoundaries2 As New System.Drawing.RectangleF(iOriginX, iOriginY + i * (iBulletSize * 2), fWidth, iBulletSize + 2)
                            Using ValueBrush As New System.Drawing.SolidBrush(ValueColor)
                                ' Legendenwert

                                If i = 0 Then
                                    Using fnt2 As New System.Drawing.Font(strFont, iBulletSize, fs Or Drawing.FontStyle.Bold)
                                        g.DrawString(strValue, fnt2, ValueBrush, rectfTextBoundaries2, strFormat)
                                    End Using ' fnt2
                                Else
                                    g.DrawString(strValue, fnt, ValueBrush, rectfTextBoundaries2, strFormat)
                                End If




                                ' Debug: Check alignment
                                ' g.DrawRectangle(System.Drawing.Pens.Black, rectfTextBoundaries2.X, rectfTextBoundaries2.Y - 2, rectfTextBoundaries2.Width, rectfTextBoundaries2.Height + 2);

                            End Using ' System.Drawing.SolidBrush ValueBrush
                        Next i

                        ' Add Foooter
                        'Bitmap m_Bitmap = new Bitmap(300, 300);
                        'renderer.Clear(System.Drawing.Color.White);
                        Dim strHTML As String = cDrawingData.GetLegendFooterHtml(strDWG, strSprache)

                        ' SizeF sfMeasuredSize = HtmlRenderer.HtmlRender.Measure(renderer, strHTML, 800); // Measure size of rendering
                        Dim sfMeasuredSize As System.Drawing.SizeF = HtmlRenderer.HtmlRender.MeasureGdiPlus(g, strHTML, 800)
                        ' Measure size of rendering
                        Dim point As New System.Drawing.PointF(iOriginX, bmp.Height - sfMeasuredSize.Height - 20)
                        ' Calculate positoin
                        Dim maxSize As New System.Drawing.SizeF(300, 300)
                        HtmlRenderer.HtmlRender.RenderGdiPlus(g, strHTML, point, maxSize)
                        ' HtmlRenderer.HtmlRender.Render(renderer, strHTML, point, maxSize);

                        'var img = HtmlRender.RenderToImageGdiPlus(html1, new Size(600, 200));
                        ' End Add Foooter

                        Using BorderColorPen As New System.Drawing.Pen(System.Drawing.Color.Black)
                            BorderColorPen.Width = 1
                            g.DrawRectangle(BorderColorPen, New System.Drawing.Rectangle(0, 0, bmp.Width - 1, bmp.Height - 1))

                            g.DrawLine(BorderColorPen, New System.Drawing.Point(origWidth, 0), New System.Drawing.Point(origWidth, bmp.Height))


                            'End Using ' System.Data.DataTable dt

                        End Using ' System.Drawing.Pen BorderColorPen

                    End Using ' System.Drawing.Font fnt

                End Using ' StringFormat strFormat

            End Using ' Graphics g

            Return bmp
        End Function ' GetLegend


    End Class ' cLegends


End Namespace ' Portal.VWS.Legenden
