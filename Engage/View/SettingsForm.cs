using System;
using System.Collections.Generic;
using System.Windows.Forms;
using log4net;
using Microsoft.Office.Interop.Outlook;

namespace Engage.View
{
    public partial class SettingsForm : Form
    {
        private bool _isAdvancedSettingsOpen;
        private List<OlBusyStatus> showAs = new List<OlBusyStatus> { OlBusyStatus.olBusy, OlBusyStatus.olFree, OlBusyStatus.olOutOfOffice, OlBusyStatus.olTentative };
        private static readonly ILog Log = LogManager.GetLogger(typeof(SettingsForm));

        public SettingsForm()
        {
            InitializeComponent();

            _isAdvancedSettingsOpen = false;
        }
        
        void SettingsFormLoad(object sender, System.EventArgs e)
        {
            tbUsername.Text = Properties.Settings.Default.Username;
            tbPassword.Text = Properties.Settings.Default.Password;
            cbEnableNotifications.Checked = Properties.Settings.Default.EnableNotification;
            numNotificationTime.Value = Properties.Settings.Default.NotificationTime;

            cbShowAs.SelectedIndex = showAs.LastIndexOf(Properties.Settings.Default.ShowAs);
            
            if (!Properties.Settings.Default.EnableNotification)
            {
                numNotificationTime.Enabled = false;
            }
        }

        private void BtnSubmitClick(object sender, EventArgs e)
        {
            if (!(tbUsername.Text == string.Empty || tbPassword.Text == string.Empty))
            {
                if (!(tbUsername.Text == Properties.Settings.Default.Username && tbPassword.Text == Properties.Settings.Default.Password))
                {
                    Properties.Settings.Default.Username = tbUsername.Text;
                    Properties.Settings.Default.Password = tbPassword.Text;
                    Properties.Settings.Default.Save();
                }
                Properties.Settings.Default.EnableNotification = cbEnableNotifications.Checked;
                Properties.Settings.Default.ShowAs = showAs[cbShowAs.SelectedIndex];

                if (cbEnableNotifications.Checked)
                    Properties.Settings.Default.NotificationTime = numNotificationTime.Value;

                Properties.Settings.Default.Save();

                Log.Info("Settings updated successfully.");

                this.Dispose();
            }
            else
            {
                Log.Warn("Incorrect Username: '" + tbUsername.Text + "' or Password: '" + tbPassword.Text + "'.");
                MessageBox.Show(@"Please enter in your Username and Password for Blue Pumpkin.");
            }
        }

        private void cbEnableNotifications_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnableNotifications.Checked)
            {
                numNotificationTime.Enabled = true;
            }
            else
            {
                numNotificationTime.Enabled = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:joseph.hosemann@blackbaud.com?subject=BBAssist Engage");
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            if (!_isAdvancedSettingsOpen)
            {
                var settings = new AdvancedSettings();
                settings.ShowDialog();
                _isAdvancedSettingsOpen = true;

                if (settings.DialogResult == DialogResult.OK)
                {
                    settings.Dispose();
                    _isAdvancedSettingsOpen = false;
                }
                else
                {
                    settings.Dispose();
                    _isAdvancedSettingsOpen = false;
                }
            }
        }
    }
}
