#pragma once

// gSoap specific includes
#include "soapGsoapEndpointProxy.h"
// std includes
#include <vector>
#include <iostream>
#include <string>
// extension includes
#include "MainMenu.h"

class PrepareAndRunAssays
{
	public:
		static void ShowMenu(GsoapEndpointProxy* proxy);
		static void SelectMethod(GsoapEndpointProxy* proxy);
		static void SelectRunset(GsoapEndpointProxy* proxy);
		static void CreateRunset(GsoapEndpointProxy* proxy);
		static void SetSamplePlateId(GsoapEndpointProxy* proxy);
		static void GetSamplePlateId(GsoapEndpointProxy* proxy);
		static void GetCurrentSamplePlateId(GsoapEndpointProxy* proxy);
		static void MoveSamplePlateTrayOut(GsoapEndpointProxy* proxy);
		static void MoveSamplePlateTrayIn(GsoapEndpointProxy* proxy);
		static void StartSelectedRunset(GsoapEndpointProxy* proxy);
		static void StartSelectedRunsetFrom(GsoapEndpointProxy* proxy);
		static void PauseRunsetAfter(GsoapEndpointProxy* proxy);
		static void ResumeRunset(GsoapEndpointProxy* proxy);
		static void ResetRunset(GsoapEndpointProxy* proxy);
		static void AbortScript(GsoapEndpointProxy* proxy);
		static void LeaveStandby(GsoapEndpointProxy* proxy);
		static void SetStandbyAfterFinish(GsoapEndpointProxy* proxy);
		static void GetStandbyAfterFinish(GsoapEndpointProxy* proxy);

};

