# GameAPI

## Description
Game API serves as a middleware, facilitating the interactions between the database and client-side applications of a game, whether it be a web frontend or the Photon Unity Networking 2 framework.

## Features

- **User Management:** Create, read, update, and delete user data including character information.
- **PasswordHashing:** Hashes the entered password and saves the hashed variant on the database for extra security

## Prerequisites
- **.NET SDK** Ensure you have the [.NET SDK](https://dotnet.microsoft.com/download) installed.
- **Visual Studio:** Recommended to use [Visual Studio](https://visualstudio.microsoft.com/visual-cpp-build-tools/) for development.

## NuGets used:
- EntityFramework
- EntityFrameworkCore
- EntityFrameworkCore.Design
- EntityFrameworkCore.SqlServer
- EntityFrameworkCore.Tools
- Swashbuckle.AspNetCore
- MOQ(Unit Testing project)
- Xunit(Unit testing project)

## Getting started

Clone the repository

Open the solution in Visual Studio, set up your launch settings, and run the project.

Establish the connection string with your SQL server in appsettings.json:
``  "ConnectionStrings": {
    "DefaultConnection": "Server=Servername;Database=Dbname;User Id=userID;Password=yourpassword; TrustServerCertificate=True;"
  }``

## API Endpoints
GET /api/users: Retrieves a list of all users.
GET /api/users/{id}: Retrieves a single user by ID.
POST /api/users: Creates a new user.
PUT /api/users/{id}: Updates an existing user.
DELETE /api/users/{id}: Deletes a user.

## Testing
The GameAPITests project contains unit tests for the API. Simply navigate to the Test Explorer in Visual Studio and run all tests to validate the API's functionality.
You can also test it manually via Swagger or Postman.

## Contributing
Feel free to fork the project, open a pull request, or submit issues with bug reports or feature requests.

## License
This project is licensed under the MIT License.