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
    class tableOfContentsWidget : BasePage
    {
        public tableOfContentsWidget(IWebDriver driver) : base(driver)
        {

        }

        [FindsBy(How = How.Id, Using = "toc")]
        public IWebElement tableOfContents { get; set; }

        [FindsBy(How = How.ClassName, Using = "toclevel-1")]
        public IList<IWebElement> tocHeadings { get; set; }        // list

        public int getNoOfResults()
        {
            return (tableOfContents.FindElements(By.TagName("li")).Count());
        }

        public void isTOCDisplayed()
        {
            // Assert.Greater(getNoOfResults(), 2);
            Assert.True((getNoOfResults() > 2) && tableOfContents.Displayed);
        }

    }
}
