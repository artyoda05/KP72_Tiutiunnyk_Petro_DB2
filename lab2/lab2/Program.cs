using System.IO;
using lab2.Controllers;
using lab2.Database;

namespace lab2
{
    internal class Program
    {
        static void Main()
        {
            using var sr = new StreamReader("../../../DataBaseSettings.txt");
            var line = sr.ReadToEnd();
            var connection = new DbConnection(line);
            var controller = new Controller(connection);
            controller.Begin();
        }
    }
}
