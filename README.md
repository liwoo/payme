#PayMe

PayMe is a Poor Malawian Man's Solution to a Mobile Money Payment Solution. Basically, it receives forwarded messages from dedicated phone(s) and processes the SMSs to extract relevant Payment Notification details.

We only save the details of the SMS if we can identify it as a Mpamba-Mpamba or Airtel Money (AM) to AM Payment Notification SMS.

## Stack
This is a .NET Core Project using the Onion (Clean) Architecture, namely:

### Api
This is a Project with depenancies on all the other layers of the project. It mainly handles API Calls and provides tooling such as 

- HealthChecks
- API Docs
- API Authorization
- etc
  
### Application
The application Layer is responsible for making **Commands** and **Queries** following the CQRS Pattern.  It also defines **Behaviours** to this Command/Query Pipeline as well as other Application Related stuff like **DTOs** and all of that good stuff.

### Core
The Entities, Validation Classes and other Domain related Stuff live here.

### Infrastucture
Mostly Entity Framework!

## Dependancies

## Installation & Setup

## Contributing

