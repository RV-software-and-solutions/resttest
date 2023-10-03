@echo off
REM Check if an argument was provided
IF "%~1"=="" (
    echo Docker compose action up/down argument not provided!
    exit /b 1
)

IF "%~2"=="" (
    echo Network name not provided!
    exit /b 2
)

set NETWORK_NAME=%2
REM Check if the argument is "up" or "down"
IF /I "%~1"=="up" (
    echo You selected UP.
    docker-compose -f .\pg.yml %1 --detach
) ELSE IF /I "%~1"=="down" (
    echo You selected DOWN.
    docker-compose -f .\pg.yml %1
) ELSE (
    echo Invalid argument! Please provide either "up" or "down".
    exit /b 1
)
