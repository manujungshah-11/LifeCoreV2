#!/usr/bin/env bash
# Creates a new EF Core migration.
# Usage: ./scripts/add-migration.sh <MigrationName>
set -euo pipefail

if [ -z "${1:-}" ]; then
  echo "Usage: ./scripts/add-migration.sh <MigrationName>"
  exit 1
fi

dotnet ef migrations add "$1" \
  --project src/LifeV2.Infrastructure/LifeV2.Infrastructure.csproj \
  --startup-project src/LifeV2.API/LifeV2.API.csproj \
  --output-dir Persistence/Migrations
