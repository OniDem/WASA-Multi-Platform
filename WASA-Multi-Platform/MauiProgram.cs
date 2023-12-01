using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;
using CommunityToolkit.Maui;
using WASA_Multi_Platform.Pages;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace WASA_Multi_Platform
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseBarcodeReader().UseMauiCommunityToolkit();

            builder.Services.AddSingleton<AuthPage>();
            builder.Services.AddSingleton(typeof(IFingerprint), CrossFingerprint.Current);
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}