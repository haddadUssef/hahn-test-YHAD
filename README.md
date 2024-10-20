# Hahn Test - Ticket Management System

This project is a **ticket management system** developed as part of a technical assessment for Hahn Software. The system consists of a **backend** API built with .NET and a **frontend** UI built using **Angular** and **NG-ZORRO (Ant Design)**.

## Table of Contents
- [Project Overview]
- [Technologies Used]
- [Features]
- [Dependencies]
- [Dependencies]
- [Setup and Installation]
  - [Backend Setup]
  - [Frontend Setup]
  - [Environment Configuration]


## Project Overview

The Ticket Management System allows users to manage and track ticket issues efficiently. It supports creating, editing, and deleting tickets, and offers a simple user interface for ticket tracking and management.

## Technologies Used

### Backend:
- **.NET Core** for API development
- **Entity Framework** for data management
- **SQL Server** as the database

### Frontend:
- **Angular** for the user interface
- **NG-ZORRO (Ant Design)** for component design
- **TypeScript** for frontend logic
- **CSS** for styling

## Features
- **Ticket Management**: Create, update, and delete tickets.
- **API Integration**: Connects frontend with backend for seamless ticket management.
- **User-Friendly UI**: Uses Ant Design components for an intuitive user experience.
- **Environment Configurations**: Supports multiple environments for development and production.
- **Pagination on Backend**: The backend API supports pagination to handle large datasets efficiently.
- **Table Sorting on Frontend**: The frontend UI provides sorting functionality on the ticket list to improve usability and data organization.
- **Unit Tests on Backend**: Some unit tests are in place to test backend functionality (though they might need a little debugging to work perfectly 😅).

## Dependencies

### Backend:
- **.NET Core SDK**: Framework for building and running the backend API.
- **Entity Framework Core**: ORM (Object-Relational Mapping) for managing database interactions.
- **FluentValidation**: A popular library for building strongly-typed validation rules.
- **OpenAPI/Swagger**: Used to generate API documentation and test endpoints interactively.
- **xUnit/MSTest**: Testing frameworks used for unit testing in the backend.
- **Moq**: A powerful library for mocking dependencies in unit tests, helping isolate components and improve testability.

### Frontend:
- **Angular CLI**: Used for creating and managing the Angular application.
- **NG-ZORRO (Ant Design)**: UI library for Angular, used to build components and design the UI.
- **TypeScript**: Primary language for building frontend logic.
- **RxJS**: Library for reactive programming using observables, integrated with Angular.
- **Zone.js**: Library used by Angular for change detection.

## Setup and Installation

### Prerequisites
- **Node.js** (for the frontend)
- **.NET Core SDK** (for the backend)
- **SQL Server** (for the database)

### Backend Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/haddadUssef/hahn-test-YHAD.git
2. Navigate to the backend:
   ```bash
   cd backend
3. Restore dependencies:
   ```bash
   dotnet restore
4. Run migrations to set up the database:
   ```bash
   dotnet ef database update
5. Run the backend API:
   ```bash
   dotnet run

### Frontend Setup
1. Navigate to the frontend folder:
   ```bash
   cd frontend
2. Install dependencies:
   ```bash
   npm install
3. Start the Angular development server:
   ```bash
   ng serve
4. Access the frontend:
   - The frontend will be available at http://localhost:4200.

### Environment Configuration

Since the `src/environments/environment.ts` file contains the backend API URL and is not pushed to the repository, you will need to create and configure it manually:

1. **Navigate to the `src/environments/` folder** and create a new file called `environment.ts`:
   ```bash
   touch src/environments/environment.ts
2**Add the following content to environment.ts, replacing {backend-port} with the actual port number your backend is running on:
   ```bash
export const environment = {
   production: false,
   apiUrl: 'http://localhost:{backend-port}/api' // Replace with your actual backend URL
   };

