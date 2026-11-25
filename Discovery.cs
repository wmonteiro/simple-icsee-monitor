using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ICSEE_Monitor
{
    public static class Discovery
    {
        public static async Task<List<string>> ScanIPRange()
        {
            List<string> foundIPs = new List<string>();
            // Testamos as 3 portas mais comuns de câmeras chinesas
            int[] commonPorts = { 34567, 3702, 8899 };
            string baseIP = "10.0.0.";

            // Varre de 1 a 254 para não ter erro de faixa
            var tasks = Enumerable.Range(1, 254).Select(async i =>
            {
                string ip = baseIP + i;
                foreach (int port in commonPorts)
                {
                    if (await IsCameraReal(ip, port, 300)) // Timeout curto para ser rápido
                    {
                        lock (foundIPs) foundIPs.Add($"{ip} (Porta: {port})");
                        break;
                    }
                }
            });

            await Task.WhenAll(tasks);
            return foundIPs.OrderBy(x => x).ToList();
        }

        private static async Task<bool> IsCameraReal(string host, int port, int timeout)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    // Se conseguir APENAS conectar na porta 34567, já vamos considerar que é a câmera
                    var connectTask = client.ConnectAsync(host, port);
                    if (await Task.WhenAny(connectTask, Task.Delay(timeout)) == connectTask && client.Connected)
                    {
                        return true;
                    }
                }
            }
            catch { return false; }
            return false;
        }
    }
}
