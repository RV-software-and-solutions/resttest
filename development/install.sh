#!/bin/bash
# Enhanced bash script to run unit tests using dotnet test

sdk="7.0"
version="7.0"

sudo apt-get update -y
echo "Installing dotnet-sdk-$sdk"
sudo apt install -y dotnet-sdk-$sdk

echo "Installing dotnet-runtime-$version"
sudo apt install -y dotnet-runtime-$version

echo "Installing aspnetcore-runtime-$version"
sudo apt install -y aspnetcore-runtime-$version
