# Engineer Task Management System

A desktop application developed in C# and WPF for managing engineers, with business logic for tasks, assignments, dependencies, and scheduling.

The project follows a layered architecture that separates the presentation, business logic, and data access layers, with an emphasis on modularity, maintainability, and object-oriented design.


## Features

- View and manage engineers through a WPF desktop interface
- Add and update engineer information
- Filter engineers by experience level
- Support task management and responsible engineer assignments through the business logic layer
- Track task dependencies and scheduling


## Technologies

- C#
- .NET 7
- WPF
- XAML
- XML
- LINQ
- Object-Oriented Programming


## Architecture

The application is built using a layered architecture that separates responsibilities between the user interface, business logic, and data access layers.

```text
Presentation Layer (PL)
        ↓
Business Logic Layer (BL)
        ↓
Data Access Layer (DAL)
```


### Presentation Layer (PL)

The Presentation Layer is implemented using WPF and XAML and is responsible for the application's user interface.

It includes the main application window and engineer management screens, allowing users to view, filter, add, and update engineers.

The Presentation Layer communicates with the Business Logic Layer instead of accessing the data directly.


### Business Logic Layer (BL)

The Business Logic Layer contains the application's core logic and business rules.

It manages engineers, tasks, task dependencies, scheduling, task statuses, and input validation.

The layer exposes interfaces that are used by the Presentation Layer and communicates with the Data Access Layer for retrieving and updating data.


### Data Access Layer (DAL)

The Data Access Layer is responsible for storing, retrieving, updating, and deleting application data.

The project includes multiple data access implementations, including in-memory storage and XML-based persistence.

The Business Logic Layer communicates with the DAL through interfaces, allowing the data storage implementation to remain separate from the application's business logic.


## Task Management

The system supports managing tasks with information such as:

- Task ID and nickname
- Description and comments
- Responsible engineer
- Required engineer level
- Difficulty level
- Execution duration
- Planned start date and deadline
- Actual start and completion dates
- Task dependencies
- Current task status


## Task Dependencies and Scheduling

Tasks can depend on other tasks in the system.

The Business Logic Layer includes scheduling validation that checks predecessor tasks before assigning a planned start date to a task.

This helps represent the execution order between dependent tasks and prevents a task from being scheduled before its required predecessor tasks.


## Project Structure

```text
engineer-task-management-system/
│
├── PL/                         # WPF Presentation Layer
├── BL/                         # Business Logic Layer
├── DalFacade/                  # Data objects and DAL interfaces
├── DalList/                    # In-memory data access implementation
├── DalXml/                     # XML-based data access implementation
├── DalTest/                    # Data layer testing and initialization
├── BlTest/                     # Business logic testing
├── xml/                        # XML data files
│
└── EngineerTaskManagementSystem.sln
```


## Design Principles

The project demonstrates several software engineering principles:

- Layered architecture
- Separation of concerns
- Interface-based design
- Object-oriented programming
- Separation between Business Objects and Data Objects
- Multiple data access implementations
- Input validation and exception handling


## Project Context

This project was developed as an academic software engineering project, with a focus on C#, WPF, layered application architecture, data access abstraction, and business logic design.
