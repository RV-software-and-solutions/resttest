
cd .\src
set connectionString=Host=127.0.0.1:5432;Username=postgres;Password=root123;Database=RestTest;

dotnet ef database update 0 --project .\Infrastructure\Infrastructure.csproj --startup-project .\Web\Web.csproj --connection %connectionString%