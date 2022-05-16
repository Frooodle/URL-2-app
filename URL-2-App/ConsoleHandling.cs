using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URL2App
{
    internal class ConsoleHandling
    {
        public static void printInfo()
        {
            Console.WriteLine("This app requires a JSON file appsettings.json to be along side it if you wish for debug or file key functionality");
            Console.WriteLine("Usage: Use link in format UAL:/// to call this app, we accept file paths with and without args (.exe path and file must be in quotes if args are used)");
            Console.WriteLine("Triple /// must be used at all times, example reuqest is ual://file:///C:\\Windows\\System32\\notepad.exe");
            Console.WriteLine("ual://file:///PATH for direct file calls or if the file is setup in appsettings.json then ual://key:///KEY");
        }
        public static void waitAndClose(bool debugMode)
        {
            if(debugMode)
                wait();
            Environment.Exit(0);
        }

        public static void waitAndClose()
        {
            wait();
            Environment.Exit(0);
        }

        public static void wait()
        {
            Console.WriteLine("Please any key to close.");
            Console.ReadKey();
        }

        public static bool consoleInputYN()
        {
            string option = Console.ReadLine();
            return option != null && (String.Equals(option, "yes", StringComparison.OrdinalIgnoreCase) || String.Equals(option, "y", StringComparison.OrdinalIgnoreCase));
        }
    }
}
