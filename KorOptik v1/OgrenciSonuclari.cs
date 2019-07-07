using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KorOptik_v1
{
    class OgrenciSonuclari
    {
        public List<String> sonuclar = new List<string>();
        public Double puan;
        public Double puanSAY;
        public Double puanEA;
        public Double puanSOZ;

        public List<String> getOgrenciSonuclari() {
            return this.sonuclar;
        }

        public void addOgrenciSonuclari(List<String> ogrenciSonuclari) {
            this.sonuclar = ogrenciSonuclari;
        }

        public Double getPuan() {
            return this.puan;
        }

        public void setPuan(Double puan) {
            this.puan = puan;
        }

        public Double getPuanSAY()
        {
            return this.puanSAY;
        }

        public void setPuanSAY(Double puan)
        {
            this.puanSAY = puan;
        }

        public Double getPuanEA()
        {
            return this.puanEA;
        }

        public void setPuanEA(Double puan)
        {
            this.puanEA = puan;
        }

        public Double getPuanSOZ()
        {
            return this.puanSOZ;
        }

        public void setPuanSOZ(Double puan)
        {
            this.puanSOZ = puan;
        }
    }
}
