
using System;
using System.Collections.Generic;
using System.Text;


namespace Tools.SQL
{


    public class MS_SQL
    {

        public static System.Data.DataTable GetDataTable(string strSQL)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            using (System.Data.Common.DbDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(strSQL, GetConnectionString()))
            {
                da.Fill(dt);
            } // End Using System.Data.Common.DbDataAdapter da

            return dt;
        }


        public static string GetConnectionString()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();
            csb.DataSource = "CORDB2008R2";
            csb.InitialCatalog = "COR_Basic_Wincasa_Test";
            csb.IntegratedSecurity = true;

            return csb.ConnectionString;
        }


    }


}
