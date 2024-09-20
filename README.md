# Article Hub

## About The Project

### Purpose
The purpose of this project is to provide an efficient system for managing articles. The platform allows users to view, create, edit, and delete articles, while administrators can manage users and categories. The system also features secure authentication using JWT tokens, ensuring role-based access control for different user roles.

### Key Features

#### Admin Features:
- **Admin Dashboard**
- **Category Management**: Add, edit, and delete categories.
- **Article Management**: Add, edit, delete, and filter articles.
- **User Management**: Manage users (create, update, and delete users).
- **Password Management**: Change user passwords.
- **Role-based Access Control**: Admin can restrict access to certain features.

#### User Features:
- **Login & Sign Up (JWT Authentication)**
- **User Dashboard**
- **Article Management**: Create, edit, and delete articles.
- **Profile Management**: Update user profile details.
- **Password Management**: Change password.

### Security Features
- **JWT Token Authentication**: Ensures secure access for users and admin.
- **Role-Based Access Control**: Limits access to features based on user roles (admin/user).

## Built With
- **Backend**: ASP.NET (C#)
- **Frontend**: Angular
- **Database**: MSSQL Database
- **Authentication**: JWT Tokens

## How To Run

### Angular Part
1. Open terminal in VS Code and run:
    ```bash
    ng s
    ```
2. Open [localhost:4200](http://localhost:4200) in your browser.

### ASP.NET Part
1. Start the ASP.NET backend using the command:
    ```bash
    dotnet run
    ```
2. Use the URL [localhost:5000](http://localhost:5000) for the backend.
3. To test the APIs, use Postman:
    - First, sign up and log in to obtain the JWT token.
    - Pass the token in the headers for protected routes.
    - Admin features are protected by role-based authorization.

### Database
1. Ensure MSSQL Server is running.
2. Update the connection string in the `appsettings.json` file of the ASP.NET project to connect to your local or remote MSSQL database.

## Note
This project is for educational purposes and was developed as part of learning exercises.
