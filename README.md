# Ecommerce Shop (client-server application based on microservice architecture)

### Description
Developing a project using the .NET 8 and ASP.NET Core framework. The application has a microservice architecture.
Why did I choose microservices for my project? Firstly, for educational purposes, and secondly, such architecture will allow me to easily expand the functionality of the application.
For each microservice will have its own architecture and its own database type. The services will use databases such as relational databases (PostgreSQL, Sql Server) and NoSQL(Redis, DocumentDb).
Implementing interaction between services via RabbitMQ message broker and using the Yarp API Gateway.

### Content:
- [BuildingBlocks Library](#buildingblocks-library)
- [Catalog Microservice](#catalog-microservice)
- [Catalog Service Tests](#catalog-service-tests)
- [Basket Microservice](#basket-microservice)


### Ports
| Microservices | Local Environment  | Docker Environment  | Docker Inside  |
| :-------------|:------------------:| :------------------:|:--------------:|
| Catalog       | 5000 - 5050        | 6000                | 8080 - 8081    |
| Basket        | 5001 - 5051        | 6001                | 8080 - 8081    |
| Diskount      | 5002 - 5052        | 6002                | 8080 - 8081    |
| Ordering      | 5003 - 5053        | 6003                | 8080 - 8081    |

### Application Diagram

![EcommerceShop (Microservices)](https://github.com/user-attachments/assets/a4c4ceec-0c0a-4422-a62f-0054dacd98d3)

# <a id="buildingblocks-library">BuildingBlocks Library</a> 

[BuildingBlocks](https://github.com/Grizzly-Alex/Ecommerce-Shop/tree/main/src/BuildingBlocks/BuildingBlocks)

This library contains code modules that will be reused by other services.
This is where abstractions for CQRS, pipeline behaviors, exception handlers, etc. are collected.
Don't forget about the DRY principle :)

![image](https://github.com/user-attachments/assets/1a29a9ec-9a35-413b-b5db-ffa21e032570)


# <a id="catalog-microservice">Catalog Microservice</a>

[Catalog.API](https://github.com/Grizzly-Alex/Ecommerce-Shop/tree/main/src/Services/Catalog/Catalog.API)

This service is responsible for management products which store in the database. 
These are the ordinary CRUD operations. Microservice works on http/https protocols with using REST architecture.

### Ports
| Services | Local Environment  | Docker Environment  | Docker Inside  |
| :--------|:------------------:| :------------------:|:--------------:|
| API      | 5000 - 5050        | 6000                | 8080 - 8081    |
| Database |                    | 5400                | 5432           |

### Requests 

[postman export](https://github.com/Grizzly-Alex/Ecommerce-Shop/tree/main/postman)

| Method  | Request URI                  | Description                    |
| :-------|:-----------------------------| :------------------------------|
| GET     | /health                      | Checking Database availability |
| GET     | /products                    | Get all products               |
| GET     | /products/{id}               | Get a product by Id            |
| GET     | /products/category/{example} | Get products by category       |
| POST    | /products                    | Create a product               |
| PUT     | /products/{id}               | Update a product               |
| DELETE  | /products/{id}               | Remove a product               | 


### Examples Of Queries 
https://localhost:5050/swagger/index.html

<details><summary>Get all products</summary>
  
   ![image](https://github.com/user-attachments/assets/64e02db9-a91c-492d-844f-a667b01cf143)
   
</details>

<details><summary>Get product by id</summary>
  
   ![image](https://github.com/user-attachments/assets/bdec7995-c50e-477c-8d59-d0f83a7e8c9b)
   
</details>

<details><summary>Get product by category</summary>
  
   ![image](https://github.com/user-attachments/assets/7b57faa9-9c70-4abf-90e3-1ff42a723426)
   
</details>

<details><summary>Create product</summary>
  
   ![image](https://github.com/user-attachments/assets/7aaacc2f-d754-47be-9a75-2debdb6f72fd)
   
</details>

<details><summary>Update product</summary>
  
   ![image](https://github.com/user-attachments/assets/c4134a44-c6c3-4c51-ba9e-9ddf89e5b583)
   
</details>

<details><summary>Delete product</summary>
  
   ![image](https://github.com/user-attachments/assets/ff9d0a80-1a8c-479a-8b40-31cacdd0bd56)
   
</details>

### Architecture
API has got Vertical Slice Architecture. Organizes our code into feature folders, each feature encapsulated in a single .cs file.

![image](https://github.com/user-attachments/assets/5a5ebbc2-1123-456e-81cf-baae8493e653) 
![image](https://github.com/user-attachments/assets/3ab5b377-8f60-4151-9c7b-8370f7a650ff)

### Underlying Data Structures
The [PostgreSQL](https://www.postgresql.org/) database was chosen to store product data and the [Marten](https://martendb.io "site Marten") ORM was chosen to interact with it.
Marten transforms PostgreSQL into a .NET Transactional Document DB. This is made possible by the unique [JSONB](https://www.postgresql.org/docs/current/datatype-json.html) support first introduced in Postgresql 9.4.
This solution combines the flexibility of a document database with the reliability of a PostgreSQL relational database.

### CQRS
For more cleaner code I used a CQRS pattern.
To implementation this pattern I used a MediatR [nuget](https://www.nuget.org/packages/mediatr/ "MediatR nuget package"). This provides low coupling with the endpoints and allows you to write cleaner, more understandable code.
Low code coupling is also ensured by using the IPipelineBehavior generic interface for validations and logging. 

![image](https://github.com/user-attachments/assets/275aa4b1-71e1-4ea5-a8b8-383088ca2013)

### Log:
Each request is logged via LoggingBenavior. The start and end of the process are logged.
If the process higher the threshold which equals 3 seconds, will be logged perfomance warning.
Every error is logged, such as input data validation errors.

![image](https://github.com/user-attachments/assets/13a7c1c8-c8a7-4cbf-b21e-ce002f96193c)
![image](https://github.com/user-attachments/assets/5b760116-bd83-4520-8bee-629f6291ced1)






# <a id="basket-microservice">Basket Microservice</a>
[Basket.API](https://github.com/Grizzly-Alex/Ecommerce-Shop/tree/main/src/Services/Basket/Basket.API)

This service is responsible for storing user baskets. Microservice works on http/https protocols with using REST architecture.
The API interacts with the NoSQL MongoDb database and Redis for caching.

### Ports
| Services | Local Environment  | Docker Environment  | Docker Inside  |
| :--------|:------------------:| :------------------:|:--------------:|
| API      | 5001 - 5051        | 6001                | 8080 - 8081    |
| Database |                    | 5401                | 27017          |
| Cacher   |                    | 6379                | 6379           |

### Requests 

[postman export](https://github.com/Grizzly-Alex/Ecommerce-Shop/tree/main/postman)

| Method  | Request URI               | Description                    |
| :-------|:--------------------------| :------------------------------|
| GET     | /health                   | Checking Database availability |
| GET     | /basket/{userId}          | Get a basket for user          |
| POST    | /basket                   | Create or Update a basket      |
| DELETE  | /basket/{userId}          | Remove a basket                | 


### Examples Of Queries 
https://localhost:5051/swagger/index.html

<details><summary>Store basket</summary>
  
   ![image](https://github.com/user-attachments/assets/8225448e-423f-407a-abb0-f61637943654)
   
</details>

<details><summary>Get basket</summary>
  
   ![image](https://github.com/user-attachments/assets/50f00dc2-46ca-4be1-9e72-dda8781370a5)
   
</details>

<details><summary>Delete basket</summary>
  
   ![image](https://github.com/user-attachments/assets/33e7574a-0a43-4407-bda7-a6e2dba90575)
   
</details>


### Architecture
API has got Vertical Slice Architecture. Organizes our code into feature folders, each feature encapsulated in a single .cs file.

![image](https://github.com/user-attachments/assets/5a5ebbc2-1123-456e-81cf-baae8493e653) 
![image](https://github.com/user-attachments/assets/3d453789-0c26-4fb8-a870-e840a9e109fd)


### Underlying Data Structures
The [MongoDb](https://www.mongodb.com/) database was chosen to store baskets. Interaction with the database occurs using [MongoDB.Driver](https://www.nuget.org/packages/mongodb.driver) ORM.
[Redis](https://redis.io/) is responsible for data caching. Interaction with the cache and database occurs using the BasketRepository and CachedBasketRepository repositories, which implement the IBasketRepository interface.
Registration of CachedBasketRepository is done using the Decorate method, this is an extension method for IServiceCollection. 

![image](https://github.com/user-attachments/assets/d1c10429-b351-48ce-a557-a1bc4dd2cb33)

### CQRS
Just like with the catalog api, I used CQRS to keep the code more clean and used for this a MediatR [nuget](https://www.nuget.org/packages/mediatr/ "MediatR nuget package"). 
This provides low coupling with the endpoints and allows you to write cleaner, more understandable code.
Low code coupling is also ensured by using the IPipelineBehavior generic interface for validations and logging. 






# <a id="catalog-service-tests">Catalog Service Tests</a>
[CtalogServiceTests](https://github.com/Grizzly-Alex/Ecommerce-Shop/tree/main/tests/CtalogServiceTests)


### Unit Tests
For unit testing I have the following nugget packages:
 - [xUnit](https://www.nuget.org/packages/xunit)
 - [Moq](https://www.nuget.org/packages/Moq)
 - [FluentAssertions](https://www.nuget.org/packages/FluentAssertions.AspNetCore.Mvc)

Marten handlers were tested by simulating various situations such as successful operation or throwing exception if the product was not found in the database.
Working with the database is simulated by mocking Marten.IDocumentSession.

![image](https://github.com/user-attachments/assets/9c0906ec-efbe-46da-9cef-903a867d4dff)

### Integration Tests
For Integration testing I have the following nugget packages:
 - [xUnit](https://www.nuget.org/packages/xunit)
 - [FluentAssertions](https://www.nuget.org/packages/FluentAssertions.AspNetCore.Mvc)
 - [Testcontainers.PostgreSql](https://www.nuget.org/packages/Testcontainers.PostgreSql)
 - [Microsoft.AspNetCore.Mvc.Testing](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing)

In time execution of integration tests, a database container is used for tests.
The container life cycle is for the period of the test execution process.
After the tests are executed, the container is deleted.

Testing endpoints for expected status codes.

![image](https://github.com/user-attachments/assets/a63f9388-9682-4692-bf16-38fdfda3c5bf)











