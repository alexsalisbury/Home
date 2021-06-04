namespace Home.Core.Models.Schedules
{
    using System;

    public record SingleSchedule : ItemSchedule
    {
        public SingleSchedule(DateTimeOffset firstRun)
        {
            firstStart = firstRun;
            running = false;
            Window = Period.None;
            Interval = 1;
        }

        public DateTimeOffset? GetNext(DateTimeOffset? input = null)
        {
            return this.lastStart == null ? this.GetNextIfAny(input ?? this.lastStart) : null;
        }

        public SingleSchedule StartTask(DateTimeOffset? start = null)
        {
            return (SingleSchedule)this.Start(start);
        }

        public SingleSchedule Check(DateTimeOffset input)
        {
            if (input.UtcDateTime > (this.expiresAt ?? DateTimeOffset.MaxValue))
            {
                return (SingleSchedule)this.Stop();
            }

            return this;
        }

        public SingleSchedule StopTask()
        {
            return (SingleSchedule)this.Stop();
        }
    }
}