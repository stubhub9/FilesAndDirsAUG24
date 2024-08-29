//  Console Template

using System.Text;

#region  FIELDS
//  vsc and dsc are only required on CROSS PLATFORM APPS
char vsc = Path.VolumeSeparatorChar;
char dsc = Path.DirectorySeparatorChar;
string fileName1 = $@"C{vsc}{dsc}MyCode{dsc}Testing{dsc}Test.dat";
string fileName2 = @"C:\MyCode\Testing\Test.dat";



#endregion
#region  PROGRAM main

Console.WriteLine ("******      Filestreams         *******");

MessageToFileAndBack ( fileName2 );


#endregion

#region  METHODS


static void MessageToFileAndBack ( string fileName2 )
{
    Console.WriteLine(" Create .dat file, write 'Hello' to file, read file, delete file.");
    using (FileStream fStream = File.Open ( fileName2, FileMode.OpenOrCreate ))
    {
        string msg = "Hello.";
        byte [] msgAsByteArray = Encoding.Default.GetBytes ( msg );

        fStream.Write ( msgAsByteArray, 0, msgAsByteArray.Length );

        //  Reset internal position of stream.
        fStream.Position = 0;

        Console.Write ( "Message as byte array:" );
        byte [] bytesFromFile = new byte [msgAsByteArray.Length];
        for (int i = 0; i < msgAsByteArray.Length; i++)
        {
            bytesFromFile [i] = (byte)fStream.ReadByte ();
            Console.Write ( bytesFromFile [i] );
        }

        Console.Write ( "\nDecoded Message:  " );
        Console.Write ( Encoding.Default.GetString ( bytesFromFile ) );
        Console.ReadLine ();
        //  END of using(FileStream)
    }
    File.Delete ( fileName2 );
}



#endregion