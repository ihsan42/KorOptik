namespace KorOptik_v1
{
    partial class FormTasarla
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
            this.components = new System.ComponentModel.Container();
            this.comboBoxOkulTuru = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFormAdi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxAdSoyad = new System.Windows.Forms.CheckBox();
            this.checkBoxOgrNo = new System.Windows.Forms.CheckBox();
            this.checkBoxSinif = new System.Windows.Forms.CheckBox();
            this.checkBoxKitTuru = new System.Windows.Forms.CheckBox();
            this.checkBoxOkulKodu = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDersAdi = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSoruSayisi = new System.Windows.Forms.TextBox();
            this.buttonEkle = new System.Windows.Forms.Button();
            this.listViewEklenenAlanlar = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_EklenenAlanlar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonFormuGoruntule = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.comboBox_TasınacakAlanlar = new System.Windows.Forms.ComboBox();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonRight = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonUpFastly = new System.Windows.Forms.Button();
            this.buttonDownFastly = new System.Windows.Forms.Button();
            this.buttonRightFastly = new System.Windows.Forms.Button();
            this.buttonLeftFastly = new System.Windows.Forms.Button();
            this.checkBoxBaslik = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonKaydet = new System.Windows.Forms.Button();
            this.contextMenuStrip_EklenenAlanlar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxOkulTuru
            // 
            this.comboBoxOkulTuru.FormattingEnabled = true;
            this.comboBoxOkulTuru.Items.AddRange(new object[] {
            "İlkokul",
            "Ortaokul",
            "Lise"});
            this.comboBoxOkulTuru.Location = new System.Drawing.Point(12, 71);
            this.comboBoxOkulTuru.Name = "comboBoxOkulTuru";
            this.comboBoxOkulTuru.Size = new System.Drawing.Size(121, 21);
            this.comboBoxOkulTuru.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Okul Türü";
            // 
            // textBoxFormAdi
            // 
            this.textBoxFormAdi.Location = new System.Drawing.Point(12, 32);
            this.textBoxFormAdi.Name = "textBoxFormAdi";
            this.textBoxFormAdi.Size = new System.Drawing.Size(121, 20);
            this.textBoxFormAdi.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Form Adı";
            // 
            // checkBoxAdSoyad
            // 
            this.checkBoxAdSoyad.AutoSize = true;
            this.checkBoxAdSoyad.Checked = true;
            this.checkBoxAdSoyad.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAdSoyad.Location = new System.Drawing.Point(215, 35);
            this.checkBoxAdSoyad.Name = "checkBoxAdSoyad";
            this.checkBoxAdSoyad.Size = new System.Drawing.Size(72, 17);
            this.checkBoxAdSoyad.TabIndex = 4;
            this.checkBoxAdSoyad.Text = "Ad-Soyad";
            this.checkBoxAdSoyad.UseVisualStyleBackColor = true;
            // 
            // checkBoxOgrNo
            // 
            this.checkBoxOgrNo.AutoSize = true;
            this.checkBoxOgrNo.Checked = true;
            this.checkBoxOgrNo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOgrNo.Location = new System.Drawing.Point(215, 58);
            this.checkBoxOgrNo.Name = "checkBoxOgrNo";
            this.checkBoxOgrNo.Size = new System.Drawing.Size(80, 17);
            this.checkBoxOgrNo.TabIndex = 5;
            this.checkBoxOgrNo.Text = "Öğrenci No";
            this.checkBoxOgrNo.UseVisualStyleBackColor = true;
            // 
            // checkBoxSinif
            // 
            this.checkBoxSinif.AutoSize = true;
            this.checkBoxSinif.Checked = true;
            this.checkBoxSinif.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSinif.Location = new System.Drawing.Point(215, 81);
            this.checkBoxSinif.Name = "checkBoxSinif";
            this.checkBoxSinif.Size = new System.Drawing.Size(74, 17);
            this.checkBoxSinif.TabIndex = 6;
            this.checkBoxSinif.Text = "Sınıf-Şube";
            this.checkBoxSinif.UseVisualStyleBackColor = true;
            // 
            // checkBoxKitTuru
            // 
            this.checkBoxKitTuru.AutoSize = true;
            this.checkBoxKitTuru.Checked = true;
            this.checkBoxKitTuru.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKitTuru.Location = new System.Drawing.Point(301, 35);
            this.checkBoxKitTuru.Name = "checkBoxKitTuru";
            this.checkBoxKitTuru.Size = new System.Drawing.Size(89, 17);
            this.checkBoxKitTuru.TabIndex = 8;
            this.checkBoxKitTuru.Text = "Kitapçık Türü";
            this.checkBoxKitTuru.UseVisualStyleBackColor = true;
            // 
            // checkBoxOkulKodu
            // 
            this.checkBoxOkulKodu.AutoSize = true;
            this.checkBoxOkulKodu.Checked = true;
            this.checkBoxOkulKodu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOkulKodu.Location = new System.Drawing.Point(301, 58);
            this.checkBoxOkulKodu.Name = "checkBoxOkulKodu";
            this.checkBoxOkulKodu.Size = new System.Drawing.Size(76, 17);
            this.checkBoxOkulKodu.TabIndex = 9;
            this.checkBoxOkulKodu.Text = "Okul Kodu";
            this.checkBoxOkulKodu.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(229, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Standart Alanlar";
            // 
            // textBoxDersAdi
            // 
            this.textBoxDersAdi.Location = new System.Drawing.Point(537, 48);
            this.textBoxDersAdi.Name = "textBoxDersAdi";
            this.textBoxDersAdi.Size = new System.Drawing.Size(100, 20);
            this.textBoxDersAdi.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(534, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Ders Adı";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(533, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Cevap Alanı Ekleme";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(652, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Soru Sayısı";
            // 
            // textBoxSoruSayisi
            // 
            this.textBoxSoruSayisi.Location = new System.Drawing.Point(655, 48);
            this.textBoxSoruSayisi.Name = "textBoxSoruSayisi";
            this.textBoxSoruSayisi.Size = new System.Drawing.Size(100, 20);
            this.textBoxSoruSayisi.TabIndex = 17;
            this.textBoxSoruSayisi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSoruSayisi_KeyPress);
            // 
            // buttonEkle
            // 
            this.buttonEkle.Location = new System.Drawing.Point(608, 77);
            this.buttonEkle.Name = "buttonEkle";
            this.buttonEkle.Size = new System.Drawing.Size(75, 23);
            this.buttonEkle.TabIndex = 19;
            this.buttonEkle.Text = "Ekle";
            this.buttonEkle.UseVisualStyleBackColor = true;
            this.buttonEkle.Click += new System.EventHandler(this.buttonEkle_Click);
            // 
            // listViewEklenenAlanlar
            // 
            this.listViewEklenenAlanlar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewEklenenAlanlar.ContextMenuStrip = this.contextMenuStrip_EklenenAlanlar;
            this.listViewEklenenAlanlar.Location = new System.Drawing.Point(845, 28);
            this.listViewEklenenAlanlar.Name = "listViewEklenenAlanlar";
            this.listViewEklenenAlanlar.Size = new System.Drawing.Size(217, 97);
            this.listViewEklenenAlanlar.TabIndex = 21;
            this.listViewEklenenAlanlar.UseCompatibleStateImageBehavior = false;
            this.listViewEklenenAlanlar.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Ders Adı";
            this.columnHeader1.Width = 117;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Soru Sayısı";
            this.columnHeader2.Width = 126;
            // 
            // contextMenuStrip_EklenenAlanlar
            // 
            this.contextMenuStrip_EklenenAlanlar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip_EklenenAlanlar.Name = "contextMenuStrip_EklenenAlanlar";
            this.contextMenuStrip_EklenenAlanlar.Size = new System.Drawing.Size(87, 26);
            this.contextMenuStrip_EklenenAlanlar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_EklenenAlanlar_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(86, 22);
            this.toolStripMenuItem1.Text = "Sil";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(854, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(194, 20);
            this.label7.TabIndex = 22;
            this.label7.Text = "Eklenen Cevap Alanları";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 104);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(671, 949);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // buttonFormuGoruntule
            // 
            this.buttonFormuGoruntule.Location = new System.Drawing.Point(845, 131);
            this.buttonFormuGoruntule.Name = "buttonFormuGoruntule";
            this.buttonFormuGoruntule.Size = new System.Drawing.Size(217, 23);
            this.buttonFormuGoruntule.TabIndex = 24;
            this.buttonFormuGoruntule.Text = "Formu Görüntüle";
            this.buttonFormuGoruntule.UseVisualStyleBackColor = true;
            this.buttonFormuGoruntule.Click += new System.EventHandler(this.buttonFormugoruntule_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(845, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(217, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "Formu Yazdır";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // comboBox_TasınacakAlanlar
            // 
            this.comboBox_TasınacakAlanlar.FormattingEnabled = true;
            this.comboBox_TasınacakAlanlar.Items.AddRange(new object[] {
            "Ad-Soyad",
            "Öğrenci No",
            "Sınıf-Şube",
            "Okul Kodu",
            "Kitapçık Türü",
            "Başlık"});
            this.comboBox_TasınacakAlanlar.Location = new System.Drawing.Point(710, 334);
            this.comboBox_TasınacakAlanlar.Name = "comboBox_TasınacakAlanlar";
            this.comboBox_TasınacakAlanlar.Size = new System.Drawing.Size(240, 21);
            this.comboBox_TasınacakAlanlar.TabIndex = 26;
            // 
            // buttonLeft
            // 
            this.buttonLeft.Location = new System.Drawing.Point(755, 462);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(39, 39);
            this.buttonLeft.TabIndex = 27;
            this.buttonLeft.Text = "<";
            this.buttonLeft.UseVisualStyleBackColor = true;
            this.buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // buttonRight
            // 
            this.buttonRight.Location = new System.Drawing.Point(845, 463);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(39, 39);
            this.buttonRight.TabIndex = 28;
            this.buttonRight.Text = ">";
            this.buttonRight.UseVisualStyleBackColor = true;
            this.buttonRight.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.Location = new System.Drawing.Point(801, 501);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(39, 39);
            this.buttonDown.TabIndex = 29;
            this.buttonDown.Text = "\\/";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Location = new System.Drawing.Point(800, 425);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(39, 39);
            this.buttonUp.TabIndex = 30;
            this.buttonUp.Text = "/\\";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonUpFastly
            // 
            this.buttonUpFastly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonUpFastly.Location = new System.Drawing.Point(800, 380);
            this.buttonUpFastly.Name = "buttonUpFastly";
            this.buttonUpFastly.Size = new System.Drawing.Size(39, 39);
            this.buttonUpFastly.TabIndex = 34;
            this.buttonUpFastly.Text = "//\\\\";
            this.buttonUpFastly.UseVisualStyleBackColor = true;
            this.buttonUpFastly.Click += new System.EventHandler(this.buttonUpFastly_Click);
            // 
            // buttonDownFastly
            // 
            this.buttonDownFastly.Location = new System.Drawing.Point(800, 546);
            this.buttonDownFastly.Name = "buttonDownFastly";
            this.buttonDownFastly.Size = new System.Drawing.Size(39, 39);
            this.buttonDownFastly.TabIndex = 33;
            this.buttonDownFastly.Text = "\\\\//";
            this.buttonDownFastly.UseVisualStyleBackColor = true;
            this.buttonDownFastly.Click += new System.EventHandler(this.buttonDownFastly_Click);
            // 
            // buttonRightFastly
            // 
            this.buttonRightFastly.Location = new System.Drawing.Point(890, 462);
            this.buttonRightFastly.Name = "buttonRightFastly";
            this.buttonRightFastly.Size = new System.Drawing.Size(39, 39);
            this.buttonRightFastly.TabIndex = 32;
            this.buttonRightFastly.Text = ">>";
            this.buttonRightFastly.UseVisualStyleBackColor = true;
            this.buttonRightFastly.Click += new System.EventHandler(this.buttonRightFastly_Click);
            // 
            // buttonLeftFastly
            // 
            this.buttonLeftFastly.Location = new System.Drawing.Point(710, 463);
            this.buttonLeftFastly.Name = "buttonLeftFastly";
            this.buttonLeftFastly.Size = new System.Drawing.Size(39, 39);
            this.buttonLeftFastly.TabIndex = 31;
            this.buttonLeftFastly.Text = "<<";
            this.buttonLeftFastly.UseVisualStyleBackColor = true;
            this.buttonLeftFastly.Click += new System.EventHandler(this.buttonLeftFastly_Click);
            // 
            // checkBoxBaslik
            // 
            this.checkBoxBaslik.AutoSize = true;
            this.checkBoxBaslik.Checked = true;
            this.checkBoxBaslik.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBaslik.Location = new System.Drawing.Point(301, 81);
            this.checkBoxBaslik.Name = "checkBoxBaslik";
            this.checkBoxBaslik.Size = new System.Drawing.Size(54, 17);
            this.checkBoxBaslik.TabIndex = 35;
            this.checkBoxBaslik.Text = "Başlık";
            this.checkBoxBaslik.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(706, 311);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(244, 20);
            this.label8.TabIndex = 36;
            this.label8.Text = "Taşıyacağınız bölümü seçiniz!";
            // 
            // buttonKaydet
            // 
            this.buttonKaydet.Location = new System.Drawing.Point(845, 160);
            this.buttonKaydet.Name = "buttonKaydet";
            this.buttonKaydet.Size = new System.Drawing.Size(217, 23);
            this.buttonKaydet.TabIndex = 37;
            this.buttonKaydet.Text = "Formu Kaydet";
            this.buttonKaydet.UseVisualStyleBackColor = true;
            this.buttonKaydet.Click += new System.EventHandler(this.buttonKaydet_Click);
            // 
            // FormTasarla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1100, 595);
            this.Controls.Add(this.buttonKaydet);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.checkBoxBaslik);
            this.Controls.Add(this.buttonUpFastly);
            this.Controls.Add(this.buttonDownFastly);
            this.Controls.Add(this.buttonRightFastly);
            this.Controls.Add(this.buttonLeftFastly);
            this.Controls.Add(this.buttonUp);
            this.Controls.Add(this.buttonDown);
            this.Controls.Add(this.buttonRight);
            this.Controls.Add(this.buttonLeft);
            this.Controls.Add(this.comboBox_TasınacakAlanlar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonFormuGoruntule);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listViewEklenenAlanlar);
            this.Controls.Add(this.buttonEkle);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxSoruSayisi);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxDersAdi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxOkulKodu);
            this.Controls.Add(this.checkBoxKitTuru);
            this.Controls.Add(this.checkBoxSinif);
            this.Controls.Add(this.checkBoxOgrNo);
            this.Controls.Add(this.checkBoxAdSoyad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxFormAdi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxOkulTuru);
            this.Name = "FormTasarla";
            this.Text = "FormTasarla";
            this.contextMenuStrip_EklenenAlanlar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxOkulTuru;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFormAdi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxAdSoyad;
        private System.Windows.Forms.CheckBox checkBoxOgrNo;
        private System.Windows.Forms.CheckBox checkBoxSinif;
        private System.Windows.Forms.CheckBox checkBoxKitTuru;
        private System.Windows.Forms.CheckBox checkBoxOkulKodu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDersAdi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxSoruSayisi;
        private System.Windows.Forms.Button buttonEkle;
        private System.Windows.Forms.ListView listViewEklenenAlanlar;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_EklenenAlanlar;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonFormuGoruntule;
        private System.Windows.Forms.Button button2;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ComboBox comboBox_TasınacakAlanlar;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Button buttonUpFastly;
        private System.Windows.Forms.Button buttonDownFastly;
        private System.Windows.Forms.Button buttonRightFastly;
        private System.Windows.Forms.Button buttonLeftFastly;
        private System.Windows.Forms.CheckBox checkBoxBaslik;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonKaydet;
    }
}