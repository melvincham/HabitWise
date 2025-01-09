using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitWise.Models
{
    public enum RecurrenceFrequency
    {
        None,
        Daily,
        Weekly,
        Monthly,
        Yearly
    }

    public class RecurrenceRule
    {
        public RecurrenceFrequency Frequency { get; set; }

        public List<DayOfWeek> DaysOfWeek { get; set; } = new();

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? RepeatCount { get; set; }

        public int? interval { get; set; }
    }
}
