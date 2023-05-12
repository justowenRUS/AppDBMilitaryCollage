
namespace Sazonov_Misha_Practic
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьТаблицуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьТаблицуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.работаСТаблицейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вводЗначенийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вводДанныхПользователяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.внешнийВидToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.темнаяТемаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.голубаяТемаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem,
            this.менюToolStripMenuItem,
            this.работаСТаблицейToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьТаблицуToolStripMenuItem,
            this.удалитьТаблицуToolStripMenuItem});
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(140, 20);
            this.менюToolStripMenuItem.Text = "Операции с таблицой";
            this.менюToolStripMenuItem.Click += new System.EventHandler(this.менюToolStripMenuItem_Click);
            // 
            // создатьТаблицуToolStripMenuItem
            // 
            this.создатьТаблицуToolStripMenuItem.Name = "создатьТаблицуToolStripMenuItem";
            this.создатьТаблицуToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.создатьТаблицуToolStripMenuItem.Text = "Создать таблицу";
            this.создатьТаблицуToolStripMenuItem.Click += new System.EventHandler(this.создатьТаблицуToolStripMenuItem_Click);
            // 
            // удалитьТаблицуToolStripMenuItem
            // 
            this.удалитьТаблицуToolStripMenuItem.Name = "удалитьТаблицуToolStripMenuItem";
            this.удалитьТаблицуToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.удалитьТаблицуToolStripMenuItem.Text = "Удалить таблицу";
            this.удалитьТаблицуToolStripMenuItem.Click += new System.EventHandler(this.удалитьТаблицуToolStripMenuItem_Click);
            // 
            // работаСТаблицейToolStripMenuItem
            // 
            this.работаСТаблицейToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вводЗначенийToolStripMenuItem,
            this.вводДанныхПользователяToolStripMenuItem});
            this.работаСТаблицейToolStripMenuItem.Name = "работаСТаблицейToolStripMenuItem";
            this.работаСТаблицейToolStripMenuItem.Size = new System.Drawing.Size(121, 20);
            this.работаСТаблицейToolStripMenuItem.Text = "Работа с таблицей";
            this.работаСТаблицейToolStripMenuItem.Click += new System.EventHandler(this.работаСТаблицейToolStripMenuItem_Click);
            // 
            // вводЗначенийToolStripMenuItem
            // 
            this.вводЗначенийToolStripMenuItem.Name = "вводЗначенийToolStripMenuItem";
            this.вводЗначенийToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.вводЗначенийToolStripMenuItem.Text = "Ввод значений";
            this.вводЗначенийToolStripMenuItem.Click += new System.EventHandler(this.вводЗначенийToolStripMenuItem_Click);
            // 
            // вводДанныхПользователяToolStripMenuItem
            // 
            this.вводДанныхПользователяToolStripMenuItem.Name = "вводДанныхПользователяToolStripMenuItem";
            this.вводДанныхПользователяToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.вводДанныхПользователяToolStripMenuItem.Text = "Ввод данных пользователя";
            this.вводДанныхПользователяToolStripMenuItem.Click += new System.EventHandler(this.вводДанныхПользователяToolStripMenuItem_Click);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.внешнийВидToolStripMenuItem,
            this.оПрограммеToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // внешнийВидToolStripMenuItem
            // 
            this.внешнийВидToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.темнаяТемаToolStripMenuItem,
            this.голубаяТемаToolStripMenuItem});
            this.внешнийВидToolStripMenuItem.Name = "внешнийВидToolStripMenuItem";
            this.внешнийВидToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.внешнийВидToolStripMenuItem.Text = "Внешний вид";
            // 
            // темнаяТемаToolStripMenuItem
            // 
            this.темнаяТемаToolStripMenuItem.Name = "темнаяТемаToolStripMenuItem";
            this.темнаяТемаToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.темнаяТемаToolStripMenuItem.Text = "Темная тема";
            this.темнаяТемаToolStripMenuItem.Click += new System.EventHandler(this.темнаяТемаToolStripMenuItem_Click);
            // 
            // голубаяТемаToolStripMenuItem
            // 
            this.голубаяТемаToolStripMenuItem.Name = "голубаяТемаToolStripMenuItem";
            this.голубаяТемаToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.голубаяТемаToolStripMenuItem.Text = "Голубая тема";
            this.голубаяТемаToolStripMenuItem.Click += new System.EventHandler(this.голубаяТемаToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem менюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьТаблицуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьТаблицуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem работаСТаблицейToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вводЗначенийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вводДанныхПользователяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem внешнийВидToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem темнаяТемаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem голубаяТемаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
    }
}

