using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace KorOptik_v1
{
    public partial class Kalibrasyon : Form
    {
        public Kalibrasyon()
        {
            InitializeComponent();
        }

        int izgaraBoşlukGenislik = 9;
        int izgaraKareGenislik = 7;
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
        List<Point> kosekoordinatlari1 = new List<Point>();
        List<Point> kosekoordinatlari2 = new List<Point>();
        List<Point> kosekoordinatlari3 = new List<Point>();
        List<int> secilenStandartAlanlarIndexler = new List<int>();
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

                if (koseKareBaslangicNoktalari.Count == 3)
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
                    MessageBox.Show("Köşelerdeki kareler bulunurken hata oluştu. Lütfen köşe kare hassasiyetini değiştirin!");
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
                            //GriResim[x, y] = (double)(.299 * p[0] + .587 * p[1] + .114 * p[2]);
                            GriResim[x, y] = (double)((p[0] + p[1] + p[2]) / 3);
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

        private void kenarBul()
        {
            parlaklikesigi = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getParlaklikEsigi();
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
                pictureBox1.Image = kalibreEdilecekForm;
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

        private void egrilikGider()
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

                    kenarBul();

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

                    kenarBul();
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

                    kenarBul();
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

                    kenarBul();
                    if (koseKareBaslangicNoktalari.Count > 1)
                    {
                        solUstY = koseKareBaslangicNoktalari[0].Y;
                        sagUstY = koseKareBaslangicNoktalari[1].Y;
                    }
                    else { break; }
                }
            }
        }

        private void tespitEdilenKareleriGoster(PaintEventArgs e)
        {
            if (koseKareBaslangicNoktalari.Count() == 3)
            {
                parlaklikesigi = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getParlaklikEsigi();
                if (kosekoordinatlari1.Count() > parlaklikesigi)
                {
                    /*foreach (Point p in kosekoordinatlari1)
                    {
                        e.Graphics.DrawEllipse(new Pen(Color.Red, 1), p.X, p.Y, 1, 1);
                    }*/
                    e.Graphics.DrawRectangle(new Pen(Color.Red, 1), koseKareBaslangicNoktalari[0].X - 7, koseKareBaslangicNoktalari[0].Y - 7, 13, 13);
                }

                if (kosekoordinatlari2.Count() > parlaklikesigi)
                {
                    /* foreach (Point p in kosekoordinatlari2)
                     {
                         e.Graphics.DrawEllipse(new Pen(Color.Red, 1), p.X, p.Y, 1, 1);
                     }*/
                    e.Graphics.DrawRectangle(new Pen(Color.Red, 1), koseKareBaslangicNoktalari[1].X - 7, koseKareBaslangicNoktalari[1].Y - 7, 13, 13);
                }

                if (kosekoordinatlari3.Count() > parlaklikesigi)
                {
                    /* foreach (Point p in kosekoordinatlari3)
                     {
                         e.Graphics.DrawEllipse(new Pen(Color.Red, 1), p.X, p.Y, 1, 1);
                     }*/
                    e.Graphics.DrawRectangle(new Pen(Color.Red, 1), koseKareBaslangicNoktalari[2].X - 7, koseKareBaslangicNoktalari[2].Y - 7, 13, 13);
                }
            }
        }

        private void adsoyadBul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;
                Point p = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic();
                x1 = p.X + koseKareBaslangicNoktalari[0].X;
                x2 = p.X + koseKareBaslangicNoktalari[0].X + izgaraBoşlukGenislik * 20;
                y1 = p.Y + koseKareBaslangicNoktalari[0].Y;
                y2 = p.Y + koseKareBaslangicNoktalari[0].Y + izgaraBoşlukGenislik * 29;

                g.Graphics.DrawRectangle(new Pen(Color.Blue, 1), x1 - 4, y1 + 1 - 3 * izgaraBoşlukGenislik, 20 * izgaraBoşlukGenislik, 32 * izgaraBoşlukGenislik);
                for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
                {
                    for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i - 3, j + 1, izgaraKareGenislik, izgaraKareGenislik);
                    }
                }
            }
        }

        private void ogrenciNoBul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                Point p = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic();
                x1 = p.X + koseKareBaslangicNoktalari[0].X;
                x2 = p.X + koseKareBaslangicNoktalari[0].X + 4 * izgaraBoşlukGenislik;
                y1 = p.Y + koseKareBaslangicNoktalari[0].Y;
                y2 = p.Y + koseKareBaslangicNoktalari[0].Y + 10 * izgaraBoşlukGenislik;

                g.Graphics.DrawRectangle(new Pen(Color.Blue, 1), x1 - 4, y1 + 1 - 2 * izgaraBoşlukGenislik, 4 * izgaraBoşlukGenislik, 12 * izgaraBoşlukGenislik);
                for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
                {
                    for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i - 3, j + 1, izgaraKareGenislik, izgaraKareGenislik);
                    }
                }
            }
        }

        private void sinifSubeBul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                Point p = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic();
                x1 = p.X + koseKareBaslangicNoktalari[0].X;
                x2 = p.X + koseKareBaslangicNoktalari[0].X + 2 * izgaraBoşlukGenislik;
                y1 = p.Y + koseKareBaslangicNoktalari[0].Y;
                y2 = p.Y + koseKareBaslangicNoktalari[0].Y + 29 * izgaraBoşlukGenislik;

                g.Graphics.DrawRectangle(new Pen(Color.Blue, 1), x1 - 4, y1 + 1 - 2 * izgaraBoşlukGenislik, 2 * izgaraBoşlukGenislik, 31 * izgaraBoşlukGenislik);
                for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
                {
                    for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i - 3, j + 1, izgaraKareGenislik, izgaraKareGenislik);
                    }
                }
            }
        }

        private void kitapcikTuruBul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                Point p = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic();
                x1 = p.X + koseKareBaslangicNoktalari[0].X;
                x2 = p.X + koseKareBaslangicNoktalari[0].X + 1 * izgaraBoşlukGenislik;
                y1 = p.Y + koseKareBaslangicNoktalari[0].Y;
                y2 = p.Y + koseKareBaslangicNoktalari[0].Y + 4 * izgaraBoşlukGenislik;

                g.Graphics.DrawRectangle(new Pen(Color.Blue, 1), x1 - 1 - 2 * izgaraBoşlukGenislik, y1 + 1 - 7 * izgaraBoşlukGenislik, 44, 108);
                for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
                {
                    for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i - 3, j + 1, izgaraKareGenislik, izgaraKareGenislik);
                    }
                }
            }
        }

        private void okulKoduBul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int x1 = 0;
                int x2 = 0;
                int y1 = 0;
                int y2 = 0;

                Point p = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic();
                x1 = p.X + koseKareBaslangicNoktalari[0].X;
                x2 = p.X + koseKareBaslangicNoktalari[0].X + 6 * izgaraBoşlukGenislik;
                y1 = p.Y + koseKareBaslangicNoktalari[0].Y;
                y2 = p.Y + koseKareBaslangicNoktalari[0].Y + 10 * izgaraBoşlukGenislik;

                g.Graphics.DrawRectangle(new Pen(Color.Blue, 1), x1 - 4, y1 + 1 - 2 * izgaraBoşlukGenislik, 6 * izgaraBoşlukGenislik, 12 * izgaraBoşlukGenislik);
                for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
                {
                    for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i - 3, j + 1, izgaraKareGenislik, izgaraKareGenislik);
                    }
                }
            }
        }

        private void ders1Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int yukseklik = izgaraBoşlukGenislik * soruSayilariKalibre[0];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = izgaraBoşlukGenislik * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = izgaraBoşlukGenislik * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = izgaraBoşlukGenislik * 5;
                }

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

                g.Graphics.DrawRectangle(new Pen(Color.Blue, 1), x1 - 3, y1 - izgaraBoşlukGenislik, genislik + izgaraBoşlukGenislik, yukseklik + izgaraBoşlukGenislik);
                for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
                {
                    for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 6, j, izgaraKareGenislik, izgaraKareGenislik);
                    }
                }

            }
        }

        private void ders2Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int yukseklik = izgaraBoşlukGenislik * soruSayilariKalibre[1];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = izgaraBoşlukGenislik * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = izgaraBoşlukGenislik * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = izgaraBoşlukGenislik * 5;
                }

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

                g.Graphics.DrawRectangle(new Pen(Color.Blue, 1), x1 - 3, y1 - izgaraBoşlukGenislik, genislik + izgaraBoşlukGenislik, yukseklik + izgaraBoşlukGenislik);
                for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
                {
                    for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 6, j, izgaraKareGenislik, izgaraKareGenislik);
                    }
                }
            }
        }

        private void ders3Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int yukseklik = izgaraBoşlukGenislik * soruSayilariKalibre[2];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = izgaraBoşlukGenislik * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = izgaraBoşlukGenislik * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = izgaraBoşlukGenislik * 5;
                }

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

                g.Graphics.DrawRectangle(new Pen(Color.Blue, 1), x1 - 3, y1 - izgaraBoşlukGenislik, genislik + izgaraBoşlukGenislik, yukseklik + izgaraBoşlukGenislik);
                for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
                {
                    for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 6, j, izgaraKareGenislik, izgaraKareGenislik);
                    }
                }
            }
        }

        private void ders4Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int yukseklik = izgaraBoşlukGenislik * soruSayilariKalibre[3];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = izgaraBoşlukGenislik * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = izgaraBoşlukGenislik * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = izgaraBoşlukGenislik * 5;
                }

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

                g.Graphics.DrawRectangle(new Pen(Color.Blue, 1), x1 - 3, y1 - izgaraBoşlukGenislik, genislik + izgaraBoşlukGenislik, yukseklik + izgaraBoşlukGenislik);
                for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
                {
                    for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 6, j, izgaraKareGenislik, izgaraKareGenislik);
                    }
                }
            }
        }

        private void ders5Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int yukseklik = izgaraBoşlukGenislik * soruSayilariKalibre[4];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = izgaraBoşlukGenislik * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = izgaraBoşlukGenislik * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = izgaraBoşlukGenislik * 5;
                }

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

                g.Graphics.DrawRectangle(new Pen(Color.Blue, 1), x1 - 3, y1 - izgaraBoşlukGenislik, genislik + izgaraBoşlukGenislik, yukseklik + izgaraBoşlukGenislik);
                for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
                {
                    for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 6, j, izgaraKareGenislik, izgaraKareGenislik);
                    }
                }
            }
        }

        private void ders6Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int yukseklik = izgaraBoşlukGenislik * soruSayilariKalibre[5];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = izgaraBoşlukGenislik * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = izgaraBoşlukGenislik * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = izgaraBoşlukGenislik * 5;
                }

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

                g.Graphics.DrawRectangle(new Pen(Color.Blue, 1), x1 - 3, y1 - izgaraBoşlukGenislik, genislik + izgaraBoşlukGenislik, yukseklik + izgaraBoşlukGenislik);
                for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
                {
                    for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 6, j, izgaraKareGenislik, izgaraKareGenislik);
                    }
                }
            }
        }

        private void ders7Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int yukseklik = izgaraBoşlukGenislik * soruSayilariKalibre[6];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = izgaraBoşlukGenislik * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = izgaraBoşlukGenislik * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = izgaraBoşlukGenislik * 5;
                }

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


                g.Graphics.DrawRectangle(new Pen(Color.Blue, 1), x1 - 3, y1 - izgaraBoşlukGenislik, genislik + izgaraBoşlukGenislik, yukseklik + izgaraBoşlukGenislik);
                for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
                {
                    for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 6, j, izgaraKareGenislik, izgaraKareGenislik);
                    }
                }
            }
        }

        private void ders8Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int yukseklik = izgaraBoşlukGenislik * soruSayilariKalibre[7];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = izgaraBoşlukGenislik * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = izgaraBoşlukGenislik * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = izgaraBoşlukGenislik * 5;
                }

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

                g.Graphics.DrawRectangle(new Pen(Color.Blue, 1), x1 - 3, y1 - izgaraBoşlukGenislik, genislik + izgaraBoşlukGenislik, yukseklik + izgaraBoşlukGenislik);
                for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
                {
                    for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 6, j, izgaraKareGenislik, izgaraKareGenislik);
                    }
                }
            }
        }

        private void ders9Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int yukseklik = izgaraBoşlukGenislik * soruSayilariKalibre[8];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = izgaraBoşlukGenislik * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = izgaraBoşlukGenislik * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = izgaraBoşlukGenislik * 5;
                }

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

                g.Graphics.DrawRectangle(new Pen(Color.Blue, 1), x1 - 3, y1 - izgaraBoşlukGenislik, genislik + izgaraBoşlukGenislik, yukseklik + izgaraBoşlukGenislik);
                for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
                {
                    for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 6, j, izgaraKareGenislik, izgaraKareGenislik);
                    }
                }
            }
        }

        private void ders10Bul(Bitmap b, PaintEventArgs g)
        {
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int yukseklik = izgaraBoşlukGenislik * soruSayilariKalibre[9];

                int genislik = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    genislik = izgaraBoşlukGenislik * 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    genislik = izgaraBoşlukGenislik * 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    genislik = izgaraBoşlukGenislik * 5;
                }

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

                g.Graphics.DrawRectangle(new Pen(Color.Blue, 1), x1 - 3, y1 - izgaraBoşlukGenislik, genislik + izgaraBoşlukGenislik, yukseklik + izgaraBoşlukGenislik);
                for (int i = x1; i < x2; i += izgaraBoşlukGenislik)
                {
                    for (int j = y1; j < y2; j += izgaraBoşlukGenislik)
                    {
                        g.Graphics.DrawRectangle(new Pen(Color.Red, 1), i + 6, j, izgaraKareGenislik, izgaraKareGenislik);
                    }
                }
            }
        }

        private void buttonSikOkumaHassasiyetiAzalt_Click(object sender, EventArgs e)
        {
            labelSikOkumaHassasiyeti.Text = (Convert.ToInt32(labelSikOkumaHassasiyeti.Text) - 1).ToString();
            if (Convert.ToInt32(labelSikOkumaHassasiyeti.Text) <= 0)
            {
                MessageBox.Show("Değer sıfırdan küçük olamaz!");
                labelSikOkumaHassasiyeti.Text = "0";
            }
        }

        private void buttonSikOkumaHassasiyetiArttir_Click(object sender, EventArgs e)
        {
            labelSikOkumaHassasiyeti.Text = (Convert.ToInt32(labelSikOkumaHassasiyeti.Text) + 1).ToString();
            if (Convert.ToInt32(labelSikOkumaHassasiyeti.Text) >= 50)
            {
                MessageBox.Show("Değer 49'dan büyük olamaz!");
                labelSikOkumaHassasiyeti.Text = "49";
            }
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
            if (Convert.ToInt32(labelEsikDegeri.Text) <= 0)
            {
                MessageBox.Show("Değer sıfırdan küçük olamaz!");
                labelEsikDegeri.Text = "0";
            }
        }

        private void buttonGrilikEsigiArttir_Click(object sender, EventArgs e)
        {
            labelEsikDegeri.Text = (Convert.ToInt32(labelEsikDegeri.Text) + 1).ToString();
            if (Convert.ToInt32(labelEsikDegeri.Text) >= 255)
            {
                MessageBox.Show("Değer 255'ten büyük olamaz!");
                labelEsikDegeri.Text = "255";
            }
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
            if (Convert.ToInt32(labelParlaklikEsigi.Text) <= 0)
            {
                MessageBox.Show("Değer sıfırdan küçük olamaz!");
                labelParlaklikEsigi.Text = "0";
            }
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
            if (koseKareBaslangicNoktalari.Count == 3)
            {
                egrilikGider();
                anaForm = new Bitmap(kalibreEdilecekForm);
                pictureBox1.Invalidate();
                pictureBox1.Image = kalibreEdilecekForm;
            }
            else
            {
                pictureBox1.Invalidate();
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
                            dersBasNoktalari[index] = new Point(dersBasNoktalari[index].X + 1, dersBasNoktalari[index].Y);
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
                            tumFormlarinDeğerleriKalibre[i].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().X + 1, tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().Y);
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
                                dersBasNoktalari[j] = new Point(dersBasNoktalari[j].X, dersBasNoktalari[j].Y - 1);
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
                            dersBasNoktalari[j] = new Point(dersBasNoktalari[j].X, dersBasNoktalari[j].Y - 1);
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
                            dersBasNoktalari[index] = new Point(dersBasNoktalari[index].X, dersBasNoktalari[index].Y - 1);
                            tumFormlarinDeğerleriKalibre[i].addDersBaslangicNoktalari(dersBasNoktalari);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        List<Point> dersBasNoktalari = new List<Point>();
                        dersBasNoktalari = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari();
                        dersBasNoktalari[index] = new Point(dersBasNoktalari[index].X, dersBasNoktalari[index].Y - 1);
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].addDersBaslangicNoktalari(dersBasNoktalari);
                    }

                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Ad-Soyad"))
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
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOgrenciNoBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().X, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().Y - 1);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Sınıf-Şube"))
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
                            tumFormlarinDeğerleriKalibre[i].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().X, tumFormlarinDeğerleriKalibre[i].getOkulKoduBaslangic().Y - 1);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setOkulKoduBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().X, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().Y - 1);
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
                            dersBasNoktalari[index] = new Point(dersBasNoktalari[index].X, dersBasNoktalari[index].Y + 1);
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
                            tumFormlarinDeğerleriKalibre[i].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().X, tumFormlarinDeğerleriKalibre[i].getAdSoyadBaslangic().Y + 1);
                        }
                    }
                    else if (radioButtonGosterilen.Checked)
                    {
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setAdSoyadBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().X, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().Y + 1);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Öğrenci No"))
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
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Sınıf-Şube"))
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
                        tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].setKitapcikTuruBaslangic(tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().X, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().Y + 1);
                    }
                }
                if (comboBoxAlanlar.SelectedItem.ToString().Equals("Okul Kodu"))
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
            baslangicnoktalariKalibre.Add(new Point(0, 0));

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
            if (koseKareBaslangicNoktalari.Count == 3)
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
            secilenStandartAlanlarIndexler = new List<int>();
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
            tespitEdilenKareleriGoster(e);
            adsoyadBul(kalibreEdilecekForm, e);
            ogrenciNoBul(kalibreEdilecekForm, e);
            sinifSubeBul(kalibreEdilecekForm, e);
            kitapcikTuruBul(kalibreEdilecekForm, e);
            okulKoduBul(kalibreEdilecekForm, e);

            switch (eklenenDersSayısıKalibre)
            {
                case 0:
                    break;
                case 1:
                    ders1Bul(kalibreEdilecekForm, e);
                    break;
                case 2:
                    ders1Bul(kalibreEdilecekForm, e);
                    ders2Bul(kalibreEdilecekForm, e);
                    break;
                case 3:
                    ders1Bul(kalibreEdilecekForm, e);
                    ders2Bul(kalibreEdilecekForm, e);
                    ders3Bul(kalibreEdilecekForm, e);
                    break;
                case 4:
                    ders1Bul(kalibreEdilecekForm, e);
                    ders2Bul(kalibreEdilecekForm, e);
                    ders3Bul(kalibreEdilecekForm, e);
                    ders4Bul(kalibreEdilecekForm, e);
                    break;
                case 5:
                    ders1Bul(kalibreEdilecekForm, e);
                    ders2Bul(kalibreEdilecekForm, e);
                    ders3Bul(kalibreEdilecekForm, e);
                    ders4Bul(kalibreEdilecekForm, e);
                    ders5Bul(kalibreEdilecekForm, e);
                    break;
                case 6:
                    ders1Bul(kalibreEdilecekForm, e);
                    ders2Bul(kalibreEdilecekForm, e);
                    ders3Bul(kalibreEdilecekForm, e);
                    ders4Bul(kalibreEdilecekForm, e);
                    ders5Bul(kalibreEdilecekForm, e);
                    ders6Bul(kalibreEdilecekForm, e);
                    break;
                case 7:
                    ders1Bul(kalibreEdilecekForm, e);
                    ders2Bul(kalibreEdilecekForm, e);
                    ders3Bul(kalibreEdilecekForm, e);
                    ders4Bul(kalibreEdilecekForm, e);
                    ders5Bul(kalibreEdilecekForm, e);
                    ders6Bul(kalibreEdilecekForm, e);
                    ders7Bul(kalibreEdilecekForm, e);
                    break;
                case 8:
                    ders1Bul(kalibreEdilecekForm, e);
                    ders2Bul(kalibreEdilecekForm, e);
                    ders3Bul(kalibreEdilecekForm, e);
                    ders4Bul(kalibreEdilecekForm, e);
                    ders5Bul(kalibreEdilecekForm, e);
                    ders6Bul(kalibreEdilecekForm, e);
                    ders7Bul(kalibreEdilecekForm, e);
                    ders8Bul(kalibreEdilecekForm, e);
                    break;
                case 9:
                    ders1Bul(kalibreEdilecekForm, e);
                    ders2Bul(kalibreEdilecekForm, e);
                    ders3Bul(kalibreEdilecekForm, e);
                    ders4Bul(kalibreEdilecekForm, e);
                    ders5Bul(kalibreEdilecekForm, e);
                    ders6Bul(kalibreEdilecekForm, e);
                    ders7Bul(kalibreEdilecekForm, e);
                    ders8Bul(kalibreEdilecekForm, e);
                    ders9Bul(kalibreEdilecekForm, e);
                    break;
                case 10:
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
                    break;
            }
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
                else
                {
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            form1.kolonIsimleriniGoruntule(formadiKalibre, dataGridViewOkunanlar, okunacakStandartAlanlarKalibre, dersAdlariKalibre, secilenStandartAlanlarIndexler);

            Bitmap secilenForm = new Bitmap(pathsSecilen[gosterilenFormSirasi]);
            Bitmap okunacakForm;
            Bitmap anaForm;

            okunacakForm = new Bitmap(secilenForm, tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormWidht(), tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getFormHeight());
            anaForm = new Bitmap(okunacakForm);
            int sikokumahassasiyeti = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSikOkumaHassasiyeti();
            grilikesigi = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getGrilikEsigi();
            parlaklikesigi = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getParlaklikEsigi();

            kenarBul();
            egrilikGider();

            if (koseKareBaslangicNoktalari.Count == 3)
            {
                int basX = 0;
                int basY = 0;
                ///ADSOYAD OKUMA/////

                basX = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().X + koseKareBaslangicNoktalari[0].X;
                basY = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getAdSoyadBaslangic().Y + koseKareBaslangicNoktalari[0].Y;

                binaryyap(okunacakForm, basX - 3, basY + 1, 20 * izgaraBoşlukGenislik, 29 * izgaraBoşlukGenislik);
                String ogrAdi = form1.adsoyadOku(okunacakForm, basX - 3, basY + 1, sikokumahassasiyeti);

                ////öĞRENCİ NO OKUMA/////

                basX = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().X + koseKareBaslangicNoktalari[0].X;
                basY = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOgrenciNoBaslangic().Y + koseKareBaslangicNoktalari[0].Y;

                binaryyap(okunacakForm, basX - 3, basY + 1, 4 * izgaraBoşlukGenislik, 10 * izgaraBoşlukGenislik);
                string ogrNumarasi = form1.ogrenciNoOku(okunacakForm, basX - 3, basY + 1, sikokumahassasiyeti);

                ////OKUL KODU OKUMA///////

                basX = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().X + koseKareBaslangicNoktalari[0].X;
                basY = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getOkulKoduBaslangic().Y + koseKareBaslangicNoktalari[0].Y;

                binaryyap(okunacakForm, basX - 3, basY + 1, 6 * izgaraBoşlukGenislik, 10 * izgaraBoşlukGenislik);
                string okulkodu = form1.okulKoduOku(okunacakForm, basX - 3, basY + 1, sikokumahassasiyeti);

                ///////KİTAPÇIK TÜRÜ OKUMA////////

                basX = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().X + koseKareBaslangicNoktalari[0].X;
                basY = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getKitapcikTuruBaslangic().Y + koseKareBaslangicNoktalari[0].Y;

                binaryyap(okunacakForm, basX - 3, basY + 1, 1 * izgaraBoşlukGenislik, 4 * izgaraBoşlukGenislik);
                string kitTuru = form1.kitapcikTuruOku(okunacakForm, basX - 3, basY + 1, sikokumahassasiyeti);

                ///////SINIF-ŞUBE OKUMA//////////

                basX = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().X + koseKareBaslangicNoktalari[0].X;
                basY = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getSinifSubeBaslangic().Y + koseKareBaslangicNoktalari[0].Y;

                binaryyap(okunacakForm, basX - 3, basY + 1, 1 * izgaraBoşlukGenislik, 12 * izgaraBoşlukGenislik);
                binaryyap(okunacakForm, basX - 3 + izgaraBoşlukGenislik, basY + 1, 1 * izgaraBoşlukGenislik, 29 * izgaraBoşlukGenislik);
                string sinif = form1.sinifOku(okunacakForm, basX - 3, basY + 1, sikokumahassasiyeti);
                string sube = form1.subeOku(okunacakForm, basX - 3 + izgaraBoşlukGenislik, basY + 1, sikokumahassasiyeti);

                //////////DERSLERİ OKUMA////////////
                int sikSayisi = 0;
                if (okulTuruKalibre.Equals("İlkokul"))
                {
                    sikSayisi = 3;
                }
                if (okulTuruKalibre.Equals("Ortaokul"))
                {
                    sikSayisi = 4;
                }
                if (okulTuruKalibre.Equals("Lise"))
                {
                    sikSayisi = 5;
                }

                List<String> cevaplar = new List<string>();
                for (int i = 0; i < eklenenDersSayısıKalibre; i++)
                {
                    int sorusayisi = soruSayilariKalibre[i];

                    basX = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari()[i].X + koseKareBaslangicNoktalari[0].X;
                    basY = tumFormlarinDeğerleriKalibre[gosterilenFormSirasi].getDersBaslangicNoktalari()[i].Y + koseKareBaslangicNoktalari[0].Y;

                    binaryyap(okunacakForm, basX + 6 + izgaraBoşlukGenislik, basY, sikSayisi * izgaraBoşlukGenislik, sorusayisi * izgaraBoşlukGenislik);
                    string cevap = form1.dersleriOku(okunacakForm, basX + 6+izgaraBoşlukGenislik, basY, okulTuruKalibre, sorusayisi, sikSayisi, sikokumahassasiyeti);
                    cevaplar.Add(cevap);
                }

                /////OKUNANLARI YAZDIRMA//////////////
                dataGridViewOkunanlar.Rows.Add();
                for (int j = 0; j < dataGridViewOkunanlar.Rows[0].Cells.Count; j++)
                {
                    if (dataGridViewOkunanlar.Columns[j].HeaderText.Equals("Ad-Soyad"))
                    {
                        dataGridViewOkunanlar.Rows[0].Cells[j].Value = ogrAdi;
                    }
                    if (dataGridViewOkunanlar.Columns[j].HeaderText.Equals("Öğrenci No"))
                    {
                        dataGridViewOkunanlar.Rows[0].Cells[j].Value = ogrNumarasi;
                    }
                    if (dataGridViewOkunanlar.Columns[j].HeaderText.Equals("Sınıf-Şube"))
                    {
                        dataGridViewOkunanlar.Rows[0].Cells[j].Value = sinif + sube;
                    }
                    if (dataGridViewOkunanlar.Columns[j].HeaderText.Equals("Kitapçık Türü"))
                    {
                        dataGridViewOkunanlar.Rows[0].Cells[j].Value = kitTuru;
                    }
                    if (dataGridViewOkunanlar.Columns[j].HeaderText.Equals("Okul Kodu"))
                    {
                        dataGridViewOkunanlar.Rows[0].Cells[j].Value = okulkodu;
                    }
                }

                for (int i = 0; i < cevaplar.Count; i++)
                {
                    dataGridViewOkunanlar.Rows[0].Cells[dataGridViewOkunanlar.Rows[0].Cells.Count + i - cevaplar.Count].Value = cevaplar[i];
                }
            }
            else
            {
                dataGridViewOkunanlar.Rows.Add();
            }
        }
    }
}
