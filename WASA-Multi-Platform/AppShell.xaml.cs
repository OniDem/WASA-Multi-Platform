using WASA_Multi_Platform.Entity;

namespace WASA_Multi_Platform
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            UserActivities.GetUserInfo(3);
            SessionUser.Text = UserEntity.UserName;
        }

        private void SessionExit_Clicked(object sender, EventArgs e)
        {

        }

        private void SessionUser_Clicked(object sender, EventArgs e)
        {

        }
    }
}