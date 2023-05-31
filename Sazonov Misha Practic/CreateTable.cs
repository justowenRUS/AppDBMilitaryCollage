using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Sazonov_Misha_Practic
{
    public partial class CreateTable : Form
    {

        public CreateTable()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tableName = textBox1.Text.Trim();
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=miltary.mdb;";
            OleDbConnection connection = new OleDbConnection(connectionString);

            if (string.IsNullOrEmpty(tableName))
            {
                MessageBox.Show("Введите название таблицы.");
                return;
            }

            // Создаем таблицу в базе данных
            try
            {
                connection.Open();

                OleDbCommand createTableCommand = new OleDbCommand();
                createTableCommand.Connection = connection;
                createTableCommand.CommandType = CommandType.Text;

                // Создаем SQL-запрос для создания таблицы
                StringBuilder sqlQuery = new StringBuilder();
                sqlQuery.Append($"CREATE TABLE {tableName} (");

                // Перебираем строки DataGridView, чтобы получить названия столбцов и их типы данных
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    string columnName = row.Cells[0].Value.ToString();
                    string columnType = row.Cells[1].Value.ToString();

                    // Добавляем столбец и его тип данных к SQL-запросу
                    sqlQuery.Append($"{columnName} {columnType},");
                }

                // Удаляем последнюю запятую
                sqlQuery.Length--;

                sqlQuery.Append(")");

                createTableCommand.CommandText = sqlQuery.ToString();

                createTableCommand.ExecuteNonQuery();

                MessageBox.Show($"Таблица {tableName} успешно создана в базе данных.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при создании таблицы: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void CreateTable_Load(object sender, EventArgs e)
        {
            // Инициализация DataGridView с двумя столбцами
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].HeaderText = "Название колонки";
            dataGridView1.Columns[1].HeaderText = "Тип данных";

            // Добавление типов данных в ComboBox ячейки второго столбца
            DataGridViewComboBoxColumn columnTypeColumn = (DataGridViewComboBoxColumn)dataGridView1.Columns[1];
            columnTypeColumn.Items.Add("INT");
            columnTypeColumn.Items.Add("VARCHAR(30)");
        }
    }
}