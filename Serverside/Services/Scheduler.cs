using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Serverside.Services {
    public class Scheduler : IDisposable {
        private bool disposed;

        private static Scheduler _instance;

        private List<Timer> timers = new List<Timer>();

        private Scheduler() { }

        public static Scheduler Instance => _instance ?? (_instance = new Scheduler());

        public void ScheduleTask(int hour, int min, double intervalInHour, Action task) {
            DateTime now = DateTime.UtcNow;
            DateTime firstRun = new DateTime(now.Year, now.Month, now.Day, hour, min, 0, 0, DateTimeKind.Utc);

            if (now > firstRun) {
                firstRun = firstRun.AddDays(1);
            }

            TimeSpan timeToGo = firstRun - now;

            if (timeToGo <= TimeSpan.Zero) {
                timeToGo = TimeSpan.Zero;
            }

            var timer = new Timer(x => {
                task.Invoke();
            }, null, timeToGo, TimeSpan.FromHours(intervalInHour));

            timers.Add(timer);
        }

        public static void IntervalInSeconds(int hour, int sec, double interval, Action task) {
            interval = interval / 3600;
            Instance.ScheduleTask(hour, sec, interval, task);
        }

        public static void IntervalInMinutes(int hour, int min, double interval, Action task) {
            interval = interval / 60;
            Instance.ScheduleTask(hour, min, interval, task);
        }

        public static void IntervalInHours(int hour, int min, double interval, Action task) {
            Instance.ScheduleTask(hour, min, interval, task);
        }

        public static void IntervalInDays(int hour, int min, double interval, Action task) {
            interval = interval * 24;
            Instance.ScheduleTask(hour, min, interval, task);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                foreach (var timer in timers) {
                    timer.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
