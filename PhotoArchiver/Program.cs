using System;
using PhotoArchiver.Logic;

namespace PhotoArchiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string destination = @"C:\Users\Larry\Pictures\ArchivedPictures";
            string source = @"C:\Users\Larry\Pictures\Camera";

            ArchiveProcess processor = new ArchiveProcess();
            processor.ArchivePhotosBasedOnDays(source,destination);
            Console.Write("Done");
            Console.ReadLine();
        }
    }
}
