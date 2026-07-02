#!/usr/bin/env bash
# Applies EF Core migrations against the configured database.
# Usage: ./scripts/run-migrations.sh
set -euo pipefail

dotnet tool restore 2>/dev/null || dotnet tool install --global dotnet-ef --version 8.* || true

dotnet ef database update \
  --project src/LifeV2.Infrastructure/LifeV2.Infrastructure.csproj \
  --startup-project src/LifeV2.API/LifeV2.API.csproj
