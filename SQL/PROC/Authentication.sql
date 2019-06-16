use EPAM_Project
go
create proc [Authentication]
@Login nvarchar(50),
@Password nvarchar(max)
as
select Name from Contacts
	where [Login] = @Login and [Password] = @Password