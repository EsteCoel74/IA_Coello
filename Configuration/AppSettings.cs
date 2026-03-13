using System.Text.Json;
using System.IO;

namespace IA_Coello.Configuration
{
    public class AppSettings
    {
        public OpenAiSettings OpenAI { get; set; } = new();

        public static AppSettings LoadSettings()
        {
            var appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(appDirectory, "appsettings.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Le fichier {filePath} n'a pas été trouvé.");
            }

            var json = File.ReadAllText(filePath);
            var settings = JsonSerializer.Deserialize<AppSettings>(json);

            if (settings?.OpenAI?.ApiKey == null || string.IsNullOrWhiteSpace(settings.OpenAI.ApiKey))
            {
                throw new InvalidOperationException("La clé API OpenAI n'est pas configurée dans appsettings.json");
            }

            return settings ?? new AppSettings();
        }
    }

    public class OpenAiSettings
    {
        public string ApiKey { get; set; } = string.Empty;
    }
}
