#!/bin/bash

# Define a new parameter for the tag
tag=$1

if [ -z "$tag" ]; then
    tag="latest"
fi

echo "Tag for this build is -> $tag"

echo "Starting Docker build process for resttest..."
docker build -t resttest -f src/Web/Dockerfile src/
echo "Docker build completed successfully."

# Re-tagging the image
docker tag resttest $tag
