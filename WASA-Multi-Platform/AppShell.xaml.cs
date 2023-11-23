using WASA_Multi_Platform.Entity;
using WASA_Multi_Platform.Pages;

namespace WASA_Multi_Platform
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            if (UserEntity.ID == null)
            {
                Navigation.PushAsync(new AuthPage());
            }
            else
            {

            }
        }

        private void SessionExit_Clicked(object sender, EventArgs e)
        {

        }
    }
}