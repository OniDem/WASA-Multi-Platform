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

                    //��� ����������� � �������� ����������� �������� ��������������� - ����������������
                    /*
                    command = new($"SELECT verifided FROM users WHERE user_name = '{Login.Text}'", con);
                    bool verifided = Convert.ToBoolean(command.ExecuteScalar());
                    if (verifided == true)
                    {
                        command = new($"SELECT user_id FROM users WHERE user_name = '{Login.Text}'", con);
                        UserEntity.ID = Convert.ToInt32(command.ExecuteScalar());
                        Navigation.RemovePage(this);
                        //������� �� �������� + ��������� UserEntity.ID
                    }
                    else
                    {
                        var toast = Toast.Make("���� ������� ������ �� ��������������, ���������� � ��������������!", CommunityToolkit.Maui.Core.ToastDuration.Long);
                        toast.Show();
                        Login.Text = "";
                        Password.Text = "";
                    }
                    */
                }
                else
                {
                    var toast = Toast.Make("����� ������� ������ �� ����������", CommunityToolkit.Maui.Core.ToastDuration.Long);
                    toast.Show();
                }
                con.Close();

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