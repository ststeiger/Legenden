
Namespace Portal.ASP


    Public Class NET


        Public Shared Function GetParameter(ByVal strRequestedKey As String) As String
            Dim strValue As String = Nothing
            If StringComparer.OrdinalIgnoreCase.Equals(System.Web.HttpContext.Current.Request.HttpMethod, "GET") Then
                Return System.Web.HttpContext.Current.Request.QueryString(strRequestedKey)
            ElseIf StringComparer.OrdinalIgnoreCase.Equals(System.Web.HttpContext.Current.Request.HttpMethod, "POST") Then
                Return System.Web.HttpContext.Current.Request.Form(strRequestedKey)
            Else
                Throw New System.Web.HttpException(500, "Invalid request method")
            End If

            Return Nothing
        End Function


        ' COR.ASP.NET.RecursiveFindControl(Me, ".NET ID")
        Public Shared Function RecursiveFindControl(ByVal ctrlParentControl As System.Web.UI.Control, ByVal strControlName As String) As System.Web.UI.Control
            If StringComparer.OrdinalIgnoreCase.Equals(ctrlParentControl.ID, strControlName) Then
                Return ctrlParentControl
            End If

            For Each ctrlThisControl As System.Web.UI.Control In ctrlParentControl.Controls
                If StringComparer.OrdinalIgnoreCase.Equals(ctrlThisControl.ID, strControlName) Then
                    Return ctrlThisControl
                Else
                    If ctrlThisControl.HasControls() Then
                        Dim ctrlFoundControl As System.Web.UI.Control = RecursiveFindControl(ctrlThisControl, strControlName)
                        If ctrlFoundControl IsNot Nothing Then
                            Return ctrlFoundControl
                        End If
                    End If
                End If
            Next

            Return Nothing
        End Function


        ' COR.ASP.NET.StripInvalidPathChars("")
        Public Shared Function StripInvalidPathChars(str As String) As String
            Dim strReturnValue As String = Nothing

            If str Is Nothing Then
                Return strReturnValue
            End If

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim achrInvalidPathChars As Char() = System.IO.Path.GetInvalidPathChars()


            For Each cThisChar As Char In str
                Dim bIsValid As Boolean = True

                For Each cInvalid As Char In achrInvalidPathChars
                    If cThisChar = cInvalid Then
                        bIsValid = False
                        Exit For
                    End If
                Next cInvalid

                If bIsValid Then
                    sb.Append(cThisChar)
                End If
            Next cThisChar

            strReturnValue = sb.ToString()
            sb = Nothing
            Return strReturnValue
        End Function ' StripInvalidPathChars


        Public Shared Function GetContentDisposition(ByVal strFileName As String) As String
            Return GetContentDisposition(strFileName, "attachment")
        End Function


        ' http://www.iana.org/assignments/cont-disp/cont-disp.xhtml
        Public Shared Function GetContentDisposition(ByVal strFileName As String, ByVal strDisposition As String) As String
            ' http://stackoverflow.com/questions/93551/how-to-encode-the-filename-parameter-of-content-disposition-header-in-http
            Dim contentDisposition As String
            strFileName = StripInvalidPathChars(strFileName)

            If String.IsNullOrEmpty(strDisposition) Then
                strDisposition = "inline"
            End If

            If System.Web.HttpContext.Current IsNot Nothing AndAlso System.Web.HttpContext.Current.Request.Browser IsNot Nothing Then
                If (System.Web.HttpContext.Current.Request.Browser.Browser = "IE" And (System.Web.HttpContext.Current.Request.Browser.Version = "7.0" Or System.Web.HttpContext.Current.Request.Browser.Version = "8.0")) Then
                    contentDisposition = strDisposition + "; filename=" + Uri.EscapeDataString(strFileName).Replace("'", Uri.HexEscape("'"c))
                ElseIf (System.Web.HttpContext.Current.Request.Browser.Browser = "Safari") Then
                    contentDisposition = strDisposition + "; filename=" + strFileName
                Else
                    contentDisposition = strDisposition + "; filename*=UTF-8''" + Uri.EscapeDataString(strFileName)
                End If
            Else
                contentDisposition = strDisposition + "; filename*=UTF-8''" + Uri.EscapeDataString(strFileName)
            End If

            Return contentDisposition
        End Function ' GetContentDisposition


        Public Shared Function ContentUrl(ByVal strPath As String) As String
            Return ContentUrl(strPath, False)
        End Function


        Public Shared Function ContentUrl(ByVal strPath As String, bNoCheck As Boolean) As String
            Dim lngFileTime As Long = COR.AJAX.Time.ToUnixTicksMapped(strPath, bNoCheck)

            Dim strReturnValue As String = Nothing
            If lngFileTime = 0 Then
                Return System.Web.VirtualPathUtility.ToAbsolute(strPath)
            Else
                strReturnValue = System.Web.VirtualPathUtility.ToAbsolute(strPath)
            End If

            If Not bNoCheck Then
                strReturnValue += "?no_cache_LastWriteTimeUtc=" + lngFileTime.ToString()
            End If

            Return strReturnValue
            'Response.Write("<h1>" + VirtualPathUtility.ToAbsolute("~/lol/yuk/Home.aspx") + "</h1>")

            'strPath = HttpContext.Current.Server.MapPath(strPath)
            'Dim strAppPath As String = HttpContext.Current.Server.MapPath("~")
            ''Dim url As String = String.Format("~{0}", strPath.Replace(strAppPath, "").Replace("\", "/"))
            ''Dim AbsolutePath As String = Request.ServerVariables("APPL_PHYSICAL_PATH")
            ''Dim AbsolutePath2 As String = HttpContext.Current.Request.ApplicationPath
            ''Dim str As String = HttpRuntime.AppDomainAppVirtualPath

            'Dim url As String = String.Format("{0}", HttpContext.Current.Request.ApplicationPath + strPath.Replace(strAppPath, "").Replace("\", "/"))

            '' https://www4.cor-asp.ch/REM_Demo_DMSc:/inetpub/wwwroot/ajax/NavigationContent.ashx?filter=nofilter1342703258627 404

            'Return url
        End Function ' ContentUrl


    End Class ' NET


End Namespace ' COR.ASP
