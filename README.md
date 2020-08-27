# Clean Architecture Template
![Build & Tests](https://github.com/ktutak1337/Clean-Architecture-Template/workflows/Build%20&%20Tests/badge.svg?branch=master)
[![NuGet Package](https://img.shields.io/badge/.Net%20Core-3.1-blue.svg)](https://dotnet.microsoft.com/download/dotnet-core/3.1)
[![NuGet Package](https://img.shields.io/badge/NuGet-1.0.0-blue.svg)](https://www.nuget.org/packages/Clean.Architecture.Template)
[![GitHub license](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ktutak1337/Clean-Architecture-Template/blob/master/LICENSE.md)

This is a configurable template for creating a Web API .NET Core projects following the principles of Clean Architecture and Domain-driven design approach. The template is available as a [NuGet package](https://www.nuget.org/packages/Clean.Architecture.Template).

# Installation
The easiest way to get started with the template is to install it by executing the following command:
``` csharp
~$ dotnet new --install Clean.Architecture.Template::1.0.0 
```
When that command is executed it will `cleanarch` template appears on the list of available templates for `dotnet new` command.<br/>

![cleanarch_installation](https://github.com/ktutak1337/Clean-Architecture-Template/blob/master/assets/cleanarch_installation.png)

# Usage
### Create project
To create a new project you can run the following command:
``` csharp
~$ dotnet new cleanarch -n MyAwesomeProject 
```
After executing this command, the project was created in a new folder named `MyAwesomeProject` or just create a folder for your project and navigate to it and execute the following command:
``` csharp
~$ dotnet new cleanarch 
```
The output of running that command is below:<br/>

![cleanarch_sln](https://github.com/ktutak1337/Clean-Architecture-Template/blob/master/assets/cleanarch_sln.png)

### Template options
The `cleanarch` template might have specific additional options you can pass in command. To see what the help output looks like for template execute the following command:
``` csharp
~$ dotnet new cleanarch --help 
```
The template has the following additional options:

| Options                  | Default (bool) | Description |
| ------------------------ | -------- | -------- |
|-d \| --docker | false | Creates Dockerfile, .dockerignore, and appsettings.docker.json files.|
|-g \| --git| false | Creates Git repository and .gitignore file. |
|-gi \| --gitignore | false | Creates .gitignore file. |
|-p \| --projects| true | Creates projects: Api, Application, Core, and Infrastructure.|
|-s \| --sln| true | Creates an sln file and add projects to it. |
|-sw \| --swagger| false | Adds the Swagger documentation. |
|-t \| --tests| true | Creates test projects: EndToEnd, Integration, and Unit.|

#### example
To create a new solution additionally with docker and swagger support, but without test projects, you can run the following command:
``` csharp
~$ dotnet new cleanarch -n MyAwesomeProject --tests false --docker --swagger
```

The output of running that command is below:<br/>

![cleanarch_example](https://github.com/ktutak1337/Clean-Architecture-Template/blob/master/assets/cleanarch_example.png)

# Give a star! :star:
If you like this project, learned something or you are using it to start your solution, please give it a star. Thanks!

# Issues
If you have discovered a bug or having some issues, please let me know by [reporting a new issue](https://github.com/ktutak1337/Clean-Architecture-Template/issues?state=open).