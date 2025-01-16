using HabitWise.Models;
using HabitWise.Services;
using SQLite;

namespace HabitWise.Data
{
    public class TagRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public TagRepository(ConnectionService connectionService)
        {
            _db = connectionService._db;
            _db.CreateTableAsync<Tag>().Wait();
            _db.CreateTableAsync<HabitTag>().Wait();
        }

        public async Task<int> SaveTagAsync(Tag tag)
        {
            if (tag.Id == 0)
                return await _db.InsertAsync(tag);
            else
                return await _db.UpdateAsync(tag);
        }

        public async Task<int> DeleteTagAsync(int tagId)
        {
            var result = await _db.DeleteAsync<Tag>(tagId);

            if (result > 0)
                await _db.ExecuteAsync("DELETE FROM HabitTag WHERE TagId = ?", tagId);

            return result;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _db.Table<Tag>().ToListAsync();
        }

        public async Task<List<Tag>> GetTagsForHabitAsync(int habitId)
        {
            var habitTagIds = await _db.Table<HabitTag>().Where(ht => ht.HabitId == habitId).ToListAsync();
            var tagIds = habitTagIds.ConvertAll(ht => ht.TagId);
            return await _db.Table<Tag>().Where(t => tagIds.Contains(t.Id)).ToListAsync();
        }

        public async Task<int> AddTagToHabitAsync(int habitId, int tagId)
        {
            var habitTag = new HabitTag { HabitId = habitId, TagId = tagId };

            var existing = await _db.Table<HabitTag>().Where(ht => ht.HabitId == habitId && ht.TagId == tagId).FirstOrDefaultAsync();
            if (existing != null)
                return 0;

            return await _db.InsertAsync(habitTag);
        }

        public async Task<int> RemoveTagFromHabitAsync(int habitId, int tagId)
        {
            return await _db.ExecuteAsync("DELETE FROM HabitTag WHERE HabitId = ? AND TagId = ?", habitId, tagId);
        }

        public async Task<int> RemoveAllTagsFromHabitAsync(int habitId)
        {
            return await _db.ExecuteAsync("DELETE FROM HabitTag WHERE HabitId = ?", habitId);
        }
    }
}
