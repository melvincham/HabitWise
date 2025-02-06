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

        private byte[] _imojiData;
        public byte[] ImojiData
        {
            get => _imojiData;
            set
            {
                _imojiData = value;
                _imoji = LoadImageFromBytes(value);
            }
        }

        private ImageSource _imoji;

        [Ignore]
        public ImageSource Imoji
        {
            get => _imoji;
            set
            {
                _imoji = value;
                _imojiData = ConvertImageToByteArray(value);
            }
        }

        public string RecurrenceJson 
        {
            get => Recurrence == null ? null : System.Text.Json.JsonSerializer.Serialize(Recurrence);
            set => Recurrence = System.Text.Json.JsonSerializer.Deserialize<RecurrenceRule>(value);
        }

        private byte[] ConvertImageToByteArray(ImageSource imageSource)
        {
            if (imageSource is FileImageSource fileImageSource)
            {
                var path = fileImageSource.File;
                if (File.Exists(path))
                    return File.ReadAllBytes(path);
            }
            return null;
        }

        private ImageSource LoadImageFromBytes(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            return ImageSource.FromStream(() => new MemoryStream(imageData));
        }
    }
}
