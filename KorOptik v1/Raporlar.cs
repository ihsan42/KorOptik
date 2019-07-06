using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace KorOptik_v1
{
    public partial class Raporlar : Form
    {
        public Raporlar()
        {
            InitializeComponent();
        }

        private void Raporlar_Load(object sender, EventArgs e)
        {
            string dizin = "KaydedilenSonuclar";
            string[] dizindekiDosyalar = Directory.GetFiles(dizin);

            foreach (string dosya in dizindekiDosyalar)
            {
                FileInfo fileInfo = new FileInfo(dosya);
                string dosyaAdi = fileInfo.Name;

                comboBoxRaporSınavAdi.Items.Add(dosyaAdi);              
            }

        }

        private void ButtonTumRaporlariIndir_Click(object sender, EventArgs e)
        {
            if (comboBoxRaporSınavAdi.SelectedIndex < 0)
            {
                MessageBox.Show("Sınav adını seçiniz!");
            }
            else
            {
                string yol = "KaydedilenSonuclar/" + comboBoxRaporSınavAdi.SelectedItem.ToString();
                StreamReader reader = File.OpenText(yol);

                String yazi;

                List<String> ogrencilerPuansiz = new List<string>();
                while ((yazi = reader.ReadLine()) != null)
                {
                    ogrencilerPuansiz.Add(yazi);
                }

                reader.Close();
              
                if (radioButtonRaporHesaplansin.Checked)
                {                    
                    

                    if (comboBoxRaporSinavTuru.SelectedIndex == 0)//bursluluk
                    {
                        List<OgrenciSonuclari> ogrenciPuanliSonuclari = new List<OgrenciSonuclari>();

                        for (int i = 1; i < ogrencilerPuansiz.Count; i++)
                        {
                            String[] ogrSonuclari = ogrencilerPuansiz[i].Split('|');

                            double turkceNeti = Convert.ToDouble(ogrSonuclari[9]);
                            double inkNeti = Convert.ToDouble(ogrSonuclari[12]);                           
                            double matNeti = Convert.ToDouble(ogrSonuclari[15]);
                            double fenNeti = Convert.ToDouble(ogrSonuclari[18]);

                            double puan = 200 + 3 * turkceNeti + 3 * inkNeti + 3 * matNeti + 3 * fenNeti;
                            puan = Math.Round(puan, 2);

                            List<String> ogrenciPuanli = new List<string>();

                            for (int j = 1; j < ogrSonuclari.Length; j++)
                            {
                                ogrenciPuanli.Add(ogrSonuclari[j]);
                            }

                            OgrenciSonuclari ogrenciSonuclari = new OgrenciSonuclari();
                            ogrenciSonuclari.addOgrenciSonuclari(ogrenciPuanli);
                            ogrenciSonuclari.setPuan(puan);
                            ogrenciPuanliSonuclari.Add(ogrenciSonuclari);
                        }

                        List<OgrenciSonuclari> ogrenciSonuclariPuanSirali = ogrenciPuanliSonuclari.OrderBy(o => o.puan).ToList<OgrenciSonuclari>();
                        ogrenciSonuclariPuanSirali.Reverse();

                        this.pdfyeAktar(ogrencilerPuansiz, ogrenciSonuclariPuanSirali, "PUANI", 26);
                    }
                    else if (comboBoxRaporSinavTuru.SelectedIndex == 1)//lgs
                    {
                        List<OgrenciSonuclari> ogrenciPuanliSonuclari = new List<OgrenciSonuclari>();

                        for (int i = 1; i < ogrencilerPuansiz.Count; i++)
                        {
                            String[] ogrSonuclari = ogrencilerPuansiz[i].Split('|');

                            double turkceNeti = Convert.ToDouble(ogrSonuclari[9]);
                            double inkNeti = Convert.ToDouble(ogrSonuclari[12]);
                            double dinNeti = Convert.ToDouble(ogrSonuclari[15]);
                            double ingNeti = Convert.ToDouble(ogrSonuclari[18]);
                            double matNeti = Convert.ToDouble(ogrSonuclari[21]);
                            double fenNeti = Convert.ToDouble(ogrSonuclari[24]);

                            double puan = 193.487 + 3.6714 * turkceNeti + 1.6813 * inkNeti + 1.9407 * dinNeti + 1.6367 * ingNeti + 4.9526 * matNeti + 4.0723 * fenNeti;
                            puan = Math.Round(puan, 2);

                            List<String> ogrenciPuanli = new List<string>();

                            for (int j = 1; j < ogrSonuclari.Length; j++)
                            {
                                ogrenciPuanli.Add(ogrSonuclari[j]);
                            }

                            OgrenciSonuclari ogrenciSonuclari = new OgrenciSonuclari();
                            ogrenciSonuclari.addOgrenciSonuclari(ogrenciPuanli);
                            ogrenciSonuclari.setPuan(puan);
                            ogrenciPuanliSonuclari.Add(ogrenciSonuclari);
                        }

                        List<OgrenciSonuclari> ogrenciSonuclariPuanSirali = ogrenciPuanliSonuclari.OrderBy(o => o.puan).ToList<OgrenciSonuclari>();
                        ogrenciSonuclariPuanSirali.Reverse();

                        this.pdfyeAktar(ogrencilerPuansiz, ogrenciSonuclariPuanSirali, "LGS PUANI", 32);
                    }
                    else if (comboBoxRaporSinavTuru.SelectedIndex == 2)//sadece tyt
                    {
                        List<OgrenciSonuclari> ogrenciPuanliSonuclari = new List<OgrenciSonuclari>();

                        for (int i = 1; i < ogrencilerPuansiz.Count; i++) {
                            String[] ogrSonuclari = ogrencilerPuansiz[i].Split('|');

                            double puan = 0;

                            if(Convert.ToDouble(ogrSonuclari[9]) >= 0.5 || Convert.ToDouble(ogrSonuclari[24]) >= 0.5)//türkçe veya matematik neti 0.5 ten küçükse puan hesabı yapılmaz
                            {
                                puan = 100 + Convert.ToDouble(ogrSonuclari[9]) * 3.3 +//TÜRKCE
                                                 Convert.ToDouble(ogrSonuclari[12]) * 3.4 +//TARİH
                                                 Convert.ToDouble(ogrSonuclari[15]) * 3.4 +//COĞRAFYA
                                                 Convert.ToDouble(ogrSonuclari[18]) * 3.4 +//FELSEFE
                                                 Convert.ToDouble(ogrSonuclari[21]) * 3.4 +//DİN
                                                 Convert.ToDouble(ogrSonuclari[24]) * 3.3 +//MATEMATİK
                                                 Convert.ToDouble(ogrSonuclari[27]) * 3.4 +//FİZİK
                                                 Convert.ToDouble(ogrSonuclari[30]) * 3.4 +//KİMYA
                                                 Convert.ToDouble(ogrSonuclari[33]) * 3.4;//BİYOLOJİ 
                            }                            

                            List <String> ogrenciPuanli = new List<string>();

                            for (int j = 1; j < ogrSonuclari.Length; j++)
                            {
                                ogrenciPuanli.Add(ogrSonuclari[j]);
                            }
                           
                            OgrenciSonuclari ogrenciSonuclari = new OgrenciSonuclari();
                            ogrenciSonuclari.addOgrenciSonuclari(ogrenciPuanli);
                            ogrenciSonuclari.setPuan(puan);
                            ogrenciPuanliSonuclari.Add(ogrenciSonuclari);
                        }

                        List<OgrenciSonuclari> ogrenciSonuclariPuanSirali = ogrenciPuanliSonuclari.OrderBy(o=>o.puan).ToList<OgrenciSonuclari>();
                        ogrenciSonuclariPuanSirali.Reverse();

                        this.pdfyeAktar(ogrencilerPuansiz, ogrenciSonuclariPuanSirali, "TYT PUANI",41);

                    }
                    else if (comboBoxRaporSinavTuru.SelectedIndex == 3)//ayt
                    {
                       
                    }
                    else
                    {
                        MessageBox.Show("Sınav türünü seçiniz!");
                    }
                }
            }
            
        }

        private void RadioButtonRaporHesaplansin_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRaporHesaplansin.Checked)
            {
                labelRaporSinavTuru.Visible = true;
                comboBoxRaporSinavTuru.Visible = true;
            }
            else
            {
                labelRaporSinavTuru.Visible = false;
                comboBoxRaporSinavTuru.Visible = false;
            }
        }

        private void RadioButtonRaporHesaplanmasin_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRaporHesaplanmasin.Checked)
            {
                labelRaporSinavTuru.Visible = false;
                comboBoxRaporSinavTuru.Visible = false;
            }
            else
            {
                labelRaporSinavTuru.Visible = true;
                comboBoxRaporSinavTuru.Visible = true;
            }
        }

        private void pdfyeAktar(List<String> ogrencilerPuansiz, List<OgrenciSonuclari> ogrenciSonuclariPuanSirali, String puanTuruAdi, int colspan) {
            /// PDFYE AKTARMA/////
            /// 
            String[] ogrBilgileri = ogrencilerPuansiz[0].Split('|');
            BaseFont arial = BaseFont.CreateFont("C:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            iTextSharp.text.Font font = new iTextSharp.text.Font(arial, 6, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font başlıkfont = new iTextSharp.text.Font(arial, 10, iTextSharp.text.Font.NORMAL);

            PdfPTable tablo = new PdfPTable(1);
            tablo.TotalWidth = 800f;
            tablo.LockedWidth = true;

            PdfPCell başlık = new PdfPCell(new Phrase(textBox1.Text, başlıkfont));
            başlık.HorizontalAlignment = 1;
            başlık.PaddingBottom = 20f;
            başlık.Border = 0;
            tablo.AddCell(başlık);

            ///ilk satır///
            PdfPTable tableOgrenci = new PdfPTable(colspan);
            PdfPCell satır = null;
            int index = -1;
            for (int i = 0; i < 7; i++)
            {
                if (ogrBilgileri[i].Equals("&&&"))
                {
                    index = i;

                    satır = new PdfPCell(new Phrase(" ", font));
                    satır.HorizontalAlignment = 1;
                    tableOgrenci.AddCell(satır);
                }
                else if (ogrBilgileri[i].Equals("Ad-Soyad"))
                {
                    satır = new PdfPCell(new Phrase(ogrBilgileri[i], font));
                    satır.HorizontalAlignment = 1;
                    satır.Colspan = 3;
                    tableOgrenci.AddCell(satır);
                }
                else
                {
                    satır = new PdfPCell(new Phrase(ogrBilgileri[i], font));
                    satır.HorizontalAlignment = 1;
                    tableOgrenci.AddCell(satır);
                }
            }

            for (int i = 7; i < ogrBilgileri.Length-3; i += 3)
            {
                satır = new PdfPCell(new Phrase(ogrBilgileri[i], font));
                satır.HorizontalAlignment = 1;
                satır.Colspan = 3;
                tableOgrenci.AddCell(satır);
            }

            satır = new PdfPCell(new Phrase(" ", font));
            satır.HorizontalAlignment = 1;
            tableOgrenci.AddCell(satır);

            satır = new PdfPCell(new Phrase(puanTuruAdi, font));
            satır.HorizontalAlignment = 1;
            tableOgrenci.AddCell(satır);

            PdfPCell cellOgrenci = new PdfPCell(tableOgrenci);
            tablo.AddCell(cellOgrenci);

            //DYN ler satırı////
            PdfPTable tabloDYN = new PdfPTable(colspan);

            satır = new PdfPCell(new Phrase(" ", font));
            satır.HorizontalAlignment = 1;
            tabloDYN.AddCell(satır);

            satır = new PdfPCell(new Phrase(" ", font));
            satır.HorizontalAlignment = 1;
            satır.Colspan = 3;
            tabloDYN.AddCell(satır);

            for (int i = 0; i < 5; i++)
            {
                satır = new PdfPCell(new Phrase(" ", font));
                satır.HorizontalAlignment = 1;
                tabloDYN.AddCell(satır);
            }

            for (int i = 0; i < ogrBilgileri.Length-8; i += 3)
            {
                satır = new PdfPCell(new Phrase("D", font));
                satır.HorizontalAlignment = 1;
                tabloDYN.AddCell(satır);

                satır = new PdfPCell(new Phrase("Y", font));
                satır.HorizontalAlignment = 1;
                tabloDYN.AddCell(satır);

                satır = new PdfPCell(new Phrase("N", font));
                satır.HorizontalAlignment = 1;
                tabloDYN.AddCell(satır);
            }

            for (int i = 0; i < 2; i++)
            {
                satır = new PdfPCell(new Phrase(" ", font));
                satır.HorizontalAlignment = 1;
                tabloDYN.AddCell(satır);
            }

            PdfPCell cellDYN = new PdfPCell(tabloDYN);
            tablo.AddCell(cellDYN);

            ///ogrenci sonuclari////
            ///
            int sırasayısı = 0;
            for (int i = 0; i < ogrenciSonuclariPuanSirali.Count; i++)
            {
                PdfPTable tableOgr = new PdfPTable(colspan);
                List<String> ogrSonuclari = ogrenciSonuclariPuanSirali[i].getOgrenciSonuclari();

                sırasayısı++;
                PdfPCell sıra = new PdfPCell(new Phrase(sırasayısı.ToString(), font));
                tableOgr.AddCell(sıra);

                satır = new PdfPCell(new Phrase(ogrSonuclari[0], font));
                satır.Colspan = 3;
                tableOgr.AddCell(satır);

                for (int j = 1; j < ogrSonuclari.Count; j++)
                {
                    satır = new PdfPCell(new Phrase(ogrSonuclari[j], font));
                    satır.HorizontalAlignment = 1;
                    tableOgr.AddCell(satır);
                }

                satır = new PdfPCell(new Phrase(ogrenciSonuclariPuanSirali[i].getPuan().ToString(), font));
                satır.HorizontalAlignment = 1;
                tableOgr.AddCell(satır);

                PdfPCell cellOgr = new PdfPCell(tableOgr);
                tablo.AddCell(cellOgr);
            }

            ///PDF kaydet///
            String s_dosyaadi = textBox1.Text.ToString() + ".pdf";
            SaveFileDialog kaydet = new SaveFileDialog();
            kaydet.FileName = s_dosyaadi;
            kaydet.Filter = "PDF Dosyası|.pdf";
            if (kaydet.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(kaydet.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 30f, 10f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(tablo);
                    pdfDoc.Close();
                    stream.Close();
                }
            }
        }
    }
}
