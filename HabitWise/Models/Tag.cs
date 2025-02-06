using SQLite;
using System.Text.Json.Serialization;

namespace HabitWise.Models
{
    public class Tag
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50), NotNull]
        public string Title { get; set; }
        public string Color { get; set; }

        [JsonIgnore]
        public Color DisplayColor
        {
            get
            {
                return Microsoft.Maui.Graphics.Color.FromArgb(Color);
            }
        }

        [JsonIgnore]
        public bool IsSelected { get; set; }
    }
}
