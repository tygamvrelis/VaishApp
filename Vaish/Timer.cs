using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Vaish
{
    public class Timer : INotifyPropertyChanged
    {
        #region Properties
        public DateTime MyDate { get; set; }

        private string myWaitString;
        public string MyWaitString
        {
            get { return TimerToString(); }
            set
            {
                myWaitString = value;
                OnPropertyChanged("MyWaitString");
            }
        }
        public TimeSpan MyWait
        {
            get { return MyDate.Subtract(DateTime.Now); }
        }
        #endregion

        #region Constructor
        public Timer(DateTime date)
        {
            CultureInfo cultureInfo = CultureInfo.InvariantCulture;
            MyDate = date;
            makeTimer();
        }
        #endregion

        #region Helper method(s)
        public string TimerToString()
        {
            int days = MyWait.Days;
            int hours = MyWait.Hours;
            int minutes = MyWait.Minutes;
            int seconds = MyWait.Seconds;
            int ms = MyWait.Milliseconds;

            return string.Format("{0} days, {1} hours, {2} minutes, {3} seconds",
                days, hours, minutes, seconds);
        }

        private void makeTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer(DispatcherPriority.Render);
            dispatcherTimer.Tick += new EventHandler(timer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            MyWaitString = TimerToString();
        }
        #endregion

        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
