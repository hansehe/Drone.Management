$ENV_FOLDER = "publish_configs"
$VERSION = ":" + $(cat $ENV_FOLDER/VERSION.txt)
$env:VERSION_API = $VERSION
$env:VERSION_MIGRATOR = $VERSION
$env:VERSION_DB = $VERSION
$env:REPO = $(cat $ENV_FOLDER/REPO.txt) + "/"
$env:AUTHOR = $(cat $ENV_FOLDER/AUTHOR.txt) + "/"

cd src/
docker-compose -f docker-compose.yml -f docker-compose.prod.yml build
docker-compose -f docker-compose.yml -f docker-compose.prod.yml push
cd ..