#pragma once

// gSoap specific includes
#include "soapGsoapEndpointProxy.h"
// std includes
#include <vector>
#include <iostream>
// extension includes
#include "MainMenu.h"

class GetInformationAboutMethods
{
	public:
		static void ShowMenu(GsoapEndpointProxy* proxy);
		static void GetNamesOfMethods(GsoapEndpointProxy* proxy);
		static void GetNamesOfMethodsOfAssayType(GsoapEndpointProxy* proxy);
		static void GetAssayTypesOfAllMethods(GsoapEndpointProxy* proxy);
		static void GetNameOfCurrentMethod(GsoapEndpointProxy* proxy);
		static void GetAssayTypeOfCurrentMethod(GsoapEndpointProxy* proxy);
		static void GetAssayTypeOfMethod(GsoapEndpointProxy* proxy);
	private:
		static std::vector<std::string> CollectNamesOfMethods(GsoapEndpointProxy* proxy);
		static std::vector<std::string> CollectAssayTypes(GsoapEndpointProxy* proxy);
};

