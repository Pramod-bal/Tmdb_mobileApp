using FilmFinderTMDB.Source.Presentation.TmdbInfo.ViewModel;

namespace FilmFinderTMDB.Source.Presentation.TmdbInfo.Page;

public partial class TmdbListPage : ContentPage
{
    public TmdbListPage()
    {
        InitializeComponent();
        BindingContext = App.Services.GetRequiredService<TmdbListViewModel>();
    }
   
    private async void OnCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        var viewModel = BindingContext as TmdbListViewModel;
        if (viewModel != null && e.LastVisibleItemIndex >= viewModel.AllMovies.Count - 1)
        {
            await viewModel.LoadMoreMoviesAsync();
        }
    }
}
