@echo off

set IMAGE_NAME=resttest

:: Check if a container named '%IMAGE_NAME%' exists
docker ps -a --format "{{.Names}}" | findstr /B /C:"%IMAGE_NAME%" >nul

:: Check the error level returned by findstr
if errorlevel 1 (
    echo Container '%IMAGE_NAME%' does NOT exist.
) else (
    echo Container '%IMAGE_NAME%' exists.
    docker rm -f %IMAGE_NAME%
    echo Container '%IMAGE_NAME%' delete.
)

set IMAGE_TAG=latest
set HTTP_PORT=5050
set HTTPS_PORT=5051

docker images | findstr /C:"%IMAGE_NAME%:%IMAGE_TAG%"
if errorlevel 1 (
    echo Image %IMAGE_NAME%:%IMAGE_TAG% does NOT exist.
) else (
    echo Image %IMAGE_NAME%:%IMAGE_TAG% exists.
)

set connectionString=Host=pg-local-db-1:5432;Username=postgres;Password=root123;Database=RestTest;

docker run -d -e ASPNETCORE_ENVIRONMENT=Development -e ConnectionStrings__DefaultConnection=%connectionString% --name %IMAGE_NAME% -p %HTTPS_PORT%:443 -p %HTTP_PORT%:80 %IMAGE_NAME%

docker network connect %IMAGE_NAME% %IMAGE_NAME%