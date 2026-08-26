# # TFM-Manager – Codex Instructions

## Project

TFM-Manager is a business management application for the Társasház-FM company group.

The application manages companies, employees, partners/properties, users, tasks, invoicing and reporting.

The project is under active development. Existing architectural and business decisions must be preserved unless the task explicitly requires changing them.

## Technology

- .NET 10
- C#
- Entity Framework Core 10
- ASP.NET Core Web API
- Azure SQL Database
- Visual Studio is the primary IDE
- Git is used for source control

## Architecture

The solution is separated into projects with distinct responsibilities.

Important rules:

- Domain entities and business-domain concepts must remain independent from UI concerns.
- Database access belongs to the Data layer.
- The API is responsible for server-side data access.
- Desktop clients must NOT connect directly to Azure SQL Database.
- Clients communicate with the system through the API.
- Avoid introducing project dependencies that violate the existing solution architecture.

Before adding a new dependency between projects, inspect the current project references and architecture.

## Entity Framework Core

The project uses Entity Framework Core 10 with Azure SQL Database / SQL Server provider.

Rules:

- EF Core migrations belong to `TFMManager.Data`.
- `TFMManager.Api` is the startup project when migrations are executed.
- Migrations are normally created and applied manually from Visual Studio NuGet Package Manager Console.
- Do NOT create or apply a migration unless explicitly requested.
- Do NOT modify an existing migration unless explicitly requested.
- Entity configuration should follow the configuration pattern already used by the project.
- Configure database constraints explicitly where business rules require them.
- Preserve existing naming and mapping conventions.

Development database:

- `TFMManager_Dev`

Production uses a separate database and connection string.

## Domain Modelling Rules

Prefer database-backed master data over C# enums for business-maintained classifications.

Do not introduce enums for business master data unless explicitly requested.

Examples of database-backed master data include concepts such as:

- employee types
- partner types
- service types
- contact types
- location types
- document types

Business records should generally be deactivated rather than physically deleted.

Use the existing `IsActive` pattern where appropriate.

Do not introduce hard deletes for entities that are intended to retain history unless explicitly requested.

## Companies

A company:

- has a unique tax number;
- can have multiple bank accounts;
- can have multiple accounting contacts;
- can have multiple employees;
- can have multiple partners through business relationships.

There is no separate short-name requirement unless already present in the implementation.

Company representatives are employees. Do not create a separate Company–Representative domain model unless explicitly requested.

## Employees

An employee can belong to multiple companies.

Company membership is represented through the appropriate company–employee relationship.

Rules:

- an employee tax identification number is required;
- one Company–Employee pair may have only one active relationship at a time;
- employee type is master data;
- a representative can represent multiple companies.

Salary information is historical.

The salary history model must support:

- effective periods;
- employment registration type / working hours;
- net salary;
- gross salary;
- contributions;
- total company cost.

Only one salary record should be active/effective for the relevant relationship at a given time according to the existing business rules.

## Partners

A Partner can represent several types of entities, including:

- condominium / property;
- private individual;
- company;
- institution;
- other partner type.

Partner type is database master data.

A Partner can be connected to multiple companies.

The Partner–Company relationship may also depend on ServiceType.

Only one active relationship should exist for the same:

`Partner + Company + ServiceType`

combination.

## Partner Locations

Partner locations are separate records.

Required business concepts include:

- PartnerId
- Name
- LocationType
- IsActive

`IsPrimary` is part of the location model where defined by the existing specification.

## Partner Contacts

Partner contacts are separate records.

Required business concepts include:

- PartnerId
- Name
- ContactType
- IsPrimary
- IsActive

## Bank Accounts

A company may have multiple bank accounts.

Only one active default bank account may exist for a company at a time.

Do not implement this rule only in the UI. Preserve or add appropriate domain/database validation when implementing the feature.

## Accounting Contacts

A company can have multiple accounting contacts.

Do not restrict this relationship to one contact unless explicitly required.

## Documents

Documents are represented by separate domain models according to their owner, including concepts such as:

- CompanyDocument
- EmployeeDocument
- PartnerDocument
- PartnerLocationDocument

Binary document content is not intended to be stored directly in Azure SQL Database.

Documents are stored on NAS/file storage. Database records store the metadata and relative path required to locate them.

Do not redesign document storage without explicit instruction.

## Users and Authorization

The login email address is used as the username.

A User must be associated with an Employee according to the application specification.

Application authorization levels include:

- Employee
- Supervisor
- Manager
- Admin

Users are not tied to a single company.

Permissions can be module-based.

Only Admin users may create users unless the specification is explicitly changed.

Do not invent additional roles or permission levels.

## General Implementation Rules

Before changing code:

1. Inspect the relevant existing projects and files.
2. Identify existing conventions and reuse them.
3. Check whether an equivalent pattern already exists elsewhere in the solution.
4. Make the smallest coherent change required for the task.

When implementing code:

- Follow existing C# naming conventions.
- Enable and respect nullable reference types.
- Avoid unnecessary abstractions.
- Avoid speculative functionality.
- Do not add packages unless they are required.
- Do not replace existing packages or architectural patterns without explicit instruction.
- Do not silently change business rules.
- Do not implement functionality that was not requested.

If a business rule is ambiguous, preserve the existing implementation rather than inventing a new rule.

## Build and Validation

After code changes, where possible:

- restore dependencies if necessary;
- build the affected project or solution;
- run relevant tests;
- report compilation or test failures.

Do not hide warnings or failures.

Do not modify unrelated code solely to make warnings disappear.

## Database Safety

Never:

- delete a database;
- drop tables;
- execute destructive SQL;
- apply production migrations;
- change production connection strings;
- connect to a production database;

unless explicitly instructed.

Do not commit secrets, passwords, tokens or connection-string credentials into source control.

## Git

Keep changes focused on the requested task.

Do not:

- rewrite Git history;
- force push;
- delete branches;
- commit unrelated modifications;

unless explicitly requested.

Before making a broad refactor, inspect the current Git diff and repository state.

## Task Execution

Treat each Codex request as a narrowly scoped development task.

For every task:

- first inspect the relevant code;
- state important assumptions when necessary;
- implement only the requested scope;
- build/test where appropriate;
- summarize the files changed;
- report unresolved issues.

When the requested change conflicts with these instructions or with an established project pattern, flag the conflict instead of silently redesigning the system.