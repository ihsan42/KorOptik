namespace KorOptik_v1
{
    partial class Raporlar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxRaporSınavAdi = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonRaporHesaplanmasin = new System.Windows.Forms.RadioButton();
            this.radioButtonRaporHesaplansin = new System.Windows.Forms.RadioButton();
            this.labelRaporSinavTuru = new System.Windows.Forms.Label();
            this.comboBoxRaporSinavTuru = new System.Windows.Forms.ComboBox();
            this.buttonTumRaporlariYazdir = new System.Windows.Forms.Button();
            this.buttonTumRaporlariIndir = new System.Windows.Forms.Button();
            this.buttonKarneYazdirIndır = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sınav Seç";
            // 
            // comboBoxRaporSınavAdi
            // 
            this.comboBoxRaporSınavAdi.FormattingEnabled = true;
            this.comboBoxRaporSınavAdi.Location = new System.Drawing.Point(10, 81);
            this.comboBoxRaporSınavAdi.Name = "comboBoxRaporSınavAdi";
            this.comboBoxRaporSınavAdi.Size = new System.Drawing.Size(177, 21);
            this.comboBoxRaporSınavAdi.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Puan hesaplansın mı?";
            // 
            // radioButtonRaporHesaplanmasin
            // 
            this.radioButtonRaporHesaplanmasin.AutoSize = true;
            this.radioButtonRaporHesaplanmasin.Checked = true;
            this.radioButtonRaporHesaplanmasin.Location = new System.Drawing.Point(110, 144);
            this.radioButtonRaporHesaplanmasin.Name = "radioButtonRaporHesaplanmasin";
            this.radioButtonRaporHesaplanmasin.Size = new System.Drawing.Size(97, 17);
            this.radioButtonRaporHesaplanmasin.TabIndex = 3;
            this.radioButtonRaporHesaplanmasin.TabStop = true;
            this.radioButtonRaporHesaplanmasin.Text = "Hesaplanmasın";
            this.radioButtonRaporHesaplanmasin.UseVisualStyleBackColor = true;
            this.radioButtonRaporHesaplanmasin.CheckedChanged += new System.EventHandler(this.RadioButtonRaporHesaplanmasin_CheckedChanged);
            // 
            // radioButtonRaporHesaplansin
            // 
            this.radioButtonRaporHesaplansin.AutoSize = true;
            this.radioButtonRaporHesaplansin.Location = new System.Drawing.Point(10, 144);
            this.radioButtonRaporHesaplansin.Name = "radioButtonRaporHesaplansin";
            this.radioButtonRaporHesaplansin.Size = new System.Drawing.Size(83, 17);
            this.radioButtonRaporHesaplansin.TabIndex = 4;
            this.radioButtonRaporHesaplansin.Text = "Hesaplansın";
            this.radioButtonRaporHesaplansin.UseVisualStyleBackColor = true;
            this.radioButtonRaporHesaplansin.CheckedChanged += new System.EventHandler(this.RadioButtonRaporHesaplansin_CheckedChanged);
            // 
            // labelRaporSinavTuru
            // 
            this.labelRaporSinavTuru.AutoSize = true;
            this.labelRaporSinavTuru.Location = new System.Drawing.Point(10, 176);
            this.labelRaporSinavTuru.Name = "labelRaporSinavTuru";
            this.labelRaporSinavTuru.Size = new System.Drawing.Size(59, 13);
            this.labelRaporSinavTuru.TabIndex = 5;
            this.labelRaporSinavTuru.Text = "Sınav Türü";
            this.labelRaporSinavTuru.Visible = false;
            // 
            // comboBoxRaporSinavTuru
            // 
            this.comboBoxRaporSinavTuru.FormattingEnabled = true;
            this.comboBoxRaporSinavTuru.Items.AddRange(new object[] {
            "İOKBS(Bursluluk)",
            "LGS",
            "YKS(Sadece TYT puanı)",
            "YKS(SAY, SÖZ, EA)"});
            this.comboBoxRaporSinavTuru.Location = new System.Drawing.Point(13, 192);
            this.comboBoxRaporSinavTuru.Name = "comboBoxRaporSinavTuru";
            this.comboBoxRaporSinavTuru.Size = new System.Drawing.Size(174, 21);
            this.comboBoxRaporSinavTuru.TabIndex = 6;
            this.comboBoxRaporSinavTuru.Visible = false;
            // 
            // buttonTumRaporlariYazdir
            // 
            this.buttonTumRaporlariYazdir.Location = new System.Drawing.Point(224, 79);
            this.buttonTumRaporlariYazdir.Name = "buttonTumRaporlariYazdir";
            this.buttonTumRaporlariYazdir.Size = new System.Drawing.Size(121, 23);
            this.buttonTumRaporlariYazdir.TabIndex = 10;
            this.buttonTumRaporlariYazdir.Text = "Tüm Raporları Yazdır";
            this.buttonTumRaporlariYazdir.UseVisualStyleBackColor = true;
            // 
            // buttonTumRaporlariIndir
            // 
            this.buttonTumRaporlariIndir.Location = new System.Drawing.Point(224, 118);
            this.buttonTumRaporlariIndir.Name = "buttonTumRaporlariIndir";
            this.buttonTumRaporlariIndir.Size = new System.Drawing.Size(121, 23);
            this.buttonTumRaporlariIndir.TabIndex = 11;
            this.buttonTumRaporlariIndir.Text = "Tüm Raporları İndir";
            this.buttonTumRaporlariIndir.UseVisualStyleBackColor = true;
            this.buttonTumRaporlariIndir.Click += new System.EventHandler(this.ButtonTumRaporlariIndir_Click);
            // 
            // buttonKarneYazdirIndır
            // 
            this.buttonKarneYazdirIndır.Location = new System.Drawing.Point(224, 164);
            this.buttonKarneYazdirIndır.Name = "buttonKarneYazdirIndır";
            this.buttonKarneYazdirIndır.Size = new System.Drawing.Size(121, 36);
            this.buttonKarneYazdirIndır.TabIndex = 12;
            this.buttonKarneYazdirIndır.Text = "Sınav Sonuç Karnesi İndir/Yazdır";
            this.buttonKarneYazdirIndır.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Sınav Adı";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(171, 20);
            this.textBox1.TabIndex = 14;
            // 
            // Raporlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(483, 256);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonKarneYazdirIndır);
            this.Controls.Add(this.buttonTumRaporlariIndir);
            this.Controls.Add(this.buttonTumRaporlariYazdir);
            this.Controls.Add(this.comboBoxRaporSinavTuru);
            this.Controls.Add(this.labelRaporSinavTuru);
            this.Controls.Add(this.radioButtonRaporHesaplansin);
            this.Controls.Add(this.radioButtonRaporHesaplanmasin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxRaporSınavAdi);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Raporlar";
            this.Text = "Raporlar";
            this.Load += new System.EventHandler(this.Raporlar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxRaporSınavAdi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonRaporHesaplanmasin;
        private System.Windows.Forms.RadioButton radioButtonRaporHesaplansin;
        private System.Windows.Forms.Label labelRaporSinavTuru;
        private System.Windows.Forms.ComboBox comboBoxRaporSinavTuru;
        private System.Windows.Forms.Button buttonTumRaporlariYazdir;
        private System.Windows.Forms.Button buttonTumRaporlariIndir;
        private System.Windows.Forms.Button buttonKarneYazdirIndır;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
    }
}