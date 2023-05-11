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

        private string tablename;

        public InputTable()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=military.mdb;";
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO " + tablename + " (UserId, FirstName, SoName, Log, Pass) VALUES (@values1, @values2, @values3, @values4, @values5)";
            command.Parameters.AddWithValue("@value1", textBox1.Text);
            command.Parameters.AddWithValue("@value2", textBox2.Text);
            command.Parameters.AddWithValue("@value3", textBox3.Text);
            command.Parameters.AddWithValue("@value4", textBox4.Text);
            command.Parameters.AddWithValue("@value5", textBox5.Text);
            command.ExecuteNonQuery();
            connection.Close();
            this.Close();
        }
    }
}
