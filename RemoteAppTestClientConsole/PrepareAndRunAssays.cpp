#include "PrepareAndRunAssays.h"

void PrepareAndRunAssays::ShowMenu(GsoapEndpointProxy* proxy)
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
			PrepareAndRunAssays::SelectMethod(proxy);
			break;
		case '2':
			PrepareAndRunAssays::SelectRunset(proxy);
			break;
		case '3':
			PrepareAndRunAssays::CreateRunset(proxy);
			break;
		case '4':
			PrepareAndRunAssays::SetSamplePlateId(proxy);
			break;
		case '5':
			PrepareAndRunAssays::GetSamplePlateId(proxy);
			break;
		case '6':
			PrepareAndRunAssays::GetCurrentSamplePlateId(proxy);
			break;
		case '7':
			PrepareAndRunAssays::MoveSamplePlateTrayOut(proxy);
			break;
		case '8':
			PrepareAndRunAssays::MoveSamplePlateTrayIn(proxy);
			break;
		case '9':
			PrepareAndRunAssays::StartSelectedRunset(proxy);
			break;
		case 'a':
			PrepareAndRunAssays::StartSelectedRunsetFrom(proxy);
			break;
		case 'b':
			PrepareAndRunAssays::PauseRunsetAfter(proxy);
			break;
		case 'c':
			PrepareAndRunAssays::ResumeRunset(proxy);
			break;
		case 'd':
			PrepareAndRunAssays::ResetRunset(proxy);
			break;
		case 'e':
			PrepareAndRunAssays::AbortScript(proxy);
			break;
		case 'f':
			PrepareAndRunAssays::LeaveStandby(proxy);
			break;
		case 'g':
			PrepareAndRunAssays::SetStandbyAfterFinish(proxy);
			break;
		case 'h':
			PrepareAndRunAssays::GetStandbyAfterFinish(proxy);
			break;
		case 'q':
			close = true;
			break;
		default:
			break;
		}
	}
}

void PrepareAndRunAssays::SelectMethod(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__SelectMethod get;
	_tempuri__SelectMethodResponse resp;

	std::string methodName;
	std::cout << "Method Name: ";
	std::getline(std::cin, methodName);
	std::getline(std::cin, methodName);

	get.methodName = &methodName;

	if (proxy->SelectMethod(&get, resp) == SOAP_OK)
		std::cout << "\nSelectMethod(" << methodName << ") returned: " << *resp.SelectMethodResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return(true);
}

void PrepareAndRunAssays::SelectRunset(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__SelectRunset get;
	_tempuri__SelectRunsetResponse resp;

	std::string runset;
	std::cout << "Runset Name: ";
	std::getline(std::cin, runset);
	std::getline(std::cin, runset);

	get.runsetName = &runset;

	if (proxy->SelectRunset(&get, resp) == SOAP_OK)
		std::cout << "\nSelectRunset(" << runset << ") returned: " << *resp.SelectRunsetResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void PrepareAndRunAssays::CreateRunset(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	std::vector<std::string> methods;

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
				methods.push_back(method);
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
				std::cout << methods.at(i) << std::endl;
			break;
		}
	}

	_tempuri__CreateRunset get;
	_tempuri__CreateRunsetResponse resp;

	get.methodNames = new arr__ArrayOfstring();
	get.methodNames->string.swap(methods);

	if (proxy->CreateRunset(&get, resp) == SOAP_OK)
		std::cout << "\nCreateRunset returned: " << *resp.CreateRunsetResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void PrepareAndRunAssays::SetSamplePlateId(GsoapEndpointProxy* proxy)
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

	_tempuri__SetSamplePlateId get;
	_tempuri__SetSamplePlateIdResponse resp;

	get.methodIndex = &index;
	get.plateId = &plateID;

	if (proxy->SetSamplePlateId(&get, resp) == SOAP_OK)
		std::cout << "\nSetSamplePlateId returned: " << *resp.SetSamplePlateIdResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void PrepareAndRunAssays::GetSamplePlateId(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	int index;
	std::cout << "\nMethod Index: ";
	std::cin >> index;

	_tempuri__GetSamplePlateId get;
	_tempuri__GetSamplePlateIdResponse resp;

	get.methodIndex = &index;

	if (proxy->GetSamplePlateId(&get, resp) == SOAP_OK)
		std::cout << "\nGetSamplePlateId returned: " << *resp.GetSamplePlateIdResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void PrepareAndRunAssays::GetCurrentSamplePlateId(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__GetCurrentSamplePlateId get;
	_tempuri__GetCurrentSamplePlateIdResponse resp;

	if (proxy->GetCurrentSamplePlateId(&get, resp) == SOAP_OK)
		std::cout << "\nGetCurrentSamplePlateId returned: " << *resp.GetCurrentSamplePlateIdResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void PrepareAndRunAssays::MoveSamplePlateTrayOut(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__MoveSamplePlateTrayOut get;
	_tempuri__MoveSamplePlateTrayOutResponse resp;

	if (proxy->MoveSamplePlateTrayOut(&get, resp) == SOAP_OK)
		std::cout << "\nMoveSamplePlateTrayOut returned: " << *resp.MoveSamplePlateTrayOutResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void PrepareAndRunAssays::MoveSamplePlateTrayIn(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__MoveSamplePlateTrayIn get;
	_tempuri__MoveSamplePlateTrayInResponse resp;

	if (proxy->MoveSamplePlateTrayIn(&get, resp) == SOAP_OK)
		std::cout << "\nMoveSamplePlateTrayIn returned: " << *resp.MoveSamplePlateTrayInResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void PrepareAndRunAssays::StartSelectedRunset(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__StartSelectedRunset get;
	_tempuri__StartSelectedRunsetResponse resp;

	if (proxy->StartSelectedRunset(&get, resp) == SOAP_OK)
		std::cout << "\nStartSelectedRunset returned: " << *resp.StartSelectedRunsetResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();

}

void PrepareAndRunAssays::StartSelectedRunsetFrom(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	int index;
	std::cout << "\nMethod Index: ";
	std::cin >> index;

	_tempuri__StartSelectedRunsetFrom get;
	_tempuri__StartSelectedRunsetFromResponse resp;

	get.methodIndex = &index;

	if (proxy->StartSelectedRunsetFrom(&get, resp) == SOAP_OK)
		std::cout << "\nStartSelectedRunsetFrom returned: " << *resp.StartSelectedRunsetFromResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void PrepareAndRunAssays::PauseRunsetAfter(GsoapEndpointProxy* proxy)
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

	_tempuri__PauseRunsetAfter get;
	_tempuri__PauseRunsetAfterResponse resp;

	get.pauseMode = &index;

	if (proxy->PauseRunsetAfter(&get, resp) == SOAP_OK)
		std::cout << "\nPauseRunsetAfter(" << index << ") returned: " << *resp.PauseRunsetAfterResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void PrepareAndRunAssays::ResumeRunset(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__ResumeRunset get;
	_tempuri__ResumeRunsetResponse resp;

	if (proxy->ResumeRunset(&get, resp) == SOAP_OK)
		std::cout << "\nResumeRunset returned: " << *resp.ResumeRunsetResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void PrepareAndRunAssays::ResetRunset(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__ResetRunset get;
	_tempuri__ResetRunsetResponse resp;

	if (proxy->ResetRunset(&get, resp) == SOAP_OK)
		std::cout << "\nResetRunset returned: " << *resp.ResetRunsetResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void PrepareAndRunAssays::AbortScript(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__AbortScript get;
	_tempuri__AbortScriptResponse resp;

	if (proxy->AbortScript(&get, resp) == SOAP_OK)
		std::cout << "\nAbortScript returned: " << *resp.AbortScriptResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void PrepareAndRunAssays::LeaveStandby(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__LeaveStandby get;
	_tempuri__LeaveStandbyResponse resp;

	if (proxy->LeaveStandby(&get, resp) == SOAP_OK)
		std::cout << "\nLeaveStandby returned: " << *resp.LeaveStandbyResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void PrepareAndRunAssays::SetStandbyAfterFinish(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	std::cout << "\nGo into standby mode after the runset has finished?" << std::endl;
	std::cout << "\ny - yes";
	std::cout << "\nn - no";
	char input;
	std::cout << "Choice: ";
	std::cin >> input;

	_tempuri__SetStandbyAfterFinish get;
	_tempuri__SetStandbyAfterFinishResponse resp;

	bool goIntoStandby = false;
	if (input == 'y')
	{
		std::cout << "\nyes selected";
		goIntoStandby = true;
	}
	else
	{
		std::cout << "\nno selected";
	}

	get.goToStandby = &goIntoStandby;

	if (proxy->SetStandbyAfterFinish(&get, resp) == SOAP_OK)
		std::cout << "\nSetStandbyAfterFinish returned: " << *resp.SetStandbyAfterFinishResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}

void PrepareAndRunAssays::GetStandbyAfterFinish(GsoapEndpointProxy* proxy)
{
	if (proxy == NULL)
		return;

	_tempuri__GetStandbyAfterFinish get;
	_tempuri__GetStandbyAfterFinishResponse resp;

	if (proxy->GetStandbyAfterFinish(&get, resp) == SOAP_OK)
		std::cout << "\nGetStandbyAfterFinish returned: " << *resp.GetStandbyAfterFinishResult << std::endl;
	else
		proxy->soap_stream_fault(std::cerr);

	MainMenu::Return();
}
