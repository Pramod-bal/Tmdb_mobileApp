using System;
using Newtonsoft.Json;

namespace FilmFinderTMDB.Source.Data.Model
{
    public class Cast
    {
        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("gender")]
        public int Gender { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("known_for_department")]
        public string KnownForDepartment { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("original_name")]
        public string OriginalName { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }

        //public string ProfilePathImageUrl => string.IsNullOrEmpty(ProfilePath) ? "person.svg" : $"https://image.tmdb.org/t/p/w500{ProfilePath}";
        public string ProfilePathImageUrl => string.IsNullOrEmpty(ProfilePath) ? "https://cdn-icons-png.flaticon.com/512/149/149071.png" : $"https://image.tmdb.org/t/p/w500{ProfilePath}";

        [JsonProperty("cast_id")]
        public int CastId { get; set; }

        [JsonProperty("character")]
        public string Character { get; set; }

        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }
    }
}

