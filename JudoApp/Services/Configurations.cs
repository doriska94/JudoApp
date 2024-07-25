using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoApp.Services;
public class Configurations
{
    public static string GetDefaultConectionString()
    {
        return GetSettings().ConnectionStrings.DefaultConection;
    }
    public static AppSettings GetSettings()
    {
        var settings = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText("appsettings.json"));
        if (settings == null)
            settings = new AppSettings();
        return settings;
    }
}

public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; } = new();

}
public class ConnectionStrings
{
    public string DefaultConection {  get; set; }
}