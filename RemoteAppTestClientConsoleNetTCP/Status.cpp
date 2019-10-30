#include "Status.h"

void Status::ShowMenu(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
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
			Status::GetOperationMode(proxy, error, heap);
			break;
		case '2':
			Status::IsChipDocked(proxy, error, heap);
			break;
		case '3':
			Status::IsSamplePlateTrayIn(proxy, error, heap);
			break;
		case '4':
			Status::HasMessage(proxy, error, heap);
			break;
		case '5':
			Status::GetMessage(proxy, error, heap);
			break;
		case '6':
			Status::HasErrors(proxy, error, heap);
			break;
		case '7':
			Status::GetErrors(proxy, error, heap);
			break;
		case '8':
			Status::HasWarnings(proxy, error, heap);
			break;
		case '9':
			Status::GetWarnings(proxy, error, heap);
			break;
		case 'q':
			close = true;
			break;
		default:
			break;
		}
	}
}

void Status::GetOperationMode(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	int operationMode = -1;

	if (GetOperationMode(proxy, error, heap, operationMode))
		std::cout << "\nCurrent operation mode is: " << operationMode << std::endl;
	else
		std::cout << "\nCouldn't get operation mode." << std::endl;

	MainMenu::Return();
}

bool Status::GetOperationMode(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap, int& operationMode)
{
	if (proxy == NULL)
		return false;

	int resp;

	if (NetTcpEndpoint_GetOperationMode(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
	{
		operationMode = resp;
		return true;
	}
	else
	{
		return false;
	}
}

void Status::IsChipDocked(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	int resp;

	if (NetTcpEndpoint_IsChipDocked(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nCurrent chip dock state is: " << resp << std::endl;
	else
		std::cout << "Error" << std::endl;

	MainMenu::Return();
}

void Status::IsSamplePlateTrayIn(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	int resp;

	if (NetTcpEndpoint_IsSamplePlateTrayIn(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nCurrent sample plate tray in state is: " << resp << std::endl;
	else
		std::cout << "Error" << std::endl;

	MainMenu::Return();
}

void Status::HasMessage(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	BOOL resp;

	if (NetTcpEndpoint_HasMessage(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nHas messages: " << resp << std::endl;
	else
		std::cout << "Error" << std::endl;

	MainMenu::Return();
}

void Status::GetMessage(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	unsigned int count;
	WCHAR** msg;

	if (NetTcpEndpoint_GetMessage(proxy, &count, &msg, heap, NULL, 0, NULL, error) == S_OK)
	{
		if (msg == NULL || count <= 0)
		{
			std::cout << "\nThere are no messages in the queue." << std::endl;
		}
		else if (count != 3)
		{
			std::cout << "\nThere message in the queue seems to have the wrong format.\nSize of the message is: " << count << std::endl;
			std::cout << "\nContetnt:" << std::endl;
			for (int i = 0; i < (int)count; i++)
			{
				std::cout << "[" << i << "] - " << msg[i] << std::endl;
			}
		}
		else
		{
			std::cout << "First message in queue:" << std::endl;
			std::cout << "Time:\t" << msg[0] << std::endl;
			std::cout << "Type:\t" << msg[1] << std::endl;
			std::cout << "Message:\t" << msg[2] << std::endl;
		}
	}
	else
		std::cout << "Error" << std::endl;

	MainMenu::Return();
}

void Status::HasErrors(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	BOOL resp;

	if (NetTcpEndpoint_HasErrors(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nHas errors: " << resp << std::endl;
	else
		std::cout << "Error" << std::endl;

	MainMenu::Return();
}

void Status::GetErrors(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	unsigned int count;
	WCHAR** msg;

	if (NetTcpEndpoint_GetErrors(proxy, &count, &msg, heap, NULL, 0, NULL, error) == S_OK)
	{
		if (msg == NULL || count <= 0)
		{
			std::cout << "\nSystem has no errors." << std::endl;
		}
		else
		{
			std::cout << "\nThe system has " << count << " errors." << std::endl;
			for (int i = 0; i < (int)count; i++)
			{
				std::cout << "[" << i << "] - " << msg[i] << std::endl;
			}
		}
	}
	else
		std::cout << "Error" << std::endl;
		
	MainMenu::Return();
}

void Status::HasWarnings(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;
	if (proxy == NULL)
		return;

	BOOL resp;

	if (NetTcpEndpoint_HasWarnings(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nHas warning(s): " << resp << std::endl;
	else
		std::cout << "Error" << std::endl;

	MainMenu::Return();
}

void Status::GetWarnings(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	unsigned int count;
	WCHAR** msg;

	if (NetTcpEndpoint_GetWarnings(proxy, &count, &msg, heap, NULL, 0, NULL, error) == S_OK)
	{
		if (msg == NULL || count <= 0)
		{
			std::cout << "\nSystem has no warnings." << std::endl;
		}
		else
		{
			std::cout << "\nThe system has " << count << " warnings." << std::endl;
			for (int i = 0; i < (int)count; i++)
			{
				std::cout << "[" << i << "] - " << msg[i] << std::endl;
			}
		}
	}
	else
		std::cout << "Error" << std::endl;

	MainMenu::Return();
}
