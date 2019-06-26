use EPAM_Project
go
create proc AddPhone
@Login nvarchar(50),
@Password nvarchar(max),
@Phone nvarchar(20)
as
declare @Contact int
EXECUTE [dbo].[Initialization]  @Login,@Password,@Contact out
if not @Contact is null 
begin

INSERT INTO [dbo].[Phones] ([Phone]) VALUES (@Phone)

INSERT INTO [dbo].[ContactsPhones]
           ([Contact]
           ,[Phone])
     VALUES
           (@Contact
           ,SCOPE_IDENTITY())
end
