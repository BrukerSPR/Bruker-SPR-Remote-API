#pragma once

// include microsoft web service
#include <WebServices.h>
// wsUtil includes
#include "schemas.microsoft.com.2003.10.Serialization.xsd.h"
#include "schemas.microsoft.com.2003.10.Serialization.xsd.h"
#include "tempuri.org.xsd.h"
#include "tempuri.org.wsdl.h"
// extension includes
#include "MainMenu.h"

class Status
{
	public:
		static void ShowMenu(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetOperationMode(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static bool GetOperationMode(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap, int &operationMode);
		static void IsChipDocked(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void IsSamplePlateTrayIn(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void HasMessage(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetMessage(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void HasErrors(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetErrors(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void HasWarnings(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetWarnings(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
};

