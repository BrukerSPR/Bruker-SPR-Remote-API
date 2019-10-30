#pragma once

// include microsoft web service
#include <WebServices.h>
// wsUtil includes
#include "schemas.microsoft.com.2003.10.Serialization.xsd.h"
#include "schemas.microsoft.com.2003.10.Serialization.xsd.h"
#include "tempuri.org.xsd.h"
#include "tempuri.org.wsdl.h"
// extension includes
#include "MainMenu.h"
// std includes
#include <vector>
#include <iostream>
#include <string>

class PrepareAndRunAssays
{
	public:
		static void ShowMenu(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void SelectMethod(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void SelectRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void CreateRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void SetSamplePlateId(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetSamplePlateId(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetCurrentSamplePlateId(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void MoveSamplePlateTrayOut(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void MoveSamplePlateTrayIn(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void StartSelectedRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void StartSelectedRunsetFrom(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void PauseRunsetAfter(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void ResumeRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void ResetRunset(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void AbortScript(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void LeaveStandby(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void SetStandbyAfterFinish(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);
		static void GetStandbyAfterFinish(WS_SERVICE_PROXY* proxy, WS_ERROR* error, WS_HEAP* heap);

};

