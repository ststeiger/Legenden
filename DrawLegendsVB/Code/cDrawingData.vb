
Imports System.Collections.Generic
Imports System.Text


Namespace Portal.VWS.Legenden


    Public Class cDrawingData


        ' Not used
        Public Shared Function GetColorTranslations() As System.Data.DataTable
            Dim dt As New System.Data.DataTable()

            Dim csb As New System.Data.SqlClient.SqlConnectionStringBuilder()
            csb.DataSource = Environment.MachineName
            csb.InitialCatalog = "COR_Basic_Wincasa"
            csb.IntegratedSecurity = True


            Dim strSQL As String = vbCr & vbLf & "SELECT " & vbCr & vbLf & vbTab & " T_SYS_ApertureColorToHex.COL_Aperture " & vbCr & vbLf & vbTab & ",T_SYS_ApertureColorToHex.COL_Hex " & vbCr & vbLf & vbTab & ",'#' + T_SYS_ApertureColorToHex.COL_Hex AS COL_Html " & vbCr & vbLf & "FROM T_SYS_ApertureColorToHex " & vbCr & vbLf & "WHERE T_SYS_ApertureColorToHex.COL_Status = 1 " & vbCr & vbLf & "ORDER BY COL_Aperture, COL_Hex " & vbCr & vbLf


            Dim da As New System.Data.SqlClient.SqlDataAdapter(strSQL, csb.ConnectionString)
            da.Fill(dt)
            Return dt
        End Function

        Public Shared Function GetLegendFooterHtml111() As String
            Return GetLegendFooterHtml("1010_GB01_EG00_0000", "de")
        End Function


        Public Shared Function GetLegendFooterHtml(strDWG As String, strSprache As String) As String
            Dim strHTML As String = Nothing

            Using cmd As System.Data.IDbCommand = SQL.claSQL.CreateCommandFromFile("LegendenFooter.sql")
                SQL.claSQL.AddParameter(cmd, "in_stichtag", System.DateTime.Now)
                SQL.claSQL.AddParameter(cmd, "in_sprache", strSprache)
                SQL.claSQL.AddParameter(cmd, "in_dwg", strDWG)

                Using dt As System.Data.DataTable = SQL.claSQL.GetDataTable(cmd)

                    If dt IsNot Nothing And dt.Rows.Count > 0 Then
                        Dim dr As System.Data.DataRow = dt.Rows(0)
                        Dim sb As New System.Text.StringBuilder(System.Convert.ToString(dr("LEG_Template")))

                        For Each dc As DataColumn In dr.Table.Columns

                            If dc.ColumnName.StartsWith("LEG_", StringComparison.OrdinalIgnoreCase) Then
                                sb = sb.Replace("@@" + dc.ColumnName + "@@", System.Convert.ToString(dr(dc.ColumnName)))
                            End If

                        Next dc

                        strHTML = sb.ToString()
                        sb = Nothing
                    Else
                        strHTML = ""
                        'Throw New InvalidOperationException("No data fetched")
                    End If

                End Using ' dt

            End Using ' cmd

            Return strHTML
        End Function ' GetLegendFooterHtml


        Public Shared Function GetData() As System.Data.DataTable
            Return GetData(0, "6705_GB01_ZG00_0000", "Nutzungsart", "de")
            ' return GetData(0, "7610_GB01_UG01_0000", "Parkplatzmieter", "de");
            ' return GetData(0, "1010_GB01_OG04_0000", "Mieter", "de");
        End Function ' GetData


        Public Shared Function GetData(mandant As Integer, DWG As String, strStylizer As String, strSprache As String) As System.Data.DataTable
            Using cmd As System.Data.IDbCommand = SQL.claSQL.CreateCommandFromFile("LegendenDaten.sql")
                SQL.claSQL.AddParameter(cmd, "in_mandant", mandant)
                SQL.claSQL.AddParameter(cmd, "in_sprache", strSprache)
                SQL.claSQL.AddParameter(cmd, "in_dwg", DWG)
                SQL.claSQL.AddParameter(cmd, "in_stylizer", strStylizer)

                Return SQL.claSQL.GetDataTable(cmd, strStylizer)
            End Using ' cmd



            'System.Data.DataTable dt = new System.Data.DataTable();
            'dt.Columns.Add("Text", typeof(string));
            'dt.Columns.Add("Value", typeof(double));

            'for (int i = 1; i <= 30; ++i)
            '{
            '    System.Data.DataRow dr = dt.NewRow();
            '    if (i < 10)
            '        dr["Text"] = "Text  " + i.ToString();
            '    else
            '        dr["Text"] = "Text " + i.ToString();
            '    dr["Value"] = 100000 + (i * 20) + (i - 1) * 0.03;
            '    dt.Rows.Add(dr);
            '} // Next i

            'dt.Rows[5]["Text"] = "This is an extra long text";


            'return dt;
        End Function 'GetData


    End Class ' cDrawingData


End Namespace ' Portal.VWS.Legenden
