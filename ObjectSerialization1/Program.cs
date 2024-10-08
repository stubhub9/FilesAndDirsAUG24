﻿//  Object Serialization

global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Xml;
global using System.Xml.Serialization;

//  Thought this using was causing an Error, until I added something that needed it. (?)
using ObjectSerialization1;
//using FilesAndDirsAUG24;



#region  Fields declared and init.

ConsoleColor prevBgColor = Console.BackgroundColor;
ConsoleColor newBgColor = (ConsoleColor)1;

var theRadio = new Radio
{
    StationPresets = new () { 89.9, 92.3, 94.7 },
    HasTweeters = true
};

SpyCar spycar = new ()
{
    CanFly = true,
    CanSubmerge = false,
    TheRadio = new ()
    {
        StationPresets = new () { 89.9, 92.3, 94.7 },
        HasTweeters = true
    }
};

List<SpyCar> myCars = new ()
{
    new SpyCar { CanFly = true, CanSubmerge = true, TheRadio = theRadio },
    new SpyCar { CanFly = true, CanSubmerge = false, TheRadio = theRadio },
    new SpyCar { CanFly = false, CanSubmerge = true, TheRadio = theRadio },
    new SpyCar { CanFly = false, CanSubmerge = false, TheRadio = theRadio },
    };

Person person = new Person
{
    FirstName = "James",
    IsAlive = true,
};

#endregion XmlFields
#region  JSON Fields


var theRadioJ = new RadioJ
{
    StationPresets = new () { 89.9, 92.3, 94.7 },
    HasTweeters = true
};

SpyCarJ spycarJ = new ()
{
    CanFly = true,
    CanSubmerge = false,
    TheRadio = new ()
    {
        StationPresets = new () { 89.9, 92.3, 94.7 },
        HasTweeters = true
    }
};

List<SpyCarJ> myCarsJ = new ()
{
    new SpyCarJ { CanFly = true, CanSubmerge = true, TheRadio = theRadioJ },
    new SpyCarJ { CanFly = true, CanSubmerge = false, TheRadio = theRadioJ },
    new SpyCarJ { CanFly = false, CanSubmerge = true, TheRadio = theRadioJ},
    new SpyCarJ { CanFly = false, CanSubmerge = false, TheRadio = theRadioJ },
    };

PersonJ personJ = new PersonJ
{
    FirstName = "James",
    IsAlive = true,
};


JsonSerializerOptions options = new JsonSerializerOptions ()
{
    PropertyNameCaseInsensitive = true,
    PropertyNamingPolicy = null,  // Pascal Casing
    IncludeFields = true,
    WriteIndented = true,
    NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
};

JsonSerializerOptions optionsWeb = new ( JsonSerializerDefaults.Web )
{
    // also .General
    //  .Web attr should include:  PropNameCaseIns, JsonNaming Camel, and string read numbers
    WriteIndented = true,
    ReferenceHandler = ReferenceHandler.IgnoreCycles,
};



#endregion  JSON Fields
#region  Top Level Statements


Console.BackgroundColor = newBgColor;
Console.WriteLine ( "Object Serialization" );
Console.BackgroundColor = prevBgColor;
newBgColor++;

SaveAsXmlFormat ( spycar, "CarData.xml" );
Console.WriteLine ( "=> Saved car in XML format" );

SaveAsXmlFormat ( person, "PersonData.xml" );
Console.WriteLine ( "=> Saved person in XML format" );

SaveAsXmlFormat ( myCars, "CarCollection.xml" );
Console.WriteLine ( "=> Saved List of Cars  in XML format" );

SpyCar savedCar = ReadAsXmlFormat<SpyCar> ( "CarData.xml" );

Console.BackgroundColor = newBgColor;
Console.WriteLine ( $"Original Car:\t {spycar.ToString ()}" );
//Console.BackgroundColor = prevBgColor;
newBgColor++;

Console.BackgroundColor = newBgColor;
Console.WriteLine ( $"Xml Read Car:\t {savedCar.ToString ()}" );
Console.BackgroundColor = prevBgColor;
newBgColor++;

List<SpyCar> myCars2 = ReadAsXmlFormat<List<SpyCar>> ( "CarCollection.xml" );

List<SpyCar> savedCars = ReadAsXmlFormat<List<SpyCar>> ( "CarCollection.xml" );
//  ?so I haven't done anything with either Read List. ?


//  Hello JSON
SaveAsJsonFormat ( spycarJ, "CarDataJ.json" );

Console.BackgroundColor = newBgColor;
Console.WriteLine ( $"=> Saved carJ in JSON format" );
//Console.BackgroundColor = prevBgColor;
newBgColor++;

//  Update Classes with [JSON include]
// or use JsonSerializerOptions to: Include all fields, and use [JsonExclude] tags
SaveAsJsonFormat ( personJ, "PersonDataJ.json" );

Console.BackgroundColor = newBgColor;
Console.WriteLine ( $"=> Saved personJ in JSON format" );
//Console.BackgroundColor = prevBgColor;
newBgColor++;



SaveAsJsonFormatAttr ( spycarJ, "CarDataJOpt.json" );

Console.BackgroundColor = newBgColor;
Console.WriteLine ( $"=> Saved personJOpt in JSON format" );
Console.BackgroundColor = prevBgColor;
newBgColor++;


Console.BackgroundColor = newBgColor;

SaveAsJsonFormatO ( options, spycarJ, "CarDataJ.json" );
Console.WriteLine ( $"=> Saved car in JSON format" );
Console.BackgroundColor = prevBgColor;
newBgColor++;
SaveAsJsonFormatO ( options, personJ, "PersonDataJ.json" );
Console.WriteLine ( $"=> Saved person in JSON format" );

SaveAsJsonFormatO ( options, myCars, "CarCollectionO.json" );

SpyCarJ savedJsonCar = ReadAsJsonFormat<SpyCarJ> ( options, "CarDataJ.json" );


Console.BackgroundColor = newBgColor;
Console.WriteLine ($"Read Car:  {savedJsonCar.ToString ()}");
Console.BackgroundColor = prevBgColor;
newBgColor++;

List<SpyCarJ> savedJsonCars = ReadAsJsonFormat<List<SpyCarJ>> ( options, "CarCollectionJ.json" );
Console.WriteLine($"Read Car:  {savedJsonCars.ToString()}");



#endregion  Top Level Statements



#region  XML Method Group

static T ReadAsXmlFormat<T> ( string filename )
{
    //  Create a Typed Instance of the XmlSerializer
    XmlSerializer xmlFormat = new XmlSerializer ( typeof ( T ) );
    using (Stream fStream = new FileStream ( filename, FileMode.Open ))
    {
        T obj = default;
        obj = (T)xmlFormat.Deserialize ( fStream );
        return obj;
    }
}

static void SaveAsXmlFormat<T> (T objGraph, string fileName)
{
    XmlSerializer xmlFormat = new XmlSerializer (typeof (T));
    using (Stream fStream = new FileStream (fileName, FileMode.Create, FileAccess.Write, FileShare.None))
    {
        xmlFormat.Serialize (fStream, objGraph);
    }
}

#endregion  XML Methods Group
#region  JSON Methods Group

static void HandleNullStrings ()
{
    Console.WriteLine ( "Handling Null Strings" );
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = null,  // Pascal Casing
        IncludeFields = true,
        WriteIndented = true,
        Converters = { new JsonStringNullToEmptyConverter () },
    };

    //  Create a new object with a null string
    var radio = new Radio
    {
        HasSubWoofers = true,
        HasTweeters = true,
        // as a test,
        RadioId = null,
    };

    //  serialize object to JSON
    var json = JsonSerializer.Serialize ( radio, options );
    Console.WriteLine(json);



}


//TODO:  Getting an error; maybe if i add [JSON ATTRIBUTES] to the XML files.
static T ReadAsJsonFormat<T> (JsonSerializerOptions options, string fileName) => System.Text.Json.JsonSerializer.Deserialize <T> ( File.ReadAllText ( fileName ), options );

//  Had to name O,  because the next method WITH 2 not 3, PARAMS, was complaining.
static void  SaveAsJsonFormatO <T> (JsonSerializerOptions options, T objGraph, string fileName)
    => File.WriteAllText ( fileName, System.Text.Json.JsonSerializer.Serialize (objGraph, options));

static void SaveAsJsonFormat<T> ( T objGraph, string fileName )
{
    File.WriteAllText ( fileName, System.Text.Json.JsonSerializer.Serialize ( objGraph ) );
}

//  IsPossible that XML and JSON attributes can comingle.  
static void SaveAsJsonFormatAttr<T> ( T objGraph, string fileName )
{
    //  Use JSO or [JsonInclude]; I've done both without [JsonExclude]
    var options = new JsonSerializerOptions
    {
        IncludeFields = true,
        //  Less minified, more readable
        WriteIndented = true,
        //  .Net PascalCasingEnabled
        PropertyNamingPolicy = null,
    };
    File.WriteAllText ( fileName, System.Text.Json.JsonSerializer.Serialize ( objGraph, options ) );
}


//  IsPossible that XML and JSON attributes can comingle.  
static void SaveAsJsonFormatAttr1<T> ( T objGraph, string fileName )
{
    //  Use JSO or [JsonInclude]; I've done both without [JsonExclude]
    var options = new JsonSerializerOptions
    {
        //  OnDeserialize, make CaseInsens.; avoid pascal or camel question.
        PropertyNameCaseInsensitive = true,
        //  .Net PascalCasingEnabled
        //   PropertyNamingPolicy = null,
        IncludeFields = true,
        //  Less minified, more readable
        WriteIndented = true,
        //  Ignore circular references, [should work with other options?]
        //  Preserve would keep the circle refs.
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        //  Number can be read from number or string (1)
        //  Numbers are written as string. (2) (2 + 1 = 3)
        NumberHandling = JsonNumberHandling.AllowReadingFromString & 
        JsonNumberHandling.WriteAsString,
    };
    File.WriteAllText ( fileName, System.Text.Json.JsonSerializer.Serialize ( objGraph, options ) );
}

//  Streaming Serialization
static async IAsyncEnumerable<int> PrintNumbers (int n)
{
    for ( int i = 0; i < n; i++ )
    {
        yield return i;
    }
}

async static void SerializeAsync ()
{
    Console.WriteLine("Async Serialization");
    using Stream stream = Console.OpenStandardOutput ();
    var data = new { Data = PrintNumbers ( 3 ) };
    await JsonSerializer.SerializeAsync ( stream, data );
    Console.WriteLine ();
}


//  New API for Streaming Deserialization
async static void DeserializeAsync ()
{
    Console.WriteLine("Async Deserialization");
    var stream = new MemoryStream (System.Text.Encoding.UTF8.GetBytes("[0,1,2,3,4]"));

    await foreach (int item in JsonSerializer.DeserializeAsyncEnumerable<int> (stream))
    {
        Console.Write(item);
    }
    Console.WriteLine();
}



#endregion  JSON Methods Group





