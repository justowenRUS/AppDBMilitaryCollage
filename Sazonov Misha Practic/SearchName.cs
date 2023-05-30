using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Sazonov_Misha_Practic
{
    public partial class SearchName : Form
    {
        private OleDbConnection connection;

        public SearchName()
        {
            InitializeComponent();
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=military.mdb";
            connection = new OleDbConnection(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text;

            // Проверяем минимальную длину текста
            if (searchValue.Length < 3)
            {
                MessageBox.Show("Введите минимум 3 символа для поиска.");
                return;
            }

            try
            {
                connection.Open();

                DataTable resultTable = new DataTable();

                // Получаем список таблиц в базе данных
                List<string> tableNames = GetTableNames();

                foreach (string tableName in tableNames)
                {
                    if (tableName.ToLower() != "students" && tableName.ToLower() != "gender")
                        continue; // Пропускаем таблицы, кроме "Students" и "Gender"

                    DataTable table = GetTableStructure(tableName);

                    foreach (DataColumn column in table.Columns)
                    {
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;

                        string sqlQuery = $"SELECT * FROM {tableName} WHERE {column.ColumnName} LIKE @searchValue";

                        command.CommandText = sqlQuery;
                        command.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");

                        OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);

                        // Заполняем таблицу с результатами поиска
                        DataTable searchResultTable = new DataTable();
                        dataAdapter.Fill(searchResultTable);

                        // Если найдены результаты поиска, добавляем их к общей таблице результатов
                        if (searchResultTable.Rows.Count > 0)
                        {
                            // Добавляем столбцы из текущей таблицы в общую таблицу результатов, если их еще нет
                            foreach (DataColumn searchColumn in searchResultTable.Columns)
                            {
                                if (!resultTable.Columns.Contains(searchColumn.ColumnName))
                                    resultTable.Columns.Add(searchColumn.ColumnName, searchColumn.DataType);
                            }

                            // Копируем данные из текущей таблицы в общую таблицу результатов
                            foreach (DataRow searchRow in searchResultTable.Rows)
                            {
                                DataRow newRow = resultTable.NewRow();

                                // Копируем данные из текущей таблицы
                                foreach (DataColumn searchColumn in searchResultTable.Columns)
                                {
                                    newRow[searchColumn.ColumnName] = searchRow[searchColumn.ColumnName];
                                }

                                // Загружаем информацию о поле (Gender) по найденному UserID из таблицы "Students"
                                if (tableName.ToLower() == "students")
                                {
                                    string userID = searchRow["UserID"].ToString();
                                    DataTable genderTable = GetGenderInfo(userID);
                                    if (genderTable.Rows.Count > 0)
                                    {
                                        // Копируем данные из таблицы "Gender"
                                        foreach (DataColumn genderColumn in genderTable.Columns)
                                        {
                                            if (!resultTable.Columns.Contains(genderColumn.ColumnName))
                                                resultTable.Columns.Add(genderColumn.ColumnName, genderColumn.DataType);

                                            newRow[genderColumn.ColumnName] = genderTable.Rows[0][genderColumn.ColumnName];
                                        }
                                    }
                                }

                                resultTable.Rows.Add(newRow);
                            }
                        }

                    }
                }

                // Удаляем столбец UserID из итоговой таблицы результатов
                resultTable.Columns.Remove("UserID");

                // Привязываем результаты поиска к DataGridView для отображения
                dataGridView1.DataSource = resultTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при выполнении поиска: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }



        private List<string> GetTableNames()
        {
            List<string> tableNames = new List<string>();

            DataTable schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            foreach (DataRow row in schemaTable.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();
                tableNames.Add(tableName);
            }

            return tableNames;
        }

        private DataTable GetTableStructure(string tableName)
        {
            DataTable tableStructure = new DataTable();

            // Проверяем, что таблица является таблицей "Students" или "Gender"
            if (tableName.ToLower() == "students" || tableName.ToLower() == "gender")
            {
                DataTable schemaTable = connection.GetSchema("Columns", new[] { null, null, tableName });

                foreach (DataRow row in schemaTable.Rows)
                {
                    string columnName = row["COLUMN_NAME"].ToString();
                    tableStructure.Columns.Add(columnName);
                }
            }

            return tableStructure;
        }

        private DataTable GetGenderInfo(string userID)
        {
            DataTable genderTable = new DataTable();

            try
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;

                string sqlQuery = $"SELECT * FROM Gender WHERE UserID = @userID";

                command.CommandText = sqlQuery;
                command.Parameters.AddWithValue("@userID", userID);

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);

                // Заполняем таблицу с информацией о поле (Gender)
                dataAdapter.Fill(genderTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке информации о поле (Gender): " + ex.Message);
            }

            return genderTable;
        }
    }
}