#pragma once

// gSoap specific includes
#include "soapGsoapEndpointProxy.h"
// std includes
#include <vector>
#include <iostream>
// extension includes
#include "MainMenu.h"

class Maintenance
{
	public:
		static void ShowMenu(GsoapEndpointProxy* proxy);
		static void GetNamesOfMaintenanceProcedures(GsoapEndpointProxy* proxy);
		static void RunMaintenanceProcedure(GsoapEndpointProxy* proxy);
	private:
		static std::vector<std::string> CollectNamesOfMaintenanceProcedures(GsoapEndpointProxy* proxy);
};

