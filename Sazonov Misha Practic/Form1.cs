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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Sazonov_Misha_Practic
{
    public partial class Form1 : Form
    {
        private bool isDragging = false;
        private Point dragStartPosition;

        public Form1()
        {
            InitializeComponent();
            InitializeMenuStripDesign();
            InitializeExitButton();
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            InitializeForm();
        }

        private void InitializeForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(44, 62, 80); // Прикольный цвет фона
        }

        private void InitializeMenuStripDesign()
        {
            // Настройка цвета фона и цвета текста для пунктов меню
            menuStrip1.BackColor = Color.FromArgb(0, 122, 204);
            menuStrip1.ForeColor = Color.White;

            // Настройка цвета фона и цвета текста для подменю
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                item.BackColor = Color.FromArgb(0, 122, 204);
                item.ForeColor = Color.White;

                foreach (ToolStripDropDownItem subItem in item.DropDownItems)
                {
                    subItem.BackColor = Color.FromArgb(0, 122, 204);
                    subItem.ForeColor = Color.White;
                }
            }
        }
        private void InitializeExitButton()
        {
            // Создаем кнопку выхода
            Button exitButton = new Button();
            exitButton.Text = "X";
            exitButton.Size = new Size(23, 23);
            exitButton.Location = new Point(this.Width - exitButton.Width - 1, 1);
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.BackColor = menuStrip1.BackColor; // Используем цвет фона меню
            exitButton.ForeColor = Color.White;
            exitButton.Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
            exitButton.Click += (sender, e) => Application.Exit();

            // Добавляем кнопку на форму
            this.Controls.Add(exitButton);

            // Перемещаем кнопку в передний план
            exitButton.BringToFront();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPosition = new Point(e.X, e.Y);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = PointToScreen(e.Location);
                Location = new Point(currentPosition.X - dragStartPosition.X, currentPosition.Y - dragStartPosition.Y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void создатьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void удалитьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteTable form3 = new DeleteTable();
            this.Hide();
            form3.ShowDialog();
            this.Close();
        }

        private void менюToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void вводЗначенийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputTable form4 = new InputTable();
            this.Hide();
            form4.ShowDialog();
            this.Close();
        }

        private void работаСТаблицейToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void вводДанныхПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userbaza userbd = new userbaza();
            this.Hide();
            userbd.ShowDialog();
            this.Close();
        }

        private void темнаяТемаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
        }

        private void голубаяТемаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void светлаяТемаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            polzovatel polz = new polzovatel();
            this.Hide();
            polz.ShowDialog();
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=military.mdb;";
 
            // Создаем документ PDF
            Document document = new Document();

            // Указываем путь и имя файла для сохранения
            string outputFile = "output.pdf";

            // Создаем писателя PDF
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outputFile, FileMode.Create));

            // Открываем документ для записи
            document.Open();

            // Создаем подключение к базе данных
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Открываем подключение к базе данных
                connection.Open();

                // Создаем SQL-запрос для получения данных
                string query = "SELECT * FROM Students";

                // Создаем команду
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    // Выполняем команду и получаем результаты
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        // Перебираем результаты и добавляем их в PDF-документ
                        while (reader.Read())
                        {
                            //читаем значения
                            string field1Value = reader.GetString(1);
                            string field2Value = reader.GetString(2);
                            string field3Value = reader.GetString(3);
                            string field4Value = reader.GetString(4);
                            string field5Value = reader.GetString(5);

                            // Добавляем значения полей в документ PDF
                            document.Add(new Paragraph(field1Value));
                            document.Add(new Paragraph(field2Value));
                            document.Add(new Paragraph(field3Value));
                            document.Add(new Paragraph(field4Value));
                            document.Add(new Paragraph(field5Value));
                        }
                    }
                }
            }

            // Закрываем документ и освобождаем ресурсы
            document.Close();
        
        }

        private void выводДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vd vdd = new vd();
            this.Hide();
            vdd.ShowDialog();
            this.Close();
        }

        private void вводДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vid vivod = new vid();
            this.Hide();
            vivod.ShowDialog();
            this.Close();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoundedForm ip = new RoundedForm();
            ip.ShowDialog();

        }

        private void создатьТаблицуToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateTable form2 = new CreateTable();
            this.Hide();
            form2.ShowDialog();
            this.Close();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteTable form3 = new DeleteTable();
            this.Hide();
            form3.ShowDialog();
            this.Close();
        }

        private void связатьТаблицыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SvuyazatTablitsi st = new SvuyazatTablitsi();
            this.Hide();
            st.ShowDialog();
            this.Close();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor editor = new Editor();
            this.Close();
            editor.ShowDialog();
            this.Close();
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchName sern = new SearchName();
            this.Close();
            sern.ShowDialog();
            this.Close();
        }
    }
}
