#!/bin/bash
# Enhanced bash script to run unit tests using dotnet test

sudo apt remove 'dotnet*' 'aspnet*' 'netstandard*'

sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-8.0