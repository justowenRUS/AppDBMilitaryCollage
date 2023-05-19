using System;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Sazonov_Misha_Practic
{
    public partial class Authorization : Form
    {
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Label usernameLabel;
        private Label passwordLabel;
        private Button loginButton;
        private Label capsLockLabel;

        public Authorization()
        {
            InitializeComponent();
            InitializeForm();
            InitializeControls();
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

        private void InitializeControls()
        {
            usernameLabel = new Label();
            usernameLabel.Text = "Ваш логин:";
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(50, 100);
            usernameLabel.Font = new Font("Arial", 14, FontStyle.Bold); // Прикольный шрифт и стиль для метки
            usernameLabel.ForeColor = Color.White; // Прикольный цвет текста
            Controls.Add(usernameLabel);

            usernameTextBox = new TextBox();
            usernameTextBox.Size = new Size(200, 30);
            usernameTextBox.Location = new Point(152, 100);
            usernameTextBox.BorderStyle = BorderStyle.None; // Убрана граница текстового поля
            usernameTextBox.Font = new Font("Arial", 12, FontStyle.Regular);
            usernameTextBox.BackColor = Color.FromArgb(236, 240, 241); // Прикольный цвет фона текстового поля
            usernameTextBox.ForeColor = Color.FromArgb(44, 62, 80); // Прикольный цвет текста
            Controls.Add(usernameTextBox);

            passwordLabel = new Label();
            passwordLabel.Text = "Пароль:";
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(50, 150);
            passwordLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            passwordLabel.ForeColor = Color.White;
            Controls.Add(passwordLabel);

            passwordTextBox = new TextBox();
            passwordTextBox.Size = new Size(200, 30);
            passwordTextBox.Location = new Point(150, 150);
            passwordTextBox.BorderStyle = BorderStyle.None;
            passwordTextBox.Font = new Font("Arial", 12, FontStyle.Regular);
            passwordTextBox.BackColor = Color.FromArgb(236, 240, 241);
            passwordTextBox.ForeColor = Color.FromArgb(44, 62, 80);
            passwordTextBox.PasswordChar = '*';
            Controls.Add(passwordTextBox);

            loginButton = new Button();
            loginButton.Size = new Size(100, 30);
            loginButton.Location = new Point((Width - loginButton.Width) / 2, 200);
            loginButton.FlatStyle = FlatStyle.Flat;
            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.BackColor = Color.FromArgb(231, 76, 60); // Прикольный цвет фона кнопки
            loginButton.ForeColor = Color.White;
            loginButton.Font = new Font("Arial", 12, FontStyle.Bold);
            loginButton.Text = "Войти";
            loginButton.Click += LoginButton_Click;
            Controls.Add(loginButton);

            Label capsLockLabel = new Label();
            capsLockLabel.AutoSize = true;
            capsLockLabel.ForeColor = Color.Red;
            capsLockLabel.Location = new Point(passwordTextBox.Right + 5, passwordTextBox.Top);
            capsLockLabel.Font = new Font("Arial", 10, FontStyle.Bold); // Прикольный шрифт для предупреждения о CapsLock
            capsLockLabel.Text = "CapsLock";
            capsLockLabel.Visible = false;
            Controls.Add(capsLockLabel);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string configFile = "config.ini";
            ConfigReader configReader = new ConfigReader(configFile);
            string databasePath = configReader.GetValue("Database", "Path");
            string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={databasePath};Persist Security Info=False";

            string query = "SELECT Policy FROM users WHERE Log = @login AND Pass = @password";
            string login = usernameTextBox.Text;
            string password = passwordTextBox.Text;

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
                        MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show("Вы успешно авторизованы как администратор", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Form1 form1 = new Form1();
                            Hide();
                            form1.ShowDialog();
                            Close();
                            // Дополнительные действия после аутентификации администратора
                        }
                        else
                        {
                            // Аутентификация успешна для другого пользователя
                            MessageBox.Show("Вы успешно авторизованы.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            userstable users = new userstable();
                            Hide();
                            users.ShowDialog();
                            Close();
                            // Дополнительные действия после аутентификации пользователя
                        }
                    }
                    else
                    {
                        // Неправильный логин или пароль
                        MessageBox.Show("Неправильный логин или пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Дополнительные действия в случае неверных учетных данных
                    }
                    if (ContainsRussianCharacters(login) || ContainsRussianCharacters(password))
                    {
                        MessageBox.Show("Логин и пароль не могут содержать русские символы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }
        private bool ContainsRussianCharacters(string input)
        {
            return Regex.IsMatch(input, @"\p{IsCyrillic}");
        }

        private void PasswordTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                MessageBox.Show("Caps Lock включен. Убедитесь, что вы вводите пароль правильно.");
                capsLockLabel.Visible = true;
            }
            else
            {
                capsLockLabel.Visible = false;
            }
        }

        private void Authorization_Load(object sender, EventArgs e)
        {
           
        }
    }
}