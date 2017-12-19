using SQLite;
using System.Collections.Generic;

namespace BOTW.Models
{
    public class MovieInfo
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }
        public string ReleaseDate { get; set; }
        public string MovieDescription { get; set; }
        public string PosterPath { get; set; } = "https://image.tmdb.org/t/p/w185";
        public int VoteCount { get; set; }
        public float VoteAverage { get; set; }
    }
}
