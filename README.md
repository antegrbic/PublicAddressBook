# PublicAddressBook

## Short application architecture
Developed using ASP .NET Core MVC (I think the project layout is pretty self-explanatory but here are a few notes):
- basic interfaces and domain model are storedDomain folder
- Service folder contains ContactService class
- Persistence contains AppDbContext and ContactRepository classes
- for test purposes added only in-memory database

## TO-DO
- GUI for adding/editing/deleting 
- SignalR 
- error logging

## Unit tests: 
Only added basic cases for domain model classs - contact and address.

## Business logic

Contact is unique for the combination of name and Address. 
When a contact is updated all of the previous telephone numbers are overwritten (aka deleted) with the new ones.
Phone numbers must be in format "XXX-XXX-XXXX" (just added to have some validation).

## API call

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

