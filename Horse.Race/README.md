# Racing backgroundService

## 👋🏼 Getting Started

### Pre-requisite requirements

- [.NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)


## 🔨 Technologies

- [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
- [MediatR](https://github.com/jbogard/MediatR)
- [AutoMapper](https://automapper.org/)
- [XUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/), [NSubstitute](https://nsubstitute.github.io/)

### 🚧 Project Logic
I have used MediaTR - CQRS pattern - Command and Query Responsibility Segregation

The racing data is retrieved from third party API client - whose url should be set in Appsettings.json. 
All the the racing data is published to the Local DB - which can be accessed by other internal applications. 

I have added the Background Service(IHostedService) - which get the racing data from ThirdParty API 
and Update the local racing data if following conditions met otherwise insert a new race in if its new race.
The background service sleeps for 15 min and again start fetching and processing data.
   a. Track Condition update.
   b. Start Time Update.
   c. Runners removal.

### Note 
1. I have not added any API end point to get the published racing data - but have added the handler(GetPublishedRaceQueryHandler).
2. I have retrieved and updated the racing data in one handler but 
3. also I can think of - if we can use SQSQueue to publish the message into the SQSQueue when any update or insert happens.
  

### Project architecture

The project is based on ideas from [Clean Architecture Solution Template]

#### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

#### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application needs to access a new repository, a new interface would be added to application and an implementation would be created within infrastructure.

#### Infrastructure

This layer contains classes for accessing external resources such as databases, Redis, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

#### WebApi

This layer is a Web API .NET Core 6 project. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only _Program.cs_ should reference Infrastructure.

#### UnitTests

```
/tests/UnitTests/Infrastructure/Repositories/TheRepositoryTests.cs
/tests/UnitTests/Infrastructure/Repositories/Http/TheApiTests.cs
/tests/UnitTests/Application/TheHandlerTests.cs
```

## 💁 Who do I talk to?

- Sneha Upale