
using HabitWise.Models;
using HabitWise.Services;
using SQLite;
using System.Text.Json;

namespace HabitWise.Data
{
    public class HabitRepository
    {
        private SQLiteAsyncConnection _db;
        private readonly TagRepository _tagRepository;

        public HabitRepository(ConnectionService connectionService, TagRepository tagRepository)
        {
            
            _tagRepository = tagRepository;
            _db = connectionService._db;
            Init().Wait();
        }

        async Task Init()
        {
            if (_db is not null)
                return;

            await _db.CreateTableAsync<Habit>();
        }

        public async Task<List<Habit>> GetHabitsAsync()
        {
            await Init();
            var Habits = await _db.Table<Habit>().ToListAsync();

            foreach (var habit in Habits)
            {
                habit.Recurrence = JsonSerializer.Deserialize<RecurrenceRule>(habit.RecurrenceJson);
            }

            return Habits;

            // SQL queries are also possible
            //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public async Task<Habit> GetHabitAsync(int id)
        {
            await Init();
            return await _db.Table<Habit>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveHabitAsync(Habit habit)
        {
            await Init();
            if (habit.Id != 0)
            {
                return await _db.UpdateAsync(habit);
            }
            else
            {
                return await _db.InsertAsync(habit);
            }
        }

        public async Task<int> DeleteHabitAsync(Habit habit)
        {
            await Init();
            return await _db.DeleteAsync(habit);
        }

        public async Task<List<Tag>> GetTagsForHabitAsync(int habitId)
        {
            return await _tagRepository.GetTagsForHabitAsync(habitId);
        }

        public async Task<int> AddTagToHabitAsync(int habitId, int tagId)
        {
            return await _tagRepository.AddTagToHabitAsync(habitId, tagId);
        }

        public async Task<int> RemoveTagFromHabitAsync(int habitId, int tagId)
        {
            return await _tagRepository.RemoveTagFromHabitAsync(habitId, tagId);
        }

        public async Task<int> RemoveAllTagsFromHabitAsync(int habitId)
        {
            return await _tagRepository.RemoveAllTagsFromHabitAsync(habitId);
        }
    }
}
