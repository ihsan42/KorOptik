namespace KorOptik_v1
{
    partial class OturumBirlestir
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
            this.comboBoxOturumSinavTuru = new System.Windows.Forms.ComboBox();
            this.comboBoxOturum1 = new System.Windows.Forms.ComboBox();
            this.labelOturum1 = new System.Windows.Forms.Label();
            this.comboBoxOturum2 = new System.Windows.Forms.ComboBox();
            this.labelOturum2 = new System.Windows.Forms.Label();
            this.button_3_OturumEkle = new System.Windows.Forms.Button();
            this.button_4_OturumEkle = new System.Windows.Forms.Button();
            this.comboBoxOturum3 = new System.Windows.Forms.ComboBox();
            this.labelOturum3 = new System.Windows.Forms.Label();
            this.button_5_OturumEkle = new System.Windows.Forms.Button();
            this.comboBoxOturum4 = new System.Windows.Forms.ComboBox();
            this.labelOturum4 = new System.Windows.Forms.Label();
            this.comboBoxOturum5 = new System.Windows.Forms.ComboBox();
            this.labelOturum5 = new System.Windows.Forms.Label();
            this.buttonOturumBirlestir = new System.Windows.Forms.Button();
            this.textBoxOturumKayitAdi = new System.Windows.Forms.TextBox();
            this.labelOturumKayitAdi = new System.Windows.Forms.Label();
            this.buttonOturumKaydet = new System.Windows.Forms.Button();
            this.buttonOturumRaporAl = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sınav Türü";
            // 
            // comboBoxOturumSinavTuru
            // 
            this.comboBoxOturumSinavTuru.FormattingEnabled = true;
            this.comboBoxOturumSinavTuru.Items.AddRange(new object[] {
            "LGS",
            "YKS",
            "Diğer"});
            this.comboBoxOturumSinavTuru.Location = new System.Drawing.Point(110, 31);
            this.comboBoxOturumSinavTuru.Name = "comboBoxOturumSinavTuru";
            this.comboBoxOturumSinavTuru.Size = new System.Drawing.Size(123, 21);
            this.comboBoxOturumSinavTuru.TabIndex = 1;
            this.comboBoxOturumSinavTuru.SelectedIndexChanged += new System.EventHandler(this.ComboBoxOturumSinavTuru_SelectedIndexChanged);
            // 
            // comboBoxOturum1
            // 
            this.comboBoxOturum1.FormattingEnabled = true;
            this.comboBoxOturum1.Location = new System.Drawing.Point(110, 70);
            this.comboBoxOturum1.Name = "comboBoxOturum1";
            this.comboBoxOturum1.Size = new System.Drawing.Size(123, 21);
            this.comboBoxOturum1.TabIndex = 3;
            this.comboBoxOturum1.Visible = false;
            // 
            // labelOturum1
            // 
            this.labelOturum1.AutoSize = true;
            this.labelOturum1.Location = new System.Drawing.Point(30, 73);
            this.labelOturum1.Name = "labelOturum1";
            this.labelOturum1.Size = new System.Drawing.Size(53, 13);
            this.labelOturum1.TabIndex = 2;
            this.labelOturum1.Text = "1. Oturum";
            this.labelOturum1.Visible = false;
            // 
            // comboBoxOturum2
            // 
            this.comboBoxOturum2.FormattingEnabled = true;
            this.comboBoxOturum2.Location = new System.Drawing.Point(110, 109);
            this.comboBoxOturum2.Name = "comboBoxOturum2";
            this.comboBoxOturum2.Size = new System.Drawing.Size(123, 21);
            this.comboBoxOturum2.TabIndex = 5;
            this.comboBoxOturum2.Visible = false;
            // 
            // labelOturum2
            // 
            this.labelOturum2.AutoSize = true;
            this.labelOturum2.Location = new System.Drawing.Point(30, 112);
            this.labelOturum2.Name = "labelOturum2";
            this.labelOturum2.Size = new System.Drawing.Size(53, 13);
            this.labelOturum2.TabIndex = 4;
            this.labelOturum2.Text = "2. Oturum";
            this.labelOturum2.Visible = false;
            // 
            // button_3_OturumEkle
            // 
            this.button_3_OturumEkle.Location = new System.Drawing.Point(257, 107);
            this.button_3_OturumEkle.Name = "button_3_OturumEkle";
            this.button_3_OturumEkle.Size = new System.Drawing.Size(44, 23);
            this.button_3_OturumEkle.TabIndex = 6;
            this.button_3_OturumEkle.Text = "+";
            this.button_3_OturumEkle.UseVisualStyleBackColor = true;
            this.button_3_OturumEkle.Visible = false;
            this.button_3_OturumEkle.Click += new System.EventHandler(this.Button_3_OturumEkle_Click);
            // 
            // button_4_OturumEkle
            // 
            this.button_4_OturumEkle.Location = new System.Drawing.Point(257, 143);
            this.button_4_OturumEkle.Name = "button_4_OturumEkle";
            this.button_4_OturumEkle.Size = new System.Drawing.Size(44, 23);
            this.button_4_OturumEkle.TabIndex = 9;
            this.button_4_OturumEkle.Text = "+";
            this.button_4_OturumEkle.UseVisualStyleBackColor = true;
            this.button_4_OturumEkle.Visible = false;
            this.button_4_OturumEkle.Click += new System.EventHandler(this.Button_4_OturumEkle_Click);
            // 
            // comboBoxOturum3
            // 
            this.comboBoxOturum3.FormattingEnabled = true;
            this.comboBoxOturum3.Location = new System.Drawing.Point(110, 145);
            this.comboBoxOturum3.Name = "comboBoxOturum3";
            this.comboBoxOturum3.Size = new System.Drawing.Size(123, 21);
            this.comboBoxOturum3.TabIndex = 8;
            this.comboBoxOturum3.Visible = false;
            // 
            // labelOturum3
            // 
            this.labelOturum3.AutoSize = true;
            this.labelOturum3.Location = new System.Drawing.Point(30, 148);
            this.labelOturum3.Name = "labelOturum3";
            this.labelOturum3.Size = new System.Drawing.Size(53, 13);
            this.labelOturum3.TabIndex = 7;
            this.labelOturum3.Text = "3. Oturum";
            this.labelOturum3.Visible = false;
            // 
            // button_5_OturumEkle
            // 
            this.button_5_OturumEkle.Location = new System.Drawing.Point(257, 180);
            this.button_5_OturumEkle.Name = "button_5_OturumEkle";
            this.button_5_OturumEkle.Size = new System.Drawing.Size(44, 23);
            this.button_5_OturumEkle.TabIndex = 12;
            this.button_5_OturumEkle.Text = "+";
            this.button_5_OturumEkle.UseVisualStyleBackColor = true;
            this.button_5_OturumEkle.Visible = false;
            this.button_5_OturumEkle.Click += new System.EventHandler(this.Button_5_OturumEkle_Click);
            // 
            // comboBoxOturum4
            // 
            this.comboBoxOturum4.FormattingEnabled = true;
            this.comboBoxOturum4.Location = new System.Drawing.Point(110, 182);
            this.comboBoxOturum4.Name = "comboBoxOturum4";
            this.comboBoxOturum4.Size = new System.Drawing.Size(123, 21);
            this.comboBoxOturum4.TabIndex = 11;
            this.comboBoxOturum4.Visible = false;
            // 
            // labelOturum4
            // 
            this.labelOturum4.AutoSize = true;
            this.labelOturum4.Location = new System.Drawing.Point(30, 185);
            this.labelOturum4.Name = "labelOturum4";
            this.labelOturum4.Size = new System.Drawing.Size(53, 13);
            this.labelOturum4.TabIndex = 10;
            this.labelOturum4.Text = "4. Oturum";
            this.labelOturum4.Visible = false;
            // 
            // comboBoxOturum5
            // 
            this.comboBoxOturum5.FormattingEnabled = true;
            this.comboBoxOturum5.Location = new System.Drawing.Point(110, 220);
            this.comboBoxOturum5.Name = "comboBoxOturum5";
            this.comboBoxOturum5.Size = new System.Drawing.Size(123, 21);
            this.comboBoxOturum5.TabIndex = 14;
            this.comboBoxOturum5.Visible = false;
            // 
            // labelOturum5
            // 
            this.labelOturum5.AutoSize = true;
            this.labelOturum5.Location = new System.Drawing.Point(30, 223);
            this.labelOturum5.Name = "labelOturum5";
            this.labelOturum5.Size = new System.Drawing.Size(53, 13);
            this.labelOturum5.TabIndex = 13;
            this.labelOturum5.Text = "5. Oturum";
            this.labelOturum5.Visible = false;
            // 
            // buttonOturumBirlestir
            // 
            this.buttonOturumBirlestir.Location = new System.Drawing.Point(257, 31);
            this.buttonOturumBirlestir.Name = "buttonOturumBirlestir";
            this.buttonOturumBirlestir.Size = new System.Drawing.Size(92, 23);
            this.buttonOturumBirlestir.TabIndex = 15;
            this.buttonOturumBirlestir.Text = "Birleştir";
            this.buttonOturumBirlestir.UseVisualStyleBackColor = true;
            this.buttonOturumBirlestir.Click += new System.EventHandler(this.ButtonOturumBirlestir_Click);
            // 
            // textBoxOturumKayitAdi
            // 
            this.textBoxOturumKayitAdi.Location = new System.Drawing.Point(368, 33);
            this.textBoxOturumKayitAdi.Name = "textBoxOturumKayitAdi";
            this.textBoxOturumKayitAdi.Size = new System.Drawing.Size(123, 20);
            this.textBoxOturumKayitAdi.TabIndex = 16;
            this.textBoxOturumKayitAdi.Visible = false;
            // 
            // labelOturumKayitAdi
            // 
            this.labelOturumKayitAdi.AutoSize = true;
            this.labelOturumKayitAdi.Location = new System.Drawing.Point(368, 14);
            this.labelOturumKayitAdi.Name = "labelOturumKayitAdi";
            this.labelOturumKayitAdi.Size = new System.Drawing.Size(48, 13);
            this.labelOturumKayitAdi.TabIndex = 17;
            this.labelOturumKayitAdi.Text = "Kayıt Adı";
            this.labelOturumKayitAdi.Visible = false;
            // 
            // buttonOturumKaydet
            // 
            this.buttonOturumKaydet.Location = new System.Drawing.Point(368, 63);
            this.buttonOturumKaydet.Name = "buttonOturumKaydet";
            this.buttonOturumKaydet.Size = new System.Drawing.Size(123, 23);
            this.buttonOturumKaydet.TabIndex = 18;
            this.buttonOturumKaydet.Text = "Kaydet";
            this.buttonOturumKaydet.UseVisualStyleBackColor = true;
            this.buttonOturumKaydet.Visible = false;
            this.buttonOturumKaydet.Click += new System.EventHandler(this.ButtonOturumKaydet_Click);
            // 
            // buttonOturumRaporAl
            // 
            this.buttonOturumRaporAl.Location = new System.Drawing.Point(368, 102);
            this.buttonOturumRaporAl.Name = "buttonOturumRaporAl";
            this.buttonOturumRaporAl.Size = new System.Drawing.Size(123, 23);
            this.buttonOturumRaporAl.TabIndex = 19;
            this.buttonOturumRaporAl.Text = "Rapor Al";
            this.buttonOturumRaporAl.UseVisualStyleBackColor = true;
            this.buttonOturumRaporAl.Visible = false;
            this.buttonOturumRaporAl.Click += new System.EventHandler(this.ButtonOturumRaporAl_Click);
            // 
            // OturumBirlestir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonOturumRaporAl);
            this.Controls.Add(this.buttonOturumKaydet);
            this.Controls.Add(this.labelOturumKayitAdi);
            this.Controls.Add(this.textBoxOturumKayitAdi);
            this.Controls.Add(this.buttonOturumBirlestir);
            this.Controls.Add(this.comboBoxOturum5);
            this.Controls.Add(this.labelOturum5);
            this.Controls.Add(this.button_5_OturumEkle);
            this.Controls.Add(this.comboBoxOturum4);
            this.Controls.Add(this.labelOturum4);
            this.Controls.Add(this.button_4_OturumEkle);
            this.Controls.Add(this.comboBoxOturum3);
            this.Controls.Add(this.labelOturum3);
            this.Controls.Add(this.button_3_OturumEkle);
            this.Controls.Add(this.comboBoxOturum2);
            this.Controls.Add(this.labelOturum2);
            this.Controls.Add(this.comboBoxOturum1);
            this.Controls.Add(this.labelOturum1);
            this.Controls.Add(this.comboBoxOturumSinavTuru);
            this.Controls.Add(this.label1);
            this.Name = "OturumBirlestir";
            this.Text = "OturumBirlestir";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxOturumSinavTuru;
        private System.Windows.Forms.ComboBox comboBoxOturum1;
        private System.Windows.Forms.Label labelOturum1;
        private System.Windows.Forms.ComboBox comboBoxOturum2;
        private System.Windows.Forms.Label labelOturum2;
        private System.Windows.Forms.Button button_3_OturumEkle;
        private System.Windows.Forms.Button button_4_OturumEkle;
        private System.Windows.Forms.ComboBox comboBoxOturum3;
        private System.Windows.Forms.Label labelOturum3;
        private System.Windows.Forms.Button button_5_OturumEkle;
        private System.Windows.Forms.ComboBox comboBoxOturum4;
        private System.Windows.Forms.Label labelOturum4;
        private System.Windows.Forms.ComboBox comboBoxOturum5;
        private System.Windows.Forms.Label labelOturum5;
        private System.Windows.Forms.Button buttonOturumBirlestir;
        private System.Windows.Forms.TextBox textBoxOturumKayitAdi;
        private System.Windows.Forms.Label labelOturumKayitAdi;
        private System.Windows.Forms.Button buttonOturumKaydet;
        private System.Windows.Forms.Button buttonOturumRaporAl;
    }
}