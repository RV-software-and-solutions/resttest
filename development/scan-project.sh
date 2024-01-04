#!/bin/bash
# Enhanced bash script to run unit tests using dotnet test
startDir="."

echo "Changing directory to $startDir"
cd "$startDir" || exit 1

echo "Scanning for security vulnerabilities..."
dotnet list package --vulnerable --include-transitive || exit 1
