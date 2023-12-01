using WASA_Multi_Platform.Activities;
using WASA_Multi_Platform.Entity;

namespace WASA_Multi_Platform.Pages
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            if (!FileIOActivities.HaveSettingsApplied())
                Navigation.PushAsync(new SettingsPage());
            else
            {
                if (!FileIOActivities.UserAuthorized())
                    Navigation.PushAsync(new AuthPage());
            }
        }
    }
}
