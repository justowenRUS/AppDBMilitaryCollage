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
            string tableName = comboBox1.SelectedItem.ToString();
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=military.mdb;Persist Security Info=False";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Проверка существования таблицы
                DataTable schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                bool tableExists = false;

                foreach (DataRow row in schemaTable.Rows)
                {
                    string existingTableName = row["TABLE_NAME"].ToString();

                    if (existingTableName == tableName)
                    {
                        tableExists = true;
                        break;
                    }
                }

                if (!tableExists)
                {
                    MessageBox.Show($"Таблица с именем '{tableName}' не существует.");
                    return;
                }

                // Создание SQL-запроса для удаления таблицы
                string query = $"DROP TABLE {tableName}";

                // Создание команды и выполнение запроса
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }

                MessageBox.Show($"Таблица '{tableName}' успешно удалена.");
                connection.Close();
            }
            ShowTables();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            this.Hide();
            frm1.ShowDialog();
            this.Close();
        }
    }
}
