# ProductionTracker

ProductionTracker is a compact backend-oriented demo project focused on modeling
product catalog and inventory operations using an explicit order-based workflow.

The project demonstrates how to structure a backend system with clear responsibility boundaries,
where application logic coordinates workflows and the domain encapsulates state and rules.

This is an intentionally backend-only project designed for architectural review and discussion.

---

## What this project demonstrates

- Explicit order-based workflow for inventory operations
- Clear separation between API, application, and domain layers
- Single entry point for inventory execution
- Thin controllers and explicit business flows
- Readable, review-friendly codebase

This project intentionally avoids unnecessary abstractions and infrastructure concerns.

---

## Core Architectural Idea

All inventory changes are driven by explicit **orders**.

An order represents an operational intent:
- what action should be performed
- on which product
- with which parameters

Inventory does not expose individual operation methods.
Instead, it executes orders through a single entry point.

This makes system behavior explicit, predictable, and easy to reason about.

---

## Request Flow

HTTP request
↓
OrderRequest (application-level request)
↓
Order (domain object)
↓
Inventory.Execute(order)
↓
Inventory state updated


Each layer has a single, clear responsibility:
- API translates HTTP to application requests
- Application layer creates and dispatches orders
- Domain layer owns rules and state transitions

---

## Features

- Read-only product catalog initialized from seed data
- Inventory operations:
  - register product
  - receive product
  - issue product
- Development-only diagnostic access to inventory state
- REST API with Swagger UI

---

## Project Structure

- **API**
  - HTTP translation only
  - No business logic
- **Application**
  - Order creation and workflow coordination
- **Domain**
  - Inventory state and business rules
  - Order semantics

Catalog data is loaded at startup and treated as immutable during runtime.

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

Returns available catalog positions

Orders

POST /api/orders

Creates and executes an inventory order

Diagnostics (development only)

GET /api/debug/inventory

Returns current inventory state

Seed Data

Catalog data is loaded from a JSON file during application startup:
Seed/catalog.demo.json

The seed mechanism is intended for development and demonstration purposes only.

What is intentionally NOT implemented

Authentication and authorization

Frontend UI

Cloud infrastructure

Background processing

These concerns are excluded to keep the focus on core domain modeling and execution flow.

Project Status

MVP

Backend-only

Actively evolving

Purpose

This project is intended as:

an architectural case study

a backend demo for code review

a discussion basis for system design interviews


---


