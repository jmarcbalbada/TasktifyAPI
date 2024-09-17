# Tasktify API

Tasktify is a task management API built with **ASP.NET Core Web API**. The application allows users to register, login, create tasks, update tasks, and manage users. It uses **Microsoft SQL Server (MSSQL)** as the database.

## Features
- User registration and login with JWT authentication
- Task management (create, update, delete tasks)
- User management (create, update, delete users)
- **REST**ful API design
- Swagger API documentation
- Entity Framework Core for data access

## Technologies
- **ASP.NET Core 8.0** - Framework for building web APIs
- **Entity Framework Core** - ORM for database interaction
- **Microsoft SQL Server (MSSQL)** - Database
- **JWT (JSON Web Tokens)** - Authentication and authorization
- **Swagger** - API documentation
- **Dependency Injection** - For better service management
- **AutoMapper** - For DTO to entity mappings
- **CORS** - Cross-Origin Resource Sharing
- **Password hashing** - Secure password storage using ASP.NET Identity

## Prerequisites

1. **.NET 8.0 SDK**: [Download here](https://dotnet.microsoft.com/download)
2. **Microsoft SQL Server**: You can download and install SQL Server from [here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
3. **SQL Server Management Studio (SSMS)** for managing your SQL Server database (Optional).

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/jmarcbalbada/TasktifyAPI.git
cd Tasktify
```
### 2. Set Up the Database
- Ensure that SQL Server is running.
- Update the connection string in the `appsettings.json` file or in `secrets.json` (recommended for sensitive information):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=Tasktify;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```
### Alternatively, use User Secrets for sensitive configuration settings like connection strings:
```
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=YOUR_SERVER_NAME;Database=Tasktify;Trusted_Connection=True;TrustServerCertificate=True;"
```
### 3. Apply Migrations
After setting up the database connection string, apply the migrations to set up the database schema:
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```
### 4. Running the Application
You can hit CTRL + F5 on your keyboard to avoid debugging and run right away.
```
dotnet run
```
By default, the API will be available at ```https://localhost:7050```.
### 5. Using Swagger for API Documentation
Once the API is running, navigate to the Swagger UI to explore the available endpoints:
```
https://localhost:7050/swagger
```
## API Endpoints
#### Authentication
> POST 
- `/api/auth/register` - Register a new user.

> POST  
- `/api/auth/login` - Login and receive a JWT token.
---
#### Users
> GET  
- `/api/user` - Get all users.

> GET   
- `/api/user/{id}` - Get user by ID.

> POST   
- `/api/user` - Create a new user.

> DELETE   
- `/api/user/{id}` - Delete a user.
---
#### Tasks
> GET  
- `/api/task` - Get all tasks.

> GET   
- `/api/task/{id}` - Get task by ID.

> POST   
- `/api/task?userId={userId}` - Create a task for a specific user.

> PUT    
- `/api/task/{id}` - Update task by ID.

> DELETE    
- `/api/task/{id}` - Delete task by ID.

### Running Tests
Unit and integration tests can be run using the following command:
```
dotnet test
```

### Project Structure
```
TasktifyAPI/
│
├── Controllers/           # API Controllers
├── Models/                # Entity models
├── Models/Dtos/           # DTO (Data Transfer Objects)
├── Repositories/          # Repository layer for database operations
├── Services/              # Service layer for business logic
├── Program.cs             # Application startup
├── appsettings.json       # Application configuration
└── Startup.cs             # Dependency Injection and middleware setup
```

### Security
- Passwords are hashed using ASP.NET Identity's password hasher.
- JWT tokens are used for securing the API.
- Make sure to set proper CORS policies when deploying to production.

### Future Improvements
- Add refresh token support for authentication.
- Implement role-based access control (RBAC).
- Add more unit and integration tests.
- Implement logging with structured logs.

### License
This project is licensed under the MIT License. See the [LICENSE](https://github.com/jmarcbalbada/TasktifyAPI/blob/master/LICENSE.txt) file for details.

### Contact
For any inquiries or support, please reach out to [jmarcbalbada@gmail.com].


### Key Sections:
- **Prerequisites**: Lists required software like .NET SDK and SQL Server.
- **Getting Started**: Provides instructions for cloning, configuring, and running the app.
- **API Endpoints**: Summarizes available API routes.
- **Built With**: Technologies used in the project.
- **Security**: Highlights key security features like password hashing and JWT.

Feel free to customize this as needed for your project!

> All rights reserved. 2024
