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

class GetInformationAboutMethods
{
	public:
		static void ShowMenu(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetNamesOfMethods(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetNamesOfMethodsOfAssayType(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetAssayTypesOfAllMethods(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetNameOfCurrentMethod(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetAssayTypeOfCurrentMethod(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetAssayTypeOfMethod(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
	private:
		static std::vector<std::string> CollectNamesOfMethods(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static std::vector<std::string> CollectAssayTypes(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
};

