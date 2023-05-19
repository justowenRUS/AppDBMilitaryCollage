using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sazonov_Misha_Practic
{
    public partial class SvuyazatTablitsi : Form
    {
        public SvuyazatTablitsi()
        {
            InitializeComponent();
            InitializeForm();
        }
        private void InitializeForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(44, 62, 80); 
        }
    }
}
