using FilmFinderTMDB.Source.Presentation.TmdbInfo.ViewModel;

namespace FilmFinderTMDB.Source.Presentation.TmdbInfo.Page;

public partial class TmdbDetailsPage : ContentPage
{
	public TmdbDetailsPage()
	{
		InitializeComponent();
        BindingContext = App.Services.GetRequiredService<TmdbDetailsViewModel>();
    }

    private async void OnCastsCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        var viewModel = BindingContext as TmdbDetailsViewModel;
        if (viewModel != null && e.LastVisibleItemIndex >= viewModel.Casts.Count - 1)
        {
            await viewModel.LoadMoreCastAsync();
        }
    }

    private async void OnCrewsCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        var viewModel = BindingContext as TmdbDetailsViewModel;
        if (viewModel != null && e.LastVisibleItemIndex >= viewModel.Crews.Count - 1)
        {
            await viewModel.LoadMoreCrewAsync();
        }
    }
}
