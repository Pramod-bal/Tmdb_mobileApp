using System;
using CommunityToolkit.Mvvm.Messaging;
using FilmFinderTMDB.Source.Presentation.NavigationService;
using FilmFinderTMDB.Source.Services;

namespace FilmFinderTMDB.Source.Presentation.TmdbInfo.ViewModel
{
	public class AppShellViewModel
	{
        private IBusinessServices _businessServices;
        private INavigationService _navigationService;

        public AppShellViewModel()
        {
        }

        public AppShellViewModel(INavigationService navigationService, IBusinessServices businessServices)
		{
            _navigationService = navigationService;
            _businessServices = businessServices;
        }
	}
}

