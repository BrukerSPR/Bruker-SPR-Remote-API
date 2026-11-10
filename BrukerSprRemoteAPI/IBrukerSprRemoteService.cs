namespace BrukerSprRemoteAPI
{
#if NET48
    using System.ServiceModel;
    using System.ServiceModel.Web;
#elif NET8_0
    using Microsoft.AspNetCore.Mvc;
#endif

#if NET48
    [System.CodeDom.Compiler.GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContract(ConfigurationName = "IBrukerSprRemoteService")]
#endif
    public interface IBrukerSprRemoteService
    {
        /// <summary>
        /// Returns an array of all method names that are loaded in the current runset library.
        /// </summary>
        /// <returns>names of all methods</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetNamesOfMethods")]
        [Produces("application/json")]
#endif
        string[] GetNamesOfMethods();

        /// <summary>
        /// Returns an array containing the names of all methods of the provided assay type that are loaded in the current runset library.
        /// </summary>
        /// <param name="assayType">the list of assay types can be obtained using the <see cref="GetAssayTypesOfAllMethods"/></param>
        /// <returns>names of all methods of the provided assay type</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetNamesOfMethodsOfAssayType")]
        [Produces("application/json")]
#endif
        string[] GetNamesOfMethodsOfAssayType(string assayType);

        /// <summary>
        /// Returns an array containing the assay types of all methods that are loaded in the current runset library.
        /// </summary>
        /// <returns>distinct array of assay types</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetAssayTypesOfAllMethods")]
        [Produces("application/json")]
#endif
        string[] GetAssayTypesOfAllMethods();

        /// <summary>
        /// Returns the name of the method that is currently running or selected for running. Returns an empty string if there is no method selected.
        /// </summary>
        /// <returns>method name</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetNameOfCurrentMethod")]
        [Produces("application/json")]
#endif
        string GetNameOfCurrentMethod();

        /// <summary>
        /// Returns the assay type of the method that is currently running or selected for running.Returns an empty string if there is no method selected.
        /// </summary>
        /// <returns>assay type</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetAssayTypeOfCurrentMethod")]
        [Produces("application/json")]
#endif
        string GetAssayTypeOfCurrentMethod();

        /// <summary>
        /// Returns the assay type of the method with the given name. Returns an empty string if there is no method with the given name in the library.
        /// </summary>
        /// <param name="methodName">name of a method</param>
        /// <returns>assay type of the method</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetAssayTypeOfMethod")]
        [Produces("application/json")]
#endif
        string GetAssayTypeOfMethod(string methodName);

        /// <summary>
        /// Returns an array of all runset names that are loaded in the current runset library.
        /// </summary>
        /// <returns>names of all runsets</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetNamesOfRunsets")]
        [Produces("application/json")]
#endif
        string[] GetNamesOfRunsets();

        /// <summary>
        /// Returns an array containing the names of all methods of the provided assay type that are loaded in the current runset library.
        /// </summary>
        /// <param name="assayType">the list of assay types can be obtained using the <see cref="GetAssayTypesOfAllRunsets"/></param>
        /// <returns>names of all runsets of the provided assay type</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetNamesOfRunsetsOfAssayType")]
        [Produces("application/json")]
#endif
        string[] GetNamesOfRunsetsOfAssayType(string assayType);

        /// <summary>
        /// Returns an array containing the assay types of all runsets that are loaded in the current runset library.
        /// </summary>
        /// <returns>distinct array of assay types</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetAssayTypesOfAllRunsets")]
        [Produces("application/json")]
#endif
        string[] GetAssayTypesOfAllRunsets();

        /// <summary>
        /// Returns the name of the runset that is currently running or selected for running. Returns an empty string if there is no runset selected.
        /// </summary>
        /// <returns>runset name</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetNameOfCurrentRunset")]
        [Produces("application/json")]
#endif
        string GetNameOfCurrentRunset();

        /// <summary>
        /// Returns the assay type of the runset that is currently running or selected for running. Returns an empty string if there is no runset selected.
        /// </summary>
        /// <returns>assay type</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetAssayTypeOfCurrentRunset")]
        [Produces("application/json")]
#endif
        string GetAssayTypeOfCurrentRunset();

        /// <summary>
        /// Returns the assay type of the runset with the given name. Returns an empty string if there is no runset with the given name in the library.
        /// </summary>
        /// <param name="runsetName">name of a runset</param>
        /// <returns>assay type of the runset</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetAssayTypeOfRunset")]
        [Produces("application/json")]
#endif
        string GetAssayTypeOfRunset(string runsetName);

        /// <summary>
        /// Returns the names of the methods in the runset with the given name.
        /// </summary>
        /// <param name="runsetName">name of a runset</param>
        /// <returns>ordered array of names in the runset</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetMethodNamesOfRunset")]
        [Produces("application/json")]
#endif
        string[] GetMethodNamesOfRunset(string runsetName);

        /// <summary>
        /// Selects the specified method for running. Returns 'false' if method could not be selected. Check for error messages in that case.
        /// Possible reasons are that the operation mode doesn't allow to select a method or that the method with the provided name doesn't exist in the library.
        /// </summary>
        /// <param name="methodName">name of a method</param>
        /// <returns>success status</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "SelectMethod")]
        [Produces("application/json")]
#endif
        bool SelectMethod(string methodName);

        /// <summary>
        /// Selects the specified runset for running. Returns 'false' if runset could not be selected. Check for error messages in that case.
        /// Possible reasons are that the operation mode doesn't allow to select a runset or that the runset with the provided name doesn't exist in the library.
        /// </summary>
        /// <param name="runsetName">name of a runset</param>
        /// <returns>success status</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "SelectRunset")]
        [Produces("application/json")]
#endif
        bool SelectRunset(string runsetName);

        /// <summary>
        /// Creates a runset that contains the methods specified by the passed array of method names and selects the runset for running.
        /// Returns 'false' if runset could not be created. Check for error messages in that case.
        /// Possible reasons are that the operation mode doesn't allow to select a runset or that one or more methods with the provided names do not exist in the library.
        /// </summary>
        /// <param name="methodNames">ordered array of method names</param>
        /// <returns>success status</returns>
#if NET48
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
#elif NET8_0
        [HttpGet("methodNames={methodNames}", Name = "CreateRunset")]
        [Produces("application/json")]
#endif
        bool CreateRunset(string[] methodNames);

        /// <summary>
        /// Assigns an id to the exchangeable plate of the method with the specified index in the currently selected runset.
        /// Returns 'false' if method index is out of range or the method with the given index doesn't use a sample plate. Check for errors in this case.
        /// This is only implemented for MASS-1 platform and will throw a <see cref="NotSupportedException"/> on SPR #64.
        /// </summary>
        /// <param name="methodIndex">index of the method in the runset for which the plate ID is assigned</param>
        /// <param name="plateId">ID of the plate</param>
        /// <returns>success status</returns>
        /// <exception cref="NotSupportedException">Thrown if not MASS-1 platform.</exception>
#if NET48
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "SetSamplePlateId?methodIndex={methodIndex}&plateId={plateId}", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet("methodIndex={methodIndex}&plateId={plateId}", Name = "SetSamplePlateId")]
        [Produces("application/json")]
#endif
        bool SetSamplePlateId(int methodIndex, string plateId);

        /// <summary>
        /// Gets the ID of the sample plate of the method with the given index in the current runset.
        /// Returns an empty string if the index is out of range, if there is no runset, no plate, or the plate doesn't have an ID assigned.
        /// This is only implemented for MASS-1 platform and will throw a <see cref="NotSupportedException"/> on SPR #64.
        /// </summary>
        /// <param name="methodIndex">index of the method in the current runset</param>
        /// <returns>ID of the sample plate</returns>
        /// <exception cref="NotSupportedException">Thrown if not MASS-1 platform.</exception>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetSamplePlateId")]
        [Produces("application/json")]
#endif
        string GetSamplePlateId(int methodIndex);

        /// <summary>
        /// Gets the ID of the sample plate that is currently inside the machine. Gets the ID of the current sample plate. Returns an empty string if there is no plate or ID is unknown.
        /// This is only implemented for MASS-1 platform and will throw a <see cref="NotSupportedException"/> on SPR #64.
        /// </summary>
        /// <returns>ID of the sample plate inside the machine</returns>
        /// <exception cref="NotSupportedException">Thrown if not MASS-1 platform.</exception>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetCurrentSamplePlateId")]
        [Produces("application/json")]
#endif
        string GetCurrentSamplePlateId();

        /// <summary>
        /// Assigns an id to the plate at the provided deck location of the method with the specified index in the currently selected runset.
        /// Returns 'false' if method index is out of range or the method with the given index doesn't use a sample plate. Check for errors in this case.
        /// </summary>
        /// <param name="methodIndex">index of the method in the runset for which the plate ID is assigned</param>
        /// /// <param name="plateId">ID of the plate</param>
        /// <param name="deckLocation">name of the deck location where the plate is located</param>
        /// <returns>success status</returns>
        /// <exception cref="ArgumentException">Thrown if location does not exist on deck.</exception>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet("methodIndex={methodIndex}&plateId={plateId}&deckLocation={deckLocation}", Name = "SetPlateId")]
        [Produces("application/json")]
#endif
        bool SetPlateId(int methodIndex, string plateId, string deckLocation);

        /// <summary>
        /// Gets the ID of the plate at the provided deck location of the method with the given index in the current runset.
        /// Returns an empty string if the index is out of range, if there is no runset, no plate, or the plate doesn't have an ID assigned.
        /// </summary>
        /// <param name="methodIndex">index of the method in the current runset</param>
        /// <param name="deckLocation">name of the deck location where the plate is located</param>
        /// <returns>ID of the plate</returns>
        /// <exception cref="ArgumentException">Thrown if location does not exist on deck.</exception>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetPlateId")]
        [Produces("application/json")]
#endif
        string GetPlateId(int methodIndex, string deckLocation);

        /// <summary>
        /// Gets the ID of the plate that is currently inside the machine at the provided deck location. Returns an empty string if there is no plate or ID is unknown.
        /// This is only implemented for MASS-1 platform and will throw a <see cref="NotSupportedException"/> on SPR #64.
        /// </summary>
        /// <param name="deckLocation">name of the deck location where the plate is located</param>
        /// <returns>ID of the plate</returns>
        /// <exception cref="ArgumentException">Thrown if location does not exist on deck.</exception>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetCurrentPlateId")]
        [Produces("application/json")]
#endif
        string GetCurrentPlateId(string deckLocation);

        /// <summary>
        /// Returns an array of all deck location names.
        /// </summary>
        /// <returns>names of all deck locations.</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetDeckLocations")]
        [Produces("application/json")]
#endif
        string[] GetDeckLocations();

        /// <summary>
        /// Moves the sample plate tray out of the device so the plate mover can get or put a plate from it. Returns 'false' if this is not possible.
        /// This might be the case if there is a runset or maintenance procedure currently running or if a reset is required. Check for errors in this case.
        /// This is only implemented for MASS-1 platform and will throw a <see cref="NotSupportedException"/> on SPR #64.
        /// </summary>
        /// <returns>success status</returns>
        /// <exception cref="NotSupportedException">Thrown if not MASS-1 platform.</exception>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "MoveSamplePlateTrayOut")]
        [Produces("application/json")]
#endif
        bool MoveSamplePlateTrayOut();

        /// <summary>
        /// Moves the sample plate tray into the housing. Returns 'false' if this is not possible.
        /// This might be the case if there is a runset or maintenance procedure is currently running or if a reset is required. Check for errors in this case.
        /// This is only implemented for MASS-1 platform and will throw a <see cref="NotSupportedException"/> on SPR #64.
        /// </summary>
        /// <returns>success status</returns>
        /// <exception cref="NotSupportedException">Thrown if not MASS-1 platform.</exception>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "MoveSamplePlateTrayIn")]
        [Produces("application/json")]
#endif
        bool MoveSamplePlateTrayIn();

        /// <summary>
        /// Moves the reagent plate tray out of the device so the plate mover can get or put a plate from it. Returns 'false' if this is not possible.
        /// This might be the case if there is a runset or maintenance procedure currently running or if a reset is required. Check for errors in this case.
        /// This is only implemented for MASS-1 platform and will throw a <see cref="NotSupportedException"/> on SPR #64.
        /// </summary>
        /// <returns>success status</returns>
        /// <exception cref="NotSupportedException">Thrown if not MASS-1 platform.</exception>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "MoveReagentPlateTrayOut")]
        [Produces("application/json")]
#endif
        bool MoveReagentPlateTrayOut();

        /// <summary>
        /// Moves the reagent plate tray into the housing. Returns 'false' if this is not possible.
        /// This might be the case if there is a runset or maintenance procedure is currently running or if a reset is required. Check for errors in this case.
        /// This is only implemented for MASS-1 platform and will throw a <see cref="NotSupportedException"/> on SPR #64.
        /// </summary>
        /// <returns>success status</returns>
        /// <exception cref="NotSupportedException">Thrown if not MASS-1 platform.</exception>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "MoveReagentPlateTrayIn")]
        [Produces("application/json")]
#endif
        bool MoveReagentPlateTrayIn();

        /// <summary>
        /// Opens the side door so that a plate mover can exchange a plate on the plate deck.
        /// This is only implemented for SPR #64 and will throw a <see cref="NotSupportedException"/> on MASS-1 platform.
        /// </summary>
        /// <returns>success status</returns>
        /// <exception cref="NotSupportedException">Thrown if not SPR #64.</exception>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "OpenSideDoor")]
        [Produces("application/json")]
#endif
        bool OpenSideDoor();

        /// <summary>
        /// Closes the side door after a plate mover has exchanged a plate on the plate deck.
        /// This is only implemented for SPR #64 and will throw a <see cref="NotSupportedException"/> on MASS-1 platform.
        /// </summary>
        /// <returns>success status</returns>
        /// <exception cref="NotSupportedException">Thrown if not SPR #64.</exception>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "CloseSideDoor")]
        [Produces("application/json")]
#endif
        bool CloseSideDoor();

        /// <summary>
        /// Starts the currently selected runset. Returns 'false' if this is not possible. Check for errors in this case.
        /// Possible reasons might be that the system is not ready, is on error, or there is no selected runset.
        /// </summary>
        /// <returns>success status</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "StartSelectedRunset")]
        [Produces("application/json")]
#endif
        bool StartSelectedRunset();

        /// <summary>
        /// Starts the runset starting from the method with the given index. Returns 'false' it this is not possible. Check for errors in this case.
        /// Possible reasons might be that the system is not ready, is on error, the index of the method is out of range, or there is no selected runset.
        /// </summary>
        /// <param name="methodIndex">index of the method in the runset from which the runset is started</param>
        /// <returns>success status</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "StartSelectedRunsetFrom")]
        [Produces("application/json")]
#endif
        bool StartSelectedRunsetFrom(int methodIndex);

        /// <summary>
        /// Pauses the runset that is currently running. The mode argument sets when the runset is paused. Returns 'false' if this is not possible because there is currently no runset running.
        /// </summary>
        /// <param name="pauseMode">1 = pause after current command, 2 = pause after current cycle, 3 = pause after current method</param>
        /// <returns>success status</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "PauseRunsetAfter")]
        [Produces("application/json")]
#endif
        bool PauseRunsetAfter(int pauseMode);

        /// <summary>
        /// Resumes the paused runset where it was paused. Returns false if this is not possible. Check for errors in this case.
        /// Reasons might be that the operation mode is different from 'paused' or device is not ready or on error.
        /// </summary>
        /// <returns>success status</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "ResumeRunset")]
        [Produces("application/json")]
#endif
        bool ResumeRunset();

        /// <summary>
        /// Resets the status of the runset after it was interrupted. Returns 'false' if this is not possible because the runset is currently running or if there is no runset to reset.
        /// Running the reset method multiple times on the same runset is possible, however.
        /// </summary>
        /// <returns>success status</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "ResetRunset")]
        [Produces("application/json")]
#endif
        bool ResetRunset();

        /// <summary>
        /// Aborts the currently running script immediately. That can either be a runset or a maintenance command. Returns 'false' if this is not possible because there is nothing running
        /// or running script is already a cancel script (since the device runs a script to clean up the flow cell when interrupted in the middle of an injection).
        /// </summary>
        /// <returns>success status</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "AbortScript")]
        [Produces("application/json")]
#endif
        bool AbortScript();

        /// <summary>
        /// Leaves the standby mode. Returns false if the machine is not in standby mode when this procedure is called.
        /// </summary>
        /// <returns>false if machine is not in standby</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "LeaveStandby")]
        [Produces("application/json")]
#endif
        bool LeaveStandby();

        /// <summary>
        /// Sets whether the device should go into standby mode after the runset has finished. Always return true.
        /// </summary>
        /// <param name="goToStandby">option whether machine goes to standby</param>
        /// <returns>always true</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "SetStandbyAfterFinish")]
        [Produces("application/json")]
#endif
        bool SetStandbyAfterFinish(bool goToStandby);

        /// <summary>
        /// Gets whether the device should go into standby mode after the runset has finished.
        /// </summary>
        /// <returns>true if device goes to standby on after finishing runsets</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetStandbyAfterFinish")]
        [Produces("application/json")]
#endif
        bool GetStandbyAfterFinish();

        /// <summary>
        /// Returns an array containing the names of the all maintenance commands that are available and can be run without user interaction.
        /// </summary>
        /// <returns>names of available maintenance procedures</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetNamesOfMaintenanceProcedures")]
        [Produces("application/json")]
#endif
        string[] GetNamesOfMaintenanceProcedures();

        /// <summary>
        /// Starts the maintenance procedure with the given name. Returns 'false' if this is not possible. Check for errors in this case.
        /// Reasons might be that the device is not ready or there is already something running. Check for errors in this case.
        /// </summary>
        /// <param name="procedureName">name of the maintenance procedure to be run</param>
        /// <returns>success status</returns>
#if NET48
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "RunMaintenanceProcedure")]
        [Produces("application/json")]
#endif
        bool RunMaintenanceProcedure(string procedureName);

        /// <summary>
        /// Gets the current operation mode of the device. The meaning of the return value can be found in a separate table.
        /// </summary>
        /// <returns>number that gives information about the operation mode:
        ///  0  Idle
        ///  1  Paused
        ///  2  Completed
        /// 10  StandBy
        /// 11  ResetRequired
        /// 12  MaintenanceRequired
        /// 13  DoorOpen
        /// 20  Running
        /// </returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetOperationMode")]
        [Produces("application/json")]
#endif
        int GetOperationMode();

        /// <summary>
        /// Checks whether a chip is docked.
        /// </summary>
        /// <returns>1 = docked, 0 = undocked, -1 = unknown </returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "IsChipDocked")]
        [Produces("application/json")]
#endif
        int IsChipDocked();

        /// <summary>
        /// Checks whether the sample plate tray is driven into the device.
        /// This is only implemented for MASS-1 platform and will throw a
        /// <see cref="NotSupportedException"/> on SPR #64.
        /// </summary>
        /// <returns>1 = tray in, 0 = tray out, -1 = unknown</returns>
        /// <exception cref="NotSupportedException">Thrown if not MASS-1 platform.</exception>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "IsSamplePlateTrayIn")]
        [Produces("application/json")]
#endif
        int IsSamplePlateTrayIn();

        /// <summary>
        /// Checks whether the reagent plate tray is driven into the device.
        /// This is only implemented for MASS-1 platform and will throw a
        /// <see cref="NotSupportedException"/> on SPR #64.
        /// </summary>
        /// <returns>1 = tray in, 0 = tray out, -1 = unknown</returns>
        /// <exception cref="NotSupportedException">Thrown if not MASS-1 platform.</exception>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "IsReagentPlateTrayIn")]
        [Produces("application/json")]
#endif
        int IsReagentPlateTrayIn();

        /// <summary>
        /// Checks whether the side door is open.
        /// This is only implemented for SPR #64 and will throw a
        /// <see cref="NotSupportedException"/> on MASS-1 platform.
        /// </summary>
        /// <returns>1 = open, 0 = closed, -1 = unknown</returns>
        /// <exception cref="NotSupportedException">Thrown if not SPR #64.</exception>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "IsSideDoorOpen")]
        [Produces("application/json")]
#endif
        int IsSideDoorOpen();

        /// <summary>
        /// Checks whether the server's message loop contains at least one new message.
        /// </summary>
        /// <returns>true if there is at least one message in the queue</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "HasMessage")]
        [Produces("application/json")]
#endif
        bool HasMessage();

        /// <summary>
        /// Gets the first message from the server's message queue. A message consists of three strings in an array where the first string is a time stamp formatted like yyyyMMdd-hhmmss.fff,
        /// the second gives information about the type of message (Error, Warning, DataSaved, OperationModeChanged, ... ), and the third string is the message itself.
        /// Call this method on a regular base using polling and check the result for null or use the HasNewMessage method first. A complete list of error types can be found in a separate table.
        /// </summary>
        /// <returns>array of three string</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetMessage")]
        [Produces("application/json")]
#endif
        string[] GetMessage();

        /// <summary>
        /// Checks whether there are any errors that are not covered by the operation mode.
        /// </summary>
        /// <returns>true if something wrong with the device's status</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "HasErrors")]
        [Produces("application/json")]
#endif
        bool HasErrors();

        /// <summary>
        /// Gets an array of strings with information about errors like 'no data', 'runset error', ...
        /// </summary>
        /// <returns>array of current errors</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetErrors")]
        [Produces("application/json")]
#endif
        string[] GetErrors();

        /// <summary>
        /// Checks whether there are any warnings that are not covered by the operation mode.
        /// </summary>
        /// <returns>true if something wrong with the device's status</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "HasWarnings")]
        [Produces("application/json")]
#endif
        bool HasWarnings();

        /// <summary>
        /// Gets an array of strings with information about errors like 'temperature unstable', 'runset warning', ...
        /// </summary>
        /// <returns>array of current errors</returns>
#if NET48
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
#elif NET8_0
        [HttpGet(Name = "GetWarnings")]
        [Produces("application/json")]
#endif
        string[] GetWarnings();
    }
}
