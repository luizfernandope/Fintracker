using FinTracker.Interfaces;
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

namespace FinTracker.AlternativeTelas
{
    public partial class GerirNovosCadastros : Form
    {
        List<Admin> solicitacoes;
        public GerirNovosCadastros()
        {
            InitializeComponent();
            carreagarLista();
        }

        public async void carreagarLista()
        {
            solicitacoes = await MetodosDB.listarSolicitacoesCadastro();
            if (solicitacoes == null || solicitacoes.Count == 0)
            {
                MessageBox.Show("Nenhuma solicitação de cadastro encontrada.");
                return;
            }
            carregarNovosPedidosDeCadastro();
        }

        public void carregarNovosPedidosDeCadastro()
        {
            flowLayoutPanel1.Controls.Clear(); // Limpa o painel antes de adicionar novos controles
            for (int i = 0; i < solicitacoes.Count; i++)
            {
                int nIdicice = i; // Captura o valor atual de i em uma variável local
                Panel panel = new Panel();
                //nao da pra usar dock top no flow layout panel, de outra forma de o painel ocupar toda a largura do flowLayoutPanel
                panel.Size = new Size(flowLayoutPanel1.Width - 40, 80); // Define a largura do painel
                
                panel.BorderStyle = BorderStyle.FixedSingle;

                Label label = new Label();
                label.Text = $"{solicitacoes[nIdicice].GetNome()}\n\n{solicitacoes[nIdicice].GetEmail()}";
                label.Size = new Size(panel.Width - 170, panel.Height); // Define a largura do label
                label.TextAlign = ContentAlignment.MiddleLeft;

                Button btnAprovar = new Button();
                btnAprovar.Text = "Aprovar";
                btnAprovar.Dock = DockStyle.Right;
                btnAprovar.Click += (sender, e) => aceitarSolicitacao(solicitacoes[nIdicice], panel);

                Button btnRejeitar = new Button();
                btnRejeitar.Text = "Rejeitar";
                btnRejeitar.Dock = DockStyle.Right;
                btnRejeitar.Click += (sender, e) => rejeitarSolicitacao(solicitacoes[nIdicice], panel);

                panel.Controls.Add(label);
                panel.Controls.Add(btnAprovar);
                panel.Controls.Add(btnRejeitar);

                flowLayoutPanel1.Controls.Add(panel);
            }
        }

        public async void aceitarSolicitacao(Admin admin, Panel panel)
        {
            bool resultado = await MetodosDB.aceitarCadastro(admin);
            if (resultado)
            {
                MessageBox.Show($"Solicitacao de cadastro aprovada!");
                flowLayoutPanel1.Controls.Remove(panel); // Remove o painel da lista de solicitações
            }
        }
        public async void rejeitarSolicitacao(Admin admin, Panel panel)
        {
            bool resultado = await MetodosDB.rejeitarCadastro(admin.GetId_Admin());
            if (resultado)
            {
                MessageBox.Show($"Solicitacao de cadastro negada!");
                flowLayoutPanel1.Controls.Remove(panel); // Remove o painel da lista de solicitações
            }
        }
    }
}
