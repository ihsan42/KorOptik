using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KorOptik_v1
{
    public partial class FormTasarla : Form
    {
        public FormTasarla()
        {
            InitializeComponent();
        }

        Graphics g;
        Bitmap bosForm;
       // Bitmap temizForm;
        int dersSayisi;
        List<Point> dersBaslangicNoktalari;
        //int genislik = 2480;
        //int yukseklik = 3508;
        int cevapSikkiEbat = 36;
        int cevapSikkiBosluk = 7;
        Point dersAlaniBaslangic = new Point(150, 1712);
        Point adSoyadBaslangic = new Point(150, 250);
        Point ogrenciNoBaslangic = new Point(1053, 250);
        Point sinifBaslangic = new Point(1569, 250);
        Point subeBaslangic = new Point(412, 250);
        Point okulKoduBaslangic = new Point(1268, 250);
        Point kitapcıkTurBaslangic = new Point(1698, 250);
        Point baslikBaslangic = new Point(1000, 150);

        int cemberKalinligi = 3;
        //float fontSizeBaslik = 50f;
        float fontSizeSik = 20f;
        float fontSizeSiraNo = 24f;
        float fontSizeDersadi = 24f;

        private void buttonEkle_Click(object sender, EventArgs e)
        {
            if (textBoxDersAdi.Text.Equals(""))
            {
                MessageBox.Show("Ders adı boş bırakılamaz!");
            }
            else if (textBoxSoruSayisi.Text.Equals(""))
            {
                MessageBox.Show("Soru sayısı boş bıralılamaz!");
            }
            else
            {
                String dersadi= textBoxDersAdi.Text;
                ListViewItem item = new ListViewItem();
                item.Text = dersadi;
                item.SubItems.Add(textBoxSoruSayisi.Text);

                Boolean durumm = false;
                for(int i = 0; i < listViewEklenenAlanlar.Items.Count; i++)
                {
                    if (dersadi.Equals(listViewEklenenAlanlar.Items[i].SubItems[0].Text))
                    {
                        durumm = true;
                    }
                }

                if (durumm == false)
                {
                    if (listViewEklenenAlanlar.Items.Count < 10)
                    {
                        listViewEklenenAlanlar.Items.Add(item);
                        comboBox_TasınacakAlanlar.Items.Add(textBoxDersAdi.Text);
                        textBoxDersAdi.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("En fazla 10 adet ders ekleyebilirsiniz!");
                    }
                }
                else
                {
                    MessageBox.Show(dersadi+" zaten eklenmiş!");
                }                            
            }
        }

        private void contextMenuStrip_EklenenAlanlar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (listViewEklenenAlanlar.SelectedItems.Count != 0)
            {
                comboBox_TasınacakAlanlar.Items.RemoveAt(6 + listViewEklenenAlanlar.SelectedItems[0].Index);
                listViewEklenenAlanlar.Items.Remove(listViewEklenenAlanlar.SelectedItems[0]);
            }
            else
            {
                MessageBox.Show("Seçilen yok!");
            }
        }

        private void buttonFormugoruntule_Click(object sender, EventArgs e)
        {
            formuGoruntule();

           /* int x1 = dersAlaniBaslangic.X;
            int x2 = dersAlaniBaslangic.X + 4 * 43;
            int y1 = dersAlaniBaslangic.Y;
            int y2 = dersAlaniBaslangic.Y + 1 * 43;
           
            for (int i = y1; i < y2; i += 43)
            {
                for (int j = x1; j < x2; j += 43)
                {
                    Graphics g = Graphics.FromImage(bosForm);
                    g.DrawRectangle(new Pen(Color.Red, 1), new Rectangle(j, i, 36, 36));
                    List<int> siyahpikseller = new List<int>();                    
                }
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintDialog yazıcıayar = new PrintDialog();
            yazıcıayar.Document = printDocument1;

            if (yazıcıayar.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(pictureBox1.Image, 0, 0, 827, 1169);
        }

        private void formuGoruntule()
        {
            dersSayisi = listViewEklenenAlanlar.Items.Count;
            dersBaslangicNoktalari = new List<Point>();
            if (comboBoxOkulTuru.SelectedIndex > -1)
            {
                if (dersSayisi > 0)
                {
                    if (comboBoxOkulTuru.SelectedIndex== 0)
                    {
                        for (int a = 0; a < dersSayisi; a++)
                        {
                            dersBaslangicNoktalari.Add(new Point(dersAlaniBaslangic.X + a * 5 * (cevapSikkiEbat + cevapSikkiBosluk), dersAlaniBaslangic.Y));
                        }
                    }
                    if (comboBoxOkulTuru.SelectedIndex == 1)
                    {
                        for (int a = 0; a < dersSayisi; a++)
                        {
                            dersBaslangicNoktalari.Add(new Point(dersAlaniBaslangic.X + a * 6 * (cevapSikkiEbat + cevapSikkiBosluk), dersAlaniBaslangic.Y));
                        }
                    }
                    if (comboBoxOkulTuru.SelectedIndex == 2)
                    {
                        for (int a = 0; a < dersSayisi; a++)
                        {
                            dersBaslangicNoktalari.Add(new Point(dersAlaniBaslangic.X + a * 7 * (cevapSikkiEbat + cevapSikkiBosluk), dersAlaniBaslangic.Y));
                        }
                    }                   

                    this.Cursor = Cursors.AppStarting;
                    bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                    /* bosForm = new Bitmap(genislik, yukseklik);
                     for (int i = 0; i < genislik; i++)
                     {
                         for (int j = 0; j < yukseklik; j++)
                         {
                             bosForm.SetPixel(i, j, Color.White);
                         }
                     }*/
                    //temizForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                    baslik_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule();
                    Bitmap form = new Bitmap(bosForm, 827, 1169);
                    pictureBox1.Image = bosForm;

                    this.Cursor = Cursors.Default;
                }
                else
                {
                    MessageBox.Show("Lütfen ders ekleyiniz!");
                }
            }
            else
            {
                MessageBox.Show("Lütfen okul türünü seçiniz!");
            }

        }

        private void eklenenAlanlar_Goruntule()
        {          
            for (int a = 0; a < dersSayisi; a++)
            {
                int sorusayisi = Convert.ToInt32(listViewEklenenAlanlar.Items[a].SubItems[1].Text);
                for (int i = dersBaslangicNoktalari[a].Y; i < dersBaslangicNoktalari[a].Y + sorusayisi * (cevapSikkiEbat + cevapSikkiBosluk); i += cevapSikkiEbat + cevapSikkiBosluk)
                {
                    if (comboBoxOkulTuru.SelectedIndex == 0)
                    {
                        int sikGen = 4 * cevapSikkiEbat + 3 * cemberKalinligi + 3 * cevapSikkiBosluk;
                        int sikYuk = cevapSikkiEbat + cemberKalinligi;
                        Bitmap ilkokulSiklar = new Bitmap(sikGen, sikYuk);
                        for (int k = 0; k < sikGen; k++)
                        {
                            for (int j = 0; j < sikYuk; j++)
                            {
                                ilkokulSiklar.SetPixel(k, j, Color.White);
                            }
                        }

                        SolidBrush brushSiklar = new SolidBrush(Color.Gray);
                        g = Graphics.FromImage(ilkokulSiklar);
                        g.DrawEllipse(new Pen(brushSiklar, cemberKalinligi), new Rectangle(cemberKalinligi + cevapSikkiEbat + cevapSikkiBosluk, 0, cevapSikkiEbat, cevapSikkiEbat));
                        g.DrawEllipse(new Pen(brushSiklar, cemberKalinligi), new Rectangle(cemberKalinligi + 2 * cevapSikkiEbat + 2 * cevapSikkiBosluk, 0, cevapSikkiEbat, cevapSikkiEbat));
                        g.DrawEllipse(new Pen(brushSiklar, cemberKalinligi), new Rectangle(cemberKalinligi + 3 * cevapSikkiEbat + 3 * cevapSikkiBosluk, 0, cevapSikkiEbat, cevapSikkiEbat));

                        int soruSiraSayisi = (i - dersBaslangicNoktalari[a].Y) / (cevapSikkiEbat + cevapSikkiBosluk) + 1;
                        Font fontSiklar = new Font("Cambria", fontSizeSik, FontStyle.Bold);
                        Font fontSiraNo = new Font("Cambria", fontSizeSiraNo, FontStyle.Bold);
                        g.DrawString(soruSiraSayisi.ToString(), fontSiraNo, brushSiklar, new Point(0, 0));
                        g.DrawString("A", fontSiklar, brushSiklar, new Point(cevapSikkiEbat + cevapSikkiBosluk + 2 * cemberKalinligi, 0));
                        g.DrawString("B", fontSiklar, brushSiklar, new Point(2 * cevapSikkiEbat + 2 * cevapSikkiBosluk + 2 * cemberKalinligi, 0));
                        g.DrawString("C", fontSiklar, brushSiklar, new Point(3 * cevapSikkiEbat + 3 * cevapSikkiBosluk + 2 * cemberKalinligi, 0));
                        g.Dispose();

                        g = Graphics.FromImage(bosForm);
                        Point[] koseler = new Point[3];
                        koseler[0] = new Point(dersBaslangicNoktalari[a].X, i);
                        koseler[1] = new Point(ilkokulSiklar.Width - 1 + dersBaslangicNoktalari[a].X, i);
                        koseler[2] = new Point(dersBaslangicNoktalari[a].X, ilkokulSiklar.Height - 1 + i);

                        g.DrawImage(ilkokulSiklar, koseler);
                        g.Dispose();
                    }
                    else if (comboBoxOkulTuru.SelectedIndex == 1)
                    {
                        int sikGen = 5 * cevapSikkiEbat + 4 * cemberKalinligi + 4 * cevapSikkiBosluk;
                        int sikYuk = cevapSikkiEbat + cemberKalinligi;
                        Bitmap ortaokulSiklar = new Bitmap(sikGen, sikYuk);
                        for (int k = 0; k < sikGen; k++)
                        {
                            for (int j = 0; j < sikYuk; j++)
                            {
                                ortaokulSiklar.SetPixel(k, j, Color.White);
                            }
                        }

                        SolidBrush brushSiklar = new SolidBrush(Color.Gray);
                        g = Graphics.FromImage(ortaokulSiklar);
                        g.DrawEllipse(new Pen(brushSiklar, cemberKalinligi), new Rectangle(cemberKalinligi + cevapSikkiEbat + cevapSikkiBosluk, 0, cevapSikkiEbat, cevapSikkiEbat));
                        g.DrawEllipse(new Pen(brushSiklar, cemberKalinligi), new Rectangle(cemberKalinligi + 2 * cevapSikkiEbat + 2 * cevapSikkiBosluk, 0, cevapSikkiEbat, cevapSikkiEbat));
                        g.DrawEllipse(new Pen(brushSiklar, cemberKalinligi), new Rectangle(cemberKalinligi + 3 * cevapSikkiEbat + 3 * cevapSikkiBosluk, 0, cevapSikkiEbat, cevapSikkiEbat));
                        g.DrawEllipse(new Pen(brushSiklar, cemberKalinligi), new Rectangle(cemberKalinligi + 4 * cevapSikkiEbat + 4 * cevapSikkiBosluk, 0, cevapSikkiEbat, cevapSikkiEbat));

                        int soruSiraSayisi = (i - dersBaslangicNoktalari[a].Y) / (cevapSikkiEbat + cevapSikkiBosluk) + 1;
                        Font fontSiklar = new Font("Cambria", fontSizeSik, FontStyle.Bold);
                        Font fontSiraNo = new Font("Cambria", fontSizeSiraNo, FontStyle.Bold);
                        g.DrawString(soruSiraSayisi.ToString(), fontSiraNo, brushSiklar, new Point(0, 0));
                        g.DrawString("A", fontSiklar, brushSiklar, new Point(cevapSikkiEbat + cevapSikkiBosluk + 2 * cemberKalinligi, 0));
                        g.DrawString("B", fontSiklar, brushSiklar, new Point(2 * cevapSikkiEbat + 2 * cevapSikkiBosluk + 2 * cemberKalinligi, 0));
                        g.DrawString("C", fontSiklar, brushSiklar, new Point(3 * cevapSikkiEbat + 3 * cevapSikkiBosluk + 2 * cemberKalinligi, 0));
                        g.DrawString("D", fontSiklar, brushSiklar, new Point(4 * cevapSikkiEbat + 4 * cevapSikkiBosluk + 2 * cemberKalinligi, 0));
                        g.Dispose();

                        g = Graphics.FromImage(bosForm);
                        Point[] koseler = new Point[3];
                        koseler[0] = new Point(dersBaslangicNoktalari[a].X, i);
                        koseler[1] = new Point(ortaokulSiklar.Width - 1 + dersBaslangicNoktalari[a].X, i);
                        koseler[2] = new Point(dersBaslangicNoktalari[a].X, ortaokulSiklar.Height - 1 + i);

                        g.DrawImage(ortaokulSiklar, koseler);
                        g.Dispose();
                    }
                    else if (comboBoxOkulTuru.SelectedIndex == 2)
                    {
                        int sikGen = 6 * cevapSikkiEbat + 5 * cemberKalinligi + 5 * cevapSikkiBosluk;
                        int sikYuk = cevapSikkiEbat + cemberKalinligi;
                        Bitmap liseSiklar = new Bitmap(sikGen, sikYuk);
                        for (int k = 0; k < sikGen; k++)
                        {
                            for (int j = 0; j < sikYuk; j++)
                            {
                                liseSiklar.SetPixel(k, j, Color.White);
                            }
                        }

                        SolidBrush brushSiklar = new SolidBrush(Color.Gray);
                        g = Graphics.FromImage(liseSiklar);
                        g.DrawEllipse(new Pen(brushSiklar, cemberKalinligi), new Rectangle(cemberKalinligi + cevapSikkiEbat + cevapSikkiBosluk, 0, cevapSikkiEbat, cevapSikkiEbat));
                        g.DrawEllipse(new Pen(brushSiklar, cemberKalinligi), new Rectangle(cemberKalinligi + 2 * cevapSikkiEbat + 2 * cevapSikkiBosluk, 0, cevapSikkiEbat, cevapSikkiEbat));
                        g.DrawEllipse(new Pen(brushSiklar, cemberKalinligi), new Rectangle(cemberKalinligi + 3 * cevapSikkiEbat + 3 * cevapSikkiBosluk, 0, cevapSikkiEbat, cevapSikkiEbat));
                        g.DrawEllipse(new Pen(brushSiklar, cemberKalinligi), new Rectangle(cemberKalinligi + 4 * cevapSikkiEbat + 4 * cevapSikkiBosluk, 0, cevapSikkiEbat, cevapSikkiEbat));
                        g.DrawEllipse(new Pen(brushSiklar, cemberKalinligi), new Rectangle(cemberKalinligi + 5 * cevapSikkiEbat + 5 * cevapSikkiBosluk, 0, cevapSikkiEbat, cevapSikkiEbat));


                        int soruSiraSayisi = (i - dersBaslangicNoktalari[a].Y) / (cevapSikkiEbat + cevapSikkiBosluk) + 1;
                        Font fontSiklar = new Font("Cambria", fontSizeSik, FontStyle.Bold);
                        Font fontSiraNo = new Font("Cambria", fontSizeSiraNo, FontStyle.Bold);
                        g.DrawString(soruSiraSayisi.ToString(), fontSiraNo, brushSiklar, new Point(0, 0));
                        g.DrawString("A", fontSiklar, brushSiklar, new Point(cevapSikkiEbat + cevapSikkiBosluk + 2 * cemberKalinligi, 0));
                        g.DrawString("B", fontSiklar, brushSiklar, new Point(2 * cevapSikkiEbat + 2 * cevapSikkiBosluk + 2 * cemberKalinligi, 0));
                        g.DrawString("C", fontSiklar, brushSiklar, new Point(3 * cevapSikkiEbat + 3 * cevapSikkiBosluk + 2 * cemberKalinligi, 0));
                        g.DrawString("D", fontSiklar, brushSiklar, new Point(4 * cevapSikkiEbat + 4 * cevapSikkiBosluk + 2 * cemberKalinligi, 0));
                        g.DrawString("E", fontSiklar, brushSiklar, new Point(5 * cevapSikkiEbat + 5 * cevapSikkiBosluk + 2 * cemberKalinligi, 0));
                        g.Dispose();

                        g = Graphics.FromImage(bosForm);
                        Point[] koseler = new Point[3];
                        koseler[0] = new Point(dersBaslangicNoktalari[a].X, i);
                        koseler[1] = new Point(liseSiklar.Width - 1 + dersBaslangicNoktalari[a].X, i);
                        koseler[2] = new Point(dersBaslangicNoktalari[a].X, liseSiklar.Height - 1 + i);

                        g.DrawImage(liseSiklar, koseler);
                        g.Dispose();
                    }
                }

                g = Graphics.FromImage(bosForm);
                string dersadi = listViewEklenenAlanlar.Items[a].SubItems[0].Text;
                Font fontDersadi = new Font("Cambria", fontSizeDersadi, FontStyle.Bold);
                SolidBrush brushDersadi = new SolidBrush(Color.Gray);
                if (comboBoxOkulTuru.SelectedIndex == 0)
                {
                    g.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(dersBaslangicNoktalari[a].X - 2, dersBaslangicNoktalari[a].Y - 4 - cemberKalinligi - (cevapSikkiEbat + cevapSikkiBosluk), 4 * (cevapSikkiBosluk + cevapSikkiEbat), (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString(dersadi, fontDersadi, brushDersadi, new Point(dersBaslangicNoktalari[a].X, dersBaslangicNoktalari[a].Y - (cevapSikkiEbat + cemberKalinligi) - 4));
                    g.Dispose();
                }
                else if (comboBoxOkulTuru.SelectedIndex == 1)
                {
                    g.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(dersBaslangicNoktalari[a].X - 2, dersBaslangicNoktalari[a].Y - 4 - cemberKalinligi - (cevapSikkiEbat + cevapSikkiBosluk), 5 * (cevapSikkiBosluk + cevapSikkiEbat), (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString(dersadi, fontDersadi, brushDersadi, new Point(dersBaslangicNoktalari[a].X, dersBaslangicNoktalari[a].Y - (cevapSikkiEbat + cemberKalinligi) - 4));
                    g.Dispose();
                }
                else if (comboBoxOkulTuru.SelectedIndex == 2)
                {
                    g.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(dersBaslangicNoktalari[a].X - 2, dersBaslangicNoktalari[a].Y - 4 - cemberKalinligi - (cevapSikkiEbat + cevapSikkiBosluk), 6 * (cevapSikkiBosluk + cevapSikkiEbat), (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString(dersadi, fontDersadi, brushDersadi, new Point(dersBaslangicNoktalari[a].X, dersBaslangicNoktalari[a].Y - (cevapSikkiEbat + cemberKalinligi) - 4));
                    g.Dispose();
                }
            }
        }

        private void kitTuru_Goruntule()
        {
            if (checkBoxKitTuru.Checked)
            {
                g = Graphics.FromImage(bosForm);
                Bitmap kitTur = new Bitmap("FormTasarlamaBileşenleri/kitapcikTuru.jpg");
                g.DrawImage(kitTur, kitapcıkTurBaslangic);
                /*Font fontSik = new Font("Cambria", fontSizeSik, FontStyle.Bold);
                SolidBrush brushSik = new SolidBrush(Color.Gray);
                g.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(kitapcıkTurBaslangic.X - 2 * (cemberKalinligi + cevapSikkiEbat), kitapcıkTurBaslangic.Y - cemberKalinligi - 7 * (cevapSikkiEbat + cevapSikkiBosluk), 5 * (cevapSikkiBosluk + cevapSikkiEbat), 12 * (cevapSikkiEbat + cevapSikkiBosluk)));
                g.DrawString("\n      Kitapçık\n          Türü\n\n       DİKKAT!\n       Bu alanı\n  işaretlemeyi\n  unutmayınız!", fontSik, brushSik, new Point(kitapcıkTurBaslangic.X - 2 * (cemberKalinligi + cevapSikkiEbat), kitapcıkTurBaslangic.Y - cemberKalinligi - 7 * (cevapSikkiEbat + cevapSikkiBosluk)));

                for (int j = 0; j < 1; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        g.DrawEllipse(new Pen(brushSik, cemberKalinligi), new Rectangle(kitapcıkTurBaslangic.X + j * (cevapSikkiEbat + cevapSikkiBosluk), kitapcıkTurBaslangic.Y + i * (cevapSikkiEbat + cevapSikkiBosluk), cevapSikkiEbat, cevapSikkiEbat));
                    }

                    g.DrawString("A", fontSik, brushSik, new Point(kitapcıkTurBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), kitapcıkTurBaslangic.Y));
                    g.DrawString("B", fontSik, brushSik, new Point(kitapcıkTurBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), kitapcıkTurBaslangic.Y + 1 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("C", fontSik, brushSik, new Point(kitapcıkTurBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), kitapcıkTurBaslangic.Y + 2 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("D", fontSik, brushSik, new Point(kitapcıkTurBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), kitapcıkTurBaslangic.Y + 3 * (cevapSikkiEbat + cevapSikkiBosluk)));
                }*/
                g.Dispose();
            }
        }

        private void okulKodu_Goruntule()
        {
            if (checkBoxOkulKodu.Checked)
            {
                g = Graphics.FromImage(bosForm);
                Bitmap okulKodu = new Bitmap("FormTasarlamaBileşenleri/okulKodu.jpg");
                g.DrawImage(okulKodu, okulKoduBaslangic);
                /*Font fontSik = new Font("Cambria", fontSizeSik, FontStyle.Bold);
                SolidBrush brushSik = new SolidBrush(Color.Gray);
                g.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(okulKoduBaslangic.X - 2, okulKoduBaslangic.Y - cemberKalinligi - 2 * (cevapSikkiEbat + cevapSikkiBosluk), 6 * (cevapSikkiBosluk + cevapSikkiEbat), (cevapSikkiEbat + cevapSikkiBosluk)));
                g.DrawString("      Okul Kodu", fontSik, brushSik, new Point(okulKoduBaslangic.X, okulKoduBaslangic.Y - cemberKalinligi - 2 * (cevapSikkiEbat + cevapSikkiBosluk)));

                for (int j = 0; j < 6; j++)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        g.DrawEllipse(new Pen(brushSik, cemberKalinligi), new Rectangle(okulKoduBaslangic.X + j * (cevapSikkiEbat + cevapSikkiBosluk), okulKoduBaslangic.Y + i * (cevapSikkiEbat + cevapSikkiBosluk), cevapSikkiEbat, cevapSikkiEbat));
                    }
                    g.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(okulKoduBaslangic.X - 2 + j * (cevapSikkiEbat + cevapSikkiBosluk), okulKoduBaslangic.Y - cemberKalinligi - (cevapSikkiEbat + cevapSikkiBosluk), (cevapSikkiBosluk + cevapSikkiEbat), cevapSikkiEbat + cevapSikkiBosluk));

                    g.DrawString("0", fontSik, brushSik, new Point(okulKoduBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), okulKoduBaslangic.Y));
                    g.DrawString("1", fontSik, brushSik, new Point(okulKoduBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), okulKoduBaslangic.Y + 1 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("2", fontSik, brushSik, new Point(okulKoduBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), okulKoduBaslangic.Y + 2 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("3", fontSik, brushSik, new Point(okulKoduBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), okulKoduBaslangic.Y + 3 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("4", fontSik, brushSik, new Point(okulKoduBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), okulKoduBaslangic.Y + 4 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("5", fontSik, brushSik, new Point(okulKoduBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), okulKoduBaslangic.Y + 5 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("6", fontSik, brushSik, new Point(okulKoduBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), okulKoduBaslangic.Y + 6 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("7", fontSik, brushSik, new Point(okulKoduBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), okulKoduBaslangic.Y + 7 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("8", fontSik, brushSik, new Point(okulKoduBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), okulKoduBaslangic.Y + 8 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("9", fontSik, brushSik, new Point(okulKoduBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), okulKoduBaslangic.Y + 9 * (cevapSikkiEbat + cevapSikkiBosluk)));
                }*/
                g.Dispose();
            }
        }

        private void sinifSube_Goruntule()
        {
            if (checkBoxSinif.Checked)
            {
                g = Graphics.FromImage(bosForm);
                Bitmap sinifSube = new Bitmap("FormTasarlamaBileşenleri/sinifSube.jpg");
                g.DrawImage(sinifSube, sinifBaslangic);
                /*Font fontSik = new Font("Cambria", fontSizeSik, FontStyle.Bold);
                SolidBrush brushSik = new SolidBrush(Color.Gray);
                g.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(sinifBaslangic.X - 2, sinifBaslangic.Y - cemberKalinligi - 2 * (cevapSikkiEbat + cevapSikkiBosluk), 2 * (cevapSikkiBosluk + cevapSikkiEbat), 2 * (cevapSikkiEbat + cevapSikkiBosluk)));
                g.DrawString("Sınıf\nŞube", fontSik, brushSik, new Point(sinifBaslangic.X, sinifBaslangic.Y - cemberKalinligi - 2 * (cevapSikkiEbat + cevapSikkiBosluk)));

                for (int j = 0; j < 1; j++)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        g.DrawEllipse(new Pen(brushSik, cemberKalinligi), new Rectangle(sinifBaslangic.X + j * (cevapSikkiEbat + cevapSikkiBosluk), sinifBaslangic.Y + i * (cevapSikkiEbat + cevapSikkiBosluk), cevapSikkiEbat, cevapSikkiEbat));
                    }
                    //g.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(sinifBaslangic.X - 2 + j * (cevapSikkiEbat + cevapSikkiBosluk), sinifBaslangic.Y - cemberKalinligi - (cevapSikkiEbat + cevapSikkiBosluk), (cevapSikkiBosluk + cevapSikkiEbat), cevapSikkiEbat + cevapSikkiBosluk));

                    g.DrawString("1", fontSik, brushSik, new Point(sinifBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), sinifBaslangic.Y));
                    g.DrawString("2", fontSik, brushSik, new Point(sinifBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), sinifBaslangic.Y + 1 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("3", fontSik, brushSik, new Point(sinifBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), sinifBaslangic.Y + 2 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("4", fontSik, brushSik, new Point(sinifBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), sinifBaslangic.Y + 3 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("5", fontSik, brushSik, new Point(sinifBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), sinifBaslangic.Y + 4 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("6", fontSik, brushSik, new Point(sinifBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), sinifBaslangic.Y + 5 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("7", fontSik, brushSik, new Point(sinifBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), sinifBaslangic.Y + 6 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("8", fontSik, brushSik, new Point(sinifBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), sinifBaslangic.Y + 7 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("9", fontSik, brushSik, new Point(sinifBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), sinifBaslangic.Y + 8 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("10", new Font("Cambria", 18f, FontStyle.Bold), brushSik, new Point(sinifBaslangic.X + j * (cevapSikkiEbat + cevapSikkiBosluk), sinifBaslangic.Y + 3 + 9 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("11", new Font("Cambria", 18f, FontStyle.Bold), brushSik, new Point(sinifBaslangic.X + j * (cevapSikkiEbat + cevapSikkiBosluk), sinifBaslangic.Y + 3 + 10 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("12", new Font("Cambria", 18f, FontStyle.Bold), brushSik, new Point(sinifBaslangic.X + j * (cevapSikkiEbat + cevapSikkiBosluk), sinifBaslangic.Y + 3 + 11 * (cevapSikkiEbat + cevapSikkiBosluk)));
                }
                g.Dispose();

                g = Graphics.FromImage(bosForm);
                fontSik = new Font("Cambria", fontSizeSik, FontStyle.Bold);
                brushSik = new SolidBrush(Color.Gray);

                for (int j = 0; j < 1; j++)
                {
                    for (int i = 0; i < 29; i++)
                    {
                        g.DrawEllipse(new Pen(brushSik, cemberKalinligi), new Rectangle(subeBaslangic.X + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + i * (cevapSikkiEbat + cevapSikkiBosluk), cevapSikkiEbat, cevapSikkiEbat));
                    }
                    // g.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(subeBaslangic.X - 2 + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y - cemberKalinligi - (cevapSikkiEbat + cevapSikkiBosluk), (cevapSikkiBosluk + cevapSikkiEbat), cevapSikkiEbat + cevapSikkiBosluk));

                    g.DrawString("A", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y));
                    g.DrawString("B", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 1 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("C", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 2 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("Ç", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 3 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("D", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 4 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("E", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 5 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("F", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 6 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("G", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 7 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("Ğ", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 3 + 8 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("H", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 9 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("I", fontSik, brushSik, new Point(subeBaslangic.X + 3 * cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 10 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("İ", fontSik, brushSik, new Point(subeBaslangic.X + 3 * cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 3 + 11 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("J", fontSik, brushSik, new Point(subeBaslangic.X + 3 * cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y - 3 + 12 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("K", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 13 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("L", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 14 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("M", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 15 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("N", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 16 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("O", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 17 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("Ö", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 18 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("P", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 19 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("R", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 20 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("S", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 21 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("Ş", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 22 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("T", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 23 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("U", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 24 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("Ü", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 25 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("V", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 26 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("Y", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 27 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("Z", fontSik, brushSik, new Point(subeBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), subeBaslangic.Y + 28 * (cevapSikkiEbat + cevapSikkiBosluk)));

                }*/
                g.Dispose();
            }
        }

        private void ogrNo_Goruntule()
        {
            if (checkBoxOgrNo.Checked)
            {
                g = Graphics.FromImage(bosForm);
                Bitmap ogrNo = new Bitmap("FormTasarlamaBileşenleri/ogrenciNo.jpg");
                g.DrawImage(ogrNo, ogrenciNoBaslangic);

                /*Font fontSik = new Font("Cambria", fontSizeSik, FontStyle.Bold);
                SolidBrush brushSik = new SolidBrush(Color.Gray);
                g.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(ogrenciNoBaslangic.X - 2, ogrenciNoBaslangic.Y - cemberKalinligi - 2 * (cevapSikkiEbat + cevapSikkiBosluk), 4 * (cevapSikkiBosluk + cevapSikkiEbat), (cevapSikkiEbat + cevapSikkiBosluk)));
                g.DrawString("Öğrenci No", fontSik, brushSik, new Point(ogrenciNoBaslangic.X, ogrenciNoBaslangic.Y - cemberKalinligi - 2 * (cevapSikkiEbat + cevapSikkiBosluk)));

                for (int j = 0; j < 4; j++)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        g.DrawEllipse(new Pen(brushSik, cemberKalinligi), new Rectangle(ogrenciNoBaslangic.X + j * (cevapSikkiEbat + cevapSikkiBosluk), ogrenciNoBaslangic.Y + i * (cevapSikkiEbat + cevapSikkiBosluk), cevapSikkiEbat, cevapSikkiEbat));
                    }
                    g.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(ogrenciNoBaslangic.X - 2 + j * (cevapSikkiEbat + cevapSikkiBosluk), ogrenciNoBaslangic.Y - cemberKalinligi - (cevapSikkiEbat + cevapSikkiBosluk), (cevapSikkiBosluk + cevapSikkiEbat), cevapSikkiEbat + cevapSikkiBosluk));

                    g.DrawString("0", fontSik, brushSik, new Point(ogrenciNoBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), ogrenciNoBaslangic.Y));
                    g.DrawString("1", fontSik, brushSik, new Point(ogrenciNoBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), ogrenciNoBaslangic.Y + 1 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("2", fontSik, brushSik, new Point(ogrenciNoBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), ogrenciNoBaslangic.Y + 2 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("3", fontSik, brushSik, new Point(ogrenciNoBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), ogrenciNoBaslangic.Y + 3 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("4", fontSik, brushSik, new Point(ogrenciNoBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), ogrenciNoBaslangic.Y + 4 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("5", fontSik, brushSik, new Point(ogrenciNoBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), ogrenciNoBaslangic.Y + 5 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("6", fontSik, brushSik, new Point(ogrenciNoBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), ogrenciNoBaslangic.Y + 6 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("7", fontSik, brushSik, new Point(ogrenciNoBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), ogrenciNoBaslangic.Y + 7 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("8", fontSik, brushSik, new Point(ogrenciNoBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), ogrenciNoBaslangic.Y + 8 * (cevapSikkiEbat + cevapSikkiBosluk)));
                    g.DrawString("9", fontSik, brushSik, new Point(ogrenciNoBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), ogrenciNoBaslangic.Y + 9 * (cevapSikkiEbat + cevapSikkiBosluk)));
                }*/
                g.Dispose();
            }
        }

        private void adsoyad_Goruntule()
        {
            if (checkBoxAdSoyad.Checked)
            {
                g = Graphics.FromImage(bosForm);
                Bitmap adsoyad = new Bitmap("FormTasarlamaBileşenleri/adsoyad.jpg");
                g.DrawImage(adsoyad, adSoyadBaslangic);
                /* Font fontSik = new Font("Cambria", fontSizeSik, FontStyle.Bold);
                 SolidBrush brushSik = new SolidBrush(Color.Gray);
                 g.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(adSoyadBaslangic.X - 2, adSoyadBaslangic.Y - cemberKalinligi - 3 * (cevapSikkiEbat + cevapSikkiBosluk), 20 * (cevapSikkiBosluk + cevapSikkiEbat), 2 * (cevapSikkiEbat + cevapSikkiBosluk)));
                 g.DrawString("                                                             AD-SOYAD \n      (Adınız ile soyadınız arasına bir kare boşluk bırakınız!)", fontSik, brushSik, new Point(adSoyadBaslangic.X, adSoyadBaslangic.Y - cemberKalinligi - 3 * (cevapSikkiEbat + cevapSikkiBosluk)));

                 for (int j = 0; j < 20; j++)
                 {
                     for (int i = 0; i < 29; i++)
                     {
                         g.DrawEllipse(new Pen(brushSik, cemberKalinligi), new Rectangle(adSoyadBaslangic.X + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + i * (cevapSikkiEbat + cevapSikkiBosluk), cevapSikkiEbat, cevapSikkiEbat));
                     }
                     g.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(adSoyadBaslangic.X - 2 + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y - cemberKalinligi - (cevapSikkiEbat + cevapSikkiBosluk), (cevapSikkiBosluk + cevapSikkiEbat), cevapSikkiEbat + cevapSikkiBosluk));

                     g.DrawString("A", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y));
                     g.DrawString("B", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 1 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("C", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 2 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("Ç", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 3 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("D", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 4 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("E", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 5 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("F", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 6 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("G", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 7 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("Ğ", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 3 + 8 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("H", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 9 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("I", fontSik, brushSik, new Point(adSoyadBaslangic.X + 3 * cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 10 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("İ", fontSik, brushSik, new Point(adSoyadBaslangic.X + 3 * cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 3 + 11 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("J", fontSik, brushSik, new Point(adSoyadBaslangic.X + 3 * cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y - 3 + 12 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("K", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 13 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("L", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 14 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("M", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 15 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("N", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 16 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("O", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 17 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("Ö", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 18 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("P", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 19 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("R", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 20 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("S", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 21 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("Ş", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 22 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("T", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 23 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("U", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 24 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("Ü", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 25 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("V", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 26 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("Y", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 27 * (cevapSikkiEbat + cevapSikkiBosluk)));
                     g.DrawString("Z", fontSik, brushSik, new Point(adSoyadBaslangic.X + cemberKalinligi + j * (cevapSikkiEbat + cevapSikkiBosluk), adSoyadBaslangic.Y + 28 * (cevapSikkiEbat + cevapSikkiBosluk)));
                 }*/
                g.Dispose();
            }
        }

        private void baslik_Goruntule()
        {
            if (checkBoxBaslik.Checked)
            {
                g = Graphics.FromImage(bosForm);
                Bitmap baslik = new Bitmap("FormTasarlamaBileşenleri/baslikCevapKagidi.jpg");
                g.DrawImage(baslik, baslikBaslangic);
                /*  Font font = new Font("Cambria", fontSizeBaslik, FontStyle.Bold);
                  SolidBrush brush = new SolidBrush(Color.Black);
                  g.DrawString("CEVAP KAĞIDI", font, brush, 1000, 100);
                g.DrawRectangle(new Pen(Color.Black, 22), new Rectangle(150, 150, 22, 22));
                g.DrawRectangle(new Pen(Color.Black, 22), new Rectangle(genislik - 150, 150, 22, 22));
                g.DrawRectangle(new Pen(Color.Black, 22), new Rectangle(150, yukseklik - 150, 22, 22));
                g.DrawRectangle(new Pen(Color.Black, 22), new Rectangle(genislik - 150, yukseklik - 150, 22, 22));*/
                g.Dispose();
            }
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            if (comboBox_TasınacakAlanlar.SelectedIndex == 0)
            {
                adSoyadBaslangic.X += 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); ogrNo_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 1)
            {
                ogrenciNoBaslangic.X += 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 2)
            {
                sinifBaslangic.X += 3;
                subeBaslangic.X += 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule(); sinifSube_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 3)
            {
                okulKoduBaslangic.X += 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 4)
            {
                kitapcıkTurBaslangic.X += 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 5)
            {
                baslikBaslangic.X += 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            for (int i = 0; i < listViewEklenenAlanlar.Items.Count; i++)
            {
                if (comboBox_TasınacakAlanlar.SelectedIndex == 6 + i)
                {
                    dersBaslangicNoktalari[i] = new Point(dersBaslangicNoktalari[i].X + 3, dersBaslangicNoktalari[i].Y);
                    bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                    sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule();
                    // Bitmap form = new Bitmap(bosForm, 827, 1169);
                    pictureBox1.Image = bosForm;
                }
            }

        }

        private void buttonRightFastly_Click(object sender, EventArgs e)
        {
            if (comboBox_TasınacakAlanlar.SelectedIndex == 0)
            {
                adSoyadBaslangic.X += 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); ogrNo_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 1)
            {
                ogrenciNoBaslangic.X += 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 2)
            {
                sinifBaslangic.X += 86;
                subeBaslangic.X += 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/adsoyad.jpg");
                adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule(); sinifSube_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 3)
            {
                okulKoduBaslangic.X += 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 4)
            {
                kitapcıkTurBaslangic.X += 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 5)
            {
                baslikBaslangic.X += 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            for (int i = 0; i < listViewEklenenAlanlar.Items.Count; i++)
            {
                if (comboBox_TasınacakAlanlar.SelectedIndex == 6 + i)
                {
                    dersBaslangicNoktalari[i] = new Point(dersBaslangicNoktalari[i].X + 86, dersBaslangicNoktalari[i].Y);
                    bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                    sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule();
                    // Bitmap form = new Bitmap(bosForm, 827, 1169);
                    pictureBox1.Image = bosForm;
                }
            }
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            if (comboBox_TasınacakAlanlar.SelectedIndex == 0)
            {
                adSoyadBaslangic.X -= 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); ogrNo_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 1)
            {
                ogrenciNoBaslangic.X -= 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 2)
            {
                sinifBaslangic.X -= 3;
                subeBaslangic.X -= 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule(); sinifSube_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 3)
            {
                okulKoduBaslangic.X -= 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 4)
            {
                kitapcıkTurBaslangic.X -= 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 5)
            {
                baslikBaslangic.X -= 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            for (int i = 0; i < listViewEklenenAlanlar.Items.Count; i++)
            {
                if (comboBox_TasınacakAlanlar.SelectedIndex == 6 + i)
                {
                    dersBaslangicNoktalari[i] = new Point(dersBaslangicNoktalari[i].X - 3, dersBaslangicNoktalari[i].Y);
                    bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                    sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule();
                    // Bitmap form = new Bitmap(bosForm, 827, 1169);
                    pictureBox1.Image = bosForm;
                }
            }
        }

        private void buttonLeftFastly_Click(object sender, EventArgs e)
        {
            if (comboBox_TasınacakAlanlar.SelectedIndex == 0)
            {
                adSoyadBaslangic.X -= 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); ogrNo_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 1)
            {
                ogrenciNoBaslangic.X -= 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 2)
            {
                sinifBaslangic.X -= 86;
                subeBaslangic.X -= 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule(); sinifSube_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 3)
            {
                okulKoduBaslangic.X -= 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 4)
            {
                kitapcıkTurBaslangic.X -= 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 5)
            {
                baslikBaslangic.X -= 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            for (int i = 0; i < listViewEklenenAlanlar.Items.Count; i++)
            {
                if (comboBox_TasınacakAlanlar.SelectedIndex == 6 + i)
                {
                    dersBaslangicNoktalari[i] = new Point(dersBaslangicNoktalari[i].X - 86, dersBaslangicNoktalari[i].Y);
                    bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                    sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule();
                    // Bitmap form = new Bitmap(bosForm, 827, 1169);
                    pictureBox1.Image = bosForm;
                }
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (comboBox_TasınacakAlanlar.SelectedIndex == 0)
            {
                adSoyadBaslangic.Y -= 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); ogrNo_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 1)
            {
                ogrenciNoBaslangic.Y -= 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 2)
            {
                sinifBaslangic.Y -= 3;
                subeBaslangic.Y -= 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule(); sinifSube_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 3)
            {
                okulKoduBaslangic.Y -= 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 4)
            {
                kitapcıkTurBaslangic.Y -= 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 5)
            {
                baslikBaslangic.Y -= 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            for (int i = 0; i < listViewEklenenAlanlar.Items.Count; i++)
            {
                if (comboBox_TasınacakAlanlar.SelectedIndex == 6 + i)
                {
                    dersBaslangicNoktalari[i] = new Point(dersBaslangicNoktalari[i].X, dersBaslangicNoktalari[i].Y - 3);
                    bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                    sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule();
                    // Bitmap form = new Bitmap(bosForm, 827, 1169);
                    pictureBox1.Image = bosForm;
                }
            }
        }

        private void buttonUpFastly_Click(object sender, EventArgs e)
        {
            if (comboBox_TasınacakAlanlar.SelectedIndex == 0)
            {
                adSoyadBaslangic.Y -= 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); ogrNo_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 1)
            {
                ogrenciNoBaslangic.Y -= 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 2)
            {
                sinifBaslangic.Y -= 86;
                subeBaslangic.Y -= 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule(); sinifSube_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 3)
            {
                okulKoduBaslangic.Y -= 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 4)
            {
                kitapcıkTurBaslangic.Y -= 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 5)
            {
                baslikBaslangic.Y -= 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            for (int i = 0; i < listViewEklenenAlanlar.Items.Count; i++)
            {
                if (comboBox_TasınacakAlanlar.SelectedIndex == 6 + i)
                {
                    dersBaslangicNoktalari[i] = new Point(dersBaslangicNoktalari[i].X, dersBaslangicNoktalari[i].Y - 86);
                    bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                    sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule();
                    // Bitmap form = new Bitmap(bosForm, 827, 1169);
                    pictureBox1.Image = bosForm;
                }
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (comboBox_TasınacakAlanlar.SelectedIndex == 0)
            {
                adSoyadBaslangic.Y += 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); ogrNo_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 1)
            {
                ogrenciNoBaslangic.Y += 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 2)
            {
                sinifBaslangic.Y += 3;
                subeBaslangic.Y += 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule(); sinifSube_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 3)
            {
                okulKoduBaslangic.Y += 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 4)
            {
                kitapcıkTurBaslangic.Y += 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 5)
            {
                baslikBaslangic.Y += 3;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            for (int i = 0; i < listViewEklenenAlanlar.Items.Count; i++)
            {
                if (comboBox_TasınacakAlanlar.SelectedIndex == 6 + i)
                {
                    dersBaslangicNoktalari[i] = new Point(dersBaslangicNoktalari[i].X, dersBaslangicNoktalari[i].Y + 3);
                    bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                    sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule();
                    // Bitmap form = new Bitmap(bosForm, 827, 1169);
                    pictureBox1.Image = bosForm;
                }
            }
        }

        private void buttonDownFastly_Click(object sender, EventArgs e)
        {
            if (comboBox_TasınacakAlanlar.SelectedIndex == 0)
            {
                adSoyadBaslangic.Y += 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); ogrNo_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 1)
            {
                ogrenciNoBaslangic.Y += 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                baslik_Goruntule(); sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 2)
            {
                sinifBaslangic.Y += 86;
                subeBaslangic.Y += 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule(); sinifSube_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 3)
            {
                okulKoduBaslangic.Y += 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule(); okulKodu_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 4)
            {
                kitapcıkTurBaslangic.Y += 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule(); kitTuru_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            if (comboBox_TasınacakAlanlar.SelectedIndex == 5)
            {
                baslikBaslangic.Y += 86;
                bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); eklenenAlanlar_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule();
                // Bitmap form = new Bitmap(bosForm, 827, 1169);
                pictureBox1.Image = bosForm;
            }
            for (int i = 0; i < listViewEklenenAlanlar.Items.Count; i++)
            {
                if (comboBox_TasınacakAlanlar.SelectedIndex == 6 + i)
                {
                    dersBaslangicNoktalari[i] = new Point(dersBaslangicNoktalari[i].X, dersBaslangicNoktalari[i].Y + 86);
                    bosForm = new Bitmap("FormTasarlamaBileşenleri/bosFormKareli.jpg");
                    sinifSube_Goruntule(); okulKodu_Goruntule(); kitTuru_Goruntule(); adsoyad_Goruntule(); ogrNo_Goruntule(); baslik_Goruntule(); eklenenAlanlar_Goruntule();
                    // Bitmap form = new Bitmap(bosForm, 827, 1169);
                    pictureBox1.Image = bosForm;
                }
            }
        }

        private void buttonKaydet_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (textBoxFormAdi.Text != "")
                {
                    String formadi = textBoxFormAdi.Text;
                    Veritabani vt = new Veritabani();
                    vt.baglan();
                    List<string> kayitliFormAdlari = new List<string>();
                    kayitliFormAdlari = vt.kayitliFormlarınIsimleriniGetir();
                    Boolean durum = false;                   
                        if (kayitliFormAdlari.Contains(formadi))
                        {
                            durum = true;
                        }                    
                    if (durum == false)
                    {                      
                        String okulturu = comboBoxOkulTuru.SelectedItem.ToString();
                        List<Point> standartAlanlarBaslangicNoktalari = new List<Point>();
                        standartAlanlarBaslangicNoktalari.Add(new Point(adSoyadBaslangic.X/5,adSoyadBaslangic.Y/5));
                        standartAlanlarBaslangicNoktalari.Add(new Point(ogrenciNoBaslangic.X / 5, ogrenciNoBaslangic.Y / 5));
                        standartAlanlarBaslangicNoktalari.Add(new Point(sinifBaslangic.X / 5, sinifBaslangic.Y / 5));
                        standartAlanlarBaslangicNoktalari.Add(new Point(kitapcıkTurBaslangic.X / 5, kitapcıkTurBaslangic.Y / 5));
                        standartAlanlarBaslangicNoktalari.Add(new Point(okulKoduBaslangic.X / 5, okulKoduBaslangic.Y / 5));
                        standartAlanlarBaslangicNoktalari.Add(new Point(baslikBaslangic.X / 5, baslikBaslangic.Y / 5));
                        List<int> sorusayilari = new List<int>();
                        for (int i = 0; i < listViewEklenenAlanlar.Items.Count; i++)
                        {
                            sorusayilari.Add(Convert.ToInt32(listViewEklenenAlanlar.Items[i].SubItems[1].Text));
                        }
                        if (sorusayilari.Count < 10)
                        {
                            int mevcut = sorusayilari.Count;
                            for (int i = 0; i < 10 - mevcut; i++)
                            {
                                sorusayilari.Add(0);
                            }
                        }
                        List<String> dersAdlari = new List<string>();
                        for (int i = 0; i < listViewEklenenAlanlar.Items.Count; i++)
                        {
                            dersAdlari.Add(listViewEklenenAlanlar.Items[i].SubItems[0].Text);
                        }
                        if (dersAdlari.Count < 10)
                        {
                            int mevcut = dersAdlari.Count;
                            for (int i = 0; i < 10 - mevcut; i++)
                            {
                                dersAdlari.Add("");
                            }
                        }
                        for(int i = 0; i < dersBaslangicNoktalari.Count; i++)
                        {
                            dersBaslangicNoktalari[i] = new Point(dersBaslangicNoktalari[i].X / 5, dersBaslangicNoktalari[i].Y / 5);
                        }
                        if (dersBaslangicNoktalari.Count < 10)
                        {
                            int mevcut = dersBaslangicNoktalari.Count;
                            for (int i = 0; i < 10 - mevcut; i++)
                            {
                                dersBaslangicNoktalari.Add(new Point(0, 0));
                            }
                        }
                        vt.baglan();
                        vt.kaydetTasarlananFormu(formadi, okulturu, standartAlanlarBaslangicNoktalari, dersBaslangicNoktalari, sorusayilari, dersAdlari);
                        bosForm.Save("KaydedilenFormlar/" + formadi + ".jpg");

                        /* SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                         saveFileDialog1.Filter = "JPeg Image|*.jpg";
                         saveFileDialog1.Title = "Formu kaydedileceği yeri seçiniz";
                         if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                         {
                             if (saveFileDialog1.FileName != "")
                             {
                                 System.IO.FileStream fs =
                                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                                 switch (saveFileDialog1.FilterIndex)
                                 {
                                     case 1:
                                         bosForm.Save(fs,
                                             System.Drawing.Imaging.ImageFormat.Jpeg);
                                         break;
                                 }
                             }*/
                        MessageBox.Show("Kaydetme işlemi tamamlandı");
                    }
                    else
                    {
                        MessageBox.Show("Bu form adıyla kayıtlı form zaten mevcut! Lütfen başka bir form ismi yazın.");
                    }                   
                }
                else
                {
                    MessageBox.Show("Lütfen form adını yazınız!");
                }

            }
            else
            {
                MessageBox.Show("Kaydedilecek form yok! Lütfen önce form oluşturunuz.");
            }
        }

        private void textBoxSoruSayisi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
