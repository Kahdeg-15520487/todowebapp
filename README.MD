# Basic todo web app for practicing devops
# Project stack and setup:
## Repository structure
- root
    - back-end
        - todo.service 
    - front-end
        - todo.app
## Software stack
- front-end: vue 3 web app with default port 8080
- back-end: .net 7 asp web api with default port 5000
- database: redis
# Requirements:
- setup git repository for the webapp project
	- https://trunkbaseddevelopment.com/  
		- currently we are using trunk based branching strategy with 1/many main trunk and multiple shortlived topic branch  
	- https://semver.org/  
		- for releasing software component, we use git tag with semver to specify which commit should be deployed
- CI: setup build pipeline and pull request policy  
	- build pipeline  
		- https://learn.microsoft.com/en-us/azure/devops/pipelines/yaml-schema/?view=azure-pipelines  
		- https://learn.microsoft.com/en-us/azure/devops/pipelines/create-first-pipeline?view=azure-devops&tabs=net%2Ctfs-2018-2%2Cbrowser  
			- we use yaml pipeline for setting up task and job for building/testing source code before integrating/deploying  
	- pull request policy  
		- https://learn.microsoft.com/en-us/azure/devops/repos/git/about-pull-requests?view=azure-devops  
		- https://learn.microsoft.com/en-us/azure/devops/repos/git/branch-policies-overview?view=azure-devops  
		- https://learn.microsoft.com/en-us/azure/devops/repos/git/branch-policies?view=azure-devops&tabs=browser
- CD: setup deployment pipeline to 2 environment: development and production
	- https://learn.microsoft.com/en-us/azure/devops/pipelines/process/stages?view=azure-devops&tabs=yaml  
	- https://learn.microsoft.com/en-us/azure/devops/pipelines/process/deployment-jobs?view=azure-devops
	- there are 2 way you can setup the cd pipeline:  
		- classic GUI release pipeline  
		- yaml pipeline (the same one used in CI)  
- create infrastructure for hosting the application with ARM template or some other IaC tech (terraform)
	- https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/overview
	- https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/syntax
	- https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/deploy-cli  
	- https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/deploy-what-if?tabs=azure-powershell  
	- https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/add-template-to-azure-pipelines  
	- sample:
		- https://learn.microsoft.com/en-us/samples/browse/?expanded=azure&products=azure-resource-manager  
		- https://learn.microsoft.com/en-us/samples/azure/azure-quickstart-templates/webapp-windows-aspnet/  
	- we use ARM template (json) to encode our infrastructure as reproducible code
- setup and integrate azure keyvault for configuration management
    - https://learn.microsoft.com/en-us/aspnet/core/security/key-vault-configuration?view=aspnetcore-7.0
    - https://learn.microsoft.com/en-us/azure/key-vault/general/tutorial-net-create-vault-azure-web-app
    - https://learn.microsoft.com/en-us/azure/key-vault/general/vault-create-template?tabs=CLI
    - https://learn.microsoft.com/en-us/azure/templates/microsoft.keyvault/vaults/accesspolicies?pivots=deployment-language-arm-template
- updating back-end and front-end's configuration file
    - front-end
        - [.env.development](./front-end/todo.app/.env.development)
        - [.env.production](./front-end/todo.app/.env.production)
    - back-end
        - [appsettings.json](back-end\todo.service\todo.service\appsettings.json)
        - [appsettings.Development.json](back-end\todo.service\todo.service\appsettings.Development.json)
        - [appsettings.Production.json](back-end\todo.service\todo.service\appsettings.Production.json)
- implement a dashboard for monitoring the deployed application's health  
	- open problem, you can use any technology/library to implement this
	- the only requirement is that in case of an error happenning in the web application, the developer can view the trace of the error
