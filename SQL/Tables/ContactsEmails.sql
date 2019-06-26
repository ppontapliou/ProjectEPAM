use EPAM_Project
go
create table ContactsEmails(
	Id int PRIMARY KEY IDENTITY(1, 1),
	Contact int NOT NULL FOREIGN KEY REFERENCES Contacts(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	Email int NOT NULL FOREIGN KEY REFERENCES Emails(Id) ON DELETE CASCADE ON UPDATE CASCADE
)