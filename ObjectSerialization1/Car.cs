using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectSerialization1
{
    public class Car
    {
        public Radio TheRadio = new Radio();
        public bool IsHatchBack;
        public override string ToString () => $"IsHatchback:  {IsHatchBack}, Radio:  {TheRadio.ToString()}";
    }
}
