
-- exec sp_helpserver
-- exec sp_helptext 'sp_helpserver'




SELECT 
	 srvname AS name 
	,srvnetname AS network 
	,CONVERT(varchar(100), '') AS status 
	,srvid AS id
	,srvstatus AS srvstat
	,topologyx AS topx
	,topologyy AS topy
	,CONVERT(sysname, CollationPropertyFromID(srvcollation, 'name')) AS collation_name
	,connecttimeout AS connect_timeout
	,querytimeout AS query_timeout
FROM master.dbo.sysservers

select * from sys.servers 
--------------------------------------




DECLARE
     @server         sysname      -- server name  
    ,@optname        varchar(35)  -- option name to limit results  
    ,@show_topology  varchar(1)   -- 't' to show topology coordinates  

SET @server = NULL 
SET @optname = NULL 
SET @show_topology = NULL 

    -- PRELIMINARY  
    SET NOCOUNT ON 
    
    declare @optbit     int,  
            @bitdesc    sysname,  
            @curbit     int  
  
    -- CHECK IF REQUESTED SERVER(S) EXIST  
    if not exists (select * from master.dbo.sysservers where  
  (@server is null or srvname = @server))  
    begin  
        if @server is null  
            raiserror(15205,-1,-1)  
        else  
            raiserror(15015,-1,-1,@server)  
        --return (1)  
    end  
  
    -- GET THE BIT VALUE(S) FOR THE OPTION REQUESTED  
    if @optname is not null  
    begin  
        select @optbit = number from master.dbo.spt_values  
            where type = 'A' and name = @optname  
        if @optbit is null  
        begin  
            raiserror(15206,-1,-1,@optname)  
            --return(1)  
        end  
    end  
    else  
        select @optbit = -1     -- 0xffffffff  
  
    -- MAKE WORK COPY OF RELEVANT PART OF SYSSERVERS  
    select name = srvname, network = srvnetname, status = convert(varchar(100), ''),  
            id = srvid, srvstat = srvstatus, topx = topologyx, topy = topologyy,  
   collation_name = convert(sysname, CollationPropertyFromID(srvcollation, 'name')),  
   connect_timeout = connecttimeout, query_timeout = querytimeout  
        into #spt_server  
        from master.dbo.sysservers  
  where (@server is null or srvname = @server) and (@optname is null or srvstatus & @optbit <> 0)  
  
    -- SET THE STATUS FIELD  
    select @curbit = 1  
    while @curbit < 0x10000 -- bit field is a smallint  
    begin  
        select @bitdesc = null  
        select @bitdesc = name from master.dbo.spt_values  
   where type = 'A' and number = @curbit  
        if @bitdesc is not null  
            update #spt_server set status = status + ',' + @bitdesc where srvstat & @curbit <> 0  
        select @curbit = @curbit * 2  
    end  
  
    -- SHOW THE RESULT SET  
    if lower(@show_topology) <> 't' or @show_topology is null  
     select name, network_name = network,  
          status = isnull(substring(status,2,8000),''),  
                id = convert(char(4), id),  
    collation_name, connect_timeout, query_timeout  
     from #spt_server order by name  
    else  
     select name, network_name = network,  
          status = isnull(substring(status,2,8000),''),  
                id = convert(char(4), id),  
    collation_name, connect_timeout, query_timeout,  
    topx, topy  
     from #spt_server order by name  
  
    -- RETURN SUCCESS  
    --return(0) -- sp_helpserver  