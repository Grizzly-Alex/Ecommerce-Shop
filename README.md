# Ecommerce Shop (client-server application based on microservice architecture)

### Description
Developing a project using the .NET 8 and ASP.NET Core framework. The application has a microservice architecture.
Why did I choose microservices for my project? Firstly, for educational purposes, and secondly, such architecture will allow me to easily expand the functionality of the application.
For each microservice will have its own architecture and its own database type. The services will use databases such as relational databases (PostgreSQL, Sql Server) and NoSQL(Redis, DocumentDb).
Implementing interaction between services via RabbitMQ message broker and using the Yarp API Gateway.

### Application Diagram
![EcommerceShop (Microservices)](https://github.com/user-attachments/assets/4a4138a6-8baa-44d9-9cef-b04e4540aaab)


## Catalog Microservice

### Description
This service is responsible for management products which store in the database.
These are the ordinary CRUD operations: 
 - update
 - delete
 - create
 - get all products with paged list
 - get by id
 - get by category

Microservice has got Vertical Slice Architecture with CQRS patern. 
To implementation the CQRS pattern I used a MediatR. This ensures a low coupling to the endpoints api.
Low code coupling is also ensured by using the IPipelineBehavior generic interface for validations and logging.

