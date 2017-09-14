using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;


namespace WebdriverClass
{
    [TestFixture]
    class TestBase
    {
        IWebDriver driver;

        public IWebDriver Driver
        {
            get
            { return driver; }
            set
            {
                driver.Quit();
                driver = value;
            }
        }

        [SetUp]
        protected void Setup()
        {
            //Firefox driver changed to Chrome because of driver issues
            //
            //FirefoxProfile profile = new FirefoxProfile();
            //profile.EnableNativeEvents = false;
            // Create a new instance of the Firefox driver
            //driver = new FirefoxDriver(profile);

            driver = new ChromeDriver();
        }

        [TearDown]
        protected void Teardown()
        {
            driver.Quit();
        }
    }
}
