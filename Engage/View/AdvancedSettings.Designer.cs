namespace Engage.View
{
    partial class AdvancedSettings
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
            this.lblHistoricOffset = new System.Windows.Forms.Label();
            this.numHistoricOffset = new System.Windows.Forms.NumericUpDown();
            this.lblHistoricOffsetDesc = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblFutureOffsetDesc = new System.Windows.Forms.Label();
            this.numFutureOffset = new System.Windows.Forms.NumericUpDown();
            this.lblFutureOffset = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numHistoricOffset)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFutureOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHistoricOffset
            // 
            this.lblHistoricOffset.AutoSize = true;
            this.lblHistoricOffset.Location = new System.Drawing.Point(0, 5);
            this.lblHistoricOffset.Name = "lblHistoricOffset";
            this.lblHistoricOffset.Size = new System.Drawing.Size(76, 13);
            this.lblHistoricOffset.TabIndex = 0;
            this.lblHistoricOffset.Text = "Historic Offset:";
            // 
            // numHistoricOffset
            // 
            this.numHistoricOffset.AutoSize = true;
            this.numHistoricOffset.Location = new System.Drawing.Point(113, 3);
            this.numHistoricOffset.Name = "numHistoricOffset";
            this.numHistoricOffset.Size = new System.Drawing.Size(45, 20);
            this.numHistoricOffset.TabIndex = 0;
            // 
            // lblHistoricOffsetDesc
            // 
            this.lblHistoricOffsetDesc.AutoSize = true;
            this.lblHistoricOffsetDesc.Location = new System.Drawing.Point(0, 26);
            this.lblHistoricOffsetDesc.Name = "lblHistoricOffsetDesc";
            this.lblHistoricOffsetDesc.Size = new System.Drawing.Size(157, 26);
            this.lblHistoricOffsetDesc.TabIndex = 1;
            this.lblHistoricOffsetDesc.Text = "The number of weeks to \r\ninclude prior to the current date.";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 13);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Default Offsets";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblFutureOffsetDesc);
            this.panel1.Controls.Add(this.numFutureOffset);
            this.panel1.Controls.Add(this.lblFutureOffset);
            this.panel1.Controls.Add(this.lblHistoricOffsetDesc);
            this.panel1.Controls.Add(this.lblHistoricOffset);
            this.panel1.Controls.Add(this.numHistoricOffset);
            this.panel1.Location = new System.Drawing.Point(12, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 118);
            this.panel1.TabIndex = 3;
            // 
            // lblFutureOffsetDesc
            // 
            this.lblFutureOffsetDesc.AutoSize = true;
            this.lblFutureOffsetDesc.Location = new System.Drawing.Point(0, 86);
            this.lblFutureOffsetDesc.Name = "lblFutureOffsetDesc";
            this.lblFutureOffsetDesc.Size = new System.Drawing.Size(160, 26);
            this.lblFutureOffsetDesc.TabIndex = 4;
            this.lblFutureOffsetDesc.Text = "The number of weeks to \r\ninclude beyond the current date.";
            // 
            // numFutureOffset
            // 
            this.numFutureOffset.AutoSize = true;
            this.numFutureOffset.Location = new System.Drawing.Point(113, 63);
            this.numFutureOffset.Name = "numFutureOffset";
            this.numFutureOffset.Size = new System.Drawing.Size(45, 20);
            this.numFutureOffset.TabIndex = 3;
            // 
            // lblFutureOffset
            // 
            this.lblFutureOffset.AutoSize = true;
            this.lblFutureOffset.Location = new System.Drawing.Point(0, 65);
            this.lblFutureOffset.Name = "lblFutureOffset";
            this.lblFutureOffset.Size = new System.Drawing.Size(71, 13);
            this.lblFutureOffset.TabIndex = 2;
            this.lblFutureOffset.Text = "Future Offset:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(113, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(12, 157);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Delete Appts";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AdvancedSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(185, 192);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Advanced Settings";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.numHistoricOffset)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFutureOffset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHistoricOffset;
        private System.Windows.Forms.NumericUpDown numHistoricOffset;
        private System.Windows.Forms.Label lblHistoricOffsetDesc;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblFutureOffsetDesc;
        private System.Windows.Forms.NumericUpDown numFutureOffset;
        private System.Windows.Forms.Label lblFutureOffset;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;

    }
}