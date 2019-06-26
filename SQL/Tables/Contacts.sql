use EPAM_Project
go
create table Contacts(
	Id int PRIMARY KEY IDENTITY(1, 1),
	Name nvarchar(100) not null,
	[Login] nvarchar(50) not null,
	[Password] nvarchar(max) not null,
)