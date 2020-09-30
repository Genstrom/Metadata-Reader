using System;
using System.IO;
namespace MetadataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input filepath to be read: ");
            //string input = Console.ReadLine();
            
            StreamReader streamreader = new StreamReader(@"C:\Users\Tommy\Desktop\banana.png");
            Console.WriteLine(streamreader.ReadToEnd()); 
        }
    }
}
