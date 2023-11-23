using Npgsql;
using WASA_Multi_Platform.Const;
using WASA_Multi_Platform.Entity;
using WASA_Multi_Platform.Pages;

namespace WASA_Multi_Platform
{
    static class UserActivities
    {
        private static NpgsqlConnection con = new(DBConnection.ConnectionString);
        private static NpgsqlCommand command;

        public static void GetUserInfo(int? user_id)
        {
            con!.Open();
            command = new($"SELECT user_name FROM users WHERE user_id = '{user_id}'", con);
            UserEntity.UserName = Convert.ToString(command.ExecuteScalar());
            command = new($"SELECT user_role FROM users WHERE user_id = '{user_id}'", con);
            UserEntity.UserRole = Convert.ToString(command.ExecuteScalar());
        }
    }
}
