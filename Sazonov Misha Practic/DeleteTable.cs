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
            ShowTables();
        }

        private void ShowTables()
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=military.mdb";
            ComboBox comboBox = new ComboBox();
            // Предполагается, что у вас уже есть комбобокс comboBox

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Получение списка всех таблиц в базе данных
                DataTable tablesSchema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                // Отображение таблиц в комбобоксе
                foreach (DataRow row in tablesSchema.Rows)
                {
                    string tableName = (string)row["TABLE_NAME"];
                    comboBox1.Items.Add(tableName);
                }
                connection.Close();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=military.mdb;Persist Security Info=False"))
            {
                connection.Open();

                string tableName = comboBox1.Text;

                // Проверка существования таблицы
                string checkTableQuery = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";
                using (OleDbCommand checkTableCommand = new OleDbCommand(checkTableQuery, connection))
                {
                    int tableCountBefore = (int)checkTableCommand.ExecuteScalar();

                    // Удаление таблицы
                    OleDbCommand dropTableCommand = new OleDbCommand();
                    dropTableCommand.Connection = connection;
                    dropTableCommand.CommandText = $"DROP TABLE {tableName}";
                    dropTableCommand.ExecuteNonQuery();

                    // Проверка успешного удаления таблицы
                    int tableCountAfter = (int)checkTableCommand.ExecuteScalar();

                    if (tableCountBefore > tableCountAfter)
                    {
                        MessageBox.Show($"Таблица '{tableName}' успешно удалена.");
                    }
                    else
                    {
                        MessageBox.Show($"Не удалось удалить таблицу '{tableName}'.");
                    }
                }
                connection.Close();
                ShowTables();
            }
        }



        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Выполнение SQL-запроса для удаления таблицы selectedTable из базы данных
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
