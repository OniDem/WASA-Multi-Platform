using WASA_Multi_Platform.Activities;

namespace WASA_Multi_Platform.Pages;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
        if (!FileIOActivities.HaveSettingsApplied())
            Navigation.PushAsync(new SettingsPage());
        else
        {
            if (!FileIOActivities.UserAuthorized())
            {
                Navigation.PushAsync(new AuthPage());
            }
        }
        AppVersion.Text = "Текущая версия: " + AppInfo.Current.Version;

    }
}