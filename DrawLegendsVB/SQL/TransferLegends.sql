

USE master
GO

--EXEC sp_helpserver 
--EXEC sp_dropserver @server =  'CORDB2008R2' 


EXEC sp_addlinkedserver 
    'CORDB2008R2',
    N'SQL Server'
GO

EXEC sp_addlinkedsrvlogin @rmtsrvname = N'CORDB2008R2', @locallogin = N'ApertureWebServicesDE', @useself = N'False', @rmtuser = N'ApertureWebServicesDE', @rmtpassword = N'meridian08'



GO


USE COR_Basic

INSERT INTO T_AP_Legenden
(
	 AP_LEG_Nr
	,AP_LEG_Typ
	,AP_LEG_DWG
	,AP_LEG_Mandant
	,AP_LEG_UID
	,AP_LEG_BezeichnungDE
	,AP_LEG_BezeichnungFR
	,AP_LEG_BezeichnungIT
	,AP_LEG_BezeichnungEN
	,AP_LEG_Zahl
	,AP_LEG_ForeColor
	,AP_LEG_BackColor
	,AP_LEG_LineColor
	,AP_LEG_Pattern
	,AP_LEG_FID_UID
) 
SELECT 
	 AP_LEG_Nr
	,AP_LEG_Typ
	,AP_LEG_DWG
	,AP_LEG_Mandant
	,AP_LEG_UID
	,AP_LEG_BezeichnungDE
	,AP_LEG_BezeichnungFR
	,AP_LEG_BezeichnungIT
	,AP_LEG_BezeichnungEN
	,AP_LEG_Zahl
	,AP_LEG_ForeColor
	,AP_LEG_BackColor
	,AP_LEG_LineColor
	,AP_LEG_Pattern
	,AP_LEG_FID_UID
FROM CORDB2008R2.COR_Basic_Wincasa_Test.dbo.T_AP_Legenden 
