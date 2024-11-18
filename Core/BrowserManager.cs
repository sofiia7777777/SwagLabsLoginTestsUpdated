using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Core
{
    public static class BrowserManager
    {
        public static IWebDriver GetDriver(string browserType)
        {
            IWebDriver driver;

            switch (browserType)
            {
                case "Chrome":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--headless");
                    driver = new ChromeDriver(chromeOptions);
                    break;

                case "Firefox":
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArgument("--headless");
                    driver = new FirefoxDriver(firefoxOptions);
                    break;

                case "Edge":
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("--headless");
                    driver = new EdgeDriver(edgeOptions);
                    break;
                default:
                    throw new ArgumentException("Unsupported browser type");
            }

            return driver;
        }
    }
}
