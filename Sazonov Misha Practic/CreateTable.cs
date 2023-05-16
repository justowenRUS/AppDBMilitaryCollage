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
    public partial class CreateTable : Form
    {
        public CreateTable()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tableName = textBox1.Text;
            string column1 = textBox2.Text;
            string column2 = textBox3.Text;
            string column3 = textBox4.Text;
            string column4 = textBox5.Text;

            string dataType1 = textBox6.Text;
            string dataType2 = textBox7.Text;
            string dataType3 = textBox8.Text;
            string dataType4 = textBox9.Text;

            // Проверяем, что все колонки и типы данных заполнены
            if (string.IsNullOrEmpty(column1) || string.IsNullOrEmpty(column2) ||
                string.IsNullOrEmpty(column3) || string.IsNullOrEmpty(column4) ||
                string.IsNullOrEmpty(dataType1) || string.IsNullOrEmpty(dataType2) ||
                string.IsNullOrEmpty(dataType3) || string.IsNullOrEmpty(dataType4))
            {
                MessageBox.Show("Пожалуйста, заполните все колонки и типы данных.");
                return;
            }

            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=military.mdb";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = $"CREATE TABLE {tableName} ({column1} {dataType1}, {column2} {dataType2}, {column3} {dataType3}, {column4} {dataType4})";
                OleDbCommand command = new OleDbCommand(createTableQuery, connection);
                command.ExecuteNonQuery();

                connection.Close();
                Form1 form1 = new Form1();
                this.Hide();
                form1.ShowDialog();
                this.Close();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            label3.Visible = true; // Здесь label1 - это ваш Label, который должен появиться
            textBox2.Visible = true; // Здесь textBox1 - это ваш TextBox, который должен появиться
            button3.Visible = true;
            label7.Visible = true; // Здесь label1 - это ваш Label, который должен появиться
            textBox6.Visible = true; // Здесь textBox1 - это ваш TextBox, который должен появиться
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label5.Visible = true; // Здесь label1 - это ваш Label, который должен появиться
            textBox4.Visible = true; // Здесь textBox1 - это ваш TextBox, который должен появиться
            button5.Visible = true;
            label9.Visible = true; // Здесь label1 - это ваш Label, который должен появиться
            textBox8.Visible = true; // Здесь textBox1 - это ваш TextBox, который должен появиться
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label4.Visible = true; // Здесь label1 - это ваш Label, который должен появиться
            textBox3.Visible = true; // Здесь textBox1 - это ваш TextBox, который должен появиться
            button4.Visible = true;
            label8.Visible = true; // Здесь label1 - это ваш Label, который должен появиться
            textBox7.Visible = true; // Здесь textBox1 - это ваш TextBox, который должен появиться
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label6.Visible = true; // Здесь label1 - это ваш Label, который должен появиться
            textBox5.Visible = true; // Здесь textBox1 - это ваш TextBox, который должен появиться
            button6.Visible = true;
            label10.Visible = true; // Здесь label1 - это ваш Label, который должен появиться
            textBox9.Visible = true; // Здесь textBox1 - это ваш TextBox, который должен появиться
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
            this.Close();
        }
    } 
}

