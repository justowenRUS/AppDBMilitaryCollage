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
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Строка подключения к базе данных MDB
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=military.mdb";

            // Значения для проверки

            // SQL-запрос для проверки аутентификации
            string query = "SELECT Policy FROM users WHERE Log = @login AND Pass = @password";
            string login = textBox1.Text;
            string password = textBox2.Text;
            // Создание подключения к базе данных
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Проверка наличия пользователя в базе данных
                string checkUserQuery = "SELECT COUNT(*) FROM users WHERE Log = @login";
                using (OleDbCommand checkUserCommand = new OleDbCommand(checkUserQuery, connection))
                {
                    checkUserCommand.Parameters.AddWithValue("@login", login);
                    int userCount = (int)checkUserCommand.ExecuteScalar();

                    if (userCount == 0)
                    {
                        // Пользователь не найден
                        MessageBox.Show("Пользователь не найден.");
                        return;
                    }
                }

                // Создание команды для выполнения запроса аутентификации
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    // Передача параметров запроса
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);

                    // Выполнение запроса и получение результата
                    object policyObj = command.ExecuteScalar();

                    if (policyObj != null)
                    {
                        string policy = policyObj.ToString();

                        if (policy == "Администратор")
                        {
                            // Аутентификация успешна для администратора
                            MessageBox.Show("Вы успешно авторизованы как администратор.");
                            Form1 form1 = new Form1();
                            form1.Show();
                            this.Hide();
                            // Дополнительные действия после аутентификации администратора
                        }
                        else
                        {
                            // Аутентификация успешна для другого пользователя
                            MessageBox.Show("Вы успешно авторизованы.");
                            userstable users = new userstable();
                            users.Show();
                            // Дополнительные действия после аутентификации пользователя
                        }
                    }
                    else
                    {
                        // Неправильный логин или пароль
                        MessageBox.Show("Неправильный логин или пароль.");
                        // Дополнительные действия в случае неверных учетных данных
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }
    }
}
