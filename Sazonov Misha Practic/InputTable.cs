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
    public partial class InputTable : Form
    {

        public InputTable()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (true)
            {
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=military.mdb;";
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO users (UserId, FirstName, SoName, Log, Pass, Policy) VALUES (@value1, @value2, @value3, @value4, @value5, @value6)";
                command.Parameters.AddWithValue("@value1", textBox1.Text);
                command.Parameters.AddWithValue("@value2", textBox2.Text);
                command.Parameters.AddWithValue("@value3", textBox3.Text);
                command.Parameters.AddWithValue("@value4", textBox4.Text);
                command.Parameters.AddWithValue("@value5", textBox5.Text);
                command.Parameters.AddWithValue("@value6", comboBox1.Text);
                command.ExecuteNonQuery();
                connection.Close();
                this.Close();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
