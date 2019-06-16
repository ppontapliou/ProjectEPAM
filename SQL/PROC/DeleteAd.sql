USE [EPAM_Project]
GO
alter proc DeleteAd
@Login nvarchar(50),
@Password nvarchar(max),
@IdAd int
as
declare @user int
exec Initialization @Login,@Password, @user out
if (select count(*) from Ads where ads.Contact = @user and ads.Id = @IdAd) = 1
DELETE FROM [dbo].[Ads]
      WHERE ads.Ad = @IdAd
GO


