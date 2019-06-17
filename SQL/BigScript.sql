CREATE DATABASE EPAM_Project
go
use EPAM_Project
go
--Table
create table [Types](
	Id int PRIMARY KEY IDENTITY(1, 1),
	[Type] Nvarchar(20) not null
)
create table Phones(
	Id int PRIMARY KEY IDENTITY(1, 1),
	Phone nvarchar(20) not null
)
create table Emails(
	Id int PRIMARY KEY IDENTITY(1, 1),
	Email nvarchar(100) not null
)
create TABLE Categories(
	Id int PRIMARY KEY IDENTITY(1, 1),
	Category nvarchar(100) not null
)
create table [States](
	Id int PRIMARY KEY IDENTITY(1, 1),
	[State] Nvarchar(20) not null
)
create table Statuses(
	Id int PRIMARY KEY IDENTITY(1, 1),
	[Status] nvarchar(100) not null
)
create table Users(
	Id int PRIMARY KEY IDENTITY(1, 1),
	[Status] int not null FOREIGN KEY REFERENCES Statuses(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[Login] nvarchar(50) not null unique,
	[Password] nvarchar(max) not null,
)
create table Contacts(
	Id int PRIMARY KEY FOREIGN KEY REFERENCES Users(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	Name nvarchar(100) not null,
)
create table ContactsEmails(
	Id int PRIMARY KEY IDENTITY(1, 1),
	Contact int NOT NULL FOREIGN KEY REFERENCES Contacts(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	Email int NOT NULL FOREIGN KEY REFERENCES Emails(Id) ON DELETE CASCADE ON UPDATE CASCADE
)
create table ContactsPhones(
	Id int PRIMARY KEY IDENTITY(1, 1),
	Contact int NOT NULL FOREIGN KEY REFERENCES Contacts(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	Phone int NOT NULL FOREIGN KEY REFERENCES Phones(Id) ON DELETE CASCADE ON UPDATE CASCADE
)
create table Ads(
	Id int PRIMARY KEY IDENTITY(1,1),
	Ad nvarchar(100) not null,
	Title nvarchar(max) not null,
	DataCreation datetime not null,
	Picture nvarchar(max) not null,
	Category int NOT NULL FOREIGN KEY REFERENCES Categories(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	Contact int NOT NULL FOREIGN KEY REFERENCES Contacts(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	Adress nvarchar(max) not null,
	[Type] int NOT NULL FOREIGN KEY REFERENCES [Types](Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[State] int NOT NULL FOREIGN KEY REFERENCES [States](Id) ON DELETE CASCADE ON UPDATE CASCADE
)
--base script
go
CREATE proc [dbo].[AddAd] 
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
go
CREATE proc [dbo].[Initialization]
@Login nvarchar(50),
@Password nvarchar(max),
@out int out
as
declare @out1 int
select @out1 =
(select Contacts.Id from Contacts inner join Users
					on Users.Id = Contacts.Id
where Users.[Login] = @Login and Users.[Password] = @Password)
select @out = @out1
go

CREATE proc [dbo].[AddContact]
@Name NVARCHAR(100),
@Login NVARCHAR(50),
@Password NVARCHAR(max),
@Status int
as
insert into Users 
		   ([Login]
		   ,[Password]
		   ,[Status])
	 values
		   (@Login
		   ,@Password
		   ,@Status)

INSERT INTO [dbo].[Contacts]
           ([Name]
		   ,[Id])
     VALUES
           (@Name
		   ,SCOPE_IDENTITY())
go
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
,Contacts.Id
 from Ads
	inner join Categories on Categories.Id = Ads.Category
	inner join Contacts on Contacts.Id = Ads.Contact
	inner join States on States.Id = Ads.[State]
	inner join [Types] on [Types].Id = Ads.[Type]
	where @id=ads.Id
go

CREATE proc [dbo].[GetAds]
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
,Contacts.Id
 from Ads
	inner join Categories on Categories.Id = Ads.Category
	inner join Contacts on Contacts.Id = Ads.Contact
	inner join States on States.Id = Ads.[State]
	inner join [Types] on [Types].Id = Ads.[Type]
go

create proc AddEmail
@Login nvarchar(50),
@Password nvarchar(max),
@Email nvarchar(20)
as
declare @Contact int
EXECUTE [dbo].[Initialization]  @Login,@Password,@Contact out
if not @Contact is null 
begin

INSERT INTO [dbo].[Emails] ([Email]) VALUES (@Email)

INSERT INTO [dbo].[ContactsEmails]
           ([Contact]
           ,[Email])
     VALUES
           (@Contact
           ,SCOPE_IDENTITY())
end

go

create proc AddPhone
@Login nvarchar(50),
@Password nvarchar(max),
@Phone nvarchar(20)
as
declare @Contact int
EXECUTE [dbo].[Initialization]  @Login,@Password,@Contact out
if not @Contact is null 
begin

INSERT INTO [dbo].[Phones] ([Phone]) VALUES (@Phone)

INSERT INTO [dbo].[ContactsPhones]
           ([Contact]
           ,[Phone])
     VALUES
           (@Contact
           ,SCOPE_IDENTITY())
end

go

CREATE proc DeleteAd
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

go

CREATE proc [dbo].[UpdateAd]
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
	 WHERE ads.Contact = @user and Ads.Id = @IdAd
go

create proc [Authentication]
@Login nvarchar(50),
@Password nvarchar(max)
as
select Name from Contacts inner join Users
					on Contacts.Id = Users.Id
	where [Login] = @Login and [Password] = @Password
go

create proc ChangeUserName
@Login nvarchar(50),
@Password nvarchar(max),
@Name nvarchar(100)
as
declare @Contact int
select @Contact = (select Id from Users where @Login = Users.[Login] and Users.[Password] = @Password)
UPDATE [dbo].[Contacts]
   SET [Name] = @Name      
 WHERE @Contact = Contacts.Id

 go

 create proc GetPhones
@Id int
as
select Phones.Phone from Contacts
	inner join ContactsPhones 
		on Contacts.Id = ContactsPhones.Contact
		inner join Phones
			on ContactsPhones.Phone = Phones.Id
	where Contacts.Id = @Id
go

create proc GetMails
@Id int
as
select Emails.Email from Contacts
	inner join ContactsEmails 
		on Contacts.Id = ContactsEmails.Contact
		inner join Emails
			on ContactsEmails.Email = Emails.Id
	where Contacts.Id = @Id
go
create proc DeleteUser
@Login nvarchar(50),
@Password nvarchar(max),
@UserID int
as
if (select COUNT(*) from Users where users.Login = @Login and Users.[Password]= @Password and Users.[Status] = 3) = 1
begin
DELETE FROM [dbo].[Users]
      WHERE Users.Id = @UserID
end
go
--initializing data
INSERT INTO [dbo].[Statuses] ([Status]) VALUES ('User')
INSERT INTO [dbo].[Statuses] ([Status]) VALUES ('Editor')
INSERT INTO [dbo].[Statuses] ([Status]) VALUES ('Admin')



EXECUTE [dbo].[AddContact] 'Илья Михайлович','log1','password1',1
EXECUTE [dbo].[AddContact] 'Владимир Стипанович','log2','password1',1
EXECUTE [dbo].[AddContact] 'Константин Иванов','log3','password1',1
EXECUTE [dbo].[AddContact] 'Павел','log4','password1',1
EXECUTE [dbo].[AddContact] 'Татьяна','log5','password1',1
EXECUTE [dbo].[AddContact] 'Дмитрий','log6','password1',1
EXECUTE [dbo].[AddContact] 'Матвей','log7','password1',1

INSERT INTO [dbo].[Types] ([Type]) VALUES ('Товар')
INSERT INTO [dbo].[Types] ([Type]) VALUES ('Услуга')
INSERT INTO [dbo].[States] ([State]) VALUES ('Новое')
INSERT INTO [dbo].[States] ([State]) VALUES ('БУ')
INSERT INTO [dbo].[States] ([State]) VALUES ('none')
INSERT INTO [dbo].[Categories] ([Category]) VALUES ('Животные')
INSERT INTO [dbo].[Categories] ([Category]) VALUES ('Учеба')
INSERT INTO [dbo].[Categories] ([Category]) VALUES ('Развлечение')
INSERT INTO [dbo].[Categories] ([Category]) VALUES ('Технологии')
INSERT INTO [dbo].[Categories] ([Category]) VALUES ('Дом')

EXECUTE [dbo].[AddAd] 
   'Ремонт ПК'
  ,'Качественный, не дорогой ремонт ПК в умеренную цену в г.Млгилёве и не только'
  ,'12.03.2018'
  ,'https://innovation-center.com.ua/media/k2/items/cache/3d10c9c21f6f61faeefd4aa27b190def_M.jpg'
  , 4 
  
  ,'Беларусь, Могилёв, Ленинская 81.'
  ,2
  ,3
  ,'log5'
  ,'password1'
EXECUTE [dbo].[AddAd] 
   'Продаю мотоблок'
  ,'Я не знаю, как его описать'
  ,'15.03.2019'
  ,'https://avatars.mds.yandex.net/get-mpic/466729/img_id4377111082610195283.jpeg/9hq'
  , 1 
  
  ,'Беларусь, Бобруйск, Заграничная 21'
  ,1
  ,1
  ,'log1'
  ,'password1'
EXECUTE [dbo].[AddAd] 
   'Продам щенка Овчарки'
  ,'Красивый, пушистый, добрый'
  ,'12.05.2018'
  ,'https://st.depositphotos.com/1004199/3250/i/450/depositphotos_32500959-stock-photo-german-shepherd-puppy.jpg'
  , 1 
  
  ,'Беларусь, Могилёв, Ленинская 81.'
  ,1
  ,3
  ,'log2'
  ,'password1'
EXECUTE [dbo].[AddAd] 
   'Монтаж Окон'
  ,'Что закажете, то и сделаем'
  ,'10.03.2018'
  ,'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQtV0G0qo2Yez05H0Oxj8bWGn6dvJtuDI1LRJ2gAYg5g8dyvEwPlg'
  , 5 

  ,'Беларусь, Могилёв, Ленинская 81.'
  ,2
  ,3
  ,'log3'
  ,'password1'
EXECUTE [dbo].[AddAd] 
   'Поднятие статистики'
  ,'Подниму вашу стату в любых играх со дна и ниже!'
  ,'12.03.2017'
  ,'https://htstatic.imgsmail.ru/pic_image/c9a95256c32c8422d4ae31a84d9fbe11/840/472/1261775/'
  , 3 

  ,'Беларусь, Могилёв, Ленинская 81.'
  ,2
  ,3
  ,'log4'
  ,'password1'
EXECUTE [dbo].[AddAd] 
   'Репетиторство'
  ,'Обучение шклольков любых классов'
  ,'12.03.2018'
  ,'https://rusvesna.su/sites/default/files/styles/orign_wm/public/minobrazovaniya_ukraina_uchitelnica.jpg'
  , 2 

  ,'Беларусь, Шклов, Ленинская 81.'
  ,2
  ,3
  ,'log6'
  ,'password1'
EXECUTE [dbo].[AddAd] 
   'Шикарный кот'
  ,'Просто красивый кот'
  ,'30.05.2019'
  ,'http://s.mediasole.ru/images/75/75506/d89e2a91d9932fef64ac532f756cbd06.jpg'
  , 5 

  ,'Никуда идти не нужно'
  ,2
  ,3
  ,'log7'
  ,'password1'
EXECUTE [dbo].[AddAd] 
   'Британские котята'
  ,'Возраст 2 мечяца, мальчик и девочка, родословная, приучены к лотку.'
  ,'12.03.2018'
  ,'http://www.bri-cats.ru/15/dvoe.jpg'
  , 1 

  ,'Беларусь, Могилёв'
  ,1
  ,3
  ,'log5'
  ,'password1'
