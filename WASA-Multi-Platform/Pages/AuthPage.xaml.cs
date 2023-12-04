using CommunityToolkit.Maui.Alerts;
using WASA_Multi_Platform.Activities;

namespace WASA_Multi_Platform.Pages;

public partial class AuthPage : ContentPage
{
    public AuthPage()
    {
        InitializeComponent();
        if (!FileIOActivities.HaveSettingsApplied())
            Navigation.PushAsync(new SettingsPage());
        else
        {
            if (FileIOActivities.UserAuthorized())
            {
                Navigation.PopAsync();
            }
        }
    }



    private void AuthButton_Clicked(object sender, EventArgs e)
    {
        if (Validations.EntryValid(Login.Text))
        {
            if (Validations.EntryValid(Password.Text))
            {
                Dispatcher.Dispatch(async () => {
                    var result = await AuthActivities.IsAuthorized(Login, Password);
                    if (result)
                    {
                        await Navigation.PushModalAsync(new AuthCodeAddPage());
                        Navigation.RemovePage(this);
                    }
                    else
                    {
                        var toast = Toast.Make("Такой учётной записи не существует", CommunityToolkit.Maui.Core.ToastDuration.Long);
                        await toast.Show();
                    }
                });
            }
            else
            {
                var toast = Toast.Make("Пароль должен быть длинее 4 символов", CommunityToolkit.Maui.Core.ToastDuration.Long);
                toast.Show();
            }
        }
        else
        {
            var toast = Toast.Make("Логин должен быть длинее 1 символа", CommunityToolkit.Maui.Core.ToastDuration.Long);
            toast.Show();
        }
    }
}