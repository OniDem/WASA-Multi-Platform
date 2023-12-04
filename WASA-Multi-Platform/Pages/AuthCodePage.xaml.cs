using CommunityToolkit.Maui.Alerts;
using WASA_Multi_Platform.Activities;
using WASA_Multi_Platform.Entity;

namespace WASA_Multi_Platform.Pages;

public partial class AuthCodePage : ContentPage
{
    SessionEntity entity = FileIOActivities.GetSessionsData();
    public AuthCodePage()
	{
        if(entity.IsBiometric == true)
        {
            Dispatcher.Dispatch(async () => {
                var result = await AuthActivities.IsBiometricAuthorized();
                if (result)
                    await Navigation.PopAsync();
            });
            
        }
        InitializeComponent();
    }

    private void CodeEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (CodeEntry.Text.Length == 4)
            CheckCodeButton.IsVisible = true;
    }

    private void CheckCodeButton_Clicked(object sender, EventArgs e)
    {

        if (entity.Code == CodeEntry.Text)
            Navigation.PopAsync();
        else
        {
            var toast = Toast.Make("Не верный код!", CommunityToolkit.Maui.Core.ToastDuration.Long);
            toast.Show();
        }
            

    }
}