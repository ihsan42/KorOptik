﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KorOptik_v1
{
    class OgrenciSonuclari
    {
        public List<String> sonuclar = new List<string>();
        public Double puan;

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
    }
}