using System;

namespace Engage.Model
{
    public class AppointmentModel
    {
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }
        public string ShortDate
        {
            get { return Date.ToString("yyyy-MM-dd"); }
        }
        public string ShortStartTime
        {
            get { return StartTime.ToString("h:mm tt"); }
        }
        public string ShortEndTime
        {
            get { return StartTime.ToString("h:mm tt"); }
        }
    }
}
