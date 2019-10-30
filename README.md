# Bruker-SPR-Remote-API
The SPR machines of Bruker offer a remote API to allow client frameworks to connect to the control software of the machine to remotely run its most important operations like starting batch jobs or maintenance operations.
To find out more about the SPR machines please go to: https://www.bruker.com/products/surface-plasmon-resonance/sierra-spr-32/overview.html

# Overview
In this repository you will find the code of the demo clients mentioned in the documentation of the Bruker SPR Remote API, which is also part of this repository. Each demo client has its own independent project. A brief description of the demo clients can be found in the following sections.

# RemoteAppTestClient 
This is a Windows Forms application purely written in C#. Its client object is based on the System.ServiceMode.ClientBase class using a IClientChannel that implements the same data contract interface as the server (i.e. the control software). The application provides controls to test almost all methods of the API and connects to the service using webHttp or TCP. The connection can be configured in the application's config file.

# RemoteAppTestClientConsole 
This is a platform independent c++ console application. With the application one can test every method of the API. It uses gSOAP (https://www.genivia.com/dev.html) to create the client code for the connection to the server (i.e. the control software). This is further descibed in the ReadMe.txt within the project.

# RemoteAppTestClientConsoleNetTCP 
This is a c++ console application which depends on Microsofts WebServices.dll. Apart from that the application has the same functionality as RemoteAppTestClientConsole. In the ReadMe.txt withing the project you will find a description on how to use Microsoft utilities to create the client code for the connection to the server (i.e. the control software).

# RemoteAppTestClientPython
This shows how a client written in python can be created. Included in this project is the Mass1Api which allows to call the remote API functions and a Tools class providing some helper methods. This project does not contain a fully functional test client application but a simple sample script in RemoteAppTestClientPython.py which shows a basic example on how the API can be used with a client written in python.
