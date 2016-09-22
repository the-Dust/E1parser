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
            //Эти три строки позволяют прикинуться этой программе браузером
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; ru; rv:1.9.2) Gecko/20100115 Firefox/3.6";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.KeepAlive = true;
            //Эта строка позволяет установить доверительное соединение с сервером
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
                
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            StreamReader sr = new StreamReader(response.GetResponseStream(), encoding);
            string html = sr.ReadToEnd();
            return html;
        }
    }
}
