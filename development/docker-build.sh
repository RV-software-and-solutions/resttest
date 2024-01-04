#!/bin/bash

echo "Starting Docker build process for resttest..."
docker build -t resttest -f src/Web/Dockerfile src/
echo "Docker build completed successfully."
