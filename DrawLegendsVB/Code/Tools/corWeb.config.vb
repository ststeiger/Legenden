Namespace Portal.Web
    Public Class config
        Public Shared Function IsNullableEnum(t As Type) As Boolean
            Dim tUnderlyingType As Type = Nullable.GetUnderlyingType(t)
            Return tUnderlyingType IsNot Nothing AndAlso tUnderlyingType.IsEnum
        End Function ' IsNullableEnum

        Public Shared Function IsEnum(t As Type) As Boolean
            If t IsNot Nothing AndAlso t.IsEnum Then
                Return True
            End If

            Return IsNullableEnum(t)
        End Function ' IsEnum


        Public Shared Function IsTrustedMachine() As Boolean
            Dim bNoTrust As Boolean = False
            Dim strTrustedMachines As String = Nothing

            Try
                strTrustedMachines = GetAppSetting(Of String)("TrustedMachines")
                bNoTrust = GetAppSetting(Of Boolean)("NoTrust")

                If bNoTrust Then
                    Return False
                End If
            Catch ex As Exception
                ' We explicitly don't care if these values don't exist
            End Try


            If Not String.IsNullOrEmpty(strTrustedMachines) Then
                Dim astrTrustedMachines As String() = strTrustedMachines.Split(","c)

                For i As Integer = 0 To astrTrustedMachines.Length - 1 Step 1
                    astrTrustedMachines(i) = Trim(astrTrustedMachines(i))

                    If StringComparer.OrdinalIgnoreCase.Equals(astrTrustedMachines(i), Environment.MachineName) Then
                        astrTrustedMachines = Nothing
                        Return True
                    End If

                Next i

                astrTrustedMachines = Nothing
            End If

            Return False
        End Function ' GetTrustedMachines


        Public Shared Function GetAppSetting(Of T1)(ByVal strKey As String, defValue As T1) As T1
            Dim t1RetVal As T1 = Nothing

            Try
                t1RetVal = GetAppSetting(Of T1)(strKey)
            Catch ex As Exception
                t1RetVal = defValue
            End Try

            Return t1RetVal
        End Function


        Public Shared Function GetAppSetting(Of T1)(ByVal strKey As String) As T1
            Return GetNonCachedConfigValue(Of T1)(strKey)
        End Function ' GetAppSetting

        Protected Shared Function GetNonCachedConfigValue(Of T1)(ByVal strKey As String) As T1
            Return GetConfigValueFromSQL(Of T1)(strKey)
        End Function ' GetNonCachedConfigValue

        Protected Shared Function GetConfigValueFromSQL(Of T)(ByVal strKey As String) As T
            Dim strValue As String = Nothing
            Dim ex As Exception = Nothing

            If String.IsNullOrEmpty(strKey) Then
                Throw New NullReferenceException("strKey is NULL or empty")
            End If

            Using cmd As System.Data.IDbCommand = SQL.claSQL.CreateCommandFromFile("GetConfiguration.sql")
                SQL.claSQL.AddParameter(cmd, "CONF_Key", strKey)

                Using dt As DataTable = SQL.claSQL.GetDataTable(cmd)
                    ' CONF_Value         CONF_Type   IsUnique
                    ' D:\WINCASA_DMS_SUS\   System.Uri     1

                    If dt Is Nothing OrElse dt.Rows.Count < 1 Then
                        ex = New Exception("Error on accessing application configuration in database. " + Environment.NewLine + "Key """ + strKey + """ is not listed in T_FMS_Configuration.FC_Key.")
                    Else
                        Dim bIsUnique As Boolean = System.Convert.ToBoolean(dt.Rows(0)("CONF_IsUnique"))
                        If Not bIsUnique Then
                            ex = New Exception("Duplicate configuration key """ + strKey + """. Key is not unique !")
                        End If


                        strValue = System.Convert.ToString(dt.Rows(0)("CONF_Value"))
                        strValue = Trim(strValue)
                        If String.IsNullOrEmpty(strValue) Then
                            ex = New NullReferenceException("T_FMS_Configuration.CONF_Value for Key """ + strKey + """ is NULL or empty (COR.web.config.GetConfigValueFromSQL)")
                        End If


                        Dim strType As String = System.Convert.ToString(dt.Rows(0)("CONF_Type"))
                        If String.IsNullOrEmpty(strType) Then
                            ex = New NullReferenceException("T_FMS_Configuration.CONF_Type for key """ + strKey + """ is NULL or empty (COR.web.config.GetConfigValueFromSQL)")
                        End If


                        If StringComparer.OrdinalIgnoreCase.Equals(strType, GetType(System.Uri).FullName) Then
                            Try
                                Dim x As System.Uri = New System.Uri(strValue, System.UriKind.RelativeOrAbsolute)
                            Catch exNoURI As Exception
                                ex = New Exception("T_FMS_Configuration.CONF_Value for key """ + strKey + """ is not a valid absolute or relative URI")
                            End Try
                        End If ' StringComparer.OrdinalIgnoreCase.Equals(strType, GetType(System.Uri).FullName) 
                    End If '  dt Is Nothing
                End Using 'dt
            End Using ' cmd

            If ex IsNot Nothing Then
                Throw ex
            End If

            Return CAnyType(Of String, T)(strValue)
        End Function

        Protected Shared Function CAnyType(Of T1, T2)(ByRef UTO As T1) As T2
            Dim objTemp As Object = CType(UTO, Object)

            If IsEnum(GetType(T2)) AndAlso Object.ReferenceEquals(GetType(T1), GetType(String)) Then
                Dim strEnumValue As String = objTemp.ToString()
                Dim t2Enum As T2 = CType([Enum].Parse(GetType(T2), strEnumValue, True), T2)
                Return t2Enum
            End If

            Return CType(objTemp, T2)
        End Function
    End Class


End Namespace
