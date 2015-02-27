
-- DECLARE @in_mandant int 
-- DECLARE @in_sprache varchar(2) 
-- DECLARE @in_dwg varchar(50) 
-- DECLARE @in_stylizer varchar(35) 

-- SET @in_mandant = 0 
-- SET @in_sprache = 'DE' 
-- SET @in_dwg = '6705_GB01_ZG00_0000' 
-- SET @in_stylizer = 'Nutzungsart' 

SET @in_sprache = UPPER(@in_sprache) 


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
        WHEN AP_LEG_TYP = (@in_stylizer + 'Summe') 
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
AND T_AP_Legenden.AP_LEG_Mandant = @in_mandant 

AND T_AP_Legenden.AP_LEG_DWG = @in_dwg 
AND 
(
    T_AP_Legenden.AP_LEG_Typ = @in_stylizer 
    OR 
    T_AP_Legenden.AP_LEG_Typ = (@in_stylizer + 'Summe') 
)

ORDER BY AP_LEG_DWG, IsSumme DESC, AP_LEG_Typ, AP_LEG_Nr 
