@echo off
rem Enhanced batch script to run security vulnerability scan using dotnet

set startDir=..\

echo Changing directory to %startDir%
cd /d "%startDir%" || exit /b 1

echo Scanning for security vulnerabilities...
dotnet list package --vulnerable --include-transitive || exit /b 1
