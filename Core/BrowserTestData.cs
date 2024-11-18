using System.Collections.Generic;

namespace Core
{
    public static class BrowserTestData
    {
        public static IEnumerable<object[]> GetBrowserTestData()
        {
            var browsers = ConfigurationLoader.GetBrowsers();

            foreach (var browser in browsers)
            {
                yield return new object[] { browser };
            }
        }

        public static IEnumerable<object[]> GetBrowserAndUserNameTestData()
        {
            var browsers = ConfigurationLoader.GetBrowsers();

            var users = new[]
            {
                "standard_user",
                "problem_user",
                "performance_glitch_user",
                "error_user",
                "visual_user"
            };

            foreach (var browser in browsers)
            {
                foreach (var user in users)
                {
                    yield return new object[] { browser, user };
                }
            }
        }
    }
}
