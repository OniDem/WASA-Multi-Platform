namespace WASA_Multi_Platform.Const
{
    public static class Paths
    {
        public static string SettingsPath = Path.Combine(FileSystem.Current.AppDataDirectory, "./settings.json");

        public static string SessionPath = Path.Combine(FileSystem.AppDataDirectory, "./session.json");
    }
}
