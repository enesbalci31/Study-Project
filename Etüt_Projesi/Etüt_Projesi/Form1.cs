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
using System.IO;

namespace Etüt_Projesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti=new SqlConnection(@"Data Source=DESKTOP-M8U6A74;Initial Catalog='Ogrenci Etüt';Integrated Security=True;");


        void derslistesi() 
        {
        
        SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Ders",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.ValueMember = "DERSID";
            comboBox1.DisplayMember = "DERSAD";
            comboBox1.DataSource = dt;
        
        }

        void Etutlistesi()
        {

            
            SqlDataAdapter da3 = new SqlDataAdapter("select ID,(Tbl_Ogretmen.AD +' ' + Tbl_Ogretmen.SOYAD)  as 'Öğretmen ',(Tbl_Ogrenci.AD+ ' '+ Tbl_Ogrenci.SOYAD) as 'Öğrenci' ,TARİH,SAAT,DURUM \r\n From  Tbl_Etüt inner join Tbl_Ders on Tbl_Etüt.DERSID=Tbl_Ders.DERSID inner join Tbl_Ogretmen on Tbl_Etüt.OGRTID= Tbl_Ogretmen.OGRTID\r\n  inner join Tbl_Ogrenci on Tbl_Ogrenci.OGRID=Tbl_Etüt.OGRID", baglanti);
            DataTable dt = new DataTable();
            da3.Fill(dt);
            dataGridView1.DataSource = dt;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'ogrenci_EtütDataSet.Tbl_Etüt' table. You can move, or remove it, as needed.
            this.tbl_EtütTableAdapter.Fill(this.ogrenci_EtütDataSet.Tbl_Etüt);

            Etutlistesi();   
            derslistesi();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            SqlDataAdapter da2 = new SqlDataAdapter("select * from Tbl_Ogretmen  where BRANSID ="+ comboBox1.SelectedValue,baglanti);

            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            comboBox2.ValueMember = "OGRTID";
            comboBox2.DisplayMember = "AD";
            comboBox2.DataSource = dt2;

        }
        private void button1_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Etüt (DERSID,OGRTID,TARİH,SAAT)  VALUES (@p1,@p2,@p3,@p4)",baglanti);
            komut.Parameters.AddWithValue("@p1", comboBox1.SelectedValue);
            komut.Parameters.AddWithValue("@p2",comboBox2.SelectedValue);
            komut.Parameters.AddWithValue("@p3", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p4", maskedTextBox3.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Etüt oluşturuldu","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);



            

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int secilen = dataGridView1.SelectedCells[0].RowIndex;


            textBox1.Text= dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update Tbl_Etüt set OGRID=@p1,DURUM=@p2 where ID=@p3",baglanti);

            komut.Parameters.AddWithValue("@p1",textBox2.Text);
            komut.Parameters.AddWithValue("@p2", "TRUE");
            komut.Parameters.AddWithValue("@p3", textBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenciye Etüt Verildi","Bilgi", MessageBoxButtons.OK,MessageBoxIcon.Information);







        }

        private void button5_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;


        }

        private void button4_Click(object sender, EventArgs e)
        {


            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Ogrenci  (AD,SOYAD,SINIF,FOTOGRAF,TELEFON ,MAİL) VALUES (@p1,@p2,@p3,@p4,@p5,@p6)",baglanti);

            komut.Parameters.AddWithValue("@p1",textBox4.Text);
            komut.Parameters.AddWithValue("@p2", textBox3.Text);
            komut.Parameters.AddWithValue("@p3", textBox6.Text);
            komut.Parameters.AddWithValue("@p4", pictureBox1.Text);
            komut.Parameters.AddWithValue("@p5", maskedTextBox2.Text);
            komut.Parameters.AddWithValue("@p6", textBox5.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Kaydedildi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }
    }
}
