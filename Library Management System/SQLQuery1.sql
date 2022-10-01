--select
--    'data source=' + @@servername +
--    ';initial catalog=' + db_name() +
--    case type_desc
--        when 'WINDOWS_LOGIN' 
--            then ';trusted_connection=true'
--    end
--    as ConnectionString
--from sys.server_principals
--where name = suser_name()

create table Books 
(
	IDs int,
	Name char(10),
)
