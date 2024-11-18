using Microsoft.Extensions.Configuration;

namespace Core
{
    public static class ConfigurationLoader
    {
        private static readonly IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
           .Build();

        public static List<string> GetBrowsers()
        {
            var browsers = configuration.GetSection("Browser").Get<List<string>>();

            if (browsers == null || browsers.Count == 0)
            {
                throw new KeyNotFoundException("Browser configuration is missing or empty.");
            }

            return browsers;
        }
    }
}
