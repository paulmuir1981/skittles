# skittles
Skittles

from skittles directory:
dotnet ef migrations add migrationname --project src/api/Migrations --startup-project src/api/Server
dotnet ef database update --project Migrations --startup-project Server

can we do it from src/api?