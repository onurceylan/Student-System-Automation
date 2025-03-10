﻿using System;
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
    public partial class FrmKulup : Form
    {
        public FrmKulup()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=ONURPC\MSSQLSERVER01;Initial Catalog=BonusOkul;Integrated Security=True;");

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tblkulupler ", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void FrmKulup_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            // burada mesela try-catch kullanmak lazım textbox daki karakter sayısı sql deki karaktern sınırını geçmesin diye
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLKULUPLER (KULUPAD) VALUES (@P1)",baglanti);
            komut.Parameters.AddWithValue("@p1", TxtKulupAd.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kulüp Listeye Eklendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.LightBlue;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.Transparent;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // SelectedCells [secilen] yerine Rows[e.RowIndex] e kullanıyoruz
            TxtKulupID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtKulupAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete from tblkulupler where kulupıd = @p1", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtKulupID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kulüp silme işlemi gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update tblkulupler set kulupad=@p1 where kulupıd = @p2", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtKulupAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtKulupID.Text);
            komut.ExecuteNonQuery();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kulüp Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }
    }
}
