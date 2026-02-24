#!/bin/bash

################################################## variables ##################################################
postgres_image="postgis/postgis:16-3.5"
mysql_image="mysql:9.1.0"
mailhog_image="mailhog/mailhog:latest"
ollama_image="ollama/ollama"
text_to_speech="ghcr.io/coqui-ai/tts-cpu"
cosmos_image="mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator:latest"
redis_image="redis"

################################################## methods ##################################################

remove_all_container(){
  docker rm --force $(docker container ls --all --quiet) # remove all containers (both running and stopped)
}

remove_all_image(){
  docker rmi -f $(docker images --all --quiet) # remove all images
}

postgres_install(){
  # variables
  postgres_container="postgres"
  postgres_user="postgres"
  postgres_password="P@ssw0rd"
  dbuser_user="dbuser"
  dbuser_password="P@ssw0rd"

  # remove the image
  docker container rm $postgres_container --force
  docker run --name $postgres_container --publish 5432:5432 --detach \
    --env "POSTGRES_USER=$postgres_user" \
    --env "POSTGRES_PASSWORD=$postgres_password" \
    "$postgres_image"
  sleep 5
  docker exec -it $postgres_container psql -U $postgres_user -c "create user $dbuser_user with superuser password '$dbuser_password';"
}

mysql_install(){
  # variables
  container="mysql"
  root_password="P@ssw0rd"
  db_user="dbuser"
  db_password="P@ssw0rd"

  # remove the image
  docker container rm $container --force
  docker run --name $container --publish 3306:3306 --detach \
    --env MYSQL_ROOT_PASSWORD=$root_password \
    $mysql_image
    # --env MYSQL_USER=$db_user \
    # --env MYSQL_PASSWORD=$db_password \
    # --env MYSQL_DATABASE=mydb \
    # http://localhost:3306/
}

mailhog_install(){
  # open in http://localhost:8025
  docker run --detach --publish 587:587 --publish 8025:8025 --name mailhog $mailhog_image
}

nginx_install(){
  # open in http://localhost:8080
  # docker run --name nginx -p 8080:80 -d nginx
  docker container rm nginx --force
  # docker run --name nginx -p 8080:80 -v ../nginx/html:/usr/share/nginx/html:ro -d nginx

  docker run --name nginx -p 8080:80 -v ../nginx/nginx.conf:/etc/nginx/nginx.conf:ro -v ../nginx/html:/usr/share/nginx/html:ro nginx
}

ollama_install(){
  # https://hub.docker.com/r/ollama/ollama
  # open in http://localhost:11434
  docker run --name ollama --publish 11434:11434 --detach $ollama_image
  # --volume ollama:/root/.ollama
}

cosmos_install(){
  # https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-develop-emulator?tabs=docker-linux%2Ccsharp&pivots=api-nosql
  # https://localhost:8081/_explorer/index.html
  docker container rm cosmos --force
  docker pull $cosmos_image
  docker run --name cosmos --publish 8081:8081 --publish 10250-10255:10250-10255 --detach \
    --env "COSMOS_ACCOUNT_NAME=cosmos" \
    --env "COSMOS_ACCOUNT_KEY=cosmos_key" \
    --env "COSMOS_DATABASE_NAME=cosmos_db" \
    --env "COSMOS_COLLECTION_NAME=cosmos_collection" \
    --env "COSMOS_PARTITION_KEY=partition_key" \
    $cosmos_image
  # docker logs -f cosmos
}

text_to_speech_install(){
  # open in http://localhost:5002
  docker container rm $text_to_speech
  docker run --publish 5002:5002 --rm -it --entrypoint /bin/bash $text_to_speech
  # python3 TTS/server/server.py --list_models #To get the list of available models
  # python3 TTS/server/server.py --model_name tts_models/en/vctk/vits # To start a server
}

redis_install(){
  docker run --detach --publish 6379:6379 --name redis $redis_image 
}

display_all(){
  docker image ls --format "table {{.Repository}}"
  docker container ls --all #--format "table {{.Names}}   {{.Image}}"
}

################################################## execute ##################################################

clear
# remove_all_container
# remove_all_image

# postgres_install
# mysql_install
# mailhog_install
# nginx_install
# ollama_install
# text_to_speech_install
cosmos_install
# redis_install

# display_all

