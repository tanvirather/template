param acrName string // ACR name (globally unique, 5-50 alphanumeric, no hyphens)
param location string = resourceGroup().location // 'Location for the registry'
param acrSku string = 'Basic' // 'SKU: Basic, Standard, or Premium'

resource acr 'Microsoft.ContainerRegistry/registries@2023-01-01-preview' = {
  name: acrName
  location: location
  sku: { name: acrSku }
}
