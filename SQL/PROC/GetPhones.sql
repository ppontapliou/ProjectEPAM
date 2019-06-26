use EPAM_Project
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