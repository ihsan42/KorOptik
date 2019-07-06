using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KorOptik_v1
{
    public class OgrenciOkunan
    {
        List<String> okunanCevaplar;       
        string okulkodu;
        string ogrenciNo;
        String adSoyad;
        String sinifSube;
        String kitapcikTürü;     

        public List<String> getOkunanCevaplar()
        {
            return this.okunanCevaplar;
        }

        public void addOkunanCevaplar(List<String> okunanCevaplar)
        {
            this.okunanCevaplar = okunanCevaplar;
        }     

        public String getOkulKodu()
        {
            return this.okulkodu;
        }
        public void setOkulKodu(String i)
        {
            this.okulkodu = i;
        }

        public String getOgrenciNo()
        {
            return this.ogrenciNo;
        }
        public void setOgrenciNo(String i)
        {
            this.ogrenciNo = i;
        }

        public String getAdSoyad()
        {
            return this.adSoyad;
        }
        public void setAdSoyad(String s)
        {
            this.adSoyad = s;
        }

        public String getSinifSube()
        {
            return this.sinifSube;
        }
        public void setSinifSube(String s)
        {
            this.sinifSube = s;
        }

        public String getKitapcikTuru()
        {
            return this.kitapcikTürü;
        }
        public void setKitapcikTuru(String s)
        {
            this.kitapcikTürü = s;
        }

        

    }
}
