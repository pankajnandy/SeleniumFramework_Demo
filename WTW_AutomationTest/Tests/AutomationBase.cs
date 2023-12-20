using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;
using SeleniumFramework_Demo.Resources;

namespace SeleniumFramework_Demo.Tests
{
    [TestClass]
    public class AutomationBase
    {
        IWebDriver driver;
        /// <summary>
        /// SetUp that will run before every test.
        /// Driver initialization
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            //var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //driver = new ChromeDriver(path);

            var factory = new WebDriverFactory();
            driver = factory.Create(BrowserType.Firefox);

        }

        /// <summary>
        /// Tear-Down Test
        /// After each test is executed. 
        /// Driver will close the Browser.
        /// </summary>
        [TestCleanup]
        public void CloseBrowser()
        {
            driver.Close();
        }

    }
}