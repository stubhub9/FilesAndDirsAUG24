using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectSerialization1
{
    //  Adding the Attributes, moved the CanFly and CanSub from the body to the XML header
    //  to www.w3.org/2001/XMLSchema" CanFly="true" CanSubmerge="false"
    [Serializable, XmlRoot(Namespace = "http://www.MyCompany.com")]
    public class SpyCar : Car
    {
        [XmlAttribute]
        public bool CanFly;
        [XmlAttribute]
        public bool CanSubmerge;
        public override string ToString () => 
            $"CanFly:  {CanFly}, CanSubmerge:  {CanSubmerge}, {base.ToString()}";
    }
}
