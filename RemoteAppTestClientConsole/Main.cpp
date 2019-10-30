// Main.cpp : Defines the entry point for the console application.
//

// std include
#include "stdafx.h"
#include <stdio.h>
#include <iostream>
#include <cstdlib>

// gSoap specific includes
#include "soapStub.h"
#include "soapGsoapEndpointProxy.h"
#include "GsoapEndpoint.nsmap"

// extension includes
#include "MainMenu.h"
#include "Status.h"
#include "Maintenance.h"
#include "GetInformationAboutMethods.h"
#include "GetInformationAboutRunsets.h"
#include "PrepareAndRunAssays.h"

const std::string DefaultURI = "http://localhost:9001/bruker-spr/gsoap";
GsoapEndpointProxy* proxy = NULL;

void RunAction(MainMenu::ACTION action)
{
	switch (action)
	{
		case MainMenu::ACTION::status:
			Status::ShowMenu(proxy);
			break;
		case MainMenu::ACTION::maintenance:
			Maintenance::ShowMenu(proxy);
			break;
		case MainMenu::ACTION::informationAboutMethods:
			GetInformationAboutMethods::ShowMenu(proxy);
			break;
		case MainMenu::ACTION::informationAboutRunsets:
			GetInformationAboutRunsets::ShowMenu(proxy);
			break;
		case MainMenu::ACTION::prepareAndRunAssays:
			PrepareAndRunAssays::ShowMenu(proxy);
			break;
		default:
			break;
	}
}

void CloseConnection()
{
	std::cout << "\nClosing the connection...";
	proxy->destroy();
	delete proxy;
	proxy = NULL;
	std::cout << "done";
}

bool TestConnection()
{
	if (proxy == NULL)
		return false;

	int operationMode;
	return Status::GetOperationMode(proxy, operationMode);
}

bool InitConnection(const char* uri)
{
	if (uri == NULL)
		return false;

	if (proxy != NULL)
		CloseConnection();

	std::cout << "\nTrying to connect to: " << uri << std::endl;

	proxy = new GsoapEndpointProxy(uri, SOAP_XML_INDENT);
	proxy->soap->send_timeout = proxy->soap->recv_timeout = 1000;

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
}

int main()
{
	std::string uri;

	MainMenu::ClearScreenAndShowHeader();
	std::cout << "*                http binding with gSoap               *\n********************************************************\n";
	std::cout << "\nType in the URI (i.e. the URL) of the server.\nThe default one is: " << DefaultURI << "\n(leave empty to use the default URI)" <<std::endl;
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

