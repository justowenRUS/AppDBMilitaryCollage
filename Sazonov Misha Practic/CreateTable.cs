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
        private int columnCount;
        private Label tableNameLabel;
        private TextBox tableNameTextBox;
        private Button createTableButton;
        private Panel panel = new Panel();
        private List<Label> columnNameLabels = new List<Label>();
        private List<TextBox> columnNameTextBoxes = new List<TextBox>();
        private List<Label> dataTypeLabels = new List<Label>();
        private List<ComboBox> dataTypeComboBoxes = new List<ComboBox>();
        private NumericUpDown columnCountNumericUpDown;

        public CreateTable()
        {
            InitializeComponent();
        }

        private void CreateTable_Load_1(object sender, EventArgs e)
        {
            CreateControls();
            AddCreateTableButton();
            InitializeForm();
        }

        private void InitializeForm()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(44, 62, 80); // Прикольный цвет фона
            Font = new Font("Arial", 12, FontStyle.Regular);
        }

        private void CreateControls()
        {
            tableNameLabel = new Label
            {
                Text = "Название таблицы:",
                Location = new System.Drawing.Point(12, 15),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Arial", 14, FontStyle.Bold),
            };
            this.Controls.Add(tableNameLabel);

            tableNameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(130, 12),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(236, 240, 241),
                ForeColor = Color.FromArgb(44, 62, 80),
                Font = new Font("Arial", 12, FontStyle.Regular),
            };
            this.Controls.Add(tableNameTextBox);

            columnCountNumericUpDown = new NumericUpDown
            {
                Location = new System.Drawing.Point(130, 42),
                Minimum = 1,
                Maximum = 10,
                BackColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.FromArgb(44, 62, 80),
                Font = new Font("Arial", 12, FontStyle.Regular),
            };
            columnCountNumericUpDown.ValueChanged += (s, e) => UpdateColumns((int)columnCountNumericUpDown.Value);
            this.Controls.Add(columnCountNumericUpDown);

            Label columnCountLabel = new Label
            {
                Text = "Количество столбцов:",
                Location = new System.Drawing.Point(12, 45),
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Arial", 14, FontStyle.Bold),
            };
            this.Controls.Add(columnCountLabel);
        }


        private void UpdateColumns(int newColumnCount)
        {
            if (newColumnCount == columnCount)
                return;

            if (newColumnCount > columnCount)
            {
                int startingIndex = columnCount > 0 ? columnNameLabels.Count : 0;

                for (int i = startingIndex; i < newColumnCount; i++)
                {
                    Label columnNameLabel = new Label { Text = "Колонка:", Location = new System.Drawing.Point(12 + 150 * i, 75) };
                    this.Controls.Add(columnNameLabel);
                    columnNameLabels.Add(columnNameLabel);

                    TextBox columnNameTextBox = new TextBox { Location = new System.Drawing.Point(130 + 150 * i, 72) };
                    this.Controls.Add(columnNameTextBox);
                    columnNameTextBoxes.Add(columnNameTextBox);

                    Label dataTypeLabel = new Label { Text = "Тип данных:", Location = new System.Drawing.Point(12 + 150 * i, 105) };
                    this.Controls.Add(dataTypeLabel);
                    dataTypeLabels.Add(dataTypeLabel);

                    ComboBox dataTypeComboBox = new ComboBox { Items = { "INT", "VARCHAR(25)" }, DropDownStyle = ComboBoxStyle.DropDownList, Location = new System.Drawing.Point(130 + 150 * i, 102) };
                    this.Controls.Add(dataTypeComboBox);
                    dataTypeComboBoxes.Add(dataTypeComboBox);
                }

                this.Width += 90 * (newColumnCount - columnCount);
            }
            else
            {
                for (int i = columnCount - 1; i >= newColumnCount; i--)
                {
                    Label columnNameLabel = columnNameLabels[i];
                    this.Controls.Remove(columnNameLabel);
                    columnNameLabel.Dispose();
                    columnNameLabels.RemoveAt(i);

                    TextBox columnNameTextBox = columnNameTextBoxes[i];
                    this.Controls.Remove(columnNameTextBox);
                    columnNameTextBox.Dispose();
                    columnNameTextBoxes.RemoveAt(i);

                    Label dataTypeLabel = dataTypeLabels[i];
                    this.Controls.Remove(dataTypeLabel);
                    dataTypeLabel.Dispose();
                    dataTypeLabels.RemoveAt(i);

                    ComboBox dataTypeComboBox = dataTypeComboBoxes[i];
                    this.Controls.Remove(dataTypeComboBox);
                    dataTypeComboBox.Dispose();
                    dataTypeComboBoxes.RemoveAt(i);
                }

                this.Width -= 100 * (columnCount - newColumnCount);
            }

            columnCount = newColumnCount;
        }

        private void CreateDatabaseTable(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                MessageBox.Show("Введите название таблицы.");
                return;
            }

            int columnCount = (int)columnCountNumericUpDown.Value;

            if (columnCount <= 0)
            {
                MessageBox.Show("Количество колонок должно быть больше нуля.");
                return;
            }

            Regex russianCharactersRegex = new Regex(@"[А-Яа-яЁё]");
            if (russianCharactersRegex.IsMatch(tableName))
            {
                MessageBox.Show("Название таблицы не должно содержать русских символов.");
                return;
            }

            string configFile = "config.ini";
            ConfigReader configReader = new ConfigReader(configFile);
            string databasePath = configReader.GetValue("Database", "Path");
            string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={databasePath};Persist Security Info=False";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

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

                if (tableExists)
                {
                    MessageBox.Show($"Таблица с именем '{tableName}' уже существует.");
                    return;
                }

                for (int i = 0; i < columnCount; i++)
                {
                    TextBox columnNameTextBox = columnNameTextBoxes[i];
                    string columnName = columnNameTextBox.Text;

                    if (russianCharactersRegex.IsMatch(columnName))
                    {
                        MessageBox.Show("Название колонки не должно содержать русских символов.");
                        return;
                    }
                }

                for (int i = 0; i < columnCount; i++)
                {
                    TextBox columnNameTextBox = columnNameTextBoxes[i];
                    ComboBox dataTypeComboBox = dataTypeComboBoxes[i];

                    string columnName = columnNameTextBox.Text;
                    string dataType = dataTypeComboBox.SelectedItem?.ToString();

                    if (string.IsNullOrEmpty(columnName))
                    {
                        MessageBox.Show("Введите название колонки.");
                        return;
                    }

                    if (string.IsNullOrEmpty(dataType))
                    {
                        MessageBox.Show("Выберите тип данных для колонки.");
                        return;
                    }
                }

                List<string> existingColumns = new List<string>();
                foreach (DataRow row in schemaTable.Rows)
                {
                    string existingTableName = row["TABLE_NAME"].ToString();

                    if (existingTableName != tableName)
                    {
                        DataTable existingTableSchema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, existingTableName, null });
                        foreach (DataRow columnRow in existingTableSchema.Rows)
                        {
                            string columnName = columnRow["COLUMN_NAME"].ToString();
                            existingColumns.Add(columnName);
                        }
                    }
                }

                for (int i = 0; i < columnCount; i++)
                {
                    ComboBox dataTypeComboBox = dataTypeComboBoxes[i];
                    TextBox columnNameTextBox = columnNameTextBoxes[i];

                    string columnName = columnNameTextBox.Text;

                    if (existingColumns.Contains(columnName))
                    {
                        MessageBox.Show($"Колонка с именем '{columnName}' уже существует.");
                        return;
                    }

                    existingColumns.Add(columnName);
                }

                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append($"CREATE TABLE {tableName} (");

                for (int i = 0; i < columnCount; i++)
                {
                    ComboBox dataTypeComboBox = dataTypeComboBoxes[i];
                    TextBox columnNameTextBox = columnNameTextBoxes[i];

                    string columnName = columnNameTextBox.Text;
                    string dataType = dataTypeComboBox.SelectedItem.ToString();

                    queryBuilder.Append($"{columnName} {dataType}, ");
                }

                queryBuilder.Length -= 2;
                queryBuilder.Append(")");

                using (OleDbCommand command = new OleDbCommand(queryBuilder.ToString(), connection))
                {
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Таблица успешно создана.");
                connection.Close();
            }
        }

        private void AddCreateTableButton()
        {
            createTableButton = new Button();
            createTableButton.Text = "Создать таблицу";
            createTableButton.Location = new System.Drawing.Point((this.ClientSize.Width - createTableButton.Width) / 2, this.ClientSize.Height - createTableButton.Height - 10);
            createTableButton.Click += createTableButton_Click;
            this.Controls.Add(createTableButton);
        }

        private void createTableButton_Click(object sender, EventArgs e)
        {
            string tableName = tableNameTextBox.Text;
            CreateDatabaseTable(tableName);
        }
    }
}