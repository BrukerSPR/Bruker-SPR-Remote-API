#include "Status.h"

void Status::ShowMenu(GsoapEndpointProxy* proxy)
{
	bool close = false;
	char input;
	while (!close)
	{
		MainMenu::ClearScreenAndShowHeader();
		std::cout << "*                   Service Methods                    *\n********************************************************\n";
		std::cout << "\n1\t- GetOperationMode";
		std::cout << "\n2\t- IsChipDocked";
		std::cout << "\n3\t- IsSamplePlateTrayIn";
		std::cout << "\n4\t- HasMessage";
		std::cout << "\n5\t- GetMessage";
		std::cout << "\n6\t- HasErrors";
		std::cout << "\n7\t- GetErrors";
		std::cout << "\n8\t- HasWarnings";
		std::cout << "\n9\t- GetWarnings";
		std::cout << "\nq\t- quit";

		std::cout << "\nChoice: ";
		std::cin >> input;
		switch (input)
		{
		case '1':
			Status::GetOperationMode(proxy);
			break;
		case '2':
			Status::IsChipDocked(proxy);
			break;
		case '3':
			Status::IsSamplePlateTrayIn(proxy);
			break;
		case '4':
			Status::HasMessage(proxy);
			break;
		case '5':
			Status::GetMessage(proxy);
			break;
		case '6':
			Status::HasErrors(proxy);
			break;
		case '7':
			Status::GetErrors(proxy);
			break;
		case '8':
			Status::HasWarnings(proxy);
			break;
		case '9':
			Status::GetWarnings(proxy);
			break;
		case 'q':
			close = true;
			break;
		default:
			break;
		}
	}
}

void Status::GetOperationMode(GsoapEndpointProxy* proxy)
{
	int operationMode = -1;

	if (GetOperationMode(proxy, operationMode))
		std::cout << "\nCurrent operation mode is: " << operationMode << std::endl;
	else
		std::cout << "\nCouldn't get operation mode." << std::endl;

	MainMenu::Return();
}

bool Status::GetOperationMode(GsoapEndpointProxy* proxy, int& operationMode)
{
	if (proxy == NULL)
		return false;

	_tempuri__GetOperationMode get;
	_tempuri__GetOperationModeResponse resp;

	if ((proxy->GetOperationMode(&get, resp) == SOAP_OK) && (resp.GetOperationModeResult != NULL))
	{
		operationMode = *resp.GetOperationModeResult;
		return true;
	}
	else
	{
		proxy->soap_stream_fault(std::cerr);
		return false;
	}
}

void Status::IsChipDocked(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__IsChipDocked get;
	_tempuri__IsChipDockedResponse resp;

	if (proxy->IsChipDocked(&get, resp) == SOAP_OK)
		std::cout << "\nCurrent chip dock state is: " << *resp.IsChipDockedResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void Status::IsSamplePlateTrayIn(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__IsSamplePlateTrayIn get;
	_tempuri__IsSamplePlateTrayInResponse resp;

	if (proxy->IsSamplePlateTrayIn(&get, resp) == SOAP_OK)
		std::cout << "\nCurrent sample plate tray in state is: " << *resp.IsSamplePlateTrayInResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void Status::HasMessage(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__HasMessage get;
	_tempuri__HasMessageResponse resp;

	if (proxy->HasMessage(&get, resp) == SOAP_OK)
		std::cout << "\nHas messages: " << *resp.HasMessageResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void Status::GetMessage(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__GetMessage get;
	_tempuri__GetMessageResponse resp;

	if (proxy->GetMessage(&get, resp) == SOAP_OK)
	{
		if (resp.GetMessageResult == NULL || resp.GetMessageResult->string.size() <= 0)
		{
			std::cout << "\nThere are no messages in the queue." << std::endl;
		}
		else if (resp.GetMessageResult->string.size() != 3)
		{
			std::cout << "\nThere message in the queue seems to have the wrong format.\nSize of the message is: " << resp.GetMessageResult->string.size() << std::endl;
			std::cout << "\nContetnt:" << std::endl;
			for (int i = 0; i < (int)resp.GetMessageResult->string.size(); i++)
			{
				std::cout << "[" << i << "] - " << resp.GetMessageResult->string.at(i) << std::endl;
			}
		}
		else
		{
			std::cout << "First message in queue:" << std::endl;
			std::cout << "Time:\t" << resp.GetMessageResult->string.at(0) << std::endl;
			std::cout << "Type:\t" << resp.GetMessageResult->string.at(1) << std::endl;
			std::cout << "Message:\t" << resp.GetMessageResult->string.at(2) << std::endl;
		}
	}
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void Status::HasErrors(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__HasErrors get;
	_tempuri__HasErrorsResponse resp;

	if (proxy->HasErrors(&get, resp) == SOAP_OK)
		std::cout << "\nHas error(s): " << *resp.HasErrorsResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void Status::GetErrors(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__GetErrors get;
	_tempuri__GetErrorsResponse resp;

	if (proxy->GetErrors(&get, resp) == SOAP_OK)
	{
		if (resp.GetErrorsResult == NULL || resp.GetErrorsResult->string.size() <= 0)
		{
			std::cout << "\nSystem has no errors." << std::endl;
		}
		else
		{
			int count = resp.GetErrorsResult->string.size();
			std::cout << "\nThe system has " << count << " errors." << std::endl;
			for (int i = 0; i < count; i++)
			{
				std::cout << "[" << i << "] - " << resp.GetErrorsResult->string.at(i) << std::endl;
			}
		}
	}
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void Status::HasWarnings(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__HasWarnings get;
	_tempuri__HasWarningsResponse resp;

	if (proxy->HasWarnings(&get, resp) == SOAP_OK)
		std::cout << "\nHas warning(s): " << *resp.HasWarningsResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);
	
	MainMenu::Return();
}

void Status::GetWarnings(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__GetWarnings get;
	_tempuri__GetWarningsResponse resp;

	if (proxy->GetWarnings(&get, resp) == SOAP_OK)
	{
		if (resp.GetWarningsResult == NULL || resp.GetWarningsResult->string.size() <= 0)
		{
			std::cout << "\nSystem has no warnings." << std::endl;
		}
		else
		{
			int count = resp.GetWarningsResult->string.size();
			std::cout << "\nThe system has " << count << " warnings." << std::endl;
			for (int i = 0; i < count; i++)
			{
				std::cout << "[" << i << "] - " << resp.GetWarningsResult->string.at(i) << std::endl;
			}
		}
	}
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}
