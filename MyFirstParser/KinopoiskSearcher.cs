using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Data;
using System.Data.OleDb;

namespace MyFirstParser
{
    public class MyFirstParserException : Exception
    {
        public MyFirstParserException(string message)
            : base(message)
        {
        }
    }

    public class KinopoiskSearcher
    {
        private string myHtml;
        private Image myCover;
        private readonly string baseUri;
        public string vacancy;
        public static DataTable ksTable = new DataTable();
        public List<string> vacancyList = new List<string>();

        public KinopoiskSearcher()
        {
            baseUri = @"https://ekb.zarplata.ru/";
        }

        public bool DownloadHtml(string movieName)
        {
            myCover = null;
            
            try
            {
                myHtml = HtmlDownloadHelper.DownloadHtml(string.Format(baseUri, movieName), Encoding.GetEncoding(65001));
                return true;
            }
            catch(Exception e)
            {
                vacancy = e.Message;
                return false;
            }
            
        }

        public void FindVacancy(string id)
        {
            string vacancyUri = string.Format(@"https://ekb.zarplata.ru/vacancy?rubric_id[]={0}&limit=50", id);
            string vacancyHtml = HtmlDownloadHelper.DownloadHtml(vacancyUri, Encoding.GetEncoding(65001));
            TextSearcher ts = new TextSearcher(vacancyHtml);
            ts.Crop("<a href=\"/vacancy/", "<a href=\"/vacancy/extended");
            vacancyList.Clear();
            while (ts.Skip("<a href=\"/vacancy/"))
            {
                string vacancyAddress = "https://ekb.zarplata.ru/vacancy/" + ts.ReadTo("\"");
                vacancyList.Add(vacancyAddress);
            }
        }

        public bool FindCover()
        {
            if (string.IsNullOrEmpty(myHtml))
                throw new MyFirstParserException("Код html не был загружен. Сначала выполните download html");

            TextSearcher ts = new TextSearcher(myHtml);
            TableHelper th = new TableHelper();
            while (ts.Skip("vacancy?rubric_id[]="))
            {
                string thisId = ts.ReadTo("\"");
                ts.Skip(">");
                string thisItem = ts.ReadTo("<");
                th.AddRow(thisId, thisItem);
                vacancy += thisItem + "\r\n";
                
               // return true;
            }
            ksTable = th.table;
            Program.myForm.comboBox1.DataSource = th.table;
            Program.myForm.comboBox1.DisplayMember = "Item";
           
            return true;
            /*
            try
            {
                myCover = HtmlDownloadHelper.DownloadImage(imageFileUri);
                return true;
            }
            catch
            {
                return false;
            }
            */
        }

        public Image Cover
        {
            get
            { 
                if(myCover == null)
                throw new MyFirstParserException("Изображение не загружено");
                return myCover; 
            }
        }

    }
}
