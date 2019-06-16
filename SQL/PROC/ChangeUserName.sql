use EPAM_Project
go
create proc ChangeUserName
@Login nvarchar(50),
@Password nvarchar(max),
@Name nvarchar(100)
as
UPDATE [dbo].[Contacts]
   SET [Name] = @Name      
 WHERE @Login = Contacts.[Login] and Contacts.[Password] = @Password