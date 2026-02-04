#!/bin/bash

################################################## variables ##################################################L


################################################## functions ##################################################L

function setup_azure_devops_cli() {
  az upgrade

  # Install or update the Azure DevOps CLI extension
  az extension add --name azure-devops --upgrade

  # Set the default organization and project for Azure DevOps CLI commands
  az devops configure --defaults organization=https://dev.azure.com/tzather project=zuhid
}

################################################## exec ##################################################

clear
# setup_azure_devops_cli


# az pipelines variable-group variable create \
#   --group-id 1 \
#   --name <VarName> \
#   --value "<Value>" \
#   --secret false                                             # set --secret true for secrets         [3](https://learn.microsoft.com/en-us/cli/azure/pipelines/variable-group/variable?view=azure-cli-latest)



  # [az pipelines variable-group](https://learn.microsoft.com/en-us/cli/azure/pipelines/variable-group?view=azure-cli-latest)

az pipelines variable-group create \
  --name "crmGroup" \
  --description "Shared variables" \
  --variables \
    appNameTest=zuhid-auth-test \
    appNameProd=zuhid-auth-prod \
    slotName=staging2 \
  # --authorize false # set to true to authorize all pipelines to use this variable group

az pipelines variable-group list -o table
# az pipelines variable-group list --query "*.{id:id,name:name}" -o table
# az pipelines variable-group list --query "[?name=='<GroupName>'].{id:id,name:name}" -o table
