using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing;

namespace Sazonov_Misha_Practic
{
    public partial class Editor : Form
    {
        private Label selectTableLabel;
        private ComboBox tablesComboBox;
        private DataGridView dataGridView;
        private DataTable dataTable;
        private string connectionString;

        public Editor()
        {
            InitializeComponent();
            InitializeForm();
            InitializeControls();
            ShowTables();
        }

        private void InitializeForm()
        {
            Text = "Редактировать";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            Font = new Font("Arial", 12, FontStyle.Regular);
        }

        private void InitializeControls()
        {
            selectTableLabel = new Label();
            selectTableLabel.Text = "Выберите таблицу:";
            selectTableLabel.AutoSize = true;
            selectTableLabel.Location = new Point(50, 50);
            selectTableLabel.Font = Font;
            Controls.Add(selectTableLabel);

            tablesComboBox = new ComboBox();
            tablesComboBox.Size = new Size(200, 30);
            tablesComboBox.Location = new Point(200, 50);
            tablesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            tablesComboBox.Font = Font;
            tablesComboBox.Margin = new Padding(0);
            tablesComboBox.SelectedIndexChanged += TablesComboBox_SelectedIndexChanged;
            Controls.Add(tablesComboBox);

            dataGridView = new DataGridView();
            dataGridView.Location = new Point(50, 100);
            dataGridView.Size = new Size(1000, 500);
            dataGridView.AllowUserToAddRows = true; // Разрешить добавление строк пользователем
            dataGridView.AllowUserToDeleteRows = true; // Разрешить удаление строк пользователем
            Controls.Add(dataGridView);
        }

        private void ShowTables()
        {
            tablesComboBox.Items.Clear();
            connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=military.mdb";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Получение списка всех таблиц в базе данных
                DataTable tablesSchema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                // Отображение таблиц в комбобоксе
                foreach (DataRow row in tablesSchema.Rows)
                {
                    string tableName = (string)row["TABLE_NAME"];
                    tablesComboBox.Items.Add(tableName);
                }
                connection.Close();
            }
        }

        private void TablesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tableName = tablesComboBox.SelectedItem.ToString();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Загрузка данных из выбранной таблицы в DataTable
                string query = $"SELECT * FROM {tableName}";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Привязка DataTable к DataGridView
                dataGridView.DataSource = dataTable;

                connection.Close();
            }
        }
    }
}