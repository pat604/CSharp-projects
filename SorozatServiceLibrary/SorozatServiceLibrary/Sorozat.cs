using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SorozatServiceLibrary
{
    [DataContract]
    public class Sorozat
    {
        [DataMember]
        public List<Epizod> Epizodok { get; set; }

        [DataMember]
        public string SorozatCim { get; set; }
    }
}
