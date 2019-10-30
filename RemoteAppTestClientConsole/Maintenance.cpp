#include "Maintenance.h"

void Maintenance::ShowMenu(GsoapEndpointProxy* proxy)
{
	bool close = false;
	char input;
	while (!close)
	{
		MainMenu::ClearScreenAndShowHeader();
		std::cout << "*                 Maintenance Methods                  *\n********************************************************\n";
		std::cout << "\n1\t- GetNamesOfMaintenanceProcedures";
		std::cout << "\n2\t- RunMaintenanceProcedure";
		std::cout << "\nq\t- quit";

		std::cout << "\nChoice: ";

		std::cin >> input;
		switch (input)
		{
		case '1':
			Maintenance::GetNamesOfMaintenanceProcedures(proxy);
			break;
		case '2':
			Maintenance::RunMaintenanceProcedure(proxy);
			break;
		case 'q':
			close = true;
			break;
		default:
			break;
		}
	}
}

void Maintenance::GetNamesOfMaintenanceProcedures(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	std::vector<std::string> procedures = Maintenance::CollectNamesOfMaintenanceProcedures(proxy);
	
	if (procedures.size() <= 0)
		std::cout << "\nNo maintenance procedures available." << std::endl;
	else
	{
		std::cout << "\nThe following maintenance procedures are available:" << std::endl;
		for(int i = 0; i < (int)procedures.size(); i++)
			std::cout << procedures.at(i) << std::endl;
	}

	MainMenu::Return();
}

void Maintenance::RunMaintenanceProcedure(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	std::vector<std::string> procedures = Maintenance::CollectNamesOfMaintenanceProcedures(proxy);

	if (procedures.size() <= 0)
	{
		std::cout << "\nNo maintenance procedures available." << std::endl;
		MainMenu::Return();
		return;
	}
	else
	{
		std::cout << "\nSelect a maintenance procedure to run:" << std::endl;
		for (int i = 0; i < (int)procedures.size(); i++)
			std::cout << i+1 << "\t- " + procedures.at(i) << std::endl;
		std::cout << "0\t- quit" << std::endl;
	}

	std::cout << "\nChoice: ";
	int input;
	std::cin >> input;

	if (input > 0 && input <= (int)procedures.size())
	{
		std::string procedureName = procedures.at(input - 1);
		std::cout << "\n\nMaintenance procedure <" << procedureName << "> will be run." << std::endl;

		_tempuri__RunMaintenanceProcedure get;
		_tempuri__RunMaintenanceProcedureResponse resp;

		get.procedureName = &procedureName;
		if (proxy->RunMaintenanceProcedure(&get, resp) != SOAP_OK)
		{
			std::cout << "\nError while sending maintenance procedure " << procedureName << " to the server." << std::endl;
			proxy->soap_stream_fault(std::cerr);
			return;
		}

		std::cout << "\nRunMaintenanceProcedure(" << procedureName << ") returned: " << *resp.RunMaintenanceProcedureResult << std::endl;
	}

	MainMenu::Return();
}

std::vector<std::string> Maintenance::CollectNamesOfMaintenanceProcedures(GsoapEndpointProxy* proxy)
{
	std::vector<std::string> procedures;

	if (proxy == NULL)
		return procedures;

	_tempuri__GetNamesOfMaintenanceProcedures get;
	_tempuri__GetNamesOfMaintenanceProceduresResponse resp;

	if (proxy->GetNamesOfMaintenanceProcedures(&get, resp) != SOAP_OK)
	{
		proxy->soap_stream_fault(std::cerr);
		return procedures;
	}

	if (resp.GetNamesOfMaintenanceProceduresResult == NULL || resp.GetNamesOfMaintenanceProceduresResult->string.size() <= 0)
		return procedures;
	
	procedures.swap(resp.GetNamesOfMaintenanceProceduresResult->string);

	return procedures;
}
