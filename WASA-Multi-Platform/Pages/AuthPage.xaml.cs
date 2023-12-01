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
                        var toast = Toast.Make("����� ������� ������ �� ����������", CommunityToolkit.Maui.Core.ToastDuration.Long);
                        await toast.Show();
                    }
                });
            }
            else
            {
                var toast = Toast.Make("������ ������ ���� ������ 4 ��������", CommunityToolkit.Maui.Core.ToastDuration.Long);
                toast.Show();
            }
        }
        else
        {
            var toast = Toast.Make("����� ������ ���� ������ 1 �������", CommunityToolkit.Maui.Core.ToastDuration.Long);
            toast.Show();
        }
    }
}