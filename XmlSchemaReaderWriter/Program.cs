// See https://aka.ms/new-console-template for more information


using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

class XmlSchemaReadWriteExample
{
    static void Main ()
    {
        try
        {
            XmlTextReader reader = new XmlTextReader ( "example.xsd" );
            XmlSchema schema = XmlSchema.Read ( reader, ValidationCallback );
            schema.Write ( Console.Out );
            FileStream file = new FileStream ( "new.xsd", FileMode.Create, FileAccess.ReadWrite );
            XmlTextWriter xwriter = new XmlTextWriter ( file, new UTF8Encoding () );
            xwriter.Formatting = Formatting.Indented;
            schema.Write ( xwriter );
        }
        catch (Exception e)
        {
            Console.WriteLine ( e );
        }
    }

    static void ValidationCallback ( object sender, ValidationEventArgs args )
    {
        if (args.Severity == XmlSeverityType.Warning)
            Console.Write ( "WARNING: " );
        else if (args.Severity == XmlSeverityType.Error)
            Console.Write ( "ERROR: " );

        Console.WriteLine ( args.Message );
    }
}


/*  Input as example.xsd
<? xml version = "1.0" ?>
< xs : schema id = "play" targetNamespace = "h ttp://tempuri.org/play.xsd" elementFormDefault = "qualified" xmlns = "h ttp://tempuri.org/play.xsd" xmlns: xs = "h ttp://www.w3.org/2001/XMLSchema" >
    < xs:element name = 'myShoeSize' >
        < xs:complexType >
            < xs:simpleContent >
                < xs:extension base='xs:decimal'>  
                    <xs:attribute name = 'sizing' type='xs:string' />  
                </xs:extension >
            </ xs:simpleContent >
        </ xs:complexType >
    </ xs:element >
</ xs:schema >
*/