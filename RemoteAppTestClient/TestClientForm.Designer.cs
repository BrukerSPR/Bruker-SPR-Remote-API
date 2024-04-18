namespace RemoteAppTestClient
{
    partial class TestClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestClientForm));
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.logGridView = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabOperation = new System.Windows.Forms.TabPage();
            this.btnPlateInOrDoorClose = new System.Windows.Forms.Button();
            this.btnPlateOutOrDoorOpen = new System.Windows.Forms.Button();
            this.labelLocalWarrning = new System.Windows.Forms.Label();
            this.groupBoxRunsetSelection = new System.Windows.Forms.GroupBox();
            this.lblTypes = new System.Windows.Forms.Label();
            this.comboBoxTypes = new System.Windows.Forms.ComboBox();
            this.comboBoxMethods = new System.Windows.Forms.ComboBox();
            this.labelLocalWarningTitle = new System.Windows.Forms.Label();
            this.comboBoxRunsets = new System.Windows.Forms.ComboBox();
            this.checkBoxrunMaintenanceCmd = new System.Windows.Forms.CheckBox();
            this.comboBoxMaintenance = new System.Windows.Forms.ComboBox();
            this.btnTypesClear = new System.Windows.Forms.Button();
            this.lblMethods = new System.Windows.Forms.Label();
            this.lblRunsets = new System.Windows.Forms.Label();
            this.lblMaintenance = new System.Windows.Forms.Label();
            this.groupBoxRunset = new System.Windows.Forms.GroupBox();
            this.labelPlateLocation = new System.Windows.Forms.Label();
            this.textBoxDeckLocation = new System.Windows.Forms.TextBox();
            this.buttonPlateID = new System.Windows.Forms.Button();
            this.lblCurrentRunset = new System.Windows.Forms.Label();
            this.lblCurrentRunsetType = new System.Windows.Forms.Label();
            this.lblSelectedMethod = new System.Windows.Forms.Label();
            this.selectedRunsetListBox = new System.Windows.Forms.ListBox();
            this.labelSetPlateID = new System.Windows.Forms.Label();
            this.lblSelectedMethodType = new System.Windows.Forms.Label();
            this.textBoxSetSamplePlateID = new System.Windows.Forms.TextBox();
            this.textBoxRunsets = new System.Windows.Forms.TextBox();
            this.textBoxMethodType = new System.Windows.Forms.TextBox();
            this.textBoxRunsetType = new System.Windows.Forms.TextBox();
            this.textBoxMethods = new System.Windows.Forms.TextBox();
            this.groupBoxPolling = new System.Windows.Forms.GroupBox();
            this.btnUpdateError = new System.Windows.Forms.Button();
            this.btnUpdateCurrentSamplePlateID = new System.Windows.Forms.Button();
            this.btnUpdateSampePlateStatus = new System.Windows.Forms.Button();
            this.btnUpdateChipStatus = new System.Windows.Forms.Button();
            this.btnUpdateOperationMode = new System.Windows.Forms.Button();
            this.operationTextBox = new System.Windows.Forms.TextBox();
            this.lblChipStatus = new System.Windows.Forms.Label();
            this.lblTrayOrDoorStatus = new System.Windows.Forms.Label();
            this.samplePlateStatTextBox = new System.Windows.Forms.TextBox();
            this.lblSamplePlateID = new System.Windows.Forms.Label();
            this.lblOperationMode = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.chipStatusTextBox = new System.Windows.Forms.TextBox();
            this.samplePlateIDTextBox = new System.Windows.Forms.TextBox();
            this.ErrorTextBox = new System.Windows.Forms.TextBox();
            this.checkboxPolling = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.comboBoxPauseAfter = new System.Windows.Forms.ComboBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logGridView)).BeginInit();
            this.tabOperation.SuspendLayout();
            this.groupBoxRunsetSelection.SuspendLayout();
            this.groupBoxRunset.SuspendLayout();
            this.groupBoxPolling.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(110)))));
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(16, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(150, 31);
            this.btnConnect.TabIndex = 11;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(110)))));
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDisconnect.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.ForeColor = System.Drawing.Color.White;
            this.btnDisconnect.Location = new System.Drawing.Point(172, 12);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(150, 31);
            this.btnDisconnect.TabIndex = 12;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.logGridView);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(867, 564);
            this.tabLog.TabIndex = 2;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // logGridView
            // 
            this.logGridView.AllowUserToOrderColumns = true;
            this.logGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Type,
            this.Message});
            this.logGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logGridView.Location = new System.Drawing.Point(3, 3);
            this.logGridView.Name = "logGridView";
            this.logGridView.Size = new System.Drawing.Size(861, 558);
            this.logGridView.TabIndex = 0;
            // 
            // Time
            // 
            this.Time.HeaderText = "ColumnTime";
            this.Time.Name = "Time";
            // 
            // Type
            // 
            this.Type.HeaderText = "ColumnType";
            this.Type.Name = "Type";
            // 
            // Message
            // 
            this.Message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Message.HeaderText = "ColumnMessage";
            this.Message.Name = "Message";
            // 
            // tabOperation
            // 
            this.tabOperation.Controls.Add(this.btnPlateInOrDoorClose);
            this.tabOperation.Controls.Add(this.btnPlateOutOrDoorOpen);
            this.tabOperation.Controls.Add(this.labelLocalWarrning);
            this.tabOperation.Controls.Add(this.groupBoxRunsetSelection);
            this.tabOperation.Controls.Add(this.groupBoxRunset);
            this.tabOperation.Controls.Add(this.groupBoxPolling);
            this.tabOperation.Controls.Add(this.checkboxPolling);
            this.tabOperation.Controls.Add(this.btnReset);
            this.tabOperation.Controls.Add(this.comboBoxPauseAfter);
            this.tabOperation.Controls.Add(this.btnPause);
            this.tabOperation.Controls.Add(this.btnStop);
            this.tabOperation.Controls.Add(this.btnStart);
            this.tabOperation.Location = new System.Drawing.Point(4, 22);
            this.tabOperation.Name = "tabOperation";
            this.tabOperation.Padding = new System.Windows.Forms.Padding(3);
            this.tabOperation.Size = new System.Drawing.Size(867, 564);
            this.tabOperation.TabIndex = 0;
            this.tabOperation.Text = "Operation";
            this.tabOperation.UseVisualStyleBackColor = true;
            // 
            // btnPlateInOrDoorClose
            // 
            this.btnPlateInOrDoorClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlateInOrDoorClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(110)))));
            this.btnPlateInOrDoorClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPlateInOrDoorClose.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlateInOrDoorClose.ForeColor = System.Drawing.Color.White;
            this.btnPlateInOrDoorClose.Location = new System.Drawing.Point(128, 525);
            this.btnPlateInOrDoorClose.Name = "btnPlateInOrDoorClose";
            this.btnPlateInOrDoorClose.Size = new System.Drawing.Size(99, 21);
            this.btnPlateInOrDoorClose.TabIndex = 59;
            this.btnPlateInOrDoorClose.Text = "Plate In";
            this.btnPlateInOrDoorClose.UseVisualStyleBackColor = false;
            this.btnPlateInOrDoorClose.Click += new System.EventHandler(this.plateInButton_Click);
            // 
            // btnPlateOutOrDoorOpen
            // 
            this.btnPlateOutOrDoorOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlateOutOrDoorOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(110)))));
            this.btnPlateOutOrDoorOpen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPlateOutOrDoorOpen.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlateOutOrDoorOpen.ForeColor = System.Drawing.Color.White;
            this.btnPlateOutOrDoorOpen.Location = new System.Drawing.Point(24, 525);
            this.btnPlateOutOrDoorOpen.Name = "btnPlateOutOrDoorOpen";
            this.btnPlateOutOrDoorOpen.Size = new System.Drawing.Size(99, 21);
            this.btnPlateOutOrDoorOpen.TabIndex = 58;
            this.btnPlateOutOrDoorOpen.Text = "Plate Out";
            this.btnPlateOutOrDoorOpen.UseVisualStyleBackColor = false;
            this.btnPlateOutOrDoorOpen.Click += new System.EventHandler(this.btnPlateOut_Click);
            // 
            // labelLocalWarrning
            // 
            this.labelLocalWarrning.AutoSize = true;
            this.labelLocalWarrning.ForeColor = System.Drawing.Color.Goldenrod;
            this.labelLocalWarrning.Location = new System.Drawing.Point(82, 450);
            this.labelLocalWarrning.Name = "labelLocalWarrning";
            this.labelLocalWarrning.Size = new System.Drawing.Size(176, 13);
            this.labelLocalWarrning.TabIndex = 57;
            this.labelLocalWarrning.Text = "Warning message is displayed here!";
            // 
            // groupBoxRunsetSelection
            // 
            this.groupBoxRunsetSelection.Controls.Add(this.lblTypes);
            this.groupBoxRunsetSelection.Controls.Add(this.comboBoxTypes);
            this.groupBoxRunsetSelection.Controls.Add(this.comboBoxMethods);
            this.groupBoxRunsetSelection.Controls.Add(this.labelLocalWarningTitle);
            this.groupBoxRunsetSelection.Controls.Add(this.comboBoxRunsets);
            this.groupBoxRunsetSelection.Controls.Add(this.checkBoxrunMaintenanceCmd);
            this.groupBoxRunsetSelection.Controls.Add(this.comboBoxMaintenance);
            this.groupBoxRunsetSelection.Controls.Add(this.btnTypesClear);
            this.groupBoxRunsetSelection.Controls.Add(this.lblMethods);
            this.groupBoxRunsetSelection.Controls.Add(this.lblRunsets);
            this.groupBoxRunsetSelection.Controls.Add(this.lblMaintenance);
            this.groupBoxRunsetSelection.Location = new System.Drawing.Point(25, 40);
            this.groupBoxRunsetSelection.Name = "groupBoxRunsetSelection";
            this.groupBoxRunsetSelection.Size = new System.Drawing.Size(259, 434);
            this.groupBoxRunsetSelection.TabIndex = 55;
            this.groupBoxRunsetSelection.TabStop = false;
            this.groupBoxRunsetSelection.Text = "Runset Selection";
            // 
            // lblTypes
            // 
            this.lblTypes.AutoSize = true;
            this.lblTypes.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypes.Location = new System.Drawing.Point(25, 37);
            this.lblTypes.Name = "lblTypes";
            this.lblTypes.Size = new System.Drawing.Size(37, 13);
            this.lblTypes.TabIndex = 4;
            this.lblTypes.Text = "Types";
            // 
            // comboBoxTypes
            // 
            this.comboBoxTypes.FormattingEnabled = true;
            this.comboBoxTypes.Location = new System.Drawing.Point(28, 53);
            this.comboBoxTypes.Name = "comboBoxTypes";
            this.comboBoxTypes.Size = new System.Drawing.Size(182, 21);
            this.comboBoxTypes.TabIndex = 0;
            this.comboBoxTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypes_SelectedIndexChanged);
            // 
            // comboBoxMethods
            // 
            this.comboBoxMethods.FormattingEnabled = true;
            this.comboBoxMethods.Location = new System.Drawing.Point(28, 103);
            this.comboBoxMethods.Name = "comboBoxMethods";
            this.comboBoxMethods.Size = new System.Drawing.Size(182, 21);
            this.comboBoxMethods.TabIndex = 1;
            this.comboBoxMethods.SelectedIndexChanged += new System.EventHandler(this.comboBoxMethods_SelectedIndexChanged);
            // 
            // labelLocalWarningTitle
            // 
            this.labelLocalWarningTitle.AutoSize = true;
            this.labelLocalWarningTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLocalWarningTitle.ForeColor = System.Drawing.Color.Goldenrod;
            this.labelLocalWarningTitle.Location = new System.Drawing.Point(6, 410);
            this.labelLocalWarningTitle.Name = "labelLocalWarningTitle";
            this.labelLocalWarningTitle.Size = new System.Drawing.Size(54, 13);
            this.labelLocalWarningTitle.TabIndex = 56;
            this.labelLocalWarningTitle.Text = "Warning";
            // 
            // comboBoxRunsets
            // 
            this.comboBoxRunsets.FormattingEnabled = true;
            this.comboBoxRunsets.Location = new System.Drawing.Point(28, 150);
            this.comboBoxRunsets.Name = "comboBoxRunsets";
            this.comboBoxRunsets.Size = new System.Drawing.Size(182, 21);
            this.comboBoxRunsets.TabIndex = 2;
            this.comboBoxRunsets.SelectedIndexChanged += new System.EventHandler(this.comboBoxRunsets_SelectedIndexChanged);
            // 
            // checkBoxrunMaintenanceCmd
            // 
            this.checkBoxrunMaintenanceCmd.AutoSize = true;
            this.checkBoxrunMaintenanceCmd.Location = new System.Drawing.Point(28, 210);
            this.checkBoxrunMaintenanceCmd.Name = "checkBoxrunMaintenanceCmd";
            this.checkBoxrunMaintenanceCmd.Size = new System.Drawing.Size(161, 17);
            this.checkBoxrunMaintenanceCmd.TabIndex = 37;
            this.checkBoxrunMaintenanceCmd.Text = "Run Maintenance Command";
            this.checkBoxrunMaintenanceCmd.UseVisualStyleBackColor = true;
            this.checkBoxrunMaintenanceCmd.CheckedChanged += new System.EventHandler(this.checkBoxRunMaintenanceCmd_CheckedChanged);
            // 
            // comboBoxMaintenance
            // 
            this.comboBoxMaintenance.FormattingEnabled = true;
            this.comboBoxMaintenance.Location = new System.Drawing.Point(28, 246);
            this.comboBoxMaintenance.Name = "comboBoxMaintenance";
            this.comboBoxMaintenance.Size = new System.Drawing.Size(182, 21);
            this.comboBoxMaintenance.TabIndex = 3;
            // 
            // btnTypesClear
            // 
            this.btnTypesClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(110)))));
            this.btnTypesClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTypesClear.Image = ((System.Drawing.Image)(resources.GetObject("btnTypesClear.Image")));
            this.btnTypesClear.Location = new System.Drawing.Point(216, 53);
            this.btnTypesClear.Name = "btnTypesClear";
            this.btnTypesClear.Size = new System.Drawing.Size(21, 21);
            this.btnTypesClear.TabIndex = 36;
            this.btnTypesClear.UseVisualStyleBackColor = false;
            this.btnTypesClear.Click += new System.EventHandler(this.btnTypesClear_Click);
            // 
            // lblMethods
            // 
            this.lblMethods.AutoSize = true;
            this.lblMethods.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMethods.Location = new System.Drawing.Point(25, 87);
            this.lblMethods.Name = "lblMethods";
            this.lblMethods.Size = new System.Drawing.Size(54, 13);
            this.lblMethods.TabIndex = 5;
            this.lblMethods.Text = "Methods";
            // 
            // lblRunsets
            // 
            this.lblRunsets.AutoSize = true;
            this.lblRunsets.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRunsets.Location = new System.Drawing.Point(25, 134);
            this.lblRunsets.Name = "lblRunsets";
            this.lblRunsets.Size = new System.Drawing.Size(48, 13);
            this.lblRunsets.TabIndex = 6;
            this.lblRunsets.Text = "Runsets";
            // 
            // lblMaintenance
            // 
            this.lblMaintenance.AutoSize = true;
            this.lblMaintenance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaintenance.Location = new System.Drawing.Point(25, 230);
            this.lblMaintenance.Name = "lblMaintenance";
            this.lblMaintenance.Size = new System.Drawing.Size(137, 13);
            this.lblMaintenance.TabIndex = 7;
            this.lblMaintenance.Text = "Maintenance Commands";
            // 
            // groupBoxRunset
            // 
            this.groupBoxRunset.Controls.Add(this.labelPlateLocation);
            this.groupBoxRunset.Controls.Add(this.textBoxDeckLocation);
            this.groupBoxRunset.Controls.Add(this.buttonPlateID);
            this.groupBoxRunset.Controls.Add(this.lblCurrentRunset);
            this.groupBoxRunset.Controls.Add(this.lblCurrentRunsetType);
            this.groupBoxRunset.Controls.Add(this.lblSelectedMethod);
            this.groupBoxRunset.Controls.Add(this.selectedRunsetListBox);
            this.groupBoxRunset.Controls.Add(this.labelSetPlateID);
            this.groupBoxRunset.Controls.Add(this.lblSelectedMethodType);
            this.groupBoxRunset.Controls.Add(this.textBoxSetSamplePlateID);
            this.groupBoxRunset.Controls.Add(this.textBoxRunsets);
            this.groupBoxRunset.Controls.Add(this.textBoxMethodType);
            this.groupBoxRunset.Controls.Add(this.textBoxRunsetType);
            this.groupBoxRunset.Controls.Add(this.textBoxMethods);
            this.groupBoxRunset.Location = new System.Drawing.Point(299, 40);
            this.groupBoxRunset.Name = "groupBoxRunset";
            this.groupBoxRunset.Size = new System.Drawing.Size(292, 434);
            this.groupBoxRunset.TabIndex = 54;
            this.groupBoxRunset.TabStop = false;
            this.groupBoxRunset.Text = "Runset Info";
            // 
            // labelPlateLocation
            // 
            this.labelPlateLocation.AutoSize = true;
            this.labelPlateLocation.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlateLocation.Location = new System.Drawing.Point(25, 389);
            this.labelPlateLocation.Name = "labelPlateLocation";
            this.labelPlateLocation.Size = new System.Drawing.Size(81, 13);
            this.labelPlateLocation.TabIndex = 51;
            this.labelPlateLocation.Text = "Plate Location";
            // 
            // textBoxDeckLocation
            // 
            this.textBoxDeckLocation.Enabled = false;
            this.textBoxDeckLocation.Location = new System.Drawing.Point(126, 386);
            this.textBoxDeckLocation.Name = "textBoxDeckLocation";
            this.textBoxDeckLocation.Size = new System.Drawing.Size(102, 20);
            this.textBoxDeckLocation.TabIndex = 50;
            // 
            // buttonPlateID
            // 
            this.buttonPlateID.BackColor = System.Drawing.Color.Lime;
            this.buttonPlateID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPlateID.Image = ((System.Drawing.Image)(resources.GetObject("buttonPlateID.Image")));
            this.buttonPlateID.Location = new System.Drawing.Point(232, 385);
            this.buttonPlateID.Name = "buttonPlateID";
            this.buttonPlateID.Size = new System.Drawing.Size(21, 21);
            this.buttonPlateID.TabIndex = 49;
            this.buttonPlateID.UseVisualStyleBackColor = false;
            this.buttonPlateID.Visible = false;
            this.buttonPlateID.Click += new System.EventHandler(this.buttonPlateID_Click);
            // 
            // lblCurrentRunset
            // 
            this.lblCurrentRunset.AutoSize = true;
            this.lblCurrentRunset.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentRunset.Location = new System.Drawing.Point(25, 34);
            this.lblCurrentRunset.Name = "lblCurrentRunset";
            this.lblCurrentRunset.Size = new System.Drawing.Size(89, 13);
            this.lblCurrentRunset.TabIndex = 26;
            this.lblCurrentRunset.Text = "Selected Runset";
            // 
            // lblCurrentRunsetType
            // 
            this.lblCurrentRunsetType.AutoSize = true;
            this.lblCurrentRunsetType.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentRunsetType.Location = new System.Drawing.Point(25, 58);
            this.lblCurrentRunsetType.Name = "lblCurrentRunsetType";
            this.lblCurrentRunsetType.Size = new System.Drawing.Size(117, 13);
            this.lblCurrentRunsetType.TabIndex = 28;
            this.lblCurrentRunsetType.Text = "Selected Runset Type";
            // 
            // lblSelectedMethod
            // 
            this.lblSelectedMethod.AutoSize = true;
            this.lblSelectedMethod.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedMethod.Location = new System.Drawing.Point(25, 311);
            this.lblSelectedMethod.Name = "lblSelectedMethod";
            this.lblSelectedMethod.Size = new System.Drawing.Size(95, 13);
            this.lblSelectedMethod.TabIndex = 31;
            this.lblSelectedMethod.Text = "Selected Method";
            // 
            // selectedRunsetListBox
            // 
            this.selectedRunsetListBox.FormattingEnabled = true;
            this.selectedRunsetListBox.Location = new System.Drawing.Point(28, 84);
            this.selectedRunsetListBox.Name = "selectedRunsetListBox";
            this.selectedRunsetListBox.Size = new System.Drawing.Size(225, 212);
            this.selectedRunsetListBox.TabIndex = 33;
            this.selectedRunsetListBox.SelectedIndexChanged += new System.EventHandler(this.selectedRunsetListBox_SelectedIndexChanged);
            // 
            // labelSetPlateID
            // 
            this.labelSetPlateID.AutoSize = true;
            this.labelSetPlateID.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSetPlateID.Location = new System.Drawing.Point(25, 363);
            this.labelSetPlateID.Name = "labelSetPlateID";
            this.labelSetPlateID.Size = new System.Drawing.Size(47, 13);
            this.labelSetPlateID.TabIndex = 48;
            this.labelSetPlateID.Text = "Plate ID";
            // 
            // lblSelectedMethodType
            // 
            this.lblSelectedMethodType.AutoSize = true;
            this.lblSelectedMethodType.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedMethodType.Location = new System.Drawing.Point(25, 337);
            this.lblSelectedMethodType.Name = "lblSelectedMethodType";
            this.lblSelectedMethodType.Size = new System.Drawing.Size(77, 13);
            this.lblSelectedMethodType.TabIndex = 34;
            this.lblSelectedMethodType.Text = "Method Type";
            // 
            // textBoxSetSamplePlateID
            // 
            this.textBoxSetSamplePlateID.Enabled = false;
            this.textBoxSetSamplePlateID.Location = new System.Drawing.Point(126, 360);
            this.textBoxSetSamplePlateID.Name = "textBoxSetSamplePlateID";
            this.textBoxSetSamplePlateID.Size = new System.Drawing.Size(102, 20);
            this.textBoxSetSamplePlateID.TabIndex = 47;
            this.textBoxSetSamplePlateID.TextChanged += new System.EventHandler(this.textBoxSetSamplePlateID_TextChanged);
            // 
            // textBoxRunsets
            // 
            this.textBoxRunsets.Location = new System.Drawing.Point(151, 31);
            this.textBoxRunsets.Name = "textBoxRunsets";
            this.textBoxRunsets.ReadOnly = true;
            this.textBoxRunsets.Size = new System.Drawing.Size(102, 20);
            this.textBoxRunsets.TabIndex = 43;
            // 
            // textBoxMethodType
            // 
            this.textBoxMethodType.Location = new System.Drawing.Point(126, 334);
            this.textBoxMethodType.Name = "textBoxMethodType";
            this.textBoxMethodType.ReadOnly = true;
            this.textBoxMethodType.Size = new System.Drawing.Size(102, 20);
            this.textBoxMethodType.TabIndex = 46;
            // 
            // textBoxRunsetType
            // 
            this.textBoxRunsetType.Location = new System.Drawing.Point(151, 55);
            this.textBoxRunsetType.Name = "textBoxRunsetType";
            this.textBoxRunsetType.ReadOnly = true;
            this.textBoxRunsetType.Size = new System.Drawing.Size(102, 20);
            this.textBoxRunsetType.TabIndex = 44;
            // 
            // textBoxMethods
            // 
            this.textBoxMethods.Location = new System.Drawing.Point(126, 308);
            this.textBoxMethods.Name = "textBoxMethods";
            this.textBoxMethods.ReadOnly = true;
            this.textBoxMethods.Size = new System.Drawing.Size(102, 20);
            this.textBoxMethods.TabIndex = 45;
            // 
            // groupBoxPolling
            // 
            this.groupBoxPolling.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPolling.Controls.Add(this.btnUpdateError);
            this.groupBoxPolling.Controls.Add(this.btnUpdateCurrentSamplePlateID);
            this.groupBoxPolling.Controls.Add(this.btnUpdateSampePlateStatus);
            this.groupBoxPolling.Controls.Add(this.btnUpdateChipStatus);
            this.groupBoxPolling.Controls.Add(this.btnUpdateOperationMode);
            this.groupBoxPolling.Controls.Add(this.operationTextBox);
            this.groupBoxPolling.Controls.Add(this.lblChipStatus);
            this.groupBoxPolling.Controls.Add(this.lblTrayOrDoorStatus);
            this.groupBoxPolling.Controls.Add(this.samplePlateStatTextBox);
            this.groupBoxPolling.Controls.Add(this.lblSamplePlateID);
            this.groupBoxPolling.Controls.Add(this.lblOperationMode);
            this.groupBoxPolling.Controls.Add(this.lblError);
            this.groupBoxPolling.Controls.Add(this.chipStatusTextBox);
            this.groupBoxPolling.Controls.Add(this.samplePlateIDTextBox);
            this.groupBoxPolling.Controls.Add(this.ErrorTextBox);
            this.groupBoxPolling.Location = new System.Drawing.Point(615, 40);
            this.groupBoxPolling.MinimumSize = new System.Drawing.Size(200, 275);
            this.groupBoxPolling.Name = "groupBoxPolling";
            this.groupBoxPolling.Size = new System.Drawing.Size(234, 434);
            this.groupBoxPolling.TabIndex = 53;
            this.groupBoxPolling.TabStop = false;
            this.groupBoxPolling.Text = "Polling Info";
            // 
            // btnUpdateError
            // 
            this.btnUpdateError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(110)))));
            this.btnUpdateError.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdateError.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateError.Image")));
            this.btnUpdateError.Location = new System.Drawing.Point(171, 236);
            this.btnUpdateError.Name = "btnUpdateError";
            this.btnUpdateError.Size = new System.Drawing.Size(21, 21);
            this.btnUpdateError.TabIndex = 54;
            this.btnUpdateError.UseVisualStyleBackColor = false;
            this.btnUpdateError.Click += new System.EventHandler(this.btnUpdateError_Click);
            // 
            // btnUpdateCurrentSamplePlateID
            // 
            this.btnUpdateCurrentSamplePlateID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(110)))));
            this.btnUpdateCurrentSamplePlateID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdateCurrentSamplePlateID.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateCurrentSamplePlateID.Image")));
            this.btnUpdateCurrentSamplePlateID.Location = new System.Drawing.Point(171, 192);
            this.btnUpdateCurrentSamplePlateID.Name = "btnUpdateCurrentSamplePlateID";
            this.btnUpdateCurrentSamplePlateID.Size = new System.Drawing.Size(21, 21);
            this.btnUpdateCurrentSamplePlateID.TabIndex = 53;
            this.btnUpdateCurrentSamplePlateID.UseVisualStyleBackColor = false;
            this.btnUpdateCurrentSamplePlateID.Click += new System.EventHandler(this.btnUpdateCurrentSamplePlateID_Click);
            // 
            // btnUpdateSampePlateStatus
            // 
            this.btnUpdateSampePlateStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(110)))));
            this.btnUpdateSampePlateStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdateSampePlateStatus.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateSampePlateStatus.Image")));
            this.btnUpdateSampePlateStatus.Location = new System.Drawing.Point(171, 148);
            this.btnUpdateSampePlateStatus.Name = "btnUpdateSampePlateStatus";
            this.btnUpdateSampePlateStatus.Size = new System.Drawing.Size(21, 21);
            this.btnUpdateSampePlateStatus.TabIndex = 52;
            this.btnUpdateSampePlateStatus.UseVisualStyleBackColor = false;
            this.btnUpdateSampePlateStatus.Click += new System.EventHandler(this.btnUpdateSamplePlateStatus_Click);
            // 
            // btnUpdateChipStatus
            // 
            this.btnUpdateChipStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(110)))));
            this.btnUpdateChipStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdateChipStatus.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateChipStatus.Image")));
            this.btnUpdateChipStatus.Location = new System.Drawing.Point(171, 97);
            this.btnUpdateChipStatus.Name = "btnUpdateChipStatus";
            this.btnUpdateChipStatus.Size = new System.Drawing.Size(21, 21);
            this.btnUpdateChipStatus.TabIndex = 51;
            this.btnUpdateChipStatus.UseVisualStyleBackColor = false;
            this.btnUpdateChipStatus.Click += new System.EventHandler(this.btnUpdateChipStatus_Click);
            // 
            // btnUpdateOperationMode
            // 
            this.btnUpdateOperationMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(110)))));
            this.btnUpdateOperationMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdateOperationMode.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateOperationMode.Image")));
            this.btnUpdateOperationMode.Location = new System.Drawing.Point(171, 49);
            this.btnUpdateOperationMode.Name = "btnUpdateOperationMode";
            this.btnUpdateOperationMode.Size = new System.Drawing.Size(21, 21);
            this.btnUpdateOperationMode.TabIndex = 38;
            this.btnUpdateOperationMode.UseVisualStyleBackColor = false;
            this.btnUpdateOperationMode.Click += new System.EventHandler(this.btnUpdateOperationMode_Click);
            // 
            // operationTextBox
            // 
            this.operationTextBox.Location = new System.Drawing.Point(38, 50);
            this.operationTextBox.Name = "operationTextBox";
            this.operationTextBox.ReadOnly = true;
            this.operationTextBox.Size = new System.Drawing.Size(127, 20);
            this.operationTextBox.TabIndex = 38;
            this.operationTextBox.Text = "Operation Mode";
            // 
            // lblChipStatus
            // 
            this.lblChipStatus.AutoSize = true;
            this.lblChipStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChipStatus.Location = new System.Drawing.Point(19, 81);
            this.lblChipStatus.Name = "lblChipStatus";
            this.lblChipStatus.Size = new System.Drawing.Size(66, 13);
            this.lblChipStatus.TabIndex = 15;
            this.lblChipStatus.Text = "Chip Status";
            // 
            // lblTrayOrDoorStatus
            // 
            this.lblTrayOrDoorStatus.AutoSize = true;
            this.lblTrayOrDoorStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrayOrDoorStatus.Location = new System.Drawing.Point(19, 131);
            this.lblTrayOrDoorStatus.Name = "lblTrayOrDoorStatus";
            this.lblTrayOrDoorStatus.Size = new System.Drawing.Size(109, 13);
            this.lblTrayOrDoorStatus.TabIndex = 16;
            this.lblTrayOrDoorStatus.Text = "Sample Plate Status";
            // 
            // samplePlateStatTextBox
            // 
            this.samplePlateStatTextBox.Location = new System.Drawing.Point(38, 148);
            this.samplePlateStatTextBox.Name = "samplePlateStatTextBox";
            this.samplePlateStatTextBox.ReadOnly = true;
            this.samplePlateStatTextBox.Size = new System.Drawing.Size(127, 20);
            this.samplePlateStatTextBox.TabIndex = 50;
            // 
            // lblSamplePlateID
            // 
            this.lblSamplePlateID.AutoSize = true;
            this.lblSamplePlateID.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSamplePlateID.Location = new System.Drawing.Point(19, 177);
            this.lblSamplePlateID.Name = "lblSamplePlateID";
            this.lblSamplePlateID.Size = new System.Drawing.Size(89, 13);
            this.lblSamplePlateID.TabIndex = 17;
            this.lblSamplePlateID.Text = "Current Plate ID";
            // 
            // lblOperationMode
            // 
            this.lblOperationMode.AutoSize = true;
            this.lblOperationMode.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperationMode.Location = new System.Drawing.Point(19, 34);
            this.lblOperationMode.Name = "lblOperationMode";
            this.lblOperationMode.Size = new System.Drawing.Size(93, 13);
            this.lblOperationMode.TabIndex = 21;
            this.lblOperationMode.Text = "Operation Mode";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.Location = new System.Drawing.Point(19, 221);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(32, 13);
            this.lblError.TabIndex = 22;
            this.lblError.Text = "Error";
            // 
            // chipStatusTextBox
            // 
            this.chipStatusTextBox.Location = new System.Drawing.Point(38, 98);
            this.chipStatusTextBox.Name = "chipStatusTextBox";
            this.chipStatusTextBox.ReadOnly = true;
            this.chipStatusTextBox.Size = new System.Drawing.Size(127, 20);
            this.chipStatusTextBox.TabIndex = 39;
            // 
            // samplePlateIDTextBox
            // 
            this.samplePlateIDTextBox.Location = new System.Drawing.Point(38, 193);
            this.samplePlateIDTextBox.Name = "samplePlateIDTextBox";
            this.samplePlateIDTextBox.ReadOnly = true;
            this.samplePlateIDTextBox.Size = new System.Drawing.Size(127, 20);
            this.samplePlateIDTextBox.TabIndex = 41;
            // 
            // ErrorTextBox
            // 
            this.ErrorTextBox.Location = new System.Drawing.Point(38, 237);
            this.ErrorTextBox.Name = "ErrorTextBox";
            this.ErrorTextBox.ReadOnly = true;
            this.ErrorTextBox.Size = new System.Drawing.Size(127, 20);
            this.ErrorTextBox.TabIndex = 42;
            // 
            // checkboxPolling
            // 
            this.checkboxPolling.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkboxPolling.AutoSize = true;
            this.checkboxPolling.Location = new System.Drawing.Point(615, 17);
            this.checkboxPolling.Name = "checkboxPolling";
            this.checkboxPolling.Size = new System.Drawing.Size(96, 17);
            this.checkboxPolling.TabIndex = 52;
            this.checkboxPolling.Text = "Turn on polling";
            this.checkboxPolling.UseVisualStyleBackColor = true;
            this.checkboxPolling.CheckedChanged += new System.EventHandler(this.checkboxPolling_CheckedChanged);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(110)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(737, 527);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(99, 21);
            this.btnReset.TabIndex = 25;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // comboBoxPauseAfter
            // 
            this.comboBoxPauseAfter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPauseAfter.FormattingEnabled = true;
            this.comboBoxPauseAfter.Items.AddRange(new object[] {
            "After Command",
            "After Cycle",
            "After Method"});
            this.comboBoxPauseAfter.Location = new System.Drawing.Point(647, 527);
            this.comboBoxPauseAfter.Name = "comboBoxPauseAfter";
            this.comboBoxPauseAfter.Size = new System.Drawing.Size(84, 21);
            this.comboBoxPauseAfter.TabIndex = 11;
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(110)))));
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPause.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.ForeColor = System.Drawing.Color.White;
            this.btnPause.Location = new System.Drawing.Point(542, 527);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(99, 21);
            this.btnPause.TabIndex = 10;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(110)))));
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(437, 527);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(99, 21);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(110)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(332, 527);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(99, 21);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start/Resume";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabOperation);
            this.tabControl.Controls.Add(this.tabLog);
            this.tabControl.Location = new System.Drawing.Point(12, 49);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(875, 590);
            this.tabControl.TabIndex = 0;
            // 
            // TestClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 651);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tabControl);
            this.MinimumSize = new System.Drawing.Size(915, 690);
            this.Name = "TestClientForm";
            this.Text = "Remote Test Client";
            this.tabLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logGridView)).EndInit();
            this.tabOperation.ResumeLayout(false);
            this.tabOperation.PerformLayout();
            this.groupBoxRunsetSelection.ResumeLayout(false);
            this.groupBoxRunsetSelection.PerformLayout();
            this.groupBoxRunset.ResumeLayout(false);
            this.groupBoxRunset.PerformLayout();
            this.groupBoxPolling.ResumeLayout(false);
            this.groupBoxPolling.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.DataGridView logGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.TabPage tabOperation;
        private System.Windows.Forms.Button buttonPlateID;
        private System.Windows.Forms.Label labelSetPlateID;
        private System.Windows.Forms.TextBox textBoxSetSamplePlateID;
        private System.Windows.Forms.TextBox textBoxMethodType;
        private System.Windows.Forms.TextBox textBoxMethods;
        private System.Windows.Forms.TextBox textBoxRunsetType;
        private System.Windows.Forms.TextBox textBoxRunsets;
        private System.Windows.Forms.TextBox ErrorTextBox;
        private System.Windows.Forms.TextBox samplePlateIDTextBox;
        private System.Windows.Forms.TextBox chipStatusTextBox;
        private System.Windows.Forms.TextBox operationTextBox;
        private System.Windows.Forms.CheckBox checkBoxrunMaintenanceCmd;
        private System.Windows.Forms.Button btnTypesClear;
        private System.Windows.Forms.Label lblSelectedMethodType;
        private System.Windows.Forms.ListBox selectedRunsetListBox;
        private System.Windows.Forms.Label lblSelectedMethod;
        private System.Windows.Forms.Label lblCurrentRunsetType;
        private System.Windows.Forms.Label lblCurrentRunset;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblOperationMode;
        private System.Windows.Forms.Label lblSamplePlateID;
        private System.Windows.Forms.Label lblTrayOrDoorStatus;
        private System.Windows.Forms.Label lblChipStatus;
        private System.Windows.Forms.ComboBox comboBoxPauseAfter;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblMaintenance;
        private System.Windows.Forms.Label lblRunsets;
        private System.Windows.Forms.Label lblMethods;
        private System.Windows.Forms.Label lblTypes;
        private System.Windows.Forms.ComboBox comboBoxMaintenance;
        private System.Windows.Forms.ComboBox comboBoxRunsets;
        private System.Windows.Forms.ComboBox comboBoxMethods;
        private System.Windows.Forms.ComboBox comboBoxTypes;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TextBox samplePlateStatTextBox;
        private System.Windows.Forms.CheckBox checkboxPolling;
        private System.Windows.Forms.GroupBox groupBoxRunsetSelection;
        private System.Windows.Forms.GroupBox groupBoxRunset;
        private System.Windows.Forms.GroupBox groupBoxPolling;
        private System.Windows.Forms.Label labelLocalWarrning;
        private System.Windows.Forms.Label labelLocalWarningTitle;
        private System.Windows.Forms.Button btnUpdateError;
        private System.Windows.Forms.Button btnUpdateCurrentSamplePlateID;
        private System.Windows.Forms.Button btnUpdateSampePlateStatus;
        private System.Windows.Forms.Button btnUpdateChipStatus;
        private System.Windows.Forms.Button btnUpdateOperationMode;
        private System.Windows.Forms.Button btnPlateInOrDoorClose;
        private System.Windows.Forms.Button btnPlateOutOrDoorOpen;
        private System.Windows.Forms.Label labelPlateLocation;
        private System.Windows.Forms.TextBox textBoxDeckLocation;
    }
}

