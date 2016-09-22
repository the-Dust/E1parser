using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyFirstParser
{
    public delegate DataRowView MyDel(object view);
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
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ks.FindVacancy(TableHelper.TakeId());
            DbCreator vacancyDb = new DbCreator();
            vacancyDb.GetDb(ks.vacancyArray);
        }
    }
}
