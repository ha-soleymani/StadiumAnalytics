# 🏟️ Stadium Analytics API

A modular, scalable, and testable .NET 8 Web API for ingesting and analyzing real-time sensor data from stadium gates. Built using Clean Architecture principles, Entity Framework Core, and background services for event simulation.

---

## 📐 Architecture

This solution follows the **Clean Architecture** pattern with the following projects:

- **StadiumAnalytics.WebAPI** – ASP.NET Core Web API entry point
- **StadiumAnalytics.Domain** – Core business logic and enterprise entities
- **StadiumAnalytics.Application** – Use cases and services
- **StadiumAnalytics.Infrastructure** – EF Core persistence and External concerns (DB, logging)
- **StadiumAnalytics.Background - Simulate events to be consumed asynchronously by the application
- **StadiumAnalytics.Tests** – Unit tests for all layers

---

## 🚀 Features

- ✅ Clean Architecture with separation of concerns
- ✅ SQLite + EF Core for lightweight persistence
- ✅ Background service simulating real-time gate events
- ✅ Health checks with custom `DbContext` probe
- ✅ Swagger UI for API exploration
- ✅ Unit tests for service, repository, controller, and background worker

---

## 🛠️ Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022+ or VS Code

### Clone the Repository

```bash
git clone https://github.com/ha-soleymani/StadiumAnalytics.git
cd StadiumAnalytics
