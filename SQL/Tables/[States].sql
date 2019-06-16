use EPAM_Project
go
create table [States](
	Id int PRIMARY KEY IDENTITY(1, 1),
	[State] Nvarchar(20) not null
)