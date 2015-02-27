
Imports System.Collections.Generic
Imports System.Text


Namespace Portal.Tools.Web


    Public Class DownloadHelper


        ' http://stackoverflow.com/questions/5629251/c-sharp-outputting-image-to-response-output-stream-giving-gdi-error
        Public Shared Sub WriteImageToOutputStream(image As System.Drawing.Image, imgFormat As System.Drawing.Imaging.ImageFormat)

            Using ms As New System.IO.MemoryStream()
                image.Save(ms, imgFormat)
                ms.WriteTo(System.Web.HttpContext.Current.Response.OutputStream)

            End Using ' ms

        End Sub ' WriteImageToOutputStream


        Public Shared Function ToCanonicalUrl(ByVal relativeUrl As String) As String
            Dim strScheme As String = System.Web.HttpContext.Current.Request.Url.Scheme
            Dim strAuth As String = System.Web.HttpContext.Current.Request.Url.Authority
            Dim strCanonicalBase As String = strScheme + System.Uri.SchemeDelimiter + strAuth

            Dim strAbsoluteURL As String = Portal.ASP.NET.ContentUrl(relativeUrl)
            Return strCanonicalBase + strAbsoluteURL

            ',Page.ResolveUrl(relativeUrl)
            'Return String.Format("http{0}://{1}{2}", If((System.Web.HttpContext.Current.Request.IsSecureConnection), "s", ""), System.Web.HttpContext.Current.Request.Url.Host, DirectCast(System.Web.HttpContext.Current.Handler, System.Web.UI.Page).ResolveUrl(relativeUrl))
        End Function ' ToCanonicalUrl


        Public Shared Function DownloadFile(strURL As String) As Byte()
            Dim imageBytes As Byte() = Nothing
            Dim webClient As New System.Net.WebClient()
            imageBytes = webClient.DownloadData(strURL)

            Return imageBytes
        End Function ' DownloadFile


        ' ImageToByteArray(img, System.Drawing.Imaging.ImageFormat.Gif);
        Public Shared Function ImageToByteArray(img As System.Drawing.Image, imgFormat As System.Drawing.Imaging.ImageFormat) As Byte()
            Dim baImageData As Byte() = Nothing

            Using ms As New System.IO.MemoryStream()
                img.Save(ms, imgFormat)
                baImageData = ms.ToArray()
            End Using ' System.IO.MemoryStream ms

            Return baImageData
        End Function ' ImageToByteArray


        Public Shared Function ByteArrayToImage(byteArrayIn As Byte()) As System.Drawing.Image
            Dim returnImage As System.Drawing.Image = Nothing

            Using ms As New System.IO.MemoryStream(byteArrayIn)
                returnImage = System.Drawing.Image.FromStream(ms)
            End Using ' System.IO.MemoryStream ms

            Return returnImage
        End Function ' ByteArrayToImage


        Public Shared Function GetLastModifiedDate(strURL As String) As DateTime
            Dim LastModified As DateTime

            Dim req As System.Net.WebRequest = System.Net.HttpWebRequest.Create(strURL)
            req.Method = "HEAD"
            Using resp As System.Net.WebResponse = req.GetResponse()

                'Check if date is good and then go to full download method.
                If DateTime.TryParse(resp.Headers.[Get]("Last-Modified"), LastModified) Then
                    ' End if (DateTime.TryParse(resp.Headers.Get("Last-Modified"), out LastModified))
                End If

            End Using ' resp

            Return LastModified
        End Function ' GetLastModifiedDate


        ' Header Beispiel
        'Content-Location: http://www.cor-management.ch/corwebsite/pictures/Titelbild_01.jpg
        'Accept-Ranges: bytes
        'Content-Length: 18509
        'Content-Type: image/jpeg
        'Date: Wed, 29 Jan 2014 14:18:21 GMT
        'ETag: "a885ab326ebdcb1:c56"
        'Last-Modified: Wed, 26 Jan 2011 15:32:10 GMT
        'Server: Microsoft-IIS/6.0
        'X-Powered-By: ASP.NET
        Public Shared Function GetHeaders(strURL As String) As String
            Dim strHeaderText As String = ""

            Dim req As System.Net.WebRequest = System.Net.HttpWebRequest.Create(strURL)
            req.Method = "HEAD"

            Using resp As System.Net.WebResponse = req.GetResponse()
                For Each strHeader As String In resp.Headers.AllKeys
                    strHeaderText += strHeader & ": " & resp.Headers(strHeader) & Environment.NewLine
                Next strHeader
            End Using ' resp

            Return strHeaderText
        End Function ' GetHeaders


        Public Shared Function StreamToByteArray(strmInput As System.IO.Stream) As Byte()
            Dim buffer As Byte() = New Byte(16 * 1024 - 1) {}
            Using ms As New System.IO.MemoryStream()
                Dim read As Integer
                While (InlineAssignHelper(read, strmInput.Read(buffer, 0, buffer.Length))) > 0
                    ms.Write(buffer, 0, read)
                End While

                Return ms.ToArray()
            End Using ' ms
        End Function ' StreamToByteArray


        Protected Shared Sub WriteBufferToStream(strmData As System.IO.Stream, baBuffer As Byte(), iStart As Integer, iSize As Integer)
            SyncLock strmData
                strmData.Seek(iStart, System.IO.SeekOrigin.Begin)
                strmData.Write(baBuffer, 0, iSize)
            End SyncLock ' strmData 
        End Sub ' WriteBufferToStream


        ' http://stackoverflow.com/questions/1366848/httpwebrequest-getresponse-throws-webexception-on-http-304
        Public Shared Function GetHttpResponse(request As System.Net.HttpWebRequest) As System.Net.HttpWebResponse
            Try
                Return DirectCast(request.GetResponse(), System.Net.HttpWebResponse)
            Catch ex As System.Net.WebException

                If ex.Status = Net.WebExceptionStatus.ConnectFailure Then
                    'GoTo label1
                    Throw New Exception("Cannot connect to Aperture - Server Down.")
                End If

                If ex.Response Is Nothing OrElse ex.Status <> System.Net.WebExceptionStatus.ProtocolError Then
                    Throw
                End If

                Return DirectCast(ex.Response, System.Net.HttpWebResponse)
            End Try

            'label1:
            '            Dim strRedirectURL As String = Portal.ASP.NET.ContentUrl("~/w8/ApertureDown.html")
            '            Console.WriteLine(strRedirectURL)

            '            System.Web.HttpContext.Current.Response.Redirect(strRedirectURL)
            '            Return Nothing
        End Function ' GetHttpResponse


        Public Shared Function GetFileAsByteArray(strURL As String) As Byte()
            Dim baFile As Byte() = Nothing

            Dim hwrRequest As System.Net.HttpWebRequest = DirectCast(System.Net.WebRequest.Create(strURL), System.Net.HttpWebRequest)
            'System.Net.HttpWebResponse wrResponse = (System.Net.HttpWebResponse)hwrRequest.GetResponse();
            Dim wrResponse As System.Net.HttpWebResponse = GetHttpResponse(hwrRequest)

            If hwrRequest.HaveResponse Then

                If wrResponse.StatusCode = System.Net.HttpStatusCode.OK Then

                    Using msTempStream As New System.IO.MemoryStream()

                        Using strmResponse As System.IO.Stream = wrResponse.GetResponseStream()
                            Dim iTotalBytes As Integer = 0
                            Const iBufferLength As Integer = 256
                            Dim baBuffer As Byte() = New Byte(iBufferLength - 1) {}
                            Dim iBytesRead As Integer = strmResponse.Read(baBuffer, 0, iBufferLength)
                            While iBytesRead > 0
                                WriteBufferToStream(msTempStream, baBuffer, iTotalBytes, iBytesRead)
                                iTotalBytes += iBytesRead
                                iBytesRead = strmResponse.Read(baBuffer, 0, iBufferLength)
                            End While

                            strmResponse.Close()
                        End Using ' strmResponse 

                        wrResponse.Close()
                        baFile = msTempStream.ToArray()

                        ' System.IO.File.WriteAllBytes("d:\test.jpg", baFile)
                        msTempStream.Close()
                    End Using ' msTempStream
                Else
                    baFile = Portal.VWS.Legenden.cDrawingTools.GetErrorAsImage(String.Format("Aperture Error for URL ""{0}"".", strURL) + Environment.NewLine + "HTTP-Status Code: " + wrResponse.StatusCode.ToString())
                End If ' if (wrResponse.StatusCode == System.Net.HttpStatusCode.OK)

            End If ' (hwrRequest.HaveResponse)

            Return baFile
        End Function ' GetByteArray


        Public Shared Sub SaveFileToPath(strURL As String, strPath As String)
            Dim hwrRequest As System.Net.HttpWebRequest = DirectCast(System.Net.WebRequest.Create(strURL), System.Net.HttpWebRequest)
            'System.Net.HttpWebResponse wrResponse = (System.Net.HttpWebResponse)hwrRequest.GetResponse();
            Dim wrResponse As System.Net.HttpWebResponse = GetHttpResponse(hwrRequest)

            If hwrRequest.HaveResponse Then
                If wrResponse.StatusCode = System.Net.HttpStatusCode.OK Then

                    Using fsFileStream As New System.IO.FileStream(strPath, System.IO.FileMode.Create, System.IO.FileAccess.Write)

                        Using strmResponse As System.IO.Stream = wrResponse.GetResponseStream()
                            Dim iTotalBytes As Integer = 0
                            Const iBufferLength As Integer = 256
                            Dim baBuffer As Byte() = New Byte(iBufferLength - 1) {}
                            Dim iBytesRead As Integer = strmResponse.Read(baBuffer, 0, iBufferLength)
                            While iBytesRead > 0
                                WriteBufferToStream(fsFileStream, baBuffer, iTotalBytes, iBytesRead)
                                iTotalBytes += iBytesRead
                                iBytesRead = strmResponse.Read(baBuffer, 0, iBufferLength)
                            End While

                            strmResponse.Close()
                        End Using ' strmResponse 

                        wrResponse.Close()
                        fsFileStream.Close()
                    End Using ' System.IO.FileStream fsFileStream 

                End If ' (wrResponse.StatusCode == System.Net.HttpStatusCode.OK)
            End If ' (hwrRequest.HaveResponse)
        End Sub  ' GetFile 


        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function ' InlineAssignHelper 


    End Class ' DownloadHelper


End Namespace ' Portal.Tools.Web
