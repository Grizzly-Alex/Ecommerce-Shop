# Ecommerce Shop (client-server application based on microservice architecture)

### Description

Developing a project using the .NET 8 and ASP.NET Core framework. The application has a microservice architecture.
Why did I choose microservices for my project? Firstly, for educational purposes, and secondly, such architecture will allow me to easily expand the functionality of the application.
For each microservice will have its own architecture and its own database type. The services will use databases such as relational databases (PostgreSQL, Sql Server) and NoSQL(Redis, DocumentDb).
Implementing interaction between services via RabbitMQ message broker and using the Yarp API Gateway.

### Ports
| Microservices | Local Environment  | Docker Environment  | Docker Inside  |
| :-------------|:------------------:| :------------------:|:--------------:|
| Catalog       | 5000 - 5050        | 6000                | 8080 - 8081    |
| Basket        | 5001 - 5051        | 6001                | 8080 - 8081    |
| Diskount      | 5002 - 5052        | 6002                | 8080 - 8081    |
| Ordering      | 5003 - 5053        | 6003                | 8080 - 8081    |

### Application Diagram
![EcommerceShop (Microservices)](https://github.com/user-attachments/assets/4a4138a6-8baa-44d9-9cef-b04e4540aaab)

# Catalog Microservice

This service is responsible for management products which store in the database. 
These are the ordinary CRUD operations. Microservice works on http/https protocols with using REST architecture.

### Ports
| Microservices | Local Environment  | Docker Environment  | Docker Inside  |
| :-------------|:------------------:| :------------------:|:--------------:|
| API           | 5000 - 5050        | 6000                | 8080 - 8081    |
| Database      |                    | 5400                | 5432           |

### Requests 
[postman export](https://github.com/Grizzly-Alex/Ecommerce-Shop/tree/feature/catalog.api/src/Services/Catalog/Postman)

| Method  | Request URI                  | Description               |
| :-------|:-----------------------------| :-------------------------|
| GET     | /health                      | Checking API availability |
| GET     | /products                    | Get all products          |
| GET     | /products/{id}               | Get a product by Id       |
| GET     | /products/category/{example} | Get products by category  |
| POST    | /products                    | Create a product          |
| PUT     | /products/{id}               | Update a product          |
| DELETE  | /products/{id}               | Remove a product          | 

https://localhost:5050/swagger/index.html

![image](https://github.com/user-attachments/assets/64e02db9-a91c-492d-844f-a667b01cf143)
![image](https://github.com/user-attachments/assets/bdec7995-c50e-477c-8d59-d0f83a7e8c9b)
![image](https://github.com/user-attachments/assets/7b57faa9-9c70-4abf-90e3-1ff42a723426)
![image](https://github.com/user-attachments/assets/7aaacc2f-d754-47be-9a75-2debdb6f72fd)
![image](https://github.com/user-attachments/assets/c4134a44-c6c3-4c51-ba9e-9ddf89e5b583)
![image](https://github.com/user-attachments/assets/ff9d0a80-1a8c-479a-8b40-31cacdd0bd56)


Microservice has got Vertical Slice Architecture with CQRS patern.
To implementation the CQRS pattern I used a MediatR [nuget](https://www.nuget.org/packages/mediatr/ "MediatR nuget package"). This provides low coupling with the endpoints and allows you to write cleaner, more understandable code.
Low code coupling is also ensured by using the IPipelineBehavior generic interface for validations and logging. 
The [PostgreSQL](https://www.postgresql.org/) database was chosen to store product data and the [Marten](https://martendb.io "site Marten") ORM was chosen to interact with it.
Marten transforms PostgreSQL into a .NET Transactional Document DB. This is made possible by the unique [JSONB](https://www.postgresql.org/docs/current/datatype-json.html) support first introduced in Postgresql 9.4.

### Request scheme

![image](https://github.com/user-attachments/assets/ca4d54f8-231e-4399-a384-12ef4498cbde)

### Log:
![image](https://github.com/user-attachments/assets/f3f7e6c7-c5c9-44de-9d99-356cce2b4cf0)

