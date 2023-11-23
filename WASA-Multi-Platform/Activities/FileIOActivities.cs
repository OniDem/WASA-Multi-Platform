using CommunityToolkit.Maui.Alerts;
using WASA_Multi_Platform.Entity;

namespace WASA_Multi_Platform.Activities
{
    static class FileIOActivities
    {

        private static string _path = Path.Combine(FileSystem.Current.AppDataDirectory, "./settings.json");


        /// <summary>
        /// Функция для добавления данных о адресе и порте подключения к БД
        /// </summary>
        public static void SetAddressData(SettingsEntity entity)
        {
            try
            {
                using (StreamWriter file = File.CreateText(_path))
                {
                    Newtonsoft.Json.JsonSerializer serializer = new();
                    serializer.Serialize(file, entity);
                }
            }
            catch (Exception ex)
            {
                var toast = Toast.Make(ex.Message);
                toast.Show();
            }
        }

        public static SettingsEntity GetAddressData()
        {
            if (File.Exists(_path) == false)
                File.CreateText(_path);
            using (StreamReader stream = File.OpenText(_path))
            {
                Newtonsoft.Json.JsonSerializer serializer = new();
                return serializer.Deserialize(stream, typeof(SettingsEntity)) as SettingsEntity;
            }
        }
    }
}
