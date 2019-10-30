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
// std includes
#include <vector>
#include <iostream>
#include <string>

class GetInformationAboutRunsets
{
	public:
		static void ShowMenu(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetNamesOfRunsets(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetNamesOfRunsetsOfAssayType(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetAssayTypesOfAllRunsets(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetNameOfCurrentRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetAssayTypeOfCurrentRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetAssayTypeOfRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetMethodNamesOfRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
	private:
		static std::vector<std::string> CollectRunsets(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static std::vector<std::string> CollectAssayTypes(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
};

