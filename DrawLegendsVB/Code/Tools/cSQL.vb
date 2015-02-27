
Imports System.Collections.Generic
Imports System.Text

Namespace Tools.SQL


    Public Class MS_SQL

        Public Shared Function GetDataTable(strSQL As String) As System.Data.DataTable
            Dim dt As New System.Data.DataTable()

            Using da As System.Data.Common.DbDataAdapter = New System.Data.SqlClient.SqlDataAdapter(strSQL, GetConnectionString())
                da.Fill(dt)
            End Using
            ' End Using System.Data.Common.DbDataAdapter da
            Return dt
        End Function


        Public Shared Function GetConnectionString() As String
            Dim dt As New System.Data.DataTable()

            Dim csb As New System.Data.SqlClient.SqlConnectionStringBuilder()
            csb.DataSource = "CORDB2008R2"
            csb.InitialCatalog = "COR_Basic_Wincasa_Test"
            csb.IntegratedSecurity = True

            Return csb.ConnectionString
        End Function


    End Class


End Namespace
