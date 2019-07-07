using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KorOptik_v1
{
    public partial class Degerlendir : Form
    {
        public Degerlendir()
        {
            InitializeComponent();
        }

        FileStream fs;
        public String formadi;
        public List<string> okunacakStandartAlanlar=new List<string>();
        public List<string> okunacakDersler=new List<string>();
        public List<OgrenciOkunan> okunanlarTekOturum;
        List<int> soruSayilari;     

        private void Degerlendir_Load(object sender, EventArgs e)
        {
            dataGridViewCevapAnahtariA.Visible = true;
            dataGridViewCevapAnahtariB.Visible = false;
            dataGridViewCevapAnahtariC.Visible = false;
            dataGridViewCevapAnahtariD.Visible = false;

            labelA.Visible = true;
            labelB.Visible = false;
            labelC.Visible = false;
            labelD.Visible = false;

            comboBoxKitapcikTuru.SelectedIndex = 0;

            Form1 form1 = (Form1)Application.OpenForms["Form1"];

            sonucListesiniHazirla(formadi, listViewSonuclar, okunacakStandartAlanlar, okunacakDersler);

            okunanlarTekOturum = new List<OgrenciOkunan>();
            foreach (OgrenciOkunan ogrenci in form1.okunanlar)
            {
                okunanlarTekOturum.Add(ogrenci);
            }

            soruSayilari = new List<int>();

            List<int> soruSay = new List<int>();
            Veritabani vt = new Veritabani();
            vt.baglan();
            soruSay = vt.soruSayılariniGetir(formadi);
            foreach (int say in soruSay)
            {
                if (say > 0)
                {
                    soruSayilari.Add(say);
                }
            }

            cevapAnahtariDersleriniGoruntule(dataGridViewCevapAnahtariA, okunacakDersler, soruSayilari);
            cevapAnahtariDersleriniGoruntule(dataGridViewCevapAnahtariB, okunacakDersler, soruSayilari);
            cevapAnahtariDersleriniGoruntule(dataGridViewCevapAnahtariC, okunacakDersler, soruSayilari);
            cevapAnahtariDersleriniGoruntule(dataGridViewCevapAnahtariD, okunacakDersler, soruSayilari);


        }

        private void cevapAnahtariDersleriniGoruntule(DataGridView dataGridView, List<String> oturumDerslerHepsi, List<int> soruSayilari)
        {
            int soruSayisiEnBuyuk = 0;
            foreach (int s in soruSayilari)
            {
                if (s > soruSayisiEnBuyuk)
                {
                    soruSayisiEnBuyuk = s;
                }
            }

            dataGridView.Columns.Add("column0", "DERSLER");

            int sira = 0;
            for (int i = 0; i < soruSayisiEnBuyuk; i++)
            {
                sira += 1;
                dataGridView.Columns.Add("column" + sira.ToString(), sira.ToString());
            }

            for (int i = 0; i < oturumDerslerHepsi.Count; i++)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i].Cells[0].Value = oturumDerslerHepsi[i];
                dataGridView.Rows[i].Cells[0].ReadOnly = true;
            }

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            for (int i = 0; i < oturumDerslerHepsi.Count; i++)
            {
                for (int j = 1; j < soruSayilari[i]+1; j++)
                {
                    dataGridView.Rows[i].Cells[j].Style.BackColor = Color.LightGray;
                    dataGridView.Columns[j].Width = 40;
                }
            }

            for (int i = 0; i < oturumDerslerHepsi.Count; i++)
            {
                for (int j = 1; j < dataGridView.Columns.Count - 1; j++)
                {
                    if (dataGridView.Rows[i].Cells[j].Style.BackColor != Color.LightGray)
                    {
                        dataGridView.Rows[i].Cells[j].ReadOnly = true;
                    }
                    
                }
            }
        }

        private void sonucListesiniHazirla(String formismi, ListView listView, List<String> okunacakStdalanlar, List<String> derslerHepsi)
        {
            listViewSonuclar.Clear();
            listView.Columns.Add("Sıra");
            int sira = 0;
            foreach (String s in okunacakStdalanlar)
            {
                sira += 1;
                listView.Columns.Add("column" + sira.ToString(), s);               
            }

            listView.Columns.Add("&&&");

            if (formadi.Equals("TYT"))
            {
                sira += 1;

                listView.Columns.Add("column" + sira.ToString(), "TYT-Türkçe");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");
              
                listView.Columns.Add("column" + sira.ToString(), "TYT-Tarih");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "TYT-Coğrafya");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "TYT-Felsefe");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "TYT-Din Kültürü");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "TYT-Matematik");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "TYT-Fizik");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "TYT-Kimya");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "TYT-Biyoloji");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");
            }
            else if (formadi.Equals("AYT"))
            {
                sira += 1;

                listView.Columns.Add("column" + sira.ToString(), "AYT-T.D.E");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "AYT-Tarih-1");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "AYT-Coğrafya-1");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "AYT-Tarih-2");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "AYT-Coğrafya-2");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "AYT-Felsefe");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "AYT-Din Kültürü");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "AYT-Matematik");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "AYT-Fizik");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "AYT-Kimya");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");

                listView.Columns.Add("column" + sira.ToString(), "AYT-Biyoloji");
                listView.Columns.Add("column" + sira.ToString(), " ");
                listView.Columns.Add("column" + sira.ToString(), " ");
            }
            else
            {
                foreach (String s in derslerHepsi)
                {
                    sira += 1;
                    listView.Columns.Add("column" + sira.ToString(), s);
                    listView.Columns.Add("column" + sira.ToString(), " ");
                    listView.Columns.Add("column" + sira.ToString(), " ");
                }
            }
            
            listView.Columns.Add("TOPLAM");
            listView.Columns.Add(" ");
            listView.Columns.Add(" ");                              

            listView.Items.Add("");
            for (int i=0;i<okunacakStdalanlar.Count+1;i++)
            {
                listView.Items[0].SubItems.Add(" ");
            }

            if (formadi.Equals("TYT"))
            {
                for (int i = 0; i < 9; i++)
                {
                    listView.Items[0].SubItems.Add("D");
                    listView.Items[0].SubItems.Add("Y");
                    listView.Items[0].SubItems.Add("N");
                }
            }
            else if (formadi.Equals("AYT"))
            {
                for (int i = 0; i < 11; i++)
                {
                    listView.Items[0].SubItems.Add("D");
                    listView.Items[0].SubItems.Add("Y");
                    listView.Items[0].SubItems.Add("N");
                }
            }
            else
            {
                for (int i = 0; i < derslerHepsi.Count; i++)
                {
                    listView.Items[0].SubItems.Add("D");
                    listView.Items[0].SubItems.Add("Y");
                    listView.Items[0].SubItems.Add("N");
                }
            }
            
            listView.Items[0].SubItems.Add("Doğru");
            listView.Items[0].SubItems.Add("Yanlış");
            listView.Items[0].SubItems.Add("Net");            
        }

        private void buttonDegerlendir_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            sonucListesiniHazirla(formadi, listViewSonuclar, okunacakStandartAlanlar, okunacakDersler);

            int siraNo = 0;
            for (int m = 0; m < okunanlarTekOturum.Count; m++)
            {
                OgrenciOkunan ogrenci = okunanlarTekOturum[m];

                double toplamDogru = 0;
                double toplamYanlis = 0;
                double toplamNet = 0;

                int listViewDersColumnsStart = listViewSonuclar.Columns.Count - ogrenci.getOkunanCevaplar().Count * 3 - 3;

                siraNo += 1;
                listViewSonuclar.Items.Add(siraNo.ToString());
                for (int k = 1; k < listViewSonuclar.Columns.Count; k++)
                {
                    listViewSonuclar.Items[m + 1].SubItems.Add(" ");
                }

                for (int k = 1; k < listViewSonuclar.Columns.Count; k++)
                {
                    if (listViewSonuclar.Columns[k].Text.Equals("Ad-Soyad"))
                    {
                        listViewSonuclar.Items[m + 1].SubItems[k].Text = ogrenci.getAdSoyad();
                    }
                    if (listViewSonuclar.Columns[k].Text.Equals("Öğrenci No"))
                    {
                        listViewSonuclar.Items[m + 1].SubItems[k].Text = ogrenci.getOgrenciNo();
                    }
                    if (listViewSonuclar.Columns[k].Text.Equals("Sınıf-Şube"))
                    {
                        listViewSonuclar.Items[m + 1].SubItems[k].Text = ogrenci.getSinifSube();
                    }
                    if (listViewSonuclar.Columns[k].Text.Equals("Kitapçık Türü"))
                    {
                        listViewSonuclar.Items[m + 1].SubItems[k].Text = ogrenci.getKitapcikTuru();
                    }
                    if (listViewSonuclar.Columns[k].Text.Equals("Okul Kodu"))
                    {
                        listViewSonuclar.Items[m + 1].SubItems[k].Text = ogrenci.getOkulKodu();
                    }
                }

                String kitapcikTuru = "";
                kitapcikTuru = ogrenci.getKitapcikTuru();
                if (kitapcikTuru.Equals(" "))
                {
                    kitapcikTuru = "A";
                }

                if (formadi.Equals("TYT"))// tyt formu seçiliyse
                {
                    Char[] turkceCevaplar = ogrenci.getOkunanCevaplar()[0].ToCharArray();
                    Char[] sosyalCevaplar = ogrenci.getOkunanCevaplar()[1].ToCharArray();
                    Char[] matematikCevaplar = ogrenci.getOkunanCevaplar()[2].ToCharArray();
                    Char[] fenCevaplar = ogrenci.getOkunanCevaplar()[3].ToCharArray();

                    double dogruSayisiTurkce = 0;
                    double yanlisSayisiTurkce = 0;

                    double dogruSayisiTarih = 0;
                    double yanlisSayisiTarih = 0;

                    double dogruSayisiCografya = 0;
                    double yanlisSayisicografya = 0;

                    double dogruSayisiFelsefe = 0;
                    double yanlisSayisiFelsefe = 0;

                    double dogruSayisiDin = 0;
                    double yanlisSayisiDin = 0;

                    double dogruSayisiMatematik = 0;
                    double yanlisSayisiMatematik = 0;

                    double dogruSayisiFizik = 0;
                    double yanlisSayisiFizik = 0;

                    double dogruSayisiKimya = 0;
                    double yanlisSayisiKimya = 0;

                    double dogruSayisiBiyoloji = 0;
                    double yanlisSayisiBiyoloji = 0;

                    DataGridView dataGridView = null;
                    if (kitapcikTuru.Equals("A"))
                    {
                        dataGridView = dataGridViewCevapAnahtariA;
                    }
                    else if (kitapcikTuru.Equals("B"))
                    {
                        dataGridView = dataGridViewCevapAnahtariB;
                    }
                    else if (kitapcikTuru.Equals("C"))
                    {
                        dataGridView = dataGridViewCevapAnahtariC;
                    }
                    else if (kitapcikTuru.Equals("D"))
                    {
                        dataGridView = dataGridViewCevapAnahtariD;
                    }


                    for (int j = 1; j < 41; j++)//türkçe
                    {
                        if (dataGridView.Rows[0].Cells[j].Value != null)
                        {
                            if (turkceCevaplar[j - 1].ToString().Equals(dataGridView.Rows[0].Cells[j].Value.ToString()))
                            {
                                dogruSayisiTurkce++;
                            }
                            else if (turkceCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiTurkce++;
                            }
                        }
                    }

                    double netSayisiTurkce = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiTurkce = Math.Round(dogruSayisiTurkce - yanlisSayisiTurkce / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiTurkce = dogruSayisiTurkce;
                    }

                    toplamDogru += dogruSayisiTurkce;
                    toplamYanlis += yanlisSayisiTurkce;
                    toplamNet += netSayisiTurkce;

                    listViewSonuclar.Items[m + 1].SubItems[7].Text = dogruSayisiTurkce.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[8].Text = yanlisSayisiTurkce.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[9].Text = netSayisiTurkce.ToString();

                    for (int j = 1; j < 6; j++)//tarih
                    {
                        if (dataGridView.Rows[1].Cells[j].Value != null)
                        {
                            if (sosyalCevaplar[j - 1].ToString().Equals(dataGridView.Rows[1].Cells[j].Value.ToString()))
                            {
                                dogruSayisiTarih++;
                            }
                            else if (sosyalCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiTarih++;
                            }
                        }
                    }

                    double netSayisiTarih = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiTarih = Math.Round(dogruSayisiTarih - yanlisSayisiTarih / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiTarih = dogruSayisiTarih;
                    }

                    toplamDogru += dogruSayisiTarih;
                    toplamYanlis += yanlisSayisiTarih;
                    toplamNet += netSayisiTarih;

                    listViewSonuclar.Items[m + 1].SubItems[10].Text = dogruSayisiTarih.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[11].Text = yanlisSayisiTarih.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[12].Text = netSayisiTarih.ToString();

                    for (int j = 6; j < 11; j++)//coğrafya
                    {
                        if (dataGridView.Rows[1].Cells[j].Value != null)
                        {
                            if (sosyalCevaplar[j - 1].ToString().Equals(dataGridView.Rows[1].Cells[j].Value.ToString()))
                            {
                                dogruSayisiCografya++;
                            }
                            else if (sosyalCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisicografya++;
                            }
                        }
                    }

                    double netSayisiCografya = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiCografya = Math.Round(dogruSayisiCografya - yanlisSayisicografya / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiCografya = dogruSayisiCografya;
                    }

                    toplamDogru += dogruSayisiCografya;
                    toplamYanlis += yanlisSayisicografya;
                    toplamNet += netSayisiCografya;

                    listViewSonuclar.Items[m + 1].SubItems[13].Text = dogruSayisiCografya.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[14].Text = yanlisSayisicografya.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[15].Text = netSayisiCografya.ToString();


                    for (int j = 11; j < 16; j++)//felsefe 
                    {
                        if (dataGridView.Rows[1].Cells[j].Value != null)
                        {
                            if (sosyalCevaplar[j - 1].ToString().Equals(dataGridView.Rows[1].Cells[j].Value.ToString()))
                            {
                                dogruSayisiFelsefe++;
                            }
                            else if (sosyalCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiFelsefe++;
                            }
                        }
                    }

                    if (sosyalCevaplar[15].ToString().Equals(" ") && sosyalCevaplar[16].ToString().Equals(" ") && sosyalCevaplar[17].ToString().Equals(" ") && sosyalCevaplar[18].ToString().Equals(" ") && sosyalCevaplar[19].ToString().Equals(" "))
                    {//din boş bırakılmış ise(dinden muaf olanlar için)
                        for (int j = 21; j < 26; j++)
                        {
                            if (dataGridView.Rows[1].Cells[j].Value != null)
                            {
                                if (sosyalCevaplar[j - 1].ToString().Equals(dataGridView.Rows[1].Cells[j].Value.ToString()))
                                {
                                    dogruSayisiFelsefe++;
                                }
                                else if (sosyalCevaplar[j - 1].ToString().Equals(" ")) { }
                                else
                                {
                                    yanlisSayisiFelsefe++;
                                }
                            }
                        }
                    }

                    double netSayisiFelsefe = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiFelsefe = Math.Round(dogruSayisiFelsefe - yanlisSayisiFelsefe / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiFelsefe = dogruSayisiFelsefe;
                    }

                    toplamDogru += dogruSayisiFelsefe;
                    toplamYanlis += yanlisSayisiFelsefe;
                    toplamNet += netSayisiFelsefe;

                    listViewSonuclar.Items[m + 1].SubItems[16].Text = dogruSayisiFelsefe.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[17].Text = yanlisSayisiFelsefe.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[18].Text = netSayisiFelsefe.ToString();

                    for (int j = 16; j < 21; j++)//din
                    {
                        if (dataGridView.Rows[1].Cells[j].Value != null)
                        {
                            if (sosyalCevaplar[j - 1].ToString().Equals(dataGridView.Rows[1].Cells[j].Value.ToString()))
                            {
                                dogruSayisiDin++;
                            }
                            else if (sosyalCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiDin++;
                            }
                        }
                    }

                    double netSayisiDin = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiDin = Math.Round(dogruSayisiDin - yanlisSayisiDin / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiDin = dogruSayisiDin;
                    }

                    toplamDogru += dogruSayisiDin;
                    toplamYanlis += yanlisSayisiDin;
                    toplamNet += netSayisiDin;

                    listViewSonuclar.Items[m + 1].SubItems[19].Text = dogruSayisiDin.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[20].Text = yanlisSayisiDin.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[21].Text = netSayisiDin.ToString();

                    for (int j = 1; j < 41; j++)//matematik
                    {
                        if (dataGridView.Rows[2].Cells[j].Value != null)
                        {
                            if (matematikCevaplar[j - 1].ToString().Equals(dataGridView.Rows[2].Cells[j].Value.ToString()))
                            {
                                dogruSayisiMatematik++;
                            }
                            else if (matematikCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiMatematik++;
                            }
                        }
                    }

                    double netSayisiMatematik = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiMatematik = Math.Round(dogruSayisiMatematik - yanlisSayisiMatematik / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiMatematik = dogruSayisiMatematik;
                    }

                    toplamDogru += dogruSayisiMatematik;
                    toplamYanlis += yanlisSayisiMatematik;
                    toplamNet += netSayisiMatematik;

                    listViewSonuclar.Items[m + 1].SubItems[22].Text = dogruSayisiMatematik.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[23].Text = yanlisSayisiMatematik.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[24].Text = netSayisiMatematik.ToString();

                    for (int j = 1; j < 8; j++)//fizik
                    {
                        if (dataGridView.Rows[3].Cells[j].Value != null)
                        {
                            if (fenCevaplar[j - 1].ToString().Equals(dataGridView.Rows[3].Cells[j].Value.ToString()))
                            {
                                dogruSayisiFizik++;
                            }
                            else if (fenCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiFizik++;
                            }
                        }
                    }

                    double netSayisiFizik = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiFizik = Math.Round(dogruSayisiFizik - yanlisSayisiFizik / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiFizik = dogruSayisiFizik;
                    }

                    toplamDogru += dogruSayisiFizik;
                    toplamYanlis += yanlisSayisiFizik;
                    toplamNet += netSayisiFizik;

                    listViewSonuclar.Items[m + 1].SubItems[25].Text = dogruSayisiFizik.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[26].Text = yanlisSayisiFizik.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[27].Text = netSayisiFizik.ToString();

                    for (int j = 8; j < 15; j++)//kimya
                    {
                        if (dataGridView.Rows[3].Cells[j].Value != null)
                        {
                            if (fenCevaplar[j - 1].ToString().Equals(dataGridView.Rows[3].Cells[j].Value.ToString()))
                            {
                                dogruSayisiKimya++;
                            }
                            else if (fenCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiKimya++;
                            }
                        }
                    }

                    double netSayisiKimya = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiKimya = Math.Round(dogruSayisiKimya - yanlisSayisiKimya / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiKimya = dogruSayisiKimya;
                    }

                    toplamDogru += dogruSayisiKimya;
                    toplamYanlis += yanlisSayisiKimya;
                    toplamNet += netSayisiKimya;

                    listViewSonuclar.Items[m + 1].SubItems[28].Text = dogruSayisiKimya.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[29].Text = yanlisSayisiKimya.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[30].Text = netSayisiKimya.ToString();

                    for (int j = 15; j < 21; j++)//biyoloji
                    {
                        if (dataGridView.Rows[3].Cells[j].Value != null)
                        {
                            if (fenCevaplar[j - 1].ToString().Equals(dataGridView.Rows[3].Cells[j].Value.ToString()))
                            {
                                dogruSayisiBiyoloji++;
                            }
                            else if (fenCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiBiyoloji++;
                            }
                        }
                    }

                    double netSayisiBiyoloji = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiBiyoloji = Math.Round(dogruSayisiBiyoloji - yanlisSayisiBiyoloji / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiBiyoloji = dogruSayisiBiyoloji;
                    }

                    toplamDogru += dogruSayisiBiyoloji;
                    toplamYanlis += yanlisSayisiBiyoloji;
                    toplamNet += netSayisiBiyoloji;

                    listViewSonuclar.Items[m + 1].SubItems[31].Text = dogruSayisiBiyoloji.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[32].Text = yanlisSayisiBiyoloji.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[33].Text = netSayisiBiyoloji.ToString();

                    //toplamları ekle
                    listViewSonuclar.Items[m + 1].SubItems[34].Text = toplamDogru.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[35].Text = toplamYanlis.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[36].Text = toplamNet.ToString();
                }
                else if (formadi.Equals("AYT"))// ayt formu seçiliyse
                {
                    Char[] TDE_SB_1_Cevaplar = ogrenci.getOkunanCevaplar()[0].ToCharArray();
                    Char[] sosyalCevaplar = ogrenci.getOkunanCevaplar()[1].ToCharArray();
                    Char[] matematikCevaplar = ogrenci.getOkunanCevaplar()[2].ToCharArray();
                    Char[] fenCevaplar = ogrenci.getOkunanCevaplar()[3].ToCharArray();

                    double dogruSayisiTDE = 0;
                    double yanlisSayisiTDE = 0;

                    double dogruSayisiTarih1 = 0;
                    double yanlisSayisiTarih1 = 0;

                    double dogruSayisiCografya1 = 0;
                    double yanlisSayisicografya1 = 0;

                    double dogruSayisiTarih2 = 0;
                    double yanlisSayisiTarih2 = 0;

                    double dogruSayisiCografya2 = 0;
                    double yanlisSayisicografya2 = 0;

                    double dogruSayisiFelsefe = 0;
                    double yanlisSayisiFelsefe = 0;

                    double dogruSayisiDin = 0;
                    double yanlisSayisiDin = 0;

                    double dogruSayisiMatematik = 0;
                    double yanlisSayisiMatematik = 0;

                    double dogruSayisiFizik = 0;
                    double yanlisSayisiFizik = 0;

                    double dogruSayisiKimya = 0;
                    double yanlisSayisiKimya = 0;

                    double dogruSayisiBiyoloji = 0;
                    double yanlisSayisiBiyoloji = 0;

                    DataGridView dataGridView = null;
                    if (kitapcikTuru.Equals("A"))
                    {
                        dataGridView = dataGridViewCevapAnahtariA;
                    }
                    else if (kitapcikTuru.Equals("B"))
                    {
                        dataGridView = dataGridViewCevapAnahtariB;
                    }
                    else if (kitapcikTuru.Equals("C"))
                    {
                        dataGridView = dataGridViewCevapAnahtariC;
                    }
                    else if (kitapcikTuru.Equals("D"))
                    {
                        dataGridView = dataGridViewCevapAnahtariD;
                    }


                    for (int j = 1; j < 25; j++)//TDE
                    {
                        if (dataGridView.Rows[0].Cells[j].Value != null)
                        {
                            if (TDE_SB_1_Cevaplar[j - 1].ToString().Equals(dataGridView.Rows[0].Cells[j].Value.ToString()))
                            {
                                dogruSayisiTDE++;
                            }
                            else if (TDE_SB_1_Cevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiTDE++;
                            }
                        }
                    }

                    double netSayisiTDE = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiTDE = Math.Round(dogruSayisiTDE - yanlisSayisiTDE / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiTDE = dogruSayisiTDE;
                    }

                    toplamDogru += dogruSayisiTDE;
                    toplamYanlis += yanlisSayisiTDE;
                    toplamNet += netSayisiTDE;

                    listViewSonuclar.Items[m + 1].SubItems[7].Text = dogruSayisiTDE.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[8].Text = yanlisSayisiTDE.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[9].Text = netSayisiTDE.ToString();

                    for (int j = 25; j < 35; j++)//Tarih1
                    {
                        if (dataGridView.Rows[0].Cells[j].Value != null)
                        {
                            if (TDE_SB_1_Cevaplar[j - 1].ToString().Equals(dataGridView.Rows[0].Cells[j].Value.ToString()))
                            {
                                dogruSayisiTarih1++;
                            }
                            else if (TDE_SB_1_Cevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiTarih1++;
                            }
                        }
                    }

                    double netSayisiTarih1 = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiTarih1 = Math.Round(dogruSayisiTarih1 - yanlisSayisiTarih1 / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiTarih1 = dogruSayisiTarih1;
                    }

                    toplamDogru += dogruSayisiTarih1;
                    toplamYanlis += yanlisSayisiTarih1;
                    toplamNet += netSayisiTarih1;

                    listViewSonuclar.Items[m + 1].SubItems[10].Text = dogruSayisiTarih1.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[11].Text = yanlisSayisiTarih1.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[12].Text = netSayisiTarih1.ToString();

                    for (int j = 35; j < 41; j++)//Coğrajya1
                    {
                        if (dataGridView.Rows[0].Cells[j].Value != null)
                        {
                            if (TDE_SB_1_Cevaplar[j - 1].ToString().Equals(dataGridView.Rows[0].Cells[j].Value.ToString()))
                            {
                                dogruSayisiCografya1++;
                            }
                            else if (TDE_SB_1_Cevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisicografya1++;
                            }
                        }
                    }

                    double netSayisiCografya1 = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiCografya1 = Math.Round(dogruSayisiCografya1 - yanlisSayisicografya1 / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiCografya1 = dogruSayisiCografya1;
                    }

                    toplamDogru += dogruSayisiCografya1;
                    toplamYanlis += yanlisSayisicografya1;
                    toplamNet += netSayisiCografya1;

                    listViewSonuclar.Items[m + 1].SubItems[13].Text = dogruSayisiCografya1.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[14].Text = yanlisSayisicografya1.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[15].Text = netSayisiCografya1.ToString();

                    for (int j = 1; j < 12; j++)//tarih2
                    {
                        if (dataGridView.Rows[1].Cells[j].Value != null)
                        {
                            if (sosyalCevaplar[j - 1].ToString().Equals(dataGridView.Rows[1].Cells[j].Value.ToString()))
                            {
                                dogruSayisiTarih2++;
                            }
                            else if (sosyalCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiTarih2++;
                            }
                        }
                    }

                    double netSayisiTarih = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiTarih = Math.Round(dogruSayisiTarih2 - yanlisSayisiTarih2 / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiTarih = dogruSayisiTarih2;
                    }

                    toplamDogru += dogruSayisiTarih2;
                    toplamYanlis += yanlisSayisiTarih2;
                    toplamNet += netSayisiTarih;

                    listViewSonuclar.Items[m + 1].SubItems[16].Text = dogruSayisiTarih2.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[17].Text = yanlisSayisiTarih2.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[18].Text = netSayisiTarih.ToString();

                    for (int j = 11; j < 23; j++)//coğrafya
                    {
                        if (dataGridView.Rows[1].Cells[j].Value != null)
                        {
                            if (sosyalCevaplar[j - 1].ToString().Equals(dataGridView.Rows[1].Cells[j].Value.ToString()))
                            {
                                dogruSayisiCografya2++;
                            }
                            else if (sosyalCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisicografya2++;
                            }
                        }
                    }

                    double netSayisiCografya = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiCografya = Math.Round(dogruSayisiCografya2 - yanlisSayisicografya2 / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiCografya = dogruSayisiCografya2;
                    }

                    toplamDogru += dogruSayisiCografya2;
                    toplamYanlis += yanlisSayisicografya2;
                    toplamNet += netSayisiCografya;

                    listViewSonuclar.Items[m + 1].SubItems[19].Text = dogruSayisiCografya2.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[20].Text = yanlisSayisicografya2.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[21].Text = netSayisiCografya.ToString();


                    for (int j = 23; j < 35; j++)//felsefe 
                    {
                        if (dataGridView.Rows[1].Cells[j].Value != null)
                        {
                            if (sosyalCevaplar[j - 1].ToString().Equals(dataGridView.Rows[1].Cells[j].Value.ToString()))
                            {
                                dogruSayisiFelsefe++;
                            }
                            else if (sosyalCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiFelsefe++;
                            }
                        }
                    }

                    if (sosyalCevaplar[34].ToString().Equals(" ") && sosyalCevaplar[35].ToString().Equals(" ") && sosyalCevaplar[36].ToString().Equals(" ") && sosyalCevaplar[37].ToString().Equals(" ") && sosyalCevaplar[38].ToString().Equals(" ") && sosyalCevaplar[39].ToString().Equals(" "))
                    {//din boş bırakılmış ise(dinden muaf olanlar için)
                        for (int j = 41; j < 47; j++)
                        {
                            if (dataGridView.Rows[1].Cells[j].Value != null)
                            {
                                if (sosyalCevaplar[j - 1].ToString().Equals(dataGridView.Rows[1].Cells[j].Value.ToString()))
                                {
                                    dogruSayisiFelsefe++;
                                }
                                else if (sosyalCevaplar[j - 1].ToString().Equals(" ")) { }
                                else
                                {
                                    yanlisSayisiFelsefe++;
                                }
                            }
                        }
                    }

                    double netSayisiFelsefe = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiFelsefe = Math.Round(dogruSayisiFelsefe - yanlisSayisiFelsefe / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiFelsefe = dogruSayisiFelsefe;
                    }

                    toplamDogru += dogruSayisiFelsefe;
                    toplamYanlis += yanlisSayisiFelsefe;
                    toplamNet += netSayisiFelsefe;

                    listViewSonuclar.Items[m + 1].SubItems[22].Text = dogruSayisiFelsefe.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[23].Text = yanlisSayisiFelsefe.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[24].Text = netSayisiFelsefe.ToString();

                    for (int j = 35; j < 41; j++)//din
                    {
                        if (dataGridView.Rows[1].Cells[j].Value != null)
                        {
                            if (sosyalCevaplar[j - 1].ToString().Equals(dataGridView.Rows[1].Cells[j].Value.ToString()))
                            {
                                dogruSayisiDin++;
                            }
                            else if (sosyalCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiDin++;
                            }
                        }
                    }

                    double netSayisiDin = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiDin = Math.Round(dogruSayisiDin - yanlisSayisiDin / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiDin = dogruSayisiDin;
                    }

                    toplamDogru += dogruSayisiDin;
                    toplamYanlis += yanlisSayisiDin;
                    toplamNet += netSayisiDin;

                    listViewSonuclar.Items[m + 1].SubItems[25].Text = dogruSayisiDin.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[26].Text = yanlisSayisiDin.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[27].Text = netSayisiDin.ToString();

                    for (int j = 1; j < 41; j++)//matematik
                    {
                        if (dataGridView.Rows[2].Cells[j].Value != null)
                        {
                            if (matematikCevaplar[j - 1].ToString().Equals(dataGridView.Rows[2].Cells[j].Value.ToString()))
                            {
                                dogruSayisiMatematik++;
                            }
                            else if (matematikCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiMatematik++;
                            }
                        }
                    }

                    double netSayisiMatematik = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiMatematik = Math.Round(dogruSayisiMatematik - yanlisSayisiMatematik / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiMatematik = dogruSayisiMatematik;
                    }

                    toplamDogru += dogruSayisiMatematik;
                    toplamYanlis += yanlisSayisiMatematik;
                    toplamNet += netSayisiMatematik;

                    listViewSonuclar.Items[m + 1].SubItems[28].Text = dogruSayisiMatematik.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[29].Text = yanlisSayisiMatematik.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[30].Text = netSayisiMatematik.ToString();

                    for (int j = 1; j < 15; j++)//fizik
                    {
                        if (dataGridView.Rows[3].Cells[j].Value != null)
                        {
                            if (fenCevaplar[j - 1].ToString().Equals(dataGridView.Rows[3].Cells[j].Value.ToString()))
                            {
                                dogruSayisiFizik++;
                            }
                            else if (fenCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiFizik++;
                            }
                        }
                    }

                    double netSayisiFizik = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiFizik = Math.Round(dogruSayisiFizik - yanlisSayisiFizik / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiFizik = dogruSayisiFizik;
                    }

                    toplamDogru += dogruSayisiFizik;
                    toplamYanlis += yanlisSayisiFizik;
                    toplamNet += netSayisiFizik;

                    listViewSonuclar.Items[m + 1].SubItems[31].Text = dogruSayisiFizik.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[32].Text = yanlisSayisiFizik.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[33].Text = netSayisiFizik.ToString();

                    for (int j = 15; j < 28; j++)//kimya
                    {
                        if (dataGridView.Rows[3].Cells[j].Value != null)
                        {
                            if (fenCevaplar[j - 1].ToString().Equals(dataGridView.Rows[3].Cells[j].Value.ToString()))
                            {
                                dogruSayisiKimya++;
                            }
                            else if (fenCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiKimya++;
                            }
                        }
                    }

                    double netSayisiKimya = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiKimya = Math.Round(dogruSayisiKimya - yanlisSayisiKimya / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiKimya = dogruSayisiKimya;
                    }

                    toplamDogru += dogruSayisiKimya;
                    toplamYanlis += yanlisSayisiKimya;
                    toplamNet += netSayisiKimya;

                    listViewSonuclar.Items[m + 1].SubItems[34].Text = dogruSayisiKimya.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[35].Text = yanlisSayisiKimya.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[36].Text = netSayisiKimya.ToString();

                    for (int j = 28; j < 41; j++)//biyoloji
                    {
                        if (dataGridView.Rows[3].Cells[j].Value != null)
                        {
                            if (fenCevaplar[j - 1].ToString().Equals(dataGridView.Rows[3].Cells[j].Value.ToString()))
                            {
                                dogruSayisiBiyoloji++;
                            }
                            else if (fenCevaplar[j - 1].ToString().Equals(" ")) { }
                            else
                            {
                                yanlisSayisiBiyoloji++;
                            }
                        }
                    }

                    double netSayisiBiyoloji = 0;
                    if (radioButtonGotursun.Checked)
                    {
                        netSayisiBiyoloji = Math.Round(dogruSayisiBiyoloji - yanlisSayisiBiyoloji / 4, 2);
                    }
                    else if (radioButtonGoturmesin.Checked)
                    {
                        netSayisiBiyoloji = dogruSayisiBiyoloji;
                    }

                    toplamDogru += dogruSayisiBiyoloji;
                    toplamYanlis += yanlisSayisiBiyoloji;
                    toplamNet += netSayisiBiyoloji;

                    listViewSonuclar.Items[m + 1].SubItems[37].Text = dogruSayisiBiyoloji.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[38].Text = yanlisSayisiBiyoloji.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[39].Text = netSayisiBiyoloji.ToString();

                    //toplamları ekle
                    listViewSonuclar.Items[m + 1].SubItems[40].Text = toplamDogru.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[41].Text = toplamYanlis.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[42].Text = toplamNet.ToString();
                }
                else// seçilen form Tyt ve ayt değilse
                {
                    for (int i = 0; i < ogrenci.getOkunanCevaplar().Count; i++)
                    {
                        double dogruSayisi = 0;
                        double bosSayisi = 0;
                        double yanlisSayisi = 0;

                        Char[] cevaplar = ogrenci.getOkunanCevaplar()[i].ToCharArray();

                        DataGridView dataGridView = null;
                        if (kitapcikTuru.Equals("A"))
                        {
                            dataGridView = dataGridViewCevapAnahtariA;
                        }
                        else if (kitapcikTuru.Equals("B"))
                        {
                            dataGridView = dataGridViewCevapAnahtariB;
                        }
                        else if (kitapcikTuru.Equals("C"))
                        {
                            dataGridView = dataGridViewCevapAnahtariC;
                        }
                        else if (kitapcikTuru.Equals("D"))
                        {
                            dataGridView = dataGridViewCevapAnahtariD;
                        }

                        for (int j = 1; j < dataGridView.ColumnCount; j++)
                        {
                            if (dataGridView.Rows[i].Cells[j].Value != null)
                            {
                                if (cevaplar[j - 1].ToString().Equals(dataGridView.Rows[i].Cells[j].Value.ToString()))
                                {
                                    dogruSayisi++;
                                }
                                else if (cevaplar[j - 1].ToString().Equals(" "))
                                {
                                    bosSayisi++;
                                }
                                else
                                {
                                    yanlisSayisi++;
                                }
                            }
                        }

                        double netSayisi = 0;
                        if (radioButtonGotursun.Checked)
                        {
                            if (form1.okulturu.Equals("İlkokul"))
                            {
                                netSayisi = Math.Round(dogruSayisi - yanlisSayisi / 2, 2);
                            }
                            if (form1.okulturu.Equals("Ortaokul"))
                            {
                                netSayisi = Math.Round(dogruSayisi - yanlisSayisi / 3, 2);
                            }
                            if (form1.okulturu.Equals("Lise"))
                            {
                                netSayisi = Math.Round(dogruSayisi - yanlisSayisi / 4, 2);
                            }
                        }
                        else if (radioButtonGoturmesin.Checked)
                        {
                            netSayisi = dogruSayisi;
                        }

                        toplamDogru += dogruSayisi;
                        toplamYanlis += yanlisSayisi;
                        toplamNet += netSayisi;

                        listViewSonuclar.Items[m + 1].SubItems[listViewDersColumnsStart + 3 * i].Text = dogruSayisi.ToString();
                        listViewSonuclar.Items[m + 1].SubItems[listViewDersColumnsStart + 3 * i + 1].Text = yanlisSayisi.ToString();
                        listViewSonuclar.Items[m + 1].SubItems[listViewDersColumnsStart + 3 * i + 2].Text = netSayisi.ToString();
                    }
                    listViewSonuclar.Items[m + 1].SubItems[listViewDersColumnsStart + ogrenci.getOkunanCevaplar().Count * 3].Text = toplamDogru.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[listViewDersColumnsStart + ogrenci.getOkunanCevaplar().Count * 3 + 1].Text = toplamYanlis.ToString();
                    listViewSonuclar.Items[m + 1].SubItems[listViewDersColumnsStart + ogrenci.getOkunanCevaplar().Count * 3 + 2].Text = toplamNet.ToString();
                }                             
            }

        }

        private void radioButtonGotursun_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonGotursun.Checked && radioButtonGoturmesin.Checked)
            {
                radioButtonGoturmesin.Checked = false;
            }
        }

        private void radioButtonGoturmesin_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonGotursun.Checked && radioButtonGoturmesin.Checked)
            {
                radioButtonGotursun.Checked = false;
            }
        }

        private void comboBoxKitapcikTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxKitapcikTuru.SelectedIndex == 0)
            {
                dataGridViewCevapAnahtariA.Visible = true;
                dataGridViewCevapAnahtariB.Visible = false;
                dataGridViewCevapAnahtariC.Visible = false;
                dataGridViewCevapAnahtariD.Visible = false;

                labelA.Visible = true;
                labelB.Visible = false;
                labelC.Visible = false;
                labelD.Visible = false;
            }
            if (comboBoxKitapcikTuru.SelectedIndex == 1)
            {
                dataGridViewCevapAnahtariA.Visible = false;
                dataGridViewCevapAnahtariB.Visible = true;
                dataGridViewCevapAnahtariC.Visible = false;
                dataGridViewCevapAnahtariD.Visible = false;

                labelA.Visible = false;
                labelB.Visible = true;
                labelC.Visible = false;
                labelD.Visible = false;
            }
            if (comboBoxKitapcikTuru.SelectedIndex == 2)
            {
                dataGridViewCevapAnahtariA.Visible = false;
                dataGridViewCevapAnahtariB.Visible = false;
                dataGridViewCevapAnahtariC.Visible = true;
                dataGridViewCevapAnahtariD.Visible = false;

                labelA.Visible = false;
                labelB.Visible = false;
                labelC.Visible = true;
                labelD.Visible = false;
            }
            if (comboBoxKitapcikTuru.SelectedIndex == 3)
            {
                dataGridViewCevapAnahtariA.Visible = false;
                dataGridViewCevapAnahtariB.Visible = false;
                dataGridViewCevapAnahtariC.Visible = false;
                dataGridViewCevapAnahtariD.Visible = true;

                labelA.Visible = false;
                labelB.Visible = false;
                labelC.Visible = false;
                labelD.Visible = true;
            }
        }

        private void buttonSonucKaydet_Click(object sender, EventArgs e)
        {
            buttonSonucKaydetSon.Visible = true;
            labelKayitIsmi.Visible = true;
            textBoxKayitIsmi.Visible = true;               
        }

        private void buttonSonucKaydetSon_Click(object sender, EventArgs e)
        {
            String kayitAdi = textBoxKayitIsmi.Text.ToString();
            if (textBoxKayitIsmi == null)
            {
                MessageBox.Show("Lütfen kayıt adı giriniz!");
            }
            else if (kayitAdi.Equals("/") || kayitAdi.Equals("*") || kayitAdi.Equals("-") || kayitAdi.Equals("+") || kayitAdi.Equals("#") || kayitAdi.Equals("/") || kayitAdi.Equals("!") || kayitAdi.Equals("^") || kayitAdi.Equals("%") || kayitAdi.Equals("&") || kayitAdi.Equals("=") || kayitAdi.Equals("|") || kayitAdi.Equals("?") || kayitAdi.Equals("'"))
            {
                MessageBox.Show("Lütfen özel karakter kullanmayın!");
            }
            else
            {
                Boolean durum1 = false;
                Boolean durum2 = false;
                String yol = "KaydedilenSonuclar/"+kayitAdi + ".txt";
                String dosyaIsmi = kayitAdi + ".txt";

                DirectoryInfo di = new DirectoryInfo("KaydedilenSonuclar");
                FileInfo[] files = di.GetFiles("*.txt", SearchOption.AllDirectories);

                Boolean varMi = false;
                foreach(FileInfo fi in files)
                {
                    if (fi.Name.Equals(dosyaIsmi)) {
                        varMi = true;
                    }
                }
                if (varMi==true)
                {
                    DialogResult onay = new DialogResult();
                    onay = MessageBox.Show("Bu kayıt ismiyle daha önceden kaydedilmiş sonuçlar var.Üzerine yazılsın mı?", "Dikkat!", MessageBoxButtons.YesNo);
                    if (onay == DialogResult.Yes)
                    {                        
                        durum1 = true;
                        if (System.IO.File.Exists(yol))
                        {
                            System.IO.File.Delete(yol);
                        }                    
                    }                   
                }
                else
                {                   
                    durum2 = true;
                }

                if (durum1 == true || durum2 == true)
                {
                    fs = new FileStream(yol, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);

                    for (int i = 0; i < listViewSonuclar.Columns.Count; i++)
                    {
                        //if (!listViewSonuclar.Columns[i].Text.ToString().Equals(""))
                        //{
                            sw.Write(listViewSonuclar.Columns[i].Text.ToString() + "|");
                        //}                                          
                    }

                    sw.WriteLine();

                    for (int i = 1; i < listViewSonuclar.Items.Count; i++)
                    {                      
                        for (int j = 0; j < listViewSonuclar.Items[i].SubItems.Count; j++)
                        {
                            sw.Write(listViewSonuclar.Items[i].SubItems[j].Text.ToString() + "|");
                        }
                        sw.WriteLine();
                    }
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                    MessageBox.Show("Kayıt tamamlandı");
                }

            }
        }

        private void SonucYazdırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Raporlar raporlar = new Raporlar();
            raporlar.Show();
        }
    }
}
