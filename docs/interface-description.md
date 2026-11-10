# Interface Description

In order to keep the client implementation as flexible as possible, our server does not use callbacks. This means that the server is not capable of contacting the client framework actively if an error has occurred or the state of the software has changed. The client must ask in regular intervals instead (polling). Messages by the control software are queued in a thread-safe queue instead. The arguments and return types are restricted to very fundamental types like numbers, Boolean values, strings, and arrays.

## Methods

In Table 1 all available API methods are listed and described. Whenever "method" (or "methods") is part of the name of the API method or its description, not the or any API method is meant but the methods that can be created and run with the control software (i.e. are part of the runset of the control software). These methods may be also named procedures.

### Table 1: API methods

| Method Name | Return Type | Arguments | Description |
| :--- | :--- | :--- | :--- |
| **Get information about methods** | | | |
| GetNamesOfMethods | string[] | - | Returns an array of all method names that are loaded in the current runset library. |
| GetNamesOfMethodsOfAssayType | string[] | assayType (string) | Returns an array containing the names of all methods of the provided assay type that are loaded in the current runset library. The list of assay types can be obtained using the GetAssayTypesOfAllMethods method. |
| GetAssayTypesOfAllMethods | string[] | - | Returns an array containing the assay types of all methods that are loaded in the current runset library. |
| GetNameOfCurrentMethod | string | - | Returns the name of the method that is currently running or selected for running. Returns an empty string if there is no method selected. |
| GetAssayTypeOfCurrentMethod | string | - | Returns the assay type of the method that is currently running or selected for running. Returns an empty string if there is no method selected. |
| GetAssayTypeOfMethod | string | methodName (string) | Returns the assay type of the method with the given name. Returns an empty string if there is no method with the given name in the runset library. |
| **Get information about runsets** | | | |
| GetNamesOfRunsets | string[] | - | Returns an array of all runset names that are loaded in the current runset library. |
| GetNamesOfRunsetsOfAssayType | string[] | assayType (string) | Returns an array containing the names of all runsets of the provided assay type that are loaded in the current runset library. The list of assay types can be obtained using the GetAssayTypesOfAllRunsets method. |
| GetAssayTypesOfAllRunsets | string[] | - | Returns an array containing the assay types of all runsets that are loaded in the current runset library. |
| GetNameOfCurrentRunset | string | - | Returns the name of the runset that is currently running or selected for running. Returns an empty string if there is no runset selected. |
| GetAssayTypeOfCurrentRunset | string | - | Returns the assay type of the runset that is currently running or selected for running. Returns an empty string if there is no runset selected. |
| GetAssayTypeOfRunset | string | runsetName (string) | Returns the assay type of the runset with the given name. Returns an empty string if there is no runset with the given name in the runset library. |
| GetMethodNamesOfRunset | string[] | runsetName (string) | Returns the names of the methods in the runset with the given name. |
| **Prepare and run assays** | | | |
| SelectMethod | bool | methodName (string) | Selects the specified method for running. Returns 'false' if the method could not be selected. Check for error messages in that case. Possible reasons are that the operation mode doesn't allow to select a method or that the method with the provided name doesn't exist in the runset library. |
| SelectRunset | bool | runsetName (string) | Selects the specified runset for running. Returns 'false' if the runset could not be selected. Check for error messages in that case. Possible reasons are that the operation mode doesn't allow to select a runset or that the runset with the provided name doesn't exist in the runset library. |
| CreateRunset | bool | methodNames (string[]) | Creates a runset that contains the methods specified by the passed array of method names and selects the runset for running. Returns 'false' if the runset couldn't be created. Check for error messages in that case. Possible reasons are that the operation mode doesn't allow to select a runset or that one or more methods with the provided names do not exist in the runset library. |
| SetSamplePlateId | bool | methodIndex (int), plateId (string) | Assigns an ID to the exchangeable plate of the method with the specified index in the currently selected runset. Returns 'false' if the index of the method is out of range or if the method with the given index doesn't use a sample plate. Check for errors in this case. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64. |
| GetSamplePlateId | string | methodIndex (int) | Gets the ID of the exchangeable plate of the method with the given index in the current runset. Returns an empty string if the index is out of range, if there is no runset, no plate, or the plate doesn't have an ID assigned. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64. |
| GetCurrentSamplePlateId | string | - | Gets the ID of the sample plate that is currently inside the machine. Returns an empty string if there is no plate inside the machine or the ID is unknown. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64. |
| SetPlateId | bool | methodIndex (int), plateId (string), deckLocation (string) | Assigns an ID to the plate at the provided deck location of the method with the specified index in the currently selected runset. Returns 'false' if the method index is out of range or the method with the given index doesn't use a sample plate. Check for errors in this case. Throws an ArgumentException if the location does not exist on deck. |
| GetPlateId | string | methodIndex (int), deckLocation (string) | Gets the ID of the plate at the provided deck location of the method with the given index in the current runset. Returns an empty string if the index is out of range, if there is no runset, no plate, or the plate doesn't have an ID assigned. Throws an ArgumentException if the location does not exist on deck. |
| GetCurrentPlateId | string | deckLocation (string) | Gets the ID of the plate at the provided deck location of the method with the given index in the current runset. Returns an empty string if there is no plate or ID is unknown. Throws an ArgumentException if the location does not exist on deck. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64. |
| GetDeckLocations | string[] | - | Returns an array of all deck location names. |
| MoveSamplePlateTrayOut | bool | - | Moves the sample plate tray out of the machine so that the plate mover can get or put a plate from it. Returns 'false' if this is not possible. This might be the case if there is a runset or maintenance procedure currently running or if a reset is required. Check for errors in this case. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64. |
| MoveSamplePlateTrayIn | bool | - | Moves the sample plate tray into the housing. Returns 'false' if this is not possible. This might be the case if there is a runset or a maintenance procedure is currently running or if a reset is required. Check for errors in this case. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64. |
| MoveReagentPlateTrayOut | bool | - | Moves the reagent plate tray out of the device so the plate mover can get or put a plate from it. Returns 'false' if this is not possible. This might be the case if there is a runset or maintenance procedure currently running or if a reset is required. Check for errors in this case. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64. |
| MoveReagentPlateTrayIn | bool | - | Moves the reagent plate tray into the housing. Returns 'false' if this is not possible. This might be the case if there is a runset or maintenance procedure is currently running or if a reset is required. Check for errors in this case. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64. |
| OpenSideDoor | bool | - | Opens the side door so that a plate mover can exchange a plate on the plate deck. This is only implemented for SPR #64 and will throw a NotSupportedException on MASS-1 platform. |
| CloseSideDoor | bool | - | Closes the side door after a plate mover has exchanged a plate on the plate deck. This is only implemented for SPR #64 and will throw a NotSupportedException on MASS-1 platform. |
| StartSelectedRunset | bool | - | Starts the currently selected runset. Returns 'false' if this is not possible. Check for errors in this case. Possible reasons might be that the system is not ready, is on error, or there is no selected runset. |
| StartSelectedRunsetFrom | bool | methodIndex (int) | Starts the selected runset starting from the method with the given index. Returns 'false' if this is not possible. Check for errors in this case. Possible reasons might be that the system is not ready, is on error, the index of the method is out of range, or no runset is selected. |
| PauseRunsetAfter | bool | pauseMode (int) | Pauses the runset that is currently running. The pauseMode argument sets when the runset is paused (1 = after current command, 2 = after current cycle, 3 = after current method). Returns 'false' if this is not possible because there is currently no runset running. |
| ResumeRunset | bool | - | Resumes the paused runset where it was paused. Returns 'false' if this is not possible. Check for errors in this case. Reasons might be that the operation mode is different from 'paused' or the device is not ready or on error. |
| ResetRunset | bool | - | Resets the status of the runset after it was interrupted. Returns 'false' if this is not possible because the runset is currently running or if there is no runset to reset. Running the reset method multiple times on the same runset is possible. |
| AbortScript | bool | - | Aborts the currently running script immediately. That can either be a runset or a maintenance command. Returns 'false' if this is not possible because there is nothing running or the running script is already a cancel script (since the device runs a script to clean up the flow cell when interrupted in the middle of an injection). |
| LeaveStandby | bool | - | Leaves the standby mode. Returns 'false' if the machine is not in standby mode when this API method is called. |
| SetStandbyAfterFinish | bool | goToStandby (bool) | Sets whether the device should go into standby mode after the runset has finished. Always return true. |
| GetStandbyAfterFinish | bool | - | Gets whether the device should go into standby mode after the runset has finished. |
| **Maintenance** | | | |
| GetNamesOfMaintenanceProcedures | string[] | - | Returns an array containing the names of all maintenance commands that are available and can be run without user interaction. |
| RunMaintenanceProcedure | bool | procedureName (string) | Starts the maintenance procedure with the given name. Returns 'false' if this is not possible. Check for errors in this case. Reasons might be that the device is not ready or there is already something running. |
| **Status** | | | |
| GetOperationMode | int | - | Returns the current operation mode as an integer. The meaning of the values is described in Table 2. |
| IsChipDocked | bool | - | Checks whether a chip is docked. Returns '1' = docked, '0' = undocked, '-1' = unknown. |
| IsSamplePlateTrayIn | bool | - | Checks whether the sample plate tray is inside the machine. Returns '1' = inside, '0' = outside, '-1' = unknown. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64. |
| IsReagentPlateTrayIn | bool | - | Checks whether the reagent plate tray is inside the machine. Returns '1' = inside, '0' = outside, '-1' = unknown. This is only implemented for MASS-1 platform and will throw a NotSupportedException on SPR #64. |
| IsSideDoorOpen | bool | - | Checks whether the side door is open. Returns '1' = open, '0' = closed, '-1' = unknown. This is only implemented for SPR #64 and will throw a NotSupportedException on MASS-1 platform. |
| HasMessage | bool | - | Checks whether the server's message loop contains at least one new message. |
| GetMessage | string | - | Returns the oldest message from the message queue and removes it from the queue. Returns an empty string if the queue is empty. Format: [Time of message, local time]\t[Type of error]\t[Sender of message]\t[Message text]. The fields are separated by tabulator characters. If multiple messages are present, each message is ended with a newline character (\n) and the oldest message is returned first. A complete list of error types can be found in Table 3. |
| HasErrors | bool | - | Checks whether there are any errors that are not covered by the operation mode. |
| GetErrors | string[] | - | Gets an array of strings with information about errors like 'no data', 'runset error', ... |
| HasWarnings | bool | - | Checks whether there are any warnings that are not covered by the operation mode. |
| GetWarnings | string[] | - | Gets an array of strings with information about warnings like 'temperature unstable', 'runset warning', ... |

## Operation Modes

### Table 2: Operation modes

| Numeric Value | Meaning | Description |
| :--- | :--- | :--- |
| 0 | Idle | The device is ready to run a script which can be a runset or a maintenance procedure. |
| 1 | Paused | The device is idle but there is a runset selected that was paused during run and might be resumed. The ResetRunset API method has to be called before a new runset can be started. Running a maintenance command in between is possible, however. |
| 2 | Completed | The device is idle and the selected runset has completed. The ResetRunset API method must be called before a new runset can be started. |
| 10 | StandBy | The device is currently in standby mode. You have to leave standby to get into the idle, paused, or completed mode. |
| 11 | ResetRequired | This state occurs after a stepper motor had an error or if a script was aborted due to an error or by user interaction. The maintenance method 'Reset System' has to be run with the API method RunMaintenanceProcedure to get into idle, paused, or completed mode. |
| 12 | MaintenanceRequired | The device is not ready for operation but needs technical maintenance. Currently not used. |
| 13 | DoorOpen | The front door of the device is open. It must be closed before the machine can be used. |
| 20 | Running | There is currently a script running. This can be either a runset or a maintenance method. |

## Error Types

### Table 3: Error types

| Type | Description |
| :--- | :--- |
| Error | A critical error has occurred, current operation cannot proceed |
| Warning | An uncritical error occurred |
| StateChanged | The operation mode has changed (see Table 2) |
| DataCleared | The recorded data were cleared from the application's memory [Not yet implemented in test version 3.5.0.6] |
| DataSaved | The recorded data were saved to file [Not yet implemented in test version 3.5.0.6] |
| RunsetStarted | A runset was started [Not yet implemented in test version 3.5.0.6] |
| RunsetCompleted | A runset has completed [Not yet implemented in test version 3.5.0.6] |
| MethodStarted | A method was started [Not yet implemented in test version 3.5.0.6] |
| MethodCompleted | A method has completed [Not yet implemented in test version 3.5.0.6] |
| CycleStarted | A cycle was started [Not yet implemented in test version 3.5.0.6] |
| CycleCompleted | A cycle has completed [Not yet implemented in test version 3.5.0.6] |
| InjectionStarted | An injection was started [Not yet implemented in test version 3.5.0.6] |
| InjectionCompleted | An injection has completed [Not yet implemented in test version 3.5.0.6] |
| AppMessage | Other message from script [Not yet implemented in test version 3.5.0.6] |
