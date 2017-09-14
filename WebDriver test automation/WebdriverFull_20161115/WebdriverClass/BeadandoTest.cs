using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebdriverClass.Pages;
using WebdriverClass.Widgets;

namespace WebdriverClass
{
    class BeadandoTest : TestBase
    {
        [Test, TestCaseSource("langData")]
        public void wikipediaTest(String lang, String text, String filename, String filename2, String filenameBP, String filename2BP)
        {
            wikiSearchWidget wikiSearchWidget = wikiHomePage.navigate(Driver).getSearchWidget();
            wikiSearchWidget.setLanguage(lang);
            wikiSearchWidget.TypeText(text);

            wikiResultPage wikiResultPage = wikiSearchWidget.Submit();
            wikiResultPage.areTitlesTheSame(text, filename); // test 1

            infoBoxWidget infoBoxWidget = wikiResultPage.getInfoBoxWidget();
            infoBoxWidget.isInfoBoxDisplayed(); // test 2

            tableOfContentsWidget tocWidget = wikiResultPage.getTableOfContentsWidget();
            tocWidget.isTOCDisplayed(); // test 3

            wikiResultPage.areHeading2sTheSame(filename2); // test 4


            // Budapest link
            wikiResultPage wikiResultPageBP = wikiResultPage.navigate(Driver);
            wikiResultPageBP.areTitlesTheSame("Budapest", filenameBP); // test 1

            infoBoxWidget infoBoxWidgetBP = wikiResultPageBP.getInfoBoxWidget();
            infoBoxWidgetBP.isInfoBoxDisplayed(); // test 2

            tableOfContentsWidget tocWidgetBP = wikiResultPageBP.getTableOfContentsWidget();
            tocWidgetBP.isTOCDisplayed(); // test 3

            wikiResultPageBP.areHeading2sTheSame(filename2BP); // test 4
        }


        static IEnumerable langData()
        {
            var doc = XElement.Load(AssemblyDirectory + "\\langData.xml");
            
            return
                from vars in doc.Descendants("langdata")
                let lang = vars.Attribute("lang").Value
                let text = vars.Attribute("text").Value
                let filename = vars.Attribute("filename").Value
                let filename2 = vars.Attribute("filename2").Value
                let filenameBP = vars.Attribute("filenameBP").Value
                let filename2BP = vars.Attribute("filename2BP").Value
                select new object[] { lang, text, filename, filename2, filenameBP, filename2BP };
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
