use EPAM_Project
go
create table [Types](
	Id int PRIMARY KEY IDENTITY(1, 1),
	[Type] Nvarchar(20) not null
)