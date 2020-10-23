using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace KorOptik_v1
{
    public class Veritabani
    {
        SQLiteConnection baglantiFormBilgileri = new SQLiteConnection(@"Data Source=tasarlananFormlar.db; Version=3; FailIfMissing=False;");
        SQLiteConnection baglantiCevapAnahtariA = new SQLiteConnection(@"Data Source=tasarlananFormlar.db; Version=3; FailIfMissing=False;");

        SQLiteCommand komutFormBilgileri = new SQLiteCommand();
        SQLiteCommand komutCevapAnahtariA = new SQLiteCommand();

        public void baglan()
        {
            if (!File.Exists("tasarlananFormlar.db")) {
                SQLiteConnection.CreateFile("tasarlananFormlar.db");

                string sql = "CREATE TABLE table_tasarlananformlar(ID INTEGER PRIMARY KEY AUTOINCREMENT,"+
                "formadi TEXT,okulturu TEXT,adisoyadiX TEXT, adisoyadiY TEXT,ogrencinoX TEXT, ogrencinoY TEXT, sinifsubeX TEXT, sinifsubeY TEXT, kitapcikturuX TEXT,"+
                "kitapcikturuY TEXT, okulkoduX TEXT, okulkoduY TEXT, baslikX TEXT, baslikY TEXT, eklenenCevapAlani1X TEXT, eklenenCevapAlani1Y TEXT," +
                "eklenenCevapAlani2X TEXT, eklenenCevapAlani2Y TEXT, eklenenCevapAlani3X TEXT, eklenenCevapAlani3Y TEXT, eklenenCevapAlani4X TEXT," +
                "eklenenCevapAlani4Y TEXT, eklenenCevapAlani5X TEXT, eklenenCevapAlani5Y TEXT, eklenenCevapAlani6X TEXT," +
                "eklenenCevapAlani6Y TEXT, eklenenCevapAlani7X TEXT, eklenenCevapAlani7Y TEXT, eklenenCevapAlani8X TEXT, eklenenCevapAlani8Y TEXT," +
                "eklenenCevapAlani9X TEXT, eklenenCevapAlani9Y TEXT, eklenenCevapAlani10X TEXT, eklenenCevapAlani10Y TEXT," +
                "sorusayisi1 TEXT, sorusayisi2 TEXT, sorusayisi3 TEXT, sorusayisi4 TEXT, sorusayisi5 TEXT, sorusayisi6 TEXT, sorusayisi7 TEXT, sorusayisi8 TEXT," +
                "sorusayisi9 TEXT, sorusayisi10 TEXT, dersadi1 TEXT, dersadi2 TEXT, dersadi3 TEXT, dersadi4 TEXT, dersadi5 TEXT, dersadi6 TEXT, dersadi7 TEXT, dersadi8 TEXT," +
                "dersadi9 TEXT, dersadi10 TEXT, grilikesigi TEXT, parlaklikesigi TEXT, sikokumahassasiyeti TEXT, widhtForm TEXT, heightForm TEXT);";

                string sqlCevapAnahtariA = "CREATE TABLE table_cevap_anahtariA(ID INTEGER PRIMARY KEY AUTOINCREMENT, cevaplar TEXT);";
                
                baglantiFormBilgileri.Open();
                komutFormBilgileri = new SQLiteCommand(sql, baglantiFormBilgileri);
                komutFormBilgileri.ExecuteNonQuery();
                baglantiFormBilgileri.Close();

                baglantiCevapAnahtariA.Open();
                komutCevapAnahtariA = new SQLiteCommand(sqlCevapAnahtariA, baglantiCevapAnahtariA);
                komutCevapAnahtariA.ExecuteNonQuery();
                baglantiCevapAnahtariA.Close();

            }
            else
            {
                if (baglantiFormBilgileri.State == ConnectionState.Closed)
                {
                    baglantiFormBilgileri.Open();
                }
                else
                {
                    baglantiFormBilgileri.Close();
                    baglantiFormBilgileri.Open();
                }

                if (baglantiCevapAnahtariA.State == ConnectionState.Closed)
                {
                    baglantiCevapAnahtariA.Open();
                }
                else
                {
                    baglantiCevapAnahtariA.Close();
                    baglantiCevapAnahtariA.Open();
                }
            }            
        }

        public int grilikesigiGetir(String formadi)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT grilikesigi FROM table_tasarlananformlar WHERE formadi = '" + formadi + "'", baglantiFormBilgileri);
            DataTable tablo = new DataTable();
            adapter.Fill(tablo);
            int grilikesigi = Convert.ToInt32(tablo.Rows[0][0]);
            adapter.Dispose();
            tablo.Dispose();
            return grilikesigi;
        }

        public int parlaklikesigiGetir(String formadi)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT parlaklikesigi FROM table_tasarlananformlar WHERE formadi = '" + formadi + "'", baglantiFormBilgileri);
            DataTable tablo = new DataTable();
            adapter.Fill(tablo);
            int parlaklikesigi = Convert.ToInt32(tablo.Rows[0][0]);
            adapter.Dispose();
            tablo.Dispose();
            return parlaklikesigi;
        }

        public int sikokumahassasiyetiGetir(String formadi)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT sikokumahassasiyeti FROM table_tasarlananformlar WHERE formadi = '" + formadi + "'", baglantiFormBilgileri);
            DataTable tablo = new DataTable();
            adapter.Fill(tablo);
            int sikokumahassasiyeti = Convert.ToInt32(tablo.Rows[0][0]);
            adapter.Dispose();
            tablo.Dispose();
            return sikokumahassasiyeti;
        }

        public Point formEbatlariniGetir(String formadi)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT widhtForm, heightForm FROM table_tasarlananformlar WHERE formadi = '" + formadi + "'", baglantiFormBilgileri);
            DataTable tablo = new DataTable();
            adapter.Fill(tablo);
            Point ebatlar =new Point(Convert.ToInt32(tablo.Rows[0][0]), Convert.ToInt32(tablo.Rows[0][1]));
            adapter.Dispose();
            tablo.Dispose();
            return ebatlar;
        }
        
        public void guncelleTasarlananFormu(String formadi, List<Point> baslangicNoktalari, int grilikesigi, int parlaklikesigi, int sikokumahassasiyeti,int widhtForm, int heightForm)
        {
            komutFormBilgileri.Connection = baglantiFormBilgileri;
            komutFormBilgileri.CommandText = "UPDATE table_tasarlananformlar SET adisoyadiX='" + baslangicNoktalari[0].X + "',adisoyadiY='" + baslangicNoktalari[0].Y + "',ogrencinoX='" + baslangicNoktalari[1].X + "',ogrencinoY='" + baslangicNoktalari[1].Y + "',sinifsubeX='" + baslangicNoktalari[2].X + "',sinifsubeY='" + baslangicNoktalari[2].Y + "',kitapcikturuX='" + baslangicNoktalari[3].X + "',kitapcikturuY='" + baslangicNoktalari[3].Y + "',okulkoduX='" + baslangicNoktalari[4].X + "',okulkoduY='" + baslangicNoktalari[4].Y + "',baslikX='" + baslangicNoktalari[5].X + "', baslikY= '" + baslangicNoktalari[5].Y + "' " +
                ",eklenenCevapAlani1X='" + baslangicNoktalari[6].X + "',eklenenCevapAlani1Y='" + baslangicNoktalari[6].Y + "',eklenenCevapAlani2X='" + baslangicNoktalari[7].X + "',eklenenCevapAlani2Y='" + baslangicNoktalari[7].Y + "',eklenenCevapAlani3X='" + baslangicNoktalari[8].X + "',eklenenCevapAlani3Y='" + baslangicNoktalari[8].Y + "',eklenenCevapAlani4X='" + baslangicNoktalari[9].X + "',eklenenCevapAlani4Y='" + baslangicNoktalari[9].Y + "',eklenenCevapAlani5X='" + baslangicNoktalari[10].X + "',eklenenCevapAlani5Y='" + baslangicNoktalari[10].Y + "'" +
                ",eklenenCevapAlani6X='" + baslangicNoktalari[11].X + "',eklenenCevapAlani6Y='" + baslangicNoktalari[11].Y + "',eklenenCevapAlani7X='" + baslangicNoktalari[12].X + "',eklenenCevapAlani7Y='" + baslangicNoktalari[12].Y + "',eklenenCevapAlani8X='" + baslangicNoktalari[13].X + "',eklenenCevapAlani8Y='" + baslangicNoktalari[13].Y + "',eklenenCevapAlani9X='" + baslangicNoktalari[14].X + "',eklenenCevapAlani9Y='" + baslangicNoktalari[14].Y + "',eklenenCevapAlani10X='" + baslangicNoktalari[15].X + "',eklenenCevapAlani10Y='" + baslangicNoktalari[15].Y + "'" +
                ",grilikesigi='" + grilikesigi + "', parlaklikesigi='" + parlaklikesigi + "', sikokumahassasiyeti='" + sikokumahassasiyeti + "', widhtForm='" + widhtForm + "', heightForm='" + heightForm + "' WHERE formadi='" + formadi + "'";
            komutFormBilgileri.ExecuteNonQuery();
            komutFormBilgileri.Dispose();
            baglantiFormBilgileri.Close();
        }

        public void kaydetTasarlananFormu(String formadi,String okulturu, List<Point> standartAlanlarBaslangicNoktalari, List<Point> cevapAlanlariBaslangicNoktalari, List<int> sorusayilari, List<String> dersAdlari)
        {
            komutFormBilgileri.Connection = baglantiFormBilgileri;
            komutFormBilgileri.CommandText = "INSERT INTO table_tasarlananformlar " +
                "(formadi,okulturu,adisoyadiX,adisoyadiY,ogrencinoX,ogrencinoY,sinifsubeX,sinifsubeY,kitapcikturuX,kitapcikturuY,okulkoduX,okulkoduY,baslikX,baslikY" +
                ",eklenenCevapAlani1X,eklenenCevapAlani1Y,eklenenCevapAlani2X,eklenenCevapAlani2Y,eklenenCevapAlani3X,eklenenCevapAlani3Y,eklenenCevapAlani4X,eklenenCevapAlani4Y,eklenenCevapAlani5X,eklenenCevapAlani5Y" +
                ",eklenenCevapAlani6X,eklenenCevapAlani6Y,eklenenCevapAlani7X,eklenenCevapAlani7Y,eklenenCevapAlani8X,eklenenCevapAlani8Y,eklenenCevapAlani9X,eklenenCevapAlani9Y,eklenenCevapAlani10X,eklenenCevapAlani10Y," +
                "sorusayisi1,sorusayisi2,sorusayisi3,sorusayisi4,sorusayisi5,sorusayisi6,sorusayisi7,sorusayisi8,sorusayisi9,sorusayisi10," +
                "dersadi1,dersadi2,dersadi3,dersadi4,dersadi5,dersadi6,dersadi7,dersadi8,dersadi9,dersadi10,grilikesigi,parlaklikesigi,sikokumahassasiyeti,widhtForm,heightForm) " +
                "VALUES ('" + formadi + "','"+okulturu+"','" + standartAlanlarBaslangicNoktalari[0].X + "','" + standartAlanlarBaslangicNoktalari[0].Y +
                "','" + standartAlanlarBaslangicNoktalari[1].X + "','" + standartAlanlarBaslangicNoktalari[1].Y +
                "','" + standartAlanlarBaslangicNoktalari[2].X + "','" + standartAlanlarBaslangicNoktalari[2].Y +
                "','" + standartAlanlarBaslangicNoktalari[3].X + "','" + standartAlanlarBaslangicNoktalari[3].Y +
                "','" + standartAlanlarBaslangicNoktalari[4].X + "','" + standartAlanlarBaslangicNoktalari[4].Y +
                "','" + standartAlanlarBaslangicNoktalari[5].X + "','" + standartAlanlarBaslangicNoktalari[5].Y +
                "','" + cevapAlanlariBaslangicNoktalari[0].X + "','" + cevapAlanlariBaslangicNoktalari[0].Y +
                "','" + cevapAlanlariBaslangicNoktalari[1].X + "','" + cevapAlanlariBaslangicNoktalari[1].Y +
                "','" + cevapAlanlariBaslangicNoktalari[2].X + "','" + cevapAlanlariBaslangicNoktalari[2].Y +
                "','" + cevapAlanlariBaslangicNoktalari[3].X + "','" + cevapAlanlariBaslangicNoktalari[3].Y +
                "','" + cevapAlanlariBaslangicNoktalari[4].X + "','" + cevapAlanlariBaslangicNoktalari[4].Y +
                "','" + cevapAlanlariBaslangicNoktalari[5].X + "','" + cevapAlanlariBaslangicNoktalari[5].Y +
                "','" + cevapAlanlariBaslangicNoktalari[6].X + "','" + cevapAlanlariBaslangicNoktalari[6].Y +
                "','" + cevapAlanlariBaslangicNoktalari[7].X + "','" + cevapAlanlariBaslangicNoktalari[7].Y +
                "','" + cevapAlanlariBaslangicNoktalari[8].X + "','" + cevapAlanlariBaslangicNoktalari[8].Y +
                "','" + cevapAlanlariBaslangicNoktalari[9].X + "','" + cevapAlanlariBaslangicNoktalari[9].Y +
                "','" + sorusayilari[0] + "','" + sorusayilari[1] + "','" + sorusayilari[2] + "','" + sorusayilari[3] + "','" + sorusayilari[4] + "','" + sorusayilari[5] +
                "','" + sorusayilari[6] + "','" + sorusayilari[7] + "','" + sorusayilari[8] + "','" + sorusayilari[9] +
                "','" + dersAdlari[0]+ "','" + dersAdlari[1] + "','" + dersAdlari[2] + "','" + dersAdlari[3] + "','" + dersAdlari[4] + "','" + dersAdlari[5] +
                "','" + dersAdlari[6] + "','" + dersAdlari[7] + "','" + dersAdlari[8] + "','" + dersAdlari[9] +"',195,60,15,523,742)";
            komutFormBilgileri.ExecuteNonQuery();
            komutFormBilgileri.Dispose();
            baglantiFormBilgileri.Close();
        }

        public List<String> kayitliFormlarınIsimleriniGetir() {
            List<String> kayıtliFormAdlari = new List<string>();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT formadi FROM table_tasarlananformlar ORDER BY formadi ASC", baglantiFormBilgileri);
            DataTable tablo = new DataTable();
            adapter.Fill(tablo);
            for (int i = 0; i < tablo.Rows.Count; i++)
            {
                kayıtliFormAdlari.Add(tablo.Rows[i][0].ToString());
            }
            adapter.Dispose();
            tablo.Dispose();
            return kayıtliFormAdlari;
        }

        public List<Point> formBaslangicNoktalariniGetir(String formadi)
        {
            List<Point> baslangicNoktalari = new List<Point>();

            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT adisoyadiX,adisoyadiY,ogrencinoX,ogrencinoY,sinifsubeX,sinifsubeY,kitapcikturuX,kitapcikturuY,okulkoduX,okulkoduY,baslikX,baslikY" +
                ",eklenenCevapAlani1X,eklenenCevapAlani1Y,eklenenCevapAlani2X,eklenenCevapAlani2Y,eklenenCevapAlani3X,eklenenCevapAlani3Y,eklenenCevapAlani4X,eklenenCevapAlani4Y,eklenenCevapAlani5X,eklenenCevapAlani5Y" +
                ",eklenenCevapAlani6X,eklenenCevapAlani6Y,eklenenCevapAlani7X,eklenenCevapAlani7Y,eklenenCevapAlani8X,eklenenCevapAlani8Y,eklenenCevapAlani9X,eklenenCevapAlani9Y,eklenenCevapAlani10X,eklenenCevapAlani10Y FROM table_tasarlananformlar WHERE formadi = '" + formadi+"'", baglantiFormBilgileri);
            DataTable tablo = new DataTable();
            adapter.Fill(tablo);

            Point adsoyadBaslangic = new Point(Convert.ToInt32(tablo.Rows[0][0]),Convert.ToInt32(tablo.Rows[0][1]));
            Point ogrenciNoBaslangic = new Point(Convert.ToInt32(tablo.Rows[0][2]), Convert.ToInt32(tablo.Rows[0][3]));
            Point sinifSubeBaslangic = new Point(Convert.ToInt32(tablo.Rows[0][4]), Convert.ToInt32(tablo.Rows[0][5]));
            Point kitapcikTuruBaslangic = new Point(Convert.ToInt32(tablo.Rows[0][6]), Convert.ToInt32(tablo.Rows[0][7]));
            Point okulKoduBaslangic = new Point(Convert.ToInt32(tablo.Rows[0][8]), Convert.ToInt32(tablo.Rows[0][9]));
            Point baslikBaslangic = new Point(Convert.ToInt32(tablo.Rows[0][10]), Convert.ToInt32(tablo.Rows[0][11]));
            Point ders1Baslangic = new Point(Convert.ToInt32(tablo.Rows[0][12]), Convert.ToInt32(tablo.Rows[0][13]));
            Point ders2Baslangic = new Point(Convert.ToInt32(tablo.Rows[0][14]), Convert.ToInt32(tablo.Rows[0][15]));
            Point ders3Baslangic = new Point(Convert.ToInt32(tablo.Rows[0][16]), Convert.ToInt32(tablo.Rows[0][17]));
            Point ders4Baslangic = new Point(Convert.ToInt32(tablo.Rows[0][18]), Convert.ToInt32(tablo.Rows[0][19]));
            Point ders5Baslangic = new Point(Convert.ToInt32(tablo.Rows[0][20]), Convert.ToInt32(tablo.Rows[0][21]));
            Point ders6Baslangic = new Point(Convert.ToInt32(tablo.Rows[0][22]), Convert.ToInt32(tablo.Rows[0][23]));
            Point ders7Baslangic = new Point(Convert.ToInt32(tablo.Rows[0][24]), Convert.ToInt32(tablo.Rows[0][25]));
            Point ders8Baslangic = new Point(Convert.ToInt32(tablo.Rows[0][26]), Convert.ToInt32(tablo.Rows[0][27]));
            Point ders9Baslangic = new Point(Convert.ToInt32(tablo.Rows[0][28]), Convert.ToInt32(tablo.Rows[0][29]));
            Point ders10Baslangic = new Point(Convert.ToInt32(tablo.Rows[0][30]), Convert.ToInt32(tablo.Rows[0][31]));

            baslangicNoktalari.Add(adsoyadBaslangic);
            baslangicNoktalari.Add(ogrenciNoBaslangic);
            baslangicNoktalari.Add(sinifSubeBaslangic);
            baslangicNoktalari.Add(kitapcikTuruBaslangic);
            baslangicNoktalari.Add(okulKoduBaslangic);
            baslangicNoktalari.Add(baslikBaslangic);
            baslangicNoktalari.Add(ders1Baslangic);
            baslangicNoktalari.Add(ders2Baslangic);
            baslangicNoktalari.Add(ders3Baslangic);
            baslangicNoktalari.Add(ders4Baslangic);
            baslangicNoktalari.Add(ders5Baslangic);
            baslangicNoktalari.Add(ders6Baslangic);
            baslangicNoktalari.Add(ders7Baslangic);
            baslangicNoktalari.Add(ders8Baslangic);
            baslangicNoktalari.Add(ders9Baslangic);
            baslangicNoktalari.Add(ders10Baslangic);

            adapter.Dispose();
            tablo.Dispose();
            return baslangicNoktalari;
        }

        public List<int> soruSayılariniGetir(String formadi)
        {
            List<int> sorusayilari = new List<int>();

            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT sorusayisi1,sorusayisi2,sorusayisi3,sorusayisi4,sorusayisi5,sorusayisi6,sorusayisi7,sorusayisi8,sorusayisi9,sorusayisi10 FROM table_tasarlananformlar WHERE formadi = '" + formadi+"'", baglantiFormBilgileri);
            DataTable tablo = new DataTable();
            adapter.Fill(tablo);

            for (int i =0; i <10; i++)
            {
                sorusayilari.Add(Convert.ToInt32(tablo.Rows[0][i]));
            }
            adapter.Dispose();
            tablo.Dispose();
            return sorusayilari;
        }

        public List<string> dersAdlariniGetir(String formadi)
        {
            List<string> dersAdlari = new List<string>();

            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT dersadi1,dersadi2,dersadi3,dersadi4,dersadi5,dersadi6,dersadi7,dersadi8,dersadi9,dersadi10 FROM table_tasarlananformlar WHERE formadi = '" + formadi+"'", baglantiFormBilgileri);
            DataTable tablo = new DataTable();
            adapter.Fill(tablo);

            for (int i = 0; i <10; i++)
            {
                dersAdlari.Add(tablo.Rows[0][i].ToString());
            }
            adapter.Dispose();
            tablo.Dispose();
            return dersAdlari;
        }

        public String okulTurunuGetir(String formadi)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT okulturu FROM table_tasarlananformlar WHERE formadi = '" + formadi + "'", baglantiFormBilgileri);
            DataTable tablo = new DataTable();
            adapter.Fill(tablo);
            String okulturu = tablo.Rows[0][0].ToString();
            adapter.Dispose();
            tablo.Dispose();
            return okulturu;
        }

        public void formuSil(string v)
        {
            komutFormBilgileri.Connection = baglantiFormBilgileri;
            komutFormBilgileri.CommandText = "DELETE FROM table_tasarlananformlar WHERE formadi = '"+v+"'";
            komutFormBilgileri.ExecuteNonQuery();
            komutFormBilgileri.Dispose();
            baglantiFormBilgileri.Close();
        }

        public void kaydetCevapAnahtariA(String cevaplar) {
            komutCevapAnahtariA.Connection = baglantiCevapAnahtariA;
            komutCevapAnahtariA.CommandText = "INSERT INTO table_cevap_anahtariA (cevaplar) VALUES('"+cevaplar+"')";
            komutCevapAnahtariA.ExecuteNonQuery();
            komutCevapAnahtariA.Dispose();
            baglantiCevapAnahtariA.Close();
        }

        public List<String> getirCevapAnahtariA(){
            List<string> cevaplar = new List<string>();

            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT cevaplar FROM table_cevap_anahtariA", baglantiCevapAnahtariA);
            DataTable tablo = new DataTable();
            adapter.Fill(tablo);
            if (tablo != null)
            {
                 for (int i = 0; i < tablo.Rows.Count; i++)
                {
                    cevaplar.Add(tablo.Rows[i][0].ToString());
                }
            }
            
            adapter.Dispose();
            tablo.Dispose();
            return cevaplar;
        }

        public void silCevapAnahtariA()
        {
            komutCevapAnahtariA.Connection = baglantiCevapAnahtariA;
            komutCevapAnahtariA.CommandText = "DELETE FROM table_cevap_anahtariA";
            komutCevapAnahtariA.ExecuteNonQuery();
            komutCevapAnahtariA.Dispose();
            baglantiCevapAnahtariA.Close();
        }
    }
}
