using System;
namespace Psycho.DAL.Core.Domain
{
    public class Time : IComparable
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public Time() { }

        public Time(uint h, uint m, uint s)
        {
            if (h > 23 || m > 59 || s > 59)
            {
                throw new ArgumentException("Invalid time specified");
            }
            Hours = (int)h; Minutes = (int)m; Seconds = (int)s;
        }

        public Time(DateTime dt)
        {
            Hours = dt.Hour;
            Minutes = dt.Minute;
            Seconds = dt.Second;
        }

        public override string ToString()
        {
            return String.Format(
                "{0:00}:{1:00}:{2:00}",
                this.Hours, this.Minutes, this.Seconds);
        }

        public int CompareTo(object obj)
        {
            Time t = obj as Time;

            if(this.Hours < t.Hours)
            {
                return -1;
            }
            else if(this.Hours == t.Hours)
            {
                if(this.Minutes < t.Minutes)
                {
                    return -1;
                }
                else if(this.Minutes == t.Minutes)
                {
                    if(this.Seconds < t.Seconds)
                    {
                        return -1;
                    }
                    else if(this.Seconds == t.Seconds)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }
    }

    public enum Day : byte
    {
        Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
    }

    public class WorkSchedule
    {
        public int Id { get; set; }

        public int PsychologistId { get; set; }
        public virtual Psychologist Psychologist { get; set; }

        public Day Day { get; set; }

        public Time StartTime { get; set; }

        public Time EndTime { get; set; }
    }
}
