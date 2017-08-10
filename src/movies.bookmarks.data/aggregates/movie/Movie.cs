using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace movies.bookmarks.domain.aggregates
{
    //Get Recommendations
    public class Movie
    {
        public bool Adult { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("genre_ids")]
        public ICollection<int> GenreIds { get; set; }
        public int Id { get; set; }
        public string OriginalLanguage { get; set; }
        public string OriginalTitle { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PosterPath { get; set; }
        public double Popularity { get; set; }
        public string Title { get; set; }
        public bool HasVideo { get; set; }
        public double VoteAverage { get; set; }
        public int VoteCount { get; set; }
    }
}
