#include "PrepareAndRunAssays.h"

void PrepareAndRunAssays::ShowMenu(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	bool close = false;
	char input;
	while (!close)
	{
		MainMenu::ClearScreenAndShowHeader();
		std::cout << "*                 Prepare And Run Assays               *\n********************************************************\n";
		std::cout << "\n1\t- SelectMethod";
		std::cout << "\n2\t- SelectRunset";
		std::cout << "\n3\t- CreateRunset";
		std::cout << "\n4\t- SetSamplePlateId";
		std::cout << "\n5\t- GetSamplePlateId";
		std::cout << "\n6\t- GetCurrentSamplePlateId";
		std::cout << "\n7\t- MoveSamplePlateTrayOut";
		std::cout << "\n8\t- MoveSamplePlateTrayIn";
		std::cout << "\n9\t- StartSelectedRunset";
		std::cout << "\na\t- StartSelectedRunsetFrom";
		std::cout << "\nb\t- PauseRunsetAfter";
		std::cout << "\nc\t- ResumeRunset";
		std::cout << "\nd\t- ResetRunset";
		std::cout << "\ne\t- AbortScript";
		std::cout << "\nf\t- LeaveStandby";
		std::cout << "\ng\t- SetStandbyAfterFinish";
		std::cout << "\nh\t- GetStandbyAfterFinish";
		std::cout << "\nq\t- quit";

		std::cout << "\nChoice: ";
		std::cin >> input;
		switch (input)
		{
		case '1':
			PrepareAndRunAssays::SelectMethod(proxy,error,heap);
			break;
		case '2':
			PrepareAndRunAssays::SelectRunset(proxy,error,heap);
			break;
		case '3':
			PrepareAndRunAssays::CreateRunset(proxy,error,heap);
			break;
		case '4':
			PrepareAndRunAssays::SetSamplePlateId(proxy,error,heap);
			break;
		case '5':
			PrepareAndRunAssays::GetSamplePlateId(proxy,error,heap);
			break;
		case '6':
			PrepareAndRunAssays::GetCurrentSamplePlateId(proxy,error,heap);
			break;
		case '7':
			PrepareAndRunAssays::MoveSamplePlateTrayOut(proxy,error,heap);
			break;
		case '8':
			PrepareAndRunAssays::MoveSamplePlateTrayIn(proxy,error,heap);
			break;
		case '9':
			PrepareAndRunAssays::StartSelectedRunset(proxy,error,heap);
			break;
		case 'a':
			PrepareAndRunAssays::StartSelectedRunsetFrom(proxy,error,heap);
			break;
		case 'b':
			PrepareAndRunAssays::PauseRunsetAfter(proxy,error,heap);
			break;
		case 'c':
			PrepareAndRunAssays::ResumeRunset(proxy,error,heap);
			break;
		case 'd':
			PrepareAndRunAssays::ResetRunset(proxy,error,heap);
			break;
		case 'e':
			PrepareAndRunAssays::AbortScript(proxy,error,heap);
			break;
		case 'f':
			PrepareAndRunAssays::LeaveStandby(proxy,error,heap);
			break;
		case 'g':
			PrepareAndRunAssays::SetStandbyAfterFinish(proxy,error,heap);
			break;
		case 'h':
			PrepareAndRunAssays::GetStandbyAfterFinish(proxy,error,heap);
			break;
		case 'q':
			close = true;
			break;
		default:
			break;
		}
	}
}

void PrepareAndRunAssays::SelectMethod(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	std::string methodName;
	std::cout << "Method Name: ";
	std::getline(std::cin, methodName);
	std::getline(std::cin, methodName);

	std::wstring pName = MainMenu::ToWstring(methodName);
	BOOL resp;

	if (NetTcpEndpoint_SelectMethod(proxy, (WCHAR*)pName.c_str(), &resp, heap, NULL, 0, NULL, error) != S_OK)
		std::cout << "\nSelectMethod(" << methodName << ") returned: " << resp << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return(true);
}

void PrepareAndRunAssays::SelectRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	std::string runset;
	std::cout << "Runset Name: ";
	std::getline(std::cin, runset);
	std::getline(std::cin, runset);

	std::wstring pName = MainMenu::ToWstring(runset);
	BOOL resp;

	if (NetTcpEndpoint_SelectRunset(proxy, (WCHAR*)pName.c_str(), &resp, heap, NULL, 0, NULL, error) != S_OK)
		std::cout << "\nSelectRunset(" << runset << ") returned: " << resp << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void PrepareAndRunAssays::CreateRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	std::vector<std::wstring> methods;

	std::cout << "To create a runset type in the names of the methods that the runset will contain." << std::endl;

	bool close = false;
	while (!close)
	{
		std::cout << "a\t- Add a method" << std::endl;
		std::cout << "r\t- Remove last added method" << std::endl;
		std::cout << "l\t- List all methods" << std::endl;
		std::cout << "c\t- continue" << std::endl;

		char input;
		std::cout << "Choice: ";
		std::cin >> input;
		switch (input)
		{
		default:
			break;
		case 'c':
			close = true;
			break;
		case 'a':
			{
				std::string method;
				std::cout << "Type in the method name: ";
				std::getline(std::cin, method);
				std::getline(std::cin, method);
				methods.push_back(MainMenu::ToWstring(method));
			}
			break;
		case 'r':
			{
				if(methods.size()>0)
					methods.erase(methods.end()-1);
			}
			break;
		case 'l':
			std::cout << "Content: " << std::endl;
			for(int i = 0; i < (int)methods.size(); i++)
				std::cout << MainMenu::ToString(methods.at(i)) << std::endl;
			break;
		}
	}

	BOOL resp;
	WCHAR** names = new WCHAR * [methods.size()];
	for (int i = 0; i < (int)methods.size(); i++)
		names[i] = (WCHAR*)methods[i].c_str();
	
	if (NetTcpEndpoint_CreateRunset(proxy, (unsigned int)methods.size(), names, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nCreateRunset returned: " << resp << std::endl;
	else
		std::cout << "\nError" << std::endl;

	delete names;
	MainMenu::Return();
}

void PrepareAndRunAssays::SetSamplePlateId(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	int index;
	std::string plateID;

	std::cout << "\nMethod Index: ";
	std::cin >> index;
	std::cout << "\nPlate ID: ";
	std::getline(std::cin, plateID);
	std::getline(std::cin, plateID);

	std::wstring pName = MainMenu::ToWstring(plateID);
	BOOL resp;

	if (NetTcpEndpoint_SetSamplePlateId(proxy, index, (WCHAR*)pName.c_str(), &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nSetSamplePlateId returned: " << resp << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void PrepareAndRunAssays::GetSamplePlateId(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	int index;
	std::cout << "\nMethod Index: ";
	std::cin >> index;

	WCHAR* resp;

	if (NetTcpEndpoint_GetSamplePlateId(proxy, index, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nSetSamplePlateId returned: " << MainMenu::ToString(std::wstring(resp)) << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void PrepareAndRunAssays::GetCurrentSamplePlateId(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	WCHAR* resp;

	if (NetTcpEndpoint_GetCurrentSamplePlateId(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nGetCurrentSamplePlateId returned: " << MainMenu::ToString(std::wstring(resp)) << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void PrepareAndRunAssays::MoveSamplePlateTrayOut(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	BOOL resp;

	if (NetTcpEndpoint_MoveSamplePlateTrayOut(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nMoveSamplePlateTrayOut returned: " << resp << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void PrepareAndRunAssays::MoveSamplePlateTrayIn(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	BOOL resp;

	if (NetTcpEndpoint_MoveSamplePlateTrayIn(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nMoveSamplePlateTrayIn returned: " << resp << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void PrepareAndRunAssays::StartSelectedRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	BOOL resp;

	if (NetTcpEndpoint_StartSelectedRunset(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nStartSelectedRunset returned: " << resp << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();

}

void PrepareAndRunAssays::StartSelectedRunsetFrom(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	int index;
	std::cout << "\nMethod Index: ";
	std::cin >> index;

	BOOL resp;

	if (NetTcpEndpoint_StartSelectedRunsetFrom(proxy, index, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nStartSelectedRunsetFrom returned: " << resp << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void PrepareAndRunAssays::PauseRunsetAfter(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	int index = -1;
	while (index < 0)
	{
		std::cout << "\nSelect a pause mode:" << std::endl;
		std::cout << "\n1 - after current command" << std::endl;
		std::cout << "\n2 - after current cycle" << std::endl;
		std::cout << "\n3 - after current method" << std::endl;
		std::cin >> index;

		switch (index)
		{
		case 1:
		case 2:
		case 3:
			break;
		default:
			index = -1;
			break;
		}
	}

	BOOL resp;

	if (NetTcpEndpoint_PauseRunsetAfter(proxy, index, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nPauseRunsetAfter(" << index << ") returned: " << resp << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void PrepareAndRunAssays::ResumeRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	BOOL resp;

	if (NetTcpEndpoint_ResumeRunset(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nResumeRunset returned: " << resp << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void PrepareAndRunAssays::ResetRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	BOOL resp;

	if (NetTcpEndpoint_ResetRunset(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nResetRunset returned: " << resp << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void PrepareAndRunAssays::AbortScript(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	BOOL resp;

	if (NetTcpEndpoint_AbortScript(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nAbortScript returned: " << resp << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void PrepareAndRunAssays::LeaveStandby(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	BOOL resp;

	if (NetTcpEndpoint_LeaveStandby(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nLeaveStandby returned: " << resp << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void PrepareAndRunAssays::SetStandbyAfterFinish(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	std::cout << "\nGo into standby mode after the runset has finished?" << std::endl;
	std::cout << "\ny - yes";
	std::cout << "\nn - no";
	char input;
	std::cout << "Choice: ";
	std::cin >> input;

	BOOL goIntoStandby = false;
	if (input == 'y')
	{
		std::cout << "\nyes selected";
		goIntoStandby = true;
	}
	else
	{
		std::cout << "\nno selected";
	}

	BOOL resp;

	if (NetTcpEndpoint_SetStandbyAfterFinish(proxy, goIntoStandby, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nSetStandbyAfterFinish returned: " << resp << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}

void PrepareAndRunAssays::GetStandbyAfterFinish(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap)
{
	if (proxy == NULL)
		return;

	BOOL resp;

	if (NetTcpEndpoint_GetStandbyAfterFinish(proxy, &resp, heap, NULL, 0, NULL, error) == S_OK)
		std::cout << "\nGetStandbyAfterFinish returned: " << resp << std::endl;
	else
		std::cout << "\nError" << std::endl;

	MainMenu::Return();
}
