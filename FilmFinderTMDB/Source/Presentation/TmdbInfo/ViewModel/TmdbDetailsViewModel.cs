//using System.Collections.ObjectModel;
//using System.Globalization;
//using CommunityToolkit.Mvvm.ComponentModel;
//using FilmFinderTMDB.Source.Data.Model;
//using FilmFinderTMDB.Source.Presentation.NavigationService;
//using FilmFinderTMDB.Source.Services;

//namespace FilmFinderTMDB.Source.Presentation.TmdbInfo.ViewModel
//{
//	public partial class TmdbDetailsViewModel : BaseViewModel, IQueryAttributable
//    {
//        private IBusinessServices _businessServices;
//        private INavigationService _navigationService;
//       // private readonly Task A = null!;

//        [ObservableProperty]
//        string backdropImageUrl, posterImageUrl, releaseDateInfo, genersInfo, overView;

//        [ObservableProperty]
//        private ObservableCollection<Cast> casts;

//        [ObservableProperty]
//        private ObservableCollection<Crew> crews;

//        public TmdbDetailsViewModel(INavigationService navigationService, IBusinessServices businessServices)
//		{
//            _businessServices = businessServices;
//            _navigationService = navigationService;
//            Casts = new ObservableCollection<Cast>();
//            Crews = new ObservableCollection<Crew>();
//        }

//        public void ApplyQueryAttributes(IDictionary<string, object> query)
//        {
//            if (query.ContainsKey("MovieDetails"))
//            {
//                var movie = query["MovieDetails"] as Movie;
//                if (movie.Id != null)
//                {
//                    _ = MovieDetailsInfo(movie);
//                }
//            }
//        }

//        private async Task MovieDetailsInfo(Movie movie)
//        {
//            try
//            {
//                BackdropImageUrl = movie.BackdropImageUrl;
//                Title = movie.Title;
//                PosterImageUrl = movie.PosterImageUrl;
//                ReleaseDateInfo = movie.ReleaseDateInfo;
//                GenersInfo = movie.GenersInfo;
//                OverView = movie.Overview;

//                if (IsNetworkConnected())
//                {
//                    MovieCredits movieCredits = await _businessServices.GetMoviesCreditsAsync(movie.Id);
//                    if (movieCredits.Cast != null && movieCredits.Crew != null)
//                    {
//                        Casts = new ObservableCollection<Cast>(movieCredits.Cast);
//                        Crews = new ObservableCollection<Crew>(movieCredits.Crew);
//                    }
//                }
//                else
//                {
//                    await NetworkUnavailabilityPopup();
//                }
//            }
//            catch (Exception ex)
//            {
//                IsBusy = false;
//            }
//        }
//    }
//}



using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using FilmFinderTMDB.Source.Data.Model;
using FilmFinderTMDB.Source.Presentation.NavigationService;
using FilmFinderTMDB.Source.Services;

namespace FilmFinderTMDB.Source.Presentation.TmdbInfo.ViewModel
{
    public partial class TmdbDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IBusinessServices _businessServices;
        private readonly INavigationService _navigationService;
        private int _castItemsLoaded;
        private int _crewItemsLoaded;
        private bool IsLoadingMoreCast;
        private bool IsLoadingMoreCrew;
        private int _movieId;

        [ObservableProperty]
        private string backdropImageUrl, posterImageUrl, releaseDateInfo, genersInfo, overView;

        [ObservableProperty]
        private ObservableCollection<Cast> casts;

        [ObservableProperty]
        private ObservableCollection<Crew> crews;

        public TmdbDetailsViewModel(INavigationService navigationService, IBusinessServices businessServices)
        {
            _businessServices = businessServices;
            _navigationService = navigationService;
            Casts = new ObservableCollection<Cast>();
            Crews = new ObservableCollection<Crew>();
            _castItemsLoaded = 0;
            _crewItemsLoaded = 0;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("MovieDetails"))
            {
                var movie = query["MovieDetails"] as Movie;
                if (movie?.Id != null)
                {
                    _movieId = movie.Id;
                    _ = LoadMovieDetailsAsync(movie);
                }
            }
        }

        private async Task LoadMovieDetailsAsync(Movie movie)
        {
            try
            {
                BackdropImageUrl = movie.BackdropImageUrl;
                Title = movie.Title;
                PosterImageUrl = movie.PosterImageUrl;
                ReleaseDateInfo = movie.ReleaseDateInfo;
                GenersInfo = movie.GenersInfo;
                OverView = movie.Overview;

                if (IsNetworkConnected())
                {
                    var movieCredits = await _businessServices.GetMoviesCreditsAsync(movie.Id);
                    if (movieCredits.IsSuccess)
                    {
                        if (movieCredits?.Cast != null && movieCredits.Crew != null)
                        {
                            Casts = new ObservableCollection<Cast>(movieCredits.Cast.Take(10));
                            Crews = new ObservableCollection<Crew>(movieCredits.Crew.Take(10));
                            _castItemsLoaded = 10;
                            _crewItemsLoaded = 10;
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", movieCredits.Message, "Ok");
                    }
                }
                else
                {
                    await NetworkUnavailabilityPopup();
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                // Handle exception
            }
        }

        public async Task LoadMoreCastAsync()
        {
            if (IsLoadingMoreCast || !IsNetworkConnected()) return;

            IsLoadingMoreCast = true;
            try
            {
                var movieCredits = await _businessServices.GetMoviesCreditsAsync(_movieId);
                if (movieCredits.IsSuccess)
                {
                    if (movieCredits?.Cast != null)
                    {
                        var additionalCast = movieCredits.Cast.Skip(_castItemsLoaded).Take(10).ToList();
                        foreach (var cast in additionalCast)
                        {
                            if (!Casts.Any(c => c.Id == cast.Id))
                            {
                                Casts.Add(cast);
                            }
                        }
                        _castItemsLoaded += 10;
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", movieCredits.Message, "Ok");
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                IsLoadingMoreCast = false;
            }
        }

        public async Task LoadMoreCrewAsync()
        {
            if (IsLoadingMoreCrew || !IsNetworkConnected()) return;

            IsLoadingMoreCrew = true;
            try
            {
                var movieCredits = await _businessServices.GetMoviesCreditsAsync(_movieId);
                if (movieCredits.IsSuccess)
                {
                    if (movieCredits?.Crew != null)
                    {
                        var additionalCrew = movieCredits.Crew.Skip(_crewItemsLoaded).Take(10).ToList();
                        foreach (var crew in additionalCrew)
                        {
                            if (!Crews.Any(c => c.Id == crew.Id))
                            {
                                Crews.Add(crew);
                            }
                        }
                        _crewItemsLoaded += 10;
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", movieCredits.Message, "Ok");
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                IsLoadingMoreCrew = false;
            }
        }
    }
}

