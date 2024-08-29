//  FileWatcher Console Project

using System.IO;

//  FileSystemEventHandler signature
//void MyNotificationHandler ( object source, FileSystemEventArgs e );

//  RenamedEventHandler
//void MyRenamedHandler ( object source, FileSystemEventArgs e );

Console.WriteLine("FileWatcher App");

//  Set path to observed dir.
FileSystemWatcher watcher = new FileSystemWatcher();
try
{
	watcher.Path = @".";
}
catch (ArgumentException ex )
{
    Console.WriteLine ( ex.Message );
    return;
}

//  Set watcher Notify filters.
watcher.NotifyFilter = NotifyFilters.LastAccess
    | NotifyFilters.LastWrite
    | NotifyFilters.FileName
    | NotifyFilters.DirectoryName;

//  Only watch .txt files.
watcher.Filter = "*.txt";


//  Add Event Handlers.
watcher.Changed += ( s, e ) =>
Console.WriteLine ( $"File:  {e.FullPath} {e.ChangeType}" );
watcher.Created += ( s, e ) =>
Console.WriteLine ( $"File:  {e.FullPath} {e.ChangeType}" );
watcher.Deleted += ( s, e ) =>
Console.WriteLine ( $"File:  {e.FullPath} {e.ChangeType}" );
watcher.Renamed += ( s, e ) =>
Console.WriteLine ( $"File:  {e.OldFullPath} {e.FullPath}" );

//  Begin watching
watcher.EnableRaisingEvents = true;

Console.WriteLine (@"Press 'q' to quit.");

//  Raise some events.
using (var sw = File.CreateText ("TestW.txt"))
{
    sw.Write ( "New text." );
}
File.Move ( "TestW.txt", "Test2.txt" );
File.Delete ( "Test2.txt" );

//  Waiting for a q.
while (Console.Read () != 'q') ;








