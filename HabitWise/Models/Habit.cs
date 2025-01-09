using SQLite;

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

        public TimeSpan? ReminderTime { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime DateCompleted { get; set; }

        public string Tags { get; set; }

        [Ignore]
        public RecurrenceRule Recurrence { get; set; }

        public string RecurrenceJson 
        {
            get => Recurrence == null ? null : System.Text.Json.JsonSerializer.Serialize(Recurrence);
            set => Recurrence = System.Text.Json.JsonSerializer.Deserialize<RecurrenceRule>(value);
        }
    }
}
