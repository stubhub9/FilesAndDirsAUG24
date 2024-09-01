using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectSerialization1
{
    public class JsonStringNullToEmptyConverter : JsonConverter <string>
    {

        public override bool HandleNull  => true;




        public override string Read ( ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options )
        {
            var value = reader.GetString ();
            if (string.IsNullOrEmpty (value))
            {
                return null;
            }
            return value;
        }

        public override void Write ( Utf8JsonWriter writer, string value, JsonSerializerOptions options )
        {
            value ??= string.Empty;
            writer.WriteStringValue ( value );
        }

        /*  Add to Program?
        static void HandleNullStrings ()
        {

        }
        */
    }
}
