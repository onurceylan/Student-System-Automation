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

namespace BonusProje1
{
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci() // ödev:cell click de öğrencinin cinsiyeti neyse o radibut seçili olsun
        {
            InitializeComponent();
        }

        
        SqlConnection baglanti = new SqlConnection(@"Data Source=ONURPC\MSSQLSERVER01;Initial Catalog=BonusOkul;Integrated Security=True;");

        DataSet1TableAdapters.TBLOGRENCILERTableAdapter ds = new DataSet1TableAdapters.TBLOGRENCILERTableAdapter();
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from TBLKULUPLER",baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut); // dataadapter da baglantıyı aç-kapa yapmasan da oluyor
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "KULUPAD"; // dsiplay -- gözüken bu olsun
            comboBox1.ValueMember = "KULUPID";  // arka plandakı değeri ıd olacak
            comboBox1.DataSource = dt;
            baglanti.Close();

        }
        string c = "";

        private void BtnEkle_Click(object sender, EventArgs e)
        {
           

            ds.OgrenciEkle(TxtAd.Text, TxtSoyad.Text, byte.Parse(comboBox1.SelectedValue.ToString()), c);
            MessageBox.Show("Ogrenci Eklendi");
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtID.Text = comboBox1.SelectedValue.ToString();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            ds.OgrenciSil(int.Parse(TxtID.Text));
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            //TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            //TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            


        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            ds.OgrenciGuncelle(TxtAd.Text,TxtSoyad.Text,byte.Parse(comboBox1.SelectedValue.ToString()),c,int.Parse(TxtID.Text));
            //veriler string olarak çekilir sonra istenilen değere dönüştürülür. 
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                c = "KIZ";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                c = "ERKEK";
            }
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciGetir(TxtAra.Text);
        }
    }
}
