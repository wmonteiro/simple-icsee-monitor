using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICSEE_Monitor
{
    internal static class Program
    {
        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern uint RegisterWindowMessage(string lpString);

        // Mensagem única para o seu app
        public static uint WM_SHOWME = RegisterWindowMessage("WM_SHOW_MY_CAMERA_APP");
        private static Mutex mutex = new Mutex(true, "{ICSEE-MONITOR-UNIQUE-ID}");

        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                mutex.ReleaseMutex();
            }
            else
            {
                // Já existe uma instância! Envia mensagem para todas as janelas
                // O valor HWND_BROADCAST é 0xffff
                PostMessage((IntPtr)0xffff, WM_SHOWME, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
