CREATE proc [dbo].[GetUserAds]
@Login nvarchar(50),
@Password nvarchar(max)
as
declare @Contact int
EXECUTE [dbo].[Initialization]  @Login,@Password,@Contact out
select 
 Ads.Id
,Ad
,Title
,DataCreation
,Picture
,Categories.Category
,Contacts.Name
,Adress
,[Types].[Type]
,States.[State]
 from Ads
	inner join Categories on Categories.Id = Ads.Category
	inner join Contacts on Contacts.Id = Ads.Contact
	inner join States on States.Id = Ads.[State]
	inner join [Types] on [Types].Id = Ads.[Type]
	where ads.Contact = @Contact