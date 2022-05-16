using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            String file;
            Settings settings = new Settings();

            Regex reg = new Regex("(" + UriScheme + ":[/]{2,3}(file|key):[/]{0,3}).+", RegexOptions.IgnoreCase);
            Match m = reg.Match(decodedURL);
            if (m != null && m.Success && m.Groups[1] != null && m.Groups[2] != null & m.Groups[1].Value != null & m.Groups[2].Value != null)
            {     
                String stringToSplit = m.Groups[1].Value;
                String entryType = m.Groups[2].Value;
                file = decodedURL.Split(new[] { stringToSplit }, StringSplitOptions.None)[1];
                if (file == null || file.Length == 0)
                    throw new Exception("File could not be processed correctly");
                if (String.Equals(entryType, "key", StringComparison.OrdinalIgnoreCase))
                {
                    file = settings.grabKeyValueFromSettings(file);
                    if (file == null || file.Length == 0)
                        throw new Exception("File could not be found using key provided");
                } 

            } else
            {
                throw new ArgumentException("Unknown URI Format doesnt contain correct scheme and/or file/key naming with triple /// for special encoding");
            }
            
            Console.WriteLine("Resultant file to call  " + file);
            return file;
        }
    }
}
