@echo off
setlocal

cd ..\src

set JSON_FILE=.\Web\appsettings.json

for /f "delims=" %%i in ('powershell -Command "Get-Content '%JSON_FILE%' | ConvertFrom-Json | Select -ExpandProperty ConnectionStrings | Select -ExpandProperty DefaultConnection"') do set DEFAULT_CONNECTION=%%i

echo Default Connection String: %DEFAULT_CONNECTION%

set connectionString=%DEFAULT_CONNECTION%

dotnet ef database update --project .\Infrastructure\Infrastructure.csproj --startup-project .\Web\Web.csproj --connection %connectionString%

endlocal