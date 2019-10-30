#include "GetInformationAboutRunsets.h"

void GetInformationAboutRunsets::ShowMenu(GsoapEndpointProxy* proxy)
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
			GetInformationAboutRunsets::GetNamesOfRunsets(proxy);
			break;
		case '2':
			GetInformationAboutRunsets::GetNamesOfRunsetsOfAssayType(proxy);
			break;
		case '3':
			GetInformationAboutRunsets::GetAssayTypesOfAllRunsets(proxy);
			break;
		case '4':
			GetInformationAboutRunsets::GetNameOfCurrentRunset(proxy);
			break;
		case '5':
			GetInformationAboutRunsets::GetAssayTypeOfCurrentRunset(proxy);
			break;
		case '6':
			GetInformationAboutRunsets::GetAssayTypeOfRunset(proxy);
			break;
		case '7':
			GetInformationAboutRunsets::GetMethodNamesOfRunset(proxy);
			break;
		case 'q':
			close = true;
			break;
		default:
			break;
		}
	}
}

void GetInformationAboutRunsets::GetNamesOfRunsets(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	std::vector<std::string> runsets = CollectRunsets(proxy);

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

void GetInformationAboutRunsets::GetNamesOfRunsetsOfAssayType(GsoapEndpointProxy* proxy)
{
	std::vector<std::string> assays = CollectAssayTypes(proxy);
	if (assays.size() <= 0)
	{
		std::cout << "\nNo assays available." << std::endl;
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
		std::cout << "\n\nNames of runsets of assay type <" << assayType << "> will be collected." << std::endl;

		_tempuri__GetNamesOfRunsetsOfAssayType get;
		_tempuri__GetNamesOfRunsetsOfAssayTypeResponse resp;

		get.assayType = &assayType;
		if (proxy->GetNamesOfRunsetsOfAssayType(&get, resp) != SOAP_OK)
		{
			std::cout << "\nError while collecting names of runsets of assay type " << assayType << "." << std::endl;
			proxy->soap_stream_fault(std::cerr);
			return;
		}
		else if (resp.GetNamesOfRunsetsOfAssayTypeResult == NULL || resp.GetNamesOfRunsetsOfAssayTypeResult->string.size() <= 0)
		{
			std::cout << "\nNo runsets with the assay type <" << assayType << "> available." << std::endl;
			return;
		}
		else
		{
			std::cout << "\n" << resp.GetNamesOfRunsetsOfAssayTypeResult->string.size() << " runsets of the assay type <" << assayType << "> are available: " << std::endl;
			for (int i = 0; i < (int)resp.GetNamesOfRunsetsOfAssayTypeResult->string.size(); i++)
				std::cout << resp.GetNamesOfRunsetsOfAssayTypeResult->string.at(i) << std::endl;
		}
	}

	MainMenu::Return();
}

void GetInformationAboutRunsets::GetAssayTypesOfAllRunsets(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	std::vector<std::string> assays = CollectAssayTypes(proxy);

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

void GetInformationAboutRunsets::GetNameOfCurrentRunset(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__GetNameOfCurrentRunset get;
	_tempuri__GetNameOfCurrentRunsetResponse resp;

	if (proxy->GetNameOfCurrentRunset(&get, resp) == SOAP_OK)
		std::cout << "\nName of current runset is: " << *resp.GetNameOfCurrentRunsetResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void GetInformationAboutRunsets::GetAssayTypeOfCurrentRunset(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__GetAssayTypeOfCurrentRunset get;
	_tempuri__GetAssayTypeOfCurrentRunsetResponse resp;

	if (proxy->GetAssayTypeOfCurrentRunset(&get, resp) == SOAP_OK)
		std::cout << "\nAssay type of current runset is: " << *resp.GetAssayTypeOfCurrentRunsetResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void GetInformationAboutRunsets::GetAssayTypeOfRunset(GsoapEndpointProxy* proxy)
{
	std::vector<std::string> runsets = CollectRunsets(proxy);
	if (runsets.size() <= 0)
	{
		std::cout << "\nNo runsets available." << std::endl;
		MainMenu::Return();
		return;
	}
	
	std::cout << "\nSelect a runset:" << std::endl;
	for (int i = 0; i < (int)runsets.size(); i++)
		std::cout << i + 1 << "\t- " + runsets.at(i) << std::endl;
	std::cout << "0\t- none" << std::endl;
	std::cout << "Choice: ";
	int input;
	std::cin >> input;

	if (input > 0 && input <= (int)runsets.size())
	{
		std::string runset = runsets.at(input - 1);
		std::cout << "\n\nAssay type of runset <" << runset << "> will be collected." << std::endl;

		_tempuri__GetAssayTypeOfRunset get;
		_tempuri__GetAssayTypeOfRunsetResponse resp;

		get.runsetName = &runset;
		if (proxy->GetAssayTypeOfRunset(&get, resp) != SOAP_OK)
		{
			std::cout << "\nError while collecting assay type of runset " << runset << "." << std::endl;
			proxy->soap_stream_fault(std::cerr);
			return;
		}
		else if (resp.GetAssayTypeOfRunsetResult == NULL)
		{
			std::cout << "\nRunset has no assay type." << std::endl;
			return;
		}
		else
		{
			std::cout << "\nThe runset <" << runset << "> is of assay type: " << *resp.GetAssayTypeOfRunsetResult << std::endl;
		}
	}

	MainMenu::Return();
}

void GetInformationAboutRunsets::GetMethodNamesOfRunset(GsoapEndpointProxy* proxy)
{
	std::vector<std::string> runsets = CollectRunsets(proxy);
	if (runsets.size() <= 0)
	{
		std::cout << "\nNo runsets available." << std::endl;
		MainMenu::Return();
		return;
	}

	std::cout << "\nSelect a runset:" << std::endl;
	for (int i = 0; i < (int)runsets.size(); i++)
		std::cout << i + 1 << "\t- " + runsets.at(i) << std::endl;
	std::cout << "0\t- none" << std::endl;
	std::cout << "Choice: ";
	int input;
	std::cin >> input;

	if (input > 0 && input <= (int)runsets.size())
	{
		std::string runset = runsets.at(input - 1);
		std::cout << "\n\nNames of methods of runset <" << runset << "> will be collected." << std::endl;

		_tempuri__GetMethodNamesOfRunset get;
		_tempuri__GetMethodNamesOfRunsetResponse resp;

		get.runsetName = &runset;
		if (proxy->GetMethodNamesOfRunset(&get, resp) != SOAP_OK)
		{
			std::cout << "\nError while collecting names of methods of runset <" << runset << ">." << std::endl;
			proxy->soap_stream_fault(std::cerr);
			return;
		}
		else if (resp.GetMethodNamesOfRunsetResult == NULL || resp.GetMethodNamesOfRunsetResult->string.size() <= 0)
		{
			std::cout << "\nThe runset <" << runset << "> has no methods." << std::endl;
			return;
		}
		else
		{
			std::cout << "\nThe runset <" << runset << "> has " << resp.GetMethodNamesOfRunsetResult->string.size() << " methods." << std::endl;
			std::cout << "\nTheir names are:" << std::endl;
			for (int i = 0; i < (int)resp.GetMethodNamesOfRunsetResult->string.size(); i++)
				std::cout << resp.GetMethodNamesOfRunsetResult->string.at(i) << std::endl;
		}
	}

	MainMenu::Return();
}

std::vector<std::string> GetInformationAboutRunsets::CollectRunsets(GsoapEndpointProxy* proxy)
{
	std::vector<std::string> runsets;

	if (proxy == NULL)
		return runsets;

	_tempuri__GetNamesOfRunsets get;
	_tempuri__GetNamesOfRunsetsResponse resp;

	if (proxy->GetNamesOfRunsets(&get, resp) != SOAP_OK)
	{
		std::cout << "\nError while sending/receiving." << std::endl;
		proxy->soap_stream_fault(std::cerr);
		return runsets;
	}

	if (resp.GetNamesOfRunsetsResult == NULL || resp.GetNamesOfRunsetsResult->string.size() <= 0)
		return runsets;

	runsets.swap(resp.GetNamesOfRunsetsResult->string);

	return runsets;
}

std::vector<std::string> GetInformationAboutRunsets::CollectAssayTypes(GsoapEndpointProxy* proxy)
{
	std::vector<std::string> assays;

	if (proxy == NULL)
		return assays;

	_tempuri__GetAssayTypesOfAllRunsets get;
	_tempuri__GetAssayTypesOfAllRunsetsResponse resp;

	if (proxy->GetAssayTypesOfAllRunsets(&get, resp) != SOAP_OK)
	{
		std::cout << "\nError while sending/receiving." << std::endl;
		proxy->soap_stream_fault(std::cerr);
		return assays;
	}

	if (resp.GetAssayTypesOfAllRunsetsResult == NULL || resp.GetAssayTypesOfAllRunsetsResult->string.size() <= 0)
		return assays;

	assays.swap(resp.GetAssayTypesOfAllRunsetsResult->string);

	return assays;
}
