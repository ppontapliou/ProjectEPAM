USE [EPAM_Project]
GO
/****** Object:  StoredProcedure [dbo].[Initialization]    Script Date: 11.06.2019 20:11:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Initialization]
@Login nvarchar(50),
@Password nvarchar(max),
@out int out
as
declare @oout int
select @oout =
(select Id from Contacts
where Contacts.[Login] = @Login and Contacts.[Password] = @Password)
select @oout =1
select Contacts.Id as [result] from Contacts
where Contacts.[Login] = @Login and Contacts.[Password] = @Password

