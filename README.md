# skittles
Skittles

from skittles\src\api directory:

`cd src\api`
`dotnet tool update --global dotnet-ef`

`dotnet ef migrations add MigrationName --project Migrations --startup-project Server`
(to remove the migration, use `dotnet ef migrations remove --project Migrations --startup-project Server`)

`dotnet ef database update --project Migrations --startup-project Server`
