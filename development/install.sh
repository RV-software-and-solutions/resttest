#!/bin/bash
# Enhanced bash script to run unit tests using dotnet test

version="7.0"

sudo apt-get update -y
echo "Installing dotnet-sdk-$version"
sudo apt install -y dotnet-sdk-$version

echo "Installing dotnet-runtime-$version"
sudo apt install -y dotnet-runtime-$version

echo "Installing aspnetcore-runtime-$version"
sudo apt install -y aspnetcore-runtime-$version
