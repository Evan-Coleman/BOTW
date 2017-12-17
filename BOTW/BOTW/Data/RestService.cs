using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BOTW.Models;
using Newtonsoft.Json;

namespace BOTW.Data
{
    public class RestService : IRestService
    {
        private HttpClient client;
        string apiKey = "0b613f3d5130e4f3ab42784c6266145a";

        private MovieInfo Movie;

        private string RestUrlPartOne = "https://api.themoviedb.org/3/search/movie?api_key=";
        private string RestUrlPartTwo = "&language=en-US&query=";
        private string RestUrlPartThree = "&page=1&include_adult=false";
        private string RestUrlComplete;

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<MovieInfo> RefreshDataAsync(string Name)
        {
            Movie = new MovieInfo();
            RestUrlComplete = RestUrlPartOne + apiKey + RestUrlPartTwo + Name + RestUrlPartThree;
            var uri = new Uri(string.Format(RestUrlComplete, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var MovieList = JsonConvert.DeserializeObject<Rootobject>(content);

                    Movie.Name = MovieList.results[0].title;
                    Movie.MovieDescription = MovieList.results[0].overview;
                    Movie.ReleaseDate = MovieList.results[0].release_date;
                    //Movie.Id = MovieList.results[0].id;
                    Movie.VoteCount = MovieList.results[0].vote_count;
                    Movie.VoteAverage = MovieList.results[0].vote_average;
                    Movie.PosterPath += MovieList.results[0].poster_path;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Movie;
        }

        public Task SaveMovieInfoAsync(MovieInfo Movie, bool isNewItem)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovieInfoAsync(string id)
        {
            throw new NotImplementedException();
        }


        #region API CLASSES
        public class Rootobject
        {
            public int page { get; set; }
            public int total_results { get; set; }
            public int total_pages { get; set; }
            public Result[] results { get; set; }
        }

        public class Result
        {
            public int vote_count { get; set; }
            public int id { get; set; }
            public bool video { get; set; }
            public float vote_average { get; set; }
            public string title { get; set; }
            public float popularity { get; set; }
            public string poster_path { get; set; }
            public string original_language { get; set; }
            public string original_title { get; set; }
            public int?[] genre_ids { get; set; }
            public string backdrop_path { get; set; }
            public bool adult { get; set; }
            public string overview { get; set; }
            public string release_date { get; set; }
        }
        #endregion
    }
}
