using HabitWise.Data;
using SQLite;

namespace HabitWise.Services
{
    public class ConnectionService
    {
        public SQLiteAsyncConnection _db;
        public ConnectionService() 
        {
            _db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }
    }
}
