
using HabitWise.Models;
using SQLite;

namespace HabitWise.Data
{
    public class HabitRepository
    {
        private readonly SQLiteAsyncConnection _db;
        private readonly TagRepository _tagRepository;

        public HabitRepository() 
        {
            Init().Wait();
            _tagRepository = new TagRepository(_db);
        }

        async Task Init()
        {
            if (_db is not null)
                return;

            _db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await _db.CreateTableAsync<Habit>();
        }

        public async Task<List<Habit>> GetHabitsAsync()
        {
            await Init();
            return await _db.Table<Habit>().ToListAsync();

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
    }
}
