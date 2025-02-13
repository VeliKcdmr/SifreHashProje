using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SifreHashProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=RAMPAGE\\SQLEXPRESS;Initial Catalog=DbSifreleme;Integrated Security=True");
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            string Ad = TxtAd.Text;
            string Soyad = TxtSoyad.Text;
            string mail = TxtMail.Text;
            string Sifre = TxtSifre.Text;
            string hesapNo = TxtHesap.Text;

            byte[] veri = ASCIIEncoding.ASCII.GetBytes(Ad);
            byte[] hashVeri = new System.Security.Cryptography.SHA256Managed().ComputeHash(veri);
            string hashliAd = Convert.ToBase64String(hashVeri);

            byte[] veri2 = ASCIIEncoding.ASCII.GetBytes(Soyad);
            byte[] hashVeri2 = new System.Security.Cryptography.SHA256Managed().ComputeHash(veri2);
            string hashliSoyadAd = Convert.ToBase64String(hashVeri2);

            byte[] veri3 = ASCIIEncoding.ASCII.GetBytes(mail);
            byte[] hashVeri3 = new System.Security.Cryptography.SHA256Managed().ComputeHash(veri3);
            string hashliMail = Convert.ToBase64String(hashVeri3);

            byte[] veri4 = ASCIIEncoding.ASCII.GetBytes(Sifre);
            byte[] hashVeri4 = new System.Security.Cryptography.SHA256Managed().ComputeHash(veri4);
            string hashliSifre = Convert.ToBase64String(hashVeri4);

            byte[] veri5 = ASCIIEncoding.ASCII.GetBytes(hesapNo);
            byte[] hashVeri5 = new System.Security.Cryptography.SHA256Managed().ComputeHash(veri5);
            string hashliHesapNo = Convert.ToBase64String(hashVeri5);

            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TblVeriler (Ad,Soyad,Mail,Sifre,HesapNo) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
                komut.Parameters.AddWithValue("@p1", hashliAd);
                komut.Parameters.AddWithValue("@p2", hashliSoyadAd);
                komut.Parameters.AddWithValue("@p3", hashliMail);
                komut.Parameters.AddWithValue("@p4", hashliSifre);
                komut.Parameters.AddWithValue("@p5", hashliHesapNo);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Başarılı Bir Şekilde Gerçekleşti");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from TblVeriler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
    }
}
