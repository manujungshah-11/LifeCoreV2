# LifeV2 — Life Insurance Portal

ASP.NET Core 8 Web API for a life insurance portal, built with **Clean Architecture**,
**Entity Framework Core + Microsoft SQL Server**, automated tests, Docker, and a
**Jenkins CI/CD** pipeline.

---

## Tech stack

| Concern         | Choice                                  |
|-----------------|-----------------------------------------|
| Runtime         | .NET 8 (LTS)                            |
| API             | ASP.NET Core Web API + Swagger          |
| Data access     | EF Core 8 (SQL Server provider)         |
| Database        | Microsoft SQL Server 2022               |
| Testing         | xUnit, Moq, FluentAssertions            |
| Containerisation| Docker + docker compose                 |
| CI/CD           | Jenkins (declarative `Jenkinsfile`)     |

## Solution layout

```
LifeV2/
├── src/
│   ├── LifeV2.Domain          # Entities, enums, repository interfaces (no dependencies)
│   ├── LifeV2.Application     # DTOs, services, business logic
│   ├── LifeV2.Infrastructure  # EF Core DbContext, MSSQL, repositories, migrations
│   └── LifeV2.API             # Controllers, middleware, Program.cs, config
├── tests/
│   ├── LifeV2.UnitTests       # Service-level unit tests (mocked repos)
│   └── LifeV2.IntegrationTests# End-to-end API tests (in-memory DB)
├── scripts/                   # DB + migration helper scripts
├── docs/                      # Architecture & developer setup docs
├── .vscode/                   # VS Code tasks, launch, recommended extensions
├── Dockerfile                 # Multi-stage build for the API
├── docker-compose.yml         # API + SQL Server for local dev
├── Jenkinsfile                # CI + CD pipeline
└── LifeV2.sln
```

Dependency rule: `API → Application → Domain` and `Infrastructure → Domain`.
The Domain layer depends on nothing. Business logic never references EF Core directly.

---

## Quick start

### Option A — Everything in Docker (recommended first run)

```bash
cp .env.example .env          # then edit the SA password
docker compose up --build
```

- API:      http://localhost:8080/swagger
- Health:   http://localhost:8080/health
- SQL Server listens on `localhost:1433` (sa / your .env password)

### Option B — Run the API locally, SQL Server in Docker

```bash
# 1. Start only the database
docker compose up -d mssql

# 2. Run the API (auto-applies migrations in Development)
dotnet run --project src/LifeV2.API
```

Open http://localhost:5080/swagger.

### Option C — Pure local (you provide SQL Server)

Update `src/LifeV2.API/appsettings.json` → `ConnectionStrings:DefaultConnection`,
then `dotnet run --project src/LifeV2.API`.

---

## Common commands

```bash
dotnet restore                       # restore packages
dotnet build                         # build the whole solution
dotnet test                          # run all tests
dotnet run --project src/LifeV2.API  # run the API

# EF Core migrations (uses pinned dotnet-ef from .config/dotnet-tools.json)
dotnet tool restore
./scripts/add-migration.sh AddSomething
./scripts/run-migrations.sh
```

In **Development** the API applies pending migrations on startup, so a fresh clone
+ a running SQL Server is enough to get tables created automatically.

---

## API endpoints (sample vertical slice: Policies)

| Method | Route                | Description          |
|--------|----------------------|----------------------|
| GET    | `/api/policies`      | List all policies    |
| GET    | `/api/policies/{id}` | Get one policy       |
| POST   | `/api/policies`      | Create a policy      |
| PUT    | `/api/policies/{id}` | Update a policy      |
| DELETE | `/api/policies/{id}` | Delete a policy      |

`Policies` is a complete reference slice (Controller → Service → Repository → EF).
Developers should copy this pattern for new domains (Claims, Quotes, Payments, ...).

---

## CI/CD (Jenkins)

The `Jenkinsfile` defines:

1. **Restore → Build → Test** (with `.trx` results + code coverage)
2. **Publish** build artifacts
3. **Docker build & push** (on `main` / `develop`)
4. **Deploy to Staging** automatically (`develop` branch)
5. **Manual approval → Deploy to Production** (`main` branch)

See `docs/DEVELOPER_SETUP.md` for the Jenkins agent prerequisites and credential IDs
you need to create.

---

## For developers

1. Clone the repo.
2. Install the .NET 8 SDK and Docker.
3. Open the folder in VS Code (accept the recommended extensions prompt).
4. `docker compose up -d mssql` then press F5 to run/debug the API.
5. Build your feature by mirroring the **Policies** slice across the four layers.
6. Add a migration if you change entities, write tests, open a PR.

More detail in `docs/`.
