using System;
using System.IO;

namespace lab2
{
    internal class Program
    {
        static void Main()
        {
            using (var sr = new StreamReader("../../../DataBaseSettings.txt"))
            {
                var line = sr.ReadToEnd();
                Console.WriteLine(line);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
