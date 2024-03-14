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

namespace Pastahane_Ürün_Maliyetlendirme_Sistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=monster;Initial Catalog=Maliyet;Integrated Security=True");
        void malzemelistele()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_Malzemeler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        void urunlistele()
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select * from Tbl_Urunler", baglanti);
            SqlDataAdapter da2 = new SqlDataAdapter(komut2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
            baglanti.Close();
        }
        void kasalistele()
        {
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select * from Tbl_Kasa", baglanti);
            SqlDataAdapter da3 = new SqlDataAdapter(komut3);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            dataGridView1.DataSource = dt3;
            baglanti.Close();
        }
        void comboboxurunler()
        {   //Combobox'a Ürünleri Alma(value-display kullanmadan yapabiliyorsun lakin bu projeden ilerde sıkıntı cıkıyor)
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_Urunler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmb_Urun.ValueMember = "URUNID";
            cmb_Urun.DisplayMember = "AD";
            cmb_Urun.DataSource = dt;
            baglanti.Close();
        }
        void comboboxmalzemeler()
        { //Combobox'a Malzemeleri Alma(value-display kullanmadan yapabiliyorsun lakin bu projeden ilerde sıkıntı cıkıyor)
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_Malzemeler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmb_Malzeme.ValueMember = "MALZEMEID";
            cmb_Malzeme.DisplayMember = "AD";
            cmb_Malzeme.DataSource = dt;
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            malzemelistele();

            comboboxurunler();

            comboboxmalzemeler();
        }

        private void btn_Urun_Listesi_Click(object sender, EventArgs e)
        {
            urunlistele();
        }

        private void btn_Kasa_Click(object sender, EventArgs e)
        {
            kasalistele();
        }

        private void btn_Malzeme_Listesi_Click(object sender, EventArgs e)
        {
            malzemelistele();
        }

        private void btn_Cikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Malzeme_Ekle_Click(object sender, EventArgs e)
        {   //Malzeme Ekleme(un vs.)
            try
            {
               
                if (string.IsNullOrWhiteSpace(txt_Malzeme_Ad.Text) || string.IsNullOrWhiteSpace(txt_Malzeme_Stok.Text) || string.IsNullOrWhiteSpace(txt_Malzeme_Fiyat.Text) )
                {
                    MessageBox.Show("Lütfen Tüm bilgileri Doldurun" , "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
               else
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Malzemeler (AD,STOK,FIYAT,NOTLAR) values (@p1,@p2,@p3,@p4)  ", baglanti);
                    komut.Parameters.AddWithValue("@p1", txt_Malzeme_Ad.Text);
                    komut.Parameters.AddWithValue("@p2", decimal.Parse(txt_Malzeme_Stok.Text));
                    komut.Parameters.AddWithValue("@p3", decimal.Parse(txt_Malzeme_Fiyat.Text));
                    komut.Parameters.AddWithValue("@p4", txt_Malzeme_Notlar.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Ürün Başarıyla Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürün eklenemedi"+ex.Message, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           finally
            {
                baglanti.Close();
            }
            malzemelistele();
        }

        private void btn_Urun_Ekle_Click(object sender, EventArgs e)
        {//Ürün Ekleme(simit vs.)
            try
            {

                if (string.IsNullOrWhiteSpace(txt_Urun_Ad.Text) || string.IsNullOrWhiteSpace(txt_Urun_Stok.Text) || string.IsNullOrWhiteSpace(txt_Urun_Maliyet_Fiyat.Text) || string.IsNullOrWhiteSpace(txt_Urun_Satis_Fiyat.Text))
                {
                    MessageBox.Show("Lütfen Tüm bilgileri Doldurun", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand(" INSERT INTO Tbl_Urunler(AD, MFIYAT, SFIYAT, STOK) values(@p1, @p2, @p3, @p4)", baglanti);
                    komut.Parameters.AddWithValue("@p1", txt_Urun_Ad.Text);
                    komut.Parameters.AddWithValue("@p2", decimal.Parse(txt_Urun_Maliyet_Fiyat.Text));
                    komut.Parameters.AddWithValue("@p3", decimal.Parse(txt_Urun_Satis_Fiyat.Text));
                    komut.Parameters.AddWithValue("@p4", txt_Urun_Stok.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Malzeme Başarıyla Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mazleme eklenemedi" + ex.Message, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                baglanti.Close();
            }
           
        }

        private void btn_Urunn_Ekle_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(cmb_Malzeme.Text) || string.IsNullOrWhiteSpace(cmb_Urun.Text) || string.IsNullOrWhiteSpace(txt_Miktar.Text) || string.IsNullOrWhiteSpace(txt_Maliyet.Text))
                {
                    MessageBox.Show("Lütfen Tüm bilgileri Doldurun", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {


                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Fırın (URUNID,MALZEMEID,MIKTAR,MALIYET) VALUES (@p1,@p2,@p3,@p4)", baglanti);
                    komut.Parameters.AddWithValue("@p1", cmb_Urun.SelectedValue);
                    komut.Parameters.AddWithValue("@p2", cmb_Malzeme.SelectedValue);
                    komut.Parameters.AddWithValue("@p3", Decimal.Parse(txt_Miktar.Text));
                    komut.Parameters.AddWithValue("@p4", Decimal.Parse(txt_Maliyet.Text));
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Ürün Başarıyla Oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    listBox1.Items.Add(cmb_Malzeme.Text + " - " + txt_Maliyet.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürün Oluşturulamadı" + ex.Message, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                baglanti.Close();
            }
            
            
           
           
            
        }

        private void txt_Miktar_TextChanged(object sender, EventArgs e)
        {
            //Ürün Oluştur kısımında malzeme seçildikden sonra mıktar kısmına sayı girilince maliyeti verme işlemi
            if(txt_Miktar.Text=="")
            {
                txt_Miktar.Text = "0";
            }
            double maliyet;
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from Tbl_Malzemeler where MALZEMEID=@p1", baglanti);//malzemeıd'ye gore maliye cıkacak
                komut.Parameters.AddWithValue("@p1", cmb_Malzeme.SelectedValue);
                SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                txt_Maliyet.Text = dr[3].ToString();//dr[3]-->fiyat
            }

            maliyet = Convert.ToDouble(txt_Maliyet.Text) / 1000 * Convert.ToDouble(txt_Miktar.Text);
            //maliyet=kiloyu 1000'e böl*miktarla çarp  -->1000e bolduk cunku gramlarla işlem yapmak istiyoruz
            txt_Maliyet.Text = maliyet.ToString();
           // sonra txtmaliyete sonucu yaz
            baglanti.Close();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            baglanti.Open();
            txt_Urun_Id.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_Urun_Ad.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            SqlCommand komut = new SqlCommand("select sum(MALIYET) from Tbl_Fırın where URUNID=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", txt_Urun_Id.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                txt_Urun_Maliyet_Fiyat.Text = dr[0].ToString();
            }
            baglanti.Close();

        }
    }
}
