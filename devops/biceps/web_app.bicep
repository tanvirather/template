param org string
param location string = resourceGroup().location
param webAppName string
param sku string = 'B1' // F1, B1, B2, B3, S1, S2, S3, P1v3, P2v3, P3v3
param slotName string = ''

// @description('Enable Application Insights and connect it to the Web App.')
// param enableAppInsights bool = true

// @description('App settings for the Web App.')
// param appSettings object = {
//   'WEBSITE_RUN_FROM_PACKAGE': '0'
// }

// @description('App settings for the slot (if created).')
// param slotAppSettings object = {}

// @description('Enable system-assigned managed identity.')
// param enableManagedIdentity bool = true

/********** App Service Plan (Linux) **********/
resource plan 'Microsoft.Web/serverfarms@2023-01-01' = {
  name: '${webAppName}-plan'
  location: location
  kind: 'linux'
  sku: {
    name: sku
    capacity: 1
  }
  properties: {
    reserved: true // Linux plans
  }
}

/********** Application Insights **********/
resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: '${webAppName}-ai'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    IngestionMode: 'ApplicationInsights'
  }
}

/********** Web App (https://learn.microsoft.com/en-us/azure/templates/microsoft.web/2023-01-01/sites?pivots=deployment-language-bicep) **********/
resource webApp 'Microsoft.Web/sites@2023-01-01' = {
  name: '${org}-${webAppName}'
  location: location
  kind: 'app,linux'
  properties: {
    serverFarmId: plan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|10.0'
    }
  }
  identity: {
    type: 'SystemAssigned'
  }
  dependsOn: [
    appInsights
  ]
}

// ===== Optional Slot (inherits .NET 10) =====
resource slot 'Microsoft.Web/sites/slots@2023-01-01' = if (slotName != '') {
  name: '${org}-${webAppName}/${slotName}'
  location: location
  properties: {
    serverFarmId: plan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|10.0' // slot runtime
    }
  }
}
