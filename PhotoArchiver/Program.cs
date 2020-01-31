using System;
using PhotoArchiver.Logic;

namespace PhotoArchiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string destination = @"D:\My Pictures\";
            string source = @"C:\DCIM\Camera\";

            ArchiveProcess processor = new ArchiveProcess();
            //processor.ArchivePhotosBasedOnDays(source,destination,false);
            Console.Write("Done");
            Console.ReadLine();
        }
    }
}
