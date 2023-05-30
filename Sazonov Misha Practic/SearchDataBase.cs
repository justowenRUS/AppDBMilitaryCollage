using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Sazonov_Misha_Practic
{
    public partial class SearchDataBase : Form
    {
        private string configFilePath = "config.ini";

        public SearchDataBase()
        {
            InitializeComponent();
        }

        private void SearchDataBase_Load(object sender, EventArgs e)
        {
            SearchDatabaseFile();
        }

        private void SearchDatabaseFile()
        {
            progressBar1.Visible = true; // Показываем ProgressBar
            progressBar1.Style = ProgressBarStyle.Marquee; // Устанавливаем стиль индикатора прогресса на "бегущую строку"

            // Ищем файл military.mdb
            string[] drives = Environment.GetLogicalDrives();
            foreach (string drive in drives)
            {
                // Проверяем доступность диска
                DriveInfo driveInfo = new DriveInfo(drive);
                if (driveInfo.IsReady)
                {
                    string mdbFilePath = SearchFile(drive, "military.mdb");
                    if (!string.IsNullOrEmpty(mdbFilePath))
                    {
                        // Нашли файл military.mdb, обновляем путь в config.ini
                        UpdateConfigIni(mdbFilePath);
                        MessageBox.Show("Файл military.mdb найден!");

                        // Переходим на первую форму
                        var mainForm = new Form1();
                        mainForm.Show();

                        // Сворачиваем текущую форму
                        WindowState = FormWindowState.Minimized;

                        progressBar1.Visible = false; // Скрываем ProgressBar
                        return;
                    }
                }
            }

            MessageBox.Show("Файл military.mdb не найден.");
            progressBar1.Visible = false; // Скрываем ProgressBar
        }

        private string SearchFile(string directory, string fileName)
        {
            try
            {
                string[] files = Directory.GetFiles(directory, fileName, SearchOption.AllDirectories);
                if (files.Length > 0)
                {
                    return files[0];
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Нет доступа к папке, игнорируем ее
            }

            return null;
        }

        private void UpdateConfigIni(string mdbFilePath)
        {
            // Обновляем путь к базе данных в config.ini
            string section = "Database";
            string key = "Path";
            string value = mdbFilePath;
            NativeMethods.WritePrivateProfileString(section, key, value, configFilePath);
        }
    }
}