# Bruker-SPR-Remote-API
The SPR machines of Bruker offer a remote API to allow client frameworks to connect to the control software of the machine to remotely run operations like starting batch jobs or maintenance operations.
To find out more about the SPR machines please go to: [https://www.bruker.com/en/products-and-solutions/biosensors-for-molecular-interactions/surface-plasmon-resonance.html](https://www.bruker.com/en/products-and-solutions/biosensors-for-molecular-interactions/surface-plasmon-resonance.html).

## Overview
In this repository you will find the code of the demo clients mentioned in the documentation of the Bruker SPR Remote API, which is also part of this repository (see [API_Doku.pdf](https://github.com/BrukerSPR/Bruker-SPR-Remote-API/blob/master/API_Doku.pdf)). Each demo client has its own independent project. A brief description of the demo clients can be found in the following sections.

## Connecting to the API
The Bruker SPR Remote API is a REST API.
On the server side it is implemented in C# with Microsoft.AspNetCore.Mvc.

To create a client with .net you can just need to add the BrukerSprRemoteAPI.Client nuget package.

### BrukerSprRemoteAPI.Python
This project contains the Python package for the Bruker SPR Remote API including `BrukerSprRemoteAPI.py` and `Tools.py`. It can be installed as a Python package via `pip install .` and imported as `BrukerSprRemoteAPI`.

### BrukerSprRemoteAPI.JavaScript
This project contains the JavaScript / Node.js library for the Bruker SPR Remote API including `BrukerSprRemoteAPI.js` and `Tools.js`, along with TypeScript typings (`BrukerSprRemoteAPI.d.ts`).

## Demo Clients

### RemoteAppTestClient 
This is a Windows Forms application purely written in C#. It comes in two variants. One implementation is for (older) .net framework clients using .net4.8 and is based on [WCF](https://learn.microsoft.com/en-gb/dotnet/framework/wcf/whats-wcf). The other one uses modern .net8 and is base on [Microsoft.AspNetCore.Mvc](https://dotnet.microsoft.com/en-us/apps/aspnet).

The application provides controls to test almost all methods of the API and connects to the service using webHttp or TCP. The connection can be configured in the application's config file.

### RemoteAppTestClientPython
This shows how a client written in python can be created. It uses the `BrukerSprRemoteAPI` package which allows calling the remote API functions and provides a `Tools` class with helper methods. This project does not contain a fully functional test client application but a simple sample script in `RemoteAppTestClientPython.py` which shows a basic example on how the API can be used with a client written in python.

### RemoteAppTestClientHtml
This shows how a web browser client written in HTML and JavaScript can interact with the Bruker SPR Remote REST API. It uses the `BrukerSprRemoteAPI.JavaScript` library to control operations, query procedures, load methods, and stream logs in real time.

## Documentation (DocFX)
Documentation for the Bruker SPR Remote API is generated using [DocFX](https://dotnet.github.io/docfx/).

### Building Documentation and PDF Locally
To restore the local tool and generate the documentation along with `API_Doku.pdf`:
```bash
dotnet tool restore
dotnet docfx
```
To specifically run the PDF generation command:
```bash
dotnet docfx pdf
```
To build and serve the documentation locally with hot-reload:
```bash
dotnet docfx --serve
```

### CI/CD Pipeline
DocFX is configured as a local .NET tool via `.config/dotnet-tools.json` and builds both HTML site and `API_Doku.pdf` in any CI/CD environment with:
1. `dotnet tool restore`
2. `dotnet docfx`
An automated GitHub Actions workflow is provided at `.github/workflows/docs.yml` which produces and archives the web documentation and `API_Doku.pdf`.
