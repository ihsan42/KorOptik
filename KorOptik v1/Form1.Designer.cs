namespace KorOptik_v1
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.formOkuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tarayıcıdanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resimDosyasıdanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.değerlendirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kalibrasyonYapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formTasarlaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formYazdırToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raporlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonOku = new System.Windows.Forms.Button();
            this.comboBoxFormTuru = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewOkunanlar = new System.Windows.Forms.DataGridView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelProgressDurum = new System.Windows.Forms.Label();
            this.labelOkumaDurumu = new System.Windows.Forms.Label();
            this.labelSecilenFormSayisi = new System.Windows.Forms.Label();
            this.oturumBirleştirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOkunanlar)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Font = new System.Drawing.Font("Calibri", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formOkuToolStripMenuItem,
            this.değerlendirToolStripMenuItem,
            this.kalibrasyonYapToolStripMenuItem,
            this.formTasarlaToolStripMenuItem,
            this.formYazdırToolStripMenuItem,
            this.oturumBirleştirToolStripMenuItem,
            this.raporlarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1273, 36);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // formOkuToolStripMenuItem
            // 
            this.formOkuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tarayıcıdanToolStripMenuItem,
            this.resimDosyasıdanToolStripMenuItem});
            this.formOkuToolStripMenuItem.Name = "formOkuToolStripMenuItem";
            this.formOkuToolStripMenuItem.Size = new System.Drawing.Size(120, 30);
            this.formOkuToolStripMenuItem.Text = "Form Yükle";
            // 
            // tarayıcıdanToolStripMenuItem
            // 
            this.tarayıcıdanToolStripMenuItem.Name = "tarayıcıdanToolStripMenuItem";
            this.tarayıcıdanToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.tarayıcıdanToolStripMenuItem.Text = "Tarayıcıdan";
            // 
            // resimDosyasıdanToolStripMenuItem
            // 
            this.resimDosyasıdanToolStripMenuItem.Name = "resimDosyasıdanToolStripMenuItem";
            this.resimDosyasıdanToolStripMenuItem.Size = new System.Drawing.Size(240, 30);
            this.resimDosyasıdanToolStripMenuItem.Text = "Resim Dosyasıdan";
            this.resimDosyasıdanToolStripMenuItem.Click += new System.EventHandler(this.resimDosyasıdanToolStripMenuItem_Click);
            // 
            // değerlendirToolStripMenuItem
            // 
            this.değerlendirToolStripMenuItem.Name = "değerlendirToolStripMenuItem";
            this.değerlendirToolStripMenuItem.Size = new System.Drawing.Size(124, 30);
            this.değerlendirToolStripMenuItem.Text = "Değerlendir";
            this.değerlendirToolStripMenuItem.Click += new System.EventHandler(this.değerlendirToolStripMenuItem_Click);
            // 
            // kalibrasyonYapToolStripMenuItem
            // 
            this.kalibrasyonYapToolStripMenuItem.Name = "kalibrasyonYapToolStripMenuItem";
            this.kalibrasyonYapToolStripMenuItem.Size = new System.Drawing.Size(161, 30);
            this.kalibrasyonYapToolStripMenuItem.Text = "Kalibrasyon Yap";
            this.kalibrasyonYapToolStripMenuItem.Click += new System.EventHandler(this.kalibrasyonYapToolStripMenuItem_Click);
            // 
            // formTasarlaToolStripMenuItem
            // 
            this.formTasarlaToolStripMenuItem.Name = "formTasarlaToolStripMenuItem";
            this.formTasarlaToolStripMenuItem.Size = new System.Drawing.Size(135, 30);
            this.formTasarlaToolStripMenuItem.Text = "Form Tasarla";
            this.formTasarlaToolStripMenuItem.Click += new System.EventHandler(this.formTasarlaToolStripMenuItem_Click);
            // 
            // formYazdırToolStripMenuItem
            // 
            this.formYazdırToolStripMenuItem.Name = "formYazdırToolStripMenuItem";
            this.formYazdırToolStripMenuItem.Size = new System.Drawing.Size(125, 30);
            this.formYazdırToolStripMenuItem.Text = "Form Yazdır";
            this.formYazdırToolStripMenuItem.Click += new System.EventHandler(this.formYazdırToolStripMenuItem_Click);
            // 
            // raporlarToolStripMenuItem
            // 
            this.raporlarToolStripMenuItem.Name = "raporlarToolStripMenuItem";
            this.raporlarToolStripMenuItem.Size = new System.Drawing.Size(99, 30);
            this.raporlarToolStripMenuItem.Text = "Raporlar";
            this.raporlarToolStripMenuItem.Click += new System.EventHandler(this.RaporlarToolStripMenuItem_Click);
            // 
            // buttonOku
            // 
            this.buttonOku.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.buttonOku.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonOku.Location = new System.Drawing.Point(450, 73);
            this.buttonOku.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOku.Name = "buttonOku";
            this.buttonOku.Size = new System.Drawing.Size(147, 39);
            this.buttonOku.TabIndex = 3;
            this.buttonOku.Text = "Oku";
            this.buttonOku.UseVisualStyleBackColor = false;
            this.buttonOku.Click += new System.EventHandler(this.buttonOku_Click);
            // 
            // comboBoxFormTuru
            // 
            this.comboBoxFormTuru.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboBoxFormTuru.FormattingEnabled = true;
            this.comboBoxFormTuru.Location = new System.Drawing.Point(10, 73);
            this.comboBoxFormTuru.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxFormTuru.Name = "comboBoxFormTuru";
            this.comboBoxFormTuru.Size = new System.Drawing.Size(139, 24);
            this.comboBoxFormTuru.TabIndex = 1;
            this.comboBoxFormTuru.SelectedIndexChanged += new System.EventHandler(this.comboBoxFormTuru_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(38, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Form Türü";
            // 
            // dataGridViewOkunanlar
            // 
            this.dataGridViewOkunanlar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOkunanlar.Location = new System.Drawing.Point(10, 135);
            this.dataGridViewOkunanlar.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewOkunanlar.Name = "dataGridViewOkunanlar";
            this.dataGridViewOkunanlar.Size = new System.Drawing.Size(1252, 345);
            this.dataGridViewOkunanlar.TabIndex = 4;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(450, 47);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(147, 19);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Visible = false;
            // 
            // labelProgressDurum
            // 
            this.labelProgressDurum.AutoSize = true;
            this.labelProgressDurum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelProgressDurum.Location = new System.Drawing.Point(151, 39);
            this.labelProgressDurum.Name = "labelProgressDurum";
            this.labelProgressDurum.Size = new System.Drawing.Size(0, 16);
            this.labelProgressDurum.TabIndex = 6;
            // 
            // labelOkumaDurumu
            // 
            this.labelOkumaDurumu.AutoSize = true;
            this.labelOkumaDurumu.Location = new System.Drawing.Point(466, 80);
            this.labelOkumaDurumu.Name = "labelOkumaDurumu";
            this.labelOkumaDurumu.Size = new System.Drawing.Size(0, 16);
            this.labelOkumaDurumu.TabIndex = 7;
            // 
            // labelSecilenFormSayisi
            // 
            this.labelSecilenFormSayisi.AutoSize = true;
            this.labelSecilenFormSayisi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelSecilenFormSayisi.Location = new System.Drawing.Point(154, 39);
            this.labelSecilenFormSayisi.Name = "labelSecilenFormSayisi";
            this.labelSecilenFormSayisi.Size = new System.Drawing.Size(0, 16);
            this.labelSecilenFormSayisi.TabIndex = 8;
            // 
            // oturumBirleştirToolStripMenuItem
            // 
            this.oturumBirleştirToolStripMenuItem.Name = "oturumBirleştirToolStripMenuItem";
            this.oturumBirleştirToolStripMenuItem.Size = new System.Drawing.Size(162, 30);
            this.oturumBirleştirToolStripMenuItem.Text = "Oturum Birleştir";
            this.oturumBirleştirToolStripMenuItem.Click += new System.EventHandler(this.OturumBirleştirToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1273, 513);
            this.Controls.Add(this.labelSecilenFormSayisi);
            this.Controls.Add(this.labelOkumaDurumu);
            this.Controls.Add(this.labelProgressDurum);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dataGridViewOkunanlar);
            this.Controls.Add(this.buttonOku);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxFormTuru);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "KorOptik v1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOkunanlar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem formOkuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tarayıcıdanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resimDosyasıdanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem değerlendirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kalibrasyonYapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formTasarlaToolStripMenuItem;
        private System.Windows.Forms.Button buttonOku;
        private System.Windows.Forms.ComboBox comboBoxFormTuru;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewOkunanlar;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelProgressDurum;
        private System.Windows.Forms.Label labelOkumaDurumu;
        private System.Windows.Forms.Label labelSecilenFormSayisi;
        private System.Windows.Forms.ToolStripMenuItem formYazdırToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem raporlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oturumBirleştirToolStripMenuItem;
    }
}

