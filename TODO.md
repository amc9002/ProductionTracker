# ProductionTracker — TODO / Roadmap

This file tracks the current state of the project and the next concrete steps.
It is intentionally explicit to avoid losing context between iterations.

---

## Current State (Baseline)

Status: **Working prototype**

Implemented:
- Application starts successfully
- Swagger UI available
- Demo product catalog loaded at startup (in-memory)
- Read-only catalog endpoint:
  - GET /api/catalog
- Clear separation of layers:
  - Api
  - Application
  - Domain
- Demo seed logic explicitly separated from domain logic
- Git repository cleaned from build artifacts (bin/obj)
- Documentation added for public classes and methods

Not implemented yet (by design):
- Persistent storage for inventory
- Full order workflow
- Authentication / authorization
- Frontend

---

## Design Principles (Do Not Break)

These rules define the project direction:

- Domain logic must not depend on infrastructure
- Demo / seed logic must be clearly marked as non-production
- API controllers are thin (translation only)
- Business rules live in Application / Domain
- Prefer explicit commands over generic CRUD
- In-memory implementations are acceptable for demo purposes

---

## Phase 1 — Inventory Commands (Highest Priority)

	Goal: demonstrate command-style backend logic without persistence.

### 1. Inventory Receive (ADD stock)

Target:

	- POST /api/inventory/receive

	Request example:
	```json
	{
	  "productId": "guid",
	  "quantity": 10
	}

Rules:

	productId must exist in catalog
	quantity must be > 0
	inventory stored in-memory
	no database usage

Tasks:

	 Define InventoryReceiveRequest DTO
	 Add InventoryApplicationService.Receive(...)
	 Validate input in application layer
	 Add InventoryController
	 Return meaningful HTTP responses (400 / 404 / 200)

### 2. Inventory Issue (REMOVE stock)

	Target:

	POST /api/inventory/issue

	Request example:

	{
	  "productId": "guid",
	  "quantity": 5
	}


Rules:

	cannot issue more than available
	clear error if insufficient quantity
	no silent failures

Tasks:

	 Define InventoryIssueRequest DTO
	 Add InventoryApplicationService.Issue(...)
	 Handle insufficient stock explicitly
	 Map domain errors to HTTP responses

### 3. Diagnostic Inventory View (Development Only)

Target:

	GET /api/debug/inventory

Rules:

	development / diagnostic purpose only
	returns current in-memory inventory state
	not part of public API contract

Tasks:

	 Expose read-only inventory snapshot
	 Clearly mark endpoint as debug-only
	 Document in README

## Phase 2 — Orders (Post-Inventory)

	Goal: show how commands orchestrate domain logic.

Planned direction:

	Order represents an intent (not CRUD entity)
	Order processing may affect inventory
	Persistence optional at this stage

Tasks (not started):

	 Clarify order lifecycle
	 Decide: synchronous vs async processing
	 Define OrderApplicationService
	 Integrate with inventory logic

## Phase 3 — Persistence (Optional / Later)
	Goal: show infrastructure separation, not production readiness.

Ideas:

	Replace in-memory inventory with database-backed implementation
	Keep domain unchanged
	Use PostgreSQL (already prepared)

Tasks:

	Introduce repository abstractions
	Implement PostgreSQL inventory repository
	Keep in-memory implementation for tests/demo

## Phase 4 — Hardening & Polish (Optional)

	 Improve README clarity
	 Add minimal unit tests (Application layer)
	 Improve error handling consistency
	 Optional Docker Compose for full stack startup

Notes to Future Self

	Do not rush infrastructure
	A working, well-explained core is more valuable than feature count
	Each phase should end with a clean commit
	If stuck, re-read this file before adding new concepts