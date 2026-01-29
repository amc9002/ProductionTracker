# ProductionTracker — TODO / Roadmap

This file tracks the current state of the project and the next concrete steps.
It is intentionally explicit to preserve architectural intent between iterations.

---

## Current State (Baseline)

Status: **Working backend prototype**

Implemented:

- Application starts successfully
- Swagger UI available
- Demo product catalog loaded at startup (in-memory)
- Read-only catalog endpoint:
  - GET /api/catalog
- Explicit order-based inventory workflow
- Clear separation of layers:
  - API
  - Application
  - Domain
- Inventory logic executed through a single entry point
- Demo / seed logic explicitly separated from domain logic
- Git repository cleaned from build artifacts (bin/obj)
- Documentation added for public classes and methods

Intentionally not implemented:

- Authentication / authorization
- Frontend
- Cloud infrastructure
- Background processing

---

## Design Principles (Do Not Break)

These rules define the architectural boundaries of the project:

- Domain logic must not depend on infrastructure
- Inventory state changes are driven by explicit orders
- Application layer coordinates workflows, not state
- API controllers are thin (HTTP translation only)
- Demo / seed logic must be clearly marked as non-production
- Prefer explicit commands over generic CRUD
- In-memory implementations are acceptable for demo purposes

---

## Phase 1 — Inventory Execution (Completed)

Goal: demonstrate explicit command-style backend logic without persistence.

Implemented:

- Inventory execution via explicit orders
- Supported actions:
  - Register product
  - Receive product
  - Issue product
- Inventory exposed through a single execution method
- Clear error handling for:
  - unknown product
  - insufficient stock
  - invalid quantities
- In-memory inventory implementation

Notes:

- Inventory does not expose individual operation methods publicly
- All state changes are driven by order semantics

---

## Phase 2 — Orders (Completed)

Goal: model intent-based operations instead of CRUD-style updates.

Implemented:

- Order represents an operational intent, not a data record
- Order contains:
  - action
  - target product
  - parameters
  - execution status
- OrderApplicationService:
  - creates orders
  - delegates execution to inventory
- InventoryApplicationService:
  - executes orders
  - controls order status transitions

Notes:

- Order persistence is intentionally omitted at this stage
- Order lifecycle is synchronous and explicit

---

## Phase 3 — Persistence (Next Step)

Goal: demonstrate infrastructure separation without changing domain logic.

Planned direction:

- Introduce persistence as an infrastructure concern
- Keep domain model unchanged
- Use PostgreSQL for inventory and order storage

Planned tasks:

- Introduce repository abstractions
- Implement PostgreSQL-backed inventory repository
- Persist orders for inspection and debugging
- Keep in-memory implementation for tests and demo mode

---

## Phase 4 — API Completion (After Persistence)

Goal: expose a complete, review-ready backend API.

Planned tasks:

- Add Orders API endpoint:
  - POST /api/orders
- Map order execution results to HTTP responses
- Keep API thin and declarative
- Ensure Swagger reflects actual execution flow

---

## Phase 5 — Hardening & Polish (Optional)

Optional improvements:

- Improve README clarity and flow diagrams
- Add minimal unit tests (Application layer)
- Improve error handling consistency
- Optional Docker Compose for local PostgreSQL startup

---

## Notes to Future Self

- Do not rush infrastructure
- A small, well-explained core is more valuable than feature count
- Each phase should end with a clean, reviewable commit
- If architecture starts drifting, stop and re-read this file
