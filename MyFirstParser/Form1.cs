using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;

namespace MyFirstParser
{
    public partial class Form1 : Form
    {
        private KinopoiskSearcher ks;
    
        public Form1()
        {
            InitializeComponent();
            ks = new KinopoiskSearcher();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ks.DownloadHtml())
            {
                ks.FindVacancyList();
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Action action = () =>
                {
                    ks.FindVacancy(TableHelper.TakeId());
                    DbCreator vacancyDb = new DbCreator();
                    vacancyDb.GetDb(ks.vacancyArray);
                };
            var task = Task.Factory.StartNew(action);
        }

        public void buttonFilePath_Click()
        {
            Action action = () =>
                {
                    saveFileDialog.Filter = "MS Access 2002 Files|*.mdb|All Files|*.*";
                    saveFileDialog.Title = "Сохранить базу данных";
                    saveFileDialog.ShowDialog();
                    if (saveFileDialog.FileName != "")
                    { FileInfo fi = new FileInfo(saveFileDialog.FileName); }
                    string s = saveFileDialog.FileName;
                    if (s.Length > 40)
                        labelFilePath.Text = s.Substring(0, s.IndexOf("\\") + 1) + "..." + s.Substring(s.LastIndexOf("\\"), s.Length - s.LastIndexOf("\\"));
                    else labelFilePath.Text = saveFileDialog.FileName;
                    DbCreator.filePath = s;
                };
            if (InvokeRequired)
                Invoke(action);
            else action();

        }

        public void buttonFilePath_Click(object sender, EventArgs e)
        {
            buttonFilePath_Click();
        }

    }
}
