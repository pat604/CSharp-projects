using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.ServiceModel;
using SorozatServiceLibrary;
using System.IO;

namespace HostApp
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlGenerate();

            ServiceHost host = new ServiceHost(typeof(SorozatService));
            host.Open();
            Console.ReadLine();
             
        }

        static private void XmlGenerate()
        {
            // SOROZATOK ÉS EPIZÓDOK

            List<Sorozat> sorozatlista = new List<Sorozat>();

            for (int i = 0; i < 3; i++)
            {
                List<Epizod> epizodlista = new List<Epizod>();
                for (int j = 0; j < 4; j++)
                {
                    epizodlista.Add(new Epizod() { Cim = "epizod" + i + j, Hossz = 50 + j });
                }

                sorozatlista.Add(new Sorozat() { SorozatCim = "sorozat" + i, Epizodok = epizodlista });
            }

            // XML

            if (File.Exists("sorozatok.xml"))
                File.Delete("sorozatok.xml");

            XDocument xdoc = new XDocument();
            XElement root = new XElement("root");

            foreach (Sorozat sorozat in sorozatlista)
            {            
                XElement ujsorozat = new XElement("sorozat", new XAttribute("sorozatcim", sorozat.SorozatCim)); 
                foreach (Epizod epizod in sorozat.Epizodok)
                {
                    ujsorozat.Add(new XElement("epizod",
                        new XElement("cim", epizod.Cim), new XElement("hossz", epizod.Hossz)));
                }

                root.Add(ujsorozat);
            }

            xdoc.Add(root);
            xdoc.Save("sorozatok.xml");
        }
    }
}
