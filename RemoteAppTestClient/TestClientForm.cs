using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteAppTestClient
{
    public partial class TestClientForm : Form
    {
        private readonly SystemType _systemType;
        private MASS1RemoteServiceClient _client;

        //Methods and Runsets from the application
        string[] _methodTypes = new string[] { };
        string[] _runsetTypes = new string[] { };
        string[] _methodNames = new string[] { };
        string[] _runsetNames = new string[] { };
        string[] _maintenanceCommands = new string[] { };

        /// <summary>
        /// Constructor
        /// </summary>
        public TestClientForm(SystemType systemType)
        {
            InitializeComponent();
            _systemType = systemType;

            tabControl.Enabled = false;

            if (systemType == SystemType.MASS1)
            {
                btnPlateOutOrDoorOpen.Text = "Plate Out";
                btnPlateInOrDoorClose.Text = "Plate In";
                lblTrayOrDoorStatus.Text = "Plate Tray Status";
            }
            else
            {
                btnPlateOutOrDoorOpen.Text = "Open Door";
                btnPlateInOrDoorClose.Text = "Close Door";
                lblTrayOrDoorStatus.Text = "Side Door Status";
            }
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

                GetDeckLocationNames();
                GetMethodsAndRunsets();
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

        private void GetDeckLocationNames()
        {
            comboBoxDeckLocations.Items.Clear();
            comboBoxDeckLocations.Items.AddRange(_client.GetDeckLocations());
            comboBoxDeckLocations.SelectedIndex = 0;
        }

        private void GetMethodsAndRunsets()
        {
            _methodTypes = _client.GetAssayTypesOfAllMethods();
            _runsetTypes = _client.GetAssayTypesOfAllRunsets();
            _methodNames = _client.GetNamesOfMethods();
            _runsetNames = _client.GetNamesOfRunsets();
            _maintenanceCommands = _client.GetNamesOfMaintenanceProcedures();
        }

        private void SetupTestClient()
        {
            comboBoxTypes.SelectedIndex = -1;
            comboBoxTypes.Items.Clear();

            foreach (string type in _methodTypes.Union(_runsetTypes).Distinct())
                comboBoxTypes.Items.Add(type);

            comboBoxMethods.SelectedIndex = -1;
            comboBoxMethods.Items.Clear();

            // Set method names in the combo box list of option 
            foreach (string methodName in _methodNames)
                comboBoxMethods.Items.Add(methodName);

            comboBoxRunsets.SelectedIndex = -1;
            comboBoxRunsets.Items.Clear();

            // Set runset names in the combo box list of option 
            foreach (string runsetName in _runsetNames)
                comboBoxRunsets.Items.Add(runsetName);

            comboBoxMaintenance.SelectedIndex = -1;
            comboBoxMaintenance.Items.Clear();

            // Set maintenance commands names in the combo box list of option 
            foreach (string mainCmd in _maintenanceCommands)
                comboBoxMaintenance.Items.Add(mainCmd);

            selectedRunsetListBox.Items.Clear();

            textBoxRunsets.Text = string.Empty;
            textBoxRunsetType.Text = string.Empty;
            textBoxMethods.Text = string.Empty;
            textBoxMethodType.Text = string.Empty;

            textBoxSetSamplePlateID.Text = string.Empty;
            textBoxSetSamplePlateID.Enabled = false;

            buttonPlateID.Visible = buttonPlateID.Enabled = false;

            SetViewState();
        }

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

        private void StartExecution()
        {
            Task.Factory.StartNew(OwnCodeCancelableTask_EveryNSeconds, _cts.Token);
        }

        private void CancelExecution()
        {
            _cts.Cancel();
        }

        private void OwnCodeCancelableTask_EveryNSeconds(object taskState)
        {
            var token = (CancellationToken)taskState;

            while (!token.IsCancellationRequested)
            {
                if (checkboxPolling.Checked)
                    StatusCheck();

                Thread.Sleep(2000);
            }
        }

        #endregion

        #region Methods - Get Information About Methods or Runsets

        private void UpdateInfoSelectedRunset()
        {
            selectedRunsetListBox.Items.Clear();

            string currentRunsetName = _client.GetNameOfCurrentRunset();
            string currentMethodName = _client.GetNameOfCurrentMethod();

            selectedRunsetListBox.Items.AddRange(_client.GetMethodNamesOfRunset(currentRunsetName));

            if (selectedRunsetListBox.Items.Count > 0)
                selectedRunsetListBox.SelectedItem = selectedRunsetListBox.Items[0];

            comboBoxTypes.Enabled = false;
            comboBoxMethods.Enabled = false;
            comboBoxRunsets.Enabled = false;

            if (InvokeRequired)
            {
                if (_systemType == SystemType.MASS1)
                {
                    Invoke(new Action<string, string, string, string, string>(SetSelectedItems), new object[] { currentRunsetName, _client.GetAssayTypeOfRunset(currentRunsetName),
                        currentMethodName, _client.GetAssayTypeOfMethod(currentMethodName), _client.GetSamplePlateId(selectedRunsetListBox.SelectedIndex) });
                }
                else if (_systemType == SystemType.SPR64)
                {
                    string deckLocation = string.IsNullOrWhiteSpace(comboBoxDeckLocations.Text) ? "P4" : comboBoxDeckLocations.Text;

                    Invoke(new Action<string, string, string, string, string>(SetSelectedItems), new object[] { currentRunsetName, _client.GetAssayTypeOfRunset(currentRunsetName),
                        currentMethodName, _client.GetAssayTypeOfMethod(currentMethodName), _client.GetPlateId(selectedRunsetListBox.SelectedIndex, deckLocation) });
                }
            }
        }

        private void UpdateInfoSelectedMethod()
        {
            selectedRunsetListBox.Items.Clear();

            string currentMethodName = _client.GetNameOfCurrentMethod();

            selectedRunsetListBox.Items.Add(currentMethodName);

            if (selectedRunsetListBox.Items.Count > 0)
                selectedRunsetListBox.SelectedItem = selectedRunsetListBox.Items[0];

            comboBoxTypes.Enabled = false;
            comboBoxMethods.Enabled = false;
            comboBoxRunsets.Enabled = false;

            if (InvokeRequired)
            {
                if (_systemType == SystemType.MASS1)
                {
                    Invoke(new Action<string, string, string, string, string>(SetSelectedItems), new object[] { "", "", currentMethodName, _client.GetAssayTypeOfMethod(currentMethodName), _client.GetSamplePlateId(0) });
                }
                else if (_systemType == SystemType.SPR64)
                {
                    string deckLocation = string.IsNullOrWhiteSpace(comboBoxDeckLocations.Text) ? "P4" : comboBoxDeckLocations.Text;

                    Invoke(new Action<string, string, string, string, string>(SetSelectedItems), new object[] { "", "", currentMethodName, _client.GetAssayTypeOfMethod(currentMethodName), _client.GetPlateId(0, deckLocation) });
                }
            }
        }

        private void SetSelectedItems(string sr, string srt, string sm, string smt, string spid)
        {
            textBoxRunsets.Text = sr;

            textBoxRunsetType.Text = srt;

            textBoxMethods.Text = sm;

            textBoxMethodType.Text = smt;

            textBoxSetSamplePlateID.Text = spid;
        }

        private void buttonPlateID_Click(object sender, EventArgs e)
        {
            buttonPlateID.Enabled = false;
            buttonPlateID.Visible = false;

            bool setPlateID = !string.IsNullOrWhiteSpace(comboBoxDeckLocations.Text) || _systemType == SystemType.SPR64 ?
                _client.SetPlateId(selectedRunsetListBox.SelectedIndex, textBoxSetSamplePlateID.Text, comboBoxDeckLocations.Text) :
                _client.SetSamplePlateId(selectedRunsetListBox.SelectedIndex, textBoxSetSamplePlateID.Text);

            if (!setPlateID)
            {
                if (selectedRunsetListBox.SelectedIndex > _client.GetMethodNamesOfRunset(_client.GetNameOfCurrentRunset()).Count() - 1)
                    DisplayWarningMessage("Selected method index is out of range! Check selected method again!", !setPlateID);
                else
                    DisplayWarningMessage("Selected method does not contain the sample plate. Check for Errors!", !setPlateID);
            }
            else
                DisplayWarningMessage(string.Empty, !setPlateID);
        }

        private void textBoxSetSamplePlateID_TextChanged(object sender, EventArgs e)
        {
            buttonPlateID.Visible = true;
            buttonPlateID.Enabled = true;
        }

        private void selectedRunsetListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxSetSamplePlateID.Enabled = true;
            comboBoxDeckLocations.Enabled = true;

            string currentRunsetName = _client.GetNameOfCurrentRunset();
            string assayTypeMRunset = _client.GetAssayTypeOfRunset(currentRunsetName);
            string currentMethodName = _client.GetNameOfCurrentMethod();
            string assayTypeMethod = _client.GetAssayTypeOfMethod(currentMethodName);
            string plateId = !string.IsNullOrWhiteSpace(comboBoxDeckLocations.Text) || _systemType == SystemType.SPR64 ?
                _client.GetPlateId(selectedRunsetListBox.SelectedIndex, comboBoxDeckLocations.Text) :
                _client.GetSamplePlateId(selectedRunsetListBox.SelectedIndex);

            if (InvokeRequired)
                Invoke(new Action<string, string, string, string, string>(SetSelectedItems), new object[] { currentRunsetName, assayTypeMRunset, currentMethodName, assayTypeMethod, plateId });
            else
                SetSelectedItems(currentRunsetName, assayTypeMRunset, currentMethodName, assayTypeMethod, plateId);              
        }

        #endregion

        #region Methods 

        private void comboBoxTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTypes.SelectedIndex > -1)
            {
                comboBoxMethods.Items.Clear();
                comboBoxRunsets.Items.Clear();

                // Set method names in the combo box list of option 
                foreach (string methodName in _client.GetNamesOfMethodsOfAssayType(comboBoxTypes.SelectedItem.ToString()))
                    comboBoxMethods.Items.Add(methodName);


                // Set runset names in the combo box list of option 
                foreach (string runsetName in _client.GetNamesOfRunsetsOfAssayType(comboBoxTypes.SelectedItem.ToString()))
                    comboBoxRunsets.Items.Add(runsetName);

                comboBoxMethods.Enabled = (comboBoxMethods.Items.Count != 0);
                comboBoxRunsets.Enabled = (comboBoxRunsets.Items.Count != 0);
            }
        }

        private void comboBoxMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMethods.SelectedIndex > -1)
            {
                bool selectMethod = _client.SelectMethod(comboBoxMethods.SelectedItem.ToString());

                if (!selectMethod)
                    DisplayWarningMessage("Can not create runset. Check Operation Mode!", !selectMethod);
                else
                    DisplayWarningMessage(string.Empty, !selectMethod);

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
                        DisplayWarningMessage("Can not find selected runset in the list. Check the name of the runset in the provided list!", !selectedRunsetStatus);
                    else
                        DisplayWarningMessage("Can not select runset. Check for error messages!", !selectedRunsetStatus);
                }
                else
                    DisplayWarningMessage(string.Empty, !selectedRunsetStatus);

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

        private void checkBoxRunMaintenanceCmd_CheckedChanged(object sender, EventArgs e)
        {

            comboBoxMaintenance.Enabled = checkBoxrunMaintenanceCmd.Checked;

            comboBoxTypes.Enabled = !checkBoxrunMaintenanceCmd.Checked;
            comboBoxMethods.Enabled = !checkBoxrunMaintenanceCmd.Checked;
            comboBoxRunsets.Enabled = !checkBoxrunMaintenanceCmd.Checked;
            selectedRunsetListBox.Enabled = !checkBoxrunMaintenanceCmd.Checked;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            int operationMode = _client.GetOperationMode();

            if (operationMode == 1)
            {
                bool startStatus = _client.ResumeRunset();

                if (!startStatus)
                    DisplayWarningMessage("Can not resume runset. Check for errors!.", !startStatus);
                else
                    DisplayWarningMessage(string.Empty, !startStatus);
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
                    if (operationMode == 11)
                        DisplayWarningMessage("'Reset Required' from the maintenance commands!", !startStatus);
                    else if (_client.GetNameOfCurrentRunset() != string.Empty)
                        DisplayWarningMessage("No selected Runset to Run.", !startStatus);
                    else
                        DisplayWarningMessage("Can not start 'Runset' check for errors.", !startStatus);
                }
                else
                    DisplayWarningMessage(string.Empty, !startStatus);

                //Start Selected runset 
                _client.StartSelectedRunset();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            bool abortStatus = _client.AbortScript();

            if (!abortStatus)
                DisplayWarningMessage("Can not stop script as currently no script is running.", !abortStatus);
            else
                DisplayWarningMessage(string.Empty, !abortStatus);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (comboBoxPauseAfter.SelectedIndex >= 0)
            {
                bool pauseStatus = _client.PauseRunsetAfter(comboBoxPauseAfter.SelectedIndex);

                if (!pauseStatus)
                    DisplayWarningMessage("Can not pause the script as currently no script is running.", !pauseStatus);
                else
                    DisplayWarningMessage(string.Empty, !pauseStatus);
            }
            else
            {
                DisplayWarningMessage("Select an option for pause (1: after current command, 2: after current cycle, 3: after current method) in the dropdown box.", true);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            bool resetStatus = _client.ResetRunset();

            if (!resetStatus)
            {
                if (_client.GetOperationMode() == 20)
                    DisplayWarningMessage("Can not 'Reset Runset', currently the runset is running.", !resetStatus);
                else
                    DisplayWarningMessage("No 'Runset' is selected to reset.", !resetStatus);
            }
            else
                DisplayWarningMessage(string.Empty, !resetStatus);

            // Reset the client 
            SetupTestClient();
        }

        #endregion

        #region Status

        private void StatusCheck()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => this.StatusCheck()));
                return;
            }

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

        private void UpdateOperationMode()
        {
            string mode = string.Empty;

            switch (_client.GetOperationMode())
            {
                // Idle: The device is ready to run a script which can be a runset or a maintenance procedure
                case 0:
                    mode = "Idle";
                    break;

                // Paused: 	The device is idle but there is a runset selected that was paused during run and might be resumed. The ResetRunset method has to be called before a new runset can be started. Running a maintenance command in between is possible, however.
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

                // Running: There is currently a script running. This can be either a runset or a maintenance procedure.
                case 20:
                    mode = "Running";
                    break;
            }

            if (InvokeRequired)
                Invoke(new Action<string>(ChangeOperationTextBox), new object[] { mode });
            else
                ChangeOperationTextBox(mode);
        }

        private void ChangeOperationTextBox(string obj)
        {
            operationTextBox.Text = obj;
        }

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

        private void ChangeErrorTextBox(string obj)
        {
            ErrorTextBox.Text = obj;
        }

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
                    status = "Undocked";
                    break;
                
                // state undocked
                case 1:
                    status = "Docked";
                    break;
            }

            if (InvokeRequired)
                Invoke(new Action<string>(ChangeChipStatusTextBox), new object[] { status });
            else
                ChangeChipStatusTextBox(status);
        }

        private void ChangeChipStatusTextBox(string obj)
        {
            chipStatusTextBox.Text = obj;
        }

        private void UpdateSamplePlateStatus()
        {
            string status = string.Empty;

            if (_systemType == SystemType.MASS1)
            {
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
            }
            else if (_systemType == SystemType.SPR64)
            {
                switch (_client.IsSideDoorOpen())
                {
                    // state unknown
                    case -1:
                        status = "Unknown";
                        break;

                    // state Closed
                    case 0:
                        status = "Closed";
                        break;

                    // state Open
                    case 1:
                        status = "Open";
                        break;
                }
            }

            if (InvokeRequired)
                Invoke(new Action<string>(SamplePlateStatusTextBox), new object[] { status });
            else
                SamplePlateStatusTextBox(status);
        }

        private void SamplePlateStatusTextBox(string obj)
        {
            samplePlateStatTextBox.Text = obj;
        }

        private void UpdateSamplePlateID()
        {
            if (_client.GetNameOfCurrentRunset() != string.Empty)
            {
                string plateId = !string.IsNullOrWhiteSpace(comboBoxDeckLocations.Text) || _systemType == SystemType.SPR64 ?
                    _client.GetCurrentPlateId(comboBoxDeckLocations.Text) :
                    _client.GetCurrentSamplePlateId();

                if (InvokeRequired)
                    Invoke(new Action<string>(CurrentSamplePlateIDTextBox), new object[] { plateId });
                else
                    CurrentSamplePlateIDTextBox(plateId);
            }
        }

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

        private void DisplayWarningMessage(string obj, bool state)
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

        private void btnUpdateSamplePlateStatus_Click(object sender, EventArgs e)
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

        private void btnPlateOut_Click(object sender, EventArgs e)
        {
            if (_systemType == SystemType.MASS1)
            {
                _client.MoveSamplePlateTrayOut();
            }
            else if (_systemType == SystemType.SPR64)
            {
                _client.OpenSideDoor();
            }
        }

        private void plateInButton_Click(object sender, EventArgs e)
        {
            if (_systemType == SystemType.MASS1)
            {
                _client.MoveSamplePlateTrayIn();
            }
            else if (_systemType == SystemType.SPR64)
            {
                _client.CloseSideDoor();
            }
        }

        #endregion
    }
}
