#include "GetInformationAboutMethods.h"

void GetInformationAboutMethods::ShowMenu(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	bool close = false;
	char input;
	while (!close)
	{
		MainMenu::ClearScreenAndShowHeader();
		std::cout << "*             Get Information About Methods            *\n********************************************************\n";
		std::cout << "\n1\t- GetNamesOfMethods";
		std::cout << "\n2\t- GetNamesOfMethodsOfAssayType";
		std::cout << "\n3\t- GetAssayTypesOfAllMethods";
		std::cout << "\n4\t- GetNameOfCurrentMethod";
		std::cout << "\n5\t- GetAssayTypeOfCurrentMethod";
		std::cout << "\n6\t- GetAssayTypeOfMethod";
		std::cout << "\nq\t- quit";

		std::cout << "\nChoice: ";
		std::cin >> input;
		switch (input)
		{
		case '1':
			GetInformationAboutMethods::GetNamesOfMethods(proxy, error, heap);
			break;
		case '2':
			GetInformationAboutMethods::GetNamesOfMethodsOfAssayType(proxy, error, heap);
			break;
		case '3':
			GetInformationAboutMethods::GetAssayTypesOfAllMethods(proxy, error, heap);
			break;
		case '4':
			GetInformationAboutMethods::GetNameOfCurrentMethod(proxy, error, heap);
			break;
		case '5':
			GetInformationAboutMethods::GetAssayTypeOfCurrentMethod(proxy, error, heap);
			break;
		case '6':
			GetInformationAboutMethods::GetAssayTypeOfMethod(proxy, error, heap);
			break;
		case 'q':
			close = true;
			break;
		default:
			break;
		}
	}
}

void GetInformationAboutMethods::GetNamesOfMethods(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	std::vector<std::string> methods = CollectNamesOfMethods(proxy, error, heap);

	if (methods.size() <= 0)
	{
		std::cout << "\nNo names of methods available." << std::endl;
	}
	else
	{
		std::cout << "\nThe following methods are available:" << std::endl;
		for (int i = 0; i < (int)methods.size(); i++)
			std::cout << methods.at(i) << std::endl;
	}

	MainMenu::Return();
}

void GetInformationAboutMethods::GetNamesOfMethodsOfAssayType(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	std::vector<std::string> assays = CollectAssayTypes(proxy, error, heap);
	if (assays.size() <= 0)
	{
		std::cout << "\nNo assay types available." << std::endl;
		MainMenu::Return();
		return;
	}

	std::cout << "\nSelect an assay type:" << std::endl;
	for (int i = 0; i < (int)assays.size(); i++)
		std::cout << i + 1 << "\t- " + assays.at(i) << std::endl;
	std::cout << "0\t- none" << std::endl;
	std::cout << "Choice: ";
	int input;
	std::cin >> input;

	if (input > 0 && input <= (int)assays.size())
	{
		std::string assayType = assays.at(input - 1);
		std::cout << "\n\nNames of methods of assya type <" << assayType << "> will be collected." << std::endl;

		unsigned int count;
		std::wstring pName = MainMenu::ToWstring(assayType);
		WCHAR** resp;

		if (NetTcpEndpoint_GetNamesOfMethodsOfAssayType(proxy, (WCHAR*)pName.c_str(), &count, &resp, heap, NULL, 0, NULL, error) != S_OK)
		{
			std::cout << "\nError while collecting names of methods of assay type " << assayType << "." << std::endl;
			return;
		}
		else if (resp == NULL || count <= 0)
		{
			std::cout << "\nNo methods with the assay type <" << assayType << "> available." << std::endl;
			return;
		}
		else
		{
			std::cout << "\n"<< count << " methods of the assay type <" << assayType << "> are available: " << std::endl;
			for(int i = 0; i < (int)count; i++)
				std::cout << MainMenu::ToString(std::wstring(resp[i])) << std::endl;
		}
	}

	MainMenu::Return();
}

void GetInformationAboutMethods::GetAssayTypesOfAllMethods(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	std::vector<std::string> assays = CollectAssayTypes(proxy,error,heap);

	if (assays.size() <= 0)
	{
		std::cout << "\nNo assay types available." << std::endl;
	}
	else
	{
		std::cout << "\nThe following assay types are available:" << std::endl;
		for (int i = 0; i < (int)assays.size(); i++)
			std::cout << assays.at(i) << std::endl;
	}

	MainMenu::Return();
}

void GetInformationAboutMethods::GetNameOfCurrentMethod(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	WCHAR* resp;

	if (NetTcpEndpoint_GetNameOfCurrentMethod(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nName of current method is: " << MainMenu::ToString(std::wstring(resp)) << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void GetInformationAboutMethods::GetAssayTypeOfCurrentMethod(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	WCHAR* resp;

	if (NetTcpEndpoint_GetAssayTypeOfCurrentMethod(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nAssay type of current method is: " << MainMenu::ToString(std::wstring(resp)) << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void GetInformationAboutMethods::GetAssayTypeOfMethod(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	std::vector<std::string> methods = CollectNamesOfMethods(proxy,error,heap);
	if (methods.size() <= 0)
	{
		std::cout << "\nNo methods available." << std::endl;
		MainMenu::Return();
		return;
	}
	
	std::cout << "\nSelect a method:" << std::endl;
	for (int i = 0; i < (int)methods.size(); i++)
		std::cout << i + 1 << "\t- " + methods.at(i) << std::endl;
	std::cout << "0\t- none" << std::endl;
	std::cout << "Choice: ";
	int input;
	std::cin >> input;

	if (input > 0 && input <= (int)methods.size())
	{
		std::string method = methods.at(input - 1);
		std::cout << "\n\nAssay type of method <" << method << "> will be collected." << std::endl;

		std::wstring pName = MainMenu::ToWstring(method);
		WCHAR* resp;

		if (NetTcpEndpoint_GetAssayTypeOfMethod(proxy, (WCHAR*)pName.c_str(), &resp, heap, NULL, 0, NULL, error) != S_OK)
		{
			std::cout << "\nError while collecting assay type of method " << method << "." << std::endl;
			return;
		}
		else
		{
			std::cout << "\nThe method <" << method << "> is of assay type: " << MainMenu::ToString(std::wstring(resp)) << std::endl;
		}
	}

	MainMenu::Return();
}

std::vector<std::string> GetInformationAboutMethods::CollectNamesOfMethods(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	std::vector<std::string> methods;

	if (proxy == NULL)
		return methods;

	unsigned int count;
	WCHAR** msg;

	if (NetTcpEndpoint_GetNamesOfMethods(proxy, &count, &msg, heap, NULL, 0, NULL, error) != S_OK)
		return methods;

	if (msg == NULL || count <= 0)
		return methods;

	for (int i = 0; i < (int)count; i++)
		methods.push_back(MainMenu::ToString(std::wstring(msg[i])));

	return methods;
}

std::vector<std::string> GetInformationAboutMethods::CollectAssayTypes(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	std::vector<std::string> assays;

	if (proxy == NULL)
		return assays;

	unsigned int count;
	WCHAR** msg;

	if (NetTcpEndpoint_GetAssayTypesOfAllMethods(proxy, &count, &msg, heap, NULL, 0, NULL, error) != S_OK)
		return assays;

	if (msg == NULL || count <= 0)
		return assays;

	for (int i = 0; i < (int)count; i++)
		assays.push_back(MainMenu::ToString(std::wstring(msg[i])));

	return assays;
}

