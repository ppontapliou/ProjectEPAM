use EPAM_Project
go
create table Emails(
	Id int PRIMARY KEY IDENTITY(1, 1),
	Email nvarchar(100) not null
)