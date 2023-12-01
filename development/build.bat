@echo off
rem Enhanced batch file to run unit tests using dotnet test

setlocal enabledelayedexpansion

rem Set the start directory and configuration
set startDir=../src/Web
set buildConfig=%1
if "%buildConfig%"=="" set buildConfig=Release

echo Changing directory to %startDir%
cd /d "%startDir%" || exit /b 1

echo Restoring projects...
dotnet restore || exit /b 1

echo Building projects in %buildConfig% configuration...
dotnet build -c %buildConfig% --no-restore --nologo || exit /b 1

cd /d %~dp0