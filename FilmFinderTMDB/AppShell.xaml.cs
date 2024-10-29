using FilmFinderTMDB.Source.Presentation.TmdbInfo.Page;
using FilmFinderTMDB.Source.Presentation.TmdbInfo.ViewModel;

namespace FilmFinderTMDB;

public partial class AppShell : Shell
{
	public AppShell(AppShellViewModel viewModel)
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(TmdbListPage), typeof(TmdbListPage));
        Routing.RegisterRoute(nameof(TmdbDetailsPage), typeof(TmdbDetailsPage)); 
        Routing.RegisterRoute(nameof(TmdbBarcodeScanPage), typeof(TmdbBarcodeScanPage));
        BindingContext = viewModel;
    }
}

