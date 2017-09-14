using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebdriverClass.Pages;

namespace WebdriverClass.Widgets
{
    class wikiSearchWidget : BasePage
    {
        public wikiSearchWidget(IWebDriver driver) : base(driver)
        {  }

        // html elements
        [FindsBy(How = How.Id, Using = "searchLanguage")]
        public IWebElement searchLanguage { get; set; }
        // <option value = "hu" lang="hu">Magyar</option>

        [FindsBy(How = How.Id, Using = "searchInput")]
        public IWebElement searchInput { get; set; }

        [FindsBy(How = How.ClassName, Using = "pure-button")]
        public IWebElement searchButton { get; set; }


        public void setLanguage(String lang)
        {
            new SelectElement(searchLanguage).SelectByValue(lang);
        }

        public void TypeText(String text)
        {
            searchInput.SendKeys(text);
        }

        public wikiResultPage Submit()
        {
            searchButton.Click();

            wikiResultPage wikiResultPage = new wikiResultPage(driver);
            PageFactory.InitElements(driver, wikiResultPage);
            return wikiResultPage;
        }








    }
}
