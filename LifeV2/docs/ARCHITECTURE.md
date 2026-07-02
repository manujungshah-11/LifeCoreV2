# Architecture

LifeV2 follows **Clean Architecture** to keep business rules independent of frameworks,
the database, and the UI.

## Layers

### Domain (`src/LifeV2.Domain`)
The core. Plain C# entities (`Policy`, `Policyholder`, `Beneficiary`), enums, and
repository **interfaces**. No NuGet dependencies. Nothing here knows about EF Core,
ASP.NET, or SQL Server.

### Application (`src/LifeV2.Application`)
Use cases / business logic. Contains DTOs, service interfaces and implementations
(`IPolicyService` / `PolicyService`), validation, and mapping. Depends only on Domain.

### Infrastructure (`src/LifeV2.Infrastructure`)
Implementation details: the EF Core `ApplicationDbContext`, entity configurations,
repository implementations, and migrations. Implements the Domain interfaces.
This is the only layer that references SQL Server.

### API (`src/LifeV2.API`)
The entry point. Thin controllers that call Application services, exception-handling
middleware, Swagger, health checks, dependency injection wiring, and configuration.

## Dependency direction

```
API ─▶ Application ─▶ Domain ◀─ Infrastructure
        │                            ▲
        └───────── (interfaces) ─────┘
```

Dependencies always point inward toward the Domain. Outer layers depend on inner
layers, never the reverse. The Infrastructure layer satisfies Domain interfaces via DI.

## Adding a new feature (e.g. Claims)

1. **Domain:** add `Claim` entity + `IClaimRepository`.
2. **Application:** add DTOs, `IClaimService` + `ClaimService`, register in `AddApplication`.
3. **Infrastructure:** add `ClaimConfiguration`, `ClaimRepository`, register in `AddInfrastructure`, add a migration.
4. **API:** add `ClaimsController`.
5. **Tests:** unit-test the service, integration-test the controller.
