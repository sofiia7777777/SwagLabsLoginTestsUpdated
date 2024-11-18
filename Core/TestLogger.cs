using Serilog;

namespace Core
{
    public static class TestLogger
    {
        static TestLogger()
        {
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("C:\\Users\\advok\\source\\repos\\SwagLabs\\Core\\log.txt", rollingInterval: RollingInterval.Infinite)
                .CreateLogger();
        }

        public static void Initialize()
        {
            Log.Information("Initializing the logger.");
        }
    }
}
