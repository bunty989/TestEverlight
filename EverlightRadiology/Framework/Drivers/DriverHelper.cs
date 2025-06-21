using EverlightRadiology.Framework.Wrapper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using Serilog;
using System.Drawing;
using Browser = EverlightRadiology.Framework.Wrapper.TestConstant.BrowserType;

namespace EverlightRadiology.Framework.Drivers
{
    public class DriverHelper
    {
        public IWebDriver? Driver;
        private string? _browserName;
        private string? _browserVersion;
        private BrowserVersionHelper _browserVersionHelper => new();

        public IWebDriver? InvokeDriverInstance(Browser browserType)
        {
            _browserVersion = BrowserVersionHelper.GetBrowserVersion(browserType);
            switch (browserType)
            {
                case Browser.Chrome:
                    {
                        var chromeOption = new ChromeOptions();
                        chromeOption.AddArguments("start-maximized", "--disable-gpu", "--no-sandbox", "--ignore-certificate-errors");
                        chromeOption.AddExcludedArgument("enable-automation");
                        //chromeOption.AddAdditionalCapability("useAutomationExtension", false);
                        chromeOption.AddUserProfilePreference("credentials_enable_service", false);
                        chromeOption.AddUserProfilePreference("profile.password_manager_enabled", false);
                        chromeOption.PageLoadStrategy = PageLoadStrategy.Eager;
                        Driver = new ChromeDriver(chromeOption);
                        break;
                    }
                case Browser.InternetExplorer:
                    {
                        var ieOptions = new InternetExplorerOptions
                        {
                            IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                            RequireWindowFocus = true,
                            EnsureCleanSession = true,
                            IgnoreZoomLevel = true
                        };
                        ieOptions.AddAdditionalInternetExplorerOption(CapabilityType.AcceptSslCertificates, true);
                        ieOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                        Driver = new InternetExplorerDriver(ieOptions);
                        break;
                    }
                case Browser.Firefox:
                    {
                        var ffOptions = new FirefoxOptions
                        {
                            AcceptInsecureCertificates = true
                        };
                        ffOptions.SetPreference("permissions.default.image", 2);
                        ffOptions.AddArguments("--width=1200", "--height=800");
                        ffOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                        Driver = new FirefoxDriver(ffOptions);
                        break;
                    }
                case Browser.FirefoxHeadless:
                    {
                        var ffOptions = new FirefoxOptions
                        {
                            AcceptInsecureCertificates = true
                        };
                        ffOptions.SetPreference("permissions.default.image", 2);
                        ffOptions.AddArguments("--width=1200", "--height=800", "-headless");
                        Driver = new FirefoxDriver(ffOptions);
                        break;
                    }
                case Browser.Edge:
                    {
                        var edgeOptions = new EdgeOptions
                        {
                            AcceptInsecureCertificates = true,
                            PageLoadStrategy = PageLoadStrategy.Eager
                        };
                        edgeOptions.AddArguments("disable-gpu", "no-sandbox", "window-size=1280,800");
                        Driver = new EdgeDriver(edgeOptions);
                        break;
                    }
                case Browser.EdgeHeadless:
                    {
                        var edgeOptions = new EdgeOptions
                        {
                            AcceptInsecureCertificates = true,
                            PageLoadStrategy = PageLoadStrategy.Eager
                        };
                        edgeOptions.AddArguments("disable-gpu", "no-sandbox", "window-size=1280,800", "--headless=new");
                        Driver = new EdgeDriver(edgeOptions);
                        break;
                    }
                case Browser.ChromeHeadless:
                    {
                        var chromeOption = new ChromeOptions();
                        chromeOption.AddArguments("disable-gpu", "no-sandbox", "--window-size=1280,800", "--headless=new", "--ignore-certificate-errors");
                        chromeOption.AddExcludedArgument("enable-automation");
                        chromeOption.AddUserProfilePreference("credentials_enable_service", false);
                        chromeOption.AddUserProfilePreference("profile.password_manager_enabled", false);
                        chromeOption.PageLoadStrategy = PageLoadStrategy.Eager;
                        Driver = new ChromeDriver(chromeOption);
                        break;
                    }
                case Browser.ChromeIncognito:
                    {
                        var chromeOption = new ChromeOptions();
                        chromeOption.AddArguments("start-maximized", "--disable-gpu", "--no-sandbox", "--incognito", "--ignore-certificate-errors");
                        chromeOption.AddExcludedArgument("enable-automation");
                        chromeOption.AddAdditionalChromeOption("useAutomationExtension", false);
                        chromeOption.AddUserProfilePreference("credentials_enable_service", false);
                        chromeOption.AddUserProfilePreference("profile.password_manager_enabled", false);
                        chromeOption.PageLoadStrategy = PageLoadStrategy.Eager;
                        Driver = new ChromeDriver(chromeOption);
                        break;
                    }
                case Browser.Safari:
                    {
                        var safariOptions = new SafariOptions
                        {
                            AcceptInsecureCertificates = true,
                            PageLoadStrategy = PageLoadStrategy.Eager
                        };
                        Driver = new SafariDriver(safariOptions);
                        break;
                    }
                default:
                    {
                        var chromeOption = new ChromeOptions();
                        chromeOption.AddArguments("start-maximized", "--disable-gpu", "--no-sandbox");
                        chromeOption.AddExcludedArgument("enable-automation");
                        chromeOption.AddAdditionalChromeOption("useAutomationExtension", false);
                        chromeOption.AddUserProfilePreference("credentials_enable_service", false);
                        chromeOption.AddUserProfilePreference("profile.password_manager_enabled", false);
                        chromeOption.PageLoadStrategy = PageLoadStrategy.Eager;
                        Driver = new ChromeDriver(chromeOption);
                        break;
                    }
            }
            _browserName = browserType.ToString();
            Log.Information("Started {0} WebDriver successfully", _browserName);
            Driver.Manage().Window.Maximize();
            Driver.Manage().Window.Size = new Size(1920, 1080);
            Driver.Manage().Timeouts().ImplicitWait =
                TimeSpan.FromSeconds(int.Parse
                (ConfigHelper.ReadConfigValue
                (TestConstant.ConfigTypes.WebDriverConfig, TestConstant.ConfigTypesKey.ImplicitWaitTimeout)));
            Driver.Manage().Timeouts().PageLoad =
                TimeSpan.FromSeconds(int.Parse
                (ConfigHelper.ReadConfigValue
                    (TestConstant.ConfigTypes.WebDriverConfig, TestConstant.ConfigTypesKey.PageLoadTimeOut)));
            return Driver;
        }

        public void Navigate(string url)
        {
            Driver?.Navigate().GoToUrl(url);
            Log.Information("Driver successfully Navigated to {0}", Driver?.Url);
        }

        public void QuitDriverInstance()
        {
            if (Driver == null)
            {
                Log.Information("Driver Instance already Killed");
                return;
            }
            try
            {
                Driver.Quit();
                Log.Information("Quit {0} WebDriver successfully", _browserName);
            }
            catch (Exception e)
            {
                Log.Error("Unable to Quit {0} WebDriver due to {1}", _browserName, e.Message);
            }
        }
    }
}
