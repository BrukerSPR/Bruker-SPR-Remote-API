/**
 * Bruker SPR Remote API JavaScript/Node.js Client.
 * Enables programmatic interaction with the Bruker SPR Control Software REST API.
 */
class BrukerSprRemoteAPI {
    /**
     * Initializes a new instance of the BrukerSprRemoteAPI client.
     * @param {string} url - The base REST service URL (e.g. 'http://localhost:9001/bruker-spr/json/').
     */
    constructor(url) {
        if (!url || typeof url !== 'string') {
            throw new TypeError("Parameter 'url' must be a non-empty string.");
        }
        this.url = url.endsWith('/') ? url : url + '/';
    }

    /**
     * URL-encodes a parameter value.
     * @param {*} value - The value to encode.
     * @returns {string} Encoded string.
     */
    __replaceSpace__(value) {
        return encodeURIComponent(String(value));
    }

    /**
     * Sends an HTTP GET request to the Bruker SPR REST service.
     * @param {string} call - The method path with query parameters.
     * @returns {Promise<any>} The parsed JSON response.
     */
    async CallApi(call) {
        const targetUrl = this.url + String(call);
        const response = await fetch(targetUrl, {
            method: 'GET',
            headers: {
                'Accept': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error(`Bruker SPR Remote API error: HTTP ${response.status} ${response.statusText}`);
        }

        return await response.json();
    }

    /**
     * Alias for CallApi.
     * @param {string} call - The method path with query parameters.
     * @returns {Promise<any>}
     */
    callApi(call) {
        return this.CallApi(call);
    }

    /**
     * Returns an array of all method names that are loaded in the current runset library.
     *
     * @returns {Promise<string[]>} - Names of all methods.
     */
    async GetNamesOfMethods() {
        return this.CallApi('GetNamesOfMethods');
    }

    /**
     * Returns an array containing the names of all methods of the provided assay type that are loaded in the current runset library.
     *
     * @param {string} assayType - The assay type (the list of assay types can be obtained using GetAssayTypesOfAllMethods).
     * @returns {Promise<string[]>} - Names of all methods of the provided assay type.
     */
    async GetNamesOfMethodsOfAssayType(assayType) {
        if (typeof assayType !== 'string') {
            throw new TypeError("Parameter 'assayType' must be of type string, but got " + (typeof assayType));
        }
        return this.CallApi('GetNamesOfMethodsOfAssayType?assayType=' + encodeURIComponent(assayType));
    }

    /**
     * Returns an array containing the assay types of all methods that are loaded in the current runset library.
     *
     * @returns {Promise<string[]>} - Distinct array of assay types.
     */
    async GetAssayTypesOfAllMethods() {
        return this.CallApi('GetAssayTypesOfAllMethods');
    }

    /**
     * Returns the name of the method that is currently running or selected for running. Returns an empty string if there is no method selected.
     *
     * @returns {Promise<string>} - Method name, or an empty string if no method is selected.
     */
    async GetNameOfCurrentMethod() {
        return this.CallApi('GetNameOfCurrentMethod');
    }

    /**
     * Returns the assay type of the method that is currently running or selected for running. Returns an empty string if there is no method selected.
     *
     * @returns {Promise<string>} - Assay type, or an empty string if no method is selected.
     */
    async GetAssayTypeOfCurrentMethod() {
        return this.CallApi('GetAssayTypeOfCurrentMethod');
    }

    /**
     * Returns the assay type of the method with the given name. Returns an empty string if there is no method with the given name in the runset library.
     *
     * @param {string} methodName - Name of a method.
     * @returns {Promise<string>} - Assay type of the method, or an empty string if not found in the runset library.
     */
    async GetAssayTypeOfMethod(methodName) {
        if (typeof methodName !== 'string') {
            throw new TypeError("Parameter 'methodName' must be of type string, but got " + (typeof methodName));
        }
        return this.CallApi('GetAssayTypeOfMethod?methodName=' + encodeURIComponent(methodName));
    }

    /**
     * Returns an array of all runset names that are loaded in the current runset library.
     *
     * @returns {Promise<string[]>} - Names of all runsets.
     */
    async GetNamesOfRunsets() {
        return this.CallApi('GetNamesOfRunsets');
    }

    /**
     * Returns an array containing the names of all runsets of the provided assay type that are loaded in the current runset library.
     *
     * @param {string} assayType - The assay type (the list of assay types can be obtained using GetAssayTypesOfAllRunsets).
     * @returns {Promise<string[]>} - Names of all runsets of the provided assay type.
     */
    async GetNamesOfRunsetsOfAssayType(assayType) {
        if (typeof assayType !== 'string') {
            throw new TypeError("Parameter 'assayType' must be of type string, but got " + (typeof assayType));
        }
        return this.CallApi('GetNamesOfRunsetsOfAssayType?assayType=' + encodeURIComponent(assayType));
    }

    /**
     * Returns an array containing the assay types of all runsets that are loaded in the current runset library.
     *
     * @returns {Promise<string[]>} - Distinct array of assay types.
     */
    async GetAssayTypesOfAllRunsets() {
        return this.CallApi('GetAssayTypesOfAllRunsets');
    }

    /**
     * Returns the name of the runset that is currently running or selected for running. Returns an empty string if there is no runset selected.
     *
     * @returns {Promise<string>} - Runset name, or an empty string if no runset is selected.
     */
    async GetNameOfCurrentRunset() {
        return this.CallApi('GetNameOfCurrentRunset');
    }

    /**
     * Returns the assay type of the runset that is currently running or selected for running. Returns an empty string if there is no runset selected.
     *
     * @returns {Promise<string>} - Assay type, or an empty string if no runset is selected.
     */
    async GetAssayTypeOfCurrentRunset() {
        return this.CallApi('GetAssayTypeOfCurrentRunset');
    }

    /**
     * Returns the assay type of the runset with the given name. Returns an empty string if there is no runset with the given name in the runset library.
     *
     * @param {string} runsetName - Name of a runset.
     * @returns {Promise<string>} - Assay type of the runset, or an empty string if not found in the runset library.
     */
    async GetAssayTypeOfRunset(runsetName) {
        if (typeof runsetName !== 'string') {
            throw new TypeError("Parameter 'runsetName' must be of type string, but got " + (typeof runsetName));
        }
        return this.CallApi('GetAssayTypeOfRunset?runsetName=' + encodeURIComponent(runsetName));
    }

    /**
     * Returns the names of the methods in the runset with the given name.
     *
     * @param {string} runsetName - Name of a runset.
     * @returns {Promise<string[]>} - Ordered array of method names in the runset.
     */
    async GetMethodNamesOfRunset(runsetName) {
        if (typeof runsetName !== 'string') {
            throw new TypeError("Parameter 'runsetName' must be of type string, but got " + (typeof runsetName));
        }
        return this.CallApi('GetMethodNamesOfRunset?runsetName=' + encodeURIComponent(runsetName));
    }

    /**
     * Selects the specified method for running. Returns 'false' if the method could not be selected. Check for error messages in that case. Possible reasons are that the operation mode doesn't allow to select a method or that the method with the provided name doesn't exist in the runset library.
     *
     * @param {string} methodName - Name of a method.
     * @returns {Promise<boolean>} - true if the method was successfully selected; otherwise, false.
     */
    async SelectMethod(methodName) {
        if (typeof methodName !== 'string') {
            throw new TypeError("Parameter 'methodName' must be of type string, but got " + (typeof methodName));
        }
        return this.CallApi('SelectMethod?methodName=' + encodeURIComponent(methodName));
    }

    /**
     * Selects the specified runset for running. Returns 'false' if the runset could not be selected. Check for error messages in that case. Possible reasons are that the operation mode doesn't allow to select a runset or that the runset with the provided name doesn't exist in the runset library.
     *
     * @param {string} runsetName - Name of a runset.
     * @returns {Promise<boolean>} - true if the runset was successfully selected; otherwise, false.
     */
    async SelectRunset(runsetName) {
        if (typeof runsetName !== 'string') {
            throw new TypeError("Parameter 'runsetName' must be of type string, but got " + (typeof runsetName));
        }
        return this.CallApi('SelectRunset?runsetName=' + encodeURIComponent(runsetName));
    }

    /**
     * Creates a runset that contains the methods specified by the passed array of method names and selects the runset for running. Returns 'false' if the runset couldn't be created. Check for error messages in that case. Possible reasons are that the operation mode doesn't allow to select a runset or that one or more methods with the provided names do not exist in the runset library.
     *
     * @param {string[]} methodNames - Ordered array of method names.
     * @returns {Promise<boolean>} - true if the runset was successfully created; otherwise, false.
     */
    async CreateRunset(methodNames) {
        if (!Array.isArray(methodNames) || !methodNames.every(x => typeof x === 'string')) {
            throw new TypeError("Parameter 'methodNames' must be an array of strings.");
        }
        const queryParts = [];
        queryParts.push(...methodNames.map(item => 'methodNames=' + encodeURIComponent(item)));
        return this.CallApi('CreateRunset?' + queryParts.join('&'));
    }

    /**
     * Assigns an ID to the exchangeable plate of the method with the specified index in the currently selected runset. Returns 'false' if the index of the method is out of range or if the method with the given index doesn't use a sample plate. Check for errors in this case. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     *
     * @param {number} methodIndex - Index of the method in the runset for which the plate ID is assigned.
     * @param {string} plateId - ID of the plate.
     * @returns {Promise<boolean>} - true if the plate ID was successfully assigned; otherwise, false.
     * @throws {NotSupportedException} Thrown if not MASS-1 platform.
     */
    async SetSamplePlateId(methodIndex, plateId) {
        if (typeof methodIndex !== 'number' || Number.isNaN(methodIndex)) {
            throw new TypeError("Parameter 'methodIndex' must be of type number, but got " + (typeof methodIndex));
        }
        if (typeof plateId !== 'string') {
            throw new TypeError("Parameter 'plateId' must be of type string, but got " + (typeof plateId));
        }
        return this.CallApi('SetSamplePlateId?methodIndex=' + methodIndex + '&plateId=' + encodeURIComponent(plateId));
    }

    /**
     * Gets the ID of the sample plate of the method with the given index in the current runset. Returns an empty string if the index is out of range, if there is no runset, no plate, or the plate doesn't have an ID assigned. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     *
     * @param {number} methodIndex - Index of the method in the current runset.
     * @returns {Promise<string>} - ID of the sample plate, or an empty string if not available.
     * @throws {NotSupportedException} Thrown if not MASS-1 platform.
     */
    async GetSamplePlateId(methodIndex) {
        if (typeof methodIndex !== 'number' || Number.isNaN(methodIndex)) {
            throw new TypeError("Parameter 'methodIndex' must be of type number, but got " + (typeof methodIndex));
        }
        return this.CallApi('GetSamplePlateId?methodIndex=' + methodIndex);
    }

    /**
     * Gets the ID of the sample plate that is currently inside the machine. Returns an empty string if there is no plate inside the machine or the ID is unknown. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     *
     * @returns {Promise<string>} - ID of the sample plate inside the machine, or an empty string if no plate is inside or the ID is unknown.
     * @throws {NotSupportedException} Thrown if not MASS-1 platform.
     */
    async GetCurrentSamplePlateId() {
        return this.CallApi('GetCurrentSamplePlateId');
    }

    /**
     * Assigns an ID to the plate at the provided deck location of the method with the specified index in the currently selected runset. Returns 'false' if the method index is out of range or the method with the given index doesn't use a sample plate. Check for errors in this case.
     *
     * @param {number} methodIndex - Index of the method in the runset for which the plate ID is assigned.
     * @param {string} plateId - ID of the plate.
     * @param {string} deckLocation - Name of the deck location where the plate is located.
     * @returns {Promise<boolean>} - true if the plate ID was successfully assigned; otherwise, false.
     * @throws {ArgumentException} Thrown if location does not exist on deck.
     */
    async SetPlateId(methodIndex, plateId, deckLocation) {
        if (typeof methodIndex !== 'number' || Number.isNaN(methodIndex)) {
            throw new TypeError("Parameter 'methodIndex' must be of type number, but got " + (typeof methodIndex));
        }
        if (typeof plateId !== 'string') {
            throw new TypeError("Parameter 'plateId' must be of type string, but got " + (typeof plateId));
        }
        if (typeof deckLocation !== 'string') {
            throw new TypeError("Parameter 'deckLocation' must be of type string, but got " + (typeof deckLocation));
        }
        return this.CallApi('SetPlateId?methodIndex=' + methodIndex + '&plateId=' + encodeURIComponent(plateId) + '&deckLocation=' + encodeURIComponent(deckLocation));
    }

    /**
     * Gets the ID of the plate at the provided deck location of the method with the given index in the current runset. Returns an empty string if the index is out of range, if there is no runset, no plate, or the plate doesn't have an ID assigned.
     *
     * @param {number} methodIndex - Index of the method in the current runset.
     * @param {string} deckLocation - Name of the deck location where the plate is located.
     * @returns {Promise<string>} - ID of the plate, or an empty string if not available.
     * @throws {ArgumentException} Thrown if location does not exist on deck.
     */
    async GetPlateId(methodIndex, deckLocation) {
        if (typeof methodIndex !== 'number' || Number.isNaN(methodIndex)) {
            throw new TypeError("Parameter 'methodIndex' must be of type number, but got " + (typeof methodIndex));
        }
        if (typeof deckLocation !== 'string') {
            throw new TypeError("Parameter 'deckLocation' must be of type string, but got " + (typeof deckLocation));
        }
        return this.CallApi('GetPlateId?methodIndex=' + methodIndex + '&deckLocation=' + encodeURIComponent(deckLocation));
    }

    /**
     * Gets the ID of the plate that is currently inside the machine at the provided deck location. Returns an empty string if there is no plate or ID is unknown. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     *
     * @param {string} deckLocation - Name of the deck location where the plate is located.
     * @returns {Promise<string>} - ID of the plate, or an empty string if no plate is present or the ID is unknown.
     * @throws {ArgumentException} Thrown if location does not exist on deck.
     * @throws {NotSupportedException} Thrown if not MASS-1 platform.
     */
    async GetCurrentPlateId(deckLocation) {
        if (typeof deckLocation !== 'string') {
            throw new TypeError("Parameter 'deckLocation' must be of type string, but got " + (typeof deckLocation));
        }
        return this.CallApi('GetCurrentPlateId?deckLocation=' + encodeURIComponent(deckLocation));
    }

    /**
     * Returns an array of all deck location names.
     *
     * @returns {Promise<string[]>} - Names of all deck locations.
     */
    async GetDeckLocations() {
        return this.CallApi('GetDeckLocations');
    }

    /**
     * Moves the sample plate tray out of the machine so that the plate mover can get or put a plate from it. Returns 'false' if this is not possible. This might be the case if there is a runset or maintenance procedure currently running or if a reset is required. Check for errors in this case. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     *
     * @returns {Promise<boolean>} - true if the sample plate tray was moved out; otherwise, false.
     * @throws {NotSupportedException} Thrown if not MASS-1 platform.
     */
    async MoveSamplePlateTrayOut() {
        return this.CallApi('MoveSamplePlateTrayOut');
    }

    /**
     * Moves the sample plate tray into the housing. Returns 'false' if this is not possible. This might be the case if there is a runset or a maintenance procedure is currently running or if a reset is required. Check for errors in this case. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     *
     * @returns {Promise<boolean>} - true if the sample plate tray was moved in; otherwise, false.
     * @throws {NotSupportedException} Thrown if not MASS-1 platform.
     */
    async MoveSamplePlateTrayIn() {
        return this.CallApi('MoveSamplePlateTrayIn');
    }

    /**
     * Moves the reagent plate tray out of the device so the plate mover can get or put a plate from it. Returns 'false' if this is not possible. This might be the case if there is a runset or maintenance procedure currently running or if a reset is required. Check for errors in this case. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     *
     * @returns {Promise<boolean>} - true if the reagent plate tray was moved out; otherwise, false.
     * @throws {NotSupportedException} Thrown if not MASS-1 platform.
     */
    async MoveReagentPlateTrayOut() {
        return this.CallApi('MoveReagentPlateTrayOut');
    }

    /**
     * Moves the reagent plate tray into the housing. Returns 'false' if this is not possible. This might be the case if there is a runset or maintenance procedure is currently running or if a reset is required. Check for errors in this case. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     *
     * @returns {Promise<boolean>} - true if the reagent plate tray was moved in; otherwise, false.
     * @throws {NotSupportedException} Thrown if not MASS-1 platform.
     */
    async MoveReagentPlateTrayIn() {
        return this.CallApi('MoveReagentPlateTrayIn');
    }

    /**
     * Opens the side door so that a plate mover can exchange a plate on the plate deck. This is only implemented for SPR #64 and will throw a NotSupportedException on MASS-1 platform.
     *
     * @returns {Promise<boolean>} - true if the side door was opened; otherwise, false.
     * @throws {NotSupportedException} Thrown if not SPR #64.
     */
    async OpenSideDoor() {
        return this.CallApi('OpenSideDoor');
    }

    /**
     * Closes the side door after a plate mover has exchanged a plate on the plate deck. This is only implemented for SPR #64 and will throw a NotSupportedException on MASS-1 platform.
     *
     * @returns {Promise<boolean>} - true if the side door was closed; otherwise, false.
     * @throws {NotSupportedException} Thrown if not SPR #64.
     */
    async CloseSideDoor() {
        return this.CallApi('CloseSideDoor');
    }

    /**
     * Starts the currently selected runset. Returns 'false' if this is not possible. Check for errors in this case. Possible reasons might be that the system is not ready, is on error, or there is no selected runset.
     *
     * @returns {Promise<boolean>} - true if the runset was started; otherwise, false.
     */
    async StartSelectedRunset() {
        return this.CallApi('StartSelectedRunset');
    }

    /**
     * Starts the selected runset starting from the method with the given index. Returns 'false' if this is not possible. Check for errors in this case. Possible reasons might be that the system is not ready, is on error, the index of the method is out of range, or no runset is selected.
     *
     * @param {number} methodIndex - Index of the method in the runset from which the runset is started.
     * @returns {Promise<boolean>} - true if the runset was started; otherwise, false.
     */
    async StartSelectedRunsetFrom(methodIndex) {
        if (typeof methodIndex !== 'number' || Number.isNaN(methodIndex)) {
            throw new TypeError("Parameter 'methodIndex' must be of type number, but got " + (typeof methodIndex));
        }
        return this.CallApi('StartSelectedRunsetFrom?methodIndex=' + methodIndex);
    }

    /**
     * Pauses the runset that is currently running. The pauseMode argument sets when the runset is paused (1 = after current command, 2 = after current cycle, 3 = after current method). Returns 'false' if this is not possible because there is currently no runset running.
     *
     * @param {number} pauseMode - 1 = pause after current command, 2 = pause after current cycle, 3 = pause after current method.
     * @returns {Promise<boolean>} - true if the runset was paused; otherwise, false.
     */
    async PauseRunsetAfter(pauseMode) {
        if (typeof pauseMode !== 'number' || Number.isNaN(pauseMode)) {
            throw new TypeError("Parameter 'pauseMode' must be of type number, but got " + (typeof pauseMode));
        }
        return this.CallApi('PauseRunsetAfter?pauseMode=' + pauseMode);
    }

    /**
     * Resumes the paused runset where it was paused. Returns 'false' if this is not possible. Check for errors in this case. Reasons might be that the operation mode is different from 'paused' or the device is not ready or on error.
     *
     * @returns {Promise<boolean>} - true if the runset was resumed; otherwise, false.
     */
    async ResumeRunset() {
        return this.CallApi('ResumeRunset');
    }

    /**
     * Resets the status of the runset after it was interrupted. Returns 'false' if this is not possible because the runset is currently running or if there is no runset to reset. Running the reset method multiple times on the same runset is possible.
     *
     * @returns {Promise<boolean>} - true if the runset was reset; otherwise, false.
     */
    async ResetRunset() {
        return this.CallApi('ResetRunset');
    }

    /**
     * Aborts the currently running script immediately. That can either be a runset or a maintenance command. Returns 'false' if this is not possible because there is nothing running or the running script is already a cancel script (since the device runs a script to clean up the flow cell when interrupted in the middle of an injection).
     *
     * @returns {Promise<boolean>} - true if the script was aborted; otherwise, false.
     */
    async AbortScript() {
        return this.CallApi('AbortScript');
    }

    /**
     * Leaves the standby mode. Returns 'false' if the machine is not in standby mode when this API method is called.
     *
     * @returns {Promise<boolean>} - true if the machine left standby mode; otherwise, false.
     */
    async LeaveStandby() {
        return this.CallApi('LeaveStandby');
    }

    /**
     * Sets whether the device should go into standby mode after the runset has finished. Always return true.
     *
     * @param {boolean} goToStandby - Option whether the device should go into standby mode after the runset has finished.
     * @returns {Promise<boolean>} - Always true.
     */
    async SetStandbyAfterFinish(goToStandby) {
        if (typeof goToStandby !== 'boolean') {
            throw new TypeError("Parameter 'goToStandby' must be of type boolean, but got " + (typeof goToStandby));
        }
        return this.CallApi('SetStandbyAfterFinish?goToStandby=' + goToStandby);
    }

    /**
     * Gets whether the device should go into standby mode after the runset has finished.
     *
     * @returns {Promise<boolean>} - true if the device goes into standby mode after finishing the runset; otherwise, false.
     */
    async GetStandbyAfterFinish() {
        return this.CallApi('GetStandbyAfterFinish');
    }

    /**
     * Returns an array containing the names of all maintenance commands that are available and can be run without user interaction.
     *
     * @returns {Promise<string[]>} - Names of all available maintenance procedures.
     */
    async GetNamesOfMaintenanceProcedures() {
        return this.CallApi('GetNamesOfMaintenanceProcedures');
    }

    /**
     * Starts the maintenance procedure with the given name. Returns 'false' if this is not possible. Check for errors in this case. Reasons might be that the device is not ready or there is already something running.
     *
     * @param {string} procedureName - Name of the maintenance procedure to be run.
     * @returns {Promise<boolean>} - true if the maintenance procedure was started; otherwise, false.
     */
    async RunMaintenanceProcedure(procedureName) {
        if (typeof procedureName !== 'string') {
            throw new TypeError("Parameter 'procedureName' must be of type string, but got " + (typeof procedureName));
        }
        return this.CallApi('RunMaintenanceProcedure?procedureName=' + encodeURIComponent(procedureName));
    }

    /**
     * Gets the current operation mode of the device. The meaning of the return value can be found in the OperationMode documentation.
     *
     * @returns {Promise<number>} - Integer value representing the operation mode. See OperationMode for further details.
     */
    async GetOperationMode() {
        return this.CallApi('GetOperationMode');
    }

    /**
     * Checks whether a chip is docked. Returns '1' = docked, '0' = undocked, '-1' = unknown.
     *
     * @returns {Promise<number>} - 1 = docked, 0 = undocked, -1 = unknown.
     */
    async IsChipDocked() {
        return this.CallApi('IsChipDocked');
    }

    /**
     * Checks whether the sample plate tray is inside the machine. Returns '1' = inside, '0' = outside, '-1' = unknown. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     *
     * @returns {Promise<number>} - 1 = inside, 0 = outside, -1 = unknown.
     * @throws {NotSupportedException} Thrown if not MASS-1 platform.
     */
    async IsSamplePlateTrayIn() {
        return this.CallApi('IsSamplePlateTrayIn');
    }

    /**
     * Checks whether the reagent plate tray is inside the machine. Returns '1' = inside, '0' = outside, '-1' = unknown. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64.
     *
     * @returns {Promise<number>} - 1 = inside, 0 = outside, -1 = unknown.
     * @throws {NotSupportedException} Thrown if not MASS-1 platform.
     */
    async IsReagentPlateTrayIn() {
        return this.CallApi('IsReagentPlateTrayIn');
    }

    /**
     * Checks whether the side door is open. Returns '1' = open, '0' = closed, '-1' = unknown. This is only implemented for SPR #64 and will throw a NotSupportedException on MASS-1 platform.
     *
     * @returns {Promise<number>} - 1 = open, 0 = closed, -1 = unknown.
     * @throws {NotSupportedException} Thrown if not SPR #64.
     */
    async IsSideDoorOpen() {
        return this.CallApi('IsSideDoorOpen');
    }

    /**
     * Checks whether the server's message loop contains at least one new message.
     *
     * @returns {Promise<boolean>} - true if there is at least one message in the queue; otherwise, false.
     */
    async HasMessage() {
        return this.CallApi('HasMessage');
    }

    /**
     * Gets the first message from the server's message queue. A message consists of three strings in an array where the first string is a time stamp formatted like yyyyMMdd-hhmmss.fff, the second gives information about the type of message (Error, Warning, DataSaved, OperationModeChanged, …), and the third string is the message itself. Call this method on a regular base using polling and check the result for null or use the HasMessage method first. A complete list of error types can be found in Error Types.
     *
     * @returns {Promise<string[]>} - Array of three strings representing the message (timestamp, type, message), or null if no message is available.
     */
    async GetMessage() {
        return this.CallApi('GetMessage');
    }

    /**
     * Checks whether there are any errors that are not covered by the operation mode.
     *
     * @returns {Promise<boolean>} - true if there are errors not covered by the operation mode; otherwise, false.
     */
    async HasErrors() {
        return this.CallApi('HasErrors');
    }

    /**
     * Gets an array of strings with information about errors like 'no data', 'runset error', ...
     *
     * @returns {Promise<string[]>} - Array of current errors.
     */
    async GetErrors() {
        return this.CallApi('GetErrors');
    }

    /**
     * Checks whether there are any warnings that are not covered by the operation mode.
     *
     * @returns {Promise<boolean>} - true if there are warnings not covered by the operation mode; otherwise, false.
     */
    async HasWarnings() {
        return this.CallApi('HasWarnings');
    }

    /**
     * Gets an array of strings with information about warnings like 'temperature unstable', 'runset warning', ...
     *
     * @returns {Promise<string[]>} - Array of current warnings.
     */
    async GetWarnings() {
        return this.CallApi('GetWarnings');
    }
}

// Universal Module Definition / CommonJS / Browser global export
if (typeof module !== 'undefined' && module.exports) {
    module.exports = { BrukerSprRemoteAPI };
}
if (typeof window !== 'undefined') {
    window.BrukerSprRemoteAPI = BrukerSprRemoteAPI;
}
if (typeof globalThis !== 'undefined') {
    globalThis.BrukerSprRemoteAPI = BrukerSprRemoteAPI;
}
