using SQLite;
using System.Linq.Expressions;

namespace Essential_Lib.API
{
    public static class LocalDatabaseAPIs
    { 
        private static  SQLiteAsyncConnection _connection;
 
        public const SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // Database is encrybted
            SQLiteOpenFlags.ProtectionComplete | 
            // enable multi-threaded database access
            SQLiteOpenFlags.FullMutex |
            SQLiteOpenFlags.SharedCache;

        /// <summary>
        /// Insert item to local database.
        /// </summary>
        public static async Task<int> AddItemAsync<T>(this T item) where T : new()
        {
            var IsInit = await SetUpDb<T>();
            if (IsInit)
                return await _connection.InsertAsync(item);  
            else
                return 0;
        }

        public static async Task<int>  AddListOfItemsAsync<T>(this IEnumerable<T> items) where T : new()
        { 
            var IsInit = await SetUpDb<T>();
            if (IsInit)
                return await _connection.InsertAllAsync(items, false);
            else
                return 0;
             
        } 
        /// <summary>
        /// Delete item to local database.
        /// </summary>
        public static async Task<int> DeleteItemAsync<T>(this T item) where T : new()
        {
            var IsInit = await SetUpDb<T>();
            if (IsInit)
                return await _connection.DeleteAsync(item);
            else
                return 0; 
        }
        public static async Task<int> DeleteSpecificItemsAsync<T>(Expression<Func<T, bool>> filter) where T : new()
        {
            var IsInit = await SetUpDb<T>();
            if (IsInit)
                return await _connection.Table<T>().DeleteAsync(filter);
            else
                return 0; 
        }
        public static async Task<int> DeleteListOfItemsAsync<T>(this IEnumerable<T> items) where T : new()
        {
            var IsInit = await SetUpDb<T>();
            int count = 0;
            if (IsInit)
            {
                foreach (var item in items)
                { 
                    count += await DeleteItemAsync(item); 
                } 
            }
            return count; 
        }
        public static async Task<int> DeleteTableAsync<T>() where T : new()
        {
            return await DropTableAsync<T>();
        }
       
        public static async Task<List<T>?> GetAllItemsAsync<T>() where T : new()
        {
            var IsInit = await SetUpDb<T>();
            if (IsInit)
                return await _connection.Table<T>().ToListAsync();
            else
                return default;
             
        }
        public static async Task<List<T>?> GetSpecificItemsNumberAsync<T>(this int take, Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>? orderby = null, Expression<Func<T, object>>? orderbyDescending = null) where T : new()
        {
            var IsInit = await SetUpDb<T>();
            if (IsInit)
            {
                var query = _connection.Table<T>();

                if (orderby != null)
                    query = query.OrderBy(orderby);
                if (orderbyDescending != null)
                    query = query.OrderByDescending(orderbyDescending);
                if (filter != null)
                    query = query.Where(filter);
                return await query.Take(take).ToListAsync();

            }
            else
                return default; 
        }
        public static async Task<List<T>?> GetSpecificLastItemsNumberAsync<T>(this int takelast, Expression<Func<T, bool>>? filter = null) where T : new()
        {
            var IsInit = await SetUpDb<T>();
            if (IsInit)
            {
                var count = await CountItemsAsync<T>() - takelast;
                if(filter!=null)
                    return await _connection.Table<T>().Where(filter).Skip(count < 0 ? 0 : count).ToListAsync();
                else
                    return await _connection.Table<T>().Skip(count < 0 ? 0 : count).ToListAsync();
            }
            else
                return default; 
        }
        /// <summary>
        /// Delete the entire table, this is irrevocable action.
        /// </summary>
        /// <typeparam name="T">T: The Table Type</typeparam>
        /// <returns></returns>
        public static async Task<int> DropTableAsync<T>() where T : new()
        {
            var IsInit = await SetUpDb<T>();
            if (IsInit)
                return await _connection.DropTableAsync<T>();
            else
                return default; 
        }

       
        public static async Task<int> CountItemsAsync<T>() where T : new()
        {
            var IsInit = await SetUpDb<T>();
            if (IsInit)
                return await _connection.Table<T>().CountAsync();
            else
                return default; 
        }
        public static async Task<int> CountItemsAsync<T>(Expression<Func<T, bool>> filter) where T : new()
        {
            var IsInit = await SetUpDb<T>();
            if (IsInit)
                return await _connection.Table<T>().Where(filter).CountAsync();
            else
                return default;
        }
        public static async Task<T> GetTheMaxValue<T, U>(Expression<Func<T, bool>> filter,Expression<Func<T, U>> orderExp) where T : new()
        {
            var IsInit = await SetUpDb<T>();
            if (IsInit)
                return await _connection.Table<T>().Where(filter).OrderByDescending(orderExp).FirstOrDefaultAsync();
            else
                return default;
        }
        public static async Task<T> GetTheMinValue<T,U>(Expression<Func<T, bool>> filter, Expression<Func<T, U>> orderExp) where T : new()
        {
            var IsInit = await SetUpDb<T>();
            if (IsInit)
                return await _connection.Table<T>().Where(filter).OrderBy(orderExp).FirstOrDefaultAsync();
            else
                return default;
        }
        public static async Task<List<T>?> GetSpecificItemAsync<T>(Expression<Func<T, bool>> filter, Expression<Func<T, object>>? orderby = null, Expression<Func<T, object>>? orderbyDescending = null) where T : new()
        {
            List<T> list = [];
            var IsInit = await SetUpDb<T>();
            if (IsInit)
            {
                //if (orderby == null)
                //    return await _connection.Table<T>().Where(filter).ToListAsync();
                //else
                //    return await _connection.Table<T>().OrderBy(orderby).Where(filter).ToListAsync();

                var query = _connection.Table<T>();

                if (orderby != null)
                    query = query.OrderBy(orderby);
                if(orderbyDescending!=null)
                    query = query.OrderByDescending(orderbyDescending);
                if (filter != null)
                    query = query.Where(filter);

                list =  await query.ToListAsync();
            }
            return list;
        }

        public static async Task<T?> GetFirstItemAsync<T>(Expression<Func<T, bool>> filter) where T : new()
        {
            var IsInit = await SetUpDb<T>();
            if (IsInit)
                return await _connection.Table<T>().Where(filter).FirstOrDefaultAsync();
            else
                return default; 
        }
        public static async Task<T?> GetLastItemAsync<T>(Expression<Func<T, object>>? orderby, Expression<Func<T, bool>> filter) where T : new()
        {
            var IsInit = await SetUpDb<T>();
            if (IsInit)
                return await _connection.Table<T>().OrderByDescending(orderby).FirstOrDefaultAsync(filter);
            else
                return default;
        }

        private static async Task InitDB()
        {
            if (_connection == null)
            {
                string? databaseFileName = Preferences.Get("DBName",null) ;
                 
                if (!string.IsNullOrWhiteSpace(databaseFileName))
                {
                    string dbPath = Path.Combine(FileSystem.AppDataDirectory, databaseFileName + ".db3");
                    _connection = new SQLiteAsyncConnection(dbPath, Flags);
                    await _connection.EnableWriteAheadLoggingAsync();
                }

            }
        }
        public static async Task<bool> SetUpDb<T>() where T : new()
        {
            await InitDB();
                await _connection.CreateTableAsync<T>();

            //            if (_connection != null && !_connection.TableMappings.Any(a => a.TableName == nameof(T)))
            //#if DEBUG
            //                await _connection.CreateTableAsync<T>();
            //#else
            //                try
            //                {
            //                    await _connection.CreateTableAsync<T>(); 
            //                }
            //                catch (Exception ex)
            //                { 
            //                    ex.DisplayExecptionAlert();
            //                    return false;
            //                } 
            //#endif
            return _connection != null; 
        }
        public static async Task DeleteDB() 
        {
            await _connection.CloseAsync();
            var path = _connection?.DatabasePath;
            if(File.Exists(path))
                File.Delete(path); 
            await InitDB();
        }
        /// <summary>
        /// Update item to local database.
        /// </summary>
        public static async Task<int> UpdateItemAsync<T>(this T item) where T : new()
        {
            var IsInit = await SetUpDb<T>();
            if (IsInit && item !=null)
                return await _connection.UpdateAsync(item);
            else
                return default; 
        }

        public static async Task<int> UpdateListOfItemsAsync<T>(this IEnumerable<T> items) where T : new()
        {
            var IsInit = await SetUpDb<T>();
            if (IsInit)
                return await _connection.UpdateAllAsync(items, false);
            else
                return default; 
        }

    }
} 
