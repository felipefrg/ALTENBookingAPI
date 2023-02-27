# ALTENBookingAPI!

Web API project to demostrate how to book a room

This project was created using Visual Studio 2022 and .NET 6 
To store data it is using in memory database with EntityFrameworkCore.InMemory 
**ATTENTION** all data will be lost when the project stops running

# Project structure
The project follows the best practices of clean architecture, which involves separating it into layers to facilitate maintenance and comprehension.

Project/ 
├── src/ 
│├── ALTENBooking.Application/ 
│├── ALTENBooking.Domain/ 
│├── ALTENBooking.Data/ 
│├── ALTENBooking.API/ 
│
├── ALTENBooking.Tests/ 

## Tests

It is also using a TDD (Test Driven Development) approach to ensure that all domain logical rules meet the requirements.
![Domain Tests](https://github.com/felipefrg/ALTENBookingAPI/blob/main/doc/TestResults.png?raw=true)

## Get Bookings
![Get](https://github.com/felipefrg/ALTENBookingAPI/blob/main/doc/GetBookings.png?raw=true)


## Create Booking
Creating a book in a available date.

[**ATTENTION**] please note that for testing purpose there is only one room available with the ID "**61f3e658-b377-4dc5-96b1-ad64ef2e03ae**"

![Create](https://github.com/felipefrg/ALTENBookingAPI/blob/main/doc/CreateBooking.png?raw=true)

## Update Booking
Update an existing booking 
![Update](https://github.com/felipefrg/ALTENBookingAPI/blob/main/doc/UpdateBooking.png?raw=true)

## Cancel Booking
Cancel an existing booking just pass the **customerId** as parameter
![Cancel](https://github.com/felipefrg/ALTENBookingAPI/blob/main/doc/CancelBooking.png?raw=true)

## Check Period
To check if a period has room available just pass a start and end date
![enter image description here](https://github.com/felipefrg/ALTENBookingAPI/blob/main/doc/GetPeriodStatus.png?raw=true)


