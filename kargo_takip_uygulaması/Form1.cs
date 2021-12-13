using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kargo_takip_uygulaması
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btngiris_Click(object sender, EventArgs e)
        {
            bool sifkontrol;
            int yetki;
            yetki = 1;
            sifkontrol = true;
            if (sifkontrol == true)
            {
                txtFiyat frm = new txtFiyat();
                frm.Show();
                if (yetki==1)
                {

                }
                else
                {
                    frm.Hide();
                }
                this.Hide();
            }
            else 
            {
                MessageBox.Show("Kullanıcı Adı Veya Şifre Hatalıdır ! ");
            }

            
            
  
        }
    }
}
