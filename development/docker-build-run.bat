@echo off

set IMAGE_NAME=resttest

call "./development/docker-build.bat"

:: Call the second batch file from the docker directory
call "./development/docker-run.bat"

cd ..
echo All done!
