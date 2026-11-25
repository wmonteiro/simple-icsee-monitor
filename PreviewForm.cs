using LibVLCSharp.Shared;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ICSEE_Monitor
{
    public partial class PreviewForm : Form
    {
        private TransparentPanel _dragOverlay;

        private const int cGrip = 16;           // Tamanho da área sensível para redimensionar
        private const int WM_NCHITTEST = 0x84;
        private const int HTBOTTOMRIGHT = 17;   // Canto inferior direito
        private const int HTCAPTION = 0x2;
        private const int WM_NCLBUTTONDOWN = 0xA1;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        public PreviewForm()
        {
            InitializeComponent();

            videoView1.Dock = DockStyle.Fill;

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.MinimumSize = new Size(160, 120);

            // -----------------------
            //   MENU DE CONTEXTO
            // -----------------------
            var menu = new ContextMenuStrip();
            var itemFechar = new ToolStripMenuItem("Fechar Preview");
            itemFechar.Click += (s, e) => this.Hide();
            menu.Items.Add(itemFechar);

            // -----------------------
            //   Painel de overlay
            // -----------------------
            _dragOverlay = new TransparentPanel
            {
                Dock = DockStyle.Fill,
                ContextMenuStrip = menu
            };

            this.Controls.Add(_dragOverlay);
            _dragOverlay.BringToFront();

            // Arrastar a janela (move)
            _dragOverlay.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    // Só arrasta se NÃO estiver na área de grip (evita conflito)
                    if (e.X <= this.ClientSize.Width - cGrip || e.Y <= this.ClientSize.Height - cGrip)
                    {
                        ReleaseCapture();
                        SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                    }
                }
            };


            // Eventos do Overlay
            _dragOverlay.MouseDown += Overlay_MouseDown;
            _dragOverlay.MouseMove += Overlay_MouseMove;
            _dragOverlay.MouseUp += (s, e) => { isResizing = false; };

        }

        private bool isResizing = false;
        private Size startSize;
        private Point startMousePos;


        private void Overlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2 && e.Button == MouseButtons.Left)
            {
                this.Hide(); // Esconde o preview imediatamente

                // Localiza a instância do Form1
                foreach (Form f in Application.OpenForms)
                {
                    if (f is Form1 mainForm)
                    {
                        // Chama o método que criamos acima
                        mainForm.RestaurarCameraPrincipal();
                        break;
                    }
                }
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (e.X >= this.Width - cGrip && e.Y >= this.Height - cGrip)
                {
                    isResizing = true;
                    startSize = this.Size;
                    startMousePos = Cursor.Position;
                }
                else
                {
                    ReleaseCapture();
                    SendMessage(this.Handle, 0xA1, 0x2, 0);
                }
            }
        }

        private void Overlay_MouseMove(object sender, MouseEventArgs e)
        {
            if (isResizing)
            {
                // Calcula quanto o mouse andou desde o clique inicial
                int deltaX = Cursor.Position.X - startMousePos.X;
                int deltaY = Cursor.Position.Y - startMousePos.Y;

                int newW = startSize.Width + deltaX;
                int newH = startSize.Height + deltaY;

                // Define limites mínimos para não bugar o player
                if (newW < 200) newW = 200;
                if (newH < 150) newH = 150;

                this.Size = new Size(newW, newH);
            }
            else
            {
                // Muda o ícone do cursor para avisar que pode redimensionar
                if (e.X >= this.Width - cGrip && e.Y >= this.Height - cGrip)
                    _dragOverlay.Cursor = Cursors.SizeNWSE;
                else
                    _dragOverlay.Cursor = Cursors.Default;
            }
        }

        public void SetPlayer(MediaPlayer player)
        {
            videoView1.Width = 240;
            videoView1.Height = 135;
            videoView1.MediaPlayer = player;
        }

        public IntPtr VideoHandle => videoView1.Handle;

        // Painel que permite ver através dele
        public class TransparentPanel : Panel
        {
            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                    return cp;
                }
            }

            protected override void OnPaintBackground(PaintEventArgs e)
            {
                // Deixa de pintar o fundo → fica realmente transparente
            }
        }
    }
}