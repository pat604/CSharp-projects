using OpenQA.Selenium;

namespace WebdriverClass.Pages
{
    class BasePage
    {
        public IWebDriver driver;

        public BasePage(IWebDriver webDriver)
        {
            driver = webDriver;
        }
    }
}
