#!/bin/bash
# This bash script runs unit tests using dotnet test

# Set the start directory
startDir="tests/"
cd "$startDir" || exit 1

targetTest="Unit"

# Use find command to recursively search for folders containing "UnitTests"
find "$startDir" -type d -name "*${targetTest}Tests*" | while read -r testDir; do
    echo "$testDir"
    dotnet test "$testDir" --collect:"XPlat Code Coverage"
done
