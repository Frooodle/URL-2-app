using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace URL_App_Launcher_Console
{
    internal class ProcessURI
    {
        public String processURIToFile(String uriArg)
        {
            if(uriArg == null)
                throw new ArgumentNullException("uri");
            if(uriArg.Length == 0)
                throw new ArgumentException("uri");
            String decodedURL = HttpUtility.UrlDecode(uriArg);
            Console.WriteLine("Decoded URL:" + decodedURL);
            String url;
            if (decodedURL.Contains("ual:///file:///"))
            {
                
                url = decodedURL.Split(new[] { "ual:///file:///" }, StringSplitOptions.None)[1];
            } else if (decodedURL.Contains("ual:///key:///"))
            {
                String key = decodedURL.Split(new[] { "ual:///key:///" }, StringSplitOptions.None)[1];
                Settings settings = new Settings();
                url = settings.grabKeyValueFromSettings(key);
            } else
            {
                throw new ArgumentException("Unknown URI Format");
            }
            Console.WriteLine("Resultant url " + url);
            return url;
        }
    }
}
