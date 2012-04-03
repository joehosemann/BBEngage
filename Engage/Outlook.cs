using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Engage.Model;
using log4net;
using Microsoft.Office.Interop.Outlook;
using Application = Microsoft.Office.Interop.Outlook.Application;
using Exception = System.Exception;

namespace Engage
{
    public class Outlook
    {
        #region Logging

        private string Username { get { return Properties.Settings.Default.Username; } }

        public void LogError(string message, System.Exception e)
        {
            //get logger
            ILog logger = LogManager.GetLogger(typeof(Outlook));

            //set user to log4net context, so we can use %X{user} in the appenders
            if (Username != string.Empty)
                MDC.Set("username", Username);

            if (logger.IsErrorEnabled)
                logger.Error(message, e); //now log error
        }

        public void LogInfo(string message)
        {
            //get logger
            ILog logger = LogManager.GetLogger(typeof(Outlook));

            //set user to log4net context, so we can use %X{user} in the appenders
            if (Username != string.Empty)
                MDC.Set("username", Username);

            if (logger.IsInfoEnabled)
                logger.Info(message); //now log error
        }

        #endregion

        #region Properties

        public int RecycleNItems { get { return Properties.Settings.Default.RecycleNItems; } }

        #endregion

        public void DeleteAll()
        {
            DeleteAppointments();
            ClearDeletedItems();
        }

        /// <summary>
        /// Adds the appointments.
        /// </summary>
        /// <param name="appointments">The appointments.</param>
        public void AddAppointments(List<AppointmentModel> appointments)
        {
            _Application app = null;

            var outlookAppointments = new List<AppointmentItem>();

            app = new Application();

            try
            {
                //++++++++++++Begin Edit Area+++++++++++++++

                foreach (var appointment in appointments)
                {

                    var newAppointment = (AppointmentItem)app.CreateItem(OlItemType.olAppointmentItem);
                    newAppointment.Start = appointment.StartTime;
                    newAppointment.End = appointment.EndTime;
                    newAppointment.Location = "Engage";
                    newAppointment.Subject = appointment.Subject;
                    newAppointment.ReminderOverrideDefault = true;
                    newAppointment.BusyStatus = Properties.Settings.Default.ShowAs;

                    if (Properties.Settings.Default.EnableNotification)
                    {
                        newAppointment.ReminderMinutesBeforeStart = (int)Properties.Settings.Default.NotificationTime;
                        newAppointment.ReminderSet = true;
                    }

                    if (appointment.StartTime < DateTime.Now)
                    {
                        newAppointment.ReminderSet = false;
                    }

                    newAppointment.Save();
                    newAppointment.Close(OlInspectorClose.olSave);
                    outlookAppointments.Add(newAppointment);

                }
                outlookAppointments.ToArray();

                //++++++++++++End Edit Area+++++++++++++++
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteAppointments()
        {
            _Application app = null;
            _NameSpace session = null;
            MAPIFolder folder = null;
            Items contactItems = null;

            app = new Application();
            session = app.Session;
            folder = session.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);
            contactItems = folder.Items;

            var noItemsLeft = false;

            try
            {
                while (noItemsLeft == false)
                {
                    //++++++++++++Begin Edit Area+++++++++++++++
                    var n = 0;

                    foreach (object item in contactItems)
                    {
                        if (item != null)
                        {
                            var getType = GetOutlookTypeForComObject(item);
                            if (getType != null && getType.Name == "AppointmentItem")
                            {
                                if ((item as AppointmentItem).Location == "Engage")
                                {
                                    n++;
                                    (item as AppointmentItem).Delete();
                                }
                            }
                        }
                    }

                    if (n == 0)
                        noItemsLeft = true;
                    //++++++++++++End Edit Area+++++++++++++++
                }

            }
            catch (System.Exception ex)
            {
                if (ex.Message != "The message you specified cannot be found.") // Required because of the MSTO not deleting items completely bug.
                    MessageBox.Show(ex.Message);
            }
        }

        public void ClearDeletedItems()
        {
            _Application app = null;
            _NameSpace session = null;
            MAPIFolder folder = null;
            Items contactItems = null;

            app = new Application();
            session = app.Session;
            folder = session.GetDefaultFolder(OlDefaultFolders.olFolderDeletedItems);
            contactItems = folder.Items;

            var noItemsLeft = false;

            try
            {
                while (noItemsLeft == false)
                {
                    //++++++++++++Begin Edit Area+++++++++++++++
                    var n = 0;

                    foreach (object item in contactItems)
                    {
                        if (item != null)
                        {
                            var getType = GetOutlookTypeForComObject(item);
                            if (getType != null && getType.Name == "AppointmentItem")
                            {
                                    n++;
                                    (item as AppointmentItem).Delete();
                            }
                        }
                    }

                    if (n == 0)
                        noItemsLeft = true;
                    //++++++++++++End Edit Area+++++++++++++++
                }
            }
            catch (System.Exception ex)
            {
                if (ex.Message != "The message you specified cannot be found.") // Required because of the MSTO not deleting items completely bug.
                    MessageBox.Show(ex.Message);
            }

            return;
        }

       private Type GetOutlookTypeForComObject(object outlookComObject)
        {
            // Credit goes to http://fernandof.wordpress.com/2008/02/05/how-to-check-the-type-of-a-com-object-system__comobject-with-visual-c-net/

            // get the com object and fetch its IUnknown
            IntPtr iunkwn = System.Runtime.InteropServices.Marshal.GetIUnknownForObject(outlookComObject);

            // enum all the types defined in the interop assembly
            System.Reflection.Assembly outlookAssembly =
                System.Reflection.Assembly.GetAssembly(typeof(Microsoft.Office.Interop.Outlook.Application));
            Type[] outlookTypes = outlookAssembly.GetTypes();

            // find the first implemented interop type
            foreach (var currType in outlookTypes)
            {
                // get the iid of the current type
                Guid iid = currType.GUID;
                if (!currType.IsInterface || iid == Guid.Empty)
                {
                    // com interop type must be an interface with valid iid
                    continue;
                }

                // query supportability of current interface on object
                IntPtr ipointer = IntPtr.Zero;
                System.Runtime.InteropServices.Marshal.QueryInterface(iunkwn, ref iid, out ipointer);

                if (ipointer != IntPtr.Zero)
                {
                    return currType;
                }
            }

            // no implemented type found
            return null;
        }

        #region DEPRECIATED

        //private bool DuplicateAppointment(AppointmentModel appointment)
        //{

        //    var objOutlookApp = new Application();
        //    NameSpace objOutlookNamespace = objOutlookApp.GetNamespace("mapi");
        //    MAPIFolder objOutlookFolder = objOutlookNamespace.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);

        //    Items objCalendarItems = objOutlookFolder.Items;

        //    return objCalendarItems.Cast<AppointmentItem>().Any(objCalendarItem => objCalendarItem.Subject == appointment.Subject && objCalendarItem.Start == appointment.StartTime && objCalendarItem.End == appointment.EndTime);
        //}

        ///// <summary>
        ///// Deletes the appointments by day.
        ///// </summary>
        ///// <param name="date">The date.</param>
        //private void DeleteAppointmentsByDay(DateTime date)
        //{

        //    var outlook = new Application();
        //    var objOutlookNamespace = outlook.GetNamespace("mapi");
        //    var objOutlookFolder = objOutlookNamespace.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);
        //    var objCalendarItems = objOutlookFolder.Items;

        //    foreach (AppointmentItem objCalendarItem in objCalendarItems)
        //    {
        //        if (objCalendarItem.Start.Date == date.Date && objCalendarItem.Location == "Engage")
        //        {
        //            objCalendarItem.Delete();
        //        }
        //    }
        //}

        ///// <summary>
        ///// Deletes all appointments.
        ///// </summary>
        //private void deleteAppointmentsOLD()
        //{
        //    var objOutlookApp = new Microsoft.Office.Interop.Outlook.Application();
        //    NameSpace objOutlookNamespace = objOutlookApp.GetNamespace("mapi");
        //    MAPIFolder objOutlookFolder = objOutlookNamespace.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);
        //    //MAPIFolder objOutlookRecycledFolder = objOutlookNamespace.GetDefaultFolder(OlDefaultFolders.olFolderDeletedItems);

        //    //Items objCalendarItems = objOutlookFolder.Items;
        //    try
        //    {
        //        // Moves appointments into recycled folder
        //        foreach (object item in objOutlookFolder.Items)
        //        {
        //            AppointmentItem appointmentItem = item as AppointmentItem;
        //            if (null != appointmentItem && appointmentItem.Location == "Engage")
        //            {
        //                appointmentItem.Delete();
        //                continue;
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void deleteRecycledOLD()
        //{

        //    var objOutlookApp = new Microsoft.Office.Interop.Outlook.Application();
        //    NameSpace objOutlookNamespace = objOutlookApp.GetNamespace("mapi");
        //    MAPIFolder objOutlookRecycledFolder = objOutlookNamespace.GetDefaultFolder(OlDefaultFolders.olFolderDeletedItems);

        //    try
        //    {
        //        // Deletes appointments from recycled folder

        //        foreach (object item in objOutlookRecycledFolder.Items)
        //        {
        //            AppointmentItem appointmentItem = item as AppointmentItem;
        //            if (null != appointmentItem && appointmentItem.Location == "Engage")
        //            {
        //                appointmentItem.Delete();
        //                continue;
        //            }
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        //var a = ex;
        //        //  LogError(ex.ToString(), ex);
        //    }
        //}


        #endregion

    }
}
