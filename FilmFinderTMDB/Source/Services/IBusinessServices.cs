using FilmFinderTMDB.Source.Data.Model;

namespace FilmFinderTMDB.Source.Services
{
    public interface IBusinessServices
    {
        Task<GenreResponse> GetGenresAsync();
        Task<Movie> GetMoviesAsync(int movieId);
        Task<MovieResponse> GetMoviesBasedOnSearchAsync(string searchFilmName, int page);
        Task<MovieCredits> GetMoviesCreditsAsync(int movieId);
    }
}