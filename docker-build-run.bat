@echo off

:: Call the first batch file from the docker directory
call "docker-build.bat"

:: Call the second batch file from the docker directory
call "docker-run.bat"

echo All done!
