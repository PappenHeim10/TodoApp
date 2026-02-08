# 1. Erstelle die leere Solution (der Container)
dotnet new sln -n TodoApp

# 2. Erstelle das Core-Projekt (Klassenbibliothek)
# Hier kommen deine Entitäten (DB-Modelle) und Interfaces rein.
dotnet new classlib -n TodoApp.Core

# 3. Erstelle das DataAccess-Projekt (Klassenbibliothek)
# Hier kommt Entity Framework und die Datenbank-Konfiguration rein.
dotnet new classlib -n TodoApp.DataAccess

# 4. Erstelle das API-Projekt (Web API)
# Hier sind deine Controller und DTOs.
dotnet new webapi -n TodoApp.Api

# 5. Füge die Projekte zur Solution hinzu
dotnet sln add TodoApp.Core/TodoApp.Core.csproj
dotnet sln add TodoApp.DataAccess/TodoApp.DataAccess.csproj
dotnet sln add TodoApp.Api/TodoApp.Api.csproj

# 6. Setze die Abhängigkeiten (Referenzen)
# DataAccess muss Core kennen (um Entitäten zu speichern)
dotnet add TodoApp.DataAccess/TodoApp.DataAccess.csproj reference TodoApp.Core/TodoApp.Core.csproj

# API muss Core kennen (für Interfaces)
dotnet add TodoApp.Api/TodoApp.Api.csproj reference TodoApp.Core/TodoApp.Core.csproj

# API muss DataAccess kennen (nur für die Dependency Injection beim Start)
dotnet add TodoApp.Api/TodoApp.Api.csproj reference TodoApp.DataAccess/TodoApp.DataAccess.csproj

echo "Projektstruktur erfolgreich erstellt! 🚀"