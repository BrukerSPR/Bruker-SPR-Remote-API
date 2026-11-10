namespace BrukerSprRemoteAPI
{
    /// <summary>
    /// <![CDATA[
    /// ## Error Types
    /// ]]>
    /// These are the errors returned by <see cref="IBrukerSprRemoteService.GetMessage"/>.
    /// </summary>
    public static class ErrorTypes
    {
        /// <summary>
        /// A critical error has occurred and current operation cannot proceed.
        /// </summary>
        public const string Error = "Error";

        /// <summary>
        /// An uncritical error occurred.
        /// </summary>
        public const string Warning = "Warning";

        /// <summary>
        /// The operation mode has changed (see <see cref="OperationMode"/>).
        /// </summary>
        public const string StateChanged = "StateChanged";

        /// <summary>
        /// The recorded data were cleared from the application's memory [Not yet implemented in test version 3.5.0.6].
        /// </summary>
        public const string DataCleared = "DataCleared";

        /// <summary>
        /// The recorded data were saved to file [Not yet implemented in test version 3.5.0.6].
        /// </summary>
        public const string DataSaved = "DataSaved";

        /// <summary>
        /// A runset was started [Not yet implemented in test version 3.5.0.6].
        /// </summary>
        public const string RunsetStarted = "RunsetStarted";

        /// <summary>
        /// A runset has completed [Not yet implemented in test version 3.5.0.6].
        /// </summary>
        public const string RunsetCompleted = "RunsetCompleted";

        /// <summary>
        /// A method was started [Not yet implemented in test version 3.5.0.6].
        /// </summary>
        public const string MethodStarted = "MethodStarted";

        /// <summary>
        /// A method has completed [Not yet implemented in test version 3.5.0.6].
        /// </summary>
        public const string MethodCompleted = "MethodCompleted";

        /// <summary>
        /// A cycle was started [Not yet implemented in test version 3.5.0.6].
        /// </summary>
        public const string CycleStarted = "CycleStarted";

        /// <summary>
        /// A cycle has completed [Not yet implemented in test version 3.5.0.6].
        /// </summary>
        public const string CycleCompleted = "CycleCompleted";

        /// <summary>
        /// An injection was started [Not yet implemented in test version 3.5.0.6].
        /// </summary>
        public const string InjectionStarted = "InjectionStarted";

        /// <summary>
        /// An injection has completed [Not yet implemented in test version 3.5.0.6].
        /// </summary>
        public const string InjectionCompleted = "InjectionCompleted";

        /// <summary>
        /// Other message from script [Not yet implemented in test version 3.5.0.6].
        /// </summary>
        public const string AppMessage = "AppMessage";
    }
}
