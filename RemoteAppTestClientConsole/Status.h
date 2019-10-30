#pragma once

// gSoap specific includes
#include "soapGsoapEndpointProxy.h"
// extension includes
#include "MainMenu.h"

class Status
{
	public:
		static void ShowMenu(GsoapEndpointProxy* proxy);
		static void GetOperationMode(GsoapEndpointProxy* proxy);
		static bool GetOperationMode(GsoapEndpointProxy* proxy, int &operationMode);
		static void IsChipDocked(GsoapEndpointProxy* proxy);
		static void IsSamplePlateTrayIn(GsoapEndpointProxy* proxy);
		static void HasMessage(GsoapEndpointProxy* proxy);
		static void GetMessage(GsoapEndpointProxy* proxy);
		static void HasErrors(GsoapEndpointProxy* proxy);
		static void GetErrors(GsoapEndpointProxy* proxy);
		static void HasWarnings(GsoapEndpointProxy* proxy);
		static void GetWarnings(GsoapEndpointProxy* proxy);
};

