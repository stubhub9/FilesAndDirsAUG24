using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectSerialization1
{
    public class Person
    {
        public bool IsAlive = true;
        //  Only public data was serialized.
        private int PersonAge = 21;
        private string _fName = string.Empty;
        public string FirstName
        {
            get { return _fName; }
            set { _fName = value; }
        }
        public override string ToString () =>
             $"IsAlive:  {IsAlive}, FirstName:  {FirstName}, Age:{PersonAge}";

    }
}
