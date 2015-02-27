
using System;
using System.Collections.Generic;
using System.Text;


namespace VWS.Legenden
{


    class cDrawingData
    {


        // Not used
        public static System.Data.DataTable GetColorTranslations()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();
            csb.DataSource = Environment.MachineName;
            csb.InitialCatalog = "COR_Basic_Wincasa";
            csb.IntegratedSecurity = true;


            string strSQL = @"
SELECT 
	 T_SYS_ApertureColorToHex.COL_Aperture 
	,T_SYS_ApertureColorToHex.COL_Hex 
	,'#' + T_SYS_ApertureColorToHex.COL_Hex AS COL_Html 
FROM T_SYS_ApertureColorToHex 
WHERE T_SYS_ApertureColorToHex.COL_Status = 1 
ORDER BY COL_Aperture, COL_Hex 
";


            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(strSQL, csb.ConnectionString);
            da.Fill(dt);
            return dt;
        }


        public static string GetLegendFooterHtml()
        {
            string strHTML = null;

            System.Text.StringBuilder sb = new System.Text.StringBuilder(@"
<!DOCTYPE html>
<html>
<head>
    <style type=""text/css"" media=""all"">
        html, body { margin: 0px; padding: 0px;} 
        body { margin-left: 10px;} 
        p { margin: 0px; padding: 0px; font-family: Verdana; font-weight: normal; font-size: 12px; } 
        .DwgReferenz { font-family: Verdana; font-weight: normal; font-size: 9px; margin-top: 10px; } 
        .LogoPosition { margin-left: 30px; } 
    </style>
</head>
<body>
   <p>Liegenschafts-Nr.: @@SO_Nr@@    <img class=""LogoPosition"" width=""67px"" height=""18px"" src=""data:@@Logo@@"" /></p>
   <p>@@Building@@ - @@Street@@ @@StreetNo@@</p>
   <p style=""color: orange;"">@@floor_designation@@</p>
   <p>@@Owner@@</p>
   <p>Flächenmanagement</p>
   <p>@@Owner_Street@@ @@Owner_StreetNo@@, CH-@@PLZ@@ @@Location@@</p>
   <p>Phone @@Phone@@&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Fax @@Fax@@</p>
   <p>Druckdatum: @@curdate@@</p>
   <p class=""DwgReferenz"">DWG-Referenz:&nbsp;&nbsp;&nbsp;@@DWG@@</p>

</body>
</html>
");
            sb = sb

                .Replace("@@Logo@@", "image/gif;base64,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")
                .Replace("@@SO_Nr@@", "1010")
                .Replace("@@Building@@", "Jelmoli House of Brands")
                .Replace("@@Street@@", "Grüzefeldstrasse")
                .Replace("@@StreetNo@@", "1")
                .Replace("@@PLZ@@", "8401")
                .Replace("@@Location@@", "Winterthur")
                .Replace("@@floor_designation@@", "Erdgeschoss (Beta-Freigabe)")
                .Replace("@@DWG@@", "1010_GB01_EG00_0000.dwg")
                .Replace("@@curdate@@", System.DateTime.Now.ToString("dd.MM.yyyy"))

                .Replace("@@Owner_Street@@", "Grüzefeldstrasse")
                .Replace("@@Owner_StreetNo@@", "41")
                .Replace("@@Owner@@", "Wincasa AG, Immobilien Dienstleistungen")

                .Replace("@@Phone@@", "052 268 87 54")
                .Replace("@@Fax@@", "052 268 89 15")
            ;

            strHTML = sb.ToString();
            sb = null;

            return strHTML;
        } // End Function GetLegendFooterHtml 


        public static System.Data.DataTable GetData()
        {
            return GetData(0, "6705_GB01_ZG00_0000", "Nutzungsart", "de");
            // return GetData(0, "7610_GB01_UG01_0000", "Parkplatzmieter", "de");
            // return GetData(0, "1010_GB01_OG04_0000", "Mieter", "de");
        } // End Function GetData 


        public static System.Data.DataTable GetData(int mandant, string DWG, string Typ, string strSprache)
        {
            DWG = DWG.Replace("'", "''");
            Typ = Typ.Replace("'", "''");
            strSprache = strSprache.Replace("'", "''").ToUpper();

            string strSQL = @"
DECLARE @in_sprache varchar(2)
SET @in_sprache = '" + strSprache + @"'

SELECT 
	 T_AP_Legenden.AP_LEG_Nr
	--,T_AP_Legenden.AP_LEG_Typ
	--,T_AP_Legenden.AP_LEG_DWG
	--,T_AP_Legenden.AP_LEG_UID
	,
	CASE @in_sprache 
		WHEN 'FR' THEN T_AP_Legenden.AP_LEG_BezeichnungFR 
		WHEN 'FR' THEN T_AP_Legenden.AP_LEG_BezeichnungIT 
		WHEN 'FR' THEN T_AP_Legenden.AP_LEG_BezeichnungEN 
		ELSE T_AP_Legenden.AP_LEG_BezeichnungDE 
	END AS [Text] 
	
	,ROUND(T_AP_Legenden.AP_LEG_Zahl, 2) AS Value 
	
	
	,T_AP_Legenden.AP_LEG_ForeColor 
	,T_AP_Legenden.AP_LEG_BackColor 
	,T_AP_Legenden.AP_LEG_LineColor 
	
	,'#' + tForeColor.COL_Hex AS Html_ForeColor 
	,'#' + tBackColor.COL_Hex AS Html_BackColor 
	,'#' + tLineColor.COL_Hex AS Html_LineColor 
	
	,T_AP_Legenden.AP_LEG_Pattern 
	--,T_AP_Legenden.AP_LEG_FID_UID 
    ,
    CASE 
        WHEN AP_LEG_TYP LIKE '%Summe' 
            THEN 1 
        ELSE 0 
    END AS IsSumme 
FROM T_AP_Legenden 

LEFT JOIN T_SYS_ApertureColorToHex AS tForeColor 
	ON tForeColor.COL_Aperture = T_AP_Legenden.AP_LEG_ForeColor 
	AND tForeColor.COL_Status = 1 
	
LEFT JOIN T_SYS_ApertureColorToHex AS tBackColor 
	ON tBackColor.COL_Aperture = T_AP_Legenden.AP_LEG_BackColor 
	AND tBackColor.COL_Status = 1 
	
LEFT JOIN T_SYS_ApertureColorToHex AS tLineColor 
	ON tLineColor.COL_Aperture = T_AP_Legenden.AP_LEG_LineColor 
	AND tLineColor.COL_Status = 1 
	
WHERE (1=1) 
AND T_AP_Legenden.AP_LEG_Mandant = " + mandant.ToString() + @" 

AND T_AP_Legenden.AP_LEG_DWG = '" + DWG + @"' 
AND 
(
    T_AP_Legenden.AP_LEG_Typ = '" + Typ + @"' 
    OR 
    T_AP_Legenden.AP_LEG_Typ = '" + Typ + @"Summe' 
)

ORDER BY AP_LEG_DWG, IsSumme DESC, AP_LEG_Typ, AP_LEG_Nr 
";

            return Tools.SQL.MS_SQL.GetDataTable(strSQL);


            //System.Data.DataTable dt = new System.Data.DataTable();
            //dt.Columns.Add("Text", typeof(string));
            //dt.Columns.Add("Value", typeof(double));

            //for (int i = 1; i <= 30; ++i)
            //{
            //    System.Data.DataRow dr = dt.NewRow();
            //    if (i < 10)
            //        dr["Text"] = "Text  " + i.ToString();
            //    else
            //        dr["Text"] = "Text " + i.ToString();
            //    dr["Value"] = 100000 + (i * 20) + (i - 1) * 0.03;
            //    dt.Rows.Add(dr);
            //} // Next i

            //dt.Rows[5]["Text"] = "This is an extra long text";


            //return dt;
        } // End Function GetData


    } // End Class cDrawingData


} // End Namespace DrawLegends
