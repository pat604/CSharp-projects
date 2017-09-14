using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebdriverClass.Widgets;

namespace WebdriverClass.Pages
{
    class wikiResultPage : BasePage
    {

        private infoBoxWidget infoBoxWidget;
        private tableOfContentsWidget tableOfContentsWidget;

        public wikiResultPage(IWebDriver driver) : base(driver)
        {

        }

        public infoBoxWidget getInfoBoxWidget()
        {
            infoBoxWidget = new infoBoxWidget(driver);
            PageFactory.InitElements(driver, infoBoxWidget);
            return infoBoxWidget;
        }

        public tableOfContentsWidget getTableOfContentsWidget()
        {
            tableOfContentsWidget = new tableOfContentsWidget(driver);
            PageFactory.InitElements(driver, tableOfContentsWidget);
            return tableOfContentsWidget;
        }

        // Tartalmazza a főcím és az alcímek definícióját (H1, H2), illetve a Budapest linket
        [FindsBy(How = How.PartialLinkText, Using = "Budapest")]
        public IWebElement linkBudapest { get; set; }

        [FindsBy(How = How.Id, Using = "firstHeading")]
        public IWebElement Heading1 { get; set; }



        public void areTitlesTheSame(String text, String filename)
        {
            // StringAssert.Contains(text, Heading1.Text); // a Heading1.Text tartalmazza a [bevezető szerkesztése] szöveget is, amit nem tudtam leválasztani a címtől lokátorokkal
            Assert.True(Heading1.Text.Contains(text) && Heading1.Displayed);

            if (File.Exists(filename))
                File.Delete(filename);

            StreamWriter sw = new StreamWriter(filename);
            sw.Write(Heading1.Text + '\n' + text);
            sw.Close();

        }

        public void areHeading2sTheSame(String filename)
        {
            IList<IWebElement> headings = driver.FindElements(By.ClassName("mw-headline"));

            if (File.Exists(filename))
                File.Delete(filename);
            StreamWriter sw = new StreamWriter(filename);

            foreach (IWebElement element in headings)
            {
                sw.Write(element.Text + '\n');
            }

            sw.Close();

            foreach (IWebElement element in tableOfContentsWidget.tocHeadings)
            {
                element.Click();
                CollectionAssert.IsNotEmpty(headings.Select(a => a.Text.Equals(element.Text) && a.Displayed));                
            }
        }
    

    public wikiResultPage navigate(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(linkBudapest.GetAttribute("href"));

            wikiResultPage wikiResultPage = new wikiResultPage(driver);
            PageFactory.InitElements(driver, wikiResultPage);
            return wikiResultPage;
        }


    }
}
