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
    public partial class InputTable : Form
    {

        public InputTable()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=military.mdb;"))
            {
                connection.Open();

                // Проверка на совпадение логина
                string checkLoginQuery = "SELECT COUNT(*) FROM users WHERE Log = @login";
                using (OleDbCommand checkLoginCommand = new OleDbCommand(checkLoginQuery, connection))
                {
                    checkLoginCommand.Parameters.AddWithValue("@login", textBox4.Text);
                    int loginCount = (int)checkLoginCommand.ExecuteScalar();

                    if (loginCount > 0)
                    {
                        // Пользователь с таким логином уже существует
                        MessageBox.Show("Пользователь с таким логином уже существует.");
                        return;
                    }
                }

                // Проверка на совпадение UserId
                string checkUserIdQuery = "SELECT COUNT(*) FROM users WHERE UserId = @userId";
                using (OleDbCommand checkUserIdCommand = new OleDbCommand(checkUserIdQuery, connection))
                {
                    checkUserIdCommand.Parameters.AddWithValue("@userId", textBox1.Text);
                    int userCount = (int)checkUserIdCommand.ExecuteScalar();

                    if (userCount > 0)
                    {
                        // Пользователь с таким UserId уже существует
                        MessageBox.Show("Пользователь с таким UserId уже существует.");
                        return;
                    }
                }

                // Создание команды для выполнения запроса добавления пользователя
                using (OleDbCommand command = new OleDbCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO users (UserId, FirstName, SoName, Log, Pass, Policy) VALUES (@value1, @value2, @value3, @value4, @value5, @value6)";
                    command.Parameters.AddWithValue("@value1", textBox1.Text);
                    command.Parameters.AddWithValue("@value2", textBox2.Text);
                    command.Parameters.AddWithValue("@value3", textBox3.Text);
                    command.Parameters.AddWithValue("@value4", textBox4.Text);
                    command.Parameters.AddWithValue("@value5", textBox5.Text);
                    command.Parameters.AddWithValue("@value6", comboBox1.Text);
                    command.ExecuteNonQuery();
                    connection.Close();

                    // Пользователь успешно добавлен
                    MessageBox.Show("Пользователь успешно добавлен.");
                    // Закрытие текущей формы и открытие другой формы (например, Form1)
                    Form1 form1 = new Form1();
                    this.Hide();
                    form1.ShowDialog();
                    this.Hide();
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
