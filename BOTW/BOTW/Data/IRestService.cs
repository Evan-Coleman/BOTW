using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using BOTW.Models;

namespace BOTW.Data
{
    public interface IRestService
    {
        Task<MovieInfo> RefreshDataAsync(string Name);
        Task SaveMovieInfoAsync(MovieInfo Movie, bool isNewItem);
        Task DeleteMovieInfoAsync(string id);
    }
}
