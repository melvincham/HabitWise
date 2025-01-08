using SQLite;

namespace HabitWise.Models
{
    public class Tag
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50), NotNull]
        public string Title { get; set; }
        public string Color { get; set; }
    }
}
