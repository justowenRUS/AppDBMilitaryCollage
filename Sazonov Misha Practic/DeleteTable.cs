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
    public partial class DeleteTable : Form
    {
        public DeleteTable()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Создание подключения к базе данных
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=military.mdb;Persist Security Info=False";
            connection.Open();

            // Удаление таблицы
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "DROP TABLE users";
            command.ExecuteNonQuery();

            // Закрытие подключения к базе данных
            connection.Close();
            this.Close();
        }
    }
}
