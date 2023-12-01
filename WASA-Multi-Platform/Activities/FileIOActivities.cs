using CommunityToolkit.Maui.Alerts;
using WASA_Multi_Platform.Const;
using WASA_Multi_Platform.Entity;

namespace WASA_Multi_Platform.Activities
{
    static class FileIOActivities
    {

        /// <summary>
        /// Функция для добавления данных о адресе и порте подключения к БД
        /// </summary>
        public static void SetSettingsData(SettingsEntity entity)
        {
            try
            {
                Task.Run(async () =>
                {
                    await Permissions.RequestAsync<Permissions.StorageWrite>();
                });
                using (StreamWriter file = File.CreateText(Paths.SettingsPath))
                {
                    Newtonsoft.Json.JsonSerializer serializer = new();
                    serializer.Serialize(file, entity);
                    var toast = Toast.Make("Данные сохранены!");
                    toast.Show();
                }
            }
            catch (Exception ex)
            {
                var toast = Toast.Make(ex.Message);
                toast.Show();
            }
        }

        public static SettingsEntity GetSettingsData()
        {
            try
            {
                Task.Run(async () =>
                {
                    await Permissions.RequestAsync<Permissions.StorageRead>();
                });
                if (File.Exists(Paths.SettingsPath) == false)
                    File.CreateText(Paths.SettingsPath);
                using StreamReader stream = File.OpenText(Paths.SettingsPath);
                Newtonsoft.Json.JsonSerializer serializer = new();
                return serializer.Deserialize(stream, typeof(SettingsEntity)) as SettingsEntity;
            }
            catch (Exception ex)
            {
                var toast = Toast.Make(ex.Message);
                toast.Show();
                return null;
            }
        }

        public static bool HaveSettingsApplied()
        {
            SettingsEntity entity = GetSettingsData();
            if (entity != null)
                return true;
            else
                return false;
        }



        public static void SetSessionData(SessionEntity entity)
        {
            try
            {
                Task.Run(async () =>
                {
                    await Permissions.RequestAsync<Permissions.StorageWrite>();
                });
                using (StreamWriter file = File.CreateText(Paths.SessionPath))
                {
                    Newtonsoft.Json.JsonSerializer serializer = new();
                    serializer.Serialize(file, entity);
                    var toast = Toast.Make("Данные сохранены!");
                    toast.Show();
                }
            }
            catch (Exception ex)
            {
                var toast = Toast.Make(ex.Message);
                toast.Show();
            }
        }

        private static SessionEntity GetSessionsData()
        {
            try
            {
                Task.Run(async () =>
                {
                    await Permissions.RequestAsync<Permissions.StorageRead>();
                });
                if (File.Exists(Paths.SessionPath) == false)
                    File.CreateText(Paths.SessionPath);
                using StreamReader stream = File.OpenText(Paths.SessionPath);
                Newtonsoft.Json.JsonSerializer serializer = new();
                return serializer.Deserialize(stream, typeof(SessionEntity)) as SessionEntity;
            }
            catch (Exception ex)
            {
                var toast = Toast.Make(ex.Message);
                toast.Show();
                return null;
            }
        }

        public static bool UserAuthorized()
        {
            SessionEntity entity = GetSessionsData();
            if (entity != null)
                return true;
            else
                return false;
        }
    }
}
