using Newtonsoft.Json;
using System.IO;

namespace JudoApp.Services;
public class Configurations
{
    private static AppSettings AppSettings => GetSettings("appsettings.json");
    private static AppSettings UserSecrets => GetSettings("appsettings.userSecrets.json");
    public static string GetDefaultConectionString()
    {
        return GetSettings().ConnectionStrings.DefaultConection;
    }

    public static AppSettings GetSettings()
    {
        if (AppSettings.UserSecrets)
            return UserSecrets;
        return AppSettings;
    }

    private static AppSettings GetSettings(string fileName)
    {
        //appsettings.userSecrets.json
        var settings = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(fileName));
        if (settings == null)
            settings = new AppSettings();
        return settings;
    }
}

public class AppSettings
{
    public bool UserSecrets {  get; set; }
    public ConnectionStrings ConnectionStrings { get; set; } = new();

}
public class ConnectionStrings
{
    public string DefaultConection { get; set; }
}