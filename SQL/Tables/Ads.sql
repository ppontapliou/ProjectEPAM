use EPAM_Project
go
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