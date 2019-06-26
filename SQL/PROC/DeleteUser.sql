use EPAM_Project
go
create proc DeleteUser
@Login nvarchar(50),
@Password nvarchar(max),
@UserID int
as
if (select COUNT(*) from Users where users.Login = @Login and Users.[Password]= @Password and Users.[Status] = 3) = 1
begin
DELETE FROM [dbo].[Users]
      WHERE Users.Id = @UserID
end