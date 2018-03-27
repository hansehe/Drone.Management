#! /bin/bash
set -e

cd src/
docker-compose build
docker-compose up
cd ..