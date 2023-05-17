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
using System.Runtime.InteropServices;
using System.IO;

namespace Sazonov_Misha_Practic
{
    public partial class CreateTable : Form
    {
        public CreateTable()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count = (int)numericUpDown1.Value;

            for (int i = 0; i < count; i++)
            {
                // Создание label и textbox для названия колонки
                Label nameLabel = new Label();
                nameLabel.Text = "Название колонки:";
                TextBox nameTextBox = new TextBox();
                nameLabel.ForeColor = SystemColors.ButtonFace;

                // Создание label и combobox для выбора типа данных
                Label typeLabel = new Label();
                typeLabel.Text = "Тип данных:";
                ComboBox typeComboBox = new ComboBox();
                typeComboBox.Items.Add("INT");
                typeComboBox.Items.Add("VARCHAR(25)");
                typeLabel.ForeColor = SystemColors.ButtonFace;

                // Создание контейнера Panel для расположения элементов горизонтально
                Panel panel = new Panel();
                panel.Dock = DockStyle.Top;

                // Задание размеров элементов
                nameLabel.Size = new Size(100, 20);
                nameTextBox.Size = new Size(100, 20);
                typeLabel.Size = new Size(100, 20);
                typeComboBox.Size = new Size(100, 20);

                // Установка позиции элементов в контейнере Panel
                nameTextBox.Location = new Point(0, 0);
                nameLabel.Location = new Point(110, 0);
                typeComboBox.Location = new Point(220, 0);
                typeLabel.Location = new Point(330, 0);

                // Установка отступа между элементами
                nameLabel.Margin = new Padding(0, 0, 10, 0); // Установите желаемое значение отступа

                // Добавление элементов в контейнер Panel
                panel.Controls.Add(nameTextBox);
                panel.Controls.Add(nameLabel);
                panel.Controls.Add(typeComboBox);
                panel.Controls.Add(typeLabel);

                // Добавление контейнера Panel на форму
                panel1.Controls.Add(panel);

                while (panel1.Controls.Count > count)
                    {
                        panel1.Controls.RemoveAt(panel1.Controls.Count - 1);
                    }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string tableName = textBox1.Text;
            string configFile = "config.ini";
            string databasePath;

            // Чтение значения пути из ini-файла
            if (File.Exists(configFile))
            {
                IniFile ini = new IniFile(configFile);
                databasePath = ini.GetValue("Database", "Path");
            }
            else
            {
                // Обработка ситуации, когда ini-файл отсутствует
                // или не содержит нужных настроек
                return;
            }

            // Создание строки подключения к базе данных
            string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={databasePath};Persist Security Info=False";

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

                if (tableExists)
                {
                    MessageBox.Show($"Таблица с именем '{tableName}' уже существует.");
                    return;
                }

                // Создание SQL-запроса для создания таблицы
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append($"CREATE TABLE {tableName} (");

                // Получение информации о колонках из элементов управления
                foreach (Control control in panel1.Controls)
                {
                    if (control is Panel panel)
                    {
                        foreach (Control innerControl in panel.Controls)
                        {
                            if (innerControl is TextBox nameTextBox)
                            {
                                string columnName = nameTextBox.Text;
                                queryBuilder.Append($"{columnName} ");
                            }
                            else if (innerControl is ComboBox typeComboBox)
                            {
                                string columnType = typeComboBox.SelectedItem.ToString();
                                queryBuilder.Append($"{columnType}, ");
                            }
                        }
                    }
                }

                // Удаление последней запятой и пробела из запроса
                queryBuilder.Length -= 2;
                queryBuilder.Append(")");

                // Создание команды и выполнение запроса
                using (OleDbCommand command = new OleDbCommand(queryBuilder.ToString(), connection))
                {
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Таблица успешно создана.");
                connection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
           
        }
    }
    public class IniFile
    {
        private readonly Dictionary<string, Dictionary<string, string>> iniData;

        public IniFile(string filePath)
        {
            iniData = new Dictionary<string, Dictionary<string, string>>();
            string currentSection = null;

            foreach (string line in File.ReadLines(filePath))
            {
                string trimmedLine = line.Trim();

                if (string.IsNullOrWhiteSpace(trimmedLine))
                    continue;

                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2);
                    iniData[currentSection] = new Dictionary<string, string>();
                }
                else if (currentSection != null && trimmedLine.Contains("="))
                {
                    string[] parts = trimmedLine.Split('=');
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    iniData[currentSection][key] = value;
                }
            }
        }

        public string GetValue(string section, string key)
        {
            if (iniData.ContainsKey(section) && iniData[section].ContainsKey(key))
            {
                return iniData[section][key];
            }

            return null;
        }
    }


    // Вспомогательный класс для импорта функций чтения и записи ini-файлов
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        internal static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder value, int size, string filePath);

        [DllImport("kernel32.dll")]
        internal static extern bool WritePrivateProfileString(string section, string key, string value, string filePath);
    }
}

