#pragma once

// gSoap specific includes
#include "soapGsoapEndpointProxy.h"
// std includes
#include <vector>
#include <iostream>
//extension includes
#include "MainMenu.h"

class GetInformationAboutRunsets
{
	public:
		static void ShowMenu(GsoapEndpointProxy* proxy);
		static void GetNamesOfRunsets(GsoapEndpointProxy* proxy);
		static void GetNamesOfRunsetsOfAssayType(GsoapEndpointProxy* proxy);
		static void GetAssayTypesOfAllRunsets(GsoapEndpointProxy* proxy);
		static void GetNameOfCurrentRunset(GsoapEndpointProxy* proxy);
		static void GetAssayTypeOfCurrentRunset(GsoapEndpointProxy* proxy);
		static void GetAssayTypeOfRunset(GsoapEndpointProxy* proxy);
		static void GetMethodNamesOfRunset(GsoapEndpointProxy* proxy);
	private:
		static std::vector<std::string> CollectRunsets(GsoapEndpointProxy* proxy);
		static std::vector<std::string> CollectAssayTypes(GsoapEndpointProxy* proxy);
};

