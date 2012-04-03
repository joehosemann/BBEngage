using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Engage.Data;
using Engage.Model;
using Engage.Properties;
using HtmlAgilityPack;
using log4net;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Engage
{
    internal class BluePumpkin
    {
        #region Properties

        private int _historicalOffset
        {
            get { return Settings.Default.HistoricalOffset; }
        }

        private int _futureOffset
        {
            get { return Settings.Default.FutureOffset; }
        }

        private string _postUrl
        {
            get { return Settings.Default.PostUrl; }
        }

        #endregion

        #region Logging

        private string Username
        {
            get { return Properties.Settings.Default.Username; }
        }

        public void LogError(string message, Exception e)
        {
            //get logger
            ILog logger = LogManager.GetLogger(typeof (BluePumpkin));

            //set user to log4net context, so we can use %X{user} in the appenders
            if (Username != string.Empty)
                MDC.Set("username", Username);

            if (logger.IsErrorEnabled)
                logger.Error(message, e); //now log error
        }

        public void LogInfo(string message)
        {
            //get logger
            ILog logger = LogManager.GetLogger(typeof (BluePumpkin));

            //set user to log4net context, so we can use %X{user} in the appenders
            if (Username != string.Empty)
                MDC.Set("username", Username);

            if (logger.IsInfoEnabled)
                logger.Info(message); //now log error
        }

        #endregion

        #region Methods

        public bool LoginSuccess()
        {
            bool result = false;
            string pageSource = GetHtml();
            if (pageSource != string.Empty)
            {
                result = true;
                LogInfo("Login successful");
            }
            else
            {
                LogInfo("Login failed");
            }
            return result;
        }

        private DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1*diff).Date;
        }

        private string BuildParameters()
        {
            string dateRangeStart =
                StartOfWeek(DateTime.Now.Subtract(TimeSpan.FromDays(_historicalOffset*7)), DayOfWeek.Monday).ToString(
                    string.Format("MM{0}dd{0}yyyy", "\\%2\\F"));
            string dateRangeEnd =
                StartOfWeek(DateTime.Now.AddDays(_futureOffset*7), DayOfWeek.Sunday).ToString(
                    string.Format("MM{0}dd{0}yyyy", "\\%2\\F"));

            var sb = new StringBuilder();

            sb.Append(string.Format(
                "dateRange_START={0}&" +
                "dateRange_END={1}&" +
                "viewType=myschedule&" +
                "sortColumn=&" +
                "sortOrder=ascending&" +
                "_drp_STARTtPkrHour=&" +
                "_drp_STARTtPkrMin=&" +
                "_drp_STARTtPkrSec=&" +
                "_drp_STARTtPkrAmPm=&" +
                "_drp_STARTtPkrDstStd=&" +
                "_drp_ENDtPkrHour=&" +
                "_drp_ENDtPkrMin=&" +
                "_drp_ENDtPkrSec=&" +
                "_drp_ENDtPkrAmPm=&" +
                "_drp_ENDtPkrDstStd=&" +
                "selectedID=&" +
                "performActionOnID=&" +
                "editIndex=0&" +
                "pageModelType=0&" +
                "pageDirty=false&" +
                "pageAction=REFRESH_ACTION", dateRangeStart, dateRangeEnd));

            return sb.ToString();
        }

        /// <summary>
        /// Gets the HTML.
        /// </summary>
        /// <returns>The schedule page source.</returns>
        private string GetHtml()
        {
            string formParams = BuildParameters();

            string pageSource = string.Empty;

            WebRequest getRequest = WebRequest.Create(_postUrl);
            string cookie = new CookieDataSource().Cookie;
            getRequest.Headers.Add("Cookie", cookie);
            getRequest.ContentType = "application/x-www-form-urlencoded";
            getRequest.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(formParams);
            getRequest.ContentLength = bytes.Length;
            try
            {
                using (Stream os = getRequest.GetRequestStream())
                {
                    os.Write(bytes, 0, bytes.Length);
                }
                WebResponse getResponse = getRequest.GetResponse();

                if (getResponse != null)
                    using (var sr = new StreamReader(getResponse.GetResponseStream()))
                    {
                        pageSource = sr.ReadToEnd();
                    }
            }
            catch (Exception ex)
            {
                if (ex.Message == "The remote server returned an error: (401) Unauthorized.")
                {
                    LogError("Bad login: ", ex);
                    MessageBox.Show(@"BluePumpkin username or password is incorrect.");
                }

                pageSource = string.Empty;
            }

            return pageSource;
        }

        private IEnumerable<AppointmentModel> WeeklySchedule(string pageSource)
        {
            //_logger.Info("WeeklySchedule(pageSource)");
            var weeklySchedule = new List<AppointmentModel>();

            var doc = new HtmlDocument();
            doc.LoadHtml(pageSource);

            HtmlNodeCollection days = doc.DocumentNode.SelectNodes("//*[@isnode='true']");

            foreach (HtmlNode day in days)
            {
                string aday = day.ChildNodes[1].InnerText;
                string aschedule = day.ChildNodes[3].InnerText;

                DateTime nday = DateTime.Parse(Regex.Match(aday, @"(\w+\,\s\w+\s\d+\,\s\d+)").Value);

                string schedule = Regex.Replace(aschedule, @"&nbsp;", "^");
                string[] nschedule = schedule.Split('^');

                foreach (string item in nschedule)
                {
                    if (item != string.Empty)
                    {
                        try
                        {
                            string[] nitem = Regex.Split(item, @"((\d+:\d\d\s\w+)(\s-\s)(\d+:\d\d\s\w+))");

                            var appt = new AppointmentModel();
                            appt.Date = nday;
                            if (!Regex.IsMatch(nitem[0], @"\w+\d+\:\d+\s\w+\s\-\s\d+\/\d+\/\d+\s\d+\:\d+\s\w+"))
                            {
                                appt.Subject = nitem[0];
                                appt.StartTime = DateTime.ParseExact(nday.Date.ToString("MM/dd/yyyy") + " " + nitem[2],
                                                                     "MM/dd/yyyy h:mm tt", CultureInfo.InvariantCulture);
                                appt.EndTime = DateTime.ParseExact(nday.Date.ToString("MM/dd/yyyy") + " " + nitem[4],
                                                                   "MM/dd/yyyy h:mm tt", CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                appt.Subject = Regex.Match(nitem[0], @"[a-zA-Z]+").Value;
                                appt.StartTime = DateTime.ParseExact(nday.Date.ToString("MM/dd/yyyy") + " 8:00 AM",
                                                                     "MM/dd/yyyy h:mm tt", CultureInfo.InvariantCulture);
                                appt.EndTime = DateTime.ParseExact(nday.Date.ToString("MM/dd/yyyy") + " 8:00 PM",
                                                                   "MM/dd/yyyy h:mm tt", CultureInfo.InvariantCulture);
                            }

                            weeklySchedule.Add(appt);
                        }
                        catch (Exception)
                        {
                            // Days off, including weekends, or if there is any funky formatting thats one off.
                        }
                    }
                }
            }
            return weeklySchedule;
        }

        public List<AppointmentModel> Update()
        {
            var result = new List<AppointmentModel>();
            //var pageSource = GetHtml();
            string pageSource = GetHtml();
            if (pageSource != string.Empty)
            {
                result = WeeklySchedule(pageSource) as List<AppointmentModel>;
            }
            return result;
        }

        #endregion
    }
}