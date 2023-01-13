# Getting started

To start project make sure that `NET6` is installed in your local.

- Clone this repo.
- Open `Contact.API` project AppSettings ; 
    * Set your Local PGAdmin Connection String to `DbContext`
    * When Project started, Oto migration will be applied and database will be created.
- Open `Report.API` project AppSettings ; 
    * Set your Local PGAdmin Connection String to `DbContext`
    * When Project started, Oto migration will be applied and database will be created.
    * You can set your `RabbitMQ` Path as well. You can use my RabbitMQ URL. It's free and available right now.
    * Make Sure That `APISettings.ContactAPI` Path is https://localhost:5001/api/ . Because, It's important to send request to `Contact.API`.
- Open `Report.WorkerService` project AppSettings ; 
    * Make Sure That `APISettings.ReportAPI` Path is https://localhost:5002/api/ . Because, It's important to send request to `Report.API`.
    * You can set your `RabbitMQ` Path as well. You can use my RabbitMQ URL. It's free and available right now.
- Open `Contact.UI` project AppSettings;
    * Make Sure That `APISettings.ContactAPI` Path is https://localhost:5001/api/ . Because, It's important to send request to `Contact.API`.
    * Make Sure That `APISettings.ReportAPI` Path is https://localhost:5002/api/ . Because, It's important to send request to `Report.API`.
- Right Click To Project Solution `Contactbook` ;
    * Click Properties 
    * Select Multiple Startup Projects.
    * Choose `Contact.API` Start
    * Choose `Contact.UI` Start
    * Choose `Report.API` Start
    * Choose `Report.WorkerService` Start

# Code Overview

## Nuget Packages

- [DocumentFormat.OpenXml](https://www.nuget.org/packages/DocumentFormat.OpenXml/) - Package for generate Excel.
- [Swashbuckle.AspNetCore](https://www.nuget.org/packages/Swashbuckle.AspNetCore) - Package for Swagger.
- [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore) - Package for ORM.
- [AutoMapper.Extensions.Microsoft.DependencyInjection](https://www.nuget.org/packages/AutoMapper.Extensions.Microsoft.DependencyInjection) - Package for Mapping Entities to View Models.
- [RabbitMQ.Client](https://www.nuget.org/packages/RabbitMQ.Client) - Package for Rabbit MQ.
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json) - Package for JSON Convert.

## Application Structure

- `CommonProject` is Shared Project for all other projects. This project contains ; BaseEntity,ExcelHelper,Extensions,HttpHelper,Response Object and ViewModels,
- `Services.Contact.Contact.API` is Contact Microservice. 
- `Services.Contact.Contact.Framework` is Contact Microservice manager. This layer contains ; ContactDbContext,Entities,MappingProfile,Migrations,Services and ContactServiceRegistration.
- `Services.Contact.Contact.Test` is Contact Microservice Test.
- `Services.Contact.Report.API` is Report Microservice.
- `Services.Contact.Report.Framework` is Report Microservice manager. This layer contains ; ReportDbContext,Entities,MappingProfile,Migrations,RabbitMQ Integration and Services. IContactHttpClientService helps to comminacate with `Contact Microservice`.
- `Services.Contact.Report.Test` is Report Microservice Test.
- `Report.WorkerService` is RabbitMQ Consumer.
- `Contact.UI` is view of the project.
- `Contact.UI.Framework` is Contact UI manager. In this project we can sent request to `Contact Microservice` and `Report Microservice`.


## Images From Project UI
![Contactbook](https://github.com/malikulle/Contactbook/blob/master/images/1.png?raw=true)
![Contactbook](https://github.com/malikulle/Contactbook/blob/master/images/2.png?raw=true)
![Contactbook](https://github.com/malikulle/Contactbook/blob/master/images/3.png?raw=true)
![Contactbook](https://github.com/malikulle/Contactbook/blob/master/images/4.png?raw=true)
![Contactbook](https://github.com/malikulle/Contactbook/blob/master/images/5.png?raw=true)