#! /bin/bash
set -e

ENV_FOLDER="publish_configs"
VERSION=":$(cat $ENV_FOLDER/VERSION.txt)"
export VERSION_API=$VERSION
export VERSION_MIGRATOR=$VERSION
export VERSION_DB=$VERSION
export REPO="$(cat $ENV_FOLDER/REPO.txt)/"
export AUTHOR="$(cat $ENV_FOLDER/AUTHOR.txt)/"

cd src/
docker-compose -f docker-compose.yml -f docker-compose.prod.yml build
docker-compose -f docker-compose.yml -f docker-compose.prod.yml push
cd ..