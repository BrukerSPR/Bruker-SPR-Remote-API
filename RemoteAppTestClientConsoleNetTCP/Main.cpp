// TestClient.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
// include microsoft web service
#include <WebServices.h>
// std includes
#include <iostream>
#include <stdio.h>

#include <locale>
#include <codecvt>
#include <string>
#include <cstring>

// wsUtil includes
#include "schemas.microsoft.com.2003.10.Serialization.xsd.h"
#include "schemas.microsoft.com.2003.10.Serialization.xsd.h"
#include "tempuri.org.xsd.h"
#include "tempuri.org.wsdl.h"
// extension includes
#include "MainMenu.h"
#include "Status.h"
#include "Maintenance.h"
#include "GetInformationAboutMethods.h"
#include "GetInformationAboutRunsets.h"
#include "PrepareAndRunAssays.h"

const std::string DefaultURI = "net.tcp://localhost:9002/bruker-spr/tcp";
std::wstring* proxyUrl = NULL;
WS_SERVICE_PROXY* proxy = NULL;
WS_ERROR* error = NULL;
WS_HEAP* heap = NULL;

void RunAction(MainMenu::ACTION action)
{
	switch (action)
	{
	case MainMenu::ACTION::status:
		Status::ShowMenu(proxy, error, heap);
		break;
	case MainMenu::ACTION::maintenance:
		Maintenance::ShowMenu(proxy, error, heap);
		break;
	case MainMenu::ACTION::informationAboutMethods:
		GetInformationAboutMethods::ShowMenu(proxy, error, heap);
		break;
	case MainMenu::ACTION::informationAboutRunsets:
		GetInformationAboutRunsets::ShowMenu(proxy,error,heap);
		break;
	case MainMenu::ACTION::prepareAndRunAssays:
		PrepareAndRunAssays::ShowMenu(proxy, error, heap);
		break;
	default:
		break;
	}
}


void CloseConnection()
{
	std::cout << "\nClosing the connection...";
	
	if (proxy != NULL)
	{
		WsCloseServiceProxy(proxy, NULL, error);
		WsFreeServiceProxy(proxy);
		proxy = NULL;
	}
	if (heap != NULL)
	{
		WsFreeHeap(heap);
		heap = NULL;
	}
	if (error != NULL)
	{
		WsFreeError(error);
		error = NULL;
	}
	if (proxyUrl != NULL)
	{
		delete proxyUrl;
		proxyUrl = NULL;
	}

	std::cout << "done";
}

bool TestConnection()
{
	if (proxy == NULL)
		return false;

	int operationMode;
	return Status::GetOperationMode(proxy,error,heap, operationMode);
}

bool InitConnection(std::string uri)
{
	std::cout << "\nTrying to connect to: " << uri << std::endl;
	HRESULT hr = S_OK;

	// Creating error object
	hr = WsCreateError(NULL, 0, &error);
	if (FAILED(hr))
	{
		std::cout << "\nFailed to create Error object" << std::endl;
		return false;
	}

	// Creating heap handle
	hr = WsCreateHeap(10000000, 0, NULL, 0, &heap, error);
	if (FAILED(hr))
	{
		std::cout << "\nFailed to create Heap object" << std::endl;
		return false;
	}

	// Creating proxy
	WS_TCP_BINDING_TEMPLATE templateValue = {};
	hr = NetTcpEndpoint_CreateServiceProxy(&templateValue,NULL,0,&proxy,error);
	if (FAILED(hr))
	{
		std::cout << "\nFailed to create proxy" << std::endl;
		return false;
	}

	// Set address
	WS_ENDPOINT_ADDRESS address = {};
	WS_STRING Url;
	Url.length = uri.length();
	proxyUrl = new std::wstring(MainMenu::ToWstring(uri));
	Url.chars = (WCHAR*)proxyUrl->c_str();
	address.url = Url;

	// open proxy
	hr = WsOpenServiceProxy(proxy, &address, NULL, error);
	if (FAILED(hr))
	{
		std::cout << "\nFailed to open proxy" << std::endl;
		return false;
	}

	// test connection
	if (TestConnection())
	{
		std::cout << "\nConnection established." << uri << std::endl;
		return true;
	}
	else
	{
		std::cout << "\nCouldn't connect to: " << uri << std::endl;
		return false;
	}
	
	return true;
}

int main()
{
	std::string uri;

	MainMenu::ClearScreenAndShowHeader();
	std::cout << "*                   net.tcp binding                    *\n********************************************************\n";
	std::cout << "\nType in the URI (i.e. the URL) of the server.\nThe default one is: " << DefaultURI << "\n(leave empty to use the default URI)" << std::endl;
	std::cout << "URI: ";
	std::getline(std::cin, uri);
	if (uri.size() <= 0)
		uri = DefaultURI;

	if (!InitConnection(uri.c_str()))
	{
		CloseConnection();
		std::cout << "\nPress any key to close the application.";
		std::getchar();
		return 0;
	}
	
	bool close = false;
	MainMenu::ACTION action;
	while (!close)
	{
		action = MainMenu::ShowMainMenu();
		if (action == MainMenu::ACTION::quit)
			close = true;
		else
			RunAction(action);
	}

	CloseConnection();

	std::cout << "\nPress any key to close the application.";
	std::getchar();
	return 0;
}

