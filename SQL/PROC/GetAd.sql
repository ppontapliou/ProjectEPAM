CREATE proc [dbo].[GetAd]
@id int
as
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
	where @id=ads.Id