using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KorOptik_v1
{
    public partial class SinifSeviyeleri : Form
    {
        public SinifSeviyeleri()
        {
            InitializeComponent();
        }
        public int sinifsatirsayisi;

        private void SinifSeviyeleri_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < sinifsatirsayisi; i++)
            {
                dataGridView1.Columns.Add("Column" + i.ToString(), (i + 1).ToString());
                dataGridView1.Columns[i].Width = 25;
            }
            dataGridView1.Rows.Add();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            List<string> seviyeler = new List<string>();
            for (int j = 0; j < dataGridView1.RowCount; j++)
            {
                for (int i = 0; i < dataGridView1.Rows[j].Cells.Count; i++)
                {                 
                    if (dataGridView1.Rows[j].Cells[i].Value == null)
                    {
                    }
                    else
                    {
                        string seviye = dataGridView1.Rows[j].Cells[i].Value.ToString();
                        if (!seviye.Equals("0") || !seviye.Equals("1") || !seviye.Equals("2") || !seviye.Equals("3") || !seviye.Equals("4") || !seviye.Equals("5") || !seviye.Equals("6") || !seviye.Equals("7") || !seviye.Equals("8") || !seviye.Equals("9") || !seviye.Equals("10") || !seviye.Equals("11") || !seviye.Equals("12"))
                        {
                            label2.Text = "Lütfen sınıf seviyelerini sayı olarak giriniz ve 12'den büyük sayı girmeyiniz";
                        }
                        else
                        {
                            seviyeler.Add(dataGridView1.Rows[j].Cells[i].Value.ToString());
                        }
                    }
                }
            }

            TextBox textBox = new TextBox();
            foreach (string seviye in seviyeler)
            {              
                    textBox.Text += seviye;                
            }

            if (seviyeler.Count() <dataGridView1.ColumnCount)
            {
                label3.Text = "Lütfen tabloyu eksiksiz doldurunuz!";
            }
            else {
                FormTanit formTanit = (FormTanit)Application.OpenForms["FormTanit"];
                formTanit.sinifseviyeleri = textBox.Text;
                this.Close();
            }
           
        }

    }
}
