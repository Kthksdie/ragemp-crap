using System;
using System.Collections.Generic;
using System.Text;

namespace Serverside.Services {
    public static class ScheduleInterval {
        public static void InSeconds(int hour, int sec, double interval, Action task) {
            interval = interval / 3600;
            Scheduler.Instance.ScheduleTask(hour, sec, interval, task);
        }

        public static void InMinutes(int hour, int min, double interval, Action task) {
            interval = interval / 60;
            Scheduler.Instance.ScheduleTask(hour, min, interval, task);
        }

        public static void InHours(int hour, int min, double interval, Action task) {
            Scheduler.Instance.ScheduleTask(hour, min, interval, task);
        }

        public static void InDays(int hour, int min, double interval, Action task) {
            interval = interval * 24;
            Scheduler.Instance.ScheduleTask(hour, min, interval, task);
        }
    }
}
