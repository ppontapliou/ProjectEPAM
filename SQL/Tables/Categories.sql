use EPAM_Project
go
create TABLE Categories(
	Id int PRIMARY KEY IDENTITY(1, 1),
	Category nvarchar(100) not null
)