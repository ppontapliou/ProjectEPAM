Create proc [dbo].[UpdateAd]
	@Ad nvarchar(100),
	@Title nvarchar(max),
	@DataCreation datetime,
	@Picture nvarchar(max),
	@Category int,
	@Adress nvarchar(max),
	@Type int,
	@State int,
	@Login nvarchar(50),
	@Password nvarchar(max),
	@IdAd int
as
	declare @user int
	exec Initialization @Login,@Password, @user out
	declare @UserStatus int
	select @UserStatus  = (select [Status] from Users where Users.[Id] = @user)
	if (select count(*) from Ads where ads.Contact = @user and ads.Id = @IdAd) = 1
	UPDATE [dbo].[Ads]
	   SET [Ad] = @Ad
	      ,[Title] = @Title
	      ,[DataCreation] = @DataCreation
	      ,[Picture] = @Picture
	      ,[Category] = @Category
	      
	      ,[Adress] = @Adress
	      ,[Type] = @Type
	      ,[State] = @State
	 WHERE (ads.Contact = @user or (@UserStatus = 3 or @UserStatus = 2)) and Ads.Id = @IdAd
