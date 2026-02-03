#!/bin/bash

################################################## variables ##################################################L


################################################## functions ##################################################L

function setup() {
  minikube delete # Deletes a local Kubernetes cluster
  minikube start # Starts a local Kubernetes cluster
  minikube addons enable metrics-server # Enable the Metrics Server Add-On
  minikube addons enable registry # Enable the Local Registry Add-On
  minikube addons list # display addons
  minikube tunnel # Creates a network route to services deployed with type LoadBalancer
  # minikube dashboard --port=39663 # Opens the Kubernetes dashboard in a browser
}

function deployToRegistry(){
  cd ../ # run from parent folder
  eval $(minikube docker-env) # configure docker to use minikube's docker daemon
  docker build -t localhost:5000/auth:latest -f Auth/Dockerfile . # run from base folder
  docker push localhost:5000/auth:latest # push to registry
}

function namespace() {
  # kubectl delete namespace $NAMESPACE  # List all namespace
  kubectl apply --filename kubernetes/namespace.yaml # Apply the file
  kubectl config set-context --current --namespace=dev-namespace  # Set a context entry in kubeconfig
  # kubectl get namespace # List all namespace
  # kubectl config view --minify --output 'jsonpath={..namespace}' # Display the current namespace
}

function deployment() {
  # kubectl delete deployment auth-deployment # Delete a deployment
  kubectl apply --filename kubernetes/deployment.yaml # Apply the file
  # kubectl get deployments # List all deployments
  # kubectl get pods # List all pods
  # kubectl logs deploy/auth-deployment --all-containers=true --follow --tail=10 # Stream logs from pods in a deployment
}

function service() {
  kubectl apply --filename kubernetes/service.yaml # Apply the file
  # kubectl get services # List all services
  minikube service auth-service -n dev-namespace --url # Get the URL of the service
}

################################################## exec ##################################################

clear
# setup
# deployToRegistry
# namespace
# deployment
# service

