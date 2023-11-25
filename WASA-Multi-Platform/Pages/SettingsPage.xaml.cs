using WASA_Multi_Platform.Activities;
using WASA_Multi_Platform.Entity;

namespace WASA_Multi_Platform.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();

        SettingsEntity entity = FileIOActivities.GetSettingsData();
		
		if(entity != null )
        {
            Name.Text = entity.DB_Name;
            IP.Text = entity.DB_Ip;
            Port.Text = entity.DB_Port;
            UserName.Text = entity.DB_UserName;
            Password.Text = entity.DB_Password;
            if (entity.DB_SllMode == "Disable")
                SllMode.IsToggled = false;
            if (entity.DB_SllMode == "Required")
                SllMode.IsToggled = true;
        }

        IP.IsPassword = true;
        Password.IsPassword = true;
	}

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
		if (SllMode.IsToggled)
		{
            FileIOActivities.SetSettingsData(new()
            {
                DB_Name = Name.Text,
                DB_Ip = IP.Text,
                DB_Port = Port.Text,
                DB_UserName = UserName.Text,
                DB_Password = Password.Text,
                DB_SllMode = "Required"
            });
        }
        else
        {
            FileIOActivities.SetSettingsData(new()
            {
                DB_Name = Name.Text,
                DB_Ip = IP.Text,
                DB_Port = Port.Text,
                DB_UserName = UserName.Text,
                DB_Password = Password.Text,
                DB_SllMode = "Disable"
            });
       }
        Navigation.RemovePage(this);
    }

    private void IPVisibility_Clicked(object sender, EventArgs e)
    {
        if (IP.IsPassword == true)
        {
            IP.IsPassword = false;
            IPVisibility.Source = "show_eye.svg";
        }
        else
        {
            IP.IsPassword = true;
            IPVisibility.Source = "hide_eye.svg";
        }
    }

    private void PasswordVisibility_Clicked(object sender, EventArgs e)
    {
        if (Password.IsPassword == true)
        {
            Password.IsPassword = false;
            PasswordVisibility.Source = "show_eye.svg";
        }
        else
        {
            Password.IsPassword = true;
            PasswordVisibility.Source = "hide_eye.svg";
        }
    }
}