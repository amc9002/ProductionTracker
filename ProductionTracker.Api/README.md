# ProductionTracker

ProductionTracker is a small backend-oriented demo project focused on modeling a product catalog and inventory operations.
The project demonstrates a clean separation between API, application logic, and domain logic, without introducing unnecessary complexity.

The application is intentionally backend-only and designed as a compact, readable codebase suitable for review.

---

## Features

- Read-only product catalog initialized from seed data
- Inventory operations (receive and issue products)
- Development-only diagnostic access to inventory state
- REST API with Swagger UI
- Clear separation of responsibilities between layers

---

## Architectural Overview

The project follows a simple layered approach:

- API controllers act only as HTTP translators
- Application services contain all business decision logic
- Domain layer encapsulates core business rules and state
- Catalog data is loaded at application startup and treated as immutable during runtime

This approach keeps the API thin and the domain logic explicit and testable.

---

## Getting Started

### Prerequisites

- .NET SDK 7.0 or later

### Running the application

```bash
dotnet run
 
After startup, Swagger UI will be available at:
https://localhost:<port>/swagger

API Endpoints (MVP)
Catalog

GET /api/catalog
Returns the list of available catalog positions.

Inventory

POST /api/inventory/receive
Registers receiving products into inventory.

POST /api/inventory/issue
Registers issuing products from inventory.

Diagnostics

GET /api/debug/inventory
Returns current inventory state (development and diagnostic purposes only).

Seed Data

Catalog data is loaded from a JSON file during application startup:
Seed/catalog.demo.json

The seed mechanism is intended for development and demonstration purposes only.

Project Status

MVP

Backend-only

No authentication

No frontend

Actively evolving

Purpose

This project is intended as a learning exercise, an architectural sandbox, and a compact backend example suitable for code review.

