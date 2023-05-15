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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void создатьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTable form2 = new CreateTable();
            this.Hide();
            form2.ShowDialog();
            this.Close();
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
                string query = "SELECT * FROM users";

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
    }
}
