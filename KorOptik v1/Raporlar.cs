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

                if (radioButtonRaporHesaplanmasin.Checked)
                {
                    String[] ogrBilgileri = ogrencilerPuansiz[0].Split('|');

                    int indexStdAlanlar = 0;
                    for (int s = 0; s < ogrBilgileri.Length; s++)
                    {
                        if (ogrBilgileri[s].Equals("&&&"))
                        {
                            indexStdAlanlar = s;
                        }
                    }

                    BaseFont arial = BaseFont.CreateFont("C:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                    iTextSharp.text.Font font = new iTextSharp.text.Font(arial, 6, iTextSharp.text.Font.NORMAL);
                    iTextSharp.text.Font başlıkfont = new iTextSharp.text.Font(arial, 10, iTextSharp.text.Font.NORMAL);

                    PdfPTable tablo = new PdfPTable(1);
                    tablo.TotalWidth = 820f;
                    tablo.LockedWidth = true;

                    PdfPCell başlık = new PdfPCell(new Phrase(textBox1.Text, başlıkfont));
                    başlık.HorizontalAlignment = 1;
                    başlık.PaddingBottom = 20f;
                    başlık.Border = 0;
                    tablo.AddCell(başlık);

                    int colspan = 0;

                    for (int i = 0; i < indexStdAlanlar; i++)
                    {
                        if (ogrBilgileri[i].Equals("Ad-Soyad"))
                        {
                            colspan += 3;
                        }
                        /*else if (ogrBilgileri[i].Equals("Okul Kodu"))
                        {
                        }*/
                        else if (ogrBilgileri[i].Equals("Kitapçık Türü"))
                        {
                        }
                        else
                        {
                            colspan += 1;
                        }
                    }
                    for (int i = indexStdAlanlar + 1; i < ogrBilgileri.Length - 3; i += 3)
                    {
                        colspan += 3;
                    }

                    PdfPTable tableOgrenci = new PdfPTable(colspan);
                    tableOgrenci.TotalWidth = 820f;
                    tableOgrenci.LockedWidth = true;
                    PdfPCell satır = null;

                    ///ilk satır///
                    for (int i = 0; i < indexStdAlanlar; i++)
                    {
                        if (ogrBilgileri[i].Equals("Ad-Soyad"))
                        {
                            satır = new PdfPCell(new Phrase(ogrBilgileri[i], font));
                            satır.HorizontalAlignment = 1;
                            satır.Colspan = 3;
                            tableOgrenci.AddCell(satır);
                        }
                       /* else if (ogrBilgileri[i].Equals("Okul Kodu"))
                        {
                        }*/
                        else if (ogrBilgileri[i].Equals("Kitapçık Türü"))
                        {
                        }
                        else
                        {
                            satır = new PdfPCell(new Phrase(ogrBilgileri[i], font));
                            satır.HorizontalAlignment = 1;
                            tableOgrenci.AddCell(satır);
                        }
                    }

                    for (int i = indexStdAlanlar + 1; i < ogrBilgileri.Length - 3; i += 3)
                    {
                        satır = new PdfPCell(new Phrase(ogrBilgileri[i], font));
                        satır.HorizontalAlignment = 1;
                        satır.Colspan = 3;
                        tableOgrenci.AddCell(satır);
                    }

                    PdfPCell pdfPCell = new PdfPCell(tableOgrenci);
                    tablo.AddCell(pdfPCell);

                    tableOgrenci = new PdfPTable(colspan);

                    ///DYNler satırı/////
                    for (int i = 0; i < indexStdAlanlar; i++)
                    {
                        if (ogrBilgileri[i].Equals("Ad-Soyad"))
                        {
                            satır = new PdfPCell(new Phrase("", font));
                            satır.HorizontalAlignment = 1;
                            satır.Colspan = 3;
                            tableOgrenci.AddCell(satır);
                        }
                       /* else if (ogrBilgileri[i].Equals("Okul Kodu"))
                        {
                        }*/
                        else if (ogrBilgileri[i].Equals("Kitapçık Türü"))
                        {
                        }
                        else
                        {
                            satır = new PdfPCell(new Phrase("", font));
                            satır.HorizontalAlignment = 1;
                            tableOgrenci.AddCell(satır);
                        }
                    }

                    for (int i = indexStdAlanlar + 1; i < ogrBilgileri.Length - 3; i += 3)
                    {
                        satır = new PdfPCell(new Phrase("D", font));
                        satır.HorizontalAlignment = 1;
                        tableOgrenci.AddCell(satır);

                        satır = new PdfPCell(new Phrase("Y", font));
                        satır.HorizontalAlignment = 1;
                        tableOgrenci.AddCell(satır);

                        satır = new PdfPCell(new Phrase("N", font));
                        satır.HorizontalAlignment = 1;
                        tableOgrenci.AddCell(satır);
                    }

                    pdfPCell = new PdfPCell(tableOgrenci);
                    tablo.AddCell(pdfPCell);

                    ////sonuçlar/////

                    List<OgrenciSonuclari> ogrencilerSiralamasiz = new List<OgrenciSonuclari>();

                    for (int i = 1; i < ogrencilerPuansiz.Count; i++)
                    {
                        String[] ogrBilg = ogrencilerPuansiz[i].Split('|');

                        List<string> ogrenciBil = new List<string>();

                        for (int j = 0; j < ogrBilg.Length - 1; j++)
                        {
                            ogrenciBil.Add(ogrBilg[j]);
                        }

                        Double toplamNet = Convert.ToDouble(ogrenciBil[ogrenciBil.Count - 1]);

                        OgrenciSonuclari ogrenci = new OgrenciSonuclari();
                        ogrenci.addOgrenciSonuclari(ogrenciBil);
                        ogrenci.setPuan(toplamNet);

                        ogrencilerSiralamasiz.Add(ogrenci);
                    }

                    List<OgrenciSonuclari> ogrenciSonuclariSirali = ogrencilerSiralamasiz.OrderBy(o => o.puan).ToList<OgrenciSonuclari>();
                    ogrenciSonuclariSirali.Reverse();

                    ogrBilgileri = ogrencilerPuansiz[0].Split('|');

                    for (int i = 0; i < ogrenciSonuclariSirali.Count; i++)
                    {
                        tableOgrenci = new PdfPTable(colspan);

                        List<string> yazdırılacaklar = new List<string>();
                        yazdırılacaklar = ogrenciSonuclariSirali[i].getOgrenciSonuclari();

                        satır = new PdfPCell(new Phrase((i + 1).ToString(), font));
                        satır.HorizontalAlignment = 1;
                        tableOgrenci.AddCell(satır);

                        for (int j = 0; j < indexStdAlanlar; j++)
                        {
                            if (ogrBilgileri[j].Equals("Ad-Soyad"))
                            {
                                satır = new PdfPCell(new Phrase(yazdırılacaklar[j], font));
                                //satır.HorizontalAlignment = 1;
                                satır.Colspan = 3;
                                tableOgrenci.AddCell(satır);
                            }
                            /*else if (ogrBilgileri[j].Equals("Okul Kodu"))
                            {
                            }*/
                            else if (ogrBilgileri[j].Equals("Kitapçık Türü"))
                            {
                            }
                            else if (ogrBilgileri[j].Equals("Sıra"))
                            {
                            }
                            else
                            {
                                satır = new PdfPCell(new Phrase(yazdırılacaklar[j], font));
                                satır.HorizontalAlignment = 1;
                                tableOgrenci.AddCell(satır);
                            }
                        }

                        for (int j = indexStdAlanlar + 1; j < yazdırılacaklar.Count; j++)
                        {
                            satır = new PdfPCell(new Phrase(yazdırılacaklar[j], font));
                            satır.HorizontalAlignment = 1;
                            tableOgrenci.AddCell(satır);
                        }

                        pdfPCell = new PdfPCell(tableOgrenci);
                        tablo.AddCell(pdfPCell);
                    }

                    //ortalamalar//
                    List<string> list = new List<string>();
                    list = ogrencilerSiralamasiz[0].getOgrenciSonuclari();

                    int satirSay = ogrenciSonuclariSirali.Count;
                    int sutunSay = list.Count - indexStdAlanlar - 1;

                    double[,] dizi = new double[satirSay, sutunSay];

                    for (int i = 0; i < ogrenciSonuclariSirali.Count; i++)
                    {                     
                        List<string> yazdırılacaklar = new List<string>();
                        yazdırılacaklar = ogrenciSonuclariSirali[i].getOgrenciSonuclari();                    

                        for (int j = indexStdAlanlar  + 1; j < yazdırılacaklar.Count; j++)
                        {
                            dizi[i, j- indexStdAlanlar - 1] = Convert.ToDouble(yazdırılacaklar[j]);
                        }
                    }
                  
                    
                    List<double> ortalamalar = new List<double>();

                    for (int i = 0; i < sutunSay; i++)
                    {
                        double toplam = 0;

                        for (int j = 0; j < satirSay; j++)
                        {
                             toplam+=dizi[j, i];
                        }

                        double ortalama = Math.Round(toplam / satirSay,2);
                        ortalamalar.Add(ortalama);
                    }

                    tableOgrenci = new PdfPTable(1);
                    satır = new PdfPCell(new Phrase("ORTALAMALAR",font));
                    satır.HorizontalAlignment = 1;
                    satır.PaddingTop = 20f;
                    tableOgrenci.AddCell(satır);

                    satır = new PdfPCell(tableOgrenci);
                    tablo.AddCell(satır);

                    tableOgrenci = new PdfPTable(sutunSay);
                    for(int i = indexStdAlanlar + 1; i < ogrBilgileri.Count()-3; i+=3)
                    {
                        satır = new PdfPCell(new Phrase(ogrBilgileri[i],font));
                        satır.Colspan = 3;
                        satır.HorizontalAlignment = 1;
                        tableOgrenci.AddCell(satır);
                    }

                    for (int i = indexStdAlanlar + 1; i < ogrBilgileri.Count() - 3; i += 3)
                    {
                        satır = new PdfPCell(new Phrase("D", font));                       
                        satır.HorizontalAlignment = 1;
                        tableOgrenci.AddCell(satır);
                        satır = new PdfPCell(new Phrase("Y", font));
                        satır.HorizontalAlignment = 1;
                        tableOgrenci.AddCell(satır);
                        satır = new PdfPCell(new Phrase("N", font));
                        satır.HorizontalAlignment = 1;
                        tableOgrenci.AddCell(satır);
                    }

                    for (int i = 0; i < ortalamalar.Count; i++)
                    {
                        satır = new PdfPCell(new Phrase(ortalamalar[i].ToString(), font));
                        satır.HorizontalAlignment = 1;
                        tableOgrenci.AddCell(satır);
                    }

                    satır = new PdfPCell(tableOgrenci);
                    tablo.AddCell(satır);

                    string dosyaadi = comboBoxRaporSınavAdi.SelectedItem.ToString().Split('.')[0] + ".pdf";
                    string yolKayit=pfdKaydetDialogla(dosyaadi, tablo);

                    //GRUPLAMA//
                    List<OgrenciOkunan> gruplanacakOgrenciler = new List<OgrenciOkunan>();

                    for (int i = 0; i < ogrenciSonuclariSirali.Count; i++)
                    {
                        OgrenciOkunan gruplanacakOgrenci = new OgrenciOkunan();

                        List<string> yazdırılacaklar = new List<string>();
                        yazdırılacaklar = ogrenciSonuclariSirali[i].getOgrenciSonuclari();
                      
                        for (int j = 0; j < indexStdAlanlar; j++)
                        {
                            if (ogrBilgileri[j].Equals("Ad-Soyad"))
                            {
                                gruplanacakOgrenci.setAdSoyad(yazdırılacaklar[j]);
                            }
                            else if (ogrBilgileri[j].Equals("Öğrenci No"))
                            {
                                gruplanacakOgrenci.setOgrenciNo(yazdırılacaklar[j]);
                            }
                            else if (ogrBilgileri[j].Equals("Sınıf-Şube"))
                            {
                                gruplanacakOgrenci.setSinifSube(yazdırılacaklar[j]);
                            }
                            else if (ogrBilgileri[j].Equals("Kitapçık Türü"))
                            {
                                gruplanacakOgrenci.setKitapcikTuru(yazdırılacaklar[j]);
                            }
                            else if (ogrBilgileri[j].Equals("Okul Kodu"))
                            {
                                gruplanacakOgrenci.setOkulKodu(yazdırılacaklar[j]);
                            }                                                  
                        }

                        List<string> ogrDersSonuclari = new List<string>();

                        for (int j = indexStdAlanlar + 1; j < yazdırılacaklar.Count; j++)
                        {
                            ogrDersSonuclari.Add(yazdırılacaklar[j].ToString());
                        }

                        gruplanacakOgrenci.addOkunanCevaplar(ogrDersSonuclari);
                        gruplanacakOgrenciler.Add(gruplanacakOgrenci);
                    }

                    var ogrencilerGroupByOkul = gruplanacakOgrenciler.OrderBy(Ogrenci => Ogrenci.okulkodu)
                                                                            .GroupBy(Ogrenci => Ogrenci.okulkodu);                                                                          

                    var ogrencilerGroupBySinif = gruplanacakOgrenciler.OrderBy(Ogrenci => Ogrenci.okulkodu)
                                                                            .GroupBy(Ogrenci => Ogrenci.okulkodu)
                                                                            .Select(OkulGrubu =>new
                        {
                            OkulKodu = OkulGrubu.Key,
                            SınıfGrubu =
                                  OkulGrubu.OrderBy(BuSiniftakiOgrenci => BuSiniftakiOgrenci.sinifSube)
                                            .GroupBy(BuSiniftakiOgrenci => BuSiniftakiOgrenci.sinifSube)
                        });

                    //OKUL OKUL YAZDIRMA//

                    //Sınıf sınıf yazdırma//

                    PdfPTable tableTumOkullar = new PdfPTable(1);
                    tableTumOkullar.TotalWidth = 820f;
                    tableTumOkullar.LockedWidth = true;                  

                    foreach (var Okul in ogrencilerGroupBySinif)
                    {
                        string path = yolKayit.Split('.')[0] + " " + Okul.OkulKodu + " Şubeler.pdf";
                        FileStream stream = new FileStream(path, FileMode.Create);
                        Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 30f, 10f);
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();

                        foreach (var SınıfGrubu in Okul.SınıfGrubu)
                        {
                            PdfPTable tableSinif = new PdfPTable(1);
                            tableSinif.TotalWidth = 820f;
                            tableSinif.LockedWidth = true;
                          
                            satır = new PdfPCell(new Phrase(SınıfGrubu.Key+" SINIFI "+textBox1.Text , font));
                            satır.HorizontalAlignment = 1;
                            tableSinif.AddCell(satır);

                            tableOgrenci = new PdfPTable(colspan);

                            for (int i = 0; i < indexStdAlanlar; i++)
                            {
                                if (ogrBilgileri[i].Equals("Ad-Soyad"))
                                {
                                    satır = new PdfPCell(new Phrase(ogrBilgileri[i], font));
                                    satır.HorizontalAlignment = 1;
                                    satır.Colspan = 3;
                                    tableOgrenci.AddCell(satır);
                                }
                                 else if (ogrBilgileri[i].Equals("Okul Kodu"))
                                 {
                                 }                              
                                else if (ogrBilgileri[i].Equals("Kitapçık Türü"))
                                {
                                }
                                else if (ogrBilgileri[i].Equals("Sınıf-Şube"))
                                {
                                }
                                else
                                {
                                    satır = new PdfPCell(new Phrase(ogrBilgileri[i], font));
                                    satır.HorizontalAlignment = 1;
                                    tableOgrenci.AddCell(satır);
                                }
                            }

                            for (int i = indexStdAlanlar + 1; i < ogrBilgileri.Count() - 3; i += 3)
                            {
                                satır = new PdfPCell(new Phrase(ogrBilgileri[i], font));
                                satır.Colspan = 3;
                                satır.HorizontalAlignment = 1;
                                tableOgrenci.AddCell(satır);
                            }

                            satır = new PdfPCell(new Phrase("Sıralama", font));
                            satır.Colspan = 2;
                            satır.HorizontalAlignment = 1;
                            tableOgrenci.AddCell(satır);

                            for (int i = 0; i < indexStdAlanlar; i++)
                            {
                                if (ogrBilgileri[i].Equals("Ad-Soyad"))
                                {
                                    satır = new PdfPCell(new Phrase("", font));
                                    satır.HorizontalAlignment = 1;
                                    satır.Colspan = 3;
                                    tableOgrenci.AddCell(satır);
                                }
                                else if (ogrBilgileri[i].Equals("Okul Kodu"))
                                {
                                }                              
                                else if (ogrBilgileri[i].Equals("Kitapçık Türü"))
                                {
                                }
                                else if (ogrBilgileri[i].Equals("Sınıf-Şube"))
                                {
                                }
                                else
                                {
                                    satır = new PdfPCell(new Phrase("", font));
                                    satır.HorizontalAlignment = 1;
                                    tableOgrenci.AddCell(satır);
                                }
                            }

                            for (int i = indexStdAlanlar + 1; i < ogrBilgileri.Count() - 3; i += 3)
                            {
                                satır = new PdfPCell(new Phrase("D", font));
                                satır.HorizontalAlignment = 1;
                                tableOgrenci.AddCell(satır);
                                satır = new PdfPCell(new Phrase("Y", font));
                                satır.HorizontalAlignment = 1;
                                tableOgrenci.AddCell(satır);
                                satır = new PdfPCell(new Phrase("N", font));
                                satır.HorizontalAlignment = 1;
                                tableOgrenci.AddCell(satır);
                            }
                            satır = new PdfPCell(new Phrase("Okul", font));
                            satır.HorizontalAlignment = 1;
                            tableOgrenci.AddCell(satır);

                            satır = new PdfPCell(new Phrase("Genel", font));
                            satır.HorizontalAlignment = 1;
                            tableOgrenci.AddCell(satır);

                            int sıraSınıf = 0;
                            foreach (var Ogrenci in SınıfGrubu)
                            {
                                sıraSınıf++;
                                satır = new PdfPCell(new Phrase(sıraSınıf.ToString(), font));
                                satır.HorizontalAlignment = 1;
                                tableOgrenci.AddCell(satır);

                                if (!Ogrenci.adSoyad.Equals("")) 
                                {
                                    satır = new PdfPCell(new Phrase(Ogrenci.adSoyad, font));
                                    satır.Colspan = 3;
                                    //satır.HorizontalAlignment = 1;
                                    tableOgrenci.AddCell(satır);
                                }
                                 if (!Ogrenci.ogrenciNo.Equals(""))
                                {
                                    satır = new PdfPCell(new Phrase(Ogrenci.ogrenciNo, font));                                  
                                    satır.HorizontalAlignment = 1;
                                    tableOgrenci.AddCell(satır);
                                }                                 

                                foreach (var sonuclar in Ogrenci.okunanCevaplar) {
                                    satır = new PdfPCell(new Phrase(sonuclar, font));
                                    satır.HorizontalAlignment = 1;
                                    tableOgrenci.AddCell(satır);
                                }

                                int sıraGenel = 0;
                                int sayi = 0;
                                foreach (OgrenciOkunan ogrenciOkunan in gruplanacakOgrenciler) {
                                    sayi++;
                                    if(Convert.ToInt32(Ogrenci.okulkodu)== Convert.ToInt32(ogrenciOkunan.getOkulKodu())
                                        && Convert.ToInt32(Ogrenci.ogrenciNo)== Convert.ToInt32(ogrenciOkunan.getOgrenciNo()))
                                    {
                                        sıraGenel = sayi;
                                    }
                                }

                                int sıraOkul = 0;
                                sayi = 0;

                                foreach (var school in ogrencilerGroupByOkul) {
                                    if (Convert.ToInt32(school.Key) == Convert.ToInt32(Ogrenci.okulkodu) ){
                                        foreach (var student in school)
                                        {
                                            sayi++;
                                            if (Convert.ToInt32(student.ogrenciNo) == Convert.ToInt32(Ogrenci.ogrenciNo)) {
                                                sıraOkul = sayi;
                                            }
                                        }
                                    }                                   
                                }

                                satır = new PdfPCell(new Phrase(sıraOkul.ToString(), font));
                                satır.HorizontalAlignment = 1;
                                tableOgrenci.AddCell(satır);

                                satır = new PdfPCell(new Phrase(sıraGenel.ToString(), font));
                                satır.HorizontalAlignment = 1;
                                tableOgrenci.AddCell(satır);
                            }
                            satır = new PdfPCell(tableOgrenci);
                            tableSinif.AddCell(satır);

                            //GENEL ORTALAMALAR//
                            tableOgrenci = new PdfPTable(1);
                            satır = new PdfPCell(new Phrase("GENEL ORTALAMALAR", font));
                            satır.HorizontalAlignment = 1;
                            satır.PaddingTop = 20f;
                            tableOgrenci.AddCell(satır);

                            satır = new PdfPCell(tableOgrenci);
                            tableSinif.AddCell(satır);

                            tableOgrenci = new PdfPTable(sutunSay);
                            for (int i = indexStdAlanlar + 1; i < ogrBilgileri.Count() - 3; i += 3)
                            {
                                satır = new PdfPCell(new Phrase(ogrBilgileri[i], font));
                                satır.Colspan = 3;
                                satır.HorizontalAlignment = 1;
                                tableOgrenci.AddCell(satır);
                            }

                            for (int i = indexStdAlanlar + 1; i < ogrBilgileri.Count() - 3; i += 3)
                            {
                                satır = new PdfPCell(new Phrase("D", font));
                                satır.HorizontalAlignment = 1;
                                tableOgrenci.AddCell(satır);
                                satır = new PdfPCell(new Phrase("Y", font));
                                satır.HorizontalAlignment = 1;
                                tableOgrenci.AddCell(satır);
                                satır = new PdfPCell(new Phrase("N", font));
                                satır.HorizontalAlignment = 1;
                                tableOgrenci.AddCell(satır);
                            }

                            for (int i = 0; i < ortalamalar.Count; i++)
                            {
                                satır = new PdfPCell(new Phrase(ortalamalar[i].ToString(), font));
                                satır.HorizontalAlignment = 1;
                                tableOgrenci.AddCell(satır);
                            }

                            satır = new PdfPCell(tableOgrenci);
                            tableSinif.AddCell(satır);

                            //OKUL ORTALAMALARI//

                            pdfDoc.NewPage();

                            pdfDoc.Add(tableSinif);
                        }

                        using (stream)
                        {                          
                            pdfDoc.Close();
                            stream.Close();
                        }
                    }
                   
                }
                else if (radioButtonRaporHesaplansin.Checked)
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

                        PdfPTable tablo = this.pdfyeAktar(ogrencilerPuansiz, ogrenciSonuclariPuanSirali, "PUANI", 26, false);

                        String s_dosyaadi = textBox1.Text.ToString() + ".pdf";
                        pfdKaydetDialogla(s_dosyaadi, tablo);

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

                        PdfPTable tablo = this.pdfyeAktar(ogrencilerPuansiz, ogrenciSonuclariPuanSirali, "LGS PUANI", 32, false);

                        String s_dosyaadi = textBox1.Text.ToString() + ".pdf";
                        pfdKaydetDialogla(s_dosyaadi, tablo);

                    }
                    else if (comboBoxRaporSinavTuru.SelectedIndex == 2)//sadece tyt
                    {
                        List<OgrenciSonuclari> ogrenciPuanliSonuclari = new List<OgrenciSonuclari>();

                        for (int i = 1; i < ogrencilerPuansiz.Count; i++)
                        {
                            String[] ogrSonuclari = ogrencilerPuansiz[i].Split('|');

                            double puan = 0;

                            if (Convert.ToDouble(ogrSonuclari[9]) >= 0.5 || Convert.ToDouble(ogrSonuclari[24]) >= 0.5)//türkçe veya matematik neti 0.5 ten küçükse puan hesabı yapılmaz
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
                                puan = Math.Round(puan, 1);
                            }

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

                        PdfPTable tablo = this.pdfyeAktar(ogrencilerPuansiz, ogrenciSonuclariPuanSirali, "TYT PUANI", 41, false);

                        String s_dosyaadi = textBox1.Text.ToString() + ".pdf";
                        pfdKaydetDialogla(s_dosyaadi, tablo);

                    }
                    else if (comboBoxRaporSinavTuru.SelectedIndex == 3)//ayt
                    {
                        List<OgrenciSonuclari> ogrenciPuanliSonuclari = new List<OgrenciSonuclari>();

                        for (int i = 1; i < ogrencilerPuansiz.Count; i++)
                        {
                            String[] ogrSonuclari = ogrencilerPuansiz[i].Split('|');

                            double puan = 0;
                            double puanSAY = 0;
                            double puanEA = 0;
                            double puanSOZ = 0;

                            //SAY PUANI
                            double MAT2neti = Convert.ToDouble(ogrSonuclari[57]);
                            double FEN2neti = Convert.ToDouble(ogrSonuclari[60]) +//AYT-FİZİK
                                             Convert.ToDouble(ogrSonuclari[63]) +//AYT-KİMYA
                                             Convert.ToDouble(ogrSonuclari[66]);//AYT-BİYOLOJİ

                            if (Convert.ToDouble(ogrSonuclari[9]) >= 0.5 || Convert.ToDouble(ogrSonuclari[24]) >= 0.5)//türkçe veya matematik neti 0.5 ten küçükse puan hesabı yapılmaz
                            {
                                if (MAT2neti >= 0.5 || FEN2neti >= 0.5)//mat2 veya fen2 neti 0.5 ten küçükse puan hesabı yapılmaz
                                {
                                    puanSAY = 100 + Convert.ToDouble(ogrSonuclari[9]) * 1.32 +//TYT-TÜRKCE
                                                 Convert.ToDouble(ogrSonuclari[12]) * 1.36 +//TYT-TARİH
                                                 Convert.ToDouble(ogrSonuclari[15]) * 1.36 +//TYT-COĞRAFYA
                                                 Convert.ToDouble(ogrSonuclari[18]) * 1.36 +//TYT-FELSEFE
                                                 Convert.ToDouble(ogrSonuclari[21]) * 1.36 +//TYT-DİN
                                                 Convert.ToDouble(ogrSonuclari[24]) * 1.32 +//TYT-MATEMATİK
                                                 Convert.ToDouble(ogrSonuclari[27]) * 1.36 +//TYT-FİZİK
                                                 Convert.ToDouble(ogrSonuclari[30]) * 1.36 +//TYT-KİMYA
                                                 Convert.ToDouble(ogrSonuclari[33]) * 1.36 +//TYT-BİYOLOJİ 

                                                 Convert.ToDouble(ogrSonuclari[57]) * 3.0 +//AYT-MATEMATİK
                                                 Convert.ToDouble(ogrSonuclari[60]) * 2.85 +//AYT-FİZİK
                                                 Convert.ToDouble(ogrSonuclari[63]) * 3.07 +//AYT-KİMYA
                                                 Convert.ToDouble(ogrSonuclari[66]) * 3.07;//AYT-BİYOLOJİ 

                                    puanSAY = Math.Round(puanSAY, 1);
                                }
                            }

                            //EA PUANI                            
                            double TDE_SB_1_neti = Convert.ToDouble(ogrSonuclari[36]) +//AYT-TDE
                                                   Convert.ToDouble(ogrSonuclari[39]) +//AYT-TARİH1
                                                   Convert.ToDouble(ogrSonuclari[42]);//AYT-COĞRAFYA1

                            if (Convert.ToDouble(ogrSonuclari[9]) >= 0.5 || Convert.ToDouble(ogrSonuclari[24]) >= 0.5)//türkçe veya matematik neti 0.5 ten küçükse puan hesabı yapılmaz
                            {
                                if (MAT2neti >= 0.5 || TDE_SB_1_neti >= 0.5)//mat2 veya TDE_SB_1_neti neti 0.5 ten küçükse puan hesabı yapılmaz
                                {
                                    puanEA = 100 + Convert.ToDouble(ogrSonuclari[9]) * 1.32 +//TYT-TÜRKCE
                                                 Convert.ToDouble(ogrSonuclari[12]) * 1.36 +//TYT-TARİH
                                                 Convert.ToDouble(ogrSonuclari[15]) * 1.36 +//TYT-COĞRAFYA
                                                 Convert.ToDouble(ogrSonuclari[18]) * 1.36 +//TYT-FELSEFE
                                                 Convert.ToDouble(ogrSonuclari[21]) * 1.36 +//TYT-DİN
                                                 Convert.ToDouble(ogrSonuclari[24]) * 1.32 +//TYT-MATEMATİK
                                                 Convert.ToDouble(ogrSonuclari[27]) * 1.36 +//TYT-FİZİK
                                                 Convert.ToDouble(ogrSonuclari[30]) * 1.36 +//TYT-KİMYA
                                                 Convert.ToDouble(ogrSonuclari[33]) * 1.36 +//TYT-BİYOLOJİ 

                                                 Convert.ToDouble(ogrSonuclari[36]) * 3.0 +//AYT-TDE
                                                 Convert.ToDouble(ogrSonuclari[39]) * 2.8 +//AYT-TARİH1
                                                 Convert.ToDouble(ogrSonuclari[42]) * 3.33 +//AYT-COĞRAFYA1                                                 
                                                 Convert.ToDouble(ogrSonuclari[57]) * 3.0;//AYT-MATEMATİK   

                                    puanEA = Math.Round(puanEA, 1);
                                }
                            }

                            //SOZ PUANI                            
                            double SB2neti = Convert.ToDouble(ogrSonuclari[45]) +//AYT-TARİH2
                                             Convert.ToDouble(ogrSonuclari[48]) +//AYT-COĞRAFYA2
                                             Convert.ToDouble(ogrSonuclari[51]) +//AYT-FELSEFE
                                             Convert.ToDouble(ogrSonuclari[54]);//AYT-DİN

                            if (Convert.ToDouble(ogrSonuclari[9]) >= 0.5 || Convert.ToDouble(ogrSonuclari[24]) >= 0.5)//türkçe veya matematik neti 0.5 ten küçükse puan hesabı yapılmaz
                            {
                                if (TDE_SB_1_neti >= 0.5 || SB2neti >= 0.5)//mat2 veya SB2neti neti 0.5 ten küçükse puan hesabı yapılmaz
                                {
                                    puanSOZ = 100 + Convert.ToDouble(ogrSonuclari[9]) * 1.32 +//TYT-TÜRKCE
                                                 Convert.ToDouble(ogrSonuclari[12]) * 1.36 +//TYT-TARİH
                                                 Convert.ToDouble(ogrSonuclari[15]) * 1.36 +//TYT-COĞRAFYA
                                                 Convert.ToDouble(ogrSonuclari[18]) * 1.36 +//TYT-FELSEFE
                                                 Convert.ToDouble(ogrSonuclari[21]) * 1.36 +//TYT-DİN
                                                 Convert.ToDouble(ogrSonuclari[24]) * 1.32 +//TYT-MATEMATİK
                                                 Convert.ToDouble(ogrSonuclari[27]) * 1.36 +//TYT-FİZİK
                                                 Convert.ToDouble(ogrSonuclari[30]) * 1.36 +//TYT-KİMYA
                                                 Convert.ToDouble(ogrSonuclari[33]) * 1.36 +//TYT-BİYOLOJİ 

                                                 Convert.ToDouble(ogrSonuclari[36]) * 3.0 +//AYT-TDE
                                                 Convert.ToDouble(ogrSonuclari[39]) * 2.8 +//AYT-TARİH1
                                                 Convert.ToDouble(ogrSonuclari[42]) * 3.33 +//AYT-COĞRAFYA1  
                                                 Convert.ToDouble(ogrSonuclari[45]) * 2.91 +//AYT-TARİH2
                                                 Convert.ToDouble(ogrSonuclari[48]) * 2.91 +//AYT-COĞRAFYA2
                                                 Convert.ToDouble(ogrSonuclari[51]) * 3.0 +//AYT-FELSEFE
                                                 Convert.ToDouble(ogrSonuclari[54]) * 3.33;//AYT-DİN 

                                    puanSOZ = Math.Round(puanSOZ, 1);
                                }
                            }

                            List<String> ogrenciPuanli = new List<string>();

                            for (int j = 1; j < ogrSonuclari.Length; j++)
                            {
                                ogrenciPuanli.Add(ogrSonuclari[j]);
                            }

                            OgrenciSonuclari ogrenciSonuclari = new OgrenciSonuclari();
                            ogrenciSonuclari.addOgrenciSonuclari(ogrenciPuanli);
                            ogrenciSonuclari.setPuan(puan);
                            ogrenciSonuclari.setPuanSAY(puanSAY);
                            ogrenciSonuclari.setPuanEA(puanEA);
                            ogrenciSonuclari.setPuanSOZ(puanSOZ);
                            ogrenciPuanliSonuclari.Add(ogrenciSonuclari);
                        }

                        List<OgrenciSonuclari> ogrenciSonuclariPuanSirali = ogrenciPuanliSonuclari.OrderBy(o => o.puanSAY).ToList<OgrenciSonuclari>();
                        ogrenciSonuclariPuanSirali.Reverse();

                        PdfPTable tablo = this.pdfyeAktar(ogrencilerPuansiz, ogrenciSonuclariPuanSirali, "TYT PUANI", 42, true);

                        String s_dosyaadi = textBox1.Text.ToString() + "(SAY Puan Sıralı)" + ".pdf";
                        string dizin = pfdKaydetDialogla(s_dosyaadi, tablo);

                        ogrenciSonuclariPuanSirali.Clear();

                        ogrenciSonuclariPuanSirali = ogrenciPuanliSonuclari.OrderBy(o => o.puanEA).ToList<OgrenciSonuclari>();
                        ogrenciSonuclariPuanSirali.Reverse();

                        tablo = this.pdfyeAktar(ogrencilerPuansiz, ogrenciSonuclariPuanSirali, "TYT PUANI", 42, true);
                        pdfKaydet(dizin + "(EA Puan Sıralı).pdf", tablo);

                        ogrenciSonuclariPuanSirali.Clear();

                        ogrenciSonuclariPuanSirali = ogrenciPuanliSonuclari.OrderBy(o => o.puanSOZ).ToList<OgrenciSonuclari>();
                        ogrenciSonuclariPuanSirali.Reverse();

                        tablo = this.pdfyeAktar(ogrencilerPuansiz, ogrenciSonuclariPuanSirali, "TYT PUANI", 42, true);
                        pdfKaydet(dizin + "(SOZ Puan Sıralı).pdf", tablo);

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

        private PdfPTable pdfyeAktar(List<String> ogrencilerPuansiz, List<OgrenciSonuclari> ogrenciSonuclariPuanSirali, String puanTuruAdi, int colspan, bool YKSmi) {
            /// PDFYE AKTARMA/////
            /// 
            String[] ogrBilgileri = ogrencilerPuansiz[0].Split('|');
            BaseFont arial = BaseFont.CreateFont("C:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            iTextSharp.text.Font font = new iTextSharp.text.Font(arial, 6, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font başlıkfont = new iTextSharp.text.Font(arial, 10, iTextSharp.text.Font.NORMAL);

            PdfPTable tablo = new PdfPTable(1);
            tablo.TotalWidth = 820f;
            tablo.LockedWidth = true;

            PdfPCell başlık = new PdfPCell(new Phrase(textBox1.Text, başlıkfont));
            başlık.HorizontalAlignment = 1;
            başlık.PaddingBottom = 20f;
            başlık.Border = 0;
            tablo.AddCell(başlık);

            ///ilk satır///
            PdfPTable tableOgrenci = new PdfPTable(colspan);
            PdfPCell satır = null;

            for (int i = 0; i < 4; i++)
            {                
                 if (ogrBilgileri[i].Equals("Ad-Soyad"))
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

            if (YKSmi==false)
            {
                satır = new PdfPCell(new Phrase(ogrBilgileri[4], font));
                satır.HorizontalAlignment = 1;
                tableOgrenci.AddCell(satır);

                satır = new PdfPCell(new Phrase(ogrBilgileri[5], font));
                satır.HorizontalAlignment = 1;
                tableOgrenci.AddCell(satır);

                satır = new PdfPCell(new Phrase(" ", font));
                satır.HorizontalAlignment = 1;
                tableOgrenci.AddCell(satır);
            }

            if (YKSmi == true)
            {
                for (int i = 34; i < ogrBilgileri.Length - 3; i += 3)
                {
                    satır = new PdfPCell(new Phrase(ogrBilgileri[i], font));
                    satır.HorizontalAlignment = 1;
                    satır.Colspan = 3;
                    tableOgrenci.AddCell(satır);
                }
            }
            else
            {
                for (int i = 7; i < ogrBilgileri.Length - 3; i += 3)
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
            }
                                   
            if (YKSmi == true)
            {
                satır = new PdfPCell(new Phrase("SAY", font));
                satır.HorizontalAlignment = 1;
                tableOgrenci.AddCell(satır);

                satır = new PdfPCell(new Phrase("EA", font));
                satır.HorizontalAlignment = 1;
                tableOgrenci.AddCell(satır);

                satır = new PdfPCell(new Phrase("SOZ", font));
                satır.HorizontalAlignment = 1;
                tableOgrenci.AddCell(satır);
            }

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
           
            if (YKSmi == true)
            {
                for (int i = 0; i < 2; i++)
                {
                    satır = new PdfPCell(new Phrase(" ", font));
                    satır.HorizontalAlignment = 1;
                    tabloDYN.AddCell(satır);
                }

                for (int i = 0; i < 11; i ++)
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
            }
            else
            {
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
            }
                        
            if (YKSmi == true)
            {
                for (int i = 0; i < 3; i++)
                {
                    satır = new PdfPCell(new Phrase(" ", font));
                    satır.HorizontalAlignment = 1;
                    tabloDYN.AddCell(satır);
                }
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

                if (YKSmi==true)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        satır = new PdfPCell(new Phrase(ogrSonuclari[j], font));
                        satır.HorizontalAlignment = 1;
                        tableOgr.AddCell(satır);
                    }

                    for (int j = 33; j < 66; j++)
                    {
                        satır = new PdfPCell(new Phrase(ogrSonuclari[j], font));
                        satır.HorizontalAlignment = 1;
                        tableOgr.AddCell(satır);
                    }
                }
                else
                {
                    for (int j = 1; j < ogrSonuclari.Count; j++)
                    {
                        satır = new PdfPCell(new Phrase(ogrSonuclari[j], font));
                        satır.HorizontalAlignment = 1;
                        tableOgr.AddCell(satır);
                    }

                    satır = new PdfPCell(new Phrase(ogrenciSonuclariPuanSirali[i].getPuan().ToString(), font));
                    satır.HorizontalAlignment = 1;
                    tableOgr.AddCell(satır);
                }                              

                if (YKSmi == true)
                {
                    satır = new PdfPCell(new Phrase(ogrenciSonuclariPuanSirali[i].getPuanSAY().ToString(), font));
                    satır.HorizontalAlignment = 1;
                    tableOgr.AddCell(satır);

                    satır = new PdfPCell(new Phrase(ogrenciSonuclariPuanSirali[i].getPuanEA().ToString(), font));
                    satır.HorizontalAlignment = 1;
                    tableOgr.AddCell(satır);

                    satır = new PdfPCell(new Phrase(ogrenciSonuclariPuanSirali[i].getPuanSOZ().ToString(), font));
                    satır.HorizontalAlignment = 1;
                    tableOgr.AddCell(satır);
                }

                PdfPCell cellOgr = new PdfPCell(tableOgr);
                tablo.AddCell(cellOgr);
            }

            return tablo;
        }

        private String pfdKaydetDialogla(string s_dosyaadi, PdfPTable tablo )
        {
            string yol = "";
            try
            {
                ///PDF kaydet///
                ///
                
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

                yol = kaydet.FileName;

                MessageBox.Show(yol+" kaydedildi.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return yol;
        }

        private void pdfKaydet(String yol ,PdfPTable tablo)
        {
            using (FileStream stream = new FileStream(yol, FileMode.Create))
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
