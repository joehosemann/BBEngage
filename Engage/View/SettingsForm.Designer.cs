namespace Engage.View
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.lblUsername = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbEnableNotifications = new System.Windows.Forms.CheckBox();
            this.numNotificationTime = new System.Windows.Forms.NumericUpDown();
            this.labNotifications = new System.Windows.Forms.Label();
            this.gbOutlook = new System.Windows.Forms.GroupBox();
            this.lblShowAs = new System.Windows.Forms.Label();
            this.cbShowAs = new System.Windows.Forms.ComboBox();
            this.lblCredit = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.LinkLabel();
            this.btnAdvanced = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNotificationTime)).BeginInit();
            this.gbOutlook.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(6, 23);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username:";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(70, 19);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(116, 20);
            this.tbUsername.TabIndex = 0;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(6, 45);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Password:";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(70, 42);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(116, 20);
            this.tbPassword.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(158, 199);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(55, 23);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.BtnSubmitClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblUsername);
            this.groupBox1.Controls.Add(this.tbUsername);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 3, 12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 72);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Blue Pumpkin:";
            // 
            // cbEnableNotifications
            // 
            this.cbEnableNotifications.AutoSize = true;
            this.cbEnableNotifications.Location = new System.Drawing.Point(7, 18);
            this.cbEnableNotifications.Name = "cbEnableNotifications";
            this.cbEnableNotifications.Size = new System.Drawing.Size(118, 17);
            this.cbEnableNotifications.TabIndex = 4;
            this.cbEnableNotifications.Text = "Enable notifications";
            this.cbEnableNotifications.UseVisualStyleBackColor = true;
            this.cbEnableNotifications.CheckedChanged += new System.EventHandler(this.cbEnableNotifications_CheckedChanged);
            // 
            // numNotificationTime
            // 
            this.numNotificationTime.Location = new System.Drawing.Point(146, 42);
            this.numNotificationTime.Name = "numNotificationTime";
            this.numNotificationTime.Size = new System.Drawing.Size(40, 20);
            this.numNotificationTime.TabIndex = 5;
            // 
            // labNotifications
            // 
            this.labNotifications.AutoSize = true;
            this.labNotifications.Location = new System.Drawing.Point(6, 45);
            this.labNotifications.Name = "labNotifications";
            this.labNotifications.Size = new System.Drawing.Size(135, 13);
            this.labNotifications.TabIndex = 6;
            this.labNotifications.Text = "Notification time in minutes:";
            // 
            // gbOutlook
            // 
            this.gbOutlook.Controls.Add(this.lblShowAs);
            this.gbOutlook.Controls.Add(this.cbShowAs);
            this.gbOutlook.Controls.Add(this.numNotificationTime);
            this.gbOutlook.Controls.Add(this.labNotifications);
            this.gbOutlook.Controls.Add(this.cbEnableNotifications);
            this.gbOutlook.Location = new System.Drawing.Point(12, 94);
            this.gbOutlook.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.gbOutlook.Name = "gbOutlook";
            this.gbOutlook.Size = new System.Drawing.Size(201, 99);
            this.gbOutlook.TabIndex = 7;
            this.gbOutlook.TabStop = false;
            this.gbOutlook.Text = "Outlook:";
            // 
            // lblShowAs
            // 
            this.lblShowAs.AutoSize = true;
            this.lblShowAs.Location = new System.Drawing.Point(4, 72);
            this.lblShowAs.Name = "lblShowAs";
            this.lblShowAs.Size = new System.Drawing.Size(51, 13);
            this.lblShowAs.TabIndex = 8;
            this.lblShowAs.Text = "Show as:";
            // 
            // cbShowAs
            // 
            this.cbShowAs.FormattingEnabled = true;
            this.cbShowAs.Items.AddRange(new object[] {
            "Busy",
            "Free",
            "Out of Office",
            "Tentative"});
            this.cbShowAs.Location = new System.Drawing.Point(60, 68);
            this.cbShowAs.Name = "cbShowAs";
            this.cbShowAs.Size = new System.Drawing.Size(126, 21);
            this.cbShowAs.TabIndex = 7;
            // 
            // lblCredit
            // 
            this.lblCredit.AutoSize = true;
            this.lblCredit.Location = new System.Drawing.Point(3, 0);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(152, 13);
            this.lblCredit.TabIndex = 8;
            this.lblCredit.Text = "Created by: Joseph Hosemann";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(3, 13);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(174, 13);
            this.lblEmail.TabIndex = 9;
            this.lblEmail.TabStop = true;
            this.lblEmail.Text = "joseph.hosemann@blackbaud.com";
            this.lblEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnAdvanced
            // 
            this.btnAdvanced.Location = new System.Drawing.Point(12, 199);
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.Size = new System.Drawing.Size(106, 23);
            this.btnAdvanced.TabIndex = 2;
            this.btnAdvanced.Text = "Advanced Settings";
            this.btnAdvanced.UseVisualStyleBackColor = true;
            this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.lblCredit);
            this.flowLayoutPanel1.Controls.Add(this.lblEmail);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 233);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(5, 3, 3, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(180, 26);
            this.flowLayoutPanel1.TabIndex = 11;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(226, 263);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnAdvanced);
            this.Controls.Add(this.gbOutlook);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSubmit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Engage: Settings";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SettingsFormLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNotificationTime)).EndInit();
            this.gbOutlook.ResumeLayout(false);
            this.gbOutlook.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbEnableNotifications;
        private System.Windows.Forms.NumericUpDown numNotificationTime;
        private System.Windows.Forms.Label labNotifications;
        private System.Windows.Forms.GroupBox gbOutlook;
        private System.Windows.Forms.ComboBox cbShowAs;
        private System.Windows.Forms.Label lblShowAs;
        private System.Windows.Forms.Label lblCredit;
        private System.Windows.Forms.LinkLabel lblEmail;
        private System.Windows.Forms.Button btnAdvanced;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}