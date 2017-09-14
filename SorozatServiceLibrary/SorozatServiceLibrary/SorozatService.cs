using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;

namespace SorozatServiceLibrary
{  
    public class SorozatService : ISorozatService
    {

        public List<string> Sorozatok()
        {
            XDocument xdoc = new XDocument();
            if (File.Exists("sorozatok.xml"))
                xdoc = XDocument.Load("sorozatok.xml");

            List<string> sorozatoklista = new List<string>();

            foreach (var item in xdoc.Descendants("sorozat"))
            {
                sorozatoklista.Add(item.Attribute("sorozatcim").Value);
            }

            return sorozatoklista;
        }

        public List<Epizod> Epizodok(string cim)
        {
            XDocument xdoc = new XDocument();
            if (File.Exists("sorozatok.xml"))
                xdoc = XDocument.Load("sorozatok.xml");

            List<Epizod> epizodlista = new List<Epizod>();

            foreach (var item in xdoc.Descendants("sorozat"))
            {
                if (item.Attribute("sorozatcim").Value == cim)
                    foreach (var item2 in item.Descendants("epizod"))
                    {
                        epizodlista.Add(new Epizod() { Cim = item2.Element("cim").Value, Hossz = (int)item2.Element("hossz") });
                    }
            }

            return epizodlista;
        }
    }
}
