namespace UCNL_FW_Update
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stateLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.infoBtn = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.isMBoxBeforeUpdateCxb = new System.Windows.Forms.CheckBox();
            this.isEmptyDeviceChb = new System.Windows.Forms.CheckBox();
            this.startUploadingBtn = new System.Windows.Forms.Button();
            this.loadBtn = new System.Windows.Forms.Button();
            this.firmwarePathTxb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.initialPortSpeedCbx = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.isRedNAVRedNODE = new System.Windows.Forms.CheckBox();
            this.portNameCbx = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.logToolStrip = new System.Windows.Forms.ToolStrip();
            this.exportLogBtn = new System.Windows.Forms.ToolStripButton();
            this.copyToClipboardLobBoxBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.LogBox = new System.Windows.Forms.RichTextBox();
            this.mainStatusStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.logToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.stateLbl,
            this.progressBar});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 502);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.mainStatusStrip.Size = new System.Drawing.Size(599, 26);
            this.mainStatusStrip.TabIndex = 0;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(63, 21);
            this.toolStripStatusLabel1.Text = "State:";
            // 
            // stateLbl
            // 
            this.stateLbl.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stateLbl.Name = "stateLbl";
            this.stateLbl.Size = new System.Drawing.Size(45, 21);
            this.stateLbl.Text = "Idle";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoBtn});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(599, 25);
            this.mainToolStrip.TabIndex = 1;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // infoBtn
            // 
            this.infoBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.infoBtn.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infoBtn.Image = ((System.Drawing.Image)(resources.GetObject("infoBtn.Image")));
            this.infoBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(44, 22);
            this.infoBtn.Text = "Info";
            this.infoBtn.Click += new System.EventHandler(this.infoBtn_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.isMBoxBeforeUpdateCxb);
            this.splitContainer1.Panel1.Controls.Add(this.isEmptyDeviceChb);
            this.splitContainer1.Panel1.Controls.Add(this.startUploadingBtn);
            this.splitContainer1.Panel1.Controls.Add(this.loadBtn);
            this.splitContainer1.Panel1.Controls.Add(this.firmwarePathTxb);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.initialPortSpeedCbx);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.isRedNAVRedNODE);
            this.splitContainer1.Panel1.Controls.Add(this.portNameCbx);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.logToolStrip);
            this.splitContainer1.Panel2.Controls.Add(this.LogBox);
            this.splitContainer1.Size = new System.Drawing.Size(599, 477);
            this.splitContainer1.SplitterDistance = 282;
            this.splitContainer1.TabIndex = 2;
            // 
            // isMBoxBeforeUpdateCxb
            // 
            this.isMBoxBeforeUpdateCxb.AutoSize = true;
            this.isMBoxBeforeUpdateCxb.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.isMBoxBeforeUpdateCxb.Location = new System.Drawing.Point(327, 97);
            this.isMBoxBeforeUpdateCxb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.isMBoxBeforeUpdateCxb.Name = "isMBoxBeforeUpdateCxb";
            this.isMBoxBeforeUpdateCxb.Size = new System.Drawing.Size(130, 24);
            this.isMBoxBeforeUpdateCxb.TabIndex = 12;
            this.isMBoxBeforeUpdateCxb.Text = "Extra pause";
            this.isMBoxBeforeUpdateCxb.UseVisualStyleBackColor = true;
            this.isMBoxBeforeUpdateCxb.CheckedChanged += new System.EventHandler(this.isMBoxBeforeUpdateCxb_CheckedChanged);
            // 
            // isEmptyDeviceChb
            // 
            this.isEmptyDeviceChb.AutoSize = true;
            this.isEmptyDeviceChb.Enabled = false;
            this.isEmptyDeviceChb.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.isEmptyDeviceChb.Location = new System.Drawing.Point(327, 36);
            this.isEmptyDeviceChb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.isEmptyDeviceChb.Name = "isEmptyDeviceChb";
            this.isEmptyDeviceChb.Size = new System.Drawing.Size(166, 24);
            this.isEmptyDeviceChb.TabIndex = 11;
            this.isEmptyDeviceChb.Text = "Device is empty";
            this.isEmptyDeviceChb.UseVisualStyleBackColor = true;
            // 
            // startUploadingBtn
            // 
            this.startUploadingBtn.Enabled = false;
            this.startUploadingBtn.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startUploadingBtn.Location = new System.Drawing.Point(504, 223);
            this.startUploadingBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startUploadingBtn.Name = "startUploadingBtn";
            this.startUploadingBtn.Size = new System.Drawing.Size(79, 53);
            this.startUploadingBtn.TabIndex = 10;
            this.startUploadingBtn.Text = "Start";
            this.startUploadingBtn.UseVisualStyleBackColor = true;
            this.startUploadingBtn.Click += new System.EventHandler(this.startUploadingBtn_Click);
            // 
            // loadBtn
            // 
            this.loadBtn.Enabled = false;
            this.loadBtn.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadBtn.Location = new System.Drawing.Point(504, 190);
            this.loadBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(79, 27);
            this.loadBtn.TabIndex = 9;
            this.loadBtn.Text = "Load";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // firmwarePathTxb
            // 
            this.firmwarePathTxb.Enabled = false;
            this.firmwarePathTxb.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.firmwarePathTxb.Location = new System.Drawing.Point(12, 190);
            this.firmwarePathTxb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.firmwarePathTxb.Name = "firmwarePathTxb";
            this.firmwarePathTxb.ReadOnly = true;
            this.firmwarePathTxb.Size = new System.Drawing.Size(487, 27);
            this.firmwarePathTxb.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Firmware file";
            // 
            // initialPortSpeedCbx
            // 
            this.initialPortSpeedCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.initialPortSpeedCbx.Enabled = false;
            this.initialPortSpeedCbx.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.initialPortSpeedCbx.FormattingEnabled = true;
            this.initialPortSpeedCbx.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.initialPortSpeedCbx.Location = new System.Drawing.Point(12, 111);
            this.initialPortSpeedCbx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.initialPortSpeedCbx.Name = "initialPortSpeedCbx";
            this.initialPortSpeedCbx.Size = new System.Drawing.Size(193, 28);
            this.initialPortSpeedCbx.TabIndex = 6;
            this.initialPortSpeedCbx.SelectedIndexChanged += new System.EventHandler(this.initialPortSpeedCbx_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Initial port baudrate";
            // 
            // isRedNAVRedNODE
            // 
            this.isRedNAVRedNODE.AutoSize = true;
            this.isRedNAVRedNODE.Enabled = false;
            this.isRedNAVRedNODE.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.isRedNAVRedNODE.Location = new System.Drawing.Point(327, 66);
            this.isRedNAVRedNODE.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.isRedNAVRedNODE.Name = "isRedNAVRedNODE";
            this.isRedNAVRedNODE.Size = new System.Drawing.Size(166, 24);
            this.isRedNAVRedNODE.TabIndex = 4;
            this.isRedNAVRedNODE.Text = "RedNAV->RedNODE";
            this.isRedNAVRedNODE.UseVisualStyleBackColor = true;
            // 
            // portNameCbx
            // 
            this.portNameCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portNameCbx.Enabled = false;
            this.portNameCbx.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.portNameCbx.FormattingEnabled = true;
            this.portNameCbx.Location = new System.Drawing.Point(12, 34);
            this.portNameCbx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.portNameCbx.Name = "portNameCbx";
            this.portNameCbx.Size = new System.Drawing.Size(199, 28);
            this.portNameCbx.TabIndex = 3;
            this.portNameCbx.SelectedIndexChanged += new System.EventHandler(this.portNameCbx_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // logToolStrip
            // 
            this.logToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportLogBtn,
            this.copyToClipboardLobBoxBtn,
            this.toolStripLabel1,
            this.toolStripSeparator1});
            this.logToolStrip.Location = new System.Drawing.Point(0, 0);
            this.logToolStrip.Name = "logToolStrip";
            this.logToolStrip.Size = new System.Drawing.Size(599, 27);
            this.logToolStrip.TabIndex = 1;
            this.logToolStrip.Text = "toolStrip1";
            // 
            // exportLogBtn
            // 
            this.exportLogBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.exportLogBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exportLogBtn.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exportLogBtn.Image = ((System.Drawing.Image)(resources.GetObject("exportLogBtn.Image")));
            this.exportLogBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportLogBtn.Name = "exportLogBtn";
            this.exportLogBtn.Size = new System.Drawing.Size(67, 24);
            this.exportLogBtn.Text = "Export";
            this.exportLogBtn.Click += new System.EventHandler(this.exportLogBtn_Click);
            // 
            // copyToClipboardLobBoxBtn
            // 
            this.copyToClipboardLobBoxBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.copyToClipboardLobBoxBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.copyToClipboardLobBoxBtn.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.copyToClipboardLobBoxBtn.Image = ((System.Drawing.Image)(resources.GetObject("copyToClipboardLobBoxBtn.Image")));
            this.copyToClipboardLobBoxBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToClipboardLobBoxBtn.Name = "copyToClipboardLobBoxBtn";
            this.copyToClipboardLobBoxBtn.Size = new System.Drawing.Size(166, 24);
            this.copyToClipboardLobBoxBtn.Text = "Copy to clipboard";
            this.copyToClipboardLobBoxBtn.Click += new System.EventHandler(this.copyToClipboardLobBoxBtn_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(99, 24);
            this.toolStripLabel1.Text = "Log window";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // LogBox
            // 
            this.LogBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogBox.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LogBox.Location = new System.Drawing.Point(3, 30);
            this.LogBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.Size = new System.Drawing.Size(593, 160);
            this.LogBox.TabIndex = 0;
            this.LogBox.Text = "";
            this.LogBox.TextChanged += new System.EventHandler(this.LogBox_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 528);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainStatusStrip);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "UC&NL Firmware update utility";
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.logToolStrip.ResumeLayout(false);
            this.logToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip logToolStrip;
        private System.Windows.Forms.ToolStripButton exportLogBtn;
        private System.Windows.Forms.ToolStripButton copyToClipboardLobBoxBtn;
        private System.Windows.Forms.RichTextBox LogBox;
        private System.Windows.Forms.ToolStripButton infoBtn;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ComboBox portNameCbx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox initialPortSpeedCbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox isRedNAVRedNODE;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.TextBox firmwarePathTxb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button startUploadingBtn;
        private System.Windows.Forms.CheckBox isEmptyDeviceChb;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel stateLbl;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.CheckBox isMBoxBeforeUpdateCxb;
    }
}

