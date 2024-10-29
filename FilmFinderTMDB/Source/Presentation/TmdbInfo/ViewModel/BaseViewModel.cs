using System;
using CommunityToolkit.Mvvm.ComponentModel;
using FilmFinderTMDB.Source.Presentation.NavigationService;
using Microsoft.Maui.Platform;

namespace FilmFinderTMDB.Source.Presentation.TmdbInfo.ViewModel
{
	public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string title;

        public BaseViewModel()
        {
        }

        public bool IsNetworkConnected()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (accessType == NetworkAccess.Internet)
                return true;
            else
                return false;
        }

        public async Task NetworkUnavailabilityPopup()
        {
            await App.Current.MainPage.DisplayAlert("Alert", "Please check your network connection.", "Ok");
            return;
        }

        public void KeyboardVisibility(bool StateFocus)
        {
#if ANDROID
            if (Platform.CurrentActivity.CurrentFocus != null)
            {
                if (StateFocus)
                {
                    Platform.CurrentActivity.ShowKeyboard(Platform.CurrentActivity.CurrentFocus);
                }
                else
                {
                    Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
                    Platform.CurrentActivity.CurrentFocus.ClearFocus();
                }
            }
#endif
        }
    }
}

