using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace KorOptik_v1
{
    public partial class FormTanit : Form
    {
        public FormTanit()
        {
            InitializeComponent();
        }
        List<Point> tiklanankoordinat = new List<Point>();
        List<Point> koordinatlar = new List<Point>();
        List<int> satirsutun = new List<int>();
        List<string> bolumlersirasi = new List<string>();
        Point adisoyadi = new Point();
        Point okulno = new Point();
        Point sinif = new Point();
        public string sinifseviyeleri;
        Point sube = new Point();
        Point kitapcıkturuA = new Point();
        Point kitapcıkturuB = new Point();
        Point kitapcıkturuC = new Point();
        Point kitapcıkturuD = new Point();
        Point turkce = new Point();
        Point matematik = new Point();
        Point sosyal = new Point();
        Point fen = new Point();
        Point din = new Point();
        Point ingilizce = new Point();
        string formadi;
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=tanitilanformlar.accdb");
        OleDbCommand komut = new OleDbCommand();

        void baglan()
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            else
            {
                baglanti.Close();
                baglanti.Open();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog sec = new OpenFileDialog();
            sec.Filter = "Resim Dosyaları|*.jpg;*.png";
            if (sec.ShowDialog() == DialogResult.OK)
            {
                Bitmap optik = new Bitmap(sec.FileName);
                Bitmap kucukoptik = new Bitmap(optik, 595, 842);
                pictureBox1.Image = kucukoptik;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Text = "29";
                textBox1.ReadOnly = true;
                textBox2.Text = "20";
                textBox2.ReadOnly = false;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                textBox1.Text = "10";
                textBox1.ReadOnly = true;
                textBox2.Text = "4";
                textBox2.ReadOnly = false;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                textBox1.Text = "4";
                textBox1.ReadOnly = false;
                textBox2.Text = "1";
                textBox2.ReadOnly = true;
            }
            if (comboBox1.SelectedIndex == 3)
            {
                textBox1.Text = "29";
                textBox1.ReadOnly = false;
                textBox2.Text = "1";
                textBox2.ReadOnly = false;
            }
            if (comboBox1.SelectedIndex == 4)
            {
                textBox1.Text = "1";
                textBox1.ReadOnly = true;
                textBox2.Text = "1";
                textBox2.ReadOnly = true;
            }
            if (comboBox1.SelectedIndex == 5)
            {
                textBox1.Text = "1";
                textBox1.ReadOnly = true;
                textBox2.Text = "1";
                textBox2.ReadOnly = true;
            }
            if (comboBox1.SelectedIndex == 6)
            {
                textBox1.Text = "1";
                textBox1.ReadOnly = true;
                textBox2.Text = "1";
                textBox2.ReadOnly = true;
            }
            if (comboBox1.SelectedIndex == 7)
            {
                textBox1.Text = "1";
                textBox1.ReadOnly = true;
                textBox2.Text = "1";
                textBox2.ReadOnly = true;
            }
            if (comboBox1.SelectedIndex == 8)
            {
                textBox1.Text = "20";
                textBox1.ReadOnly = false;
                textBox2.Text = "4";
                textBox2.ReadOnly = false;
            }
            if (comboBox1.SelectedIndex == 9)
            {
                textBox1.Text = "20";
                textBox1.ReadOnly = false;
                textBox2.Text = "4";
                textBox2.ReadOnly = false;
            }
            if (comboBox1.SelectedIndex == 10)
            {
                textBox1.Text = "20";
                textBox1.ReadOnly = false;
                textBox2.Text = "4";
                textBox2.ReadOnly = false;
            }
            if (comboBox1.SelectedIndex == 11)
            {
                textBox1.Text = "20";
                textBox1.ReadOnly = false;
                textBox2.Text = "4";
                textBox2.ReadOnly = false;
            }
            if (comboBox1.SelectedIndex == 12)
            {
                textBox1.Text = "20";
                textBox1.ReadOnly = false;
                textBox2.Text = "4";
                textBox2.ReadOnly = false;
            }
            if (comboBox1.SelectedIndex == 13)
            {
                textBox1.Text = "20";
                textBox1.ReadOnly = false;
                textBox2.Text = "4";
                textBox2.ReadOnly = false;
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen form seçiniz");
            }
            else
            {
                if (comboBox1.SelectedIndex < 0)
                {
                    MessageBox.Show("Lütfen bölüm seçiniz");
                }
                else
                {
                    tiklanankoordinat.Add(pictureBox1.PointToClient(Cursor.Position));
                    int satir = Convert.ToInt32(textBox1.Text);
                    int sutun = Convert.ToInt32(textBox2.Text);
                    int x = tiklanankoordinat[tiklanankoordinat.Count - 1].X - 12;
                    int y = tiklanankoordinat[tiklanankoordinat.Count - 1].Y - 12;
                    for (int i = 0; i < sutun; i++)
                    {
                        x += 12;
                        y = tiklanankoordinat[tiklanankoordinat.Count - 1].Y - 12;

                        for (int j = 0; j < satir; j++)
                        {
                            y += 12;
                            koordinatlar.Add(new Point(x, y));
                        }
                    }
                    satirsutun.Add(Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox2.Text));
                    if (comboBox1.SelectedIndex == 0)
                    {
                        adisoyadi = tiklanankoordinat[tiklanankoordinat.Count - 1];
                        bolumlersirasi.Add("adisoyadi");
                    }
                    if (comboBox1.SelectedIndex == 1)
                    {
                        okulno = tiklanankoordinat[tiklanankoordinat.Count - 1];
                        bolumlersirasi.Add("okulno");
                    }
                    if (comboBox1.SelectedIndex == 2)
                    {
                        sinif = tiklanankoordinat[tiklanankoordinat.Count - 1];
                        bolumlersirasi.Add("sinif");
                    }
                    if (comboBox1.SelectedIndex == 3)
                    {
                        sube = tiklanankoordinat[tiklanankoordinat.Count - 1];
                        bolumlersirasi.Add("sube");
                    }
                    if (comboBox1.SelectedIndex == 4)
                    {
                        kitapcıkturuA = tiklanankoordinat[tiklanankoordinat.Count - 1];
                        bolumlersirasi.Add("kitapcıkturuA");
                    }
                    if (comboBox1.SelectedIndex == 5)
                    {
                        kitapcıkturuB = tiklanankoordinat[tiklanankoordinat.Count - 1];
                        bolumlersirasi.Add("kitapcıkturuB");
                    }
                    if (comboBox1.SelectedIndex == 6)
                    {
                        kitapcıkturuC = tiklanankoordinat[tiklanankoordinat.Count - 1];
                        bolumlersirasi.Add("kitapcıkturuC");
                    }
                    if (comboBox1.SelectedIndex == 7)
                    {
                        kitapcıkturuD = tiklanankoordinat[tiklanankoordinat.Count - 1];
                        bolumlersirasi.Add("kitapcıkturuD");
                    }
                    if (comboBox1.SelectedIndex == 8)
                    {
                        turkce = tiklanankoordinat[tiklanankoordinat.Count - 1];
                        bolumlersirasi.Add("turkce");
                    }
                    if (comboBox1.SelectedIndex == 9)
                    {
                        matematik = tiklanankoordinat[tiklanankoordinat.Count - 1];
                        bolumlersirasi.Add("matematik");
                    }
                    if (comboBox1.SelectedIndex == 10)
                    {
                        sosyal = tiklanankoordinat[tiklanankoordinat.Count - 1];
                        bolumlersirasi.Add("sosyal");
                    }
                    if (comboBox1.SelectedIndex == 11)
                    {
                        fen = tiklanankoordinat[tiklanankoordinat.Count - 1];
                        bolumlersirasi.Add("fen");
                    }
                    if (comboBox1.SelectedIndex == 12)
                    {
                        din = tiklanankoordinat[tiklanankoordinat.Count - 1];
                        bolumlersirasi.Add("din");
                    }
                    if (comboBox1.SelectedIndex == 13)
                    {
                        ingilizce = tiklanankoordinat[tiklanankoordinat.Count - 1];
                        bolumlersirasi.Add("ingilizce");
                    }
                    pictureBox1.Invalidate();
                    if (comboBox1.SelectedIndex == 2)
                    {
                        SinifSeviyeleri form2 = new SinifSeviyeleri();
                        form2.sinifsatirsayisi = Convert.ToInt32(textBox1.Text);
                        form2.ShowDialog();                   
                    }
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (tiklanankoordinat.Count > 0)
            {
                Pen kalem = new Pen(Color.Red, 1);
                if (tiklanankoordinat[tiklanankoordinat.Count - 1].X != 0)
                {
                    for (int i = 0; i < koordinatlar.Count; i++)
                    {
                        e.Graphics.DrawRectangle(kalem, koordinatlar[i].X, koordinatlar[i].Y, 10, 10);
                    }
                }
            }
        }

        private void FormTanit_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM tanitilanformlar", baglanti);
            DataTable tablo = new DataTable();
            adapter.Fill(tablo);
            for (int i = 0; i < tablo.Rows.Count; i++)
            {
                comboBox2.Items.Add(tablo.Rows[i][30]);
            }
            adapter.Dispose();
            tablo.Dispose();
            textBox1.Text = "0";
            textBox2.Text = "0";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tiklanankoordinat.RemoveAll(a => a != null);
            koordinatlar.RemoveAll(a => a != null);
            tiklanankoordinat.RemoveAll(a => a != null);
            bolumlersirasi.RemoveAll(a => a != null);
            adisoyadi = new Point(0, 0);
            okulno = new Point(0, 0);
            sinif = new Point(0, 0);
            sube = new Point(0, 0);
            kitapcıkturuA = new Point(0, 0);
            kitapcıkturuB = new Point(0, 0);
            kitapcıkturuC = new Point(0, 0);
            kitapcıkturuD = new Point(0, 0);
            turkce = new Point(0, 0);
            matematik = new Point(0, 0);
            sosyal = new Point(0, 0);
            fen = new Point(0, 0);
            din = new Point(0, 0);
            ingilizce = new Point(0, 0);
            pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tiklanankoordinat.Count > 0)
            {
                koordinatlar.RemoveRange(koordinatlar.Count - satirsutun[satirsutun.Count - 1], satirsutun[satirsutun.Count - 1]);
                tiklanankoordinat.RemoveAt(tiklanankoordinat.Count - 1);
                satirsutun.RemoveAt(satirsutun.Count - 1);
                bolumlersirasi.RemoveAt(bolumlersirasi.Count - 1);
                if (bolumlersirasi.Count > 0)
                {
                    if (bolumlersirasi[bolumlersirasi.Count - 1] == "adisoyadi")
                    {
                        adisoyadi = tiklanankoordinat[tiklanankoordinat.Count - 1];
                    }
                    else if (bolumlersirasi.Contains("adisoyadi"))
                    {
                        int index = bolumlersirasi.LastIndexOf("adisoyadi");
                        adisoyadi = tiklanankoordinat[index];
                    }
                    else
                    {
                        adisoyadi = new Point(0, 0);
                    }

                    if (bolumlersirasi[bolumlersirasi.Count - 1] == "okulno")
                    {
                        okulno = tiklanankoordinat[tiklanankoordinat.Count - 1];
                    }
                    else if (bolumlersirasi.Contains("okulno"))
                    {
                        int index = bolumlersirasi.LastIndexOf("okulno");
                        okulno = tiklanankoordinat[index];
                    }
                    else
                    {
                        okulno = new Point(0, 0);
                    }

                    if (bolumlersirasi[bolumlersirasi.Count - 1] == "sinif")
                    {
                        sinif = tiklanankoordinat[tiklanankoordinat.Count - 1];
                    }
                    else if (bolumlersirasi.Contains("sinif"))
                    {
                        int index = bolumlersirasi.LastIndexOf("sinif");
                        sinif = tiklanankoordinat[index];
                    }
                    else
                    {
                        sinif = new Point(0, 0);
                    }

                    if (bolumlersirasi[bolumlersirasi.Count - 1] == "sube")
                    {
                        sube = tiklanankoordinat[tiklanankoordinat.Count - 1];
                    }
                    else if (bolumlersirasi.Contains("sube"))
                    {
                        int index = bolumlersirasi.LastIndexOf("sube");
                        sube = tiklanankoordinat[index];
                    }
                    else
                    {
                        sube = new Point(0, 0);
                    }

                    if (bolumlersirasi[bolumlersirasi.Count - 1] == "kitapcıkturuA")
                    {
                        kitapcıkturuA = tiklanankoordinat[tiklanankoordinat.Count - 1];
                    }
                    else if (bolumlersirasi.Contains("kitapcıkturuA"))
                    {
                        int index = bolumlersirasi.LastIndexOf("kitapcıkturuA");
                        kitapcıkturuA = tiklanankoordinat[index];
                    }
                    else
                    {
                        kitapcıkturuA = new Point(0, 0);
                    }

                    if (bolumlersirasi[bolumlersirasi.Count - 1] == "kitapcıkturuB")
                    {
                        kitapcıkturuB = tiklanankoordinat[tiklanankoordinat.Count - 1];
                    }
                    else if (bolumlersirasi.Contains("kitapcıkturuB"))
                    {
                        int index = bolumlersirasi.LastIndexOf("kitapcıkturuB");
                        kitapcıkturuB = tiklanankoordinat[index];
                    }
                    else
                    {
                        kitapcıkturuB = new Point(0, 0);
                    }

                    if (bolumlersirasi[bolumlersirasi.Count - 1] == "kitapcıkturuC")
                    {
                        kitapcıkturuC = tiklanankoordinat[tiklanankoordinat.Count - 1];
                    }
                    else if (bolumlersirasi.Contains("kitapcıkturuC"))
                    {
                        int index = bolumlersirasi.LastIndexOf("kitapcıkturuC");
                        kitapcıkturuC = tiklanankoordinat[index];
                    }
                    else
                    {
                        kitapcıkturuC = new Point(0, 0);
                    }

                    if (bolumlersirasi[bolumlersirasi.Count - 1] == "kitapcıkturuD")
                    {
                        kitapcıkturuD = tiklanankoordinat[tiklanankoordinat.Count - 1];
                    }
                    else if (bolumlersirasi.Contains("kitapcıkturuD"))
                    {
                        int index = bolumlersirasi.LastIndexOf("kitapcıkturuD");
                        kitapcıkturuD = tiklanankoordinat[index];
                    }
                    else
                    {
                        kitapcıkturuD = new Point(0, 0);
                    }

                    if (bolumlersirasi[bolumlersirasi.Count - 1] == "turkce")
                    {
                        turkce = tiklanankoordinat[tiklanankoordinat.Count - 1];
                    }
                    else if (bolumlersirasi.Contains("turkce"))
                    {
                        int index = bolumlersirasi.LastIndexOf("turkce");
                        turkce = tiklanankoordinat[index];
                    }
                    else
                    {
                        turkce = new Point(0, 0);
                    }

                    if (bolumlersirasi[bolumlersirasi.Count - 1] == "matematik")
                    {
                        matematik = tiklanankoordinat[tiklanankoordinat.Count - 1];
                    }
                    else if (bolumlersirasi.Contains("matematik"))
                    {
                        int index = bolumlersirasi.LastIndexOf("matematik");
                        matematik = tiklanankoordinat[index];
                    }
                    else
                    {
                        matematik = new Point(0, 0);
                    }

                    if (bolumlersirasi[bolumlersirasi.Count - 1] == "sosyal")
                    {
                        sosyal = tiklanankoordinat[tiklanankoordinat.Count - 1];
                    }
                    else if (bolumlersirasi.Contains("sosyal"))
                    {
                        int index = bolumlersirasi.LastIndexOf("sosyal");
                        sosyal = tiklanankoordinat[index];
                    }
                    else
                    {
                        sosyal = new Point(0, 0);
                    }

                    if (bolumlersirasi[bolumlersirasi.Count - 1] == "fen")
                    {
                        fen = tiklanankoordinat[tiklanankoordinat.Count - 1];
                    }
                    else if (bolumlersirasi.Contains("fen"))
                    {
                        int index = bolumlersirasi.LastIndexOf("fen");
                        fen = tiklanankoordinat[index];
                    }
                    else
                    {
                        fen = new Point(0, 0);
                    }

                    if (bolumlersirasi[bolumlersirasi.Count - 1] == "din")
                    {
                        din = tiklanankoordinat[tiklanankoordinat.Count - 1];
                    }
                    else if (bolumlersirasi.Contains("din"))
                    {
                        int index = bolumlersirasi.LastIndexOf("din");
                        din = tiklanankoordinat[index];
                    }
                    else
                    {
                        din = new Point(0, 0);
                    }

                    if (bolumlersirasi[bolumlersirasi.Count - 1] == "ingilizce")
                    {
                        ingilizce = tiklanankoordinat[tiklanankoordinat.Count - 1];
                    }
                    else if (bolumlersirasi.Contains("ingilizce"))
                    {
                        int index = bolumlersirasi.LastIndexOf("ingilizce");
                        ingilizce = tiklanankoordinat[index];
                    }
                    else
                    {
                        ingilizce = new Point(0, 0);
                    }
                }
            }
            if (tiklanankoordinat.Count == 0)
            {
                adisoyadi = new Point(0, 0);
                okulno = new Point(0, 0);
                sinif = new Point(0, 0);
                sube = new Point(0, 0);
                kitapcıkturuA = new Point(0, 0);
                kitapcıkturuB = new Point(0, 0);
                kitapcıkturuC = new Point(0, 0);
                kitapcıkturuD = new Point(0, 0);
                turkce = new Point(0, 0);
                matematik = new Point(0, 0);
                sosyal = new Point(0, 0);
                fen = new Point(0, 0);
                din = new Point(0, 0);
                ingilizce = new Point(0, 0);
            }
            pictureBox1.Invalidate();



        }

        private void button4_Click(object sender, EventArgs e)
        {
            formadi = textBox3.Text;
            baglan();
            komut.Connection = baglanti;
            komut.CommandText = "INSERT INTO tanitilanformlar (adisoyadiX,adisoyadiY,okulnoX,okulnoY,sinifiX,sinifiY,sinifseviyeleri,subeX,subeY,kitapcikturuAX,kitapcikturuAY,kitapcikturuBX,kitapcikturuBY,kitapcikturuCX,kitapcikturuCY,kitapcikturuDX,kitapcikturuDY,turkceX,turkceY,matematikX,matematikY,sosyalX,sosyalY,fenX,fenY,dinX,dinY,ingilizceX,ingilizceY,formadi) VALUES ('" + adisoyadi.X + "','" + adisoyadi.Y + "','" + okulno.X + "','" + okulno.Y + "','" + sinif.X + "','" + sinif.Y + "','" + sinifseviyeleri + "','" + sube.X + "','" + sube.Y + "','" + kitapcıkturuA.X + "','" + kitapcıkturuA.Y + "','" + kitapcıkturuB.X + "','" + kitapcıkturuB.Y + "','" + kitapcıkturuC.X + "','" + kitapcıkturuC.Y + "','" + kitapcıkturuD.X + "','" + kitapcıkturuD.Y + "','" + turkce.X + "','" + turkce.Y + "','" + matematik.X + "','" + matematik.Y + "','" + sosyal.X + "','" + sosyal.Y + "','" + fen.X + "','" + fen.Y + "','" + din.X + "','" + din.Y + "','" + ingilizce.X + "','" + ingilizce.Y + "','" + formadi + "')";
            komut.ExecuteNonQuery();
            komut.Dispose();
            baglanti.Close();
            comboBox2.Items.Add(textBox3.Text);
            MessageBox.Show("Form başarıyla kaydedildi");
        } 

        private void button5_Click(object sender, EventArgs e)
        {
            baglan();
            komut.Connection = baglanti;
            komut.CommandText = "DELETE FROM tanitilanformlar WHERE formadi='" + comboBox2.SelectedItem + "'";
            komut.ExecuteNonQuery();
            comboBox2.Items.Remove(comboBox2.SelectedItem);
            MessageBox.Show("Kayıtlı form başarıyla silindi");
            komut.Dispose();
            baglanti.Close();
        }

    
    }
}
