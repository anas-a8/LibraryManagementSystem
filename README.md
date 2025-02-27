# Library Management System API

## Overview
This is a **Library Management System API** built using **ASP.NET Core** with **JWT authentication**, **Entity Framework Core**, and **SQL Server** as the database. It provides endpoints for user authentication and book management, ensuring secure access based on user roles.

## Features
- User authentication with JWT (JSON Web Tokens)
- Role-based access control (Admin, User)
- Book management (CRUD operations)
- Database integration using Entity Framework Core
- Secure API with role-based authorization
- LINQ queries for advanced data retrieval
- Asynchronous programming for efficient API calls
- Swagger API documentation

## Technologies Used
- **ASP.NET Core** (Web API)
- **Entity Framework Core** (EF Core)
- **SQL Server** (LocalDB for development)
- **JWT Authentication** (JSON Web Token)
- **Swagger UI** (API documentation)
- **LINQ** (for advanced queries)
- **Async/Await** (for external API calls)

---

## Project Structure
```
LibraryManagementSystem/
│── Controllers/          # API Controllers
│   ├── AuthController.cs # Handles authentication (login)
│   ├── BooksController.cs # Handles book-related operations
│
│── Data/                # Database Context
│   ├── LibraryDbContext.cs # Manages database operations
│   ├── Migrations/       # EF Core migrations (if present)
│
│── Models/              # Data Models
│   ├── Book.cs          # Book entity
│   ├── UserRoles.cs     # User roles (Admin, User)
│
│── Properties/
│── appsettings.json      # Configuration file (Database, JWT settings)
│── Program.cs           # Main entry point & middleware setup
│── LibraryManagementSystem.sln # Solution file
```

---

## Setup & Installation

### 1. Clone the Repository
```sh
git clone https://github.com/your-repo/LibraryManagementSystem.git
cd LibraryManagementSystem
```

### 2. Configure Database Connection
Edit the **`appsettings.json`** file to update the **database connection string**:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=LibraryDB;Trusted_Connection=True;"
}
```

### 3. Apply Database Migrations
```sh
dotnet ef database update
```

### 4. Run the API
```sh
dotnet run
```

### 5. Access Swagger UI (API Testing)
Open your browser and go to:
```
https://localhost:5169/swagger
```

---

## Authentication & Authorization

### **Login API**
#### Endpoint: `POST /api/auth/login`
#### Request Body (JSON):
```json
{
  "username": "admin",
  "password": "admin123"
}
```
#### Response:
```json
{
  "token": "<JWT Token>"
}
```
Use this token for authenticated requests by adding it to the **Authorization** header:
```
Authorization: Bearer <JWT Token>
```

---

## Book Management

### **Get All Books**
#### Endpoint: `GET /api/books`
#### Requires authentication.

### **Add a Book (Admin Only)**
#### Endpoint: `POST /api/books`
#### Request Body (JSON):
```json
{
  "title": "ASP.NET Core Guide",
  "author": "Jane Smith",
  "isbn": "0987654321",
  "copiesAvailable": 8
}
```

### **Delete a Book (Admin Only)**
#### Endpoint: `DELETE /api/books/{id}`

### **Get Books by Author**
#### Endpoint: `GET /api/books/grouped`
#### Description: Retrieves books grouped by author using LINQ.

### **Get Top 3 Most Borrowed Books**
#### Endpoint: `GET /api/books/topborrowed`
#### Description: Retrieves the top 3 most borrowed books based on mock borrowing data.

### **Fetch Book Details (Async API Call)**
#### Endpoint: `GET /api/books/external/{isbn}`
#### Description: Simulates fetching book details from an external API using async/await.

---

## Security Enhancements (Planned)
- **Use hashed passwords** (instead of hardcoded ones)
- **Implement refresh tokens**
- **Multi-factor authentication (MFA)**
- **Fine-grained role-based access control (RBAC)**

---

## Additional Notes
### **Evaluation Criteria**
This project aligns with Bincom Academy's **C# Intermediate Test Requirements**, fulfilling:
- **Authentication & Role-Based Authorization (15 Points)** ✅
- **Entity Framework Core Integration (15 Points)** ✅
- **LINQ for Data Queries (10 Points)** ✅
- **Async Programming (10 Points)** ✅
- **API Security & Swagger Documentation (10 Points)** ✅
- **CI/CD Pipeline in Azure DevOps (Bonus 5 Points)** (Pending)

---

## Contributing
Feel free to contribute by submitting **issues** and **pull requests**.

---

## License
This project is licensed under the MIT License.

