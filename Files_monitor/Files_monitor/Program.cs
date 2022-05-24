using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files_monitor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string directoryName;
            Console.WriteLine("Enter the directory to monitor:");
            directoryName = Console.ReadLine();

            Folder folder = new Folder(directoryName);
            Monitor monitor = new Monitor(folder);

            monitor.start();


            Console.ReadKey();
        }
    }
}
