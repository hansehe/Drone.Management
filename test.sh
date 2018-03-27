#! /bin/bash
set -e

cd src/
docker-compose -f docker-compose.tests.yml build
cd ..