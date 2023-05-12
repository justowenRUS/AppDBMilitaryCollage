using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Sazonov_Misha_Practic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void создатьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTable form2 = new CreateTable();
            this.Hide();
            form2.ShowDialog();
            this.Close();
        }

        private void удалитьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteTable form3 = new DeleteTable();
            this.Hide();
            form3.ShowDialog();
            this.Close();
        }

        private void менюToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void вводЗначенийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputTable form4 = new InputTable();
            this.Hide();
            form4.ShowDialog();
            this.Close();
        }

        private void работаСТаблицейToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void вводДанныхПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userbaza userbd = new userbaza();
            this.Hide();
            userbd.ShowDialog();
            this.Close();
        }
    }
}
