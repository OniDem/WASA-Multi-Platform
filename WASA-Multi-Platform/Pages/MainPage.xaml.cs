using WASA_Multi_Platform.Entity;

namespace WASA_Multi_Platform.Pages
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            if (UserEntity.ID == null)
                Navigation.PushAsync(new AuthPage());
        }

        private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
        {
            if (UserEntity.ID == null)
                Navigation.PushAsync(new AuthPage());
        }
    }
}
