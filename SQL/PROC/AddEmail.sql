use EPAM_Project
go
create proc AddEmail
@Login nvarchar(50),
@Password nvarchar(max),
@Email nvarchar(20)
as
declare @Contact int
EXECUTE [dbo].[Initialization]  @Login,@Password,@Contact out
if not @Contact is null 
begin

INSERT INTO [dbo].[Emails] ([Email]) VALUES (@Email)

INSERT INTO [dbo].[ContactsEmails]
           ([Contact]
           ,[Email])
     VALUES
           (@Contact
           ,SCOPE_IDENTITY())
end
