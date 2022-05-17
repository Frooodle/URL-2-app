using System.Diagnostics;
using URL2App;
using System.Runtime.InteropServices;

[DllImport("kernel32.dll")]
static extern IntPtr GetConsoleWindow();

[DllImport("user32.dll")]
static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

[DllImport("user32.dll")]
static extern int MessageBox(IntPtr hInstance, string lpText, string lpCaption, int type);


const int SW_HIDE = 0;
const int SW_SHOW = 5;



const string UriScheme = "U2A";
const string FriendlyName = "UrlToApp";


bool debugMode = args.Length == 0;
try
{
    Process process = Process.GetCurrentProcess();
    var dupl = (Process.GetProcessesByName(process.ProcessName));
    if (dupl.Length > 1)
    {
        foreach (var p in dupl)
        {
            if (p.Id != process.Id)
                p.Kill();
        }
    }

    Settings settings = new Settings();
    var handle = GetConsoleWindow();
    debugMode = settings.isDebug() || args.Length == 0;
    if (debugMode) {
        ShowWindow(handle, SW_SHOW);
        
    } else
    {
        ShowWindow(handle, SW_HIDE);
    }

    for (int i = 0; i < args.Length; i++)
    {
        Console.WriteLine($"Arg[{i}] = [{args[i]}]");
    }

    Console.WriteLine("Starting " + FriendlyName);

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
                if (pathToFileToOpen[0] == '"')
                {
                    String[] splitResults = pathToFileToOpen.Split(new[] { '"' }, 3);
                    pathToFileToOpen = splitResults[1];
                    arguements = splitResults[2].Trim();
                }
                if(!File.Exists(pathToFileToOpen))
                {
                    throw new ArgumentException("File " + pathToFileToOpen + " does not exist if you are passing arguements the exe path must be in double quotes");
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

            if(settings.isConfirmationBeforeExecuting())
            {
                String text = "Do you wish to run " + pathToFileToOpen;
                if (arguements != null && !arguements.Equals(""))
                    text += "\nUsing arguements: \n"  + arguements;
                int response = MessageBox((IntPtr)0, text, "Approve execution?", 4);
                if(response == 6) //6 = yes 7 = no
                    Process.Start(startInfo);
            } else { 
                Process.Start(startInfo);
            }

        } else
        {
            throw new ArgumentException("Invalid uri, Could not be parsed " + uri);
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

  