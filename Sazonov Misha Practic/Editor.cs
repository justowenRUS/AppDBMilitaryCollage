using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Sazonov_Misha_Practic
{
    public partial class Editor : Form
    {
        private ComboBox tableComboBox;
        private ComboBox functionComboBox;
        private DataGridView dataGridView;
        private Button saveButton;
 
        string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=military.mdb";

        public Editor()
        {
            InitializeComponents();
            InitializeComboBoxes();
        }

        private void InitializeComponents()
        {
            tableComboBox = new ComboBox();
            tableComboBox.Location = new System.Drawing.Point(12, 12);
            tableComboBox.Size = new System.Drawing.Size(200, 21);
            tableComboBox.SelectedIndexChanged += TableComboBox_SelectedIndexChanged;
            Controls.Add(tableComboBox);

            functionComboBox = new ComboBox();
            functionComboBox.Location = new System.Drawing.Point(12, 39);
            functionComboBox.Size = new System.Drawing.Size(200, 21);
            functionComboBox.SelectedIndexChanged += functionComboBox_SelectedIndexChanged;
            Controls.Add(functionComboBox);

            dataGridView = new DataGridView();
            dataGridView.Location = new System.Drawing.Point(12, 66);
            dataGridView.Size = new System.Drawing.Size(500, 300);
            Controls.Add(dataGridView);

            saveButton = new Button();
            saveButton.Location = new System.Drawing.Point(12, 372);
            saveButton.Size = new System.Drawing.Size(75, 23);
            saveButton.Text = "Сохранить";
            saveButton.Click += SaveButton_Click;
            Controls.Add(saveButton);
        }

        private void InitializeComboBoxes()
        {
            // Заполняем ComboBox с таблицами из базы данных
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                DataTable tables = connection.GetSchema("Tables");

                foreach (DataRow row in tables.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();
                    tableComboBox.Items.Add(tableName);
                }
            }

            // Заполняем ComboBox с функциями
            functionComboBox.Items.Add("Изменить значение");
            functionComboBox.Items.Add("Изменить название колонки");
        }

        private void TableComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Обработка изменения выбранной таблицы
            string selectedFunction = functionComboBox.SelectedItem?.ToString();

            if (selectedFunction == "Изменить значение")
            {
                // Отображаем все столбцы и значения из таблицы
                string selectedTable = tableComboBox.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedTable))
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();
                        string query = $"SELECT * FROM [{selectedTable}]";
                        OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            else if (selectedFunction == "Изменить название колонки")
            {
                string selectedTable = tableComboBox.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedTable))
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();
                        DataTable schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, selectedTable, null });
                        DataTable columnNames = new DataTable();
                        columnNames.Columns.Add("Старое название");
                        columnNames.Columns.Add("Новое название");

                        foreach (DataRow row in schemaTable.Rows)
                        {
                            string columnName = row["COLUMN_NAME"].ToString();
                            columnNames.Rows.Add(columnName, "");
                        }

                        dataGridView.DataSource = columnNames;
                    }
                }
            }
        }

        private void functionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Обработка изменения выбранной функции
            string selectedFunction = functionComboBox.SelectedItem?.ToString();

            if (selectedFunction == "Изменить значение")
            {
                // Отображаем все столбцы и значения из таблицы
                string selectedTable = tableComboBox.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedTable))
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();
                        string query = $"SELECT * FROM [{selectedTable}]";
                        OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            else if (selectedFunction == "Изменить название колонки")
            {
                string selectedTable = tableComboBox.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedTable))
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();
                        DataTable schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, selectedTable, null });
                        DataTable columnNames = new DataTable();
                        columnNames.Columns.Add("Старое название");
                        columnNames.Columns.Add("Новое название");

                        foreach (DataRow row in schemaTable.Rows)
                        {
                            string columnName = row["COLUMN_NAME"].ToString();
                            columnNames.Rows.Add(columnName, columnName); // Заполняем новое название со старым названием
                        }

                        dataGridView.DataSource = columnNames;
                    }
                }
            }
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Сохранение изменений в базе данных
            string selectedFunction = functionComboBox.SelectedItem?.ToString();
            string selectedTable = tableComboBox.SelectedItem?.ToString();

            if (selectedFunction == "Изменить значение")
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM [{selectedTable}]";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                    OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                    DataTable dataTable = (DataTable)dataGridView.DataSource;
                    adapter.Update(dataTable);
                }
            }
            else if (selectedFunction == "Изменить название колонки")
            {
                if (!string.IsNullOrEmpty(selectedTable))
                {
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();
                        DataTable columnNames = (DataTable)dataGridView.DataSource;

                        // Создание новой таблицы с новыми названиями колонок
                        string newTableName = $"{selectedTable}_temp";
                        string createTableQuery = $"SELECT * INTO {newTableName} FROM {selectedTable} WHERE 1=0";
                        OleDbCommand createTableCommand = new OleDbCommand(createTableQuery, connection);
                        createTableCommand.ExecuteNonQuery();

                        foreach (DataRow row in columnNames.Rows)
                        {
                            string oldColumnName = row["Старое название"]?.ToString();
                            string newColumnName = row["Новое название"]?.ToString();

                            if (!string.IsNullOrEmpty(newColumnName))
                            {
                                // Добавление переименованных колонок в новую таблицу
                                string alterTableQuery = $"ALTER TABLE {newTableName} ADD COLUMN [{newColumnName}]";
                                OleDbCommand alterTableCommand = new OleDbCommand(alterTableQuery, connection);
                                alterTableCommand.ExecuteNonQuery();

                                // Копирование данных из старой таблицы в новую таблицу
                                string copyDataQuery = $"UPDATE {newTableName} SET [{newColumnName}] = [{oldColumnName}]";
                                OleDbCommand copyDataCommand = new OleDbCommand(copyDataQuery, connection);
                                copyDataCommand.ExecuteNonQuery();
                            }
                        }

                        // Удаление старой таблицы
                        string dropTableQuery = $"DROP TABLE {selectedTable}";
                        OleDbCommand dropTableCommand = new OleDbCommand(dropTableQuery, connection);
                        dropTableCommand.ExecuteNonQuery();

                        // Переименование новой таблицы
                        string renameTableQuery = $"ALTER TABLE {newTableName} RENAME TO {selectedTable}";
                        OleDbCommand renameTableCommand = new OleDbCommand(renameTableQuery, connection);
                        renameTableCommand.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show("Изменения сохранены!");
        }
    }
}