# Clean Architecture Template
![Build & Tests](https://github.com/ktutak1337/Clean-Architecture-Template/workflows/Build%20&%20Tests/badge.svg?branch=master)
[![NuGet Package](https://img.shields.io/badge/.Net%20Core-3.1-blue.svg)](https://dotnet.microsoft.com/download/dotnet-core/3.1)
[![NuGet Package](https://img.shields.io/badge/NuGet-1.0.42-blue.svg)](https://www.nuget.org/packages/Clean.Architecture.Template)
[![GitHub license](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ktutak1337/Clean-Architecture-Template/blob/master/LICENSE.md)

This is a configurable template for creating a Web API .NET Core projects following the principles of Clean Architecture and Domain-driven design approach. The template is available as a [NuGet package](https://www.nuget.org/packages/Clean.Architecture.Template).

# Installation
The easiest way to get started with the template is to install it by executing the following command:
``` csharp
~$ dotnet new --install Clean.Architecture.Template::1.0.42
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
|-d \| --docker | false | Creates Dockerfile, .dockerignore, and appsettings.docker.json files. |
|-dc \| --docker-compose | false | Creates docker-compose scripts for selected infrastructure elements (MongoDB, etc.). The `--docker` option is **required**, otherwise no scripts will be generated. |
|-g \| --git| false | Creates Git repository and .gitignore file. |
|-gi \| --gitignore | false | Creates .gitignore file. |
|-p \| --postgres | false | If specified, adds PostgreSQL to the solution. |
|-pr \| --projects | true | Creates projects: Api, Application, Core, and Infrastructure. |
|-m \| --mongo | false | If specified, adds MongoDB to the solution. |
|--no-restore | false | If specified, skips the automatic restore of the project on create. |
|-s \| --shared | false | If specified, creates shared project. |
|-sl \| --sln | true | Creates an sln file and add projects to it. |
|-sw \| --swagger | false | Adds the Swagger documentation. |
|-t \| --tests | true | Creates test projects: EndToEnd, Integration, and Unit. |
|-xu \| --xunit | false | Creates test projects: EndToEnd, Integration, and Unit based on the XUnit Framework. |

#### examples
1. To create a new solution additionally with docker and swagger support, but without test projects, you can run the following command:
``` csharp
~$ dotnet new cleanarch -n MyAwesomeProject --tests false --docker --swagger
```

&nbsp;&nbsp;&nbsp;&nbsp;The output of running that command is below:<br/>

&nbsp;&nbsp;&nbsp;&nbsp;![cleanarch_example](https://github.com/ktutak1337/Clean-Architecture-Template/blob/master/assets/cleanarch_example.png)

2. To create a new solution additionally with Docker, MongoDB, git init and tests based on XUnit Framework (By default NUnit), but with skips the automatic restore of the project on creating, you can run the following command:

``` csharp
~$ dotnet new cleanarch -n MyAwesomeProject --docker --mongo --git --xunit --no-restore
```
&nbsp;&nbsp;&nbsp;&nbsp;or a shorter version of the above command:

``` csharp
~$ dotnet new cleanarch -n MyAwesomeProject -d -m --g -xu --no-restore
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

If you want to start the entire infrastructure (API, MongoDB, Redis, etc. ). The easiest way to run it is by using `docker-compose`. For this case, navigate to `/scripts/compose` and execute the following command:
``` bash
~$ docker-compose -f script_name.yml up -d
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
| SQL Databases support (EF): [**X**] PostgreSQL,<br/> [ ] MS SQL Server | on hold | 2020-09-28 |
| Redis | todo | - |
| Serilog | todo | - |
| Elasticsearch | todo | - |
| Kibana | todo | - |
| SignalR | todo | - |
| OData | todo | - |
| MongoDB as optional | Completed | 2020-09-02 |
| GraphQL | todo | - |
| Docker compose: *[**X**] API, [**X**] MongoDB, [ ] Redis, <br/>[**X**] PostgreSQL,[**X**] PgAdmin, [ ] Elasticsearch, <br/> [ ] Kibana, [ ] Front-End* | on hold | 2020-09-06 [1] <br/> 2020-09-28 [2] |
| Add Shared project as optional | Completed | 2020-10-18 |
| API versioning | todo | - |
| Error handling | Completed | 2020-10-05 |
| Restore on create | Completed | 2020-09-02 |
| Selection of the test framework (NUnit, XUnit)| Completed | 2020-09-02 |
| Front-End SPA application | todo | - |

**NOTE:** If you have a proposal for a new feature or a change to the existing code, please don't hesitate to report it. All proposals will be considered.