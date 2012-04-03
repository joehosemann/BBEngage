using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Engage.Data;
using Engage.Properties;
using Engage.View;
using log4net;
using log4net.Config;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Outlook;
using Application = Microsoft.Office.Interop.Outlook.Application;
using Exception = Microsoft.Office.Interop.Outlook.Exception;

namespace Engage
{
    public partial class ThisAddIn
    {

        #region Properties

        // Toolbar Properties
        CommandBar _bbaToolBar;
        CommandBarButton _bbaButton;
        private bool _isSettingsOpen;
        private bool _isDeleteRecycledOpen;

        public Application ThisApplication;

        public void LogError(string message, System.Exception e)
        {
            //get logger
            ILog logger = LogManager.GetLogger(typeof(ThisAddIn));

            //set user to log4net context, so we can use %X{user} in the appenders
            if (Settings.Default.Username != string.Empty)
                MDC.Set("username", Settings.Default.Username);

            if (logger.IsErrorEnabled)
                logger.Error(message, e); //now log error
        }

        public void LogInfo(string message)
        {
            //get logger
            ILog logger = LogManager.GetLogger(typeof(ThisAddIn));

            //set user to log4net context, so we can use %X{user} in the appenders
            if (Settings.Default.Username != string.Empty)
                MDC.Set("username", Settings.Default.Username);

            if (logger.IsInfoEnabled)
                logger.Info(message); //now log error
        }

        public bool IsOver1000 { get { return Application.Session.GetDefaultFolder(OlDefaultFolders.olFolderDeletedItems).Items.Count > 1000; } }

        #endregion

        #region Methods

        /// <summary>
        /// Initial Method
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ThisAddInStartup(object sender, EventArgs e)
        {
            // Reqired for Logging
            XmlConfigurator.Configure();

            // Toolbar Creation
            CreateToolbar();

#if DEBUG
            //DeleteAllAppointments();
          //  MessageBox.Show(@"Done Deleting...");
          //  AddTestAppointments();
          //  MessageBox.Show(@"Done Adding...");
#endif
            if (IsOver1000)
            {
                InitalizeDeleteRecycledForm();
            }
            else
            {
                // Events
                Application.NewMailEx += NewEmailEvent;

                if (Settings.Default.Password == string.Empty || Settings.Default.Username == string.Empty)
                {
                    LogInfo("Installation successful.");
                    MessageBox.Show(@"Thank you for installing Engage.");
                    InitalizeSettingsForm();
                }
                else
                {
                    LogInfo("Begin Session");

                    new CookieDataSource().Reset();
                    StartUpdating();
                }
            }


        }

#if DEBUG
        public void AddTestAppointments()
        {
            _Application app = null;

            var outlookAppointments = new List<AppointmentItem>();


            app = new Application();

            try
            {
                //++++++++++++Begin Edit Area+++++++++++++++
                var n = 0;
                while (n < 500)
                {

                    var newAppointment = (AppointmentItem)app.CreateItem(OlItemType.olAppointmentItem);
                    //newAppointment.Start = DateTime.Now;
                    //newAppointment.End = DateTime.Now;
                    //newAppointment.Subject = "BLAH" + new Random().ToString();
                    newAppointment.Location = "Debug";

                    newAppointment.Save();
                    newAppointment.Close(OlInspectorClose.olSave);
                    outlookAppointments.Add(newAppointment);

                }
                outlookAppointments.ToArray();

                //++++++++++++End Edit Area+++++++++++++++

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        public void DeleteAllAppointments()
        {
            _Application app = null;
            _NameSpace session = null;
            MAPIFolder folder = null;
            Items contactItems = null;

            app = new Application();
            session = app.Session;
            folder = session.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);
            contactItems = folder.Items;

            try
            {
                //++++++++++++Begin Edit Area+++++++++++++++

                foreach (object item in contactItems)
                {
                    AppointmentItem appointmentItem = item as AppointmentItem;

                    appointmentItem.Delete();
                }

                //++++++++++++End Edit Area+++++++++++++++
            }
            catch (System.Exception ex)
            {
            }
        }

#endif

        /// <summary>
        /// Starts the updating process.
        /// </summary>
        void StartUpdating()
        {

            var outlook = new Outlook();
            outlook.DeleteAll();

            var bp = new BluePumpkin();
            if (bp.LoginSuccess())
            {
                outlook.AddAppointments(bp.Update());
            }
            else
            {
                InitalizeSettingsForm();
            }

        }

        /// <summary>
        /// Initalizes the settings form.
        /// </summary>
        void InitalizeDeleteRecycledForm()
        {

            if (!_isDeleteRecycledOpen)
            {
                var dr = new DeleteRecycled();
                dr.ShowDialog();
                _isDeleteRecycledOpen = true;

                if (dr.DialogResult == DialogResult.OK)
                {
                    StartUpdating();

                    dr.Dispose();

                    _isDeleteRecycledOpen = false;
                }
                else
                {
                    dr.Dispose();

                    // Events
                    Application.NewMailEx += NewEmailEvent;

                    if (Settings.Default.Password == string.Empty || Settings.Default.Username == string.Empty)
                    {
                        LogInfo("Installation successful.");
                        MessageBox.Show(@"Thank you for installing Engage.");
                        InitalizeSettingsForm();
                    }
                    else
                    {
                        LogInfo("Begin Session");

                        new CookieDataSource().Reset();
                        StartUpdating();
                    }

                    _isDeleteRecycledOpen = false;
                }
            }
        }

        /// <summary>
        /// Initalizes the settings form.
        /// </summary>
        void InitalizeSettingsForm()
        {

            if (!_isSettingsOpen)
            {
                var settings = new SettingsForm();
                settings.ShowDialog();
                _isSettingsOpen = true;

                if (settings.DialogResult == DialogResult.OK)
                {
                    StartUpdating();

                    settings.Dispose();

                    _isSettingsOpen = false;
                }
                else
                {
                    settings.Dispose();

                    _isSettingsOpen = false;
                }
            }
        }

        /// <summary>
        /// Creates the toolbar.
        /// </summary>
        private void CreateToolbar()
        {
            // Verify the BBAssist Toolbar exists and add to the application
            if (_bbaToolBar == null)
            {
                var packtBars = Application.ActiveExplorer().CommandBars;
                _bbaToolBar = packtBars.Add("Engage", MsoBarPosition.msoBarTop, false, true);
            }
            var myButton1 = (CommandBarButton)_bbaToolBar.Controls.Add(1, missing, missing, missing, missing);
            myButton1.Style = MsoButtonStyle.msoButtonCaption;
            myButton1.Caption = "Engage";
            myButton1.Tag = "Engage - Settings";
            if (_bbaButton == null)
            {
                _bbaButton = myButton1;
                _bbaButton.Click += ToolbarButtonClick;
            }
        }

        private void ToolbarButtonClick(CommandBarButton buttonContrl, ref bool cancelOption)
        {
            InitalizeSettingsForm();
        }

        /// <summary>
        /// Email event.
        /// </summary>
        /// <param name="entryIdCollection">The entry id collection.</param>
        private void NewEmailEvent(string entryIdCollection)
        {
            var outlookNS = Application.GetNamespace("MAPI");

            const string sssubject = "SCHEDULE ADJUSTMENT:  Your schedule has been adjusted.  Please review this update in BP.";

            var mail = (MailItem)outlookNS.GetItemFromID(entryIdCollection, Type.Missing);
            var subject = mail.Subject;

            if (subject == sssubject)
            {
                StartUpdating();
            }
        }

        #endregion

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            Startup += ThisAddInStartup;
            Shutdown += new EventHandler(ThisAddIn_Shutdown);
        }

        void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            //  MessageBox.Show(task.Status.ToString());
        }




        #endregion
    }
}
