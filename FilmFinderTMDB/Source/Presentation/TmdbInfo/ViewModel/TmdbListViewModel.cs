using System;
using CommunityToolkit.Mvvm.Input;
using FilmFinderTMDB.Source.Services;
using FilmFinderTMDB.Source.Presentation.NavigationService;
using FilmFinderTMDB.Source.Presentation.TmdbInfo.Page;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using FilmFinderTMDB.Source.Data.Model;
using System.Globalization;
using CommunityToolkit.Mvvm.Messaging;
using FilmFinderTMDB.Source.Message;

namespace FilmFinderTMDB.Source.Presentation.TmdbInfo.ViewModel
{
	public partial class TmdbListViewModel : BaseViewModel, IRecipient<ScanCodeMessage>
    {
        private IBusinessServices _businessServices;
        private INavigationService _navigationService;
        private readonly Task _initializationTask = null!;
        private ObservableCollection<Genre> AllGenreListInfo = new ObservableCollection<Genre>();
        private List<Genre> GenreList = new List<Genre>();
        private int _currentPage;

        private bool IsLoadingMore = false;

        [ObservableProperty]
        ObservableCollection<Movie> allMovies;

        [ObservableProperty]
        string tmdbSearchBarEntry;

        [ObservableProperty]
        bool isSearchCrossButtonEnable;

        public TmdbListViewModel(INavigationService navigationService, IBusinessServices businessServices)
		{
            _businessServices = businessServices;
            _navigationService = navigationService;
            _currentPage = 1;
            WeakReferenceMessenger.Default.Register<ScanCodeMessage>(this);
            AllMovies = new ObservableCollection<Movie>();
            _initializationTask = GetGenresAsync();
        }

        /// <summary>
        /// Refresh screen on view will appeare
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task Appearing()
        {
            try
            {
                if (AllGenreListInfo == null && AllGenreListInfo?.Count() == 0)
                {
                   await GetGenresAsync();
                }
            }
            
            catch (Exception ex)
            {
                
            }
        }

        private async Task GetGenresAsync()
        {
            try
            {
                if (IsNetworkConnected())
                {
                    IsBusy = true;
                    GenreResponse allGenreList = await _businessServices.GetGenresAsync();
                    if (allGenreList.IsSuccess)
                    {
                        AllGenreListInfo = new ObservableCollection<Genre>(allGenreList?.Genres);
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", allGenreList.Message, "Ok");
                    }
                    IsBusy = false;
                }
                else
                {
                    await NetworkUnavailabilityPopup();
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async void TmdbSearchBarTextChanged(object searchText)
        {
            try
            {
                var viewModel = (TmdbListViewModel)searchText;
                TmdbSearchBarEntry = viewModel.TmdbSearchBarEntry;
                var searchItem = TmdbSearchBarEntry.ToString().ToLower();
                if (!string.IsNullOrWhiteSpace(searchItem))
                {
                    IsBusy = true;
                    IsSearchCrossButtonEnable = true;
                    _currentPage = 1;
                    AllMovies.Clear();
                    await LoadMoreMoviesAsync();
                    IsBusy = false;
                }
                else
                {
                    IsBusy = true;
                    AllMovies.Clear();
                    IsSearchCrossButtonEnable = false;
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        public async Task LoadMoreMoviesAsync()
        {
            try
            {
                if (IsNetworkConnected())
                {
                    if (IsLoadingMore) return;

                    IsLoadingMore = true;
                    var movieResponse = await _businessServices.GetMoviesBasedOnSearchAsync(TmdbSearchBarEntry, _currentPage);
                    if (movieResponse.IsSuccess)
                    {
                        if (movieResponse.Results.Count() > 0)
                        {
                            var genreDictionary = AllGenreListInfo.ToDictionary(g => g.Id, g => g.Name);

                            foreach (var movie in movieResponse.Results)
                            {
                                var genreNames = movie.GenreIds
                                                      .Where(genreDictionary.ContainsKey)
                                                      .Select(genreId => genreDictionary[genreId])
                                                      .Distinct()
                                                      .ToList();

                                movie.GenersInfo = string.Join(", ", genreNames);

                                if (DateTime.TryParse(movie.ReleaseDate, out DateTime releaseDate))
                                {
                                    movie.ReleaseDateInfo = releaseDate.ToString("dd MMMM yyyy", new CultureInfo("en-US"));
                                }

                                // Check for duplicates before adding
                                if (!AllMovies.Any(m => m.Id == movie.Id))
                                {
                                    AllMovies.Add(movie);
                                }
                            }
                            _currentPage++;
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", movieResponse.Message, "Ok");
                        return;
                    }
                    IsLoadingMore = false;
                }
                else
                {
                    await NetworkUnavailabilityPopup();
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Delete Tmdb Name Entry
        /// </summary>
        /// <param name="sender"></param>
        [RelayCommand]
        private void DeleteTmdbSearchBarEntry(object sender)
        {
            try
            {
                IsBusy = true;
                //await Task.Delay(50);
                if (!string.IsNullOrWhiteSpace(TmdbSearchBarEntry))
                {
                    TmdbSearchBarEntry = string.Empty;
                    IsSearchCrossButtonEnable = false;
                }
                else
                {
                    IsSearchCrossButtonEnable = false;
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Open Task ScanButton 
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        [RelayCommand]
        private async Task ScanButton(object sender)
        {
            try
            {
                KeyboardVisibility(false);
                await _navigationService.NavigationToAsync(nameof(TmdbBarcodeScanPage));
            }
            catch (Exception ex)
            {

            }
        }

        [RelayCommand]
        private async Task FilmItemClick(object sender)
        {
            try
            {
                KeyboardVisibility(false);
                IsBusy = true;
                // await Task.Delay(100);
                var  movie = (Movie)sender;
                if (movie?.Id > 0)
                {
                    Dictionary<string, object> filmParameter = new Dictionary<string, object>();
                    filmParameter.Add("MovieDetails", movie);
                    await _navigationService.NavigationToAsync(nameof(TmdbDetailsPage), filmParameter);
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
            }
        }

        public void Receive(ScanCodeMessage message)
        {
            var scanCode = message.Value as string;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                TmdbSearchBarEntry = scanCode;
            });
        }
    }
}

