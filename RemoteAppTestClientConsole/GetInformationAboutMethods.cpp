#include "GetInformationAboutMethods.h"

void GetInformationAboutMethods::ShowMenu(GsoapEndpointProxy* proxy)
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
			GetInformationAboutMethods::GetNamesOfMethods(proxy);
			break;
		case '2':
			GetInformationAboutMethods::GetNamesOfMethodsOfAssayType(proxy);
			break;
		case '3':
			GetInformationAboutMethods::GetAssayTypesOfAllMethods(proxy);
			break;
		case '4':
			GetInformationAboutMethods::GetNameOfCurrentMethod(proxy);
			break;
		case '5':
			GetInformationAboutMethods::GetAssayTypeOfCurrentMethod(proxy);
			break;
		case '6':
			GetInformationAboutMethods::GetAssayTypeOfMethod(proxy);
			break;
		case 'q':
			close = true;
			break;
		default:
			break;
		}
	}
}

void GetInformationAboutMethods::GetNamesOfMethods(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	std::vector<std::string> methods = CollectNamesOfMethods(proxy);

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

void GetInformationAboutMethods::GetNamesOfMethodsOfAssayType(GsoapEndpointProxy* proxy)
{
	std::vector<std::string> assays = CollectAssayTypes(proxy);
	if (assays.size() <= 0)
	{
		std::cout << "\nNo assay types available." << std::endl;
		MainMenu::Return();
		return;
	}
	else
	{
		std::cout << "\nSelect an assay type:" << std::endl;
		for (int i = 0; i < (int)assays.size(); i++)
			std::cout << i + 1 << "\t- " + assays.at(i) << std::endl;
		std::cout << "0\t- none" << std::endl;
	}

	std::cout << "\nChoice: ";
	int input;
	std::cin >> input;

	if (input > 0 && input <= (int)assays.size())
	{
		std::string assayType = assays.at(input - 1);
		std::cout << "\n\nNames of methods of assya type <" << assayType << "> will be collected." << std::endl;

		_tempuri__GetNamesOfMethodsOfAssayType get;
		_tempuri__GetNamesOfMethodsOfAssayTypeResponse resp;

		get.assayType = &assayType;
		if (proxy->GetNamesOfMethodsOfAssayType(&get, resp) != SOAP_OK)
		{
			std::cout << "\nError while collecting names of methods of assay type " << assayType << "." << std::endl;
			proxy->soap_stream_fault(std::cerr);
			return;
		}
		else if (resp.GetNamesOfMethodsOfAssayTypeResult == NULL || resp.GetNamesOfMethodsOfAssayTypeResult->string.size() <= 0)
		{
			std::cout << "\nNo methods with the assay type <" << assayType << "> available." << std::endl;
			return;
		}
		else
		{
			std::cout << "\n"<< resp.GetNamesOfMethodsOfAssayTypeResult->string.size() << " methods of the assay type <" << assayType << "> are available: " << std::endl;
			for(int i = 0; i < (int)resp.GetNamesOfMethodsOfAssayTypeResult->string.size(); i++)
				std::cout << resp.GetNamesOfMethodsOfAssayTypeResult->string.at(i) << std::endl;
		}
	}

	MainMenu::Return();
}

void GetInformationAboutMethods::GetAssayTypesOfAllMethods(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	std::vector<std::string> assays = CollectAssayTypes(proxy);

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

void GetInformationAboutMethods::GetNameOfCurrentMethod(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__GetNameOfCurrentMethod get;
	_tempuri__GetNameOfCurrentMethodResponse resp;

	if (proxy->GetNameOfCurrentMethod(&get, resp) == SOAP_OK)
		std::cout << "\nName of current method is: " << *resp.GetNameOfCurrentMethodResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void GetInformationAboutMethods::GetAssayTypeOfCurrentMethod(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__GetAssayTypeOfCurrentMethod get;
	_tempuri__GetAssayTypeOfCurrentMethodResponse resp;

	if (proxy->GetAssayTypeOfCurrentMethod(&get, resp) == SOAP_OK)
		std::cout << "\nAssay type of current method is: " << *resp.GetAssayTypeOfCurrentMethodResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void GetInformationAboutMethods::GetAssayTypeOfMethod(GsoapEndpointProxy* proxy)
{
	std::vector<std::string> methods = CollectNamesOfMethods(proxy);
	if (methods.size() <= 0)
	{
		std::cout << "\nNo methods available." << std::endl;
		MainMenu::Return();
		return;
	}
	else
	{
		std::cout << "\nSelect a method:" << std::endl;
		for (int i = 0; i < (int)methods.size(); i++)
			std::cout << i + 1 << "\t- " + methods.at(i) << std::endl;
		std::cout << "0\t- none" << std::endl;
	}

	std::cout << "\nChoice: ";
	int input;
	std::cin >> input;

	if (input > 0 && input <= (int)methods.size())
	{
		std::string method = methods.at(input - 1);
		std::cout << "\n\nAssay type of method <" << method << "> will be collected." << std::endl;

		_tempuri__GetAssayTypeOfMethod get;
		_tempuri__GetAssayTypeOfMethodResponse resp;

		get.methodName = &method;
		if (proxy->GetAssayTypeOfMethod(&get, resp) != SOAP_OK || resp.GetAssayTypeOfMethodResult == NULL)
		{
			std::cout << "\nError while collecting assay type of method " << method << "." << std::endl;
			proxy->soap_stream_fault(std::cerr);
			return;
		}
		else
		{
			std::cout << "\nThe method <" << method << "> is of assay type: " << *resp.GetAssayTypeOfMethodResult << std::endl;
		}
	}

	MainMenu::Return();
}

std::vector<std::string> GetInformationAboutMethods::CollectNamesOfMethods(GsoapEndpointProxy* proxy)
{
	std::vector<std::string> methods;

	if (proxy == NULL)
		return methods;

	_tempuri__GetNamesOfMethods get;
	_tempuri__GetNamesOfMethodsResponse resp;

	if (proxy->GetNamesOfMethods(&get, resp) != SOAP_OK)
	{
		std::cout << "Error while sending/receiving." << std::endl;
		proxy->soap_stream_fault(std::cerr);
		return methods;
	}

	if (resp.GetNamesOfMethodsResult == NULL || resp.GetNamesOfMethodsResult->string.size() <= 0)
		return methods;

	methods.swap(resp.GetNamesOfMethodsResult->string);

	return methods;
}

std::vector<std::string> GetInformationAboutMethods::CollectAssayTypes(GsoapEndpointProxy* proxy)
{
	std::vector<std::string> assays;

	if (proxy == NULL)
		return assays;

	_tempuri__GetAssayTypesOfAllMethods get;
	_tempuri__GetAssayTypesOfAllMethodsResponse resp;

	if (proxy->GetAssayTypesOfAllMethods(&get, resp) != SOAP_OK)
	{
		std::cout << "Error while sending/receiving." << std::endl;
		proxy->soap_stream_fault(std::cerr);
		return assays;
	}

	if (resp.GetAssayTypesOfAllMethodsResult == NULL || resp.GetAssayTypesOfAllMethodsResult->string.size() <= 0)
		return assays;

	assays.swap(resp.GetAssayTypesOfAllMethodsResult->string);

	return assays;
}
