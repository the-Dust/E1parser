using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;

namespace MyFirstParser
{
    public class HtmlDownloadHelper
    {
        public static string DownloadHtml(string uri)
        {
            return DownloadHtml(uri, Encoding.UTF8);
        }

        public static string DownloadHtml(string uri, Encoding encoding)
        {
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; ru; rv:1.9.2) Gecko/20100115 Firefox/3.6";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.KeepAlive = true;

            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            
    
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            StreamReader sr = new StreamReader(response.GetResponseStream(), encoding);
            //sr.ReadLine(); //пропускаем первую строку полученного хтмл, в которой всякий мусор
            string fhtml = sr.ReadToEnd();
            string html = fhtml;
            return html;
        }

        public static Image DownloadImage(string uri)
        {
            WebRequest request = WebRequest.Create(uri);
            WebResponse response = request.GetResponse();

            return Image.FromStream(response.GetResponseStream());
        }
    }
}
