# Project Overview

This project is a .NET 8 application using C# 12.0. It follows a clean architecture and n-layered architecture approach to ensure separation of concerns and maintainability.

# Clean Architecture Project

This project follows the Clean Architecture principles to ensure separation of concerns and maintainability.

## Folder Structure

### API
Contains the API controllers and related configurations.

### Application
Contains the application logic, including features, commands, and queries.

### Domain
Contains the domain entities and interfaces.

### Infrastructure
Contains the implementation of the domain interfaces, including data access and external services.

### Services
Contains the service layer, including business logic and mapping profiles.

## Key Features

- **CategoriesController**: Manages category-related operations.
- **CategoryProfile**: AutoMapper profile for category-related mappings.
- **ServiceResult**: Standardized service result pattern for handling responses.

# N-Layered Architecture Project

This project follows the N-Layered Architecture principles to ensure separation of concerns and maintainability.

## Folder Structure

### API
Contains the API controllers, related configurations, and middleware.

### Services
Contains the service layer, including business logic, mapping profiles, and service interfaces.

### Repositories
Contains the implementation of the domain interfaces, including data access, external services, and repositories.

## Key Features

- **CategoriesController**: Manages category-related operations.
- **CategoryProfile**: AutoMapper profile for category-related mappings.
- **ServiceResult**: Standardized service result pattern for handling responses.

## Technologies Used

- .NET 8
- C# 12.0
- AutoMapper
- ASP.NET Core