using WASA_Multi_Platform.Activities;
using WASA_Multi_Platform.Pages;

namespace WASA_Multi_Platform
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            if (!FileIOActivities.UserAuthorized())
            {
                Navigation.PushAsync(new AuthPage());
            }
        }

        private void SessionExit_Clicked(object sender, EventArgs e)
        {
            FileIOActivities.SetSessionData(null);
            Navigation.PushAsync(new AuthPage());
            Current.FlyoutIsPresented = false;
        }
    }
}