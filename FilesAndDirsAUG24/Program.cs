
//  Bind to the current working directory.
//using System.Text;

//  FIELDS

ConsoleColor prevColorBackground = Console.BackgroundColor;
//ConsoleColor newColor = (ConsoleColor) 1;
int nextColor = 1;



char vsc = Path.VolumeSeparatorChar;
char dsc = Path.DirectorySeparatorChar;


//  "." makes reference to THIS directory.
DirectoryInfo directoryInfo1 = new DirectoryInfo ( "." );

//  Bind to C:\Windows, using a verbatim string.
DirectoryInfo directoryInfo2 = new DirectoryInfo ( @"C:\Users" );

//  Bind to a directory, before creating it.
DirectoryInfo directoryInfo3 = new DirectoryInfo ( @"C:\MyCode\Testing" );
directoryInfo3.Create ();

//  .NET paths for different PLATFORMS.
DirectoryInfo directoryInfo4 = new DirectoryInfo ( $@"C{Path.VolumeSeparatorChar}{Path.DirectorySeparatorChar}MyCode{Path.DirectorySeparatorChar}Testing" );

DirectoryInfo directoryInfo5 = new
    DirectoryInfo ( $@"C{vsc}{dsc}Images1" );

//  PROGRAM     ******       PROGRAM     ******       PROGRAM     ******       PROGRAM     ******       PROGRAM     ******       PROGRAM     ******     
Console.ForegroundColor = (ConsoleColor)nextColor;
nextColor++;

DiscoverConsoleColor ();

/*  Directory '.' for THIS directory
 *  /../../  to get above \bin\Debug
Parent @ . :  C:\Users\wonder\source\repos\FilesAndDirsAUG24\FilesAndDirsAUG24\bin\Debug
 */
Console.ForegroundColor = (ConsoleColor)nextColor;
nextColor++;
DumpDirectoryInfo ( directoryInfo1 );
Console.ForegroundColor = (ConsoleColor)nextColor;
nextColor++;
DumpDirectoryInfo ( directoryInfo2 );
Console.ForegroundColor = (ConsoleColor)nextColor;
nextColor++;
DumpDirectoryInfo ( directoryInfo5 );
Console.ForegroundColor = (ConsoleColor)nextColor;
nextColor++;

DumpFileInfo ( directoryInfo5 );

Console.ForegroundColor = (ConsoleColor)nextColor;
nextColor++;




//   METHODS or Functions    *******          METHODS     *******          METHODS     *******          METHODS     *******          METHODS     *******          METHODS     *******          METHODS     *******       

static void DumpDirectoryInfo ( DirectoryInfo dirInfo )
{

    DirectoryInfo []? subDirInfo = dirInfo.GetDirectories ();
    PrintDirectoryInfo ( dirInfo );
    foreach (DirectoryInfo dir in subDirInfo )
    {
        PrintDirectoryInfo ( dir );
    }
}

static void PrintDirectoryInfo (DirectoryInfo dirInfo)
{ 
    Console.WriteLine("*****           Dir Info             *****");
    Console.WriteLine ( $"Exists          :  {dirInfo.Exists}" );
    Console.WriteLine ( $"Full Name       :  {dirInfo.FullName}" );
    Console.WriteLine ( $"Name            :  {dirInfo.Name}" );
    Console.WriteLine ( $"Parent          :  {dirInfo.Parent}" );
    Console.WriteLine ( $"CreationTime    :  {dirInfo.CreationTime}" );
    Console.WriteLine ( $"Attributes      :  {dirInfo.Attributes}" );
    Console.WriteLine ( $"Root            :  {dirInfo.Root}" );
    Console.WriteLine ( $"LastAccessTime  :  {dirInfo.LastAccessTime}" );
    Console.WriteLine ( $"LastWriteTime   :  {dirInfo.LastWriteTime}" );
    //Console.WriteLine ( $"LastWriteTime  :  {dirInfo.}" );
    Console.WriteLine("***************************************\n");

}


//  Check dirs in C{vsc}{dsc}Windows{dsc}Web{dsc}Wallpaper
static void DumpFileInfo ( DirectoryInfo dirInfo )
{

    //DirectoryInfo dirInfo = new
    //    DirectoryInfo ( $@"C{vsc}{dsc}Windows{dsc}Web{dsc}Wallpaper" );


    //FileInfo [] imageFiles = dirInfo.GetFiles ( "*.jpg, SearchOption.AllDirectories" );
    FileInfo [] filesInfo = dirInfo.GetFiles ( "*.jpg" );


    Console.WriteLine($"Found {filesInfo.Length} *.jpg files\n ");

    foreach (FileInfo file in filesInfo)
    {
        Console.WriteLine ( "*************************" );
        Console.WriteLine ($"Exists              :  {file.Exists}");
        Console.WriteLine ( $"Full File name  :  {file.FullName}" );
        Console.WriteLine ( $"File name        :  {file.Name}" );
        Console.WriteLine ( $"File size          :  {file.Length}" );
        Console.WriteLine ( $"Creation Time  :  {file.CreationTime}" );
        Console.WriteLine ( $"Attributes        :  {file.Attributes}" );
        Console.WriteLine ( $"Directory Name :  {file.DirectoryName}" );
        Console.WriteLine ( $"Extension         :  {file.Extension}" );
        Console.WriteLine ( $"IsReadOnly       :  {file.IsReadOnly}" );
        Console.WriteLine ( $"LastAccessTime :  {file.LastAccessTime}" );
        Console.WriteLine ( $"LastWriteTime   :  {file.LastWriteTime}" );
        Console.WriteLine ( "*************************\n" );
    }

}



static void DiscoverConsoleColor ()
{
//    ConsoleColor color1 = ConsoleColor.Red;
    System.Enum enum1 = ConsoleColor.Green;

    Console.WriteLine ( ConsoleColor.Red.ToString ());
    Console.WriteLine ( $"Info about  {enum1.GetType ().Name}" );
    Console.WriteLine ( $"Underlying storage type  {Enum.GetUnderlyingType ( enum1.GetType ())}" );
    Array enumData = Enum.GetValues ( enum1.GetType () );
    Console.WriteLine("This enum has {enumData.Length} members.");

    for (int i = 0; i < enumData.Length; i++)
    {
        Console.WriteLine("Name:  {0}, Value:  {0:D}", enumData.GetValue (i));
    }


}

//static int NextColorVal ()
//{
//    int colorVal = ((int)newColor) + 1;
//}