# Clean Architecture Template
![Build & Tests](https://github.com/ktutak1337/Clean-Architecture-Template/workflows/Build%20&%20Tests/badge.svg?branch=master)
[![NuGet Package](https://img.shields.io/badge/.NET%20-6.0-blue.svg)](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
[![NuGet Package](https://img.shields.io/badge/.NET%20-5.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet-core/5.0)
[![NuGet Package](https://img.shields.io/badge/.NET%20Core-3.1-blue.svg)](https://dotnet.microsoft.com/download/dotnet-core/3.1)
[![NuGet Package](https://img.shields.io/badge/NuGet-6.0.0-blue.svg)](https://www.nuget.org/packages/Clean.Architecture.Template)
[![GitHub license](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ktutak1337/Clean-Architecture-Template/blob/master/LICENSE.md)

This is a configurable template for creating .NET Core Web API projects following the principles of Clean Architecture and Domain-driven design approach. The template is available as a [NuGet package](https://www.nuget.org/packages/Clean.Architecture.Template).

# Installation
The easiest way to get started with the template is to install it by executing the following command:
``` csharp
~$ dotnet new --install Clean.Architecture.Template::6.0.0
```
When that command is executed, the `cleanarch` template will appear on the list of available templates for the `dotnet new` command.<br/>

![cleanarch_installation](https://github.com/ktutak1337/Clean-Architecture-Template/blob/master/assets/cleanarch_installation.png)

# Usage
### Create project
To create a new project, you can run the following command:
``` csharp
~$ dotnet new cleanarch -n MyAwesomeProject
```
After executing this command, the project will be created in a new folder named `MyAwesomeProject`. 
If you want to create your project in the current folder, execute the following command:
``` csharp
~$ dotnet new cleanarch
```
The output of running that command is below:<br/>

![cleanarch_sln](https://github.com/ktutak1337/Clean-Architecture-Template/blob/master/assets/cleanarch_sln.png)

### Template options
The `cleanarch` template has additional options that you can pass with this command. To display these options, execute the following command:
``` csharp
~$ dotnet new cleanarch --help
```
The template has the following additional options:

| Options                  | Default (bool) | Description |
| ------------------------ | -------- | -------- |
|-d \| --docker | false | If specified, creates Dockerfile, .dockerignore, appsettings.docker.json files, and docker-compose scripts for selected infrastructure elements (MongoDB, etc.). |
|-g \| --git| false | Creates Git repository and .gitignore file. |
|-gi \| --gitignore | false | Creates .gitignore file. |
|-p \| --postgres | false | If specified, adds PostgreSQL to the solution. |
|-pr \| --projects | true | Creates projects: Api, Application, Core, and Infrastructure. |
|-m \| --mongo | false | If specified, adds MongoDB to the solution. |
|--no-restore | false | If specified, skips the automatic restore of the project on create. |
|-s \| --shared | false | If specified, creates shared project. |
|-sl \| --serilog | false | If specified, adds Serilog configuration for Console and File sliks. |
|-sl-elastic \| --serilog-elastic | false | If specified, adds Serilog sinks for the Elasticsearch and required docker configuration for it. The `--serilog` option is required, otherwise no configuration will be generated. |
|-sl-seq \| --serilog-seq | false | If specified, adds Serilog sinks for the Seq and required docker configuration for it. The `--serilog` option is required, otherwise no configuration will be generated. |
|--sln | true | Creates an sln file and add projects to it. |
|-sw \| --swagger | false | Adds the Swagger documentation. |
|-t \| --tests | true | Creates test projects: EndToEnd, Integration, and Unit. [XUnit is the default test framework] |
|--no-sample | false | If specified, does not generate sample code for order domain. |
|-me \|--mediatr | false | If specified, Adds MediatR library to create and process commands, queries, and events by CQRS concept (By default is Convey library). |
|-nu \| --nunit | false | If specified, creates test projects: EndToEnd, Integration, and Unit based on the NUnit Framework. [XUnit is the default test framework] |

#### examples
1. To create a new solution additionally with docker and swagger support, but without test projects, you can run the following command:
``` csharp
~$ dotnet new cleanarch -n MyAwesomeProject --tests false --docker --swagger
```

&nbsp;&nbsp;&nbsp;&nbsp;The output of running that command is below:<br/>

&nbsp;&nbsp;&nbsp;&nbsp;![cleanarch_example](https://github.com/ktutak1337/Clean-Architecture-Template/blob/master/assets/cleanarch_example.png)

2. To create a new solution additionally with Docker, MongoDB, git init and tests based on NUnit Framework (By default XUnit), but with skips the automatic restore of the project on creating, you can run the following command:

``` csharp
~$ dotnet new cleanarch -n MyAwesomeProject --docker --mongo --git --nunit --no-restore
```
&nbsp;&nbsp;&nbsp;&nbsp;or a shorter version of the above command:

``` csharp
~$ dotnet new cleanarch -n MyAwesomeProject -d -m --g -nu --no-restore
```

# How to start the solution?
#### locally
The API can be started locally within `/src/your_project_name.Api` directory *(by default it will be available under `https://localhost:5001`)* using the following command:
``` csharp
~$ dotnet run
```
or by running `./scripts/start.sh` shell script in the **main directory**.

#### Docker
You can also run the API using Docker:
1. Make sure you are in the **main directory** and run the following command to build your image:
``` bash
~$ docker build --tag tag_name .
```
2. Run the following command to start a container based on your new image:
``` bash
~$ docker run -p 5001:5001 -d --name container_name tag_name
```
&nbsp;&nbsp;&nbsp;&nbsp;\**to run the container in the background use `-d` options.*

If you want to start the entire infrastructure (MongoDB, Seq, etc. ). The easiest way to run it is by using `docker-compose`. For this case, navigate to `/scripts/compose` and execute the following commands:
``` bash
~$ cd scripts/compose
~$ docker-compose -f infrastructure.yml up -d
```

The API service can be also started with `docker-compose`:
``` bash
~$ cd scripts/compose
~$ docker-compose -f api.yml up -d
```

&nbsp;&nbsp;&nbsp;&nbsp;\**to run in the background use `-d` options.*

Or just install the entire infrastructure locally on your machine.

#### Database migration
If you add PostgreSQL to the solution, you need to create a migration. To create an `initial database migration` execute the migration command in the **Package Manager Console** inside Visual Studio. In **Package Manager Console** select `your_project_name.Infrastructure` project and run the following command:
``` csharp
PM> Add-Migration migration_name
```
Migration can be run from the CLI, but before that, you must install `dotnet-ef` tools, by executing the following commands:

``` csharp
~$ dotnet tool install --global dotnet-ef
```
``` csharp
~$ dotnet restore
```
&nbsp;&nbsp;&nbsp;&nbsp; or by running `./scripts/install_dotnet-ef_cli.sh` shell script in the **main directory**.<br/>
To create an initial database migration via CLI. Navigate to `/src/your_project_name.Infrastructure` and run the following command:
``` csharp
~$ dotnet ef migrations add migration_name
```
&nbsp;&nbsp;&nbsp;&nbsp; or by running `./scripts/create-migration.sh` shell script in the **main directory**. The script takes the name of the migration as a parameter. If no name is given then it generates a migration called 'Initial' (example: `./scripts/create-migration.sh migration_name`). There are also scripts for removal and revert migration available in the scripts directory.

# Give a star! :star:
If you like this project, learned something or you are using it to start your solution, please give it a star. Thanks!

# Issues
If you have discovered a bug or having some issues, please let me know by [reporting a new issue](https://github.com/ktutak1337/Clean-Architecture-Template/issues?state=open).

# Roadmap
List of features to add:

| Name                     | Status | Release date |
| ------------------------ | -------- | -------- |
| API versioning | todo | - |
| Elasticsearch | todo | - |
| GraphQL | todo | - |
| OData | todo | - |
| SignalR | todo | - |
| Redis | todo | - |
| Minimal API - adding a choice between a minimal API and MVC when generating a Web API project | todo | - |
| Sample unit [ ], integration [ ], and End-to-End tests [ ] | todo | - |
| ?? Front-End SPA application ?? | todo | - |
| SQL Databases support (EF): [**X**] PostgreSQL, [ ] MS SQL Server | on hold | 2020-09-28 |
| Docker compose: [**X**] API, [**X**] MongoDB, [**X**] PostgreSQL, [ ] MS SQL Server, [**X**] Elasticsearch,<br/>[ ] Redis, [**X**] PgAdmin, [**X**] Kibana, [**X**] Seq | on hold | 2020-09-06 [1] <br/> 2020-09-28 [2] <br/> 2021-11-02 [3] |
| Migration to .NET 6 | Completed | 2021-12-09 |
| MediataR as the second option for CQRS | Completed | 2020-11-08 |
| Remove `--docker-compose` option from template | Completed | 2021-11-02 |
| Ability to generate a solution without sample code (Orders domain) | Completed | 2021-11-02 |
| Add infrastructure.yml file to start the required infrastructure<br/> by docker-compose command | Completed | 2021-11-02 |
| XUnit as default test framework | Completed | 2021-11-02 |
| Serilog | Completed | 2021-10-25 |
| Add Shared project as optional | Completed | 2020-10-19 |
| Error handling | Completed | 2020-10-05 [1] <br/> 2021-10-25 [2] |
| MongoDB as optional | Completed | 2020-09-02 |
| Selection of the test framework (NUnit, XUnit)| Completed | 2020-09-02 |
| Restore on create | Completed | 2020-09-02 |

**NOTE:** If you have a proposal for a new feature or a change to the existing code, please don't hesitate to report it. All proposals will be considered.