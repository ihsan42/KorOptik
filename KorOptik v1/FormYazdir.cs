using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace KorOptik_v1
{
    public partial class FormYazdir : Form
    {
        public FormYazdir()
        {
            InitializeComponent();
        }

        private void FormYazdir_Load(object sender, EventArgs e)
        {
            Veritabani vt = new Veritabani();
            vt.baglan();
            List<String> kayitliFormAdlari = new List<string>();
            kayitliFormAdlari = vt.kayitliFormlarınIsimleriniGetir();
            if (kayitliFormAdlari.Count != 0)
            {
                for (int i = 0; i < kayitliFormAdlari.Count; i++)
                {
                    comboBoxFormAdlari.Items.Add(kayitliFormAdlari[i]);
                }
            }
        }

        private void comboBoxFormAdlari_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dosyadizini = "KaydedilenFormlar/" + comboBoxFormAdlari.SelectedItem.ToString() + ".jpg";
            if (File.Exists(dosyadizini) == true)
            {
                pictureBox1.Image = Image.FromFile(dosyadizini);
            }
            else
            {
                MessageBox.Show("Dosya silinmiş veya yeri değiştirilmiş!");
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                e.Graphics.DrawImage(pictureBox1.Image, 0, 0, 827, 1169);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                PrintDialog yazıcıayar = new PrintDialog();
                yazıcıayar.Document = printDocument1;

                if (yazıcıayar.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else
            {
                MessageBox.Show("Görüntülenen form yok!");
            }            
        }
    }
}
