using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace URL2App
{
    internal class ProcessURI
    {
        public String processURIToFile(String UriScheme, String uriArg)
        {
            if(uriArg == null)
                throw new ArgumentNullException("uri");
            if(uriArg.Length == 0)
                throw new ArgumentException("uri");
            if (UriScheme == null)
                throw new ArgumentNullException("UriScheme");
            if (UriScheme.Length == 0)
                throw new ArgumentException("UriScheme");
            UriScheme = UriScheme.ToLower();
            String decodedURL = HttpUtility.UrlDecode(uriArg);
            Console.WriteLine("Decoded URL is " + decodedURL);
            String url;
            if (decodedURL.Contains(UriScheme + ":///file:///"))
            {
                
                url = decodedURL.Split(new[] { UriScheme + ":///file:///" }, StringSplitOptions.None)[1];
            } else if (decodedURL.Contains(UriScheme + ":///key:///"))
            {
                String key = decodedURL.Split(new[] { UriScheme + ":///key:///" }, StringSplitOptions.None)[1];
                Settings settings = new Settings();
                url = settings.grabKeyValueFromSettings(key);
            } else
            {
                throw new ArgumentException("Unknown URI Format doesnt contain correct scheme and/or file/key naming with triple ////");
            }
            Console.WriteLine("Resultant url " + url);
            return url;
        }
    }
}
