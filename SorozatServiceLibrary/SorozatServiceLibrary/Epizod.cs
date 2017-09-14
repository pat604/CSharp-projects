using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SorozatServiceLibrary
{
    [DataContract]
    public class Epizod
    {
        [DataMember]
        public string Cim { get; set; }

        [DataMember]
        public int Hossz { get; set; }
    }
}
