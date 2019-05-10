use ae_code_challange


/*
	Setup
*/

insert into dbo.server_response_log (StartTime, EndTime, HTTPStatusCode, ResponseText) values
('5000-05-01 12:00', '2019-05-01 12:35', 200, 'Test Entry 001'),
('5000-05-01 12:00', '2019-05-01 12:35', 200, 'Test Entry 002'),
('5000-05-01 13:00', '2019-05-01 13:35', 100, 'Test Entry 003'),
('5000-05-01 13:30', '2019-05-01 13:35', 408, 'Test Entry 004'),
('5000-05-01 14:00', '2019-05-01 14:35', 200, 'Test Entry 005')

declare @StatusTestTable table
(
	StartTime datetime2 not null,
	EndTime datetime2 not null,
	HTTPStatusCode int not null,
	ErrorCode int not null,
	ResponseText varchar(max)
)

insert into @StatusTestTable (StartTime, EndTime, HTTPStatusCode, ErrorCode, ResponseText)
select
	StartTime,
	EndTime,
	HTTPStatusCode,
	ErrorCode,
	ResponseText
from dbo.server_response_log
where ResponseText in ('Test Entry 001', 'Test Entry 002', 'Test Entry 003', 'Test Entry 004', 'Test Entry 005')


declare @ViewGroupingTestTable table
(
	LogDateHour datetime not null,
	ErrorCode int not null,
	ErrorCount int not null
)

insert into @ViewGroupingTestTable (LogDateHour, ErrorCode, ErrorCount)
select
	Time,
	ErrorCode,
	Count
from ServerResponseLog_GetHourlyErrorCodeCounts
where datepart(year, Time) = '5000'

/*
	Testing
*/

select *
from @StatusTestTable

-- HTTP 200 should be ErrorCode 1
if exists (select 1 from @StatusTestTable where ResponseText = 'Test Entry 001' and ErrorCode <> 1)
raiserror('ErrorCode set incorrectly - Should have been 1 for HTTP 200', 16, 1)

-- HTTP 100 should be ErrorCode 2
if exists (select 1 from @StatusTestTable where ResponseText = 'Test Entry 003' and ErrorCode <> 2)
raiserror('ErrorCode set incorrectly - Should have been 2 for HTTP 100', 16, 1)

-- HTTP 408 should be ErrorCode -999
if exists (select 1 from @StatusTestTable where ResponseText = 'Test Entry 004' and ErrorCode <> -999)
raiserror('ErrorCode set incorrectly - Should have been -999 for HTTP 200', 16, 1)


select *
from @ViewGroupingTestTable

-- Should be 1 for timeout
if exists (select 1 from @ViewGroupingTestTable where LogDateHour = '5000-05-01 13:00:00.000' and ErrorCode <> -999 and ErrorCount <> 1)
raiserror('Incorrect count for ErrorCode -999.  Should be 1.', 16, 1)

-- Should be 2 at 12:00 for ErrorCode 1
if exists (select 1 from @ViewGroupingTestTable where LogDateHour = '5000-05-01 12:00:00.000' and ErrorCode <> 1 and ErrorCount <> 2)
raiserror('Incorrect count for ErrorCode 1 at 12:00.  Should be 2.', 16, 1)

-- Should be 2 at 14:00 for ErrorCode 1
if exists (select 1 from @ViewGroupingTestTable where LogDateHour = '5000-05-01 14:00:00.000' and ErrorCode <> 1 and ErrorCount <> 2)
raiserror('Incorrect count for ErrorCode 1 at 14:00.  Should be 2.', 16, 1)

-- Should be 1 for ErrorCode 2
if exists (select 1 from @ViewGroupingTestTable where LogDateHour = '5000-05-01 13:00:00.000' and ErrorCode <> 2 and ErrorCount <> 1)
raiserror('Incorrect count for ErrorCode 2.  Should be 2.', 16, 1)


/*
	Cleanup
*/

delete from dbo.server_response_log
where ResponseText in ('Test Entry 001', 'Test Entry 002', 'Test Entry 003', 'Test Entry 004')




