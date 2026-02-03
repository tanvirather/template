#!/bin/bash

################################################## variables ##################################################L
project="auth"
location="centralus"

################################################## functions ##################################################L
# resource_groups(){
#   az group delete --name $RESOURCE_GROUP_NETWORK_WATCHER --yes 2>/dev/null
#   # az group delete --name $RESOURCE_GROUP_AKS --yes 2>/dev/null
#   # az group delete --name $RESOURCE_GROUP --yes 2>/dev/null
#   # az group create --name $RESOURCE_GROUP --location $LOCATION 1>/dev/null && echo "Resource group created : $RESOURCE_GROUP"
# }

delete_all_resource_groups(){
  az group list --query "[].name" -o tsv | while IFS= read -r rg; do
    # echo "Resource Group: $rg"
    az group delete --name "$rg" --yes --no-wait
  done
}

stop_services(){
  az webapp stop --resource-group auth-test-rg --name zuhid-auth-test
  az webapp stop --resource-group auth-prod-rg --name zuhid-auth-prod
  az webapp stop --resource-group auth-prod-rg --name zuhid-auth-prod --slot staging
}

start_services(){
  az webapp start --resource-group auth-test-rg --name zuhid-auth-test
  az webapp start --resource-group auth-prod-rg --name zuhid-auth-prod
  az webapp start --resource-group auth-prod-rg --name zuhid-auth-prod --slot staging
}

create_test(){
  resource_group="${project}-test-rg"
  # az group delete --name $resource_group --yes 2>/dev/null
  # az group create --name $resource_group --location $location 1>/dev/null && echo "Resource group created : $resource_group"

  az deployment group create -g $resource_group -f biceps/web_app.bicep -p \
    org='zuhid' \
    webAppName='auth-test' \
    sku='B1' \
    slotName='staging'
}

create_prod(){
  resource_group="${project}-prod-rg"
  # az group delete --name $resource_group --yes 2>/dev/null
  # az group create --name $resource_group --location $location 1>/dev/null && echo "Resource group created : $resource_group"

  az deployment group create -g $resource_group -f biceps/web_app.bicep -p \
    org='zuhid' \
    webAppName='auth-prod' \
    sku='P0V3' \
    slotName='staging'
}

function buildAndDeploy() {
  repo=$1
  image=$2
  version=$3
  docker rmi "${repo}/${image}:${version}" 2>/dev/null
  docker build --tag ${repo}/${image}:${version} --file ../Auth/Dockerfile ../ # run from current folder
  docker image ls --format '{{.Repository}}:{{.Tag}}' | grep "${repo}/${image}:${version}"
  docker push "${repo}/${image}:${version}" # push container https://hub.docker.com/u/tzather
}

################################################## exec ##################################################

# clear
# delete_all_resource_groups
# az login
# resource_groups
# stop_services
# start_services

# create_test
# create_prod


# buildAndDeploy "zuhiddev.azurecr.io" "auth" "v1"

