
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
- **Hosting**: Amazon EC2
- **Unit Testing**: xUnit, Moq

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
2. CRUD operations for user management.
3. Role-based access to secured endpoints.

### 3.2 Test Results
```
Test Run Successful.
Total tests: 12
Passed: 12
Failed: 0
Skipped: 0
```

---

## 4. Deployment Instructions

### 4.1 Amazon EC2 Deployment
1. **Prepare EC2 Instance**:
   - Launch an Amazon Linux 2 or Ubuntu 22.04 instance.
   - Install .NET Runtime and Nginx.
2. **Transfer Files**:
   - Use SCP to upload the published project files to `/var/www/myproject`.
3. **Run the Application**:
   - Start the app with `dotnet MyProject.dll` or configure it as a systemd service.

### 4.2 CI/CD Setup
- **Pipeline**: AWS CodePipeline with CodeBuild for CI.
- **Deploy Artifacts**: Stored in S3, automatically deployed to EC2.

---

## 5. Monitoring and Scaling

### 5.1 Monitoring
- **Metrics**: CPU, memory, and disk usage monitored via CloudWatch.
- **Logging**: Application logs sent to CloudWatch Logs for centralized monitoring.

### 5.2 Scaling
- **Auto Scaling**: Configured for horizontal scaling.
- **Elastic Load Balancer**: Distributes traffic across multiple instances.

---
