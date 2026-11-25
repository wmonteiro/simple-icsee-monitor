namespace ICSEE_Monitor
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                _libVLC?.Dispose();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.camera1 = new System.Windows.Forms.Button();
            this.camera2 = new System.Windows.Forms.Button();
            this.camera3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.videoView1 = new LibVLCSharp.WinForms.VideoView();
            this.audio_toggle = new System.Windows.Forms.Button();
            this.quality_toggle = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.config = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // camera1
            // 
            this.camera1.Location = new System.Drawing.Point(12, 12);
            this.camera1.Name = "camera1";
            this.camera1.Size = new System.Drawing.Size(84, 29);
            this.camera1.TabIndex = 0;
            this.camera1.TabStop = false;
            this.camera1.Text = "Cam 1";
            this.camera1.UseVisualStyleBackColor = true;
            this.camera1.Click += new System.EventHandler(this.camera1_Click);
            // 
            // camera2
            // 
            this.camera2.Location = new System.Drawing.Point(12, 47);
            this.camera2.Name = "camera2";
            this.camera2.Size = new System.Drawing.Size(84, 29);
            this.camera2.TabIndex = 1;
            this.camera2.TabStop = false;
            this.camera2.Text = "Cam 2";
            this.camera2.UseVisualStyleBackColor = true;
            this.camera2.Click += new System.EventHandler(this.camera2_Click);
            // 
            // camera3
            // 
            this.camera3.Location = new System.Drawing.Point(12, 82);
            this.camera3.Name = "camera3";
            this.camera3.Size = new System.Drawing.Size(84, 29);
            this.camera3.TabIndex = 2;
            this.camera3.TabStop = false;
            this.camera3.Text = "Cam 3";
            this.camera3.UseVisualStyleBackColor = true;
            this.camera3.Click += new System.EventHandler(this.camera3_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.videoView1);
            this.panel1.Location = new System.Drawing.Point(102, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1030, 577);
            this.panel1.TabIndex = 3;
            // 
            // videoView1
            // 
            this.videoView1.BackColor = System.Drawing.Color.Black;
            this.videoView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoView1.Location = new System.Drawing.Point(0, 0);
            this.videoView1.MediaPlayer = null;
            this.videoView1.Name = "videoView1";
            this.videoView1.Size = new System.Drawing.Size(1030, 577);
            this.videoView1.TabIndex = 0;
            this.videoView1.Text = "videoView1";
            // 
            // audio_toggle
            // 
            this.audio_toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.audio_toggle.Location = new System.Drawing.Point(831, 5);
            this.audio_toggle.Name = "audio_toggle";
            this.audio_toggle.Size = new System.Drawing.Size(122, 21);
            this.audio_toggle.TabIndex = 4;
            this.audio_toggle.Text = "Turn Sounds On";
            this.audio_toggle.UseVisualStyleBackColor = true;
            this.audio_toggle.Click += new System.EventHandler(this.audio_toggle_Click);
            // 
            // quality_toggle
            // 
            this.quality_toggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.quality_toggle.Location = new System.Drawing.Point(959, 5);
            this.quality_toggle.Name = "quality_toggle";
            this.quality_toggle.Size = new System.Drawing.Size(72, 21);
            this.quality_toggle.TabIndex = 7;
            this.quality_toggle.Text = "FHD";
            this.quality_toggle.UseVisualStyleBackColor = true;
            this.quality_toggle.Click += new System.EventHandler(this.quality_toggle_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.audio_toggle);
            this.panel2.Controls.Add(this.quality_toggle);
            this.panel2.Location = new System.Drawing.Point(102, 595);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1030, 30);
            this.panel2.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 8;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "ICSEE Câmeras";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previewToolStripMenuItem,
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(116, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.previewToolStripMenuItem.Text = "Preview";
            this.previewToolStripMenuItem.Click += new System.EventHandler(this.previewToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(115, 22);
            this.toolStripMenuItem1.Text = "Fechar";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // config
            // 
            this.config.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.config.Location = new System.Drawing.Point(12, 596);
            this.config.Name = "config";
            this.config.Size = new System.Drawing.Size(84, 29);
            this.config.TabIndex = 9;
            this.config.TabStop = false;
            this.config.Text = "Configurar";
            this.config.UseVisualStyleBackColor = true;
            this.config.Click += new System.EventHandler(this.config_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1144, 637);
            this.Controls.Add(this.config);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.camera3);
            this.Controls.Add(this.camera2);
            this.Controls.Add(this.camera1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ICSEE Câmeras";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button camera1;
        private System.Windows.Forms.Button camera2;
        private System.Windows.Forms.Button camera3;
        private System.Windows.Forms.Panel panel1;
        private LibVLCSharp.WinForms.VideoView videoView1;
        private System.Windows.Forms.Button audio_toggle;
        private System.Windows.Forms.Button quality_toggle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
        private System.Windows.Forms.Button config;
    }
}

