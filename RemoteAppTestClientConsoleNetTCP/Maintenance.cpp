#include "Maintenance.h"


void Maintenance::ShowMenu(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
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
			Maintenance::GetNamesOfMaintenanceProcedures(proxy,error,heap);
			break;
		case '2':
			Maintenance::RunMaintenanceProcedure(proxy, error, heap);
			break;
		case 'q':
			close = true;
			break;
		default:
			break;
		}
	}
}

void Maintenance::GetNamesOfMaintenanceProcedures(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	std::vector<std::string> procedures = Maintenance::CollectNamesOfMaintenanceProcedures(proxy,error,heap);
	
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

void Maintenance::RunMaintenanceProcedure(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;
	
	std::vector<std::string> procedures = Maintenance::CollectNamesOfMaintenanceProcedures(proxy,error,heap);

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

		BOOL resp;
		std::wstring pName = MainMenu::ToWstring(procedureName);
		
		if (NetTcpEndpoint_RunMaintenanceProcedure(proxy, (WCHAR*)pName.c_str(), &resp, heap, NULL, 0, NULL, error) != S_OK)
		{
			std::cout << "\nError while sending maintenance procedure " << procedureName << " to the server." << std::endl;
			return;
		}

		std::cout << "\nRunMaintenanceProcedure(" << procedureName << ") returned: " << resp << std::endl;
	}

	MainMenu::Return();
}

std::vector<std::string> Maintenance::CollectNamesOfMaintenanceProcedures(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	std::vector<std::string> procedures;

	if (proxy == NULL)
		return procedures;

	unsigned int count;
	WCHAR** msg;

	if (NetTcpEndpoint_GetNamesOfMaintenanceProcedures(proxy, &count, &msg, heap, NULL, 0, NULL, error) != S_OK)
		return procedures;

	if (msg == NULL || count <= 0)
		return procedures;
	
	for (int i = 0; i < (int)count; i++)
	{
		std::wstring ws(msg[i]);
		std::string str(ws.begin(), ws.end());
		procedures.push_back(str);
	}

	return procedures;
}
