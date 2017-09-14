using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SorozatServiceLibrary
{   
    [ServiceContract]
    public interface ISorozatService
    {
        [OperationContract]
        List<string> Sorozatok();

        [OperationContract]
        List<Epizod> Epizodok(string cim);



       
    }

   

    }

