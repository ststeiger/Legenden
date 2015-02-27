'' R.L. (19.07.2012): From cor-basic claSQL.

Imports System.Collections.Generic
Imports System.Data, System.Data.SqlClient


Namespace SQL

    Public Enum Mandant
        [Global] = 0
        Sursee = 10
        Raiffeisen = 20
        Reichle = 30
        SwissLife = 40
        Sympany = 50
        Wincasa = 60
        SNB = 70
        SwissRe = 900
    End Enum


    ''' <summary>
    ''' Handles the SQL database access for direct SQL executes.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class claSQL
#Region "Methods"
        ''' <summary>
        ''' Executes a SQL command.
        ''' </summary>
        ''' <param name="pQuerystring">SQL.</param>
        ''' <param name="pConnectionString">ConnectionString.</param>
        ''' <param name="pSQLParameter">SQL parameters.</param>
        ''' <returns>The first value in the first field found.</returns>
        ''' <remarks></remarks>
        Public Shared Function executeScalarSQL(pQuerystring As String, pConnectionString As String, Optional pSQLParameter As SqlParameter() = Nothing) As Object
            Dim tempObject As Object = Nothing

            Using tempSQLConnection As New SqlConnection(pConnectionString)
                Using tempSQLCommand As New SqlCommand(pQuerystring, tempSQLConnection)
                    If Not pSQLParameter Is Nothing Then tempSQLCommand.Parameters.AddRange(pSQLParameter)

                    tempSQLConnection.Open()
                    tempObject = tempSQLCommand.ExecuteScalar()
                    tempSQLConnection.Close()
                End Using
            End Using

            Return tempObject
        End Function

#Region "Stefans copy-paste methods"
        Public Shared Function CreateCommand(ByVal strSQL As String) As System.Data.IDbCommand
            Return New System.Data.SqlClient.SqlCommand(strSQL)
        End Function

        Public Shared Function ExecuteReader(ByVal cmd As System.Data.IDbCommand) As System.Data.IDataReader
            Dim dr As System.Data.IDataReader = Nothing

            Dim sqldbConnection As System.Data.IDbConnection = Nothing

            SyncLock cmd

                Try
                    sqldbConnection = New System.Data.SqlClient.SqlConnection(getConnectionString())

                    Dim bSuccess As Boolean = System.Threading.Monitor.TryEnter(sqldbConnection, 5000)
                    If Not bSuccess Then
                        Throw New Exception("Could not get lock on SQL DB connection in COR.SQL.ExecuteReader ==> Threading.Monitor.TryEnter")
                    End If

                    cmd.Connection = sqldbConnection

                    If Not cmd.Connection.State = System.Data.ConnectionState.Open Then
                        cmd.Connection.Open()
                    End If

                    ' When CommandBehavior.CloseConnection is used, then closing/disposing the reader 
                    ' is specified to also close the Connection.

                    ' Dispose methods may also mark the object in such a way that it can no longer be used.  
                    ' Typically, Dispose methods do the same thing as Close with a further action 
                    ' to mark the object as disposed and no longer usable.  
                    ' In a proper implementation, either Close or Dispose, by itself, 
                    ' is sufficient to prevent leakage of unmanaged objects.

                    dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
                Catch ex As Exception
                    'Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
                    'Dim strMessage As String = "Exception in ExecuteReader " + Environment.NewLine
                    'strMessage += "SQL: " + cmd.CommandText + Environment.NewLine
                    'strMessage += "Description: " + ex.Message + Environment.NewLine

                    'If ex.InnerException IsNot Nothing Then
                    '    strMessage += Environment.NewLine + Environment.NewLine
                    '    strMessage += "Inner Exception: " + ex.InnerException.Message + Environment.NewLine
                    '    strMessage += "InnerStacktrace: " + ex.InnerException.StackTrace + Environment.NewLine
                    'End If
                    'strMessage += Environment.NewLine + Environment.NewLine
                    'strMessage += "Stacktrace: " + ex.StackTrace

                    'COR.Debug.Output.MsgBox(strMessage)
                    'System.Diagnostics.Trace.WriteLine(strMessage)
                    Throw
                End Try

            End SyncLock ' cmd

            Return dr
        End Function ' ExecuteReader


        Public Shared Function GetDataTable(cmd As System.Data.IDbCommand) As System.Data.DataTable
            Return GetDataTable(cmd, Nothing)
        End Function ' End Function GetDataTable


        Public Shared Function GetDataTable(cmd As System.Data.IDbCommand, strTableName As String) As System.Data.DataTable
            Dim dt As New System.Data.DataTable

            If Not String.IsNullOrEmpty(strTableName) Then
                dt.TableName = strTableName
            End If

            Using idbc As System.Data.IDbConnection = New System.Data.SqlClient.SqlConnection(getConnectionString())

                SyncLock idbc

                    SyncLock cmd

                        Try
                            cmd.Connection = idbc

                            Using daQueryTable As System.Data.Common.DbDataAdapter = New System.Data.SqlClient.SqlDataAdapter(DirectCast(cmd, System.Data.SqlClient.SqlCommand))

                                SyncLock daQueryTable
                                    daQueryTable.SelectCommand = DirectCast(cmd, System.Data.Common.DbCommand)
                                    daQueryTable.Fill(dt)
                                End SyncLock

                            End Using ' daQueryTable

                            ' End Try
                        Catch ex As Exception
                            'Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
                            Throw
                            'COR.Debug.MsgBox("Exception executing ExecuteInTransaction: " + ex.Message);
                            'COR.Debug.Output.MsgBox(String.Format("Exception executing GetDataTable (cmd As System.Data.IDbCommand): {0}", ex.Message.ToString() + vbCrLf + "strSQL=" + cmd.CommandText))
                        Finally
                            ' End Catch
                            If idbc.State <> System.Data.ConnectionState.Closed Then
                                idbc.Close()
                            End If

                        End Try

                    End SyncLock ' cmd

                End SyncLock ' idbc

            End Using 'idbc

            'Dim iResultCOunt As Integer = dt.Rows.Count

            Return dt
        End Function ' GetDataTable


        Public Shared Function GetDataTable(ByVal strSQL As String) As DataTable
            Return GetDataTable(strSQL, "Table_1")
        End Function


        Public Shared Function GetDataTable(ByVal strSQL As String, ByVal strTableName As String) As DataTable
            Dim dt As System.Data.DataTable = Nothing

            Using cmd As System.Data.IDbCommand = New System.Data.SqlClient.SqlCommand(strSQL)
                dt = GetDataTable(cmd, strTableName)
            End Using ' sqlConnection

            Return dt
        End Function ' End Function GetDataTable

        Public Shared Sub PowerOnSelfTest()
            Dim strConnectionString As String = ""
            Try
                strConnectionString = getConnectionString()
                Dim iUserCount As Integer = CInt(executeScalarSQL("SELECT COUNT(*) FROM T_Benutzer", strConnectionString))
            Catch ex As Exception
                Dim csb As System.Data.Common.DbConnectionStringBuilder = New System.Data.Common.DbConnectionStringBuilder()
                csb.ConnectionString = strConnectionString

                If csb.ContainsKey("password") Then
                    csb.Item("password") = "**********"
                End If

                If csb.ContainsKey("pwd") Then
                    csb.Item("pwd") = "**********"
                End If

                strConnectionString = csb.ConnectionString
                Throw New Exception("Verbindungstest zur Datenbank fehlgeschlagen." + Environment.NewLine + "Falsche Verbindungsinformationen / Server aus ? " + Environment.NewLine + Environment.NewLine + "ConnectionString: " + strConnectionString + Environment.NewLine + Environment.NewLine + "Fehlerdetails:" + Environment.NewLine + ex.Message)
                'Throw New Exception("Power on self-test failed..." + Environment.NewLine + "Invalid connection string?", ex)
            End Try
        End Sub ' PowerOnSelfTest

        Public Shared Function getMandant() As SQL.Mandant
            Dim strSQL As String = "select top 1 [MDT_Kurz_DE] from [T_AP_Ref_Mandant] where([MDT_Status] = 1)"
            Dim tCommand As System.Data.IDbCommand = CreateCommand(strSQL)
            Dim strMDT As String = ExecuteScalar(Of String)(tCommand)
            Try
                Return CType(System.Enum.Parse(GetType(SQL.Mandant), strMDT), SQL.Mandant)
            Catch ex As Exception
                Return SQL.Mandant.Global
            End Try
        End Function

        Public Overloads Shared Function CreateCommandFromFile(ByVal strEmbeddedFileName As String) As System.Data.IDbCommand
            Return CreateCommandFromFile(strEmbeddedFileName, -1, getMandant())
        End Function

        'Public Overloads Shared Function CreateCommandFromFile(ByVal strEmbeddedFileName As String, ByVal intBE_ID As Int32) As System.Data.IDbCommand
        '    Return CreateCommandFromFile(strEmbeddedFileName, intBE_ID, getMandant())
        'End Function

        Public Overloads Shared Function CreateCommandFromFile(ByVal strEmbeddedFileName As String, ByVal enuMandant As Mandant) As System.Data.IDbCommand
            Return CreateCommandFromFile(strEmbeddedFileName, -1, enuMandant)
        End Function

        Public Overloads Shared Function CreateCommandFromFile(ByVal strEmbeddedFileName As String, ByVal intBE_ID As Int32, ByVal enuMandant As Mandant) As System.Data.IDbCommand
            'Start: Rico Test
            If (Not String.IsNullOrEmpty(strEmbeddedFileName) AndAlso Not strEmbeddedFileName.StartsWith(".")) Then
                strEmbeddedFileName = ("." + strEmbeddedFileName)
            End If
            'End: Rico Test

            Dim strSQL As String = GetEmbeddedSqlScript(strEmbeddedFileName, enuMandant)
            Dim tCommand As System.Data.IDbCommand = CreateCommand(strSQL)
            'AddParameter(tCommand, "@BE_ID", intBE_ID)
            'AddParameter(tCommand, "@MDT_ID", CType(enuMandant, Integer))
            Return tCommand
        End Function


        Public Shared Function GetClass(Of T)(cmd As System.Data.IDbCommand) As T
            Dim tThisValue As T = Nothing
            Dim tThisType As Type = GetType(T)

            SyncLock cmd
                Using idr As System.Data.IDataReader = ExecuteReader(cmd)

                    SyncLock idr

                        While idr.Read()

                            tThisValue = Activator.CreateInstance(Of T)()

                            For i As Integer = 0 To idr.FieldCount - 1
                                Dim strName As String = idr.GetName(i)
                                Dim objVal As Object = idr.GetValue(i)

                                Dim fi As System.Reflection.FieldInfo = tThisType.GetField(strName)
                                If fi IsNot Nothing Then
                                    fi.SetValue(tThisValue, System.Convert.ChangeType(objVal, fi.FieldType))
                                Else
                                    Dim pi As System.Reflection.PropertyInfo = tThisType.GetProperty(strName)
                                    If pi IsNot Nothing Then
                                        pi.SetValue(tThisValue, System.Convert.ChangeType(objVal, pi.PropertyType), Nothing)
                                        ' Else silently ignore value
                                    End If
                                    ' End else of if (fi != null)
                                End If
                            Next i
                            Exit While

                        End While
                        ' Whend

                        idr.Close()
                    End SyncLock 'idr 

                End Using ' idr
            End SyncLock ' cmd


            Return tThisValue
        End Function ' GetClass


        Public Shared Function GetClass(Of T)(strSQL As String) As T
            Dim tReturnValue As T = Nothing

            Using cmd As System.Data.IDbCommand = CreateCommand(strSQL)
                tReturnValue = GetClass(Of T)(cmd)
            End Using ' cmd

            Return tReturnValue
        End Function ' GetClass


        'COR.SQL.ExecuteNonQuery("DELECT FROM T_WHATEVER")
        Public Shared Sub ExecuteNonQueryFromFile(ByVal strEmbeddedFileName As String)
            Dim strSQL As String = GetEmbeddedSqlScript(strEmbeddedFileName, getMandant())
            ExecuteNonQuery(strSQL)
        End Sub ' ExecuteNonQuery


        Public Shared Sub ExecuteNonQuery(ByVal strSQL As String)
            ExecuteNonQuery(strSQL, getConnectionString())
        End Sub ' ExecuteNonQuery


        Public Shared Sub ExecuteNonQuery(ByVal strSQL As String, ByVal strConnectionString As String)

            Using cmd As System.Data.IDbCommand = New System.Data.SqlClient.SqlCommand(strSQL)
                ExecuteNonQuery(cmd, strConnectionString)
            End Using 'cmd

        End Sub ' ExecuteNonQuery


        Public Shared Sub ExecuteNonQuery(cmd As System.Data.IDbCommand)
            ExecuteNonQuery(cmd, getConnectionString())
        End Sub ' ExecuteNonQuery


        Public Shared Sub ExecuteNonQuery(cmd As System.Data.IDbCommand, ByVal strConnectionString As String)

            ' Create a connection
            SyncLock cmd

                Using idbConn As System.Data.IDbConnection = New System.Data.SqlClient.SqlConnection(strConnectionString)

                    SyncLock idbConn

                        If Not String.IsNullOrEmpty(strConnectionString) Then
                            idbConn.ConnectionString = strConnectionString
                        End If

                        cmd.Connection = idbConn

                        Try
                            If Not idbConn.State = System.Data.ConnectionState.Open Then
                                idbConn.Open()
                            End If

                            cmd.ExecuteNonQuery()
                            'MsgBox("Command completed successfully", MsgBoxStyle.OkOnly, "Success !")
                            'COR.Debug.MsgBox(String.Format("Command ExecuteSQLStmtNoNotification completed successfully. {0}", vbCrLf + "strSQL=" + strSQL))
                        Catch ex As System.Data.Common.DbException
                            'Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
                            ' Elmah.ErrorLog.Default.Log(New Elmah.Error(ex))

                            Throw
                            'COR.Debug.Output.MsgBox(String.Format("Exception executing ExecuteNonQuery: {0}", ex.Message.ToString() + vbCrLf + "strSQL=" + cmd.CommandText))
                        Finally
                            If idbConn IsNot Nothing Then
                                If Not idbConn.State = System.Data.ConnectionState.Closed Then
                                    idbConn.Close()
                                End If
                            End If
                        End Try

                    End SyncLock ' idbConn

                End Using ' idbConn

            End SyncLock ' cmd

        End Sub 'ExecuteNonQuery



        Private Shared Function IsNullable(t As Type) As Boolean
            If t Is Nothing Then
                Return False
            End If

            'Dim tDebug As Type = Nothing

            'If t.IsGenericType Then
            'tDebug = t.GetGenericTypeDefinition()
            'End If

            'If Not Object.ReferenceEquals(t, GetType(String)) AndAlso Not Object.ReferenceEquals(t, GetType(Integer)) Then
            'Console.WriteLine("SET BreakPoint HERE")
            'End If

            Return t.IsGenericType AndAlso Object.ReferenceEquals(t.GetGenericTypeDefinition(), GetType(Nullable(Of )))
        End Function ' IsNullable


        'protected const System.Reflection.BindingFlags m_CaseSensitivity = System.Reflection.BindingFlags.IgnoreCase;
        Protected Const m_CaseSensitivity As System.Reflection.BindingFlags = System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.Public Or System.Reflection.BindingFlags.IgnoreCase



        Private Shared Function MyChangeType(objVal As Object, ByVal t As Type) As Object


            If objVal Is Nothing Or objVal Is System.DBNull.Value Then
                Return Nothing
            End If

            'getbasetype
            Dim tThisType As Type = objVal.GetType()

            Dim bNullable As Boolean = IsNullable(t)
            If bNullable Then
                t = Nullable.GetUnderlyingType(t)
            End If

            If Object.ReferenceEquals(t, GetType(String)) AndAlso Object.ReferenceEquals(tThisType, GetType(Guid)) Then
                Return objVal.ToString()
            End If

            Return System.Convert.ChangeType(objVal, t)
        End Function


        ' Anything else than a struct or a class
        Private Shared Function IsSimpleType(tThisType As Type) As Boolean

            If tThisType.IsPrimitive Then
                Return True
            End If

            If Object.ReferenceEquals(tThisType, GetType(System.String)) Then
                Return True
            End If

            If Object.ReferenceEquals(tThisType, GetType(System.DateTime)) Then
                Return True
            End If

            If Object.ReferenceEquals(tThisType, GetType(System.Guid)) Then
                Return True
            End If

            If Object.ReferenceEquals(tThisType, GetType(System.Decimal)) Then
                Return True
            End If

            If Object.ReferenceEquals(tThisType, GetType(System.Object)) Then
                Return True
            End If


            Return False
        End Function





        Public Shared Function GetList(Of T)(cmd As System.Data.IDbCommand) As System.Collections.Generic.List(Of T)
            Dim lsReturnValue As New System.Collections.Generic.List(Of T)()
            Dim tThisValue As T = Nothing
            Dim tThisType As Type = GetType(T)


            SyncLock cmd
                Using idr As System.Data.IDataReader = ExecuteReader(cmd)

                    SyncLock idr

                        If IsSimpleType(tThisType) Then

                            While idr.Read()
                                Dim objVal As Object = idr.GetValue(0)
                                tThisValue = DirectCast(MyChangeType(objVal, GetType(T)), T)
                                'tThisValue = System.Convert.ChangeType(objVal, T),

                                lsReturnValue.Add(tThisValue)
                            End While

                        Else


                            Dim myi As Integer = idr.FieldCount


                            Dim fis As System.Reflection.FieldInfo() = New System.Reflection.FieldInfo(idr.FieldCount - 1) {}
                            'Action<T, object>[] setters = new Action<T, object>[idr.FieldCount];

                            For i As Integer = 0 To idr.FieldCount - 1
                                Dim strName As String = idr.GetName(i)
                                Dim fi As System.Reflection.FieldInfo = tThisType.GetField(strName, m_CaseSensitivity)

                                'if (fi != null)
                                '    setters[i] = GetSetter<T>(fi);
                                fis(i) = fi
                            Next i

                            While idr.Read()
                                'idr.GetOrdinal("")
                                tThisValue = Activator.CreateInstance(Of T)()

                                ' Console.WriteLine(idr.FieldCount);
                                For i As Integer = 0 To idr.FieldCount - 1
                                    Dim strName As String = idr.GetName(i)
                                    Dim objVal As Object = idr.GetValue(i)

                                    'System.Reflection.FieldInfo fi = t.GetField(strName, m_CaseSensitivity);
                                    If fis(i) IsNot Nothing Then
                                        'if (fi != null)
                                        'fi.SetValue(tThisValue, System.Convert.ChangeType(objVal, fi.FieldType));
                                        If objVal Is System.DBNull.Value Then
                                            objVal = Nothing
                                            'SetValue<T>(tThisValue, fi, null);
                                            'setters[i](tThisValue, null);
                                            fis(i).SetValue(tThisValue, Nothing)
                                        Else
                                            'System.ComponentModel.TypeConverter conv = System.ComponentModel.TypeDescriptor.GetConverter(fi.FieldType);

                                            'bool bNullable = IsNullable(fi.FieldType);
                                            Dim bNullable As Boolean = IsNullable(fis(i).FieldType)

                                            If bNullable Then
                                                fis(i).SetValue(tThisValue, objVal)
                                            Else
                                                'SetValue<T>(tThisValue, fi, objVal);
                                                'setters[i](tThisValue, objVal);


                                                'fis(i).SetValue(tThisValue, System.Convert.ChangeType(objVal, fis(i).FieldType))
                                                fis(i).SetValue(tThisValue, MyChangeType(objVal, fis(i).FieldType))



                                                'SetValue<T>(tThisValue, fi, System.Convert.ChangeType(objVal, fi.FieldType));
                                                'setters[i](tThisValue, System.Convert.ChangeType(objVal, fis[i].FieldType));
                                            End If

                                        End If
                                    Else
                                        ' End if (fi != null) 
                                        Dim pi As System.Reflection.PropertyInfo = tThisType.GetProperty(strName, m_CaseSensitivity)
                                        If pi IsNot Nothing Then
                                            'pi.SetValue(tThisValue, System.Convert.ChangeType(objVal, pi.PropertyType), null);

                                            If objVal Is System.DBNull.Value Then
                                                objVal = Nothing
                                                pi.SetValue(tThisValue, Nothing, Nothing)
                                            Else
                                                Dim bNullable As Boolean = IsNullable(pi.PropertyType)
                                                If bNullable Then
                                                    pi.SetValue(tThisValue, objVal, Nothing)
                                                Else
                                                    pi.SetValue(tThisValue, System.Convert.ChangeType(objVal, pi.PropertyType), Nothing)
                                                End If

                                            End If

                                            ' Else silently ignore value
                                        End If ' End if (pi != null)

                                        'Console.WriteLine(strName);
                                    End If ' End else of if (fi != null)
                                Next i

                                lsReturnValue.Add(tThisValue)
                            End While ' Whend

                        End If ' tThisType.IsPrimitive OrElse Object.ReferenceEquals(tThisType, GetType(String)) 

                        idr.Close()
                    End SyncLock ' idr

                End Using ' idr

            End SyncLock ' cmd

            Return lsReturnValue
        End Function ' GetList


        Public Shared Function ExecuteScalar(Of T)(ByVal cmd As System.Data.IDbCommand) As T
            Dim strReturnValue As String = ""

            ' Create a connection
            Using sqldbConnection As System.Data.IDbConnection = New System.Data.SqlClient.SqlConnection(getConnectionString())

                SyncLock sqldbConnection

                    SyncLock cmd

                        Try
                            cmd.Connection = sqldbConnection

                            If Not cmd.Connection.State = ConnectionState.Open Then
                                cmd.Connection.Open()
                            End If


                            Dim objResult As Object = cmd.ExecuteScalar()
                            If objResult IsNot Nothing Then
                                strReturnValue = objResult.ToString()
                            Else
                                strReturnValue = Nothing
                            End If
                            objResult = Nothing

                            'MsgBox("Command completed successfully", MsgBoxStyle.OkOnly, "Success !")
                        Catch ex As System.Data.Common.DbException
                            'Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
                            Dim strMessage As String = "Exception in ExecuteSQLstmtScalar(Of T)" + Environment.NewLine
                            strMessage += "SQL: " + cmd.CommandText + Environment.NewLine
                            strMessage += "Exception: " + ex.Message.ToString + Environment.NewLine
                            strMessage += "StackTrace: " + ex.StackTrace.ToString

                            'COR.Debug.Output.MsgBox(strMessage)
                            'System.Diagnostics.Debug.WriteLine(strMessage)
                            Throw
                        Finally
                            If cmd.Connection IsNot Nothing Then
                                If Not cmd.Connection.State = System.Data.ConnectionState.Closed Then
                                    cmd.Connection.Close()
                                End If
                            End If
                        End Try

                    End SyncLock ' cmd

                End SyncLock ' sqldbConnection

            End Using ' sqldbConnection


            Try
                Dim tReturnType As Type = GetType(T)

                If tReturnType Is GetType(String) Then
                    Return CAnyType(Of T)(CType(strReturnValue, Object))
                ElseIf tReturnType Is GetType(Boolean) Then
                    If String.IsNullOrEmpty(strReturnValue) Then
                        Return CAnyType(Of T)(False)
                    End If

                    If IsNumeric(strReturnValue) Then
                        If strReturnValue.Trim() = "0" Then
                            Return CAnyType(Of T)(False)
                        Else
                            Return CAnyType(Of T)(True)
                        End If
                    End If

                    Dim bReturnValue As Boolean = Boolean.Parse(strReturnValue)
                    Return CAnyType(Of T)(CType(bReturnValue, Object))
                ElseIf tReturnType Is GetType(Integer) Then
                    Dim iReturnValue As Integer = Integer.Parse(strReturnValue)
                    Return CAnyType(Of T)(CType(iReturnValue, Object))
                ElseIf tReturnType Is GetType(Long) Then
                    Dim lngReturnValue As Long = Long.Parse(strReturnValue)
                    Return CAnyType(Of T)(CType(lngReturnValue, Object))
                ElseIf tReturnType Is GetType(Type) Then
                    ' Type.GetType() will only look in the calling assembly and then mscorlib.dll for the type. 
                    ' Use Type.AssemblyQualifiedName for getting any type.
                    Dim tReturnValue As Type = Type.GetType(strReturnValue)
                    If StringComparer.OrdinalIgnoreCase.Equals(strReturnValue, "System.Uri") Then
                        tReturnValue = GetType(System.Uri)
                    End If

                    Return CAnyType(Of T)(CType(tReturnValue, Object))
                Else
                    'COR.Debug.Output.MsgBox("ExecuteSQLstmtScalar(Of " + GetType(T).ToString() + "): This type is not yet defined.")
                    'System.Diagnostics.Trace.WriteLine("ExecuteSQLstmtScalar(Of T): This type is not yet defined.")
                    Throw New System.NotImplementedException("ExecuteSQLstmtScalar(Of " + GetType(T).ToString() + "): This type is not yet defined.")
                End If

            Catch ex As Exception
                'Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
                'COR.Debug.Output.MsgBox("Exception in ExecuteSQLstmtScalar(Of " + GetType(T).ToString() + ")." + Environment.NewLine + "Description: " + ex.Message)
                'System.Diagnostics.Trace.WriteLine("Exception in ExecuteSQLstmtScalar. Description: " + ex.Message)
                Throw
            End Try

            Return Nothing
        End Function ' ExecuteScalar


        Protected Shared Function CAnyType(Of T)(ByRef UTO As Object) As T
            Return CType(UTO, T)
        End Function ' CAnyType


        Public Shared Function ExecuteScalarFromFile(Of T)(ByVal strEmbeddedFileName As String) As T
            Dim strSQL As String = GetEmbeddedSqlScript(strEmbeddedFileName, getMandant())
            Return ExecuteScalar(Of T)(strSQL)
        End Function


        Public Shared Function ExecuteScalar(Of T)(ByVal strSQL As String) As T
            Dim tReturnValue As T = Nothing

            Using cmd As System.Data.IDbCommand = New System.Data.SqlClient.SqlCommand(strSQL)
                tReturnValue = ExecuteScalar(Of T)(cmd)
            End Using ' cmd

            Return tReturnValue
        End Function ' ExecuteScalar


        Public Shared Function AddParameter(command As System.Data.IDbCommand, strParameterName As String, objValue As Object) As System.Data.IDbDataParameter
            Return AddParameter(command, strParameterName, objValue, System.Data.ParameterDirection.Input)
        End Function ' AddParameter


        Public Shared Function AddParameter(command As System.Data.IDbCommand, strParameterName As String, objValue As Object, pad As System.Data.ParameterDirection) As System.Data.IDbDataParameter
            If objValue Is Nothing Then
                'throw new ArgumentNullException("objValue");
                objValue = System.DBNull.Value
            End If
            ' End if (objValue == null)
            Dim tDataType As System.Type = objValue.GetType()
            Dim dbType As System.Data.DbType = GetDbType(tDataType)

            Return AddParameter(command, strParameterName, objValue, pad, dbType)
        End Function ' AddParameter


        Public Shared Function AddParameter(command As System.Data.IDbCommand, strParameterName As String, objValue As Object, pad As System.Data.ParameterDirection, dbType As System.Data.DbType) As System.Data.IDbDataParameter
            Dim parameter As System.Data.IDbDataParameter = command.CreateParameter()

            If Not strParameterName.StartsWith("@") Then
                strParameterName = "@" + strParameterName
            End If
            ' End if (!strParameterName.StartsWith("@"))
            parameter.ParameterName = strParameterName
            parameter.DbType = dbType
            parameter.Direction = pad

            If objValue Is Nothing Then
                parameter.Value = System.DBNull.Value
            Else
                parameter.Value = objValue
            End If

            command.Parameters.Add(parameter)
            Return parameter
        End Function ' AddParameter


        ' From Type to DBType
        Protected Shared Function GetDbType(type As Type) As System.Data.DbType
            ' http://social.msdn.microsoft.com/Forums/en/winforms/thread/c6f3ab91-2198-402a-9a18-66ce442333a6
            Dim strTypeName As String = type.Name
            Dim DBtype As System.Data.DbType = System.Data.DbType.String ' default value

            If Object.ReferenceEquals(type, GetType(System.DBNull)) Then
                Return DBtype
            End If

            Try
                DBtype = CType(System.Enum.Parse(GetType(System.Data.DbType), strTypeName, True), System.Data.DbType)
                ' add error handling to suit your taste
            Catch exMappingInexistant As Exception
                'Elmah.ErrorSignal.FromCurrentContext().Raise(exMappingInexistant)
            End Try

            Return DBtype
        End Function ' GetDbType

        Private Shared Function getWebApplicationAssembly() As System.Reflection.Assembly
            Return System.Reflection.Assembly.ReflectionOnlyLoad("Portal_SQL")
        End Function

        Private Shared Function getWebApplicationAssembly_old() As System.Reflection.Assembly
            Dim assReturnValue As System.Reflection.Assembly
            Dim context As System.Web.HttpContext = System.Web.HttpContext.Current

            assReturnValue = GetType(claSQL).Assembly
            If assReturnValue IsNot Nothing Then
                Return assReturnValue
            End If


            Dim AspNetNamespace As String = "ASP"

            'System.Web.IHttpHandler handler = context.CurrentHandler;
            Dim handler As System.Web.IHttpHandler = context.Handler
            If handler Is Nothing Then
                Return Nothing
            End If

            Dim type As Type = handler.GetType()
            While type IsNot Nothing AndAlso Not Object.ReferenceEquals(type, GetType(Object)) AndAlso type.Namespace = AspNetNamespace
                type = type.BaseType
            End While

            Return type.Assembly
        End Function ' getWebApplicationAssembly


        Public Shared Function GetEmbeddedSqlScript(ByVal strScriptName As String, ByVal enuMandant As Mandant) As String
            'Dim ass As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            'Dim ass As System.Reflection.Assembly = System.Reflection.Assembly.GetCallingAssembly()

            Static ass As System.Reflection.Assembly = getWebApplicationAssembly()
            Return GetEmbeddedSqlScript(strScriptName, ass, enuMandant)
        End Function ' GetEmbeddedSqlScript


        ' COR.SQL.GetEmbeddedSQLscript("UpdateFreigabeDatum.sql")
        Public Shared Function GetEmbeddedSqlScript(ByVal strScriptName As String, ByRef ass As System.Reflection.Assembly, ByVal enuMandant As Mandant) As String
            Dim strReturnValue As String = Nothing

            Try
                If ass.EntryPoint IsNot Nothing Then
                    Dim strDefaultNamespace As String = ass.EntryPoint.DeclaringType.FullName
                    strDefaultNamespace = Mid(strDefaultNamespace, 1, strDefaultNamespace.IndexOf(".") + 1)
                    strScriptName = strDefaultNamespace + strScriptName
                End If
            Catch ex As Exception
                'Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
            End Try


            Dim bNotFound As Boolean = True
            Dim strOverwrite As String = (ass.FullName.Split(Microsoft.VisualBasic.ChrW(44))(0) + ("._" + CInt(enuMandant).ToString))
            For Each strThisRessourceName As String In ass.GetManifestResourceNames
                'Mandant overwritten version, leave on find
                If ((Not (strThisRessourceName) Is Nothing) _
                            AndAlso (strThisRessourceName.EndsWith(strScriptName, StringComparison.OrdinalIgnoreCase) AndAlso strThisRessourceName.StartsWith(strOverwrite, StringComparison.OrdinalIgnoreCase))) Then
                    Dim sr As System.IO.StreamReader = New System.IO.StreamReader(ass.GetManifestResourceStream(strThisRessourceName))
                    bNotFound = False
                    strReturnValue = sr.ReadToEnd
                    Exit For
                End If
                'Global version, continue on find if not global
                If ((Not (strThisRessourceName) Is Nothing) _
                            AndAlso strThisRessourceName.EndsWith(strScriptName, StringComparison.OrdinalIgnoreCase)) Then
                    Dim sr As System.IO.StreamReader = New System.IO.StreamReader(ass.GetManifestResourceStream(strThisRessourceName))
                    bNotFound = False
                    strReturnValue = sr.ReadToEnd
                End If
            Next

            'Dim bNotFound As Boolean = True
            'For Each strThisRessourceName As String In ass.GetManifestResourceNames
            '    If strThisRessourceName IsNot Nothing AndAlso strThisRessourceName.EndsWith(strScriptName, StringComparison.OrdinalIgnoreCase) Then

            '        Using sr As New System.IO.StreamReader(ass.GetManifestResourceStream(strThisRessourceName))
            '            bNotFound = False
            '            strReturnValue = sr.ReadToEnd()
            '        End Using

            '        Exit For
            '    End If
            'Next strThisRessourceName

            If bNotFound Then
                Throw New Exception("No script called """ + strScriptName + """ found in embedded ressources.")
            End If

            Return strReturnValue
        End Function ' GetEmbeddedSqlScript
#End Region

        ''' <summary>
        ''' Returns the ConnectionString.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Returns corDBAccess_SqlClient.GetConnectionString.</remarks>
        Public Shared Function getConnectionString() As String
            Return claSQL.getConnectionString(Nothing)
        End Function


        Public Shared Function GetInitialCatalog() As String
            Dim strInitialCatalog As String = Nothing
            Dim csb As System.Data.SqlClient.SqlConnectionStringBuilder = New System.Data.SqlClient.SqlConnectionStringBuilder
            csb.ConnectionString = getConnectionString()

            strInitialCatalog = csb.InitialCatalog
            csb.Clear()
            csb = Nothing

            Return strInitialCatalog
        End Function


#Region "Copied from corDBAccess.vb"
        ' Requires reference to System.Configuration
        ' http://stackoverflow.com/questions/6582970/separate-connectionstrings-and-mailsettings-from-web-config-possible
        Protected Shared strStaticConnectionString As String = Nothing
        Public Shared Function GetConnectionString(ByVal strIntitialCatalog As String) As String
            Dim strReturnValue As String = Nothing
            Dim strProviderName As String = Nothing


            If String.IsNullOrEmpty(strStaticConnectionString) Then
                Dim strConnectionStringName As String = System.Environment.MachineName

                If String.IsNullOrEmpty(strConnectionStringName) Then
                    strConnectionStringName = "LocalSqlServer"
                End If



                ' Walk through the collection and return the first 
                ' connection string matching the connectionString name.
                Dim settings As System.Configuration.ConnectionStringSettingsCollection = System.Configuration.ConfigurationManager.ConnectionStrings
                If (settings IsNot Nothing) Then
                    For Each cs As System.Configuration.ConnectionStringSettings In settings
                        If StringComparer.OrdinalIgnoreCase.Equals(cs.Name, strConnectionStringName) Then
                            strReturnValue = cs.ConnectionString
                            strProviderName = cs.ProviderName
                            Exit For
                        End If
                    Next
                End If

                If String.IsNullOrEmpty(strReturnValue) Then
                    strConnectionStringName = "server"

                    Dim conString As System.Configuration.ConnectionStringSettings = System.Configuration.ConfigurationManager.ConnectionStrings(strConnectionStringName)

                    If conString IsNot Nothing Then
                        strReturnValue = conString.ConnectionString
                    End If
                End If

                If String.IsNullOrEmpty(strReturnValue) Then
                    Throw New ArgumentNullException("ConnectionString """ & strConnectionStringName & """ in file web.config.")
                End If

                settings = Nothing
                strConnectionStringName = Nothing
            Else
                If String.IsNullOrEmpty(strIntitialCatalog) Then
                    Return strStaticConnectionString
                End If

                strReturnValue = strStaticConnectionString
            End If

            Dim sb As System.Data.SqlClient.SqlConnectionStringBuilder = New System.Data.SqlClient.SqlConnectionStringBuilder(strReturnValue)


            If String.IsNullOrEmpty(strStaticConnectionString) Then
                If Not sb.IntegratedSecurity Then
                    sb.Password = "TopSecret" ' Portal.Crypt.DeCrypt(System.Convert.ToString(sb.Password))
                End If
                strReturnValue = sb.ConnectionString
                strStaticConnectionString = strReturnValue
            End If


            If Not String.IsNullOrEmpty(strIntitialCatalog) Then
                sb.InitialCatalog = strIntitialCatalog
            End If


            strReturnValue = sb.ConnectionString
            sb = Nothing

            Return strReturnValue
        End Function ' GetConnectionString
#End Region
#End Region
    End Class


End Namespace
