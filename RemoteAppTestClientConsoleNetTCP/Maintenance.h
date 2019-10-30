#pragma once

// include microsoft web service
#include <WebServices.h>
// wsUtil includes
#include "schemas.microsoft.com.2003.10.Serialization.xsd.h"
#include "schemas.microsoft.com.2003.10.Serialization.xsd.h"
#include "tempuri.org.xsd.h"
#include "tempuri.org.wsdl.h"
// std includes
#include <vector>
#include <iostream>
#include <string>
// extension includes
#include "MainMenu.h"

class Maintenance
{
	public:
		static void ShowMenu(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetNamesOfMaintenanceProcedures(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void RunMaintenanceProcedure(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
	private:
		static std::vector<std::string> CollectNamesOfMaintenanceProcedures(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
};

