@echo off

set IMAGE_NAME=resttest

call "docker-build.bat"
cd ../development

:: Call the second batch file from the docker directory
call "docker-run.bat"

echo All done!
