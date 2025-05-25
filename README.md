# Document Access Approval System

## Overview

This project is a Document Access Approval System built with ASP.NET Core. It simulates an internal document access request workflow with support for multiple roles (User, Approver, Admin) and email notifications (simulated).

## Architecture

The project follows a layered architecture:

```
/API              - Controllers & startup logic
/Application      - Services, DTOs, Interfaces, Background tasks, fake email service
/Domain           - Core domain models and enums
/Infrastructure   - Data access (EF Core InMemory)
/Tests            - Unit tests
```

## Implemented Features

* User can create access requests
* Approer can view and decide on pending requests
* Admin can view all requests and do Approver job
* Role-based access control via JWT
* Background job trigger email notification after approval
* Swagger UI for API exploration

## Roles

* **User** — can submit and view their requests
* **Approver** — can approve/reject pending requests
* **Admin** — can do Approver job and see all requests

## Getting Started

### Running the Application

```bash
# Run the API project
cd API
dotnet run
```

### Running the Tests

```bash
# From solution root or Tests folder
cd Tests
dotnet test
```

### Swagger UI

Open in browser:

```
https://localhost:7177/swagger
```

## Assumptions & Limitations

* Email notifications are logged to the console (not sent)
* No real user registration or password hashing (users are hardcoded)

## Potential Improvements

* Implement real email delivery using SMTP or SendGrid
* Add persistent storage (e.g., SQL Server or PostgreSQL)
* Create Admin interface to manage users and their roles
* Add UI (web frontend) to interact with the API
* Improve security (e.g., password hashing, account lockout)

---

Thank you for reviewing!
