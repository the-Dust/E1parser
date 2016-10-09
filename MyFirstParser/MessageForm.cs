using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyFirstParser;
using System.Threading;

namespace E1Parser
{
    public partial class MessageForm : Form
    {
        public MessageForm()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRewrite_Click(object sender, EventArgs e)
        {
            DbCreator.rewriteVar = 1;
            this.Close();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            DbCreator.rewriteVar = 2;
            this.Hide();
            Program.myForm.buttonFilePath_Click();
            this.Close();
        }
    }
}
