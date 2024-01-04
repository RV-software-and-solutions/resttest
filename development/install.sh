#!/bin/bash
# Enhanced bash script to run unit tests using dotnet test

version="7.0"

echo "Installing dotnet-sdk-$version"
sudo apt install dotnet-sdk-$version -Y

echo "Installing dotnet-runtime-$version"
sudo apt install dotnet-runtime-$version -Y

echo "Installing aspnetcore-runtime-$version"
sudo apt install aspnetcore-runtime-$version -Y
