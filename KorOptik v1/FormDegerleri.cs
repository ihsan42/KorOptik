using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KorOptik_v1
{
    public class FormDegerleri
    {
        int formWidht;
        int formHeight;
        int grilikesigi;
        int parlaklikesigi;
        int sikokumahassasiyeti;
        Point solUstSiyahKareBaslangic;
        List<Point> dersBaslangicNoktalari;
        Point adSoyadBaslangic;
        Point ogrenciNoBaslangic;
        Point sinifSubeBaslangic;
       // Point subeBaslangic;
        Point okulKoduBaslangic;
        Point kitapcıkTurBaslangic;

        /*public void formBaslangicNoktalari(Point solUstSiyahKareBaslangic, Point adSoyadBaslangic,Point ogrenciNoBaslangic,Point sinifSubeBaslangic,
        Point subeBaslangic,Point okulKoduBaslangic,Point kitapcıkTurBaslangic,Point ders1Baslangic,Point ders2Baslangic,
        Point ders3Baslangic,Point ders4Baslangic,Point ders5Baslangic,Point ders6Baslangic,Point ders7Baslangic,
        Point ders8Baslangic,Point ders9Baslangic,Point ders10Baslangic)
        {
            this.solUstSiyahKareBaslangic =solUstSiyahKareBaslangic;
            this.adSoyadBaslangic= adSoyadBaslangic;
            this.ogrenciNoBaslangic= ogrenciNoBaslangic;
            this.sinifSubeBaslangic= sinifSubeBaslangic;
            this.subeBaslangic= subeBaslangic;
            this.okulKoduBaslangic= okulKoduBaslangic;
            this.kitapcıkTurBaslangic= kitapcıkTurBaslangic;
            this.ders1Baslangic = ders1Baslangic;
            this.ders2Baslangic= ders2Baslangic;
            this.ders3Baslangic= ders3Baslangic;
            this.ders4Baslangic= ders4Baslangic;
            this.ders5Baslangic= ders5Baslangic;
            this.ders6Baslangic= ders6Baslangic;
            this.ders7Baslangic= ders7Baslangic;
            this.ders8Baslangic= ders8Baslangic;
            this.ders9Baslangic= ders9Baslangic;
            this.ders10Baslangic= ders10Baslangic;
        }*/

        public int getFormWidht()
        {
            return this.formWidht;
        }

        public void setFormWidht(int i)
        {
            this.formWidht = i;
        }

        public int getFormHeight()
        {
            return this.formHeight;
        }

        public void setFormHeight(int i)
        {
            this.formHeight = i;
        }

        public int getGrilikEsigi()
        {
            return this.grilikesigi;
        }

        public void setGrilikEsigi(int i)
        {
            this.grilikesigi = i;
        }

        public int getParlaklikEsigi()
        {
            return this.parlaklikesigi;
        }

        public void setParlaklikEsigi(int i)
        {
            this.parlaklikesigi = i;
        }

        public int getSikOkumaHassasiyeti()
        {
            return this.sikokumahassasiyeti;
        }

        public void setSikOkumaHassasiyeti(int i)
        {
            this.sikokumahassasiyeti = i;
        }


        public Point getsolUstSiyahKareBaslangic()
        {
            return this.solUstSiyahKareBaslangic;
        }

        public void setsolUstSiyahKareBaslangic(int x, int y)
        {
            this.solUstSiyahKareBaslangic = new Point(x, y);
        }

        public Point getAdSoyadBaslangic()
        {
            return this.adSoyadBaslangic;
        }

        public void setAdSoyadBaslangic(int x, int y)
        {
            this.adSoyadBaslangic = new Point(x,y);
        }

        public Point getOgrenciNoBaslangic()
        {
            return this.ogrenciNoBaslangic;
        }

        public void setOgrenciNoBaslangic(int x, int y)
        {
            this.ogrenciNoBaslangic = new Point(x, y);
        }

        public Point getSinifSubeBaslangic()
        {
            return this.sinifSubeBaslangic;
        }

        public void setSinifSubeBaslangic(int x, int y)
        {
            this.sinifSubeBaslangic = new Point(x, y);
        }

        public Point getKitapcikTuruBaslangic()
        {
            return this.kitapcıkTurBaslangic;
        }

        public void setKitapcikTuruBaslangic(int x, int y)
        {
            this.kitapcıkTurBaslangic = new Point(x, y);
        }

        public Point getOkulKoduBaslangic()
        {
            return this.okulKoduBaslangic;
        }

        public void setOkulKoduBaslangic(int x, int y)
        {
            this.okulKoduBaslangic = new Point(x, y);
        }

        public List<Point> getDersBaslangicNoktalari()
        {
            return this.dersBaslangicNoktalari;
        }

        public void addDersBaslangicNoktalari(List<Point> dersBasNok)
        {
            this.dersBaslangicNoktalari = dersBasNok;
        }
    }
}
