using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
        private readonly string baseUri;
        public static DataTable ksTable = new DataTable();
        public string[] vacancyArray;

        public KinopoiskSearcher()
        {
            baseUri = @"https://ekb.zarplata.ru/";
        }

        public bool DownloadHtml()
        {
            try
            {
                myHtml = HtmlDownloadHelper.DownloadHtml(baseUri, Encoding.GetEncoding(65001));
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public void FindVacancy(string id)
        {
            string vacancyUri = string.Format(@"https://ekb.zarplata.ru/vacancy?rubric_id[]={0}&limit=50", id);
            string vacancyHtml = HtmlDownloadHelper.DownloadHtml(vacancyUri, Encoding.GetEncoding(65001));
            TextSearcher ts = new TextSearcher(vacancyHtml);
            vacancyHtml = ts.Crop("<a href=\"/vacancy/", "<a href=\"/vacancy/extended");
            int dimension = new Regex("<a href=\"/vacancy/").Matches(vacancyHtml).Count;
            vacancyArray = new string[dimension];
            Array.Clear(vacancyArray, 0, dimension);
            Program.myForm.label1.Text = "Построение списка всех вакансий...";
            for (int i = 0; i < vacancyArray.Length; i++)
            {
                ts.Skip("<a href=\"/vacancy/");
                vacancyArray[i] = "https://ekb.zarplata.ru/vacancy/" + ts.ReadTo("\"");
            }
            Program.myForm.label1.Text = "Список построен...";
        }

        public void FindVacancyList()
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
            }
            Program.myForm.label2.Text = "Готово";
            Program.myForm.comboBox1.DataSource = th.table;
            Program.myForm.comboBox1.DisplayMember = "Item";
        }
    }
}
