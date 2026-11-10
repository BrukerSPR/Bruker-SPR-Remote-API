using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrukerSprRemoteAPI.Client;

namespace RemoteAppTestClient
{
    public partial class TestClientForm : Form
    {
        private readonly SystemType systemType;
        private BrukerSprRemoteAPIclient client;
        private CancellationTokenSource cts;

        private string[] methodTypes = [];
        private string[] runsetTypes = [];
        private string[] methodNames = [];
        private string[] runsetNames = [];
        private string[] maintenanceCommands = [];

        public TestClientForm(SystemType systemType)
        {
            InitializeComponent();
            this.systemType = systemType;

            this.tabControl.Enabled = false;

            if (systemType == SystemType.MASS1)
            {
                this.btnPlateOutOrDoorOpen.Text = "Plate Out";
                this.btnPlateInOrDoorClose.Text = "Plate In";
                this.lblTrayOrDoorStatus.Text = "Plate Tray Status";
            }
            else
            {
                this.btnPlateOutOrDoorOpen.Text = "Open Door";
                this.btnPlateInOrDoorClose.Text = "Close Door";
                this.lblTrayOrDoorStatus.Text = "Side Door Status";
            }
        }

        /// <summary>
        /// Connect the client to the Server
        /// </summary>
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                this.client = new();

                this.tabControl.Enabled = true;

                GetDeckLocationNames();
                GetMethodsAndRunsets();
                SetupTestClient();

                this.cts = new CancellationTokenSource();

                StartExecution();

                this.btnConnect.Enabled = false;
                this.btnDisconnect.Enabled = true;

                StatusCheck();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connect Exception: {ex}");
            }
        }

        /// <summary>
        /// Disconnect the client from the server
        /// </summary>
        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                CancelExecution();

                this.tabControl.Enabled = false;

                this.client.ChannelFactory.Close(); // forgetting this line will cause a System.Net.Sockets.SocketException
                this.client.Close();

                this.btnConnect.Enabled = true;
                this.btnDisconnect.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Disconnect Exception: {ex}");
            }
        }

        private void GetDeckLocationNames()
        {
            this.comboBoxDeckLocations.Items.Clear();
            this.comboBoxDeckLocations.Items.AddRange(this.client.GetDeckLocations());
            this.comboBoxDeckLocations.SelectedIndex = 0;
        }

        private void GetMethodsAndRunsets()
        {
            this.methodTypes = this.client.GetAssayTypesOfAllMethods();
            this.runsetTypes = this.client.GetAssayTypesOfAllRunsets();
            this.methodNames = this.client.GetNamesOfMethods();
            this.runsetNames = this.client.GetNamesOfRunsets();
            this.maintenanceCommands = this.client.GetNamesOfMaintenanceProcedures();
        }

        private void SetupTestClient()
        {
            this.comboBoxTypes.SelectedIndex = -1;
            this.comboBoxTypes.Items.Clear();

            foreach (string type in this.methodTypes.Union(this.runsetTypes).Distinct())
            {
                this.comboBoxTypes.Items.Add(type);
            }

            this.comboBoxMethods.SelectedIndex = -1;
            this.comboBoxMethods.Items.Clear();

            // Set method names in the combo box list of option.
            this.comboBoxMethods.Items.AddRange(this.methodNames);

            this.comboBoxRunsets.SelectedIndex = -1;
            this.comboBoxRunsets.Items.Clear();

            // Set runset names in the combo box list of option.
            this.comboBoxRunsets.Items.AddRange(this.runsetNames);

            this.comboBoxMaintenance.SelectedIndex = -1;
            this.comboBoxMaintenance.Items.Clear();

            // Set maintenance commands names in the combo box list of option.
            this.comboBoxMaintenance.Items.AddRange(this.maintenanceCommands);

            this.selectedRunsetListBox.Items.Clear();

            this.textBoxRunsets.Text = string.Empty;
            this.textBoxRunsetType.Text = string.Empty;
            this.textBoxMethods.Text = string.Empty;
            this.textBoxMethodType.Text = string.Empty;

            this.textBoxSetSamplePlateID.Text = string.Empty;
            this.textBoxSetSamplePlateID.Enabled = false;

            this.buttonPlateID.Visible = this.buttonPlateID.Enabled = false;

            SetViewState();
        }

        private void SetViewState()
        {
            this.comboBoxMaintenance.Enabled = this.checkBoxrunMaintenanceCmd.Checked;

            this.comboBoxTypes.Enabled = !this.checkBoxrunMaintenanceCmd.Checked;
            this.comboBoxMethods.Enabled = !this.checkBoxrunMaintenanceCmd.Checked;
            this.comboBoxRunsets.Enabled = !this.checkBoxrunMaintenanceCmd.Checked;
            this.selectedRunsetListBox.Enabled = !this.checkBoxrunMaintenanceCmd.Checked;

            this.comboBoxTypes.Invalidate();
            this.comboBoxMethods.Invalidate();
            this.comboBoxRunsets.Invalidate();
        }

        private void StartExecution()
        {
            Task.Factory.StartNew(OwnCodeCancelableTask_EveryNSeconds, this.cts.Token);
        }

        private void CancelExecution()
        {
            this.cts.Cancel();
        }

        private void OwnCodeCancelableTask_EveryNSeconds(object taskState)
        {
            var token = (CancellationToken)taskState;

            while (!token.IsCancellationRequested)
            {
                if (this.checkboxPolling.Checked)
                {
                    StatusCheck();
                }

                Thread.Sleep(2000);
            }
        }

        private void UpdateInfoSelectedRunset()
        {
            this.selectedRunsetListBox.Items.Clear();

            string currentRunsetName = this.client.GetNameOfCurrentRunset();
            string currentMethodName = this.client.GetNameOfCurrentMethod();

            this.selectedRunsetListBox.Items.AddRange(this.client.GetMethodNamesOfRunset(currentRunsetName));

            if (this.selectedRunsetListBox.Items.Count > 0)
            {
                this.selectedRunsetListBox.SelectedItem = this.selectedRunsetListBox.Items[0];
            }

            this.comboBoxTypes.Enabled = false;
            this.comboBoxMethods.Enabled = false;
            this.comboBoxRunsets.Enabled = false;

            if (InvokeRequired)
            {
                if (this.systemType == SystemType.MASS1)
                {
                    Invoke(new Action<string, string, string, string, string>(SetSelectedItems), [ currentRunsetName, this.client.GetAssayTypeOfRunset(currentRunsetName),
                        currentMethodName, this.client.GetAssayTypeOfMethod(currentMethodName), this.client.GetSamplePlateId(this.selectedRunsetListBox.SelectedIndex)]);
                }
                else if (this.systemType == SystemType.SPR64)
                {
                    string deckLocation = string.IsNullOrWhiteSpace(this.comboBoxDeckLocations.Text) ? "P4" : this.comboBoxDeckLocations.Text;

                    Invoke(new Action<string, string, string, string, string>(SetSelectedItems), [ currentRunsetName, this.client.GetAssayTypeOfRunset(currentRunsetName),
                        currentMethodName, this.client.GetAssayTypeOfMethod(currentMethodName), this.client.GetPlateId(this.selectedRunsetListBox.SelectedIndex, deckLocation)]);
                }
            }
        }

        private void UpdateInfoSelectedMethod()
        {
            this.selectedRunsetListBox.Items.Clear();

            string currentMethodName = this.client.GetNameOfCurrentMethod();

            this.selectedRunsetListBox.Items.Add(currentMethodName);

            if (this.selectedRunsetListBox.Items.Count > 0)
            {
                this.selectedRunsetListBox.SelectedItem = this.selectedRunsetListBox.Items[0];
            }

            this.comboBoxTypes.Enabled = false;
            this.comboBoxMethods.Enabled = false;
            this.comboBoxRunsets.Enabled = false;

            if (InvokeRequired)
            {
                if (this.systemType == SystemType.MASS1)
                {
                    Invoke(new Action<string, string, string, string, string>(SetSelectedItems), [string.Empty, string.Empty, currentMethodName, this.client.GetAssayTypeOfMethod(currentMethodName), this.client.GetSamplePlateId(0)]);
                }
                else if (this.systemType == SystemType.SPR64)
                {
                    string deckLocation = string.IsNullOrWhiteSpace(this.comboBoxDeckLocations.Text) ? "P4" : this.comboBoxDeckLocations.Text;

                    Invoke(new Action<string, string, string, string, string>(SetSelectedItems), [string.Empty, string.Empty, currentMethodName, this.client.GetAssayTypeOfMethod(currentMethodName), this.client.GetPlateId(0, deckLocation)]);
                }
            }
        }

        private void SetSelectedItems(string sr, string srt, string sm, string smt, string spid)
        {
            this.textBoxRunsets.Text = sr;
            this.textBoxRunsetType.Text = srt;
            this.textBoxMethods.Text = sm;
            this.textBoxMethodType.Text = smt;
            this.textBoxSetSamplePlateID.Text = spid;
        }

        private void BtnPlateID_Click(object sender, EventArgs e)
        {
            this.buttonPlateID.Enabled = false;
            this.buttonPlateID.Visible = false;

            bool setPlateID = !string.IsNullOrWhiteSpace(this.comboBoxDeckLocations.Text) || this.systemType == SystemType.SPR64 ?
                this.client.SetPlateId(this.selectedRunsetListBox.SelectedIndex, this.textBoxSetSamplePlateID.Text, this.comboBoxDeckLocations.Text) :
                this.client.SetSamplePlateId(this.selectedRunsetListBox.SelectedIndex, this.textBoxSetSamplePlateID.Text);

            if (!setPlateID)
            {
                if (this.selectedRunsetListBox.SelectedIndex > this.client.GetMethodNamesOfRunset(this.client.GetNameOfCurrentRunset()).Length - 1)
                {
                    DisplayWarningMessage("Selected method index is out of range! Check selected method again!", !setPlateID);
                }
                else
                {
                    DisplayWarningMessage("Selected method does not contain the sample plate. Check for Errors!", !setPlateID);
                }
            }
            else
            {
                DisplayWarningMessage(string.Empty, !setPlateID);
            }
        }

        private void TextBoxSetSamplePlateID_TextChanged(object sender, EventArgs e)
        {
            this.buttonPlateID.Visible = true;
            this.buttonPlateID.Enabled = true;
        }

        private void SelectedRunsetListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBoxSetSamplePlateID.Enabled = true;
            this.comboBoxDeckLocations.Enabled = true;

            string currentRunsetName = this.client.GetNameOfCurrentRunset();
            string assayTypeMRunset = this.client.GetAssayTypeOfRunset(currentRunsetName);
            string currentMethodName = this.client.GetNameOfCurrentMethod();
            string assayTypeMethod = this.client.GetAssayTypeOfMethod(currentMethodName);
            string plateId = !string.IsNullOrWhiteSpace(this.comboBoxDeckLocations.Text) || this.systemType == SystemType.SPR64 ?
                this.client.GetPlateId(this.selectedRunsetListBox.SelectedIndex, this.comboBoxDeckLocations.Text) :
                this.client.GetSamplePlateId(this.selectedRunsetListBox.SelectedIndex);

            if (InvokeRequired)
            {
                Invoke(new Action<string, string, string, string, string>(SetSelectedItems), [currentRunsetName, assayTypeMRunset, currentMethodName, assayTypeMethod, plateId]);
            }
            else
            {
                SetSelectedItems(currentRunsetName, assayTypeMRunset, currentMethodName, assayTypeMethod, plateId);
            }
        }

        private void ComboBoxTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxTypes.SelectedIndex > -1)
            {
                this.comboBoxMethods.Items.Clear();
                this.comboBoxRunsets.Items.Clear();

                // Set method names in the combo box list of option.
                this.comboBoxMethods.Items.AddRange(this.client.GetNamesOfMethodsOfAssayType(this.comboBoxTypes.SelectedItem.ToString()));

                // Set runset names in the combo box list of option.
                this.comboBoxRunsets.Items.AddRange(this.client.GetNamesOfRunsetsOfAssayType(this.comboBoxTypes.SelectedItem.ToString()));

                this.comboBoxMethods.Enabled = (this.comboBoxMethods.Items.Count != 0);
                this.comboBoxRunsets.Enabled = (this.comboBoxRunsets.Items.Count != 0);
            }
        }

        private void ComboBoxMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxMethods.SelectedIndex > -1)
            {
                bool selectMethod = this.client.SelectMethod(this.comboBoxMethods.SelectedItem.ToString());

                if (!selectMethod)
                {
                    DisplayWarningMessage("Can not create runset. Check Operation Mode!", !selectMethod);
                }
                else
                {
                    DisplayWarningMessage(string.Empty, !selectMethod);
                }

                UpdateInfoSelectedMethod();
            }
        }

        private void ComboBoxRunsets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxRunsets.SelectedIndex > -1)
            {
                bool selectedRunsetStatus = this.client.SelectRunset(this.comboBoxRunsets.SelectedItem.ToString());

                if (!selectedRunsetStatus)
                {
                    if (!this.runsetNames.Contains(this.comboBoxMethods.SelectedItem.ToString()))
                    {
                        DisplayWarningMessage("Can not find selected runset in the list. Check the name of the runset in the provided list!", !selectedRunsetStatus);
                    }
                    else
                    {
                        DisplayWarningMessage("Can not select runset. Check for error messages!", !selectedRunsetStatus);
                    }
                }
                else
                {
                    DisplayWarningMessage(string.Empty, !selectedRunsetStatus);
                }

                UpdateInfoSelectedRunset();
            }
        }

        private void BtnTypesClear_Click(object sender, EventArgs e)
        {
            if (this.comboBoxTypes.SelectedIndex > -1)
            {
                this.comboBoxTypes.SelectedIndex = -1;

                SetupTestClient();
            }
        }

        private void CheckBoxRunMaintenanceCmd_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxMaintenance.Enabled = this.checkBoxrunMaintenanceCmd.Checked;

            this.comboBoxTypes.Enabled = !this.checkBoxrunMaintenanceCmd.Checked;
            this.comboBoxMethods.Enabled = !this.checkBoxrunMaintenanceCmd.Checked;
            this.comboBoxRunsets.Enabled = !this.checkBoxrunMaintenanceCmd.Checked;
            this.selectedRunsetListBox.Enabled = !this.checkBoxrunMaintenanceCmd.Checked;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            int operationMode = this.client.GetOperationMode();

            if (operationMode == 1)
            {
                bool startStatus = this.client.ResumeRunset();

                if (!startStatus)
                {
                    DisplayWarningMessage("Can not resume runset. Check for errors!.", !startStatus);
                }
                else
                {
                    DisplayWarningMessage(string.Empty, !startStatus);
                }
            }
            else
            {
                bool startStatus = false;

                if (this.checkBoxrunMaintenanceCmd.Checked)
                {
                    if (this.comboBoxMaintenance.SelectedIndex >= 0)
                    {
                        startStatus = this.client.RunMaintenanceProcedure(this.client.GetNamesOfMaintenanceProcedures()[this.comboBoxMaintenance.SelectedIndex]);
                    }
                }
                else
                {
                    if (this.selectedRunsetListBox.SelectedIndex > 0)
                    {
                        this.client.StartSelectedRunsetFrom(this.selectedRunsetListBox.SelectedIndex);
                    }
                    else
                    {
                        startStatus = this.client.StartSelectedRunset();
                    }
                }

                if (!startStatus)
                {
                    if (operationMode == 11)
                    {
                        DisplayWarningMessage("'Reset Required' from the maintenance commands!", !startStatus);
                    }
                    else if (this.client.GetNameOfCurrentRunset() != string.Empty)
                    {
                        DisplayWarningMessage("No selected Runset to Run.", !startStatus);
                    }
                    else
                    {
                        DisplayWarningMessage("Can not start 'Runset' check for errors.", !startStatus);
                    }
                }
                else
                {
                    DisplayWarningMessage(string.Empty, !startStatus);
                }

                // Start Selected runset.
                this.client.StartSelectedRunset();
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            bool abortStatus = this.client.AbortScript();

            if (!abortStatus)
            {
                DisplayWarningMessage("Can not stop script as currently no script is running.", !abortStatus);
            }
            else
            {
                DisplayWarningMessage(string.Empty, !abortStatus);
            }
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            if (this.comboBoxPauseAfter.SelectedIndex >= 0)
            {
                bool pauseStatus = this.client.PauseRunsetAfter(this.comboBoxPauseAfter.SelectedIndex);

                if (!pauseStatus)
                {
                    DisplayWarningMessage("Can not pause the script as currently no script is running.", !pauseStatus);
                }
                else
                {
                    DisplayWarningMessage(string.Empty, !pauseStatus);
                }
            }
            else
            {
                DisplayWarningMessage("Select an option for pause (1: after current command, 2: after current cycle, 3: after current method) in the dropdown box.", true);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            bool resetStatus = this.client.ResetRunset();

            if (!resetStatus)
            {
                if (this.client.GetOperationMode() == 20)
                {
                    DisplayWarningMessage("Can not 'Reset Runset', currently the runset is running.", !resetStatus);
                }
                else
                {
                    DisplayWarningMessage("No 'Runset' is selected to reset.", !resetStatus);
                }
            }
            else
            {
                DisplayWarningMessage(string.Empty, !resetStatus);
            }

            // Reset the client.
            SetupTestClient();
        }

        private void StatusCheck()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => StatusCheck()));
                return;
            }

            UpdateOperationMode();
            UpdateChipStatus();
            UpdateSamplePlateStatus();
            UpdateSamplePlateID();

            if (this.client.HasErrors())
            {
                UpdateErrors();
            }

            while (this.client.HasMessage())
            {
                string[] message = this.client.GetMessage();
                this.logGridView.Rows.Add(message[0], message[1], message[2]);
            }
        }

        private void UpdateOperationMode()
        {
            string mode = string.Empty;

            switch (this.client.GetOperationMode())
            {
                // Idle: The device is ready to run a script which can be a runset or a maintenance procedure
                case 0:
                    mode = "Idle";
                    break;

                // Paused: The device is idle but there is a runset selected that was paused during run and might be resumed. The ResetRunset method has to be called before a new runset can be started. Running a maintenance command in between is possible, however.
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
            {
                Invoke(new Action<string>(ChangeOperationTextBox), [mode]);
            }
            else
            {
                ChangeOperationTextBox(mode);
            }
        }

        private void ChangeOperationTextBox(string obj)
        {
            this.operationTextBox.Text = obj;
        }

        private void UpdateErrors()
        {
            this.lblError.Visible = true;
            this.ErrorTextBox.Visible = true;

            while (this.client.HasErrors())
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<string>(ChangeChipStatusTextBox), [this.client.GetErrors().First()]);
                }
                else
                {
                    ChangeErrorTextBox(this.client.GetErrors().First());
                }
            }
        }

        private void ChangeErrorTextBox(string obj)
        {
            this.ErrorTextBox.Text = obj;
        }

        private void UpdateChipStatus()
        {
            string status = string.Empty;

            switch (this.client.IsChipDocked())
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
            {
                Invoke(new Action<string>(ChangeChipStatusTextBox), [status]);
            }
            else
            {
                ChangeChipStatusTextBox(status);
            }
        }

        private void ChangeChipStatusTextBox(string obj)
        {
            this.chipStatusTextBox.Text = obj;
        }

        private void UpdateSamplePlateStatus()
        {
            string status = string.Empty;

            if (this.systemType == SystemType.MASS1)
            {
                switch (this.client.IsSamplePlateTrayIn())
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
            else if (this.systemType == SystemType.SPR64)
            {
                switch (this.client.IsSideDoorOpen())
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
            {
                Invoke(new Action<string>(SamplePlateStatusTextBox), [status]);
            }
            else
            {
                SamplePlateStatusTextBox(status);
            }
        }

        private void SamplePlateStatusTextBox(string obj)
        {
            this.samplePlateStatTextBox.Text = obj;
        }

        private void UpdateSamplePlateID()
        {
            if (this.client.GetNameOfCurrentRunset() != string.Empty)
            {
                string plateId = !string.IsNullOrWhiteSpace(this.comboBoxDeckLocations.Text) || this.systemType == SystemType.SPR64 ?
                    this.client.GetCurrentPlateId(this.comboBoxDeckLocations.Text) :
                    this.client.GetCurrentSamplePlateId();

                if (InvokeRequired)
                {
                    Invoke(new Action<string>(CurrentSamplePlateIDTextBox), [plateId]);
                }
                else
                {
                    CurrentSamplePlateIDTextBox(plateId);
                }
            }
        }

        private void CurrentSamplePlateIDTextBox(string obj)
        {
            this.samplePlateIDTextBox.Text = obj;
        }

        private void CheckboxPolling_CheckedChanged(object sender, EventArgs e)
        {
            this.btnUpdateOperationMode.Enabled = this.btnUpdateOperationMode.Visible = !this.checkboxPolling.Checked;
            this.btnUpdateSampePlateStatus.Enabled = this.btnUpdateSampePlateStatus.Visible = !this.checkboxPolling.Checked;
            this.btnUpdateChipStatus.Enabled = this.btnUpdateChipStatus.Visible = !this.checkboxPolling.Checked;
            this.btnUpdateCurrentSamplePlateID.Enabled = this.btnUpdateCurrentSamplePlateID.Visible = !this.checkboxPolling.Checked;
            this.btnUpdateError.Enabled = this.btnUpdateError.Visible = !this.checkboxPolling.Checked;
        }

        private void DisplayWarningMessage(string obj, bool state)
        {
            this.labelLocalWarningTitle.Visible = state;
            this.labelLocalWarrning.Visible = state;
            this.labelLocalWarrning.Text = obj;
        }

        private void BtnUpdateOperationMode_Click(object sender, EventArgs e)
        {
            UpdateOperationMode();
        }

        private void BtnUpdateChipStatus_Click(object sender, EventArgs e)
        {
            UpdateChipStatus();
        }

        private void BtnUpdateSamplePlateStatus_Click(object sender, EventArgs e)
        {
            UpdateSamplePlateStatus();
        }

        private void BtnUpdateCurrentSamplePlateID_Click(object sender, EventArgs e)
        {
            UpdateSamplePlateID();
        }

        private void BtnUpdateError_Click(object sender, EventArgs e)
        {
            UpdateErrors();
        }

        private void BtnPlateOut_Click(object sender, EventArgs e)
        {
            if (this.systemType == SystemType.MASS1)
            {
                this.client.MoveSamplePlateTrayOut();
            }
            else if (this.systemType == SystemType.SPR64)
            {
                this.client.OpenSideDoor();
            }
        }

        private void PlateInButton_Click(object sender, EventArgs e)
        {
            if (this.systemType == SystemType.MASS1)
            {
                this.client.MoveSamplePlateTrayIn();
            }
            else if (this.systemType == SystemType.SPR64)
            {
                this.client.CloseSideDoor();
            }
        }
    }
}
