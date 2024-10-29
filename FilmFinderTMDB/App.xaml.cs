using FilmFinderTMDB.Source.Presentation.Controls;
using FilmFinderTMDB.Source.Presentation.NavigationService;
using FilmFinderTMDB.Source.Presentation.TmdbInfo.ViewModel;
using FilmFinderTMDB.Source.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FilmFinderTMDB;

public partial class App : Application
{
    IBusinessServices _businessServices;
    public static IServiceProvider Services { get; private set; }
    public App(IBusinessServices businessServices, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _businessServices = businessServices;
        Services = serviceProvider;
        //MainPage = new AppShell();
        ExecuteAppInfo();
    }


    private void ExecuteAppInfo()
    {
        try
        {
            //for borderless entry
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderLessEntry), (handler, view) =>
            {
                if (view is BorderLessEntry)
                {
#if ANDROID
                    handler.PlatformView.Background = null;
                    handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif IOS
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.Layer.BorderWidth = 0;
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
                }
            });

            MainPage = new AppShell(new AppShellViewModel(new NavigationService(), _businessServices));

        }
        catch (Exception ex)
        {

        }
    }
}

