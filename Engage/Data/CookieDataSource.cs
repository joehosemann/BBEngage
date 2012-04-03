using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using log4net;

namespace Engage.Data
{
    public class CookieDataSource
    {
        public string Cookie
        {
            get { return Properties.Settings.Default.Cookie; }
        }
        private DateTime _cookieDate
        {
            get { return Properties.Settings.Default.CookieDate; }
        }

        public CookieDataSource()
        {
            // Conditions to reset Cookie:
            // 1) New day
            // 2) Cookie string is empty
            // 3) Login failed.

            if (_cookieDate.Date != DateTime.Now.Date || Cookie == string.Empty )
            {
                Properties.Settings.Default.Cookie = GetCookie();
                Properties.Settings.Default.CookieDate = DateTime.Now;
                Properties.Settings.Default.Save();
            }

            //// If no Cookie is stored
            //if (string.IsNullOrEmpty(Cookie))
            //{
            //    // If GetCookie has data
            //    if (!string.IsNullOrEmpty(GetCookie()))
            //    {
           
            //    }
            //    else
            //    {
            //        // TODO: Write in retry in 5 minutes.
            //    }
            //}
        }


        #region Properties

        private string Username { get { return Properties.Settings.Default.Username; } }
        private string Password { get { return Properties.Settings.Default.Password; } }
        private string UserLoginUrl = Properties.Settings.Default.UserLoginUrl;

        #endregion

        #region Logging

        public void LogError(string message, Exception e)
        {
            //get logger
            ILog logger = LogManager.GetLogger(typeof(CookieDataSource));

            //set user to log4net context, so we can use %X{user} in the appenders
            if (Username != string.Empty)
                MDC.Set("username", Username);

            if (logger.IsErrorEnabled)
                logger.Error(message, e); //now log error
        }

        public void LogInfo(string message)
        {
            //get logger
            ILog logger = LogManager.GetLogger(typeof(CookieDataSource));

            //set user to log4net context, so we can use %X{user} in the appenders
            if (Username != string.Empty)
                MDC.Set("username", Username);

            if (logger.IsInfoEnabled)
                logger.Info(message); //now log error
        }

        #endregion 

        #region Methods

        public void Reset()
        {
            Properties.Settings.Default.Cookie = string.Empty;
            Properties.Settings.Default.Save();

        }




        /// <summary>
        /// Logs into BluePumpkin and saves a cookie instance.
        /// Should only be called once per Outlook session.
        /// </summary>
        private string GetCookie()
        {
            LogInfo("GetCookie");


            var cookie = string.Empty;

            

            string formParams = string.Format("browserCheckEnabled={0}&username={1}&password={2}&pageModelType={3}&pageDirty={4}&pageAction={5}", "false", Username, Password, "0", "false", "Login");
            //string formParams = string.Format("browserCheckEnabled={0}&username={1}&password={2}&pageModelType={3}&pageDirty={4}&pageAction={5}", "false", "JosephHo", "magnavox", "0", "false", "Login");
            WebRequest req = WebRequest.Create(UserLoginUrl);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(formParams);
            req.ContentLength = bytes.Length;
            try
            {
                using (Stream os = req.GetRequestStream())
                {
                    os.Write(bytes, 0, bytes.Length);
                }
                WebResponse resp = req.GetResponse();
                if (resp != null) cookie = resp.Headers["Set-cookie"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            const string cookieValidator = "BP_SUITE_JSESSIONID.+path.+USER_ID.+expires.+LANGUAGE_ID.+expires.+";
            if (!Regex.Match(cookie, cookieValidator).Success)
            {
                cookie = string.Empty;
            }

            return cookie;
        }

        #endregion
    }
}
