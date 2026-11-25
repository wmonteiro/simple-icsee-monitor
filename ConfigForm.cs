using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICSEE_Monitor
{
    public partial class ConfigForm : Form
    {
        public string Content { get; set; }

        public ConfigForm(string config)
        {
            InitializeComponent();
            textBox1.Text = config;
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Content = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private async void discover_Click(object sender, EventArgs e)
        {
            discover.Enabled = false;

            // Executa o scan inteligente
            List<string> ipsEncontrados = await Discovery.ScanIPRange();

            if (ipsEncontrados.Count > 0)
            {
                string mensagem = "Câmeras ICSee detectadas:\n\n";
                foreach (var ip in ipsEncontrados)
                {
                    mensagem += $"• IP: {ip}\n";
                }
                MessageBox.Show(mensagem, "Busca Concluída");

                // Exemplo: Colocar o primeiro IP encontrado no campo de texto se estiver vazio
                // txtConfig.AppendText(ipsEncontrados[0] + ";10.0.0.X;usuario;senha" + Environment.NewLine);
            }
            else
            {
                MessageBox.Show("Nenhum dispositivo ICSee respondeu na faixa 10.0.0.1-9.");
            }

            discover.Enabled = true;
        }
    }
}
