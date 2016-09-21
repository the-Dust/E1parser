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
    public partial class Form1 : Form
    {
        private KinopoiskSearcher ks;
    
        public Form1()
        {
            InitializeComponent();
            ks = new KinopoiskSearcher();
            
        }
        public ComboBox getComboBox()
        {
            return comboBox1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ks.DownloadHtml(textBox1.Text))
                if (ks.FindCover())
                { textBox2.Text += ks.vacancy; }
            
           // else textBox2.Text = ks.vacansy;
          
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string s = Convert.ToString(Program.myForm.comboBox1.SelectedItem);
            Program.myForm.textBox1.Text = TableHelper.TakeId();
            ks.FindVacancy(TableHelper.TakeId());
            DbCreator vacancyDb = new DbCreator();
            vacancyDb.GetDb(ks.vacancyList);
        }

    }
}
