using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace kargo_takip_uygulaması
{
   
    public partial class txtFiyat : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-CQJ8G3J\\SQLEXPRESS;Initial Catalog=Adresler;Integrated Security=True");

        public txtFiyat()
        {
            InitializeComponent();
        }
        
        
        private void şahıs_radio_CheckedChanged(object sender, EventArgs e)
        {
           
            tüzel_radio.Checked = false;
            txtİsim.Enabled = true ;
            txtSoyad.Enabled = true;
            txtTc.Enabled = true;
            txtKurum_adı.Enabled = false;
            txtVergi_no.Enabled = false;
            txtKurum_adı.Clear();
            txtVergi_no.Clear();
            
        }

        private void tüzel_radio_CheckedChanged(object sender, EventArgs e)
        {
            şahıs_radio.Checked = false;
            txtİsim.Enabled = false;
            txtSoyad.Enabled = false;
            txtTc.Enabled = false;
            txtKurum_adı.Enabled = true;
            txtVergi_no.Enabled = true;
            txtİsim.Clear();
            txtSoyad.Clear();
            txtTc.Clear();

        }

        private void tc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void vergi_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            string tckimlik;
            int tek = 0, çift = 0;
            try
            {
              tckimlik = txtTc.Text;
                int index = 0;
                int toplam = 0;
                if (tckimlik[0].ToString()=="0")
                {
                    errorProvider1.SetError(txtTc, "İlk basamak 0 olamaz !");
                }
                else
                {
                    errorProvider1.Clear();
                    for (int i = 0 ; i < 9; i+=2)
                    {
                        tek += Convert.ToInt16(txtTc.Text[i].ToString());
                    }
                    for (int i = 1; i < 9; i += 2)
                    {
                        çift += Convert.ToInt16(txtTc.Text[i].ToString());
                    }
                    int basamak10 = ((tek * 7) - çift) % 10;
                    if (txtTc.Text[9].ToString()!=basamak10.ToString())
                    {
                        errorProvider1.SetError(txtTc, "10.basamak hatalı girdiniz");
                    }
                    else
                    {
                        errorProvider1.Clear();
                    }
                    foreach (char n in tckimlik)
                    {
                        if (index < 10)
                        {
                            toplam += Convert.ToInt16(char.ToString(n));
                        }
                        index++;
                    }
                    if (toplam%10==Convert.ToInt16(tckimlik[10].ToString()))
                    {
                        errorProvider1.Clear();
                    }
                    else
                    {
                        errorProvider1.SetError(txtTc, "11.basamak yanlış girildi !");
                    }
                }
                
            }
            catch
            {
               
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Sehir();
            txtDesi.Enabled = false;
            txtEsasKg.Enabled = false;
        }
        private void Sehir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Sehirler ", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbSehir.Items.Add(oku["SehirAdi"]);
                cmbSehir2.Items.Add(oku["SehirAdi"]);
            }
            baglanti.Close();
        }

        private void cmbSehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbİlçe.Items.Clear();
            cmbİlçe.Text = "";
            cmbMahalle.Items.Clear();
            cmbMahalle.Text = "";

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select * from Ilceler where SehirId=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", cmbSehir.SelectedIndex + 1);
            SqlDataReader oku = komut2.ExecuteReader();
            while (oku.Read())
            {
                cmbİlçe.Items.Add(oku["IlceAdi"]);
            }
            baglanti.Close();
        }

        private void cmbİlçe_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMahalle.Items.Clear();
            cmbMahalle.Text = "";

            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select * from SemtMah where ilceId=@p2", baglanti);
            komut3.Parameters.AddWithValue("@p2", cmbİlçe.SelectedIndex + 1);
            SqlDataReader oku = komut3.ExecuteReader();
            while (oku.Read())
            {
                cmbMahalle.Items.Add(oku["MahalleAdi"]);
            }
            baglanti.Close();
        }

        private void txtTc2_TextChanged(object sender, EventArgs e)
        {
            string tckimlik;
            int tek = 0, çift = 0;
            try
            {
                tckimlik = txtTc2.Text;
                int index = 0;
                int toplam = 0;
                if (tckimlik[0].ToString() == "0")
                {
                    errorProvider1.SetError(txtTc2, "İlk basamak 0 olamaz !");
                }
                else
                {
                    errorProvider1.Clear();
                    for (int i = 0; i < 9; i += 2)
                    {
                        tek += Convert.ToInt16(txtTc2.Text[i].ToString());
                    }
                    for (int i = 1; i < 9; i += 2)
                    {
                        çift += Convert.ToInt16(txtTc2.Text[i].ToString());
                    }
                    int basamak10 = ((tek * 7) - çift) % 10;
                    if (txtTc2.Text[9].ToString() != basamak10.ToString())
                    {
                        errorProvider1.SetError(txtTc2, "10.basamak hatalı girdiniz");
                    }
                    else
                    {
                        errorProvider1.Clear();
                    }
                    foreach (char n in tckimlik)
                    {
                        if (index < 10)
                        {
                            toplam += Convert.ToInt16(char.ToString(n));
                        }
                        index++;
                    }
                    if (toplam % 10 == Convert.ToInt16(tckimlik[10].ToString()))
                    {
                        errorProvider1.Clear();
                    }
                    else
                    {
                        errorProvider1.SetError(txtTc2, "11.basamak yanlış girildi!");
                    }
                }

            }
            catch
            {

            }

        }

        private void cmbSehir2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbİlçe2.Items.Clear();
            cmbİlçe2.Text = "";
            cmbMahalle2.Items.Clear();
            cmbMahalle2.Text = "";

            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("select * from Ilceler where SehirId=@p3", baglanti);
            komut4.Parameters.AddWithValue("@p3", cmbSehir2.SelectedIndex + 1);
            SqlDataReader oku = komut4.ExecuteReader();
            while (oku.Read())
            {
                cmbİlçe2.Items.Add(oku["IlceAdi"]);
            }
            baglanti.Close();
        }

        private void cmbİlçe2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMahalle2.Items.Clear();
            cmbMahalle2.Text = "";

            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("select * from SemtMah where ilceId=@p4", baglanti);
            komut5.Parameters.AddWithValue("@p4", cmbİlçe2.SelectedIndex + 1);
            SqlDataReader oku = komut5.ExecuteReader();
            while (oku.Read())
            {
                cmbMahalle2.Items.Add(oku["MahalleAdi"]);
            }
            baglanti.Close();
        }

        private void cmbGonderi_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbHat.Text = "";
            cmbAdet.Text = "";
            txt_Fiyat.Text = "";
            txtEn.Text = "";
            txtBoy.Text = "";
            txtYukseklik.Text = "";
            txtKilogram.Text = "";
            txtDesi.Text = "";
            txtEsasKg.Text ="";

            if (cmbGonderi.Text=="ZARF")
            {
                lblDesi.Visible = false;
                txtDesi.Visible = false;
                lblEn.Visible = false;
                txtEn.Visible = false;
                lblBoy.Visible = false;
                txtBoy.Visible = false;
                lblYukseklik.Visible = false;
                txtYukseklik.Visible = false;
                btnHesapla.Visible = false;
                lblKilogram.Visible = false;
                txtKilogram.Visible = false;
                lblEsasKg.Visible = false;
                txtEsasKg.Visible = false;

            }
            else
            {
                lblDesi.Visible = true;
                txtDesi.Visible = true;
                lblEn.Visible = true;
                txtEn.Visible = true;
                lblBoy.Visible = true;
                txtBoy.Visible = true;
                lblYukseklik.Visible = true;
                txtYukseklik.Visible = true;
                btnHesapla.Visible = true;
                lblKilogram.Visible = true;
                txtKilogram.Visible = true;
                lblEsasKg.Visible = true;
                txtEsasKg.Visible = true;
                
                

            }
        }

        private void cmbHat_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAdet.Text = "";
            txt_Fiyat.Text = "";
            txtEn.Text = "";
            txtBoy.Text = "";
            txtYukseklik.Text = "";
            txtKilogram.Text = "";
            txtDesi.Text = "";
            txtEsasKg.Text = "";
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            try
            {
                double en, boy, yukseklik, desi,kilogram;
                en = Convert.ToDouble(txtEn.Text);
                boy = Convert.ToDouble(txtBoy.Text);
                yukseklik = Convert.ToDouble(txtYukseklik.Text);
                desi = (en * boy * yukseklik)/3000;
                txtDesi.Text = Convert.ToString(desi);
                kilogram = Convert.ToDouble(txtKilogram.Text);
                
                if (desi>kilogram)
                {
                    txtEsasKg.Text = desi.ToString();
                }
                else
                {
                    txtEsasKg.Text = kilogram.ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("En-Boy-Yükseklik-Kilogram Giriniz");
            }
            
        }

        private void cmbAdet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGonderi.Text=="ZARF")
            {
                if (cmbAdet.Text == "1" && cmbHat.Text== "1 - 200 KM")
                {
                    txt_Fiyat.Text = "16 TL";
                }
                else if (cmbAdet.Text == "1" && cmbHat.Text == "201- 600 KM")
                {
                    txt_Fiyat.Text = "20 TL";
                }
                else if (cmbAdet.Text == "1" && cmbHat.Text == "601 - 1000 KM")
                {
                    txt_Fiyat.Text = "23 TL";
                }
                else if (cmbAdet.Text == "1" && cmbHat.Text == "1.000 KM ÜZERİ")
                {
                    txt_Fiyat.Text = "25 TL";
                }

                else if (cmbAdet.Text == "2"&& cmbHat.Text== "1 - 200 KM")
                {
                    txt_Fiyat.Text = "21.30 TL";
                }
                else if (cmbAdet.Text == "2" && cmbHat.Text =="201- 600 KM")
                {
                    txt_Fiyat.Text = "23.20 TL";
                }
                else if (cmbAdet.Text == "2" && cmbHat.Text == "601 - 1000 KM")
                {
                    txt_Fiyat.Text = "25.50 TL";
                }
                else if (cmbAdet.Text == "2" && cmbHat.Text == "1.000 KM ÜZERİ")
                {
                    txt_Fiyat.Text = "27.60 TL";
                }

                else if (cmbAdet.Text == "3" && cmbHat.Text == "1 - 200 KM")
                {
                    txt_Fiyat.Text = "25.30 TL";
                }
                else if (cmbAdet.Text == "3" && cmbHat.Text == "201- 600 KM")
                {
                    txt_Fiyat.Text = "27.50 TL";
                }
                else if (cmbAdet.Text == "3" && cmbHat.Text == "601 - 1000 KM")
                {
                    txt_Fiyat.Text = "28.65 TL";
                }
                else if (cmbAdet.Text == "3" && cmbHat.Text == "1.000 KM ÜZERİ")
                {
                    txt_Fiyat.Text = "30.60 TL";
                }

                else if (cmbAdet.Text == "4" && cmbHat.Text == "1 - 200 KM")
                {
                    txt_Fiyat.Text = "27.30 TL";
                }
                else if (cmbAdet.Text == "4" && cmbHat.Text == "201- 600 KM")
                {
                    txt_Fiyat.Text = "29.50 TL";
                }
                else if (cmbAdet.Text == "4" && cmbHat.Text == "601 - 1000 KM")
                {
                    txt_Fiyat.Text = "31.65 TL";
                }
                else if (cmbAdet.Text == "4" && cmbHat.Text == "1.000 KM ÜZERİ")
                {
                    txt_Fiyat.Text = "33.60 TL";
                }

            }
     
            if (cmbGonderi.Text=="KUTU")
            {
                try
                {
                    double esas = Convert.ToInt16(txtEsasKg.Text);
                    if (esas >= 0 && esas <= 30)
                    {
                        if (cmbAdet.Text == "1" && cmbHat.Text == "1 - 200 KM")
                        {
                            txt_Fiyat.Text = "18.98 TL";
                        }
                        else if (cmbAdet.Text == "1" && cmbHat.Text == "201 - 600 KM")
                        {
                            txt_Fiyat.Text = "21.28 TL";
                        }
                        else if (cmbAdet.Text == "1" && cmbHat.Text == "601 - 1000 KM")
                        {
                            txt_Fiyat.Text = "23.54 TL";
                        }
                        else if (cmbAdet.Text == "1" && cmbHat.Text == "1.000 KM ÜZERİ")
                        {
                            txt_Fiyat.Text = "25.62 TL";
                        }

                        else if (cmbAdet.Text == "2" && cmbHat.Text == "1 - 200 KM")
                        {
                            txt_Fiyat.Text = "26.37 TL";
                        }
                        else if (cmbAdet.Text == "2" && cmbHat.Text == "201 - 600 KM")
                        {
                            txt_Fiyat.Text = "30.10 TL";
                        }
                        else if (cmbAdet.Text == "2" && cmbHat.Text == "601 - 1000 KM")
                        {
                            txt_Fiyat.Text = "33.03 TL";
                        }
                        else if (cmbAdet.Text == "2" && cmbHat.Text == "1.000 KM ÜZERİ")
                        {
                            txt_Fiyat.Text = "37.59 TL";
                        }

                        else if (cmbAdet.Text == "3" && cmbHat.Text == "1 - 200 KM")
                        {
                            txt_Fiyat.Text = "31.17 TL";
                        }
                        else if (cmbAdet.Text == "3" && cmbHat.Text == "201 - 600 KM")
                        {
                            txt_Fiyat.Text = "36.23 TL";
                        }
                        else if (cmbAdet.Text == "3" && cmbHat.Text == "601 - 1000 KM")
                        {
                            txt_Fiyat.Text = "40.29 TL";
                        }
                        else if (cmbAdet.Text == "3" && cmbHat.Text == "1.000 KM ÜZERİ")
                        {
                            txt_Fiyat.Text = "46.59 TL";
                        }

                        else if (cmbAdet.Text == "4" && cmbHat.Text == "1 - 200 KM")
                        {
                            txt_Fiyat.Text = "37.56 TL";
                        }
                        else if (cmbAdet.Text == "4" && cmbHat.Text == "201 - 600 KM")
                        {
                            txt_Fiyat.Text = "44.56 TL";
                        }
                        else if (cmbAdet.Text == "4" && cmbHat.Text == "601 - 1000 KM")
                        {
                            txt_Fiyat.Text = "50.78 TL";
                        }
                        else if (cmbAdet.Text == "4" && cmbHat.Text == "1.000 KM ÜZERİ")
                        {
                            txt_Fiyat.Text = "59.56 TL";
                        }
                    }


                    if (esas >= 31 && esas <= 60)
                    {
                        if (cmbAdet.Text == "1" && cmbHat.Text == "1 - 200 KM")
                        {
                            txt_Fiyat.Text = "44.39 TL";
                        }
                        else if (cmbAdet.Text == "1" && cmbHat.Text == "201 - 600 KM")
                        {
                            txt_Fiyat.Text = "53.56 TL";
                        }
                        else if (cmbAdet.Text == "1" && cmbHat.Text == "601 - 1000 KM")
                        {
                            txt_Fiyat.Text = "62.68 TL";
                        }
                        else if (cmbAdet.Text == "1" && cmbHat.Text == "1.000 KM ÜZERİ")
                        {
                            txt_Fiyat.Text = "73.97 TL";
                        }

                        else if (cmbAdet.Text == "2" && cmbHat.Text == "1 - 200 KM")
                        {
                            txt_Fiyat.Text = "52.38 TL";
                        }
                        else if (cmbAdet.Text == "2" && cmbHat.Text == "201 - 600 KM")
                        {
                            txt_Fiyat.Text = "62.25 TL";
                        }
                        else if (cmbAdet.Text == "2" && cmbHat.Text == "601 - 1000 KM")
                        {
                            txt_Fiyat.Text = "74.96 TL";
                        }
                        else if (cmbAdet.Text == "2" && cmbHat.Text == "1.000 KM ÜZERİ")
                        {
                            txt_Fiyat.Text = "90.31 TL";
                        }

                        else if (cmbAdet.Text == "3" && cmbHat.Text == "1 - 200 KM")
                        {
                            txt_Fiyat.Text = "60.58 TL";
                        }
                        else if (cmbAdet.Text == "3" && cmbHat.Text == "201 - 600 KM")
                        {
                            txt_Fiyat.Text = "72.17 TL";
                        }
                        else if (cmbAdet.Text == "3" && cmbHat.Text == "601 - 1000 KM")
                        {
                            txt_Fiyat.Text = "88.18 TL";
                        }
                        else if (cmbAdet.Text == "3" && cmbHat.Text == "1.000 KM ÜZERİ")
                        {
                            txt_Fiyat.Text = "106.17 TL";
                        }

                        else if (cmbAdet.Text == "4" && cmbHat.Text == "1 - 200 KM")
                        {
                            txt_Fiyat.Text = "68.38 TL";
                        }
                        else if (cmbAdet.Text == "4" && cmbHat.Text == "201 - 600 KM")
                        {
                            txt_Fiyat.Text = "81.37 TL";
                        }
                        else if (cmbAdet.Text == "4" && cmbHat.Text == "601 - 1000 KM")
                        {
                            txt_Fiyat.Text = "100.17 TL";
                        }
                        else if (cmbAdet.Text == "4" && cmbHat.Text == "1.000 KM ÜZERİ")
                        {
                            txt_Fiyat.Text = "122.18 TL";
                        }

                    }


                    if (esas >= 61 && esas <= 100)
                    {
                        if (cmbAdet.Text == "1" && cmbHat.Text == "1 - 200 KM")
                        {
                            txt_Fiyat.Text = "76.28 TL";
                        }
                        else if (cmbAdet.Text == "1" && cmbHat.Text == "201 - 600 KM")
                        {
                            txt_Fiyat.Text = "90.15 TL";
                        }
                        else if (cmbAdet.Text == "1" && cmbHat.Text == "601 - 1000 KM")
                        {
                            txt_Fiyat.Text = "112.37 TL";
                        }
                        else if (cmbAdet.Text == "1" && cmbHat.Text == "1.000 KM ÜZERİ")
                        {
                            txt_Fiyat.Text = "136.60 TL";
                        }

                        else if (cmbAdet.Text == "2" && cmbHat.Text == "1 - 200 KM")
                        {
                            txt_Fiyat.Text = "83.02 TL";
                        }
                        else if (cmbAdet.Text == "2" && cmbHat.Text == "201 - 600 KM")
                        {
                            txt_Fiyat.Text = "99.31 TL";
                        }
                        else if (cmbAdet.Text == "2" && cmbHat.Text == "601 - 1000 KM")
                        {
                            txt_Fiyat.Text = "124.48 TL";
                        }
                        else if (cmbAdet.Text == "2" && cmbHat.Text == "1.000 KM ÜZERİ")
                        {
                            txt_Fiyat.Text = "151.66 TL";
                        }

                        else if (cmbAdet.Text == "3" && cmbHat.Text == "1 - 200 KM")
                        {
                            txt_Fiyat.Text = "90.88 TL";
                        }
                        else if (cmbAdet.Text == "3" && cmbHat.Text == "201 - 600 KM")
                        {
                            txt_Fiyat.Text = "109.28 TL";
                        }
                        else if (cmbAdet.Text == "3" && cmbHat.Text == "601 - 1000 KM")
                        {
                            txt_Fiyat.Text = "137.11 TL";
                        }
                        else if (cmbAdet.Text == "3" && cmbHat.Text == "1.000 KM ÜZERİ")
                        {
                            txt_Fiyat.Text = "167.13 TL";
                        }

                        else if (cmbAdet.Text == "4" && cmbHat.Text == "1 - 200 KM")
                        {
                            txt_Fiyat.Text = "99.12 TL";
                        }
                        else if (cmbAdet.Text == "4" && cmbHat.Text == "201 - 600 KM")
                        {
                            txt_Fiyat.Text = "119.33 TL";
                        }
                        else if (cmbAdet.Text == "4" && cmbHat.Text == "601 - 1000 KM")
                        {
                            txt_Fiyat.Text = "151.07 TL";
                        }
                        else if (cmbAdet.Text == "4" && cmbHat.Text == "1.000 KM ÜZERİ")
                        {
                            txt_Fiyat.Text = "183.40 TL";
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Esas Ağırlığı Hesaplayınız");
                }
               
            }
           
           
        }

        private void txtEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtBoy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtYukseklik_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtKilogram_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtGonder_Click(object sender, EventArgs e)
        {
            if (txt_Fiyat.Text==string.Empty)
            {
                MessageBox.Show("Fiyat Alınız");
            }
            else
            {
                MessageBox.Show("Gönderi Alınmıştır");
            }
            
        }

       
    }



    
    
}
