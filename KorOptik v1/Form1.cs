using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KorOptik_v1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int izgaraBoşlukGenislik = 9;
        int izgaraKareGenislik = 7;
        public List<OgrenciOkunan> okunanlar;
        List<Point> koseKareBaslangicNoktalari;
        List<Point> kosekoordinatlari1 = new List<Point>();
        List<Point> kosekoordinatlari2 = new List<Point>();
        List<Point> kosekoordinatlari3 = new List<Point>();
        List<Point> baslangicnoktalari = new List<Point>();
        List<string> okunacakStandartAlanAdlari;
        List<string> okunacakDersAdlari;
        List<String> kayitliTumDersler;
        List<string> paths = new List<string>();
        List<int> soruSayilari;
        List<int> okunacakDersIndexleri;
        int eklenenDersSayisi;
        public Boolean kalibrasyonYapildiMi = false;
        public List<FormDegerleri> tumFormlarinDeğerleri = new List<FormDegerleri>();

        public String okulturu = "";
        String formadi = "";
        int grilikesigi;
        int parlaklikesigi;
        int sikokumahassasiyeti;
        Point formebatlari;
        Bitmap resimm;
        private bool buttonStopClicked = false;

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
           // try
           // {
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
                    this.Cursor = Cursors.AppStarting;

                    buttonStop.Visible = true;

                    okunacakStandartAlanAdlari = new List<string>();
                    okunacakDersAdlari = new List<string>();
                    okunacakDersIndexleri = new List<int>();
                    kolonIsimleriniGoruntule(formadi, dataGridViewOkunanlar, okunacakStandartAlanAdlari, okunacakDersAdlari, okunacakDersIndexleri);

                    okunacakStandartAlanAdlari = new List<string>();
                    okunacakDersAdlari = new List<string>();
                    formBilgileriGetir(comboBoxFormTuru.SelectedItem.ToString(), okunacakStandartAlanAdlari, okunacakDersAdlari);

                    progressBar1.Maximum = paths.Count;
                    progressBar1.Minimum = 0;
                    progressBar1.Value = 0;
                    progressBar1.Show();
                    labelOkunanSayisi.Visible = true;
                    labelOkunanSayisi.Text = "0/" + paths.Count.ToString();

                    List<String> okunamayanlar = new List<string>();
                    for (int m = 0; m < paths.Count; m++)
                    {
                        if (buttonStopClicked == true)
                        {
                            buttonStopClicked = false;
                            break;

                        }
                        progressBar1.Value += 1;
                        labelOkunanSayisi.Text = (m + 1).ToString() + "/" + paths.Count.ToString();

                        Bitmap secilenForm = new Bitmap(paths[m]);
                        Bitmap okunacakForm;
                        Bitmap anaForm;
                        if (kalibrasyonYapildiMi == true)
                        {
                            okunacakForm = new Bitmap(secilenForm, tumFormlarinDeğerleri[m].getFormWidht(), tumFormlarinDeğerleri[m].getFormHeight());
                            anaForm = new Bitmap(okunacakForm);
                            sikokumahassasiyeti = tumFormlarinDeğerleri[m].getSikOkumaHassasiyeti();
                            grilikesigi = tumFormlarinDeğerleri[m].getGrilikEsigi();
                            parlaklikesigi = tumFormlarinDeğerleri[m].getParlaklikEsigi();
                        }
                        else
                        {
                            okunacakForm = new Bitmap(secilenForm, formebatlari.X, formebatlari.Y);
                            anaForm = new Bitmap(okunacakForm);
                        }

                        kenarBul(okunacakForm, m);
                        egrilikGider(okunacakForm, anaForm, m);

                        if (koseKareBaslangicNoktalari.Count == 3)
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
                            binaryyap(okunacakForm, basX, basY, 20 * izgaraBoşlukGenislik, 29 * izgaraBoşlukGenislik, m);
                            String ogrAdi = adsoyadOku(okunacakForm, basX, basY, sikokumahassasiyeti);

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
                            binaryyap(okunacakForm, basX, basY, 4 * izgaraBoşlukGenislik, 10 * izgaraBoşlukGenislik, m);
                            string ogrNumarasi = ogrenciNoOku(okunacakForm, basX, basY, sikokumahassasiyeti);

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
                            binaryyap(okunacakForm, basX, basY, 6 * izgaraBoşlukGenislik, 10 * izgaraBoşlukGenislik, m);
                            string okulkodu = okulKoduOku(okunacakForm, basX, basY, sikokumahassasiyeti);

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
                            binaryyap(okunacakForm, basX, basY, 1 * izgaraBoşlukGenislik, 4 * izgaraBoşlukGenislik, m);
                            string kitTuru = kitapcikTuruOku(okunacakForm, basX, basY, sikokumahassasiyeti);

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
                            binaryyap(okunacakForm, basX, basY, 1 * izgaraBoşlukGenislik, 12 * izgaraBoşlukGenislik, m);
                            binaryyap(okunacakForm, basX+ izgaraBoşlukGenislik, basY, 1 * izgaraBoşlukGenislik, 29 * izgaraBoşlukGenislik, m);
                            string sinif = sinifOku(okunacakForm, basX, basY, sikokumahassasiyeti);
                            string sube = subeOku(okunacakForm, basX + izgaraBoşlukGenislik, basY, sikokumahassasiyeti);

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

                                binaryyap(okunacakForm, basX+izgaraBoşlukGenislik, basY, sikSayisi * izgaraBoşlukGenislik, sorusayisi * izgaraBoşlukGenislik, m);
                                string cevap = dersleriOku(okunacakForm, basX+izgaraBoşlukGenislik, basY, okulturu, sorusayisi, sikSayisi, sikokumahassasiyeti);
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
                    buttonStop.Visible = false;
                }
                this.Cursor = Cursors.Default;
                progressBar1.Hide();
                labelOkunanSayisi.Visible = false;
           //}
            //catch (Exception h)
           // {
              //  this.Cursor = Cursors.Default;
              //  MessageBox.Show(h.Message);
               
           // }
        }

        ///okuma bitiş

        private void değerlendirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewOkunanlar.RowCount == 0)
            {
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

        public String adsoyadOku(Bitmap b, int basX, int basY, int sikOkumaHassasligi)
        {
            int x1 = basX;
            int x2 = basX + 20 * izgaraBoşlukGenislik;
            int y1 = basY;
            int y2 = basY + 29 * izgaraBoşlukGenislik;

            List<int> sutunlardakiSiyahPikselSayilari = new List<int>();
            for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
            {
                for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                {
                    List<int> siyahpikseller = new List<int>();
                    for (int m = i; m < i + izgaraKareGenislik; m++)
                    {
                        for (int n = j; n < j + izgaraKareGenislik; n++)
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
                    if (sutunlardakiSiyahPikselSayilari[k] > sikOkumaHassasligi)
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

        public String ogrenciNoOku(Bitmap b, int basX, int basY, int sikOkumaHassasligi)
        {
            int x1 = basX;
            int x2 = basX + 4 * izgaraBoşlukGenislik;
            int y1 = basY;
            int y2 = basY + 10 * izgaraBoşlukGenislik;

            List<int> sutunlardakiSiyahPikselSayilari = new List<int>();
            for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
            {
                for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                {
                    List<int> siyahpikseller = new List<int>();
                    for (int m = i; m < i + izgaraKareGenislik; m++)
                    {
                        for (int n = j; n < j + izgaraKareGenislik; n++)
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
                    if (sutunlardakiSiyahPikselSayilari[k] > sikOkumaHassasligi)
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

        public String sinifOku(Bitmap b, int basX, int basY, int sikOkumaHassasligi)
        {
            int x1 = basX;
            int x2 = basX + 1 * izgaraBoşlukGenislik;
            int y1 = basY;
            int y2 = basY + 12 * izgaraBoşlukGenislik;

            List<int> sutunlardakiSiyahPikselSayilari = new List<int>();
            for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
            {
                for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                {
                    List<int> siyahpikseller = new List<int>();
                    for (int m = i; m < i + izgaraKareGenislik; m++)
                    {
                        for (int n = j; n < j + izgaraKareGenislik; n++)
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
                    if (sutunlardakiSiyahPikselSayilari[k] > sikOkumaHassasligi)
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

        public String subeOku(Bitmap b, int basX, int basY, int sikOkumaHassasligi)
        {
            int x1 = basX;
            int x2 = basX + izgaraBoşlukGenislik;
            int y1 = basY;
            int y2 = basY + 29 * izgaraBoşlukGenislik;

            List<int> sutunlardakiSiyahPikselSayilari = new List<int>();
            for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
            {
                for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                {
                    List<int> siyahpikseller = new List<int>();
                    for (int m = i; m < i + izgaraKareGenislik; m++)
                    {
                        for (int n = j; n < j + izgaraKareGenislik; n++)
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
                    if (sutunlardakiSiyahPikselSayilari[k] > sikOkumaHassasligi)
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

        public String kitapcikTuruOku(Bitmap b, int basX, int basY, int sikOkumaHassasligi)
        {
            int x1 = basX;
            int x2 = basX + izgaraBoşlukGenislik;
            int y1 = basY;
            int y2 = basY + 4 * izgaraBoşlukGenislik;

            List<int> sutunlardakiSiyahPikselSayilari = new List<int>();
            for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
            {
                for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                {
                    List<int> siyahpikseller = new List<int>();
                    for (int m = i; m < i + izgaraKareGenislik; m++)
                    {
                        for (int n = j; n < j + izgaraKareGenislik; n++)
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
                    if (sutunlardakiSiyahPikselSayilari[k] > sikOkumaHassasligi)
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

        public String okulKoduOku(Bitmap b, int basX, int basY, int sikOkumaHassasligi)
        {
            int x1 = basX;
            int x2 = basX + 6 * izgaraBoşlukGenislik;
            int y1 = basY;
            int y2 = basY + 10 * izgaraBoşlukGenislik;

            List<int> sutunlardakiSiyahPikselSayilari = new List<int>();
            for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
            {
                for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                {
                    List<int> siyahpikseller = new List<int>();
                    for (int m = i; m < i + izgaraKareGenislik; m++)
                    {
                        for (int n = j; n < j + izgaraKareGenislik; n++)
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
                    if (sutunlardakiSiyahPikselSayilari[k] > sikOkumaHassasligi)
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

        public String dersleriOku(Bitmap b, int basX, int basY, String okulTuru, int soruSayisi, int sikSayisi, int sikOkumaHassasligi)
        {
            int x1 = basX;
            int x2 = basX + sikSayisi * izgaraBoşlukGenislik;
            int y1 = basY;
            int y2 = basY + soruSayisi * izgaraBoşlukGenislik;

            List<int> sutunlardakiSiyahPikselSayilari = new List<int>();
            for (int i = y1; i < y2; i += izgaraBoşlukGenislik)
            {
                for (int j = x1; j < x2; j += izgaraBoşlukGenislik)
                {
                    List<int> siyahpikseller = new List<int>();
                    for (int m = i; m < i + izgaraKareGenislik; m++)
                    {
                        for (int n = j; n < j + izgaraKareGenislik; n++)
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
                    if (sutunlardakiSiyahPikselSayilari[k] > sikOkumaHassasligi)
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

        public void kolonIsimleriniGoruntule(String formadi, DataGridView dataGridView, List<string> okunacakStdAlanlar, List<String> okunacakDersler, List<int> okunacakDersIndexs)
        {
            okunacakDersler = new List<string>();
            okunacakStdAlanlar = new List<string>();
            okunacakDersIndexs = new List<int>();
            dataGridView.Columns.Clear();
            List<String> dersNames = new List<string>();
            List<int> standartAlanlarIndexler = new List<int>();
            List<Point> baslangicPoints = new List<Point>();

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

                    if (kalibrasyonYapildiMi == true)
                    {
                        grilikesigi = tumFormlarinDeğerleri[formSirasi].getGrilikEsigi();
                    }

                    for (int y = 0; y < yukseklik; ++y)
                    {
                        for (int x = 0; x < genislik; ++x)
                        {
                            // GriResim[x, y] = (double)(.299 * p[0] + .587 * p[1] + .114 * p[2]);
                            GriResim[x, y] = (double)((p[0] + p[1] + p[2]) / 3);
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

        private static List<Point> sobelOperator(Bitmap sourceImage, Point startPoint, int width, int height, double factor = 1, int bias = 0, bool grayscale = false)
        {
            double[,] xkernel = new double[,]{
                    { -1, 0, 1 },
                    { -2, 0, 2 },
                    { -1, 0, 1 }
            };

            double[,] ykernel = new double[,]{
                    {  1,  2,  1 },
                    {  0,  0,  0 },
                    { -1, -2, -1 }
            };

            //Lock source image bits into system memory
            BitmapData srcData = sourceImage.LockBits(new Rectangle(startPoint.X, startPoint.Y, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            //Get the total number of bytes in your image - 32 bytes per pixel x image width x image height -> for 32bpp images
            int bytes = srcData.Stride * srcData.Height;

            //Create byte arrays to hold pixel information of your image
            byte[] pixelBuffer = new byte[bytes];
            byte[] resultBuffer = new byte[bytes];

            //Get the address of the first pixel data
            IntPtr srcScan0 = srcData.Scan0;

            //Copy image data to one of the byte arrays
            Marshal.Copy(srcScan0, pixelBuffer, 0, bytes);

            //Unlock bits from system memory -> we have all our needed info in the array
            sourceImage.UnlockBits(srcData);

            //Create variable for pixel data for each kernel
            double xr = 0.0;
            double xg = 0.0;
            double xb = 0.0;
            double yr = 0.0;
            double yg = 0.0;
            double yb = 0.0;
            double rt = 0.0;
            double gt = 0.0;
            double bt = 0.0;

            //This is how much your center pixel is offset from the border of your kernel
            //Sobel is 3x3, so center is 1 pixel from the kernel border
            int filterOffset = 1;
            int calcOffset = 0;
            int byteOffset = 0;

            //Start with the pixel that is offset 1 from top and 1 from the left side
            //this is so entire kernel is on your image
            List<Point> sınırKoordinatları = new List<Point>();

            for (int OffsetY = filterOffset; OffsetY < height - filterOffset; OffsetY++)
            {
                for (int OffsetX = filterOffset; OffsetX < width - filterOffset; OffsetX++)
                {
                    //reset rgb values to 0
                    xr = xg = xb = yr = yg = yb = 0;
                    rt = gt = bt = 0.0;

                    //position of the kernel center pixel
                    byteOffset = OffsetY * srcData.Stride + OffsetX * 4;

                    //kernel calculations
                    for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset + filterX * 4 + filterY * srcData.Stride;
                            xb += (double)(pixelBuffer[calcOffset]) * xkernel[filterY + filterOffset, filterX + filterOffset];
                            xg += (double)(pixelBuffer[calcOffset + 1]) * xkernel[filterY + filterOffset, filterX + filterOffset];
                            xr += (double)(pixelBuffer[calcOffset + 2]) * xkernel[filterY + filterOffset, filterX + filterOffset];
                            yb += (double)(pixelBuffer[calcOffset]) * ykernel[filterY + filterOffset, filterX + filterOffset];
                            yg += (double)(pixelBuffer[calcOffset + 1]) * ykernel[filterY + filterOffset, filterX + filterOffset];
                            yr += (double)(pixelBuffer[calcOffset + 2]) * ykernel[filterY + filterOffset, filterX + filterOffset];
                        }
                    }

                    //total rgb values for this pixel
                    bt = Math.Sqrt((xb * xb) + (yb * yb));
                    gt = Math.Sqrt((xg * xg) + (yg * yg));
                    rt = Math.Sqrt((xr * xr) + (yr * yr));

                    //set limits, bytes can hold values from 0 up to 255;
                    if (bt > 255) bt = 255;
                    else if (bt < 0) bt = 0;
                    if (gt > 255) gt = 255;
                    else if (gt < 0) gt = 0;
                    if (rt > 255) rt = 255;
                    else if (rt < 0) rt = 0;

                    if (bt == 255 && gt == 255 && rt == 255)
                    {
                        sınırKoordinatları.Add(new Point(OffsetX + startPoint.X, OffsetY + startPoint.Y));
                    }
                }
            }
            return sınırKoordinatları;
        }
        private void kenarBul(Bitmap kalibreEdilecekForm, int formSirasi)
        {

            if (kalibrasyonYapildiMi == true)
            {
                parlaklikesigi = tumFormlarinDeğerleri[formSirasi].getParlaklikEsigi();
            }

            bool solUstVarMı = true;
            bool sagUstVarMı = true;
            bool solAltVarMı = true;
            int width = 90;
            int height = 90;

            koseKareBaslangicNoktalari = new List<Point>();

            ///////////sağ alt////////////////////////
            Point startPoint4 = new Point(kalibreEdilecekForm.Width - 100, kalibreEdilecekForm.Height - 100);
            List<Point> kosekoordinatlari4 = sobelOperator(kalibreEdilecekForm, startPoint4, width, height, 1.0, 0, true);
            if (kosekoordinatlari4.Count > parlaklikesigi)
            {
                kalibreEdilecekForm.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }

            ///////////Sol Üst////////////////////////
            string s = "";
            Point startPoint1 = new Point(10, 10);
            kosekoordinatlari1 = sobelOperator(kalibreEdilecekForm, startPoint1, width, height, 1.0, 0, true);

            if (kosekoordinatlari1.Count() > parlaklikesigi)
            {
                double ortTopX = 0;
                double ortTopY = 0;

                for (int i = 0; i < kosekoordinatlari1.Count(); i++)
                {
                    ortTopX += (double)(kosekoordinatlari1[kosekoordinatlari1.Count() - 1 - i].X + kosekoordinatlari1[i].X) / 2;
                    ortTopY += (double)(kosekoordinatlari1[kosekoordinatlari1.Count() - 1 - i].Y + kosekoordinatlari1[i].Y) / 2;
                }

                int ortX = (int)Math.Round(ortTopX / kosekoordinatlari1.Count());
                int ortY = (int)Math.Round(ortTopY / kosekoordinatlari1.Count());

                koseKareBaslangicNoktalari.Add(new Point(ortX, ortY));
            }
            else
            {
                solUstVarMı = false;
            }


            ///////////sağ Üst////////////////////////
            Point startPoint2 = new Point(kalibreEdilecekForm.Width - 100, 10);
            kosekoordinatlari2 = sobelOperator(kalibreEdilecekForm, startPoint2, width, height, 1.0, 0, true);
            if (kosekoordinatlari2.Count() > parlaklikesigi)
            {
                double ortTopX = 0;
                double ortTopY = 0;

                for (int i = 0; i < kosekoordinatlari2.Count(); i++)
                {
                    ortTopX += (double)(kosekoordinatlari2[kosekoordinatlari2.Count() - 1 - i].X + kosekoordinatlari2[i].X) / 2;
                    ortTopY += (double)(kosekoordinatlari2[kosekoordinatlari2.Count() - 1 - i].Y + kosekoordinatlari2[i].Y) / 2;
                }

                int ortX = (int)Math.Round(ortTopX / kosekoordinatlari2.Count());
                int ortY = (int)Math.Round(ortTopY / kosekoordinatlari2.Count());

                koseKareBaslangicNoktalari.Add(new Point(ortX, ortY));
            }
            else
            {
                sagUstVarMı = false;
            }

            ///////////Sol alt////////////////////////
            Point startPoint3 = new Point(10, kalibreEdilecekForm.Height - 100);
            kosekoordinatlari3 = sobelOperator(kalibreEdilecekForm, startPoint3, width, height, 1.0, 0, true);
            if (kosekoordinatlari3.Count() > parlaklikesigi)
            {
                double ortTopX = 0;
                double ortTopY = 0;

                for (int i = 0; i < kosekoordinatlari3.Count(); i++)
                {
                    ortTopX += (double)(kosekoordinatlari3[kosekoordinatlari3.Count() - 1 - i].X + kosekoordinatlari3[i].X) / 2;
                    ortTopY += (double)(kosekoordinatlari3[kosekoordinatlari3.Count() - 1 - i].Y + kosekoordinatlari3[i].Y) / 2;
                }

                int ortX = (int)Math.Round(ortTopX / kosekoordinatlari3.Count());
                int ortY = (int)Math.Round(ortTopY / kosekoordinatlari3.Count());

                koseKareBaslangicNoktalari.Add(new Point(ortX, ortY));
            }
            else
            {
                solAltVarMı = false;
            }
        }

        private void egrilikGider(Bitmap kalibreEdilecekForm, Bitmap anaForm, int formSirasi)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int solUstX = koseKareBaslangicNoktalari[0].X;
                int solAltX = koseKareBaslangicNoktalari[2].X;
                int solUstY = koseKareBaslangicNoktalari[0].Y;
                int sagUstY = koseKareBaslangicNoktalari[1].Y;

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

                    if (koseKareBaslangicNoktalari.Count > 2)
                    {
                        solUstX = koseKareBaslangicNoktalari[0].X;
                        solAltX = koseKareBaslangicNoktalari[2].X;
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
                    if (koseKareBaslangicNoktalari.Count > 2)
                    {
                        solUstX = koseKareBaslangicNoktalari[0].X;
                        solAltX = koseKareBaslangicNoktalari[2].X;
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
                    if (koseKareBaslangicNoktalari.Count > 1)
                    {
                        solUstY = koseKareBaslangicNoktalari[0].Y;
                        sagUstY = koseKareBaslangicNoktalari[1].Y;
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
                    if (koseKareBaslangicNoktalari.Count > 1)
                    {
                        solUstY = koseKareBaslangicNoktalari[0].Y;
                        sagUstY = koseKareBaslangicNoktalari[1].Y;
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

        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBoxFormTuru.SelectedIndex < 0)
            {
                MessageBox.Show("Seçilmiş form yok!");
            }
            else
            {
                DialogResult soru = MessageBox.Show(comboBoxFormTuru.SelectedItem.ToString() + " formunu silmek istediğinizden emin misiniz ?", "Uyarı",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (soru == DialogResult.Yes)
                {

                    Veritabani vt = new Veritabani();
                    vt.baglan();
                    vt.formuSil(comboBoxFormTuru.SelectedItem.ToString());
                    if (System.IO.File.Exists(@"KaydedilenFormlar/" + comboBoxFormTuru.SelectedItem.ToString() + ".jpg"))
                    {
                        System.IO.File.Delete(@"KaydedilenFormlar/" + comboBoxFormTuru.SelectedItem.ToString() + ".jpg");
                    }

                    MessageBox.Show(comboBoxFormTuru.SelectedItem.ToString() + " silindi.");

                    vt.baglan();
                    List<String> kayitliFormAdlari = new List<string>();
                    kayitliFormAdlari = vt.kayitliFormlarınIsimleriniGetir();

                    comboBoxFormTuru.Items.Clear();

                    if (kayitliFormAdlari.Count != 0)
                    {
                        for (int i = 0; i < kayitliFormAdlari.Count; i++)
                        {
                            comboBoxFormTuru.Items.Add(kayitliFormAdlari[i]);
                        }
                    }

                }
            }
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            buttonStopClicked = true;

        }
    }
}
