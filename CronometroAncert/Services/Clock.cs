using CronometroAncert.Services.Interfaces;
using System;
using System.Threading;

namespace CronometroAncert
{
    public class Clock : IClock
    {
        Thread clockThread;
        bool clockWorking = true;
        private long storedTime = 0;
        public long StoredTime
        {
            get
            {
                return this.storedTime;
            }

            set
            {
                if (value != this.storedTime)
                {
                    this.storedTime = value;
                }
            }
        }

        public void StartClock()
        {
            clockThread = new Thread(ClockThread);
            clockWorking = true;
            clockThread.Start();
        }

        public void PauseClock()
        {
            clockWorking = false;
        }

        public void StopClock()
        {
            clockWorking = false;
            StoredTime = 0;
        }

        //Es facilmente expandible con otros metodos que devuelvan el valor en Date o Tick
        public string GetClockTime()
        {
            return TicksToString(StoredTime);
        }

        #region private methods

        private void ClockThread()
        {
            long clockThreadBaseTime = DateTime.Now.Ticks;
            clockThreadBaseTime = clockThreadBaseTime - StoredTime;
            while (clockWorking)
            {
                StoredTime = DateTime.Now.Ticks - clockThreadBaseTime;
                Thread.Sleep(500);
            }
        }

        private string TicksToString(long ticks)
        {
            DateTime dateFromTicks = new DateTime(ticks);
            TimeSpan timeElapsed = dateFromTicks.Subtract(DateTime.MinValue);
            var totalHours = (long)timeElapsed.TotalHours;

            return $"{(totalHours.ToString().Length < 2 ? "0" + totalHours : totalHours.ToString())}" +
                $":{(dateFromTicks.Minute.ToString().Length < 2 ? "0" + dateFromTicks.Minute : dateFromTicks.Minute.ToString())}" +
                $":{(dateFromTicks.Second.ToString().Length < 2 ? "0" + dateFromTicks.Second : dateFromTicks.Second.ToString())}";
        }

        #endregion
    }
}
