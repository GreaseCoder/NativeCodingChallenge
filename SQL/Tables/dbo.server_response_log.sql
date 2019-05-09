use ae_code_challange

drop table dbo.server_response_log

create table dbo.server_response_log
(
	LogID uniqueidentifier constraint [DF_server_response_log_LogID] default (newsequentialid()) rowguidcol not null,
	StartTime datetime2 not null,
	EndTime datetime2 not null,
	HTTPStatusCode int null,
	ResponseText varchar(255) collate SQL_Latin1_General_CP1_CI_AS null,
	ErrorCode int not null,
	InsertDate datetime2 default GetUTCDate(),
	constraint [pk__server_response_log__LogID] primary key clustered (LogID asc)
)
go

create nonclustered index nix__server_response_log__StartTime_ResponseText 
on dbo.server_response_log (StartTime) include (ResponseText)
go


