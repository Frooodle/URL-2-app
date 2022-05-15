using System.Diagnostics;
using URL2App;
using System.Runtime.InteropServices;

[DllImport("kernel32.dll")]
static extern IntPtr GetConsoleWindow();

[DllImport("user32.dll")]
static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

const int SW_HIDE = 0;
const int SW_SHOW = 5;



const string UriScheme = "U2A";
const string FriendlyName = "UrlToApp";


bool debugMode = args.Length == 0;
try
{
    for (int i = 0; i < args.Length; i++)
    {
        Console.WriteLine($"Arg[{i}] = [{args[i]}]");
    }

    Console.WriteLine("Starting " + FriendlyName);
    Settings settings = new Settings();
    var handle = GetConsoleWindow();
    debugMode = settings.isDebug() || args.Length == 0;
    if (debugMode) {
        ShowWindow(handle, SW_SHOW);
        
    } else
    {
        ShowWindow(handle, SW_HIDE);
    }

    //If not started mannually
    if (args.Length > 0)
    {
        if (Uri.TryCreate(args[0], UriKind.Absolute, out var uri) &&
            string.Equals(uri.Scheme, UriScheme, StringComparison.OrdinalIgnoreCase))
        {
            ProcessURI processURI = new ProcessURI();
            String pathToFileToOpen = processURI.processURIToFile(UriScheme,args[0]);
            String arguements = "";
            if (pathToFileToOpen == null || pathToFileToOpen.Length == 0)
            {
                throw new ArgumentException("Path passed is invalid");
            }
            else
            {
                if (pathToFileToOpen.Contains('"'))
                {
                    String[] splitResults = pathToFileToOpen.Split('"');
                    pathToFileToOpen = splitResults[1];
                    arguements = splitResults[2].Trim();
                }
                if(!File.Exists(pathToFileToOpen))
                {
                    throw new ArgumentException("Path " + pathToFileToOpen + " does not exist if you are passing arguements the exe path must be in double quotes");
                }
            }

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = pathToFileToOpen;
            if (arguements != null && !arguements.Equals(""))
            {
                startInfo.Arguments = arguements;
            }
            Console.WriteLine("Starting app \"" + pathToFileToOpen + "\"");
            Console.WriteLine("With arguements \"" + arguements + "\"");
            Process.Start(startInfo);


        }
    }
    else
    {
        //mannually openned so setup app configs and registry entries
        ConsoleHandling.printInfo();

        Console.WriteLine("\n\nApp mannually started");
        Console.WriteLine("Is the app placed in the location you wish to perminatly run it from? [yes/no]");
        bool accepted = ConsoleHandling.consoleInputYN();
        if (accepted)
        {
            RegisterURLHandler.RegisterUriScheme(UriScheme, FriendlyName);
            Console.WriteLine("Registry setup, app now ready to be called from URL");
        }
        else {
            Console.WriteLine("Please move exe and optional settings file to desired location before running");
        }
        
    }
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
    ConsoleHandling.waitAndClose();
}

ConsoleHandling.waitAndClose(debugMode);
  