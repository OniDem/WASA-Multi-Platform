using WASA_Multi_Platform.Activities;
using WASA_Multi_Platform.Entity;

namespace WASA_Multi_Platform.Pages;

public partial class AuthCodeAddPage : ContentPage
{
	public AuthCodeAddPage()
	{
		InitializeComponent();
	}

    private void CodeAddEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (CodeAddEntry.Text.Length == 4)
            ConfirmCodeAddEntry.IsVisible = true;
        else
            ConfirmCodeAddEntry.IsVisible = false;
    }

    private void ConfirmCodeAddEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (ConfirmCodeAddEntry.Text.Length == 4 && CodeAddEntry.Text == ConfirmCodeAddEntry.Text)
        {
            BiometricInfoLabel.IsVisible = true;
            BiometricSwitch.IsVisible = true;
            ContinueButton.IsVisible = true;
        }
        else
        {
            BiometricInfoLabel.IsVisible = false;
            BiometricSwitch.IsVisible = false;
            ContinueButton.IsVisible = false;
        }
    }

    private async void ContinueButton_Clicked(object sender, EventArgs e)
    {
        SessionEntity entity = FileIOActivities.GetSessionsData();
        FileIOActivities.SetSessionData(new SessionEntity
        {
            ID = entity.ID,
            UserName = entity.UserName,
            Code = CodeAddEntry.Text,
            IsBiometric = BiometricSwitch.IsToggled
        });
        await Navigation.PopModalAsync();
    }
}