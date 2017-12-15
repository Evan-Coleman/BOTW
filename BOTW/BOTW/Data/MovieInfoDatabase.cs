using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOTW.Models;
using Prism.Services;
using SQLite;
using Xamarin.Forms;

namespace BOTW.Data
{
    public class MovieInfoDatabase
    {
        private readonly SQLiteAsyncConnection database;

        public MovieInfoDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<MovieInfo>().Wait();
        }


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
            if (movieInfo.ID == 0)
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
    }
}
