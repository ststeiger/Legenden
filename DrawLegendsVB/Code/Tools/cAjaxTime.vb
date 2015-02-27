
Namespace COR.AJAX


    Public Class Time


        Public Shared Function FromUnixTicks(lngMilliseconds As Int64) As DateTime
            Dim d1 As New DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)
            Dim d2 As DateTime = d1.AddMilliseconds(lngMilliseconds)

            Dim dt As DateTime = d2.ToLocalTime()
            Return dt
        End Function ' FromUnixTicks


        Public Shared Function ToUnixTicks() As Int64
            Return ToUnixTicks(DateTime.Now)
        End Function ' ToUnixTicks


        Public Shared Function ToUnixTicks(strPathToFile As String) As Int64
            Dim dLastWriteTime As DateTime = System.IO.File.GetLastWriteTimeUtc(strPathToFile)
            Return ToUnixTicks(dLastWriteTime)
        End Function ' ToUnixTicks


        Public Shared Function ToUnixTicksMapped(ByVal strPathToFile As String) As Int64
            Return ToUnixTicksMapped(strPathToFile, False)
        End Function


        Public Shared Function ToUnixTicksMapped(ByVal strPathToFile As String, ByVal bNoChek As Boolean) As Int64
            strPathToFile = System.Web.HttpContext.Current.Server.MapPath(strPathToFile)

            If bNoChek Then
                Return ToUnixTicks(strPathToFile)
            End If

            If System.IO.File.Exists(strPathToFile) Then
                Return ToUnixTicks(strPathToFile)
            Else
                If Not (strPathToFile.EndsWith("\DMS", StringComparison.OrdinalIgnoreCase) Or strPathToFile.EndsWith("\DMS\bilder\{0}", StringComparison.OrdinalIgnoreCase)) Then
                    Throw New System.IO.FileNotFoundException("Die Datei """ + strPathToFile + """ existiert nicht")
                End If

            End If

            Return 0
        End Function ' ToUnixTicksMapped


        Public Shared Function ToUnixTicks(dt As DateTime) As Int64
            Dim d1 As New DateTime(1970, 1, 1)
            Dim d2 As DateTime = dt.ToUniversalTime()
            Dim ts As New TimeSpan(d2.Ticks - d1.Ticks)
            Return System.Convert.ToInt64(ts.TotalMilliseconds)
        End Function ' ToUnixTicks


    End Class ' Time


End Namespace ' COR.AJAX
