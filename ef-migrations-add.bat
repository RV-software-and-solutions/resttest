@echo off
REM Check if an argument was provided
IF "%~1"=="" (
    echo No migration name provided
    exit /b 1
)

cd .\src

dotnet ef migrations add %1 --project .\Infrastructure\Infrastructure.csproj --startup-project .\Web\Web.csproj --output-dir Persistence\Migrations
