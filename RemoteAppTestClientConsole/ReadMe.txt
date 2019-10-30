#-----------------------------------#
# Introduction   					#
#-----------------------------------#

This instruction will explain how to use gSoap (https://www.genivia.com/products.html) to generate the files needed to create a c++ application which can connect to the Sierra SPR Control Control Software (with verision >=3.5).

The control software starts a web service. To check if the service is running start a web browser and type in the url. If you are on the same machine as the service the url is: http://localhost:9001/bruker-spr
In any other case replace <localhost> with the specific IP of the machine where the control software is running on.

If you have visual studio installed, you can also use the 'WcfTestClient.exe' to test the connection. (See Appendix A for further instruction)

#-----------------------------------#
# Usage of gSoap					#
#-----------------------------------#

This will explain how to use gSoap to create the files needed for a client application that connects to the web service of the control software.
After you have downloaded and extracted gSoap to the folder ..\gsoap , you should have a 'bin' folder inside that folder. If you are on a windows or mac environment the needed programs 'wsdl2h' and 'soapcpp2' should be in there.
Start a console (command prompt) and navigate to the folder containing the executables 'wsdl2h' and 'soapcpp2'.
The first step is to create a file containing the definitions for the connection to the service. We will name this file 'spr.h' and it is created with the following command:
wsdl2h -Nname -o spr.h http://localhost:9001/bruker-spr?wsdl -t ..\gsoap\typemap.dat
If you are on a different machine <localhost> has to be replaced with the IP address of the machine where the service (i.e. the control software) is running on.
The 'spr.h' will be created in the same folder as 'wsdl2h' is in.
The next step is to create the needed .h and .cpp files for our client application. This is done with the following command:
soapcpp2 -j spr.h -I..\gsoap\import -I..\gsoap
This will create a bunch of files. For the client application we need:
soapH.h
soapC.cpp
soapGsoapEndpointProxy.h
soapGsoapEndpointProxy.cpp
soapStub.h
GsoapEndpoint.nsmap
We also need 'stdsoap2.cpp' from the ..\gsoap folder and the files 'duration.h' and 'duration.c' from the ..\gsoap\custom folder.
Copy all these files to the folder of your client application and rename 'duration.c' into 'duration.cpp'.
Afterwards include these files into your project. If your are using Visual Studio for the development set 'Precompiled Header' to 'Not Using Precompiled Headers' for the files soapC.cpp, soapGsoapEndpointProxy.cpp and stdsoap2.cpp.
To do so, select the files -> right click -> Properties -> C/C++ -> Precompiled Headers -> set to 'Not Using Precompiled Headers'.

To connect to the control software you need to
#include "soapStub.h"
#include "soapGsoapEndpointProxy.h"
#include "GsoapEndpoint.nsmap"
into your class.

Within a function you than can connect to the control software with the following code:

std::string uri = "http://localhost:9001/bruker-spr/gsoap";
GsoapEndpointProxy* proxy = new new GsoapEndpointProxy(uri.c_str(), SOAP_XML_INDENT);

To close the connection use:
proxy->destroy();

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