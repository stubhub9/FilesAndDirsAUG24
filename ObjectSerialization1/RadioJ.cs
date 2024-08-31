using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectSerialization1
{
    public class RadioJ
    {
        [JsonInclude]
        public bool HasTweeters;
        [JsonInclude]
        public bool HasSubWoofers;
        [JsonInclude]
        public List <double> StationPresets;
        [JsonInclude]
        public string RadioId = "XF-5";
        public override string ToString ()
        {
            var presets = string.Join(",", StationPresets.Select ( i => i.ToString () ).ToList () );
            return $"HasTweeters:  {HasTweeters}, Has Subwoofers:  {HasSubWoofers}, Station Presets:  {presets}";
        }
    }
}
