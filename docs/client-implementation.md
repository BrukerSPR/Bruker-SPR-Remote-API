# Client Implementation

Because the server provides several endpoints, there are many ways to implement a client. We do not provide any client applications which are intended for real usage, however there are several demo clients that show how a client application can use the API. You can download the code of these demo clients at <https://github.com/BrukerSPR/Bruker-SPR-Remote-API>.

## C#

The C# implementation can be found in the RemoteAppTestClient project. It is a Windows Forms application where the client object is based on the `System.ServiceModel.ClientBase` class using a `IClientChannel` that implements the same data contract interface as the server. The application provides controls to test almost all methods of the API and connects to the service using webHttp or TCP. The connection can be configured in the application's config file.

## C++

Two options are given to create a C++ client. Option number 1 depends on Microsoft and its `WebServices.dll`. This way is described in the `ReadMe.txt` of the RemoteAppTestClientConsoleNetTCP project. The 2nd option is platform independent, uses gSOAP and is described in the `ReadMe.txt` of the RemoteAppTestClientConsole project. Both options do not have a hard-coded connection and their classes (`*.c`/`*.cpp` and `*.h` files) are generated with the described tools (see the specific `ReadMe.txt`).

Both demo applications are simple console applications providing the same features. Their usage is self-explanatory. After launching them they ask the user to enter a connection URI (or choose the default) and then offer different options which the user can select by entering the corresponding key.

## Python, Java Script or other Web Languages

The webHttp endpoint can also be accessed using an arbitrary web browser. The data exchanged is based on JSON and requests can be submitted by just typing into the browser's address bar. It is hence easy to write a client in "web languages" like Python or Java Script.

A python example can be found in the RemoteAppTestClientPython project. It contains the class `Mass1Api` which allows to call the remote API functions and a `Tools` class providing some helper methods. These two classes make it trivial to write a simple script to enqueue operations. An example for such a script is `RemoteAppTestClientPython.py`.
