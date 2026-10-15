# RaceDay – Event Management System

## 1. Project Overview

RaceDay is a C# ASP.NET Core Web API project developed to manage events, categories, organizers, participants, enrollments, and results. The application connects to a SQL Server database to store, retrieve, update, and delete information.

The project demonstrates database integration, API development, CRUD operations, input validation, and automated testing.

## 2. Technologies Used

* **C#** – Main programming language.
* **ASP.NET Core Web API** – Development of API endpoints.
* **SQL Server LocalDB** – Database storage.
* **Microsoft.Data.SqlClient** – Database connectivity.
* **xUnit** – Automated testing.
* **Visual Studio** – Development environment.
* **Git and GitHub** – Version control and source code management.

## 3. Main Features

The system includes six main components:

* **Categories:** Create, retrieve, update, and delete event categories.
* **Events:** Manage event names, locations, dates, categories, and organizers.
* **Organizers:** Manage organizer details and validate login credentials.
* **Participants:** Register participants, retrieve their details, and update or delete records.
* **Enrollments:** Associate participants with events and manage enrollment records.
* **Results:** Record and manage participants' scores and finishing positions.

## 4. Database

The application uses a SQL Server LocalDB database named `raceday`.

The database stores information in related tables, including:

* `Category`
* `Event_`
* `Organizer`
* `Participant`
* `Enrollment`
* `Results`

The tables use identifiers to associate related records, such as linking events to categories and organizers, and enrollments to events and participants.

## 5. Testing

The project uses **xUnit** to test controller functionality.

The tests cover successful and unsuccessful operations, including retrieving records, adding information, updating existing records, and handling records that do not exist.

To run the tests in Visual Studio:

1. Open the solution.
2. Build the solution.
3. Open **Test → Test Explorer**.
4. Select **Run All Tests**.
5. Review the results to identify any failing tests.

## 6. How to Run the Project

1. Install Visual Studio with the required .NET and ASP.NET development components.
2. Ensure SQL Server LocalDB is installed.
3. Open the project solution in Visual Studio.
4. Ensure the `raceday` database exists on the configured LocalDB instance.
5. Check that the connection string in the controllers matches your database configuration.
6. Build and run the application.

API endpoints can then be tested using an API testing tool or the configured API documentation interface, if available.

## 7. Learning Outcomes

This project demonstrates practical experience in C# programming, ASP.NET Core Web API development, SQL Server integration, CRUD operations, input validation, relational database management, and automated software testing.

## 8. Author

**Imitha Maneli**

GitHub: [imitha123](https://github.com/imitha123)
