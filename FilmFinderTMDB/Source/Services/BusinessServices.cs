using Newtonsoft.Json;
using FilmFinderTMDB.Source.Data.Model;
using FilmFinderTMDB.Source.AppConfiguration;
using Microsoft.Extensions.Configuration;

namespace FilmFinderTMDB.Source.Services
{
    /// <summary>
    /// /// Class responsible for handling business logic related to web API interactions.
    /// /// </summary>
    public class BusinessServices : IBusinessServices
    {
        private readonly string _baseUrl;
        private readonly string _apiKey;
        private readonly WebApiClient _webApiClient;
        private readonly IConfiguration _configuration;
        private readonly ApiConfigurationSetting _apiConfigurationSetting;

        public BusinessServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiConfigurationSetting = ConfigureApiBaseUri();
            _baseUrl = _apiConfigurationSetting.ApiBaseUri;
            _apiKey = _apiConfigurationSetting.ApiKey;
            _webApiClient = new WebApiClient(_apiKey);
        }

        private ApiConfigurationSetting ConfigureApiBaseUri()
        {
            var apiConfigurationSetting = _configuration.GetRequiredSection("AppSettings").Get<ApiConfigurationSetting>();
            return apiConfigurationSetting;

        }

    public async Task<GenreResponse> GetGenresAsync()
        {
            GenreResponse genere = new GenreResponse();

            try
            {
                string requestUri = $"{_baseUrl}/genre/list?api_key={_apiKey}";
                string serviceResult = await _webApiClient.GetData(requestUri);

                if (!string.IsNullOrWhiteSpace(serviceResult))
                {
                    var genreResponse = JsonConvert.DeserializeObject<GenreResponse>(serviceResult);
                    genere.Genres = genreResponse.Genres;
                    genere.IsSuccess = true;
                }
                else
                {
                    genere.IsSuccess = false;
                    genere.Message = serviceResult;
                }
            }
            catch (System.Net.WebException ex)
            {
                genere.IsSuccess = false;
                genere.Message = ex.Message;
            }
            catch (Exception ex)
            {
                genere.IsSuccess = false;
                genere.Message = ex.Message;
            }

            return genere;
        }

        public async Task<Movie> GetMoviesAsync(int movieId)
        {
            Movie movie = new Movie();

            try
            {
                string requestUri = $"{_baseUrl}/movie/{movieId}?api_key={_apiKey}";
                string serviceResult = await _webApiClient.GetData(requestUri);

                if (!string.IsNullOrWhiteSpace(serviceResult))
                {
                    movie = JsonConvert.DeserializeObject<Movie>(serviceResult);
                    movie.IsSuccess = true;
                }
                else
                {
                    movie.IsSuccess = false;
                    movie.Message = serviceResult;
                }
            }
            catch (System.Net.WebException ex)
            {
                movie.IsSuccess = false;
                movie.Message = ex.Message;
            }
            catch (Exception ex)
            {
                movie.IsSuccess = false;
                movie.Message = ex.Message;
            }

            return movie;
        }

        public async Task<MovieResponse> GetMoviesBasedOnSearchAsync(string searchFilmName, int page)
        {
            MovieResponse movie = new MovieResponse();

            try
            {
                string requestUri = $"{_baseUrl}/search/movie?api_key={_apiKey}&query={searchFilmName}&page={page}";
                string serviceResult = await _webApiClient.GetData(requestUri);

                if (!string.IsNullOrWhiteSpace(serviceResult))
                {
                    movie = JsonConvert.DeserializeObject<MovieResponse>(serviceResult);
                    movie.IsSuccess = true;
                }
                else
                {
                    movie.IsSuccess = false;
                    movie.Message = serviceResult;
                }
            }
            catch (System.Net.WebException ex)
            {
                movie.IsSuccess = false;
                movie.Message = ex.Message;
            }
            catch (Exception ex)
            {
                movie.IsSuccess = false;
                movie.Message = ex.Message;
            }

            return movie;
        }

        public async Task<MovieCredits> GetMoviesCreditsAsync(int movieId)
        {
            MovieCredits movie = new MovieCredits();

            try
            {
                string requestUri = $"{_baseUrl}/movie/{movieId}/credits?api_key={_apiKey}";
                string serviceResult = await _webApiClient.GetData(requestUri);

                if (!string.IsNullOrWhiteSpace(serviceResult))
                {
                    movie = JsonConvert.DeserializeObject<MovieCredits>(serviceResult);
                    movie.IsSuccess = true;
                }
                else
                {
                    movie.IsSuccess = false;
                    movie.Message = serviceResult;
                }
            }
            catch (System.Net.WebException ex)
            {
                movie.IsSuccess = false;
                movie.Message = ex.Message;
            }
            catch (Exception ex)
            {
                movie.IsSuccess = false;
                movie.Message = ex.Message;
            }

            return movie;
        }


    }
}

