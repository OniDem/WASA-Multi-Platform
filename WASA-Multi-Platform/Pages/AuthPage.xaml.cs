using CommunityToolkit.Maui.Alerts;
using Npgsql;
using WASA_Multi_Platform.Const;
using WASA_Multi_Platform.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WASA_Multi_Platform.Pages;

public partial class AuthPage : ContentPage
{
	public AuthPage()
	{
		InitializeComponent();
    }

    private void AuthButton_Clicked(object sender, EventArgs e)
    {
        if (Validations.LoginValid(Login.Text))
        {

            if (Validations.PasswordValid(Password.Text))
            {
                NpgsqlConnection con = new(DBConnection.ConnectionString);

                con.Open();
                NpgsqlCommand command = new($"SELECT user_password FROM users WHERE user_name = '{Login.Text}'", con);
                if (Convert.ToString(command.ExecuteScalar()) == Password.Text)
                {

                    command = new($"SELECT user_id FROM users WHERE user_name = '{Login.Text}'", con);
                    UserEntity.ID = Convert.ToInt32(command.ExecuteScalar());
                    Navigation.RemovePage(this);

                    //При потребности в проверке верификации аккаунта администратором - раскоментировать
                    /*
                    command = new($"SELECT verifided FROM users WHERE user_name = '{Login.Text}'", con);
                    bool verifided = Convert.ToBoolean(command.ExecuteScalar());
                    if (verifided == true)
                    {
                        command = new($"SELECT user_id FROM users WHERE user_name = '{Login.Text}'", con);
                        UserEntity.ID = Convert.ToInt32(command.ExecuteScalar());
                        Navigation.RemovePage(this);
                        //переход на страницы + заполнить UserEntity.ID
                    }
                    else
                    {
                        var toast = Toast.Make("Ваша учётная запись не верифицирована, обратитесь к администратору!", CommunityToolkit.Maui.Core.ToastDuration.Long);
                        toast.Show();
                        Login.Text = "";
                        Password.Text = "";
                    }
                    */
                }
                else
                {
                    var toast = Toast.Make("Такой учётной записи не существует", CommunityToolkit.Maui.Core.ToastDuration.Long);
                    toast.Show();
                }
                con.Close();

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