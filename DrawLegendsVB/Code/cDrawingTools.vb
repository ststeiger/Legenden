
Imports System.Collections.Generic
Imports System.Text


Namespace Portal.VWS.Legenden


    Public Class cDrawingTools


        Public Shared Function GetErrorAsImage(ex As Exception) As Byte()
            Return GetErrorAsImage(ex.Message)
        End Function


        Public Shared Function GetErrorAsImage(ByVal strErrorText As String) As Byte()
            Dim baReturnValue As Byte() = Nothing

            If String.IsNullOrEmpty(strErrorText) Then
                'strErrorText = "This is a very long string that might just not fit on one line entirely hello hello you test"
                strErrorText = "Unbekannter Fehler." + Environment.NewLine
                strErrorText += "Unknown error." + Environment.NewLine
                strErrorText += "Erreur inconnue." + Environment.NewLine
                strErrorText += "Errore sconosciuto."
            End If


            Using bmp As New System.Drawing.Bitmap(1204, 856)

                Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmp)
                    g.Clear(System.Drawing.Color.White)

                    Dim strFont As String = "Tahoma"
                    ' strFont = "Algerian"
                    ' strFont = "Cambria"
                    ' strFont = "Vivaldi"
                    strFont = "Comic Sans MS"
                    ' strFont = "GDT"
                    ' strFont = "Wingdings"
                    '                    

                    Dim fs As System.Drawing.FontStyle = Portal.VWS.Legenden.cDrawingTools.GetBestFontStyle(strFont)
                    fs = Drawing.FontStyle.Bold Or Drawing.FontStyle.Italic


                    Dim rectfTextBoundaries2 As New System.Drawing.RectangleF(0, 0, bmp.Width, bmp.Height)


                    Using fnt As System.Drawing.Font = New System.Drawing.Font(strFont, 36, fs)

                        Using TextBrush As New System.Drawing.SolidBrush(System.Drawing.Color.Crimson)
                            ' ErrorText
                            'g.DrawString(strErrorText, fnt, TextBrush, New System.Drawing.PointF(25, 390))

                            Using strFormat As New System.Drawing.StringFormat()
                                strFormat.Alignment = System.Drawing.StringAlignment.Near
                                strFormat.LineAlignment = System.Drawing.StringAlignment.Center ' Vertical

                                g.DrawString(strErrorText, fnt, TextBrush, rectfTextBoundaries2, strFormat)
                            End Using

                        End Using ' System.Drawing.SolidBrush TextBrush

                    End Using '  System.Drawing.Font fnt

                End Using ' System.Drawing.Graphics g 

                baReturnValue = Portal.Tools.Web.DownloadHelper.ImageToByteArray(bmp, System.Drawing.Imaging.ImageFormat.Png)
            End Using

            Return baReturnValue
        End Function ' ProcessRequest


        Public Shared Function GetBestFontStyle(strFont As String) As System.Drawing.FontStyle
            Dim fs As System.Drawing.FontStyle = System.Drawing.FontStyle.Regular

            If VWS.Legenden.cDrawingTools.HasFont(strFont) Then
                Using ffThisFamily As New System.Drawing.FontFamily(strFont)

                    If ffThisFamily.IsStyleAvailable(System.Drawing.FontStyle.Regular) Then
                        fs = System.Drawing.FontStyle.Regular
                    ElseIf ffThisFamily.IsStyleAvailable(System.Drawing.FontStyle.Italic) Then
                        fs = System.Drawing.FontStyle.Italic
                    ElseIf ffThisFamily.IsStyleAvailable(System.Drawing.FontStyle.Bold) Then
                        fs = System.Drawing.FontStyle.Bold
                    ElseIf ffThisFamily.IsStyleAvailable(System.Drawing.FontStyle.Strikeout) Then
                        fs = System.Drawing.FontStyle.Strikeout
                    ElseIf ffThisFamily.IsStyleAvailable(System.Drawing.FontStyle.Underline) Then
                        fs = System.Drawing.FontStyle.Underline
                    Else
                        Throw New InvalidOperationException(String.Format("No font style available for font ""{0}"".", strFont))
                    End If

                End Using

            Else
                Throw New InvalidOperationException(String.Format("No font with name ""{0}"" installed.", strFont))
            End If

            Return fs
        End Function


        Public Shared Function GetHatchStyles() As System.Drawing.Drawing2D.HatchStyle()
            Dim styles As System.Drawing.Drawing2D.HatchStyle() = DirectCast([Enum].GetValues(GetType(System.Drawing.Drawing2D.HatchStyle)), System.Drawing.Drawing2D.HatchStyle())
            Return styles
        End Function ' GetHatchStyles


        Public Shared Sub FillTriangle(g As System.Drawing.Graphics, CurrentBrush As System.Drawing.Brush, iOriginX As Integer, iOriginY As Integer, i As Integer, iBulletSize As Integer)
            ' Create points that define polygon.
            Dim point1 As New System.Drawing.PointF(iOriginX, iOriginY + i * (iBulletSize * 2) + iBulletSize)
            Dim point2 As New System.Drawing.PointF(iOriginX + iBulletSize, iOriginY + i * (iBulletSize * 2) + iBulletSize)
            Dim point3 As New System.Drawing.PointF(iOriginX + iBulletSize / 2.0F, iOriginY + i * (iBulletSize * 2))
            Dim curvePoints As System.Drawing.PointF() = {point1, point2, point3}

            ' Define fill mode.
            Dim newFillMode As System.Drawing.Drawing2D.FillMode = System.Drawing.Drawing2D.FillMode.Winding

            ' Fill polygon to screen.
            g.FillPolygon(CurrentBrush, curvePoints, newFillMode)
        End Sub ' FillTriangle


        Public Shared Sub DrawCircle(g As System.Drawing.Graphics, pen As System.Drawing.Pen, centerX As Single, centerY As Single, radius As Single)
            g.DrawEllipse(pen, centerX - radius, centerY - radius, radius + radius, radius + radius)
        End Sub ' DrawCircle


        Public Shared Sub FillCircle(g As System.Drawing.Graphics, brush As System.Drawing.Brush, centerX As Single, centerY As Single, radius As Single)
            g.FillEllipse(brush, centerX - radius, centerY - radius, radius + radius, radius + radius)
        End Sub ' FillCircle


        Public Shared Function GetHatchImage(iHatchNumber As Integer, strHtmlColor As String) As System.Drawing.Image
            Dim targetcol As System.Drawing.Color = System.Drawing.ColorTranslator.FromHtml(strHtmlColor)
            Return GetHatchImage(iHatchNumber, targetcol)
        End Function ' GetHatchImage


        Public Shared Function GetHatchImage(iHatchNumber As Integer, targetcol As System.Drawing.Color) As System.Drawing.Image
            Dim bmp As System.Drawing.Bitmap = Nothing

#If TARGET = "winexe" Or TARGET = "exe" Then
            Dim strBasePath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
            strBasePath = System.IO.Path.Combine(strBasePath, "hatches")
#Else
            Dim strBasePath As String = System.Web.HttpContext.Current.Request.MapPath("~/images/VWS/hatches/")
#End If


            Dim strInput As String = System.IO.Path.Combine(strBasePath, "hatch" + iHatchNumber.ToString() + ".png")

            Dim sourcecol As System.Drawing.Color = System.Drawing.Color.Black

            Select Case iHatchNumber
                Case 9
                    sourcecol = System.Drawing.Color.FromArgb(255, 255, 65, 65)
                    ' hatch9.png
                    Exit Select
                Case 35
                    sourcecol = System.Drawing.Color.FromArgb(255, 32, 119, 32)
                    ' hatch 35.png
                    sourcecol = System.Drawing.Color.HotPink
                    Exit Select
                Case 69
                    sourcecol = System.Drawing.Color.FromArgb(255, 0, 0, 222)
                    ' hatch 69.png
                    Exit Select
                Case Else
                    sourcecol = System.Drawing.Color.HotPink
                    Throw New NotImplementedException("No sourcecolor for hatch pattern no. " + iHatchNumber.ToString() + " implemented.")
            End Select ' iHatchNumber 

            'targetcol = System.Drawing.Color.HotPink

            Dim coltrans As System.Drawing.Color = System.Drawing.Color.FromArgb(255, 255, 255, 255)
            Dim coltrans2 As System.Drawing.Color = System.Drawing.Color.FromArgb(0, 0, 0, 0)


            Dim sourcehsl As New Tools.ColorSpace.HSLColor(sourcecol)

            Using img As System.Drawing.Image = System.Drawing.Image.FromFile(strInput)
                bmp = New System.Drawing.Bitmap(img)

                For x As Integer = 0 To bmp.Width - 1
                    For y As Integer = 0 To bmp.Height - 1
                        Dim col As System.Drawing.Color = bmp.GetPixel(x, y)

                        If col = coltrans OrElse col = coltrans2 Then
                            Continue For
                        End If

                        'col = System.Drawing.Color.FromArgb(255, 255 - col.R, 255 - col.G, 255 - col.B);
                        Dim hsl As New Tools.ColorSpace.HSLColor(col)


                        Dim deltalight As Double = sourcehsl.Lightness - hsl.Lightness
                        Dim deltasat As Double = sourcehsl.Saturation - hsl.Saturation


                        Dim targethsl As New Tools.ColorSpace.HSLColor(targetcol)
                        targethsl.Lightness = targethsl.Lightness - deltalight
                        If targethsl.Hue <> 0 OrElse targethsl.Saturation <> 0 Then
                            targethsl.Saturation = targethsl.Saturation - deltasat
                        End If


                        targethsl.Lightness = Math.Min(targethsl.Lightness, 1.0)
                        targethsl.Lightness = Math.Max(targethsl.Lightness, 0.0)

                        targethsl.Saturation = Math.Min(targethsl.Saturation, 1.0)
                        targethsl.Saturation = Math.Max(targethsl.Saturation, 0.0)


                        ' targethsl.Lightness = hsl.Lightness;
                        ' targethsl.Saturation = hsl.Saturation;
                        'if (targethsl.Hue != 0 || targethsl.Saturation != 0) targethsl.Saturation = hsl.Saturation;
                        'col = targetcol;
                        'bmp.MakeTransparent(System.Drawing.Color.White);
                        col = targethsl.Color
                        bmp.SetPixel(x, y, col)
                    Next y

                Next x
            End Using ' (System.Drawing.Image img = System.Drawing.Image.FromFile(strInput)) 

            Return bmp
        End Function ' GetHatchImage


        Public Shared Function GetBrush(iLegendPattern As Integer, colFC As System.Drawing.Color, colHB As System.Drawing.Color, colHL As System.Drawing.Color) As System.Drawing.Brush
            If iLegendPattern = 12 Then
                ' Normal, SolidBrush
                Return New System.Drawing.SolidBrush(colFC)
            ElseIf iLegendPattern = 43 Then
                ' MB: Leerstand, HatchBrush - HatchStyle.DarkUpwardDiagonal
                Return New System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.DarkUpwardDiagonal, System.Drawing.Color.Black, System.Drawing.Color.White)
            ElseIf iLegendPattern = 0 Then
                ' 0 -- >40 Zeilen / >40 lignes, keine
                ' Console.WriteLine("40 Zeilen / >40 lignes");
                Return Nothing
            End If

            'else // [9: kein Mietobjekt,35: MB Mieter ], TextureBrush
            Dim tb As System.Drawing.TextureBrush = Nothing

            Using image As System.Drawing.Image = GetHatchImage(iLegendPattern, colFC)
                tb = New System.Drawing.TextureBrush(image, System.Drawing.Drawing2D.WrapMode.Tile)
            End Using ' image

            ' tb.TranslateTransform(x, y);
            Return tb
        End Function ' GetBrush


        Public Shared Function HasFont(fontName As String) As Boolean
            Dim fontsCollection As System.Drawing.Text.FontCollection = New System.Drawing.Text.InstalledFontCollection()
            For Each ffThisFontFamiliy As System.Drawing.FontFamily In fontsCollection.Families
                ' if (ffThisFontFamiliy.Name == fontName)
                If StringComparer.OrdinalIgnoreCase.Equals(ffThisFontFamiliy.Name, fontName) Then
                    Return True
                End If
            Next ffThisFontFamiliy

            Return False
        End Function ' HasFont


    End Class ' cDrawingTools


End Namespace ' Portal.VWS.Legenden
