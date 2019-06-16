USe EPAM_Project
go
Create proc AddContact
@Name NVARCHAR(100),
@Login NVARCHAR(50),
@Password NVARCHAR(max)
as

INSERT INTO [dbo].[Contacts]
           ([Name]
		   ,[Login]
		   ,[Password])
     VALUES
           (@Name
		   ,@Login
		   ,@Password)
