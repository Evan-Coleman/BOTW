using System.Collections.Generic;
using System.Threading.Tasks;
using BOTW.Models;
using SQLite;

namespace BOTW.Data
{
    public class MovieInfoDatabase
    {
        #region Fields
        private readonly SQLiteAsyncConnection database;
        #endregion

        #region Constructor
        public MovieInfoDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<MovieInfo>().Wait();
        }
        #endregion

        #region Methods
        public Task<List<MovieInfo>> GetMoviesAsync()

        {
            return database.Table<MovieInfo>().ToListAsync();
        }

        public Task<MovieInfo> GetMovieInfoasync(int id)
        {
            return database.Table<MovieInfo>().
                Where(i => i.ID == id).
                FirstOrDefaultAsync();
        }

        public Task<int> SaveMovieInfoAsync(MovieInfo movieInfo)
        {
            if (movieInfo.ID != 0)
            {
                return database.UpdateAsync(movieInfo);
            }
            else
            {
                return database.InsertAsync(movieInfo);
            }
        }

        public Task<int> DeleteMovieInfoAsync(MovieInfo movieInfo)
        {
            return database.DeleteAsync(movieInfo);
        }
        #endregion
    }
}
