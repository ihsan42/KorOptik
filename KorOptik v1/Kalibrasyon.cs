using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace KorOptik_v1
{
    public partial class Kalibrasyon : Form
    {
        public Kalibrasyon()
        {
            InitializeComponent();
        }

        int sikOkumaHassasiyeti;
        int grilikesigi;
        int parlaklikesigi;
        int widhtForm;
        int heightForm;
        int gosterilenFormSirasi = 0;       
        Bitmap resimm;
        Bitmap kalibreEdilecekForm;
        Bitmap anaForm;

        List<FormDegerleri> tumFormlarinDeğerleriKalibre = new List<FormDegerleri>();
        public List<String> pathsSecilen = new List<string>();
        List<string> dersAdlariKalibre = new List<string>();
        List<String> okunacakStandartAlanlarKalibre = new List<string>();
        List<int> soruSayilariKalibre = new List<int>();
        List<Point> baslangicnoktalariKalibre = new List<Point>();
        String okulTuruKalibre = "";
        int eklenenDersSayısıKalibre;
        public String formadiKalibre = "";
        private List<Point> koseKareBaslangicNoktalari;       

        private void Kalibrasyon_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            formBilgileriGetir(formadiKalibre, okunacakStandartAlanlarKalibre);
            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            if (form1.kalibrasyonYapildiMi == false)
            {
                Bitmap secilenform = new Bitmap(pathsSecilen[gosterilenFormSirasi]);
                kalibreEdilecekForm = new Bitmap(secilenform, widhtForm, heightForm);
                anaForm = new Bitmap(kalibreEdilecekForm);
                secilenform.Dispose();

                tumFormlarinDeğerleriKalibre = new List<FormDegerleri>();
                for (int i = 0; i < pathsSecilen.Count; i++)
                {
                    FormDegerleri baslangicNoktalariObject = new FormDegerleri();
                    baslangicNoktalariObject.setFormWidht(widhtForm);
                    baslangicNoktalariObject.setFormHeight(heightForm);
                    baslangicNoktalariObject.setGrilikEsigi(grilikesigi);
                    baslangicNoktalariObject.setParlaklikEsigi(parlaklikesigi);
                    baslangicNoktalariObject.setSikOkumaHassasiyeti(sikOkumaHassasiyeti);                    
                    baslangicNoktalariObject.setAdSoyadBaslangic(baslangicnoktalariKalibre[0].X, baslangicnoktalariKalibre[0].Y);
                    baslangicNoktalariObject.setOgrenciNoBaslangic(baslangicnoktalariKalibre[1].X, baslangicnoktalariKalibre[1].Y);
                    baslangicNoktalariObject.setSinifSubeBaslangic(baslangicnoktalariKalibre[2].X, baslangicnoktalariKalibre[2].Y);
                    baslangicNoktalariObject.setKitapcikTuruBaslangic(baslangicnoktalariKalibre[3].X, baslangicnoktalariKalibre[3].Y);
                    baslangicNoktalariObject.setOkulKoduBaslangic(baslangicnoktalariKalibre[4].X, baslangicnoktalariKalibre[4].Y);

                    List<Point> dersBasNoktalari = new List<Point>();
                    for (int j = 6; j < baslangicnoktalariKalibre.Count; j++)
                    {
                        dersBasNoktalari.Add(baslangicnoktalariKalibre[j]);
                    }
                    baslangicNoktalariObject.addDersBaslangicNoktalari(dersBasNoktalari);

                    tumFormlarinDeğerleriKalibre.Add(baslangicNoktalariObject);
                }

                kenarBul();

                if (koseKareBaslangicNoktalari.Count == 6)
                {
                    for (int i = 0; i < pathsSecilen.Count; i++)
                    {                                          
                        if (koseKareBaslangicNoktalari.Count > 0)
                        {
                            tumFormlarinDeğerleriKalibre[i].setsolUstSiyahKareBaslangic(koseKareBaslangicNoktalari[0].X, koseKareBaslangicNoktalari[0].Y);
                        }
                        else
                        {
                            tumFormlarinDeğerleriKalibre[i].setsolUstSiyahKareBaslangic(0, 0);
                        }                        
                    }
                    egrilikGider();
                    anaForm = new Bitmap(kalibreEdilecekForm);                 
                    pictureBox1.Invalidate();                 
                    pictureBox1.Image = kalibreEdilecekForm;
                }
                else
                {                   
                    pictureBox1.Image = kalibreEdilecekForm;
                    MessageBox.Show("Köşelerdeki kareler bulunamadı. Lütfen köşe kare hassasiyetini değiştirin!");
                }
                labelParlaklikEsigi.Text = parlaklikesigi.ToString();
                labelEsikDegeri.Text = grilikesigi.ToString();
                labelSikOkumaHassasiyeti.Text = sikOkumaHassasiyeti.ToString();
                textBoxPageNumber.Text = (gosterilenFormSirasi + 1).ToString();
                textBoxPageCount.Text = pathsSecilen.Count.ToString();
            }
            else
            {
                tumFormlarinDeğerleriKalibre = new List<FormDegerleri>();
                foreach (FormDegerleri formDegerleri in form1.tumFormlarinDeğerleri)
                {
                    tumFormlarinDeğerleriKalibre.Add(formDegerleri);
                }
                formuGoruntule();
            }

            foreach (String s in okunacakStandartAlanlarKalibre)
            {
                comboBoxAlanlar.Items.Add(s);
            }

            int sayac = 0;
            foreach (String s in dersAdlariKalibre)
            {
                if (!s.Equals(""))
                {
                    comboBoxAlanlar.Items.Add(s);
                    sayac++;
                }
            }
            eklenenDersSayısıKalibre = sayac;
            this.Cursor = Cursors.Default;
            comboBoxAlanlar.SelectedIndex = 0;
        }

        public void binaryyap(Bitmap b, int x1, int y1, int genislik, int yukseklik)
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
                            if (GriResim[x, y] < tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getGrilikEsigi())
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

        private void kenarBul()
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

                    if (power > tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getParlaklikEsigi())
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
                    if (power > tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getParlaklikEsigi())
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

                    if (power > tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getParlaklikEsigi())
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
      
        private void egrilikGider()
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

                    kenarBul();

                    if (koseKareBaslangicNoktalari.Count > 4) {
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

                    kenarBul();
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

                    kenarBul();
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

                    kenarBul();
                    if (koseKareBaslangicNoktalari.Count > 2)
                    {
                        solUstY = koseKareBaslangicNoktalari[0].Y;
                        sagUstY = koseKareBaslangicNoktalari[2].Y;
                    }
                    else { break; }
                }
            }           
        }

        private void tespitEdilenKenarlariGoster(PaintEventArgs g)
        {          
            ////////////Sağ Alt/////////////////////////
            List<Point> kosekoordinatlari4 = new List<Point>();
            for (int i = kalibreEdilecekForm.Width - 70; i < kalibreEdilecekForm.Width - 1; i++)
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

                    if (power > tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getParlaklikEsigi())
                    {
                        kosekoordinatlari4.Add(new Point(i, j));
                    }
                }
            }
            int genislik = 0;
            int yukseklik = 0;
            //Graphics g = Graphics.FromImage(kalibreEdilecekForm);          
            
            if (kosekoordinatlari4.Count > 0)
            {
                genislik = kosekoordinatlari4[kosekoordinatlari4.Count - 1].X - kosekoordinatlari4[0].X;
                yukseklik = kosekoordinatlari4[kosekoordinatlari4.Count - 1].Y - kosekoordinatlari4[0].Y;
                
                g.Graphics.DrawRectangle(new Pen(Color.Red, 1), kosekoordinatlari4[0].X - 2, kosekoordinatlari4[0].Y - 2, genislik + 4, yukseklik + 5);
                //g.Dispose();
            }

            if (koseKareBaslangicNoktalari.Count > 1)
            {
                ///////////Sol Üst////////////////////////                
                genislik = koseKareBaslangicNoktalari[1].X - koseKareBaslangicNoktalari[0].X;
                yukseklik = koseKareBaslangicNoktalari[1].Y - koseKareBaslangicNoktalari[0].Y;
                //g = Graphics.FromImage(kalibreEdilecekForm);
                //g.DrawRectangle(new Pen(Color.Red, 1), 0,0, 70, 70);
                g.Graphics.DrawRectangle(new Pen(Color.Red, 1), koseKareBaslangicNoktalari[0].X - 2, koseKareBaslangicNoktalari[0].Y - 2, genislik + 4, yukseklik + 5);
               // g.Dispose();
            }

            if (koseKareBaslangicNoktalari.Count > 3)
            {
                //////////Sağ Üst////////////////////////////              
                genislik = koseKareBaslangicNoktalari[3].X - koseKareBaslangicNoktalari[2].X;
                yukseklik = koseKareBaslangicNoktalari[3].Y - koseKareBaslangicNoktalari[2].Y;
                //g = Graphics.FromImage(kalibreEdilecekForm);
                g.Graphics.DrawRectangle(new Pen(Color.Red, 1), koseKareBaslangicNoktalari[2].X - 2, koseKareBaslangicNoktalari[2].Y - 2, genislik + 4, yukseklik + 5);
               // g.Dispose();
            }

            if (koseKareBaslangicNoktalari.Count > 5)
            {
                ////////////Sol Alt/////////////               
                genislik = koseKareBaslangicNoktalari[5].X - koseKareBaslangicNoktalari[4].X;
                yukseklik = koseKareBaslangicNoktalari[5].Y - koseKareBaslangicNoktalari[4].Y;
               // g = Graphics.FromImage(kalibreEdilecekForm);
                g.Graphics.DrawRectangle(new Pen(Color.Red, 1), koseKareBaslangicNoktalari[4].X - 2, koseKareBaslangicNoktalari[4].Y - 2, genislik + 4, yukseklik + 5);
                //g.Dispose();
            }

        }

        private void adsoyadBul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                //Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;
                Point p = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic();
                x1 = p.X + koseKareBaslangicNoktalari[0].X;
                x2 = p.X + koseKareBaslangicNoktalari[0].X + 180;
                y1 = p.Y + koseKareBaslangicNoktalari[0].Y;
                y2 = p.Y + koseKareBaslangicNoktalari[0].Y + 261;
                for (int i = x1; i < x2; i += 9)
                {
                    for (int j = y1; j < y2; j += 9)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i, j + 27, 9, 9);
                    }
                }
                //g.Dispose();
            }            
        }

        private void ogrenciNoBul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                //Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                Point p = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic();
                x1 = p.X + koseKareBaslangicNoktalari[0].X;
                x2 = p.X + koseKareBaslangicNoktalari[0].X + 36;
                y1 = p.Y + koseKareBaslangicNoktalari[0].Y;
                y2 = p.Y + koseKareBaslangicNoktalari[0].Y + 90;

                for (int i = x1; i < x2; i += 9)
                {
                    for (int j = y1; j < y2; j += 9)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i, j + 18, 9, 9);
                    }
                }
                //g.Dispose();
            }
        }

        private void sinifSubeBul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                //Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                Point p = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic();
                x1 = p.X + koseKareBaslangicNoktalari[0].X;
                x2 = p.X + koseKareBaslangicNoktalari[0].X + 18;
                y1 = p.Y + koseKareBaslangicNoktalari[0].Y;
                y2 = p.Y + koseKareBaslangicNoktalari[0].Y + 261;

                for (int i = x1; i < x2; i += 9)
                {
                    for (int j = y1; j < y2; j += 9)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i, j + 18, 9, 9);
                    }
                }
                //g.Dispose();
            }
        }

        private void kitapcikTuruBul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                //Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                Point p = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic();
                x1 = p.X + koseKareBaslangicNoktalari[0].X;
                x2 = p.X + koseKareBaslangicNoktalari[0].X + 9;
                y1 = p.Y + koseKareBaslangicNoktalari[0].Y;
                y2 = p.Y + koseKareBaslangicNoktalari[0].Y + 36;

                g.Graphics.DrawRectangle(new Pen(Color.Blue, 2), x1 + 3, y1, 45, 108);
                for (int i = x1; i < x2; i += 9)
                {
                    for (int j = y1; j < y2; j += 9)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 18, j + 63, 9, 9);
                    }
                }
                //g.Dispose();
            }
        }

        private void okulKoduBul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count>0)
            {
                //Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                Point p = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic();
                x1 = p.X + koseKareBaslangicNoktalari[0].X;
                x2 = p.X + koseKareBaslangicNoktalari[0].X + 54;
                y1 = p.Y + koseKareBaslangicNoktalari[0].Y;
                y2 = p.Y + koseKareBaslangicNoktalari[0].Y + 90;

                for (int i = x1; i < x2; i += 9)
                {
                    for (int j = y1; j < y2; j += 9)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i, j + 18, 9, 9);
                    }
                }
                //g.Dispose();
            }            
        }

        private void ders1Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                int yukseklik = 9 * soruSayilariKalibre[0];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = 9 * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = 9 * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = 9 * 5;
                }

               // Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                List<Point> dersbaslNok = new List<Point>();
                dersbaslNok = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                x1 = dersbaslNok[0].X + koseKareBaslangicNoktalari[0].X;
                x2 = dersbaslNok[0].X + koseKareBaslangicNoktalari[0].X + genislik;
                y1 = dersbaslNok[0].Y + koseKareBaslangicNoktalari[0].Y;
                y2 = dersbaslNok[0].Y + koseKareBaslangicNoktalari[0].Y + yukseklik;


                for (int i = x1; i < x2; i += 9)
                {
                    for (int j = y1; j < y2; j += 9)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 9, j, 9, 9);
                    }
                }
                //g.Dispose();
            }
        }

        private void ders2Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                int yukseklik = 9 * soruSayilariKalibre[1];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = 9 * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = 9 * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = 9 * 5;
                }

               // Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                List<Point> dersbaslNok = new List<Point>();
                dersbaslNok = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                x1 = dersbaslNok[1].X + koseKareBaslangicNoktalari[0].X;
                x2 = dersbaslNok[1].X + koseKareBaslangicNoktalari[0].X + genislik;
                y1 = dersbaslNok[1].Y + koseKareBaslangicNoktalari[0].Y;
                y2 = dersbaslNok[1].Y + koseKareBaslangicNoktalari[0].Y + yukseklik;


                for (int i = x1; i < x2; i += 9)
                {
                    for (int j = y1; j < y2; j += 9)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 9, j, 9, 9);
                    }
                }
                //g.Dispose();
            }
        }

        private void ders3Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                int yukseklik = 9 * soruSayilariKalibre[2];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = 9 * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = 9 * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = 9 * 5;
                }

                //Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                List<Point> dersbaslNok = new List<Point>();
                dersbaslNok = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                x1 = dersbaslNok[2].X + koseKareBaslangicNoktalari[0].X;
                x2 = dersbaslNok[2].X + koseKareBaslangicNoktalari[0].X + genislik;
                y1 = dersbaslNok[2].Y + koseKareBaslangicNoktalari[0].Y;
                y2 = dersbaslNok[2].Y + koseKareBaslangicNoktalari[0].Y + yukseklik;


                for (int i = x1; i < x2; i += 9)
                {
                    for (int j = y1; j < y2; j += 9)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 9, j, 9, 9);
                    }
                }
                //g.Dispose();
            }
        }

        private void ders4Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                int yukseklik = 9 * soruSayilariKalibre[3];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = 9 * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = 9 * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = 9 * 5;
                }

                //Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                List<Point> dersbaslNok = new List<Point>();
                dersbaslNok = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                x1 = dersbaslNok[3].X + koseKareBaslangicNoktalari[0].X;
                x2 = dersbaslNok[3].X + koseKareBaslangicNoktalari[0].X + genislik;
                y1 = dersbaslNok[3].Y + koseKareBaslangicNoktalari[0].Y;
                y2 = dersbaslNok[3].Y + koseKareBaslangicNoktalari[0].Y + yukseklik;


                for (int i = x1; i < x2; i += 9)
                {
                    for (int j = y1; j < y2; j += 9)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 9, j, 9, 9);
                    }
                }
               // g.Dispose();
            }
        }

        private void ders5Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                int yukseklik = 9 * soruSayilariKalibre[4];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = 9 * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = 9 * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = 9 * 5;
                }

               // Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                List<Point> dersbaslNok = new List<Point>();
                dersbaslNok = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                x1 = dersbaslNok[4].X + koseKareBaslangicNoktalari[0].X;
                x2 = dersbaslNok[4].X + koseKareBaslangicNoktalari[0].X + genislik;
                y1 = dersbaslNok[4].Y + koseKareBaslangicNoktalari[0].Y;
                y2 = dersbaslNok[4].Y + koseKareBaslangicNoktalari[0].Y + yukseklik;


                for (int i = x1; i < x2; i += 9)
                {
                    for (int j = y1; j < y2; j += 9)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 9, j, 9, 9);
                    }
                }
                //g.Dispose();
            }
        }

        private void ders6Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                int yukseklik = 9 * soruSayilariKalibre[5];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = 9 * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = 9 * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = 9 * 5;
                }

                //Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                List<Point> dersbaslNok = new List<Point>();
                dersbaslNok = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                x1 = dersbaslNok[5].X + koseKareBaslangicNoktalari[0].X;
                x2 = dersbaslNok[5].X + koseKareBaslangicNoktalari[0].X + genislik;
                y1 = dersbaslNok[5].Y + koseKareBaslangicNoktalari[0].Y;
                y2 = dersbaslNok[5].Y + koseKareBaslangicNoktalari[0].Y + yukseklik;


                for (int i = x1; i < x2; i += 9)
                {
                    for (int j = y1; j < y2; j += 9)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 9, j, 9, 9);
                    }
                }
               // g.Dispose();
            }
        }

        private void ders7Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                int yukseklik = 9 * soruSayilariKalibre[6];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = 9 * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = 9 * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = 9 * 5;
                }

                //Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                List<Point> dersbaslNok = new List<Point>();
                dersbaslNok = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                x1 = dersbaslNok[6].X + koseKareBaslangicNoktalari[0].X;
                x2 = dersbaslNok[6].X + koseKareBaslangicNoktalari[0].X + genislik;
                y1 = dersbaslNok[6].Y + koseKareBaslangicNoktalari[0].Y;
                y2 = dersbaslNok[6].Y + koseKareBaslangicNoktalari[0].Y + yukseklik;


                for (int i = x1; i < x2; i += 9)
                {
                    for (int j = y1; j < y2; j += 9)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 9, j, 9, 9);
                    }
                }
                //g.Dispose();
            }
        }

        private void ders8Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                int yukseklik = 9 * soruSayilariKalibre[7];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = 9 * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = 9 * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = 9 * 5;
                }

                //Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                List<Point> dersbaslNok = new List<Point>();
                dersbaslNok = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                x1 = dersbaslNok[7].X + koseKareBaslangicNoktalari[0].X;
                x2 = dersbaslNok[7].X + koseKareBaslangicNoktalari[0].X + genislik;
                y1 = dersbaslNok[7].Y + koseKareBaslangicNoktalari[0].Y;
                y2 = dersbaslNok[7].Y + koseKareBaslangicNoktalari[0].Y + yukseklik;


                for (int i = x1; i < x2; i += 9)
                {
                    for (int j = y1; j < y2; j += 9)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 9, j, 9, 9);
                    }
                }
                //g.Dispose();
            }
        }

        private void ders9Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                int yukseklik = 9 * soruSayilariKalibre[8];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = 9 * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = 9 * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = 9 * 5;
                }

                //Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                List<Point> dersbaslNok = new List<Point>();
                dersbaslNok = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                x1 = dersbaslNok[8].X + koseKareBaslangicNoktalari[0].X;
                x2 = dersbaslNok[8].X + koseKareBaslangicNoktalari[0].X + genislik;
                y1 = dersbaslNok[8].Y + koseKareBaslangicNoktalari[0].Y;
                y2 = dersbaslNok[8].Y + koseKareBaslangicNoktalari[0].Y + yukseklik;


                for (int i = x1; i < x2; i += 9)
                {
                    for (int j = y1; j < y2; j += 9)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 9, j, 9, 9);
                    }
                }
                //g.Dispose();
            }
        }

        private void ders10Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                int yukseklik = 9 * soruSayilariKalibre[9];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = 9 * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = 9 * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = 9 * 5;
                }

               // Graphics g = Graphics.FromImage(kalibreEdilecekForm);
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                List<Point> dersbaslNok = new List<Point>();
                dersbaslNok = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                x1 = dersbaslNok[9].X + koseKareBaslangicNoktalari[0].X;
                x2 = dersbaslNok[9].X + koseKareBaslangicNoktalari[0].X + genislik;
                y1 = dersbaslNok[9].Y + koseKareBaslangicNoktalari[0].Y;
                y2 = dersbaslNok[9].Y + koseKareBaslangicNoktalari[0].Y + yukseklik;


                for (int i = x1; i < x2; i += 9)
                {
                    for (int j = y1; j < y2; j += 9)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 9, j, 9, 9);
                    }
                }
                //g.Dispose();
            }           
        }

        private void buttonSikOkumaHassasiyetiAzalt_Click(object sender, EventArgs e)
        {          
            labelSikOkumaHassasiyeti.Text =(Convert.ToInt32(labelSikOkumaHassasiyeti.Text)-1).ToString();
        }

        private void buttonSikOkumaHassasiyetiArttir_Click(object sender, EventArgs e)
        {
            labelSikOkumaHassasiyeti.Text = (Convert.ToInt32(labelSikOkumaHassasiyeti.Text) + 1).ToString();
        }

        private void buttonSikHasUygula_Click(object sender, EventArgs e)
        {
            if (radioButtonGosterilen.Checked)
            {
                tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setSikOkumaHassasiyeti(Convert.ToInt32(labelSikOkumaHassasiyeti.Text));
            }
            else if (radioButtonTüm.Checked)
            {
                for (int i = 0; i < pathsSecilen.Count; i++)
                {
                    tumFormlarinDeğerleriKalibre[i].setSikOkumaHassasiyeti(Convert.ToInt32(labelSikOkumaHassasiyeti.Text));
                }
            }
        }

        private void buttonGrilikEsigiAzalt_Click(object sender, EventArgs e)
        {
            labelEsikDegeri.Text = (Convert.ToInt32(labelEsikDegeri.Text) - 1).ToString();         
        }

        private void buttonGrilikEsigiArttir_Click(object sender, EventArgs e)
        {
            labelEsikDegeri.Text = (Convert.ToInt32(labelEsikDegeri.Text) + 1).ToString();
        }

        private void buttonGrilikUygula_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            if (radioButtonGosterilen.Checked)
            {
                tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setGrilikEsigi(Convert.ToInt32(labelEsikDegeri.Text));
            }
            else if (radioButtonTüm.Checked)
            {
                for (int i = 0; i < pathsSecilen.Count; i++)
                {
                    tumFormlarinDeğerleriKalibre[i].setGrilikEsigi(Convert.ToInt32(labelEsikDegeri.Text));
                }
            }
            Bitmap secilenform = new Bitmap(pathsSecilen[gosterilenFormSirasi]);
            kalibreEdilecekForm = new Bitmap(secilenform, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht(), tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight());
            kenarBul();
            egrilikGider();
            anaForm = new Bitmap(kalibreEdilecekForm);
            binaryyap(kalibreEdilecekForm, 0, 0, kalibreEdilecekForm.Width, kalibreEdilecekForm.Height);

            pictureBox1.Invalidate();
            pictureBox1.Image = kalibreEdilecekForm;
            this.Cursor = Cursors.Default;
        }

        private void buttonParlaklikEşigiArttir_Click(object sender, EventArgs e)
        {
          
            labelParlaklikEsigi.Text = (Convert.ToInt32(labelParlaklikEsigi.Text) + 1).ToString();           
        }

        private void buttonParlaklikEşigiAzalt_Click(object sender, EventArgs e)
        {
            labelParlaklikEsigi.Text = (Convert.ToInt32(labelParlaklikEsigi.Text) - 1).ToString();

        }

        private void buttonParlaklikUygula_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.AppStarting;

            if (radioButtonGosterilen.Checked)
            {
                tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setParlaklikEsigi(Convert.ToInt32(labelParlaklikEsigi.Text));
            }
            else if (radioButtonTüm.Checked)
            {
                for (int i = 0; i < pathsSecilen.Count; i++)
                {
                    tumFormlarinDeğerleriKalibre[i].setParlaklikEsigi(Convert.ToInt32(labelParlaklikEsigi.Text));
                }
            }
            Bitmap secilenform = new Bitmap(pathsSecilen[gosterilenFormSirasi]);
            kalibreEdilecekForm = new Bitmap(secilenform, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht(), tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight());
            anaForm = new Bitmap(kalibreEdilecekForm);
            secilenform.Dispose();
            kenarBul();
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                egrilikGider();
                anaForm = new Bitmap(kalibreEdilecekForm);
                pictureBox1.Invalidate();
                pictureBox1.Image = kalibreEdilecekForm;
            }
            else
            {
                MessageBox.Show("Aralığın dışına çıktınız!");
            }
            this.Cursor = Cursors.Default;
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {          
            if (radioButtonGosterilen.Checked)
            {
                tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setFormWidht(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht() + 1);
                kalibreEdilecekForm = new Bitmap(anaForm, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht(), tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight());
            }
            else if (radioButtonTüm.Checked)
            {                            
                for (int i = 0; i < pathsSecilen.Count; i++)
                {
                    tumFormlarinDeğerleriKalibre[i].setFormWidht(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht() + 1);
                }
                kalibreEdilecekForm = new Bitmap(anaForm, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht(), tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight());
            }
            
            kenarBul();
            pictureBox1.Invalidate();           
            pictureBox1.Width = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht();
            pictureBox1.Height = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight();
            pictureBox1.Image = kalibreEdilecekForm;           
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {           
            if (radioButtonGosterilen.Checked)
            {
                tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setFormWidht(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht() - 1);
                kalibreEdilecekForm = new Bitmap(anaForm, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht(), tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight());
            }
            else if (radioButtonTüm.Checked)
            {
                for (int i = 0; i < pathsSecilen.Count; i++)
                {
                    tumFormlarinDeğerleriKalibre[i].setFormWidht(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht() - 1);
                }
                kalibreEdilecekForm = new Bitmap(anaForm, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht(), tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight());
            }
           
            kenarBul();          
            pictureBox1.Invalidate();
            pictureBox1.Width = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht();
            pictureBox1.Height = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight();
            pictureBox1.Image = kalibreEdilecekForm;            
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (radioButtonGosterilen.Checked)
            {
                tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setFormHeight(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight() + 1);
                kalibreEdilecekForm = new Bitmap(anaForm, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht(), tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight());
            }
            else if (radioButtonTüm.Checked)
            {
                for (int i = 0; i < pathsSecilen.Count; i++)
                {
                    tumFormlarinDeğerleriKalibre[i].setFormHeight(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight() + 1);
                }
                kalibreEdilecekForm = new Bitmap(anaForm, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht(), tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight());
            }

            kenarBul();          
            pictureBox1.Invalidate();
            pictureBox1.Width = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht();
            pictureBox1.Height = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight();
            pictureBox1.Image = kalibreEdilecekForm;

        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (radioButtonGosterilen.Checked)
            {
                tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setFormHeight(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight() - 1);
                kalibreEdilecekForm = new Bitmap(anaForm, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht(), tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight());
            }
            else if (radioButtonTüm.Checked)
            {
                for (int i = 0; i < pathsSecilen.Count; i++)
                {
                    tumFormlarinDeğerleriKalibre[i].setFormHeight(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight() - 1);
                }
                kalibreEdilecekForm = new Bitmap(anaForm, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht(), tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight());
            }

            kenarBul();
            pictureBox1.Invalidate();
            pictureBox1.Width = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht();
            pictureBox1.Height = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight();
            pictureBox1.Image = kalibreEdilecekForm;
        }

        private void buttonızgaraRight_Click(object sender, EventArgs e)
        {
            if (comboBoxAlanlar.SelectedIndex > -1)
            {
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Tüm Alanlar"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            List<Point> dersBasNoktalari = new List<Point>();
                            dersBasNoktalari = tumFormlarinDeğerleriKalibre[i].getDersBaslangicNoktalari();
                            for (int j = 0; j < eklenenDersSayısıKalibre; j++)
                            {
                                dersBasNoktalari[j] = new Point(dersBasNoktalari[j].X + 1, dersBasNoktalari[j].Y);
                            }                                
                            tumFormlarinDeğerleriKalibre[i].addDersBaslangicNoktalari(dersBasNoktalari);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        List<Point> dersBasNoktalari = new List<Point>();
                        dersBasNoktalari = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                        for (int j = 0; j < eklenenDersSayısıKalibre; j++)
                        {
                            dersBasNoktalari[j] = new Point(dersBasNoktalari[j].X + 1, dersBasNoktalari[j].Y);
                        }
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].addDersBaslangicNoktalari(dersBasNoktalari);
                    }

                    if (okunacakStandartAlanlarKalibre.Contains("Ad-Soyad"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().X + 1, tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().Y);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().X + 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().Y);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Öğrenci No"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().X + 1, tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().Y);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().X + 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().Y);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Sınıf-Şube"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().X + 1, tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().Y);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().X + 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().Y);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Kitapçık Türü"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().X + 1, tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().Y);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().X + 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().Y);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Okul Kodu"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().X + 1, tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().Y);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().X + 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().Y);
                        }
                    }
                }

                if (dersAdlariKalibre.Contains(comboBoxAlanlar.SelectedItem.ToString()))
                {
                    int index = dersAdlariKalibre.IndexOf(comboBoxAlanlar.SelectedItem.ToString());

                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            List<Point> dersBasNoktalari = new List<Point>();
                            dersBasNoktalari = tumFormlarinDeğerleriKalibre[i].getDersBaslangicNoktalari();
                            dersBasNoktalari[index] = new Point(dersBasNoktalari[index].X+1,dersBasNoktalari[index].Y);
                            tumFormlarinDeğerleriKalibre[i].addDersBaslangicNoktalari(dersBasNoktalari);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        List<Point> dersBasNoktalari = new List<Point>();
                        dersBasNoktalari = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                        dersBasNoktalari[index] = new Point(dersBasNoktalari[index].X + 1, dersBasNoktalari[index].Y);
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].addDersBaslangicNoktalari(dersBasNoktalari);
                    }

                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Ad-Soyad"))
                {
                    if (radioButtonTüm.Checked)
                    {                       
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().X+1,tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().Y);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().X + 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().Y);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Öğrenci No"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().X + 1, tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().Y);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().X + 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().Y);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Sınıf-Şube"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().X + 1, tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().Y);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().X + 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().Y);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Kitapçık Türü"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().X + 1, tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().Y);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().X + 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().Y);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Okul Kodu"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().X + 1, tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().Y);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().X + 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().Y);
                    }
                }              

                pictureBox1.Invalidate();               
                pictureBox1.Image = kalibreEdilecekForm;
            }
            else
            {
                MessageBox.Show("Lütfen ızgarasını taşıyacağınız alan adını seçiniz");
            }
        }

        private void buttonIzgaraLeft_Click(object sender, EventArgs e)
        {
            if (comboBoxAlanlar.SelectedIndex > -1)
            {
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Tüm Alanlar"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            List<Point> dersBasNoktalari = new List<Point>();
                            dersBasNoktalari = tumFormlarinDeğerleriKalibre[i].getDersBaslangicNoktalari();
                            for (int j = 0; j < eklenenDersSayısıKalibre; j++)
                            {
                                dersBasNoktalari[j] = new Point(dersBasNoktalari[j].X - 1, dersBasNoktalari[j].Y);
                            }
                            tumFormlarinDeğerleriKalibre[i].addDersBaslangicNoktalari(dersBasNoktalari);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        List<Point> dersBasNoktalari = new List<Point>();
                        dersBasNoktalari = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                        for (int j = 0; j < eklenenDersSayısıKalibre; j++)
                        {
                            dersBasNoktalari[j] = new Point(dersBasNoktalari[j].X - 1, dersBasNoktalari[j].Y);
                        }
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].addDersBaslangicNoktalari(dersBasNoktalari);
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Ad-Soyad"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().X - 1, tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().Y);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().X - 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().Y);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Öğrenci No"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().X - 1, tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().Y);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().X - 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().Y);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Sınıf-Şube"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().X - 1, tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().Y);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().X - 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().Y);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Kitapçık Türü"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().X - 1, tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().Y);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().X - 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().Y);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Okul Kodu"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().X - 1, tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().Y);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().X - 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().Y);
                        }
                    }
                }
                    if (dersAdlariKalibre.Contains(comboBoxAlanlar.SelectedItem.ToString()))
                {
                    int index = dersAdlariKalibre.IndexOf(comboBoxAlanlar.SelectedItem.ToString());

                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            List<Point> dersBasNoktalari = new List<Point>();
                            dersBasNoktalari = tumFormlarinDeğerleriKalibre[i].getDersBaslangicNoktalari();
                            dersBasNoktalari[index] = new Point(dersBasNoktalari[index].X - 1, dersBasNoktalari[index].Y);
                            tumFormlarinDeğerleriKalibre[i].addDersBaslangicNoktalari(dersBasNoktalari);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        List<Point> dersBasNoktalari = new List<Point>();
                        dersBasNoktalari = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                        dersBasNoktalari[index] = new Point(dersBasNoktalari[index].X - 1, dersBasNoktalari[index].Y);
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].addDersBaslangicNoktalari(dersBasNoktalari);
                    }

                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Ad-Soyad"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().X - 1, tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().Y);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().X - 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().Y);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Öğrenci No"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().X - 1, tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().Y);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().X - 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().Y);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Sınıf-Şube"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().X - 1, tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().Y);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().X - 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().Y);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Kitapçık Türü"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().X - 1, tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().Y);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().X - 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().Y);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Okul Kodu"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().X - 1, tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().Y);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().X - 1, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().Y);
                    }
                }
              
                pictureBox1.Invalidate();              
                pictureBox1.Image = kalibreEdilecekForm;
            }
            else
            {
                MessageBox.Show("Lütfen ızgarasını taşıyacağınız alan adını seçiniz");
            }
        }

        private void buttonIzgaraUp_Click(object sender, EventArgs e)
        {
            if (comboBoxAlanlar.SelectedIndex > -1)
            {
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Tüm Alanlar"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            List<Point> dersBasNoktalari = new List<Point>();
                            dersBasNoktalari = tumFormlarinDeğerleriKalibre[i].getDersBaslangicNoktalari();
                            for (int j = 0; j < eklenenDersSayısıKalibre; j++)
                            {
                                dersBasNoktalari[j] = new Point(dersBasNoktalari[j].X , dersBasNoktalari[j].Y - 1);
                            }
                            tumFormlarinDeğerleriKalibre[i].addDersBaslangicNoktalari(dersBasNoktalari);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        List<Point> dersBasNoktalari = new List<Point>();
                        dersBasNoktalari = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                        for (int j = 0; j < eklenenDersSayısıKalibre; j++)
                        {
                            dersBasNoktalari[j] = new Point(dersBasNoktalari[j].X , dersBasNoktalari[j].Y - 1);
                        }
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].addDersBaslangicNoktalari(dersBasNoktalari);
                    }

                    if (okunacakStandartAlanlarKalibre.Contains("Ad-Soyad"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().X, tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().Y - 1);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().X, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().Y - 1);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Öğrenci No"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().X, tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().Y - 1);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().X, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().Y - 1);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Sınıf-Şube"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().X, tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().Y - 1);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().X, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().Y - 1);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Kitapçık Türü"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().X, tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().Y - 1);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().X, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().Y - 1);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Okul Kodu"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().X, tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().Y - 1);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().X, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().Y - 1);
                        }
                    }
                }
                    if (dersAdlariKalibre.Contains(comboBoxAlanlar.SelectedItem.ToString()))
                {
                    int index = dersAdlariKalibre.IndexOf(comboBoxAlanlar.SelectedItem.ToString());

                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            List<Point> dersBasNoktalari = new List<Point>();
                            dersBasNoktalari = tumFormlarinDeğerleriKalibre[i].getDersBaslangicNoktalari();
                            dersBasNoktalari[index] = new Point(dersBasNoktalari[index].X , dersBasNoktalari[index].Y - 1);
                            tumFormlarinDeğerleriKalibre[i].addDersBaslangicNoktalari(dersBasNoktalari);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        List<Point> dersBasNoktalari = new List<Point>();
                        dersBasNoktalari = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                        dersBasNoktalari[index] = new Point(dersBasNoktalari[index].X , dersBasNoktalari[index].Y - 1);
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].addDersBaslangicNoktalari(dersBasNoktalari);
                    }

                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Ad-Soyad"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().X , tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().Y - 1);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().X , tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().Y - 1);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Öğrenci No"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().X, tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().Y - 1);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().X , tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().Y - 1);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Sınıf-Şube"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().X , tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().Y - 1);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().X , tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().Y - 1);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Kitapçık Türü"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().X, tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().Y - 1);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().X, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().Y - 1);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Okul Kodu"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().X , tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().Y - 1);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().X , tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().Y - 1);
                    }
                }             

                pictureBox1.Invalidate();              
                pictureBox1.Image = kalibreEdilecekForm;
            }
            else
            {
                MessageBox.Show("Lütfen ızgarasını taşıyacağınız alan adını seçiniz");
            }
        }

        private void buttonIzgaraDown_Click(object sender, EventArgs e)
        {
            if (comboBoxAlanlar.SelectedIndex > -1)
            {
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Tüm Alanlar"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            List<Point> dersBasNoktalari = new List<Point>();
                            dersBasNoktalari = tumFormlarinDeğerleriKalibre[i].getDersBaslangicNoktalari();
                            for (int j = 0; j < eklenenDersSayısıKalibre; j++)
                            {
                                dersBasNoktalari[j] = new Point(dersBasNoktalari[j].X, dersBasNoktalari[j].Y + 1);
                            }
                            tumFormlarinDeğerleriKalibre[i].addDersBaslangicNoktalari(dersBasNoktalari);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        List<Point> dersBasNoktalari = new List<Point>();
                        dersBasNoktalari = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                        for (int j = 0; j < eklenenDersSayısıKalibre; j++)
                        {
                            dersBasNoktalari[j] = new Point(dersBasNoktalari[j].X, dersBasNoktalari[j].Y + 1);
                        }
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].addDersBaslangicNoktalari(dersBasNoktalari);
                    }

                    if (okunacakStandartAlanlarKalibre.Contains("Ad-Soyad"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().X, tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().Y + 1);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().X, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().Y + 1);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Öğrenci No"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().X, tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().Y + 1);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().X, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().Y + 1);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Sınıf-Şube"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().X, tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().Y + 1);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().X, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().Y + 1);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Kitapçık Türü"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().X, tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().Y + 1);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().X, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().Y + 1);
                        }
                    }
                    if (okunacakStandartAlanlarKalibre.Contains("Okul Kodu"))
                    {
                        if (radioButtonTüm.Checked)
                        {
                            for (int i = 0; i < pathsSecilen.Count; i++)
                            {
                                tumFormlarinDeğerleriKalibre[i].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().X, tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().Y + 1);
                            }
                        }
                        else if (radioButtonGosterilen.Checked)
                        {
                            tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().X, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().Y + 1);
                        }
                    }
                }

                if (dersAdlariKalibre.Contains(comboBoxAlanlar.SelectedItem.ToString()))
                {
                    int index = dersAdlariKalibre.IndexOf(comboBoxAlanlar.SelectedItem.ToString());

                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            List<Point> dersBasNoktalari = new List<Point>();
                            dersBasNoktalari = tumFormlarinDeğerleriKalibre[i].getDersBaslangicNoktalari();
                            dersBasNoktalari[index] = new Point(dersBasNoktalari[index].X , dersBasNoktalari[index].Y + 1);
                            tumFormlarinDeğerleriKalibre[i].addDersBaslangicNoktalari(dersBasNoktalari);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        List<Point> dersBasNoktalari = new List<Point>();
                        dersBasNoktalari = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                        dersBasNoktalari[index] = new Point(dersBasNoktalari[index].X, dersBasNoktalari[index].Y + 1);
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].addDersBaslangicNoktalari(dersBasNoktalari);
                    }

                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Ad-Soyad"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().X , tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().Y + 1);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().X , tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().Y + 1);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Öğrenci No"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().X , tumFormlarinDeğerleriKalibre[i].getOgrenciNoBaslangic().Y + 1);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().X , tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().Y + 1);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Sınıf-Şube"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().X , tumFormlarinDeğerleriKalibre[i].getSinifSubeBaslangic().Y + 1);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setSinifSubeBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().X , tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().Y + 1);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Kitapçık Türü"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().X, tumFormlarinDeğerleriKalibre[i].getKitapcikTuruBaslangic().Y + 1);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().X , tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().Y + 1);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Okul Kodu"))
                {
                    if (radioButtonTüm.Checked)
                    {
                        for (int i = 0; i < pathsSecilen.Count; i++)
                        {
                            tumFormlarinDeğerleriKalibre[i].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().X , tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().Y + 1);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().X , tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().Y + 1);
                    }
                }             

                pictureBox1.Invalidate();               
                pictureBox1.Image = kalibreEdilecekForm;
            }
            else
            {
                MessageBox.Show("Lütfen ızgarasını taşıyacağınız alan adını seçiniz");
            }
        }

        private void buttonKaydet_Click(object sender, EventArgs e)
        {                    
            baslangicnoktalariKalibre = new List<Point>();
            baslangicnoktalariKalibre.Add(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic());
            baslangicnoktalariKalibre.Add(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic());
            baslangicnoktalariKalibre.Add(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic());
            baslangicnoktalariKalibre.Add(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic());
            baslangicnoktalariKalibre.Add(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic());
            baslangicnoktalariKalibre.Add(new Point(0,0));

            List<Point> noktalar = new List<Point>();
            noktalar = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
            foreach (Point p in noktalar)
            {
                baslangicnoktalariKalibre.Add(p);
            }

            Veritabani vt = new Veritabani();
            vt.baglan();
            vt.guncelleTasarlananFormu(formadiKalibre, baslangicnoktalariKalibre, this.tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getGrilikEsigi(), this.tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getParlaklikEsigi(), this.tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSikOkumaHassasiyeti(), this.tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht(), this.tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight());

            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            form1.kalibrasyonYapildiMi = true;
            form1.tumFormlarinDeğerleri = new List<FormDegerleri>();
            foreach (FormDegerleri formDegerleri in tumFormlarinDeğerleriKalibre)
            {
                form1.tumFormlarinDeğerleri.Add(formDegerleri);
            }
            MessageBox.Show("Kalibrasyon kaydedildi");
        }

        private void buttonPictureNext_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            if (gosterilenFormSirasi < pathsSecilen.Count() - 1)
            {
                gosterilenFormSirasi += 1;
                formuGoruntule();
            }
            this.Cursor = Cursors.Default;
        }

        private void buttonPictureBack_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            if (gosterilenFormSirasi > 0)
            {
                gosterilenFormSirasi -= 1;
                formuGoruntule();
            }
            this.Cursor = Cursors.Default;
        }

        private void formuGoruntulemeyeHazirla(int gosterimSirasi)
        {
            Bitmap secilenform = new Bitmap(pathsSecilen[gosterilenFormSirasi]);
            kalibreEdilecekForm = new Bitmap(secilenform, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht(), tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight());
            anaForm = new Bitmap(kalibreEdilecekForm);
            secilenform.Dispose();
            kenarBul();
        }

        private void formuGoruntule()
        {
            Bitmap secilenform = new Bitmap(pathsSecilen[gosterilenFormSirasi]);
            kalibreEdilecekForm = new Bitmap(secilenform, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht(), tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight());
            anaForm = new Bitmap(kalibreEdilecekForm);
            secilenform.Dispose();
            kenarBul();
            if (koseKareBaslangicNoktalari.Count == 6)
            {
                egrilikGider();
                anaForm = new Bitmap(kalibreEdilecekForm);
                pictureBox1.Invalidate();               
                labelParlaklikEsigi.Text = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getParlaklikEsigi().ToString();
                labelEsikDegeri.Text = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getGrilikEsigi().ToString();
                labelSikOkumaHassasiyeti.Text = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSikOkumaHassasiyeti().ToString();
                textBoxPageNumber.Text = (gosterilenFormSirasi + 1).ToString();
            }
            pictureBox1.Image = kalibreEdilecekForm;
        }

        private void formBilgileriGetir(String formadi, List<string> okunacakStdAlanlar)
        {
            baslangicnoktalariKalibre = new List<Point>();
            List<int> secilenStandartAlanlarIndexler = new List<int>();
            soruSayilariKalibre = new List<int>();
            dersAdlariKalibre = new List<string>();

            Veritabani vt = new Veritabani();
            vt.baglan();
            dersAdlariKalibre = vt.dersAdlariniGetir(formadi);
            vt.baglan();
            baslangicnoktalariKalibre = vt.formBaslangicNoktalariniGetir(formadi);
            vt.baglan();
            soruSayilariKalibre = vt.soruSayılariniGetir(formadi);
            vt.baglan();
            parlaklikesigi = vt.parlaklikesigiGetir(formadi);
            vt.baglan();
            grilikesigi = vt.grilikesigiGetir(formadi);
            vt.baglan();
            soruSayilariKalibre = vt.soruSayılariniGetir(formadi);
            vt.baglan();
            sikOkumaHassasiyeti = vt.sikokumahassasiyetiGetir(formadi);
            vt.baglan();
            Point formebatlari = vt.formEbatlariniGetir(formadi);
            widhtForm = formebatlari.X;
            heightForm = formebatlari.Y;

            vt.baglan();
            okulTuruKalibre = vt.okulTurunuGetir(formadi);
            vt.baglan();           

            for (int i = 0; i < 6; i++)
            {
                if (baslangicnoktalariKalibre[i].X != 0 && baslangicnoktalariKalibre[i].Y != 0)
                {
                    secilenStandartAlanlarIndexler.Add(i);
                }
            }

            foreach (int i in secilenStandartAlanlarIndexler)
            {
                if (i == 0)
                {
                    okunacakStandartAlanlarKalibre.Add("Ad-Soyad");
                }
                if (i == 1)
                {
                    okunacakStandartAlanlarKalibre.Add("Öğrenci No");
                }
                if (i == 2)
                {
                    okunacakStandartAlanlarKalibre.Add("Sınıf-Şube");
                }
                if (i == 3)
                {
                    okunacakStandartAlanlarKalibre.Add("Kitapçık Türü");
                }
                if (i == 4)
                {
                    okunacakStandartAlanlarKalibre.Add("Okul Kodu");
                }
            }

            secilenStandartAlanlarIndexler.Clear();
        }

        private void radioButtonTüm_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTüm.Checked && radioButtonGosterilen.Checked)
            {
                radioButtonGosterilen.Checked = false;
            }
        }

        private void radioButtonGosterilen_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonGosterilen.Checked && radioButtonTüm.Checked)
            {
                radioButtonTüm.Checked = false;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            tespitEdilenKenarlariGoster(e);
            adsoyadBul(kalibreEdilecekForm,e);
            ogrenciNoBul(kalibreEdilecekForm, e);
            sinifSubeBul(kalibreEdilecekForm, e);
            kitapcikTuruBul(kalibreEdilecekForm, e);
            okulKoduBul(kalibreEdilecekForm, e);
            ders1Bul(kalibreEdilecekForm, e);
            ders2Bul(kalibreEdilecekForm, e);
            ders3Bul(kalibreEdilecekForm, e);
            ders4Bul(kalibreEdilecekForm, e);
            ders5Bul(kalibreEdilecekForm, e);
            ders6Bul(kalibreEdilecekForm, e);
            ders7Bul(kalibreEdilecekForm, e);
            ders8Bul(kalibreEdilecekForm, e);
            ders9Bul(kalibreEdilecekForm, e);
            ders10Bul(kalibreEdilecekForm, e);

        }                  

        private void textBoxPageNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == 13)
            {
                if (Convert.ToInt32(textBoxPageNumber.Text) > pathsSecilen.Count)
                {
                    MessageBox.Show("Toplam form sayısından büyük bir değer girdiniz");
                    textBoxPageNumber.SelectAll();
                }
                else {
                    this.Cursor = Cursors.AppStarting;
                    gosterilenFormSirasi = Convert.ToInt32(textBoxPageNumber.Text) - 1;
                    formuGoruntule();
                    this.Cursor = Cursors.Default;
                }                   
            }
        }      

        private void textBoxPageNumber_Click(object sender, EventArgs e)
        {
            textBoxPageNumber.SelectAll();
        }
    }
}
