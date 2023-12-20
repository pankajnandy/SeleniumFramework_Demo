using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using SeleniumFramework_Demo.Pages;
using SeleniumFramework_Demo.Resources;

namespace SeleniumFramework_Demo.Tests
{
    [TestClass]
    [TestCategory("RegressionTest")]
    public class WTW_WebAutomation 
    {
        //WebDriver driver;
        public IWebDriver Driver { get; private set; }

        [TestInitialize]
        public void Init()
        {
            //var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //driver = new ChromeDriver(path);
            var factory = new WebDriverFactory();
            Driver = factory.Create(BrowserType.Chrome);


        }

        /// <summary>
        /// Test Scenarios Execution 
        /// </summary>
        [TestMethod]
        public void VerifyUrls_IFRS_17_Search_Article_Filter()
        {

            var wtwBasePage = new WtwBasePage(Driver);

            //Given open webUrl
            wtwBasePage.OpenUrl("https://www.wtwco.com/");
            wtwBasePage.Wait(300);

            wtwBasePage.SelectLocLanguage("en-us");
                        
            //When search test and sort by Date
            wtwBasePage.Search("IFRS 17");
            //wtwBasePage.Wait(500);
            Thread.Sleep(200);

            WebElement element = (WebElement)Driver.FindElement(By.ClassName("CoveoQuerySummary"));
            string actualtext = element.Text;

            StringAssert.Matches(actualtext, new Regex(@"Results\s[0-9]+-\d\d of \d\d for IFRS 17"));

            string searchByDate = wtwBasePage.IsDateSelected();
            if(searchByDate == "false")
            {
                wtwBasePage.SelectDateSearch();
            }

            wtwBasePage.SelectContentType("Article");

            //Then verify url start with following 

            List<string> expected = new List<string>();
            expected = wtwBasePage.FindArticleLinks();
            foreach (string url in expected)
            {
                StringAssert.StartsWith(url, "https://www.wtwco.com/en-us/", StringComparison.CurrentCulture);
            }
            
        }

        /// <summary>
        /// Tear-Down Test
        /// After each test is executed. 
        /// Driver will close the Browser.
        /// </summary>
        [TestCleanup]
        public void CloseBrowser()
        {
            Driver.Close();
            Driver.Quit();
        }



    }
}