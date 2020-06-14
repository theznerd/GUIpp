using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI__Editor.Controllers
{
    public static class ImageController
    {
        public static string ConvertURI(string uri, string baseXML)
        {
            if(null != uri)
            {
                if (uri.StartsWith("http://") ||
                   uri.StartsWith("https://") ||
                   File.Exists(uri))
                {
                    return uri;
                }
                else
                {
                    return baseXML + "\\" + uri;
                }
            }
            else
            {
                return "";
            }
        }
    }
}
