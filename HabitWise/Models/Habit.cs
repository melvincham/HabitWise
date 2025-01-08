using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitWise.Models
{
    public class Habit
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string SmallestUnit { get; set; }

        public string SmallestUnitName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public RecurrenceType Recurrence { get; set; }

        public TimeSpan? ReminderTime { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime DateCompleted { get; set; }

        public string Tags { get; set; }
    }
}
