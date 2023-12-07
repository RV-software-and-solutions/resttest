#!/bin/bash
# Enhanced bash script to run unit tests using dotnet test

# Set the start directory and configuration
startDir="../src/Web"
buildConfig=$1
if [ -z "$buildConfig" ]; then
    buildConfig="Release"
fi

echo "Changing directory to $startDir"
cd "$startDir" || exit 1

echo "Restoring projects..."
dotnet restore || exit 1

echo "Building projects in $buildConfig configuration..."
dotnet build -c "$buildConfig" --no-restore --nologo || exit 1

# Return to the original directory
cd "$(dirname "$0")"
