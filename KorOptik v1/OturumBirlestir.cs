using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace KorOptik_v1
{
    public partial class OturumBirlestir : Form
    {
        public OturumBirlestir()
        {
            InitializeComponent();
        }

        List<String> ogrencilerBirleştirilmiş;

        private void ComboBoxOturumSinavTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxOturumSinavTuru.SelectedIndex == 0)
            {
                labelOturum1.Visible = true;
                labelOturum1.Text = "Sözel";
                comboBoxOturum1.Visible = true;
                labelOturum2.Visible = true;
                labelOturum2.Text = "Sayısal";
                comboBoxOturum2.Visible = true;

                labelOturum3.Visible = false;
                labelOturum4.Visible = false;
                labelOturum5.Visible = false;

                comboBoxOturum3.Visible = false;
                comboBoxOturum4.Visible = false;
                comboBoxOturum5.Visible = false;

                button_3_OturumEkle.Visible = false;
                button_4_OturumEkle.Visible = false;
                button_5_OturumEkle.Visible = false;

                string dizin = "KaydedilenSonuclar";
                string[] dizindekiDosyalar = Directory.GetFiles(dizin);

                foreach (string dosya in dizindekiDosyalar)
                {
                    FileInfo fileInfo = new FileInfo(dosya);
                    string dosyaAdi = fileInfo.Name;

                    comboBoxOturum1.Items.Add(dosyaAdi);
                    comboBoxOturum2.Items.Add(dosyaAdi);
                }
            }

            if (comboBoxOturumSinavTuru.SelectedIndex == 1)
            {
                labelOturum1.Visible = true;
                labelOturum1.Text = "TYT";
                comboBoxOturum1.Visible = true;

                labelOturum2.Visible = true;
                labelOturum2.Text = "AYT";
                comboBoxOturum2.Visible = true;
                comboBoxOturum2.Items.Add("Sınav Yapılmadı");

                labelOturum3.Visible = false;               
                comboBoxOturum3.Visible = false;               

                labelOturum4.Visible = false;
                labelOturum5.Visible = false;

                comboBoxOturum4.Visible = false;
                comboBoxOturum5.Visible = false;

                button_3_OturumEkle.Visible = false;
                button_4_OturumEkle.Visible = false;
                button_5_OturumEkle.Visible = false;

                string dizin = "KaydedilenSonuclar";
                string[] dizindekiDosyalar = Directory.GetFiles(dizin);

                foreach (string dosya in dizindekiDosyalar)
                {
                    FileInfo fileInfo = new FileInfo(dosya);
                    string dosyaAdi = fileInfo.Name;

                    comboBoxOturum1.Items.Add(dosyaAdi);
                    comboBoxOturum2.Items.Add(dosyaAdi);
                }
            }

            if (comboBoxOturumSinavTuru.SelectedIndex == 2)
            {
                labelOturum1.Visible = true;
                labelOturum1.Text = "1. Oturum";
                comboBoxOturum1.Visible = true;
                labelOturum2.Visible = true;
                labelOturum2.Text = "2. Oturum";
                comboBoxOturum2.Visible = true;
                button_3_OturumEkle.Visible = true;

                labelOturum3.Visible = false;
                labelOturum4.Visible = false;
                labelOturum5.Visible = false;

                comboBoxOturum3.Visible = false;
                comboBoxOturum4.Visible = false;
                comboBoxOturum5.Visible = false;

                button_4_OturumEkle.Visible = false;
                button_5_OturumEkle.Visible = false;

                string dizin = "KaydedilenSonuclar";
                string[] dizindekiDosyalar = Directory.GetFiles(dizin);

                foreach (string dosya in dizindekiDosyalar)
                {
                    FileInfo fileInfo = new FileInfo(dosya);
                    string dosyaAdi = fileInfo.Name;

                    comboBoxOturum1.Items.Add(dosyaAdi);
                    comboBoxOturum2.Items.Add(dosyaAdi);
                }
            }
        }

        private void Button_3_OturumEkle_Click(object sender, EventArgs e)
        {
            labelOturum3.Visible = true;
            labelOturum3.Text = "3. Oturum";
            comboBoxOturum3.Visible = true;
            button_4_OturumEkle.Visible = true;

            button_3_OturumEkle.Visible = false;

            string dizin = "KaydedilenSonuclar";
            string[] dizindekiDosyalar = Directory.GetFiles(dizin);

            foreach (string dosya in dizindekiDosyalar)
            {
                FileInfo fileInfo = new FileInfo(dosya);
                string dosyaAdi = fileInfo.Name;

                comboBoxOturum3.Items.Add(dosyaAdi);
            }
        }

        private void Button_4_OturumEkle_Click(object sender, EventArgs e)
        {
            labelOturum4.Visible = true;
            labelOturum4.Text = "4. Oturum";
            comboBoxOturum4.Visible = true;
            button_5_OturumEkle.Visible = true;

            button_4_OturumEkle.Visible = false;

            string dizin = "KaydedilenSonuclar";
            string[] dizindekiDosyalar = Directory.GetFiles(dizin);

            foreach (string dosya in dizindekiDosyalar)
            {
                FileInfo fileInfo = new FileInfo(dosya);
                string dosyaAdi = fileInfo.Name;

                comboBoxOturum4.Items.Add(dosyaAdi);
            }
        }

        private void Button_5_OturumEkle_Click(object sender, EventArgs e)
        {
            labelOturum5.Visible = true;
            labelOturum5.Text = "5. Oturum";
            comboBoxOturum5.Visible = true;          

            button_5_OturumEkle.Visible = false;

            string dizin = "KaydedilenSonuclar";
            string[] dizindekiDosyalar = Directory.GetFiles(dizin);

            foreach (string dosya in dizindekiDosyalar)
            {
                FileInfo fileInfo = new FileInfo(dosya);
                string dosyaAdi = fileInfo.Name;

                comboBoxOturum5.Items.Add(dosyaAdi);
            }
        }

        private void ButtonOturumBirlestir_Click(object sender, EventArgs e)
        {
            if (comboBoxOturumSinavTuru.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen sınav türünü seçiniz!");
            }
            else if (comboBoxOturumSinavTuru.SelectedIndex == 0)//lgs
            {
                if (comboBoxOturum1.SelectedIndex < 0)
                {
                    MessageBox.Show("Lütfen kayıtlı Sözel bölümü seçiniz!");
                }

                if (comboBoxOturum2.SelectedIndex < 0)
                {
                    MessageBox.Show("Lütfen kayıtlı Sayısal bölümü seçiniz!");
                }

                if (comboBoxOturum1.SelectedIndex > -1 && comboBoxOturum2.SelectedIndex > -1)
                {
                    labelOturumKayitAdi.Visible = true;
                    textBoxOturumKayitAdi.Visible = true;
                    buttonOturumKaydet.Visible = true;
                    //birleştirme yap

                    string yolSozel = "KaydedilenSonuclar/" + comboBoxOturum1.SelectedItem.ToString();
                    StreamReader reader = File.OpenText(yolSozel);

                    String yazi;

                    List<String> ogrencilerSozel = new List<string>();
                    while ((yazi = reader.ReadLine()) != null)
                    {
                        ogrencilerSozel.Add(yazi);
                    }

                    reader.Close();

                    string yolSayisal = "KaydedilenSonuclar/" + comboBoxOturum2.SelectedItem.ToString();
                    reader = File.OpenText(yolSayisal);

                    List<String> ogrencilerSayisal = new List<string>();
                    while ((yazi = reader.ReadLine()) != null)
                    {
                        ogrencilerSayisal.Add(yazi);
                    }

                    reader.Close();

                    ogrencilerBirleştirilmiş = new List<string>();

                    string genelBilgiler = "Sıra|Ad-Soyad|Öğrenci No|Sınıf-Şube|Kitapçık Türü|Okul Kodu|&&&|Türkçe| | |İnkılap| | |Din Kültürü| | |Yabancı Dil| | |Matematik| | |Fen Bilimleri| | |TOPLAM| | |";
                    ogrencilerBirleştirilmiş.Add(genelBilgiler);

                    int toplamOgrSozel = ogrencilerSozel.Count;
                    int toplamOgrSayisal = ogrencilerSayisal.Count;                       

                    if (toplamOgrSozel==toplamOgrSayisal||toplamOgrSozel>toplamOgrSayisal)//sözelde giren öğrenci sayısı fazla ise
                    {                      
                        for (int i = 1; i < toplamOgrSozel; i++)
                        {
                            string ogrenciBirleşik = "";
                            string ogrenciSozelToplamBolumuHaric = "";
                            string ogrenciSayısalDersBilgileri = "";
                            string toplamBolumu="";
                            bool durumİkiOturumdaVarmi = false;

                            string[] ogrenciBilSozel = ogrencilerSozel[i].Split('|');                            

                            for (int k = 0; k < ogrenciBilSozel.Length - 4; k++)
                            {
                                ogrenciSozelToplamBolumuHaric += ogrenciBilSozel[k] + "|";
                            }

                            for (int j = 1; j < toplamOgrSayisal; j++)
                            {
                                string[] ogrenciBilSayisal = ogrencilerSayisal[j].Split('|');
                                if (ogrenciBilSozel[5].Equals("      "))
                                {
                                    ogrenciBilSozel[5] = "0";
                                }
                                   
                                if (ogrenciBilSayisal[5].Equals("      "))
                                {
                                    ogrenciBilSayisal[5] = "0";
                                }
                                                       
                                if (Convert.ToInt32(ogrenciBilSozel[5]) == Convert.ToInt32(ogrenciBilSayisal[5]) && Convert.ToInt32(ogrenciBilSozel[2]) == Convert.ToInt32(ogrenciBilSayisal[2]))
                                {                                    
                                    for (int k=7;k<ogrenciBilSayisal.Length-4;k++)
                                    {
                                        ogrenciSayısalDersBilgileri+= ogrenciBilSayisal[k]+"|";
                                    }                                   

                                    double toplamDogru = Convert.ToDouble(ogrenciBilSozel[19]) + Convert.ToDouble(ogrenciBilSayisal[13]);
                                    double toplamYanlis = Convert.ToDouble(ogrenciBilSozel[20]) + Convert.ToDouble(ogrenciBilSayisal[14]);
                                    double toplamNet = Convert.ToDouble(ogrenciBilSozel[21]) + Convert.ToDouble(ogrenciBilSayisal[15]);

                                    toplamBolumu= toplamDogru.ToString() + "|" + toplamYanlis.ToString() + "|" + toplamNet.ToString() + "|";
                                    
                                    durumİkiOturumdaVarmi = true;
                                }                                
                            }

                            if (durumİkiOturumdaVarmi == true)
                            {
                                ogrenciBirleşik = ogrenciSozelToplamBolumuHaric + ogrenciSayısalDersBilgileri + toplamBolumu;
                            }
                            else
                            {
                                ogrenciBirleşik = ogrenciSozelToplamBolumuHaric + "0|0|0|0|0|0|"+ogrenciBilSozel[19]+"|" + ogrenciBilSozel[20] + "|" + ogrenciBilSozel[21] + "|";
                            }

                            ogrencilerBirleştirilmiş.Add(ogrenciBirleşik);
                        }
                    }

                    if (toplamOgrSayisal>toplamOgrSozel)//sayısalda giren öğrenci sayısı fazla ise
                    {
                        for (int i = 1; i < toplamOgrSayisal; i++)
                        {
                            string ogrenciBirleşik = "";
                            string ogrenciSayisalToplamDerslerHaric = "";
                            string ogrenciSayısalDersBilgileriToplamHaric = "";
                            string ogrenciSozellDersBilgileri = "";
                            string toplamBolumu = "";
                            bool durumİkiOturumdaVarmi = false;

                            string[] ogrenciBilSayisal = ogrencilerSayisal[i].Split('|');

                            for (int k = 0; k <7; k++)
                            {
                                ogrenciSayisalToplamDerslerHaric += ogrenciBilSayisal[k] + "|";
                            }

                            for (int k = 7; k < ogrenciBilSayisal.Length-4; k++)
                            {
                                ogrenciSayısalDersBilgileriToplamHaric += ogrenciBilSayisal[k] + "|";
                            }

                            for (int j = 1; j < toplamOgrSozel; j++)
                            {
                                string[] ogrenciBilSozel = ogrencilerSozel[j].Split('|');

                                if (Convert.ToInt32(ogrenciBilSozel[5]) == Convert.ToInt32(ogrenciBilSayisal[5]) && Convert.ToInt32(ogrenciBilSozel[2]) == Convert.ToInt32(ogrenciBilSayisal[2]))
                                {
                                    for (int k = 7; k < ogrenciBilSozel.Length - 4; k++)
                                    {
                                        ogrenciSozellDersBilgileri += ogrenciBilSozel[k] + "|";
                                    }

                                    double toplamDogru = Convert.ToDouble(ogrenciBilSozel[19]) + Convert.ToDouble(ogrenciBilSayisal[13]);
                                    double toplamYanlis = Convert.ToDouble(ogrenciBilSozel[20]) + Convert.ToDouble(ogrenciBilSayisal[14]);
                                    double toplamNet = Convert.ToDouble(ogrenciBilSozel[21]) + Convert.ToDouble(ogrenciBilSayisal[15]);

                                    toplamBolumu = toplamDogru.ToString() + "|" + toplamYanlis.ToString() + "|" + toplamNet.ToString() + "|";

                                    durumİkiOturumdaVarmi = true;
                                }
                            }

                            if (durumİkiOturumdaVarmi == true)
                            {
                                ogrenciBirleşik = ogrenciSayisalToplamDerslerHaric + ogrenciSozellDersBilgileri+ogrenciSayısalDersBilgileriToplamHaric + toplamBolumu;
                            }
                            else
                            {
                                ogrenciBirleşik = ogrenciSayisalToplamDerslerHaric + "0|0|0|0|0|0|0|0|0|0|0|0|"+ogrenciSayısalDersBilgileriToplamHaric + ogrenciBilSayisal[13] + "|" + ogrenciBilSayisal[14] + "|" + ogrenciBilSayisal[15] + "|";
                            }

                            ogrencilerBirleştirilmiş.Add(ogrenciBirleşik);
                        }
                    }

                    MessageBox.Show("Birleştirme tamamlandı! Kaydetmeyi unutmayınız!");
                }
            }
            else if (comboBoxOturumSinavTuru.SelectedIndex == 1) //YKS
            {
                if (comboBoxOturum1.SelectedIndex < 0)
                {
                    MessageBox.Show("Lütfen kayıtlı TYT sınav sonucunu seçiniz!");
                }

                if (comboBoxOturum2.SelectedIndex < 0)
                {
                    MessageBox.Show("Lütfen kayıtlı AYT sınav sonucunu seçiniz!");
                }
              
                if (comboBoxOturum1.SelectedIndex > -1 && comboBoxOturum2.SelectedIndex > -1)
                {
                    labelOturumKayitAdi.Visible = true;
                    textBoxOturumKayitAdi.Visible = true;
                    buttonOturumKaydet.Visible = true;

                    //birleştirme yap
                    string yolTYT = "KaydedilenSonuclar/" + comboBoxOturum1.SelectedItem.ToString();
                    StreamReader reader = File.OpenText(yolTYT);

                    String yazi;

                    List<String> ogrencilerTYT = new List<string>();
                    while ((yazi = reader.ReadLine()) != null)
                    {
                        ogrencilerTYT.Add(yazi);
                    }

                    reader.Close();

                    string yolAYT = "KaydedilenSonuclar/" + comboBoxOturum2.SelectedItem.ToString();
                    reader = File.OpenText(yolAYT);

                    List<String> ogrencilerAYT = new List<string>();
                    while ((yazi = reader.ReadLine()) != null)
                    {
                        ogrencilerAYT.Add(yazi);
                    }

                    reader.Close();

                    ogrencilerBirleştirilmiş = new List<string>();

                    string genelBilgiler = "Sıra|Ad-Soyad|Öğrenci No|Sınıf-Şube|Kitapçık Türü|Okul Kodu| |TYT-Türkçe| | |TYT-Tarih| | |TYT-Coğrafya| | |TYT-Felsefe| | |TYT-Din Kültürü| | |TYT-Matematik| | |TYT-Fizik| | |TYT-Kimya| | |TYT-Biyoloji| | |AYT-T.D.E| | |AYT-Tarih-1| | |AYT-Coğrafya-1| | |AYT-Tarih-2| | |AYT-Coğrafya-2| | |AYT-Felsefe| | |AYT-Din Kültürü| | |AYT-Matematik| | |AYT-Fizik| | |AYT-Kimya| | |AYT-Biyoloji| | |";
                    ogrencilerBirleştirilmiş.Add(genelBilgiler);

                    int toplamOgrTYT = ogrencilerTYT.Count;
                    int toplamOgrAYT = ogrencilerAYT.Count;

                    if (toplamOgrTYT == toplamOgrAYT||toplamOgrTYT > toplamOgrAYT)//tytde giren öğrenci sayısı fazla ise
                    {
                        for (int i = 1; i < toplamOgrTYT; i++)
                        {
                            string ogrenciBirleşik = "";
                            string ogrenciTYTToplamBolumuHaric = "";
                            string ogrenciAYTDersBilgileri = "";

                            bool durumİkiOturumdaVarmi = false;

                            string[] ogrenciBilTYT = ogrencilerTYT[i].Split('|');

                            for (int k = 0; k < ogrenciBilTYT.Length - 4; k++)
                            {
                                ogrenciTYTToplamBolumuHaric += ogrenciBilTYT[k] + "|";
                            }

                            for (int j = 1; j < toplamOgrAYT; j++)
                            {
                                string[] ogrenciBilAYT = ogrencilerAYT[j].Split('|');

                                if (Convert.ToInt32(ogrenciBilTYT[5]) == Convert.ToInt32(ogrenciBilAYT[5]) && Convert.ToInt32(ogrenciBilTYT[2]) == Convert.ToInt32(ogrenciBilAYT[2]))
                                {
                                    for (int k = 7; k < ogrenciBilAYT.Length - 4; k++)
                                    {
                                        ogrenciAYTDersBilgileri += ogrenciBilAYT[k] + "|";
                                    }
                                  
                                    durumİkiOturumdaVarmi = true;
                                }
                            }

                            if (durumİkiOturumdaVarmi == true)
                            {
                                ogrenciBirleşik = ogrenciTYTToplamBolumuHaric + ogrenciAYTDersBilgileri;
                            }
                            else
                            {
                                ogrenciBirleşik = ogrenciTYTToplamBolumuHaric + "0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|";
                            }

                            ogrencilerBirleştirilmiş.Add(ogrenciBirleşik);
                        }
                    }

                    if (toplamOgrAYT>toplamOgrTYT)//aytde giren öğrenci sayısı fazla ise
                    {
                        for (int i = 1; i < toplamOgrAYT; i++)
                        {
                            string ogrenciBirleşik = "";
                            string ogrenciAYTToplamDerslerHaric = "";
                            string ogrenciAYTDersBilgileriToplamHaric = "";
                            string ogrenciTYTDersBilgileri = "";

                            bool durumİkiOturumdaVarmi = false;

                            string[] ogrenciBilAYT = ogrencilerAYT[i].Split('|');

                            for (int k = 0; k < 7; k++)
                            {
                                ogrenciAYTToplamDerslerHaric += ogrenciBilAYT[k] + "|";
                            }

                            for (int k = 7; k < ogrenciBilAYT.Length - 4; k++)
                            {
                                ogrenciAYTDersBilgileriToplamHaric += ogrenciBilAYT[k] + "|";
                            }

                            for (int j = 1; j < toplamOgrTYT; j++)
                            {
                                string[] ogrenciBilTYT = ogrencilerTYT[j].Split('|');

                                if (Convert.ToInt32(ogrenciBilTYT[5]) == Convert.ToInt32(ogrenciBilAYT[5]) && Convert.ToInt32(ogrenciBilTYT[2]) == Convert.ToInt32(ogrenciBilAYT[2]))
                                {
                                    for (int k = 7; k < ogrenciBilTYT.Length - 4; k++)
                                    {
                                        ogrenciTYTDersBilgileri += ogrenciBilTYT[k] + "|";
                                    }                 

                                    durumİkiOturumdaVarmi = true;
                                }
                            }

                            if (durumİkiOturumdaVarmi == true)
                            {
                                ogrenciBirleşik = ogrenciAYTToplamDerslerHaric + ogrenciTYTDersBilgileri + ogrenciAYTDersBilgileriToplamHaric;
                            }
                            else
                            {
                                ogrenciBirleşik = ogrenciAYTToplamDerslerHaric + "0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|0|" + ogrenciAYTDersBilgileriToplamHaric;
                            }

                            ogrencilerBirleştirilmiş.Add(ogrenciBirleşik);
                        }
                    }

                    MessageBox.Show("Birleştirme tamamlandı! Kaydetmeyi unutmayınız!");
                }
            }
            else if (comboBoxOturumSinavTuru.SelectedIndex == 2)//diğer
            {
                if(comboBoxOturum5.Visible==false && comboBoxOturum4.Visible == false && comboBoxOturum3.Visible == false)
                {
                    if (comboBoxOturum1.SelectedIndex < 0)
                    {
                        MessageBox.Show("Lütfen 1. oturumu seçiniz!");
                    }

                    if (comboBoxOturum2.SelectedIndex < 0)
                    {
                        MessageBox.Show("Lütfen 2. oturumu seçiniz!");
                    }

                    if (comboBoxOturum1.SelectedIndex > -1 && comboBoxOturum2.SelectedIndex > -1)
                    {
                        labelOturumKayitAdi.Visible = true;
                        textBoxOturumKayitAdi.Visible = true;
                        buttonOturumKaydet.Visible = true;
                        //birleştirme yap

                        MessageBox.Show("Birleştirme tamamlandı! Kaydetmeyi unutmayınız!");
                    }
                }

                if (comboBoxOturum5.Visible == false && comboBoxOturum4.Visible == false && comboBoxOturum3.Visible == true)
                {
                    if (comboBoxOturum1.SelectedIndex < 0)
                    {
                        MessageBox.Show("Lütfen 1. oturumu seçiniz!");
                    }

                    if (comboBoxOturum2.SelectedIndex < 0)
                    {
                        MessageBox.Show("Lütfen 2. oturumu seçiniz!");
                    }

                    if (comboBoxOturum3.SelectedIndex < 0)
                    {
                        MessageBox.Show("Lütfen 3. oturumu seçiniz!");
                    }

                    if (comboBoxOturum1.SelectedIndex > -1 && comboBoxOturum2.SelectedIndex > -1 && comboBoxOturum3.SelectedIndex > -1)
                    {
                        labelOturumKayitAdi.Visible = true;
                        textBoxOturumKayitAdi.Visible = true;
                        buttonOturumKaydet.Visible = true;
                        //birleştirme yap

                        MessageBox.Show("Birleştirme tamamlandı! Kaydetmeyi unutmayınız!");
                    }
                }

                if (comboBoxOturum5.Visible == false && comboBoxOturum4.Visible == true && comboBoxOturum3.Visible == true)
                {
                    if (comboBoxOturum1.SelectedIndex < 0)
                    {
                        MessageBox.Show("Lütfen 1. oturumu seçiniz!");
                    }

                    if (comboBoxOturum2.SelectedIndex < 0)
                    {
                        MessageBox.Show("Lütfen 2. oturumu seçiniz!");
                    }

                    if (comboBoxOturum3.SelectedIndex < 0)
                    {
                        MessageBox.Show("Lütfen 3. oturumu seçiniz!");
                    }

                    if (comboBoxOturum4.SelectedIndex < 0)
                    {
                        MessageBox.Show("Lütfen 4. oturumu seçiniz!");
                    }

                    if (comboBoxOturum1.SelectedIndex > -1 && comboBoxOturum2.SelectedIndex > -1 && comboBoxOturum3.SelectedIndex > -1 && comboBoxOturum4.SelectedIndex > -1)
                    {
                        labelOturumKayitAdi.Visible = true;
                        textBoxOturumKayitAdi.Visible = true;
                        buttonOturumKaydet.Visible = true;
                        //birleştirme yap

                        MessageBox.Show("Birleştirme tamamlandı! Kaydetmeyi unutmayınız!");
                    }
                }

                if (comboBoxOturum5.Visible == true && comboBoxOturum4.Visible == true && comboBoxOturum3.Visible == true)
                {
                    if (comboBoxOturum1.SelectedIndex < 0)
                    {
                        MessageBox.Show("Lütfen 1. oturumu seçiniz!");
                    }

                    if (comboBoxOturum2.SelectedIndex < 0)
                    {
                        MessageBox.Show("Lütfen 2. oturumu seçiniz!");
                    }

                    if (comboBoxOturum3.SelectedIndex < 0)
                    {
                        MessageBox.Show("Lütfen 3. oturumu seçiniz!");
                    }

                    if (comboBoxOturum4.SelectedIndex < 0)
                    {
                        MessageBox.Show("Lütfen 4. oturumu seçiniz!");
                    }

                    if (comboBoxOturum5.SelectedIndex < 0)
                    {
                        MessageBox.Show("Lütfen 5. oturumu seçiniz!");
                    }

                    if (comboBoxOturum1.SelectedIndex > -1 && comboBoxOturum2.SelectedIndex > -1 && comboBoxOturum3.SelectedIndex > -1 && comboBoxOturum4.SelectedIndex > -1 && comboBoxOturum5.SelectedIndex > -1)
                    {
                        labelOturumKayitAdi.Visible = true;
                        textBoxOturumKayitAdi.Visible = true;
                        buttonOturumKaydet.Visible = true;
                        //birleştirme yap

                        MessageBox.Show("Birleştirme tamamlandı! Kaydetmeyi unutmayınız!");
                    }
                }
            }
        }
        private void ButtonOturumKaydet_Click(object sender, EventArgs e)
        {
            buttonOturumRaporAl.Visible = true;

            String kayitAdi = textBoxOturumKayitAdi.Text.ToString();
            if (textBoxOturumKayitAdi == null)
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
                String yol = "KaydedilenSonuclar/" + kayitAdi + ".txt";
                String dosyaIsmi = kayitAdi + ".txt";

                DirectoryInfo di = new DirectoryInfo("KaydedilenSonuclar");
                FileInfo[] files = di.GetFiles("*.txt", SearchOption.AllDirectories);

                Boolean varMi = false;
                foreach (FileInfo fi in files)
                {
                    if (fi.Name.Equals(dosyaIsmi))
                    {
                        varMi = true;
                    }
                }
                if (varMi == true)
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
                    FileStream fs = new FileStream(yol, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);

                    for (int i = 0; i < ogrencilerBirleştirilmiş.Count; i++)
                    {                        
                        sw.WriteLine(ogrencilerBirleştirilmiş[i]);                                                                 
                    }
                                  
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                    MessageBox.Show("Kayıt tamamlandı");

                    ogrencilerBirleştirilmiş.Clear();
                }

            }
        }

        private void ButtonOturumRaporAl_Click(object sender, EventArgs e)
        {
            Raporlar raporlar = new Raporlar();
            raporlar.Show();
        }
    }
}
