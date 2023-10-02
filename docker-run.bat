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
set PORT=5050

docker images | findstr /C:"%IMAGE_NAME%:%IMAGE_TAG%"
if errorlevel 1 (
    echo Image %IMAGE_NAME%:%IMAGE_TAG% does NOT exist.
) else (
    echo Image %IMAGE_NAME%:%IMAGE_TAG% exists.
)

docker run -d --name %IMAGE_NAME% -p %PORT%:80 %IMAGE_NAME%
