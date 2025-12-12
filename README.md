📚 Books API – Clean Architecture (ASP.NET Core)

Dit project is een voorbeeld van een ASP.NET Core Web API gebouwd volgens het Clean Architecture-principe.
De API beheert boeken en gebruikt Entity Framework Core, SQLite, Serilog, Repositories, Services, en een duidelijke scheiding tussen Domain, Application, Infrastructure en Presentation (WebApi).

🚀 Features

✔ Clean Architecture (Domain → Application → Infrastructure → WebApi)

✔ Entity Framework Core + SQLite

✔ Repository Pattern

✔ Dependency Injection

✔ Serilog structured logging

✔ CRUD endpoints voor:

  📘 Books

✔ Gescheiden DTO’s en entiteiten

✔ Async/await overal toegepast

🏛 Clean Architecture Structuur

De oplossing bestaat uit vier projecten:
src/
 ├─ WebApi/                 → Controllers, DI, Request Pipeline, Serilog
 ├─ Application/            → DTO's, Interfaces, Services (Use Cases)
 ├─ Domain/                 → Entities, Business Rules (geen EF afhankelijkheid)
 └─ Infrastructure/         → EF Core, DbContext, Repository implementaties

🌐 WebApi (Presentation Layer)

Controllers

Serilog configuratie

Routing

Dependency Injection

🧠 Application Layer

Business logic (Use Cases)

DTO’s (Data Transfer Objects)

Repository interfaces

📦 Domain Layer

Pure C# entiteiten

Geen afhankelijkheid van frameworks

🗄 Infrastructure Layer

EF Core DbContext

Repository implementaties

SQLite data-opslag

🧪 Endpoints
📘 Books
Methode	Endpoint	Omschrijving
GET	/api/books	Haal alle boeken op
GET	/api/books/{id}	Haal één boek op
POST	/api/books	Maak nieuw boek aan
PUT	/api/books/{id}	Werk een boek bij
DELETE	/api/books/{id}	Verwijder een boek

🛠 Installatie & Gebruik
1. Clone de repository
```
  git clone https://github.com/Jorrit-vd-Heide/BookStoreAPI.git
  cd BookStoreAPI
```

2. Restore packages
```
  dotnet restore
```

3. Database migratie uitvoeren
Als je EF Core migraties wilt uitvoeren:
```
  cd src/WebApi
  dotnet ef database update
```
De database (books.db) wordt automatisch aangemaakt in de WebApi map.

4. Build en start de API
```
  dotnet build
  dotnet run 
```
De API draait vervolgens op:
```
https://localhost:5025
```

5. Testen met Swagger
```
https://localhost:5025/swagger
```

📜 Logging (Serilog)

De API gebruikt Serilog voor gestructureerde logging, met:

Console output

Dagelijkse logfiles in:
```
WebApi/logs/log-yyyyMMdd.txt
```

💾 Database

SQLite databasebestand: books.db

EF Core Migrations worden opgeslagen in Infrastructure/Migrations

🧱 Technologieën

.NET 8

ASP.NET Core Web API

Entity Framework Core

SQLite

Serilog

Clean Architecture

Repository Pattern

C# 12

📂 Structuuroverzicht
src/
 ├─ WebApi/
 │   ├─ Controllers/
 │   ├─ Program.cs
 │   ├─ appsettings.json
 │   └─ logs/
 │
 ├─ Application/
 │   ├─ DTOs/
 │   ├─ Interfaces/
 │   └─ Services/
 │
 ├─ Domain/
 │   └─ Entities/
 │
 └─ Infrastructure/
     ├─ Persistence/
     ├─ Repositories/
     └─ Migrations/
