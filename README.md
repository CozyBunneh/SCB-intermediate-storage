# SCB-intermediate-storage 🏳️‍⚧️🏳️‍🌈
Dotnet Core intermediate storage api against the Swedish statistics database

## To simply run the application
As long as dotnet and such are installed.

### In the scb-api folder

$ `dotnet restore`

$ `dotnet run` or F5 in VS Code

Open: https://localhost:5001/

## Arch Linux Development Instructions
### DotNet Core
##### Install these packages:
```
community/dotnet-targeting-pack
    The .NET Core targeting pack
community/dotnet-sdk
    The .NET Core SDK
community/dotnet-runtime
    The .NET Core runtime
community/dotnet-host
    A generic driver for the .NET Core Command Line Interface
community/aspnet-runtime
    The ASP.NET Core runtime
```

###### Install this for dotnet entity framework
$ `dotnet tool install --global dotnet-ef
Add to path: PATH="/usr/local/bin:/usr/local/sbin/:/home/user-name/.dotnet:/home/user-name/.dotnet/tools:$PATH"`

##### In the scb-api folder
$ `dotnet restore`

$ `dotnet run`
or F5 in VS Code

## Editor
Use VS Code and open the root folder of the project.

## scb-api
The backend project, written in .NET 5

### How to start
Simply press F5 in VS Code and the project will start in debug mode (as long as the necessary plugins are installed).

## Navigate API

### Swagger Docs for the API
Open: https://localhost:5001/

### Api enpoint
#### Version 1.0
https://localhost:5001/api/v1.0/
