namespace WASA_Multi_Platform.Pages;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
		AppVersion.Text = "Текущая версия: " + AppInfo.Current.Version;

    }
}