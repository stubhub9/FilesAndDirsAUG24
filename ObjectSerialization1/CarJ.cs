using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectSerialization1
{
    public class CarJ
    {
        [JsonInclude]
        public RadioJ TheRadio = new RadioJ();
        [JsonInclude]
        public bool IsHatchBack;
        public override string ToString () => $"IsHatchback:  {IsHatchBack}, Radio:  {TheRadio.ToString()}";
    }
}
