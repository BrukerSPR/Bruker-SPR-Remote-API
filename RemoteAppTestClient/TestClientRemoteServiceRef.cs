using System.ServiceModel;
using System.ServiceModel.Web;

[System.CodeDom.Compiler.GeneratedCode("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContract(ConfigurationName = "IBrukerSprRemoteService")]
public interface IBrukerSprRemoteService
{
    #region Get Information About Methods

    /// <summary>
    /// Returns an array of all method names that are loaded in the current runset library.
    /// </summary>
    /// <returns>names of all methods</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string[] GetNamesOfMethods();

    /// <summary>
    /// Returns an array containing the names of all methods of the provided assay type that are loaded in the current runset library.
    /// </summary>
    /// <param name="assayType">the list of assay types can be obtained using the <see cref="GetAssayTypesOfAllMethods"/></param>
    /// <returns>names of all methods of the provided assay type</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string[] GetNamesOfMethodsOfAssayType(string assayType);

    /// <summary>
    /// Returns an array containing the assay types of all methods that are loaded in the current runset library.
    /// </summary>
    /// <returns>distinct array of assay types</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string[] GetAssayTypesOfAllMethods();

    /// <summary>
    /// Returns the name of the method that is currently running or selected for running. Returns an empty string if there is no method selected.
    /// </summary>
    /// <returns>method name</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string GetNameOfCurrentMethod();

    /// <summary>
    /// Returns the assay type of the method that is currently running or selected for running.Returns an empty string if there is no method selected.
    /// </summary>
    /// <returns>assay type</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string GetAssayTypeOfCurrentMethod();

    /// <summary>
    /// Returns the assay type of the method with the given name. Returns an empty string if there is no method with the given name in the library.
    /// </summary>
    /// <param name="methodName">name of a method</param>
    /// <returns>assay type of the method</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string GetAssayTypeOfMethod(string methodName);

    #endregion


    #region Get Information About Runsets

    /// <summary>
    /// Returns an array of all runset names that are loaded in the current runset library.
    /// </summary>
    /// <returns>names of all runsets</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string[] GetNamesOfRunsets();

    /// <summary>
    /// Returns an array containing the names of all methods of the provided assay type that are loaded in the current runset library.
    /// </summary>
    /// <param name="assayType">the list of assay types can be obtained using the <see cref="GetAssayTypesOfAllRunsets"/></param>
    /// <returns>names of all runsets of the provided assay type</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string[] GetNamesOfRunsetsOfAssayType(string assayType);

    /// <summary>
    /// Returns an array containing the assay types of all runsets that are loaded in the current runset library.
    /// </summary>
    /// <returns>distinct array of assay types</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string[] GetAssayTypesOfAllRunsets();

    /// <summary>
    /// Returns the name of the runset that is currently running or selected for running. Returns an empty string if there is no runset selected.
    /// </summary>
    /// <returns>runset name</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string GetNameOfCurrentRunset();

    /// <summary>
    /// Returns the assay type of the runset that is currently running or selected for running. Returns an empty string if there is no runset selected.
    /// </summary>
    /// <returns>assay type</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string GetAssayTypeOfCurrentRunset();

    /// <summary>
    /// Returns the assay type of the runset with the given name. Returns an empty string if there is no runset with the given name in the library.
    /// </summary>
    /// <param name="runsetName">name of a runset</param>
    /// <returns>assay type of the runset</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string GetAssayTypeOfRunset(string runsetName);

    /// <summary>
    /// Returns the names of the methods in the runset with the given name.
    /// </summary>
    /// <param name="runsetName">name of a runset</param>
    /// <returns>ordered array of names in the runset</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string[] GetMethodNamesOfRunset(string runsetName);

    #endregion


    #region Prepare And Run Assays

    /// <summary>
    /// Selects the specified method for running. Returns 'false' if method could not be selected. Check for error messages in that case. 
    /// Possible reasons are that the operation mode doesn't allow to select a method or that the method with the provided name doesn't exist in the library.
    /// </summary>
    /// <param name="methodName">name of a method</param>
    /// <returns>success status</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    bool SelectMethod(string methodName);

    /// <summary>
    /// Selects the specified runset for running. Returns 'false' if runset could not be selected. Check for error messages in that case. 
    /// Possible reasons are that the operation mode doesn't allow to select a runset or that the runset with the provided name doesn't exist in the library.
    /// </summary>
    /// <param name="runsetName">name of a runset</param>
    /// <returns>success status</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    bool SelectRunset(string runsetName);

    /// <summary>
    /// Creates a runset that contains the methods specified by the passed array of method names and selects the runset for running. 
    /// Returns 'false' if runset could not be created. Check for error messages in that case. 
    /// Possible reasons are that the operation mode doesn't allow to select a runset or that one or more methods with the provided names do not exist in the library.
    /// </summary>
    /// <param name="methodNames">ordered array of method names</param>
    /// <returns>success status</returns>
    [System.ServiceModel.OperationContract()]
    [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
    bool CreateRunset(string[] methodNames);

    /// <summary>
    /// Assigns an id to the exchangeable plate of the method with the specified index in the currently selected runset. 
    /// Returns 'false' if method index is out of range or the method with the given index doesn't use a sample plate. Check for errors in this case.
    /// This is only implemented for MASS-1 platform and will throw a <see cref="System.NotSupportedException"/> on SPR #64.
    /// </summary>
    /// <param name="methodIndex">index of the method in the runset for which the plate ID is assigned</param>
    /// <param name="plateId">ID of the plate</param>
    /// <returns>success status</returns>
    /// <exception cref="System.NotSupportedException">Thrown if not MASS-1 platform.</exception>
    [OperationContract]
    [WebInvoke(Method = "GET", UriTemplate = "SetSamplePlateId?methodIndex={methodIndex}&plateId={plateId}", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
    bool SetSamplePlateId(int methodIndex, string plateId);

    /// <summary>
    /// Gets the ID of the sample plate of the method with the given index in the current runset. 
    /// Returns an empty string if the index is out of range, if there is no runset, no plate, or the plate doesn't have an ID assigned.
    /// This is only implemented for MASS-1 platform and will throw a <see cref="System.NotSupportedException"/> on SPR #64.
    /// </summary>
    /// <param name="methodIndex">index of the method in the current runset</param>
    /// <returns>ID of the sample plate</returns>
    /// <exception cref="System.NotSupportedException">Thrown if not MASS-1 platform.</exception>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    string GetSamplePlateId(int methodIndex);

    /// <summary>
    /// Gets the ID of the sample plate that is currently inside the machine. Gets the ID of the current sample plate. Returns an empty string if there is no plate or ID is unknown.
    /// This is only implemented for MASS-1 platform and will throw a <see cref="System.NotSupportedException"/> on SPR #64.
    /// </summary>
    /// <returns>ID of the sample plate inside the machine</returns>
    /// <exception cref="System.NotSupportedException">Thrown if not MASS-1 platform.</exception>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    string GetCurrentSamplePlateId();

    /// <summary>
    /// Assigns an id to the plate at the provided deck location of the method with the specified index in the currently selected runset. 
    /// Returns 'false' if method index is out of range or the method with the given index doesn't use a sample plate. Check for errors in this case.
    /// </summary>
    /// <param name="methodIndex">index of the method in the runset for which the plate ID is assigned</param>
    /// <param name="deckLocation">name of the deck location where the plate is located</param>
    /// <param name="plateId">ID of the plate</param>
    /// <returns>success status</returns>
    /// <exception cref="System.ArgumentException">Thrown if location does not exist on deck.</exception>
    [OperationContract]
    [WebInvoke(Method = "GET", UriTemplate = "SetPlateId?methodIndex={methodIndex}&plateId={plateId}&deckLocation={deckLocation}", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
    bool SetPlateId(int methodIndex, string plateId, string deckLocation);

    /// <summary>
    /// Gets the ID of the plate at the provided deck location of the method with the given index in the current runset. 
    /// Returns an empty string if the index is out of range, if there is no runset, no plate, or the plate doesn't have an ID assigned.
    /// </summary>
    /// <param name="methodIndex">index of the method in the current runset</param>
    /// <param name="deckLocation">name of the deck location where the plate is located</param>
    /// <returns>ID of the plate</returns>
    /// <exception cref="System.ArgumentException">Thrown if location does not exist on deck.</exception>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    string GetPlateId(int methodIndex, string deckLocation);

    /// <summary>
    /// Gets the ID of the plate that is currently inside the machine at the provided deck location. Returns an empty string if there is no plate or ID is unknown.
    /// This is only implemented for MASS-1 platform and will throw a <see cref="System.NotSupportedException"/> on SPR #64.
    /// </summary>
    /// <param name="deckLocation">name of the deck location where the plate is located</param>
    /// <returns>ID of the plate</returns>
    /// <exception cref="System.ArgumentException">Thrown if location does not exist on deck.</exception>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    string GetCurrentPlateId(string deckLocation);

    /// <summary>
    /// Returns an array of all deck location names.
    /// </summary>
    /// <returns>names of all deck locations.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    string[] GetDeckLocations();

    /// <summary>
    /// Moves the sample plate tray out of the device so the plate mover can get or put a plate from it. Returns 'false' if this is not possible. 
    /// This might be the case if there is a runset or maintenance procedure currently running or if a reset is required. Check for errors in this case.
    /// This is only implemented for MASS-1 platform and will throw a <see cref="System.NotSupportedException"/> on SPR #64.
    /// </summary>
    /// <returns>success status</returns>
    /// <exception cref="System.NotSupportedException">Thrown if not MASS-1 platform.</exception>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    bool MoveSamplePlateTrayOut();

    /// <summary>
    /// Moves the sample plate tray into the housing. Returns 'false' if this is not possible. 
    /// This might be the case if there is a runset or maintenance procedure is currently running or if a reset is required. Check for errors in this case.
    /// This is only implemented for MASS-1 platform and will throw a <see cref="System.NotSupportedException"/> on SPR #64.
    /// </summary>
    /// <returns>success status</returns>
    /// <exception cref="System.NotSupportedException">Thrown if not MASS-1 platform.</exception>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    bool MoveSamplePlateTrayIn();

    /// <summary>
    /// Opens the side door so that a plate mover can exchange a plate on the plate deck.
    /// This is only implemented for SPR #64 and will throw a <see cref="System.NotSupportedException"/> on MASS-1 platform.
    /// </summary>
    /// <returns>success status</returns>
    /// <exception cref="System.NotSupportedException">Thrown if not SPR #64.</exception>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    bool OpenSideDoor();

    /// <summary>
    /// Closes the side door after a plate mover has exchanged a plate on the plate deck.
    /// This is only implemented for SPR #64 and will throw a <see cref="System.NotSupportedException"/> on MASS-1 platform.
    /// </summary>
    /// <returns>success status</returns>
    /// <exception cref="System.NotSupportedException">Thrown if not SPR #64.</exception>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    bool CloseSideDoor();

    /// <summary>
    /// Starts the currently selected runset. Returns 'false' if this is not possible. Check for errors in this case. 
    /// Possible reasons might be that the system is not ready, is on error, or there is no selected runset.
    /// </summary>
    /// <returns>success status</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    bool StartSelectedRunset();

    /// <summary>
    /// Starts the runset starting from the method with the given index. Returns 'false' it this is not possible. Check for errors in this case. 
    /// Possible reasons might be that the system is not ready, is on error, the index of the method is out of range, or there is no selected runset.
    /// </summary>
    /// <param name="methodIndex">index of the method in the runset from which the runset is started</param>
    /// <returns>success status</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    bool StartSelectedRunsetFrom(int methodIndex);

    /// <summary>
    /// Pauses the runset that is currently running. The mode argument sets when the runset is paused. Returns 'false' if this is not possible because there is currently no runset running.
    /// </summary>
    /// <param name="pauseMode">1 = pause after current command, 2 = pause after current cycle, 3 = pause after current method</param>
    /// <returns>success status</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    bool PauseRunsetAfter(int pauseMode);

    /// <summary>
    /// Resumes the paused runset where it was paused. Returns false if this is not possible. Check for errors in this case. 
    /// Reasons might be that the operation mode is different from 'paused' or device is not ready or on error.
    /// </summary>
    /// <returns>success status</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    bool ResumeRunset();

    /// <summary>
    /// Resets the status of the runset after it was interrupted. Returns 'false' if this is not possible because the runset is currently running or if there is no runset to reset. 
    /// Running the reset method multiple times on the same runset is possible, however. 
    /// </summary>
    /// <returns>success status</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    bool ResetRunset();

    /// <summary>
    /// Aborts the currently running script immediately. That can either be a runset or a maintenance command. Returns 'false' if this is not possible because there is nothing running 
    /// or running script is already a cancel script (since the device runs a script to clean up the flow cell when interrupted in the middle of an injection).
    /// </summary>
    /// <returns>success status</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    bool AbortScript();

    #endregion


    #region Maintenance

    /// <summary>
    /// Returns an array containing the names of the all maintenance commands that are available and can be run without user interaction.
    /// </summary>
    /// <returns>names of available maintenance procedures</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string[] GetNamesOfMaintenanceProcedures();

    /// <summary>
    /// Starts the maintenance procedure with the given name. Returns 'false' if this is not possible. Check for errors in this case. 
    /// Reasons might be that the device is not ready or there is already something running. Check for errors in this case.
    /// </summary>
    /// <param name="procedureName">name of the maintenance procedure to be run</param>
    /// <returns>success status</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    bool RunMaintenanceProcedure(string procedureName);

    #endregion


    #region Status

    /// <summary>
    /// Gets the current operation mode of the device. The meaning of the return value can be found in a separate table.
    /// </summary>
    /// <returns>number that gives information about the operation mode:
    ///  0	Idle
    ///  1	Paused
    ///  2	Completed
    /// 10	StandBy
    /// 11	ResetRequired
    /// 12	MaintenanceRequired
    /// 13	DoorOpen
    /// 20	Running
    /// </returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    int GetOperationMode();

    /// <summary>
    /// Checks whether a chip is docked.
    /// </summary>
    /// <returns>1 = docked, 0 = undocked, -1 = unknown </returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    int IsChipDocked();

    /// <summary>
    /// Checks whether the sample plate tray is driven into the device.
    /// This is only implemented for MASS-1 platform and will throw a
    /// <see cref="System.NotSupportedException"/> on SPR #64.
    /// </summary>
    /// <returns>1 = tray in, 0 = tray out, -1 = unknown</returns>
    /// <exception cref="System.NotSupportedException">Thrown if not MASS-1 platform.</exception>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    int IsSamplePlateTrayIn();

    /// <summary>
    /// Checks whether the side door is open.
    /// This is only implemented for SPR #64 and will throw a
    /// <see cref="System.NotSupportedException"/> on MASS-1 platform.
    /// </summary>
    /// <returns>1 = open, 0 = closed, -1 = unknown</returns>
    /// <exception cref="System.NotSupportedException">Thrown if not SPR #64.</exception>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json)]
    int IsSideDoorOpen();

    /// <summary>
    /// Checks whether the server's message loop contains at least one new message.
    /// </summary>
    /// <returns>true if there is at least one message in the queue</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    bool HasMessage();

    /// <summary>
    /// Gets the first message from the server's message queue. A message consists of three strings in an array where the first string is a time stamp formatted like yyyyMMdd-hhmmss.fff, 
    /// the second gives information about the type of message (Error, Warning, DataSaved, OperationModeChanged, ... ), and the third string is the message itself. 
    /// Call this method on a regular base using polling and check the result for null or use the HasNewMessage method first. A complete list of error types can be found in a separate table. 
    /// </summary>
    /// <returns>array of three string</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string[] GetMessage();


    System.Threading.Tasks.Task<string> GetMessageAsync(string name);

    /// <summary>
    /// Checks whether there are any errors that are not covered by the operation mode.
    /// </summary>
    /// <returns>true if something wrong with the device's status</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    bool HasErrors();

    /// <summary>
    /// Gets an array of strings with information about errors like 'no data', 'temperature unstable', 'runset error', ...
    /// </summary>
    /// <returns>array of current errors</returns>
    [System.ServiceModel.OperationContract()]
    [WebGet]
    string[] GetErrors();

    #endregion
}


[System.CodeDom.Compiler.GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface IRemoteServiceChannel : IBrukerSprRemoteService, System.ServiceModel.IClientChannel
{
}

public partial class MASS1RemoteServiceClient : System.ServiceModel.ClientBase<IRemoteServiceChannel> , IBrukerSprRemoteService
{

    public MASS1RemoteServiceClient()
    {

    }

    #region Get Information About Methods

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

    #endregion


    #region Get Information About Runsets

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

    #endregion


    #region Prepare And Run Assays

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
        return (Channel as IBrukerSprRemoteService).AbortScript();
    }

    #endregion


    #region Maintenance

    public string[] GetNamesOfMaintenanceProcedures()
    {
        return Channel.GetNamesOfMaintenanceProcedures();
    }

    public bool RunMaintenanceProcedure(string procedureName)
    {
        return Channel.RunMaintenanceProcedure(procedureName);
    }

    #endregion


    #region Status

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

    public System.Threading.Tasks.Task<string> GetMessageAsync(string name)
    {
        return Channel.GetMessageAsync(name);
    }

    public bool HasErrors()
    {
        return Channel.HasErrors();
    }

    public string[] GetErrors()
    {
        return Channel.GetErrors();
    }

    #endregion
}

