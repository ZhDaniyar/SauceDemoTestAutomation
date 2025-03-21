using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace SauceDemoUITests.Drivers
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateDriver(string browser)
        {
            if (browser.Equals("chrome", StringComparison.OrdinalIgnoreCase))
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                return new ChromeDriver();
            }
            else if (browser.Equals("edge", StringComparison.OrdinalIgnoreCase))
            {
                try
                {

                    var edgeOptions = new EdgeOptions();

                    edgeOptions.AddArgument("--ignore-certificate-errors");

                    var service = EdgeDriverService.CreateDefaultService();


                    return new EdgeDriver(service, edgeOptions);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to initialize Edge driver with options: {ex.Message}");

                    Console.WriteLine("Attempting direct Edge driver initialization...");

                    try
                    {
                        return new EdgeDriver();
                    }
                    catch (Exception innerEx)
                    {
                        Console.WriteLine($"All Edge driver initialization attempts failed: {innerEx.Message}");
                        Console.WriteLine("Please install Microsoft Edge WebDriver from:");
                        Console.WriteLine("https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/");
                        throw;
                    }
                }
            }
            else
            {
                throw new ArgumentException($"Unsupported browser: {browser}");
            }
        }
    }
}