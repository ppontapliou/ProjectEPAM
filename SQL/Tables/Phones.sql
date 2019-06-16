use EPAM_Project
go
create table Phones(
	Id int PRIMARY KEY IDENTITY(1, 1),
	Phone nvarchar(20) not null
)