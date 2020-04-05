# Web API Template

_Set of definitions and protocols that will be use to develop and integrate Web API application software._

## Starting 🚀

_This document will allow you to understand the architecture of this project._

## Pre-requirements 📋

_To execute this project you will need the following packages installed in your computer:_

1. Visual Studio 2019
2. NET Core 3.1

## Arquitecture 

_This project is splitted in diferent layers using the Clean Architecture methodology described here (https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures). Here you can find an explanation of each layer:_

1. CleanArchitecture.WebAPI: It contains the API controllers. Don't add business logic here. API credentials and connection string database will be in the apisecuritysettings.json and connectionstrings.json files.
2. CleanArchitecture.Core: It contains the business logic: Entities, Services, DTOs, etc.
3. CleanArchitecture.Infrastructure.Data: It contains the data layer (Repositories).
4. CleanArchitecture.Infrastructure.Migrations: It contains the nuget package Microsoft.EntityFrameworkCore.Tools necessary to be able to execute the EF CodeFirst commands like Add-Migrations, Update-Database, etc.
5. CleanArchitecture.Infrastructure.DependencyBuilder: It contains all the dependecy injections. If you add a new service or repository class, you will need to add the injection here.

## Good practices

If you are going to add some code to this project, it's very important that you follow these rules:

1. Apply the SOLID principles: SRP, OCP, LSP, ISP and DIP.
2. Leblanc’s law: Later equals never. If you see a mess, fix it.
3. The Boy Scout rule: “Leave the campground cleaner than you found it”.
4. The Stepdown rule: Each function introduces the next.
5. The Einstein rule: Make it as simple as possible but not simpler.
6. Names mean one thing and only one thing.
7. Single level of abstraction. One method, one thing.
8. Comments are to compensate our failure to express ourself in code.
9. Demeter's law: Do not accept candies from strangers
10. Murphy's law: If something can go wrong, it will. (Be pessimistic)

## Entity Framework Code First Migrations

_To be able to execute the EF commands you will need to follow these steps:_

1. Select project CleanArchitecture.Infrastructure.Migrations as startup project in solution explorer.
2. Open console packet manager (PM) and select Website.Infrastructure.Data as default project.
3. Add-Migration <name_migration>
4. Update-Database

## Testing ⚙️

_TODO. In this section we'll explain how to execute the Unit Tests defined in this project._

## Deployment 📦

_TODO. Pending to add information._

## Authors ✒️

* **Oscar Rodríguez** - *Responsible for define the project arhitecture. https://www.linkedin.com/in/oscar-rodriguez-lopez-70b2a337/* 

