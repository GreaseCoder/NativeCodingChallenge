use ae_code_challange

drop table dbo.server_response_log

create table dbo.server_response_log
(
	LogID uniqueidentifier constraint [DF_server_response_log_LogID] default (newsequentialid()) rowguidcol not null,
	StartTime datetime2 not null,
	EndTime datetime2 not null,
	HTTPStatusCode int not null,
	ResponseText varchar(max) collate SQL_Latin1_General_CP1_CI_AS null,
	ErrorCode as 
		case HTTPStatusCode
			when 200 then 1
			when 408 then -999
			else 2
		end,
	InsertDateUTC datetime2 default GetUTCDate(),
	constraint [pk__server_response_log__LogID] primary key clustered (LogID asc),
)
go

create nonclustered index nix__server_response_log__StartTime_ResponseText 
on dbo.server_response_log (StartTime) include (ResponseText)
go


