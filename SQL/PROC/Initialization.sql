USE [EPAM_Project]
GO
/****** Object:  StoredProcedure [dbo].[Initialization]    Script Date: 11.06.2019 22:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Initialization]
@Login nvarchar(50),
@Password nvarchar(max),
@out int out
as
declare @out1 int
select @out1 =
(select Id from Contacts
where Contacts.[Login] = @Login and Contacts.[Password] = @Password)
select @out = @out1


