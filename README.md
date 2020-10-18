# SCB-intermediate-storage
Dotnet Core intermediate storage api against the Swedish statistics database

## Installation
### DotNet Core
##### Install these packages:
```
community/dotnet-targeting-pack 3.1.8.sdk108-1 (2.0 MiB 24.2 MiB) (Installed)
    The .NET Core targeting pack
community/dotnet-sdk 3.1.8.sdk108-1 (40.2 MiB 142.9 MiB) (Installed)
    The .NET Core SDK
community/dotnet-runtime 3.1.8.sdk108-1 (22.0 MiB 68.7 MiB) (Installed)
    The .NET Core runtime
community/dotnet-host 3.1.8.sdk108-1 (159.3 KiB 492.2 KiB) (Installed)
    A generic driver for the .NET Core Command Line Interface
community/aspnet-runtime 3.1.8.sdk108-1 (6.1 MiB 17.2 MiB)
    The ASP.NET Core runtime
```

###### Install this for dotnet entity framework
$ dotnet tool install --global dotnet-ef
Add to path: PATH="/usr/local/bin:/usr/local/sbin/:/home/bunneh/.dotnet:/home/bunneh/.dotnet/tools:$PATH"

##### In the api folder
$ dotnet restore

$ dotnet run
or F5 in VS Code

## Editor
Use VS Code and open the root folder of the project.

## scb-api
The backend project, written in dotnet core 3.1

### How to start
Simply press F5 in VS Code and the project will start in debug mode (as long as the necessary plugins are installed).

## Navigate API

### Swagger Docs for the API
Open: https://localhost:5001/

### Api enpoint
#### Version 1.0
https://localhost:5001/api/v1.0/