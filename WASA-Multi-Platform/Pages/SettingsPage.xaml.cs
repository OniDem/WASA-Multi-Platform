using WASA_Multi_Platform.Activities;
using WASA_Multi_Platform.Entity;

namespace WASA_Multi_Platform.Pages;

public partial class SettingsPage : ContentPage
{
	SettingsEntity entity = new();

    public SettingsPage()
	{
		InitializeComponent();
		Task.Run(async () => await Dispatcher.DispatchAsync(() =>
		{
            entity = FileIOActivities.GetSettingsData();
        }));
		Name.Text = entity.DB_Name;
		IP.Text = entity.DB_Ip;
		Port.Text = entity.DB_Port;
		UserName.Text = entity.DB_UserName;
		Password.Text = entity.DB_Password;
	}

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
		FileIOActivities.SetSettingsData(new()
		{
			DB_Name = Name.Text,
			DB_Ip = IP.Text,
			DB_Port = Port.Text,
			DB_UserName = UserName.Text,
			DB_Password = Password.Text,
		});
		Navigation.RemovePage(this);
    }
}