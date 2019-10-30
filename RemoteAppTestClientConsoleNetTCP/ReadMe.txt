#-----------------------------------#
# Introduction   					#
#-----------------------------------#
This instruction will explain how to use the Microsoft utilities SvcUtil and wsutil to generate the files needed to create a c++ application which can connect to the Sierra SPR Control Control Software (with verision >=3.5).

The control software starts a web service. To check if the service is running start a web browser and type in the url. If you are on the same machine as the service the url is: http://localhost:9001/bruker-spr
In any other case replace <localhost> with the specific IP of the machine where the control software is running on.

If you have visual studio installed, you can also use the 'WcfTestClient.exe' to test the connection. (See Appendix A for further instruction)

#-----------------------------------#
# Usage of SvcUtil					#
#-----------------------------------#

The first step to create the client application is to create the corresponding *.wsdl and *.xsd files. This can be done with SvcUtil.exe.
Search for svcutil.exe on your file system, start a console (command prompt), navigate to svcutil.exe and run the following command:
svcutil.exe /t:metadata http://localhost:9001/bruker-spr?wsdl
If you are not on the same machine as the service replace <localhost> with the IP of that machine.
The command will create the following files:
schemas.microsoft.com.2003.10.Serialization.Arrays.xsd
schemas.microsoft.com.2003.10.Serialization.xsd
tempuri.org.wsdl
tempuri.org.xsd

#-----------------------------------#
# Usage of wsutil					#
#-----------------------------------#

The second step is to create the corresponding *.c and *.h file to the *.xsd and *.wsdl files we just have created.
To do so, copy the created *.xsd and *.wsdl files, search for wsutil.exe on your file system, navigate there and copy the *.xsd and *.wsdl files in that location.
Afterwards, start a console (command prompt), navigate to wsutil.exe and run the follwing command:
wsutil *.xsd *.wsdl
This will create the following files:
schemas.microsoft.com.2003.10.Serialization.Arrays.c
schemas.microsoft.com.2003.10.Serialization.Arrays.h
schemas.microsoft.com.2003.10.Serialization.c
schemas.microsoft.com.2003.10.Serialization.h
tempuri.org.c
tempuri.org.h
tempuri.org.c
tempuri.org.h

#-----------------------------------#
# Creating the client				#
#-----------------------------------#

To create a client application, copy the files:
schemas.microsoft.com.2003.10.Serialization.Arrays.c
schemas.microsoft.com.2003.10.Serialization.Arrays.h
schemas.microsoft.com.2003.10.Serialization.c
schemas.microsoft.com.2003.10.Serialization.h
tempuri.org.c
tempuri.org.h
tempuri.org.c
tempuri.org.h
to your project directory and include them. DO NOT COPY THE *.xsd and *.wsdl FILES!
If you are developing with Visual Studio you need to set 'Precompiled Header' to 'Not Using Precompiled Headers' for the files:
schemas.microsoft.com.2003.10.Serialization.Arrays.c
schemas.microsoft.com.2003.10.Serialization.c
tempuri.org.c
tempuri.org.c
To do so, select the files -> right click -> Properties -> C/C++ -> Precompiled Headers -> set to 'Not Using Precompiled Headers'.
Also you have to
#include <WebServices.h>
And to add 'WebServices.lib' as an Additional Dependency.

To create a connection to the service you have (at least) two options. You can either use the net.tcp or soap binding. The net.tcp binding can be used by connection to the NetTcpEndpoint. The soap binding can be used by connecting to the GsoapEndpoint.
No matter what option you chose, the following instances need to be declared for the time of client connection:
WS_ERROR* error = NULL;
WS_HEAP* heap = NULL;
WS_SERVICE_PROXY* proxy = NULL;
'error' is created by calling: WsCreateError(NULL, 0, &error);
'heap' is created by calling: WsCreateHeap(10000000, 0, NULL, 0, &heap, error);
The creation of 'proxy' depends on which binding you want to use.

NetTcpEndpoint (net.tcp binding)
================================
To use the NetTcpEndpoint you fist have to create a WS_TCP_BINDING_TEMPLATE. The following code will do this:
WS_TCP_BINDING_TEMPLATE templateValue = {};
NetTcpEndpoint_CreateServiceProxy(&templateValue,NULL,0,&proxy,error);
 
GsoapEndpoint (soap binding)
============================
To use the GsoapEndpoint you fist have to create a WS_HTTP_BINDING_TEMPLATE. The following code will do this:
WS_HTTP_BINDING_TEMPLATE templateValue = {};
GsoapEndpoint_CreateServiceProxy(&templateValue, NULL, 0, &proxy, error);

Opening the proxy
=================
For either binding option the proxy is opened the same way. This is done by the following code:
WsOpenServiceProxy(proxy, &address, NULL, error);
'address' is of type WS_ENDPOINT_ADDRESS and holds the url to the endpoint.
For the NetTcpEndpoint this is: net.tcp://localhost:9002/bruker-spr/tcp
For the GsoapEndpoint this is: http://localhost:9001/bruker-spr/gsoap
Replace <localhost> with the appropiate IP.

Closing the proxy
=================
To close the connection to the service you should run the following code:
WsCloseServiceProxy(proxy, NULL, error);
WsFreeServiceProxy(proxy);
WsFreeHeap(heap);
WsFreeError(error);	

#-----------------------------------#
# Appendix A						#
#-----------------------------------#

To test a webservice with the 'WcfTestClient.exe', which is part of your visual studio version, search for the exe on your file system and start it.
Click on 'File' -> 'Add Service', type in the url of the service and click ok. In our case the url is: http://localhost:9001/bruker-spr (or <localhost> replaced with the appropiate IP).
If the connection to the service was successful you should see a tree on the left side of the application.
'-My Service Projects
  '- http://localhost:9001/bruker-spr
     '- IBrukerSprRemoteService(GsoapEndpoint)
		'- GetNamesOfMethods()
		'- GetNamesOfMethodsOfAssayType()
		'- ...
		 ...
	 '- IBrukerSprRemoteService(SoapEndpoint)
	 '- IBrukerSprRemoteService(NetTcpEndpoint)
	 '- Config File