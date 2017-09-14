using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using WebdriverClass.Widgets;

namespace WebdriverClass.Pages
{
    class wikiHomePage : BasePage
    {
        private wikiSearchWidget searchWidget;

        public wikiHomePage(IWebDriver driver) : base(driver)
        {

        }

        public static wikiHomePage navigate(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://www.wikipedia.org/");
            return new wikiHomePage(driver);
        }

        public wikiSearchWidget getSearchWidget()
        {
            searchWidget = new wikiSearchWidget(driver);
            PageFactory.InitElements(driver, searchWidget);
            return searchWidget;
        }
    }
}
