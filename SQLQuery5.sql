USE [EPAM_Project]
GO

DECLARE @RC int
DECLARE @Login nvarchar(50)
DECLARE @Password nvarchar(max)
declare @out int
EXECUTE @RC = [dbo].[Initialization]  'log1','password1',@out
select @out AS 'Company Name' ; 
   
GO


