# Developer & DevOps Setup

## Local prerequisites
- .NET 8 SDK
- Docker Desktop (or Docker Engine + compose)
- VS Code with the C# Dev Kit extension (auto-recommended by `.vscode/extensions.json`)

## First run
```bash
git clone <repo-url> LifeV2
cd LifeV2
cp .env.example .env        # set a strong SA password
docker compose up -d mssql  # start SQL Server
dotnet run --project src/LifeV2.API
```

## Jenkins setup (DevOps)

### Agent requirements
Each build agent needs:
- .NET 8 SDK on `PATH`
- Docker engine accessible to the Jenkins user
- `git`, `ssh`

### Required Jenkins credentials
Create these under **Manage Jenkins → Credentials** and match the IDs in the `Jenkinsfile`:

| Credential ID                   | Type              | Used for                       |
|---------------------------------|-------------------|--------------------------------|
| `lifev2-registry-credentials`   | Username/password | Push images to the registry    |
| `lifev2-staging-ssh`            | SSH private key   | Deploy to the staging host     |
| `lifev2-prod-ssh`              | SSH private key   | Deploy to the production host  |

### Pipeline configuration
1. Create a **Multibranch Pipeline** job pointing at the repo.
2. Jenkins auto-discovers the `Jenkinsfile`.
3. Update the `environment` block variables (`REGISTRY`, `IMAGE_NAME`, host names).
4. Recommended plugins: *Docker Pipeline*, *SSH Agent*, *JUnit*, *Pipeline Utility Steps*.

### Branch → environment mapping
| Branch    | Pipeline behaviour                                   |
|-----------|------------------------------------------------------|
| `develop` | Build + test + push image + **auto-deploy to staging** |
| `main`    | Build + test + push image + **manual approval → prod** |
| others    | Build + test only                                    |

### Deploy hosts
Each deploy target (`/opt/lifev2`) should contain a `docker-compose.yml` referencing
`${REGISTRY}/lifev2-api:${IMAGE_TAG}` plus its own SQL Server connection settings
provided via environment variables / secrets (never commit production secrets).

## Database
- Local/dev: SQL Server in Docker (`docker compose up -d mssql`).
- Migrations live in `src/LifeV2.Infrastructure/Persistence/Migrations`.
- Dev applies migrations automatically on API startup.
- For non-dev environments run `./scripts/run-migrations.sh` as a deploy step, or
  generate an idempotent SQL script with:
  `dotnet ef migrations script --idempotent -o migrate.sql`.

## Secrets management
- `appsettings.json` ships with a **placeholder** connection string for local dev only.
- Never commit real passwords. Use environment variables (`ConnectionStrings__DefaultConnection`),
  user-secrets locally (`dotnet user-secrets`), or your secret store in CI/CD.
