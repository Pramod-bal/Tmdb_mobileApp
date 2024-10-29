using System;
using Newtonsoft.Json;

namespace FilmFinderTMDB.Source.Data.Model
{
    public class Movie : BaseResponseModel
    {
        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("belongs_to_collection")]
        public object BelongsToCollection { get; set; }

        [JsonProperty("budget")]
        public int Budget { get; set; }

        [JsonProperty("genre_ids")]
        public List<int> GenreIds { get; set; }

        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }

        [JsonProperty("homepage")]
        public string Homepage { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }

        [JsonProperty("origin_country")]
        public List<string> OriginCountry { get; set; }

        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("production_companies")]
        public List<ProductionCompany> ProductionCompanies { get; set; }

        [JsonProperty("production_countries")]
        public List<ProductionCountry> ProductionCountries { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("revenue")]
        public long Revenue { get; set; }

        [JsonProperty("runtime")]
        public int Runtime { get; set; }

        [JsonProperty("spoken_languages")]
        public List<SpokenLanguage> SpokenLanguages { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }

        public string PosterImageUrl => string.IsNullOrEmpty(PosterPath)
            ? "person.svg"
            : $"https://image.tmdb.org/t/p/w500{PosterPath}";

        public string BackdropImageUrl => string.IsNullOrEmpty(BackdropPath)
           ? "person.svg"
           : $"https://image.tmdb.org/t/p/w500{BackdropPath}";

        public string GenersInfo { get; set; }

        public string ReleaseDateInfo { get; set; }
    }

    public class MovieResponse : BaseResponseModel
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("results")]
        public List<Movie> Results { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
    }

}

