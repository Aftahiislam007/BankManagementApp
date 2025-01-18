
# Bank Management System App

---

## 1. Project Overview

### 1.1 Purpose
The Bank Management System App is a RESTful web service built using ASP.NET Core 8.0. It provides user authentication and management with role-based access control. The API is designed to support scalable, secure, and efficient web applications.

### 1.2 Technologies Used
- **Framework**: ASP.NET Core 8.0, ASP.NET Web API
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: JWT
- **Unit Testing**: Cypress


### 1.3 Generate JWT Secret Key

The JWT secret key is created using the C# code. 

### 1.4 Generate Encryption Key

The Encryption key is created using the C# code. 

---

## 2. API Endpoints

### 2.1 Authentication
| HTTP Method | Endpoint        | Description             | Request Body              | Response                     |
|-------------|-----------------|-------------------------|---------------------------|------------------------------|
| `POST`      | `/api/auth/login` | Login and get JWT token | `{ "username": "test", "password": "pass" }` | `{ "token": "JWT_Token" }`   |

### 2.2 User Management
| HTTP Method | Endpoint              | Description          | Request Body                          | Response                       |
|-------------|-----------------------|----------------------|---------------------------------------|--------------------------------|
| `GET`       | `/api/users`          | Get all users        | -                                     | `[{ "id": 1, "username": "..." }]` |
| `POST`      | `/api/users`          | Create a new user    | `{ "username": "new", "password": "pass", "role": "User" }` | `{ "id": 1, "username": "new" }` |
| `GET`       | `/api/users/{id}`     | Get user by ID       | -                                     | `{ "id": 1, "username": "..." }` |
| `PUT`       | `/api/users/{id}`     | Update user details  | `{ "username": "updated", "password": "newpass" }` | `{ "message": "Updated successfully" }` |
| `DELETE`    | `/api/users/{id}`     | Delete a user        | -                                     | `{ "message": "Deleted successfully" }` |

---

## 3. Unit Testing

### 3.1 Test Coverage
The following functionalities have been unit tested:
1. User login with valid and invalid credentials.

