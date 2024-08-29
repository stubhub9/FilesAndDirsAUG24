using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectSerialization1
{
    public class Radio
    {
        public bool HasTweeters;
        public bool HasSubWoofers;
        public List <double> StationPresets;
        public string RadioId = "XF-5";
        public override string ToString ()
        {
            var presets = string.Join(",", StationPresets.Select ( i => i.ToString () ).ToList () );

        }

    }
}
