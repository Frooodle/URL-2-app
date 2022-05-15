using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace URL2App
{
    internal class RegisterURLHandler
    {
        public static void RegisterUriScheme(String schemeName, String friendlySchemeName)
        {
            try {
                using (var key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\" + schemeName))
                {
                    string applicationLocation = Process.GetCurrentProcess().MainModule.FileName;
                    key.SetValue("", "URL:" + friendlySchemeName);
                    key.SetValue("URL Protocol", "");

                    using (var defaultIcon = key.CreateSubKey("DefaultIcon"))
                    {
                        defaultIcon.SetValue("", applicationLocation + ",1");
                    }

                    using (var commandKey = key.CreateSubKey(@"shell\open\command"))
                    {
                        commandKey.SetValue("", "\"" + applicationLocation + "\" \"%1\"");
                    }
                }
            }catch(Exception e)
            {

            }
        }
    }
}
