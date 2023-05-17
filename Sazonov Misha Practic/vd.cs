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
    public partial class vd : Form
    {
        private string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=military.mdb";
        private OleDbConnection connection;

        public vd()
        {
            InitializeComponent();
            connection = new OleDbConnection(connectionString);
            connection.Open();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT s.UserID, s.FirstName, s.SoName, g.Gender " +
                   "FROM Students s " +
                   "INNER JOIN Gender g ON s.UserID = g.UserID";

            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int studentID = reader.GetInt32(0);
                    string studentFirstName = reader.GetString(1);
                    string studentSoName = reader.GetString(2);
                    string studentGender = reader.GetString(3);

                    textBox6.Text = studentID.ToString();
                    textBox4.Text = studentFirstName;
                    textBox3.Text = studentSoName;
                    textBox7.Text = studentGender;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int userID = int.Parse(textBox5.Text);
            string firstName = textBox1.Text;
            string soName = textBox2.Text;
            string gender = comboBox1.Text;

            // Вставка данных в таблицу "Students"
            string insertStudentsQuery = $"INSERT INTO Students (UserID, FirstName, SoName) VALUES ({userID}, '{firstName}', '{soName}')";
            OleDbCommand insertStudentsCommand = new OleDbCommand(insertStudentsQuery, connection);
            insertStudentsCommand.ExecuteNonQuery();

            // Вставка данных в таблицу "Gender"
            string insertGenderQuery = $"INSERT INTO Gender (UserID, Gender) VALUES ({userID}, '{gender}')";
            OleDbCommand insertGenderCommand = new OleDbCommand(insertGenderQuery, connection);
            insertGenderCommand.ExecuteNonQuery();

            MessageBox.Show("Данные успешно добавлены.");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            this.Hide();
            frm1.ShowDialog();
            this.Close();
        }
    }
}
