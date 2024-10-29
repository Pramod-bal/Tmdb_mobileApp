using System;
using Newtonsoft.Json;

namespace FilmFinderTMDB.Source.Data.Model
{
    public class Genre
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class GenreResponse : BaseResponseModel
    {
        public List<Genre> Genres { get; set; }
    }

    public class ProductionCompany
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("logo_path")]
        public string LogoPath { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("origin_country")]
        public string OriginCountry { get; set; }
    }

    public class ProductionCountry
    {
        [JsonProperty("iso_3166_1")]
        public string Iso3166_1 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class SpokenLanguage
    {
        [JsonProperty("iso_639_1")]
        public string Iso639_1 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("english_name")]
        public string EnglishName { get; set; }
    }

}

