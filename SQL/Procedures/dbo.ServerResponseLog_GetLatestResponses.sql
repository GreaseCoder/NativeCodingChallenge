use ae_code_challange

/*

exec ae_code_challange.dbo.ServerResponseLog_GetLatestResponses
		@StartTime = '',
		@EndTime = ''


*/

if not exists(select 1 from sysobjects where id = object_id(N'[dbo].[ServerResponseLog_GetLatestResponses]') and objectproperty(id, N'IsProcedure') = 1)
exec('CREATE PROCEDURE [dbo].[ServerResponseLog_GetLatestResponses] AS BEGIN SET NOCOUNT ON; END')
go

alter procedure dbo.ServerResponseLog_GetLatestResponses
	@StartTime datetime2,
	@EndTime datetime2
as
begin

	select top 5 *
	from dbo.server_response_log with (nolock)
	where StartTime between @startTime and @endTime
	order by StartTime desc

end