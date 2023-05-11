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
            // Создание подключения к базе данных
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=military.mdb;";
            connection.Open();

            // Создание листа в базе данных
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "CREATE TABLE " + textBox1.Text + " (UserId INT PRIMARY KEY, FirstName VARCHAR(50), SoName VARCHAR(50), Log VARCHAR(23), Pass VARCHAR(25))";
            command.ExecuteNonQuery();

            // Закрытие подключения к базе данных
            connection.Close();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
