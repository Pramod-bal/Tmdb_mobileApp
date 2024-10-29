using System.Reflection;
using CommunityToolkit.Maui;
using FilmFinderTMDB.Source.Presentation.NavigationService;
using FilmFinderTMDB.Source.Presentation.TmdbInfo.ViewModel;
using FilmFinderTMDB.Source.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;

namespace FilmFinderTMDB;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        try
        {
            var builder = MauiApp.CreateBuilder();
            string appsettingfile = string.Empty;
            
            var getAssemebly = Assembly.GetExecutingAssembly();
            appsettingfile = "FilmFinderTMDB.Source.AppConfiguration.AppSettings.json";
            using var stream = getAssemebly.GetManifestResourceStream(appsettingfile);

            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            builder.Configuration.AddConfiguration(config);


            builder
                .UseMauiApp<App>()
                .UseBarcodeReader()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.UseMauiCommunityToolkit();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IBusinessServices, BusinessServices>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            //ViewModel
            builder.Services.AddTransient<BaseViewModel>();
            builder.Services.AddTransient<TmdbListViewModel>();
            builder.Services.AddTransient<TmdbDetailsViewModel>();

            return builder.Build();
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
        }
    }
}

