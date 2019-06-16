use EPAM_Project
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