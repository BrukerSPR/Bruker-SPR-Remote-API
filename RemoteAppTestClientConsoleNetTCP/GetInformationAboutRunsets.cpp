#include "GetInformationAboutRunsets.h"

void GetInformationAboutRunsets::ShowMenu(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	bool close = false;
	char input;
	while (!close)
	{
		MainMenu::ClearScreenAndShowHeader();
		std::cout << "*             Get Information About Runsets            *\n********************************************************\n";
		std::cout << "\n1\t- GetNamesOfRunsets";
		std::cout << "\n2\t- GetNamesOfRunsetsOfAssayType";
		std::cout << "\n3\t- GetAssayTypesOfAllRunsets";
		std::cout << "\n4\t- GetNameOfCurrentRunset";
		std::cout << "\n5\t- GetAssayTypeOfCurrentRunset";
		std::cout << "\n6\t- GetAssayTypeOfRunset";
		std::cout << "\n7\t- GetMethodNamesOfRunset";
		std::cout << "\nq\t- quit";

		std::cout << "\nChoice: ";
		std::cin >> input;
		switch (input)
		{
		case '1':
			GetInformationAboutRunsets::GetNamesOfRunsets(proxy,error,heap);
			break;
		case '2':
			GetInformationAboutRunsets::GetNamesOfRunsetsOfAssayType(proxy, error, heap);
			break;
		case '3':
			GetInformationAboutRunsets::GetAssayTypesOfAllRunsets(proxy, error, heap);
			break;
		case '4':
			GetInformationAboutRunsets::GetNameOfCurrentRunset(proxy, error, heap);
			break;
		case '5':
			GetInformationAboutRunsets::GetAssayTypeOfCurrentRunset(proxy, error, heap);
			break;
		case '6':
			GetInformationAboutRunsets::GetAssayTypeOfRunset(proxy, error, heap);
			break;
		case '7':
			GetInformationAboutRunsets::GetMethodNamesOfRunset(proxy, error, heap);
			break;
		case 'q':
			close = true;
			break;
		default:
			break;
		}
	}
}

void GetInformationAboutRunsets::GetNamesOfRunsets(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	std::vector<std::string> runsets = CollectRunsets(proxy, error, heap);

	if (runsets.size() <= 0)
	{
		std::cout << "\nNo runsets available." << std::endl;
	}
	else
	{
		std::cout << "\nThe following runsets are available:" << std::endl;
		for (int i = 0; i < (int)runsets.size(); i++)
			std::cout << runsets.at(i) << std::endl;
	}

	MainMenu::Return();
}

void GetInformationAboutRunsets::GetNamesOfRunsetsOfAssayType(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	std::vector<std::string> assays = CollectAssayTypes(proxy, error, heap);
	if (assays.size() <= 0)
	{
		std::cout << "\nNo assays available." << std::endl;
		MainMenu::Return();
		return;
	}
	
	std::cout << "\nSelect an assay type:" << std::endl;
	for (int i = 0; i < (int)assays.size(); i++)
		std::cout << i + 1 << "\t- " + assays.at(i) << std::endl;
	std::cout << "0\t- none" << std::endl;
	std::cout << "\nChoice: ";
	int input;
	std::cin >> input;

	if (input > 0 && input <= (int)assays.size())
	{
		std::string assayType = assays.at(input - 1);
		std::cout << "\n\nNames of methods of assya type <" << assayType << "> will be collected." << std::endl;

		unsigned int count;
		std::wstring pName = MainMenu::ToWstring(assayType);
		WCHAR** resp;

		if (NetTcpEndpoint_GetNamesOfRunsetsOfAssayType(proxy, (WCHAR*)pName.c_str(), &count, &resp, heap, NULL, 0, NULL, error) != S_OK)
		{
			std::cout << "\nError while collecting names of runsets of assay type " << assayType << "." << std::endl;
			return;
		}
		else if (resp == NULL || count <= 0)
		{
			std::cout << "\nNo runsets with the assay type <" << assayType << "> available." << std::endl;
			return;
		}
		else
		{
			std::cout << "\n" << count << " runsets of the assay type <" << assayType << "> are available: " << std::endl;
			for (int i = 0; i < (int)count; i++)
				std::cout << MainMenu::ToString(std::wstring(resp[i])) << std::endl;
		}
	}

	MainMenu::Return();
}

void GetInformationAboutRunsets::GetAssayTypesOfAllRunsets(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	std::vector<std::string> assays = CollectAssayTypes(proxy,error,heap);

	if (assays.size() <= 0)
	{
		std::cout << "\nNo assays available." << std::endl;
	}
	else
	{
		std::cout << "\nThe following assay types are available:" << std::endl;
		for (int i = 0; i < (int)assays.size(); i++)
			std::cout << assays.at(i) << std::endl;
	}

	MainMenu::Return();
}

void GetInformationAboutRunsets::GetNameOfCurrentRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	WCHAR* resp;

	if (NetTcpEndpoint_GetNameOfCurrentRunset(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nName of current runset is: " << MainMenu::ToString(std::wstring(resp)) << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void GetInformationAboutRunsets::GetAssayTypeOfCurrentRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	WCHAR* resp;

	if (NetTcpEndpoint_GetAssayTypeOfCurrentRunset(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nAssay type of current runset is: " << MainMenu::ToString(std::wstring(resp)) << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void GetInformationAboutRunsets::GetAssayTypeOfRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	std::vector<std::string> runsets = CollectRunsets(proxy,error, heap);
	if (runsets.size() <= 0)
	{
		std::cout << "\nNo runsets available." << std::endl;
		MainMenu::Return();
		return;
	}
	else
	{
		std::cout << "\nSelect a runset:" << std::endl;
		for (int i = 0; i < (int)runsets.size(); i++)
			std::cout << i + 1 << "\t- " + runsets.at(i) << std::endl;
		std::cout << "0\t- none" << std::endl;
	}

	std::cout << "\nChoice: ";
	int input;
	std::cin >> input;

	if (input > 0 && input <= (int)runsets.size())
	{
		std::string runset = runsets.at(input - 1);
		std::cout << "\n\nAssay type of runset <" << runset << "> will be collected." << std::endl;

		std::wstring pName = MainMenu::ToWstring(runset);
		WCHAR* resp;

		if (NetTcpEndpoint_GetAssayTypeOfRunset(proxy, (WCHAR*)pName.c_str(), &resp, heap, NULL, 0, NULL, error) != S_OK)
		{
			std::cout << "\nError while collecting assay type of runset " << runset << "." << std::endl;
			return;
		}
		else
		{
			std::cout << "\nThe runset <" << runset << "> is of assay type: " << MainMenu::ToString(std::wstring(resp)) << std::endl;
		}
	}

	MainMenu::Return();
}

void GetInformationAboutRunsets::GetMethodNamesOfRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	std::vector<std::string> runsets = CollectRunsets(proxy,error,heap);
	if (runsets.size() <= 0)
	{
		std::cout << "\nNo runsets available." << std::endl;
		MainMenu::Return();
		return;
	}
	else
	{
		std::cout << "\nSelect a runset:" << std::endl;
		for (int i = 0; i < (int)runsets.size(); i++)
			std::cout << i + 1 << "\t- " + runsets.at(i) << std::endl;
		std::cout << "0\t- none" << std::endl;
	}

	std::cout << "\nChoice: ";
	int input;
	std::cin >> input;

	if (input > 0 && input <= (int)runsets.size())
	{
		std::string runset = runsets.at(input - 1);
		std::cout << "\n\nNames of methods of runset <" << runset << "> will be collected." << std::endl;

		unsigned int count;
		std::wstring pName = MainMenu::ToWstring(runset);
		WCHAR** resp;

		if (NetTcpEndpoint_GetMethodNamesOfRunset(proxy, (WCHAR*)pName.c_str(), &count, &resp, heap, NULL, 0, NULL, error) != S_OK)
		{
			std::cout << "\nError while collecting names of methods of runset " << runset << "." << std::endl;
			return;
		}
		else if (resp == NULL || count <= 0)
		{
			std::cout << "\nThe runset <" << runset << "> has no methods." << std::endl;
			return;
		}
		else
		{
			std::cout << "\nThe runset <" << runset << "> has " << count << " methods." << std::endl;
			std::cout << "Their names are:" << std::endl;
			for (int i = 0; i < (int)count; i++)
				std::cout << MainMenu::ToString(std::wstring(resp[i])) << std::endl;
		}
	}

	MainMenu::Return();
}

std::vector<std::string> GetInformationAboutRunsets::CollectRunsets(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	std::vector<std::string> runsets;

	if (proxy == NULL)
		return runsets;

	unsigned int count;
	WCHAR** msg;

	if (NetTcpEndpoint_GetNamesOfRunsets(proxy, &count, &msg, heap, NULL, 0, NULL, error) != S_OK)
		return runsets;

	if (msg == NULL || count <= 0)
		return runsets;

	for (int i = 0; i < (int)count; i++)
		runsets.push_back(MainMenu::ToString(std::wstring(msg[i])));

	return runsets;
}

std::vector<std::string> GetInformationAboutRunsets::CollectAssayTypes(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	std::vector<std::string> assays;

	if (proxy == NULL)
		return assays;

	unsigned int count;
	WCHAR** msg;

	if (NetTcpEndpoint_GetAssayTypesOfAllRunsets(proxy, &count, &msg, heap, NULL, 0, NULL, error) != S_OK)
		return assays;

	if (msg == NULL || count <= 0)
		return assays;

	for (int i = 0; i < (int)count; i++)
		assays.push_back(MainMenu::ToString(std::wstring(msg[i])));

	return assays;
}
