using System.Threading.Tasks;
using BOTW.Models;

namespace BOTW.Data
{
    public class MovieInfoManager
    {
        private IRestService restService;

        public MovieInfoManager(IRestService service)
        {
            restService = service;
        }

        public Task<MovieInfo> GetTasksAsync(string Name)
        {
            return restService.RefreshDataAsync(Name);
        }

        public Task SaveTaskAsync(MovieInfo item, bool isNewItem = false)
        {
            return restService.SaveMovieInfoAsync(item, isNewItem);
        }

        public Task DeleteTaskAsync(MovieInfo item)
        {
            return restService.DeleteMovieInfoAsync("placeholder");
        }
    }
}
