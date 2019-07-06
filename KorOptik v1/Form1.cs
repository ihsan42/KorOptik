using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace KorOptik_v1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<OgrenciOkunan> okunanlar;
        List<Point> koseKareBaslangicNoktalari;
        List<Point> baslangicnoktalari = new List<Point>();
        List<string> okunacakStandartAlanAdlari;
        List<string> okunacakDersAdlari;
        List<String> kayitliTumDersler;
        List<string> paths = new List<string>();           
        List<int> soruSayilari;
        List<int> okunacakDersIndexleri;
        int eklenenDersSayisi;
        public Boolean kalibrasyonYapildiMi=false;
        public List<FormDegerleri> tumFormlarinDeğerleri=new List<FormDegerleri>();

        public String okulturu = "";
        String formadi="";
        int grilikesigi;
        int parlaklikesigi;
        int sikokumahassasiyeti;
        Point formebatlari;      
        Bitmap resimm;

        private void Form1_Load(object sender, EventArgs e)
        {
            Veritabani vt = new Veritabani();
            vt.baglan();
            List<String> kayitliFormAdlari = new List<string>();
            kayitliFormAdlari = vt.kayitliFormlarınIsimleriniGetir();
            if (kayitliFormAdlari.Count != 0)
            {
                for (int i = 0; i < kayitliFormAdlari.Count; i++)
                {
                    comboBoxFormTuru.Items.Add(kayitliFormAdlari[i]);
                }
            }
        }

        private void buttonOku_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            if (paths.Count == 0)
            {
                MessageBox.Show("Yüklenmiş form yok!");

            }
            else if (comboBoxFormTuru.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen form türünü seçiniz!");
            }
            else
            {               
                okunacakStandartAlanAdlari = new List<string>();
                okunacakDersAdlari = new List<string>();
                okunacakDersIndexleri = new List<int>();
                kolonIsimleriniGoruntule(formadi, dataGridViewOkunanlar, okunacakStandartAlanAdlari, okunacakDersAdlari, okunacakDersIndexleri);

                okunacakStandartAlanAdlari = new List<string>();
                okunacakDersAdlari = new List<string>();
                formBilgileriGetir(comboBoxFormTuru.SelectedItem.ToString(), okunacakStandartAlanAdlari, okunacakDersAdlari);

                List<String> okunamayanlar = new List<string>();
                for (int m = 0; m < paths.Count; m++)
                {                 
                    Bitmap secilenForm = new Bitmap(paths[m]);
                    Bitmap okunacakForm;
                    Bitmap anaForm;
                    if (kalibrasyonYapildiMi == true)
                    {
                        okunacakForm = new Bitmap(secilenForm, tumFormlarinDeğerleri[m].getFormWidht(), tumFormlarinDeğerleri[m].getFormHeight());
                        anaForm = new Bitmap(okunacakForm);
                    }
                    else
                    {
                        okunacakForm = new Bitmap(secilenForm, formebatlari.X, formebatlari.Y);
                        anaForm = new Bitmap(okunacakForm);
                    }

                    kenarBul(okunacakForm, m);
                    egrilikGider(okunacakForm, anaForm, m);

                    if (koseKareBaslangicNoktalari.Count == 6)
                    {
                        int basX = 0;
                        int basY = 0;
                        ///ADSOYAD OKUMA/////
                        if (kalibrasyonYapildiMi == true)
                        {
                            basX = tumFormlarinDeğerleri[m].getAdSoyadBaslangic().X + koseKareBaslangicNoktalari[0].X;
                            basY = tumFormlarinDeğerleri[m].getAdSoyadBaslangic().Y + koseKareBaslangicNoktalari[0].Y;
                        }
                        else
                        {
                            basX = baslangicnoktalari[0].X + koseKareBaslangicNoktalari[0].X;
                            basY = baslangicnoktalari[0].Y + koseKareBaslangicNoktalari[0].Y;
                        }
                        binaryyap(okunacakForm, basX, basY + 27, 20 * 9, 29 * 9, m);
                        String ogrAdi = adsoyadOku(okunacakForm, basX, basY);

                        ////öĞRENCİ NO OKUMA/////
                        if (kalibrasyonYapildiMi == true)
                        {
                            basX = tumFormlarinDeğerleri[m].getOgrenciNoBaslangic().X + koseKareBaslangicNoktalari[0].X;
                            basY = tumFormlarinDeğerleri[m].getOgrenciNoBaslangic().Y + koseKareBaslangicNoktalari[0].Y;
                        }
                        else
                        {
                            basX = baslangicnoktalari[1].X + koseKareBaslangicNoktalari[0].X;
                            basY = baslangicnoktalari[1].Y + koseKareBaslangicNoktalari[0].Y;
                        }
                        binaryyap(okunacakForm, basX, basY + 18, 4 * 9, 10 * 9, m);
                        string ogrNumarasi = ogrenciNoOku(okunacakForm, basX, basY);

                        ////OKUL KODU OKUMA///////
                        if (kalibrasyonYapildiMi == true)
                        {
                            basX = tumFormlarinDeğerleri[m].getOkulKoduBaslangic().X + koseKareBaslangicNoktalari[0].X;
                            basY = tumFormlarinDeğerleri[m].getOkulKoduBaslangic().Y + koseKareBaslangicNoktalari[0].Y;
                        }
                        else
                        {
                            basX = baslangicnoktalari[4].X + koseKareBaslangicNoktalari[0].X;
                            basY = baslangicnoktalari[4].Y + koseKareBaslangicNoktalari[0].Y;
                        }
                        binaryyap(okunacakForm, basX, basY + 18, 6 * 9, 10 * 9, m);
                        string okulkodu = okulKoduOku(okunacakForm, basX, basY);

                        ///////KİTAPÇIK TÜRÜ OKUMA////////
                        if (kalibrasyonYapildiMi == true)
                        {
                            basX = tumFormlarinDeğerleri[m].getKitapcikTuruBaslangic().X + koseKareBaslangicNoktalari[0].X;
                            basY = tumFormlarinDeğerleri[m].getKitapcikTuruBaslangic().Y + koseKareBaslangicNoktalari[0].Y;
                        }
                        else
                        {
                            basX = baslangicnoktalari[3].X + koseKareBaslangicNoktalari[0].X;
                            basY = baslangicnoktalari[3].Y + koseKareBaslangicNoktalari[0].Y;
                        }
                        binaryyap(okunacakForm, basX + 18, basY + 63, 1 * 9, 4 * 9, m);
                        string kitTuru = kitapcikTuruOku(okunacakForm, basX, basY);

                        ///////SINIF-ŞUBE OKUMA//////////
                        if (kalibrasyonYapildiMi == true)
                        {
                            basX = tumFormlarinDeğerleri[m].getSinifSubeBaslangic().X + koseKareBaslangicNoktalari[0].X;
                            basY = tumFormlarinDeğerleri[m].getSinifSubeBaslangic().Y + koseKareBaslangicNoktalari[0].Y;
                        }
                        else
                        {
                            basX = baslangicnoktalari[2].X + koseKareBaslangicNoktalari[0].X;
                            basY = baslangicnoktalari[2].Y + koseKareBaslangicNoktalari[0].Y;
                        }
                        binaryyap(okunacakForm, basX, basY + 18, 1 * 9, 12 * 9, m);
                        binaryyap(okunacakForm, basX + 9, basY + 18, 1 * 9, 29 * 9, m);
                        string sinif = sinifOku(okunacakForm, basX, basY);
                        string sube = subeOku(okunacakForm, basX, basY);

                        //////////DERSLERİ OKUMA////////////
                        int sikSayisi = 0;
                        if (okulturu.Equals("İlkokul"))
                        {
                            sikSayisi = 3;
                        }
                        if (okulturu.Equals("Ortaokul"))
                        {
                            sikSayisi = 4;
                        }
                        if (okulturu.Equals("Lise"))
                        {
                            sikSayisi = 5;
                        }

                        List<String> cevaplar = new List<string>();
                        for (int i = 0; i < okunacakDersAdlari.Count; i++)
                        {
                            int sorusayisi = soruSayilari[i];
                            if (kalibrasyonYapildiMi == true)
                            {
                                basX = tumFormlarinDeğerleri[m].getDersBaslangicNoktalari()[i].X + koseKareBaslangicNoktalari[0].X;
                                basY = tumFormlarinDeğerleri[m].getDersBaslangicNoktalari()[i].Y + koseKareBaslangicNoktalari[0].Y;
                            }
                            else
                            {
                                basX = baslangicnoktalari[6 + i].X + koseKareBaslangicNoktalari[0].X;
                                basY = baslangicnoktalari[6 + i].Y + koseKareBaslangicNoktalari[0].Y;
                            }

                            binaryyap(okunacakForm, basX + 9, basY, sikSayisi * 9, sorusayisi * 9, m);
                            string cevap = dersleriOku(okunacakForm, basX, basY, okulturu, sorusayisi, sikSayisi);
                            cevaplar.Add(cevap);
                        }

                        /////OKUNANLARI YAZDIRMA//////////////
                        dataGridViewOkunanlar.Rows.Add();
                        for (int j = 0; j < dataGridViewOkunanlar.Rows[m].Cells.Count; j++)
                        {
                            if (dataGridViewOkunanlar.Columns[j].HeaderText.Equals("Ad-Soyad"))
                            {
                                dataGridViewOkunanlar.Rows[m].Cells[j].Value = ogrAdi;
                            }
                            if (dataGridViewOkunanlar.Columns[j].HeaderText.Equals("Öğrenci No"))
                            {
                                dataGridViewOkunanlar.Rows[m].Cells[j].Value = ogrNumarasi;
                            }
                            if (dataGridViewOkunanlar.Columns[j].HeaderText.Equals("Sınıf-Şube"))
                            {
                                dataGridViewOkunanlar.Rows[m].Cells[j].Value = sinif + sube;
                            }
                            if (dataGridViewOkunanlar.Columns[j].HeaderText.Equals("Kitapçık Türü"))
                            {
                                dataGridViewOkunanlar.Rows[m].Cells[j].Value = kitTuru;
                            }
                            if (dataGridViewOkunanlar.Columns[j].HeaderText.Equals("Okul Kodu"))
                            {
                                dataGridViewOkunanlar.Rows[m].Cells[j].Value = okulkodu;
                            }
                        }
                        for (int i = 0; i < cevaplar.Count; i++)
                        {
                            dataGridViewOkunanlar.Rows[m].Cells[dataGridViewOkunanlar.Rows[m].Cells.Count + i - cevaplar.Count].Value = cevaplar[i];
                        }


                        secilenForm.Dispose();
                        okunacakForm.Dispose();
                        anaForm.Dispose();
                        koseKareBaslangicNoktalari.Clear();
                        Application.DoEvents();
                    }
                    else
                    {
                        dataGridViewOkunanlar.Rows.Add();
                        okunamayanlar.Add((m + 1).ToString() + ". sıra " + paths[m]);
                    }
                }
                String sonuc = "Okuma Tamamlandı.";
                if (okunamayanlar.Count > 0)
                {
                    sonuc = "Okuma Tamamlandı. Okunamayan optikler var! Lütfen kalibrasyon yapın.\n\n Okunamayanlar\n";
                    int sira = 0;
                    foreach (String s in okunamayanlar)
                    {
                        sira += 1;
                        sonuc += sira + ") " + s + "\n";
                    }
                }

                MessageBox.Show(sonuc);

            }
            this.Cursor = Cursors.Default;
        }

        private void değerlendirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewOkunanlar.RowCount == 0) {
                MessageBox.Show("Okunmuş form yok!");
            }
            else
            {              
                okunanlar = new List<OgrenciOkunan>();

                for (int i = 0; i < dataGridViewOkunanlar.RowCount - 1; i++)
                {
                    OgrenciOkunan ogrenci = new OgrenciOkunan();
                    for (int j = 0; j < dataGridViewOkunanlar.ColumnCount; j++)
                    {
                        if (dataGridViewOkunanlar.Columns[j].HeaderText.ToString().Equals("Ad-Soyad"))
                        {
                            ogrenci.setAdSoyad(dataGridViewOkunanlar.Rows[i].Cells[j].Value.ToString());
                        }
                        if (dataGridViewOkunanlar.Columns[j].HeaderText.Equals("Öğrenci No"))
                        {
                            ogrenci.setOgrenciNo(dataGridViewOkunanlar.Rows[i].Cells[j].Value.ToString());
                        }
                        if (dataGridViewOkunanlar.Columns[j].HeaderText.Equals("Sınıf-Şube"))
                        {
                            ogrenci.setSinifSube(dataGridViewOkunanlar.Rows[i].Cells[j].Value.ToString());
                        }
                        if (dataGridViewOkunanlar.Columns[j].HeaderText.Equals("Kitapçık Türü"))
                        {
                            ogrenci.setKitapcikTuru(dataGridViewOkunanlar.Rows[i].Cells[j].Value.ToString());
                        }
                        if (dataGridViewOkunanlar.Columns[j].HeaderText.Equals("Okul Kodu"))
                        {
                            ogrenci.setOkulKodu(dataGridViewOkunanlar.Rows[i].Cells[j].Value.ToString());
                        }
                    }

                    List<String> cevaplar = new List<string>();
                    for (int j = dataGridViewOkunanlar.ColumnCount - okunacakDersAdlari.Count; j < dataGridViewOkunanlar.ColumnCount; j++)
                    {
                        cevaplar.Add(dataGridViewOkunanlar.Rows[i].Cells[j].Value.ToString());
                    }
                    ogrenci.addOkunanCevaplar(cevaplar);

                    okunanlar.Add(ogrenci);
                }

                Degerlendir degerlendir = new Degerlendir();
                degerlendir.formadi = formadi;

                foreach (String s in okunacakDersAdlari)
                {
                    degerlendir.okunacakDersler.Add(s);
                }
                foreach (String s in okunacakStandartAlanAdlari)
                {
                    degerlendir.okunacakStandartAlanlar.Add(s);
                }
                degerlendir.Show();
            }                             
        }

        private void formTasarlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTasarla formTasarla = new FormTasarla();
            formTasarla.Show();
        }

        private void resimDosyasıdanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paths = new List<string>();
            paths = FormSec();
            if (paths.Count() > 0)
            {
                labelSecilenFormSayisi.Text = paths.Count().ToString() + " dosya seçildi.";
            }
        }

        private List<string> FormSec()
        {
            List<String> dosyayollari = new List<string>();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Filter = "Resim Dosyaları(*.jpg;*.png)|*.jpg;*.png|Tüm Dosyalar(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                kalibrasyonYapildiMi = false;
                foreach (string path in fileDialog.FileNames)
                {
                    dosyayollari.Add(path);
                }
            }
            return dosyayollari;
        }

        private void formYazdırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormYazdir formYazdir = new FormYazdir();
            formYazdir.Show();
        }

        private void kalibrasyonYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (paths.Count > 0)
            {
                Kalibrasyon kalibrasyon = new Kalibrasyon();
                if (comboBoxFormTuru.SelectedIndex > -1)
                {                    
                    kalibrasyon.formadiKalibre = formadi;
                    foreach (String path in paths)
                    {
                        kalibrasyon.pathsSecilen.Add(path);
                    }                   
                    kalibrasyon.Show();
                }
                else
                {
                    MessageBox.Show("Lütfen form türünü seçiniz!");
                }
            }
            else
            {
                MessageBox.Show("Yüklenen form yok!");
            }
        }

        private void comboBoxFormTuru_SelectedIndexChanged(object sender, EventArgs e)
        {          
                formadi = comboBoxFormTuru.SelectedItem.ToString();
                okunacakStandartAlanAdlari = new List<string>();
                okunacakDersAdlari = new List<string>();
                formBilgileriGetir(formadi, okunacakStandartAlanAdlari, okunacakDersAdlari);
                eklenenDersSayisi = okunacakDersAdlari.Count;                      
        }     

        public String adsoyadOku(Bitmap b,int basX, int basY)
        {
            int x1 = basX;
            int x2 = basX + 20 * 9;
            int y1 = basY + 27;
            int y2 = basY + 27 + 29 * 9;

            List<int> sutunlardakiSiyahPikselSayilari = new List<int>();
            for (int i = x1; i < x2; i += 9)
            {
                for (int j = y1; j < y2; j += 9)
                {
                    List<int> siyahpikseller = new List<int>();
                    for (int m = i; m < i + 9; m++)
                    {
                        for (int n = j; n < j + 9; n++)
                        {
                            Color renk = b.GetPixel(m, n);
                            if (renk.R == 0 && renk.G == 0 && renk.B == 0)
                            {
                                siyahpikseller.Add(1);
                            }
                        }
                    }
                    sutunlardakiSiyahPikselSayilari.Add(siyahpikseller.Count());
                    siyahpikseller.Clear();
                }
            }

            List<String> adsoyad = new List<string>();
            for(int i = 0; i < sutunlardakiSiyahPikselSayilari.Count; i += 29)
            {
                int j = -1;
                List<int> isatetlenenler = new List<int>();
                for (int k = i; k < i + 29; k++)
                {                    
                    if (sutunlardakiSiyahPikselSayilari[k] > sikokumahassasiyeti)
                    {
                        isatetlenenler.Add(sutunlardakiSiyahPikselSayilari[k]);
                        j = k;
                    }                   
                }
                
                if (isatetlenenler.Count > 1)
                {
                    adsoyad.Add("?");
                }
                else if (isatetlenenler.Count == 0)
                {
                    adsoyad.Add(" ");
                }
                else
                {
                    if (j == i + 0)
                    {
                        adsoyad.Add("A");
                    }
                    if (j == i + 1)
                    {
                        adsoyad.Add("B");
                    }
                    if (j == i + 2)
                    {
                        adsoyad.Add("C");
                    }
                    if (j == i + 3)
                    {
                        adsoyad.Add("Ç");
                    }
                    if (j == i + 4)
                    {
                        adsoyad.Add("D");
                    }
                    if (j == i + 5)
                    {
                        adsoyad.Add("E");
                    }
                    if (j == i + 6)
                    {
                        adsoyad.Add("F");
                    }
                    if (j == i + 7)
                    {
                        adsoyad.Add("G");
                    }
                    if (j == i + 8)
                    {
                        adsoyad.Add("Ğ");
                    }
                    if (j == i + 9)
                    {
                        adsoyad.Add("H");
                    }
                    if (j == i + 10)
                    {
                        adsoyad.Add("I");
                    }
                    if (j == i + 11)
                    {
                        adsoyad.Add("İ");
                    }
                    if (j == i + 12)
                    {
                        adsoyad.Add("J");
                    }
                    if (j == i + 13)
                    {
                        adsoyad.Add("K");
                    }
                    if (j == i + 14)
                    {
                        adsoyad.Add("L");
                    }
                    if (j == i + 15)
                    {
                        adsoyad.Add("M");
                    }
                    if (j == i + 16)
                    {
                        adsoyad.Add("N");
                    }
                    if (j == i + 17)
                    {
                        adsoyad.Add("O");
                    }
                    if (j == i + 18)
                    {
                        adsoyad.Add("Ö");
                    }
                    if (j == i + 19)
                    {
                        adsoyad.Add("P");
                    }
                    if (j == i + 20)
                    {
                        adsoyad.Add("R");
                    }
                    if (j == i + 21)
                    {
                        adsoyad.Add("S");
                    }
                    if (j == i + 22)
                    {
                        adsoyad.Add("Ş");
                    }
                    if (j == i + 23)
                    {
                        adsoyad.Add("T");
                    }
                    if (j == i + 24)
                    {
                        adsoyad.Add("U");
                    }
                    if (j == i + 25)
                    {
                        adsoyad.Add("Ü");
                    }
                    if (j == i + 26)
                    {
                        adsoyad.Add("V");
                    }
                    if (j == i + 27)
                    {
                        adsoyad.Add("Y");
                    }
                    if (j == i + 28)
                    {
                        adsoyad.Add("Z");
                    }
                }
                isatetlenenler.Clear();
            }

            String isim = "";
            foreach (String s in adsoyad)
            {
                isim += s;
            }
            adsoyad.Clear();
            sutunlardakiSiyahPikselSayilari.Clear();   
            
            return isim;
        }

        public String ogrenciNoOku(Bitmap b, int basX, int basY)
        {
            int x1 = basX;
            int x2 = basX + 4 * 9;
            int y1 = basY + 18;
            int y2 = basY + 18 + 10 * 9;

            List<int> sutunlardakiSiyahPikselSayilari = new List<int>();
            for (int i = x1; i < x2; i += 9)
            {
                for (int j = y1; j < y2; j += 9)
                {
                    List<int> siyahpikseller = new List<int>();
                    for (int m = i; m < i + 9; m++)
                    {
                        for (int n = j; n < j + 9; n++)
                        {
                            Color renk = b.GetPixel(m, n);
                            if (renk.R == 0 && renk.G == 0 && renk.B == 0)
                            {
                                siyahpikseller.Add(1);
                            }
                        }
                    }
                    sutunlardakiSiyahPikselSayilari.Add(siyahpikseller.Count());
                    siyahpikseller.Clear();
                }
            }

            List<String> ogrenciNo = new List<string>();
            for (int i = 0; i < sutunlardakiSiyahPikselSayilari.Count; i += 10)
            {
                int j = -1;
                List<int> isatetlenenler = new List<int>();
                for (int k = i; k < i + 10; k++)
                {
                    if (sutunlardakiSiyahPikselSayilari[k] > sikokumahassasiyeti)
                    {
                        isatetlenenler.Add(sutunlardakiSiyahPikselSayilari[k]);
                        j = k;
                    }
                }

                if (isatetlenenler.Count > 1)
                {
                    ogrenciNo.Add("?");
                }
                else if (isatetlenenler.Count == 0)
                {
                    ogrenciNo.Add(" ");
                }
                else
                {
                    if (j == i + 0)
                    {
                        ogrenciNo.Add("0");
                    }
                    if (j == i + 1)
                    {
                        ogrenciNo.Add("1");
                    }
                    if (j == i + 2)
                    {
                        ogrenciNo.Add("2");
                    }
                    if (j == i + 3)
                    {
                        ogrenciNo.Add("3");
                    }
                    if (j == i + 4)
                    {
                        ogrenciNo.Add("4");
                    }
                    if (j == i + 5)
                    {
                        ogrenciNo.Add("5");
                    }
                    if (j == i + 6)
                    {
                        ogrenciNo.Add("6");
                    }
                    if (j == i + 7)
                    {
                        ogrenciNo.Add("7");
                    }
                    if (j == i + 8)
                    {
                        ogrenciNo.Add("8");
                    }
                    if (j == i + 9)
                    {
                        ogrenciNo.Add("9");
                    }                  
                }
                isatetlenenler.Clear();
            }

            String ogrNo = "";
            foreach (String s in ogrenciNo)
            {
                ogrNo += s;
            }
            ogrenciNo.Clear();
            sutunlardakiSiyahPikselSayilari.Clear();

            return ogrNo;
        }

        public String sinifOku(Bitmap b, int basX, int basY)
        {
            int x1 = basX ;
            int x2 = basX + 1 * 9;
            int y1 = basY + 18;
            int y2 = basY + 18 + 12 * 9;

            List<int> sutunlardakiSiyahPikselSayilari = new List<int>();
            for (int i = x1; i < x2; i += 9)
            {
                for (int j = y1; j < y2; j += 9)
                {
                    List<int> siyahpikseller = new List<int>();
                    for (int m = i; m < i + 9; m++)
                    {
                        for (int n = j; n < j + 9; n++)
                        {
                            Color renk = b.GetPixel(m, n);
                            if (renk.R == 0 && renk.G == 0 && renk.B == 0)
                            {
                                siyahpikseller.Add(1);
                            }
                        }
                    }
                    sutunlardakiSiyahPikselSayilari.Add(siyahpikseller.Count());
                    siyahpikseller.Clear();
                }
            }

            List<String> adsoyad = new List<string>();
            for (int i = 0; i < sutunlardakiSiyahPikselSayilari.Count; i += 12)
            {
                int j = -1;
                List<int> isatetlenenler = new List<int>();
                for (int k = i; k < i + 12; k++)
                {
                    if (sutunlardakiSiyahPikselSayilari[k] > sikokumahassasiyeti)
                    {
                        isatetlenenler.Add(sutunlardakiSiyahPikselSayilari[k]);
                        j = k;
                    }
                }

                if (isatetlenenler.Count > 1)
                {
                    adsoyad.Add("?");
                }
                else if (isatetlenenler.Count == 0)
                {
                    adsoyad.Add(" ");
                }
                else
                {
                    if (j == i + 0)
                    {
                        adsoyad.Add("1");
                    }
                    if (j == i + 1)
                    {
                        adsoyad.Add("2");
                    }
                    if (j == i + 2)
                    {
                        adsoyad.Add("3");
                    }
                    if (j == i + 3)
                    {
                        adsoyad.Add("4");
                    }
                    if (j == i + 4)
                    {
                        adsoyad.Add("5");
                    }
                    if (j == i + 5)
                    {
                        adsoyad.Add("6");
                    }
                    if (j == i + 6)
                    {
                        adsoyad.Add("7");
                    }
                    if (j == i + 7)
                    {
                        adsoyad.Add("8");
                    }
                    if (j == i + 8)
                    {
                        adsoyad.Add("9");
                    }
                    if (j == i + 9)
                    {
                        adsoyad.Add("10");
                    }
                    if (j == i + 10)
                    {
                        adsoyad.Add("11");
                    }
                    if (j == i + 11)
                    {
                        adsoyad.Add("12");
                    }                    
                }
                isatetlenenler.Clear();
            }

            String sube = "";
            foreach (String s in adsoyad)
            {
                sube += s;
            }
            adsoyad.Clear();
            sutunlardakiSiyahPikselSayilari.Clear();

            return sube;
        }

        public String subeOku(Bitmap b, int basX, int basY)
        {
            int x1 = basX+9;
            int x2 = basX+9 + 1 * 9;
            int y1 = basY + 18;
            int y2 = basY + 18 + 29 * 9;

            List<int> sutunlardakiSiyahPikselSayilari = new List<int>();
            for (int i = x1; i < x2; i += 9)
            {
                for (int j = y1; j < y2; j += 9)
                {
                    List<int> siyahpikseller = new List<int>();
                    for (int m = i; m < i + 9; m++)
                    {
                        for (int n = j; n < j + 9; n++)
                        {
                            Color renk = b.GetPixel(m, n);
                            if (renk.R == 0 && renk.G == 0 && renk.B == 0)
                            {
                                siyahpikseller.Add(1);
                            }
                        }
                    }
                    sutunlardakiSiyahPikselSayilari.Add(siyahpikseller.Count());
                    siyahpikseller.Clear();
                }
            }

            List<String> adsoyad = new List<string>();
            for (int i = 0; i < sutunlardakiSiyahPikselSayilari.Count; i += 29)
            {
                int j = -1;
                List<int> isatetlenenler = new List<int>();
                for (int k = i; k < i + 29; k++)
                {
                    if (sutunlardakiSiyahPikselSayilari[k] > sikokumahassasiyeti)
                    {
                        isatetlenenler.Add(sutunlardakiSiyahPikselSayilari[k]);
                        j = k;
                    }
                }

                if (isatetlenenler.Count > 1)
                {
                    adsoyad.Add("?");
                }
                else if (isatetlenenler.Count == 0)
                {
                    adsoyad.Add(" ");
                }
                else
                {
                    if (j == i + 0)
                    {
                        adsoyad.Add("A");
                    }
                    if (j == i + 1)
                    {
                        adsoyad.Add("B");
                    }
                    if (j == i + 2)
                    {
                        adsoyad.Add("C");
                    }
                    if (j == i + 3)
                    {
                        adsoyad.Add("Ç");
                    }
                    if (j == i + 4)
                    {
                        adsoyad.Add("D");
                    }
                    if (j == i + 5)
                    {
                        adsoyad.Add("E");
                    }
                    if (j == i + 6)
                    {
                        adsoyad.Add("F");
                    }
                    if (j == i + 7)
                    {
                        adsoyad.Add("G");
                    }
                    if (j == i + 8)
                    {
                        adsoyad.Add("Ğ");
                    }
                    if (j == i + 9)
                    {
                        adsoyad.Add("H");
                    }
                    if (j == i + 10)
                    {
                        adsoyad.Add("I");
                    }
                    if (j == i + 11)
                    {
                        adsoyad.Add("İ");
                    }
                    if (j == i + 12)
                    {
                        adsoyad.Add("J");
                    }
                    if (j == i + 13)
                    {
                        adsoyad.Add("K");
                    }
                    if (j == i + 14)
                    {
                        adsoyad.Add("L");
                    }
                    if (j == i + 15)
                    {
                        adsoyad.Add("M");
                    }
                    if (j == i + 16)
                    {
                        adsoyad.Add("N");
                    }
                    if (j == i + 17)
                    {
                        adsoyad.Add("O");
                    }
                    if (j == i + 18)
                    {
                        adsoyad.Add("Ö");
                    }
                    if (j == i + 19)
                    {
                        adsoyad.Add("P");
                    }
                    if (j == i + 20)
                    {
                        adsoyad.Add("R");
                    }
                    if (j == i + 21)
                    {
                        adsoyad.Add("S");
                    }
                    if (j == i + 22)
                    {
                        adsoyad.Add("Ş");
                    }
                    if (j == i + 23)
                    {
                        adsoyad.Add("T");
                    }
                    if (j == i + 24)
                    {
                        adsoyad.Add("U");
                    }
                    if (j == i + 25)
                    {
                        adsoyad.Add("Ü");
                    }
                    if (j == i + 26)
                    {
                        adsoyad.Add("V");
                    }
                    if (j == i + 27)
                    {
                        adsoyad.Add("Y");
                    }
                    if (j == i + 28)
                    {
                        adsoyad.Add("Z");
                    }
                }
                isatetlenenler.Clear();
            }

            String sube = "";
            foreach (String s in adsoyad)
            {
                sube += s;
            }
            adsoyad.Clear();
            sutunlardakiSiyahPikselSayilari.Clear();

            return sube;
        }

        public String kitapcikTuruOku(Bitmap b, int basX, int basY)
        {
            int x1 = basX+18;
            int x2 = basX +18+ 1 * 9;
            int y1 = basY + 63;
            int y2 = basY + 63 + 4 * 9;

            List<int> sutunlardakiSiyahPikselSayilari = new List<int>();
            for (int i = x1; i < x2; i += 9)
            {
                for (int j = y1; j < y2; j += 9)
                {
                    List<int> siyahpikseller = new List<int>();
                    for (int m = i; m < i + 9; m++)
                    {
                        for (int n = j; n < j + 9; n++)
                        {
                            Color renk = b.GetPixel(m, n);
                            if (renk.R == 0 && renk.G == 0 && renk.B == 0)
                            {
                                siyahpikseller.Add(1);
                            }
                        }
                    }
                    sutunlardakiSiyahPikselSayilari.Add(siyahpikseller.Count());
                    siyahpikseller.Clear();
                }
            }

            List<String> ogrenciNo = new List<string>();
            for (int i = 0; i < sutunlardakiSiyahPikselSayilari.Count; i += 4)
            {
                int j = -1;
                List<int> isatetlenenler = new List<int>();
                for (int k = i; k < i + 4; k++)
                {
                    if (sutunlardakiSiyahPikselSayilari[k] > sikokumahassasiyeti)
                    {
                        isatetlenenler.Add(sutunlardakiSiyahPikselSayilari[k]);
                        j = k;
                    }
                }

                if (isatetlenenler.Count > 1)
                {
                    ogrenciNo.Add("?");
                }
                else if (isatetlenenler.Count == 0)
                {
                    ogrenciNo.Add(" ");
                }
                else
                {
                    if (j == i + 0)
                    {
                        ogrenciNo.Add("A");
                    }
                    if (j == i + 1)
                    {
                        ogrenciNo.Add("B");
                    }
                    if (j == i + 2)
                    {
                        ogrenciNo.Add("C");
                    }
                    if (j == i + 3)
                    {
                        ogrenciNo.Add("D");
                    }                   
                }
                isatetlenenler.Clear();
            }

            String ogrNo = "";
            foreach (String s in ogrenciNo)
            {
                ogrNo += s;
            }
            ogrenciNo.Clear();
            sutunlardakiSiyahPikselSayilari.Clear();

            return ogrNo;
        }

        public String okulKoduOku(Bitmap b, int basX, int basY)
        {
            int x1 = basX;
            int x2 = basX + 6 * 9;
            int y1 = basY + 18;
            int y2 = basY + 18 + 10 * 9;

            List<int> sutunlardakiSiyahPikselSayilari = new List<int>();
            for (int i = x1; i < x2; i += 9)
            {
                for (int j = y1; j < y2; j += 9)
                {
                    List<int> siyahpikseller = new List<int>();
                    for (int m = i; m < i + 9; m++)
                    {
                        for (int n = j; n < j + 9; n++)
                        {
                            Color renk = b.GetPixel(m, n);
                            if (renk.R == 0 && renk.G == 0 && renk.B == 0)
                            {
                                siyahpikseller.Add(1);
                            }
                        }
                    }
                    sutunlardakiSiyahPikselSayilari.Add(siyahpikseller.Count());
                    siyahpikseller.Clear();
                }
            }

            List<String> ogrenciNo = new List<string>();
            for (int i = 0; i < sutunlardakiSiyahPikselSayilari.Count; i += 10)
            {
                int j = -1;
                List<int> isatetlenenler = new List<int>();
                for (int k = i; k < i + 10; k++)
                {
                    if (sutunlardakiSiyahPikselSayilari[k] > sikokumahassasiyeti)
                    {
                        isatetlenenler.Add(sutunlardakiSiyahPikselSayilari[k]);
                        j = k;
                    }
                }

                if (isatetlenenler.Count > 1)
                {
                    ogrenciNo.Add("?");
                }
                else if (isatetlenenler.Count == 0)
                {
                    ogrenciNo.Add(" ");
                }
                else
                {
                    if (j == i + 0)
                    {
                        ogrenciNo.Add("0");
                    }
                    if (j == i + 1)
                    {
                        ogrenciNo.Add("1");
                    }
                    if (j == i + 2)
                    {
                        ogrenciNo.Add("2");
                    }
                    if (j == i + 3)
                    {
                        ogrenciNo.Add("3");
                    }
                    if (j == i + 4)
                    {
                        ogrenciNo.Add("4");
                    }
                    if (j == i + 5)
                    {
                        ogrenciNo.Add("5");
                    }
                    if (j == i + 6)
                    {
                        ogrenciNo.Add("6");
                    }
                    if (j == i + 7)
                    {
                        ogrenciNo.Add("7");
                    }
                    if (j == i + 8)
                    {
                        ogrenciNo.Add("8");
                    }
                    if (j == i + 9)
                    {
                        ogrenciNo.Add("9");
                    }
                }
                isatetlenenler.Clear();
            }

            String ogrNo = "";
            foreach (String s in ogrenciNo)
            {
                ogrNo += s;
            }
            ogrenciNo.Clear();
            sutunlardakiSiyahPikselSayilari.Clear();

            return ogrNo;
        }

        public String dersleriOku(Bitmap b, int basX, int basY,String okulTuru,int soruSayisi,int sikSayisi)
        {
            int x1 = basX +9;
            int x2 = basX+9+sikSayisi*9;                     
            int y1 = basY ;
            int y2 = basY+ soruSayisi*9;

            List<int> sutunlardakiSiyahPikselSayilari = new List<int>();
            for (int i = y1; i < y2; i += 9)
            {
                for (int j = x1; j < x2; j += 9)
                {
                    List<int> siyahpikseller = new List<int>();
                    for (int m = i; m < i + 9; m++)
                    {
                        for (int n = j; n < j + 9; n++)
                        {
                            Color renk = b.GetPixel(n, m);
                            if (renk.R == 0 && renk.G == 0 && renk.B == 0)
                            {
                                siyahpikseller.Add(1);
                            }
                        }
                    }
                    sutunlardakiSiyahPikselSayilari.Add(siyahpikseller.Count());
                    siyahpikseller.Clear();
                }
            }

            List<String> ogrenciNo = new List<string>();
            for (int i = 0; i < sutunlardakiSiyahPikselSayilari.Count; i += sikSayisi)
            {
                int j = -1;
                List<int> isatetlenenler = new List<int>();
                for (int k = i; k < i + sikSayisi; k++)
                {
                    if (sutunlardakiSiyahPikselSayilari[k] > sikokumahassasiyeti)
                    {
                        isatetlenenler.Add(sutunlardakiSiyahPikselSayilari[k]);
                        j = k;
                    }
                }

                if (isatetlenenler.Count > 1)
                {
                    ogrenciNo.Add("?");
                }
                else if (isatetlenenler.Count == 0)
                {
                    ogrenciNo.Add(" ");
                }
                else
                {
                    if (j == i + 0)
                    {
                        ogrenciNo.Add("A");
                    }
                    if (j == i + 1)
                    {
                        ogrenciNo.Add("B");
                    }
                    if (j == i + 2)
                    {
                        ogrenciNo.Add("C");
                    }
                    if (j == i + 3)
                    {
                        ogrenciNo.Add("D");
                    }
                    if (j == i + 4)
                    {
                        ogrenciNo.Add("E");
                    }
                }
                isatetlenenler.Clear();
            }

            String ogrNo = "";
            foreach (String s in ogrenciNo)
            {
                ogrNo += s;
            }
            ogrenciNo.Clear();
            sutunlardakiSiyahPikselSayilari.Clear();

            return ogrNo;
        }              

        public void kolonIsimleriniGoruntule(String formadi,DataGridView dataGridView,List<string> okunacakStdAlanlar,List<String> okunacakDersler,List<int> okunacakDersIndexs)
        {
            okunacakDersler = new List<string>();
            okunacakStdAlanlar = new List<string>();
            okunacakDersIndexs = new List<int>();
            dataGridViewOkunanlar.Columns.Clear();
            List<String> dersNames = new List<string>();
            List<int> standartAlanlarIndexler = new List<int>();
            List<Point> baslangicPoints= new List<Point>();

            Veritabani vt = new Veritabani();
            vt.baglan();
            dersNames = vt.dersAdlariniGetir(formadi);
            vt.baglan();
            baslangicnoktalari = vt.formBaslangicNoktalariniGetir(formadi);
            for (int i = 0; i < 6; i++)
            {
                if (baslangicnoktalari[i].X != 0 && baslangicnoktalari[i].Y != 0)
                {
                    standartAlanlarIndexler.Add(i);
                }
            }
           
            foreach (int i in standartAlanlarIndexler)
            {
                if (i == 0)
                {
                    dataGridView.Columns.Add("column" + i.ToString(), "Ad-Soyad");
                    okunacakStdAlanlar.Add("Ad-Soyad");
                }
                if (i == 1)
                {
                    dataGridView.Columns.Add("column" + i.ToString(), "Öğrenci No");
                    okunacakStdAlanlar.Add("Öğrenci No");
                }
                if (i == 2)
                {
                    dataGridView.Columns.Add("column" + i.ToString(), "Sınıf-Şube");
                    okunacakStdAlanlar.Add("Sınıf-Şube");
                }
                if (i == 3)
                {
                    dataGridView.Columns.Add("column" + i.ToString(), "Kitapçık Türü");
                    okunacakStdAlanlar.Add("Kitapçık Türü");
                }
                if (i == 4)
                {
                    dataGridView.Columns.Add("column" + i.ToString(), "Okul Kodu");
                    okunacakStdAlanlar.Add("Okul Kodu");
                }
            }

            int colonSıraNo = 4;
            foreach (String s in dersNames)
            {
                colonSıraNo += 1;
                if (!s.Equals(""))
                {
                    dataGridView.Columns.Add("column" + colonSıraNo.ToString(), s);
                    okunacakDersler.Add(s);
                    okunacakDersIndexs.Add(dersNames.IndexOf(s));
                }
            }

            baslangicPoints.Clear();
            dersNames.Clear();
            standartAlanlarIndexler.Clear();
        }
       
        private void formBilgileriGetir(String formadi, List<string> okunacakStdAlanlar, List<String> okunacakDersler)
        {
            okunacakDersler = new List<string>();
            okunacakStdAlanlar = new List<string>();
            baslangicnoktalari = new List<Point>();
            List<int> secilenStandartAlanlarIndexler = new List<int>();
            soruSayilari = new List<int>();
            kayitliTumDersler = new List<string>();

            Veritabani vt = new Veritabani();
            vt.baglan();
            kayitliTumDersler = vt.dersAdlariniGetir(formadi);
            vt.baglan();
            baslangicnoktalari = vt.formBaslangicNoktalariniGetir(formadi);
            vt.baglan();
            soruSayilari = vt.soruSayılariniGetir(formadi);
            vt.baglan();
            parlaklikesigi = vt.parlaklikesigiGetir(formadi);
            vt.baglan();
            grilikesigi = vt.grilikesigiGetir(formadi);
            vt.baglan();
            soruSayilari = vt.soruSayılariniGetir(formadi);
            vt.baglan();
            sikokumahassasiyeti = vt.sikokumahassasiyetiGetir(formadi);
            vt.baglan();
            formebatlari = vt.formEbatlariniGetir(formadi);
            vt.baglan();
            okulturu = vt.okulTurunuGetir(formadi);
            vt.baglan();


            foreach (String s in kayitliTumDersler)
            {
                if (!s.Equals(""))
                {
                    okunacakDersAdlari.Add(s);
                }
            }

            for (int i = 0; i < 6; i++)
            {
                if (baslangicnoktalari[i].X != 0 && baslangicnoktalari[i].Y != 0)
                {
                    secilenStandartAlanlarIndexler.Add(i);
                }
            }

            foreach (int i in secilenStandartAlanlarIndexler)
            {
                if (i == 0)
                {
                    okunacakStandartAlanAdlari.Add("Ad-Soyad");
                }
                if (i == 1)
                {
                    okunacakStandartAlanAdlari.Add("Öğrenci No");
                }
                if (i == 2)
                {
                    okunacakStandartAlanAdlari.Add("Sınıf-Şube");
                }
                if (i == 3)
                {
                    okunacakStandartAlanAdlari.Add("Kitapçık Türü");
                }
                if (i == 4)
                {
                    okunacakStandartAlanAdlari.Add("Okul Kodu");
                }
            }

            secilenStandartAlanlarIndexler.Clear();
        }

        public void binaryyap(Bitmap b, int x1, int y1, int genislik, int yukseklik, int formSirasi)
        {
            try
            {
                resimm = b;
                BitmapData bmData = resimm.LockBits(new Rectangle(x1, y1, genislik, yukseklik), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                int stride = bmData.Stride;
                IntPtr Scan0 = bmData.Scan0; // Resmin ilk pixelinin adresini alıyoruz.
                double[,] GriResim = new double[genislik, yukseklik];
                unsafe
                {
                    int sayac = 0;
                    byte* p = (byte*)(void*)Scan0;

                    int nOffset = stride - genislik * 3;

                    for (int y = 0; y < yukseklik; ++y)
                    {
                        for (int x = 0; x < genislik; ++x)
                        {
                            GriResim[x, y] = (double)(.299 * p[0] + .587 * p[1] + .114 * p[2]);

                            if (kalibrasyonYapildiMi == true) {
                                grilikesigi = tumFormlarinDeğerleri[formSirasi].getGrilikEsigi();
                            }

                            if (GriResim[x, y] < grilikesigi)
                            {
                                GriResim[x, y] = (double)(0);

                                p[0] = p[1] = p[2] = (byte)GriResim[x, y];
                                sayac += 1;
                                p += 3;
                            }
                            else
                            {
                                GriResim[x, y] = (double)(255);

                                p[0] = p[1] = p[2] = (byte)GriResim[x, y];
                                sayac += 1;
                                p += 3;
                            }
                        }
                        p += nOffset;
                    }
                }
                resimm.UnlockBits(bmData);
            }
            catch { }
        }

        private void kenarBul(Bitmap kalibreEdilecekForm,int formSirasi)
        {
            koseKareBaslangicNoktalari = new List<Point>();
            ///////////Sol Üst////////////////////////
            List<Point> kosekoordinatlari1 = new List<Point>();
            for (int i = 1; i < 70; i++)
            {
                for (int j = 1; j < 70; j++)
                {
                    Color renk = kalibreEdilecekForm.GetPixel(i, j);
                    Color cr = kalibreEdilecekForm.GetPixel(i + 1, j);
                    Color cl = kalibreEdilecekForm.GetPixel(i - 1, j);
                    Color cu = kalibreEdilecekForm.GetPixel(i, j - 1);
                    Color cd = kalibreEdilecekForm.GetPixel(i, j + 1);
                    int dx = cr.R - cl.R;
                    int dy = cd.R - cu.R;
                    double power = Math.Sqrt((dx * dx / 4) + (dy * dy / 4));

                    if (kalibrasyonYapildiMi == true) {
                        parlaklikesigi = tumFormlarinDeğerleri[formSirasi].getParlaklikEsigi();
                    }

                    if (power > parlaklikesigi)
                    {
                        kosekoordinatlari1.Add(new Point(i, j));
                    }
                }
            }

            if (kosekoordinatlari1.Count > 0)
            {
                koseKareBaslangicNoktalari.Add(kosekoordinatlari1[0]);
                koseKareBaslangicNoktalari.Add(kosekoordinatlari1[kosekoordinatlari1.Count - 1]);
                kosekoordinatlari1.Clear();
            }

            //////////Sağ Üst////////////////////////////
            List<Point> kosekoordinatlari2 = new List<Point>();

            for (int i = kalibreEdilecekForm.Width - 70; i < kalibreEdilecekForm.Width - 1; i++)
            {
                for (int j = 1; j < 70; j++)
                {
                    Color renk = kalibreEdilecekForm.GetPixel(i, j);
                    Color cr = kalibreEdilecekForm.GetPixel(i + 1, j);
                    Color cl = kalibreEdilecekForm.GetPixel(i - 1, j);
                    Color cu = kalibreEdilecekForm.GetPixel(i, j - 1);
                    Color cd = kalibreEdilecekForm.GetPixel(i, j + 1);
                    int dx = cr.R - cl.R;
                    int dy = cd.R - cu.R;
                    double power = Math.Sqrt((dx * dx / 4) + (dy * dy / 4));

                    if (kalibrasyonYapildiMi == true)
                    {
                        parlaklikesigi = tumFormlarinDeğerleri[formSirasi].getParlaklikEsigi();
                    }

                    if (power > parlaklikesigi)
                    {
                        kosekoordinatlari2.Add(new Point(i, j));
                    }
                }
            }

            if (kosekoordinatlari2.Count > 0)
            {
                koseKareBaslangicNoktalari.Add(kosekoordinatlari2[0]);
                koseKareBaslangicNoktalari.Add(kosekoordinatlari2[kosekoordinatlari2.Count - 1]);
                kosekoordinatlari2.Clear();
            }

            ////////////Sol Alt/////////////
            List<Point> kosekoordinatlari3 = new List<Point>();
            for (int i = 1; i < 70; i++)
            {
                for (int j = kalibreEdilecekForm.Height - 70; j < kalibreEdilecekForm.Height - 1; j++)
                {
                    Color renk = kalibreEdilecekForm.GetPixel(i, j);
                    Color cr = kalibreEdilecekForm.GetPixel(i + 1, j);
                    Color cl = kalibreEdilecekForm.GetPixel(i - 1, j);
                    Color cu = kalibreEdilecekForm.GetPixel(i, j - 1);
                    Color cd = kalibreEdilecekForm.GetPixel(i, j + 1);
                    int dx = cr.R - cl.R;
                    int dy = cd.R - cu.R;
                    double power = Math.Sqrt((dx * dx / 4) + (dy * dy / 4));

                    if (kalibrasyonYapildiMi == true)
                    {
                        parlaklikesigi = tumFormlarinDeğerleri[formSirasi].getParlaklikEsigi();
                    }

                    if (power > parlaklikesigi)
                    {
                        kosekoordinatlari3.Add(new Point(i, j));
                    }
                }
            }

            if (kosekoordinatlari3.Count > 0)
            {
                koseKareBaslangicNoktalari.Add(kosekoordinatlari3[0]);
                koseKareBaslangicNoktalari.Add(kosekoordinatlari3[kosekoordinatlari3.Count - 1]);
                kosekoordinatlari3.Clear();
            }
        }

        private void egrilikGider(Bitmap kalibreEdilecekForm,Bitmap anaForm,int formSirasi)
        {
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                int solUstX = koseKareBaslangicNoktalari[0].X;
                int solAltX = koseKareBaslangicNoktalari[4].X;
                int solUstY = koseKareBaslangicNoktalari[0].Y;
                int sagUstY = koseKareBaslangicNoktalari[2].Y;

                int gen = 0;
                int yuk = 0;

                while (solUstX > solAltX)
                {
                    gen += 1;
                    Point[] koseler = new Point[3];
                    koseler[0] = new Point(0, 0);
                    koseler[1] = new Point(kalibreEdilecekForm.Width - 1, 0);
                    koseler[2] = new Point(gen, kalibreEdilecekForm.Height - 1);

                    Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                    g.Clear(Color.White);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(anaForm, koseler);
                    g.Dispose();

                    kenarBul(kalibreEdilecekForm, formSirasi);

                    if (koseKareBaslangicNoktalari.Count > 4)
                    {
                        solUstX = koseKareBaslangicNoktalari[0].X;
                        solAltX = koseKareBaslangicNoktalari[4].X;
                    }
                    else { break; }

                }

                while (solUstX < solAltX)
                {
                    gen -= 1;
                    Point[] koseler = new Point[3];
                    koseler[0] = new Point(0, 0);
                    koseler[1] = new Point(kalibreEdilecekForm.Width - 1, 0);
                    koseler[2] = new Point(gen, kalibreEdilecekForm.Height - 1);

                    Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                    g.Clear(Color.White);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(anaForm, koseler);
                    g.Dispose();

                    kenarBul(kalibreEdilecekForm, formSirasi);
                    if (koseKareBaslangicNoktalari.Count > 4)
                    {
                        solUstX = koseKareBaslangicNoktalari[0].X;
                        solAltX = koseKareBaslangicNoktalari[4].X;
                    }
                    else { break; }

                }

                while (solUstY > sagUstY)
                {
                    yuk += 1;
                    Point[] koseler = new Point[3];
                    koseler[0] = new Point(0, 0);
                    koseler[1] = new Point(kalibreEdilecekForm.Width - 1, yuk);
                    koseler[2] = new Point(gen, kalibreEdilecekForm.Height - 1);

                    Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                    g.Clear(Color.White);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(anaForm, koseler);
                    g.Dispose();

                    kenarBul(kalibreEdilecekForm, formSirasi);
                    if (koseKareBaslangicNoktalari.Count > 2)
                    {
                        solUstY = koseKareBaslangicNoktalari[0].Y;
                        sagUstY = koseKareBaslangicNoktalari[2].Y;
                    }
                    else { break; }

                }

                while (solUstY < sagUstY)
                {
                    yuk -= 1;
                    Point[] koseler = new Point[3];
                    koseler[0] = new Point(0, 0);
                    koseler[1] = new Point(kalibreEdilecekForm.Width - 1, yuk);
                    koseler[2] = new Point(gen, kalibreEdilecekForm.Height - 1);

                    Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                    g.Clear(Color.White);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(anaForm, koseler);
                    g.Dispose();

                    kenarBul(kalibreEdilecekForm, formSirasi);
                    if (koseKareBaslangicNoktalari.Count > 2)
                    {
                        solUstY = koseKareBaslangicNoktalari[0].Y;
                        sagUstY = koseKareBaslangicNoktalari[2].Y;
                    }
                    else { break; }
                }
            }
        }

        private void RaporlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Raporlar raporlar = new Raporlar();
            raporlar.Show();
        }

        private void OturumBirleştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OturumBirlestir oturumBirlestir = new OturumBirlestir();
            oturumBirlestir.Show();
        }
    }
}
