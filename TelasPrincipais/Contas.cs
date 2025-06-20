using FinTracker.AlternativeTelas;
using FinTracker.BD;
using FinTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.TelasPrincipais
{
    public partial class Contas : Form
    {
        Admin admin;
        List<Conta> contas_a_pagar;
        List<Conta> contas_a_receber;
        double valorTotal = 0;
        int qtdEncontrada = 0;
        public Contas()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
        }
        public Contas(Admin admin)
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
            this.admin = admin;
            nomeUsuario.Text = admin.GetNome();
            button2_Click(button2, null); // Carrega as contas a pagar e receber ao iniciar a tela
        }

        private void pnlVerPerfil_Click(object sender, MouseEventArgs e)
        {
            Perfil perfil = new Perfil(admin);
            perfil.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GerirNovosCadastros gerirNovosCadastros = new GerirNovosCadastros();
            gerirNovosCadastros.Show();
        }

        private async void pegarContas_a_pagar()
        {
            valorTotal = 0;
            contas_a_pagar = await new ContaPagarRepository().pegarContas_a_pagar();
            qtdEncontrada = contas_a_pagar.Count;
            for (int i = 0; i < contas_a_pagar.Count; i++)
            {
                Panel card = CriarCard(contas_a_pagar[i]);
                flowLayoutPanel1.Controls.Add(card);
                valorTotal += contas_a_pagar[i].valor;
            }
            txtTotal.Text = "Valor total: R$ " + valorTotal.ToString("F2");
            txtQtdEncontrado.Text = "Quantidade encontrada: " + (contas_a_pagar.Count).ToString();
        }
        private async void pegarContas_a_receber()
        {
            valorTotal = 0;
            contas_a_receber = await new ContaReceberRepository().pegarContas_a_receber();
            for (int i = 0; i < contas_a_receber.Count; i++)
            {
                Panel card = CriarCard(contas_a_receber[i]);
                flowLayoutPanel1.Controls.Add(card);
                valorTotal += contas_a_receber[i].valor;
            }
            txtTotal.Text = "Valor total: R$ " + valorTotal.ToString("F2");
            txtQtdEncontrado.Text = "Quantidade encontrada: " + (contas_a_receber.Count).ToString();
        }

        private Panel CriarCard(Conta conta)
        {
            // Container principal
            Panel cardPanel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Size = new Size(370, 250),
                Location = new Point(30, 30),
                Padding = new Padding(15),
            };

            // Botão de opções (...)
            Button btnOptions = new Button
            {
                Text = "...",
                Font = new Font("Segoe UI", 12),
                Size = new Size(30, 30),
                Location = new Point(cardPanel.Width - 45, 10),
                FlatStyle = FlatStyle.Flat
            };
            btnOptions.FlatAppearance.BorderSize = 0;

            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("Editar", null, (s, e) => MessageBox.Show("Editar clicado"));
            menu.Items.Add("Apagar", null, (s, e) => MessageBox.Show("Apagar clicado"));
            btnOptions.Click += (s, e) =>
            {
                menu.Show(btnOptions, new Point(0, btnOptions.Height));
            };
            cardPanel.Controls.Add(btnOptions);

            // Labels
            Label lblValorTitle = new Label
            {
                Text = "Valor",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                Location = new Point(15, 10),
                AutoSize = true
            };
            cardPanel.Controls.Add(lblValorTitle);

            Label lblValor = new Label
            {
                Text = "R$ "+conta.valor.ToString(),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 40, 20),
                Location = new Point(15, 30),
                AutoSize = true
            };
            cardPanel.Controls.Add(lblValor);

            Label lblNomeTitle = new Label
            {
                Text = string.IsNullOrEmpty(conta.nomeFornecedor) ? "Nome do cliente" : "nome do fornecedor",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                Location = new Point(15, 65),
                AutoSize = true
            };
            cardPanel.Controls.Add(lblNomeTitle);

            Label lblNome = new Label
            {
                Text = string.IsNullOrEmpty(conta.nomeCliente) ? conta.nomeFornecedor : conta.nomeCliente,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 40, 20),
                Location = new Point(15, 85),
                AutoSize = true
            };
            cardPanel.Controls.Add(lblNome);

            Label lblMetodoTitle = new Label
            {
                Text = "Método de pagamento",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                Location = new Point(15, 120),
                AutoSize = true
            };
            cardPanel.Controls.Add(lblMetodoTitle);

            Label lblMetodo = new Label
            {
                Text = conta.metodoPagamento,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 40, 20),
                Location = new Point(15, 140),
                AutoSize = true
            };
            cardPanel.Controls.Add(lblMetodo);

            // Data e previsão
            Label lblDataTitle = new Label
            {
                Text = "Data da transação",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                Location = new Point(15, 180),
                AutoSize = true
            };
            cardPanel.Controls.Add(lblDataTitle);

            Button btnData = new Button
            {
                Text = conta.dataTransacao.ToString("dd MMM yyyy"),
                Location = new Point(15, 200),
                Size = new Size(100, 30),
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            btnData.FlatAppearance.BorderColor = Color.Black;
            cardPanel.Controls.Add(btnData);

            Label lblPrevisaoTitle = new Label
            {
                Text = "Previsão de termino",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                Location = new Point(130, 180),
                AutoSize = true
            };
            cardPanel.Controls.Add(lblPrevisaoTitle);

            Button btnPrevisao = new Button
            {
                Text = conta.previsaoTermino.ToString("dd MMM yyyy"),
                Location = new Point(130, 200),
                Size = new Size(100, 30),
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            btnPrevisao.FlatAppearance.BorderColor = Color.Black;
            cardPanel.Controls.Add(btnPrevisao);

            return cardPanel;
        }

        private void zerarSelecaoBotoes()
        {
            foreach (Button b in pnlBotoes.Controls.OfType<Button>())
            {
                b.BackColor = Color.White;
                b.ForeColor = Color.FromArgb(21, 39, 29);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            zerarSelecaoBotoes();
            Button button = sender as Button;
            button.BackColor = Color.FromArgb(21, 39, 29);
            button.ForeColor = Color.FromArgb(247, 247, 242);
            flowLayoutPanel1.Controls.Clear(); // Limpa os cards existentes
            pegarContas_a_pagar();
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            zerarSelecaoBotoes();
            Button button = sender as Button;
            button.BackColor = Color.FromArgb(21, 39, 29);
            button.ForeColor = Color.FromArgb(247, 247, 242);
            flowLayoutPanel1.Controls.Clear(); // Limpa os cards existentes
            pegarContas_a_receber();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            valorTotal = 0;
            zerarSelecaoBotoes();
            Button button = sender as Button;
            button.BackColor = Color.FromArgb(21, 39, 29);
            button.ForeColor = Color.FromArgb(247, 247, 242);
            flowLayoutPanel1.Controls.Clear(); // Limpa os cards existentes
            contas_a_receber = await new ContaReceberRepository().pegarContas_a_receber();
            for (int i = 0; i < contas_a_receber.Count; i++)
            {
                Panel card = CriarCard(contas_a_receber[i]);
                flowLayoutPanel1.Controls.Add(card);
                valorTotal += contas_a_receber[i].valor;
            }
            contas_a_pagar = await new ContaPagarRepository().pegarContas_a_pagar();
            for (int i = 0; i < contas_a_pagar.Count; i++)
            {
                Panel card = CriarCard(contas_a_pagar[i]);
                flowLayoutPanel1.Controls.Add(card);
                valorTotal += contas_a_pagar[i].valor;
            }
            txtTotal.Text = "Valor total: R$ " + valorTotal.ToString("F2");
            txtQtdEncontrado.Text = "Quantidade encontrada: " + (contas_a_receber.Count + contas_a_pagar.Count).ToString();
        }
    }
}
