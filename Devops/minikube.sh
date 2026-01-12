#!/bin/bash

################################################## variables ##################################################L


################################################## functions ##################################################L

function setup() {
  minikube delete # Deletes a local Kubernetes cluster
  minikube start # Starts a local Kubernetes cluster
  minikube addons enable metrics-server # Enable the Metrics Server Add-On
  minikube addons list # display addons
  az acr login --name "zuhiddev.azurecr.io" # login to Azure Container Registry
}

function namespace() {
  namespace="zuhid-namespace"
  # kubectl delete namespace $NAMESPACE  # List all namespace
  kubectl apply --filename kubernetes/namespace.yaml # Apply the file
  kubectl get namespace # List all namespace
  kubectl config set-context --current --namespace=$namespace  # Set a context entry in kubeconfig
  kubectl config view --minify --output 'jsonpath={..namespace}' # Display the current namespace
}

################################################## exec ##################################################

clear
# setup
namespace
