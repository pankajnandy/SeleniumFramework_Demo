using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using SeleniumExtras.WaitHelpers;
using NLog;

namespace SeleniumFramework_Demo.Pages
{
    internal class WtwBasePage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public WtwBasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebDriver Driver { get; }

        internal void OpenUrl(string weburl)
        {
            Driver.Navigate().GoToUrl(weburl);

            _logger.Info("Opened url :" + weburl);

            if (Driver.FindElement(By.Id("onetrust-accept-btn-handler")).Displayed)
            {
                _logger.Info("Accept Cookies Page is Displayed");
                Driver.FindElement(By.Id("onetrust-accept-btn-handler")).Click();
            }
            Driver.Manage().Window.Maximize();
            _logger.Info("Application Window is maximized");

        }

        /// <summary>
        /// To select the location and region.
        /// </summary>
        /// <param name="language"></param>
        internal void SelectLocLanguage(string language)
        {

            Driver.FindElement(By.ClassName("material-symbols-outlined")).Click();
            //Driver.FindElement(By.XPath("/html/body/div[3]/div[1]/nav[1]/button")).Click();

            //Driver.FindElement(By.Id("region-0")).Click();
            //Driver.FindElement(By.XPath("/html/body/div[3]/div[2]/nav/div[4]/div[3]/div/div/div/div/div[1]/button/span[2]")).Click();
            Driver.FindElement(By.XPath("//*[@id='region-0']/span[1]/span")).Click();

            //Driver.Manage().Window.Maximize();
            IList<IWebElement> ListOfElements = Driver.FindElements(By.TagName("a"));
            
            foreach (var item in ListOfElements)
            {
                if (item.GetAttribute("href").Contains(language))
                {
                    //string cssval = item.GetCssValue("href");
                    string url = item.GetAttribute("href");
                    Driver.Navigate().GoToUrl(url);
                    //var eleType = item.GetType();
                    break;
                }
            }

        }

        /// <summary>
        /// To search any text in the main search bar.
        /// </summary>
        /// <param name="searchTerm"></param>
        internal void Search(string searchTerm)
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[3]/div[2]/button[2]")));

            Driver.FindElement(By.XPath("/html/body/div[3]/div[2]/button[2]")).Click();
            Driver.FindElement(By.XPath("/html/body/div[3]/div[2]/section/div/div/div/div/div/div[2]/div[4]/div[1]/form/input")).SendKeys(searchTerm);
            Driver.FindElement(By.XPath("/html/body/div[3]/div[2]/section/div/div/div/div/div/div[2]/div[4]/div[1]/form/a")).Click();

            wait.Until(Driver => Driver.FindElement(By.ClassName("CoveoQuerySummary")).Text is not null);
            //wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("CoveoQuerySummary")));
            
        }

        internal void Wait(int seconds)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
        }

        /// <summary>
        /// This checks thether the search is displayed based on Date.
        /// returns "true" or "false"
        /// </summary>
        /// <returns></returns>
        internal string IsDateSelected()
        {
            //var d = Driver.ExecuteJavaScript<bool>("document.querySelector('#coveo9de96e90 > span:nth-child(1)').ariaChecked");

           return  Driver.FindElement(By.XPath("//*[@class='CoveoSort']")).GetAttribute("aria-checked");
        }

        /// <summary>
        /// This is to enable serach result based on date
        /// </summary>
        internal void SelectDateSearch()
        {
            Driver.FindElement(By.XPath("//*[@class='CoveoSort']")).Click();
            //Driver.FindElement(By.XPath("/html/body/main/section/div/div/div[1]/div[4]/div/div[2]/div[2]/div[3]/div[2]/div[2]/span/span[1]")).Click();
        }

        /// <summary>
        /// To filter the search result based on Content Type 
        /// This is used to check the available valid check box
        /// </summary>
        /// <param name="contentType"></param>
        internal void SelectContentType(string contentType)
        {
            Driver.FindElement(By.XPath("//*[@class='coveo-facet-value-caption'][@title='"+contentType+"']")).Click();
        }

        /// <summary>
        /// This will return List of all links displayed in the Search result
        /// based on the filters selected.
        /// </summary>
        /// <returns></returns>
        internal List<string> FindArticleLinks()
        {
            List<string> Links = new List<string>();

                IList<IWebElement> ListOfArticles = Driver.FindElements(By.XPath("//*[@class='CoveoResultLink']"));

                foreach (var item in ListOfArticles)
                {

                    string url = item.GetAttribute("href");
                    Links.Add(url);

                }

            return Links;
        }
    }
}