//  Console- Binary Writer


FileInfo f = new FileInfo ( "BinFile.dat" );
using ( BinaryWriter bw = new BinaryWriter ( f.OpenWrite()))
{
    Console.WriteLine ( $"Base stream is: {bw.BaseStream}" );

    double aDub = 123.4;
    int anInt = 23;
    string aString = "1, 3, C";

    bw.Write ( aDub );
    bw.Write ( anInt );
    bw.Write ( aString );
    
    Console.WriteLine($"Wrote Data to {f.Name}");
}


f = new FileInfo ( "BinFile.dat" );
using ( BinaryReader br = new BinaryReader ( f.OpenRead()))
{
    Console.WriteLine ( br.ReadDouble () );
    Console.WriteLine ( br.ReadInt32 () );
    Console.WriteLine ( br.ReadString () );
}
Console.ReadLine ();