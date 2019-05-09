use ae_code_challange

/*

select * from ServerResponseLog_GetHourlyErrorCodeCounts



*/

if not exists(select 1 from sys.views where name = 'ServerResponseLog_GetHourlyErrorCodeCounts')
exec('CREATE VIEW ServerResponseLog_GetHourlyErrorCodeCounts [Time], ErrorCode, [Count])')
go

alter view ServerResponseLog_GetHourlyErrorCodeCounts ([Time], ErrorCode, [Count])
as

select 
	convert(varchar, StartTime, 23) + ' ' + convert(varchar, datepart(hour, StartTime)),
	ErrorCode, 
	count(ErrorCode)
from dbo.server_response_log
group by
	convert(varchar, StartTime, 23) + ' ' + convert(varchar, datepart(hour, StartTime)),
	ErrorCode

