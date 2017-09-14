using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebdriverClass.Pages;

namespace WebdriverClass.Widgets
{
    class infoBoxWidget : BasePage
    {
        
        public infoBoxWidget(IWebDriver driver) : base(driver)
        {

        }

        [FindsBy(How = How.ClassName, Using = "infobox")]
        public IWebElement infoBox { get; set; }


        public int getNoOfResults()
        {
            return (infoBox.FindElements(By.TagName("tr")).Count());
        }

        public void isInfoBoxDisplayed()
        {
            // Assert.Greater(getNoOfResults(), 2);
            Assert.True((getNoOfResults() > 2) && infoBox.Displayed);
        }
    }
}
