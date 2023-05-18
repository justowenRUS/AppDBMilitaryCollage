using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sazonov_Misha_Practic
{
    public partial class RoundedForm : Form
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        private static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HT_CAPTION;
                return;
            }

            base.WndProc(ref m);
        }

        public RoundedForm()
        {
            InitializeComponent();
            InitializeForm();
            InitializeControls();
        }

        private void InitializeForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            Font = SystemFonts.MessageBoxFont;
            ClientSize = new Size(400, 300); // Установка размеров формы
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            Hide();
            frm1.ShowDialog();
            Close();
        }

        private void InitializeControls()
        {
            var titleLabel = new Label();
            titleLabel.Text = "Информация о программе";
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(20, 20);
            titleLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            Controls.Add(titleLabel);

            var descriptionLabel = new Label();
            descriptionLabel.Text = "Программа для работы с базой данных.\nУпрощает работу без использования Excel.\nАвтоматизированная система работы с БД.";
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(20, 70);
            Controls.Add(descriptionLabel);

            var versionLabel = new Label();
            versionLabel.Text = $"Версия: {Application.ProductVersion}";
            versionLabel.AutoSize = true;
            versionLabel.Location = new Point(20, 130);
            Controls.Add(versionLabel);

            var designedLabel = new Label();
            designedLabel.Text = "Designed by Сазонов Михаил, 2023";
            designedLabel.AutoSize = true;
            designedLabel.Location = new Point(20, 160);
            Controls.Add(designedLabel);

            var authorLabel = new Label();
            authorLabel.Text = "© Сазонов Михаил";
            authorLabel.AutoSize = true;
            authorLabel.Location = new Point(20, 190);
            Controls.Add(authorLabel);

            var closeButton = new Button();
            closeButton.Text = "Закрыть";
            closeButton.Size = new Size(100, 30);
            closeButton.Location = new Point((Width - closeButton.Width) / 2, 230);
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.BackColor = SystemColors.ControlLight;
            closeButton.Font = SystemFonts.MessageBoxFont;
            closeButton.Click += closeButton_Click;
            Controls.Add(closeButton);
        }
    }
}