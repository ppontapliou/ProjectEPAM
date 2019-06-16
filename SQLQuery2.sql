USE EPAM_Project
Go
alter proc Initialization
@Login nvarchar(50),
@Password nvarchar(max)
as
select 'ok' as [result] from Contacts
where Contacts.[Login] = @Login and Contacts.[Password] = @Password