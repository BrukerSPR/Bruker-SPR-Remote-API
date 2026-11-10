namespace BrukerSprRemoteAPI.Client
{
#if NET48
    public partial class BrukerSprRemoteAPIclient : System.ServiceModel.ClientBase<IRemoteServiceChannel>, IBrukerSprRemoteService
    {
#elif NET8_0_OR_GREATER
    public class BrukerSprRemoteAPIclient(string address = BrukerSprRemoteAPIclient_net8.DefaultAddress) : IBrukerSprRemoteService
    {
        private readonly BrukerSprRemoteAPIclient_net8 client = new(address);

        public IBrukerSprRemoteService Channel => this.client.Channel;

        public RestChannelFactory ChannelFactory => this.client.ChannelFactory;

        public void Close() => this.client.Close();
#endif

        public string[] GetNamesOfMethods()
        {
            return Channel.GetNamesOfMethods();
        }

        public string[] GetNamesOfMethodsOfAssayType(string assayType)
        {
            return Channel.GetNamesOfMethodsOfAssayType(assayType);
        }

        public string[] GetAssayTypesOfAllMethods()
        {
            return Channel.GetAssayTypesOfAllMethods();
        }

        public string GetNameOfCurrentMethod()
        {
            return Channel.GetNameOfCurrentMethod();
        }

        public string GetAssayTypeOfCurrentMethod()
        {
            return Channel.GetAssayTypeOfCurrentMethod();
        }

        public string GetAssayTypeOfMethod(string methodName)
        {
            return Channel.GetAssayTypeOfMethod(methodName);
        }

        public string[] GetNamesOfRunsets()
        {
            return Channel.GetNamesOfRunsets();
        }

        public string[] GetNamesOfRunsetsOfAssayType(string assayType)
        {
            return Channel.GetNamesOfRunsetsOfAssayType(assayType);
        }

        public string[] GetAssayTypesOfAllRunsets()
        {
            return Channel.GetAssayTypesOfAllRunsets();
        }

        public string GetNameOfCurrentRunset()
        {
            return Channel.GetNameOfCurrentRunset();
        }

        public string GetAssayTypeOfCurrentRunset()
        {
            return Channel.GetAssayTypeOfCurrentRunset();
        }

        public string GetAssayTypeOfRunset(string runsetName)
        {
            return Channel.GetAssayTypeOfRunset(runsetName);
        }

        public string[] GetMethodNamesOfRunset(string runsetName)
        {
            return Channel.GetMethodNamesOfRunset(runsetName);
        }

        public bool SelectMethod(string methodName)
        {
            return Channel.SelectMethod(methodName);
        }

        public bool SelectRunset(string runsetName)
        {
            return Channel.SelectRunset(runsetName);
        }

        public bool CreateRunset(string[] methodNames)
        {
            return Channel.CreateRunset(methodNames);
        }

        public bool SetSamplePlateId(int methodIndex, string plateId)
        {
            return Channel.SetSamplePlateId(methodIndex, plateId);
        }

        public string GetSamplePlateId(int methodIndex)
        {
            return Channel.GetSamplePlateId(methodIndex);
        }

        public string GetCurrentSamplePlateId()
        {
            return Channel.GetCurrentSamplePlateId();
        }

        public bool SetPlateId(int methodIndex, string plateId, string deckLocation)
        {
            return Channel.SetPlateId(methodIndex, plateId, deckLocation);
        }

        public string GetPlateId(int methodIndex, string deckLocation)
        {
            return Channel.GetPlateId(methodIndex, deckLocation);
        }

        public string GetCurrentPlateId(string deckLocation)
        {
            return Channel.GetCurrentPlateId(deckLocation);
        }

        public string[] GetDeckLocations()
        {
            return Channel.GetDeckLocations();
        }

        public bool MoveSamplePlateTrayOut()
        {
            return Channel.MoveSamplePlateTrayOut();
        }

        public bool MoveSamplePlateTrayIn()
        {
            return Channel.MoveSamplePlateTrayIn();
        }

        public bool MoveReagentPlateTrayOut()
        {
            return Channel.MoveReagentPlateTrayOut();
        }

        public bool MoveReagentPlateTrayIn()
        {
            return Channel.MoveReagentPlateTrayIn();
        }

        public bool OpenSideDoor()
        {
            return Channel.OpenSideDoor();
        }

        public bool CloseSideDoor()
        {
            return Channel.CloseSideDoor();
        }

        public bool StartSelectedRunset()
        {
            return Channel.StartSelectedRunset();
        }

        public bool StartSelectedRunsetFrom(int methodIndex)
        {
            return Channel.StartSelectedRunsetFrom(methodIndex);
        }

        public bool PauseRunsetAfter(int pauseMode)
        {
            return Channel.PauseRunsetAfter(pauseMode);
        }

        public bool ResumeRunset()
        {
            return Channel.ResumeRunset();
        }

        public bool ResetRunset()
        {
            return Channel.ResetRunset();
        }

        public bool AbortScript()
        {
            return Channel.AbortScript();
        }

        public bool LeaveStandby()
        {
            return Channel.LeaveStandby();
        }

        public bool SetStandbyAfterFinish(bool goToStandby)
        {
            return Channel.SetStandbyAfterFinish(goToStandby);
        }

        public bool GetStandbyAfterFinish()
        {
            return Channel.GetStandbyAfterFinish();
        }

        public string[] GetNamesOfMaintenanceProcedures()
        {
            return Channel.GetNamesOfMaintenanceProcedures();
        }

        public bool RunMaintenanceProcedure(string procedureName)
        {
            return Channel.RunMaintenanceProcedure(procedureName);
        }

        public int GetOperationMode()
        {
            return Channel.GetOperationMode();
        }

        public int IsChipDocked()
        {
            return Channel.IsChipDocked();
        }

        public int IsSamplePlateTrayIn()
        {
            return Channel.IsSamplePlateTrayIn();
        }

        public int IsReagentPlateTrayIn()
        {
            return Channel.IsReagentPlateTrayIn();
        }

        public int IsSideDoorOpen()
        {
            return Channel.IsSideDoorOpen();
        }

        public bool HasMessage()
        {
            return Channel.HasMessage();
        }

        public string[] GetMessage()
        {
            return Channel.GetMessage();
        }

        public bool HasErrors()
        {
            return Channel.HasErrors();
        }

        public string[] GetErrors()
        {
            return Channel.GetErrors();
        }

        public bool HasWarnings()
        {
            return Channel.HasWarnings();
        }

        public string[] GetWarnings()
        {
            return Channel.GetWarnings();
        }
    }

}
