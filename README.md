# skittles
Skittles

from skittles/src/api directory:
dotnet tool update --global dotnet-ef
dotnet ef migrations add migrationname --project Migrations --startup-project Server
dotnet ef database update --project Migrations --startup-project Server
