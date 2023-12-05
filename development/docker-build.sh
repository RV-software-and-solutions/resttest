#!/bin/bash

# ******* This script build docker image **********

cd src
docker build -t resttest -f Web/Dockerfile .