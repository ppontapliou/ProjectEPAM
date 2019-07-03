use EPAM_Project
go
create proc GetPartAd
@firstElement int
as
WITH NthRowCTE AS
(
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
	,Contacts.Id as 'ContactId'
	,ROW_NUMBER() OVER (ORDER BY Ads.Id ) AS RNum
	 from Ads
		inner join Categories on Categories.Id = Ads.Category
		inner join Contacts on Contacts.Id = Ads.Contact
		inner join States on States.Id = Ads.[State]
		inner join [Types] on [Types].Id = Ads.[Type]
)
SELECT 
	 NthRowCTE.Id 
	,Title
	,DataCreation
	,Picture
	,NthRowCTE.Category
	,NthRowCTE.Name
	,Adress
	,NthRowCTE.[Type]
	,NthRowCTE.[State]
	,NthRowCTE.ContactId 
	FROM NthRowCTE WHERE RNum between @firstElement and @firstElement+5