using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;
using Npgsql;
using WASA_Multi_Platform.Entity;
using WASA_Multi_Platform.Const;

namespace WASA_Multi_Platform.Activities
{
    internal class AuthActivities
    {

        public static async Task<bool> IsBiometricAuthorized()
        {
            try
            {
                await Permissions.RequestAsync<Permissions.Sensors>();
                var request = new AuthenticationRequestConfiguration("Проверка на наличие пальцев!", "Нет пальчиков, нет доступа");
                var result = await CrossFingerprint.Current.AuthenticateAsync(request).ConfigureAwait(false);
                return result.Authenticated;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<bool> IsAuthorized(Entry login, Entry password)
        {
            NpgsqlConnection con = new(DBConnection.ConnectionString);
            await con.OpenAsync();
            NpgsqlCommand command = new($"SELECT user_password FROM users WHERE user_name = '{login.Text}'", con);
            if (Convert.ToString(await command.ExecuteScalarAsync()) == password.Text)
            {
                command = new($"SELECT user_id FROM users WHERE user_name = '{login.Text}'", con);
                FileIOActivities.SetSessionData(new SessionEntity
                {
                    ID = Convert.ToInt32(await command.ExecuteScalarAsync()),
                    UserName = login.Text
                });

                //При потребности в проверке верификации аккаунта администратором - раскоментировать
                /*
                command = new($"SELECT verifided FROM users WHERE user_name = '{Login.Text}'", con);
                bool verifided = Convert.ToBoolean(command.ExecuteScalar());
                if (verifided == true)
                {
                    command = new($"SELECT user_id FROM users WHERE user_name = '{Login.Text}'", con);
                    UserEntity.ID = Convert.ToInt32(command.ExecuteScalar());
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
                return true;
            }
            else
                return false;
        }
    }
}
