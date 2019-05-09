use ae_code_challange

drop table dbo.map_error_to_description

create table dbo.map_error_to_description
(
	ErrorCode int primary key not null,
	HTTPStatusCode int not null,
	Description varchar(255) null
)
go

insert into dbo.map_error_to_description (ErrorCode, HTTPStatusCode, Description) values
(1, 200, '200 - Success'),
(2, 418, 'I am a teapot'),
(-999, 408, 'Timeout')
go



