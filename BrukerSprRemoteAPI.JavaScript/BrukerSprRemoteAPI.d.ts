/**
 * Bruker SPR Remote API JavaScript Client.
 */
export class BrukerSprRemoteAPI {
    /**
     * Initializes a new instance of the BrukerSprRemoteAPI client.
     * @param url The base REST service URL.
     */
    constructor(url: string);

    /** Base URL of the REST service. */
    readonly url: string;

    /** Executes an API call. */
    CallApi(call: string): Promise<any>;
    callApi(call: string): Promise<any>;

    /**
     * Returns an array of all method names that are loaded in the current runset library.
     * @returns Names of all methods.
     */
    GetNamesOfMethods(): Promise<string[]>;

    /**
     * Returns an array containing the names of all methods of the provided assay type that are loaded in the current runset library.
     * @param assayType The assay type (the list of assay types can be obtained using GetAssayTypesOfAllMethods).
     * @returns Names of all methods of the provided assay type.
     */
    GetNamesOfMethodsOfAssayType(assayType: string): Promise<string[]>;

    /**
     * Returns an array containing the assay types of all methods that are loaded in the current runset library.
     * @returns Distinct array of assay types.
     */
    GetAssayTypesOfAllMethods(): Promise<string[]>;

    /**
     * Returns the name of the method that is currently running or selected for running. Returns an empty string if there is no method selected.
     * @returns Method name, or an empty string if no method is selected.
     */
    GetNameOfCurrentMethod(): Promise<string>;

    /**
     * Returns the assay type of the method that is currently running or selected for running. Returns an empty string if there is no method selected.
     * @returns Assay type, or an empty string if no method is selected.
     */
    GetAssayTypeOfCurrentMethod(): Promise<string>;

    /**
     * Returns the assay type of the method with the given name. Returns an empty string if there is no method with the given name in the runset library.
     * @param methodName Name of a method.
     * @returns Assay type of the method, or an empty string if not found in the runset library.
     */
    GetAssayTypeOfMethod(methodName: string): Promise<string>;

    /**
     * Returns an array of all runset names that are loaded in the current runset library.
     * @returns Names of all runsets.
     */
    GetNamesOfRunsets(): Promise<string[]>;

    /**
     * Returns an array containing the names of all runsets of the provided assay type that are loaded in the current runset library.
     * @param assayType The assay type (the list of assay types can be obtained using GetAssayTypesOfAllRunsets).
     * @returns Names of all runsets of the provided assay type.
     */
    GetNamesOfRunsetsOfAssayType(assayType: string): Promise<string[]>;

    /**
     * Returns an array containing the assay types of all runsets that are loaded in the current runset library.
     * @returns Distinct array of assay types.
     */
    GetAssayTypesOfAllRunsets(): Promise<string[]>;

    /**
     * Returns the name of the runset that is currently running or selected for running. Returns an empty string if there is no runset selected.
     * @returns Runset name, or an empty string if no runset is selected.
     */
    GetNameOfCurrentRunset(): Promise<string>;

    /**
     * Returns the assay type of the runset that is currently running or selected for running. Returns an empty string if there is no runset selected.
     * @returns Assay type, or an empty string if no runset is selected.
     */
    GetAssayTypeOfCurrentRunset(): Promise<string>;

    /**
     * Returns the assay type of the runset with the given name. Returns an empty string if there is no runset with the given name in the runset library.
     * @param runsetName Name of a runset.
     * @returns Assay type of the runset, or an empty string if not found in the runset library.
     */
    GetAssayTypeOfRunset(runsetName: string): Promise<string>;

    /**
     * Returns the names of the methods in the runset with the given name.
     * @param runsetName Name of a runset.
     * @returns Ordered array of method names in the runset.
     */
    GetMethodNamesOfRunset(runsetName: string): Promise<string[]>;

    /**
     * Selects the specified method for running. Returns 'false' if the method could not be selected. Check for error messages in that case. Possible reasons are that the operation mode doesn't allow to select a method or that the method with the provided name doesn't exist in the runset library.
     * @param methodName Name of a method.
     * @returns true if the method was successfully selected; otherwise, false.
     */
    SelectMethod(methodName: string): Promise<boolean>;

    /**
     * Selects the specified runset for running. Returns 'false' if the runset could not be selected. Check for error messages in that case. Possible reasons are that the operation mode doesn't allow to select a runset or that the runset with the provided name doesn't exist in the runset library.
     * @param runsetName Name of a runset.
     * @returns true if the runset was successfully selected; otherwise, false.
     */
    SelectRunset(runsetName: string): Promise<boolean>;

    /**
     * Creates a runset that contains the methods specified by the passed array of method names and selects the runset for running. Returns 'false' if the runset couldn't be created. Check for error messages in that case. Possible reasons are that the operation mode doesn't allow to select a runset or that one or more methods with the provided names do not exist in the runset library.
     * @param methodNames Ordered array of method names.
     * @returns true if the runset was successfully created; otherwise, false.
     */
    CreateRunset(methodNames: string[]): Promise<boolean>;

    /**
     * Assigns an ID to the exchangeable plate of the method with the specified index in the currently selected runset. Returns 'false' if the index of the method is out of range or if the method with the given index doesn't use a sample plate. Check for errors in this case. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     * @param methodIndex Index of the method in the runset for which the plate ID is assigned.
     * @param plateId ID of the plate.
     * @returns true if the plate ID was successfully assigned; otherwise, false.
     */
    SetSamplePlateId(methodIndex: number, plateId: string): Promise<boolean>;

    /**
     * Gets the ID of the sample plate of the method with the given index in the current runset. Returns an empty string if the index is out of range, if there is no runset, no plate, or the plate doesn't have an ID assigned. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     * @param methodIndex Index of the method in the current runset.
     * @returns ID of the sample plate, or an empty string if not available.
     */
    GetSamplePlateId(methodIndex: number): Promise<string>;

    /**
     * Gets the ID of the sample plate that is currently inside the machine. Returns an empty string if there is no plate inside the machine or the ID is unknown. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     * @returns ID of the sample plate inside the machine, or an empty string if no plate is inside or the ID is unknown.
     */
    GetCurrentSamplePlateId(): Promise<string>;

    /**
     * Assigns an ID to the plate at the provided deck location of the method with the specified index in the currently selected runset. Returns 'false' if the method index is out of range or the method with the given index doesn't use a sample plate. Check for errors in this case.
     * @param methodIndex Index of the method in the runset for which the plate ID is assigned.
     * @param plateId ID of the plate.
     * @param deckLocation Name of the deck location where the plate is located.
     * @returns true if the plate ID was successfully assigned; otherwise, false.
     */
    SetPlateId(methodIndex: number, plateId: string, deckLocation: string): Promise<boolean>;

    /**
     * Gets the ID of the plate at the provided deck location of the method with the given index in the current runset. Returns an empty string if the index is out of range, if there is no runset, no plate, or the plate doesn't have an ID assigned.
     * @param methodIndex Index of the method in the current runset.
     * @param deckLocation Name of the deck location where the plate is located.
     * @returns ID of the plate, or an empty string if not available.
     */
    GetPlateId(methodIndex: number, deckLocation: string): Promise<string>;

    /**
     * Gets the ID of the plate that is currently inside the machine at the provided deck location. Returns an empty string if there is no plate or ID is unknown. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     * @param deckLocation Name of the deck location where the plate is located.
     * @returns ID of the plate, or an empty string if no plate is present or the ID is unknown.
     */
    GetCurrentPlateId(deckLocation: string): Promise<string>;

    /**
     * Returns an array of all deck location names.
     * @returns Names of all deck locations.
     */
    GetDeckLocations(): Promise<string[]>;

    /**
     * Moves the sample plate tray out of the machine so that the plate mover can get or put a plate from it. Returns 'false' if this is not possible. This might be the case if there is a runset or maintenance procedure currently running or if a reset is required. Check for errors in this case. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     * @returns true if the sample plate tray was moved out; otherwise, false.
     */
    MoveSamplePlateTrayOut(): Promise<boolean>;

    /**
     * Moves the sample plate tray into the housing. Returns 'false' if this is not possible. This might be the case if there is a runset or a maintenance procedure is currently running or if a reset is required. Check for errors in this case. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     * @returns true if the sample plate tray was moved in; otherwise, false.
     */
    MoveSamplePlateTrayIn(): Promise<boolean>;

    /**
     * Moves the reagent plate tray out of the device so the plate mover can get or put a plate from it. Returns 'false' if this is not possible. This might be the case if there is a runset or maintenance procedure currently running or if a reset is required. Check for errors in this case. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     * @returns true if the reagent plate tray was moved out; otherwise, false.
     */
    MoveReagentPlateTrayOut(): Promise<boolean>;

    /**
     * Moves the reagent plate tray into the housing. Returns 'false' if this is not possible. This might be the case if there is a runset or maintenance procedure is currently running or if a reset is required. Check for errors in this case. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     * @returns true if the reagent plate tray was moved in; otherwise, false.
     */
    MoveReagentPlateTrayIn(): Promise<boolean>;

    /**
     * Opens the side door so that a plate mover can exchange a plate on the plate deck. This is only implemented for SPR #64 and will throw a NotSupportedException on MASS-1 platform.
     * @returns true if the side door was opened; otherwise, false.
     */
    OpenSideDoor(): Promise<boolean>;

    /**
     * Closes the side door after a plate mover has exchanged a plate on the plate deck. This is only implemented for SPR #64 and will throw a NotSupportedException on MASS-1 platform.
     * @returns true if the side door was closed; otherwise, false.
     */
    CloseSideDoor(): Promise<boolean>;

    /**
     * Starts the currently selected runset. Returns 'false' if this is not possible. Check for errors in this case. Possible reasons might be that the system is not ready, is on error, or there is no selected runset.
     * @returns true if the runset was started; otherwise, false.
     */
    StartSelectedRunset(): Promise<boolean>;

    /**
     * Starts the selected runset starting from the method with the given index. Returns 'false' if this is not possible. Check for errors in this case. Possible reasons might be that the system is not ready, is on error, the index of the method is out of range, or no runset is selected.
     * @param methodIndex Index of the method in the runset from which the runset is started.
     * @returns true if the runset was started; otherwise, false.
     */
    StartSelectedRunsetFrom(methodIndex: number): Promise<boolean>;

    /**
     * Pauses the runset that is currently running. The pauseMode argument sets when the runset is paused (1 = after current command, 2 = after current cycle, 3 = after current method). Returns 'false' if this is not possible because there is currently no runset running.
     * @param pauseMode 1 = pause after current command, 2 = pause after current cycle, 3 = pause after current method.
     * @returns true if the runset was paused; otherwise, false.
     */
    PauseRunsetAfter(pauseMode: number): Promise<boolean>;

    /**
     * Resumes the paused runset where it was paused. Returns 'false' if this is not possible. Check for errors in this case. Reasons might be that the operation mode is different from 'paused' or the device is not ready or on error.
     * @returns true if the runset was resumed; otherwise, false.
     */
    ResumeRunset(): Promise<boolean>;

    /**
     * Resets the status of the runset after it was interrupted. Returns 'false' if this is not possible because the runset is currently running or if there is no runset to reset. Running the reset method multiple times on the same runset is possible.
     * @returns true if the runset was reset; otherwise, false.
     */
    ResetRunset(): Promise<boolean>;

    /**
     * Aborts the currently running script immediately. That can either be a runset or a maintenance command. Returns 'false' if this is not possible because there is nothing running or the running script is already a cancel script (since the device runs a script to clean up the flow cell when interrupted in the middle of an injection).
     * @returns true if the script was aborted; otherwise, false.
     */
    AbortScript(): Promise<boolean>;

    /**
     * Leaves the standby mode. Returns 'false' if the machine is not in standby mode when this API method is called.
     * @returns true if the machine left standby mode; otherwise, false.
     */
    LeaveStandby(): Promise<boolean>;

    /**
     * Sets whether the device should go into standby mode after the runset has finished. Always return true.
     * @param goToStandby Option whether the device should go into standby mode after the runset has finished.
     * @returns Always true.
     */
    SetStandbyAfterFinish(goToStandby: boolean): Promise<boolean>;

    /**
     * Gets whether the device should go into standby mode after the runset has finished.
     * @returns true if the device goes into standby mode after finishing the runset; otherwise, false.
     */
    GetStandbyAfterFinish(): Promise<boolean>;

    /**
     * Returns an array containing the names of all maintenance commands that are available and can be run without user interaction.
     * @returns Names of all available maintenance procedures.
     */
    GetNamesOfMaintenanceProcedures(): Promise<string[]>;

    /**
     * Starts the maintenance procedure with the given name. Returns 'false' if this is not possible. Check for errors in this case. Reasons might be that the device is not ready or there is already something running.
     * @param procedureName Name of the maintenance procedure to be run.
     * @returns true if the maintenance procedure was started; otherwise, false.
     */
    RunMaintenanceProcedure(procedureName: string): Promise<boolean>;

    /**
     * Gets the current operation mode of the device. The meaning of the return value can be found in the OperationMode documentation.
     * @returns Integer value representing the operation mode. See OperationMode for further details.
     */
    GetOperationMode(): Promise<number>;

    /**
     * Checks whether a chip is docked. Returns '1' = docked, '0' = undocked, '-1' = unknown.
     * @returns 1 = docked, 0 = undocked, -1 = unknown.
     */
    IsChipDocked(): Promise<number>;

    /**
     * Checks whether the sample plate tray is inside the machine. Returns '1' = inside, '0' = outside, '-1' = unknown. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     * @returns 1 = inside, 0 = outside, -1 = unknown.
     */
    IsSamplePlateTrayIn(): Promise<number>;

    /**
     * Checks whether the reagent plate tray is inside the machine. Returns '1' = inside, '0' = outside, '-1' = unknown. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     * @returns 1 = inside, 0 = outside, -1 = unknown.
     */
    IsReagentPlateTrayIn(): Promise<number>;

    /**
     * Checks whether the side door is open. Returns '1' = open, '0' = closed, '-1' = unknown. This is only implemented for SPR #64 and will throw a NotSupportedException on MASS-1 platform.
     * @returns 1 = open, 0 = closed, -1 = unknown.
     */
    IsSideDoorOpen(): Promise<number>;

    /**
     * Checks whether the server's message loop contains at least one new message.
     * @returns true if there is at least one message in the queue; otherwise, false.
     */
    HasMessage(): Promise<boolean>;

    /**
     * Gets the first message from the server's message queue. A message consists of three strings in an array where the first string is a time stamp formatted like yyyyMMdd-hhmmss.fff, the second gives information about the type of message (Error, Warning, DataSaved, OperationModeChanged, …), and the third string is the message itself. Call this method on a regular base using polling and check the result for null or use the HasMessage method first. A complete list of error types can be found in Error Types.
     * @returns Array of three strings representing the message (timestamp, type, message), or null if no message is available.
     */
    GetMessage(): Promise<string[]>;

    /**
     * Checks whether there are any errors that are not covered by the operation mode.
     * @returns true if there are errors not covered by the operation mode; otherwise, false.
     */
    HasErrors(): Promise<boolean>;

    /**
     * Gets an array of strings with information about errors like 'no data', 'runset error', ...
     * @returns Array of current errors.
     */
    GetErrors(): Promise<string[]>;

    /**
     * Checks whether there are any warnings that are not covered by the operation mode.
     * @returns true if there are warnings not covered by the operation mode; otherwise, false.
     */
    HasWarnings(): Promise<boolean>;

    /**
     * Gets an array of strings with information about warnings like 'temperature unstable', 'runset warning', ...
     * @returns Array of current warnings.
     */
    GetWarnings(): Promise<string[]>;

}
export default BrukerSprRemoteAPI;
