USE [EPAM_Project]
GO
/****** Object:  StoredProcedure [dbo].[AddAd]    Script Date: 11.06.2019 21:12:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[AddAd] 
@Ad nvarchar(100),
@Title nvarchar(max),
@DataCreation datetime,
@Picture nvarchar(max),
@Category int,
@Adress nvarchar(max),
@Type int,
@State int,
@Login nvarchar(50),
@Password nvarchar(max)
as
declare @Contact int
EXECUTE [dbo].[Initialization]  @Login,@Password,@Contact out
INSERT INTO [dbo].[Ads]
           ([Ad]
           ,[Title]
           ,[DataCreation]
           ,[Picture]
           ,[Category]
           ,[Contact]
           ,[Adress]
           ,[Type]
           ,[State])
     VALUES
           (@Ad
           ,@Title
           ,@DataCreation
           ,@Picture
           ,@Category
           ,@Contact
           ,@Adress
           ,@Type
           ,@State)