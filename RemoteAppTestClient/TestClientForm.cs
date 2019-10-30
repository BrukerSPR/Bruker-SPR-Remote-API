using System;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteAppTestClient
{
    public partial class TestClientForm : Form
    {

        private MASS1RemoteServiceClient _client;

        //MEthods and Runsets from the applicaiton
        string[] _methodTypes = new string[] { };
        string[] _runsetTypes = new string[] { };
        string[] _methodNames = new string[] { };
        string[] _runsetNames = new string[] { };
        string[] _maintenanceCommands = new string[] { };

        /// <summary>
        /// Constructor
        /// </summary>
        public TestClientForm()
        {
            InitializeComponent();

            tabControl.Enabled = false;
        }


        #region Connection to Server

        /// <summary>
        /// Connect the client to the Server
        /// </summary>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                _client = new MASS1RemoteServiceClient();

                tabControl.Enabled = true;

                GetMethodsandRunsets();
                SetupTestClient();

                _cts = new CancellationTokenSource();

                StartExecution();

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;

                StatusCheck();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connect Exception: " + ex.ToString());
            }
        }

        /// <summary>
        /// Disconnect the client from the server
        /// </summary>
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                CancelExecution();

                tabControl.Enabled = false;

                _client.ChannelFactory.Close(); // forgetting this line will cause a System.Net.Sockets.SocketException
                _client.Close();

                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Disconnect Exception: " + ex.ToString());
            }
        }

        /// <summary>
        /// Gets the names of the methods and runsets
        /// </summary>
        public void GetMethodsandRunsets()
        {
            _methodTypes = _client.GetAssayTypesOfAllMethods();
            _runsetTypes = _client.GetAssayTypesOfAllRunsets();
            _methodNames = _client.GetNamesOfMethods();
            _runsetNames = _client.GetNamesOfRunsets();
            _maintenanceCommands = _client.GetNamesOfMaintenanceProcedures();
        }

        /// <summary>
        /// After the client is connected we setup the test client with the list of methods and runsets from the server
        /// </summary>
        public void SetupTestClient()
        {
            comboBoxTypes.SelectedIndex = -1;
            comboBoxTypes.Items.Clear();

            foreach (string type in _methodTypes.Union(_runsetTypes).Distinct())
                comboBoxTypes.Items.Add(type);

            comboBoxMethods.SelectedIndex = -1;
            comboBoxMethods.Items.Clear();

            // Set method names in the comboxbox list of option 
            foreach (string methodName in _methodNames)
                comboBoxMethods.Items.Add(methodName);

            comboBoxRunsets.SelectedIndex = -1;
            comboBoxRunsets.Items.Clear();

            // Set runset names in the comboxbox list of option 
            foreach (string runsetName in _runsetNames)
                comboBoxRunsets.Items.Add(runsetName);

            comboBoxMaintenance.SelectedIndex = -1;
            comboBoxMaintenance.Items.Clear();

            // Set manintenance commands names in the comboxbox list of option 
            foreach (string mainCmd in _maintenanceCommands)
                comboBoxMaintenance.Items.Add(mainCmd);

            selectedRunsetListBox.Items.Clear();

            textBoxRunsets.Text = string.Empty;
            textBoxRunsetType.Text = string.Empty;
            textBoxMethods.Text = string.Empty;
            textBoxMethodType.Text = string.Empty;

            textBoxSetSamplePlateID.Text = string.Empty;
            textBoxSetSamplePlateID.Enabled = false;

            buttonSetSamplePlateID.Visible = buttonSetSamplePlateID.Enabled = false;

            SetViewState();
        }

        /// <summary>
        /// Sets the state of the view
        /// </summary>
        private void SetViewState()
        {
            comboBoxMaintenance.Enabled = checkBoxrunMaintenanceCmd.Checked;

            comboBoxTypes.Enabled = !checkBoxrunMaintenanceCmd.Checked;
            comboBoxMethods.Enabled = !checkBoxrunMaintenanceCmd.Checked;
            comboBoxRunsets.Enabled = !checkBoxrunMaintenanceCmd.Checked;
            selectedRunsetListBox.Enabled = !checkBoxrunMaintenanceCmd.Checked;

            comboBoxTypes.Invalidate();
            comboBoxMethods.Invalidate();
            comboBoxRunsets.Invalidate();
        }

        #endregion


        #region Polling

        private CancellationTokenSource _cts;

        public void StartExecution()
        {
            Task.Factory.StartNew(OwnCodeCancelableTask_EveryNSeconds, _cts.Token);
        }

        public void CancelExecution()
        {
            _cts.Cancel();
        }

        /// <summary>
        /// "Infinite" loop that runs every N seconds. Good for checking for a heartbeat or updates.
        /// </summary>
        /// <param name="taskState">The cancellation token from our _cts field, passed in the StartNew call</param>
        private void OwnCodeCancelableTask_EveryNSeconds(object taskState)
        {
            var token = (CancellationToken)taskState;

            while (!token.IsCancellationRequested)
            {
                if(checkboxPolling.Checked)
                    StatusCheck();

                Thread.Sleep(2000);
            }
        }

        #endregion


        #region Methods - Get Information About Methods or Runsets

        /// <summary>
        /// Updates the selected runset information
        /// </summary>
        private void UpdateInfoSelectedRunset()
        {
            selectedRunsetListBox.Items.Clear();
            selectedRunsetListBox.Items.AddRange(_client.GetMethodNamesOfRunset(_client.GetNameOfCurrentRunset()));

            if (selectedRunsetListBox.Items.Count > 0)
                selectedRunsetListBox.SelectedItem = selectedRunsetListBox.Items[0];

            comboBoxTypes.Enabled = false;
            comboBoxMethods.Enabled = false;
            comboBoxRunsets.Enabled = false;

            if (InvokeRequired)
                Invoke(new Action<string, string, string, string, string>(SetSeletedItems), new object[] { _client.GetNameOfCurrentRunset(), _client.GetAssayTypeOfRunset(_client.GetNameOfCurrentRunset()),
                _client.GetNameOfCurrentMethod(),_client.GetAssayTypeOfMethod(_client.GetNameOfCurrentMethod()), _client.GetSamplePlateId(selectedRunsetListBox.SelectedIndex)  });
        }

        /// <summary>
        /// Updates the selected method information when ran as an individual method
        /// </summary>
        private void UpdateInfoSelectedMethod()
        {
            selectedRunsetListBox.Items.Clear();
            selectedRunsetListBox.Items.Add(_client.GetNameOfCurrentMethod());

            if (selectedRunsetListBox.Items.Count > 0)
                selectedRunsetListBox.SelectedItem = selectedRunsetListBox.Items[0];

            comboBoxTypes.Enabled = false;
            comboBoxMethods.Enabled = false;
            comboBoxRunsets.Enabled = false;

            if (InvokeRequired)
                Invoke(new Action<string, string, string, string, string>(SetSeletedItems), new object[] { "", "", _client.GetNameOfCurrentMethod(), _client.GetAssayTypeOfMethod(_client.GetNameOfCurrentMethod()), _client.GetSamplePlateId(0) });
        }


        /// <summary>
        /// Sets the selected item info
        /// </summary>
        private void SetSeletedItems(string sr, string srt, string sm, string smt, string spid)
        {
            textBoxRunsets.Text = sr;

            textBoxRunsetType.Text = srt;

            textBoxMethods.Text = sm;

            textBoxMethodType.Text = smt;

            textBoxSetSamplePlateID.Text = spid;
        }

        /// <summary>
        /// Set the sample plate id on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetSamplePlateID_Click(object sender, EventArgs e)
        {
            buttonSetSamplePlateID.Enabled = false;
            buttonSetSamplePlateID.Visible = false;

            bool  setplateID = _client.SetSamplePlateId(selectedRunsetListBox.SelectedIndex, textBoxSetSamplePlateID.Text);

            if (!setplateID)
            {
                if (selectedRunsetListBox.SelectedIndex > _client.GetMethodNamesOfRunset(_client.GetNameOfCurrentRunset()).Count() - 1)
                    DispalyWarningMessage("Selected method index is out of range! Check selected method again!", !setplateID);
                else
                    DispalyWarningMessage("Selected method doesnot contain the sample plate. Check for Errors!", !setplateID);
            }
            else
                DispalyWarningMessage(string.Empty, !setplateID);
        }

        /// <summary>
        /// Enable edit sample plate Id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSetSamplePlateID_TextChanged(object sender, EventArgs e)
        {
            buttonSetSamplePlateID.Visible = true;
            buttonSetSamplePlateID.Enabled = true;
        }

        /// <summary>
        /// Update the Info
        /// </summary>
        private void selectedRunsetListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxSetSamplePlateID.Enabled = true;

            if (InvokeRequired)
                Invoke(new Action<string, string, string, string, string>(SetSeletedItems), new object[] { _client.GetNameOfCurrentRunset(), _client.GetAssayTypeOfRunset(_client.GetNameOfCurrentRunset()) ,
                selectedRunsetListBox.Items.ToString(),_client.GetAssayTypeOfMethod(selectedRunsetListBox.Items.ToString()), _client.GetSamplePlateId(selectedRunsetListBox.SelectedIndex) });
            else
                SetSeletedItems(_client.GetNameOfCurrentRunset(), _client.GetAssayTypeOfRunset(_client.GetNameOfCurrentRunset()),
                _client.GetNameOfCurrentMethod(), _client.GetAssayTypeOfMethod(selectedRunsetListBox.Items.ToString()), _client.GetSamplePlateId(selectedRunsetListBox.SelectedIndex));
        }

        #endregion


        #region Methods 


        /// <summary>
        /// offers the option to filter methods and runsets by type
        /// </summary>
        private void comboBoxTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTypes.SelectedIndex > -1)
            {
                comboBoxMethods.Items.Clear();
                comboBoxRunsets.Items.Clear();

                // Set method names in the comboxbox list of option 
                foreach (string methodName in _client.GetNamesOfMethodsOfAssayType(comboBoxTypes.SelectedItem.ToString()))
                    comboBoxMethods.Items.Add(methodName);


                // Set runset names in the comboxbox list of option 
                foreach (string runsetName in _client.GetNamesOfRunsetsOfAssayType(comboBoxTypes.SelectedItem.ToString()))
                    comboBoxRunsets.Items.Add(runsetName);

                comboBoxMethods.Enabled = (comboBoxMethods.Items.Count != 0);
                comboBoxRunsets.Enabled = (comboBoxRunsets.Items.Count != 0);
            }
        }

        /// <summary>
        /// Sets the selected method and creats a runset using the selected method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMethods.SelectedIndex > -1)
            {

                bool selectMethod = _client.SelectMethod(comboBoxMethods.SelectedItem.ToString());

                if (!selectMethod)
                    DispalyWarningMessage("Can not create runset. Check Operation Mode!", !selectMethod);
                else
                    DispalyWarningMessage(string.Empty, !selectMethod);

                //string[] selectedMethod = new string[1] { comboBoxMethods.SelectedItem.ToString() };
                //bool createRunsetStatus = _client.CreateRunset(selectedMethod);

                //if (!createRunsetStatus)
                //{
                //    if(!_methodNames.Contains(comboBoxMethods.SelectedItem.ToString()))
                //        DispalyWarningMessage("Can not create runset. Selected method does not exist in library", !createRunsetStatus);
                //    else
                //        DispalyWarningMessage("Can not create runset. Check Operation Mode!", !createRunsetStatus);
                //}
                //else
                //    DispalyWarningMessage(string.Empty, !createRunsetStatus);

                UpdateInfoSelectedMethod();
            }
        }

        private void comboBoxRunsets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRunsets.SelectedIndex > -1)
            {
                bool selectedRunsetStatus = _client.SelectRunset(comboBoxRunsets.SelectedItem.ToString());

                if (!selectedRunsetStatus)
                {
                    if (!_runsetNames.Contains(comboBoxMethods.SelectedItem.ToString()))
                        DispalyWarningMessage("Can not find selected runset in the list. Check the name of the runset in the provided list!", !selectedRunsetStatus);
                    else
                        DispalyWarningMessage("Can not select runset. Check for error messages!", !selectedRunsetStatus);
                }
                else
                    DispalyWarningMessage(string.Empty, !selectedRunsetStatus);

                UpdateInfoSelectedRunset();
            }
        }

        private void btnTypesClear_Click(object sender, EventArgs e)
        {
            if (comboBoxTypes.SelectedIndex > -1)
            {
                comboBoxTypes.SelectedIndex = -1;

                SetupTestClient();
            }
        }

        private void checkBoxrunMaintenanceCmd_CheckedChanged(object sender, EventArgs e)
        {

            comboBoxMaintenance.Enabled = checkBoxrunMaintenanceCmd.Checked;

            comboBoxTypes.Enabled = !checkBoxrunMaintenanceCmd.Checked;
            comboBoxMethods.Enabled = !checkBoxrunMaintenanceCmd.Checked;
            comboBoxRunsets.Enabled = !checkBoxrunMaintenanceCmd.Checked;
            selectedRunsetListBox.Enabled = !checkBoxrunMaintenanceCmd.Checked;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            int operationmode = _client.GetOperationMode();

            if (operationmode == 1)
            {
                bool startStatus = _client.ResumeRunset();

                if (!startStatus)
                    DispalyWarningMessage("Can not resume runset. Check for errors!.", !startStatus);
                else
                    DispalyWarningMessage(string.Empty, !startStatus);
            }
            else
            {

                bool startStatus = false;

                if (checkBoxrunMaintenanceCmd.Checked)
                {
                    if (comboBoxMaintenance.SelectedIndex >= 0)
                        startStatus = _client.RunMaintenanceProcedure(_client.GetNamesOfMaintenanceProcedures()[comboBoxMaintenance.SelectedIndex]);
                }
                else
                {
                    if (selectedRunsetListBox.SelectedIndex > 0)
                        _client.StartSelectedRunsetFrom(selectedRunsetListBox.SelectedIndex);
                    else
                        startStatus = _client.StartSelectedRunset();
                }

                if (!startStatus)
                {
                    if (operationmode == 11)
                        DispalyWarningMessage("'Reset Required' from the mainentance commnands!", !startStatus);
                    else if (_client.GetNameOfCurrentRunset() != string.Empty)
                        DispalyWarningMessage("No selected Runset to Run.", !startStatus);
                    else
                        DispalyWarningMessage("Can not start 'Runset' check for errors.", !startStatus);
                }
                else
                    DispalyWarningMessage(string.Empty, !startStatus);

                //Start Selected runset 
                _client.StartSelectedRunset();
            }
        }

        /// <summary>
        /// Stops and aborts the current running script
        /// </summary>
        private void btnStop_Click(object sender, EventArgs e)
        {
            bool abortStatus = _client.AbortScript();

            if (!abortStatus)
                DispalyWarningMessage("Can not stop script as currently no script is running.", !abortStatus);
            else
                DispalyWarningMessage(string.Empty, !abortStatus);
        }

        /// <summary>
        /// Pasues the runset based on the selected option (1: after current command, 2: after current cycle, 3: after current method)
        /// </summary>
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (comboBoxPauseAfter.SelectedIndex >= 0)
            {
                bool pauseStatus = _client.PauseRunsetAfter(comboBoxPauseAfter.SelectedIndex);

                if (!pauseStatus)
                    DispalyWarningMessage("Can not pause the script as currently no script is running.", !pauseStatus);
                else
                    DispalyWarningMessage(string.Empty, !pauseStatus);
            }
            else
            {
                DispalyWarningMessage("Select an option for pause (1: after current command, 2: after current cycle, 3: after current method) in the dropdown box.", true);
            }
        }

        /// <summary>
        /// Reset the Runset
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            bool resetStatus = _client.ResetRunset();

            if (!resetStatus)
            {
                if (_client.GetOperationMode() == 20)
                    DispalyWarningMessage("Can not 'Reset Runset', currently the runset is running.", !resetStatus);
                else
                    DispalyWarningMessage("No 'Runset' is selected to reset.", !resetStatus);
            }
            else
                DispalyWarningMessage(string.Empty, !resetStatus);

            // Reset the client 
            SetupTestClient();
        }

        #endregion


        #region Status

        /// <summary>
        /// Checks the status of the Machine 
        /// </summary>
        private void StatusCheck()
        {
            UpdateOperationMode();

            UpdateChipStatus();

            UpdateSamplePlateStatus();

            UpdateSamplePlateID();

            if (_client.HasErrors())
                UpdateErrors();

            while (_client.HasMessage())
            {
                string[] message = _client.GetMessage();
                logGridView.Rows.Add(message[0], message[1], message[2]);
            }
        }

        /// <summary>
        /// Updates the Operation Mode
        /// </summary>
        private void UpdateOperationMode()
        {
            string mode = string.Empty;

            switch (_client.GetOperationMode())
            {
                // Idle: The device is ready to run a script which can be a runset or a maintenance procedure
                case 0:
                    mode = "Idle";
                    break;

                // Paused: 	The device is idle but there is a runset selected that was paused during run and might be resumed. The ResetRunset method has to be called before a new runset can be started. Runing a maintenance command in between is possible, however.
                case 1:
                    mode = "Paused";
                    break;

                // Completed: The device is idle and the selected runset has completed. The ResetRunset method has to be called before a new runset can be started.
                case 2:
                    mode = "Completed";
                    break;

                // StandBy: The device is currently in standby mode. You have to leave standby to get into the idle, paused, or completed mode.
                case 10:
                    mode = "StandBy";
                    break;

                // ResetRequired: This state occurs after a stepper motor had an error or if a script was aborted due to an error or by user interaction. The maintenance procedure 'Reset System' has to be run to get into the idle, paused, or completed mode.
                case 11:
                    mode = "Reset Required";
                    break;

                // MaintenanceRequired: The device is not ready for operation but needs technical maintenance. Not used at the moment.
                case 12:
                    mode = "Maintenance Required";
                    break;

                // DoorOpen: The front door of the device was opened. It has to be closed before the machine can be used.
                case 13:
                    mode = "Door Open";
                    break;

                // Running: There is currently a script running. This can be either a runset or a maintencane procedure.
                case 20:
                    mode = "Running";
                    break;
            }

            if (InvokeRequired)
                Invoke(new Action<string>(ChangeOperationTextBox), new object[] { mode });
            else
                ChangeOperationTextBox(mode);
        }

        /// <summary>
        /// Operation Mode text changed
        /// </summary>
        private void ChangeOperationTextBox(string obj)
        {
            operationTextBox.Text = obj;
        }


        /// <summary>
        /// Updates the error and messages
        /// </summary>
        private void UpdateErrors()
        {
            lblError.Visible = true;
            ErrorTextBox.Visible = true;

            while (_client.HasErrors())
            {

                if (InvokeRequired)
                    Invoke(new Action<string>(ChangeChipStatusTextBox), new object[] { _client.GetErrors().First() });
                else
                    ChangeErrorTextBox(_client.GetErrors().First());
            }
        }

        /// <summary>
        /// Error Box Text Changes
        /// </summary>
        private void ChangeErrorTextBox(string obj)
        {
            ErrorTextBox.Text = obj;
        }


        /// <summary>
        /// Updates the status of the chip
        /// </summary>
        private void UpdateChipStatus()
        {
            string status = string.Empty;

            switch(_client.IsChipDocked())
            {
                // state unknown
                case -1:
                    status = "Unknown";
                    break;

                // state docked
                case 0:
                    status = "Docked";
                    break;
                
                // state undocked
                case 1:
                    status = "Undocked";
                    break;
            }

            if (InvokeRequired)
                Invoke(new Action<string>(ChangeChipStatusTextBox), new object[] { status });
            else
                ChangeChipStatusTextBox(status);
        }

        /// <summary>
        /// Chip Status box Text changed
        /// </summary>
        private void ChangeChipStatusTextBox(string obj)
        {
            chipStatusTextBox.Text = obj;
        }

        /// <summary>
        /// Updates the status of the sample plate
        /// </summary>
        private void UpdateSamplePlateStatus()
        {
            string status = string.Empty;

            switch (_client.IsSamplePlateTrayIn())
            {
                // state unknown
                case -1:
                    status = "Unknown";
                    break;

                // state Outside
                case 0:
                    status = "Outside";
                    break;

                // state Inside
                case 1:
                    status = "Inside";
                    break;
            }

            if (InvokeRequired)
                Invoke(new Action<string>(SamplePlateStatusTextBox), new object[] { status });
            else
                SamplePlateStatusTextBox(status);
        }

        /// <summary>
        /// Sample Plate Box Text Changed
        /// </summary>
        private void SamplePlateStatusTextBox(string obj)
        {
            samplePlateStatTextBox.Text = obj;
        }


        /// <summary>
        /// Updates the ID of the sample plate
        /// </summary>
        private void UpdateSamplePlateID()
        {
            if (_client.GetNameOfCurrentRunset() != string.Empty)
            {
                if (InvokeRequired)
                    Invoke(new Action<string>(CurrentSamplePlateIDTextBox), new object[] { _client.GetCurrentSamplePlateId() });
                else
                    CurrentSamplePlateIDTextBox(_client.GetCurrentSamplePlateId());
            }
        }

        /// <summary>
        /// currentsampleplate id updated in textbox
        /// </summary>
        /// <param name="obj"></param>
        private void CurrentSamplePlateIDTextBox(string obj)
        {
            samplePlateIDTextBox.Text = obj;
        }


        private void checkboxPolling_CheckedChanged(object sender, EventArgs e)
        {
            btnUpdateOperationMode.Enabled = btnUpdateOperationMode.Visible = !checkboxPolling.Checked;
            btnUpdateSampePlateStatus.Enabled = btnUpdateSampePlateStatus.Visible = !checkboxPolling.Checked;
            btnUpdateChipStatus.Enabled = btnUpdateChipStatus.Visible = !checkboxPolling.Checked;
            btnUpdateCurrentSamplePlateID.Enabled = btnUpdateCurrentSamplePlateID.Visible = !checkboxPolling.Checked;
            btnUpdateError.Enabled = btnUpdateError.Visible = !checkboxPolling.Checked;
        }

        /// <summary>
        /// Display local Warning
        /// </summary>
        private void DispalyWarningMessage(string obj, bool state)
        {
            labelLocalWarningTitle.Visible = state;
            labelLocalWarrning.Visible = state;
            labelLocalWarrning.Text = obj;
        }

        private void btnUpdateOperationMode_Click(object sender, EventArgs e)
        {
            UpdateOperationMode();
        }

        private void btnUpdateChipStatus_Click(object sender, EventArgs e)
        {
            UpdateChipStatus();
        }

        private void btnUpdateSampePlateStatus_Click(object sender, EventArgs e)
        {
            UpdateSamplePlateStatus();
        }

        private void btnUpdateCurrentSamplePlateID_Click(object sender, EventArgs e)
        {
            UpdateSamplePlateID();
        }

        private void btnUpdateError_Click(object sender, EventArgs e)
        {
            UpdateErrors();
        }
        #endregion
    }
}
