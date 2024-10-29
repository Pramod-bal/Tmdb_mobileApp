using CommunityToolkit.Mvvm.Messaging;
using FilmFinderTMDB.Source.Message;
using ZXing.Net.Maui;

namespace FilmFinderTMDB.Source.Presentation.TmdbInfo.Page;

public partial class TmdbBarcodeScanPage : ContentPage
{
	public TmdbBarcodeScanPage()
	{
		InitializeComponent();

        barcodeView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = true
        };
    }

    private async void OnBarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        try
        {
            string scannedValue = e.Results.FirstOrDefault()?.Value;

            if (!string.IsNullOrWhiteSpace(scannedValue))
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    WeakReferenceMessenger.Default.Send(new ScanCodeMessage(scannedValue));
                    await Shell.Current.Navigation.PopAsync();
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
