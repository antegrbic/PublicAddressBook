# PublicAddressBook

## Short application architecture
Developed using ASP .NET Core MVC (I think the project layout is pretty self-explanatory but here are a few notes):
- Basic interfaces and domain model are stored in Domain folder
- Service folder contains ContactService class
- Persistence folder contains AppDbContext and ContactRepository classes
- For test purposes added only in-memory database
- live updates on client side (provided with SignalR)
- simple GUI with pagination (Razor pages) - view data only

## TO-DO
- GUI for adding/editing/deleting Contacts
- error logging

## Unit tests: 
Only added basic cases for domain model classs - contact and address.

## Business logic

Contact is unique for the combination of name and Address. 
When a contact is updated all of the previous telephone numbers are overwritten (aka deleted) with the new ones.
Phone numbers must be in format "XXX-XXX-XXXX" (just added to have some validation).

## API call example

{
  "Id": "1",
  "Name": "Ante",
  "DateOfBirth": "2000-02-01",
  "StreetName": "Ilica",
  "StreetNumber": "1",
  "City": "Zagreb",
  "Country": "HRV",
  "PhoneNumbers": [
	"222-555-6789"
  	]
}

