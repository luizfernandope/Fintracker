using FinTracker.LoginCadastro;
using FinTracker.Models;
using FinTracker.Telas;
using FinTracker.TelasPrincipais;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FinTracker
{
    public partial class Form1 : Form
    {
        Admin admin;
        Principal telaMae;
        public Form1()
        {
            InitializeComponent();
            mudarTelaDoPanelPrincipal(new Home());
        }
        public Form1(Admin admin, Principal telaMae)
        {
            InitializeComponent();
            mudarTelaDoPanelPrincipal(new Home(admin));
            this.admin = admin;
            this.telaMae = telaMae;
        }

        public void trocarTela(object btnTelaAlvo)
        {
            Button button = (Button)btnTelaAlvo;
            if (button.ForeColor == Color.FromArgb(21, 39, 29))
                return;//se ja botao escolhido ja esta escolhido, nn faz nada

            Button[] buttons = { btnHome, btnClientes, btnContas, btnFornecedores, btnImportar, btnPagamentos, btnVendas };
            foreach (Button b in buttons)
            {
                b.BackColor = Color.FromArgb(21, 39, 29);
                b.ForeColor = Color.White;
            }//Resetando botoes de navegacao (como se nenhum estivesse marcado)
            
            //personalizando estilo do botao para estilo de selecionado
            button.BackColor = Color.FromArgb(233, 233, 220);
            button.ForeColor = Color.FromArgb(21, 39, 29);

            if (button.Name.Contains("Home"))
                mudarTelaDoPanelPrincipal(new Home(admin));
            else if (button.Name.Contains("Vendas"))
                mudarTelaDoPanelPrincipal(new Vendas(admin));            
            else if (button.Name.Contains("Pagamen"))
                mudarTelaDoPanelPrincipal(new Pagamentos(admin));
            else if (button.Name.Contains("Contas"))
                mudarTelaDoPanelPrincipal(new Contas(admin));
            else if (button.Name.Contains("Fornece"))
                mudarTelaDoPanelPrincipal(new Fornecedores(admin));
            else if (button.Name.Contains("Client"))
                mudarTelaDoPanelPrincipal(new Clientes(admin));
            else if (button.Name.Contains("Importar"))
                mudarTelaDoPanelPrincipal(new Importar(admin));

        }

        private void mudarTelaDoPanelPrincipal(object Tela)
        {
            if (this.panelPrincipal.Controls.Count > 0)
                this.panelPrincipal.Controls.RemoveAt(0);
            Form f = Tela as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelPrincipal.Controls.Add(f);
            this.panelPrincipal.Tag = f;
            f.Show();
        }

        private void btnVendas_Click(object sender, EventArgs e)
        {
            trocarTela(sender);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            trocarTela(sender);
        }

        private void btnPagamentos_Click(object sender, EventArgs e)
        {
            trocarTela(sender);
        }

        private void btnContas_Click(object sender, EventArgs e)
        {
            trocarTela(sender);
        }

        private void btnFornecedores_Click(object sender, EventArgs e)
        {
            trocarTela(sender);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            trocarTela(sender);
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            trocarTela(sender);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //perguntar se o usuario realmente deseja sair
            DialogResult dialogResult = MessageBox.Show("Deseja realmente sair?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
                return; //se o usuario nn quiser sair, nn faz nada
            telaMae.mudarTelaDoPanelPrincipal(new Login(telaMae));
            telaMae.Size = new System.Drawing.Size(1080, 604); // Definindo um tamanho fixo para a tela principal
            telaMae.MaximizeBox = false;
            telaMae.WindowState = FormWindowState.Normal; // Normaliza a tela principal
            telaMae.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}
