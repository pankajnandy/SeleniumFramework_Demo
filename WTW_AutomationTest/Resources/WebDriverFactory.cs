using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework_Demo.Resources
{
    public class WebDriverFactory
    {
        public  IWebDriver Create(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    return GetChromeDriver();
                case BrowserType.Firefox:
                    return GetFirefoxDriver();
                default:
                    throw new ArgumentOutOfRangeException("Browser Type do not exist");
            }

        }

        private IWebDriver GetChromeDriver()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return new ChromeDriver(path);

        }

        private IWebDriver GetFirefoxDriver()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return new FirefoxDriver(path);
        }
    }
}
