namespace Home.Core.Models.Schedules
{
    using System;
    using System.Diagnostics;

    public enum Period
    {
        None = 0, // One Time.
        Daily = 100,
        Weekly = 700,
    }

    public abstract record ItemSchedule
    {
        protected DateTimeOffset firstStart;
        protected DateTimeOffset? lastStart;
        protected DateTimeOffset? expiresAt;
        protected bool running;
        protected Period Window;
        protected int Interval;
        protected int Hour;

        protected ItemSchedule Start(DateTimeOffset? start = null)
        {
            var now = start?.UtcDateTime ?? DateTime.UtcNow;

            return this with
            {
                lastStart = now,
                expiresAt = now.AddHours(3),
                running = true
            };
        }

        protected ItemSchedule Stop()
        {
            //TODO: Auto expire?
            return this with
            {
                expiresAt = null,
                running = false
            };
        }

        protected DateTimeOffset? GetNextIfAny(DateTimeOffset? input = null)
        {
            var now = input ?? firstStart.AddMinutes(1);
            if (running) // && (DateTime.UtcNow < (expiresAt ?? DateTimeOffset.MaxValue)))
            {
                return null;
            }

            if (input < firstStart || !lastStart.HasValue)
            {
                return firstStart;
            }

            var start = lastStart.Value.UtcDateTime.Date.AddHours(24 + this.Hour);

            return Window switch
            {
                Period.None => lastStart == null ? firstStart : null,
                Period.Daily => start,
                Period.Weekly => GetNextWeekly(start).AddHours(Hour),
                _ => firstStart
            };
        }

        private DateTimeOffset GetNextWeekly(DateTimeOffset start)
        {
            Debug.Assert((Interval >= 0) && (Interval < 7));

            var counter = start.UtcDateTime.Date;
            while (Interval != (int)counter.DayOfWeek)
            {
                counter = counter.AddDays(1);
            }

            return counter;
        }
    }
}