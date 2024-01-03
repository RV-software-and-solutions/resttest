@echo off
rem This batch file runs unit tests using dotnet test
setlocal enabledelayedexpansion
set startDir=./../tests
cd /d "%startDir%"

set targetTest="Integration"


rem Use dir command to recursively search for folders containing "unittests"
for /d /r "%startDir%" %%a in (*%targetTest%Tests*) do (
    echo %%a
    dotnet test %%a --collect:"XPlat Code Coverage"
)
