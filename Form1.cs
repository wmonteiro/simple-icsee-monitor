using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace ICSEE_Monitor
{
    public partial class Form1 : Form
    {
        private LibVLC _libVLC;
        private MediaPlayer _player;
        bool _mute = true;
        bool _fhd = false;
        int current_camera = 0;
        private string CaminhoArquivo = "cameras.txt";

        private List<CameraConfig> _cameras = new List<CameraConfig>{};

        public Form1()
        {
            InitializeComponent();
            Core.Initialize();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            LoadCamerasFromFile(); // Tenta carregar do arquivo primeiro

            var options = new string[]
            {
                "--rtsp-tcp",
                "--network-caching=3000",
                "--skip-frames",
                "--clock-jitter=0",
                "--clock-synchro=0",
                "--no-drop-late-frames",
                "--no-skip-frames"
            };

            _libVLC = new LibVLC(options);
            _player = new MediaPlayer(_libVLC);
            videoView1.MediaPlayer = _player;

            if(_cameras.Count > 0) camera1.PerformClick();
        }

        protected override void WndProc(ref Message m)
        {
            // Verifica se a mensagem recebida é a nossa "WM_SHOWME"
            if (m.Msg == Program.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

        private void ShowMe()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            this.Show();
            this.Activate();
        }

        private void setCamera(int index)
        {
            var config = _cameras[index];
            current_camera = index;
            startCamera(config);
        }

        private Media _media;

        private void startCamera(CameraConfig config)
        {
            // 1. Parar o player antes de trocar a fonte
            if (_player.IsPlaying)
            {
                _player.Stop();
            }

            string streamIndex = _fhd ? "0" : "1";
            string url = $"rtsp://{config.User}:{config.Password}@{config.IP}:554/user={config.User}&password={config.Password}&channel=1&stream={streamIndex}.sdp";

            // 2. Liberar a mídia anterior com segurança
            _media?.Dispose();

            // 3. Criar a nova mídia
            _media = new Media(_libVLC, new Uri(url));

            // Opções de performance
            _media.AddOption(":rtsp-tcp");
            _media.AddOption(":network-caching=1000"); // Reduzi para 1000ms para ser mais responsivo
            _media.AddOption(":clock-jitter=0");

            // 4. Atualizar UI
            quality_toggle.Text = (_fhd ? "HD" : "FHD");
            label1.Text = $"{config.Name} | Online | {(_fhd ? "FHD" : "HD")}";

            // 5. Play
            _player.Mute = _mute;
            _player.Play(_media);
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
            }

            videoView1.Invalidate();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                notifyIcon1.Visible = true;
            }
            base.OnFormClosing(e);
        }

        protected override void OnResizeBegin(EventArgs e)
        {
            base.OnResizeBegin(e);
            this.SuspendLayout();
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            this.ResumeLayout(true);
            videoView1.Refresh();
        }

        private void camera1_Click(object sender, EventArgs e)
        {
            setCamera(0);
            camera1.Enabled = false;
            camera2.Enabled = true;
            camera3.Enabled = true;
        }

        private void camera2_Click(object sender, EventArgs e)
        {
            setCamera(1);
            camera1.Enabled = true;
            camera2.Enabled = false;
            camera3.Enabled = true;
        }

        private void camera3_Click(object sender, EventArgs e)
        {
            setCamera(2);
            camera1.Enabled = true;
            camera2.Enabled = true;
            camera3.Enabled = false;
        }

        private void audio_toggle_Click(object sender, EventArgs e)
        {
            _mute = !_mute;
            _player.Mute = _mute;
            audio_toggle.Text = _mute ? "Turn Sounds On" : "Turn Sounds Off";
        }

        private void quality_toggle_Click(object sender, EventArgs e)
        {
            _fhd = !_fhd;
            setCamera(current_camera);
            quality_toggle.Text = _fhd ? "HD" : "FHD";
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            notifyIcon1.Visible = false;


            // DEVOLVE O VÍDEO PARA A TELA PRINCIPAL
            if (_preview != null && !_preview.IsDisposed)
            {
                _player.Stop(); // Para o vídeo
                startCamera(_cameras[current_camera]);
                _preview.Hide();
            }
            _player.Hwnd = videoView1.Handle;
            videoView1.MediaPlayer = _player;
        }


        // Dentro do Form1.cs
        public void RestaurarCameraPrincipal()
        {
            // 1. Para o vídeo que estava no Preview
            if (_player != null && _player.IsPlaying)
            {
                _player.Stop();
            }

            // 2. Garante que a janela principal apareça
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            this.Activate();

            // 3. Reinicia o vídeo no handle do Form1
            // (Assumindo que no Form1 você também usa o mesmo _player)
            _player.Hwnd = videoView1.Handle;

            // 4. Chama sua função de start
            startCamera(_cameras[current_camera]);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private PreviewForm _preview;



        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_preview == null || _preview.IsDisposed)
            {
                _preview = new PreviewForm();
            }

            if (!this.Visible) // Só executa se a janela principal estiver na bandeja
            {
                _preview.SetPlayer(_player);

                // FORÇA O VÍDEO A APARECER:
                // 1. Reatribui o ID da janela (Handle)
                _player.Hwnd = _preview.VideoHandle;

                _player.Stop(); // Para o vídeo
                startCamera(_cameras[current_camera]); // Reinicia a conexão direto no preview

                if (!_preview.Visible)
                {
                    var areaTrabalho = Screen.PrimaryScreen.WorkingArea;

                    int x = areaTrabalho.Right - _preview.Width - 10;
                    int y = areaTrabalho.Bottom - _preview.Height - 10;

                    _preview.Location = new System.Drawing.Point(x, y);
                    _preview.Show();
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void config_Click(object sender, EventArgs e)
        {
            using (var form = new ConfigForm(GetCamerasAsText()))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    SaveCamerasFromText(form.Content);

                    camera1.PerformClick();

                    camera1.Text = _cameras.Count > 0 ? _cameras[0].Name : "Cam 1";
                    camera2.Text = _cameras.Count > 1 ? _cameras[1].Name : "Cam 2";
                    camera3.Text = _cameras.Count > 2 ? _cameras[2].Name : "Cam 3";
                }
            }
        }

        // Converte a lista atual para o formato de texto: Nome;IP;Usuario;Senha
        private string GetCamerasAsText()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var cam in _cameras)
            {
                sb.AppendLine($"{cam.Name};{cam.IP};{cam.User};{cam.Password}");
            }
            return sb.ToString();
        }

        // Converte o texto do TextBox de volta para a lista de objetos
        private void SaveCamerasFromText(string text)
        {
            var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            _cameras.Clear();

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 4)
                {
                    _cameras.Add(new CameraConfig
                    {
                        Name = parts[0].Trim(),
                        IP = parts[1].Trim(),
                        User = parts[2].Trim(),
                        Password = parts[3].Trim()
                    });
                }
            }

            camera1.Text = _cameras[0].Name;
            camera2.Text = _cameras[1].Name;
            camera3.Text = _cameras[2].Name;

            // Salva fisicamente no arquivo
            System.IO.File.WriteAllText(CaminhoArquivo, text);
        }

        private void LoadCamerasFromFile()
        {
            if (System.IO.File.Exists(CaminhoArquivo))
            {
                string conteudo = System.IO.File.ReadAllText(CaminhoArquivo);
                SaveCamerasFromText(conteudo);
            }
        }

    }

    public class CameraConfig
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool IsFHD { get; set; }
    }
}
