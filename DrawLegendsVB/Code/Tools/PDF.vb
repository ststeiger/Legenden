
Imports System.Collections.Generic
Imports System.Web


Namespace Tools.FileFormat


    Public Class PDF


        Protected Shared Function CreateDocument() As PdfSharp.Pdf.PdfDocument
            Dim doc As New PdfSharp.Pdf.PdfDocument()
            doc.Info.Title = "AmChart image created with PDFsharp"
            doc.Info.Author = "Stefan Steiger"
            doc.Info.Subject = "AmCharts"
            ' "Created with code snippets that show the use of graphical functions";
            doc.Info.Keywords = "PDFsharp, XGraphics, AmCharts"

            Return doc
        End Function ' CreateDocument 


        Public Shared Sub ImageDataToPdfFile(imgdata As Byte(), strPdfFileName As String)
            If System.IO.File.Exists(strPdfFileName) Then
                System.IO.File.Delete(strPdfFileName)
            End If

            Using pdfdoc As PdfSharp.Pdf.PdfDocument = CreateDocument()
                Dim pg As PdfSharp.Pdf.PdfPage = pdfdoc.AddPage()
                AddImageToPage(pg, imgdata)

                pdfdoc.Save(strPdfFileName)
            End Using ' pdfdoc
        End Sub ' ImageDataToPdfFile


        Public Shared Function ImageToPdfData(img As System.Drawing.Image) As Byte()
            Dim baReturnValue As Byte() = Nothing

            Using ms As New System.IO.MemoryStream()

                Using pdfdoc As PdfSharp.Pdf.PdfDocument = CreateDocument()
                    Dim pg As PdfSharp.Pdf.PdfPage = pdfdoc.AddPage()
                    AddImageToPage(pg, img)

                    pdfdoc.Save(ms)
                End Using ' pdfdoc

                baReturnValue = ms.ToArray()
            End Using ' ms

            Return baReturnValue
        End Function ' ImageToPdfData


        Public Shared Function ImageDataToPdfData(imgdata As Byte()) As Byte()
            Dim baReturnValue As Byte() = Nothing

            Using ms As New System.IO.MemoryStream()

                Using pdfdoc As PdfSharp.Pdf.PdfDocument = CreateDocument()
                    Dim pg As PdfSharp.Pdf.PdfPage = pdfdoc.AddPage()
                    AddImageToPage(pg, imgdata)

                    pdfdoc.Save(ms)
                End Using ' pdfdoc

                baReturnValue = ms.ToArray()
            End Using ' ms

            Return baReturnValue
        End Function ' ImageDataToPdfData 


        Public Shared Function Base64ImageToPdfData(data As String) As Byte()
            If String.IsNullOrEmpty(data) Then
                Throw New ArgumentNullException("data")
            End If

            Dim ma As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(data, "data:image/(?<type>.+?),(?<data>.+)")
            If ma.Success Then
                Dim base64Data As String = ma.Groups("data").Value
                Dim binData As Byte() = Convert.FromBase64String(base64Data)

                Return ImageDataToPdfData(binData)
            End If

            Throw New ArgumentException("data improper")
        End Function ' Base64ImageToPdfData 


        Public Shared Function ImageFileToPdfData(strImageFileName As String) As Byte()
            Dim baReturnValue As Byte() = Nothing

            If Not System.IO.File.Exists(strImageFileName) Then
                Throw New System.IO.FileNotFoundException(strImageFileName)
            End If

            Using ms As New System.IO.MemoryStream()
                Using pdfdoc As PdfSharp.Pdf.PdfDocument = CreateDocument()
                    Dim pg As PdfSharp.Pdf.PdfPage = pdfdoc.AddPage()
                    AddImageToPage(pg, strImageFileName)

                    pdfdoc.Save(ms)
                End Using ' pdfdoc

                baReturnValue = ms.ToArray()
            End Using ' ms

            Return baReturnValue
        End Function ' ImageFileToPdfData 


        Public Shared Sub ImageDataToImageFile(binData As Byte(), strImageFileName As String)

            Using stream As System.IO.Stream = New System.IO.MemoryStream(binData)
                Using img As System.Drawing.Image = System.Drawing.Image.FromStream(stream)
                    If System.IO.File.Exists(strImageFileName) Then
                        System.IO.File.Delete(strImageFileName)
                    End If

                    img.Save(strImageFileName, System.Drawing.Imaging.ImageFormat.Png)
                End Using ' img
            End Using ' stream

        End Sub ' ImageDataToImageFile 


        ' http://www.pdfsharp.net/wiki/Images-sample.ashx
        Public Shared Sub ImageFileToPdfFile(strImageFileName As String, strPdfFileName As String)
            If Not System.IO.File.Exists(strImageFileName) Then
                Throw New System.IO.FileNotFoundException(strImageFileName)
            End If

            If System.IO.File.Exists(strPdfFileName) Then
                System.IO.File.Delete(strPdfFileName)
            End If

            Using pdfdoc As PdfSharp.Pdf.PdfDocument = CreateDocument()
                Dim pg As PdfSharp.Pdf.PdfPage = pdfdoc.AddPage()
                AddImageToPage(pg, strImageFileName)

                pdfdoc.Save(strPdfFileName)
            End Using 'pdfdoc 
        End Sub ' ImageFileToPdfFile


        Public Shared Sub ImageFileToPdfFileStream(strImageFileName As String, strPdfFileName As String)
            If Not System.IO.File.Exists(strImageFileName) Then
                Throw New System.IO.FileNotFoundException(strImageFileName)
            End If

            If System.IO.File.Exists(strPdfFileName) Then
                System.IO.File.Delete(strPdfFileName)
            End If

            Using pdfdoc As PdfSharp.Pdf.PdfDocument = CreateDocument()
                Dim pg As PdfSharp.Pdf.PdfPage = pdfdoc.AddPage()
                AddImageToPage(pg, strImageFileName)

                Using fs As System.IO.FileStream = System.IO.File.Create(strPdfFileName)
                    pdfdoc.Save(fs)
                End Using ' fs
            End Using ' pdfdoc
        End Sub ' ImageFileToPdfFileStream


        Protected Shared Sub AddImageToPage(page As PdfSharp.Pdf.PdfPage, binData As Byte())
            Using stream As System.IO.Stream = New System.IO.MemoryStream(binData)
                Using img As System.Drawing.Image = System.Drawing.Image.FromStream(stream)
                    'if (System.IO.File.Exists(strImageFileName))
                    'System.IO.File.Delete(strImageFileName);

                    'img.Save(strImageFileName, System.Drawing.Imaging.ImageFormat.Png);

                    Using image As PdfSharp.Drawing.XImage = PdfSharp.Drawing.XImage.FromGdiPlusImage(img)
                        AddImageToPage(page, image)
                    End Using ' image
                    ' End Using img
                    '
                    '                var pictureBox = new PictureBox
                    '                {
                    '                    Image = new System.Drawing.Bitmap(stream),
                    '                };
                    '                

                    'var form = new Form { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
                    'form.Controls.Add(pictureBox);

                End Using 'img

            End Using ' stream

        End Sub ' AddImageToPage


        Protected Shared Sub AddImageToPage(page As PdfSharp.Pdf.PdfPage, strImageFileName As String)
            Using image As PdfSharp.Drawing.XImage = PdfSharp.Drawing.XImage.FromFile(strImageFileName)
                AddImageToPage(page, image)
            End Using ' image
        End Sub ' AddImageToPage


        Protected Shared Sub AddImageToPage(page As PdfSharp.Pdf.PdfPage, img As System.Drawing.Image)
            Using image As PdfSharp.Drawing.XImage = PdfSharp.Drawing.XImage.FromGdiPlusImage(img)
                AddImageToPage(page, image)
            End Using ' image
        End Sub ' AddImageToPage


        Protected Shared Sub old_AddImageToPage(page As PdfSharp.Pdf.PdfPage, image As PdfSharp.Drawing.XImage)
            Using gfx As PdfSharp.Drawing.XGraphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page)
                '1 centimeter = 28.3464567 PostScript points
                Dim iMarginTop As Integer = CInt(Math.Truncate(1.5 * 28.3464567))

                ' Left position in point
                Dim iMarginLeft As Integer = CInt(page.Width.Point - image.PixelWidth * 72 / image.HorizontalResolution) \ 2

                gfx.DrawImage(image, iMarginLeft, iMarginTop)
            End Using ' gfx
        End Sub ' AddImageToPage


        Protected Shared Sub AddImageToPage(page As PdfSharp.Pdf.PdfPage, image As PdfSharp.Drawing.XImage)

            Using gfx As PdfSharp.Drawing.XGraphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page)
                If page.Width.Point < image.PointWidth Then page.Orientation = PdfSharp.PageOrientation.Landscape
                If page.Width.Point < image.PointWidth Then page.Width = image.PointWidth

                Dim iMarginTop As Integer = CInt((page.Height.Point - image.PointHeight) / 2)
                Dim iMarginLeft As Integer = CInt((page.Width.Point - image.PointWidth) / 2)

                gfx.DrawImage(image, iMarginLeft, iMarginTop)
            End Using ' gfx

        End Sub ' AddImageToPage


    End Class ' PDF


End Namespace ' Tools.FileFormat
