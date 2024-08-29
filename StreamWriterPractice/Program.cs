// StreamWriterPractice Console Project



#region  FIELDS
string filename1 = "Reminders.txt";
#endregion
#region  Project MAIN

UseStreamWriter ( filename1 );
UseStreamReader ( filename1 );
File.Delete ( filename1 );

UseStreamWriterOnly ( filename1 );
UseStreamReaderOnly ( filename1 );

#endregion
#region  METHODS

static void UseStreamWriter ( string fileName )
{
    Console.WriteLine ( "UseStreamWriter" );

    //   using (StreamWriter writer = File.CreateText ( "Reminders.txt" ))
    //ERROR:    using (StreamWriter writer = File.CreateText ( filename1 ))
    //        string filename2 = "Reminders.txt";
    //    using (StreamWriter writer = File.CreateText ( filename2 ))

    using (StreamWriter writer = File.CreateText ( fileName ))
    {
        writer.Write ( "Plan for Father's Day ..." );
        writer.Write ( "Plan for birthdays ..." );
        writer.Write ( "Important numbers:" );
        for (int i = 0; i < 10; i++)
        {
            writer.Write ( i + " " );
        }

        writer.Write ( writer.NewLine );
    }
    Console.WriteLine ( "Created file, \n Wrote file,\nDone Writing." );
    //   Console.Write ( "Created file, \n Wrote file, \n  Deleted file,\nDone" );
    //END of UseStreamWriter
}

static void UseStreamReader ( string fileName )
{
    Console.WriteLine ( "UseStreamReader" );
    using (StreamReader reader = File.OpenText ( fileName ))
    {
        string? input = null;
        while ((input = reader.ReadLine ()) != null)
        {
            Console.WriteLine ( input );
        }
    }
    //END of UseStreamReader
}

//  Don't use File()
static void UseStreamWriterOnly ( string fileName )
{
    Console.WriteLine ( "UseStreamWriterOnly, No File();" );
    //  Create or Overwrite text file
    using (StreamWriter writer = new StreamWriter ( fileName ))
    {
        writer.Write ( "Plan for Father's Day ..." );
        writer.Write ( "Plan for birthdays ..." );
        writer.Write ( "Important numbers:" );
        for (int i = 0; i < 10; i++)
        {
            writer.Write ( i + " " );
        }

        writer.Write ( writer.NewLine );
    }
    Console.WriteLine ( "Created file, \n Wrote file,\nDone Writing." );
    //   Console.Write ( "Created file, \n Wrote file, \n  Deleted file,\nDone" );
    //END of UseStreamWriter
}

static void UseStreamReaderOnly ( string fileName )
{
    Console.WriteLine ( "UseStreamReaderOnly" );
    using (StreamReader reader = new StreamReader ( fileName ))
    {
        string? input = null;
        while ((input = reader.ReadLine ()) != null)
        {
            Console.WriteLine ( input );
        }
    }
    //END of UseStreamReader
}

#endregion