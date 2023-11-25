using WASA_Multi_Platform.Activities;
using WASA_Multi_Platform.Entity;

namespace WASA_Multi_Platform.Const
{
    static class DBConnection
    {
        static SettingsEntity entity = FileIOActivities.GetSettingsData();
        
        public static string ConnectionString = $"Host='{entity.DB_Ip}';Port='{entity.DB_Port}';Database='{entity.DB_Name}';Username='{entity.DB_UserName}';Password='{entity.DB_Password}';sslmode=Disable";
       
    }
}