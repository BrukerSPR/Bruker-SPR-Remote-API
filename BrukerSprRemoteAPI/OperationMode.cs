namespace BrukerSprRemoteAPI
{
    /// <summary>
    /// <![CDATA[
    /// ## OperationMode
    ///
    /// These modes describe the state of the device.
    /// ]]>
    /// </summary>
    public enum OperationMode
    {
        /// <summary>
        /// The device is ready to run a script which can be a runset or a maintenance procedure.
        /// </summary>
        Idle = 0,

        /// <summary>
        /// The device is idle but there is a runset selected that was paused during run and might be resumed.
        /// The <see cref="IBrukerSprRemoteService.ResetRunset"/> API method has to be called before a new runset can be started.
        /// Running a maintenance command in between is possible, however.
        /// </summary>
        Paused = 1,

        /// <summary>
        /// The device is idle and the selected runset has completed.
        /// The <see cref="IBrukerSprRemoteService.ResetRunset"/> API method must be called before a new runset can be started.
        /// </summary>
        Completed = 2,

        /// <summary>
        /// The device is currently in standby mode.
        /// You have to leave standby to get into the idle, paused, or completed mode.
        /// </summary>
        StandBy = 10,

        /// <summary>
        /// This state occurs after a stepper motor had an error or if a script was aborted
        /// due to an error or by user interaction.
        /// The maintenance method 'Reset System' has to be run with the API method
        /// <see cref="IBrukerSprRemoteService.RunMaintenanceProcedure(string)"/> to get into
        /// <see cref="Idle"/>, <see cref="Paused"/>, <see cref="Completed"/> mode.
        /// </summary>
        ResetRequired = 11,

        /// <summary>
        /// The device is not ready for operation but needs technical maintenance.
        /// Currently not used.
        /// </summary>
        MaintenanceRequired = 12,

        /// <summary>
        /// The front door of the device is open. It must be closed before the machine can be used.
        /// </summary>
        DoorOpen = 13,

        /// <summary>
        /// There is currently a script running. This can be either a runset or a maintenance method.
        /// </summary>
        Running = 20,
    }
}
