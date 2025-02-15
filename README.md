# dotnet-new-templates
A collection of dotnet new templates including but not limited to project templates, item templates, etc.

# About dotnet new Templates
## Types of Templates
### Item Templates
- A specific type of template that contains one or more files. 
- These types of templates are useful when you already have a project you want to generate another file, like a config file or code file.
## Required Folders
```plaintext
parent
├───test
└───working
    └───content
```
- `parent` is the root folder for the template.
- `test` is the folder where you can test the template.
- `working` is the folder where the template files are stored.
- `content` ????.
- The `working` folder and `test` folder should be under the same `parent` folder

# Useful Links
## Microsoft Learn
- [Custom Templates for dotnet new](https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates)
- [Tutorial: Create an item template](https://learn.microsoft.com/en-us/dotnet/core/tutorials/cli-templates-create-item-template)
- [Tutorial: Create a project template](https://learn.microsoft.com/en-us/dotnet/core/tutorials/cli-templates-create-project-template)
- [Tutorial: Create a template package](https://learn.microsoft.com/en-us/dotnet/core/tutorials/cli-templates-create-template-package)
## GitHub
- [dotnet/templating repository](https://github.com/dotnet/templating/)
- [dotnet/templating wiki](https://github.com/dotnet/templating/wiki)
- [template samples](https://github.com/dotnet/templating/tree/main/dotnet-template-samples)