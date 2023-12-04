#!/bin/bash
# This Bash script runs unit tests using dotnet test

startDir="../tests"
cd "$startDir" || exit 1

targetTest="Integration"

# Use find command to recursively search for folders containing "unittests"
find "$startDir" -type d -name "*$targetTest*Tests*" | while read -r dir; do
    echo "$dir"
    dotnet test "$dir" --collect:"XPlat Code Coverage"
done
